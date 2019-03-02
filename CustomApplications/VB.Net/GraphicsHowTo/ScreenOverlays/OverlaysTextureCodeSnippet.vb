#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace ScreenOverlays
	Class OverlaysTextureCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("ScreenOverlays\OverlaysTextureCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim imageFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/agi_logo_transparent.png").FullPath
            ExecuteSnippet(scene, root, imageFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddALogo", _
            "Add a company logo with a texture overlay", _
            "Graphics | ScreenOverlays", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsScreenOverlay" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "The image file")> ByVal imageFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

            Dim texture2D As IAgStkGraphicsRendererTexture2D = manager.Textures.LoadFromStringUri( _
                imageFile)

            Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(attachID("$xLocation$The X location of the screen overlay$", 10), attachID("$yLocation$The yLocation of the screen overlay$", 0), texture2D.Template.Width / 2, texture2D.Template.Height / 2)
            DirectCast(overlay, IAgStkGraphicsOverlay).X = 10
            DirectCast(overlay, IAgStkGraphicsOverlay).XUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels
            DirectCast(overlay, IAgStkGraphicsOverlay).Translucency = attachID("$translucency$The translucency of the screen overlay$", 0.1F)
            DirectCast(overlay, IAgStkGraphicsOverlay).Origin = attachID("$origin$The origin of the screen overlay$", AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomRight)
            overlay.Texture = texture2D

            overlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))
            '#End Region

            m_Overlay = DirectCast(overlay, IAgStkGraphicsScreenOverlay)
            OverlayHelper.AddTextBox("TextureScreenOverlay can be used to overlay a company logo by loading " & vbCrLf & _
                                     "the logo image into a Texture2D and providing it to a TextureScreenOverlay.", manager)
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

				overlayManager.Remove(m_Overlay)
				m_Overlay = Nothing

				OverlayHelper.RemoveTextBox(manager)
				scene.Render()
			End If
		End Sub

		Private m_Overlay As IAgStkGraphicsScreenOverlay
	End Class
End Namespace
