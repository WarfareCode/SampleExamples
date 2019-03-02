//-------------------------------------------------------------------------
//
//  This is part of the STK 8 Object Model Examples
//  Copyright (C) 2006 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 8 Object Model.
//
//  File: STKProTutorial.cs
//  STKProTutorial
//
//
//  The features used in this example try to mimick the proTutorial.pdf 
//  shipped with STK using STKX. By choosing the drop down items in order
//  you will essentially be following the tutorial step by step. Some drop
//  down items have dependencies on previous drop down items and will throw
//  an exception if the previous action did not occur. 
//
//--------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;


namespace STKProTutorial
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : Form
	{
		private AGI.STKObjects.AgStkObjectRoot root = null;

		private IAgFacility baikonur,
			                perth,
							wallops,
							santiago,
							whitesands;

		private IAgSatellite ers1,
							 shuttle;

		private IAgAreaTarget searchArea;

		private IAgStkAccess access;

		private IAgSensor horizon;

		public Form1()
		{
			
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public void AccessConstraintsRemoveSunElevationAngle()
		{
			try
			{
				horizon.AccessConstraints.RemoveConstraint(AgEAccessConstraints.eCstrSunElevationAngle);
				access.RemoveAccess();
				ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void AccessConstraintsRemoveRange()
		{
			try
			{
				horizon.AccessConstraints.RemoveConstraint(AgEAccessConstraints.eCstrRange);
				access.RemoveAccess();
				ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void AccessConstraintsRange()
		{
			try
			{
				access = ((IAgStkObject)horizon).GetAccessToObject((IAgStkObject)baikonur);
				access.ComputeAccess();
				IAgAccessCnstrMinMax minMax = (IAgAccessCnstrMinMax)horizon.AccessConstraints.AddConstraint(AgEAccessConstraints.eCstrRange);

				minMax.EnableMax = true;
				minMax.Max = 2000;
				minMax.Max = 1500;
				minMax.Max = 1000;
				minMax.Max = 500;
				ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void AccessConstraintsSunElevationAngle()
		{
			try
			{
				access = ((IAgStkObject)horizon).GetAccessToObject((IAgStkObject)baikonur);
				access.ComputeAccess();

				IAgAccessCnstrMinMax minMax = (IAgAccessCnstrMinMax)horizon.AccessConstraints.AddConstraint(AgEAccessConstraints.eCstrSunElevationAngle);
				minMax.EnableMin = true;
				minMax.Min = 10;


				minMax.Min = 5;
				minMax.Min = 0;
				minMax.Min = 15;
				minMax.Min = 20;
			
				this.ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}


		public void LimitSensorVisiblity()
		{
			try
			{
				IAgSensor fiveDegElev = (IAgSensor)ObjectRoot.CurrentScenario.Children["Wallops"].Children.New(AgESTKObjectType.eSensor, "FiveDegElev");

				fiveDegElev.SetPatternType(AgESnPattern.eSnComplexConic);
				IAgSnComplexConicPattern complexConic = (IAgSnComplexConicPattern)fiveDegElev.Pattern;
				complexConic.InnerConeHalfAngle = 0;
				complexConic.OuterConeHalfAngle = 85;
				complexConic.MinimumClockAngle = 0;
				complexConic.MaximumClockAngle = 360;

				fiveDegElev.SetPointingType(AgESnPointing.eSnPtFixed);
				IAgSnPtFixed fixedPt = (IAgSnPtFixed)fiveDegElev.Pointing;
				IAgOrientationAzEl azEl = (IAgOrientationAzEl)fixedPt.Orientation.ConvertTo(AgEOrientationType.eAzEl);
				azEl.Elevation= 90;
				azEl.AboutBoresight = AgEAzElAboutBoresight.eAzElAboutBoresightRotate;
				fixedPt.Orientation.Assign(azEl);

				fiveDegElev.Graphics.Projection.DistanceType = AgESnProjectionDistanceType.eConstantAlt;
				IAgSnProjDisplayDistance dispDistance = (IAgSnProjDisplayDistance)fiveDegElev.Graphics.Projection.DistanceData;
				dispDistance.Max = 785.248;
				dispDistance.Min = 0;
				dispDistance.NumberOfSteps = 1;
				ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void AccessDisplayIntervals()
		{
			try
			{
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
				ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void SensorCustomDisplay()
		{
			try
			{
				IAgVePropagatorJ4Perturbation j4 = (IAgVePropagatorJ4Perturbation)ers1.Propagator;
                IAgCrdnEventIntervalSmartInterval interval = j4.EphemerisInterval;
                interval.SetExplicitInterval(interval.FindStartTime(), "2 Jul 2002 00:00:00.000");
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
				ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void CreateSensors()
		{
			try
			{
				horizon = (IAgSensor)ObjectRoot.CurrentScenario.Children["ERS1"].Children.New(AgESTKObjectType.eSensor, "Horizon");
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
				ers1.Graphics.ElevContours.IsVisible = false;

				IAgSensor downlink = (IAgSensor)ObjectRoot.CurrentScenario.Children["ERS1"].Children.New(AgESTKObjectType.eSensor, "Downlink");

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

				ObjectRoot.Rewind();

			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void RemoveAccess()
		{
			try
			{
				access.RemoveAccess();
				ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void Access()
		{
			try
			{
				access = ((IAgStkObject)ers1).GetAccessToObject((IAgStkObject)searchArea);
				access.ComputeAccess();
				ObjectRoot.Rewind();

				IAgDataPrvInterval interval = (IAgDataPrvInterval)access.DataProviders["Access Data"];

				IAgDrResult result = (IAgDrResult)interval.Exec("1 Jul 2002 00:00:00.000", "1 Jul 2002 04:00:00.000");
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
			
			//With the result returned, the user can use the data any way they prefer.
		}

		public void CreateAreaTarget()
		{
			try
			{
				searchArea = (IAgAreaTarget)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eAreaTarget, "SearchArea");
				IAgATGraphics atGfx = searchArea.Graphics;
				atGfx.MarkerStyle = "None";
				atGfx.Inherit = false;
				atGfx.LabelVisible = false;
				atGfx.CentroidVisible = false;

				searchArea.AutoCentroid = false;

				searchArea.AreaType = AgEAreaType.ePattern;
				IAgAreaTypePatternCollection patterns = (IAgAreaTypePatternCollection)searchArea.AreaTypeData;
				patterns.Add(78.4399, -77.6125);
				patterns.Add(77.7879, -71.1578);
				patterns.Add(74.5279, -69.0714);
				patterns.Add(71.6591, -69.1316);
				patterns.Add(70.0291, -70.8318);
				patterns.Add(71.9851, -76.3086);

                searchArea.UseTerrainData = false;
                IAgSpherical sphere = (IAgSpherical)searchArea.Position.ConvertTo(AgEPositionType.eSpherical);
				sphere.Lat = 74.9533;
				sphere.Lon = -74.5482;
				sphere.Radius = 6358.186790;
				searchArea.Position.Assign(sphere);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void ModifyShuttleContours()
		{
			try
			{
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

				ObjectRoot.Rewind();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void CreateSatellites()
		{
			try
			{
				IAgSatellite tdrs = (IAgSatellite)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "TDRS");
				tdrs.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);
				IAgVePropagatorTwoBody twobody = (IAgVePropagatorTwoBody)tdrs.Propagator;

				IAgOrbitStateClassical classical = (IAgOrbitStateClassical)twobody.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical);
				classical.CoordinateSystemType = AGI.STKUtil.AgECoordinateSystem.eCoordinateSystemJ2000;
                IAgCrdnEventIntervalSmartInterval interval = twobody.EphemerisInterval;
                interval.SetExplicitInterval("1 Jul 2002 00:00:00.000", "1 Jul 2002 04:00:00.000");
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

                AGI.STKUtil.AgExecCmdResult result = (AgExecCmdResult)ObjectRoot.ExecuteCommand("GetDirectory / Database Satellite");
                string satDataDir = result[0];
                string filelocation = satDataDir + @"\stkSatDB.sd";
				string command = "ImportFromDB * Satellite \"" + filelocation + "\" Rename TDRS_3 Propagate On CommonName \"TDRS 3\"";
				ObjectRoot.ExecuteCommand(command);

				IAgSatellite tdrsC = (IAgSatellite)ObjectRoot.CurrentScenario.Children["TDRS_3"];

				IAgVePropagatorSGP4 sgp4 = (IAgVePropagatorSGP4)tdrsC.Propagator;
                interval = sgp4.EphemerisInterval;
                interval.SetExplicitInterval("1 Jul 2002 00:00:00.000", "1 Jul 2002 04:00:00.000");


				ers1 = (IAgSatellite)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "ERS1");
				ers1.SetPropagatorType(AgEVePropagatorType.ePropagatorJ4Perturbation);
				IAgVePropagatorJ4Perturbation j4 = (IAgVePropagatorJ4Perturbation)ers1.Propagator;
                interval = j4.EphemerisInterval;
                interval.SetExplicitInterval("1 Jul 2002 00:00:00.000", "1 Jul 2002 04:00:00.000");
				j4.Step = 60.00;

                IAgOrbitState oOrb;
                oOrb = j4.InitialState.Representation as IAgOrbitState;
                oOrb.Epoch = "1 Jul 2002 00:00:00.000";

				classical = (IAgOrbitStateClassical)j4.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical);
				classical.CoordinateSystemType = AGI.STKUtil.AgECoordinateSystem.eCoordinateSystemJ2000;
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

				ObjectRoot.Rewind();

				ers1.Graphics.Passes.VisibleSides = AgEVeGfxVisibleSides.eVisibleSidesDescending;


				ers1.Graphics.Passes.VisibleSides = AgEVeGfxVisibleSides.eVisibleSidesBoth;


				shuttle = (IAgSatellite)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Shuttle");
				shuttle.SetPropagatorType(AgEVePropagatorType.ePropagatorJ4Perturbation);
				j4 = (IAgVePropagatorJ4Perturbation)shuttle.Propagator;
                interval = j4.EphemerisInterval;
                interval.SetExplicitInterval("1 Jul 2002 00:00:00.000", "1 Jul 2002 03:00:00.000");
				j4.Step = 60.00;

                oOrb = j4.InitialState.Representation as IAgOrbitState;
                oOrb.Epoch = "1 Jul 2002 00:00:00.000";

				classical = (IAgOrbitStateClassical)j4.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical);
				classical.CoordinateSystemType = AGI.STKUtil.AgECoordinateSystem.eCoordinateSystemJ2000;
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
				ObjectRoot.Rewind();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void CreateShip()
		{
			try
			{
				IAgShip cruise = (IAgShip)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eShip, "Cruise");
				cruise.SetRouteType(AgEVePropagatorType.ePropagatorGreatArc);
				IAgVePropagatorGreatArc greatArc = (IAgVePropagatorGreatArc)cruise.Route;
                IAgCrdnEventIntervalSmartInterval interval = greatArc.EphemerisInterval;
                interval.SetExplicitInterval("1 Jul 2002 00:00:00.00", interval.FindStopTime());
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
				ObjectRoot.Rewind();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void CreateTarget()
		{
			try
			{
				IAgTarget iceberg = (IAgTarget)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eTarget, "Iceberg");
                iceberg.UseTerrain = false;
                IAgPlanetodetic planetodetic = (IAgPlanetodetic)iceberg.Position.ConvertTo(AgEPositionType.ePlanetodetic);
                planetodetic.Lat = 74.91;
                planetodetic.Lon = -74.5;
                planetodetic.Alt = 0.0;

                iceberg.Position.Assign(planetodetic);

				((IAgStkObject)iceberg).ShortDescription = "Only the tip.";
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void ChangeFacilitiesColor()
		{
			try
			{
				baikonur.Graphics.Color = Color.FromArgb(0);
				perth.Graphics.Color = Color.FromArgb(16777215);
				wallops.Graphics.Color = Color.FromArgb(13421772);
				santiago.Graphics.Color = Color.FromArgb(88888);
				whitesands.Graphics.Color = Color.FromArgb(1234567);
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void CreateFacilities()
		{
			try
			{
				baikonur = (IAgFacility)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Baikonur");
                baikonur.UseTerrain = false;
                IAgPlanetodetic planetodetic = (IAgPlanetodetic)baikonur.Position.ConvertTo(AgEPositionType.ePlanetodetic);
                planetodetic.Lat = 48.0;
                planetodetic.Lon = 55.0;
                planetodetic.Alt = 0.0;
                baikonur.Position.Assign(planetodetic);
				((IAgStkObject)baikonur).ShortDescription = "Launch Site";
				((IAgStkObject)baikonur).LongDescription = "Launch site in Kazakhstan. Also known as Tyuratam.";

				perth = (IAgFacility)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Perth");
                perth.UseTerrain = false;
                planetodetic = (IAgPlanetodetic)perth.Position.ConvertTo(AgEPositionType.ePlanetodetic);
                planetodetic.Lat = -31.0;
                planetodetic.Lon = 116.0;
                planetodetic.Alt = 0;
                perth.Position.Assign(planetodetic);
				((IAgStkObject)perth).ShortDescription = "Australian Tracking Station";

                wallops = (IAgFacility)ObjectRoot.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Wallops");
                wallops.UseTerrain = false;
                planetodetic = (IAgPlanetodetic)wallops.Position.ConvertTo(AgEPositionType.ePlanetodetic);
                planetodetic.Lat = 37.8602;
                planetodetic.Lon = -75.5095;
                planetodetic.Alt = -0.0127878;
                wallops.Position.Assign(planetodetic);
				((IAgStkObject)wallops).ShortDescription = "NASA Launch Site/Tracking Station";

                AGI.STKUtil.AgExecCmdResult result = (AgExecCmdResult)ObjectRoot.ExecuteCommand("GetDirectory / Database Facility");
                string facDataDir = result[0];
                string filelocation = facDataDir + @"\stkFacility.fd";
                string command = "ImportFromDB * Facility \"" + filelocation + "\"Class Facility SiteName \"Santiago Station AGO 3 STDN AGO3\" Network \"NASA NEN\" Rename Santiago";
				ObjectRoot.ExecuteCommand(command);
                command = "ImportFromDB * Facility \"" + filelocation + "\"Class Facility SiteName \"White Sands\" Network \"Other\" Rename WhiteSands";
				ObjectRoot.ExecuteCommand(command);

				santiago = (IAgFacility)ObjectRoot.CurrentScenario.Children["Santiago"];
				whitesands = (IAgFacility)ObjectRoot.CurrentScenario.Children["WhiteSands"];
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void NewScenario()
		{
			try
			{
				this.ObjectRoot.CloseScenario();
				this.ObjectRoot.NewScenario("ProTutorial");

				IAgUnitPrefsDimCollection dimensions = this.ObjectRoot.UnitPreferences;
				dimensions.ResetUnits();
				dimensions.SetCurrentUnit("DateFormat", "UTCG");
				IAgScenario scene= (IAgScenario)this.ObjectRoot.CurrentScenario;
			
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
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
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

		public AgStkObjectRoot ObjectRoot
		{
			get
			{
                if (root == null)
                {
                    root = new AGI.STKObjects.AgStkObjectRoot();
                }
				return root;
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            AGI.STKX.AgSTKXApplication STKXApp = null;
            try
            {
                STKXApp = new AGI.STKX.AgSTKXApplication();

                if (!STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl))
                {
                    MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (System.Runtime.InteropServices.COMException exception)
            {
                if (exception.ErrorCode == unchecked((int)0x80040154))
                {
                    string errorMessage = "Could not instantiate AgSTKXApplication.";
                    errorMessage += Environment.NewLine;
                    errorMessage += Environment.NewLine;
                    errorMessage += "Check that STK or STK Engine 64-bit is installed on this machine.";

                    MessageBox.Show(errorMessage, "STK Engine Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    throw;
                }
            }
            if (STKXApp != null)
            {
                Application.Run(new Form1());
            }
		}


		private void button1_Click(object sender, System.EventArgs e)
		{
            button1.Enabled = false;
            NewScenario();
            button2.Enabled = true;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
            button2.Enabled = false;
            CreateFacilities();
            button3.Enabled = true;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
            button3.Enabled = false;
            ChangeFacilitiesColor();
            button4.Enabled = true;
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
            button4.Enabled = false;
            CreateTarget();
            button5.Enabled = true;
		}

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            CreateShip();
            button6.Enabled = true;
        }

		private void button6_Click(object sender, System.EventArgs e)
		{
            button6.Enabled = false;
            CreateSatellites();
            button7.Enabled = true;
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
            button7.Enabled = false;
            ModifyShuttleContours();
            button8.Enabled = true;
		}

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Enabled = false;
            CreateAreaTarget();
            button9.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Enabled = false;
            Access();
            button10.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            RemoveAccess();
            button11.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.Enabled = false;
            CreateSensors();
            button12.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;
            LimitSensorVisiblity();
            button13.Enabled = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Enabled = false;
            SensorCustomDisplay();
            button14.Enabled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.Enabled = false;
            AccessConstraintsSunElevationAngle();
            button15.Enabled = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button15.Enabled = false;
            AccessConstraintsRange();
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (root != null)
            {
                root.CloseScenario();
            }
        }

	}
}
