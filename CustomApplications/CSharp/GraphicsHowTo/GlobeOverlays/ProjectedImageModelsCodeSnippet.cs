using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;

#region UsingDirectives
using System;
using System.Globalization;
using AGI.STKGraphics;
using AGI.STKObjects;
using System.Drawing;
using AGI.STKUtil;
using AGI.STKVgt;
#endregion

namespace GraphicsHowTo
{
    class ProjectedImageModelsCodeSnippet : CodeSnippet
    {
        public ProjectedImageModelsCodeSnippet()
            : base(@"GlobeOverlays\ProjectedImageModelsCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string videoFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "ProjectedImagery/buildings.avi").FullPath;
            string providerFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "ProjectedImagery/buildings.txt").FullPath;
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
                animationSettings.AnimStepValue = 1.0 / 7.5;
                animationSettings.RefreshDelta = 1.0 / 15.0;
                animationSettings.RefreshDeltaType = AgEScRefreshDeltaType.eRefreshDelta;
                scenario.StopTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "05 Oct 2010 16:00:52.000").Format("epSec"));
                scenario.StartTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "05 Oct 2010 16:00:00.000").Format("epSec"));
                animationSettings.StartTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "05 Oct 2010 16:00:00.000").Format("epSec"));
                animationSettings.EnableAnimCycleTime = true;
                animationSettings.AnimCycleTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "05 Oct 2010 16:00:52.000").Format("epSec"));
                animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime;
                animation.Rewind();

#region CodeSnippet
                //
                // Enable Raster Model Projection
                //
                scene.GlobeOverlaySettings.ProjectedRasterModelProjection = true;

                //
                // Add projected raster globe overlay with a raster and projection stream
                //
                IAgStkGraphicsVideoStream videoStream = manager.Initializers.VideoStream.InitializeWithStringUri(videoFile);
                videoStream.Playback = AgEStkGraphicsVideoPlayback.eStkGraphicsVideoPlaybackTimeInterval;
                videoStream.IntervalStartTime = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$startDate$Date to begin the projection$*/"05 Oct 2010 16:00:00.000");
                videoStream.IntervalEndTime = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$endDate$Date to end the projection$*/"05 Oct 2010 16:00:52.000");

                PositionOrientationProvider projectionProvider = new PositionOrientationProvider(providerFile, root);

                IAgStkGraphicsProjectionRasterStreamPluginActivator activator =
                    manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize();
                IAgStkGraphicsProjectionRasterStreamPluginProxy proxy =
                    activator.CreateFromDisplayName(/*$pluginDisplayName$DisplayName of the ProjectionRasterStreamPlugin$*/"ProjectionRasterStreamPlugin.CSharp");

                //
                // Use reflection to set the plugin's properties
                //
                Type plugin = proxy.RealPluginObject.GetType();
                plugin.GetProperty("NearPlane").SetValue(proxy.RealPluginObject, /*$nearPlane$The near plane of the projection$*/20.0, null);
                plugin.GetProperty("FarPlane").SetValue(proxy.RealPluginObject, /*$farPlane$The far plane of the projection$*/300.0, null);
                plugin.GetProperty("FieldOfViewHorizontal").SetValue(proxy.RealPluginObject, /*$fieldOfViewHorizontal$The horizontal field of view$*/0.232709985, null);
                plugin.GetProperty("FieldOfViewVertical").SetValue(proxy.RealPluginObject, /*$fieldOfViewVertical$The vertical field of view$*/0.175929193, null);
                plugin.GetProperty("Dates").SetValue(proxy.RealPluginObject, projectionProvider.Dates, null);
                plugin.GetProperty("Positions").SetValue(proxy.RealPluginObject, projectionProvider.Positions, null);
                plugin.GetProperty("Orientations").SetValue(proxy.RealPluginObject, projectionProvider.Orientations, null);


                IAgStkGraphicsProjectionStream projectionStream = proxy.ProjectionStream;

                IAgStkGraphicsProjectedRasterOverlay rasterProjection =
                    manager.Initializers.ProjectedRasterOverlay.Initialize(
                    (IAgStkGraphicsRaster)videoStream, (IAgStkGraphicsProjection)projectionStream);
                rasterProjection.ShowFrustum = /*$showFrustum$Determines if the frustum is shown$*/true;
                rasterProjection.FrustumColor = /*$frustumColor$The System.Drawing.Color of the frustum$*/Color.Black;
                rasterProjection.FrustumTranslucency = /*$frustumTranslucency$The translucency of the frustum$*/.5f;
                rasterProjection.ShowShadows = /*$showShadows$Determines if shadows are shown$*/true;
                rasterProjection.ShadowColor = /*$shadowColor$The System.Drawing.Color of the shadow$*/Color.Orange;
                rasterProjection.ShadowTranslucency = /*$shadowTranslucency$The translucency of the shadow$*/.5f;
                rasterProjection.ShowFarPlane = /*$showFarPlane$Determines if the far plane is shown$*/true;
                rasterProjection.FarPlaneColor = /*$farPlaneColor$The System.Drawing.Color of the far plane$*/Color.LightBlue;
                rasterProjection.Color = /*$tintColor$The System.Drawing.Color of the tint to add to the raster$*/Color.LightBlue;
                ((IAgStkGraphicsGlobeImageOverlay)rasterProjection).Translucency = /*$rasterTranslucency$The translucency of the raster$*/.2f;

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
            // Add model
            //
            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/phoenix_gray/phoenix.dae").FullPath);

            Array position = new object[]
            {
                33.4918312268, -112.0751720286, 0.0
            };
            IAgPosition origin = root.ConversionUtility.NewPositionOnEarth();
            origin.AssignPlanetodetic((double)position.GetValue(0), (double)position.GetValue(1), (double)position.GetValue(2));
            IAgCrdnAxesFixed axes = CreateAxes(root, "Earth", origin);
            IAgCrdnSystem system = CreateSystem(root, "Earth", origin, axes);
            IAgCrdnAxesFindInAxesResult result = root.VgtRoot.WellKnownAxes.Earth.Fixed.FindInAxes(((IAgScenario)root.CurrentScenario).Epoch, ((IAgCrdnAxes)axes));
            
            model.SetPositionCartographic("Earth", ref position);
            model.Orientation = result.Orientation;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);

            m_Primitive = (IAgStkGraphicsPrimitive)model;            

            OverlayHelper.AddTextBox(
@"Video is projected onto a model by first initializing a VideoStream 
object with a video.  A ProjectedRasterOverlay is then created using 
the video stream and a projection stream defining how to project the 
video onto the model. Shadows are visualized in orange.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Primitive == null)
                return;

            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgAnimation animation = (IAgAnimation)root;
            animation.PlayForward();

            Array center = m_Primitive.BoundingSphere.Center;
            IAgStkGraphicsBoundingSphere boundingSphere = manager.Initializers.BoundingSphere.Initialize(ref center, 100);

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere, 20, 25);

            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgAnimation animation = (IAgAnimation)root;

            manager.Primitives.Remove(m_Primitive);

            if (m_Overlay != null)
            {
                scene.CentralBodies["Earth"].Imagery.Remove(m_Overlay);

                m_Overlay = null;
                m_Primitive = null;

                OverlayHelper.RemoveTextBox(manager);    
            }

            //
            // Disable Raster Model Projection
            //
            scene.GlobeOverlaySettings.ProjectedRasterModelProjection = false;

            animation.Rewind();
            SetAnimationDefaults(root);
            scene.Render();
        }

        private IAgStkGraphicsPrimitive m_Primitive;
        private IAgStkGraphicsGlobeImageOverlay m_Overlay;
    };
}
