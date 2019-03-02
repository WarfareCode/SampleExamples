#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.Model
	Class ModelArticulationCodeSnippet
		Inherits CodeSnippet
		Public Sub New(epoch As Object)
            MyBase.New("Primitives\Model\ModelArticulationCodeSnippet.vb")
			m_Epoch = epoch
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/commuter.mdl").FullPath
            ExecuteSnippet(scene, root, modelFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAModelWithArticulations", _
            "Draw a model with moving articulations", _
            "Graphics | Primitives | Model Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsModelPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")> ByVal modelFile As String)
			'#Region "CodeSnippet"
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			'
			' Create the model
			'
			Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
				modelFile)

			Dim position As Array = New Object() {attachID("$lat$The latitude of the model$", 36), attachID("$lon$The longitude of the model$", -116.75), attachID("$alt$The altitude of the model$", 25000.0)}
			model.SetPositionCartographic(attachID("$planetName$The name of the planet on which the model will be placed$", "Earth"), position)

			'
			' Rotate the model to be oriented correctly
			'
			model.Articulations.GetByName(attachID("$articulationOneName$The name of the first articulation$", "Commuter")).GetByName(attachID("$transformationOneName$The name of the first transformation$", "Roll")).CurrentValue = attachID("$transformationOneValue$The value of the first transformation$", 4.08407)
			model.Articulations.GetByName(attachID("$articulationTwoName$The name of the second articulation$", "Commuter")).GetByName(attachID("$transformationTwoName$The name of the second transformation$", "Yaw")).CurrentValue = attachID("$transformationTwoValue$The value of the second transformation$", -0.43633)

			manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))
			'#End Region

            m_Model = DirectCast(model, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("The Articulations collection provides access to a model's moving parts." & vbCrLf & _
                                     "In this example, the propellers' spin articulation is modified in the " & vbCrLf & _
                                     "TimeChanged event based on the current time.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
			'
			' Set-up the animation for this specific example
			'

			animation.Pause()
			SetAnimationDefaults(root)
			DirectCast(root.CurrentScenario, IAgScenario).Animation.AnimStepValue = 1.0
			animation.PlayForward()

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Model.BoundingSphere, -15, 3)

			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Model)
			m_Model = Nothing

			OverlayHelper.RemoveTextBox(manager)
			scene.Render()
		End Sub

			'#Region "CodeSnippet"
			Friend Sub TimeChanged(TimeEpSec As Double)
				'
				' Rotate the propellors every time the animation updates
				'
				If m_Model IsNot Nothing Then
					Dim TwoPI As Double = 2 * Math.PI
					DirectCast(m_Model, IAgStkGraphicsModelPrimitive).Articulations.GetByName("props").GetByName("Spin").CurrentValue = TimeEpSec Mod TwoPI
				End If
			End Sub
			'#End Region

		Private m_Model As IAgStkGraphicsPrimitive
		Private m_Epoch As Object
	End Class
End Namespace
