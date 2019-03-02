#Region "UsingDirectives"

Imports System.IO
Imports System.Drawing
Imports GraphicsHowTo.Imaging
Imports AGI.STKGraphics
Imports AGI.STKObjects

#End Region

Namespace ScreenOverlays
	Public Class OverlaysPanelCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("ScreenOverlays\OverlaysPanelCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim imageFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/originalLogo.png").FullPath
            Dim rasterFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/SpinSat_256.gif").FullPath
            ExecuteSnippet(scene, root, imageFile, rasterFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddAPanelOverlay", _
            "Add overlays to a panel overlay", _
            "Graphics | ScreenOverlays", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsScreenOverlay" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "The image file to use for the child overlay")> ByVal imageFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("rasterFile", "The raster file for the second child overlay")> ByVal rasterFile As String)
            Try
                '#Region "CodeSnippet"
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
                Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

                Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(attachID("$xLocation$The x location of the screen overlay$", 0), attachID("$yLocation$The y location of the screen overlay$", 0), attachID("$overlayWidth$The width of the screen overlay$", 188), attachID("$overlayHeight$The height of the screen overlay$", 200))
                DirectCast(overlay, IAgStkGraphicsOverlay).Origin = attachID("$origin$The origin of the screen overlay$", AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft)
                DirectCast(overlay, IAgStkGraphicsOverlay).BorderTranslucency = attachID("$borderTranslucency$The translucency of the border$", 0.3F)
                DirectCast(overlay, IAgStkGraphicsOverlay).BorderSize = attachID("$borderWidth$The width of the border$", 1)
                DirectCast(overlay, IAgStkGraphicsOverlay).BorderColor = attachID("$borderColor$The System.Drawing.Color of the border$", Color.LightBlue)
                DirectCast(overlay, IAgStkGraphicsOverlay).Color = attachID("$color$The System.Drawing.Color of the screen overlay$", Color.LightSkyBlue)
                DirectCast(overlay, IAgStkGraphicsOverlay).Translucency = attachID("$translucency$The translucency of the screen overlay$", 0.7F)

                Dim childOverlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(attachID("$childXLocation$The x location of the child overlay$", 0), attachID("$childYLocation$The y location of the child overlay$", 0), DirectCast(overlay, IAgStkGraphicsOverlay).Width, DirectCast(overlay, IAgStkGraphicsOverlay).Height)
                DirectCast(childOverlay, IAgStkGraphicsOverlay).Origin = attachID("$childOrigin$The origin of the child overlay$", AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenter)

                childOverlay.Texture = manager.Textures.LoadFromStringUri(imageFile)

                '
                ' Create the RasterStream from the plugin the same way as in ImageDynamicCodeSnippet.cs
                '
                Dim activator As IAgStkGraphicsProjectionRasterStreamPluginActivator = manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize()
                Dim proxy As IAgStkGraphicsProjectionRasterStreamPluginProxy = activator.CreateFromDisplayName("ProjectionRasterStreamPlugin.VBNET")
                Dim plugin As Type = proxy.RealPluginObject.[GetType]()
                plugin.GetProperty("RasterPath").SetValue(proxy.RealPluginObject, rasterFile, Nothing)

                Dim rasterStream As IAgStkGraphicsRasterStream = proxy.RasterStream
                rasterStream.UpdateDelta = 0.01667
                Dim texture2D As IAgStkGraphicsRendererTexture2D = manager.Textures.FromRaster(DirectCast(rasterStream, IAgStkGraphicsRaster))

                Dim secondChildOverlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 0, 128, 128)
                DirectCast(secondChildOverlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight
                DirectCast(secondChildOverlay, IAgStkGraphicsOverlay).TranslationX = -36
                DirectCast(secondChildOverlay, IAgStkGraphicsOverlay).TranslationY = -18
                DirectCast(secondChildOverlay, IAgStkGraphicsOverlay).ClipToParent = False
                secondChildOverlay.Texture = texture2D

                overlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))

                Dim parentOverlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(DirectCast(overlay, IAgStkGraphicsOverlay).Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
                parentOverlayManager.Add(DirectCast(childOverlay, IAgStkGraphicsScreenOverlay))

                Dim childOverlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(DirectCast(childOverlay, IAgStkGraphicsOverlay).Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
                childOverlayManager.Add(DirectCast(secondChildOverlay, IAgStkGraphicsScreenOverlay))

                '#End Region

                m_Overlay = DirectCast(overlay, IAgStkGraphicsScreenOverlay)
                DirectCast(root, IAgAnimation).PlayForward()

            Catch e As Exception
                If e.Message.Contains("ProjectionRasterStreamPlugin") Then
                    MessageBox.Show("A COM exception has occurred." & vbLf & vbLf & _
                                    "It is possible that one of the following may be the issue:" & vbLf & vbLf & _
                                    "1. ProjectionRasterStreamPlugin.dll is not registered for COM interop." & vbLf & vbLf & _
                                    "2. That the plugin has not been added to the GfxPlugin category within a <install dir>\Plugins\*.xml file." & vbLf & vbLf & _
                                    "To resolve either of these issues:" & vbLf & vbLf & _
                                    "1. To register the plugin, open a Visual Studio " & _
                                    If(IntPtr.Size = 8, "x64 ", "") & "Command Prompt and execute the command:" & vbLf & vbLf & _
                                    vbTab & "regasm /codebase ""<install dir>\<CodeSamples>\Extend\Graphics\VB.Net\ProjectionRasterStreamPlugin\bin\<Config>\ProjectionRasterStreamPlugin.dll""" & vbLf & vbLf & _
                                    vbTab & "Note: if you do not have access to a Visual Studio Command Prompt regasm can be found here:" & vbLf & _
                                    vbTab & "C:\Windows\Microsoft.NET\Framework" & If(IntPtr.Size = 8, "64", "") & "\<.NET Version>\" & vbLf & vbLf & _
                                    "2. To add it to the GfxPlugins plugins registry category:" & vbLf & vbLf & _
                                    vbTab & "a. Copy the Graphics.xml from the <install dir>\CodeSamples\Extend\Graphics\Graphics.xml file to the <install dir>\Plugins directory." & vbLf & vbLf & _
                                    vbTab & "b. Then uncomment the plugin entry that contains a display name of ProjectionRasterStreamPlugin.VBNET." & vbLf & vbLf, "Plugin Not Registered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show("Could not create globe overlay.  Your video card may not support this feature.", _
                                    "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Return
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
