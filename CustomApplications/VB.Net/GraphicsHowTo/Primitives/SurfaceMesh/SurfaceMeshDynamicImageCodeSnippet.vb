Imports System.Windows.Forms

#Region "UsingDirectives"
Imports System.IO
Imports System.Runtime.InteropServices
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Primitives.SurfaceMesh
	Class SurfaceMeshDynamicImageCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\SurfaceMesh\SurfaceMeshDynamicImageCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim rasterFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/lava.gif").FullPath
            ExecuteSnippet(scene, root, rasterFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawADynamicallyTexturedExtentOnTerrain", _
            "Draw a filled, dynamically textured extent on terrain", _
            "Graphics | Primitives | Surface Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("rasterFile", "The file to use as the raster")> ByVal rasterFile As String)
            Dim manager2 As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            If Not manager2.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod() Then
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            Dim overlay As IAgStkGraphicsTerrainOverlay = Nothing
            Dim overlays As IAgStkGraphicsTerrainCollection = scene.CentralBodies.Earth.Terrain
            For Each eachOverlay As IAgStkGraphicsTerrainOverlay In overlays
                If DirectCast(eachOverlay, IAgStkGraphicsGlobeOverlay).UriAsString.EndsWith("St Helens.pdtt", StringComparison.Ordinal) Then
                    overlay = eachOverlay
                    Exit For
                End If
            Next

            '
            ' Don't load terrain if another code snippet already loaded it.
            '
            If overlay Is Nothing Then
                overlay = scene.CentralBodies.Earth.Terrain.AddUriString( _
                    New AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/St Helens.pdtt").FullPath)
            End If

            Try
                '#Region "CodeSnippet"
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

                Dim activator As IAgStkGraphicsProjectionRasterStreamPluginActivator = manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize()
                Dim proxy As IAgStkGraphicsProjectionRasterStreamPluginProxy = activator.CreateFromDisplayName(attachID("$pluginDisplayName$Display Name of the ProjectionRasterStreamPlugin$", "ProjectionRasterStreamPlugin.VBNET"))

                '
                ' Use reflection to set the plugin's properties
                '
                Dim plugin As Type = proxy.RealPluginObject.[GetType]()
                plugin.GetProperty("RasterPath").SetValue(proxy.RealPluginObject, rasterFile, Nothing)

                Dim rasterStream As IAgStkGraphicsRasterStream = proxy.RasterStream
                rasterStream.UpdateDelta = attachID("$updateDelta$The interval at which the Update method will be called$", 0.025)

                Dim texture As IAgStkGraphicsRendererTexture2D = manager.Textures.FromRaster(DirectCast(rasterStream, IAgStkGraphicsRaster))
                Dim extent As Array = DirectCast(attachID("$terrainOverlay$The terrain overlay$", overlay), IAgStkGraphicsGlobeOverlay).Extent
                Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple("Earth", extent)
                Dim mesh As IAgStkGraphicsSurfaceMeshPrimitive = manager.Initializers.SurfaceMeshPrimitive.Initialize()
                DirectCast(mesh, IAgStkGraphicsPrimitive).Translucency = attachID("$translucency$The translucency of the surface mesh$", 0.2F)
                mesh.Texture = texture
                mesh.Set(triangles)
                manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
                '#End Region

                m_Overlay = overlay
                m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
                OverlayHelper.AddTextBox("Dynamic textures are created by creating a class that derives " & vbCrLf & _
                                         "from RasterStream that provides time dependent textures.", manager)

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
			If m_Overlay IsNot Nothing Then
				Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)

				scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
				scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

                ViewHelper.ViewExtent(scene, root, "Earth", DirectCast(m_Overlay, IAgStkGraphicsGlobeOverlay).Extent, -135, 30)

				animation.PlayForward()
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing AndAlso m_Primitive IsNot Nothing Then
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
				Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)

				animation.Rewind()
				scene.CentralBodies("Earth").Terrain.Remove(m_Overlay)
				manager.Primitives.Remove(m_Primitive)
				m_Primitive = Nothing
				m_Overlay = Nothing

				OverlayHelper.RemoveTextBox(manager)
				scene.Render()
			End If
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
		Private m_Overlay As IAgStkGraphicsTerrainOverlay
	End Class
End Namespace
