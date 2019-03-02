#Region "UsingDirectives"
Imports System.Drawing
Imports System.Collections.Generic
Imports System.IO
Imports AGI.STKUtil
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Primitives.Polyline
	Class PolylineAreaTargetCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Polyline\PolylineAreaTargetCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim positions As Array = STKUtil.ReadAreaTargetPoints( _
                New AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/_pennsylvania_1.at").FullPath, root)
            ExecuteSnippet(scene, root, positions)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAnAreaTargetOutline", _
            "Draw a STK area target outline on the globe", _
            "Graphics | Primitives | Polyline Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("positions", "The area target positions")> ByVal positions As Array)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim line As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.Initialize()

            line.Set(positions)
            line.Width = attachID("$width$The width of the polyline$", 2)
            DirectCast(line, IAgStkGraphicsPrimitive).Color = attachID("$color$The color of the polyline$", Color.Yellow)
            line.DisplayOutline = attachID("$showOutline$Whether or not an outline is shown around the polyline$", True)
            line.OutlineWidth = attachID("$outlineWidth$The width of the outline$", 2)
            line.OutlineColor = attachID("$outlineColor$The color of the outline$", Color.Black)

            manager.Primitives.Add(DirectCast(line, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(line, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("Positions defining the boundary of an STK area target are read from " & vbCrLf & _
                                     "disk and visualized with the polyline primitive.", manager)
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
