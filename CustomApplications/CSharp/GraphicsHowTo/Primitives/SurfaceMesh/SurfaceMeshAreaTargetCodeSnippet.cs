#region UsingDirectives
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives.SurfaceMesh
{
    class SurfaceMeshAreaTargetCodeSnippet : CodeSnippet
    {
        public SurfaceMeshAreaTargetCodeSnippet()
            : base(@"Primitives\SurfaceMesh\SurfaceMeshAreaTargetCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string terrainFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/925.pdtt").FullPath;
            Array positions = STKUtil.ReadAreaTargetPoints(new AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/925.at").FullPath, root);
            Execute(scene, root, terrainFile, positions);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAnAreaTargetOnTerrain",
            /* Description */ "Draw a filled STK area target on terrain",
            /* Category    */ "Graphics | Primitives | Surface Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("terrainFile", "The terrain file")] string terrainFile, [AGI.CodeSnippets.CodeSnippet.Parameter("positions", "The positions used to compute triangulation")] Array positions)
        {
            IAgStkGraphicsSceneManager videoCheck = ((IAgScenario)root.CurrentScenario).SceneManager;
            if (!videoCheck.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod())
            {
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.",
                    "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsTerrainOverlay overlay = scene.CentralBodies.Earth.Terrain.AddUriString(
                terrainFile);

            IAgStkGraphicsSurfaceTriangulatorResult triangles =
                manager.Initializers.SurfacePolygonTriangulator.Compute(/*$planetName$The planet on which the surface mesh is to be placed$*/"Earth", ref positions);

            IAgStkGraphicsSurfaceMeshPrimitive mesh = manager.Initializers.SurfaceMeshPrimitive.Initialize();
            ((IAgStkGraphicsPrimitive)mesh).Color = /*$color$The color of the surface mesh$*/Color.Purple;
            mesh.Set(triangles);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
#endregion

            m_Overlay = (IAgStkGraphicsGlobeOverlay)overlay;
            m_Primitive = (IAgStkGraphicsPrimitive)mesh;
            OverlayHelper.AddTextBox(
@"Similar to the triangle mesh example, triangles for the interior of an 
STK area target are computed using SurfacePolygonTriangulator.Compute.  
This is used an input to a SurfaceMeshPrimitive, which makes the 
visualization conform to terrain.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
                scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

                ViewHelper.ViewExtent(scene, root, "Earth", m_Overlay.Extent,
                    -45, 30);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                scene.CentralBodies["Earth"].Terrain.Remove((IAgStkGraphicsTerrainOverlay)m_Overlay);
                manager.Primitives.Remove(m_Primitive);
                m_Primitive = null;
                m_Overlay = null;

                OverlayHelper.RemoveTextBox(manager);
                scene.Render();
            }
        }

        private IAgStkGraphicsPrimitive m_Primitive;
        private IAgStkGraphicsGlobeOverlay m_Overlay;
    };
}
