#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.DisplayConditions
{
    class DistanceDisplayConditionCodeSnippet : CodeSnippet
    {
        public DistanceDisplayConditionCodeSnippet()
            : base(@"DisplayConditions\DistanceDisplayConditionCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/hellfire.dae").FullPath;
            Execute(scene, root, modelFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddDistanceDisplayCondition",
            /* Description */ "Draw a primitive based on viewer distance",
            /* Category    */ "Graphics | DisplayConditions",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsDistanceDisplayCondition"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "Location of the model file")] string modelFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                modelFile);
            Array position = new object[3] { /*$lat$Model latitude$*/29.98, /*$lon$Model longitude$*/-90.25, /*$alt$Model altitude$*/8000.0 };
            model.SetPositionCartographic(/*$planetName$Name of the planet to place the model$*/"Earth", ref position);
            model.Scale = Math.Pow(10, /*$scale$Scale of the model$*/3);

            IAgStkGraphicsDistanceDisplayCondition condition =
                manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(/*$minDistance$Minimum distance at which the model is visible$*/2000, /*$maxDistance$Maximum distance at which the model is visible$*/40000);
            ((IAgStkGraphicsPrimitive)model).DisplayCondition = condition as IAgStkGraphicsDisplayCondition;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)model;
            OverlayHelper.AddTextBox(
@"Zoom in and out to see the primitive disappear and 
reappear based on distance. 

This is implemented by assigning a DistanceDisplayCondition 
to the primitive's DisplayCondition property.", manager);
            
            OverlayHelper.AddDistanceOverlay(scene, manager);
            OverlayHelper.DistanceDisplay.AddIntervals(new Interval[] { new Interval(2000, 40000) });
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere, 
                45, 10);
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Primitive);
            OverlayHelper.RemoveTextBox(manager);
            OverlayHelper.DistanceDisplay.RemoveIntervals(new Interval[] { new Interval(2000, 40000) });
            OverlayHelper.RemoveDistanceOverlay(manager);

            scene.Render();

            m_Primitive = null;
            
        }

        private IAgStkGraphicsPrimitive m_Primitive;
        
    };
}
