#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives.Model
{
    class ModelArticulationCodeSnippet : CodeSnippet
    {
        public ModelArticulationCodeSnippet(object epoch)
            : base(@"Primitives\Model\ModelArticulationCodeSnippet.cs")
        {
            m_Epoch = epoch;
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/commuter.mdl").FullPath;
            Execute(scene, root, modelFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAModelWithArticulations",
            /* Description */ "Draw a model with moving articulations",
            /* Category    */ "Graphics | Primitives | Model Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsModelPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")] string modelFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            //
            // Create the model
            //
            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                modelFile);

            Array position = new object[3] { /*$lat$The latitude of the model$*/36, /*$lon$The longitude of the model$*/-116.75, /*$alt$The altitude of the model$*/25000.0 };
            model.SetPositionCartographic(/*$planetName$The name of the planet on which the model will be placed$*/"Earth", ref position);

            //
            // Rotate the model to be oriented correctly
            //
            model.Articulations.GetByName(/*$articulationOneName$The name of the first articulation$*/"Commuter").GetByName(/*$transformationOneName$The name of the first transformation$*/"Roll").CurrentValue = /*$transformationOneValue$The value of the first transformation$*/4.084070562;
            model.Articulations.GetByName(/*$articulationTwoName$The name of the second articulation$*/"Commuter").GetByName(/*$transformationTwoName$The name of the second transformation$*/"Yaw").CurrentValue = /*$transformationTwoValue$The value of the second transformation$*/-0.436332325;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);
#endregion

            m_Model = (IAgStkGraphicsPrimitive)model;
            OverlayHelper.AddTextBox(
@"The Articulations collection provides access to a model's moving parts.
In this example, the propellers' spin articulation is modified in the 
TimeChanged event based on the current time.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgAnimation animation = (IAgAnimation)root;
            //
            // Set-up the animation for this specific example
            //

            animation.Pause();
            SetAnimationDefaults(root);
            ((IAgScenario)root.CurrentScenario).Animation.AnimStepValue = 1.0;
            animation.PlayForward();

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Model.BoundingSphere,
                -15, 3);

            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Model);
            m_Model = null;

            OverlayHelper.RemoveTextBox(manager);
            scene.Render();
        }

#region CodeSnippet
            internal void TimeChanged(double TimeEpSec)
            {
                //
                // Rotate the propellors every time the animation updates
                //
                if (/*$modelPrimitive$The model primitive to articulate$*/m_Model != null)
                {
                    double TwoPI = 2 * Math.PI;
                    ((IAgStkGraphicsModelPrimitive)/*$modelPrimitive$The model primitive to articulate$*/m_Model).Articulations.GetByName(/*$timeArticulationName$The name of the articulation to changed based on time$*/"props").GetByName(/*$timeTransformationName$The name of the transformation to changed based on time$*/"Spin").CurrentValue = TimeEpSec % TwoPI;
                }
            }
#endregion

        private IAgStkGraphicsPrimitive m_Model;
        private object m_Epoch;
    };
}
