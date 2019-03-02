package codesnippets.picking;

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
import agi.stkx.*;
import agi.stkx.awt.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class PickZoomCodeSnippet
extends STKGraphicsCodeSnippet
implements IAgGlobeCntrlEvents
{
	private IAgStkGraphicsPrimitive	m_Models;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgStkGraphicsSceneClass	m_AgStkGraphicsSceneClass;
	private int						m_LastMouseClickX;
	private int						m_LastMouseClickY;

	public PickZoomCodeSnippet(Component c)
	{
		super(c, "Zoom to a model on double click", "picking", "PickZoomCodeSnippet.java");

		this.m_AgGlobeCntrlClass = (AgGlobeCntrlClass)this.getComponent();
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		this.m_AgStkObjectRootClass = root;
		this.m_AgStkGraphicsSceneClass = scene;

		this.m_AgGlobeCntrlClass.addIAgGlobeCntrlEvents(this);

		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
		IAgStkGraphicsCompositePrimitive models = manager.getInitializers().getCompositePrimitive().initializeDefault();

		// Create the positions
		Object[] p0 = new Object[] {new Double(39.88), new Double(-75.25), new Double(3000.0)};
		Object[] p1 = new Object[] {new Double(38.85), new Double(-77.04), new Double(0.0)};
		Object[] p2 = new Object[] {new Double(29.98), new Double(-90.25), new Double(0.0)};
		Object[] p3 = new Object[] {new Double(37.37), new Double(-121.92), new Double(0.0)};

		models.add(createModel(p0, root));
		models.add(createModel(p1, root));
		models.add(createModel(p2, root));
		models.add(createModel(p3, root));

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)models);

		OverlayHelper.addTextBox(this, manager, "Double click on a model to zoom to it.\r\n\r\nScene.pick is called in response to a double-click in the 3D window to determine the primitive under the \r\nmouse. Camera.viewSphere is then used to zoom to the primitive.");

		m_Models = (IAgStkGraphicsPrimitive)models;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Models.getBoundingSphere());
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
		
		OverlayHelper.removeTextBox(manager);
		scene.render();
	}

	private static IAgStkGraphicsPrimitive createModel(Object[] position, AgStkObjectRootClass root)
	throws Throwable
	{
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		IAgPosition origin = root.getConversionUtility().newPositionOnEarth();
		origin.assignPlanetodetic(position[0], position[1], ((Double)position[2]).doubleValue());
		IAgCrdnAxesFixed axes = STKVgtHelper.createAxes(root, "Earth", origin);

		IAgCrdnAxesFindInAxesResult result = root.getVgtRoot().getWellKnownAxes().getEarth().getFixed().findInAxes(((IAgScenario)root.getCurrentScenario()).getEpoch(), ((IAgCrdnAxes)axes));

		String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Land"+fileSep+"facility.mdl");
		IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);
		model.setPositionCartographic("Earth", position);
		model.setOrientation(result.getOrientation());
		model.setScale(Math.pow(10, 3.5));

		return (IAgStkGraphicsPrimitive)model;
	}

	public void onAgGlobeCntrlEvent(AgGlobeCntrlEvent e)
	{
		try
		{
			int type = e.getType();
			if(type == AgGlobeCntrlEvent.TYPE_MOUSE_UP)
			{
				Object[] params = e.getParams();
				m_LastMouseClickX = ((Integer)params[2]).intValue();
				m_LastMouseClickY = ((Integer)params[3]).intValue();
			}
			else if(type == AgGlobeCntrlEvent.TYPE_DBL_CLICK)
			{
				if(m_Models != null)
				{
					// #region CodeSnippet

					// Get a collection of picked objects under the mouse location.
					// The collection is sorted with the closest object at index zero.
					IAgStkGraphicsPickResultCollection collection = null;
					collection = this.m_AgStkGraphicsSceneClass.pick(m_LastMouseClickX, m_LastMouseClickY);
					if(collection.getCount() != 0)
					{
						IAgStkGraphicsObjectCollection objects = collection.getItem(0).getObjects();
						//Check that we have not selected the Central Body as the closest object. 
						//Central Bodies are returned as a string with the name of the central body.
						if (!(objects.getItem_AsObject(0) instanceof String))
						{
							IAgDispatch composite = objects.getItem(0).getDispatch();

							// Was a model in our composite picked?
							if(composite.equals(m_Models))
							{
								IAgDispatch dispatch = (IAgDispatch)objects.getItem_AsObject(1);
								IAgStkGraphicsPrimitive model = new AgStkGraphicsPrimitive(dispatch);
								ViewHelper.viewBoundingSphere(this.m_AgStkObjectRootClass, this.m_AgStkGraphicsSceneClass, "Earth", model.getBoundingSphere());
								this.m_AgStkGraphicsSceneClass.render();
							}
						}
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