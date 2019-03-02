#Region "UsingDirectives"
Imports System.Collections.Generic
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.PointBatch
	Class PointBatchColorsCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\PointBatch\PointBatchColorsCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawASetOfUniquelyColoredPoints", _
            "Draw a set of uniquely colored points", _
            "Graphics | Primitives | Point Batch Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPointBatchPrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            ' San Francisco, Sacramento, Los Angeles, San Diego
            Dim positions As Array = New Object() _
            { _
                attachID("$lat1$The latitude of the first marker$", 37.62), attachID("$lon1$The longitude of the first marker$", -122.38), attachID("$alt1$The altitude of the first marker$", 0.0), _
                attachID("$lat2$The latitude of the second marker$", 38.52), attachID("$lon2$The longitude of the second marker$", -121.5), attachID("$alt2$The altitude of the second marker$", 0.0), _
                attachID("$lat3$The latitude of the third marker$", 33.93), attachID("$lon3$The longitude of the third marker$", -118.4), attachID("$alt3$The altitude of the third marker$", 0.0), _
                attachID("$lat4$The latitude of the fourth marker$", 32.82), attachID("$lon4$The longitude of the fourth marker$", -117.13), attachID("$alt4$The altitude of the fourth marker$", 0.0) _
            }

            Dim colors As Array = New Object() _
            { _
                attachID("$color1$The color of the first point (a System.Drawing.Color converted to Argb)$", Color.Red.ToArgb()), _
                 attachID("$color2$The color of the second point (a System.Drawing.Color converted to Argb)$", Color.Orange.ToArgb()), _
                 attachID("$color3$The color of the third point (a System.Drawing.Color converted to Argb)$", Color.Blue.ToArgb()), _
                 attachID("$color4$The color of the fourth point (a System.Drawing.Color converted to Argb)$", Color.White.ToArgb()) _
            }

            Dim pointBatch As IAgStkGraphicsPointBatchPrimitive = manager.Initializers.PointBatchPrimitive.Initialize()
            pointBatch.SetCartographicWithColors(attachID("$planetName$The name of the planet on which the points will be placed$", "Earth"), positions, colors)
            pointBatch.PixelSize = attachID("$pointSize$The size of the points in pixels$", 8)

            manager.Primitives.Add(DirectCast(pointBatch, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(pointBatch, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("A collection of positions and a collection of colors are provided to " & vbCrLf & _
                                     "the PointBatchPrimitive to visualize points with unique colors.", manager)
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
