Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt

Public MustInherit Class CodeSnippet
    Protected Sub New(ByVal filePath As String)
        Dim codeSnippetsPath As String = Path.Combine(Application.StartupPath, "CodeSnippets")
        Dim sourceFilePath As String = Path.Combine(codeSnippetsPath, filePath)
        m_reader = New CodeSnippetReader(sourceFilePath)
    End Sub
    Public MustOverride Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
    Public MustOverride Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
    Public MustOverride Sub Remove(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
    Public ReadOnly Property Code() As String
        Get
            Return m_reader.Code
        End Get
    End Property
    Public ReadOnly Property UsingDirectives() As String
        Get
            Return m_reader.UsingDirectives
        End Get
    End Property
    Public ReadOnly Property FileName() As String
        Get
            Return m_reader.FileName
        End Get
    End Property
    Public ReadOnly Property FilePath() As String
        Get
            Return m_reader.FilePath
        End Get
    End Property

    Public Function CreatePosition(ByVal root As AgStkObjectRoot, ByVal lat As Double, ByVal lon As Double, ByVal alt As Double) As IAgPosition
        Dim pos As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
        pos.AssignPlanetodetic(lat, lon, alt)
        Return pos
    End Function

    Public Shared Function CreateAxes(ByVal root As AgStkObjectRoot, ByVal centralBodyName As String, ByVal pos As IAgPosition) As IAgCrdnAxesFixed
        Dim provider As IAgCrdnProvider = root.CentralBodies(centralBodyName).Vgt
        Dim fixed As IAgCrdnPointFixedInSystem = DirectCast(VgtHelper.CreatePoint(provider, AgECrdnPointType.eCrdnPointTypeFixedInSystem), IAgCrdnPointFixedInSystem)
        Dim x As Double, y As Double, z As Double
        pos.QueryCartesian(x, y, z)
        fixed.FixedPoint.AssignCartesian(x, y, z)
        fixed.Reference.SetSystem(provider.WellKnownSystems.Earth.Fixed)
        Dim axes As IAgCrdnAxesOnSurface = DirectCast(VgtHelper.CreateAxes(provider, AgECrdnAxesType.eCrdnAxesTypeOnSurface), IAgCrdnAxesOnSurface)
        axes.ReferencePoint.SetPoint(DirectCast(fixed, IAgCrdnPoint))
        axes.CentralBody.SetPath(centralBodyName)
        Dim eastNorthUp As IAgCrdnAxesFixed = DirectCast(VgtHelper.CreateAxes(provider, AgECrdnAxesType.eCrdnAxesTypeFixed), IAgCrdnAxesFixed)
        eastNorthUp.ReferenceAxes.SetAxes(DirectCast(axes, IAgCrdnAxes))
        eastNorthUp.FixedOrientation.AssignEulerAngles(AgEEulerOrientationSequence.e321, 90, 0, 0)
        Return eastNorthUp
    End Function

    Public Shared Function CreateSystem(ByVal root As AgStkObjectRoot, ByVal centralBodyName As String, ByVal pos As IAgPosition, ByVal axes As IAgCrdnAxesFixed) As IAgCrdnSystem
        Dim provider As IAgCrdnProvider = root.CentralBodies(centralBodyName).Vgt
        Dim x As Double, y As Double, z As Double
        Dim point As IAgCrdnPointFixedInSystem = DirectCast(VgtHelper.CreatePoint(provider, AgECrdnPointType.eCrdnPointTypeFixedInSystem), IAgCrdnPointFixedInSystem)
        pos.QueryCartesian(x, y, z)
        point.FixedPoint.AssignCartesian(x, y, z)
        point.Reference.SetSystem(provider.Systems("Fixed"))
        Dim system As IAgCrdnSystemAssembled = DirectCast(VgtHelper.CreateSystem(provider, AgECrdnSystemType.eCrdnSystemTypeAssembled), IAgCrdnSystemAssembled)
        system.OriginPoint.SetPoint(DirectCast(point, IAgCrdnPoint))
        system.ReferenceAxes.SetAxes(DirectCast(axes, IAgCrdnAxes))
        Return DirectCast(system, IAgCrdnSystem)
    End Function

    Public Shared Sub SetAnimationDefaults(ByVal root As AgStkObjectRoot)
        Dim animationControl As IAgAnimation = DirectCast(root, IAgAnimation)
        Dim scenario As IAgScenario = DirectCast(root.CurrentScenario, IAgScenario)
        Dim animationSettings As IAgScAnimation = DirectCast(root.CurrentScenario, IAgScenario).Animation
        animationControl.Rewind()
        scenario.StartTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:00:00.000").Format("epSec"))
        scenario.StopTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "31 May 2008 14:00:00.000").Format("epSec"))
        animationSettings.StartTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:00:00.000").Format("epSec"))


        animationSettings.AnimStepValue = 15.0
        animationSettings.EnableAnimCycleTime = True
        animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime
        animationControl.Rewind()
    End Sub

    Public Shared Function MeasureString(ByVal text As String, ByVal font As System.Drawing.Font) As System.Drawing.Size
        'Graphics.MeasureString() is more accurate than TextRenderer.MeasureText, but it requires a Graphics object
        Using graphics As System.Drawing.Graphics = HowToForm.Instance.Control3D.CreateGraphics()
            Return graphics.MeasureString(text, font).ToSize()
        End Using
    End Function

    Public Shared Function RadiansToDegrees(ByVal radians As Double) As Double
        Return radians * (360.0 / (2 * Math.PI))
    End Function

    Public Shared Function ConvertIListToArray(ByVal positions As IList(Of Array)) As Array
        Dim array__1 As Array = Array.CreateInstance(GetType(Object), positions.Count * 3)
        For i As Integer = 0 To positions.Count - 1
            Dim position As Array = positions(i)
            position.CopyTo(array__1, i * 3)
        Next
        Return array__1
    End Function

    Public Shared Function ConvertIListToArray(ByVal ParamArray positions As Array()) As Array
        Dim array__1 As Array = Array.CreateInstance(GetType(Object), positions.Length * 3)
        For i As Integer = 0 To positions.Length - 1
            Dim position As Array = positions(i)
            position.CopyTo(array__1, i * 3)
        Next
        Return array__1
    End Function

    Public Shared Function attachID(ByVal id As String, ByVal value As String) As String
        Return value
    End Function

    Public Shared Function attachID(ByVal id As String, ByVal value As Double) As Double
        Return value
    End Function

    Public Shared Function attachID(ByVal id As String, ByVal value As Integer) As Integer
        Return value
    End Function
    Public Shared Function attachID(ByVal id As String, ByVal value As Object) As Object
        Return value
    End Function

    Private ReadOnly m_reader As CodeSnippetReader
