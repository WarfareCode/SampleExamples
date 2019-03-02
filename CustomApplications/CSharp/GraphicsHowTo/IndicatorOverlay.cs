using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AGI.STKGraphics;

namespace GraphicsHowTo
{

    public enum IndicatorStyle
    {
        Bar,
        Marker
    }

    public enum IndicatorLabelType
    {
        None,
        Percent,
        ValueOfMax
    }

    public class IndicatorOverlay : AgStkGraphicsScreenOverlay
    {
        public IndicatorOverlay(Array position, Array size, Interval range,
            bool isHorizontal, IndicatorStyle indicatorStyle, IAgStkGraphicsSceneManager manager)
        {
            m_SceneManager = manager;
            m_Overlay = m_SceneManager.Initializers.ScreenOverlay.InitializeWithPosAndSize(ref position, ref size) as IAgStkGraphicsOverlay;

            m_IsHorizontal = isHorizontal;
            m_Intervals = new List<IntervalOverlay>();
            m_Range = CheckValues(range);
            m_Value = m_Range.Minimum;
            m_IndicatorStyle = indicatorStyle;
            m_MarkerSize = 1;
        }

        public IndicatorOverlay(double xPixels, double yPixels, double widthPixels, double heightPixels,
            double minValue, double maxValue, bool isHorizontal, IndicatorStyle indicatorStyle, IAgStkGraphicsSceneManager manager)
            : this(new object[] { xPixels, yPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels }, 
                new object[] { widthPixels, heightPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels },
                new Interval(minValue, maxValue), isHorizontal, indicatorStyle, manager)
        {
            //convenience method
        }

        public IAgStkGraphicsScreenOverlay RealScreenOverlay
        {
            get { return m_Overlay as IAgStkGraphicsScreenOverlay; }
        }

        public Interval Range
        {
            get { return m_Range; }
        }

        public double Value
        {
            get { return m_Value; }
            set
            {
                m_Value = (double.IsPositiveInfinity(value)) ? double.MaxValue :
                    ((double.IsNegativeInfinity(value)) ? double.MinValue : value);
                UpdateForeground();
                UpdateIntervals();
            }
        }

        public System.Drawing.Color ForegroundColor
        {
            get { return ((IAgStkGraphicsOverlay)m_Foreground).Color; }
            set { ((IAgStkGraphicsOverlay)m_Foreground).Color = value; }
        }

        public double MarkerSize
        {
            get { return m_MarkerSize; }
            set
            {
                m_MarkerSize = value;
                switch (m_IndicatorStyle)
                {
                    case IndicatorStyle.Bar:
                        break;
                    case IndicatorStyle.Marker:
                        ((IAgStkGraphicsOverlay)m_Foreground).Size = GetIndicationMarkerSize(m_MarkerSize);
                        break;
                    default:
                        break;
                }

            }
        }

        private void CreateForeground()
        {
            Array foregroundPosition = new object[] { 0, 0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels };
            Array foregroundSize = new object[] { 0, 0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels };
            m_Foreground = m_SceneManager.Initializers.ScreenOverlay.InitializeWithPosAndSize(ref foregroundPosition, ref foregroundSize);
            switch (m_IndicatorStyle)
            {
                case IndicatorStyle.Bar:
                    ((IAgStkGraphicsOverlay)m_Foreground).Color = System.Drawing.Color.Green;
                    break;
                case IndicatorStyle.Marker:
                    ((IAgStkGraphicsOverlay)m_Foreground).Color = System.Drawing.Color.LimeGreen;
                    ((IAgStkGraphicsOverlay)m_Foreground).BorderSize = 1;
                    ((IAgStkGraphicsOverlay)m_Foreground).BorderColor = System.Drawing.Color.Black;
                    ((IAgStkGraphicsOverlay)m_Foreground).BorderTranslucency = 0.5f;
                    break;
                default:
                    break;
            }
            ((IAgStkGraphicsOverlay)m_Foreground).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = this.Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
            overlayManager.Add(m_Foreground);
            UpdateForeground();
        }

        private void UpdateForeground()
        {
            if (m_Foreground == null) { CreateForeground(); }
            switch (m_IndicatorStyle)
            {
                case IndicatorStyle.Bar:
                    ((IAgStkGraphicsOverlay)m_Foreground).Size = GetIndicationBarSize(Value);
                    ((IAgStkGraphicsOverlay)m_Foreground).Position = GetIndicationPosition(0);
                    break;
                case IndicatorStyle.Marker:
                    ((IAgStkGraphicsOverlay)m_Foreground).Size = GetIndicationMarkerSize(MarkerSize);
                    ((IAgStkGraphicsOverlay)m_Foreground).Position = GetIndicationPosition(Value);
                    ((IAgStkGraphicsOverlay)m_Foreground).BringToFront();
                    break;
                default:
                    break;
            }

        }

