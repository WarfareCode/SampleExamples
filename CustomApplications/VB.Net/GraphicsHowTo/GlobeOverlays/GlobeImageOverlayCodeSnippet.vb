#Region "UsingDirectives"
Imports System.IO
Imports System.Windows.Forms
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace GlobeOverlays
	Public Class GlobeImageOverlayCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("GlobeOverlays\GlobeImageOverlayCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim globeOverlayFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/St Helens.jp2").FullPath
            ExecuteSnippet(scene, root, globeOverlayFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddGlobeImagery", _
            "Add jp2 imagery to the globe", _
            "Graphics | GlobeOverlays", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsGlobeImageOverlay" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("globeOverlayFile", "The globe overlay file")> ByVal globeOverlayFile As String)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim globeOverlay As IAgStkGraphicsGlobeImageOverlay = Nothing
            Dim overlays As IAgStkGraphicsImageCollection = scene.CentralBodies.Earth.Imagery
            For Each overlay As IAgStkGraphicsGlobeOverlay In overlays
                If overlay.UriAsString IsNot Nothing AndAlso overlay.UriAsString.EndsWith("St Helens.jp2", StringComparison.Ordinal) Then
                    globeOverlay = DirectCast(overlay, IAgStkGraphicsGlobeImageOverlay)
                    Exit For
                End If
            Next

            '
            ' Don't load imagery if another code snippet already loaded it.
            '
            If globeOverlay Is Nothing Then
                Try
                    '#Region "CodeSnippet"
                    '
                    ' Either jp2 or pdttx can be used here
                    '
                    Dim overlay As IAgStkGraphicsGlobeImageOverlay = scene.CentralBodies.Earth.Imagery.AddUriString(globeOverlayFile)
                    '#End Region

                    m_overlays = overlay
                Catch
                    MessageBox.Show("Could not create globe overlays.  Your video card may not support this feature.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            Else
                m_overlays = globeOverlay
            End If
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_overlays IsNot Nothing Then
				scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
				scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed
				Dim extent As Array = DirectCast(m_overlays, IAgStkGraphicsGlobeOverlay).Extent
				scene.Camera.ViewExtent("Earth", extent)
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_overlays IsNot Nothing Then
				scene.CentralBodies("Earth").Imagery.Remove(m_overlays)
				scene.Render()

				m_overlays = Nothing
			End If
		End Sub

		Private m_overlays As IAgStkGraphicsGlobeImageOverlay
	End Class
End Namespace
