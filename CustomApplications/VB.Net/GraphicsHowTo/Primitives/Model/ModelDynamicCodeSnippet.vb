#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Primitives.Model
	Class ModelDynamicCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Model\ModelDynamicCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/hellfireflame.dae").FullPath
            ExecuteSnippet(scene, root, modelFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAModelWithDynamicTextures", _
            "Draw a dynamically textured Collada model", _
            "Graphics | Primitives | Model Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsModelPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")> ByVal modelFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                modelFile)

            Dim position As Array = New Object() {attachID("$lat$The latitude of the model$", 49.88), attachID("$lon$The longitude of the model$", -77.25), attachID("$alt$The altitude of the model$", 5000.0)}
            model.SetPositionCartographic(attachID("$planetName$The planet on which the model will be placed$", "Earth"), position)
            model.Scale = Math.Pow(10, attachID("$scale$The scale of the model$", 2))

            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))

            '  hellfireflame.anc
            ' 
            '  <?xml version = "1.0" standalone = "yes"?>
            '  <ancillary_model_data version = "1.0">
            '       <video_textures>
            '           <video_texture image_id = "smoketex_tga" init_from = "smoke.avi" video_loop="true" video_framerate="60" />
            '           <video_texture image_id = "flametex_tga" init_from = "flame.mov" video_loop="true" video_framerate="60" />
            '      </video_textures>
            '  </ancillary_model_data>
            '#End Region

            OverlayHelper.AddTextBox("Dynamic textures, e.g. videos, can be used to create " & vbCrLf & _
                                     "effects like fire and smoke." & vbCrLf & vbCrLf & _
                                     "Dynamic textures on models are created using an XML-based" & vbCrLf & _
                                     "ancillary file. The ancillary file has the same filename " & vbCrLf & _
                                     "as the model but with an .anc extension. As shown in the " & vbCrLf & _
                                     "code window, the video_texture tag is used to define a " & vbCrLf & _
                                     "video, which is referenced by the model. Once loaded " & vbCrLf & _
                                     "using a model primitive, the video will play in sync with" & vbCrLf & _
                                     "Insight3D animation.", manager)

            m_Primitive = DirectCast(model, IAgStkGraphicsPrimitive)

        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			Dim center As Array = m_Primitive.BoundingSphere.Center
			Dim boundingSphere As IAgStkGraphicsBoundingSphere = manager.Initializers.BoundingSphere.Initialize(center, m_Primitive.BoundingSphere.Radius * 0.055)

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere, -50, 15)
			DirectCast(root, IAgAnimation).PlayForward()

			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			DirectCast(root, IAgAnimation).Rewind()
			manager.Primitives.Remove(m_Primitive)
			OverlayHelper.RemoveTextBox(manager)
			scene.Render()

			m_Primitive = Nothing
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive

	End Class
End Namespace
