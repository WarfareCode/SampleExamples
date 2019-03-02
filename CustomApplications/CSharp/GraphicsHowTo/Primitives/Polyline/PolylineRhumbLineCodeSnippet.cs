#region UsingDirectives
using System.Collections.Generic;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.Primitives.Polyline
{
    class PolylineRhumbLineCodeSnippet : CodeSnippet
    {
        public PolylineRhumbLineCodeSnippet()
            : base(@"Primitives\Polyline\PolylineRhumbLineCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawARhumbLine",
            /* Description */ "Draw a rhumb line on the globe",
            /* Category    */ "Graphics | Primitives | Polyline Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array newOrleans = new object[] 
            {
                /*$startLat$The latitude of the start point$*/29.98, 
                /*$startLon$The longitude of the start point$*/-90.25, 
                /*$startAlt$The altitude of the start point$*/0.0
            };
            Array sanJose = new object[] 
            {
                /*$endLat$The latitude of the end point$*/37.37, 
                /*$endLon$The longitude of the end point$*/-121.92, 
                /*$endAlt$The altitude of the end point$*/0.0
            };

            Array positions = new object[6];
            newOrleans.CopyTo(positions, 0);
            sanJose.CopyTo(positions, 3);

            IAgStkGraphicsPositionInterpolator interpolator = manager.Initializers.RhumbLineInterpolator.Initialize() as IAgStkGraphicsPositionInterpolator;
            IAgStkGraphicsPolylinePrimitive line = manager.Initializers.PolylinePrimitive.InitializeWithInterpolator(interpolator);
            line.SetCartographic(/*$planetName$The planet on which the polyline will be placed$*/"Earth", ref positions);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)line);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)line;
            OverlayHelper.AddTextBox(
@"The PolylinePrimitive is initialized with a RhumbLineInterpolator to 
visualize a rhumb line instead of a straight line.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
            scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

            double fit = 1.0; //for helping fit the line into the extent

            Array extent = new object[]
            { 
                -121.92 - fit,
                29.98 - fit,
                -90.25 + fit,
                37.37 + fit
            };

            scene.Camera.ViewExtent("Earth", ref extent);

            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Primitive);
            m_Primitive = null;

            OverlayHelper.RemoveTextBox(manager);
            scene.Render();
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    };
}
