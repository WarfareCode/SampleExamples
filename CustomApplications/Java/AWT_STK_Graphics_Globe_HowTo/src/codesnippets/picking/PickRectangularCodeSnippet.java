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

public class PickRectangularCodeSnippet
extends STKGraphicsCodeSnippet
implements IAgGlobeCntrlEvents
{
	private IAgStkGraphicsCompositePrimitive	m_Models;
	private ArrayList<IAgStkGraphicsPrimitive>	m_SelectedModels;
	private IAgStkGraphicsScreenOverlay			m_Overlay;
	private AgGlobeCntrlClass					m_AgGlobeCntrlClass;
	private AgStkObjectRootClass				m_AgStkObjectRootClass;
	private AgStkGraphicsSceneClass				m_AgStkGraphicsSceneClass;

	public PickRectangularCodeSnippet(Component c)
	{
		super(c, "Change model colors within a rectangular region", "picking", "PickRectangularCodeSnippet.java");

		this.m_AgGlobeCntrlClass = (AgGlobeCntrlClass)this.getComponent();
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		this.m_AgStkObjectRootClass = root;
		this.m_AgStkGraphicsSceneClass = scene;

		this.m_AgGlobeCntrlClass.addIAgGlobeCntrlEvents(this);

		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		// Create a screen overlay to visualize the 100 by 100 picking region.
		m_Overlay = manager.getInitializers().getScreenOverlay().initializeDefault(0, 0, 100, 100);
		((IAgStkGraphicsOverlay)m_Overlay).setPinningOrigin(AgEStkGraphicsScreenOverlayPinningOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_PINNING_ORIGIN_CENTER);
		((IAgStkGraphicsOverlay)m_Overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_LEFT);
		((IAgStkGraphicsOverlay)m_Overlay).setTranslucency(.9f);
		((IAgStkGraphicsOverlay)m_Overlay).setBorderSize(2);

		Random r = new Random();

		IAgStkGraphicsCompositePrimitive models = manager.getInitializers().getCompositePrimitive().initializeDefault();
		m_SelectedModels = new ArrayList<IAgStkGraphicsPrimitive>();

		for(int i = 0; i < 25; ++i)
		{
			Object[] position = new Object[] {new Double(35 + r.nextDouble()), new Double(-(82 + r.nextDouble())), new Double(0.0)};

			models.add(createModel(position, root));
		}

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)models);

		OverlayHelper.addTextBox(this, manager,"Move the rectangular box over models to change their color.\r\n\r\nThis technique, \"roll over\" picking with a rectangular region,\r\nis implemented by calling Scene.PickRectangular in the 3D \r\nwindow's MouseMove event to determine which primitives are \r\nunder the rectangular region associated with the mouse.");

		m_Models = models;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		ViewHelper.viewBoundingSphere(root, scene, "Earth", ((IAgStkGraphicsPrimitive)m_Models).getBoundingSphere(), -90, 15);
		double distance = scene.getCamera().getDistance();
		scene.getCamera().setDistance(distance * 0.7); // zoom in a bit
		scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		this.m_AgGlobeCntrlClass.removeIAgGlobeCntrlEvents(this);

		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
		IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

		if(m_Overlay != null)
		{
			overlayManager.remove(m_Overlay);
			m_Overlay = null;
		}
		
		if(m_Models != null)
		{
			manager.getPrimitives().remove((IAgStkGraphicsPrimitive)m_Models);
			m_Models = null;
		}
		
		if(m_SelectedModels != null)
		{
			m_SelectedModels = null;
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
		IAgCrdnAxesFindInAxesResult result = root.getVgtRoot().getWellKnownAxes().getEarth().getFixed().findInAxes(((IAgScenario)root.getCurrentScenario()).getEpoch(), ((IAgCrdnAxes)axes));

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

				IAgStkGraphicsSceneManager manager = ((IAgScenario)m_AgStkObjectRootClass.getCurrentScenario()).getSceneManager();
				IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
				if(m_Models != null)
				{
					if(!overlayManager.contains(m_Overlay))
					{
						overlayManager.add(m_Overlay);
					}
					((IAgStkGraphicsOverlay)m_Overlay).setPosition(new Object[] {new Integer(mouseX), new Integer(mouseY), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()),
					new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())});

					// #region CodeSnippet

					// Get a collection of picked objects in a 100 by 100 rectangular region.
					// The collection is sorted with the closest object at index zero.
					ArrayList<IAgStkGraphicsPrimitive> newModels = new ArrayList<IAgStkGraphicsPrimitive>();
					IAgStkGraphicsPickResultCollection collection = this.m_AgStkGraphicsSceneClass.pickRectangular(mouseX - 50, mouseY + 50, mouseX + 50, mouseY - 50);
					for(int i = 0; i < collection.getCount(); i++)
					{
						IAgStkGraphicsPickResult pickResult = collection.getItem(i);
						IAgStkGraphicsObjectCollection objects = pickResult.getObjects();
						Object selectedObject0 = objects.getItem_AsObject(0);
						if(selectedObject0 instanceof IAgDispatch)
						{
							IAgDispatch composite = (IAgDispatch)selectedObject0;

							// Was a model in our composite picked?
							if(composite.equals(m_Models))
							{
								Object selectedObject1 = objects.getItem_AsObject(1);
								if(selectedObject1 instanceof IAgDispatch)
								{
									IAgDispatch dispatch2 = (IAgDispatch)selectedObject1;
									IAgStkGraphicsModelPrimitive model = null;
									model = new AgStkGraphicsModelPrimitiveClass(dispatch2);

									// Selected Model
									((IAgStkGraphicsPrimitive)model).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.CYAN));
									newModels.add((IAgStkGraphicsPrimitive)model);
								}
							}
						}
					}
					// Reset color of models that were previous selected but were not in this pick.
					for(int i = 0; i < m_SelectedModels.size(); i++)
					{
						IAgStkGraphicsModelPrimitive selectedModel = (IAgStkGraphicsModelPrimitive)m_SelectedModels.get(i);
						if(!newModels.contains(selectedModel))
						{
							((IAgStkGraphicsPrimitive)selectedModel).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
						}
					}
					m_SelectedModels = newModels;

					manager.render();

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