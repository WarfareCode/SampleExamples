using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AGI.STKObjects.Astrogator;
using AGI.STKObjects;
using AGI.STKUtil;

namespace MarsProbe
{
    public partial class Form1 : Form
    {
        private IAgStkObjectRoot stkRootObject = null;
        private IAgVADriverMCS _driver;

        private AGI.STKObjects.IAgStkObjectRoot stkRoot
        {
            get
            {
                if (stkRootObject == null)
                {
                    stkRootObject = new AGI.STKObjects.AgStkObjectRootClass();
                }
                return stkRootObject;
            }
        }

        public Form1()
        {
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            stkRoot.NewScenario("MarsProbe");
            IAgScenario scene = stkRoot.CurrentScenario as IAgScenario;
            scene.StartTime = "1 Mar 1997 00:00:00.000";
            scene.StopTime = "1 Mar 1998 00:00:00.000";
            scene.Epoch = "1 Mar 1997 00:00:00.000";
            scene.Animation.AnimStepValue = 3600;
            button2.Enabled = true;
            scene.Graphics.PlanetOrbitsVisible = true;
            scene.Graphics.SubPlanetPointsVisible = false;
            scene.Graphics.SubPlanetLabelsVisible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            IAgPlanet earth = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.ePlanet, "Planet1") as IAgPlanet;
            earth.PositionSource = AgEPlPositionSourceType.ePosCentralBody;
            IAgPlPosCentralBody cb = earth.PositionSourceData as IAgPlPosCentralBody;
            cb.CentralBody = "Earth";
            cb.EphemSource = AgEEphemSourceType.eEphemJPLDE;

