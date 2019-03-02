#region UsingDirectives
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.Primitives.TriangleMesh
{
    class TriangleMeshExtentCodeSnippet : CodeSnippet
    {
        public TriangleMeshExtentCodeSnippet()
            : base(@"Primitives\TriangleMesh\TriangleMeshExtentCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAnExtent",
            /* Description */ "Draw a filled rectangular extent on the globe",
            /* Category    */ "Graphics | Primitives | Triangle Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            Array extent = new object[]
            {
                /*$westLon$Westernmost longitude$*/-94, /*$southLat$Southernmost latitude$*/29,
                /*$eastLon$Easternmost longitude$*/-89, /*$northLat$Northernmost latitude$*/33
            };

            IAgStkGraphicsSurfaceTriangulatorResult triangles = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple(
                    /*$planetName$The planet on which the triangle mesh will be placed$*/"Earth", ref extent);

            IAgStkGraphicsTriangleMeshPrimitive mesh = manager.Initializers.TriangleMeshPrimitive.Initialize();
            mesh.SetTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
            ((IAgStkGraphicsPrimitive)mesh).Color = /*$color$The System.Drawing.Color of the triangle mesh$*/Color.Salmon;
            mesh.Lighting = false;  /* Turn off lighting for the mesh so the color we assigned will always be consistent */ 

            manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)mesh;
            OverlayHelper.AddTextBox(
@"SurfaceExtentTriangulator.Compute computes triangles for a rectangular 
extent bounded by lines of constant latitude and longitude.", manager);
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
