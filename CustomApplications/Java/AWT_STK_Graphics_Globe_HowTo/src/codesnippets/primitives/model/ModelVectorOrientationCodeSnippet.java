package codesnippets.primitives.model;

//#region Imports

//Java API
import java.awt.*;

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

public class ModelVectorOrientationCodeSnippet
extends STKGraphicsCodeSnippet
implements IAgStkObjectRootEvents2
{
	private IAgStkGraphicsModelPrimitive	m_Model;
	private PositionOrientationHelper		m_Provider;
	private Object							m_Epoch;
	private double							m_StopTime;

	public ModelVectorOrientationCodeSnippet(Component c)
	{
		super(c, "Orient a model along its velocity vector", "primitives", "model", "ModelVectorOrientationCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgScenario scenario = (IAgScenario)root.getCurrentScenario();

		this.m_Epoch = scenario.getStartTime_AsObject();

		// #region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		// Get the position and orientation data for the model from the data file
		String filePath1 = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"f-35_jsf_cvData.txt");
		PositionOrientationHelper provider = new PositionOrientationHelper(root, filePath1);

		// Create the model for the aircraft
		String filePath2 = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"f-35_jsf_cv.mdl");
		IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath2);
		model.setScale(Math.pow(10, 1.5));
		((IAgStkGraphicsPrimitive)model).setReferenceFrame(root.getVgtRoot().getWellKnownSystems().getEarth().getFixed());
		model.setPosition((Object[])provider.getPositionsList().get(0));
		IAgOrientation orientation = root.getConversionUtility().newOrientation();
		Object[] o = (Object[])provider.getOrientationsList().get(0);
		orientation.assignQuaternion(((Double)o[0]).doubleValue(), ((Double)o[1]).doubleValue(), ((Double)o[2]).doubleValue(), ((Double)o[3]).doubleValue());
		model.setOrientation(orientation);

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);
		// #endregion

		root.addIAgStkObjectRootEvents2(this);

		m_Model = model;
		m_Provider = provider;
		m_StopTime = Double.parseDouble(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:07:57.000").format("epSec"));
		OverlayHelper.addTextBox(this, manager, "The model's position and orientation are updated in the TimeChanged \r\nevent based on a point and axes evaluator created from a waypoint propagator.");
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
		animationSettings.setEnableAnimCycleTime(true);
		animationSettings.setAnimCycleTime(new Double(m_StopTime));
		animationSettings.setAnimCycleType(AgEScEndLoopType.E_END_TIME);
		animationControl.playForward();

		IAgPosition centerPosition = root.getConversionUtility().newPositionOnEarth();
		centerPosition.assignPlanetodetic(new Double(39.615), new Double(-77.205), 3000);

		Object[] xyz = (Object[])centerPosition.queryCartesianArray_AsObject();
		IAgStkGraphicsBoundingSphere boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(xyz, 1500);

		ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere, 0, 15);

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

				if((m_Provider != null) && (timeEpSec <= m_StopTime))
				{
					int index = m_Provider.findIndexOfClosestTime(timeEpSec, 0, m_Provider.getDatesList().size());

					// Update model's position and orientation every animation update
					m_Model.setPosition((Object[])m_Provider.getPositionsList().get(index));
					IAgOrientation orientation = root.getConversionUtility().newOrientation();

					Object[] o = (Object[])m_Provider.getOrientationsList().get(index);
					orientation.assignQuaternion(((Double)o[0]).doubleValue(), ((Double)o[1]).doubleValue(), ((Double)o[2]).doubleValue(), ((Double)o[3]).doubleValue());

					m_Model.setOrientation(orientation);
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