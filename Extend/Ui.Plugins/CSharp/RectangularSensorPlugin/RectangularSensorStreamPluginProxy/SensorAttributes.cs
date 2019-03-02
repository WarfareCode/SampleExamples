using System;
using System.Drawing;

namespace RectangularSensorStreamPluginProxy
{
    /// <summary>
    /// Stores settings that are related to the ProjectionStream of a rectangular sensor
    /// </summary>
    public class SensorAttributes
    {
        public static readonly string BodyFlippedAxesName = "BodyFlipped";

        public SensorAttributes(string sensorPath)
        {
            SensorBodyAxes = BodyFlippedAxesName;

            Path = sensorPath;
            IsConfigured = false;

            // Configure default values
            Uri = String.Empty;
            UriIsCompatibleVideo = false;

            UseTintColor = false;
            TintColor = Color.White;
            UseTransparentColor = false;
            TransparentColor = Color.Black;

            VideoStartTime = "00:00:00";
            VideoEndTime = "00:00:00";

            UseRealTime = false;
            UseManualFrameRate = false;
            FrameRate = 30.0;
            Loop = true;

            UseTimeInterval = true;
            TimeIntervalStart = String.Empty;
            TimeIntervalEnd = String.Empty;

            BorderColor = Color.Black;
            BorderTranslucency = 0.0f;
            BorderWidth = 0;

            ShowFarPlane = true;
            FarPlaneColor = Color.White;
            FarPlaneTranslucency = 0.0f;

            ShowFrustum = true;
            FrustumColor = Color.Lime;
            FrustumTranslucency = 0.0f;

            ShowShadow = true;
            ShadowColor = Color.Transparent;
            ShadowTranslucency = 1.0f;

            HorizontalHalfAngle = 0.0;
            VerticalHalfAngle = 0.0;

            IsProjectionAdded = false;
            ProjectionOverlay = null;
        }

        public string Path { get; set; }

        public bool IsConfigured { get; set; }

        // Projection
        public string Uri { get; set; }
        public bool UriIsCompatibleVideo { get; set; }

        public bool UseTintColor { get; set; }
        public Color TintColor { get; set; }
        public bool UseTransparentColor { get; set; }
        public Color TransparentColor { get; set; }

        // Video Playback Options
        public string VideoStartTime { get; set; }
        public string VideoEndTime { get; set; }

        public bool UseRealTime { get; set; }
        public bool UseManualFrameRate { get; set; }
        public double FrameRate { get; set; }
        public bool Loop { get; set; }

        public bool UseTimeInterval { get; set; }
        public string TimeIntervalStart { get; set; }
        public string TimeIntervalEnd { get; set; }

        // Border
        public Color BorderColor { get; set; }
        public float BorderTranslucency { get; set; }
        public int BorderWidth { get; set; }

        // Far Plane
        public bool ShowFarPlane { get; set; }
        public Color FarPlaneColor { get; set; }
        public float FarPlaneTranslucency { get; set; }

        // Frustrum
        public bool ShowFrustum { get; set; }
        public Color FrustumColor { get; set; }
        public float FrustumTranslucency { get; set; }

        // Shadow
        public bool ShowShadow { get; set; }
        public Color ShadowColor { get; set; }
        public float ShadowTranslucency { get; set; }

        // Field of View
        public double HorizontalHalfAngle { get; set; }
        public double VerticalHalfAngle { get; set; }

        // Backed up sensor settings
        public bool GraphicsInheritFromScenario { get; set; }
        public bool GraphicsEnable { get; set; }
        public double VOPercentTranslucency { get; set; }

        // Sensor's projection tracking variables
        public bool IsProjectionAdded { get; set; }
        public AGI.STKGraphics.IAgStkGraphicsGlobeImageOverlay ProjectionOverlay { get; set; }


        public string SensorBodyAxes { get; set; }

        /// <summary>
        /// Creates a clone of the SensorAttributes, but with a different Path
        /// </summary>
        public SensorAttributes Copy(string newPath)
        {
            SensorAttributes copy = (SensorAttributes)this.MemberwiseClone();
            copy.Path = new string(newPath.ToCharArray());
            return copy;
        }

    }
}
