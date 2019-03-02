#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Primitives.Model
	Class ModelColladaFXCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Model\ModelColladaFXCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/Satellite.dae").FullPath
            ExecuteSnippet(scene, root, modelFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAModelWithLighting", _
            "Draw a Collada model with user defined lighting", _
            "Graphics | Primitives | Model Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsModelPrimitive")> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")> ByVal modelFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                modelFile)

            Dim position As Array = New Object() {attachID("$lat$The latitude of the model$", 39.88), attachID("$lon$The longitude of the model$", -75.25), attachID("$alt$The altitude of the model$", 500000.0)}
            model.SetPositionCartographic(attachID("$planetName$The planet on which the model will be placed$", "Earth"), position)
            model.Scale = Math.Pow(10, attachID("$scale$The scale of the model$", 2))

            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))
            '#End Region

            OverlayHelper.AddTextBox("Models can contain user defined lighting to give " & vbCrLf & _
                                     "it properties of real world materials such as metal, " & vbCrLf & _
                                     "glass, plastic, etc." & vbCrLf & vbCrLf & _
                                     "This model uses a shader that models the properties of" & vbCrLf & _
                                     "metal. It uses normal mapping to achieve the foil like" & vbCrLf & _
                                     "texture and it calculates lighting using a reflectance" & vbCrLf & _
                                     "model based on the Ashikhmin-Shirley BRDF.", manager)

            m_Primitive = DirectCast(model, IAgStkGraphicsPrimitive)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere, -45, 3)

			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Primitive)
			OverlayHelper.RemoveTextBox(manager)
			scene.Render()

			m_Primitive = Nothing
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
	End Class
End Namespace
