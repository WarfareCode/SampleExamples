''====================================================='
''  Copyright 2005, Analytical Graphics, Inc.          '
''====================================================='

#Region "TDRS SRP Model Description"
'-----------------------------------------------------------------------------------
' Intended use:	Computes SRP acceleration for a TDRS spacecraft in orbit using
'				the Pechenick model as described in "TDRS Solar Pressure Model
'				For Filtering", Pechenick, K. and Hujsak, R., Applied
'				Technology Associates Of Delaware, Inc., Dec 1987.
'
'				The intended use is such that CR = 1.0 should be an
'				approximately correct filter input.
'
'				The model computes diffuse and specular reflection 
'				contributions. Contributions are even made in directions
'				orthogonal to the sunlight vector (called "sailing" for solar 
'				pressure).
'
'  Caveat Emptor: The TDRS satellite is geosynchronous, with the solar panel 
'				extending on a mast from the crosstrack surface of the 
'				satellite. Additionally, there is a solar sail, a AW C-band 
'				antenna, and a SGL antenna that are nominally earth-pointing.
'				This model takes advantage of these facts in several
'				places. Any modification for other satellites should be 
'				careful of the assumption.
'
'	Special Note:  ODTK option for "diffuse reflecting sphere" should NOT 
'				be used because it multiplies the CR by 1 + 4./9.
'-----------------------------------------------------------------------------------
'	This model uses coefficients of specular and diffuse reflection for flat 
'		plates and is designed to facilitate ODTK adaptability. This adds some
'		complexity that a typical STK/HPOP user might not implement
'
'	1. The satellite should be configured to compute the SRP acceleration 
'	   so that all necessary SRP-related variables are computed
'
'	2. The entire SRP contribution to the acceleration, as computed by HPOP 
'	   internally, will be subtracted out
'
'	3. The SRP acceleration contribution, as computed by the Pechenick model,
'	   will then be added
'
'--------------------SOLAR PRESSURE DETAIL-----------------------------------------
'
'	The standard solar pressure model for a sphere is of the form
'		accel(sphere) = CR * X1 * k
'           X1 = A/M * Illum * Irrad / c
'		where CR = 1.0 for a perfectly reflective sphere
'				A = Area, M = Mass, 
'				Illum = illumination factor (0 <= Illum <= 1 )
'				Irrad = irradiance in Watts/Meter^2 
'					  = solar flux = Luminosity/(4*pi*distance_sunFromSat^2),
'				c = speed of light
'				k = unit vector from sun to satellite
'	For the Pechenick model there are three acceleration directions: 
'				along k
'				along k x ( k x N ) [N is normal to solar panel surface
'									 and arises from solar panels]
'				along k x ( k x M ) [M is radial is arises from some earth
'									 pointing sub-structures]
'	and the model has the form:
'		accel = CR * ( a1 * k + a2 * k x ( k x N ) + a3 * k x ( k x M ) ) 
'						* Illum * Irrad /c/M
'
'-----------------------------------------------------------------------------------
#End Region

Option Strict Off
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.Win32.RegistryKey

Imports AGI.Attr
Imports AGI.Plugin
Imports AGI.Hpop.Plugin


' NOTE: Generate your own Guid using Microsoft's GuidGen.exe
' NOTE: Create your own ProgId to match your plugin's namespace and name
' NOTE: Specify the ClassInterfaceType.None enumeration, so the custom COM Interface 
' you created, i.e. IExample1, is used instead of an autogenerated COM Interface.
<JustInTimeActivation(True), _
GuidAttribute("5E8E9636-A969-4b4c-BE3A-C9F15014D997"), _
ProgId("AGI.Hpop.Plugin.Examples.Stk.ForceModeling.Srp.VB_NET.Example1"), _
ClassInterface(ClassInterfaceType.None)> _
Public Class Example1
    Implements IExample1
    Implements AGI.Hpop.Plugin.IAgAsHpopPlugin
    Implements IAgUtPluginConfig

