#Region "UsingDirectives"
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.SurfaceMesh
	Class SurfaceMeshAreaTargetCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\SurfaceMesh\SurfaceMeshAreaTargetCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim terrainFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/925.pdtt").FullPath
            Dim positions As Array = STKUtil.ReadAreaTargetPoints(New AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/925.at").FullPath, root)
            ExecuteSnippet(scene, root, terrainFile, positions)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAnAreaTargetOnTerrain", _
            "Draw a filled STK area target on terrain", _
            "Graphics | Primitives | Surface Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("terrainFile", "The terrain file")> ByVal terrainFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("positions", "The positions used to compute triangulation")> ByVal positions As Array)
            Dim videoCheck As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            If Not videoCheck.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod() Then
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim overlay As IAgStkGraphicsTerrainOverlay = scene.CentralBodies.Earth.Terrain.AddUriString( _
                terrainFile)

            Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfacePolygonTriangulator.Compute(attachID("$planetName$The planet on which the surface mesh is to be placed$", "Earth"), positions)

            Dim mesh As IAgStkGraphicsSurfaceMeshPrimitive = manager.Initializers.SurfaceMeshPrimitive.Initialize()
            DirectCast(mesh, IAgStkGraphicsPrimitive).Color = attachID("$color$The color of the surface mesh$", Color.Purple)
            mesh.Set(triangles)
            manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
            '#End Region

            m_Overlay = DirectCast(overlay, IAgStkGraphicsGlobeOverlay)
            m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("Similar to the triangle mesh example, triangles for the interior of an " & vbCrLf & _
                                     "STK area target are computed using SurfacePolygonTriangulator.Compute.  " & vbCrLf & _
                                     "This is used an input to a SurfaceMeshPrimitive, which makes the " & vbCrLf & _
                                     "visualization conform to terrain.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing Then
				scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
				scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

                ViewHelper.ViewExtent(scene, root, "Earth", m_Overlay.Extent, -45, 30)
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing Then
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
