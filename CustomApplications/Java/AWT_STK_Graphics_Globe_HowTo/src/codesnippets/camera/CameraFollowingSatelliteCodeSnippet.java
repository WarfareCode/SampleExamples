package codesnippets.camera;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;
import codesnippets.primitives.*;

//#endregion

public class CameraFollowingSatelliteCodeSnippet
extends STKGraphicsCodeSnippet
// implements IDisposable
 implements IAgStkObjectRootEvents2
{
	private IAgSatellite					m_Satellite;
	private IAgStkGraphicsModelPrimitive	m_Model;
	private ReferenceFrameGraphics			m_ReferenceFrameGraphics;
	private IAgCrdnProvider					m_Provider;
	private IAgCrdnPoint					m_Point;
	private IAgCrdnAxes						m_Axes;

	private double							m_NewAngle;
	private double							m_OldAngle;
	private boolean							m_FirstRun	= true;

	private double							m_OldTime;
	private double							m_StartTime;

	private String							objectName	= "CameraFollowingSatellite";

	public CameraFollowingSatelliteCodeSnippet(Component c)
	{
		super(c, "Follow an Earth orbiting satellite", "camera", "CameraFollowingSatelliteCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		// #region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
		IAgStkGraphicsSceneManager manager = scenario.getSceneManager();

		// Create the model and its propagator and set the camera view
		IAgSatellite satellite = createSatellite(root);

		// Create the SGP4 Propogator from the TLE
		satellite.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_SGP4);
		IAgVePropagatorSGP4 propagator = (IAgVePropagatorSGP4)satellite.getPropagator();
		String path = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"ISS.tle");
		propagator.getCommonTasks().addSegsFromFile("25544", path);
		IAgCrdnEventInterval intvl = root.getCurrentScenario().getVgt().getEventIntervals().getItem("AnalysisInterval");
        propagator.getEphemerisInterval().setImplicitInterval(intvl);
		propagator.propagate();
		double epoch = propagator.getSegments().getItem(0).getEpoch();

		// Get the Vector Geometry Tool provider for the satellite and find its initial position and orientation.
		IAgCrdnProvider provider = ((IAgStkObject)satellite).getVgt();
		IAgCrdnSystem system = root.getVgtRoot().getWellKnownSystems().getEarth().getInertial();
		Object startTime = scenario.getStartTime_AsObject();
		IAgCrdnPointLocateInSystemResult positionResult = provider.getPoints().getItem("Center").locateInSystem(startTime, system);
		IAgCartesian3Vector position = positionResult.getPosition();
		IAgCrdnAxes axes = root.getVgtRoot().getWellKnownAxes().getEarth().getInertial();
		IAgCrdnAxesFindInAxesResult orientationResult = axes.findInAxes(startTime, provider.getAxes().getItem("Body"));
		IAgOrientation orientation = orientationResult.getOrientation();

		// Create the satellite model
		String path2 = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Space"+fileSep+"hs601.mdl");
		IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(path2);
		((IAgStkGraphicsPrimitive)model).setReferenceFrame(root.getVgtRoot().getWellKnownSystems().getEarth().getInertial());
		model.setPosition(new Object[] {new Double(position.getX()), new Double(position.getY()), new Double(position.getZ())});
		model.setOrientation(orientation);
		manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);

		// Set the time
		scenario.getAnimation().setStartTime(new Double(epoch));

		// #endregion

		root.addIAgStkObjectRootEvents2(this);

		// Set the member variables
		m_Satellite = satellite;
		m_Model = model;
		m_ReferenceFrameGraphics = new ReferenceFrameGraphics(root, provider.getSystems().getItem("Body"), 25);
		m_Provider = provider;
		m_Point = provider.getPoints().getItem("Center");
		m_Axes = provider.getAxes().getItem("Body");

		OverlayHelper.addTextBox(this,manager,"The SGP4 propagator is used to propagate a satellite from a TLE.\r\n A model primitive that automatically follows the propagator's point is \r\ncreated to visualize the satellite. Camera.ViewOffset and Camera.Constrained are used \r\nto view the model.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgAnimation animationControl = (IAgAnimation)root;
		IAgScAnimation animationSettings = ((IAgScenario)root.getCurrentScenario()).getAnimation();

		// Set-up the animation for this specific example
		double cycleTime = ((Double)animationSettings.getStartTime_AsObject()).doubleValue() + 3600.0;

		animationControl.pause();
		STKObjectsHelper.setAnimationDefaults(root);
		animationSettings.setAnimStepValue(1.0);
		animationSettings.setEnableAnimCycleTime(true);
		animationSettings.setAnimCycleTime(new Double(cycleTime));
		animationSettings.setAnimCycleType(AgEScEndLoopType.E_LOOP_AT_TIME);
		animationControl.playForward();

		// #region CodeSnippet

		// Create the viewer point, which is an offset from near the satellite position
		// looking towards the satellite. Set the camera to look from the viewer to the
		// satellite.
		Object[] offset = new Object[] {new Double(50.0), new Double(50.0), new Double(-50.0)};
		scene.getCamera().viewOffset(m_Axes, m_Point, offset);
		scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_NEGATIVE_Z);

		// #endregion
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

		if(m_ReferenceFrameGraphics != null)
		{
			m_ReferenceFrameGraphics.dispose();
			m_ReferenceFrameGraphics = null;
		}
		
		root.getCurrentScenario().getChildren().unload(AgESTKObjectType.E_SATELLITE, objectName);
		m_Satellite = null;

		m_Provider = null;
		m_Point = null;
		m_Axes = null;

		scene.getCamera().viewCentralBody("Earth", root.getVgtRoot().getWellKnownAxes().getEarth().getInertial());
		scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);

		((IAgAnimation)root).pause();
		STKObjectsHelper.setAnimationDefaults(root);
		((IAgAnimation)root).rewind();

		OverlayHelper.removeTextBox(manager);

		scene.render();
	}

	private IAgSatellite createSatellite(IAgStkObjectRoot root)
	throws AgCoreException
	{
		IAgSatellite satellite = null;
		if(root.getCurrentScenario().getChildren().contains(AgESTKObjectType.E_SATELLITE, objectName))
		{
			satellite = (IAgSatellite)root.getObjectFromPath("Satellite/" + objectName);
		}
		else
		{
			satellite = (IAgSatellite)root.getCurrentScenario().getChildren()._new(AgESTKObjectType.E_SATELLITE, objectName);
			satellite.getVO().getPass().getTrackData().getPassData().getOrbit().setLeadDataType(AgELeadTrailData.E_DATA_NONE);
			satellite.getVO().getPass().getTrackData().getPassData().getOrbit().setTrailSameAsLead();
			satellite.getGraphics().setUseInstNameLabel(false);
			satellite.getGraphics().setLabelName("");
			satellite.getVO().getModel().getOrbitMarker().setVisible(false);
			satellite.getVO().getModel().setVisible(false);
		}

		return satellite;
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
	            if (m_Satellite != null)
	            {
    				Object[] params = e.getParams();
    				double timeEpSec = ((Double)params[0]).doubleValue();

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

	                // Rotates the satellite panels. The panel rotation is reversed when the animation is reversed.
	                // Set boolean flag for update to path.
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
	                }

	                // Sets the old angle to current (new) angle.
	                m_OldAngle = m_NewAngle;
	                m_OldTime = timeEpSec;

	                // Update the position and orientation of the model 
	                IAgCrdnPointLocateInSystemResult positionResult = null;
	                positionResult = m_Point.locateInSystem(new Double(timeEpSec), root.getVgtRoot().getWellKnownSystems().getEarth().getInertial());
	                IAgCrdnAxesFindInAxesResult orientationResult = root.getVgtRoot().getWellKnownAxes().getEarth().getInertial().findInAxes(new Double(timeEpSec), m_Axes);

	                m_Model.setPosition(new Object[] 
	                {	
	                	new Double(positionResult.getPosition().getX()),
	                	new Double(positionResult.getPosition().getY()),
	                	new Double(positionResult.getPosition().getZ())
	                });

	                m_Model.setOrientation(orientationResult.getOrientation());
	            }
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
	// #endregion
}