#Region "Tuple3 class (used for vector operations) "
    Public Class Tuple3
        Public x As Double
        Public y As Double
        Public z As Double

        Public Sub New()
            x = 0.0
            y = 0.0
            z = 0.0
        End Sub

        Public Sub New(ByVal aX As Double, ByVal aY As Double, ByVal aZ As Double)

            x = aX
            y = aY
            z = aZ

        End Sub

        Public Sub New(ByVal a As Tuple3)
            x = a.x
            y = a.y
            z = a.z
        End Sub

        Public Sub Tuple3(ByVal a As Tuple3)
            x = a.x
            y = a.y
            z = a.z
        End Sub

        Public Sub scaleBy(ByVal val As Double)
            x = x * val
            y = y * val
            z = z * val
        End Sub

        Public Sub addTo(ByVal a As Tuple3)
            x = x + a.x
            y = y + a.y
            z = z + a.z
        End Sub

    End Class

    Public Function dotProduct(ByVal a As Tuple3, ByVal b As Tuple3) As Double
        Dim dotp As Double
        dotp = a.x * b.x + a.y * b.y + a.z * b.z
        Return dotp
    End Function

    Public Function magnitude(ByVal a As Tuple3) As Double
        Dim mag As Double
        mag = Math.Sqrt(dotProduct(a, a))
        Return mag
    End Function

    Public Function normalize(ByRef a As Tuple3) As Double
        Dim mag As Double
        mag = magnitude(a)
        If (mag > 0.0) Then
            a.x = a.x / mag
            a.y = a.y / mag
            a.z = a.z / mag
        End If

        Return mag
    End Function

    Public Sub crossProduct(ByVal a As Tuple3, ByVal b As Tuple3, ByRef c As Tuple3)

        c.x = a.y * b.z - a.z * b.y
        c.y = a.z * b.x - a.x * b.z
        c.z = a.x * b.y - a.y * b.x
    End Sub

#End Region

#Region "Plugin Private Data Members"

    Private Shared m_SolarPanelArrayArea As Double = 29.518 'meters^2 (A_P_1)
    Private Shared m_OtherArea As Double = 6.454    'meters^2 (A_P_2)

    Private m_UPS As IAgUtPluginSite
    Private m_Scope As Object

    Private m_BusRadius As Double
    Private m_SA_AntennaRadius As Double

    Private m_SpeedOfLight As Double
    Private m_SpacecraftMass As Double

    Private m_BusTerm As Double
    Private m_SA_Term As Double
    Private m_BP1Term As Double
    Private m_BP2Term As Double

    Private m_MsgCntr As Integer
    Private m_EvalMsgsOn As Boolean

    Private m_SunlightSRP As Tuple3

#End Region

#Region "Life Cycle Methods"

    ' Default constructor.
    Public Sub New()
        MyBase.New()

        m_BusRadius = 1.157         ' effective radius in meters (R_tilde_b)
        m_SA_AntennaRadius = 0.945  ' effective radius in meters (R_tidle_E)

        m_SpeedOfLight = 299792458.0
        m_SpacecraftMass = 1764.17

        m_BusTerm = 0.0
        m_SA_Term = 0.0
        m_BP1Term = 0.0
        m_BP2Term = 0.0

        m_MsgCntr = -1
        m_EvalMsgsOn = False

        m_SunlightSRP = New Tuple3(0.0, 0.0, 0.0)

        Call SetAttributeConfigDefaults()

    End Sub

    '/// <summary>
    '/// Initializes the Plugin Attribute configuration 
    '/// Data Members to their defaults
    '/// </summary>
    Public Sub SetAttributeConfigDefaults()
        '/===========================
        '// General Plugin attributes
        '//===========================

        m_Name = "Hpop.FrcMdl.Srp.VB_Net.Example1"
        m_Enabled = True
        m_DebugMode = False

        m_SpecularReflectivity = 0.25
        m_DiffuseReflectivity = 0.75

        m_MsgInterval = 500

    End Sub
#End Region

