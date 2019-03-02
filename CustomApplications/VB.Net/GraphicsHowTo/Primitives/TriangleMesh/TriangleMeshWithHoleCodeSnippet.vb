#Region "UsingDirectives"
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.TriangleMesh
	Class TriangleMeshWithHoleCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\TriangleMesh\TriangleMeshWithHoleCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim positions As Array = STKUtil.ReadAreaTargetPoints(New AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/LogoBoundary.at").FullPath, root)
            Dim holePositions As Array = STKUtil.ReadAreaTargetPoints(New AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/LogoHole.at").FullPath, root)
            ExecuteSnippet(scene, root, positions, holePositions)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAFilledPolygonWithHole", _
            "Draw a filled polygon with a hole on the globe", _
            "Graphics | Primitives | Triangle Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("positions", "The positions used to compute triangulation")> ByVal positions As Array, <AGI.CodeSnippets.CodeSnippet.Parameter("holePositions", "The positions of the hole")> ByVal holePositions As Array)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfacePolygonTriangulator.ComputeWithHole(attachID("$planetName$The planet on which the triangle mesh will be placed$", "Earth"), positions, holePositions)

            Dim mesh As IAgStkGraphicsTriangleMeshPrimitive = manager.Initializers.TriangleMeshPrimitive.Initialize()
            mesh.SetTriangulator(DirectCast(triangles, IAgStkGraphicsTriangulatorResult))
            DirectCast(mesh, IAgStkGraphicsPrimitive).Color = attachID("$color$The System.Drawing.Color of the triangle mesh$", Color.Gray)
            DirectCast(mesh, IAgStkGraphicsPrimitive).Translucency = attachID("$translucency$The translucency of the triangle mesh$", 0.5F)
            manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))

            Dim boundaryLine As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.Initialize()
            Dim boundaryPositionsArray As Array = triangles.BoundaryPositions
            boundaryLine.Set(boundaryPositionsArray)
            DirectCast(boundaryLine, IAgStkGraphicsPrimitive).Color = attachID("$outlineColor$The color of the outline around the area target$", Color.Red)
            boundaryLine.Width = attachID("$outlineWidth$The width of the outline$", 2)
            manager.Primitives.Add(DirectCast(boundaryLine, IAgStkGraphicsPrimitive))

            Dim holeLine As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.Initialize()
            holeLine.Set(holePositions)
            DirectCast(holeLine, IAgStkGraphicsPrimitive).Color = attachID("$holeOutlineColor$The System.Drawing.Color of the hole's outline$", Color.Red)
            holeLine.Width = attachID("$holeOutlineWidth$The width of the hole's outline$", 2)
            manager.Primitives.Add(DirectCast(holeLine, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
            m_BoundaryLine = DirectCast(boundaryLine, IAgStkGraphicsPrimitive)
            m_HoleLine = DirectCast(holeLine, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("SurfacePolygonTriangulator.Compute has overloads that take the exterior " & vbCrLf & _
                                     "boundary positions as well as positions for an interior hole.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere)
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Primitive)
			manager.Primitives.Remove(m_BoundaryLine)
			manager.Primitives.Remove(m_HoleLine)
			m_Primitive = Nothing
			m_BoundaryLine = Nothing
			m_HoleLine = Nothing

			OverlayHelper.RemoveTextBox(manager)
			scene.Render()
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
		Private m_BoundaryLine As IAgStkGraphicsPrimitive
		Private m_HoleLine As IAgStkGraphicsPrimitive
	End Class
End Namespace
