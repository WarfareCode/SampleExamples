#region UsingDirectives
using System.Collections.Generic;
using AGI.STKGraphics;
using AGI.STKUtil;
using AGI.STKObjects;
using System;
#endregion

namespace GraphicsHowTo.Primitives.Polyline
{
    class PolylineCodeSnippet : CodeSnippet
    {
        public PolylineCodeSnippet()
            : base(@"Primitives\Polyline\PolylineCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAPolyline",
            /* Description */ "Draw a line between two points",
            /* Category    */ "Graphics | Primitives | Polyline Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array philadelphia = new object[]
            {
                /*$startLat$The latitude of the start point$*/39.88,
                /*$startLon$The longitude of the start point$*/-75.25,
                /*$startAlt$The altitude of the start point$*/3000.0
            };
            Array washingtonDC = new object[]
            {
                /*$endLat$The latitude of the end point$*/38.85,
                /*$endLon$The longitude of the end point$*/-77.04,
                /*$endAlt$The altitude of the end point$*/3000.0
            };

            Array positions = new object[6];
            philadelphia.CopyTo(positions, 0);
            washingtonDC.CopyTo(positions, 3);

            IAgStkGraphicsPolylinePrimitive line = manager.Initializers.PolylinePrimitive.Initialize();
            line.SetCartographic(/*$planetName$Name of the planet to place primitive$*/"Earth", ref positions);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)line);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)line;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
            scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

            double fit = 1.0; //for helping fit the line into the extent

            Array extent = new object[]
            { 
                38.85 - fit,
                -77.04 - fit,
                39.88 + fit,
                -75.25 + fit
            };

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere,
                -40, 10);

            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Primitive);
            scene.Render();

            m_Primitive = null;
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    };
}
