#region UsingDirectives
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using System;
#endregion

namespace GraphicsHowTo.DisplayConditions
{
    public class AltitudeDisplayConditionCodeSnippet : CodeSnippet
    {
        public AltitudeDisplayConditionCodeSnippet()
            : base(@"DisplayConditions\AltitudeDisplayConditionCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddAltitudeDisplayCondition",
            /* Description */ "Draw a primitive based on viewer altitude",
            /* Category    */ "Graphics | DisplayConditions",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsAltitudeDisplayCondition"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array extent = new object[]
            {
                /*$westLon$Westernmost longitude$*/-94, /*$southLat$Southernmost latitude$*/29,
                /*$eastLon$Easternmost longitude$*/-89, /*$northLat$Northernmost latitude$*/33
            };

            IAgStkGraphicsSurfaceTriangulatorResult triangles = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple("Earth", ref extent);

            IAgStkGraphicsPolylinePrimitive line = manager.Initializers.PolylinePrimitive.Initialize();
            Array boundaryPositions = triangles.BoundaryPositions;
            line.Set(ref boundaryPositions);
            ((IAgStkGraphicsPrimitive)line).Color = /*$color$System.Drawing.Color of the primitive$*/Color.White;

            IAgStkGraphicsAltitudeDisplayCondition condition = manager.Initializers.AltitudeDisplayCondition.InitializeWithAltitudes(/*$minAlt$Minimum altitude at which the primitive is visible$*/500000, /*$maxAlt$Maximum altitude at which the primitive is visible$*/2500000);
            ((IAgStkGraphicsPrimitive)line).DisplayCondition = condition as IAgStkGraphicsDisplayCondition;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)line);
#endregion

            OverlayHelper.AddTextBox(
                @"Zoom in and out to see the primitive disappear and 
reappear based on altitude. 

This is implemented by assigning an 
AltitudeDisplayCondition to the primitive's 
DisplayCondition property.", manager);

            OverlayHelper.AddAltitudeOverlay(scene, manager);
            OverlayHelper.AltitudeDisplay.AddIntervals(new Interval[] { new Interval(500000, 2500000) });

            m_Primitive = (IAgStkGraphicsPrimitive)line;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere);
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Primitive);

            OverlayHelper.RemoveTextBox(manager);
            OverlayHelper.AltitudeDisplay.RemoveIntervals(new Interval[] { new Interval(500000, 2500000) });
            OverlayHelper.RemoveAltitudeOverlay(manager);

            scene.Render();

            m_Primitive = null;
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    }
}