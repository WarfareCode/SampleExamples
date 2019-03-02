#region UsingDirectives
using System;
using System.Collections.Generic;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives.MarkerBatch
{
    class MarkerBatchAggregationDeaggregationCodeSnippet : CodeSnippet
    {
        public MarkerBatchAggregationDeaggregationCodeSnippet()
            : base(@"Primitives\MarkerBatch\MarkerBatchAggregationDeaggregationCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string friendlyBattalionTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_battalion.png").FullPath;
            string friendlyCompanyTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_company.png").FullPath;
            string friendlyPlatoonTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_platoon.png").FullPath;
            string friendlySquadTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_squad.png").FullPath;
            string friendlySoldierTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_soldier.png").FullPath;
            string hostileBattalionTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_battalion.png").FullPath;
            string hostileCompanyTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_company.png").FullPath;
            string hostilePlatoonTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_platoon.png").FullPath;
            string hostileSquadTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_squad.png").FullPath;
            string hostileSoldierTexture = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_soldier.png").FullPath;
            Execute(scene, root, friendlyBattalionTexture, friendlyCompanyTexture, friendlyPlatoonTexture, friendlySquadTexture, friendlySoldierTexture, hostileBattalionTexture, hostileCompanyTexture, hostilePlatoonTexture, hostileSquadTexture, hostileSoldierTexture);
        }

        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("friendlyBattalionTexture", "The texture to use for the Friendly Battalion")] string friendlyBattalionTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("friendlyCompanyTexture", "The texture to use for the Friendly Company")] string friendlyCompanyTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("friendlyPlatoonTexture", "The texture to use for the Friendly Platoon")] string friendlyPlatoonTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("friendlySquadTexture", "The texture to use for the Friendly Squad")] string friendlySquadTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("friendlySoldierTexture", "The texture to use for the Friendly Soldier")] string friendlySoldierTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("hostileBattalionTexture", "The texture to use for the Hostile Battalion")] string hostileBattalionTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("hostileCompanyTexture", "The texture to use for the Hostile Company")] string hostileCompanyTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("hostilePlatoonTexture", "The texture to use for the Hostile Platoon")] string hostilePlatoonTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("hostileSquadTexture", "The texture to use for the Hostile Squad")] string hostileSquadTexture, [AGI.CodeSnippets.CodeSnippet.Parameter("hostileSoldierTexture", "The texture to use for the Hostile Soldier")] string hostileSoldierTexture)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            //
            // Set up positions for marker batches
            //
            IList<Array> friendlyBattalionPositions = new List<Array>();
            IList<Array> friendlyCompanyPositions = new List<Array>();
            IList<Array> friendlyPlatoonPositions = new List<Array>();
            IList<Array> friendlySquadPositions = new List<Array>();
            IList<Array> friendlySoldierPositions = new List<Array>();
            IList<Array> hostileBattalionPositions = new List<Array>();
            IList<Array> hostileCompanyPositions = new List<Array>();
            IList<Array> hostilePlatoonPositions = new List<Array>();
            IList<Array> hostileSquadPositions = new List<Array>();
            IList<Array> hostileSoldierPositions = new List<Array>();

            int soldiersInSquad = /*$solidersPerSquad$The number of soldiers that make up a squad$*/10;
            int squadsInPlatoon = /*$squadsPerPlatoon$The number of squads that make up a platoon$*/3;
            int platoonsInCompany = /*$platoonsPerCompany$The number of platoons that make up a company$*/4;
            int companiesInBattalion = /*$companiesPerBattalion$The number of companies that make up a battalion$*/5;

            //
            // Create friendly army
            //
            CreatePositions(1,
                            friendlyBattalionPositions, friendlyCompanyPositions, friendlyPlatoonPositions, friendlySquadPositions, friendlySoldierPositions,
                            soldiersInSquad, squadsInPlatoon, platoonsInCompany, companiesInBattalion, root);

            IAgStkGraphicsMarkerBatchPrimitive friendlyBattalion = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array friendlyBattalionPositionsArray = ConvertIListToArray(friendlyBattalionPositions);
            friendlyBattalion.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref friendlyBattalionPositionsArray);

            IAgStkGraphicsMarkerBatchPrimitive friendlyCompany = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array friendlyCompanyPositionsArray = ConvertIListToArray(friendlyCompanyPositions);
            friendlyCompany.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref friendlyCompanyPositionsArray);

            IAgStkGraphicsMarkerBatchPrimitive friendlyPlatoon = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array friendlyPlatoonPositionsArray = ConvertIListToArray(friendlyPlatoonPositions);
            friendlyPlatoon.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref friendlyPlatoonPositionsArray);

            IAgStkGraphicsMarkerBatchPrimitive friendlySquad = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array friendlySquadPositionsArray = ConvertIListToArray(friendlySquadPositions);
            friendlySquad.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref friendlySquadPositionsArray);

            IAgStkGraphicsMarkerBatchPrimitive friendlySoldier = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array friendlySoldierPositionsArray = ConvertIListToArray(friendlySoldierPositions);
            friendlySoldier.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref friendlySoldierPositionsArray);

            //
            // Create hostile army
            //
            CreatePositions(2,
                hostileBattalionPositions, hostileCompanyPositions, hostilePlatoonPositions, hostileSquadPositions, hostileSoldierPositions,
                soldiersInSquad, squadsInPlatoon, platoonsInCompany, companiesInBattalion, root);

            IAgStkGraphicsMarkerBatchPrimitive hostileBattalion = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array hostileBattalionPositionsArray = ConvertIListToArray(hostileBattalionPositions);
            hostileBattalion.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref hostileBattalionPositionsArray);

            IAgStkGraphicsMarkerBatchPrimitive hostileCompany = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array hostileCompanyPositionsArray = ConvertIListToArray(hostileCompanyPositions);
            hostileCompany.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref hostileCompanyPositionsArray);

            IAgStkGraphicsMarkerBatchPrimitive hostilePlatoon = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array hostilePlatoonPositionsArray = ConvertIListToArray(hostilePlatoonPositions);
            hostilePlatoon.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref hostilePlatoonPositionsArray);

            IAgStkGraphicsMarkerBatchPrimitive hostileSquad = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array hostileSquadPositionsArray = ConvertIListToArray(hostileSquadPositions);
            hostileSquad.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref hostileSquadPositionsArray);

            IAgStkGraphicsMarkerBatchPrimitive hostileSoldier = manager.Initializers.MarkerBatchPrimitive.Initialize();
            Array hostileSoldierPositionsArray = ConvertIListToArray(hostileSoldierPositions);
            hostileSoldier.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref hostileSoldierPositionsArray);

            //
            // View-distance levels
            //
            int level0 = 0;
            int level1 = soldiersInSquad * 2500;
            int level2 = squadsInPlatoon * level1;
            int level3 = platoonsInCompany * level2;
            int level4 = companiesInBattalion * level3;
            int level5 = level4 * 5;

            //
            // Set display conditions
            //
            IAgStkGraphicsDistanceDisplayCondition battalionAltitude = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level4 + 0.01, level5);
            IAgStkGraphicsDistanceDisplayCondition companyAltitude = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level3 + 0.01, level4);
            IAgStkGraphicsDistanceDisplayCondition platoonAltitude = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level2 + 0.01, level3);
            IAgStkGraphicsDistanceDisplayCondition squadAltitude = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level1 + 0.01, level2);
            IAgStkGraphicsDistanceDisplayCondition soldierAltitude = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level0, level1);

            friendlyBattalion.DistanceDisplayConditionPerMarker = battalionAltitude;
            friendlyCompany.DistanceDisplayConditionPerMarker = companyAltitude;
            friendlyPlatoon.DistanceDisplayConditionPerMarker = platoonAltitude;
            friendlySquad.DistanceDisplayConditionPerMarker = squadAltitude;
            friendlySoldier.DistanceDisplayConditionPerMarker = soldierAltitude;
            hostileBattalion.DistanceDisplayConditionPerMarker = battalionAltitude;
            hostileCompany.DistanceDisplayConditionPerMarker = companyAltitude;
            hostilePlatoon.DistanceDisplayConditionPerMarker = platoonAltitude;
            hostileSquad.DistanceDisplayConditionPerMarker = squadAltitude;
            hostileSoldier.DistanceDisplayConditionPerMarker = soldierAltitude;

            friendlyBattalion.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            friendlyCompany.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            friendlyPlatoon.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            friendlySquad.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            friendlySoldier.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            hostileBattalion.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            hostileCompany.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            hostilePlatoon.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            hostileSquad.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;
            hostileSoldier.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;

            //
            // Load the textures
            //
            friendlyBattalion.Texture = manager.Textures.LoadFromStringUri(friendlyBattalionTexture);
            friendlyCompany.Texture = manager.Textures.LoadFromStringUri(friendlyCompanyTexture);
            friendlyPlatoon.Texture = manager.Textures.LoadFromStringUri(friendlyPlatoonTexture);
            friendlySquad.Texture = manager.Textures.LoadFromStringUri(friendlySquadTexture);
            friendlySoldier.Texture = manager.Textures.LoadFromStringUri(friendlySoldierTexture);
            hostileBattalion.Texture = manager.Textures.LoadFromStringUri(hostileBattalionTexture);
            hostileCompany.Texture = manager.Textures.LoadFromStringUri(hostileCompanyTexture);
            hostilePlatoon.Texture = manager.Textures.LoadFromStringUri(hostilePlatoonTexture);
            hostileSquad.Texture = manager.Textures.LoadFromStringUri(hostileSquadTexture);
            hostileSoldier.Texture = manager.Textures.LoadFromStringUri(hostileSoldierTexture);

            //
            // Add marker batches to scene
            //
            manager.Primitives.Add((IAgStkGraphicsPrimitive)friendlyBattalion);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)friendlyCompany);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)friendlyPlatoon);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)friendlySquad);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)friendlySoldier);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)hostileBattalion);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)hostileCompany);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)hostilePlatoon);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)hostileSquad);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)hostileSoldier);
