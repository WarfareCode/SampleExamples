#Region "UsingDirectives"
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.TriangleMesh
	Class TriangleMeshCircleCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\TriangleMesh\TriangleMeshCircleCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAFilledCircle", _
            "Draw a filled circle on the globe", _
            "Graphics | Primitives | Triangle Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            '$lat$new latitude$
            '$lon$new longitude$
            '$alt$new altitude$
            Dim center As Array = New Object() {attachID("$lat$The latitude of the center$", 39.88), attachID("$lon$The longitude of the center$", -75.25), attachID("$alt$The altitude of the center$", 0.0)}

            '$planetName$Name of the planet to place primitive$
            '$radius$The radius of the circle.$
            Dim shape As IAgStkGraphicsSurfaceShapesResult = manager.Initializers.SurfaceShapes.ComputeCircleCartographic(attachID("$planetName$Name of the planet to place primitive$", "Earth"), center, attachID("$radius$The radius of the circle$", 10000))
            Dim positions As Array = shape.Positions
            Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfacePolygonTriangulator.Compute(attachID("$planetName$Name of the planet to place primitive$", "Earth"), positions)

            Dim mesh As IAgStkGraphicsTriangleMeshPrimitive = manager.Initializers.TriangleMeshPrimitive.Initialize()
            mesh.SetTriangulator(DirectCast(triangles, IAgStkGraphicsTriangulatorResult))
            DirectCast(mesh, IAgStkGraphicsPrimitive).Color = attachID("$color$The System.Drawing.Color of the circle$", Color.White)
            DirectCast(mesh, IAgStkGraphicsPrimitive).Translucency = attachID("$translucency$The translucency of the circle$", 0.5F)

            manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("Boundary positions for a circle are computed with " & vbCrLf & _
                                     "SurfaceShapes.ComputeCircleCartographic.  Triangles for the circle's " & vbCrLf & _
                                     "interior are then computed with SurfacePolygonTriangulator.Compute " & vbCrLf & _
                                     "and visualized with a TriangleMeshPrimitive.", manager)
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
