#Region "UsingDirectives"
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.Polyline
	Class PolylineCircleCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Polyline\PolylineCircleCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawACircle", _
            "Draw the outline of a circle on the globe", _
            "Graphics | Primitives | Polyline Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim philadelphia As Array = New Object() {attachID("$lat$The latitude of the center point$", 39.88), attachID("$lon$The longitude of the center point$", -75.25), attachID("$alt$The altitude of the center point$", 0.0)}

            Dim shape As IAgStkGraphicsSurfaceShapesResult = manager.Initializers.SurfaceShapes.ComputeCircleCartographic(attachID("$planetName$The planet on which the circle will be placed$", "Earth"), philadelphia, attachID("$radius$The radius of the circle$", 10000))
            Dim positions As Array = shape.Positions

            Dim line As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.InitializeWithType(shape.PolylineType)
            line.Set(positions)
            line.Width = attachID("$width$The width of the polyline that makes up the circle$", 2)
            DirectCast(line, IAgStkGraphicsPrimitive).Color = attachID("$color$The color of the circle$", Color.White)

            manager.Primitives.Add(DirectCast(line, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(line, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("SurfaceShapes.ComputeCircleCartographic is used to compute the positions " & vbCrLf & _
                                     "of a circle on the surface, which is visualized with the polyline primitive.", manager)
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
