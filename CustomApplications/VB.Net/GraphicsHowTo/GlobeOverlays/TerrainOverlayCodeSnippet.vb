#Region "UsingDirectives"
Imports System.IO
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace GlobeOverlays
	Class TerrainOverlayCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("GlobeOverlays\TerrainOverlayCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim terrainOverlayFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/St Helens.pdtt").FullPath
            ExecuteSnippet(scene, root, terrainOverlayFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddGlobeTerrain", _
            "Add terrain to the globe", _
            "Graphics | GlobeOverlays", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsGlobeImageOverlay" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("terrainOverlayFile", "The terrain overlay file")> ByVal terrainOverlayFile As String)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim globeOverlay As IAgStkGraphicsTerrainOverlay = Nothing
            Dim overlays As IAgStkGraphicsTerrainCollection = scene.CentralBodies.Earth.Terrain
            For Each overlay As IAgStkGraphicsTerrainOverlay In overlays
                If DirectCast(overlay, IAgStkGraphicsGlobeOverlay).UriAsString.EndsWith("St Helens.pdtt", StringComparison.Ordinal) Then
                    globeOverlay = overlay
                    Exit For
                End If
            Next

            '
            ' Don't load terrain if another code snippet already loaded it.
            '
            If globeOverlay Is Nothing Then
                Try
                    '#Region "CodeSnippet"
                    Dim overlay As IAgStkGraphicsTerrainOverlay = scene.CentralBodies.Earth.Terrain.AddUriString( _
                        terrainOverlayFile)
                    '#End Region

                    m_Overlay = overlay
                Catch
                    MessageBox.Show("Could not create globe overlay.  Your video card may not support this feature.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            Else
                m_Overlay = globeOverlay
            End If
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing Then
				scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
				scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed
				Dim extent As Array = DirectCast(m_Overlay, IAgStkGraphicsGlobeOverlay).Extent
                ViewHelper.ViewExtent(scene, root, "Earth", extent, 45, 15)

				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing Then
				scene.CentralBodies("Earth").Terrain.Remove(m_Overlay)
				scene.Render()

				m_Overlay = Nothing
			End If
		End Sub

		Private m_Overlay As IAgStkGraphicsTerrainOverlay
	End Class
End Namespace
