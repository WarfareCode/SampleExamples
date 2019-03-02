package codesnippets.picking;

//#region Imports

//Java API
import java.util.*;
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class PickChangeColorCodeSnippet
extends STKGraphicsCodeSnippet
implements IAgGlobeCntrlEvents
{
	private IAgStkGraphicsPrimitive	m_Models;
	private IAgStkGraphicsPrimitive	m_SelectedModel;

	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;
	private AgStkGraphicsSceneClass	m_AgStkGraphicsSceneClass;

	public PickChangeColorCodeSnippet(Component c)
	{
		super(c, "Change a model's color on mouse over", "picking", "PickChangeColorCodeSnippet.java");

		this.m_AgGlobeCntrlClass = (AgGlobeCntrlClass)this.getComponent();
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		this.m_AgStkGraphicsSceneClass = scene;

		this.m_AgGlobeCntrlClass.addIAgGlobeCntrlEvents(this);

		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
		Random r = new Random();

		IAgStkGraphicsCompositePrimitive models = manager.getInitializers().getCompositePrimitive().initializeDefault();

		for(int i = 0; i < 25; ++i)
		{
			Object[] position = new Object[] {new Double(35 + r.nextDouble()), new Double(-(82 + r.nextDouble())), new Double(0.0)};

			models.add(createModel(position, root));
		}

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)models);

		OverlayHelper.addTextBox(this, manager,"Move the mouse over a model to change its color from red to cyan.\r\n\r\nThis technique, \"roll over\" picking, is implemented by calling \r\nScene.pick in the 3D window's MouseMove event to determine \r\nwhich primitive is under the mouse.");

		m_Models = (IAgStkGraphicsPrimitive)models;
		m_SelectedModel = null;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Models.getBoundingSphere(), -90, 15);
		double distance = scene.getCamera().getDistance();
		scene.getCamera().setDistance(distance * 0.7); // zoom in a bit
		scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		this.m_AgGlobeCntrlClass.removeIAgGlobeCntrlEvents(this);

		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		if(m_Models != null)
		{
			manager.getPrimitives().remove(m_Models);
			m_Models = null;
		}

		if(m_SelectedModel != null)
		{
			m_SelectedModel = null;
		}
		
		OverlayHelper.removeTextBox(manager);
		scene.render();
	}

	private static IAgStkGraphicsPrimitive createModel(Object[] position, AgStkObjectRootClass root)
	throws AgCoreException
	{
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		IAgPosition origin = root.getConversionUtility().newPositionOnEarth();
		origin.assignPlanetodetic(position[0], position[1], ((Double)position[2]).doubleValue());
		IAgCrdnAxesFixed axes = STKVgtHelper.createAxes(root, "Earth", origin);
		AgVariant epoch = ((IAgScenario)root.getCurrentScenario()).getEpoch();
		IAgCrdnAxesFindInAxesResult result = root.getVgtRoot().getWellKnownAxes().getEarth().getFixed().findInAxes(epoch, ((IAgCrdnAxes)axes));

		String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"facility-colorless.mdl");
		IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);
		model.setPositionCartographic("Earth", position);
		model.setOrientation(result.getOrientation());
		model.setScale(Math.pow(10, 2));
		((IAgStkGraphicsPrimitive)model).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));

		return (IAgStkGraphicsPrimitive)model;
	}

	public void onAgGlobeCntrlEvent(AgGlobeCntrlEvent e)
	{
		try
		{
			int type = e.getType();
			if(type == AgGlobeCntrlEvent.TYPE_MOUSE_MOVE)
			{
				Object[] params = e.getParams();
				int mouseX = ((Integer)params[2]).intValue();
				int mouseY = ((Integer)params[3]).intValue();

				if(m_Models != null)
				{
					// #region CodeSnippet

					// Get a collection of picked objects under the mouse location.
					// The collection is sorted with the closest object at index zero.
					IAgStkGraphicsPickResultCollection collection = this.m_AgStkGraphicsSceneClass.pick(mouseX, mouseY);
					if(collection.getCount() != 0)
					{
						IAgStkGraphicsObjectCollection objects = collection.getItem(0).getObjects();
						Object selectedObject0 = objects.getItem_AsObject(0);
						if(selectedObject0 instanceof IAgDispatch)
						{
							IAgDispatch composite = (IAgDispatch)selectedObject0;

							// Was a model in our composite picked?
							if(composite.equals(m_Models))
							{
								IAgDispatch dispatch = (IAgDispatch)objects.getItem_AsObject(1);
								IAgStkGraphicsPrimitive model = new AgStkGraphicsPrimitive(dispatch);

								// Selected Model
								model.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.CYAN));

								if(!model.equals(m_SelectedModel))
								{
									// Unselect previous model
									if(m_SelectedModel != null)
									{
										m_SelectedModel.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
									}
									m_SelectedModel = model;
									this.m_AgStkGraphicsSceneClass.render();
								}
								return;
							}
						}
					}

					// Unselect previous model
					if(m_SelectedModel != null)
					{
						m_SelectedModel.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
						m_SelectedModel = null;
						this.m_AgStkGraphicsSceneClass.render();
					}
					// #endregion
				}
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
}