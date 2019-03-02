#region UsingDirectives
using System;
using System.IO;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
using System.Text;
using System.Net;
#endregion

namespace GraphicsHowTo.GlobeOverlays
{
    class OpenStreetMapCodeSnippet : CodeSnippet
    {
        public OpenStreetMapCodeSnippet()
            : base(@"GlobeOverlays\OpenStreetMapCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddCustomGlobeOverlayImagery",
            /* Description */ "Add custom imagery to the globe",
            /* Category    */ "Graphics | GlobeOverlays",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsCustomImageGlobeOverlay"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
			IAgStkGraphicsSceneManager manager2 = ((IAgScenario)root.CurrentScenario).SceneManager;
            try
            {
                #region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

                IAgStkGraphicsCustomImageGlobeOverlayPluginActivator activator =
                    manager.Initializers.CustomImageGlobeOverlayPluginActivator.Initialize();
                IAgStkGraphicsCustomImageGlobeOverlayPluginProxy proxy =
                    activator.CreateFromDisplayName(/*$pluginDisplayName$Display Name of the OpenStreetMapPlugin$*/"OpenStreetMapPlugin.CSharp");

                IAgStkGraphicsCustomImageGlobeOverlay overlay = proxy.CustomImageGlobeOverlay;
                scene.CentralBodies.Earth.Imagery.Add((IAgStkGraphicsGlobeImageOverlay)overlay);
                #endregion

                m_Overlay = (IAgStkGraphicsGlobeImageOverlay)overlay;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("OpenStreetMapPlugin"))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("A COM exception has occurred.\n\n");
                    sb.Append("It is possible that one of the following may be the issue:\n\n");
                    sb.Append("1. OpenStreetMapPlugin.dll is not registered for COM interop.\n\n");
                    sb.Append("2. That the plugin has not been added to the GfxPlugin category within a <install dir>\\Plugins\\*.xml file.\n\n");
                    sb.Append("To resolve either of these issues:\n\n");
                    sb.Append("1. To register the plugin, open a Visual Studio ");
                    if (IntPtr.Size == 8)
                        sb.Append("x64 ");
                    sb.Append("Command Prompt and execute the command:\n\n");
                    sb.Append("\tregasm /codebase \"<install dir>\\<CodeSamples>\\Extend\\Graphics\\CSharp\\OpenStreetMapPlugin\\bin\\<Config>\\OpenStreetMapPlugin.dll\"\n\n");
                    sb.Append("\tNote: if you do not have access to a Visual Studio Command Prompt regasm can be found here:\n");
                    sb.Append("\tC:\\Windows\\Microsoft.NET\\Framework");
                    if (IntPtr.Size == 8)
                        sb.Append("64");
                    sb.Append("\\<.NET Version>\\\n\n");
                    sb.Append("2. To add it to the GfxPlugins plugins registry category:\n\n");
                    sb.Append("\ta. Copy the Graphics.xml from the <install dir>\\CodeSamples\\Extend\\Graphics\\Graphics.xml file to the <install dir>\\Plugins directory.\n\n");
                    sb.Append("\tb. Then uncomment the plugin entry that contains a display name of OpenStreetMapPlugin.CSharp.\n\n");

                    MessageBox.Show(sb.ToString(), "Plugin Not Registered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Could not create globe overlay.  Your video card may not support this feature.",
                        "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return;
            }

            OverlayHelper.AddTextBox(
@"Create an OpenStreetMapImageGlobeOverlay, with an 
optional extent. This example requires an active
internet connection, otherwise no data is shown.", manager2);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                scene.CentralBodies["Earth"].Imagery.Remove(m_Overlay);
                scene.Render();

                m_Overlay = null;
            }

            OverlayHelper.RemoveTextBox(((IAgScenario)root.CurrentScenario).SceneManager);
            scene.Render();
        }

        private IAgStkGraphicsGlobeImageOverlay m_Overlay;
    };
}
