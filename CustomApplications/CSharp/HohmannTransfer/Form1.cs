using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKObjects.Astrogator;

namespace HohmannTransfer
{
    public partial class Form1 : Form
    {
        private IAgStkObjectRoot stkRootObject = null;
        private IAgSatellite _sat;
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
            stkRoot.NewScenario("HohmannTransfer");
            this.button1.Enabled = false;
            this.button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _sat = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Satellite1") as IAgSatellite;
            _sat.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator);
            _driver = _sat.Propagator as IAgVADriverMCS;
            _driver.MainSequence.RemoveAll();
            this.button2.Enabled = false;
            this.button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IAgVAMCSInitialState initialState = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeInitialState, "Inner Orbit", "-") as IAgVAMCSInitialState;
            initialState.SetElementType(AgEVAElementType.eVAElementTypeKeplerian);
            IAgVAElementKeplerian modKep = initialState.Element as IAgVAElementKeplerian;
            modKep.PeriapsisRadiusSize = 6700;
            modKep.ArgOfPeriapsis = 0;
            modKep.Eccentricity = 0;
            modKep.Inclination = 0;
            modKep.RAAN = 0;
            modKep.TrueAnomaly = 0;
            this.button3.Enabled = false;
            this.button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IAgVAMCSPropagate propagate = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Propagate", "-") as IAgVAMCSPropagate;
            propagate.PropagatorName = "Earth Point Mass";
            IAgVAStoppingCondition sc = propagate.StoppingConditions["Duration"].Properties as IAgVAStoppingCondition;
            sc.Trip = 7200;
            this.button4.Enabled = false;
            this.button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IAgVAMCSManeuver maneuver = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "DV1", "-") as IAgVAMCSManeuver;
            maneuver.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive);
            IAgVAManeuverImpulsive impulsive = maneuver.Maneuver as IAgVAManeuverImpulsive;
            impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector);
            IAgVAAttitudeControlImpulsiveThrustVector thrustVector = impulsive.AttitudeControl as IAgVAAttitudeControlImpulsiveThrustVector;
            thrustVector.DeltaVVector.AssignCartesian(2.421, 0, 0);
            impulsive.UpdateMass = true;
            this.button5.Enabled = false;
            this.button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IAgVAMCSPropagate propagate = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Transfer Ellipse", "-") as IAgVAMCSPropagate;
            propagate.PropagatorName = "Earth Point Mass";
            propagate.StoppingConditions.Add("Apoapsis");
            propagate.StoppingConditions.Remove("Duration");
            this.button6.Enabled = false;
            this.button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IAgVAMCSManeuver maneuver = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "DV2", "-") as IAgVAMCSManeuver;
            maneuver.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive);
            IAgVAManeuverImpulsive impulsive = maneuver.Maneuver as IAgVAManeuverImpulsive;
            impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector);
            IAgVAAttitudeControlImpulsiveThrustVector thrustVector = impulsive.AttitudeControl as IAgVAAttitudeControlImpulsiveThrustVector;
            thrustVector.DeltaVVector.AssignCartesian(1.465, 0, 0);
            impulsive.UpdateMass = true;
            this.button7.Enabled = false;
            this.button8.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            IAgVAMCSPropagate propagate = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Outer Orbit", "-") as IAgVAMCSPropagate;
            propagate.PropagatorName = "Earth Point Mass";
            IAgVAStoppingCondition sc = propagate.StoppingConditions["Duration"].Properties as IAgVAStoppingCondition;
            sc.Trip = 86400;
            this.button8.Enabled = false;
            this.button9.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _driver.RunMCS();
            this.button9.Enabled = false;
            this.button10.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form2 dataForm = new Form2();
            String reportData = "";
            IAgVAMCSPropagate outerOrbit = _driver.MainSequence["Outer Orbit"] as IAgVAMCSPropagate;
            IAgDrResult result = ((IAgVAMCSSegment)outerOrbit).ExecSummary;
            Console.WriteLine((AgEDrCategories)result.Category);
            IAgDrIntervalCollection intervals = result.Value as IAgDrIntervalCollection;
            for (int i = 0; i < intervals.Count; i++)
            {
                IAgDrInterval interval = intervals[i];
                IAgDrDataSetCollection datasets = interval.DataSets;
                for (int j = 0; j < datasets.Count; j++)
                {
                    IAgDrDataSet dataset = datasets[j];
                    dataForm.FormTitle = dataset.ElementName;
                    System.Array elements = dataset.GetValues();
                    foreach (object o in elements)
                    {
                        reportData += o + "\r\n";
                    }
                }
            }
            dataForm.ReportData = reportData;
            dataForm.ShowDialog();
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