#Region "Messaging Code"

    Private Sub Message(ByVal severity As AgEUtLogMsgType, ByVal msgStr As String)

        If (IsReference(m_UPS)) Then
            m_UPS.Message(severity, msgStr)
        End If

    End Sub

    Private Sub DebugMsg(ByVal msgStr As String)

        If (m_DebugMode And m_EvalMsgsOn) Then
            If (m_MsgCntr Mod m_MsgInterval = 0) Then
                Message(AgEUtLogMsgType.eUtLogMsgDebug, msgStr)
            End If
        End If

    End Sub

#End Region

#Region "SRP computation"

    Private Sub computeSRP(ByVal illum As Double, ByVal cr As Double, ByVal solarFlux As Double, _
                            ByVal pos As Tuple3, ByVal vel As Tuple3, ByVal sunPos As Tuple3, _
                            ByRef sunlightSRP As Tuple3, ByRef sailingSRP As Tuple3)

        '
        ' Begin computation of SRP using the Pechenick model
        '

        ' Compute some reference vectors

        Dim w As New Tuple3

        ' compute unit_w
        crossProduct(pos, vel, w)
        normalize(w)

        ' compute unit_r
        Dim r As New Tuple3(pos)
        normalize(r)

        ' compute unit_k
        Dim k As New Tuple3(sunPos)
        Dim distance_SunToSat As Double
        distance_SunToSat = normalize(k)
        k.scaleBy(-1.0)

        'DebugMsg("DistSun "&distance_SunToSat);
        'DebugMsg("k : ("&k.x&", "&k.y&", "&k.z&")");

        ' compute unit_c
        Dim c As New Tuple3
        crossProduct(w, k, c)
        normalize(c) ' unit vector in orbit plane in plane of solar array (c_hat)

        ' compute unit_n
        Dim n As New Tuple3
        crossProduct(w, c, n)
        normalize(n) ' unit vector in orbit plane normal to solar panel face towards sun
        ' (solar panel faces sun_to_sat direction)

        'DebugMsg("n : ("&n.x&", "&n.y&", "&n.z&")");

        ' compute unit_m
        Dim m As New Tuple3(r)
        Dim dotP As Double
        dotP = dotProduct(r, k)

        If (dotP >= 0.0) Then
            dotP = -1.0
        Else
            dotP = 1.0
        End If

        m.scaleBy(dotP)

        'DebugMsg("m : ("&m.x&", "&m.y&", "&m.z&")");

        Dim temp As New Tuple3      ' temp vector

        'compute k x (k x n)
        Dim kkn As New Tuple3

        crossProduct(k, n, temp)
        crossProduct(k, temp, kkn)

        'DebugMsg("kkn : ("&kkn.x&", "&kkn.y&", "&kkn.z&")");

        'compute k x (k x m)
        Dim kkm As New Tuple3

        crossProduct(k, m, temp)
        crossProduct(k, temp, kkm)

        'DebugMsg("kkm : ("&kkm.x&", "&kkm.y&", "&kkm.z&")");

        ' compute angles

        Dim CosAlpha As Double
        Dim Cos2Alpha As Double
        Dim CosAlphaStar As Double
        Dim Cos2AlphaStar As Double

        CosAlpha = -1.0 * dotProduct(n, k) ' n_hat and k_hat are almost anti-aligned
        Cos2Alpha = 2.0 * CosAlpha * CosAlpha - 1.0

        CosAlphaStar = -1.0 * dotProduct(m, k)
        Cos2AlphaStar = 2.0 * CosAlphaStar * CosAlphaStar - 1.0

        'DebugMsg("Cos(alpha) = "&CosAlpha&", Cos(alphaStar) = "&cosAlphaStar);

        ' compute some aux qtys

        ' NOTE: we are using the formulas here with the app providing Luminosity (thru the solar flux value),
        '		Mass, Cr, spacecraft mass, speed of light. Thus, the value for B_tilde_P_i and C_tilde
        '		from the paper will be computed on the fly, rather than being assumed constant
        '
        '		Also, there is a typo in formula (3.1) on page 5: in that formula, r 
        '		means magnitude(sun_to_Sat_vector)


        ' NOTE: Cr is applied only to the k direction, not kkn nor kkm

        Dim C_Term As Double
        Dim B_P_1_Term As Double
        Dim B_P_2_Term As Double
        Dim tempVal As Double

        C_Term = cr * illum * solarFlux * (m_BusTerm + 2 * m_SA_Term)
        B_P_1_Term = illum * solarFlux * m_BP1Term
        B_P_2_Term = illum * solarFlux * m_BP2Term

        ' compute sailing srp contributions

        tempVal = B_P_1_Term * CosAlpha * (2.0 + m_SpecularReflectivity * (6.0 * CosAlpha - 2.0))

        Dim B_P_1_Term_Contrib_kkn As New Tuple3(kkn)
        B_P_1_Term_Contrib_kkn.scaleBy(tempVal)

        tempVal = B_P_2_Term * CosAlphaStar * (2.0 + m_SpecularReflectivity * (6.0 * CosAlphaStar - 2.0))

        Dim B_P_2_Term_Contrib_kkm As New Tuple3(kkm)
        B_P_2_Term_Contrib_kkm.scaleBy(tempVal)

        sailingSRP.scaleBy(0.0)
        sailingSRP.addTo(B_P_1_Term_Contrib_kkn)
        sailingSRP.addTo(B_P_2_Term_Contrib_kkm)

        DebugMsg("Sailing SRP = (" & sailingSRP.x & ", " & sailingSRP.y & ", " & sailingSRP.z & ")")

        ' compute sunlight srp contributions

        Dim C_Term_Contrib As New Tuple3(k)
        C_Term_Contrib.scaleBy(C_Term)

        tempVal = cr * B_P_1_Term * CosAlpha * ((3.0 + 2.0 * CosAlpha) + _
                    m_SpecularReflectivity * (3.0 * Cos2Alpha - 2.0 * CosAlpha))

        Dim B_P_1_Term_Contrib_k As New Tuple3(k)
        B_P_1_Term_Contrib_k.scaleBy(tempVal)

        tempVal = cr * B_P_2_Term * CosAlphaStar * ((3.0 + 2.0 * CosAlphaStar) + _
          m_SpecularReflectivity * (3.0 * Cos2AlphaStar - 2.0 * CosAlphaStar))

        Dim B_P_2_Term_Contrib_k As New Tuple3(k)
        B_P_2_Term_Contrib_k.scaleBy(tempVal)

        sunlightSRP.scaleBy(0.0)
        sunlightSRP.addTo(C_Term_Contrib)
        sunlightSRP.addTo(B_P_1_Term_Contrib_k)
        sunlightSRP.addTo(B_P_2_Term_Contrib_k)

        DebugMsg("Sunlight SRP = (" & sunlightSRP.x & ", " & sunlightSRP.y & ", " & sunlightSRP.z & ")")
    End Sub

