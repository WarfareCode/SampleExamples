#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports System.Windows.Forms
#End Region

Namespace Primitives.SurfaceMesh
	Class SurfaceMeshTexturedExtentCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\SurfaceMesh\SurfaceMeshTexturedExtentCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim textureFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/originalLogo.png").FullPath
            ExecuteSnippet(scene, root, textureFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAFilledTexturedExtentOnTerrain", _
            "Draw a filled, textured extent on terrain", _
            "Graphics | Primitives | Surface Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("textureFile", "The file to use as the texture of the surface mesh")> ByVal textureFile As String)
            Dim videoCheck As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            If Not videoCheck.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod() Then
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            Dim overlay As IAgStkGraphicsTerrainOverlay = Nothing
            Dim overlays As IAgStkGraphicsTerrainCollection = scene.CentralBodies.Earth.Terrain
            For Each eachOverlay As IAgStkGraphicsGlobeOverlay In overlays
                If eachOverlay.UriAsString.EndsWith("St Helens.pdtt", StringComparison.Ordinal) Then
                    overlay = DirectCast(eachOverlay, IAgStkGraphicsTerrainOverlay)
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

            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim overlayExtent As Array = DirectCast(attachID("$terrainOverlay$The terrain overlay$", overlay), IAgStkGraphicsGlobeOverlay).Extent
            Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple(attachID("$planetName$The planet on which the surface mesh will be placed$", "Earth"), overlayExtent)
            Dim texture As IAgStkGraphicsRendererTexture2D = manager.Textures.LoadFromStringUri( _
                textureFile)
            Dim mesh As IAgStkGraphicsSurfaceMeshPrimitive = manager.Initializers.SurfaceMeshPrimitive.Initialize()
            DirectCast(mesh, IAgStkGraphicsPrimitive).Translucency = attachID("$translucency$The translucency of the surface mesh$", 0.3F)
            mesh.Texture = texture
            mesh.Set(triangles)
            manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
            '#End Region

            m_Overlay = DirectCast(overlay, IAgStkGraphicsGlobeOverlay)
            m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("The surface mesh's Texture property is used to visualize a texture " & vbCrLf & _
                                     "conforming to terrain.  For high resolution imagery, it is recommended " & vbCrLf & _
                                     "to use a globe overlay.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing Then
				scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
				scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

                ViewHelper.ViewExtent(scene, root, "Earth", m_Overlay.Extent, -135, 30)
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing AndAlso m_Primitive IsNot Nothing Then
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
				scene.CentralBodies("Earth").Terrain.Remove(DirectCast(m_Overlay, IAgStkGraphicsTerrainOverlay))
				manager.Primitives.Remove(m_Primitive)
				m_Primitive = Nothing
				m_Overlay = Nothing

				OverlayHelper.RemoveTextBox(manager)
				scene.Render()
			End If
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
		Private m_Overlay As IAgStkGraphicsGlobeOverlay
	End Class
End Namespace
