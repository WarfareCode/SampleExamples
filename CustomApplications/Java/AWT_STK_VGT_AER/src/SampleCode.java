//Java
import java.awt.*;
//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;

public class SampleCode
{
	private final static String				s_DisplacementVectorName	= "Range";
	private final static String				s_AzimuthAngleName			= "Azimuth";
	private final static String				s_ElevationAngleName		= "Elevation";

	private AgStkObjectRootClass			m_AgStkObjectRootClass;
	private AgScenarioClass					m_AgScenarioClass;
	private IAgStkGraphicsSceneManager		m_IAgStkGraphicsSceneManager;
	private IAgStkGraphicsScene				m_IAgStkGraphicsScene;

	private IAgCrdnProvider					m_AircraftProvider;
	private IAgCrdnProvider					m_FacilityProvider;

	private AgCrdnVectorDisplacementClass	m_DisplacementVector;
	private AgCrdnAngleDihedralClass		m_AzimuthAngle;
	private AgCrdnAngleToPlaneClass			m_ElevationAngle;

	private Double							m_StopTime;

	/* package */SampleCode(AgStkObjectRootClass root)
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass = root;
	}

	/* package */void createScenario()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.newScenario("Scenario");
		this.m_AgScenarioClass = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
		this.m_IAgStkGraphicsSceneManager = this.m_AgScenarioClass.getSceneManager();
		this.m_IAgStkGraphicsScene = this.m_IAgStkGraphicsSceneManager.getScenes().getItem(0);

		this.setUnitPreferences();

		this.m_AgStkObjectRootClass.beginUpdate();

		// Create an aircraft and a facility
		AgAircraftClass aircraft = this.createAircraft();
		AgFacilityClass facility = this.createFacility();

		// Create the required VGT vectors and angles
		this.m_AircraftProvider = aircraft.getVgt();
		this.m_FacilityProvider = facility.getVgt();

		// Groups
		IAgCrdnPointGroup acPoints = this.m_AircraftProvider.getPoints();
		IAgCrdnPointGroup facPoints = this.m_FacilityProvider.getPoints();
		IAgCrdnVectorGroup facVectors = this.m_FacilityProvider.getVectors();
		IAgCrdnAngleGroup facAngles = this.m_FacilityProvider.getAngles();
		IAgCrdnPlaneGroup facPlanes = this.m_FacilityProvider.getPlanes();

		// Factories
		IAgCrdnVectorFactory facVecFactory = facVectors.getFactory();
		IAgCrdnAngleFactory facAngleFactory = facAngles.getFactory();

		// Displacement
		IAgCrdnPoint acCenterPoint = acPoints.getItem("Center");
		IAgCrdnPoint facCenterPoint = facPoints.getItem("Center");
		this.m_DisplacementVector = (AgCrdnVectorDisplacementClass)facVecFactory.createDisplacementVector(s_DisplacementVectorName, facCenterPoint, acCenterPoint);

		// Azimuth
		IAgCrdnVector bodyXVector = facVectors.getItem("Body.X");
		IAgCrdnVector bodyZVector = facVectors.getItem("Body.Z");
		this.m_AzimuthAngle = (AgCrdnAngleDihedralClass)facAngleFactory.create(SampleCode.s_AzimuthAngleName, "", AgECrdnAngleType.E_CRDN_ANGLE_TYPE_DIHEDRAL_ANGLE);
		((IAgCrdnAngleDihedral)this.m_AzimuthAngle).getFromVector().setVector(bodyXVector);
		((IAgCrdnAngleDihedral)this.m_AzimuthAngle).getToVector().setVector(this.m_DisplacementVector);
		((IAgCrdnAngleDihedral)this.m_AzimuthAngle).getPoleAbout().setVector(bodyZVector);

		// Elevation
		IAgCrdnPlane bodyXYPlane = facPlanes.getItem("BodyXY");
		this.m_ElevationAngle = (AgCrdnAngleToPlaneClass)facAngleFactory.create(SampleCode.s_ElevationAngleName, "", AgECrdnAngleType.E_CRDN_ANGLE_TYPE_TO_PLANE);
		((IAgCrdnAngleToPlane)this.m_ElevationAngle).getReferencePlane().setPlane(bodyXYPlane);
		((IAgCrdnAngleToPlane)this.m_ElevationAngle).getReferenceVector().setVector(this.m_DisplacementVector);

		IAgVOVector facVoVector = facility.getVO().getVector();
		this.displayVGTVectors(facVoVector);

		this.m_AgStkObjectRootClass.executeCommand("SetAnimation * EndMode Loop");

		this.m_AgStkObjectRootClass.endUpdate();
	}

	private void setUnitPreferences()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DateFormat", "epSec");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("TimeUnit", "sec");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DistanceUnit", "m");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("AngleUnit", "deg");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("LongitudeUnit", "deg");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("LatitudeUnit", "deg");
	}

	private AgAircraftClass createAircraft()
	throws AgCoreException
	{
		final double constantVelocity = 20;

		IAgStkObjectCollection children = null;
		children = this.m_AgScenarioClass.getChildren();

		AgAircraftClass aircraft = null;
		aircraft = (AgAircraftClass)children._new(AgESTKObjectType.E_AIRCRAFT, "Aircraft");

		IAgAcVO aircraftVO = aircraft.getVO();

		IAgVeRouteVOModel voModel = aircraftVO.getModel();
		voModel.setScaleValue(1.1);

		IAgVOMarker voMarker = voModel.getRouteMarker();
		voMarker.setVisible(false);

		IAgAcGraphics gfx = aircraft.getGraphics();
		gfx.setUseInstNameLabel(false);
		gfx.setLabelName("");

		IAgVeVORoute voRoute = aircraftVO.getRoute();
		IAgVeVOLeadTrailData trackData = voRoute.getTrackData();
		trackData.setLeadDataType(AgELeadTrailData.E_DATA_NONE);
		trackData.setTrailSameAsLead();

		// Construct the waypoint propagator for the aircraft
		IAgVePropagatorGreatArc propagator = (IAgVePropagatorGreatArc)aircraft.getRoute();
		propagator.setMethod(AgEVeWayPtCompMethod.E_DETERMINE_TIME_ACC_FROM_VEL);
		IAgCrdnEventIntervalSmartInterval interval = null;
		interval = propagator.getEphemerisInterval();
		interval.setExplicitInterval(this.m_AgScenarioClass.getStartTime(), interval.findStopTime());

		IAgVeWaypointsCollection waypoints = propagator.getWaypoints();

		// Create the start point of our route with a particular date, location, and velocity.
		IAgVeWaypointsElement waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.60));
		waypoint.setLongitude(new Double(-77.20));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		// Create the next few waypoints from a location, the same velocity, and the previous waypoint.
		waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.60));
		waypoint.setLongitude(new Double(-77.21));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.61));
		waypoint.setLongitude(new Double(-77.22));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.62));
		waypoint.setLongitude(new Double(-77.22));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.63));
		waypoint.setLongitude(new Double(-77.21));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.63));
		waypoint.setLongitude(new Double(-77.20));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.62));
		waypoint.setLongitude(new Double(-77.19));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.61));
		waypoint.setLongitude(new Double(-77.19));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		waypoint = waypoints.add();
		waypoint.setLatitude(new Double(39.60));
		waypoint.setLongitude(new Double(-77.20));
		waypoint.setAltitude(3000.0);
		waypoint.setSpeed(constantVelocity);

		propagator.propagate();

		// Set the StopTime so that the scenario can loop
		this.m_StopTime = (Double)interval.findStopTime_AsObject();

		return aircraft;
	}

	private AgFacilityClass createFacility()
	throws AgCoreException
	{
		IAgStkObjectCollection children = null;
		children = this.m_AgScenarioClass.getChildren();

		AgFacilityClass facility = null;
		facility = (AgFacilityClass)children._new(AgESTKObjectType.E_FACILITY, "Facility");

		IAgFaVO facilityVO = facility.getVO();

		IAgPtTargetVOModel voModel = facilityVO.getModel();
		voModel.setScaleValue(1.1);

		IAgVOMarker voMarker = voModel.getMarker();
		voMarker.setVisible(false);

		IAgFaGraphics gfx = facility.getGraphics();
		gfx.setUseInstNameLabel(false);

		// Position the facility at the center of the aircraft's route.
		facility.getPosition().assignPlanetodetic(new Double(39.615), new Double(-77.205), 0);

		return facility;
	}

	/* package */void turnOffDefaultAnnotations()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.executeCommand("VO * Annotation Time Show Off ShowTimeStep Off");
		this.m_AgStkObjectRootClass.executeCommand("VO * Annotation Frame Show Off");
	}

	private void displayVGTVectors(IAgVOVector vectorSettings)
	throws AgCoreException
	{
		IAgVORefCrdnCollection refCrdns = vectorSettings.getRefCrdns();

		IAgVORefCrdn voElement = null;

		String dispVectorPath = this.m_DisplacementVector.getQualifiedPath();
		voElement = refCrdns.add(AgEGeometricElemType.E_VECTOR_ELEM, dispVectorPath);
		IAgVORefCrdnVector voVector = (IAgVORefCrdnVector)voElement;
		voVector.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));
		voVector.setMagnitudeVisible(true);
		voVector.setMagnitudeUnitAbrv("m");
		voVector.setLabelVisible(true);

		String azAnglePath = this.m_AzimuthAngle.getQualifiedPath();
		voElement = refCrdns.add(AgEGeometricElemType.E_ANGLE_ELEM, azAnglePath);
		IAgVORefCrdnAngle voAngle = (IAgVORefCrdnAngle)voElement;
		voAngle.setColor(AgCoreColor.LIMEGREEN);
		voAngle.setAngleValueVisible(true);
		voAngle.setAngleUnitAbrv("deg");
		voAngle.setLabelVisible(true);

		String elevAnglePath = this.m_ElevationAngle.getQualifiedPath();
		voElement = refCrdns.add(AgEGeometricElemType.E_ANGLE_ELEM, elevAnglePath);
		voAngle = (IAgVORefCrdnAngle)voElement;
		voAngle.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
		voAngle.setAngleValueVisible(true);
		voAngle.setAngleUnitAbrv("deg");
		voAngle.setLabelVisible(true);
	}

	/* package */void viewScene(AgGlobeCntrlClass globe)
	throws AgCoreException
	{
		// Set-up the animation for this specific example
		IAgScAnimation anim = this.m_AgScenarioClass.getAnimation();

		anim.setEnableAnimCycleTime(true);
		anim.setAnimCycleType(AgEScEndLoopType.E_LOOP_AT_TIME);
		anim.setAnimCycleTime(this.m_StopTime);
		anim.setAnimStepValue(0.5);

		this.m_AgStkObjectRootClass.playForward();

		IAgCrdnAxesGroup axes = this.m_FacilityProvider.getAxes();
		IAgCrdnAxes axis = axes.getItem("NorthWestUp");

		IAgCrdnPointGroup points = this.m_FacilityProvider.getPoints();
		IAgCrdnPoint point = points.getItem("Center");

		IAgStkGraphicsCamera camera = this.m_IAgStkGraphicsScene.getCamera();
		
		Object[] offset = new Object[] { new Double(2000), new Double(2000), new Double(9000) };
		camera.viewOffset(axis, point, offset);

		this.m_IAgStkGraphicsScene.render();
	}

	public double[] getAER(Double timeEpSec)
	throws AgCoreException
	{
		double azimuth = 0.0;
		double elevation = 0.0;
		double range = 0.0;

		if(this.m_FacilityProvider != null)
		{
			IAgCrdnAxesGroup axes = this.m_FacilityProvider.getAxes();
			IAgCrdnAxes axis = axes.getItem("Body");

			IAgCrdnAngleFindAngleResult azimuthResult = this.m_AzimuthAngle.findAngle(timeEpSec);
			IAgCrdnAngleFindAngleResult elevationResult = this.m_ElevationAngle.findAngle(timeEpSec);
			IAgCrdnVectorFindInAxesResult rangeResult = this.m_DisplacementVector.findInAxes(timeEpSec, axis);

			azimuth = this.convertRadiansToDegrees(azimuthResult.getAngle());
			elevation = this.convertRadiansToDegrees(elevationResult.getAngle());
			range = this.getVectorMagnitude(rangeResult.getVector());
		}

		double[] aer = new double[3];
		aer[0] = azimuth;
		aer[1] = elevation;
		aer[2] = range;

		return aer;
	}

	private double getVectorMagnitude(IAgCartesian3Vector vector)
	throws AgCoreException
	{
		double x = vector.getX();
		double y = vector.getY();
		double z = vector.getZ();

		double x2 = Math.pow(x, 2);
		double y2 = Math.pow(y, 2);
		double z2 = Math.pow(z, 2);

		double value = x2 + y2 + z2;
		return Math.sqrt(value);
	}

	private double convertRadiansToDegrees(AgVariant radians)
	throws AgCoreException
	{
		return radians.getDouble() * (180.0 / Math.PI);
	}
}