#End Region

#Region "IExample1 Interface Implementation"
    ''=============================================
    '' Plugin Attributes to be configured
    '' will be exposed via .NET properties
    '' and using the Attribute Builder reference
    '' passed as a parameter to the GetPluginConfig
    '' Method.
    ''==============================================

    Private m_Name As String
    Private m_Enabled As Boolean
    Private m_DebugMode As Boolean

    Private m_SpecularReflectivity As Double
    Private m_DiffuseReflectivity As Double

    Private m_MsgInterval As Integer

    Public ReadOnly Property Name() As String Implements IAgAsHpopPlugin.Name
        Get
            Return m_Name
        End Get
    End Property

    Public Property MyName() As String Implements IExample1.MyName
        Get
            Return m_Name
        End Get
        Set(ByVal Value As String)
            m_Name = Value
        End Set
    End Property

    Public Property Enabled() As Boolean Implements IExample1.Enabled
        Get
            Return m_Enabled
        End Get
        Set(ByVal Value As Boolean)
            m_Enabled = Value
        End Set
    End Property

    Public Property DebugMode() As Boolean Implements IExample1.DebugMode
        Get
            Return m_DebugMode
        End Get
        Set(ByVal Value As Boolean)
            m_DebugMode = Value
        End Set
    End Property

    Public Property SpecularReflectivity() As Double Implements IExample1.SpecularReflectivity
        Get
            Return m_SpecularReflectivity
        End Get
        Set(ByVal Value As Double)
            m_SpecularReflectivity = Value
        End Set
    End Property

    Public Property DiffuseReflectivity() As Double Implements IExample1.DiffuseReflectivity
        Get
            Return m_DiffuseReflectivity
        End Get
        Set(ByVal Value As Double)
            m_DiffuseReflectivity = Value
        End Set
    End Property

    Public Property MsgInterval() As Integer Implements IExample1.MsgInterval
        Get
            Return m_MsgInterval
        End Get
        Set(ByVal Value As Integer)
            m_MsgInterval = Value
        End Set
    End Property

