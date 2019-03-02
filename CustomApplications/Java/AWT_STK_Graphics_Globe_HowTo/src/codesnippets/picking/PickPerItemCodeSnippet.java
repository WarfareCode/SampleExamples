package codesnippets.picking;

//#region Imports

//Java API
import java.util.*;
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.*;
import agi.stkutil.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;
import agi.stkx.*;
import agi.stkx.awt.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class PickPerItemCodeSnippet
extends STKGraphicsCodeSnippet
implements IAgGlobeCntrlEvents
{
	private IAgStkGraphicsPrimitive		m_MarkerBatch;
	private ArrayList<Object[]>			m_markerPositions;
	private AgGlobeCntrlClass			m_AgGlobeCntrlClass;
	private AgStkObjectRootClass		m_AgStkObjectRootClass;
	private AgStkGraphicsSceneClass		m_AgStkGraphicsSceneClass;
	private int							m_LastMouseClickX;
	private int							m_LastMouseClickY;

	public PickPerItemCodeSnippet(Component c)
	{
		super(c, "Zoom to a particular marker in a batch", "picking", "PickPerItemCodeSnippet.java");

		this.m_AgGlobeCntrlClass = (AgGlobeCntrlClass)this.getComponent();
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        this.m_AgStkObjectRootClass = root;
		this.m_AgStkGraphicsSceneClass = scene;

		this.m_AgGlobeCntrlClass.addIAgGlobeCntrlEvents(this);

		// #region CodeSnippet
		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		ArrayList<Object[]> positions = new ArrayList<Object[]>();
		positions.add(new Object[] {new Double(39.88), new Double(-75.25), new Double(3000.0)});
		positions.add(new Object[] {new Double(38.85), new Double(-77.04), new Double(3000.0)});
		positions.add(new Object[] {new Double(38.85), new Double(-77.04), new Double(0.0)});
		positions.add(new Object[] {new Double(29.98), new Double(-90.25), new Double(0.0)});
		positions.add(new Object[] {new Double(37.37), new Double(-121.92), new Double(0.0)});

		Object[] positionsArray = this.convertPositionListToArray(positions);

		IAgStkGraphicsMarkerBatchPrimitive markerBatch = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
		String data = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"facility.png");
		markerBatch.setTexture(manager.getTextures().loadFromStringUri(data));
		markerBatch.setCartographic("Earth", positionsArray);

		// Save the positions of the markers for use in the pick event
		m_markerPositions = positions;

		// Enable per item picking
		markerBatch.setPerItemPickingEnabled(true);

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)markerBatch);
		// #endregion

		m_MarkerBatch = (IAgStkGraphicsPrimitive)markerBatch;

		OverlayHelper.addTextBox(this, manager,"Double click on a marker to zoom to it.\r\n\r\nThe PerItemPicking property is set to true for the \r\nbatch primitive.  Scene.pick is called in response \r\nto a double-click in the 3D window to determine\r\nthe primitive and item index under the mouse. Camera.viewSphere\r\nis then used to zoom to the marker at the picked index.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		if(m_MarkerBatch != null)
		{
			ViewHelper.viewBoundingSphere(root, scene, "Earth", m_MarkerBatch.getBoundingSphere());
			scene.render();
		}
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		if(m_MarkerBatch != null)
		{
			this.m_AgGlobeCntrlClass.removeIAgGlobeCntrlEvents(this);

			IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

			if(m_MarkerBatch != null)
			{
				manager.getPrimitives().remove(m_MarkerBatch);
				m_MarkerBatch = null;
			}
			
			OverlayHelper.removeTextBox(manager);
			scene.render();
		}
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
	            IAgStkGraphicsSceneManager manager = ((IAgScenario)this.m_AgStkObjectRootClass.getCurrentScenario()).getSceneManager();

	            if (m_MarkerBatch != null)
	            {
	            	//#region CodeSnippet
	                // Get a collection of picked objects under the mouse location.
	                // The collection is sorted with the closest object at index zero.
	                IAgStkGraphicsPickResultCollection collection = this.m_AgStkGraphicsSceneClass.pick(m_LastMouseClickX, m_LastMouseClickY);
	                if (collection.getCount() != 0)
	                {
	                    IAgStkGraphicsObjectCollection objects = collection.getItem(0).getObjects();
	                    IAgDispatch batchPrimitive = (IAgDispatch)objects.getItem_AsObject(0);

	                    // Was a marker in our marker batch picked?
	                    if (batchPrimitive.equals(m_MarkerBatch))
	                    {
	                        // Get the index of the particular marker we picked
		                    IAgDispatch dispatch1 = (IAgDispatch)objects.getItem_AsObject(1);
	                        IAgStkGraphicsBatchPrimitiveIndex markerIndex = new AgStkGraphicsBatchPrimitiveIndex(dispatch1);
	                        
	                        // Get the position of the particular marker we picked
	                        int index = markerIndex.getIndex();
	                        Object[] markerCartographic = (Object[])m_markerPositions.get(index);

	                        IAgPosition markerPosition = this.m_AgStkObjectRootClass.getConversionUtility().newPositionOnEarth();
	                        markerPosition.assignPlanetodetic(
	                            (Double)markerCartographic[0],
	                            (Double)markerCartographic[1], 
	                            ((Double)markerCartographic[2]).doubleValue());

	                        Object[] selectedMarkerCartesianPosition = (Object[])markerPosition.queryCartesianArray_AsObject();

	                        // Zoom to the position
	                        IAgStkGraphicsBoundingSphere boundingSphere = null;
	                        boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(selectedMarkerCartesianPosition, 100);
	                        ViewHelper.viewBoundingSphere(this.m_AgStkObjectRootClass, this.m_AgStkGraphicsSceneClass, "Earth", boundingSphere);
	                        this.m_AgStkGraphicsSceneClass.render();
	                    }
	                }
	                //#endregion
	            }
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
}