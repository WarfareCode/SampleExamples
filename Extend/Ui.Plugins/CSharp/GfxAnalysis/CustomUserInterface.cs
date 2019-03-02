using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

namespace Agi.Ui.Plugins.CSharp.GfxAnalysis
{
    public partial class CustomUserInterface : UserControl, AGI.Ui.Plugins.IAgUiPluginEmbeddedControl
    {
        private AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        private IAgScenario scenario;
        private IAgSatellite satellite;
        private IAgVePropagatorTwoBody twoBody;
        private IAgSensor sensor;
        private GfxAnalysisPlugin m_uiPlugin;
        private bool bSolarPanelFirstTime = true;
        private bool bAreaPanelFirstTime = true;

        public CustomUserInterface()
        {
            InitializeComponent();
            gfxTypeCombo.SelectedIndex = 0;
        }

        #region IAgUiPluginEmbeddedControl Members

        public stdole.IPictureDisp GetIcon()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnClosing()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnSaveModified()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SetSite(AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as GfxAnalysisPlugin;
            m_root = m_uiPlugin.STKRoot;
            m_uiPlugin.InterfaceWasOpen = false;
            //Initialize the interface
            if (m_root.CurrentScenario == null)
            {
                newSatelliteBtn.Enabled = false;
                newSensorBtn.Enabled = false;
                computeBtn.Enabled = false;
                closeScenarioBtn.Enabled = true;
            }
            else if (m_root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, "Satellite1"))
            {
                if (m_root.CurrentScenario.Children["Satellite1"].Children.Contains(AgESTKObjectType.eSensor, "Sensor1"))
                {
                    newScenarioBtn.Enabled = false;
                    newSatelliteBtn.Enabled = false;
                    newSensorBtn.Enabled = false;
                    computeBtn.Enabled = true;
                    closeScenarioBtn.Enabled = true;
                }
                else
                {
                    newScenarioBtn.Enabled = false;
                    newSatelliteBtn.Enabled = false;
                    newSensorBtn.Enabled = true;
                    computeBtn.Enabled = false;
                    closeScenarioBtn.Enabled = true;
                }
            }
            else
            {
                newScenarioBtn.Enabled = false;
                newSatelliteBtn.Enabled = true;
                newSensorBtn.Enabled = false;
                computeBtn.Enabled = false;
                closeScenarioBtn.Enabled = true;
            }
        }

        #endregion

        private void newScenarioBtn_Click(object sender, EventArgs e)
        {
            m_uiPlugin.InterfaceWasOpen = true;
            // Close current scenario 
            if (m_root.CurrentScenario != null)
            {
                m_root.CloseScenario();
            }
            m_root.NewScenario("Scenario1");
            m_root.UnitPreferences.SetCurrentUnit("DistanceUnit", "km");
            m_root.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG");

            // Get IAgScenario interface 
            scenario = m_root.CurrentScenario as IAgScenario;

            // Set scenario start and stop times 
            scenario.SetTimePeriod("1 Jul 2007 12:00:00.00", "2 Jul 2007 12:00:00.00");
            scenario.Epoch = "1 Jul 2007 12:00:00.000";
            scenario.Animation.StartTime = "1 Jul 2007 12:00:00.000";
            m_root.Rewind();
            newScenarioBtn.Enabled = false;
            newSatelliteBtn.Enabled = true;
            closeScenarioBtn.Enabled = true;
        }

        private void newSatelliteBtn_Click(object sender, EventArgs e)
        {
            if (scenario == null)
                scenario = m_root.CurrentScenario as IAgScenario;

            //Create a new Satellite
            satellite = m_root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Satellite1") as IAgSatellite;
            satellite.VO.Model.ScaleValue = 0.0;
            satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);

            twoBody = satellite.Propagator as IAgVePropagatorTwoBody;
            IAgCrdnEventIntervalSmartInterval interval = twoBody.EphemerisInterval;
            interval.SetExplicitInterval(scenario.StartTime, scenario.StopTime);
            twoBody.Step = 60;

            twoBody.InitialState.Representation.AssignClassical(AGI.STKUtil.AgECoordinateSystem.eCoordinateSystemJ2000, 6878.14, 0.0, 45.0, 0.0, 0.0, 0.0);
            twoBody.Propagate();

