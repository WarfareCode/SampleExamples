using System.Windows.Forms;
using System.Text;

#region UsingDirectives
using System;
using System.IO;
using System.Runtime.InteropServices;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Primitives.SurfaceMesh
{
    class SurfaceMeshDynamicImageCodeSnippet : CodeSnippet
    {
        public SurfaceMeshDynamicImageCodeSnippet()
            : base(@"Primitives\SurfaceMesh\SurfaceMeshDynamicImageCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string rasterFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/lava.gif").FullPath;
            Execute(scene, root, rasterFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawADynamicallyTexturedExtentOnTerrain",
            /* Description */ "Draw a filled, dynamically textured extent on terrain",
            /* Category    */ "Graphics | Primitives | Surface Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("rasterFile", "The file to use as the raster")] string rasterFile)
        {
            IAgStkGraphicsSceneManager manager2 = ((IAgScenario)root.CurrentScenario).SceneManager;

            if (!manager2.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod())
            {
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.",
                    "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            IAgStkGraphicsTerrainOverlay overlay = null;
            IAgStkGraphicsTerrainCollection overlays = scene.CentralBodies.Earth.Terrain;
            foreach (IAgStkGraphicsTerrainOverlay eachOverlay in overlays)
            {
                if (((IAgStkGraphicsGlobeOverlay)eachOverlay).UriAsString.EndsWith("St Helens.pdtt", StringComparison.Ordinal))
                {
                    overlay = eachOverlay;
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

            try
            {
                #region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgStkGraphicsProjectionRasterStreamPluginActivator activator =
                    manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize();
                IAgStkGraphicsProjectionRasterStreamPluginProxy proxy =
                    activator.CreateFromDisplayName(/*$pluginDisplayName$Display Name of the ProjectionRasterStreamPlugin$*/"ProjectionRasterStreamPlugin.CSharp");

                //
                // Use reflection to set the plugin's properties
                //
                Type plugin = proxy.RealPluginObject.GetType();
                plugin.GetProperty("RasterPath").SetValue(proxy.RealPluginObject, rasterFile, null);

                IAgStkGraphicsRasterStream rasterStream = proxy.RasterStream;
                rasterStream.UpdateDelta = /*$updateDelta$The interval at which the Update method will be called$*/0.025;

                IAgStkGraphicsRendererTexture2D texture = manager.Textures.FromRaster((IAgStkGraphicsRaster)rasterStream);
                Array extent = ((IAgStkGraphicsGlobeOverlay)/*$terrainOverlay$The terrain overlay$*/overlay).Extent;
                IAgStkGraphicsSurfaceTriangulatorResult triangles = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple("Earth", ref extent);
                IAgStkGraphicsSurfaceMeshPrimitive mesh = manager.Initializers.SurfaceMeshPrimitive.Initialize();
                ((IAgStkGraphicsPrimitive)mesh).Translucency = /*$translucency$The translucency of the surface mesh$*/0.2f;
                mesh.Texture = texture;
                mesh.Set(triangles);
                manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
                #endregion

                m_Overlay = overlay;
                m_Primitive = (IAgStkGraphicsPrimitive)mesh;
                OverlayHelper.AddTextBox(
    @"Dynamic textures are created by creating a class that derives 
from RasterStream that provides time dependent textures.", manager);

            }
            catch (Exception e)
            {
                if (e.Message.Contains("ProjectionRasterStreamPlugin"))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("A COM exception has occurred.\n\n");
                    sb.Append("It is possible that one of the following may be the issue:\n\n");
                    sb.Append("1. ProjectionRasterStreamPlugin.dll is not registered for COM interop.\n\n");
                    sb.Append("2. That the plugin has not been added to the GfxPlugin category within a <install dir>\\Plugins\\*.xml file.\n\n");
                    sb.Append("To resolve either of these issues:\n\n");
                    sb.Append("1. To register the plugin, open a Visual Studio ");
                    if (IntPtr.Size == 8)
                        sb.Append("x64 ");
                    sb.Append("Command Prompt and execute the command:\n\n");
                    sb.Append("\tregasm /codebase \"<install dir>\\<CodeSamples>\\Extend\\Graphics\\CSharp\\ProjectionRasterStreamPlugin\\bin\\<Config>\\ProjectionRasterStreamPlugin.dll\"\n\n");
                    sb.Append("\tNote: if you do not have access to a Visual Studio Command Prompt regasm can be found here:\n");
                    sb.Append("\tC:\\Windows\\Microsoft.NET\\Framework");
                    if (IntPtr.Size == 8)
                        sb.Append("64");
                    sb.Append("\\<.NET Version>\\\n\n");
                    sb.Append("2. To add it to the GfxPlugins plugins registry category:\n\n");
                    sb.Append("\ta. Copy the Graphics.xml from the <install dir>\\CodeSamples\\Extend\\Graphics\\Graphics.xml file to the <install dir>\\Plugins directory.\n\n");
                    sb.Append("\tb. Then uncomment the plugin entry that contains a display name of ProjectionRasterStreamPlugin.CSharp.\n\n");

                    MessageBox.Show(sb.ToString(), "Plugin Not Registered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Could not create globe overlay.  Your video card may not support this feature.",
                        "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                IAgAnimation animation = (IAgAnimation)root;
                
                scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
                scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

                ViewHelper.ViewExtent(scene, root, "Earth", ((IAgStkGraphicsGlobeOverlay)m_Overlay).Extent,
                    -135, 30);

                animation.PlayForward();
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null && m_Primitive != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgAnimation animation = (IAgAnimation)root;

                animation.Rewind();
                scene.CentralBodies["Earth"].Terrain.Remove(m_Overlay);
                manager.Primitives.Remove(m_Primitive);
                m_Primitive = null;
                m_Overlay = null;

                OverlayHelper.RemoveTextBox(manager);
                scene.Render();
            }
        }

        private IAgStkGraphicsPrimitive m_Primitive;
        private IAgStkGraphicsTerrainOverlay m_Overlay;
    };
}
