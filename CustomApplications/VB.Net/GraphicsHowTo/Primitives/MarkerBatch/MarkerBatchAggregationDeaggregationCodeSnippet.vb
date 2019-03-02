#Region "UsingDirectives"
Imports System.Collections.Generic
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.MarkerBatch
	Class MarkerBatchAggregationDeaggregationCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\MarkerBatch\MarkerBatchAggregationDeaggregationCodeSnippet.vb")
		End Sub

        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            '
            ' Set up positions for marker batches
            '
            Dim friendlyBattalionPositions As IList(Of Array) = New List(Of Array)()
            Dim friendlyCompanyPositions As IList(Of Array) = New List(Of Array)()
            Dim friendlyPlatoonPositions As IList(Of Array) = New List(Of Array)()
            Dim friendlySquadPositions As IList(Of Array) = New List(Of Array)()
            Dim friendlySoldierPositions As IList(Of Array) = New List(Of Array)()
            Dim hostileBattalionPositions As IList(Of Array) = New List(Of Array)()
            Dim hostileCompanyPositions As IList(Of Array) = New List(Of Array)()
            Dim hostilePlatoonPositions As IList(Of Array) = New List(Of Array)()
            Dim hostileSquadPositions As IList(Of Array) = New List(Of Array)()
            Dim hostileSoldierPositions As IList(Of Array) = New List(Of Array)()

            Dim soldiersInSquad As Integer = attachID("$solidersPerSquad$The number of soldiers that make up a squad$", 10)
            Dim squadsInPlatoon As Integer = attachID("$squadsPerPlatoon$The number of squads that make up a platoon$", 3)
            Dim platoonsInCompany As Integer = attachID("$platoonsPerCompany$The number of platoons that make up a company$", 4)
            Dim companiesInBattalion As Integer = attachID("$companiesPerBattalion$The number of companies that make up a battalion$", 5)

            '
            ' Create friendly army
            '
            CreatePositions(1, friendlyBattalionPositions, friendlyCompanyPositions, friendlyPlatoonPositions, friendlySquadPositions, friendlySoldierPositions, _
             soldiersInSquad, squadsInPlatoon, platoonsInCompany, companiesInBattalion, root)

            Dim friendlyBattalion As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim friendlyBattalionPositionsArray As Array = ConvertIListToArray(friendlyBattalionPositions)
            friendlyBattalion.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), friendlyBattalionPositionsArray)

            Dim friendlyCompany As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim friendlyCompanyPositionsArray As Array = ConvertIListToArray(friendlyCompanyPositions)
            friendlyCompany.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), friendlyCompanyPositionsArray)

            Dim friendlyPlatoon As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim friendlyPlatoonPositionsArray As Array = ConvertIListToArray(friendlyPlatoonPositions)
            friendlyPlatoon.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), friendlyPlatoonPositionsArray)

            Dim friendlySquad As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim friendlySquadPositionsArray As Array = ConvertIListToArray(friendlySquadPositions)
            friendlySquad.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), friendlySquadPositionsArray)

            Dim friendlySoldier As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim friendlySoldierPositionsArray As Array = ConvertIListToArray(friendlySoldierPositions)
            friendlySoldier.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), friendlySoldierPositionsArray)

            '
            ' Create hostile army
            '
            CreatePositions(2, hostileBattalionPositions, hostileCompanyPositions, hostilePlatoonPositions, hostileSquadPositions, hostileSoldierPositions, _
             soldiersInSquad, squadsInPlatoon, platoonsInCompany, companiesInBattalion, root)

            Dim hostileBattalion As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim hostileBattalionPositionsArray As Array = ConvertIListToArray(hostileBattalionPositions)
            hostileBattalion.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), hostileBattalionPositionsArray)

            Dim hostileCompany As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim hostileCompanyPositionsArray As Array = ConvertIListToArray(hostileCompanyPositions)
            hostileCompany.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), hostileCompanyPositionsArray)

            Dim hostilePlatoon As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim hostilePlatoonPositionsArray As Array = ConvertIListToArray(hostilePlatoonPositions)
            hostilePlatoon.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), hostilePlatoonPositionsArray)

            Dim hostileSquad As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim hostileSquadPositionsArray As Array = ConvertIListToArray(hostileSquadPositions)
            hostileSquad.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), hostileSquadPositionsArray)

            Dim hostileSoldier As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            Dim hostileSoldierPositionsArray As Array = ConvertIListToArray(hostileSoldierPositions)
            hostileSoldier.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), hostileSoldierPositionsArray)

            '
            ' View-distance levels
            '
            Dim level0 As Integer = 0
            Dim level1 As Integer = soldiersInSquad * 2500
            Dim level2 As Integer = squadsInPlatoon * level1
            Dim level3 As Integer = platoonsInCompany * level2
            Dim level4 As Integer = companiesInBattalion * level3
            Dim level5 As Integer = level4 * 5

            '
            ' Set display conditions
            '
            Dim battalionAltitude As IAgStkGraphicsDistanceDisplayCondition = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level4 + 0.01, level5)
            Dim companyAltitude As IAgStkGraphicsDistanceDisplayCondition = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level3 + 0.01, level4)
            Dim platoonAltitude As IAgStkGraphicsDistanceDisplayCondition = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level2 + 0.01, level3)
            Dim squadAltitude As IAgStkGraphicsDistanceDisplayCondition = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level1 + 0.01, level2)
            Dim soldierAltitude As IAgStkGraphicsDistanceDisplayCondition = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(level0, level1)

            friendlyBattalion.DistanceDisplayConditionPerMarker = battalionAltitude
            friendlyCompany.DistanceDisplayConditionPerMarker = companyAltitude
            friendlyPlatoon.DistanceDisplayConditionPerMarker = platoonAltitude
            friendlySquad.DistanceDisplayConditionPerMarker = squadAltitude
            friendlySoldier.DistanceDisplayConditionPerMarker = soldierAltitude
            hostileBattalion.DistanceDisplayConditionPerMarker = battalionAltitude
            hostileCompany.DistanceDisplayConditionPerMarker = companyAltitude
            hostilePlatoon.DistanceDisplayConditionPerMarker = platoonAltitude
            hostileSquad.DistanceDisplayConditionPerMarker = squadAltitude
            hostileSoldier.DistanceDisplayConditionPerMarker = soldierAltitude

            friendlyBattalion.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            friendlyCompany.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            friendlyPlatoon.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            friendlySquad.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            friendlySoldier.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            hostileBattalion.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            hostileCompany.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            hostilePlatoon.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            hostileSquad.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent
            hostileSoldier.RenderPass = AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent

            '
            ' Load the textures
            '
            friendlyBattalion.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_battalion.png").FullPath)
            friendlyCompany.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_company.png").FullPath)
            friendlyPlatoon.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_platoon.png").FullPath)
            friendlySquad.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_squad.png").FullPath)
            friendlySoldier.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/friendly_soldier.png").FullPath)
            hostileBattalion.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_battalion.png").FullPath)
            hostileCompany.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_company.png").FullPath)
            hostilePlatoon.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_platoon.png").FullPath)
            hostileSquad.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_squad.png").FullPath)
            hostileSoldier.Texture = manager.Textures.LoadFromStringUri(New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/hostile_soldier.png").FullPath)

            '
            ' Add marker batches to scene
            '
            manager.Primitives.Add(DirectCast(friendlyBattalion, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(friendlyCompany, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(friendlyPlatoon, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(friendlySquad, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(friendlySoldier, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(hostileBattalion, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(hostileCompany, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(hostilePlatoon, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(hostileSquad, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(hostileSoldier, IAgStkGraphicsPrimitive))
            '#End Region

            m_FriendlyBattalionMarkerBatch = DirectCast(friendlyBattalion, IAgStkGraphicsPrimitive)
            m_FriendlyCompanyMarkerBatch = DirectCast(friendlyCompany, IAgStkGraphicsPrimitive)
            m_FriendlyPlatoonMarkerBatch = DirectCast(friendlyPlatoon, IAgStkGraphicsPrimitive)
            m_FriendlySquadMarkerBatch = DirectCast(friendlySquad, IAgStkGraphicsPrimitive)
            m_FriendlySoldierMarkerBatch = DirectCast(friendlySoldier, IAgStkGraphicsPrimitive)
            m_HostileBattalionMarkerBatch = DirectCast(hostileBattalion, IAgStkGraphicsPrimitive)
            m_HostileCompanyMarkerBatch = DirectCast(hostileCompany, IAgStkGraphicsPrimitive)
            m_HostilePlatoonMarkerBatch = DirectCast(hostilePlatoon, IAgStkGraphicsPrimitive)
            m_HostileSquadMarkerBatch = DirectCast(hostileSquad, IAgStkGraphicsPrimitive)
            m_HostileSoldierMarkerBatch = DirectCast(hostileSoldier, IAgStkGraphicsPrimitive)

            OverlayHelper.AddTextBox("Zoom in and out to see layers of markers based on distance. " & vbCrLf & _
                                     "MIL-STD-2525B symbols are used to represent individual soldiers" & vbCrLf & _
                                     "when zoomed in closest. As you zoom out, soldiers combine into " & vbCrLf & _
                                     "squads, platoons, companies, and finally battalions." & vbCrLf & vbCrLf & _
                                     "This aggregation/deaggregation technique is implemented by " & vbCrLf & _
                                     "assigning a different DistanceDisplayCondition to each " & vbCrLf & _
                                     "MarkerBatchPrimitive representing a different layer.", manager)

            OverlayHelper.AddDistanceOverlay(scene, manager)

            m_Intervals = New List(Of Interval)()

            m_Intervals.Add(New Interval(level4 + 0.01, level5))
            m_Intervals.Add(New Interval(level3 + 0.01, level4))
            m_Intervals.Add(New Interval(level2 + 0.01, level3))
            m_Intervals.Add(New Interval(level1 + 0.01, level2))
            m_Intervals.Add(New Interval(level0, level1))

            OverlayHelper.DistanceDisplay.AddIntervals(m_Intervals)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_FriendlyBattalionMarkerBatch IsNot Nothing Then
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
				Dim center As Array = m_FriendlyBattalionMarkerBatch.BoundingSphere.Center
				Dim boundingSphere As IAgStkGraphicsBoundingSphere = manager.Initializers.BoundingSphere.Initialize(center, 500000)

				ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere)
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_FriendlyBattalionMarkerBatch)
			manager.Primitives.Remove(m_FriendlyCompanyMarkerBatch)
			manager.Primitives.Remove(m_FriendlyPlatoonMarkerBatch)
			manager.Primitives.Remove(m_FriendlySquadMarkerBatch)
			manager.Primitives.Remove(m_FriendlySoldierMarkerBatch)
			manager.Primitives.Remove(m_HostileBattalionMarkerBatch)
			manager.Primitives.Remove(m_HostileCompanyMarkerBatch)
			manager.Primitives.Remove(m_HostilePlatoonMarkerBatch)
			manager.Primitives.Remove(m_HostileSquadMarkerBatch)
			manager.Primitives.Remove(m_HostileSoldierMarkerBatch)

			OverlayHelper.RemoveTextBox(manager)
			OverlayHelper.DistanceDisplay.RemoveIntervals(m_Intervals)
			OverlayHelper.RemoveDistanceOverlay(manager)

			scene.Render()

			m_FriendlyBattalionMarkerBatch = Nothing
			m_FriendlyCompanyMarkerBatch = Nothing
			m_FriendlyPlatoonMarkerBatch = Nothing
			m_FriendlySquadMarkerBatch = Nothing
			m_FriendlySoldierMarkerBatch = Nothing
			m_HostileBattalionMarkerBatch = Nothing
			m_HostileCompanyMarkerBatch = Nothing
			m_HostilePlatoonMarkerBatch = Nothing
			m_HostileSquadMarkerBatch = Nothing
			m_HostileSoldierMarkerBatch = Nothing
		End Sub

		Private Shared Sub CreatePositions(seed As Integer, battalionPositions As IList(Of Array), companyPositions As IList(Of Array), platoonPositions As IList(Of Array), squadPositions As IList(Of Array), soldierPositions As IList(Of Array), _
			soldiersInSquad As Integer, squadsInPlatoon As Integer, platoonsInCompany As Integer, companiesInBattalion As Integer, root As AgStkObjectRoot)
			Dim r As New Random(seed)
            Dim battalionsPt As Array = New Object() {33 + (r.NextDouble() - 0.5), 43 + (r.NextDouble() - 0.5), 0.0}
            battalionPositions.Add(battalionsPt)

            '
            ' A series of recursive for loops to generate a heirarchical structure
            '
            For companies As Integer = 0 To companiesInBattalion - 1
                Dim companiesX As Double = (r.NextDouble() - 0.5) / companiesInBattalion
                Dim companiesY As Double = (r.NextDouble() - 0.5) / companiesInBattalion

                Dim companiesPt As Array = New Object() {CDbl(battalionsPt.GetValue(0)) + companiesX, CDbl(battalionsPt.GetValue(1)) + companiesY, 0.0}

                companyPositions.Add(companiesPt)

                For platoons As Integer = 0 To platoonsInCompany - 1
                    Dim platoonsX As Double = (r.NextDouble() - 0.5) / platoonsInCompany
                    Dim platoonsY As Double = (r.NextDouble() - 0.5) / platoonsInCompany

                    Dim platoonsPt As Array = New Object() {CDbl(companiesPt.GetValue(0)) + platoonsX, CDbl(companiesPt.GetValue(1)) + platoonsY, 0.0}

                    platoonPositions.Add(platoonsPt)

                    For squads As Integer = 0 To squadsInPlatoon - 1
                        Dim squadsX As Double = (r.NextDouble() - 0.5) / squadsInPlatoon
                        Dim squadsY As Double = (r.NextDouble() - 0.5) / squadsInPlatoon

                        Dim squadsPt As Array = New Object() {CDbl(platoonsPt.GetValue(0)) + squadsX, CDbl(platoonsPt.GetValue(1)) + squadsY, 0.0}


                        squadPositions.Add(squadsPt)

                        For soldiers As Integer = 0 To soldiersInSquad - 1
                            Dim soldiersX As Double = (r.NextDouble() - 0.5) / soldiersInSquad
                            Dim soldiersY As Double = (r.NextDouble() - 0.5) / soldiersInSquad

                            Dim soldiersPt As Array = New Object() {CDbl(squadsPt.GetValue(0)) + soldiersX, CDbl(squadsPt.GetValue(1)) + soldiersY, 0.0}

                            soldierPositions.Add(soldiersPt)
                        Next
                    Next
                Next
            Next
		End Sub

		Private m_FriendlyBattalionMarkerBatch As IAgStkGraphicsPrimitive
		Private m_FriendlyCompanyMarkerBatch As IAgStkGraphicsPrimitive
		Private m_FriendlyPlatoonMarkerBatch As IAgStkGraphicsPrimitive
		Private m_FriendlySquadMarkerBatch As IAgStkGraphicsPrimitive
		Private m_FriendlySoldierMarkerBatch As IAgStkGraphicsPrimitive
		Private m_HostileBattalionMarkerBatch As IAgStkGraphicsPrimitive
		Private m_HostileCompanyMarkerBatch As IAgStkGraphicsPrimitive
		Private m_HostilePlatoonMarkerBatch As IAgStkGraphicsPrimitive
		Private m_HostileSquadMarkerBatch As IAgStkGraphicsPrimitive
		Private m_HostileSoldierMarkerBatch As IAgStkGraphicsPrimitive

		Private m_Intervals As List(Of Interval)

	End Class
End Namespace
