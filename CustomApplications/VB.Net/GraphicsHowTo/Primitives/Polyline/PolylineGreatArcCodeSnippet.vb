#Region "UsingDirectives"
Imports System.Collections.Generic
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.Polyline
	Class PolylineGreatArcCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Polyline\PolylineGreatArcCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAGreatArc", _
            "Draw a great arc on the globe", _
            "Graphics | Primitives | Polyline Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim washingtonDC As Array = New Object() {attachID("$startLat$The latitude of the start point$", 38.85), attachID("$startLon$The longitude of the start point$", -77.04), attachID("$startAlt$The altitude of the start point$", 0.0)}
            Dim newOrleans As Array = New Object() {attachID("$endLat$The latitude of the end point$", 29.98), attachID("$endLon$The longitude of the start point$", -90.25), attachID("$endAlt$The altitude of the start point$", 0.0)}

            Dim positions As Array = New Object(5) {}
            washingtonDC.CopyTo(positions, 0)
            newOrleans.CopyTo(positions, 3)

            Dim interpolator As IAgStkGraphicsPositionInterpolator = TryCast(manager.Initializers.GreatArcInterpolator.Initialize(), IAgStkGraphicsPositionInterpolator)
            Dim line As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.InitializeWithInterpolator(interpolator)
            line.SetCartographic(attachID("$planetName$The planet on which the polyline will be placed$", "Earth"), positions)

            manager.Primitives.Add(DirectCast(line, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(line, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("The PolylinePrimitive is initialized with a GreatArcInterpolator to " & vbCrLf & _
                                     "visualize a great arc instead of a straight line.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
			scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

			Dim fit As Double = 1.0
			'for helping fit the line into the extent
            Dim extent As Array = New Object() {-90.25 - fit, 29.98 - fit, -77.04 + fit, 38.85 + fit}

			scene.Camera.ViewExtent("Earth", extent)

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
