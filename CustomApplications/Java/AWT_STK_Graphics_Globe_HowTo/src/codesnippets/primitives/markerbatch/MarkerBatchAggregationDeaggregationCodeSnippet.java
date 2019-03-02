package codesnippets.primitives.markerbatch;

//#region Imports

//Java API
import java.util.*;
import java.util.List;
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.customtypes.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class MarkerBatchAggregationDeaggregationCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsPrimitive	m_FriendlyBattalionMarkerBatch;
	private IAgStkGraphicsPrimitive	m_FriendlyCompanyMarkerBatch;
	private IAgStkGraphicsPrimitive	m_FriendlyPlatoonMarkerBatch;
	private IAgStkGraphicsPrimitive	m_FriendlySquadMarkerBatch;
	private IAgStkGraphicsPrimitive	m_FriendlySoldierMarkerBatch;
	private IAgStkGraphicsPrimitive	m_HostileBattalionMarkerBatch;
	private IAgStkGraphicsPrimitive	m_HostileCompanyMarkerBatch;
	private IAgStkGraphicsPrimitive	m_HostilePlatoonMarkerBatch;
	private IAgStkGraphicsPrimitive	m_HostileSquadMarkerBatch;
	private IAgStkGraphicsPrimitive	m_HostileSoldierMarkerBatch;

	private ArrayList<Interval>			m_Intervals;

	public MarkerBatchAggregationDeaggregationCodeSnippet(Component c)
	{
		super(c, "Draw and combine sets of marker batches at various distances", "primitives", "markerbatch", "MarkerBatchAggregationDeaggregationCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        // Set up positions for marker batches
        ArrayList<Object[]> friendlyBattalionPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> friendlyCompanyPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> friendlyPlatoonPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> friendlySquadPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> friendlySoldierPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> hostileBattalionPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> hostileCompanyPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> hostilePlatoonPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> hostileSquadPositions = new ArrayList<Object[]>();
        ArrayList<Object[]> hostileSoldierPositions = new ArrayList<Object[]>();

        int soldiersInSquad = 10;
        int squadsInPlatoon = 3;
        int platoonsInCompany = 4;
        int companiesInBattalion = 5;

        // Create friendly army
        createPositions(1,
                        friendlyBattalionPositions, friendlyCompanyPositions, friendlyPlatoonPositions, friendlySquadPositions, friendlySoldierPositions,
                        soldiersInSquad, squadsInPlatoon, platoonsInCompany, companiesInBattalion, root);

        IAgStkGraphicsMarkerBatchPrimitive friendlyBattalion = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] friendlyBattalionPositionsArray = this.convertPositionListToArray(friendlyBattalionPositions);
        friendlyBattalion.setCartographic("Earth", friendlyBattalionPositionsArray);

        IAgStkGraphicsMarkerBatchPrimitive friendlyCompany = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] friendlyCompanyPositionsArray = this.convertPositionListToArray(friendlyCompanyPositions);
        friendlyCompany.setCartographic("Earth", friendlyCompanyPositionsArray);

        IAgStkGraphicsMarkerBatchPrimitive friendlyPlatoon = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] friendlyPlatoonPositionsArray = this.convertPositionListToArray(friendlyPlatoonPositions);
        friendlyPlatoon.setCartographic("Earth", friendlyPlatoonPositionsArray);

        IAgStkGraphicsMarkerBatchPrimitive friendlySquad = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] friendlySquadPositionsArray = this.convertPositionListToArray(friendlySquadPositions);
        friendlySquad.setCartographic("Earth", friendlySquadPositionsArray);

        IAgStkGraphicsMarkerBatchPrimitive friendlySoldier = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] friendlySoldierPositionsArray = this.convertPositionListToArray(friendlySoldierPositions);
        friendlySoldier.setCartographic("Earth", friendlySoldierPositionsArray);

        // Create hostile army
        createPositions(2,
            hostileBattalionPositions, hostileCompanyPositions, hostilePlatoonPositions, hostileSquadPositions, hostileSoldierPositions,
            soldiersInSquad, squadsInPlatoon, platoonsInCompany, companiesInBattalion, root);

        IAgStkGraphicsMarkerBatchPrimitive hostileBattalion = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] hostileBattalionPositionsArray = this.convertPositionListToArray(hostileBattalionPositions);
        hostileBattalion.setCartographic("Earth", hostileBattalionPositionsArray);

        IAgStkGraphicsMarkerBatchPrimitive hostileCompany = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] hostileCompanyPositionsArray = this.convertPositionListToArray(hostileCompanyPositions);
        hostileCompany.setCartographic("Earth", hostileCompanyPositionsArray);

        IAgStkGraphicsMarkerBatchPrimitive hostilePlatoon = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] hostilePlatoonPositionsArray = this.convertPositionListToArray(hostilePlatoonPositions);
        hostilePlatoon.setCartographic("Earth", hostilePlatoonPositionsArray);

        IAgStkGraphicsMarkerBatchPrimitive hostileSquad = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] hostileSquadPositionsArray = this.convertPositionListToArray(hostileSquadPositions);
        hostileSquad.setCartographic("Earth", hostileSquadPositionsArray);

        IAgStkGraphicsMarkerBatchPrimitive hostileSoldier = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        Object[] hostileSoldierPositionsArray = this.convertPositionListToArray(hostileSoldierPositions);
        hostileSoldier.setCartographic("Earth", hostileSoldierPositionsArray);

        // View-distance levels
        int level0 = 0;
        int level1 = soldiersInSquad * 2500;
        int level2 = squadsInPlatoon * level1;
        int level3 = platoonsInCompany * level2;
        int level4 = companiesInBattalion * level3;
        int level5 = level4 * 5;

        // Set display conditions
        IAgStkGraphicsDistanceDisplayCondition battalionAltitude = manager.getInitializers().getDistanceDisplayCondition().initializeWithDistances(level4 + 0.01, level5);
        IAgStkGraphicsDistanceDisplayCondition companyAltitude = manager.getInitializers().getDistanceDisplayCondition().initializeWithDistances(level3 + 0.01, level4);
        IAgStkGraphicsDistanceDisplayCondition platoonAltitude = manager.getInitializers().getDistanceDisplayCondition().initializeWithDistances(level2 + 0.01, level3);
        IAgStkGraphicsDistanceDisplayCondition squadAltitude = manager.getInitializers().getDistanceDisplayCondition().initializeWithDistances(level1 + 0.01, level2);
        IAgStkGraphicsDistanceDisplayCondition soldierAltitude = manager.getInitializers().getDistanceDisplayCondition().initializeWithDistances(level0, level1);

        friendlyBattalion.setDistanceDisplayConditionPerMarker(battalionAltitude);
        friendlyCompany.setDistanceDisplayConditionPerMarker(companyAltitude);
        friendlyPlatoon.setDistanceDisplayConditionPerMarker(platoonAltitude);
        friendlySquad.setDistanceDisplayConditionPerMarker(squadAltitude);
        friendlySoldier.setDistanceDisplayConditionPerMarker(soldierAltitude);
        hostileBattalion.setDistanceDisplayConditionPerMarker(battalionAltitude);
        hostileCompany.setDistanceDisplayConditionPerMarker(companyAltitude);
        hostilePlatoon.setDistanceDisplayConditionPerMarker(platoonAltitude);
        hostileSquad.setDistanceDisplayConditionPerMarker(squadAltitude);
        hostileSoldier.setDistanceDisplayConditionPerMarker(soldierAltitude);

        friendlyBattalion.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        friendlyCompany.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        friendlyPlatoon.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        friendlySquad.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        friendlySoldier.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        hostileBattalion.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        hostileCompany.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        hostilePlatoon.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        hostileSquad.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);
        hostileSoldier.setRenderPass(AgEStkGraphicsMarkerBatchRenderPass.E_STK_GRAPHICS_MARKER_BATCH_RENDER_PASS_TRANSLUCENT);

        // Load the textures
        friendlyBattalion.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"friendly_battalion.png")));
        friendlyCompany.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"friendly_company.png")));
        friendlyPlatoon.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"friendly_platoon.png")));
        friendlySquad.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"friendly_squad.png")));
        friendlySoldier.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"friendly_soldier.png")));
        hostileBattalion.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"hostile_battalion.png")));
        hostileCompany.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"hostile_company.png")));
        hostilePlatoon.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"hostile_platoon.png")));
        hostileSquad.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"hostile_squad.png")));
        hostileSoldier.setTexture(manager.getTextures().loadFromStringUri(DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"hostile_soldier.png")));

        // Add marker batches to scene
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)friendlyBattalion);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)friendlyCompany);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)friendlyPlatoon);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)friendlySquad);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)friendlySoldier);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)hostileBattalion);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)hostileCompany);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)hostilePlatoon);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)hostileSquad);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)hostileSoldier);
        //#endregion

        m_FriendlyBattalionMarkerBatch = (IAgStkGraphicsPrimitive)friendlyBattalion;
        m_FriendlyCompanyMarkerBatch = (IAgStkGraphicsPrimitive)friendlyCompany;
        m_FriendlyPlatoonMarkerBatch = (IAgStkGraphicsPrimitive)friendlyPlatoon;
        m_FriendlySquadMarkerBatch = (IAgStkGraphicsPrimitive)friendlySquad;
        m_FriendlySoldierMarkerBatch = (IAgStkGraphicsPrimitive)friendlySoldier;
        m_HostileBattalionMarkerBatch = (IAgStkGraphicsPrimitive)hostileBattalion;
        m_HostileCompanyMarkerBatch = (IAgStkGraphicsPrimitive)hostileCompany;
        m_HostilePlatoonMarkerBatch = (IAgStkGraphicsPrimitive)hostilePlatoon;
        m_HostileSquadMarkerBatch = (IAgStkGraphicsPrimitive)hostileSquad;
        m_HostileSoldierMarkerBatch = (IAgStkGraphicsPrimitive)hostileSoldier;

    	OverlayHelper.addTextBox(this, manager, "Zoom in and out to see layers of markers based on distance. \r\nMIL-STD-2525B symbols are used to represent individual soldiers\r\nwhen zoomed in closest. As you zoom out, soldiers combine into \r\nsquads, platoons, companies, and finally battalions.\r\n\r\nThis aggregation/deaggregation technique is implemented by \r\nassigning a different DistanceDisplayCondition to each \r\nMarkerBatchPrimitive representing a different layer.");

        OverlayHelper.addDistanceOverlay(this, manager, scene);

        m_Intervals = new ArrayList<Interval>();

        m_Intervals.add(new Interval(level4 + 0.01, level5));
        m_Intervals.add(new Interval(level3 + 0.01, level4));
        m_Intervals.add(new Interval(level2 + 0.01, level3));
        m_Intervals.add(new Interval(level1 + 0.01, level2));
        m_Intervals.add(new Interval(level0, level1));

        OverlayHelper.getDistanceDisplay().addIntervals(m_Intervals);
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_FriendlyBattalionMarkerBatch != null)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
            Object[] center = (Object[])m_FriendlyBattalionMarkerBatch.getBoundingSphere().getCenter_AsObject();
            IAgStkGraphicsBoundingSphere boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(center, 500000);

            ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere);
            scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_FriendlyBattalionMarkerBatch);
			m_FriendlyBattalionMarkerBatch = null;
		}
		
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_FriendlyCompanyMarkerBatch);
			m_FriendlyPlatoonMarkerBatch = null;
		}
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_FriendlyPlatoonMarkerBatch);
			m_FriendlyBattalionMarkerBatch = null;
		}
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_FriendlySquadMarkerBatch);
			m_FriendlySquadMarkerBatch = null;
		}
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_FriendlySoldierMarkerBatch);
			m_FriendlySoldierMarkerBatch = null;
		}
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_HostileBattalionMarkerBatch);
			m_HostileBattalionMarkerBatch = null;
		}
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_HostileCompanyMarkerBatch);
			m_HostileCompanyMarkerBatch = null;
		}
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_HostilePlatoonMarkerBatch);
			m_HostilePlatoonMarkerBatch = null;
		}
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_HostileSquadMarkerBatch);
			m_HostileSquadMarkerBatch = null;
		}
		if(m_FriendlyBattalionMarkerBatch != null)
		{
			manager.getPrimitives().remove(m_HostileSoldierMarkerBatch);
			m_HostileSoldierMarkerBatch = null;
		}

		OverlayHelper.removeTextBox(manager);
		if(m_Intervals != null)
		{
			OverlayHelper.getDistanceDisplay().removeIntervals(m_Intervals);
			m_Intervals = null;
		}
		OverlayHelper.removeDistanceOverlay(this, manager);

		scene.render();
	}

	private static void createPositions(int seed, List<Object[]> battalionPositions, List<Object[]> companyPositions, List<Object[]> platoonPositions, List<Object[]> squadPositions,
	List<Object[]> soldierPositions, int soldiersInSquad, int squadsInPlatoon, int platoonsInCompany, int companiesInBattalion, AgStkObjectRootClass root)
	{
		Random r = new Random(seed);
		Object[] battalionsPt = new Object[] {new Double(33 + (r.nextDouble() - 0.5)), new Double(43 + (r.nextDouble() - 0.5)), new Double(0.0)};
		battalionPositions.add(battalionsPt);

		// A series of recursive for loops to generate a heirarchical structure
		for(int companies = 0; companies < companiesInBattalion; ++companies)
		{
			double companiesX = (r.nextDouble() - 0.5) / companiesInBattalion;
			double companiesY = (r.nextDouble() - 0.5) / companiesInBattalion;

			Object[] companiesPt = new Object[] {new Double(((Double)battalionsPt[0]).doubleValue() + companiesX), new Double(((Double)battalionsPt[1]).doubleValue() + companiesY), new Double(0.0)};

			companyPositions.add(companiesPt);

			for(int platoons = 0; platoons < platoonsInCompany; ++platoons)
			{
				double platoonsX = (r.nextDouble() - 0.5) / platoonsInCompany;
				double platoonsY = (r.nextDouble() - 0.5) / platoonsInCompany;

				Object[] platoonsPt = new Object[] {new Double(((Double)companiesPt[0]).doubleValue() + platoonsX), new Double(((Double)companiesPt[1]).doubleValue() + platoonsY), new Double(0.0)};

				platoonPositions.add(platoonsPt);

				for(int squads = 0; squads < squadsInPlatoon; ++squads)
				{
					double squadsX = (r.nextDouble() - 0.5) / squadsInPlatoon;
					double squadsY = (r.nextDouble() - 0.5) / squadsInPlatoon;

					Object[] squadsPt = new Object[] {new Double(((Double)platoonsPt[0]).doubleValue() + squadsX), new Double(((Double)platoonsPt[1]).doubleValue() + squadsY), new Double(0.0)};

					squadPositions.add(squadsPt);

					for(int soldiers = 0; soldiers < soldiersInSquad; ++soldiers)
					{
						double soldiersX = (r.nextDouble() - 0.5) / soldiersInSquad;
						double soldiersY = (r.nextDouble() - 0.5) / soldiersInSquad;

						Object[] soldiersPt = new Object[] {new Double(((Double)squadsPt[0]).doubleValue() + soldiersX), new Double(((Double)squadsPt[1]).doubleValue() + soldiersY), new Double(0.0)};

						soldierPositions.add(soldiersPt);
					}
				}
			}
		}
	}
}