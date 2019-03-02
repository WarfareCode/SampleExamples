#Region "UsingDirectives"
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace DisplayConditions
	Public Class AltitudeDisplayConditionCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("DisplayConditions\AltitudeDisplayConditionCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddAltitudeDisplayCondition", _
            "Draw a primitive based on viewer altitude", _
            "Graphics | DisplayConditions", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsAltitudeDisplayCondition" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim extent As Array = New Object() _
            { _
                attachID("$westLon$Westernmost longitude$", -94), attachID("$southLat$Southernmost latitude$", 29), _
                attachID("$eastLon$Easternmost longitude$", -89), attachID("$northLat$Northernmost latitude$", 33) _
            }

            Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple("Earth", extent)

            Dim line As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.Initialize()
            Dim boundaryPositions As Array = triangles.BoundaryPositions
            line.Set(boundaryPositions)
            DirectCast(line, IAgStkGraphicsPrimitive).Color = attachID("$color$System.Drawing.Color of the primitive$", Color.White)

            Dim condition As IAgStkGraphicsAltitudeDisplayCondition = manager.Initializers.AltitudeDisplayCondition.InitializeWithAltitudes(attachID("$minAlt$Minimum altitude at which the primitive is visible$", 500000), attachID("$maxAlt$Maximum altitude at which the primitive is visible$", 2500000))
            DirectCast(line, IAgStkGraphicsPrimitive).DisplayCondition = TryCast(condition, IAgStkGraphicsDisplayCondition)

            manager.Primitives.Add(DirectCast(line, IAgStkGraphicsPrimitive))

            '#End Region

            OverlayHelper.AddTextBox("Zoom in and out to see the primitive disappear and " & vbCrLf & _
                                     "reappear based on altitude. " & vbCrLf & vbCrLf & _
                                     "This is implemented by assigning an " & vbCrLf & _
                                     "AltitudeDisplayCondition to the primitive's " & vbCrLf & _
                                     "DisplayCondition property.", manager)

            OverlayHelper.AddAltitudeOverlay(scene, manager)
            OverlayHelper.AltitudeDisplay.AddIntervals(New Interval() {New Interval(500000, 2500000)})

            m_Primitive = DirectCast(line, IAgStkGraphicsPrimitive)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere)
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Primitive)

			OverlayHelper.RemoveTextBox(manager)
			OverlayHelper.AltitudeDisplay.RemoveIntervals(New Interval() {New Interval(500000, 2500000)})
			OverlayHelper.RemoveAltitudeOverlay(manager)

			scene.Render()

			m_Primitive = Nothing
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
	End Class
End Namespace
