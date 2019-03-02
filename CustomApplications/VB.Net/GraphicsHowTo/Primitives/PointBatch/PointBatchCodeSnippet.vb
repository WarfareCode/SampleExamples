#Region "UsingDirectives"
Imports System.Collections.Generic
Imports System.Drawing
Imports AGI.STKUtil
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Primitives.PointBatch
	Class PointBatchCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\PointBatch\PointBatchCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawASetOfPoints", _
            "Draw a set of points", _
            "Graphics | Primitives | Point Batch Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPointBatchPrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            ' Philadelphia
            ' Washington, D.C.
            ' New Orleans
            ' San Jose
            Dim positions As Array = New Object() {attachID("$lat1$The latitude of the first marker$", 39.88), attachID("$lon1$The longitude of the first marker$", -75.25), attachID("$alt1$The altitude of the first marker$", 0), _
                                                   attachID("$lat2$The latitude of the second marker$", 38.85), attachID("$lon2$The longitude of the second marker$", -77.04), attachID("$alt2$The altitude of the second marker$", 0), _
                                                   attachID("$lat3$The latitude of the third marker$", 29.98), attachID("$lon3$The longitude of the third marker$", -90.25), attachID("$alt3$The altitude of the third marker$", 0), _
                                                   attachID("$lat4$The latitude of the fourth marker$", 37.37), attachID("$lon4$The longitude of the fourth marker$", -121.92), attachID("$alt4$The altitude of the fourth marker$", 0)}

            Dim pointBatch As IAgStkGraphicsPointBatchPrimitive = manager.Initializers.PointBatchPrimitive.Initialize()
            pointBatch.SetCartographic(attachID("$planetName$The name of the planet on which the points will be placed$", "Earth"), positions)
            pointBatch.PixelSize = attachID("$pointSize$The size of the point in pixels$", 5)
            DirectCast(pointBatch, IAgStkGraphicsPrimitive).Color = attachID("$pointColor$The color of the points in the batch$", Color.White)
            pointBatch.DisplayOutline = attachID("$showOutline$Whether or not an outline should surround the point$", True)
            pointBatch.OutlineWidth = attachID("$outlineWidth$The width of the outline$", 2)
            pointBatch.OutlineColor = attachID("$outlineColor$The color of the outline$", Color.Red)

            manager.Primitives.Add(DirectCast(pointBatch, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(pointBatch, IAgStkGraphicsPrimitive)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere)
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
