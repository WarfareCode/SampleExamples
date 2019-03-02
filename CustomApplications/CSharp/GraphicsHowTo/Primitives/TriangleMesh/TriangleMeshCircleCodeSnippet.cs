#region UsingDirectives
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.Primitives.TriangleMesh
{
    class TriangleMeshCircleCodeSnippet : CodeSnippet
    {
        public TriangleMeshCircleCodeSnippet()
            : base(@"Primitives\TriangleMesh\TriangleMeshCircleCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAFilledCircle",
            /* Description */ "Draw a filled circle on the globe",
            /* Category    */ "Graphics | Primitives | Triangle Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array center = new object[] { /*$lat$The latitude of the center$*/39.88, /*$lon$The longitude of the center$*/-75.25, /*$alt$The altitude of the center$*/0.0 };

            IAgStkGraphicsSurfaceShapesResult shape = manager.Initializers.SurfaceShapes.ComputeCircleCartographic(/*$planetName$Name of the planet to place primitive$*/"Earth", ref center, /*$radius$The radius of the circle.$*/10000);
            Array positions = shape.Positions;
            IAgStkGraphicsSurfaceTriangulatorResult triangles = manager.Initializers.SurfacePolygonTriangulator.Compute(/*$planetName$Name of the planet to place primitive$*/"Earth", ref positions);

            IAgStkGraphicsTriangleMeshPrimitive mesh = manager.Initializers.TriangleMeshPrimitive.Initialize();
            mesh.SetTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
            ((IAgStkGraphicsPrimitive)mesh).Color = /*$color$The System.Drawing.Color of the circle$*/Color.White;
            ((IAgStkGraphicsPrimitive)mesh).Translucency = /*$translucency$The translucency of the circle$*/0.5f;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)mesh;
            OverlayHelper.AddTextBox(
@"Boundary positions for a circle are computed with 
SurfaceShapes.ComputeCircleCartographic.  Triangles for the circle's 
interior are then computed with SurfacePolygonTriangulator.Compute 
and visualized with a TriangleMeshPrimitive.", manager);
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
