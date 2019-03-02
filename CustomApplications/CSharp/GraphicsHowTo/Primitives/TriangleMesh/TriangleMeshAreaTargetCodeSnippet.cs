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
    public class TriangleMeshAreaTargetCodeSnippet : CodeSnippet
    {
        public TriangleMeshAreaTargetCodeSnippet()
            : base(@"Primitives\TriangleMesh\TriangleMeshAreaTargetCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            Array positions = STKUtil.ReadAreaTargetPoints(new AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/_pennsylvania_1.at").FullPath, root);
            Execute(scene, root, positions);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAFilledAreaTarget",
            /* Description */ "Draw a filled STK area target",
            /* Category    */ "Graphics | Primitives | Triangle Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("positions", "The positions used to compute triangulation")] Array positions)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgStkGraphicsSurfaceTriangulatorResult triangles =
                manager.Initializers.SurfacePolygonTriangulator.Compute(/*$planetName$The planet on which the triangle mesh will be placed$*/"Earth", ref positions);

            IAgStkGraphicsTriangleMeshPrimitive mesh = manager.Initializers.TriangleMeshPrimitive.Initialize();
            mesh.SetTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
            ((IAgStkGraphicsPrimitive)mesh).Color = /*$color$The System.Drawing.Color of the triangle mesh$*/Color.Red;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)mesh;
            OverlayHelper.AddTextBox(
                @"Positions defining the boundary of an STK area target are read from 
disk.  SurfacePolygonTriangulator.Compute computes triangles for the area 
target's interior, which are then visualized with a TriangleMeshPrimitive.", manager);
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
    }
}