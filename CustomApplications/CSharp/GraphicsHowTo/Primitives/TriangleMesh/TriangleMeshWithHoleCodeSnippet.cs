#region UsingDirectives
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.Primitives.TriangleMesh
{
    class TriangleMeshWithHoleCodeSnippet : CodeSnippet
    {
        public TriangleMeshWithHoleCodeSnippet()
            : base(@"Primitives\TriangleMesh\TriangleMeshWithHoleCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            Array positions = STKUtil.ReadAreaTargetPoints(new AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/LogoBoundary.at").FullPath, root);
            Array holePositions = STKUtil.ReadAreaTargetPoints(new AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/LogoHole.at").FullPath, root);
            Execute(scene, root, positions, holePositions);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAFilledPolygonWithHole",
            /* Description */ "Draw a filled polygon with a hole on the globe",
            /* Category    */ "Graphics | Primitives | Triangle Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("positions", "The positions used to compute triangulation")] Array positions, [AGI.CodeSnippets.CodeSnippet.Parameter("holePositions", "The positions of the hole")] Array holePositions)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgStkGraphicsSurfaceTriangulatorResult triangles = manager.Initializers.SurfacePolygonTriangulator.ComputeWithHole(
                /*$planetName$The planet on which the triangle mesh will be placed$*/"Earth", ref positions, ref holePositions);

            IAgStkGraphicsTriangleMeshPrimitive mesh = manager.Initializers.TriangleMeshPrimitive.Initialize();
            mesh.SetTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
            ((IAgStkGraphicsPrimitive)mesh).Color = /*$color$The System.Drawing.Color of the triangle mesh$*/Color.Gray;
            ((IAgStkGraphicsPrimitive)mesh).Translucency = /*$translucency$The translucency of the triangle mesh$*/0.5f;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);

            IAgStkGraphicsPolylinePrimitive boundaryLine = manager.Initializers.PolylinePrimitive.Initialize();
            Array boundaryPositionsArray = triangles.BoundaryPositions;
            boundaryLine.Set(ref boundaryPositionsArray);
            ((IAgStkGraphicsPrimitive)boundaryLine).Color = /*$outlineColor$The color of the outline around the area target$*/Color.Red;
            boundaryLine.Width = /*$outlineWidth$The width of the outline$*/2;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)boundaryLine);

            IAgStkGraphicsPolylinePrimitive holeLine = manager.Initializers.PolylinePrimitive.Initialize();
            holeLine.Set(ref holePositions);
            ((IAgStkGraphicsPrimitive)holeLine).Color = /*$holeOutlineColor$The System.Drawing.Color of the hole's outline$*/Color.Red;
            holeLine.Width = /*$holeOutlineWidth$The width of the hole's outline$*/2;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)holeLine);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)mesh;
            m_BoundaryLine = (IAgStkGraphicsPrimitive)boundaryLine;
            m_HoleLine = (IAgStkGraphicsPrimitive)holeLine;
            OverlayHelper.AddTextBox(
@"SurfacePolygonTriangulator.Compute has overloads that take the exterior 
boundary positions as well as positions for an interior hole.", manager);
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
            manager.Primitives.Remove(m_BoundaryLine);
            manager.Primitives.Remove(m_HoleLine);
            m_Primitive = null;
            m_BoundaryLine = null;
            m_HoleLine = null;

            OverlayHelper.RemoveTextBox(manager);
            scene.Render();
        }

        private IAgStkGraphicsPrimitive m_Primitive;
        private IAgStkGraphicsPrimitive m_BoundaryLine;
        private IAgStkGraphicsPrimitive m_HoleLine;
    };
}
