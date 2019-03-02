#Region "UsingDirectives"

Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects

#End Region

Namespace Imaging
	Public Class ImageDynamicCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Imaging\ImageDynamicCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim imageFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/SpinSat_256.gif").FullPath
            ExecuteSnippet(scene, root, imageFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DisplayARasterStream", _
            "Load and display a raster stream", _
            "Graphics | Imaging", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsRasterStream" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "The image file")> ByVal imageFile As String)
            Try
                '#Region "CodeSnippet"
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
                Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

                '
                ' Create the RasterStream from the plugin
                '
                Dim activator As IAgStkGraphicsProjectionRasterStreamPluginActivator = manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize()
                Dim proxy As IAgStkGraphicsProjectionRasterStreamPluginProxy = activator.CreateFromDisplayName(attachID("$pluginDisplayName$DisplayName of the ProjectionRasterStreamPlugin$", "ProjectionRasterStreamPlugin.VBNET"))

                '
                ' Use reflection to set the plugin's properties
                '
                Dim plugin As Type = proxy.RealPluginObject.[GetType]()
                plugin.GetProperty("RasterPath").SetValue(proxy.RealPluginObject, imageFile, Nothing)

                Dim rasterStream As IAgStkGraphicsRasterStream = proxy.RasterStream
                rasterStream.UpdateDelta = attachID("$updateDelta$The interval at which the raster is updated$", 0.01667)

                '
                ' Creates the texture overlay
                '
                Dim texture As IAgStkGraphicsRendererTexture2D = manager.Textures.FromRaster(DirectCast(rasterStream, IAgStkGraphicsRaster))
                Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(attachID("$xLocation$The x location of the screen overlay$", 0), attachID("$yLocation$The y location of the screen overlay$", 0), attachID("$overlayWidth$The width of the screen overlay$", texture.Template.Width), attachID("$overlayHeight$The height of the screen overlay$", texture.Template.Height))
                overlay.Texture = texture
                DirectCast(overlay, IAgStkGraphicsOverlay).Origin = attachID("$overlayOrigin$The origin of the screen overlay$", AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterLeft)

                overlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))

                '#End Region

                OverlayHelper.LabelOverlay(DirectCast(overlay, IAgStkGraphicsScreenOverlay), "Raster Stream", manager)

                m_Overlay = DirectCast(overlay, IAgStkGraphicsScreenOverlay)

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
			Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
			animation.PlayForward()
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
			animation.Rewind()
			overlayManager.Remove(m_Overlay)
			scene.Render()

			m_Overlay = Nothing
		End Sub

		Private m_Overlay As IAgStkGraphicsScreenOverlay
	End Class
End Namespace
