using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using AGI.STKGraphics;

namespace GraphicsHowTo
{
    public class DistanceOverlay : StatusOverlay<double>
    {
        public DistanceOverlay(IAgStkGraphicsScene scene, IAgStkGraphicsSceneManager manager)
            :
            base(true, 0, 10000000, "0", "10000", manager)
        {
            m_Scene = scene;
            m_SceneManager = manager;
            ((AgStkGraphicsScene)scene).Rendering += new IAgStkGraphicsSceneEvents_RenderingEventHandler(Scene_Rendering);
        }

        void Scene_Rendering(object Sender, IAgStkGraphicsRenderingEventArgs Args)
        {
            Update(Value, m_SceneManager);
        }

        public override double ValueTransform(double value)
        {
            return (value >= 1) ? Math.Log10(value / 10000.0) : value;
        }

        public override double Value
        {
            get { return VectorMagnitude((double)m_Scene.Camera.Position.GetValue(0), (double)m_Scene.Camera.Position.GetValue(1), (double)m_Scene.Camera.Position.GetValue(2)); }
        }

        public override string Text
        {
            get { return "Current Distance:\n" + 
                String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0,10:0.000} km", Value / 1000); }
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
                Indicator.AddInterval(ValueTransform(interval.Minimum), ValueTransform(interval.Maximum), 
                    IndicatorStyle.Bar, colors[i], m_SceneManager);
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

        private static double VectorMagnitude(double x, double y, double z)
        {
            return Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        private IAgStkGraphicsScene m_Scene;
        private IAgStkGraphicsSceneManager m_SceneManager;
    }
}