        private void UpdateIntervals()
        {
            foreach (IntervalOverlay interval in m_Intervals)
            {
                switch (interval.IntervalStyle)
                {
                    case IndicatorStyle.Bar:
                        ((IAgStkGraphicsOverlay)interval.Marker).Translucency = 0.6f;
                        if (interval.Range.Minimum < Value && Value < interval.Range.Maximum)
                        {
                            ((IAgStkGraphicsOverlay)interval.Marker).Translucency = 0.2f;
                        }
                        break;
                    case IndicatorStyle.Marker:
                        ((IAgStkGraphicsOverlay)interval.Marker).Size = GetIndicationMarkerSize(MarkerSize);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Turns a value into a fraction of this indication's entire range. Returns [0.0-1.0]
        /// </summary>
        private double GetIndicationFraction(double value)
        {
            return Math.Min(Math.Max(Range.Minimum, value), Range.Maximum) / (Range.Maximum - Range.Minimum);
        }

        /// <summary>
        /// Calculate indication bar size based on the given value.
        /// </summary>
        private Array GetIndicationBarSize(double value)
        {
            value = Math.Max(GetIndicationFraction(value), 0.0001); //can't be zero
            if (m_IsHorizontal) { return new object[] { value, 1.0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction }; }
            else { return new object[] { 1.0, value, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction }; }
        }

        /// <summary>
        /// Calculate indication marker size based on the given value.
        /// </summary>
        private Array GetIndicationMarkerSize(double markerWidthPixels)
        {
            markerWidthPixels = Math.Max(markerWidthPixels, 0.0001); //can't be zero
            if (m_IsHorizontal) { return new object[] { markerWidthPixels, 1.0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction }; }
            else { return new object[] { 1.0, markerWidthPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels }; }
        }

        /// <summary>
        /// Get the indication position based on value and direction.
        /// </summary>
        private Array GetIndicationPosition(double value)
        {
            if (m_IsHorizontal) { return new object[] { GetIndicationFraction(value), 0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction }; }
            else { return new object[] { 0, GetIndicationFraction(value), AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction }; }
        }

        struct IntervalOverlay
        {
            public IntervalOverlay(Interval range, IndicatorStyle style, System.Drawing.Color color, IndicatorOverlay parent, IAgStkGraphicsSceneManager manager)
            {
                m_Range = CheckValues(range);
                m_IntervalStyle = style;

                switch (m_IntervalStyle)
                {
                    case IndicatorStyle.Bar:
                        double rangeSize = Math.Min(m_Range.Maximum - Math.Max(m_Range.Minimum, 0), parent.Range.Maximum - Math.Max(m_Range.Minimum, 0));
                        Array barPosition = parent.GetIndicationPosition(m_Range.Minimum);
                        Array barSize = parent.GetIndicationBarSize(rangeSize);
                        m_Marker = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(ref barPosition, ref barSize);

                        ((IAgStkGraphicsOverlay)m_Marker).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft;
                        ((IAgStkGraphicsOverlay)m_Marker).Color = color;
                        ((IAgStkGraphicsOverlay)m_Marker).Translucency = 0.6f;
                        m_MarkerTwo = null;
                        break;
                    case IndicatorStyle.Marker:
                        //make start and end markers
                        Array size = parent.GetIndicationMarkerSize(parent.MarkerSize);
                        Array markerPosition = parent.GetIndicationPosition(m_Range.Minimum);
                        m_Marker = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(ref markerPosition, ref size);

                        ((IAgStkGraphicsOverlay)m_Marker).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft;
                        ((IAgStkGraphicsOverlay)m_Marker).Color = color;

                        Array markerTwoPosition = parent.GetIndicationPosition(m_Range.Maximum);
                        m_MarkerTwo = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(ref markerTwoPosition, ref size);
                        
                        ((IAgStkGraphicsOverlay)m_MarkerTwo).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft;
                        ((IAgStkGraphicsOverlay)m_MarkerTwo).Color = color;
                        break;
                    default:
                        m_Marker = null;
                        m_MarkerTwo = null;
                        break;
                }

            }

            internal Interval Range
            {
                get { return m_Range; }
            }

            internal IAgStkGraphicsScreenOverlay Marker
            {
                get { return m_Marker; }
            }

            internal IAgStkGraphicsScreenOverlay MarkerTwo
            {
                get { return m_MarkerTwo; }
            }

            internal IndicatorStyle IntervalStyle
            {
                get { return m_IntervalStyle; }
            }

            private readonly Interval m_Range;
            private readonly IAgStkGraphicsScreenOverlay m_Marker;
            private readonly IAgStkGraphicsScreenOverlay m_MarkerTwo;
            private readonly IndicatorStyle m_IntervalStyle;
        }

        public void AddInterval(double minimum, double maximum, IndicatorStyle style, IAgStkGraphicsSceneManager manager)
        {
            AddInterval(minimum, maximum, style, System.Drawing.Color.LightBlue, manager);
        }

        public void AddInterval(double minimum, double maximum, IndicatorStyle style, System.Drawing.Color color, IAgStkGraphicsSceneManager manager)
        {
            AddInterval(new Interval(minimum, maximum), style, color, manager);
        }

        public void AddInterval(Interval range, IndicatorStyle style, System.Drawing.Color color, IAgStkGraphicsSceneManager manager)
        {
            range = CheckValues(range);
            IntervalOverlay interval;
            if (!TryGetInterval(range, out interval))
            {
                interval = new IntervalOverlay(range, style, color, this, manager);
                m_Intervals.Add(interval);
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = this.Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
                overlayManager.Add(interval.Marker);
                if (interval.MarkerTwo != null) { overlayManager.Add(interval.MarkerTwo); }
            }
        }

        public void RemoveInterval(double minimum, double maximum)
        {
            RemoveInterval(new Interval(minimum, maximum));
        }

        public void RemoveInterval(Interval range)
        {
            range = CheckValues(range);
            IntervalOverlay interval;
            if (TryGetInterval(range, out interval))
            {
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = this.Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
                overlayManager.Remove(interval.Marker);
                if (interval.MarkerTwo != null) { overlayManager.Remove(interval.MarkerTwo); }
                m_Intervals.Remove(interval);
            }
        }

        private bool TryGetInterval(Interval range, out IntervalOverlay interval)
        {
            foreach (IntervalOverlay i in m_Intervals)
            {
                if (i.Range == range)
                {
                    interval = i;
                    return true;
                }
            }
            interval = new IntervalOverlay();
            return false;
        }

        private static Interval CheckValues(Interval range)
        {
            if (double.IsInfinity(range.Minimum)) { range.Minimum = double.MinValue; }
            if (double.IsInfinity(range.Maximum)) { range.Maximum = double.MaxValue; }
            return range;
        }

        private IAgStkGraphicsScreenOverlay m_Foreground;
        private List<IntervalOverlay> m_Intervals;

        private double m_Value;
        private double m_MarkerSize;

        private readonly Interval m_Range;
        private readonly IndicatorStyle m_IndicatorStyle;
        private readonly bool m_IsHorizontal;

        private IAgStkGraphicsSceneManager m_SceneManager;


        #region _IAgStkGraphicsScreenOverlay Members

        public System.Drawing.Color BorderColor
        {
            get
            {
                return m_Overlay.BorderColor;
            }
            set
            {
                m_Overlay.BorderColor = value;
            }
        }

        public int BorderSize
        {
            get
            {
                return m_Overlay.BorderSize;
            }
            set
            {
                m_Overlay.BorderSize = value;
            }
        }

        public float BorderTranslucency
        {
            get
            {
                return m_Overlay.BorderTranslucency;
            }
            set
            {
                m_Overlay.BorderTranslucency = value;
            }
        }

        public Array Bounds
        {
            get { return m_Overlay.Bounds; }
        }

        public void BringToFront()
        {
            m_Overlay.BringToFront();
        }

        public bool ClipToParent
        {
            get
            {
                return m_Overlay.ClipToParent;
            }
            set
            {
                m_Overlay.ClipToParent = value;
            }
        }

        public System.Drawing.Color Color
        {
            get
            {
                return m_Overlay.Color;
            }
            set
            {
                m_Overlay.Color = value;
            }
        }

        public Array ControlBounds
        {
            get { return m_Overlay.ControlBounds; }
        }

        public Array ControlPosition
        {
            get { return m_Overlay.ControlPosition; }
        }

        public Array ControlSize
        {
            get { return m_Overlay.ControlSize; }
        }

        public Array ControlToOverlay(double X, double Y)
        {
            return m_Overlay.ControlToOverlay(X, Y);
        }

        public bool Display
        {
            get
            {
                return m_Overlay.Display;
            }
            set
            {
                m_Overlay.Display = value;
            }
        }

        public IAgStkGraphicsDisplayCondition DisplayCondition
        {
            get
            {
                return m_Overlay.DisplayCondition;
            }
            set
            {
                m_Overlay.DisplayCondition = value;
            }
        }

        public bool FlipX
        {
            get
            {
                return m_Overlay.FlipX;
            }
            set
            {
                m_Overlay.FlipX = value;
            }
        }

        public bool FlipY
        {
            get
            {
                return m_Overlay.FlipY;
            }
            set
            {
                m_Overlay.FlipY = value;
            }
        }

        public double Height
        {
            get
            {
                return m_Overlay.Height;
            }
            set
            {
                m_Overlay.Height = value;
            }
        }

        public AgEStkGraphicsScreenOverlayUnit HeightUnit
        {
            get
            {
                return m_Overlay.HeightUnit;
            }
            set
            {
                m_Overlay.HeightUnit = value;
            }
        }

        public Array MaximumSize
        {
            get
            {
                return m_Overlay.MaximumSize;
            }
            set
            {
                m_Overlay.MaximumSize = value;
            }
        }

        public Array MinimumSize
        {
            get
            {
                return m_Overlay.MinimumSize;
            }
            set
            {
                m_Overlay.MinimumSize = value;
            }
        }

        public AgEStkGraphicsScreenOverlayOrigin Origin
        {
            get
            {
                return m_Overlay.Origin;
            }
            set
            {
                m_Overlay.Origin = value;
            }
        }

        public Array OverlayToControl(double X, double Y)
        {
            return m_Overlay.OverlayToControl(X, Y);
        }

        public IAgStkGraphicsScreenOverlayCollection Overlays
        {
            get { return m_Overlay.Overlays; }
        }

        public Array Padding
        {
            get
            {
                return m_Overlay.Padding;
            }
            set
            {
                m_Overlay.Padding = value;
            }
        }

        public IAgStkGraphicsScreenOverlayContainer Parent
        {
            get { return m_Overlay.Parent; }
        }

        public bool PickingEnabled
        {
            get
            {
                return m_Overlay.PickingEnabled;
            }
            set
            {
                m_Overlay.PickingEnabled = value;
            }
        }

        public AgEStkGraphicsScreenOverlayPinningOrigin PinningOrigin
        {
            get
            {
                return m_Overlay.PinningOrigin;
            }
            set
            {
                m_Overlay.PinningOrigin = value;
            }
        }

        public Array PinningPosition
        {
            get
            {
                return m_Overlay.PinningPosition;
            }
            set
            {
                m_Overlay.PinningPosition = value;
            }
        }

        public Array Position
        {
            get
            {
                return m_Overlay.Position;
            }
            set
            {
                m_Overlay.Position = value;
            }
        }

        public double RotationAngle
        {
            get
            {
                return m_Overlay.RotationAngle;
            }
            set
            {
                m_Overlay.RotationAngle = value;
            }
        }

        public Array RotationPoint
        {
            get
            {
                return m_Overlay.RotationPoint;
            }
            set
            {
                m_Overlay.RotationPoint = value;
            }
        }

        public double Scale
        {
            get
            {
                return m_Overlay.Scale;
            }
            set
            {
                m_Overlay.Scale = value;
            }
        }

        public void SendToBack()
        {
            m_Overlay.SendToBack();
        }

        public Array Size
        {
            get
            {
                return m_Overlay.Size;
            }
            set
            {
                m_Overlay.Size = value;
            }
        }

        public double TranslationX
        {
            get
            {
                return m_Overlay.TranslationX;
            }
            set
            {
                m_Overlay.TranslationX = value;
            }
        }

        public double TranslationY
        {
            get
            {
                return m_Overlay.TranslationY;
            }
            set
            {
                m_Overlay.TranslationY = value;
            }
        }

        public float Translucency
        {
            get
            {
                return m_Overlay.Translucency;
            }
            set
            {
                m_Overlay.Translucency = value;
            }
        }

        public double Width
        {
            get
            {
                return m_Overlay.Width;
            }
            set
            {
                m_Overlay.Width = value;
            }
        }

        public AgEStkGraphicsScreenOverlayUnit WidthUnit
        {
            get
            {
                return m_Overlay.WidthUnit;
            }
            set
            {
                m_Overlay.WidthUnit = value;
            }
        }

        public double X
        {
            get
            {
                return m_Overlay.X;
            }
            set
            {
                m_Overlay.X = value;
            }
        }

        public AgEStkGraphicsScreenOverlayUnit XUnit
        {
            get
            {
                return m_Overlay.XUnit;
            }
            set
            {
                m_Overlay.XUnit = value;
            }
        }

        public double Y
        {
            get
            {
                return m_Overlay.Y;
            }
            set
            {
                m_Overlay.Y = value;
            }
        }

        public AgEStkGraphicsScreenOverlayUnit YUnit
        {
            get
            {
                return m_Overlay.YUnit;
            }
            set
            {
                m_Overlay.YUnit = value;
            }
        }

        public object Tag
        {
            get
            {
                return m_Overlay.Tag;
            }
            set
            {
                m_Overlay.Tag = value;
            }
        }

        private IAgStkGraphicsOverlay m_Overlay;

        #endregion
    }
}
