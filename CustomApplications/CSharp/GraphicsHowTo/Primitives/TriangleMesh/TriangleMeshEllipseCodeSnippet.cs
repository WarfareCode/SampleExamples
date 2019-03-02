#region UsingDirectives
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.Primitives.TriangleMesh
{
    class TriangleMeshEllipseCodeSnippet : CodeSnippet
    {
        public TriangleMeshEllipseCodeSnippet()
            : base(@"Primitives\TriangleMesh\TriangleMeshEllipseCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAnEllipse",
            /* Description */ "Draw a filled ellipse on the globe",
            /* Category    */ "Graphics | Primitives | Triangle Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array center = new object[] { /*$lat$The latitude of the ellipse$*/38.85, /*$lon$The longitude of the center of the ellipse$*/-77.04, /*$alt$The altitude of the center of the ellipse$*/3000.0 }; // Washington, DC

            IAgStkGraphicsSurfaceShapesResult shape = manager.Initializers.SurfaceShapes.ComputeEllipseCartographic(
                /*$planetName$The planet on which the ellipse will be placed$*/"Earth", ref center, /*$majorAxisRadius$The radius of the major axis of the ellipse$*/45000, /*$minorAxisRadius$The radius of the minor axis of the ellipse$*/30000, 45);
            Array positions = shape.Positions;

            IAgStkGraphicsSurfaceTriangulatorResult triangles = manager.Initializers.SurfacePolygonTriangulator.Compute(
                /*$planetName$The planet on which the ellipse will be placed$*/"Earth", ref positions);
            IAgStkGraphicsTriangleMeshPrimitive mesh = manager.Initializers.TriangleMeshPrimitive.Initialize();
            mesh.SetTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
            ((IAgStkGraphicsPrimitive)mesh).Color = /*$color$The System.Drawing.Color of the ellipse$*/Color.Cyan;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)mesh;
            OverlayHelper.AddTextBox(
@"Boundary positions for an ellipse are computed with 
SurfaceShapes.ComputeEllipseCartographic.  Triangles for the ellipse's
interior are then computed with SurfacePolygonTriangulator.Compute and 
visualized with a TriangleMeshPrimitive.", manager);
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
