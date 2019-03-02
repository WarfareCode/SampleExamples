#Region "UsingDirectives"
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.TriangleMesh
	Class TriangleMeshEllipseCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\TriangleMesh\TriangleMeshEllipseCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAnEllipse", _
            "Draw a filled ellipse on the globe", _
            "Graphics | Primitives | Triangle Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim washingtonDC As Array = New Object() {attachID("$lat$The latitude of the ellipse$", 38.85), attachID("$lon$The longitude of the center of the ellipse$", -77.04), attachID("$alt$The altitude of the center of the ellipse$", 3000.0)}

            Dim shape As IAgStkGraphicsSurfaceShapesResult = manager.Initializers.SurfaceShapes.ComputeEllipseCartographic(attachID("$planetName$The planet on which the ellipse will be placed$", "Earth"), washingtonDC, attachID("$majorAxisRadius$The radius of the major axis of the ellipse$", 45000), attachID("$minorAxisRadius$The radius of the minor axis of the ellipse$", 30000), 45)
            Dim positions As Array = shape.Positions

            Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfacePolygonTriangulator.Compute(attachID("$planetName$The planet on which the ellipse will be placed$", "Earth"), positions)
            Dim mesh As IAgStkGraphicsTriangleMeshPrimitive = manager.Initializers.TriangleMeshPrimitive.Initialize()
            mesh.SetTriangulator(DirectCast(triangles, IAgStkGraphicsTriangulatorResult))
            DirectCast(mesh, IAgStkGraphicsPrimitive).Color = attachID("$color$The System.Drawing.Color of the ellipse$", Color.Cyan)

            manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("Boundary positions for an ellipse are computed with " & vbCrLf & _
                                     "SurfaceShapes.ComputeEllipseCartographic.  Triangles for the ellipse's" & vbCrLf & _
                                     "interior are then computed with SurfacePolygonTriangulator.Compute and " & vbCrLf & _
                                     "visualized with a TriangleMeshPrimitive.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere)
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Primitive)
			m_Primitive = Nothing

			OverlayHelper.RemoveTextBox(manager)
			scene.Render()
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
	End Class
End Namespace
