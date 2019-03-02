#region UsingDirectives

using System.Text;
using System.IO;
using System.Drawing;
using GraphicsHowTo.Imaging;
using AGI.STKGraphics;
using AGI.STKObjects;
using System;
using System.Windows.Forms;

#endregion

namespace GraphicsHowTo.ScreenOverlays
{
    public class OverlaysPanelCodeSnippet : CodeSnippet
    {
        public OverlaysPanelCodeSnippet()
            : base(@"ScreenOverlays\OverlaysPanelCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string imageFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/originalLogo.png").FullPath;
            string rasterFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/SpinSat_256.gif").FullPath;
            Execute(scene, root, imageFile, rasterFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddAPanelOverlay",
            /* Description */ "Add overlays to a panel overlay",
            /* Category    */ "Graphics | ScreenOverlays",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsScreenOverlay"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "The image file to use for the child overlay")] string imageFile, [AGI.CodeSnippets.CodeSnippet.Parameter("rasterFile", "The raster file for the second child overlay")] string rasterFile)
        {
            try
            {
                #region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

                IAgStkGraphicsTextureScreenOverlay overlay =
                    manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(/*$xLocation$The x location of the screen overlay$*/0, /*$yLocation$The y location of the screen overlay$*/0,
                    /*$overlayWidth$The width of the screen overlay$*/188, /*$overlayHeight$The height of the screen overlay$*/200);
                ((IAgStkGraphicsOverlay)overlay).Origin = /*$origin$The origin of the screen overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft;
                ((IAgStkGraphicsOverlay)overlay).Color = /*$color$The System.Drawing.Color of the screen overlay$*/Color.LightSkyBlue;
                ((IAgStkGraphicsOverlay)overlay).Translucency = /*$translucency$The translucency of the screen overlay$*/0.7f;
                ((IAgStkGraphicsOverlay)overlay).BorderTranslucency = /*$borderTranslucency$The translucency of the border$*/0.3f;
                ((IAgStkGraphicsOverlay)overlay).BorderSize = /*$borderWidth$The width of the border$*/1;
                ((IAgStkGraphicsOverlay)overlay).BorderColor = /*$borderColor$The System.Drawing.Color of the border$*/Color.LightBlue;

                IAgStkGraphicsTextureScreenOverlay childOverlay =
                    manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(
                    /*$childXLocation$The x location of the child overlay$*/0, /*$childYLocation$The y location of the child overlay$*/0, ((IAgStkGraphicsOverlay)overlay).Width, ((IAgStkGraphicsOverlay)overlay).Height);
                ((IAgStkGraphicsOverlay)childOverlay).Origin = /*$childOrigin$The origin of the child overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenter;

                childOverlay.Texture = manager.Textures.LoadFromStringUri(imageFile);

                //
                // Create the RasterStream from the plugin the same way as in ImageDynamicCodeSnippet.cs
                //
                IAgStkGraphicsProjectionRasterStreamPluginActivator activator =
                    manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize();
                IAgStkGraphicsProjectionRasterStreamPluginProxy proxy =
                    activator.CreateFromDisplayName(/*$pluginDisplayName$Display Name of the ProjectionRasterStreamPlugin$*/"ProjectionRasterStreamPlugin.CSharp");
                Type plugin = proxy.RealPluginObject.GetType();
                plugin.GetProperty("RasterPath").SetValue(proxy.RealPluginObject, rasterFile, null);

                IAgStkGraphicsRasterStream rasterStream = proxy.RasterStream;
                rasterStream.UpdateDelta = /*$updateDelta$The interval at which the update method will be called$*/0.01667;
                IAgStkGraphicsRendererTexture2D texture2D = manager.Textures.FromRaster((IAgStkGraphicsRaster)rasterStream);

                IAgStkGraphicsTextureScreenOverlay secondChildOverlay =
                    manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(/*$secondChildXLocation$The x location of the second child overlay$*/0, /*$secondChildYLocation$The y location of the screen overlay$*/0,
                    /*$secondChildWidth$The width of the second child overlay$*/128, /*$secondChildHeight$The height of the second child overlay$*/128);
                ((IAgStkGraphicsOverlay)secondChildOverlay).Origin = /*$secondChildOrigin$The origin of the second child overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight;
                ((IAgStkGraphicsOverlay)secondChildOverlay).TranslationX = /*$secondChildXTranslation$The X translation of the second child overlay$*/-36;
                ((IAgStkGraphicsOverlay)secondChildOverlay).TranslationY = /*$secondChildYTranslation$The Y translation of the second child overlay$*/-18;
                ((IAgStkGraphicsOverlay)secondChildOverlay).ClipToParent = /*$secondChildClipToParent$Whether or not the second child overlay should clip to its parent$*/false;
                secondChildOverlay.Texture = texture2D;

                overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);

                IAgStkGraphicsScreenOverlayCollectionBase parentOverlayManager = ((IAgStkGraphicsOverlay)overlay).Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
                parentOverlayManager.Add((IAgStkGraphicsScreenOverlay)childOverlay);

                IAgStkGraphicsScreenOverlayCollectionBase childOverlayManager = ((IAgStkGraphicsOverlay)childOverlay).Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
                childOverlayManager.Add((IAgStkGraphicsScreenOverlay)secondChildOverlay);
                #endregion

                m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
                ((IAgAnimation)root).PlayForward();
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
                    MessageBox.Show("Could not create overlay.  Your video card may not support this feature.",
                        "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            //
            // Overlays are always fixed to the screen regardless of view
            //
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

                ((IAgAnimation)root).Rewind();
                overlayManager.Remove(m_Overlay);
                scene.Render();

                m_Overlay = null;
            }
        }

        private IAgStkGraphicsScreenOverlay m_Overlay;
    }
}