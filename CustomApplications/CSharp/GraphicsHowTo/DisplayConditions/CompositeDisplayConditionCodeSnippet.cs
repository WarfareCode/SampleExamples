using System.Drawing;
using System.Windows.Forms;
#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
#endregion

namespace GraphicsHowTo.DisplayConditions
{
    class CompositeDisplayConditionCodeSnippet : CodeSnippet
    {
        public CompositeDisplayConditionCodeSnippet()
            : base(@"DisplayConditions\CompositeDisplayConditionCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath;
            Array position = new object[] { 29.98, -90.25, 0.0 };
            IAgPosition origin = root.ConversionUtility.NewPositionOnEarth();
            origin.AssignPlanetodetic((double)position.GetValue(0), (double)position.GetValue(1), (double)position.GetValue(2));
            IAgCrdnAxesFixed axes = CreateAxes(root, "Earth", origin);

            Execute(scene, root, modelFile, axes);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddACompositeDisplayCondition",
            /* Description */ "Draw a primitive based on multiple conditions",
            /* Category    */ "Graphics | DisplayConditions",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsCompositeDisplayCondition"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "Location of the model file")] string modelFile, [AGI.CodeSnippets.CodeSnippet.Parameter("Axes", "An axes used to orient the model")] IAgCrdnAxesFixed axes)
        {
            OverlayHelper.AddTextBox(
@"The primitive will be drawn on 5/30/2008 between 2:00:00
PM and 2:30:00 PM and between 3:00:00 PM and 3:30:00 PM.

Two TimeIntervalDisplayConditions are created and added to a
CompositeDisplayCondition, which is assigned to the model's 
DisplayCondition property. The composite is set to use the 
logical or operator so the primitive is shown if the current
animation time is within one of the time intervals. 

Note - the display conditions that make up a composite 
display condition need not be of the same type.", ((IAgScenario)root.CurrentScenario).SceneManager);
            
            OverlayHelper.AddTimeOverlay(root);


#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array position = new object[] {/*$lat$Model latitude$*/29.98, /*$lon$Model longitude$*/-90.25, /*$alt$Model altitude$*/0.0 };

            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                modelFile);
            model.SetPositionCartographic(/*$planetName$Name of the planet to place the model$*/"Earth", ref position);
            model.Scale = Math.Pow(10, /*$scale$Scale of the model$*/1.5);

            IAgDate start1 = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$startDate1$The first start date$*/"30 May 2008 14:00:00.000");
            IAgDate end1 = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$endDate1$The first end date$*/"30 May 2008 14:30:00.000");
            ((IAgAnimation)root).CurrentTime = double.Parse(start1.Format("epSec"));
            IAgDate start2 = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$startDate2$The second start date$*/"30 May 2008 15:00:00.000");
            IAgDate end2 = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$endDate2$The second end date$*/"30 May 2008 15:30:00.000");

            IAgStkGraphicsTimeIntervalDisplayCondition time1 = manager.Initializers.TimeIntervalDisplayCondition.InitializeWithTimes(start1, end1);
            IAgStkGraphicsTimeIntervalDisplayCondition time2 = manager.Initializers.TimeIntervalDisplayCondition.InitializeWithTimes(start2, end2);
            IAgStkGraphicsCompositeDisplayCondition composite = manager.Initializers.CompositeDisplayCondition.Initialize();

            composite.Add((IAgStkGraphicsDisplayCondition)time1);
            composite.Add((IAgStkGraphicsDisplayCondition)time2);
            composite.LogicOperation = AgEStkGraphicsBinaryLogicOperation.eStkGraphicsBinaryLogicOperationOr;
            ((IAgStkGraphicsPrimitive)model).DisplayCondition = composite as IAgStkGraphicsDisplayCondition;

            IAgCrdnAxesFindInAxesResult result = root.VgtRoot.WellKnownAxes.Earth.Fixed.FindInAxes(((IAgScenario)root.CurrentScenario).Epoch, ((IAgCrdnAxes)axes));
            model.Orientation = result.Orientation;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);
#endregion

            m_Start1 = double.Parse(start1.Format("epSec").ToString());
            m_End1 = double.Parse(end1.Format("epSec").ToString());
            m_Start2 = double.Parse(start2.Format("epSec").ToString());
            m_End2 = double.Parse(end2.Format("epSec").ToString());
            OverlayHelper.TimeDisplay.AddInterval(m_Start1, m_End1);
            OverlayHelper.TimeDisplay.AddInterval(m_Start2, m_End2);

            m_Primitive = (IAgStkGraphicsPrimitive)model;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            //
            // Set-up the animation for this specific example
            //
            IAgAnimation animation = (IAgAnimation)root;
            animation.Pause();
            SetAnimationDefaults(root);
            animation.PlayForward();

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere);
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            ((IAgAnimation)root).Rewind();
            OverlayHelper.TimeDisplay.RemoveInterval(m_Start1, m_End1);
            OverlayHelper.TimeDisplay.RemoveInterval(m_Start2, m_End2);
            OverlayHelper.RemoveTimeOverlay(manager);
            OverlayHelper.RemoveTextBox(manager);

            manager.Primitives.Remove(m_Primitive);
            scene.Render();
            
            m_Primitive = null;
        }

        private IAgStkGraphicsPrimitive m_Primitive;

        private double m_Start1;
        private double m_End1;
        private double m_Start2;
        private double m_End2;


    };
}
