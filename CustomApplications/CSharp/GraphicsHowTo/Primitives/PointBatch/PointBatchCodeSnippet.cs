#region UsingDirectives
using System.Collections.Generic;
using System.Drawing;
using AGI.STKUtil;
using AGI.STKGraphics;
using AGI.STKObjects;
using System;
#endregion

namespace GraphicsHowTo.Primitives.PointBatch
{
    class PointBatchCodeSnippet : CodeSnippet
    {
        public PointBatchCodeSnippet()
            : base(@"Primitives\PointBatch\PointBatchCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawASetOfPoints",
            /* Description */ "Draw a set of points",
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
                /*$lat1$The latitude of the first marker$*/39.88, /*$lon1$The longitude of the first marker$*/-75.25, /*$alt1$The altitude of the first marker$*/0,    // Philadelphia
                /*$lat2$The latitude of the second marker$*/38.85, /*$lon2$The longitude of the second marker$*/-77.04, /*$alt2$The altitude of the second marker$*/0, // Washington, D.C.   
                /*$lat3$The latitude of the third marker$*/29.98, /*$lon3$The longitude of the third marker$*/-90.25, /*$alt3$The altitude of the third marker$*/0, // New Orleans
                /*$lat4$The latitude of the fourth marker$*/37.37, /*$lon4$The longitude of the fourth marker$*/-121.92, /*$alt4$The altitude of the fourth marker$*/0    // San Jose
            };

            IAgStkGraphicsPointBatchPrimitive pointBatch = manager.Initializers.PointBatchPrimitive.Initialize();
            pointBatch.SetCartographic(/*$planetName$The name of the planet on which the points will be placed$*/"Earth", ref positions);
            pointBatch.PixelSize = /*$pointSize$The size of the point in pixels$*/5;
            ((IAgStkGraphicsPrimitive)pointBatch).Color = /*$pointColor$The color of the points in the batch$*/Color.White;
            pointBatch.DisplayOutline = /*$showOutline$Whether or not an outline should surround the point$*/true;
            pointBatch.OutlineWidth = /*$outlineWidth$The width of the outline$*/2;
            pointBatch.OutlineColor = /*$outlineColor$The color of the outline$*/Color.Red;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)pointBatch);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)pointBatch;
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
            scene.Render();

            m_Primitive = null;
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    };
}
