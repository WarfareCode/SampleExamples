#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Primitives.Model
{
    class ModelDynamicCodeSnippet : CodeSnippet
    {
        public ModelDynamicCodeSnippet()
            : base(@"Primitives\Model\ModelDynamicCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/hellfireflame.dae").FullPath;
            Execute(scene, root, modelFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAModelWithDynamicTextures",
            /* Description */ "Draw a dynamically textured Collada model",
            /* Category    */ "Graphics | Primitives | Model Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsModelPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")] string modelFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                modelFile);

            Array position = new object[] { /*$lat$The latitude of the model$*/49.88, /*$lon$The longitude of the model$*/-77.25, /*$alt$The altitude of the model$*/5000.0 };
            model.SetPositionCartographic(/*$planetName$The planet on which the model will be placed$*/"Earth", ref position);
            model.Scale = Math.Pow(10, /*$scale$The scale of the model$*/2);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);

            //  hellfireflame.anc
            // 
            //  <?xml version = "1.0" standalone = "yes"?>
            //  <ancillary_model_data version = "1.0">
            //       <video_textures>
            //           <video_texture image_id = "smoketex_tga" init_from = "smoke.avi" video_loop="true" video_framerate="60" />
            //           <video_texture image_id = "flametex_tga" init_from = "flame.mov" video_loop="true" video_framerate="60" />
            //      </video_textures>
            //  </ancillary_model_data>
#endregion

            OverlayHelper.AddTextBox(
@"Dynamic textures, e.g. videos, can be used to create 
effects like fire and smoke.

Dynamic textures on models are created using an XML-based
ancillary file. The ancillary file has the same filename 
as the model but with an .anc extension. As shown in the 
code window, the video_texture tag is used to define a 
video, which is referenced by the model. Once loaded 
using a model primitive, the video will play in sync with
Insight3D animation.", manager);
            
            m_Primitive = (IAgStkGraphicsPrimitive)model;

        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array center = m_Primitive.BoundingSphere.Center;
            IAgStkGraphicsBoundingSphere boundingSphere = manager.Initializers.BoundingSphere.Initialize(
                ref center, m_Primitive.BoundingSphere.Radius * 0.055);

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere,
                -50, 15);
            ((IAgAnimation)root).PlayForward();

            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            ((IAgAnimation)root).Rewind();
            manager.Primitives.Remove(m_Primitive);
            OverlayHelper.RemoveTextBox(manager);
            scene.Render();

            m_Primitive = null;
        }

        private IAgStkGraphicsPrimitive m_Primitive;
        
    };
}