End Class


Public NotInheritable Class VgtHelper
    Private Sub New()
    End Sub

    Shared _counter As Integer = 0
    Public Enum VgtTypes
        Vector
        System
        Axes
        Point
    End Enum
    Public Shared Function GetTransientName(ByVal type As VgtTypes) As String
        Return String.Format("{0}_{1}", type, System.Math.Max(System.Threading.Interlocked.Increment(_counter), _counter - 1))
    End Function
    Public Shared Function CreatePoint(ByVal provider As IAgCrdnProvider, ByVal type As AgECrdnPointType) As IAgCrdnPoint
        Return provider.Points.Factory.Create(GetTransientName(VgtHelper.VgtTypes.Point), String.Empty, type)
    End Function
    Public Shared Function CreateAxes(ByVal provider As IAgCrdnProvider, ByVal type As AgECrdnAxesType) As IAgCrdnAxes
        Return provider.Axes.Factory.Create(GetTransientName(VgtHelper.VgtTypes.Axes), String.Empty, type)
    End Function
    Public Shared Function CreateSystem(ByVal provider As IAgCrdnProvider, ByVal type As AgECrdnSystemType) As IAgCrdnSystem
        Return provider.Systems.Factory.Create(GetTransientName(VgtHelper.VgtTypes.System), String.Empty, type)
    End Function
End Class

Namespace AGI.CodeSnippets

    <AttributeUsage(AttributeTargets.Method)> _
    Public Class CodeSnippet
        Inherits Attribute
        <AttributeUsage(AttributeTargets.Parameter)> _
        Public Class Parameter
            Inherits Attribute
            Private _id As String
            Public Property ID() As String
                Get
                    Return _id
                End Get
                Set(ByVal value As String)
                    _id = value
                End Set
            End Property

            Private _name As String
            Public Property Name() As String
                Get
                    Return _name
                End Get
                Set(ByVal value As String)
                    _name = value
                End Set
            End Property

            Private _description As String
            Public Property Description() As String
                Get
                    Return _description
                End Get
                Set(ByVal value As String)
                    _description = value
                End Set
            End Property

            Private _paramType As String
            Public Property ParamType() As String
                Get
                    Return _paramType
                End Get
                Set(ByVal value As String)
                    _paramType = value
                End Set
            End Property

            Public Sub New(ByVal id As String, ByVal description As String)
                _id = id
                _description = description
            End Sub
            Private _eid As String
            Public Property EID() As String
                Get
                    Return _eid
                End Get
                Set(ByVal value As String)
                    _eid = value
                End Set
            End Property
        End Class

        Private _parameters As List(Of Parameter)
        Public ReadOnly Property Parameters() As List(Of Parameter)
            Get
                Return _parameters
            End Get
        End Property

        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _category As String
        Public Property Category() As String
            Get
                Return _category
            End Get
            Set(ByVal value As String)
                _category = value
            End Set
        End Property

        Private _references As String
        Public Property References() As String
            Get
                Return _references
            End Get
            Set(ByVal value As String)
                _references = value
            End Set
        End Property

        Private _namespaces As String
        Public Property Namespaces() As String
            Get
                Return _namespaces
            End Get
            Set(ByVal value As String)
                _namespaces = value
            End Set
        End Property

        Private _file As String
        Public Property File() As String
            Get
                Return _file
            End Get
            Set(ByVal value As String)
                _file = value
            End Set
        End Property

        Private _startLine As Integer
        Public Property StartLine() As Integer
            Get
                Return _startLine
            End Get
            Set(ByVal value As Integer)
                _startLine = value
            End Set
        End Property

        Private _endLine As Integer
        Public Property EndLine() As Integer
            Get
                Return _endLine
            End Get
            Set(ByVal value As Integer)
                _endLine = value
            End Set
        End Property

        Private _code As String
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property

        Private _eid As String
        Public Property EID() As String
            Get
                Return _eid
            End Get
            Set(ByVal value As String)
                _eid = value
            End Set
        End Property

        Private _language As String
        Public ReadOnly Property Language() As String
            Get
                If Not String.IsNullOrEmpty(_language) Then
                    Return _language
                End If

                If _file.EndsWith("cs") Then
                    _language = "CSharp"
                ElseIf _file.EndsWith("vb") Then
                    _language = "VB"
                End If

                Return _language
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal description As String, ByVal category As String, ByVal references As String, ByVal namespaces As String, ByVal eid As String)
            _name = name
            _description = description
            _category = category
            _references = references
            _namespaces = namespaces
            _eid = eid
            _parameters = New List(Of Parameter)()
        End Sub
    End Class
End Namespace
