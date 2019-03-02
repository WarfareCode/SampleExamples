using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using AGI.STKGraphics;

namespace GraphicsHowTo
{
    public abstract class StatusOverlay<T> : AgStkGraphicsScreenOverlay
        where T : struct
    {
        protected StatusOverlay(bool isIndicatorHorizontal, T minimum, T maximum, IAgStkGraphicsSceneManager manager)
            : this(isIndicatorHorizontal, minimum, maximum, null, null, manager)
        {

        }

        protected StatusOverlay(bool isIndicatorHorizontal, T minimum, T maximum, string minimumLabel, string maximumLabel, 
            IAgStkGraphicsSceneManager manager)
        {
            m_SceneManager = manager;
            m_Overlay = m_SceneManager.Initializers.ScreenOverlay.Initialize(5, 5, 40, 40) as IAgStkGraphicsOverlay;

            m_IsIndicatorHorizontal = isIndicatorHorizontal;
            m_Minimum = minimum;
            m_Maximum = maximum;
            m_MinText = minimumLabel;
            m_MaxText = maximumLabel;
        }

        public void SetDefaultStyle()
        {
            this.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft;
            this.Color = System.Drawing.Color.Black;
            this.Translucency = 0.5f;
            this.BorderColor = System.Drawing.Color.White;
            this.BorderSize = 2;
            this.BorderTranslucency = 0f;
        }

        public IAgStkGraphicsScreenOverlay RealScreenOverlay
        {
            get { return m_Overlay as IAgStkGraphicsScreenOverlay; }
        }

        public abstract double ValueTransform(T value);

        public abstract T Value
        {
            get;
        }

        public abstract string Text
        {
            get;
        }

        public void Update(T newValue, IAgStkGraphicsSceneManager manager)
        {
            if (ValueTransform(LastValue) - ValueTransform(newValue) != 0)
            {
                Indicator.Value = ValueTransform(Value);
                TextOverlayHelper.UpdateTextOverlay(TextOverlay, Text, m_Font, manager);
                LastValue = newValue;
                if (m_MinTextOverlay != null) { ((IAgStkGraphicsOverlay)m_MinTextOverlay).BringToFront(); }
                if (m_MaxTextOverlay != null) { ((IAgStkGraphicsOverlay)m_MaxTextOverlay).BringToFront(); }
            }
        }

        protected IndicatorOverlay Indicator
        {
            get
            {
                if (m_Indicator == null)
                {
                    if (m_IsIndicatorHorizontal)
                    {
                        m_Indicator = new IndicatorOverlay(0, 0, ((IAgStkGraphicsOverlay)TextOverlay).Width, 20, ValueTransform(m_Minimum),
                            ValueTransform(m_Maximum), m_IsIndicatorHorizontal, IndicatorStyle.Marker, m_SceneManager);
                        m_Indicator.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomCenter;
                        this.Size = new object[] 
                            {
                                ((IAgStkGraphicsOverlay)TextOverlay).Width,
                                ((IAgStkGraphicsOverlay)TextOverlay).Height + m_Indicator.Height,
                                this.Size.GetValue(2), this.Size.GetValue(3)
                            };
                    }
                    else
                    {
                        m_Indicator = new IndicatorOverlay(0, 0, 30, ((IAgStkGraphicsOverlay)TextOverlay).Height, ValueTransform(m_Minimum),
                            ValueTransform(m_Maximum), m_IsIndicatorHorizontal, IndicatorStyle.Marker, m_SceneManager);
                        m_Indicator.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterLeft;
                        this.Size = new object[] 
                            {
                                ((IAgStkGraphicsOverlay)TextOverlay).Width + m_Indicator.Width,
                                ((IAgStkGraphicsOverlay)TextOverlay).Height,
                                this.Size.GetValue(2), this.Size.GetValue(3)
                            };
                    }

                    m_Indicator.Value = ValueTransform(Value);
                    m_Indicator.BorderSize = 1;
                    m_Indicator.BorderColor = System.Drawing.Color.White;
                    m_Indicator.Color = System.Drawing.Color.Black;
                    m_Indicator.Translucency = 0.5f;
                    IAgStkGraphicsScreenOverlayCollectionBase overlayManager = this.Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
                    overlayManager.Add(m_Indicator.RealScreenOverlay);

                    if (!String.IsNullOrEmpty(m_MinText) && !String.IsNullOrEmpty(m_MaxText))
                    {
                        m_MinTextOverlay = TextOverlayHelper.CreateTextOverlay(m_MinText, m_LabelFont, m_SceneManager);
                        m_MaxTextOverlay = TextOverlayHelper.CreateTextOverlay(m_MaxText, m_LabelFont, m_SceneManager);

                        ((IAgStkGraphicsOverlay)m_MinTextOverlay).Origin = m_IsIndicatorHorizontal ? AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterLeft : AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomCenter;
                        ((IAgStkGraphicsOverlay)m_MaxTextOverlay).Origin = m_IsIndicatorHorizontal ? AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight : AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter;

                        IAgStkGraphicsScreenOverlayCollectionBase indicatorOverlayManager = m_Indicator.Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
                        indicatorOverlayManager.Add((IAgStkGraphicsScreenOverlay)m_MinTextOverlay);
                        indicatorOverlayManager.Add((IAgStkGraphicsScreenOverlay)m_MaxTextOverlay);
                    }

                }
                return m_Indicator;
            }
            set { m_Indicator = value; }
        }

        protected IAgStkGraphicsTextureScreenOverlay TextOverlay
        {
            get
            {
                if (m_TextOverlay == null)
                {
                    m_TextOverlay = TextOverlayHelper.CreateTextOverlay(Text, m_Font, m_SceneManager);
                    ((IAgStkGraphicsOverlay)m_TextOverlay).BorderSize = 1;
                    ((IAgStkGraphicsOverlay)m_TextOverlay).BorderColor = System.Drawing.Color.White;

                    if (m_IsIndicatorHorizontal) { ((IAgStkGraphicsOverlay)m_TextOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter; }
                    else { ((IAgStkGraphicsOverlay)m_TextOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight; }

                    IAgStkGraphicsScreenOverlayCollectionBase overlayManager = this.Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
                    overlayManager.Add((IAgStkGraphicsScreenOverlay)m_TextOverlay);
                }
                return m_TextOverlay;
            }
            set { m_TextOverlay = value; }
        }

        protected T LastValue
        {
            get { return m_LastValue; }
            set { m_LastValue = value; }
        }

        protected Font Font
        {
            get { return m_Font; }
            set { m_Font = value; }
        }

        private bool m_IsIndicatorHorizontal = false;
        private Font m_Font = new Font("Arial", 10, FontStyle.Bold);
        private Font m_LabelFont = new Font("Arial Narrow", 8, FontStyle.Bold);
        private string m_MinText;
        private string m_MaxText;

        private IAgStkGraphicsTextureScreenOverlay m_MinTextOverlay = null;
        private IAgStkGraphicsTextureScreenOverlay m_MaxTextOverlay = null;
        private IAgStkGraphicsTextureScreenOverlay m_TextOverlay = null;
        private IndicatorOverlay m_Indicator = null;

        private T m_LastValue = new T();
        private T m_Minimum = new T();
        private T m_Maximum = new T();

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
