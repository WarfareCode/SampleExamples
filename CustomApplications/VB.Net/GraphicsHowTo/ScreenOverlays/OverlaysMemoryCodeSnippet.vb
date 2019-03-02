#Region "UsingDirectives"
Imports System.Drawing
Imports System.Collections.Generic
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
#End Region

Namespace ScreenOverlays
	Class OverlaysMemoryCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("ScreenOverlays\OverlaysMemoryCodeSnippet.vb")
		End Sub

        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

            Dim size As Integer = attachID("$size$The size of the screen overlay$", 256)
            Dim tex As New ProcedurallyGeneratedTexture(size)

            ' Create a Bitmap and BitmapData and Lock all pixels to be written
            Dim bitmap As New Bitmap(size, size)
            Dim bitmapData As BitmapData = bitmap.LockBits(New Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat)

            ' Copy the data from the byte array into BitmapData.Scan0
            Dim data As Byte() = tex.Next()
            data = tex.Next()
            Marshal.Copy(data, 0, bitmapData.Scan0, data.Length)

            ' Unlock the pixels and save temporarily
            bitmap.UnlockBits(bitmapData)
            bitmap.Save(imagePath)

            Dim img As IAgStkGraphicsRaster = manager.Initializers.Raster.InitializeWithStringUriXYWidthAndHeight(imagePath, 0, 0, size, size)

            Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 0, size, size)
            overlay.Texture = manager.Textures.FromRaster(DirectCast(img, IAgStkGraphicsRaster))
            DirectCast(overlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenter

            overlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))
            scene.Render()
            '#End Region
            m_Overlay = DirectCast(overlay, IAgStkGraphicsScreenOverlay)
            System.IO.File.Delete(imagePath)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			' Use current view.
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

			overlayManager.Remove(m_Overlay)
			scene.Render()

			m_Overlay = Nothing
		End Sub

		Private m_Overlay As IAgStkGraphicsScreenOverlay
		Private imagePath As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "OverlayFromMemory.bmp").FullPath
	End Class
End Namespace
