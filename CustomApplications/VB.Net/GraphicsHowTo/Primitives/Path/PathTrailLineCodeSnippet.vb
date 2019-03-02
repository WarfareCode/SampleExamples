#Region "UsingDirectives"
Imports System.IO
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKVgt
Imports AGI.STKUtil
#End Region

Namespace Primitives
	Class PathTrailLineCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Path\PathTrailLineCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim segmentFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/ISS.tle").FullPath
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/hs601.mdl").FullPath
            Dim satellite As IAgSatellite = CreateSatellite(root)
            ExecuteSnippet(scene, root, segmentFile, modelFile, satellite)
        End Sub

        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("segmentFile", "The segment file")> ByVal segmentFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")> ByVal modelFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("satellite", "A Satellite")> ByVal satellite As IAgSatellite)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            '
            ' Create the SGP4 Propogator from the TLE
            '
            satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4)
            Dim propagator As IAgVePropagatorSGP4 = TryCast(satellite.Propagator, IAgVePropagatorSGP4)
            propagator.CommonTasks.AddSegsFromFile("25544", segmentFile)
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
                modelFile)
            DirectCast(model, IAgStkGraphicsPrimitive).ReferenceFrame = root.VgtRoot.WellKnownSystems.Earth.Inertial
            model.Position = New Object() {position.X, position.Y, position.Z}
            model.Orientation = orientation
            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))

            '
            ' Create the path primitive
            '
            Dim path As IAgStkGraphicsPathPrimitive = manager.Initializers.PathPrimitive.Initialize()
            DirectCast(path, IAgStkGraphicsPrimitive).ReferenceFrame = root.VgtRoot.WellKnownSystems.Earth.Inertial
            'path.UpdatePolicy = manager.Initializers.MaximumCountPathPrimitiveUpdatePolicy.InitializeWithParameters(
            '    512, AgEStkGraphicsPathPrimitiveRemoveLocation.eStkGraphicsRemoveLocationFront) as IAgStkGraphicsPathPrimitiveUpdatePolicy;
            path.UpdatePolicy = TryCast(manager.Initializers.DurationPathPrimitiveUpdatePolicy.InitializeWithParameters(60, AgEStkGraphicsPathPrimitiveRemoveLocation.eStkGraphicsRemoveLocationFront), IAgStkGraphicsPathPrimitiveUpdatePolicy)
            manager.Primitives.Add(DirectCast(path, IAgStkGraphicsPrimitive))

            '
            ' Set the time
            '
            '((IAgScenario)root.CurrentScenario).Animation.StartTime = epoch;
            'Insight3DHelper.Animation.Time = epoch;
            '#End Region

            '
            ' Set the member variables
            '
            m_Satellite = satellite
            m_Model = model
            m_Path = path
            m_Provider = provider
            m_Point = provider.Points("Center")
            m_Axes = provider.Axes("Body")
            m_Root = root

            AddHandler root.OnAnimUpdate, AddressOf TimeChanged
            OverlayHelper.AddTextBox("Points are added to the path in the TimeChanged event." & vbLf & "The DurationPathPrimitiveUpdatePolicy will remove points after the given duration.", manager)
        End Sub

		Private Function CreateSatellite(root As AgStkObjectRoot) As IAgSatellite
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

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim animationControl As IAgAnimation = TryCast(root, IAgAnimation)
			Dim animationSettings As IAgScAnimation = DirectCast(root.CurrentScenario, IAgScenario).Animation
			'
			' Set-up the animation for this specific example
			'
			animationControl.Pause()
			SetAnimationDefaults(root)
			animationSettings.AnimStepValue = 1.0
			animationSettings.EnableAnimCycleTime = True
			animationSettings.AnimCycleTime = Double.Parse(animationSettings.StartTime.ToString()) + 3600.0
			animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime
			animationControl.PlayForward()

			'
			' Create the viewer point, which is an offset from near the model position looking
			' towards the model.  Set the camera to look from the viewer to the model.
			'
			Dim offset As Array = New Object() {50.0, 50.0, -50.0}
			scene.Camera.ViewOffset(m_Axes, m_Point, offset)
			scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisNegativeZ
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(DirectCast(m_Model, IAgStkGraphicsPrimitive))
			manager.Primitives.Remove(DirectCast(m_Path, IAgStkGraphicsPrimitive))
			root.CurrentScenario.Children.Unload(AgESTKObjectType.eSatellite, objectName)
			RemoveHandler root.OnAnimUpdate, AddressOf TimeChanged
			OverlayHelper.RemoveTextBox(manager)

			m_Satellite = Nothing
			m_Provider = Nothing
			m_Path = Nothing
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

		Private Sub TimeChanged(TimeEpSec As Double)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager
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

				If Not m_AnimateForward Then
					m_AnimateForward = True
					m_AnimateDirectionChanged = True
				End If
			Else
				If m_NewAngle > m_OldAngle Then
					m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + m_NewAngle) Mod TwoPI
				ElseIf m_NewAngle < m_OldAngle Then
					m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + (TwoPI - m_NewAngle)) Mod TwoPI
				End If

				If m_AnimateForward Then
					m_AnimateForward = False
					m_AnimateDirectionChanged = True
				End If
			End If

			If m_AnimateDirectionChanged Then
				m_Path.Clear()
				m_AnimateDirectionChanged = False
			End If

			'
			' Sets the old angle to current (new) angle.
			'
			m_OldAngle = m_NewAngle
			m_OldTime = TimeEpSec

			'
			' Update the position and orientation of the model 
			'
			Dim positionResult As IAgCrdnPointLocateInSystemResult = m_Point.LocateInSystem(TimeEpSec, m_Root.VgtRoot.WellKnownSystems.Earth.Inertial)
			Dim orientationResult As IAgCrdnAxesFindInAxesResult = m_Root.VgtRoot.WellKnownAxes.Earth.Inertial.FindInAxes(TimeEpSec, m_Axes)

			Dim positionPathPoint As Array = New Object() {positionResult.Position.X, positionResult.Position.Y, positionResult.Position.Z}

			m_Model.Position = positionPathPoint
			m_Model.Orientation = orientationResult.Orientation

            m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDatePositionAndColor(m_Root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), positionPathPoint, Color.LightGreen))
		End Sub

		#End Region

		Private m_Satellite As IAgSatellite
		Private m_Path As IAgStkGraphicsPathPrimitive
		Private m_Model As IAgStkGraphicsModelPrimitive
		Private m_Provider As IAgCrdnProvider
		Private m_Point As IAgCrdnPoint
		Private m_Axes As IAgCrdnAxes
		Private m_Root As AgStkObjectRoot

		Private m_NewAngle As Double
		Private m_OldAngle As Double
		Private m_FirstRun As Boolean = True

		Private m_OldTime As Double
		Private m_StartTime As Double

		Private m_AnimateForward As Boolean = True
		Private m_AnimateDirectionChanged As Boolean = False

		Private Const objectName As String = "PathTrailLineSatellite"
	End Class
End Namespace
