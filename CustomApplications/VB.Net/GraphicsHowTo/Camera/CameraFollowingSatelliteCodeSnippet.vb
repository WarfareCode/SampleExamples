#Region "UsingDirectives"

Imports System.IO
Imports GraphicsHowTo.Primitives
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt

#End Region

Namespace Camera
	Public Class CameraFollowingSatelliteCodeSnippet
		Inherits CodeSnippet
		Implements IDisposable
		Public Sub New()
            MyBase.New("Camera\CameraFollowingSatelliteCodeSnippet.vb")
		End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim satellite As IAgSatellite = CreateSatellite(root)
            Dim segmentFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/ISS.tle").FullPath
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/hs601.mdl").FullPath
            ExecuteSnippet(scene, root, satellite, segmentFile, modelFile)
        End Sub

        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("satellite", "Satellite to follow")> ByVal satellite As IAgSatellite, <AGI.CodeSnippets.CodeSnippet.Parameter("segmentFilePath", "Path of segment file")> ByVal segmentFilePath As String, <AGI.CodeSnippets.CodeSnippet.Parameter("modelPath", "Path of model file")> ByVal modelPath As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            '
            ' Create the SGP4 Propagator from the TLE
            '
            satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4)
            Dim propagator As IAgVePropagatorSGP4 = TryCast(satellite.Propagator, IAgVePropagatorSGP4)
            propagator.CommonTasks.AddSegsFromFile("25544", segmentFilePath)
            propagator.EphemerisInterval.SetImplicitInterval(root.CurrentScenario.Vgt.EventIntervals("AnalysisInterval"))
            propagator.Propagate()
            Dim epoch As Double = propagator.Segments(0).Epoch

            '
            ' Get the Vector Geometry Tool provider for the satellite and find its initial position and orientation.
            '
            Dim provider As IAgCrdnProvider = DirectCast(satellite, IAgStkObject).Vgt

            Dim positionResult As IAgCrdnPointLocateInSystemResult = provider.Points("Center").LocateInSystem(DirectCast(root.CurrentScenario, IAgScenario).StartTime, root.VgtRoot.WellKnownSystems.Earth.Inertial)
            Dim position As IAgCartesian3Vector = positionResult.Position

            Dim orientationResult As IAgCrdnAxesFindInAxesResult = root.VgtRoot.WellKnownAxes.Earth.Inertial.FindInAxes(DirectCast(root.CurrentScenario, IAgScenario).StartTime, provider.Axes("Body"))
            Dim orientation As IAgOrientation = orientationResult.Orientation

            '
            ' Create the satellite model
            '
            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                modelPath)
            DirectCast(model, IAgStkGraphicsPrimitive).ReferenceFrame = root.VgtRoot.WellKnownSystems.Earth.Inertial
            model.Position = New Object() {position.X, position.Y, position.Z}
            model.Orientation = orientation
            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))

            '
            ' Set the time
            '
            DirectCast(root.CurrentScenario, IAgScenario).Animation.StartTime = epoch
            '#End Region

            '
            ' Set the member variables
            '
            m_Satellite = satellite
            m_Model = model
            m_ReferenceFrameGraphics = New ReferenceFrameGraphics(root, provider.Systems("Body"), 25)
            m_Provider = provider
            m_Point = provider.Points("Center")
            m_Axes = provider.Axes("Body")

            OverlayHelper.AddTextBox("The SGP4 propagator is used to propagate a satellite from a TLE." & vbCrLf & _
                                     "A model primitive that automatically follows the propagator's " & vbCrLf & _
                                     "point is created to visualize the satellite. Camera.ViewOffset " & vbCrLf & _
                                     "and Camera.Constrained are used to view the model.", manager)
        End Sub

        Private Function CreateSatellite(ByVal root As AgStkObjectRoot) As IAgSatellite
            Dim satellite As IAgSatellite
            If root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, objectName) Then
                satellite = TryCast(root.GetObjectFromPath("Satellite/" & objectName), IAgSatellite)
            Else
                satellite = TryCast(root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, objectName), IAgSatellite)
            End If
            satellite.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType(AgELeadTrailData.eDataNone)
            satellite.VO.Pass.TrackData.PassData.Orbit.SetTrailSameAsLead()
            satellite.Graphics.UseInstNameLabel = False
            satellite.Graphics.LabelName = String.Empty
            satellite.VO.Model.OrbitMarker.Visible = False
            satellite.VO.Model.Visible = False

            Return satellite
        End Function

        Public Overrides Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim animationControl As IAgAnimation = TryCast(root, IAgAnimation)
            Dim animationSettings As IAgScAnimation = DirectCast(root.CurrentScenario, IAgScenario).Animation
            '
            ' Set-up the animation for this specific example
            '
            animationControl.Pause()
            'SetAnimationDefaults(root);
            animationSettings.AnimStepValue = 1.0
            animationSettings.EnableAnimCycleTime = True
            animationSettings.AnimCycleTime = Double.Parse(animationSettings.StartTime.ToString()) + 3600.0
            animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime
            animationControl.PlayForward()

            '#Region "CodeSnippet"

            '
            ' Create the viewer point, which is an offset from near the satellite position 
            ' looking towards the satellite.  Set the camera to look from the viewer to the 
            ' satellite.
            '
            Dim offset As Array = New Object() {50.0, 50.0, -50.0}
            scene.Camera.ViewOffset(m_Axes, m_Point, offset)
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisNegativeZ

            '#End Region
        End Sub

        Public Overrides Sub Remove(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            manager.Primitives.Remove(DirectCast(m_Model, IAgStkGraphicsPrimitive))
            m_ReferenceFrameGraphics.Dispose()
            root.CurrentScenario.Children.Unload(AgESTKObjectType.eSatellite, objectName)
            OverlayHelper.RemoveTextBox(manager)

            m_Satellite = Nothing
            m_Provider = Nothing
            m_ReferenceFrameGraphics = Nothing
            m_Model = Nothing
            m_Point = Nothing
            m_Axes = Nothing

            scene.Camera.ViewCentralBody("Earth", root.VgtRoot.WellKnownAxes.Earth.Inertial)
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ


            DirectCast(root, IAgAnimation).Pause()
            SetAnimationDefaults(root)
            DirectCast(root, IAgAnimation).Rewind()

            scene.Render()
        End Sub

#Region "CodeSnippet"

        Public Sub TimeChanged(ByVal root As AgStkObjectRoot, ByVal TimeEpSec As Double)
            If m_Satellite IsNot Nothing Then
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
                '
                ' Get vector pointing from the satellite to the sun.
                '

                Dim result As IAgCrdnVectorFindInAxesResult = m_Provider.Vectors("Sun").FindInAxes(TimeEpSec, m_Axes)
                Dim xNormalizedIn2D As Double = result.Vector.X / (Math.Sqrt(Math.Pow(result.Vector.X, 2.0) + Math.Pow(result.Vector.Z, 2.0)))
                m_NewAngle = Math.Acos(xNormalizedIn2D)

                '
                ' Initialize tracking of angle changes.  To when the angle reaches 180 degrees and starts to
                ' fall back down to 0, the panel rotation must use a slightly different calculation.
                '
                If m_FirstRun Then
                    m_StartTime = TimeEpSec
                    m_OldTime = TimeEpSec
                    m_OldAngle = m_NewAngle
                    m_FirstRun = False
                End If

                Dim TwoPI As Double = Math.PI * 2
                Dim HalfPI As Double = Math.PI * 0.5

                '
                ' Rotates the satellite panels. The panel rotation is reversed when the animation is reversed.
                ' Set boolean flag for update to path.
                '
                If TimeEpSec - m_StartTime >= m_OldTime - m_StartTime Then
                    If m_NewAngle < m_OldAngle Then
                        m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + m_NewAngle) Mod TwoPI
                    ElseIf m_NewAngle > m_OldAngle Then
                        m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + (TwoPI - m_NewAngle)) Mod TwoPI
                    End If
                Else
                    If m_NewAngle > m_OldAngle Then
                        m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + m_NewAngle) Mod TwoPI
                    ElseIf m_NewAngle < m_OldAngle Then
                        m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + (TwoPI - m_NewAngle)) Mod TwoPI
                    End If
                End If

                '
                ' Sets the old angle to current (new) angle.
                '
                m_OldAngle = m_NewAngle
                m_OldTime = TimeEpSec

                '
                ' Update the position and orientation of the model 
                '
                Dim positionResult As IAgCrdnPointLocateInSystemResult = m_Point.LocateInSystem(TimeEpSec, root.VgtRoot.WellKnownSystems.Earth.Inertial)
                Dim orientationResult As IAgCrdnAxesFindInAxesResult = root.VgtRoot.WellKnownAxes.Earth.Inertial.FindInAxes(TimeEpSec, m_Axes)

                m_Model.Position = New Object() {positionResult.Position.X, positionResult.Position.Y, positionResult.Position.Z}
                m_Model.Orientation = orientationResult.Orientation
            End If
        End Sub

#End Region

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overrides Sub Finalize()
            Try
                Dispose(False)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If m_ReferenceFrameGraphics IsNot Nothing Then
                    m_ReferenceFrameGraphics.Dispose()
                    m_ReferenceFrameGraphics = Nothing
                End If
            End If
        End Sub

        Private m_Satellite As IAgSatellite
        Private m_Model As IAgStkGraphicsModelPrimitive
        Private m_ReferenceFrameGraphics As ReferenceFrameGraphics
        Private m_Provider As IAgCrdnProvider
        Private m_Point As IAgCrdnPoint
        Private m_Axes As IAgCrdnAxes

        Private m_NewAngle As Double
        Private m_OldAngle As Double
        Private m_FirstRun As Boolean = True

        Private m_OldTime As Double
        Private m_StartTime As Double

        Private Const objectName As String = "CameraFollowingSatellite"
    End Class
End Namespace
