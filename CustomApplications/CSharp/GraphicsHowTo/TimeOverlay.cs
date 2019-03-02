using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using AGI.STKObjects;
using AGI.STKUtil;

namespace GraphicsHowTo
{
    public class TimeOverlay : StatusOverlay<double>
    {
        public TimeOverlay(AgStkObjectRoot root)
            :
            base(true, double.Parse(((IAgScenario)root.CurrentScenario).StartTime.ToString()), double.Parse(((IAgScenario)root.CurrentScenario).StopTime.ToString()),
                String.Format(CultureInfo.InvariantCulture, "{0:MM/dd}", root.ConversionUtility.NewDate("epSec", ((IAgScenario)root.CurrentScenario).StartTime.ToString()).OLEDate),
                String.Format(CultureInfo.InvariantCulture, "{0:MM/dd}", root.ConversionUtility.NewDate("epSec", ((IAgScenario)root.CurrentScenario).StopTime.ToString()).OLEDate),
                ((IAgScenario)root.CurrentScenario).SceneManager)
        {
            m_Root = root;
            m_CurrentTime = root.ConversionUtility.NewDate("epSec", ((IAgScenario)root.CurrentScenario).StartTime.ToString());

            root.OnAnimUpdate += new IAgStkObjectRootEvents_OnAnimUpdateEventHandler(StkTimeChanged);
        }

        void StkTimeChanged(double TimeEpSec)
        {
            m_CurrentTime = m_Root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString());
            Update(Value, ((IAgScenario)m_Root.CurrentScenario).SceneManager);
        }

        public override double ValueTransform(double value)
        {
            return value - double.Parse(((IAgScenario)m_Root.CurrentScenario).StartTime.ToString());
        }

        public override double Value
        {
            get { return double.Parse(m_CurrentTime.Format("epSec")); }
        }

        public override string Text
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Current Time:\n{0:MM/dd/yyyy}\n{0:hh:mm:ss tt}",
                    m_CurrentTime.OLEDate);
            }
        }

        internal void AddInterval(double start, double end)
        {
            Indicator.AddInterval(ValueTransform(start), ValueTransform(end), IndicatorStyle.Bar, ((IAgScenario)m_Root.CurrentScenario).SceneManager);
        }

        internal void RemoveInterval(double start, double end)
        {
            Indicator.RemoveInterval(ValueTransform(start), ValueTransform(end));
        }

        private AgStkObjectRoot m_Root;
        private IAgDate m_CurrentTime;
    }
}
