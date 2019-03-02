#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using System.Windows.Forms;
#endregion

namespace GraphicsHowTo.Primitives.SurfaceMesh
{
    class SurfaceMeshTexturedExtentCodeSnippet : CodeSnippet
    {
        public SurfaceMeshTexturedExtentCodeSnippet()
            : base(@"Primitives\SurfaceMesh\SurfaceMeshTexturedExtentCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string textureFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/originalLogo.png").FullPath;
            Execute(scene, root, textureFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAFilledTexturedExtentOnTerrain",
            /* Description */ "Draw a filled, textured extent on terrain",
            /* Category    */ "Graphics | Primitives | Surface Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("textureFile", "The file to use as the texture of the surface mesh")] string textureFile)
        {
            IAgStkGraphicsSceneManager videoCheck = ((IAgScenario)root.CurrentScenario).SceneManager;

            if (!videoCheck.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod())
            {
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.",
                    "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            IAgStkGraphicsTerrainOverlay overlay = null;
            IAgStkGraphicsTerrainCollection overlays = scene.CentralBodies.Earth.Terrain;
            foreach (IAgStkGraphicsGlobeOverlay eachOverlay in overlays)
            {
                if (eachOverlay.UriAsString.EndsWith("St Helens.pdtt", StringComparison.Ordinal))
                {
                    overlay = (IAgStkGraphicsTerrainOverlay)eachOverlay;
                    break;
                }
            }
            //
            // Don't load terrain if another code snippet already loaded it.
            //
            if (overlay == null)
            {
                overlay = scene.CentralBodies.Earth.Terrain.AddUriString(
                    new AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/St Helens.pdtt").FullPath);
            }

#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array overlayExtent = ((IAgStkGraphicsGlobeOverlay)/*$terrainOverlay$The terrain overlay$*/overlay).Extent;
            IAgStkGraphicsSurfaceTriangulatorResult triangles =
                manager.Initializers.SurfaceExtentTriangulator.ComputeSimple(/*$planetName$The planet on which the surface mesh will be placed$*/"Earth", ref overlayExtent);
            IAgStkGraphicsRendererTexture2D texture = manager.Textures.LoadFromStringUri(
                textureFile);
            IAgStkGraphicsSurfaceMeshPrimitive mesh = manager.Initializers.SurfaceMeshPrimitive.Initialize();
            ((IAgStkGraphicsPrimitive)mesh).Translucency = /*$translucency$The translucency of the surface mesh$*/0.3f;
            mesh.Texture = texture;
            mesh.Set(triangles);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
#endregion

            m_Overlay = (IAgStkGraphicsGlobeOverlay)overlay;
            m_Primitive = (IAgStkGraphicsPrimitive)mesh;
            OverlayHelper.AddTextBox(
@"The surface mesh's Texture property is used to visualize a texture 
conforming to terrain.  For high resolution imagery, it is recommended 
to use a globe overlay.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
                scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

                ViewHelper.ViewExtent(scene, root, "Earth", m_Overlay.Extent, 
                    -135, 30);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null && m_Primitive != null)
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
