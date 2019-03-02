#Region "UsingDirectives"
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Generic
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.MarkerBatch
	Class MarkerBatchCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\MarkerBatch\MarkerBatchCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim markerImageFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/facility.png").FullPath
            ExecuteSnippet(scene, root, markerImageFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawASetOfMarkers", _
            "Draw a set of markers", _
            "Graphics | Primitives | Marker Batch Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsMarkerBatchPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("markerImageFile", "The image file to use for the markers")> ByVal markerImageFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim positions As Array = New Object() { _
                attachID("$lat1$The latitude of the first marker$", 39.88), attachID("$lon1$The longitude of the first marker$", -75.25), attachID("$alt1$The altitude of the first marker$", 0), _
                 attachID("$lat2$The latitude of the second marker$", 38.85), attachID("$lon2$The longitude of the second marker$", -77.04), attachID("$alt2$The altitude of the second marker$", 0), _
                 attachID("$lat3$The latitude of the third marker$", 29.98), attachID("$lon3$The longitude of the third marker$", -90.25), attachID("$alt3$The altitude of the third marker$", 0), _
                 attachID("$lat4$The latitude of the fourth marker$", 37.37), attachID("$lon4$The longitude of the fourth marker$", -121.92), attachID("$alt4$The altitude of the fourth marker$", 0)}

            Dim markerBatch As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            markerBatch.Texture = manager.Textures.LoadFromStringUri( _
                markerImageFile)
            markerBatch.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), positions)

            manager.Primitives.Add(DirectCast(markerBatch, IAgStkGraphicsPrimitive))
            '#End Region

            m_MarkerBatch = DirectCast(markerBatch, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("A collection of positions is passed to the MarkerBatchPrimitive to " & vbCrLf & _
                                     "visualize markers for each position.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_MarkerBatch IsNot Nothing Then
				ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_MarkerBatch.BoundingSphere)
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_MarkerBatch IsNot Nothing Then
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
				manager.Primitives.Remove(m_MarkerBatch)
				m_MarkerBatch = Nothing

				OverlayHelper.RemoveTextBox(manager)
				scene.Render()
			End If
		End Sub

		Private m_MarkerBatch As IAgStkGraphicsPrimitive
	End Class
End Namespace
