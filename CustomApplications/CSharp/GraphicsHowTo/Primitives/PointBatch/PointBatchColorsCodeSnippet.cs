#region UsingDirectives
using System.Collections.Generic;
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using System;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives.PointBatch
{
    class PointBatchColorsCodeSnippet : CodeSnippet
    {
        public PointBatchColorsCodeSnippet()
            : base(@"Primitives\PointBatch\PointBatchColorsCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawASetOfUniquelyColoredPoints",
            /* Description */ "Draw a set of uniquely colored points",
            /* Category    */ "Graphics | Primitives | Point Batch Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPointBatchPrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array positions = new object[]
            {
                /*$lat1$The latitude of the first marker$*/37.62, /*$lon1$The longitude of the first marker$*/-122.38, /*$alt1$The altitude of the first marker$*/0.0,    // San Francisco
                /*$lat2$The latitude of the second marker$*/38.52, /*$lon2$The longitude of the second marker$*/-121.50, /*$alt2$The altitude of the second marker$*/0.0,    // Sacramento
                /*$lat3$The latitude of the third marker$*/33.93, /*$lon3$The longitude of the third marker$*/-118.40, /*$alt3$The altitude of the third marker$*/0.0,    // Los Angeles
                /*$lat4$The latitude of the fourth marker$*/32.82, /*$lon4$The longitude of the fourth marker$*/-117.13, /*$alt4$The altitude of the fourth marker$*/0.0     // San Diego
            };

            Array colors = new object[]
            {
                /*$color1$The color of the first point (a System.Drawing.Color converted to Argb and cast to an unsigned integer$*/(uint)Color.Red.ToArgb(),
                /*$color2$The color of the second point (a System.Drawing.Color converted to Argb and cast to an unsigned integer$*/(uint)Color.Orange.ToArgb(),
                /*$color3$The color of the third point (a System.Drawing.Color converted to Argb and cast to an unsigned integer$*/(uint)Color.Blue.ToArgb(),
                /*$color4$The color of the fourth point (a System.Drawing.Color converted to Argb and cast to an unsigned integer$*/(uint)Color.White.ToArgb()
            };

            IAgStkGraphicsPointBatchPrimitive pointBatch = manager.Initializers.PointBatchPrimitive.Initialize();
            pointBatch.SetCartographicWithColors(/*$planetName$The name of the planet on which the points will be placed$*/"Earth", ref positions, ref colors);
            pointBatch.PixelSize = /*$pointSize$The size of the points in pixels$*/8;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)pointBatch);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)pointBatch;
            OverlayHelper.AddTextBox(
@"A collection of positions and a collection of colors are provided to 
the PointBatchPrimitive to visualize points with unique colors.", manager);
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
            m_Primitive = null;

            OverlayHelper.RemoveTextBox(manager);
            scene.Render();
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    };
}