#End Region

#Region "IAgAsHpopPlugin Interface Implementation"
    Public Function Init(ByVal Ups As AGI.Plugin.IAgUtPluginSite) As Boolean Implements AGI.Hpop.Plugin.IAgAsHpopPlugin.Init

        Try
            Debug.WriteLine("--> Entered", m_Name & ".Init()")

            m_UPS = Ups

            If (IsReference(m_UPS)) Then
                If (m_DebugMode) Then
                    If (m_Enabled) Then
                        Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & " Enabled in " & m_Name & ".Init()")
                    Else
                        Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name + " Disabled in " & m_Name & ".Init()")
                    End If
                End If
            Else
                Throw New Exception("UtPluginSite was null")
            End If

        Catch ex As Exception
            m_Enabled = False

            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".Init(): Exception Message( " & ex.Message & " )")
            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".Init(): Exception StackTr( " & ex.StackTrace & " )")

            Debug.WriteLine("Exception Message( " & ex.Message & " )", m_Name & ".Init()")
            Debug.WriteLine("Exception StackTr( " & ex.StackTrace & " )", m_Name & ".Init()")
        End Try

        Return m_Enabled
    End Function

    Public Function PrePropagate(ByVal Result As IAgAsHpopPluginResult) As Boolean Implements IAgAsHpopPlugin.PrePropagate

        Try
            Debug.WriteLine("--> Entered", m_Name & ".PrePropagate()")

            If (m_Enabled) Then

                ' Insure that SRP is On
                Dim srpIsOn As Boolean
                srpIsOn = Result.IsForceModelOn(AgEForceModelType.eSRPModel)

                If (Not srpIsOn) Then
                    m_Enabled = False

                    Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PrePropagate(): SRP must be ON for this plugin to work but is currently OFF.")
                    Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PrePropagate(): Turning OFF all methods for " & m_Name)

                    Return m_Enabled
                End If

                ' compute terms that don't change over time

                m_SpeedOfLight = Result.LightSpeed
                m_SpacecraftMass = Result.Mass

                Dim tempVal As Double
                tempVal = m_SpacecraftMass * m_SpeedOfLight

                If (m_DebugMode) Then
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PrePropagate(): Mass = " & m_SpacecraftMass)
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PrePropagate(): c = " & m_SpeedOfLight)
                End If

                m_BusTerm = Math.PI * m_BusRadius * m_BusRadius * _
                  (1.0 + 4.0 / 9.0 * m_DiffuseReflectivity) / tempVal

                m_SA_Term = Math.PI * m_SA_AntennaRadius * m_SA_AntennaRadius * _
                 (1.0 + 4.0 / 9.0 * m_DiffuseReflectivity) / tempVal

                m_BP1Term = m_SolarPanelArrayArea / (3.0 * tempVal)
                m_BP2Term = m_OtherArea / (3.0 * tempVal)

                If (m_DebugMode) Then
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PrePropagate(): BP1Term = " & m_BP1Term)
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PrePropagate(): BP2Term = " & m_BP2Term)
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PrePropagate(): Bus_Term = " & m_BusTerm)
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PrePropagate(): SA_Term = " & m_SA_Term)
                End If

            ElseIf (m_DebugMode) Then
                Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".Prepropagate(): Disabled")
            End If

        Catch ex As Exception
            m_Enabled = False

            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PrePropagate(): Exception Message( " & ex.Message & " )")
            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PrePropagate(): Exception StackTr( " & ex.StackTrace & " )")

            Debug.WriteLine("Exception Message( " & ex.Message & " )", m_Name & ".PrePropagate()")
            Debug.WriteLine("Exception StackTr( " & ex.StackTrace & " )", m_Name & ".PrePropagate()")

        End Try
        Return m_Enabled
    End Function

    Public Function PreNextStep(ByVal Result As IAgAsHpopPluginResult) As Boolean Implements IAgAsHpopPlugin.PreNextStep

        Try
            m_MsgCntr = m_MsgCntr + 1

            If (m_Enabled) Then
                If (m_DebugMode) Then
                    If ((m_MsgCntr Mod m_MsgInterval) = 0) Then
                        Dim deltaT As Double
                        deltaT = Result.TimeSinceRefEpoch

                        Message(AgEUtLogMsgType.eUtLogMsgDebug, "PreNextStep( " & m_MsgCntr & " ): Time since Ref Epoch = " & deltaT & " secs")
                    End If
                End If
            ElseIf (m_DebugMode = True) Then
                Message(AgEUtLogMsgType.eUtLogMsgDebug, "PreNextStep(): Disabled")
            End If

        Catch ex As Exception
            m_Enabled = False

            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PreNextStep(): Exception Message( " & ex.Message & " )")
            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PreNextStep(): Exception StackTr( " & ex.StackTrace & " )")

            Debug.WriteLine("Exception Message( " + ex.Message + " )", m_Name & ".PreNextStep()")
            Debug.WriteLine("Exception StackTr( " + ex.StackTrace + " )", m_Name & ".PreNextStep()")

        End Try

        m_EvalMsgsOn = True

        Return m_Enabled
    End Function

    Public Function Evaluate(ByVal ResultEval As IAgAsHpopPluginResultEval) As Boolean Implements IAgAsHpopPlugin.Evaluate

        Try
            If (m_MsgCntr Mod m_MsgInterval = 0) Then
                Debug.WriteLine("--> Entered", m_Name & ".Evaluate( " & m_MsgCntr & " )")
            End If

            If (m_Enabled) Then
                ' if illumination is zero, there isn't any contribution anyway, so do nothing

                Dim illum As Double
                ' SRP must be on else this call throws an exception
                illum = ResultEval.SolarIntensity

                If (illum = 0.0) Then
                    Return m_Enabled
                End If

                Dim cr As Double
                Dim solarFlux As Double
                Dim r As New Tuple3
                Dim v As New Tuple3
                Dim sunPos As New Tuple3

                cr = ResultEval.Cr
                ' SRP must be on else this call throws an exception
                solarFlux = ResultEval.SolarFlux  ' L /(4 * pi * R_sun^2)

                ResultEval.PosVel(AgEUtFrame.eUtFrameInertial, r.x, r.y, r.z, v.x, v.y, v.z)

                ' SRP must be on else this call throws an exception because of AgEUtSunPosType.eUtSunPosTypeSRP
                ResultEval.SunPosition(AgEUtSunPosType.eUtSunPosTypeSRP, AgEUtFrame.eUtFrameInertial, sunPos.x, sunPos.y, sunPos.z)

                Dim sailingSRP As New Tuple3

                computeSRP(illum, cr, solarFlux, r, v, sunPos, m_SunlightSRP, sailingSRP)

                ' For OD to be able to estimate Cr, we need HPOP to compute SRP itself (i.e., the sunlight portion)
                ' but of course we want HPOP to compute the value that we just computed
                '
                ' THUS, for the sunlight portion, we'll modify the SRPArea to make this happen

                Dim magnitude As Double
                magnitude = dotProduct(m_SunlightSRP, m_SunlightSRP)
                magnitude = Math.Sqrt(magnitude)

                ResultEval.SRPArea = magnitude / (cr * solarFlux * illum / (m_SpacecraftMass * m_SpeedOfLight))

                ' add sailing SRP contribution

                ResultEval.AddAcceleration(AgEUtFrame.eUtFrameInertial, sailingSRP.x, sailingSRP.y, sailingSRP.z)

            ElseIf (m_DebugMode) Then
                Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".Evaluate(): Disabled")
            End If

        Catch ex As Exception
            m_Enabled = False

            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".Evaluate(): Exception Message( " & ex.Message & " )")
            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".Evaluate(): Exception StackTr( " & ex.StackTrace & " )")

            Debug.WriteLine("Exception Message( " & ex.Message & " )", m_Name & ".Evaluate()")
            Debug.WriteLine("Exception StackTr( " & ex.StackTrace & " )", m_Name & ".Evaluate()")

        End Try

        Return m_Enabled

    End Function

    Public Function PostEvaluate(ByVal ResultEval As IAgAsHpopPluginResultPostEval) As Boolean Implements IAgAsHpopPlugin.PostEvaluate

        Try
            If (m_MsgCntr Mod m_MsgInterval = 0) Then
                Debug.WriteLine("--> Entered", m_Name & ".PostEvaluate( " & m_MsgCntr & " )")
            End If
            If (m_Enabled And m_DebugMode) Then
                ' get SRP acceleration from HPOP

                Dim srpAccel As New Tuple3(0.0, 0.0, 0.0)

                ResultEval.GetAcceleration(AgEAccelType.eSRPAccel, AgEUtFrame.eUtFrameInertial, _
                       srpAccel.x, srpAccel.y, srpAccel.z)

                DebugMsg("HPOP computed Sunlight SRP = (" & srpAccel.x & ", " & srpAccel.y & ", " & srpAccel.z & ")")

                srpAccel.scaleBy(-1.0)
                srpAccel.addTo(m_SunlightSRP)

                DebugMsg("Difference in Sunlight SRP = (" & srpAccel.x & ", " & srpAccel.y & ", " & srpAccel.z & ")")

                Dim cr As Double
                Dim dragArea As Double
                Dim solarFlux As Double
                Dim illum As Double

                cr = ResultEval.Cr
                dragArea = ResultEval.DragArea
                solarFlux = ResultEval.SolarFlux
                illum = ResultEval.SolarIntensity

                DebugMsg("Area = " & dragArea & ", Flux = " & solarFlux & ", Cr = " & cr & ",  Mass = " & m_SpacecraftMass & ", Illum = " & illum)

            ElseIf (m_DebugMode) Then
                Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PostEvaluate(): Disabled")
            End If

        Catch ex As Exception
            m_Enabled = False

            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PostEvaluate(): Exception Message( " & ex.Message & " )")
            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PostEvaluate(): Exception StackTr( " & ex.StackTrace & " )")

            Debug.WriteLine("Exception Message( " & ex.Message & " )", m_Name & ".PostEvaluate()")
            Debug.WriteLine("Exception StackTr( " & ex.StackTrace & " )", m_Name & ".PostEvaluate()")

        End Try

        m_EvalMsgsOn = False

        Return m_Enabled
    End Function

    Public Function PostPropagate(ByVal Result As IAgAsHpopPluginResult) As Boolean Implements IAgAsHpopPlugin.PostPropagate

        Try
            Debug.WriteLine("--> Entered", m_Name & ".PostPropagate()")

            If (m_Enabled) Then
                If (m_DebugMode) Then
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PostPropagate():")
                End If
            Else
                If (m_DebugMode) Then
                    Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".PostPropagate(): Disabled")
                End If
            End If

        Catch ex As Exception

            m_Enabled = False

            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PostPropagate(): Exception Message( " & ex.Message & " )")
            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".PostPropagate(): Exception StackTr( " & ex.StackTrace & " )")

            Debug.WriteLine("Exception Message( " + ex.Message + " )", m_Name & ".PostPropagate()")
            Debug.WriteLine("Exception StackTr( " + ex.StackTrace + " )", m_Name & ".PostPropagate()")

        End Try

        Return m_Enabled

    End Function

    Public Sub Free() Implements IAgAsHpopPlugin.Free

        Try
            Debug.WriteLine("--> Entered", m_Name & ".Free()")

            If (m_DebugMode) Then
                Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".Free():")
                Message(AgEUtLogMsgType.eUtLogMsgDebug, m_Name & ".Free(): MsgCntr( " & m_MsgCntr & " )")
            End If

            If (IsReference(m_UPS)) Then
                Marshal.ReleaseComObject(m_UPS)
                m_UPS = Nothing
            End If
        Catch ex As Exception
            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".Free(): Exception Message( " & ex.Message & " )")
            Message(AgEUtLogMsgType.eUtLogMsgAlarm, m_Name & ".Free(): Exception StackTr( " & ex.StackTrace & " )")

            Debug.WriteLine("Exception Message( " & ex.Message + " )", m_Name & ".Free()")
            Debug.WriteLine("Exception StackTr( " & ex.StackTrace + " )", m_Name & ".Free()")
        End Try

    End Sub

