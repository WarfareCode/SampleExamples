#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Imaging
	Class ImageSwizzleCodeSnippet
		Inherits CodeSnippet

		Public Sub New()
            MyBase.New("Imaging\ImageSwizzleCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim imageFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/f-22a_raptor.png").FullPath
            ExecuteSnippet(scene, root, imageFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "SwizzleAnImage", _
            "Swizzle an image's components", _
            "Graphics | Imaging", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsBandOrderFilter" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "The image file")> ByVal imageFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
            '
            ' The URI can be a file path, http, https, or ftp location
            '
            Dim image As IAgStkGraphicsRaster = manager.Initializers.Raster.InitializeWithStringUri( _
                imageFile)

            '
            ' Swizzle RGBA to BGRA
            '
            Dim channelOrder As IAgStkGraphicsBandOrderFilter = manager.Initializers.BandOrderFilter.InitializeWithOrder(attachID("$newChannelOrder$The new order of the raster's channels$", AgEStkGraphicsRasterFormat.eStkGraphicsRasterFormatBgra))
            image.ApplyInPlace(DirectCast(channelOrder, IAgStkGraphicsRasterFilter))

            Dim texture As IAgStkGraphicsRendererTexture2D = manager.Textures.FromRaster(image)

            Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.Initialize()
            DirectCast(overlay, IAgStkGraphicsOverlay).Width = attachID("$overlayWidth$The width of the screen overlay$", 0.2)
            DirectCast(overlay, IAgStkGraphicsOverlay).WidthUnit = attachID("$overlayWidthUnit$The width unit of the screen overlay$", AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction)
            DirectCast(overlay, IAgStkGraphicsOverlay).Height = attachID("$overlayHeight$The height of the screen overlay$", 0.2)
            DirectCast(overlay, IAgStkGraphicsOverlay).HeightUnit = attachID("$overlayHeightUnit$The height of the screen overlay$", AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction)
            overlay.Texture = texture
            DirectCast(overlay, IAgStkGraphicsOverlay).Origin = attachID("$overlayOrigin$The origin of the screen overlay$", AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter)

            overlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))
            '#End Region
            OverlayHelper.AddOriginalImageOverlay(manager)
            OverlayHelper.LabelOverlay(DirectCast(overlay, IAgStkGraphicsScreenOverlay), "Channels Swizzled", manager)
            m_Overlay = DirectCast(overlay, IAgStkGraphicsScreenOverlay)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Remove(m_Overlay)
			OverlayHelper.RemoveOriginalImageOverlay(manager)
			scene.Render()

			m_Overlay = Nothing
		End Sub

		Private m_Overlay As IAgStkGraphicsScreenOverlay
	End Class
End Namespace
