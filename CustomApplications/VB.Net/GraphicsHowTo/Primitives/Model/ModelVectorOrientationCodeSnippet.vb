#Region "UsingDirectives"
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKVgt
Imports AGI.STKUtil
Imports System.Collections.Generic
#End Region

Namespace Primitives.Model
	Class ModelVectorOrientationCodeSnippet
		Inherits CodeSnippet
		Public Sub New(epoch As Object)
            MyBase.New("Primitives\Model\ModelVectorOrientationCodeSnippet.vb")
			m_Epoch = epoch
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/f-35_jsf_cv.mdl").FullPath
            ExecuteSnippet(scene, root, modelFile)
        End Sub

        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")> ByVal modelFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            '
            ' Get the position and orientation data for the model from the data file
            '
            Dim pathToModel As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/f-35_jsf_cvData.txt").FullPath
            Dim provider As New PositionOrientationProvider(attachID("$providerDataFile$The file containing position and orientation data for the model$", pathToModel), root)

            '
            ' Create the model for the aircraft
            '
            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                modelFile)
            model.Scale = Math.Pow(10, attachID("$scale$The scale of the model$", 1.5))
            DirectCast(model, IAgStkGraphicsPrimitive).ReferenceFrame = attachID("$referenceSystem$The system to use as a reference frame$", root.VgtRoot.WellKnownSystems.Earth.Fixed)
            model.Position = New Object() {provider.Positions(0).GetValue(0), provider.Positions(0).GetValue(1), provider.Positions(0).GetValue(2)}
            Dim orientation As IAgOrientation = root.ConversionUtility.NewOrientation()
            orientation.AssignQuaternion(CDbl(provider.Orientations(0).GetValue(0)), CDbl(provider.Orientations(0).GetValue(1)), CDbl(provider.Orientations(0).GetValue(2)), CDbl(provider.Orientations(0).GetValue(3)))
            model.Orientation = orientation

            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))

            '
            ' Create the path primitive
            '
            Dim path As IAgStkGraphicsPathPrimitive = manager.Initializers.PathPrimitive.Initialize()
            path.PolylineType = AgEStkGraphicsPolylineType.eStkGraphicsPolylineTypeLines
            path.UpdatePolicy = TryCast(manager.Initializers.DurationPathPrimitiveUpdatePolicy.InitializeWithParameters(120, AgEStkGraphicsPathPrimitiveRemoveLocation.eStkGraphicsRemoveLocationFront), IAgStkGraphicsPathPrimitiveUpdatePolicy)

            manager.Primitives.Add(DirectCast(path, IAgStkGraphicsPrimitive))
            '#End Region

            m_Model = model
            m_Provider = provider
            m_StopTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:07:57.000").Format("epSec"))
            OverlayHelper.AddTextBox("The model's position and orientation are updated in the TimeChanged " & vbCrLf & _
                                     "event based on a point and axes evaluator created from a waypoint propagator.", manager)
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
			animationSettings.EnableAnimCycleTime = True
			animationSettings.AnimCycleTime = m_StopTime
			animationSettings.AnimCycleType = AgEScEndLoopType.eEndTime
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
            If (attachID("$provider$The position and orientation provider$", m_Provider) IsNot Nothing) AndAlso (TimeEpSec <= attachID("$stopTime$The stop time$", m_StopTime)) Then
                Dim index As Integer = m_Provider.FindIndexOfClosestTime(TimeEpSec, 0, m_Provider.Dates.Count)

                '
                ' Update model's position and orientation every animation update
                '
                m_Model.Position = m_Provider.Positions(index)
                Dim orientation As IAgOrientation = root.ConversionUtility.NewOrientation()
                orientation.AssignQuaternion(CDbl(m_Provider.Orientations(index).GetValue(0)), CDbl(m_Provider.Orientations(index).GetValue(1)), CDbl(m_Provider.Orientations(index).GetValue(2)), CDbl(m_Provider.Orientations(index).GetValue(3)))
                m_Model.Orientation = orientation
            End If
		End Sub
		#End Region

		Private m_Model As IAgStkGraphicsModelPrimitive
		Private m_Provider As PositionOrientationProvider = Nothing
		Private m_Epoch As Object
		Private m_StopTime As Double
	End Class
End Namespace
