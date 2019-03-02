package codesnippets.primitives.composite;

//#region Imports

//Java API
import java.util.*;
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
import utils.customtypes.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class CompositeLayersCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsPrimitive				m_Models;
	private IAgStkGraphicsMarkerBatchPrimitive	m_Markers;
	private IAgStkGraphicsPointBatchPrimitive	m_Points;
	private ArrayList<Interval>					m_Intervals;

	public CompositeLayersCodeSnippet(Component c)
	{
		super(c, "Create layers of primitives", "primitives", "composite", "CompositeLayersCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        Random r = new Random();
        final int modelCount = 25;

        Object[] positions = new Object[modelCount * 3];

        // Create the models
        IAgStkGraphicsCompositePrimitive models = null;
        models = manager.getInitializers().getCompositePrimitive().initializeDefault();

        for (int i = 0; i < modelCount; ++i)
        {
            double latitude = 35 + 1.5 * r.nextDouble();
            double longitude = -(80 + 1.5 * r.nextDouble());
            double altitude = 0;
            Object[] position = new Object[]{new Double(latitude), new Double(longitude), new Double(altitude)};

            positions[i*3] = new Double(latitude);
            positions[i*3+1] = new Double(longitude);
            positions[i*3+2] = new Double(altitude);

            models.add(createModel(position, root));
        }

        // Create the markers
        IAgStkGraphicsMarkerBatchPrimitive markers = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        markers.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);

        String facilityMarkerTexturePath = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"facility.png");
        markers.setTexture(manager.getTextures().loadFromStringUri(facilityMarkerTexturePath));
        markers.setCartographic("Earth", positions);

        // Create the points
        IAgStkGraphicsPointBatchPrimitive points = manager.getInitializers().getPointBatchPrimitive().initializeDefault();
        points.setPixelSize(5);
        Object[] colors = new Object[modelCount];
        for (int i = 0; i < colors.length; i++)
        {
            colors[i] = new Long(Color.ORANGE.getRGB());
        }
        points.setCartographicWithColorsAndRenderPass("Earth", positions, colors, AgEStkGraphicsRenderPassHint.E_STK_GRAPHICS_RENDER_PASS_HINT_OPAQUE);

        // Set the display Conditions
        IAgStkGraphicsAltitudeDisplayCondition near = manager.getInitializers().getAltitudeDisplayCondition().initializeWithAltitudes(0, 500000);
        ((IAgStkGraphicsPrimitive)models).setDisplayCondition((IAgStkGraphicsDisplayCondition)near);

        IAgStkGraphicsAltitudeDisplayCondition medium = manager.getInitializers().getAltitudeDisplayCondition().initializeWithAltitudes(500000, 2000000);
        ((IAgStkGraphicsPrimitive)markers).setDisplayCondition((IAgStkGraphicsDisplayCondition)medium);

        IAgStkGraphicsAltitudeDisplayCondition far = manager.getInitializers().getAltitudeDisplayCondition().initializeWithAltitudes(2000000, 4000000);
        ((IAgStkGraphicsPrimitive)points).setDisplayCondition((IAgStkGraphicsDisplayCondition)far);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)models);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)markers);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)points);
        //#endregion

    	OverlayHelper.addTextBox(this, manager, "Zoom in and out to see layers of primitives based on altitude.\r\nModels are shown when zoomed in closest. As you zoom out, \r\nmodels switch to markers, then to points.\r\n\r\nThis level of detail technique is implemented by adding each\r\nModelPrimitive to a CompositePrimitive. A different\r\nAltitudeDisplayCondition is assigned to the composite, \r\na marker batch, and a point batch.");

        OverlayHelper.addAltitudeOverlay(this, manager, scene);
        m_Intervals = new ArrayList<Interval>();

        m_Intervals.add(new Interval(0, 500000));
        m_Intervals.add(new Interval(500000, 2000000));
        m_Intervals.add(new Interval(2000000, 4000000));

        OverlayHelper.getAltitudeDisplay().addIntervals(m_Intervals);

        m_Models = (IAgStkGraphicsPrimitive)models;
        m_Markers = markers;
        m_Points = points;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		if(m_Models != null)
		{
			ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Models.getBoundingSphere());
			scene.render();
		}
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
		if(m_Models != null)
		{
			manager.getPrimitives().remove(m_Models);
			m_Models = null;
		}

		if(m_Markers != null)
		{
			manager.getPrimitives().remove((IAgStkGraphicsPrimitive)m_Markers);
			m_Markers = null;
		}
		
		if(m_Points != null)
		{
			manager.getPrimitives().remove((IAgStkGraphicsPrimitive)m_Points);
			m_Points = null;
		}

		OverlayHelper.removeTextBox(manager);

		if(m_Intervals != null)
		{
			OverlayHelper.getAltitudeDisplay().removeIntervals(m_Intervals);
			m_Intervals = null;
		}

		OverlayHelper.removeAltitudeOverlay(this, manager);
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

        String modelPath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Land"+fileSep+"facility.mdl");

        IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(modelPath);
        model.setPositionCartographic("Earth", position);
        model.setOrientation(result.getOrientation());
        model.setScale(Math.pow(10, 2));

        return (IAgStkGraphicsPrimitive)model;
    }
}