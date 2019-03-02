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

namespace MoonMissionWithBPlaneTargeting
{
    public partial class Form1 : Form
    {
        private AgStkObjectRoot stkRootObject = null;
        private IAgVADriverMCS _driver;
        private IAgSatellite _lunarProbe;
        IAgVAMCSLaunch _launch;
        IAgVAMCSPropagate _coast;
        IAgVAMCSPropagate _toPersilene;
        IAgVAMCSTargetSequence _ts;
        IAgVAMCSManeuver _transLunarInjection;
        IAgVAProfileDifferentialCorrector _dcCopy;

        private AGI.STKObjects.AgStkObjectRoot stkRoot
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

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            stkRoot.NewScenario("Test");
            stkRoot.UnitPreferences.SetCurrentUnit("DistanceUnit", "km");
            //
            IAgScenario scene = (IAgScenario)stkRoot.CurrentScenario;
            scene.StartTime = "1 Jan 1993 00:00:00.00";
            scene.StopTime = "1 Jan 1994 00:00:00.00";
            scene.Animation.StartTime = "1 Jan 1993 00:00:00.00";
            scene.Animation.EnableAnimCycleTime = true;
            scene.Animation.AnimCycleType = AgEScEndLoopType.eEndTime;
            scene.Animation.AnimCycleTime = "1 Jan 1994 00:00:00.00";
            stkRoot.Rewind();
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            IAgPlanet sun = (IAgPlanet)stkRoot.CurrentScenario.Children.New(AgESTKObjectType.ePlanet, "Sun");
            sun.PositionSource = AgEPlPositionSourceType.ePosCentralBody;
            IAgPlPosCentralBody pos = (IAgPlPosCentralBody)sun.PositionSourceData;
            pos.AutoRename = true;
            pos.CentralBody = "Sun";

            IAgPlanet moon = (IAgPlanet)stkRoot.CurrentScenario.Children.New(AgESTKObjectType.ePlanet, "Moon");
            moon.PositionSource = AgEPlPositionSourceType.ePosCentralBody;
            pos = (IAgPlPosCentralBody)moon.PositionSourceData;
            pos.CentralBody = "Moon";

