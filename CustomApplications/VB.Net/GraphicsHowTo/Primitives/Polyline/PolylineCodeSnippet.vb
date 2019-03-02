#Region "UsingDirectives"
Imports System.Collections.Generic
Imports AGI.STKGraphics
Imports AGI.STKUtil
Imports AGI.STKObjects
#End Region

Namespace Primitives.Polyline
	Class PolylineCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Polyline\PolylineCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAPolyline", _
            "Draw a line between two points", _
            "Graphics | Primitives | Polyline Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim philadelphia As Array = New Object() {attachID("$startLat$The latitude of the start point$", 39.88), attachID("$startLon$The longitude of the start point$", -75.25), attachID("$startAlt$The altitude of the start point$", 3000.0)}
            Dim washingtonDC As Array = New Object() {attachID("$endLat$The latitude of the end point$", 38.85), attachID("$endLon$The longitude of the end point$", -77.04), attachID("$endAlt$The altitude of the end point$", 3000.0)}

            Dim positions As Array = New Object(5) {}
            philadelphia.CopyTo(positions, 0)
            washingtonDC.CopyTo(positions, 3)

            Dim line As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.Initialize()
            '$planetName$Name of the planet to place primitive$
            line.SetCartographic(attachID("$planetName$Name of the planet to place primitive$", "Earth"), positions)
            manager.Primitives.Add(DirectCast(line, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(line, IAgStkGraphicsPrimitive)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
			scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

			Dim fit As Double = 1.0
			'for helping fit the line into the extent
			Dim extent As Array = New Object() {38.85 - fit, -77.04 - fit, 39.88 + fit, -75.25 + fit}

            'scene.Camera.ViewExtent("Earth", ref extent);
            'ViewHelper.ViewExtent(scene, root, "Earth", extent, DegreesToRadians(-40), DegreesToRadians(10))
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere, -40, 10)

			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Primitive)
			scene.Render()

			m_Primitive = Nothing
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
	End Class
End Namespace
