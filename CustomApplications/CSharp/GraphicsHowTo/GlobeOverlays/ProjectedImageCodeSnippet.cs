#region UsingDirectives
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.GlobeOverlays
{
    class ProjectedImageCodeSnippet : CodeSnippet
    {
        public ProjectedImageCodeSnippet()
            : base(@"GlobeOverlays\ProjectedImageCodeSnippet.cs")
        {
            m_Terrain = new TerrainOverlayCodeSnippet();
            m_Imagery = new GlobeImageOverlayCodeSnippet();
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string videoFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "ProjectedImagery/fig8.avi").FullPath;
            string providerFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "ProjectedImagery/fig8.txt").FullPath;
            Execute(scene, root, videoFile, providerFile);
        }

        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("videoFile", "Video file to project")] string videoFile, [AGI.CodeSnippets.CodeSnippet.Parameter("providerFile", "Text file containing position and orientation data for the projection")] string providerFile)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgAnimation animation = (IAgAnimation)root;
            IAgScAnimation animationSettings = ((IAgScenario)root.CurrentScenario).Animation;
            IAgScenario scenario = ((IAgScenario)root.CurrentScenario);
            try
            {

                //
                // Set-up the animation for this specific example
                //
                animation.Pause();
                animationSettings.AnimStepValue = 1.0;
                animationSettings.RefreshDelta = 1.0 / 30.000;
                animationSettings.RefreshDeltaType = AgEScRefreshDeltaType.eRefreshDelta;
                scenario.StartTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:00:00.000").Format("epSec"));
                scenario.StopTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:11:58.162").Format("epSec"));
                animationSettings.StartTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:00:00.000").Format("epSec"));
                animationSettings.EnableAnimCycleTime = true;
                animationSettings.AnimCycleTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:11:58.162").Format("epSec"));
                animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime;
                animation.Rewind();


                #region CodeSnippet
                //
                // Add projected raster globe overlay with a raster and projection stream
                //
                IAgStkGraphicsVideoStream videoStream = manager.Initializers.VideoStream.InitializeWithStringUri(
                    videoFile);
                videoStream.Playback = AgEStkGraphicsVideoPlayback.eStkGraphicsVideoPlaybackTimeInterval;
                videoStream.IntervalStartTime = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$startDate$Date to begin the projection$*/"30 May 2008 14:00:00.000");
                videoStream.IntervalEndTime = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$endDate$Date to end the projection$*/"30 May 2008 14:11:58.162");

                PositionOrientationProvider projectionProvider = new PositionOrientationProvider(
                    providerFile, root);

                IAgStkGraphicsProjectionRasterStreamPluginActivator activator =
                    manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize();
                IAgStkGraphicsProjectionRasterStreamPluginProxy proxy =
                    activator.CreateFromDisplayName(/*$pluginDisplayName$Display Name of the ProjectionRasterStreamPlugin$*/"ProjectionRasterStreamPlugin.CSharp");

                //
                // Use reflection to set the plugin's properties
                //
                Type plugin = proxy.RealPluginObject.GetType();
                plugin.GetProperty("NearPlane").SetValue(proxy.RealPluginObject, /*$nearPlane$The near plane of the projection$*/20.0, null);
                plugin.GetProperty("FarPlane").SetValue(proxy.RealPluginObject, /*$farPlane$The far plane of the projection$*/10000.0, null);
                plugin.GetProperty("FieldOfViewHorizontal").SetValue(proxy.RealPluginObject, /*$fieldOfViewHorizontal$The horizontal field of view$*/0.230908805, null);
                plugin.GetProperty("FieldOfViewVertical").SetValue(proxy.RealPluginObject, /*$fieldOfViewVertical$The vertical field of view$*/0.174532925, null);
                plugin.GetProperty("Dates").SetValue(proxy.RealPluginObject, projectionProvider.Dates, null);
                plugin.GetProperty("Positions").SetValue(proxy.RealPluginObject, projectionProvider.Positions, null);
                plugin.GetProperty("Orientations").SetValue(proxy.RealPluginObject, projectionProvider.Orientations, null);


                IAgStkGraphicsProjectionStream projectionStream = proxy.ProjectionStream;

                IAgStkGraphicsProjectedRasterOverlay rasterProjection =
                    manager.Initializers.ProjectedRasterOverlay.Initialize(
                    (IAgStkGraphicsRaster)videoStream, (IAgStkGraphicsProjection)projectionStream);
                rasterProjection.ShowFrustum = /*$showFrustum$Determines if the frustum is shown$*/true;
                rasterProjection.ShowShadows = /*$showShadows$Determines if shadows are shown$*/true;

                scene.CentralBodies.Earth.Imagery.Add((IAgStkGraphicsGlobeImageOverlay)rasterProjection);
                #endregion

                m_Overlay = (IAgStkGraphicsGlobeImageOverlay)rasterProjection;
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

            //
            // Add terrain and imagery
            //
            m_Terrain.Execute(scene, root);
            m_Imagery.Execute(scene, root);

            OverlayHelper.AddTextBox(
@"Video is projected onto terrain by first initializing a VideoStream 
object with a video.  A ProjectedRasterOverlay is then created using 
the video stream and a projection stream defining how to project the 
video onto terrain.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgAnimation animation = (IAgAnimation)root;
            animation.PlayForward();

            m_Terrain.View(scene, root);
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            m_Imagery.Remove(scene, root);
            m_Terrain.Remove(scene, root);

            if (m_Overlay != null)
            {
                scene.CentralBodies["Earth"].Imagery.Remove(m_Overlay);
                
                m_Overlay = null;
                OverlayHelper.RemoveTextBox(((IAgScenario)root.CurrentScenario).SceneManager);
            }

            IAgAnimation animation = (IAgAnimation)root;
            animation.Rewind();
            SetAnimationDefaults(root);
            scene.Render();
        }

        private IAgStkGraphicsGlobeImageOverlay m_Overlay;
        private GlobeImageOverlayCodeSnippet m_Imagery;
        private TerrainOverlayCodeSnippet m_Terrain;
    };
}
