//AGI Java API
import agi.core.*;
import agi.stkobjects.*;
import agi.stkobjects.astrogator.*;

/* package */class SampleCode
{
	private AgStkObjectRootClass		m_AgStkObjectRootClass;

	private IAgVADriverMCS				m_AgVADriverMCS;

	IAgSatellite						m_LunarProbe;
	IAgVAMCSLaunch						m_MCSLaunch;
	IAgVAMCSPropagate					m_MCSPropagateCoast;
	IAgVAMCSPropagate					m_MCSPropagateToPersilene;
	IAgVAMCSTargetSequence				m_MCSPropagateTargetSequence;
	IAgVAMCSManeuver					m_MCSManeuverTransLunarInjection;
	IAgVAProfileDifferentialCorrector	m_ProfileDifferentialCorrector;

	/* package */SampleCode(AgStkObjectRootClass root)
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass = root;
	}

	/* package */void createScenario()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.closeScenario();
		this.m_AgStkObjectRootClass.newScenario("MoonMissionWithBPlaneTargeting");

		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DistanceUnit", "km");

		String startTime = "1 Jan 1993 00:00:00.000";
		String stopTime = "1 Jan 1994 00:00:00.000";

		IAgScenario scen = (IAgScenario)this.m_AgStkObjectRootClass.getCurrentScenario();

		scen.setStartTime(startTime);
		scen.setStopTime(stopTime);

		IAgScAnimation anim = scen.getAnimation();
		anim.setStartTime(startTime);
		anim.setEnableAnimCycleTime(true);
		anim.setAnimCycleType(AgEScEndLoopType.E_END_TIME);
		anim.setAnimCycleTime(stopTime);

		this.m_AgStkObjectRootClass.rewind();
	}

	/* package */void createPlanets()
	throws AgCoreException
	{
		AgScenarioClass scen = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
		IAgStkObjectCollection children = scen.getChildren();

		IAgPlanet sun = (IAgPlanet)children._new(AgESTKObjectType.E_PLANET, "Sun");
		sun.setPositionSource(AgEPlPositionSourceType.E_POS_CENTRAL_BODY);
		IAgPlPosCentralBody suncb = (IAgPlPosCentralBody)sun.getPositionSourceData();
		suncb.setCentralBody("Sun");

		IAgPlanet moon = (IAgPlanet)children._new(AgESTKObjectType.E_PLANET, "Moon");
		moon.setPositionSource(AgEPlPositionSourceType.E_POS_CENTRAL_BODY);
		IAgPlPosCentralBody mooncb = (IAgPlPosCentralBody)moon.getPositionSourceData();
		mooncb.setCentralBody("Moon");

		IAgPlanet earth = (IAgPlanet)children._new(AgESTKObjectType.E_PLANET, "Earth");
		earth.setPositionSource(AgEPlPositionSourceType.E_POS_CENTRAL_BODY);
		IAgPlPosCentralBody earthcb = (IAgPlPosCentralBody)earth.getPositionSourceData();
		earthcb.setCentralBody("Earth");
	}

	/* package */void setSpacecraftGraphics()
	throws AgCoreException
	{
		AgScenarioClass scen = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
		IAgStkObjectCollection children = scen.getChildren();

		this.m_LunarProbe = (IAgSatellite)children._new(AgESTKObjectType.E_SATELLITE, "LunarProbe");
		this.m_LunarProbe.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_ASTROGATOR);
		this.m_AgVADriverMCS = (IAgVADriverMCS)this.m_LunarProbe.getPropagator();

		this.m_AgVADriverMCS.getOptions().setDrawTrajectoryIn2D(true);
		this.m_AgVADriverMCS.getOptions().setDrawTrajectoryIn3D(true);
		this.m_AgVADriverMCS.getOptions().setUpdateAnimationTimeForAllObjects(true);
		this.m_LunarProbe.getGraphics().setAttributesType(AgEVeGfxAttributes.E_ATTRIBUTES_BASIC);

		this.m_LunarProbe.getGraphics().getPassData().getGroundTrack().setLeadDataType(AgELeadTrailData.E_DATA_NONE);
		this.m_LunarProbe.getGraphics().getPassData().getOrbit().setLeadDataType(AgELeadTrailData.E_DATA_ALL);
		this.m_LunarProbe.getVO().getPass().getTrackData().setInheritFrom2D(true);
		this.m_LunarProbe.getVO().getModel().getOrbitMarker().setMarkerType(AgEMarkerType.E_SHAPE);

		IAgVOMarker marker = this.m_LunarProbe.getVO().getModel().getOrbitMarker();
		IAgVOMarkerData markerData = marker.getMarkerData();
		
		IAgVOMarkerShape markerShape = (IAgVOMarkerShape)markerData;
		markerShape.setStyle(AgE3dMarkerShape.E3D_SHAPE_POINT);

		this.m_LunarProbe.getVO().getModel().getOrbitMarker().setPixelSize(7);
		this.m_LunarProbe.getVO().getModel().getDetailThreshold().setMarkerLabel(1000000000000.0);
		this.m_LunarProbe.getVO().getModel().getDetailThreshold().setMarker(1000000000000.0);
		this.m_LunarProbe.getVO().getModel().getDetailThreshold().setPoint(1000000000000.0);
	}

	/* package */void set2DGraphics()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.executeCommand("MapDetails * Background Image None");
		this.m_AgStkObjectRootClass.executeCommand("MapDetails * Map RWDB2_Coastlines State Off");
		this.m_AgStkObjectRootClass.executeCommand("MapDetails * Map RWDB2_International_Borders State Off");
		this.m_AgStkObjectRootClass.executeCommand("MapDetails * Map RWDB2_Islands State Off");
		this.m_AgStkObjectRootClass.executeCommand("MapDetails * LatLon Lat Off");
		this.m_AgStkObjectRootClass.executeCommand("MapDetails * LatLon Lon Off");
		this.m_AgStkObjectRootClass.executeCommand("MapProjection * Orthographic Center 89 -90 Format BBR 900000 Sun");
	}

	/* package */void set3DGraphicsWindowEarthCentered()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.executeCommand("VO * Celestial Moon Label Off WindowID 2");
		this.m_AgStkObjectRootClass.executeCommand("VO * Grids Space ShowECI On ShowRadial On WindowID 2");
		this.m_AgStkObjectRootClass.executeCommand("Window3D * ViewVolume MaxVisibleDist 1000000000000 WindowID 2");
	}

	/* package */void set3DGraphicsWindowMoonCentered()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.executeCommand("VO * CentralBody Moon 1");
		this.m_AgStkObjectRootClass.executeCommand("VO * Celestial Moon Label Off WindowID 1");
		this.m_AgStkObjectRootClass.executeCommand("VO * Grids Space ShowECI On ShowRadial On WindowID 1");
		this.m_AgStkObjectRootClass.executeCommand("Window3D * ViewVolume MaxVisibleDist 1000000000000 WindowID 1");
	}

	/* package */void setupMCS()
	throws AgCoreException
	{
		this.m_AgVADriverMCS.getMainSequence().removeAll();
		this.m_MCSPropagateTargetSequence = (IAgVAMCSTargetSequence)this.m_AgVADriverMCS.getMainSequence().insert(AgEVASegmentType.E_VASEGMENT_TYPE_TARGET_SEQUENCE, "Target Sequence", "-");
		this.m_MCSLaunch = (IAgVAMCSLaunch)this.m_MCSPropagateTargetSequence.getSegments().insert(AgEVASegmentType.E_VASEGMENT_TYPE_LAUNCH, "Launch", "-");
		this.m_MCSLaunch.setEpoch("1 Jan 1993 00:00:00.00");

		this.m_MCSPropagateCoast = (IAgVAMCSPropagate)this.m_MCSPropagateTargetSequence.getSegments().insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Coast", "-");
		((IAgVAStoppingCondition)this.m_MCSPropagateCoast.getStoppingConditions().getItem(new Integer(0)).getProperties()).setTrip(new Integer(2700));

		this.m_MCSManeuverTransLunarInjection = (IAgVAMCSManeuver)this.m_MCSPropagateTargetSequence.getSegments().insert(AgEVASegmentType.E_VASEGMENT_TYPE_MANEUVER, "TransLunarInjection", "-");
		this.m_MCSManeuverTransLunarInjection.setManeuverType(AgEVAManeuverType.E_VAMANEUVER_TYPE_IMPULSIVE);
		IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)this.m_MCSManeuverTransLunarInjection.getManeuver();
		impulsive.setAttitudeControlType(AgEVAAttitudeControl.E_VAATTITUDE_CONTROL_THRUST_VECTOR);
		
		IAgVAMCSPropagate toSwingBy = (IAgVAMCSPropagate)this.m_MCSPropagateTargetSequence.getSegments().insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "ToSwingBy", "-");
		toSwingBy.setPropagatorName("CisLunar");
		((IAgVAStoppingCondition)toSwingBy.getStoppingConditions().add("R Magnitude").getProperties()).setTrip(new Integer(300000));
		toSwingBy.getStoppingConditions().remove("Duration");

		this.m_MCSPropagateToPersilene = (IAgVAMCSPropagate)this.m_MCSPropagateTargetSequence.getSegments().insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "ToPersilene", "-");
		this.m_MCSPropagateToPersilene.setPropagatorName("CisLunar");
		((IAgVAStoppingCondition)this.m_MCSPropagateToPersilene.getStoppingConditions().getItem(new Integer(0)).getProperties()).setTrip(new Integer(864000));
		IAgVAStoppingCondition alt = (IAgVAStoppingCondition)this.m_MCSPropagateToPersilene.getStoppingConditions().add("Altitude").getProperties();
		alt.setTrip(new Integer(0));
		alt.setCentralBodyName("Moon");

		IAgVAStoppingCondition periapsis = (IAgVAStoppingCondition)this.m_MCSPropagateToPersilene.getStoppingConditions().add("Periapsis").getProperties();
		periapsis.setCentralBodyName("Moon");
	}

	/* package */void transLunnarInjectionFirstGuess()
	throws AgCoreException
	{
		IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)this.m_MCSManeuverTransLunarInjection.getManeuver();
		IAgVAAttitudeControlImpulsiveThrustVector thrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.getAttitudeControl();
		thrustVector.getDeltaVVector().assignCartesian(3.15, 0, 0);
		this.m_AgVADriverMCS.runMCS();
	}

	/* package */void setupTheTargeterToCalculateLaunchAndCoastTimes()
	throws AgCoreException
	{
		this.m_MCSLaunch.enableControlParameter(AgEVAControlLaunch.E_VACONTROL_LAUNCH_EPOCH);
		this.m_MCSPropagateCoast.getStoppingConditions().getItem(new Integer(0)).enableControlParameter(AgEVAControlStoppingCondition.E_VACONTROL_STOPPING_CONDITION_TRIP_VALUE);

		((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getResults().add("MultiBody/Delta Right Asc");
		((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getResults().add("MultiBody/Delta Declination");

		IAgVAProfileDifferentialCorrector diffCorrector = (IAgVAProfileDifferentialCorrector)this.m_MCSPropagateTargetSequence.getProfiles().getItem(new Integer(0));
		diffCorrector.setName("Delta RA and Dec");
		diffCorrector.getControlParameters().getControlByPaths("Launch", "Launch.Epoch").setPerturbation(new Integer(60));
		diffCorrector.getControlParameters().getControlByPaths("Launch", "Launch.Epoch").setMaxStep(new Integer(3600));
		diffCorrector.getControlParameters().getControlByPaths("Launch", "Launch.Epoch").setEnable(true);
		diffCorrector.getControlParameters().getControlByPaths("Coast", "StoppingConditions.Duration.TripValue").setPerturbation(new Integer(60));
		diffCorrector.getControlParameters().getControlByPaths("Coast", "StoppingConditions.Duration.TripValue").setMaxStep(new Integer(300));
		diffCorrector.getControlParameters().getControlByPaths("Coast", "StoppingConditions.Duration.TripValue").setEnable(true);

		diffCorrector.getResults().getItem(0).setDesiredValue(new Integer(0));
		diffCorrector.getResults().getItem(0).setEnable(true);

		diffCorrector.getResults().getItem(1).setDesiredValue(new Integer(0));
		diffCorrector.getResults().getItem(1).setEnable(true);
	}

	/* package */void runTheTargeter()
	throws AgCoreException
	{
		this.m_MCSPropagateTargetSequence.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES_ONCE);
		this.m_AgVADriverMCS.runMCS();
		this.m_MCSPropagateTargetSequence.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES);
		this.m_AgVADriverMCS.runMCS();

		this.m_MCSPropagateTargetSequence.applyProfiles();
		this.m_MCSPropagateTargetSequence.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_NOMINAL_SEQ);
		this.m_AgVADriverMCS.runMCS();
	}

	/* package */void settingUpTheTargeterToTargetOnTheBPlane()
	throws AgCoreException
	{
		this.m_ProfileDifferentialCorrector = (IAgVAProfileDifferentialCorrector)this.m_MCSPropagateTargetSequence.getProfiles().getItem(new Integer(0)).copy();
		this.m_ProfileDifferentialCorrector.setName("B_Plane_Targeting");
		this.m_MCSPropagateTargetSequence.getProfiles().getItem(new Integer(0)).setMode(AgEVAProfileMode.E_VAPROFILE_MODE_NOT_ACTIVE);

		((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getResults().add("Epoch");
		((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getResults().add("MultiBody/BDotT");
		((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getResults().add("MultiBody/BDotR");

		this.m_MCSManeuverTransLunarInjection.enableControlParameter(AgEVAControlManeuver.E_VACONTROL_MANEUVER_IMPULSIVE_CARTESIAN_X);

		this.m_ProfileDifferentialCorrector.getControlParameters().getControlByPaths("TransLunarInjection", "ImpulsiveMnvr.Cartesian.X").setEnable(true);

		this.m_ProfileDifferentialCorrector.getResults().getResultByPaths("ToPersilene", "Delta Declination").setEnable(false);
		this.m_ProfileDifferentialCorrector.getResults().getResultByPaths("ToPersilene", "Delta Right Asc").setEnable(false);

		this.m_ProfileDifferentialCorrector.getResults().getResultByPaths("ToPersilene", "BDotR").setEnable(true);
		this.m_ProfileDifferentialCorrector.getResults().getResultByPaths("ToPersilene", "BDotT").setEnable(true);
		this.m_ProfileDifferentialCorrector.getResults().getResultByPaths("ToPersilene", "Epoch").setEnable(true);
		this.m_ProfileDifferentialCorrector.getResults().getResultByPaths("ToPersilene", "BDotR").setDesiredValue(new Integer(5000));
		this.m_ProfileDifferentialCorrector.getResults().getResultByPaths("ToPersilene", "BDotT").setDesiredValue(new Integer(0));
		this.m_ProfileDifferentialCorrector.getResults().getResultByPaths("ToPersilene", "Epoch").setDesiredValue("4 Jan 1993 00:00:00.00");
	}

	/* package */void drawingTheBPlane()
	throws AgCoreException
	{
		IAgVeVOBPlaneTemplate template = this.m_LunarProbe.getVO().getBPlanes().getTemplates().add();
		template.setName("Lunar_B-Plane");
		template.setCentralBody("Moon");
		template.setReferenceVector("CentralBody/Moon Orbit_Normal Vector");
		IAgVeVOBPlaneInstance bPlane = this.m_LunarProbe.getVO().getBPlanes().getInstances().add("Lunar_B-Plane");
		bPlane.setName("LunarBPlane");

		((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getProperties().getBPlanes().add("LunarBPlane");
		((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getProperties().applyFinalStateToBPlanes();
	}

	/* package */void runningTheTargeterToAchieveBPlaneParams()
	throws AgCoreException
	{
		this.m_MCSPropagateTargetSequence.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES_ONCE);
		this.m_AgVADriverMCS.runMCS();

		this.m_MCSPropagateTargetSequence.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES);
		this.m_AgVADriverMCS.runMCS();

		this.m_MCSPropagateTargetSequence.applyProfiles();
		this.m_MCSPropagateTargetSequence.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_NOMINAL_SEQ);
		this.m_AgVADriverMCS.runMCS();
	}

	/* package */void targetingAltitudeAndInclination()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.executeCommand("VectorTool * Moon Create Axes True_Moon_Equator \"Aligned and Constrained\" Cartesian 0 0 1 \"CentralBody/Moon Angular_Velocity\"  Cartesian 1 0 0 \"CentralBody/Moon VernalEquinox\"");
		this.m_AgStkObjectRootClass.executeCommand("VectorTool * Moon Create System True_Lunar_Equatorial \"Assembled\" \"CentralBody/Moon Center\" \"CentralBody/Moon True_Moon_Equator\"");

		IAgVAProfileDifferentialCorrector altInc = (IAgVAProfileDifferentialCorrector)this.m_ProfileDifferentialCorrector.copy();
		altInc.setName("Altitude and Inclination");

		this.m_MCSPropagateTargetSequence.getProfiles().getItem(new AgVariant(0)).setMode(AgEVAProfileMode.E_VAPROFILE_MODE_NOT_ACTIVE);
		this.m_ProfileDifferentialCorrector.setMode(AgEVAProfileMode.E_VAPROFILE_MODE_NOT_ACTIVE);

		IAgVAStateCalcGeodeticElem calcAlt = (IAgVAStateCalcGeodeticElem)((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getResults().add("Geodetic/Altitude");
		calcAlt.setCentralBodyName("Moon");
		IAgVAStateCalcInclination calcInc = (IAgVAStateCalcInclination)((IAgVAMCSSegment)this.m_MCSPropagateToPersilene).getResults().add("Keplerian Elems/Inclination");
		calcInc.setCoordSystemName("CentralBody/Moon True_Lunar_Equatorial");

		for(int count = 0; count < altInc.getResults().getCount(); count++)
		{
			altInc.getResults().getItem(count).setEnable(false);
		}
		altInc.getResults().getResultByPaths("ToPersilene", "Altitude").setEnable(true);
		altInc.getResults().getResultByPaths("ToPersilene", "Epoch").setEnable(true);
		altInc.getResults().getResultByPaths("ToPersilene", "Inclination").setEnable(true);

		altInc.getResults().getResultByPaths("ToPersilene", "Altitude").setDesiredValue(new Integer(100));
		altInc.getResults().getResultByPaths("ToPersilene", "Inclination").setDesiredValue(new Integer(90));
		altInc.getResults().getResultByPaths("ToPersilene", "Epoch").setDesiredValue("4 Jan 1993 00:00:00.00");

		this.m_MCSPropagateTargetSequence.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES);
		this.m_AgVADriverMCS.runMCS();
		this.m_MCSPropagateTargetSequence.applyProfiles();
		this.m_MCSPropagateTargetSequence.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_NOMINAL_SEQ);
		this.m_AgVADriverMCS.runMCS();
	}

	/* package */void approachingTheMoon()
	throws AgCoreException
	{
		IAgVAMCSPropagate prop3Day = (IAgVAMCSPropagate)this.m_AgVADriverMCS.getMainSequence().insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Prop3Days", "-");
		prop3Day.setPropagatorName("CisLunar");
		((IAgVAStoppingCondition)prop3Day.getStoppingConditions().getItem(new Integer(0)).getProperties()).setTrip(new Integer(259200));

		this.m_AgVADriverMCS.runMCS();
	}

	/* package */void lunarOrbitInsertion()
	throws AgCoreException
	{
		IAgVAMCSTargetSequence ts2 = (IAgVAMCSTargetSequence)this.m_AgVADriverMCS.getMainSequence().insert(AgEVASegmentType.E_VASEGMENT_TYPE_TARGET_SEQUENCE, "Target Sequence2", "Prop3Days");
		IAgVAMCSManeuver loi = (IAgVAMCSManeuver)ts2.getSegments().insert(AgEVASegmentType.E_VASEGMENT_TYPE_MANEUVER, "LOI", "-");
		loi.setManeuverType(AgEVAManeuverType.E_VAMANEUVER_TYPE_IMPULSIVE);
		IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)loi.getManeuver();
		impulsive.setAttitudeControlType(AgEVAAttitudeControl.E_VAATTITUDE_CONTROL_THRUST_VECTOR);
		IAgVAAttitudeControlImpulsiveThrustVector thrust = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.getAttitudeControl();
		thrust.setThrustAxesName("Satellite VNC(Moon)");
		loi.enableControlParameter(AgEVAControlManeuver.E_VACONTROL_MANEUVER_IMPULSIVE_CARTESIAN_X);
		IAgVAStateCalcEccentricity ecc = (IAgVAStateCalcEccentricity)((IAgVAMCSSegment)loi).getResults().add("Keplerian Elems/Eccentricity");
		ecc.setCentralBodyName("Moon");

		IAgVAProfileDifferentialCorrector diffCorrector = (IAgVAProfileDifferentialCorrector)ts2.getProfiles().getItem(new Integer(0));
		diffCorrector.getControlParameters().getItem(0).setEnable(true);
		diffCorrector.getResults().getItem(0).setEnable(true);

		ts2.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES);
		this.m_AgVADriverMCS.runMCS();
		ts2.applyProfiles();
		ts2.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_NOMINAL_SEQ);
		this.m_AgVADriverMCS.runMCS();
	}

	/* package */void closeScenario()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.closeScenario();
	}
}