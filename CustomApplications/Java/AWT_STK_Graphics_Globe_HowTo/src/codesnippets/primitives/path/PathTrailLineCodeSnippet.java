package codesnippets.primitives.path;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class PathTrailLineCodeSnippet
extends STKGraphicsCodeSnippet 
implements IAgStkObjectRootEvents2
{
    private IAgStkGraphicsPathPrimitive m_Path;
    private IAgStkGraphicsModelPrimitive m_Model;
    private IAgCrdnProvider m_Provider;
    private IAgCrdnPoint m_Point;
    private IAgCrdnAxes m_Axes;
    private AgStkObjectRootClass m_Root;

    private double m_NewAngle;
    private double m_OldAngle;
    private boolean m_FirstRun = true;

    private double m_OldTime;
    private double m_StartTime;

    private boolean m_AnimateForward = true;
    private boolean m_AnimateDirectionChanged = false;

    private final String objectName = "PathTrailLineSatellite";

    public PathTrailLineCodeSnippet(Component c)
	{
		super(c, "Draw a trail line behind a satellite", "primitives", "path", "PathTrailLineCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        //#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        //  Create the model and its propagator and set the camera view
        IAgSatellite satellite = createSatellite(root);

        // Create the SGP4 Propogator from the TLE
        satellite.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_SGP4);
        IAgVePropagatorSGP4 propagator = (IAgVePropagatorSGP4)satellite.getPropagator();
       
        Object startTime = ((IAgScenario)root.getCurrentScenario()).getStartTime_AsObject();
        String filePath = DataPaths.getDataPaths().getSharedDataPath("Models/ISS.tle");
        propagator.getCommonTasks().addSegsFromFile("25544", filePath);
        IAgCrdnEventInterval intvl = root.getCurrentScenario().getVgt().getEventIntervals().getItem("AnalysisInterval");
        propagator.getEphemerisInterval().setImplicitInterval(intvl);
        propagator.propagate();
        double epoch = propagator.getSegments().getItem(0).getEpoch();

        // Get the Vector Geometry Tool provider for the satellite and find its initial position and orientation.
        IAgCrdnProvider provider = ((IAgStkObject)satellite).getVgt();

        IAgCrdnSystem crdnSys = root.getVgtRoot().getWellKnownSystems().getEarth().getInertial();
        IAgCrdnPointLocateInSystemResult positionResult = provider.getPoints().getItem("Center").locateInSystem(startTime, crdnSys);
        IAgCartesian3Vector position = positionResult.getPosition();

        IAgCrdnAxes crdnAxes = root.getVgtRoot().getWellKnownAxes().getEarth().getInertial();
        IAgCrdnAxesFindInAxesResult orientationResult = crdnAxes.findInAxes(startTime, provider.getAxes().getItem("Body"));
        IAgOrientation orientation = orientationResult.getOrientation();

        // Create the satellite model
        String filePath2 = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Space"+fileSep+"hs601.mdl");
        IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath2);
        ((IAgStkGraphicsPrimitive)model).setReferenceFrame(crdnSys);
        model.setPosition(new Object[] { new Double(position.getX()), new Double(position.getY()), new Double(position.getZ()) });
        model.setOrientation(orientation);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);

        // Create the path primitive
        IAgStkGraphicsPathPrimitive path = manager.getInitializers().getPathPrimitive().initializeDefault();
        ((IAgStkGraphicsPrimitive)path).setReferenceFrame(crdnSys);
        manager.getInitializers().getDurationPathPrimitiveUpdatePolicy().initializeWithParameters(
        60, AgEStkGraphicsPathPrimitiveRemoveLocation.E_STK_GRAPHICS_REMOVE_LOCATION_FRONT);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)path);

        // Set the time
        ((IAgScenario)root.getCurrentScenario()).getAnimation().setStartTime(new Double(epoch));
        //#endregion

        // Set the member variables
        m_Model = model;
        m_Path = path;
        m_Provider = provider;
        m_Point = provider.getPoints().getItem("Center");
        m_Axes = provider.getAxes().getItem("Body");
        m_Root = root;

        root.addIAgStkObjectRootEvents2(this);
	}

    private IAgSatellite createSatellite(AgStkObjectRootClass root)
    throws AgCoreException
    {
        IAgSatellite satellite;
        if (root.getCurrentScenario().getChildren().contains(AgESTKObjectType.E_SATELLITE, objectName))
            satellite = (IAgSatellite)root.getObjectFromPath("Satellite/" + objectName);
        else
            satellite = (IAgSatellite)root.getCurrentScenario().getChildren()._new(AgESTKObjectType.E_SATELLITE, objectName);
        satellite.getVO().getPass().getTrackData().getPassData().getOrbit().setLeadDataType(AgELeadTrailData.E_DATA_NONE);
        satellite.getVO().getPass().getTrackData().getPassData().getOrbit().setTrailSameAsLead();
        satellite.getGraphics().setUseInstNameLabel(false);
        satellite.getGraphics().setLabelName("");
        satellite.getVO().getModel().getOrbitMarker().setVisible(false);
        satellite.getVO().getModel().setVisible(false);

        return satellite;
    }

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgAnimation animationControl = (IAgAnimation)root;
        IAgScAnimation animationSettings = ((IAgScenario)root.getCurrentScenario()).getAnimation();

        // Set-up the animation for this specific example
        animationControl.pause();
        STKObjectsHelper.setAnimationDefaults(root);
        animationSettings.setAnimStepValue(1.0);
        animationSettings.setEnableAnimCycleTime(true);
        animationSettings.setAnimCycleTime(new Double(((Double)animationSettings.getStartTime_AsObject()).doubleValue() + 3600.0));
        animationSettings.setAnimCycleType(AgEScEndLoopType.E_LOOP_AT_TIME);
        animationControl.playForward();

        // Create the viewer point, which is an offset from near the model position looking
        // towards the model.  Set the camera to look from the viewer to the model.
        Object[] offset = new Object[] { new Double(50.0), new Double(50.0), new Double(-50.0) };
        scene.getCamera().viewOffset(m_Axes, m_Point, offset);
        scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_NEGATIVE_Z);
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        root.removeIAgStkObjectRootEvents2(this);

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if(m_Model != null)
        {
        	manager.getPrimitives().remove((IAgStkGraphicsPrimitive)m_Model);
            m_Model = null;
        }
        
        if(m_Path != null)
        {
        	manager.getPrimitives().remove((IAgStkGraphicsPrimitive)m_Path);
            m_Path = null;
        }
        
        root.getCurrentScenario().getChildren().unload(AgESTKObjectType.E_SATELLITE, objectName);

        if(m_Provider != null)
        {
        	m_Provider = null;
        }
        
        if(m_Point != null)
        {
        	m_Point = null;
        }
        
        if(m_Axes != null)
        {
        	m_Axes = null;
        }
        
        scene.getCamera().viewCentralBody("Earth", root.getVgtRoot().getWellKnownAxes().getEarth().getInertial());
        scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);

        ((IAgAnimation)root).pause();
        STKObjectsHelper.setAnimationDefaults(root);
        ((IAgAnimation)root).rewind();

        scene.render();
	}

	// #region CodeSnippet
	public void onAgStkObjectRootEvent(AgStkObjectRootEvent e)
	{
		try
		{
			int type = e.getType();
			AgStkObjectRootClass root = (AgStkObjectRootClass)e.getSource();

			if(type == AgStkObjectRootEvent.TYPE_ON_ANIM_UPDATE)
			{
				Object[] params = e.getParams();
				double timeEpSec = ((Double)params[0]).doubleValue();

	            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
	
	            // Get vector pointing from the satellite to the sun.
	            IAgCrdnVectorFindInAxesResult result = m_Provider.getVectors().getItem("Sun").findInAxes(new Double(timeEpSec), m_Axes);
	            double xNormalizedIn2D = result.getVector().getX() / (Math.sqrt(Math.pow(result.getVector().getX(), 2.0) + Math.pow(result.getVector().getZ(), 2.0)));
	            m_NewAngle = Math.acos(xNormalizedIn2D);
	
	            // Initialize tracking of angle changes.  To when the angle reaches 180 degrees and starts to
	            // fall back down to 0, the panel rotation must use a slightly different calculation.
	            if (m_FirstRun)
	            {
	                m_StartTime = timeEpSec;
	                m_OldTime = timeEpSec;
	                m_OldAngle = m_NewAngle;
	                m_FirstRun = false;
	            }
	
	            double TwoPI = Math.PI * 2;
	            double HalfPI = Math.PI * .5;
	
	            //
	            // Rotates the satellite panels. The panel rotation is reversed when the animation is reversed.
	            // Set boolean flag for update to path.
	            //
	            if (timeEpSec - m_StartTime >= m_OldTime - m_StartTime)
	            {
	                if (m_NewAngle < m_OldAngle)
	                {
	                    m_Model.getArticulations().getByName("SolarArrays").getByName("Rotate").setCurrentValue((-HalfPI + m_NewAngle) % TwoPI);
	                }
	                else if (m_NewAngle > m_OldAngle)
	                {
	                    m_Model.getArticulations().getByName("SolarArrays").getByName("Rotate").setCurrentValue((-HalfPI + (TwoPI - m_NewAngle)) % TwoPI);
	                }
	
	                if (!m_AnimateForward)
	                {
	                    m_AnimateForward = true;
	                    m_AnimateDirectionChanged = true;
	                }
	            }
	            else
	            {
	                if (m_NewAngle > m_OldAngle)
	                {
	                    m_Model.getArticulations().getByName("SolarArrays").getByName("Rotate").setCurrentValue((-HalfPI + m_NewAngle) % TwoPI);
	                }
	                else if (m_NewAngle < m_OldAngle)
	                {
	                    m_Model.getArticulations().getByName("SolarArrays").getByName("Rotate").setCurrentValue((-HalfPI + (TwoPI - m_NewAngle)) % TwoPI);
	                }
	
	                if (m_AnimateForward)
	                {
	                    m_AnimateForward = false;
	                    m_AnimateDirectionChanged = true;
	                }
	            }
	
	            if (m_AnimateDirectionChanged)
	            {
	                m_Path.clear();
	                m_AnimateDirectionChanged = false;
	            }
	
	            // Sets the old angle to current (new) angle.
	            m_OldAngle = m_NewAngle;
	            m_OldTime = timeEpSec;
	
	            // Update the position and orientation of the model 
	            IAgCrdnPointLocateInSystemResult positionResult = m_Point.locateInSystem(
	            	new Double(timeEpSec), m_Root.getVgtRoot().getWellKnownSystems().getEarth().getInertial());
	            IAgCrdnAxesFindInAxesResult orientationResult = m_Root.getVgtRoot().getWellKnownAxes().getEarth().getInertial().findInAxes(
	                new Double(timeEpSec), m_Axes);
	
	            Object[] positionPathPoint = new Object[] { new Double(positionResult.getPosition().getX()), new Double(positionResult.getPosition().getY()), new Double(positionResult.getPosition().getZ()) };
	
	            m_Model.setPosition(positionPathPoint);
	            m_Model.setOrientation(orientationResult.getOrientation());
	
	            AgCoreColor coreColor = AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN);
	            m_Path.addBack(manager.getInitializers().getPathPoint().initializeWithDatePositionAndColor(
	                m_Root.getConversionUtility().newDate("epSec", new Double(timeEpSec).toString()), positionPathPoint, coreColor));
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
	// #endregion
}