#Region "UsingDirectives"
Imports System.Collections.Generic
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.Polyline
	Class PolylineRhumbLineCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Polyline\PolylineRhumbLineCodeSnippet.vb")
		End Sub

        ' Name        --
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawARhumbLine", _
            "Draw a rhumb line on the globe", _
            "Graphics | Primitives | Polyline Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim newOrleans As Array = New Object() {attachID("$startLat$The latitude of the start point$", 29.98), attachID("$startLon$The longitude of the start point$", -90.25), attachID("$startAlt$The altitude of the start point$", 0.0)}
            Dim sanJose As Array = New Object() {attachID("$endLat$The latitude of the end point$", 37.37), attachID("$endLon$The longitude of the end point$", -121.92), attachID("$endAlt$The altitude of the end point$", 0.0)}

            Dim positions As Array = New Object(5) {}
            newOrleans.CopyTo(positions, 0)
            sanJose.CopyTo(positions, 3)

            Dim interpolator As IAgStkGraphicsPositionInterpolator = TryCast(manager.Initializers.RhumbLineInterpolator.Initialize(), IAgStkGraphicsPositionInterpolator)
            Dim line As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.InitializeWithInterpolator(interpolator)
            line.SetCartographic(attachID("$planetName$The planet on which the polyline will be placed$", "Earth"), positions)
            manager.Primitives.Add(DirectCast(line, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(line, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("The PolylinePrimitive is initialized with a RhumbLineInterpolator to " & vbCrLf & _
                                     "visualize a rhumb line instead of a straight line.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
			scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

			Dim fit As Double = 1.0
			'for helping fit the line into the extent
            Dim extent As Array = New Object() {-121.92 - fit, 29.98 - fit, -90.25 + fit, 37.37 + fit}

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
