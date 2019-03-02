using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKObjects.Astrogator;
using AGI.STKUtil;

namespace HohmannTransferUsingTargeter
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
            stkRoot.NewScenario("HohmannTransfer");
            IAgSatellite sat1 = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Satellite1") as IAgSatellite;
            sat1.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator);
            _driver = sat1.Propagator as IAgVADriverMCS;
            _driver.MainSequence.RemoveAll();
            _driver.Options.DrawTrajectoryIn3D = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            IAgVAMCSInitialState initState = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeInitialState, "Inner Orbit", "-") as IAgVAMCSInitialState;
            initState.SetElementType(AgEVAElementType.eVAElementTypeKeplerian);
            IAgVAElementKeplerian modKep = initState.Element as IAgVAElementKeplerian;
            modKep.PeriapsisRadiusSize = 6700;
            modKep.ArgOfPeriapsis = 0;
            modKep.Eccentricity = 0;
            modKep.Inclination = 0;
            modKep.RAAN = 0;
            modKep.TrueAnomaly = 0;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            IAgVAMCSPropagate propagate = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Propagate", "-") as IAgVAMCSPropagate;
            propagate.PropagatorName = "Earth Point Mass";
            ((IAgVAMCSSegment)propagate).Properties.Color = Color.Blue;
            ((IAgVAStoppingCondition)propagate.StoppingConditions["Duration"].Properties).Trip = 7200;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            IAgVAMCSTargetSequence ts = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "Start Transfer", "-") as IAgVAMCSTargetSequence;
            IAgVAMCSManeuver dv1 = ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "DV1", "-") as IAgVAMCSManeuver;
            dv1.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive);
            IAgVAManeuverImpulsive impulsive = dv1.Maneuver as IAgVAManeuverImpulsive;
            impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector);
            IAgVAAttitudeControlImpulsiveThrustVector thrustVector = impulsive.AttitudeControl as IAgVAAttitudeControlImpulsiveThrustVector;
            thrustVector.ThrustAxesName = "Satellite/Satellite1 VNC(Earth)";
            dv1.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX);
            ((IAgVAMCSSegment)dv1).Results.Add("Keplerian Elems/Radius of Apoapsis");
            IAgVAProfileDifferentialCorrector dc = ts.Profiles["Differential Corrector"] as IAgVAProfileDifferentialCorrector;
            IAgVADCControl xControlParam = dc.ControlParameters.GetControlByPaths("DV1", "ImpulsiveMnvr.Cartesian.X");
            xControlParam.Enable = true;
            xControlParam.MaxStep = 0.3;
            IAgVADCResult roaResult = dc.Results.GetResultByPaths("DV1", "Radius Of Apoapsis");
            roaResult.Enable = true;
            roaResult.DesiredValue = 42238;
            roaResult.Tolerance = 0.1;
            dc.MaxIterations = 50;
            dc.EnableDisplayStatus = true;
            dc.Mode = AgEVAProfileMode.eVAProfileModeIterate;
            ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            IAgVAMCSPropagate transferEllipse = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Transfer Ellipse", "-") as IAgVAMCSPropagate;
            ((IAgVAMCSSegment)transferEllipse).Properties.Color = Color.Red;
            transferEllipse.PropagatorName = "Earth Point Mass";
            transferEllipse.StoppingConditions.Add("Apoapsis");
            transferEllipse.StoppingConditions.Remove("Duration");
            button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            IAgVAMCSTargetSequence ts = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "Finish Transfer", "-") as IAgVAMCSTargetSequence;
            IAgVAMCSManeuver dv2 = ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "DV2", "-") as IAgVAMCSManeuver;
            dv2.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive);
            IAgVAManeuverImpulsive impulsive = dv2.Maneuver as IAgVAManeuverImpulsive;
            impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector);
            IAgVAAttitudeControlImpulsiveThrustVector thrustVector = impulsive.AttitudeControl as IAgVAAttitudeControlImpulsiveThrustVector;
            thrustVector.ThrustAxesName = "Satellite/Satellite1 VNC(Earth)";
            dv2.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX);
            ((IAgVAMCSSegment)dv2).Results.Add("Keplerian Elems/Eccentricity");
            IAgVAProfileDifferentialCorrector dc = ts.Profiles["Differential Corrector"] as IAgVAProfileDifferentialCorrector;
            IAgVADCControl xControlParam = dc.ControlParameters.GetControlByPaths("DV2", "ImpulsiveMnvr.Cartesian.X");
            xControlParam.Enable = true;
            xControlParam.MaxStep = 0.3;
            IAgVADCResult eccResult = dc.Results.GetResultByPaths("DV2", "Eccentricity");
            eccResult.Enable = true;
            eccResult.DesiredValue = 0;
            dc.EnableDisplayStatus = true;
            dc.Mode = AgEVAProfileMode.eVAProfileModeIterate;
            ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles;
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            IAgVAMCSPropagate outerOrbit = _driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Outer Orbit", "-") as IAgVAMCSPropagate;
            ((IAgVAMCSSegment)outerOrbit).Properties.Color = Color.Green;
            outerOrbit.PropagatorName = "Earth Point Mass";
            ((IAgVAStoppingCondition)outerOrbit.StoppingConditions["Duration"].Properties).Trip = 86400;
            button8.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.button8.Enabled = false;
            _driver.RunMCS();
            IAgVAMCSTargetSequence startTransfer = _driver.MainSequence["Start Transfer"] as IAgVAMCSTargetSequence;
            IAgVAMCSTargetSequence finishTransfer = _driver.MainSequence["Finish Transfer"] as IAgVAMCSTargetSequence;
            IAgVAProfileDifferentialCorrector startDC = startTransfer.Profiles["Differential Corrector"] as IAgVAProfileDifferentialCorrector;
            Console.WriteLine(startDC.ControlParameters.GetControlByPaths("DV1", "ImpulsiveMnvr.Cartesian.X").FinalValue);
            IAgVAProfileDifferentialCorrector finishDC = finishTransfer.Profiles["Differential Corrector"] as IAgVAProfileDifferentialCorrector;
            Console.WriteLine(finishDC.ControlParameters.GetControlByPaths("DV2", "ImpulsiveMnvr.Cartesian.X").FinalValue);
            IAgVAMCSManeuver dv1 = startTransfer.Segments["DV1"] as IAgVAMCSManeuver;
            IAgVAManeuverImpulsive dv1Impulsive = dv1.Maneuver as IAgVAManeuverImpulsive;
            IAgVAAttitudeControlImpulsiveThrustVector dv1ThrustVector = dv1Impulsive.AttitudeControl as IAgVAAttitudeControlImpulsiveThrustVector;
            IAgCartesian cartesian = dv1ThrustVector.DeltaVVector.ConvertTo(AgEPositionType.eCartesian) as IAgCartesian;
            Console.WriteLine(cartesian.X);
            startTransfer.ApplyProfiles();
            cartesian = dv1ThrustVector.DeltaVVector.ConvertTo(AgEPositionType.eCartesian) as IAgCartesian;
            Console.WriteLine(cartesian.X);

            IAgVAMCSManeuver dv2 = finishTransfer.Segments["DV2"] as IAgVAMCSManeuver;
            IAgVAManeuverImpulsive dv2Impulsive = dv2.Maneuver as IAgVAManeuverImpulsive;
            IAgVAAttitudeControlImpulsiveThrustVector dv2ThrustVector = dv2Impulsive.AttitudeControl as IAgVAAttitudeControlImpulsiveThrustVector;
            cartesian = dv2ThrustVector.DeltaVVector.ConvertTo(AgEPositionType.eCartesian) as IAgCartesian;
            Console.WriteLine(cartesian.X);
            finishTransfer.ApplyProfiles();
            cartesian = dv2ThrustVector.DeltaVVector.ConvertTo(AgEPositionType.eCartesian) as IAgCartesian;
            Console.WriteLine(cartesian.X);
            this.button10.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
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




    }
}