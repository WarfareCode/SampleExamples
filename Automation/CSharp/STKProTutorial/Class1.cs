//-------------------------------------------------------------------------
//
//  This is part of the STK 8 Object Model Examples
//  Copyright (C) 2006 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 8 Object Model.
//
//  File: Class1.cs
//  STKProTutorial
//
//
//  The features used in this example try to mimick the proTutorial.pdf 
//  shipped with STK. This example follows the tutorial step by step. 
//
//--------------------------------------------------------------------------

using System;
using AGI.STKUtil;
using AGI.STKObjects;
using AGI.Ui.Application;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing;
using AGI.STKVgt;

namespace STKProTutorial
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			Class1 class1 = new Class1();
			class1.Run();
		}

		#region Class Members
        private AgUiApplication     m_oSTK;
		private IAgStkObjectRoot	m_oApplication;
		#endregion

		#region Constructor
		public Class1()
		{
			try
			{
                m_oSTK = Marshal.GetActiveObject("STK11.Application") as AGI.Ui.Application.AgUiApplication;
                Console.Write("Looking for an instance of STK... ");
			}
			catch
			{
                Console.Write("Creating a new STK 11 instance... ");
                Guid clsID = typeof(AgUiApplicationClass).GUID;
                Type oType = Type.GetTypeFromCLSID(clsID);
                m_oSTK = Activator.CreateInstance(oType) as AGI.Ui.Application.AgUiApplication;
                try
                {
                    m_oSTK.LoadPersonality("STK");
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
			}

            try
            {
                m_oApplication = (IAgStkObjectRoot)m_oSTK.Personality2;
                Console.WriteLine("done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
		#endregion

        public void Run()
		{
			this.m_oApplication.CloseScenario();
			this.m_oApplication.NewScenario("ProTutorial");

			IAgUnitPrefsDimCollection dimensions = this.m_oApplication.UnitPreferences;
			dimensions.ResetUnits();
			dimensions.SetCurrentUnit("DateFormat", "UTCG");
			IAgScenario scene= (IAgScenario)this.m_oApplication.CurrentScenario;
			
			scene.StartTime = "1 Jul 2002 00:00:00.00";
			scene.StopTime = "1 Jul 2002 04:00:00.00";
			scene.Epoch = "1 Jul 2002 00:00:00.00";

			dimensions.SetCurrentUnit("DistanceUnit", "km");
			dimensions.SetCurrentUnit("TimeUnit", "sec");
			dimensions.SetCurrentUnit("AngleUnit", "deg");
			dimensions.SetCurrentUnit("MassUnit", "kg");
			dimensions.SetCurrentUnit("PowerUnit", "dbw");
			dimensions.SetCurrentUnit("FrequencyUnit", "ghz");
			dimensions.SetCurrentUnit("SmallDistanceUnit", "m");
			dimensions.SetCurrentUnit("latitudeUnit", "deg");
			dimensions.SetCurrentUnit("longitudeunit", "deg");
			dimensions.SetCurrentUnit("DurationUnit", "HMS");
			dimensions.SetCurrentUnit("Temperature", "K");
			dimensions.SetCurrentUnit("SmallTimeUnit", "sec");
			dimensions.SetCurrentUnit("RatioUnit", "db");
			dimensions.SetCurrentUnit("rcsUnit", "dbsm");
			dimensions.SetCurrentUnit("DopplerVelocityUnit", "m/s");
            dimensions.SetCurrentUnit("Percent", "unitValue");

			//currently there is no way to set the 2d graphics properties for the scenario listed in the tutorial


			IAgFacility baikonur = (IAgFacility)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Baikonur");
            baikonur.UseTerrain = false;
            IAgPlanetodetic planetodetic = (IAgPlanetodetic)baikonur.Position.ConvertTo(AgEPositionType.ePlanetodetic);
            planetodetic.Lat = 48.0;
            planetodetic.Lon = 55.0;
            planetodetic.Alt = 0.0;
            baikonur.Position.Assign(planetodetic);

			((IAgStkObject)baikonur).ShortDescription = "Launch Site";
			((IAgStkObject)baikonur).LongDescription = "Launch site in Kazakhstan. Also known as Tyuratam.";

			IAgFacility perth = (IAgFacility)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Perth");
			IAgFacility wallops = (IAgFacility)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Wallops");

            perth.UseTerrain = false;
            planetodetic = (IAgPlanetodetic)perth.Position.ConvertTo(AgEPositionType.ePlanetodetic);
            planetodetic.Lat = -31.0;
            planetodetic.Lon = 116.0;
            planetodetic.Alt = 0;
            perth.Position.Assign(planetodetic);
		
			((IAgStkObject)perth).ShortDescription = "Australian Tracking Station";

            wallops.UseTerrain = false;
            planetodetic = (IAgPlanetodetic)wallops.Position.ConvertTo(AgEPositionType.ePlanetodetic);
            planetodetic.Lat = 37.8602;
            planetodetic.Lon = -75.5095;
            planetodetic.Alt = -0.0127878;
            wallops.Position.Assign(planetodetic);
		
			((IAgStkObject)wallops).ShortDescription = "NASA Launch Site/Tracking Station";

            AGI.STKUtil.AgExecCmdResult facDbResult = (AgExecCmdResult)m_oApplication.ExecuteCommand("GetDirectory / Database Facility");
            string facDataDir = facDbResult[0];
            string filelocation = facDataDir + @"\stkFacility.fd";
            string command = "ImportFromDB * Facility \"" + filelocation + "\"Class Facility SiteName \"Santiago Station AGO 3 STDN AGO3\" Network \"NASA NEN\" Rename Santiago";
			m_oApplication.ExecuteCommand(command);
            command = "ImportFromDB * Facility \"" + filelocation + "\"Class Facility SiteName \"White Sands\" Network \"Other\" Rename WhiteSands";
			m_oApplication.ExecuteCommand(command);

			IAgFacility santiago = (IAgFacility)m_oApplication.CurrentScenario.Children["Santiago"];
			IAgFacility whitesands = (IAgFacility)m_oApplication.CurrentScenario.Children["WhiteSands"];

			baikonur.Graphics.Color = Color.Black;
			perth.Graphics.Color = Color.FromArgb(16777215);
			wallops.Graphics.Color = Color.FromArgb(13421772);
			santiago.Graphics.Color = Color.FromArgb(88888);
			whitesands.Graphics.Color = Color.FromArgb(1234567);


			IAgTarget iceberg = (IAgTarget)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eTarget, "Iceberg");
            iceberg.UseTerrain = false;
            planetodetic = (IAgPlanetodetic)iceberg.Position.ConvertTo(AgEPositionType.ePlanetodetic);
            planetodetic.Lat = 74.91;
            planetodetic.Lon = -74.5;
            planetodetic.Alt = 0.0;
            iceberg.Position.Assign(planetodetic);

			((IAgStkObject)iceberg).ShortDescription = "Only the tip.";

			IAgShip cruise = (IAgShip)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eShip, "Cruise");
			cruise.SetRouteType(AgEVePropagatorType.ePropagatorGreatArc);
			IAgVePropagatorGreatArc greatArc = (IAgVePropagatorGreatArc)cruise.Route;
            IAgCrdnEventIntervalSmartInterval smartInterval = greatArc.EphemerisInterval;
            smartInterval.SetExplicitInterval("1 Jul 2002 00:00:00.00", smartInterval.FindStopTime());
			greatArc.Method = AgEVeWayPtCompMethod.eDetermineTimeAccFromVel;

			AddWaypoint(greatArc.Waypoints, 44.1, -8.5, 0.0, .015, 0.0);
			AddWaypoint(greatArc.Waypoints, 51.0, -26.6, 0.0, .015, 0.0);
			AddWaypoint(greatArc.Waypoints, 52.1, -40.1, 0.0, .015, 0.0);
			AddWaypoint(greatArc.Waypoints, 60.2, -55.0, 0.0, .015, 0.0);
			AddWaypoint(greatArc.Waypoints, 68.2, -65.0, 0.0, .015, 0.0);
			AddWaypoint(greatArc.Waypoints, 72.5, -70.1, 0.0, .015, 0.0);
			AddWaypoint(greatArc.Waypoints, 74.9, -74.5, 0.0, .015, 0.0);

			cruise.SetAttitudeType(AgEVeAttitude.eAttitudeStandard);
			IAgVeRouteAttitudeStandard attitude = (IAgVeRouteAttitudeStandard)cruise.Attitude;
			attitude.Basic.SetProfileType(AgEVeProfile.eProfileECFVelocityAlignmentWithRadialConstraint);
			cruise.Graphics.WaypointMarker.IsWaypointMarkersVisible = true;
			cruise.Graphics.WaypointMarker.IsTurnMarkersVisible = true;
			greatArc.Propagate();

			IAgSatellite tdrs = (IAgSatellite)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "TDRS");
			tdrs.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);
			IAgVePropagatorTwoBody twobody = (IAgVePropagatorTwoBody)tdrs.Propagator;

			IAgOrbitStateClassical classical = (IAgOrbitStateClassical)twobody.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical);
			classical.CoordinateSystemType = AgECoordinateSystem.eCoordinateSystemJ2000;
            smartInterval = twobody.EphemerisInterval;
            smartInterval.SetExplicitInterval("1 Jul 2002 00:00:00.000", "1 Jul 2002 04:00:00.000");
			twobody.Step = 60;
			classical.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
			IAgClassicalLocationTrueAnomaly trueAnomaly = (IAgClassicalLocationTrueAnomaly)classical.Location;
			trueAnomaly.Value = 178.845262;

			classical.SizeShapeType = AgEClassicalSizeShape.eSizeShapePeriod;
			IAgClassicalSizeShapePeriod period = (IAgClassicalSizeShapePeriod)classical.SizeShape;
			period.Eccentricity = 0.0;

			period.Period = 86164.090540;

			classical.Orientation.ArgOfPerigee = 0.0;

			classical.Orientation.Inclination = 0.0;

			classical.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeLAN;
			IAgOrientationAscNodeLAN lan = (IAgOrientationAscNodeLAN)classical.Orientation.AscNode;
			lan.Value = 259.999982;

			twobody.InitialState.Representation.Assign(classical);

			twobody.Propagate();

            AGI.STKUtil.AgExecCmdResult satDbResult = (AgExecCmdResult)m_oApplication.ExecuteCommand("GetDirectory / Database Satellite");
            string satDataDir = satDbResult[0];
            filelocation = satDataDir + @"\stkSatDB.sd";
			command = "ImportFromDB * Satellite \"" + filelocation + "\" Rename \"TDRS 3\" Propagate On CommonName \"TDRS 3\"";
			m_oApplication.ExecuteCommand(command);

			IAgSatellite tdrsC = (IAgSatellite)m_oApplication.CurrentScenario.Children["TDRS_3"];

			IAgVePropagatorSGP4 sgp4 = (IAgVePropagatorSGP4)tdrsC.Propagator;
            smartInterval = sgp4.EphemerisInterval;
            smartInterval.SetExplicitInterval("1 Jul 2002 00:00:00.000", "1 Jul 2002 04:00:00.000");

			IAgSatellite ers1 = (IAgSatellite)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "ERS1");
			ers1.SetPropagatorType(AgEVePropagatorType.ePropagatorJ4Perturbation);
			IAgVePropagatorJ4Perturbation j4 = (IAgVePropagatorJ4Perturbation)ers1.Propagator;
            smartInterval = j4.EphemerisInterval;
            smartInterval.SetExplicitInterval("1 Jul 2002 00:00:00.000", "1 Jul 2002 04:00:00.000");
			j4.Step = 60.00;

            IAgOrbitState j4Orb = j4.InitialState.Representation as IAgOrbitState;
            j4Orb.Epoch = "1 Jul 2002 00:00:00.00";
			classical = (IAgOrbitStateClassical)j4.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical);
			classical.CoordinateSystemType = AgECoordinateSystem.eCoordinateSystemJ2000;
			classical.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
			trueAnomaly = (IAgClassicalLocationTrueAnomaly)classical.Location;
			trueAnomaly.Value = 0.0;
			classical.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
			IAgClassicalSizeShapeSemimajorAxis semi = (IAgClassicalSizeShapeSemimajorAxis)classical.SizeShape;
			semi.SemiMajorAxis = 7163.14;
			semi.Eccentricity = 0.0;
			classical.Orientation.ArgOfPerigee = 0.0;
			classical.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeLAN;
			lan = (IAgOrientationAscNodeLAN)classical.Orientation.AscNode;
			lan.Value = 99.38;
			classical.Orientation.Inclination = 98.50;

			j4.InitialState.Representation.Assign(classical);

			j4.Propagate();
			
			ers1.Graphics.Passes.VisibleSides = AgEVeGfxVisibleSides.eVisibleSidesDescending;


			ers1.Graphics.Passes.VisibleSides = AgEVeGfxVisibleSides.eVisibleSidesBoth;


			IAgSatellite shuttle = (IAgSatellite)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Shuttle");
			shuttle.SetPropagatorType(AgEVePropagatorType.ePropagatorJ4Perturbation);
			j4 = (IAgVePropagatorJ4Perturbation)shuttle.Propagator;
            smartInterval = j4.EphemerisInterval;
            smartInterval.SetExplicitInterval("1 Jul 2002 00:00:00.000", "1 Jul 2002 03:00:00.000");
			j4.Step = 60.00;

            j4Orb = j4.InitialState.Representation as IAgOrbitState;
            j4Orb.Epoch = "1 Jul 2002 00:00:00.00";
			classical = (IAgOrbitStateClassical)j4.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical);
			classical.CoordinateSystemType = AgECoordinateSystem.eCoordinateSystemJ2000;
			classical.LocationType = AgEClassicalLocation.eLocationTrueAnomaly;
			trueAnomaly = (IAgClassicalLocationTrueAnomaly)classical.Location;
			trueAnomaly.Value = 0.0;
			classical.SizeShapeType = AgEClassicalSizeShape.eSizeShapeAltitude;
			IAgClassicalSizeShapeAltitude altitude = (IAgClassicalSizeShapeAltitude)classical.SizeShape;
			altitude.ApogeeAltitude= 370.4;
			altitude.PerigeeAltitude = 370.4;
			classical.Orientation.ArgOfPerigee = 0.0;
			classical.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeLAN;
			lan = (IAgOrientationAscNodeLAN)classical.Orientation.AscNode;
			lan.Value = -151.0;
			classical.Orientation.Inclination = 28.5;

			j4.InitialState.Representation.Assign(classical);

			j4.Propagate();

			shuttle.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesBasic);
			IAgVeGfxAttributesOrbit orbitgfx = (IAgVeGfxAttributesOrbit)shuttle.Graphics.Attributes;
			orbitgfx.Line.Style = AgELineStyle.eDashed;
			orbitgfx.MarkerStyle = "Plus";

			IAgVeGfxElevContours contours = (IAgVeGfxElevContours)shuttle.Graphics.ElevContours;
			IAgVeGfxElevationsCollection elevations = contours.Elevations;
			elevations.RemoveAll();
			elevations.AddLevelRange(0, 50, 10);

			for(int i = 0; i < elevations.Count; i++)
			{
				IAgVeGfxElevationsElement elem = elevations[i];
				elem.DistanceVisible = false;
				elem.LineStyle = AgELineStyle.eDotDashed;
				elem.LineWidth = AgELineWidth.e3;
			}

			contours.IsVisible = true;
	
			//Again we don't have the capability in OM to handle a second graphics window and modify it's properties

			IAgAreaTarget searchArea = (IAgAreaTarget)m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eAreaTarget, "SearchArea");
			IAgATGraphics atGfx = searchArea.Graphics;
			atGfx.MarkerStyle = "None";
			atGfx.Inherit = false;
			atGfx.LabelVisible = false;
			atGfx.CentroidVisible = false;

            searchArea.UseTerrainData = false;
			searchArea.AutoCentroid = false;
			searchArea.AreaType = AgEAreaType.ePattern;
			IAgAreaTypePatternCollection patterns = (IAgAreaTypePatternCollection)searchArea.AreaTypeData;
			patterns.Add(78.4399, -77.6125);
			patterns.Add(77.7879, -71.1578);
			patterns.Add(74.5279, -69.0714);
			patterns.Add(71.6591, -69.1316);
			patterns.Add(70.0291, -70.8318);
			patterns.Add(71.9851, -76.3086);

			IAgSpherical sphere = (IAgSpherical)searchArea.Position.ConvertTo(AgEPositionType.eSpherical);
			sphere.Lat = 74.9533;
			sphere.Lon = -74.5482;
			sphere.Radius = 6358.186790;
			searchArea.Position.Assign(sphere);


			IAgStkAccess access = ((IAgStkObject)ers1).GetAccessToObject((IAgStkObject)searchArea);
			access.ComputeAccess();

			IAgDataPrvInterval interval = (IAgDataPrvInterval)access.DataProviders["Access Data"];

			IAgDrResult result = (IAgDrResult)interval.Exec("1 Jul 2002 00:00:00.000", "1 Jul 2002 04:00:00.000");
			
			//with the result returned, the user can use the data any way they prefer.

			access.RemoveAccess();
			

			IAgSensor horizon = (IAgSensor)m_oApplication.CurrentScenario.Children["ERS1"].Children.New(AgESTKObjectType.eSensor, "Horizon");
			horizon.SetPatternType(AgESnPattern.eSnSimpleConic);
			IAgSnSimpleConicPattern simpleConic  = (IAgSnSimpleConicPattern)horizon.Pattern;
			simpleConic.ConeAngle = 90;
			horizon.SetPointingType(AgESnPointing.eSnPtFixed);
			IAgSnPtFixed fixedPt = (IAgSnPtFixed)horizon.Pointing;
			IAgOrientationAzEl azEl = (IAgOrientationAzEl)fixedPt.Orientation.ConvertTo(AgEOrientationType.eAzEl);
			azEl.Elevation = 90;
			azEl.AboutBoresight = AgEAzElAboutBoresight.eAzElAboutBoresightRotate;
			fixedPt.Orientation.Assign(azEl);

			//removing the ers1 elevcontours from the 2d window
			contours.IsVisible = false;

			IAgSensor downlink = (IAgSensor)m_oApplication.CurrentScenario.Children["ERS1"].Children.New(AgESTKObjectType.eSensor, "Downlink");

			downlink.SetPatternType(AgESnPattern.eSnHalfPower);
			IAgSnHalfPowerPattern halfpower = (IAgSnHalfPowerPattern)downlink.Pattern;
			halfpower.Frequency = .85;
			halfpower.AntennaDiameter = 1.0;


			downlink.SetPointingType(AgESnPointing.eSnPtTargeted);
			IAgSnPtTargeted targeted = (IAgSnPtTargeted)downlink.Pointing;
			targeted.Boresight = AgESnPtTrgtBsightType.eSnPtTrgtBsightTracking;
			IAgSnTargetCollection targets = targeted.Targets;
			targets.Add("Facility/Baikonur");
			targets.Add("Facility/WhiteSands");
			targets.Add("Facility/Perth");
			targets.AddObject((IAgStkObject)santiago);
			targets.Add(((IAgStkObject)wallops).Path);


			IAgSensor fiveDegElev = (IAgSensor)m_oApplication.CurrentScenario.Children["Wallops"].Children.New(AgESTKObjectType.eSensor, "FiveDegElev");

			fiveDegElev.SetPatternType(AgESnPattern.eSnComplexConic);
			IAgSnComplexConicPattern complexConic = (IAgSnComplexConicPattern)fiveDegElev.Pattern;
			complexConic.InnerConeHalfAngle = 0;
			complexConic.OuterConeHalfAngle = 85;
			complexConic.MinimumClockAngle = 0;
			complexConic.MaximumClockAngle = 360;

			fiveDegElev.SetPointingType(AgESnPointing.eSnPtFixed);
			fixedPt = (IAgSnPtFixed)fiveDegElev.Pointing;
			azEl = (IAgOrientationAzEl)fixedPt.Orientation.ConvertTo(AgEOrientationType.eAzEl);
			azEl.Elevation= 90;
			azEl.AboutBoresight = AgEAzElAboutBoresight.eAzElAboutBoresightRotate;
			fixedPt.Orientation.Assign(azEl);

			fiveDegElev.Graphics.Projection.DistanceType = AgESnProjectionDistanceType.eConstantAlt;
			IAgSnProjDisplayDistance dispDistance = (IAgSnProjDisplayDistance)fiveDegElev.Graphics.Projection.DistanceData;
			dispDistance.Max = 785.248;
			dispDistance.Min = 0;
			dispDistance.NumberOfSteps = 1;

			j4 = (IAgVePropagatorJ4Perturbation)ers1.Propagator;
            smartInterval = j4.EphemerisInterval;
            smartInterval.SetExplicitInterval(smartInterval.FindStartTime(), "2 Jul 2002 00:00:00.000");
			j4.Propagate();

			ers1.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesCustom);
			IAgVeGfxAttributesCustom customAtt = (IAgVeGfxAttributesCustom)ers1.Graphics.Attributes;
			IAgVeGfxInterval gfxInterval = customAtt.Intervals.Add("1 Jul 2002 11:30:00.000", "1 Jul 2002 12:00:00.000");
			gfxInterval.GfxAttributes.Color = Color.FromArgb(15649024); //EEC900
			gfxInterval.GfxAttributes.IsVisible = true;
			gfxInterval.GfxAttributes.Inherit= true;

			gfxInterval = customAtt.Intervals.Add("1 Jul 2002 23:30:00.000", "1 Jul 2002 24:00:00.000");
			gfxInterval.GfxAttributes.Color = Color.FromArgb(11680494); //B23AEE
			gfxInterval.GfxAttributes.IsVisible = true;
			gfxInterval.GfxAttributes.Inherit = true;


			ers1.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesAccess);
			IAgVeGfxAttributesAccess gfxAccess = (IAgVeGfxAttributesAccess)ers1.Graphics.Attributes;

			gfxAccess.AccessObjects.Add("Facility/Wallops");
			gfxAccess.AccessObjects.Add("Facility/Santiago");
			gfxAccess.AccessObjects.Add("Facility/Baikonur");
			gfxAccess.AccessObjects.Add("Facility/Perth");
			gfxAccess.AccessObjects.Add(((IAgStkObject)whitesands).Path);

			IAgVeGfxAttributesOrbit orbitGfx = (IAgVeGfxAttributesOrbit)gfxAccess.NoAccess;
			orbitGfx.IsVisible = true;
			orbitGfx.Inherit = false;
			orbitGfx.IsGroundMarkerVisible = false;
			orbitGfx.IsOrbitMarkerVisible = false;

			IAgDisplayTm horizonDispTm = (IAgDisplayTm)horizon;
			horizonDispTm.SetDisplayStatusType(AgEDisplayTimesType.eDuringAccess);
			IAgDuringAccess duringAccess = (IAgDuringAccess)horizonDispTm.DisplayTimesData;

			IAgObjectLinkCollection accessObjects = duringAccess.AccessObjects;
			accessObjects.Add("Facility/Wallops");
			accessObjects.Add("Facility/Santiago");
			accessObjects.Add("Facility/Baikonur");
			accessObjects.AddObject((IAgStkObject)perth);
			accessObjects.Add(((IAgStkObject)whitesands).Path);



			access = ((IAgStkObject)horizon).GetAccessToObject((IAgStkObject)baikonur);
			access.ComputeAccess();

			IAgAccessCnstrMinMax minMax = (IAgAccessCnstrMinMax)horizon.AccessConstraints.AddConstraint(AgEAccessConstraints.eCstrSunElevationAngle);
			minMax.EnableMin = true;
			minMax.Min = 10;


			minMax.Min = 5;
			minMax.Min = 0;
			minMax.Min = 15;
			minMax.Min = 20;

			horizon.AccessConstraints.RemoveConstraint(AgEAccessConstraints.eCstrSunElevationAngle);

			minMax = (IAgAccessCnstrMinMax)horizon.AccessConstraints.AddConstraint(AgEAccessConstraints.eCstrRange);

			minMax.EnableMax = true;
			minMax.Max = 2000;
			minMax.Max = 1500;
			minMax.Max = 1000;
			minMax.Max = 500;

			horizon.AccessConstraints.RemoveConstraint(AgEAccessConstraints.eCstrRange);
			
			access.RemoveAccess();
			((IAgAnimation)this.m_oApplication).Rewind();

            Console.Write("Press Enter to exit application:");
            Console.ReadLine();
		}


		private void AddWaypoint(IAgVeWaypointsCollection waypoints, object Lat, object Lon, double Alt, double Speed, double tr)
		{
			IAgVeWaypointsElement elem = waypoints.Add();
			elem.Latitude = Lat;
			elem.Longitude = Lon;
			elem.Altitude = Alt;
			elem.Speed = Speed;
			elem.TurnRadius = tr;
		}
	}
}
