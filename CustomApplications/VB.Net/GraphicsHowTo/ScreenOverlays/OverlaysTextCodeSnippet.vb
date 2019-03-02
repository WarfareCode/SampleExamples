#Region "UsingDirectives"
Imports System.Drawing
Imports System.Windows.Forms
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace ScreenOverlays
	Class OverlaysTextCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("ScreenOverlays\OverlaysTextCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim temporaryFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "TemoraryTextOverlay.bmp").FullPath
            ExecuteSnippet(scene, root, temporaryFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddATextOverlay", _
            "Write text to a texture overlay", _
            "Graphics | ScreenOverlays", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsScreenOverlay" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("temporaryFile", "The file to which the bitmap will be temporarily saved (should not exist yet)")> ByVal temporaryFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

            Dim font As New Font(attachID("$fontName$The name of the font to use$", "Arial"), attachID("$fontSize$The size of the font$", 12), CType(attachID("$fontStyle$The style of the font$", FontStyle.Bold), System.Drawing.FontStyle))
            Dim text As String = attachID("$text$The text to add to the screen overlay$", "Insight3D" & vbLf & "Analytical Graphics" & vbLf & "Overlays")
            Dim textSize As Size = MeasureString(text, font)
            Dim textBitmap As New Bitmap(textSize.Width, textSize.Height)
            Dim gfx As Graphics = Graphics.FromImage(textBitmap)
            gfx.DrawString(text, font, Brushes.White, New PointF(0, 0))

            Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(attachID("$xLocation$The X location of the screen overlay$", 10), attachID("$yLocation$The Y location of the screen overlay$", 10), textSize.Width, textSize.Height)
            DirectCast(overlay, IAgStkGraphicsOverlay).Origin = attachID("$origin$The origin of the screen overlay$", AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft)

            '
            ' Any bitmap can be written to a texture by temporarily saving the texture to disk.
            '
            Dim filePath As String = temporaryFile
            textBitmap.Save(filePath)
            overlay.Texture = manager.Textures.LoadFromStringUri(filePath)
            ' The temporary file is no longer required and can be deleted
            System.IO.File.Delete(filePath)

            overlay.TextureFilter = attachID("$textureFilter$The texture filter for the overlay$", manager.Initializers.TextureFilter2D.NearestClampToEdge)

            overlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))
            '#End Region

            m_Overlay = DirectCast(overlay, IAgStkGraphicsScreenOverlay)
            OverlayHelper.AddTextBox(".NET's Graphics.DrawString method can be used to write " & vbCrLf & _
                                     "text into Bitmap for use for the TextureScreenOverlay.", manager)
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
