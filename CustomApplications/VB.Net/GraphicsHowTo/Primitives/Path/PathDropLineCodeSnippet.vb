#Region "UsingDirectives"
Imports System.Collections.Generic
Imports System.IO
Imports AGI.STKUtil
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKVgt
#End Region

Namespace Primitives
	Class PathDropLineCodeSnippet
		Inherits CodeSnippet
		Public Sub New(epoch As Object)
            MyBase.New("Primitives\Path\PathDropLineCodeSnippet.vb")
			m_Epoch = epoch
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim providerDataFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/f-35_jsf_cvData.txt").FullPath
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/f-35_jsf_cv.mdl").FullPath
            ExecuteSnippet(scene, root, providerDataFile, modelFile)
        End Sub

        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("providerDataFile", "The file containing position and orientation data for the model")> ByVal providerDataFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")> ByVal modelFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            '
            ' Get the position and orientation data for the model from the data file
            '
            Dim provider As New PositionOrientationProvider(providerDataFile, root)

            '
            ' Create the model for the aircraft
            '
            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                modelFile)
            model.Scale = Math.Pow(10, attachID("$scale$The scale of the model$", 1.5))
            model.Position = New Object() {provider.Positions(0).GetValue(0), provider.Positions(0).GetValue(1), provider.Positions(0).GetValue(2)}
            Dim orientation As IAgOrientation = root.ConversionUtility.NewOrientation()
            orientation.AssignQuaternion(CDbl(provider.Orientations(0).GetValue(0)), CDbl(provider.Orientations(0).GetValue(1)), CDbl(provider.Orientations(0).GetValue(2)), CDbl(provider.Orientations(0).GetValue(3)))
            model.Orientation = orientation

            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))

            '
            ' Create the path primitive
            '
            Dim path As IAgStkGraphicsPathPrimitive = manager.Initializers.PathPrimitive.Initialize()
            path.PolylineType = attachID("$polylineType$The type of the polyline$", AgEStkGraphicsPolylineType.eStkGraphicsPolylineTypeLines)
            path.UpdatePolicy = TryCast(manager.Initializers.DurationPathPrimitiveUpdatePolicy.InitializeWithParameters(attachID("$duration$The amount of time that a location will be on the path$", 120), attachID("$removeLocation$The location to remove expired points from$", AgEStkGraphicsPathPrimitiveRemoveLocation.eStkGraphicsRemoveLocationFront)), IAgStkGraphicsPathPrimitiveUpdatePolicy)

            manager.Primitives.Add(DirectCast(path, IAgStkGraphicsPrimitive))
            '#End Region

            m_Provider = provider
            m_Path = path
            m_Model = model
            m_PreviousPosition = model.Position
            m_PreviousDrop = Double.MinValue
            m_StartTime = Double.MinValue
            m_StopTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:07:57.000").Format("epSec"))

            OverlayHelper.AddTextBox("Drop lines are added to the trail line of a model on a given interval.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			Dim animationControl As IAgAnimation = TryCast(root, IAgAnimation)
			Dim animationSettings As IAgScAnimation = DirectCast(root.CurrentScenario, IAgScenario).Animation
			'
			' Set-up the animation for this specific example
			'
			animationControl.Pause()
			SetAnimationDefaults(root)
			animationSettings.AnimStepValue = 1.0
			animationSettings.StartTime = m_Epoch
			animationControl.PlayForward()

			Dim centerPosition As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
            centerPosition.AssignPlanetodetic(39.615, -77.205, 3000)
			Dim x As Double, y As Double, z As Double
			centerPosition.QueryCartesian(x, y, z)
			Dim center As Array = New Object() {x, y, z}
			Dim boundingSphere As IAgStkGraphicsBoundingSphere = manager.Initializers.BoundingSphere.Initialize(center, 1500)

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere, 0, 15)

			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(DirectCast(m_Model, IAgStkGraphicsPrimitive))

			m_Model = Nothing
			m_Provider = Nothing

			OverlayHelper.RemoveTextBox(manager)
			scene.Render()
		End Sub

		#Region "CodeSnippet"
		Friend Sub TimeChanged(root As AgStkObjectRoot, TimeEpSec As Double)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			'
			' Record the animation start time.
			'
			If m_StartTime = Double.MinValue Then
				m_StartTime = TimeEpSec
			End If

            If (attachID("$provider$The position and orientation provider$", m_Provider) IsNot Nothing) AndAlso (TimeEpSec <= attachID("$stopTime$The stop time$", m_StopTime)) Then
                Dim index As Integer = m_Provider.FindIndexOfClosestTime(TimeEpSec, 0, m_Provider.Dates.Count)

                '
                ' If the animation was restarted, the path must be cleared
                ' and record of previous drop line and position must be reset.
                '
                If TimeEpSec = m_StartTime Then
                    m_Path.Clear()
                    m_PreviousPosition = m_Provider.Positions(index)
                    m_PreviousDrop = TimeEpSec
                End If
                Dim positionPathPoint As Array = m_Provider.Positions(index)
                '
                ' Update model's position and orientation every animation update
                '
                m_Model.Position = positionPathPoint
                Dim orientation As IAgOrientation = root.ConversionUtility.NewOrientation()
                orientation.AssignQuaternion(CDbl(m_Provider.Orientations(index).GetValue(0)), CDbl(m_Provider.Orientations(index).GetValue(1)), CDbl(m_Provider.Orientations(index).GetValue(2)), CDbl(m_Provider.Orientations(index).GetValue(3)))
                m_Model.Orientation = orientation

                '
                ' Update path with model's new position and check
                ' to add drop line at every animation update
                '
                m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDateAndPosition(root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), m_PreviousPosition))
                m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDateAndPosition(root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), positionPathPoint))

                m_PreviousPosition = positionPathPoint

                ' Add drop line
                If Math.Abs(TimeEpSec - m_PreviousDrop) > 10 Then
                    Dim endpointPosition As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
                    endpointPosition.AssignCartesian(CDbl(m_Model.Position.GetValue(0)), CDbl(m_Model.Position.GetValue(1)), CDbl(m_Model.Position.GetValue(2)))

                    Dim endpointLat As New Object, endpointLong As New Object
                    Dim endpointAlt As New Double
                    Dim x As New Double, y As New Double, z As New Double
                    endpointPosition.QueryPlanetodetic(endpointLat, endpointLong, endpointAlt)

                    endpointPosition.AssignPlanetodetic(endpointLat, endpointLong, 0)

                    endpointPosition.QueryCartesian(x, y, z)
                    Dim endpoint As Array = New Object() {x, y, z}

                    m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDateAndPosition(root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), positionPathPoint))
                    m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDateAndPosition(root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), endpoint))

                    m_PreviousDrop = TimeEpSec
                End If
            End If
		End Sub
		#End Region

		Private m_Provider As PositionOrientationProvider = Nothing
		Private m_Epoch As Object
		Private m_StartTime As Double
		Private m_StopTime As Double
		Private m_Path As IAgStkGraphicsPathPrimitive
		Private m_Model As IAgStkGraphicsModelPrimitive
		Private m_PreviousPosition As Array
		Private m_PreviousDrop As Double
	End Class
End Namespace