            IAgPlanet earth = (IAgPlanet)stkRoot.CurrentScenario.Children.New(AgESTKObjectType.ePlanet, "Earth");
            earth.PositionSource = AgEPlPositionSourceType.ePosCentralBody;
            pos = (IAgPlPosCentralBody)earth.PositionSourceData;
            pos.CentralBody = "Earth";
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            _lunarProbe = (IAgSatellite)stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "LunarProbe");
            _lunarProbe.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator);
            _driver = (IAgVADriverMCS)_lunarProbe.Propagator;
            _driver.Options.DrawTrajectoryIn2D = true;
            _driver.Options.DrawTrajectoryIn3D = true;
            _driver.Options.UpdateAnimationTimeForAllObjects = true;
            _lunarProbe.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesBasic);
            //
            _lunarProbe.Graphics.PassData.GroundTrack.SetLeadDataType(AgELeadTrailData.eDataNone);
            _lunarProbe.Graphics.PassData.Orbit.SetLeadDataType(AgELeadTrailData.eDataAll);
            _lunarProbe.VO.Pass.TrackData.InheritFrom2D = true;
            _lunarProbe.VO.Model.OrbitMarker.MarkerType = AgEMarkerType.eShape;
            IAgVOMarkerShape markerData = (IAgVOMarkerShape)_lunarProbe.VO.Model.OrbitMarker.MarkerData;
            markerData.Style = AgE3dMarkerShape.e3dShapePoint;
            _lunarProbe.VO.Model.OrbitMarker.PixelSize = 7;
            _lunarProbe.VO.Model.DetailThreshold.MarkerLabel = 1000000000000.0;
            _lunarProbe.VO.Model.DetailThreshold.Marker = 1000000000000.0;
            _lunarProbe.VO.Model.DetailThreshold.Point = 1000000000000.0;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            stkRoot.ExecuteCommand("MapDetails * Background Image None");
            stkRoot.ExecuteCommand("MapDetails * Map RWDB2_Coastlines State Off");
            stkRoot.ExecuteCommand("MapDetails * Map RWDB2_International_Borders State Off");
            stkRoot.ExecuteCommand("MapDetails * Map RWDB2_Islands State Off");
            stkRoot.ExecuteCommand("MapDetails * LatLon Lat Off");
            stkRoot.ExecuteCommand("MapDetails * LatLon Lon Off");
            stkRoot.ExecuteCommand("MapProjection * Orthographic Center 89 -90 Format BBR 900000 Sun");

            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            stkRoot.ExecuteCommand("VO * Celestial Moon Label Off WindowID 2");
            stkRoot.ExecuteCommand("VO * Grids Space ShowECI On ShowRadial On WindowID 2");
            stkRoot.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 1000000000000 WindowID 2");
            button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            stkRoot.ExecuteCommand("VO * CentralBody Moon 1");
            stkRoot.ExecuteCommand("VO * Celestial Moon Label Off WindowID 1");
            stkRoot.ExecuteCommand("VO * Grids Space ShowECI On ShowRadial On WindowID 1");
            stkRoot.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 1000000000000 WindowID 1");
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            _driver.MainSequence.RemoveAll();
            _ts = (IAgVAMCSTargetSequence)_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "Target Sequence", "-");
            _launch = (IAgVAMCSLaunch)_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeLaunch, "Launch", "-");
            _launch.Epoch = "1 Jan 1993 00:00:00.00";
            //
            _coast = (IAgVAMCSPropagate)_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Coast", "-");
            ((IAgVAStoppingCondition)_coast.StoppingConditions[0].Properties).Trip = 2700;
            //
            _transLunarInjection = (IAgVAMCSManeuver)_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "TransLunarInjection", "-");
            _transLunarInjection.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive);
            IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)_transLunarInjection.Maneuver;
            impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector);
            IAgVAAttitudeControlImpulsiveThrustVector thrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.AttitudeControl;
            //
            IAgVAMCSPropagate toSwingBy = (IAgVAMCSPropagate)_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypePropagate, "ToSwingBy", "-");
            toSwingBy.PropagatorName = "CisLunar";
            ((IAgVAStoppingCondition)toSwingBy.StoppingConditions.Add("R Magnitude").Properties).Trip = 300000;
            toSwingBy.StoppingConditions.Remove("Duration");
            //
            _toPersilene = (IAgVAMCSPropagate)_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypePropagate, "ToPersilene", "-");
            _toPersilene.PropagatorName = "CisLunar";
            ((IAgVAStoppingCondition)_toPersilene.StoppingConditions[0].Properties).Trip = 864000;
            IAgVAStoppingCondition alt = (IAgVAStoppingCondition)_toPersilene.StoppingConditions.Add("Altitude").Properties;
            alt.Trip = 0;
            alt.CentralBodyName = "Moon";
            //
            IAgVAStoppingCondition periapsis = (IAgVAStoppingCondition)_toPersilene.StoppingConditions.Add("Periapsis").Properties;
            periapsis.CentralBodyName = "Moon";
            button8.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Enabled = false;
            IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)_transLunarInjection.Maneuver;
            IAgVAAttitudeControlImpulsiveThrustVector thrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.AttitudeControl;
            thrustVector.DeltaVVector.AssignCartesian(3.15, 0, 0);
            _driver.RunMCS();
            button9.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Enabled = false;
            _launch.EnableControlParameter(AgEVAControlLaunch.eVAControlLaunchEpoch);
			_coast.StoppingConditions[0].EnableControlParameter(AgEVAControlStoppingCondition.eVAControlStoppingConditionTripValue);
//
			IAgComponentInfo calcObject = ((IAgVAMCSSegment)_toPersilene).Results.Add("MultiBody/Delta Right Asc");
			((IAgVAMCSSegment)_toPersilene).Results.Add("MultiBody/Delta Declination");
