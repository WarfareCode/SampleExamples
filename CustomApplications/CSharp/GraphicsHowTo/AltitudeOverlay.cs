using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;

namespace GraphicsHowTo
{
    public class AltitudeOverlay : StatusOverlay<double>
    {
        public AltitudeOverlay(IAgStkGraphicsScene scene, IAgStkGraphicsSceneManager manager)
            :
            base(false, 0, 10000000, "0", "10000", manager)
        {
            m_Scene = scene;
            m_SceneManager = manager;
            ((AgStkGraphicsScene)scene).Rendering += new IAgStkGraphicsSceneEvents_RenderingEventHandler(SceneRendering);
        }

        void SceneRendering(object Sender, IAgStkGraphicsRenderingEventArgs Args)
        {
            Update(Value, m_SceneManager);
        }

        public override double ValueTransform(double value)
        {
            return (value >= 1) ? Math.Log10(value / 10000.0) : value;
        }

        public override double Value
        {
            get { return m_Scene.Camera.Distance; }
        }

        public override string Text
        {
            get { return "Current Altitude:\n" + 
                String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0,10:0.000} km\n\n\n", Value / 1000); }
        }

        /// <summary>
        /// Add a list of intervals of doubles using a different color for each.
        /// </summary>
        /// <param name="intervals">Collection of raw (non-ValueTransformed) Intervals.</param>
        internal void AddIntervals(ICollection<Interval> intervals)
        {
            Color[] colors = new Color[] 
            { 
                System.Drawing.Color.SkyBlue, System.Drawing.Color.LightGreen,
                System.Drawing.Color.Yellow, System.Drawing.Color.LightSalmon,
                System.Drawing.Color.DarkRed, System.Drawing.Color.MediumPurple 
            };
            
            int i = 0;
            foreach (Interval interval in intervals)
            {
                Indicator.AddInterval(ValueTransform(interval.Minimum), ValueTransform(interval.Maximum), IndicatorStyle.Bar,
                    colors[i], m_SceneManager);
                i = (i + 1) % colors.Length;
            }
        }

        internal void RemoveIntervals(ICollection<Interval> intervals)
        {
            foreach (Interval interval in intervals)
            {
                Indicator.RemoveInterval(ValueTransform(interval.Minimum), ValueTransform(interval.Maximum));
            }
        }

        private IAgStkGraphicsScene m_Scene;
        private IAgStkGraphicsSceneManager m_SceneManager;

    }
}