            IAgPlanet mars = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.ePlanet, "Planet2") as IAgPlanet;
            mars.PositionSource = AgEPlPositionSourceType.ePosCentralBody;
            cb = mars.PositionSourceData as IAgPlPosCentralBody;
            cb.CentralBody = "Mars";
            cb.EphemSource = AgEEphemSourceType.eEphemJPLDE;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            IAgSatellite sat1 = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Satellite1") as IAgSatellite;
            sat1.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator);
            _driver = sat1.Propagator as IAgVADriverMCS;
            sat1.Graphics.PassData.Orbit.SetLeadDataType(AgELeadTrailData.eDataAll);
            stkRoot.ExecuteCommand("VO * CentralBody Sun 1");
            stkRoot.ExecuteCommand("VO * Celestial Moon Label Off WindowID 1");
            stkRoot.ExecuteCommand("VO * Grids Space ShowEcliptic On WindowID 1");
            stkRoot.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 2e15 WindowID 1");
            stkRoot.ExecuteCommand("VO * View Top WindowID 1");
            stkRoot.ExecuteCommand("VO * View North WindowID 1");

            stkRoot.ExecuteCommand("VO * CentralBody Mars 2");
            stkRoot.ExecuteCommand("VO * Celestial Moon Label Off WindowID 2");
            stkRoot.ExecuteCommand("VO * Grids Space ShowEcliptic On WindowID 2");
            stkRoot.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 2e15 WindowID 2");
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            _driver.MainSequence.RemoveAll();
            IAgVAMCSInitialState initState = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeInitialState, "InitialState1", "-") as IAgVAMCSInitialState;
            initState.CoordSystemName = "CentralBody/Sun J2000";
            initState.SetElementType(AgEVAElementType.eVAElementTypeKeplerian);
            initState.OrbitEpoch = "1 Mar 1997 00:00:00.000";
            IAgVAElementKeplerian kep = initState.Element as IAgVAElementKeplerian;
            kep.SemiMajorAxis = 193216365.381;
            kep.Eccentricity = 0.236386;
            kep.Inclination = 23.455;
            kep.RAAN = 0.258;
            kep.ArgOfPeriapsis = 71.347;
            kep.TrueAnomaly = 85.152;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            IAgVAMCSPropagate propagate = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Propagate", "-") as IAgVAMCSPropagate;
            propagate.PropagatorName = "Heliocentric";
            ((IAgVAMCSSegment)propagate).Properties.Color = System.Drawing.Color.Orange;
            propagate.EnableMaxPropagationTime = false;
            ((IAgVAStoppingCondition)propagate.StoppingConditions.Add("Periapsis").Properties).CentralBodyName = "Earth";
            propagate.StoppingConditions.Remove("Duration");
            _driver.RunMCS();
            button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            Form2 dataDisplay = new Form2();
            String data = "";
            IAgVAMCSPropagate propagate = _driver.MainSequence["Propagate"] as IAgVAMCSPropagate;
            ((IAgVAStoppingCondition)propagate.StoppingConditions["Periapsis"].Properties).CentralBodyName = "Mars";
            _driver.RunMCS();
            ((IAgVAMCSSegment)propagate).Properties.DisplayCoordinateSystem = "CentralBody/Mars Inertial";
            IAgDrResult result = ((IAgVAMCSSegment)propagate).ExecSummary;
            Console.WriteLine((AgEDrCategories)result.Category);
            IAgDrIntervalCollection intervals = result.Value as IAgDrIntervalCollection;
            for (int i = 0; i < intervals.Count; i++)
            {
                IAgDrInterval interval = intervals[i];
                IAgDrDataSetCollection datasets = interval.DataSets;
                for (int j = 0; j < datasets.Count; j++)
                {
                    IAgDrDataSet dataset = datasets[j];
                    dataDisplay.FormTitle = dataset.ElementName;

                    System.Array elements = dataset.GetValues();
                    foreach (object o in elements)
                    {
                        data += o + "\r\n";
                    }
                }
            }
            dataDisplay.ReportData = data;
            dataDisplay.ShowDialog();
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            IAgScenario scene = stkRoot.CurrentScenario as IAgScenario;
            IAgComponentInfoCollection components = scene.ComponentDirectory.GetComponents(AgEComponent.eComponentAstrogator).GetFolder("Propagators");
            IAgComponentInfo epmComponent = ((IAgCloneable)components["Earth Point Mass"]).CloneObject() as IAgComponentInfo;
            epmComponent.Name = "Mars Point Mass";
            IAgVANumericalPropagatorWrapper epm = epmComponent as IAgVANumericalPropagatorWrapper;
            epm.CentralBodyName = "Mars";
            button8.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Enabled = false;
            IAgVAMCSTargetSequence ts = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "TargetSequence", "-") as IAgVAMCSTargetSequence;
            IAgVAMCSManeuver man1 = ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "Maneuver1", "-") as IAgVAMCSManeuver;
            man1.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive);
            IAgVAManeuverImpulsive impulsive = man1.Maneuver as IAgVAManeuverImpulsive;
            impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector);
            IAgVAAttitudeControlImpulsiveThrustVector thrustVector = impulsive.AttitudeControl as IAgVAAttitudeControlImpulsiveThrustVector;

            man1.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX);
            man1.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianZ);
            IAgVAStateCalcEccentricity eccentricity = ((IAgVAMCSSegment)man1).Results.Add("Keplerian Elems/Eccentricity") as IAgVAStateCalcEccentricity;
            eccentricity.CentralBodyName = "Mars";
            IAgVAProfileDifferentialCorrector dc = ts.Profiles["Differential Corrector"] as IAgVAProfileDifferentialCorrector;
            for (int i = 0; i < dc.ControlParameters.Count; i++)
            {
                dc.ControlParameters[i].Enable = true;
            }
            dc.Results[0].Enable = true;
            dc.Results[0].Tolerance = 0.01;
            dc.MaxIterations = 50;
            dc.Mode = AgEVAProfileMode.eVAProfileModeIterate;
            ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles;
            button9.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Enabled = false;
            IAgVAMCSPropagate prop2 = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Propagate2", "-") as IAgVAMCSPropagate;
            IAgVAStoppingCondition periapsis = prop2.StoppingConditions.Add("Periapsis").Properties as IAgVAStoppingCondition;
            prop2.StoppingConditions.Remove("Duration");
            periapsis.CentralBodyName = "Mars";
            periapsis.RepeatCount = 2;
            prop2.PropagatorName = "Mars Point Mass";
            _driver.RunMCS();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
        }


    }
}