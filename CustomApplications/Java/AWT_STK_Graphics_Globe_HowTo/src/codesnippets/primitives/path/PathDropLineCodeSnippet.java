package codesnippets.primitives.path;

//#region Imports

//Java API
import java.awt.*;
import java.util.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkutil.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class PathDropLineCodeSnippet
extends STKGraphicsCodeSnippet 
implements IAgStkObjectRootEvents2
{
    private PositionOrientationHelper m_Provider = null;
    private Object m_Epoch;
    private double m_StartTime;
    private double m_StopTime;
    private IAgStkGraphicsPathPrimitive m_Path;
    private IAgStkGraphicsModelPrimitive m_Model;
    private Object[] m_PreviousPosition;
    private double m_PreviousDrop;

    public PathDropLineCodeSnippet(Component c)
	{
		super(c, "Draw trail and drop lines behind an aircraft", "primitives", "path", "PathDropLineCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        root.addIAgStkObjectRootEvents2(this);

        //#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        m_Epoch = ((IAgScenario)root.getCurrentScenario()).getStartTime_AsObject();
        
        // Get the position and orientation data for the model from the data file
        String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"f-35_jsf_cvData.txt");
        PositionOrientationHelper provider = new PositionOrientationHelper(root, filePath);

        // Create the model for the aircraft
        String filePath2 = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"f-35_jsf_cv.mdl");
        IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath2);
        model.setScale(Math.pow(10, 1.5));
        model.setPosition((Object[])provider.getPositionsList().get(0));
        IAgOrientation orientation = root.getConversionUtility().newOrientation();
        ArrayList<Double[]> o = provider.getOrientationsList();
        
        orientation.assignQuaternion(
            ((Double[])o.get(0))[0].doubleValue(),
            ((Double[])o.get(0))[1].doubleValue(),
            ((Double[])o.get(0))[2].doubleValue(),
            ((Double[])o.get(0))[3].doubleValue());
        model.setOrientation(orientation);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);

        // Create the path primitive
        IAgStkGraphicsPathPrimitive path = manager.getInitializers().getPathPrimitive().initializeDefault();
        path.setPolylineType(AgEStkGraphicsPolylineType.E_STK_GRAPHICS_POLYLINE_TYPE_LINES);
        IAgStkGraphicsDurationPathPrimitiveUpdatePolicy  policy = null;
        policy = manager.getInitializers().getDurationPathPrimitiveUpdatePolicy().initializeWithParameters(120, AgEStkGraphicsPathPrimitiveRemoveLocation.E_STK_GRAPHICS_REMOVE_LOCATION_FRONT);
        path.setUpdatePolicy((IAgStkGraphicsPathPrimitiveUpdatePolicy)policy);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)path);
        //#endregion

        m_Provider = provider;
        m_Path = path;
        m_Model = model;
        m_PreviousPosition = (Object[])model.getPosition_AsObject();
        m_PreviousDrop = Double.MIN_VALUE;
        m_StartTime = Double.MIN_VALUE;
        m_StopTime = Double.parseDouble(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:07:57.000").format("epSec"));
        
		OverlayHelper.addTextBox(this, manager, "Drop lines are added to the trail line of a model on a given interval.");
	}
	
	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgAnimation animationControl = (IAgAnimation)root;
        IAgScAnimation animationSettings = ((IAgScenario)root.getCurrentScenario()).getAnimation();

        // Set-up the animation for this specific example
        animationControl.pause();
        STKObjectsHelper.setAnimationDefaults(root);
        animationSettings.setAnimStepValue(1.0);
        animationSettings.setStartTime(m_Epoch);
        animationControl.playForward();

        IAgPosition centerPosition = root.getConversionUtility().newPositionOnEarth();
        centerPosition.assignPlanetodetic(new Double(39.615), new Double(-77.205), 3000);
        Object[] xyz = (Object[])centerPosition.queryCartesianArray_AsObject();
        IAgStkGraphicsBoundingSphere boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(xyz, 1500);

        ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere,
            0, 15);

        scene.render();
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

        if(m_Provider != null)
        {
        	m_Provider = null;
        }
        
        OverlayHelper.removeTextBox(manager);
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

	            // Record the animation start time.
	            if (m_StartTime == Double.MIN_VALUE)
	            {
	                m_StartTime = timeEpSec;
	            }

	            if ((/*$provider$The position and orientation provider$*/m_Provider != null) && (timeEpSec <= /*$stopTime$The stop time$*/m_StopTime))
	            {
	                int index = m_Provider.findIndexOfClosestTime(timeEpSec, 0, m_Provider.getDatesList().size());

	                //
	                // If the animation was restarted, the path must be cleared
	                // and record of previous drop line and position must be reset.
	                //
	                if (timeEpSec == m_StartTime)
	                {
	                    m_Path.clear();
	                    m_PreviousPosition = (Object[])m_Provider.getPositionsList().get(index);
	                    m_PreviousDrop = timeEpSec;
	                }
	                Object[] positionPathPoint = (Object[])m_Provider.getPositionsList().get(index);
	                
	                // Update model's position and orientation every animation update
	                ArrayList<Double[]> o = m_Provider.getOrientationsList();
	                m_Model.setPosition(positionPathPoint);
	                IAgOrientation orientation = root.getConversionUtility().newOrientation();
	                orientation.assignQuaternion(
	                    ((Double[])o.get(index))[0].doubleValue(),
	                    ((Double[])o.get(index))[1].doubleValue(),
	                    ((Double[])o.get(index))[2].doubleValue(),
	                    ((Double[])o.get(index))[3].doubleValue());
	                m_Model.setOrientation(orientation);

	                // Update path with model's new position and check
	                // to add drop line at every animation update
	                m_Path.addBack(manager.getInitializers().getPathPoint().initializeWithDateAndPosition(
	                    root.getConversionUtility().newDate("epSec", Double.toString(timeEpSec)), m_PreviousPosition));
	                
	                m_Path.addBack(manager.getInitializers().getPathPoint().initializeWithDateAndPosition(
	                    root.getConversionUtility().newDate("epSec", Double.toString(timeEpSec)), positionPathPoint));                    

	                m_PreviousPosition = positionPathPoint;

	                // Add drop line
	                if (Math.abs(timeEpSec - m_PreviousDrop) > 10)
	                {
	                    IAgPosition endpointPosition = root.getConversionUtility().newPositionOnEarth();
	                    Object[] pos = (Object[])m_Model.getPosition_AsObject();
	                    endpointPosition.assignCartesian(((Double)pos[0]).doubleValue(), ((Double)pos[1]).doubleValue(), ((Double)pos[2]).doubleValue());
	                    
	                    Object[] lla = null;
	                    lla = (Object[])endpointPosition.queryPlanetodeticArray_AsObject();

	                    endpointPosition.assignPlanetodetic(lla[0], lla[1], 0);

	                    Object[] xyz = null;
	                    xyz = (Object[])endpointPosition.queryCartesianArray_AsObject();

	                    m_Path.addBack(manager.getInitializers().getPathPoint().initializeWithDateAndPosition(
	                        root.getConversionUtility().newDate("epSec", new Double(timeEpSec).toString()), positionPathPoint));
	                    m_Path.addBack(manager.getInitializers().getPathPoint().initializeWithDateAndPosition(
	                        root.getConversionUtility().newDate("epSec",new Double(timeEpSec).toString()), xyz));

	                    m_PreviousDrop = timeEpSec;
	                }
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
