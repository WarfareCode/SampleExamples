#Region "UsingDirectives"
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt
#End Region

Namespace Primitives.Polyline
	Class PolylineEllipseCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Polyline\PolylineEllipseCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAnEllipse", _
            "Draw the outline of an ellipse on the globe", _
            "Graphics | Primitives | Polyline Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim washingtonDC As Array = New Object() {attachID("$lat$The latitude of the center point$", 38.85), attachID("$lon$The longitude of the center point$", -77.04), attachID("$alt$The altitude of the center point$", 3000.0)}

            Dim shape As IAgStkGraphicsSurfaceShapesResult = manager.Initializers.SurfaceShapes.ComputeEllipseCartographic(attachID("$planetName$The planet on which the circle will be placed$", "Earth"), washingtonDC, attachID("$majorAxisRadius$The radius of the major axis$", 45000), attachID("$minorAxisRadius$The radius of the minor axis$", 30000), attachID("$bearing$The bearing of the ellipse$", 45))
            Dim positions As Array = shape.Positions

            Dim line As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.InitializeWithType(shape.PolylineType)
            line.Set(positions)
            DirectCast(line, IAgStkGraphicsPrimitive).Color = attachID("$color$The color of the ellipse$", Color.Cyan)

            manager.Primitives.Add(DirectCast(line, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(line, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("SurfaceShapes.ComputeEllipseCartographic is used to compute the " & vbCrLf & _
                                     "positions of an ellipse on the surface, which is visualized with " & vbCrLf & _
                                     "the polyline primitive.", manager)
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
