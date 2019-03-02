#Region "UsingDirectives"
Imports System.IO
Imports System.Windows.Forms
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace GlobeOverlays
	Class GlobeOverlayRenderOrderCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("GlobeOverlays\GlobeOverlayRenderOrderCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim topOverlayFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/top.jp2").FullPath
            Dim bottomOverlayFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/bottom.jp2").FullPath
            ExecuteSnippet(scene, root, topOverlayFile, bottomOverlayFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddOrderedGlobeImagery", _
            "Draw an image on top of another", _
            "Graphics | GlobeOverlays", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsGlobeImageOverlay" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("topOverlayFile", "The top globe overlay file")> ByVal topOverlayFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("bottomOverlayFile", "The bottom globe overlay file")> ByVal bottomOverlayFile As String)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Try
                '#Region "CodeSnippet"
                Dim topOverlay As IAgStkGraphicsGlobeImageOverlay = scene.CentralBodies.Earth.Imagery.AddUriString(topOverlayFile)
                Dim bottomOverlay As IAgStkGraphicsGlobeImageOverlay = scene.CentralBodies.Earth.Imagery.AddUriString(bottomOverlayFile)

                '
                ' Since bottom.jp2 was added after top.jp2, bottom.jp2 will be 
                ' drawn on top.  In order to draw top.jp2 on top, we swap the Overlays. 
                '
                scene.CentralBodies.Earth.Imagery.Swap(topOverlay, bottomOverlay)

                '#End Region

                m_TopOverlay = topOverlay
                m_BottomOverlay = bottomOverlay
            Catch
                MessageBox.Show("Could not create globe Overlay.  Your video card may not support this feature.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End Try

            OverlayHelper.AddTextBox("Swap, BringToFront, and SendToBack methods are used " & vbCrLf & _
                                     "to change the ordering of imagery on the globe.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_TopOverlay IsNot Nothing Then
				Dim top As Array = DirectCast(m_TopOverlay, IAgStkGraphicsGlobeOverlay).Extent
				Dim bottom As Array = DirectCast(m_BottomOverlay, IAgStkGraphicsGlobeOverlay).Extent

				scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
				scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

                ViewHelper.ViewExtent(scene, root, "Earth", Math.Min(CDbl(top.GetValue(0)), CDbl(bottom.GetValue(0))), Math.Min(CDbl(top.GetValue(1)), CDbl(bottom.GetValue(1))), Math.Max(CDbl(top.GetValue(2)), CDbl(bottom.GetValue(2))), _
                 Math.Max(CDbl(top.GetValue(3)), CDbl(bottom.GetValue(3))), -90, 25)

				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_TopOverlay IsNot Nothing Then
				scene.CentralBodies("Earth").Imagery.Remove(m_TopOverlay)
				scene.CentralBodies("Earth").Imagery.Remove(m_BottomOverlay)

				m_TopOverlay = Nothing
				m_BottomOverlay = Nothing
				OverlayHelper.RemoveTextBox(DirectCast(root.CurrentScenario, IAgScenario).SceneManager)

				scene.Render()
			End If
		End Sub

		Private m_TopOverlay As IAgStkGraphicsGlobeImageOverlay
		Private m_BottomOverlay As IAgStkGraphicsGlobeImageOverlay
	End Class
End Namespace
