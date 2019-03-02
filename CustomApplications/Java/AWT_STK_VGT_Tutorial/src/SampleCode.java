import java.awt.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkobjects.*;
import agi.stkvgt.*;

public class SampleCode
{
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgScenarioClass			m_AgScenarioClass;
	private IAgCrdnProvider			m_SatelliteProvider;
	private IAgCrdnProvider			m_FacilityProvider;
	private IAgVORefCrdnCollection	m_VORefCrdnCollection;

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
	}

	/* package */void createSatellite()
	throws AgCoreException
	{
		IAgStkObjectCollection children = null;
		children = this.m_AgScenarioClass.getChildren();

		AgSatelliteClass sat = null;
		sat = (AgSatelliteClass)children._new(AgESTKObjectType.E_SATELLITE, "Satellite1");

		sat.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_TWO_BODY);

		IAgVePropagatorTwoBody twoBody = null;
		twoBody = (IAgVePropagatorTwoBody)sat.getPropagator();

		twoBody.propagate();

		IAgSaVO vo = null;
		vo = sat.getVO();

		IAgVOVector vovector = null;
		vovector = vo.getVector();

		vovector.setVectorSizeScale(1.3);
		vovector.setAngleSizeScale(1);

		this.m_VORefCrdnCollection = vovector.getRefCrdns();

		this.m_SatelliteProvider = sat.getVgt();
	}

	/* package */void createFacility()
	throws AgCoreException
	{
		IAgStkObjectCollection children = null;
		children = this.m_AgScenarioClass.getChildren();

		AgFacilityClass fac = null;
		fac = (AgFacilityClass)children._new(AgESTKObjectType.E_FACILITY, "Facility1");

		IAgFaVO vo = null;
		vo = fac.getVO();

		IAgPtTargetVOModel mdl = null;
		mdl = vo.getModel();

		mdl.setVisible(true);

		IAgFaGraphics gfx = null;
		gfx = fac.getGraphics();

		gfx.setLabelVisible(true);

		this.m_FacilityProvider = fac.getVgt();
	}

	/* package */void viewPoint()
	throws Throwable
	{
		IAgStkObjectCollection children = null;
		children = this.m_AgScenarioClass.getChildren();

		AgSatelliteClass sat = null;
		sat = (AgSatelliteClass)children.getItem("Satellite1");

		// Create a viewpoint
		IAgCrdnPointGroup group = this.m_SatelliteProvider.getPoints();
		IAgCrdnPointFactory factory = group.getFactory();

		IAgCrdnPointFixedInSystem viewPoint = null;
		viewPoint = (IAgCrdnPointFixedInSystem)factory.create("ViewPoint", "View sat from this point", AgECrdnPointType.E_CRDN_POINT_TYPE_FIXED_IN_SYSTEM);

		IAgPosition fixedPoint = viewPoint.getFixedPoint();
		fixedPoint.assignCartesian(.08, -.1, -.020);

		// Zoom to Satellite
		String path = ((IAgCrdn)viewPoint).getQualifiedPath();

		StringBuffer sbzoomCmd = new StringBuffer();
		sbzoomCmd.append("VO * View FromTo FromRegName \"STK Object\" FromName \"");
		sbzoomCmd.append(path);
		sbzoomCmd.append("\" ToRegName \"STK Object\" ToName \"");
		sbzoomCmd.append(sat.getClassName());
		sbzoomCmd.append("/");
		sbzoomCmd.append(sat.getInstanceName());
		sbzoomCmd.append("\"");
		String zoomCmd = sbzoomCmd.toString();

		this.m_AgStkObjectRootClass.executeCommand(zoomCmd);
	}

	/* package */void displacementVector()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// Get the center of the satellite
			IAgCrdnPointGroup satPoints = this.m_SatelliteProvider.getPoints();
			IAgCrdnPoint satCenterPoint = satPoints.getItem("Center");

			// Get the center of the facility
			IAgCrdnPointGroup facPoints = this.m_FacilityProvider.getPoints();
			IAgCrdnPoint facCenterPoint = facPoints.getItem("Center");

			// Create displacement vector to show distance from satellite to facility
			String vecName = "DistanceVector";
			String vecDesc = "Vector to Facility";
			AgECrdnVectorType vecType = AgECrdnVectorType.E_CRDN_VECTOR_TYPE_DISPLACEMENT;
			
			IAgCrdnVectorGroup satVectors = this.m_SatelliteProvider.getVectors();
			IAgCrdnVectorFactory satVectorFactory = satVectors.getFactory();
			
			IAgCrdnVectorDisplacement vecDisp = null;
			vecDisp = (IAgCrdnVectorDisplacement)satVectorFactory.create(vecName, vecDesc, vecType);

			vecDisp.getOrigin().setPoint(satCenterPoint);
			vecDisp.getDestination().setPoint(facCenterPoint);
			vecDisp.setApparent(true);

			// Display 3d vector
			String path = ((IAgCrdn)vecDisp).getQualifiedPath();
			
			this.m_VORefCrdnCollection.add(AgEGeometricElemType.E_VECTOR_ELEM, path);
			
			IAgVORefCrdnVector voVectorToFac = null;
			voVectorToFac = (IAgVORefCrdnVector)this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_VECTOR_ELEM, path);
			voVectorToFac.setVisible(true);
			voVectorToFac.setMagnitudeVisible(true);
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
		}
	}

	/* package */void velocityVector()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			//Get reference to velocity vector
			IAgCrdnVectorGroup group = null;
			group = this.m_SatelliteProvider.getVectors();
			IAgCrdn crdn = (IAgCrdn)group.getItem("Velocity");
	
			//Check that the vector can be used
			if(!crdn.getIsReady()) throw new Exception("The vector is not fully configured");
			if(!crdn.getIsValid()) throw new Exception("The vector is not invalid.");
			
			// Display the velocity vector
			String path = crdn.getQualifiedPath();
			IAgVORefCrdnVector vecVelocity = null;
			vecVelocity = (IAgVORefCrdnVector)this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_VECTOR_ELEM, path);
			vecVelocity.setVisible(true);
			vecVelocity.setMagnitudeVisible(true);
			vecVelocity.setLabelVisible(true);
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
		}
	}

	/* package */void axes()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();
			
			//Get reference to VVLH axes
			IAgCrdnAxesGroup axesGroup = null;
			axesGroup = this.m_SatelliteProvider.getAxes();
			IAgCrdn axes = (IAgCrdn)axesGroup.getItem("VVLH(Earth)");

			//Check that the axes can be used
			if(!axes.getIsReady()) throw new Exception("The vector is not fully configured");
			if(!axes.getIsValid()) throw new Exception("The vector is not invalid.");

			//Add Axes from default collection
			String path = axes.getQualifiedPath();
			this.m_VORefCrdnCollection.add(AgEGeometricElemType.E_AXES_ELEM, path);

			// Hide the velocity vector label
			IAgCrdnVectorGroup vecGroup = null;
			vecGroup = this.m_SatelliteProvider.getVectors();
			IAgCrdn vector = (IAgCrdn)vecGroup.getItem("Velocity");
			String pathOld = vector.getQualifiedPath();
			IAgVORefCrdn refVector = null;
			refVector = this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_VECTOR_ELEM, pathOld);
			refVector.setVisible(false);
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
		}
	}

	/* package */void plane()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			IAgCrdnPlaneGroup planeGroup = null;
			planeGroup = this.m_SatelliteProvider.getPlanes();

			IAgCrdnVectorGroup vecGroup = null;
			vecGroup = this.m_SatelliteProvider.getVectors();

			IAgCrdnAxesGroup axesGroup = null;
			axesGroup = this.m_SatelliteProvider.getAxes();

			IAgCrdnPointGroup pointGroup = null;
			pointGroup = this.m_SatelliteProvider.getPoints();
			
			IAgCrdnPlaneFactory planeFactory = null;
			planeFactory = planeGroup.getFactory();

			//======================
			// Create a normal plane
			//======================
			String planeName = "NormalPlane";
			String planeDesc = "Normal plane to Velocity vector";
			AgECrdnPlaneType planeType = AgECrdnPlaneType.E_CRDN_PLANE_TYPE_NORMAL;
			
			IAgCrdnPlaneNormal normalPlane = null;
			normalPlane = (IAgCrdnPlaneNormal)planeFactory.create(planeName, planeDesc, planeType);

			//Set Normal Vector
			IAgCrdnVector velVector = null;
			velVector = vecGroup.getItem("Velocity");
			normalPlane.getNormalVector().setVector(velVector);

			//Set Reference Vector
			IAgCrdnVector earthVector = null;
			earthVector = vecGroup.getItem("Earth");
			normalPlane.getReferenceVector().setVector(earthVector);

			//Set Reference Point
			IAgCrdnPoint centerPoint = null;
			centerPoint = pointGroup.getItem("Center");
			normalPlane.getReferencePoint().setPoint(centerPoint);

			//Get Normal plane path and display in globe control
			IAgCrdn normalCrdn = (IAgCrdn)planeGroup.getItem("NormalPlane");
			String normalPath = normalCrdn.getQualifiedPath();
			this.m_VORefCrdnCollection.add(AgEGeometricElemType.E_PLANE_ELEM, normalPath);

			IAgVORefCrdn normalVoRef = null;
			normalVoRef = this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_PLANE_ELEM, normalPath);
			normalVoRef.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));

			IAgVORefCrdnPlane normalVoRefPlane = null;
			normalVoRefPlane = (IAgVORefCrdnPlane)normalVoRef;
			normalVoRefPlane.setTransparentPlaneVisible(true);
			normalVoRefPlane.setAxisLabelsVisible(true);

			// Get BodyXY plane path and display in globe control 
			IAgCrdn bodyXYCrdn = (IAgCrdn)planeGroup.getItem("BodyXY");
			String bodyXYPath = bodyXYCrdn.getQualifiedPath();

			IAgVORefCrdn bodyXYVoRef = null;
			bodyXYVoRef = this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_PLANE_ELEM, bodyXYPath);
			bodyXYVoRef.setVisible(true);
			bodyXYVoRef.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));

			IAgVORefCrdnPlane bodyXYVoRefPlane = null;
			bodyXYVoRefPlane = (IAgVORefCrdnPlane)bodyXYVoRef;
			bodyXYVoRefPlane.setTransparentPlaneVisible(true);
			bodyXYVoRefPlane.setAxisLabelsVisible(true);
			
			// Hide Axes Labels
			IAgCrdn vvlhEarthCrdn = (IAgCrdn)axesGroup.getItem("VVLH(Earth)");
			String vvlhEarthPath = vvlhEarthCrdn.getQualifiedPath();
			IAgVORefCrdn vvlhEarthVoRef = null;
			vvlhEarthVoRef = this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_AXES_ELEM, vvlhEarthPath);
			vvlhEarthVoRef.setLabelVisible(false);
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
		}
	}

	// create and display angles
	/* package */void angles()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			//Prep for Angles
			IAgCrdnAngleGroup angleGroup = null;
			angleGroup = this.m_SatelliteProvider.getAngles();
			IAgCrdnAngleFactory angleFactory = null;
			angleFactory = angleGroup.getFactory();

			//Prep for Planes
			IAgCrdnPlaneGroup planeGroup = null;
			planeGroup = this.m_SatelliteProvider.getPlanes();
			IAgCrdnPlane bodyXYPlane = planeGroup.getItem("BodyXY");
			IAgCrdnPlane normalPlane = planeGroup.getItem("NormalPlane");
			
			//Prep for vectors
			IAgCrdnVectorGroup vectorGroup = null;
			vectorGroup = this.m_SatelliteProvider.getVectors();
			IAgCrdnVector velVector = vectorGroup.getItem("Velocity");
			IAgCrdnVector distanceVector = vectorGroup.getItem("DistanceVector");
			
			//=====================================================
			// Angle from Normal Plane to XY Plane and display it
			//=====================================================
			String angleName = "AngleBetweenPlanes";
			String angleDesc = "Angle from PlaneXY to NormalPlane";
			AgCrdnAngleBetweenPlanesClass angleBetweenPlanes = null;
			angleBetweenPlanes = (AgCrdnAngleBetweenPlanesClass)angleFactory.create(angleName, angleDesc, AgECrdnAngleType.E_CRDN_ANGLE_TYPE_BETWEEN_PLANES);
			angleBetweenPlanes.getFromPlane().setPlane(bodyXYPlane);
			angleBetweenPlanes.getToPlane().setPlane(normalPlane);
			String angleBetweenPlanesPath = angleBetweenPlanes.getQualifiedPath();
			this.m_VORefCrdnCollection.add(AgEGeometricElemType.E_ANGLE_ELEM, angleBetweenPlanesPath);
			IAgVORefCrdnAngle voAnglePlane = null;
			voAnglePlane = (IAgVORefCrdnAngle)this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_ANGLE_ELEM, angleBetweenPlanesPath);
			voAnglePlane.setAngleValueVisible(true);

			//=====================================================
			// Angle from Velocity/Trajectory and display it
			//=====================================================
			String angleName2 = "AngleToVector";
			String angleDesc2 = "";
			AgCrdnAngleBetweenVectorsClass angleBetweenVector = null;
			angleBetweenVector = (AgCrdnAngleBetweenVectorsClass)angleFactory.create(angleName2, angleDesc2, AgECrdnAngleType.E_CRDN_ANGLE_TYPE_BETWEEN_VECTORS);
			angleBetweenVector.getFromVector().setVector(velVector);
			angleBetweenVector.getToVector().setVector(distanceVector);
			String angleBetweenVectorsPath = angleBetweenVector.getQualifiedPath();
			this.m_VORefCrdnCollection.add(AgEGeometricElemType.E_ANGLE_ELEM, angleBetweenVectorsPath);
			IAgVORefCrdnAngle voAngleVector = null;
			voAngleVector = (IAgVORefCrdnAngle)this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_ANGLE_ELEM, angleBetweenVectorsPath);
			voAngleVector.setAngleValueVisible(true);

			//==========================================================
			// Angle from AccessFacility vector to Plane and display it
			//==========================================================
			String angleName3 = "AngleFromFac";
			String angleDesc3 = "Angle from Plane to Vector";
			AgCrdnAngleToPlaneClass angleToPlane = null;
			angleToPlane = (AgCrdnAngleToPlaneClass)angleFactory.create(angleName3, angleDesc3, AgECrdnAngleType.E_CRDN_ANGLE_TYPE_TO_PLANE);
			angleToPlane.getReferenceVector().setVector(distanceVector);
			angleToPlane.getReferencePlane().setPlane(normalPlane);
			String angleToPlanePath = angleToPlane.getQualifiedPath();
			this.m_VORefCrdnCollection.add(AgEGeometricElemType.E_ANGLE_ELEM, angleToPlanePath);
			IAgVORefCrdnAngle voAnglePlaneFac = null;
			voAnglePlaneFac = (IAgVORefCrdnAngle)this.m_VORefCrdnCollection.getCrdnByName(AgEGeometricElemType.E_ANGLE_ELEM, angleToPlanePath);
			voAnglePlaneFac.setAngleValueVisible(true);
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
		}
	}

	/* package */void saveScenario()
	throws AgCoreException
	{
		String workingDirectory = System.getProperty("user.dir");
		String platFilePathSep = System.getProperty("file.separator");
		StringBuffer filePath = new StringBuffer();
		filePath.append(workingDirectory);
		filePath.append(platFilePathSep);
		filePath.append("Scenario");

		this.m_AgStkObjectRootClass.saveScenarioAs(filePath.toString());
	}

	/* package */void reset()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.closeScenario();
		this.m_AgScenarioClass = null;
	}
}