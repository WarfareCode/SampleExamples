Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.Common
Imports System.Collections
Imports System.Collections.Specialized

Imports AGI.STKUtil
Imports AGI.STKX
Imports AGI.STKObjects
Imports AGI.STKVgt

Public Class VGTTutorial

#Region "Private Data Members"
    Private stkRootObject As AgStkObjectRoot = Nothing
    Private m_satellite As AgSatellite = Nothing
    Private m_facility As IAgFacility = Nothing
    Const m_sViewPointName As String = "ViewPoint"
    Const m_sDistanceVectorName As String = "accessFac"
#End Region

    Private ReadOnly Property stkRoot() As AGI.STKObjects.AgStkObjectRoot
        Get
            If stkRootObject Is Nothing Then
                stkRootObject = New AGI.STKObjects.AgStkObjectRoot()
                AddHandler stkRootObject.OnAnimUpdate, AddressOf stkRootObject_OnAnimUpdate
            End If
            Return stkRootObject
        End Get
    End Property

    Private Sub stkRootObject_OnAnimUpdate(ByVal TimeEpSec As Double)
        If m_satellite IsNot Nothing Then
            Dim provider As IAgCrdnProvider = m_satellite.Vgt
            If provider.Vectors.Contains(m_sDistanceVectorName) Then
                Dim vector As IAgCrdnVector = provider.Vectors(m_sDistanceVectorName)
                Dim result As IAgCrdnVectorFindInAxesResult = vector.FindInAxes(TimeEpSec, provider.WellKnownAxes.Earth.Fixed)
                If result.IsValid Then
                    ' Calculate the vector's L2-norm.
                    Dim cartVec As IAgCartesian3Vector = result.Vector
                    Dim mag As Double = Math.Sqrt(cartVec.X * cartVec.X + cartVec.Y * cartVec.Y + cartVec.Z * cartVec.Z)
                    TextBox1.Text = String.Format("{0} km", (mag / 1000).ToString("F"))
                End If
            End If
        End If
    End Sub
    Private Sub CloseScenario()
        If stkRootObject IsNot Nothing Then
            stkRootObject.CloseScenario()
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub VGTTutorial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MainLabel.Text = My.Resources.Strings.Welcome
    End Sub
    Private Sub CreateScenario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateScenario.Click
        '--Set Instructions--
        MainLabel.Text = My.Resources.Strings.CreateSatellite

        '--Creates new scenario--
        stkRoot.NewScenario("VGTTutorial")
        stkRoot.UnitPreferences.SetCurrentUnit("TimeUnit", "sec")
        stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec")

        Dim scenario As IAgScenario = DirectCast(stkRoot.CurrentScenario, IAgScenario)
        scenario.Animation.AnimStepValue = 5

        '--Enable Next--
        CreateSatellite.Enabled = True
        '--Disable Current--
        CreateScenario.Enabled = False
    End Sub

    Private Sub CreateSatellite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateSatellite.Click
        '--Set Instructions--
        MainLabel.Text = My.Resources.Strings.CreateVector

        '--Create and Propogate m_satellite--
        m_satellite = DirectCast(stkRoot.CurrentScenario.Children.[New](AGI.STKObjects.AgESTKObjectType.eSatellite, "AGILE_31135"), AGI.STKObjects.AgSatellite)
        m_satellite.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorSGP4)
        Dim prop As AGI.STKObjects.IAgVePropagatorSGP4 = DirectCast(m_satellite.Propagator, AGI.STKObjects.IAgVePropagatorSGP4)
        prop.[Step] = 10
        prop.Propagate()

        '--Create Facility--
        m_facility = DirectCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.eFacility, "accessFac"), IAgFacility)
        m_facility.Graphics.LabelVisible = True
        m_facility.VO.Model.Visible = True

        '--Initialize m_satellite--
        m_satellite = DirectCast(stkRoot.CurrentScenario.Children("AGILE_31135"), AgSatellite)

        '--Set vector/angle Scale for appearance--
        m_satellite.VO.Vector.VectorSizeScale = 1.3
        m_satellite.VO.Vector.AngleSizeScale = 1

        '--Initialize _provider as IAgStkObject to access VGT--
        Dim provider As IAgCrdnProvider = m_satellite.Vgt

        '--Set up view point to use with "ZoomTo" function--
        '--Point coordinates relative to provider (m_satellite)--
        Dim viewPoint As IAgCrdnPointFixedInSystem = DirectCast(provider.Points.Factory.Create("ViewPoint", "View sat from this point", AgECrdnPointType.eCrdnPointTypeFixedInSystem), IAgCrdnPointFixedInSystem)
        viewPoint.FixedPoint.AssignCartesian(0.09, -0.1, -0.025)

        '--Enable Next--
        CreateVector.Enabled = True
        '--Disable current--
        CreateSatellite.Enabled = False
        '--Zoom to Satellite--
        ZoomTo()
    End Sub

    Private Sub CreateVector_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateVector.Click
        '--Set Instructions--
        MainLabel.Text = My.Resources.Strings.ShowVelocityVector

        Dim provider As IAgCrdnProvider = m_satellite.Vgt

        stkRoot.BeginUpdate()
        Try
            '--Create Vector to accessFac--
            Dim vDisplacement As IAgCrdnVectorDisplacement = DirectCast(provider.Vectors.Factory.Create(m_sDistanceVectorName, "Vector to facility", AGI.STKVgt.AgECrdnVectorType.eCrdnVectorTypeDisplacement), IAgCrdnVectorDisplacement)
            vDisplacement.Origin.SetPoint(provider.Points("Center"))
            vDisplacement.Destination.SetPoint(TryCast(m_facility, IAgStkObject).Vgt.Points("Center"))
            vDisplacement.Apparent = True

            '--Show the Vectors--
            m_satellite.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eVectorElem, TryCast(vDisplacement, IAgCrdn).QualifiedPath)
            Dim voVectorToFac As IAgVORefCrdnVector = DirectCast(m_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eVectorElem, TryCast(vDisplacement, IAgCrdn).QualifiedPath), IAgVORefCrdnVector)
            voVectorToFac.MagnitudeVisible = True
            voVectorToFac.LabelVisible = True
            voVectorToFac.Color = Color.AliceBlue
        Finally
            stkRoot.EndUpdate()
        End Try
        'Enable Next
        ShowVelocity.Enabled = True
        'Disable Current
        CreateVector.Enabled = False
    End Sub

    Private Sub ShowVelocity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowVelocity.Click
        MainLabel.Text = My.Resources.Strings.CreateAxes

        Dim provider As IAgCrdnProvider = m_satellite.Vgt

        Dim path As String = TryCast(provider.Vectors("Velocity"), IAgCrdn).QualifiedPath

        stkRoot.BeginUpdate()
        Try
            '--Add Predefined Velocity Vector--
            Dim voVelocity As IAgVORefCrdnVector = DirectCast(m_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eVectorElem, path), IAgVORefCrdnVector)
            voVelocity.Visible = True
            voVelocity.MagnitudeVisible = True
            voVelocity.LabelVisible = True
        Finally
            stkRoot.EndUpdate()
        End Try
        '--Disable current--
        ShowVelocity.Enabled = False
        '--Enable next--
        CreateAxes.Enabled = True
    End Sub

    Private Sub CreateAxes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateAxes.Click
        MainLabel.Text = My.Resources.Strings.CreatePlane

        Dim provider As IAgCrdnProvider = m_satellite.Vgt

        Dim path As String = TryCast(provider.Axes("ICR"), IAgCrdn).QualifiedPath

        stkRoot.BeginUpdate()
        Try
            '--Add Predefined Axis--
            m_satellite.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eAxesElem, path)
            Dim voAxes As IAgVORefCrdnAxes = DirectCast(m_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eAxesElem, path), IAgVORefCrdnAxes)
            voAxes.Visible = True
            voAxes.Color = Color.Tomato

            '--Hide velocity label--
            m_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eVectorElem, TryCast(provider.Vectors("Velocity"), IAgCrdn).QualifiedPath).Visible = False
        Finally
            stkRoot.EndUpdate()
        End Try
        '--Show next--
        CreatePlane.Enabled = True
        '--Disable Current--
        CreateAxes.Enabled = False
    End Sub

    Private Sub CreatePlane_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreatePlane.Click
        MainLabel.Text = My.Resources.Strings.CreateAngles

        Dim provider As IAgCrdnProvider = m_satellite.Vgt

        stkRoot.BeginUpdate()
        Try
            '--Create Plane Normal to Velocity Vector--
            Dim plane As IAgCrdnPlaneNormal = DirectCast(provider.Planes.Factory.Create("NormalPlane", "Normal to Velocity vector.", AgECrdnPlaneType.eCrdnPlaneTypeNormal), IAgCrdnPlaneNormal)
            plane.NormalVector.SetVector(provider.Vectors("Velocity"))
            plane.ReferenceVector.SetVector(provider.Vectors("Earth"))
            plane.ReferencePoint.SetPoint(provider.Points("Center"))

            Dim path As String = TryCast(provider.Planes("NormalPlane"), IAgCrdn).QualifiedPath
            '--Add plane to View--
            m_satellite.VO.Vector.RefCrdns.Add(AgEGeometricElemType.ePlaneElem, path)
            Dim voPlane As IAgVORefCrdnPlane = DirectCast(m_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.ePlaneElem, path), IAgVORefCrdnPlane)
            voPlane.TransparentPlaneVisible = True
            voPlane.Color = Color.Yellow
            voPlane.AxisLabelsVisible = True

            '--Add BodyXY Plane--
            Dim pathXY As String = TryCast(provider.Planes("BodyXY"), IAgCrdn).QualifiedPath
            Dim voPlaneBodyXY As IAgVORefCrdnPlane = DirectCast(m_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.ePlaneElem, pathXY), IAgVORefCrdnPlane)
            voPlaneBodyXY.Visible = True
            voPlaneBodyXY.TransparentPlaneVisible = True
            voPlaneBodyXY.Color = Color.Yellow
            voPlaneBodyXY.AxisLabelsVisible = True

            '--Hide Axes Labels--
            path = TryCast(provider.Axes("ICR"), IAgCrdn).QualifiedPath
            Dim axes As IAgVORefCrdnAxes = DirectCast(m_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eAxesElem, path), IAgVORefCrdnAxes)
            axes.LabelVisible = False
        Finally
            stkRoot.EndUpdate()
        End Try
        '--Show next--
        CreateAngle.Enabled = True
        '--Disable Current--
        CreatePlane.Enabled = False
    End Sub

    Private Sub CreateAngle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateAngle.Click
        '--Set Instructions--
        MainLabel.Text = My.Resources.Strings.Finish
        Dim vector3d As IAgVOVector = m_satellite.VO.Vector
        Dim provider As IAgCrdnProvider = m_satellite.Vgt

        stkRoot.BeginUpdate()
        Try
            '--Angle from Normal Plane to XY Plane (trajectory)--   
            Dim anglePlane As IAgCrdnAngleBetweenPlanes = DirectCast(provider.Angles.Factory.Create("AngleBetweenPlanes", "Angle from PlaneXY to NormalPlane", AgECrdnAngleType.eCrdnAngleTypeBetweenPlanes), IAgCrdnAngleBetweenPlanes)
            anglePlane.FromPlane.SetPlane(provider.Planes("BodyXY"))
            anglePlane.ToPlane.SetPlane(provider.Planes("NormalPlane"))

            '--Add to View--
            vector3d.RefCrdns.Add(AgEGeometricElemType.eAngleElem, DirectCast(anglePlane, IAgCrdn).QualifiedPath)
            Dim voAnglePlane As IAgVORefCrdnAngle = DirectCast(vector3d.RefCrdns.GetCrdnByName(AgEGeometricElemType.eAngleElem, DirectCast(anglePlane, IAgCrdn).QualifiedPath), IAgVORefCrdnAngle)
            voAnglePlane.AngleValueVisible = True

            '--Angle from Velocity/Trajector vector to AccessFacility vector--
            Dim angleVector As IAgCrdnAngleBetweenVectors = DirectCast(provider.Angles.Factory.Create("AngleToVector", "Angle from Vector to Plane", AgECrdnAngleType.eCrdnAngleTypeBetweenVectors), IAgCrdnAngleBetweenVectors)
            angleVector.FromVector.SetVector(provider.Vectors("Velocity"))
            angleVector.ToVector.SetVector(provider.Vectors(m_sDistanceVectorName))

            '--Add to View--
            vector3d.RefCrdns.Add(AgEGeometricElemType.eAngleElem, DirectCast(angleVector, IAgCrdn).QualifiedPath)
            Dim voAngleVector As IAgVORefCrdnAngle = DirectCast(vector3d.RefCrdns.GetCrdnByName(AgEGeometricElemType.eAngleElem, DirectCast(angleVector, IAgCrdn).QualifiedPath), IAgVORefCrdnAngle)
            voAngleVector.AngleValueVisible = True
            voAngleVector.Color = Color.SpringGreen

            '--Angle from AccessFacility vector to Plane--
            Dim anglePlaneFac As IAgCrdnAngleToPlane = DirectCast(provider.Angles.Factory.Create("AngleFromFac", "Angle from Plane to Vector", AgECrdnAngleType.eCrdnAngleTypeToPlane), IAgCrdnAngleToPlane)
            anglePlaneFac.ReferenceVector.SetVector(provider.Vectors(m_sDistanceVectorName))
            anglePlaneFac.ReferencePlane.SetPlane(provider.Planes("NormalPlane"))
            '--Add to View--
            m_satellite.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eAngleElem, TryCast(anglePlaneFac, IAgCrdn).QualifiedPath)
            Dim voAngleFac As IAgVORefCrdnAngle = DirectCast(vector3d.RefCrdns.GetCrdnByName(AgEGeometricElemType.eAngleElem, TryCast(anglePlaneFac, IAgCrdn).QualifiedPath), IAgVORefCrdnAngle)
            voAngleFac.AngleValueVisible = True
        Finally
            stkRoot.EndUpdate()
        End Try
        '--Show next--
        CloseScenario1.Enabled = True
        '--Disable current--
        CreateAngle.Enabled = False
    End Sub

    Private Sub CloseScenario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseScenario1.Click
        CloseScenario()
        Application.[Exit]()
    End Sub

    Private Sub Button_ResetAnim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ResetAnim.Click
        stkRoot.Rewind()
    End Sub

    Private Sub Button_StepBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_StepBack.Click
        stkRoot.StepBackward()
    End Sub

    Private Sub Button_ReverseAnim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ReverseAnim.Click
        stkRoot.PlayBackward()
    End Sub

    Private Sub Button_Pause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Pause.Click
        stkRoot.Pause()
    End Sub

    Private Sub Button_Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Play.Click
        stkRoot.PlayForward()
    End Sub

    Private Sub Button_StepAhead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_StepAhead.Click
        stkRoot.StepForward()
    End Sub

    Private Sub Button_SlowDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SlowDown.Click
        stkRoot.Slower()
    End Sub

    Private Sub Button_SpeedUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SpeedUp.Click
        stkRoot.Faster()
    End Sub
    Private Sub ZoomTo()
        Dim viewPoint As IAgCrdnPointFixedInSystem = DirectCast(m_satellite.Vgt.Points(m_sViewPointName), IAgCrdnPointFixedInSystem)
        '--Zoom to m_satellite--
        Dim zoomCmd As String = String.Format("VO * View FromTo FromRegName ""STK Object"" FromName ""{1}"" ToRegName ""STK Object"" ToName ""{0}""", String.Format("{0}/{1}", m_satellite.ClassName, m_satellite.InstanceName), DirectCast(viewPoint, IAgCrdn).QualifiedPath)
        stkRootObject.ExecuteCommand(zoomCmd)
    End Sub
    Private Sub Unload(ByVal sender As Object, ByVal e As EventArgs)
        CloseScenario()
        Application.[Exit]()
    End Sub
End Class