#endregion

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

            OverlayHelper.AddTextBox(
@"Zoom in and out to see layers of markers based on distance. 
MIL-STD-2525B symbols are used to represent individual soldiers
when zoomed in closest. As you zoom out, soldiers combine into 
squads, platoons, companies, and finally battalions.

This aggregation/deaggregation technique is implemented by 
assigning a different DistanceDisplayCondition to each 
MarkerBatchPrimitive representing a different layer.", manager);

            OverlayHelper.AddDistanceOverlay(scene, manager);

            m_Intervals = new List<Interval>();

            m_Intervals.Add(new Interval(level4 + 0.01, level5));
            m_Intervals.Add(new Interval(level3 + 0.01, level4));
            m_Intervals.Add(new Interval(level2 + 0.01, level3));
            m_Intervals.Add(new Interval(level1 + 0.01, level2));
            m_Intervals.Add(new Interval(level0, level1));

            OverlayHelper.DistanceDisplay.AddIntervals(m_Intervals);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_FriendlyBattalionMarkerBatch != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                Array center = m_FriendlyBattalionMarkerBatch.BoundingSphere.Center;
                IAgStkGraphicsBoundingSphere boundingSphere = manager.Initializers.BoundingSphere.Initialize(
                    ref center, 500000);

                ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_FriendlyBattalionMarkerBatch);
            manager.Primitives.Remove(m_FriendlyCompanyMarkerBatch);
            manager.Primitives.Remove(m_FriendlyPlatoonMarkerBatch);
            manager.Primitives.Remove(m_FriendlySquadMarkerBatch);
            manager.Primitives.Remove(m_FriendlySoldierMarkerBatch);
            manager.Primitives.Remove(m_HostileBattalionMarkerBatch);
            manager.Primitives.Remove(m_HostileCompanyMarkerBatch);
            manager.Primitives.Remove(m_HostilePlatoonMarkerBatch);
            manager.Primitives.Remove(m_HostileSquadMarkerBatch);
            manager.Primitives.Remove(m_HostileSoldierMarkerBatch);

            OverlayHelper.RemoveTextBox(manager);
            OverlayHelper.DistanceDisplay.RemoveIntervals(m_Intervals);
            OverlayHelper.RemoveDistanceOverlay(manager);

            scene.Render();

            m_FriendlyBattalionMarkerBatch = null;
            m_FriendlyCompanyMarkerBatch = null;
            m_FriendlyPlatoonMarkerBatch = null;
            m_FriendlySquadMarkerBatch = null;
            m_FriendlySoldierMarkerBatch = null;
            m_HostileBattalionMarkerBatch = null;
            m_HostileCompanyMarkerBatch = null;
            m_HostilePlatoonMarkerBatch = null;
            m_HostileSquadMarkerBatch = null;
            m_HostileSoldierMarkerBatch = null;
        }

        private static void CreatePositions(int seed,
                                     IList<Array> battalionPositions,
                                     IList<Array> companyPositions,
                                     IList<Array> platoonPositions,
                                     IList<Array> squadPositions,
                                     IList<Array> soldierPositions,
                                     int soldiersInSquad, int squadsInPlatoon, int platoonsInCompany, int companiesInBattalion,
                                     AgStkObjectRoot root)
        {
            Random r = new Random(seed);
            Array battalionsPt = new object[3] {
                        33 + (r.NextDouble() - 0.5),
                        43 + (r.NextDouble() - 0.5),
                        0.0};
            battalionPositions.Add(battalionsPt);

            //
            // A series of recursive for loops to generate a heirarchical structure
            //
            for (int companies = 0; companies < companiesInBattalion; ++companies)
            {
                double companiesX = (r.NextDouble() - 0.5) / companiesInBattalion;
                double companiesY = (r.NextDouble() - 0.5) / companiesInBattalion;

                Array companiesPt = new object[3] {
                        (double)battalionsPt.GetValue(0) + companiesX,
                        (double)battalionsPt.GetValue(1) + companiesY,
                        0.0};

                companyPositions.Add(companiesPt);

                for (int platoons = 0; platoons < platoonsInCompany; ++platoons)
                {
                    double platoonsX = (r.NextDouble() - 0.5) / platoonsInCompany;
                    double platoonsY = (r.NextDouble() - 0.5) / platoonsInCompany;

                    Array platoonsPt = new object[3] {
                            (double)companiesPt.GetValue(0) + platoonsX,
                            (double)companiesPt.GetValue(1) + platoonsY,
                            0.0};

                    platoonPositions.Add(platoonsPt);

                    for (int squads = 0; squads < squadsInPlatoon; ++squads)
                    {
                        double squadsX = (r.NextDouble() - 0.5) / squadsInPlatoon;
                        double squadsY = (r.NextDouble() - 0.5) / squadsInPlatoon;

                        Array squadsPt = new object[3] {
                                (double)platoonsPt.GetValue(0) + squadsX,
                                (double)platoonsPt.GetValue(1) + squadsY,
                                0.0};


                        squadPositions.Add(squadsPt);

                        for (int soldiers = 0; soldiers < soldiersInSquad; ++soldiers)
                        {
                            double soldiersX = (r.NextDouble() - 0.5) / soldiersInSquad;
                            double soldiersY = (r.NextDouble() - 0.5) / soldiersInSquad;

                            Array soldiersPt = new object[3] {
                                    (double)squadsPt.GetValue(0) + soldiersX,
                                    (double)squadsPt.GetValue(1) + soldiersY,
                                    0.0};

                            soldierPositions.Add(soldiersPt);
                        }
                    }
                }
            }
        }

        private IAgStkGraphicsPrimitive m_FriendlyBattalionMarkerBatch;
        private IAgStkGraphicsPrimitive m_FriendlyCompanyMarkerBatch;
        private IAgStkGraphicsPrimitive m_FriendlyPlatoonMarkerBatch;
        private IAgStkGraphicsPrimitive m_FriendlySquadMarkerBatch;
        private IAgStkGraphicsPrimitive m_FriendlySoldierMarkerBatch;
        private IAgStkGraphicsPrimitive m_HostileBattalionMarkerBatch;
        private IAgStkGraphicsPrimitive m_HostileCompanyMarkerBatch;
        private IAgStkGraphicsPrimitive m_HostilePlatoonMarkerBatch;
        private IAgStkGraphicsPrimitive m_HostileSquadMarkerBatch;
        private IAgStkGraphicsPrimitive m_HostileSoldierMarkerBatch;

        private List<Interval> m_Intervals;

    };
}
