#region UsingDirectives
using System.Collections.Generic;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.Primitives.Polyline
{
    class PolylineGreatArcCodeSnippet : CodeSnippet
    {
        public PolylineGreatArcCodeSnippet()
            : base(@"Primitives\Polyline\PolylineGreatArcCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAGreatArc",
            /* Description */ "Draw a great arc on the globe",
            /* Category    */ "Graphics | Primitives | Polyline Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array washingtonDC = new object[] 
            {
                /*$startLat$The latitude of the start point$*/38.85,
                /*$startLon$The longitude of the start point$*/-77.04,
                /*$startAlt$The altitude of the start point$*/0.0
            };
            Array newOrleans = new object[] 
            {
                /*$endLat$The latitude of the end point$*/29.98, 
                /*$endLon$The longitude of the end point$*/-90.25,
                /*$endAlt$The altitude of the end point$*/0.0
            };

            Array positions = new object[6];
            washingtonDC.CopyTo(positions, 0);
            newOrleans.CopyTo(positions, 3);

            IAgStkGraphicsPositionInterpolator interpolator = manager.Initializers.GreatArcInterpolator.Initialize() as IAgStkGraphicsPositionInterpolator;
            IAgStkGraphicsPolylinePrimitive line = manager.Initializers.PolylinePrimitive.InitializeWithInterpolator(interpolator);
            line.SetCartographic(/*$planetName$The planet on which the polyline will be placed$*/"Earth", ref positions);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)line);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)line;
            OverlayHelper.AddTextBox(
@"The PolylinePrimitive is initialized with a GreatArcInterpolator to 
visualize a great arc instead of a straight line.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
            scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

            double fit = 1.0; //for helping fit the line into the extent

            Array extent = new object[]
            { 
                -90.25 - fit,
                29.98 - fit,
                -77.04 + fit,
                38.85 + fit
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