#End Region

#Region "IAgUtPluginConfig Interface Implementation"

    Public Function GetPluginConfig(ByVal aab As AgAttrBuilder) As Object Implements IAgUtPluginConfig.GetPluginConfig

        Try
            Debug.WriteLine("--> Entered", m_Name & ".GetPluginConfig()")

            If (IsReference(m_Scope)) Then
                m_Scope = aab.NewScope()

                '//===========================
                '// General Plugin attributes
                '//===========================
                aab.AddStringDispatchProperty(m_Scope, "PluginName", "Human readable plugin name or alias", "MyName", CInt(AgEAttrAddFlags.eAddFlagNone))
                aab.AddBoolDispatchProperty(m_Scope, "PluginEnabled", "If the plugin is enabled or has experience an error", "Enabled", CInt(AgEAttrAddFlags.eAddFlagNone))
                aab.AddBoolDispatchProperty(m_Scope, "DebugMode", "Turn debug messages on or off", "DebugMode", CInt(AgEAttrAddFlags.eAddFlagNone))

                '//===========================
                '// Reflectivity related
                '//===========================

                aab.AddDoubleDispatchProperty(m_Scope, "Reflectivity_Specular", "Specular reflectivity coefficient", "SpecularReflectivity", CInt(AgEAttrAddFlags.eAddFlagNone))
                aab.AddDoubleDispatchProperty(m_Scope, "Reflectivity_Diffuse", "Diffuse reflectivity coefficient", "DiffuseReflectivity", CInt(AgEAttrAddFlags.eAddFlagNone))

                '//===========================
                '// Messaging related attributes
                '//===========================
                aab.AddIntDispatchProperty(m_Scope, "MessageInterval", "The interval at which to send messages during propagation", "MsgInterval", CInt(AgEAttrAddFlags.eAddFlagNone))
            End If

            Dim config As String

            config = aab.ToString(Me, m_Scope)
        Catch
        End Try

        Debug.WriteLine("<-- Exited", m_Name & ".GetPluginConfig()")

        GetPluginConfig = m_Scope
    End Function

    Public Sub VerifyPluginConfig(ByVal apcvr As AgUtPluginConfigVerifyResult) Implements IAgUtPluginConfig.VerifyPluginConfig

        Dim result As Boolean
        Dim message As String

        result = True
        message = "Ok"

        Debug.WriteLine("--> Entered", m_Name & ".VerifyPluginConfig()")

        If (m_SpecularReflectivity < 0.0 Or m_SpecularReflectivity > 1.0) Then
            result = False
            message = "Specular Reflectivity must be between 0.0 and 1.0"
        ElseIf (m_DiffuseReflectivity < 0.0 Or m_DiffuseReflectivity > (1.0 - m_SpecularReflectivity)) Then

            result = False
            message = "Diffuse Reflectivity must be between 0.0 and (1.0 - Specular)"
        End If

        Debug.WriteLine("<-- Exited", m_Name & ".VerifyPluginConfig()")

        apcvr.Result = result
        apcvr.Message = message
    End Sub


#End Region

End Class

''====================================================='
''  Copyright 2005, Analytical Graphics, Inc.          '
''====================================================='
