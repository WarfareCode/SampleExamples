#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Primitives.Model
{
    class ModelColladaFXCodeSnippet : CodeSnippet
    {
        public ModelColladaFXCodeSnippet()
            : base(@"Primitives\Model\ModelColladaFXCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/Satellite.dae").FullPath;
            Execute(scene, root, modelFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAModelWithLighting",
            /* Description */ "Draw a Collada model with user defined lighting",
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

            Array position = new object[] { /*$lat$The latitude of the model$*/39.88, /*$lon$The longitude of the model$*/-75.25, /*$alt$The altitude of the model$*/500000.0 };
            model.SetPositionCartographic(/*$planetName$The planet on which the model will be placed$*/"Earth", ref position);
            model.Scale = Math.Pow(10, /*$scale$The scale of the model$*/2);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);
#endregion

            OverlayHelper.AddTextBox(
@"Models can contain user defined lighting to give 
it properties of real world materials such as metal, 
glass, plastic, etc.

This model uses a shader that models the properties of
metal. It uses normal mapping to achieve the foil like
texture and it calculates lighting using a reflectance
model based on the Ashikhmin-Shirley BRDF.", manager);

            m_Primitive = (IAgStkGraphicsPrimitive)model;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere,
                -45, 3);

            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Primitive);
            OverlayHelper.RemoveTextBox(manager);
            scene.Render();

            m_Primitive = null;
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    };
}