            newSatelliteBtn.Enabled = false;
            newSensorBtn.Enabled = true;
        }

        private void newSensorBtn_Click(object sender, EventArgs e)
        {
            //Add a sensor to the satellite
            IAgStkObject obj = m_root.CurrentScenario.Children["Satellite1"];
            sensor = obj.Children.New(AgESTKObjectType.eSensor, "Sensor1") as IAgSensor;
            sensor.VO.VertexOffset.InheritFromParentObj = false;
            sensor.VO.VertexOffset.EnableTranslational = true;
            sensor.VO.VertexOffset.SetAxisOffsetValue(AgEAxisOffset.eSensorRadius, 0);
            sensor.VO.VertexOffset.X = 0;
            sensor.VO.VertexOffset.Y = 0;
            sensor.VO.VertexOffset.Z = -0.02;
            m_root.Rewind();

            newSensorBtn.Enabled = false;
            computeBtn.Enabled = true;
        }

        private void computeBtn_Click(object sender, EventArgs e)
        {
            computeBtn.Enabled = false;
            closeScenarioBtn.Enabled = false;
            gfxTypeCombo.Enabled = false;

            if (scenario == null)
                scenario = m_root.CurrentScenario as IAgScenario;

            switch (gfxTypeCombo.SelectedIndex)
            {
                case 0:
                    computeSolarPanelTool();
                    break;
                case 1:
                    computeAreaTool();
                    break;
                case 2:
                    computeObscurationTool();
                    break;
                case 3:
                    computeAzElMaskTool();
                    break;
            }

            computeBtn.Enabled = true;
            closeScenarioBtn.Enabled = true;
            gfxTypeCombo.Enabled = true;
        }

        private void closeScenarioBtn_Click(object sender, EventArgs e)
        {
            //Close the scenario
            if (!bSolarPanelFirstTime)
            {
                bSolarPanelFirstTime = true;
                m_root.ExecuteCommand("VO */Satellite/Satellite1 SolarPanel DeleteData");
            }
            if (!bAreaPanelFirstTime)
            {
                bAreaPanelFirstTime = true;
                m_root.ExecuteCommand("VO */Satellite/Satellite1 Area DeleteData");
            }
            m_root.CloseScenario();
            newScenarioBtn.Enabled = true;
            newSatelliteBtn.Enabled = false;
            newSensorBtn.Enabled = false;
            computeBtn.Enabled = false;
            closeScenarioBtn.Enabled = false;
            gfxTypeCombo.Enabled = true;
        }

        private void computeSolarPanelTool()
        {
            if (bSolarPanelFirstTime)
            {
                m_root.ExecuteCommand("VO */Satellite/Satellite1 SolarPanel Visualization AddGroup default");
                bSolarPanelFirstTime = false;
            }
            else
                m_root.ExecuteCommand("VO */Satellite/Satellite1 SolarPanel DeleteData");

            m_root.ExecuteCommand("VO */Satellite/Satellite1 SolarPanel Visualization Radius On 25.00");
            m_root.ExecuteCommand("VO */Satellite/Satellite1 SolarPanel Compute \"" + scenario.StartTime.ToString() + "\" \"" + scenario.StopTime.ToString() + "\" 120 Power \"" + Path.Combine(Path.GetTempPath(), "SolarPowerReport.txt") + "\"");
            m_root.ExecuteCommand("ReportCreate */Satellite/Satellite1 Type Display Style \"Solar Panel Power\"");
        }

        private void computeAreaTool()
        {
            if (bAreaPanelFirstTime)
                bAreaPanelFirstTime = false;
            else
                m_root.ExecuteCommand("VO */Satellite/Satellite1 Area DeleteData");

            m_root.ExecuteCommand("VO */Satellite/Satellite1 Area Times \"" + scenario.StartTime.ToString() + "\" \"" + scenario.StopTime.ToString() + "\" 300.0");
            m_root.ExecuteCommand("VO */Satellite/Satellite1 Area BoundRadius On 12.0");
            m_root.ExecuteCommand("VO */Satellite/Satellite1 Area Compute");
            m_root.ExecuteCommand("ReportCreate */Satellite/Satellite1 Type Display Style \"Model Area\"");
        }

        private void computeObscurationTool()
        {
            m_root.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 Obscuration Object On Satellite/Satellite1");
            m_root.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 Obscuration Compute \"" + scenario.StartTime.ToString() + "\" \"" + scenario.StopTime.ToString() + "\" 180 \"" + Path.Combine(Path.GetTempPath(), "ObscurationReport.txt") + "\"");
            m_root.ExecuteCommand("ReportCreate */Satellite/Satellite1/Sensor/Sensor1 Type Display Style \"Obscuration\"");
        }

        private void computeAzElMaskTool()
        {
            m_root.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 AzElmaskTool Time \"" + scenario.StartTime.ToString() + "\"");
            m_root.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 AzElmaskTool Object On Satellite/Satellite1");
            m_root.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 AzElmaskTool File \"" + Path.Combine(Path.GetTempPath(), "AzElmaskReport.txt") + "\"");
            m_root.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 AzElmaskTool Compute");
        }
    }
}
