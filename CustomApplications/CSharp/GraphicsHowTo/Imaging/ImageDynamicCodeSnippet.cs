using System.Text;

#region UsingDirectives

using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using System;
using System.Windows.Forms;

#endregion

namespace GraphicsHowTo.Imaging
{
    public class ImageDynamicCodeSnippet : CodeSnippet
    {
        public ImageDynamicCodeSnippet()
            : base(@"Imaging\ImageDynamicCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string imageFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/SpinSat_256.gif").FullPath;
            Execute(scene, root, imageFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DisplayARasterStream",
            /* Description */ "Load and display a raster stream",
            /* Category    */ "Graphics | Imaging",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsRasterStream"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "Image file to use for the raster")] string imageFile)
        {
            try
            {
#region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

                //
                // Create the RasterStream from the plugin
                //
                IAgStkGraphicsProjectionRasterStreamPluginActivator activator =
                    manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize();
                IAgStkGraphicsProjectionRasterStreamPluginProxy proxy =
                    activator.CreateFromDisplayName(/*$pluginDisplayName$DisplayName of the ProjectionRasterStreamPlugin$*/"ProjectionRasterStreamPlugin.CSharp");

                //
                // Use reflection to set the plugin's properties
                //
                Type plugin = proxy.RealPluginObject.GetType();
                plugin.GetProperty("RasterPath").SetValue(proxy.RealPluginObject, imageFile, null);

                IAgStkGraphicsRasterStream rasterStream = proxy.RasterStream;
                rasterStream.UpdateDelta = /*$updateDelta$The interval at which the raster is updated$*/0.01667;

                //
                // Creates the texture screen overlay to display the raster
                //
                IAgStkGraphicsRendererTexture2D texture = manager.Textures.FromRaster((IAgStkGraphicsRaster)rasterStream);
                IAgStkGraphicsTextureScreenOverlay overlay =
                    manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(/*$xLocation$The x location of the screen overlay$*/0, /*$yLocation$The y location of the screen overlay$*/0,
                    /*$overlayWidth$The width of the screen overlay$*/texture.Template.Width, /*$overlayHeight$The height of the screen overlay$*/texture.Template.Height);
                overlay.Texture = texture;
                ((IAgStkGraphicsOverlay)overlay).Origin = /*$overlayOrigin$The origin of the screen overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterLeft;

                overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
#endregion

                OverlayHelper.LabelOverlay((IAgStkGraphicsScreenOverlay)overlay, "Raster Stream", manager);

                m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
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
            IAgAnimation animation = (IAgAnimation)root;
            animation.PlayForward();
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            IAgAnimation animation = (IAgAnimation)root;
            animation.Rewind();
            overlayManager.Remove(m_Overlay);
            scene.Render();

            m_Overlay = null;
        }

        private IAgStkGraphicsScreenOverlay m_Overlay;
    }
}