//
//
			IAgVAProfileDifferentialCorrector diffCorrector = (IAgVAProfileDifferentialCorrector)_ts.Profiles[0];
			diffCorrector.Name = "Delta RA and Dec";
            diffCorrector.ControlParameters.GetControlByPaths("Launch", "Launch.Epoch").Perturbation = 60;
            diffCorrector.ControlParameters.GetControlByPaths("Launch", "Launch.Epoch").MaxStep = 3600;
            diffCorrector.ControlParameters.GetControlByPaths("Launch", "Launch.Epoch").Enable = true;
            diffCorrector.ControlParameters.GetControlByPaths("Coast", "StoppingConditions.Duration.TripValue").Perturbation = 60;
            diffCorrector.ControlParameters.GetControlByPaths("Coast", "StoppingConditions.Duration.TripValue").MaxStep = 300;
            diffCorrector.ControlParameters.GetControlByPaths("Coast", "StoppingConditions.Duration.TripValue").Enable = true;
//
			diffCorrector.Results[0].DesiredValue = 0;
            diffCorrector.Results[0].Enable = true;
//
            diffCorrector.Results[1].DesiredValue = 0;
            diffCorrector.Results[1].Enable = true;
            button10.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfilesOnce;
            _driver.RunMCS();
            _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles;
            _driver.RunMCS();
            //
            _ts.ApplyProfiles();
            _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunNominalSeq;
            _driver.RunMCS();
            button11.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.Enabled = false;
            _dcCopy = (IAgVAProfileDifferentialCorrector)_ts.Profiles[0].Copy();
            _dcCopy.Name = "B_Plane_Targeting";
            _ts.Profiles[0].Mode = AgEVAProfileMode.eVAProfileModeNotActive;
            //
            ((IAgVAMCSSegment)_toPersilene).Results.Add("Epoch");
            ((IAgVAMCSSegment)_toPersilene).Results.Add("MultiBody/BDotT");
            ((IAgVAMCSSegment)_toPersilene).Results.Add("MultiBody/BDotR");
            //
            _transLunarInjection.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX);
            //
            _dcCopy.ControlParameters.GetControlByPaths("TransLunarInjection", "ImpulsiveMnvr.Cartesian.X").Enable = true;
            //
            _dcCopy.Results.GetResultByPaths("ToPersilene", "Delta Declination").Enable = false;
            _dcCopy.Results.GetResultByPaths("ToPersilene", "Delta Right Asc").Enable = false;
            //
            _dcCopy.Results.GetResultByPaths("ToPersilene", "BDotR").Enable = true;
            _dcCopy.Results.GetResultByPaths("ToPersilene", "BDotT").Enable = true;
            _dcCopy.Results.GetResultByPaths("ToPersilene", "Epoch").Enable = true;
            _dcCopy.Results.GetResultByPaths("ToPersilene", "BDotR").DesiredValue = 5000;
            _dcCopy.Results.GetResultByPaths("ToPersilene", "BDotT").DesiredValue = 0;
            _dcCopy.Results.GetResultByPaths("ToPersilene", "Epoch").DesiredValue = "4 Jan 1993 00:00:00.00";
            button12.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;
            IAgVeVOBPlaneTemplate template = (IAgVeVOBPlaneTemplate)_lunarProbe.VO.BPlanes.Templates.Add();
            template.Name = "Lunar_B-Plane";
            template.CentralBody = "Moon";
            template.ReferenceVector = "CentralBody/Moon Orbit_Normal Vector";
            IAgVeVOBPlaneInstance bPlane = (IAgVeVOBPlaneInstance)_lunarProbe.VO.BPlanes.Instances.Add("Lunar_B-Plane");
            bPlane.Name = "LunarBPlane";
            //
            ((IAgVAMCSSegment)_toPersilene).Properties.BPlanes.Add("LunarBPlane");
            ((IAgVAMCSSegment)_toPersilene).Properties.ApplyFinalStateToBPlanes();
            button13.Enabled = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Enabled = false;
            _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfilesOnce;
            _driver.RunMCS();
            //
            _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles;
            _driver.RunMCS();
            ////
            _ts.ApplyProfiles();
            _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunNominalSeq;
            _driver.RunMCS();
            button14.Enabled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.Enabled = false;
            stkRoot.ExecuteCommand("VectorTool * Moon Create Axes True_Moon_Equator \"Aligned and Constrained\" Cartesian 0 0 1 \"CentralBody/Moon Angular_Velocity\"  Cartesian 1 0 0 \"CentralBody/Moon VernalEquinox\"");
            stkRoot.ExecuteCommand("VectorTool * Moon Create System True_Lunar_Equatorial \"Assembled\" \"CentralBody/Moon Center\" \"CentralBody/Moon True_Moon_Equator\"");
            //
            //
            IAgVAProfileDifferentialCorrector altInc = (IAgVAProfileDifferentialCorrector)_dcCopy.Copy();
            altInc.Name = "Altitude and Inclination";
            //
            _ts.Profiles[0].Mode = AgEVAProfileMode.eVAProfileModeNotActive;
            _dcCopy.Mode = AgEVAProfileMode.eVAProfileModeNotActive;
            //
            //
            IAgVAStateCalcGeodeticElem calcAlt = (IAgVAStateCalcGeodeticElem)((IAgVAMCSSegment)_toPersilene).Results.Add("Geodetic/Altitude");
            calcAlt.CentralBodyName = "Moon";
            IAgVAStateCalcInclination calcInc = (IAgVAStateCalcInclination)((IAgVAMCSSegment)_toPersilene).Results.Add("Keplerian Elems/Inclination");
            calcInc.CoordSystemName = "CentralBody/Moon True_Lunar_Equatorial";
            //

            for (int count = 0; count < altInc.Results.Count; count++)
            {
                altInc.Results[count].Enable = false;
            }
            altInc.Results.GetResultByPaths("ToPersilene", "Altitude").Enable = true;
            altInc.Results.GetResultByPaths("ToPersilene", "Epoch").Enable = true;
            altInc.Results.GetResultByPaths("ToPersilene", "Inclination").Enable = true;
            //
            altInc.Results.GetResultByPaths("ToPersilene", "Altitude").DesiredValue = 100;
            altInc.Results.GetResultByPaths("ToPersilene", "Inclination").DesiredValue = 90;
            altInc.Results.GetResultByPaths("ToPersilene", "Epoch").DesiredValue = "4 Jan 1993 00:00:00.00";
            //
            _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles;
            _driver.RunMCS();
            _ts.ApplyProfiles();
            _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunNominalSeq;
            _driver.RunMCS();
            
            button15.Enabled = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button15.Enabled = false;
            IAgVAMCSPropagate prop3Day = (IAgVAMCSPropagate)_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Prop3Days", "-");
            prop3Day.PropagatorName = "CisLunar";
            ((IAgVAStoppingCondition)prop3Day.StoppingConditions[0].Properties).Trip = 259200;
            //
            _driver.RunMCS();
            button16.Enabled = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button16.Enabled = false;
            IAgVAMCSTargetSequence ts2 = (IAgVAMCSTargetSequence)_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "Target Sequence2", "Prop3Days");
            IAgVAMCSManeuver loi = (IAgVAMCSManeuver)ts2.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "LOI", "-");
            loi.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive);
            IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)loi.Maneuver;
            impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector);
            IAgVAAttitudeControlImpulsiveThrustVector thrust = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.AttitudeControl;
            thrust.ThrustAxesName = "Satellite VNC(Moon)";
            loi.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX);
            IAgVAStateCalcEccentricity ecc = (IAgVAStateCalcEccentricity)((IAgVAMCSSegment)loi).Results.Add("Keplerian Elems/Eccentricity");
            ecc.CentralBodyName = "Moon";
            //
            IAgVAProfileDifferentialCorrector diffCorrector = (IAgVAProfileDifferentialCorrector)ts2.Profiles[0];
            diffCorrector.ControlParameters[0].Enable = true;
            diffCorrector.Results[0].Enable = true;
            //
            ts2.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles;
            _driver.RunMCS();
            ts2.ApplyProfiles();
            ts2.Action = AgEVATargetSeqAction.eVATargetSeqActionRunNominalSeq;
            _driver.RunMCS();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            stkRoot.PlayForward();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            stkRoot.Rewind();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            stkRoot.Slower();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            stkRoot.Faster();
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