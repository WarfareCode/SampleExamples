#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace DisplayConditions
	Class DistanceDisplayConditionCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("DisplayConditions\DistanceDisplayConditionCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/hellfire.dae").FullPath
            ExecuteSnippet(scene, root, modelFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddDistanceDisplayCondition", _
            "Draw a primitive based on viewer distance", _
            "Graphics | DisplayConditions", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsDistanceDisplayCondition" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "Location of the model file")> ByVal modelFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                modelFile)
            Dim position As Array = New Object() {attachID("$lat$Model latitude$", 29.98), attachID("$lon$Model longitude$", -90.25), attachID("$alt$Model altitude$", 8000.0)}
            model.SetPositionCartographic(attachID("$planetName$Name of the planet to place the model$", "Earth"), position)
            model.Scale = Math.Pow(10, attachID("$scale$Scale of the model$", 3))

            Dim condition As IAgStkGraphicsDistanceDisplayCondition = manager.Initializers.DistanceDisplayCondition.InitializeWithDistances(attachID("$minDistance$Minimum distance at which the model is visible$", 2000), attachID("$maxDistance$Maximum distance at which the model is visible$", 40000))
            DirectCast(model, IAgStkGraphicsPrimitive).DisplayCondition = TryCast(condition, IAgStkGraphicsDisplayCondition)

            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(model, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("Zoom in and out to see the primitive disappear and " & vbCrLf & _
                                     "reappear based on distance. " & vbCrLf & vbCrLf & _
                                     "This is implemented by assigning a DistanceDisplayCondition " & vbCrLf & _
                                     "to the primitive's DisplayCondition property.", manager)

            OverlayHelper.AddDistanceOverlay(scene, manager)
            OverlayHelper.DistanceDisplay.AddIntervals(New Interval() {New Interval(2000, 40000)})
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere, _
                                          45, 10)
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Primitive)
			OverlayHelper.RemoveTextBox(manager)
			OverlayHelper.DistanceDisplay.RemoveIntervals(New Interval() {New Interval(2000, 40000)})
			OverlayHelper.RemoveDistanceOverlay(manager)

			scene.Render()

			m_Primitive = Nothing

		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive

	End Class
End Namespace
