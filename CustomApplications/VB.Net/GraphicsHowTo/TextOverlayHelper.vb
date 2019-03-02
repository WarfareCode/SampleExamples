Imports System.Text
Imports System.Drawing
Imports AGI.STKGraphics

Public NotInheritable Class TextOverlayHelper
	Private Sub New()
	End Sub
	Public Shared Function CreateTextOverlay(text As String, font As Font, manager As IAgStkGraphicsSceneManager) As IAgStkGraphicsTextureScreenOverlay
		Dim textBitmapPath As String = CreateTextBitmap(text, font)
		Dim textTexture As IAgStkGraphicsRendererTexture2D = manager.Textures.LoadFromStringUri(textBitmapPath)

		Dim textOverlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 0, textTexture.Template.Width, textTexture.Template.Height)
		textOverlay.Texture = textTexture
        textOverlay.TextureFilter = manager.Initializers.TextureFilter2D.NearestClampToEdge

        System.IO.File.Delete(textBitmapPath)

        Return textOverlay
    End Function

	Public Shared Function CreateTextBitmap(text As String, font As Font) As String
		Dim textSize As Size = CodeSnippet.MeasureString(text, font)
		Dim textBitmap As New Bitmap(textSize.Width, textSize.Height)
		Dim gfx As Graphics = Graphics.FromImage(textBitmap)
		gfx.DrawString(text, font, Brushes.White, New PointF(0, 0))

		Dim filePath As String = GenerateUniqueFilename()
		textBitmap.Save(filePath)

		Return filePath
	End Function

	Public Shared Sub UpdateTextOverlay(overlay As IAgStkGraphicsTextureScreenOverlay, text As String, font As Font, manager As IAgStkGraphicsSceneManager)
		Dim textBitmapPath As String = CreateTextBitmap(text, font)
		Dim updatedTextTexture As IAgStkGraphicsRendererTexture2D = manager.Textures.LoadFromStringUri(textBitmapPath)
		System.IO.File.Delete(textBitmapPath)

        DirectCast(overlay, IAgStkGraphicsOverlay).Size = New Object() {updatedTextTexture.Template.Width, updatedTextTexture.Template.Height, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}
		Dim temp As IAgStkGraphicsRendererTexture2D = overlay.Texture
        overlay.Texture = updatedTextTexture

        'calling this here improves memory performance
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(temp)
	End Sub

	Private Shared Function GenerateUniqueFilename() As String
		Dim filename As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "TextOverlay").FullPath & fileNumber.ToString() & ".bmp"
        fileNumber += CUInt(1)

		If Not System.IO.File.Exists(filename) Then
			Return filename
		Else
			Return GenerateUniqueFilename()
		End If
	End Function

	Private Shared fileNumber As UInteger = 0
End Class
