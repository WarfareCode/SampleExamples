#region UsingDirectives
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.Primitives.Polyline
{
    class PolylineCircleCodeSnippet : CodeSnippet
    {
        public PolylineCircleCodeSnippet()
            : base( @"Primitives\Polyline\PolylineCircleCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawACircle",
            /* Description */ "Draw the outline of a circle on the globe",
            /* Category    */ "Graphics | Primitives | Polyline Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array center = new object[] { /*$lat$The latitude of the center point$*/39.88, /*$lon$The longitude of the center point$*/-75.25, /*$alt$The altitude of the center point$*/0.0 }; // Philadelphia

            IAgStkGraphicsSurfaceShapesResult shape = manager.Initializers.SurfaceShapes.ComputeCircleCartographic(/*$planetName$The planet on which the circle will be placed$*/"Earth", ref center, /*$radius$The radius of the circle$*/10000);
            Array positions = shape.Positions;

            IAgStkGraphicsPolylinePrimitive line = manager.Initializers.PolylinePrimitive.InitializeWithType(shape.PolylineType);
            line.Set(ref positions);
            line.Width = /*$width$The width of the polyline that makes up the circle$*/2;
            ((IAgStkGraphicsPrimitive)line).Color = /*$color$The color of the circle$*/Color.White;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)line);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)line;
            OverlayHelper.AddTextBox(
@"SurfaceShapes.ComputeCircleCartographic is used to compute the positions 
of a circle on the surface, which is visualized with the polyline primitive.", manager);
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
