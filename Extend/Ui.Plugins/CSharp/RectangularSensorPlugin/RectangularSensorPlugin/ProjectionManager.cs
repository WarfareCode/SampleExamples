using System;
using AGI.STKGraphics;
using AGI.STKObjects;
using RectangularSensorStreamPluginProxy;

namespace RectangularSensorPlugin
{
    internal static class ProjectionManager
    {
        public static void AddProjection(AgStkObjectRoot root, SensorAttributes attributes)
        {
            IAgStkGraphicsSceneManager sceneManager = ((IAgScenario)root.CurrentScenario).SceneManager;
            
            // Call into static proxy to share attributes
            RectangularSensorStreamPluginProxy.PluginProxy.ProxySensorAttributes = attributes;

            // Create the projection stream from the plugin
            IAgStkGraphicsProjectionRasterStreamPluginActivator activator = 
                sceneManager.Initializers.ProjectionRasterStreamPluginActivator.Initialize();
            IAgStkGraphicsProjectionRasterStreamPluginProxy proxy = 
                activator.CreateFromDisplayName("SensorStreamingPlugin");
            IAgStkGraphicsProjectionStream projectionStream = proxy.ProjectionStream;

            Uri uri = new Uri(attributes.Uri);
            if (attributes.UriIsCompatibleVideo)
            {
                // Since the URI can be represented as a video, create a VideoStream to use as the raster
                IAgStkGraphicsVideoStream videoStream = sceneManager.Initializers.VideoStream.InitializeWithStringUri(uri.LocalPath);
                videoStream.StartTime = TimeSpan.Parse(attributes.VideoStartTime).TotalSeconds;
                videoStream.EndTime = TimeSpan.Parse(attributes.VideoEndTime).TotalSeconds;

                if (attributes.UseRealTime == true)
                {
                    videoStream.Playback = AgEStkGraphicsVideoPlayback.eStkGraphicsVideoPlaybackRealTime;
                    if (attributes.UseManualFrameRate == true)
                    {
                        videoStream.FrameRate = attributes.FrameRate;
                    }
                    videoStream.Loop = attributes.Loop;
                }
                else if (attributes.UseTimeInterval == true)
                {
                    videoStream.Playback = AgEStkGraphicsVideoPlayback.eStkGraphicsVideoPlaybackTimeInterval;
                    videoStream.IntervalStartTime = root.ConversionUtility.NewDate("UTCG", attributes.TimeIntervalStart);
                    videoStream.IntervalEndTime = root.ConversionUtility.NewDate("UTCG", attributes.TimeIntervalEnd);
                }

                // Create a ProjectedRasterOverlay using the VideoStream and set its properties
                IAgStkGraphicsProjectedRasterOverlay videoOverlay = sceneManager.Initializers.ProjectedRasterOverlay.Initialize(
                    (IAgStkGraphicsRaster)videoStream, (IAgStkGraphicsProjection)projectionStream);
                SetGeneralOverlayProperties(videoOverlay, attributes);

                // Add the ProjectedRasterOverlay to the globe
                sceneManager.Scenes[0].CentralBodies.Earth.Imagery.Add(
                    (IAgStkGraphicsGlobeImageOverlay)videoOverlay);
                sceneManager.Render();

                // Record the GlobeImageOverlay of this sensor's projection so it can be manipulated later
                attributes.IsProjectionAdded = true;
                attributes.ProjectionOverlay = (IAgStkGraphicsGlobeImageOverlay)videoOverlay;
            }
            else
            {
                // The URI was not a video, so create a normal Raster
                IAgStkGraphicsRaster raster = sceneManager.Initializers.Raster.InitializeWithStringUri(uri.LocalPath);
                
                // Create a ProjectedRasterOverlay using the Raster and set its properties
                IAgStkGraphicsProjectedRasterOverlay imageOverlay = sceneManager.Initializers.ProjectedRasterOverlay.Initialize(
                    raster, (IAgStkGraphicsProjection)projectionStream);
                SetGeneralOverlayProperties(imageOverlay, attributes);

                sceneManager.Scenes[0].CentralBodies.Earth.Imagery.Add(
                    (IAgStkGraphicsGlobeImageOverlay)imageOverlay);
                sceneManager.Render();

                // Record the IAgStkGraphicsGlobeImageOverlay of this sensor's projection.
                attributes.IsProjectionAdded = true;
                attributes.ProjectionOverlay = (IAgStkGraphicsGlobeImageOverlay)imageOverlay;
            }
        }

        private static void SetGeneralOverlayProperties(IAgStkGraphicsProjectedRasterOverlay overlay, SensorAttributes attributes)
        {
            if (attributes.UseTintColor == true)
                overlay.Color = attributes.TintColor;
            else
                overlay.Color = System.Drawing.Color.White;

            overlay.UseTransparentColor = attributes.UseTransparentColor;
            overlay.TransparentColor = attributes.TransparentColor;

            overlay.BorderColor = attributes.BorderColor;
            overlay.BorderTranslucency = attributes.BorderTranslucency / 100.0f;
            overlay.BorderWidth = (float)attributes.BorderWidth;

            overlay.ShowFarPlane = attributes.ShowFarPlane;
            overlay.FarPlaneColor = attributes.FarPlaneColor;
            overlay.FarPlaneTranslucency = attributes.FarPlaneTranslucency / 100.0f;

            overlay.ShowFrustum = attributes.ShowFrustum;
            overlay.FrustumColor = attributes.FrustumColor;
            overlay.FrustumTranslucency = attributes.FrustumTranslucency / 100.0f;

            overlay.ShowShadows = attributes.ShowShadow;
            overlay.ShadowColor = attributes.ShadowColor;
            overlay.ShadowTranslucency = attributes.ShadowTranslucency / 100.0f;
        }
    }
}
