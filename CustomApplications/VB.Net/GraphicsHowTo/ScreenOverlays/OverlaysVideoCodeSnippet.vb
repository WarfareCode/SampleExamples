#Region "UsingDirectives"
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace ScreenOverlays
	Class OverlaysVideoCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("ScreenOverlays\OverlaysVideoCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim videoFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Videos/ShenzhouVII_BX1.wmv").FullPath
            ExecuteSnippet(scene, root, videoFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddAVideo", _
            "Add a video with a texture overlay", _
            "Graphics | ScreenOverlays", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsScreenOverlay" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("videoFile", "The video file")> ByVal videoFile As String)
            Try
                '#Region "CodeSnippet"
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
                Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

                Dim videoStream As IAgStkGraphicsVideoStream = manager.Initializers.VideoStream.InitializeWithStringUri(videoFile)
                videoStream.Playback = attachID("$playbackMode$The playback mode of the video$", AgEStkGraphicsVideoPlayback.eStkGraphicsVideoPlaybackRealTime)
                videoStream.[Loop] = attachID("$shouldLoop$Whether or not the video will return to the beginning when it reaches the end$", True)

                Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(attachID("$xLocation$The X location of the screen overlay$", 0), attachID("$yLocation$The Y location of the screen overlay$", 0), DirectCast(videoStream, IAgStkGraphicsRaster).Width / 4, DirectCast(videoStream, IAgStkGraphicsRaster).Height / 4)
                DirectCast(overlay, IAgStkGraphicsOverlay).BorderSize = attachID("$borderSize$The size of the border around the screen overlay$", 1)
                DirectCast(overlay, IAgStkGraphicsOverlay).BorderTranslucency = attachID("$borderTranslucency$The translucency of the border$", 0.3F)
                DirectCast(overlay, IAgStkGraphicsOverlay).Translucency = attachID("$translucency$The translucency of the screen overlay$", 0.3F)
                DirectCast(overlay, IAgStkGraphicsOverlay).Origin = attachID("$origin$The origin of the screen overlay$", AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight)
                overlay.Texture = manager.Textures.FromRaster(DirectCast(videoStream, IAgStkGraphicsRaster))

                overlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))
                '#End Region

                m_Overlay = DirectCast(overlay, IAgStkGraphicsScreenOverlay)
                DirectCast(root, IAgAnimation).PlayForward()
            Catch
                Dim message As New System.Text.StringBuilder("There was a problem accessing the video file located at: ")
                message.Append(videoFile)

                System.Windows.Forms.MessageBox.Show(message.ToString(), "Error finding the video")
            End Try
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			'
			' Overlays are always fixed to the screen regardless of view
			'
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing Then
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
				Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

				DirectCast(root, IAgAnimation).Rewind()
				overlayManager.Remove(m_Overlay)
				scene.Render()

				m_Overlay = Nothing
			End If
		End Sub

		Private m_Overlay As IAgStkGraphicsScreenOverlay
	End Class
End Namespace
