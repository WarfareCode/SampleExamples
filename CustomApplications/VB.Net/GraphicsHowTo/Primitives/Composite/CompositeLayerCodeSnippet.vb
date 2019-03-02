#Region "UsingDirectives"
Imports System.Drawing
Imports System.IO
Imports System.Collections.Generic
Imports GraphicsHowTo
Imports AGI.STKObjects
Imports AGI.STKGraphics
Imports AGI.STKUtil
Imports AGI.STKVgt

#End Region

Namespace Primitives.Composite
	Public Class CompositeLayersCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\Composite\CompositeLayerCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath
            Dim markerFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/facility.png").FullPath
            ExecuteSnippet(scene, root, modelFile, markerFile)
        End Sub

        ' Name        
        ' Description 
        ' Category    
        ' References  
        ' Namespaces  
        ' EID     
        <AGI.CodeSnippets.CodeSnippet( _
            "CreateALayerOfPrimitives", _
            "Create layers of primitives", _
            "Graphics | Primitives | Composite Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsCompositePrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "Location of the model file")> ByVal modelFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("markerFile", "The file to use for the marker batch")> ByVal markerFile As String)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim r As New Random()
            Dim holder As Integer = attachID("$numberOfModels$The number of models to create$", 25)
            Dim modelCount As Integer = attachID("$numberOfModels$The number of models to create$", 25)

            Dim positions As Array = Array.CreateInstance(GetType(Object), modelCount * 3)

            '
            ' Create the models
            '
            Dim models As IAgStkGraphicsCompositePrimitive = manager.Initializers.CompositePrimitive.Initialize()

            For i As Integer = 0 To modelCount - 1
                Dim latitude As Double = attachID("$centralLat$A central latitude that the models will be positioned close to$", 35) + 1.5 * r.NextDouble()
                Dim longitude As Double = -(attachID("$centralLon$A central longitude that the models will be positioned close to$", 80) + 1.5 * r.NextDouble())
                Dim altitude As Double = attachID("$alt$The altitude of the models$", 0)
                Dim position As Array = New Object() {latitude, longitude, altitude}

                positions.SetValue(latitude, 3 * i)
                positions.SetValue(longitude, (3 * i) + 1)
                positions.SetValue(altitude, (3 * i) + 2)

                Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri(modelFile)
                model.SetPositionCartographic("Earth", position)
                model.Scale = Math.Pow(10, 2)
                models.Add(model)
            Next

            '
            ' Create the markers
            '
            Dim markers As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
            markers.RenderPass = attachID("$renderPass$The pass during which the marker batch is rendered$", AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent)

            markers.Texture = manager.Textures.LoadFromStringUri(markerFile)
            markers.SetCartographic(attachID("$planetName$The planet to place the markers on$", "Earth"), positions)

            '
            ' Create the points
            '
            Dim points As IAgStkGraphicsPointBatchPrimitive = manager.Initializers.PointBatchPrimitive.Initialize()
            points.PixelSize = attachID("$pointSize$The size of the points in pixels$", 5)
            DirectCast(points, IAgStkGraphicsPrimitive).Color = Color.Orange
            Dim colors As Array = Array.CreateInstance(GetType(Object), modelCount)
            For i As Integer = 0 To colors.Length - 1
                colors.SetValue(attachID("$pointColor$The System.Drawing.Color of the points$", Color.Orange.ToArgb()), i)
            Next
            points.SetCartographicWithColorsAndRenderPass(attachID("$planetName$The planet to place the markers on$", "Earth"), positions, colors, attachID("$renderHint$For efficiency, how the points will be rendered$", AgEStkGraphicsRenderPassHint.eStkGraphicsRenderPassHintOpaque))

            '
            ' Set the display Conditions
            '
            Dim near As IAgStkGraphicsAltitudeDisplayCondition = manager.Initializers.AltitudeDisplayCondition.InitializeWithAltitudes(attachID("$modelMinAlt$Minimum altitude at which the models will be displayed$", 0), attachID("$modelMaxAlt$Maximum altitude at which the models will be displayed$", 500000))
            DirectCast(models, IAgStkGraphicsPrimitive).DisplayCondition = DirectCast(near, IAgStkGraphicsDisplayCondition)

            Dim medium As IAgStkGraphicsAltitudeDisplayCondition = manager.Initializers.AltitudeDisplayCondition.InitializeWithAltitudes(attachID("$markerMinAlt$Minimum altitude at which the markers will be displayed$", 500000), attachID("$markerMaxAlt$Maximum altitude at which the markers will be displayed$", 2000000))
            DirectCast(markers, IAgStkGraphicsPrimitive).DisplayCondition = DirectCast(medium, IAgStkGraphicsDisplayCondition)

            Dim far As IAgStkGraphicsAltitudeDisplayCondition = manager.Initializers.AltitudeDisplayCondition.InitializeWithAltitudes(attachID("$pointMinAlt$Minimum altitude at which the points will be displayed$", 2000000), attachID("$pointMaxAlt$Maximum altitude at which the points will be displayed$", 4000000))
            DirectCast(points, IAgStkGraphicsPrimitive).DisplayCondition = DirectCast(far, IAgStkGraphicsDisplayCondition)

            manager.Primitives.Add(DirectCast(models, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(markers, IAgStkGraphicsPrimitive))
            manager.Primitives.Add(DirectCast(points, IAgStkGraphicsPrimitive))

            '#End Region

            OverlayHelper.AddTextBox("Zoom in and out to see layers of primitives based on altitude." & vbCrLf & _
                                     "Models are shown when zoomed in closest. As you zoom out, " & vbCrLf & _
                                     "models switch to markers, then to points." & vbCrLf & vbCrLf & _
                                     "This level of detail technique is implemented by adding each" & vbCrLf & _
                                     "ModelPrimitive to a CompositePrimitive. A different" & vbCrLf & _
                                     "AltitudeDisplayCondition is assigned to the composite, " & vbCrLf & _
                                     "a marker batch, and a point batch.", manager)

            OverlayHelper.AddAltitudeOverlay(scene, manager)
            m_Intervals = New List(Of Interval)()

            m_Intervals.Add(New Interval(0, 500000))
            m_Intervals.Add(New Interval(500000, 2000000))
            m_Intervals.Add(New Interval(2000000, 4000000))

            OverlayHelper.AltitudeDisplay.AddIntervals(m_Intervals)

            m_Models = DirectCast(models, IAgStkGraphicsPrimitive)
            m_Markers = markers
            m_Points = points
        End Sub

		Private Shared Function CreateModel(position As Array, root As AgStkObjectRoot) As IAgStkGraphicsPrimitive
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

			Dim origin As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
			origin.AssignPlanetodetic(CDbl(position.GetValue(0)), CDbl(position.GetValue(1)), CDbl(position.GetValue(2)))
			Dim axes As IAgCrdnAxesFixed = CreateAxes(root, "Earth", origin)
			Dim system As IAgCrdnSystem = CreateSystem(root, "Earth", origin, axes)
			Dim result As IAgCrdnAxesFindInAxesResult = root.VgtRoot.WellKnownAxes.Earth.Fixed.FindInAxes(DirectCast(root.CurrentScenario, IAgScenario).Epoch, DirectCast(axes, IAgCrdnAxes))

            Dim modelPath As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath

			Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri(modelPath)
			model.SetPositionCartographic("Earth", position)
			model.Orientation = result.Orientation
			model.Scale = Math.Pow(10, 2)

			Return DirectCast(model, IAgStkGraphicsPrimitive)
		End Function

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Models IsNot Nothing Then
				ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Models.BoundingSphere)
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			If m_Models IsNot Nothing Then
				manager.Primitives.Remove(m_Models)
				manager.Primitives.Remove(DirectCast(m_Markers, IAgStkGraphicsPrimitive))
				manager.Primitives.Remove(DirectCast(m_Points, IAgStkGraphicsPrimitive))

				OverlayHelper.RemoveTextBox(manager)
				OverlayHelper.AltitudeDisplay.RemoveIntervals(m_Intervals)
				OverlayHelper.RemoveAltitudeOverlay(manager)
				scene.Render()

				m_Models = Nothing
				m_Markers = Nothing
				m_Points = Nothing
			End If
		End Sub

		Private m_Models As IAgStkGraphicsPrimitive
		Private m_Markers As IAgStkGraphicsMarkerBatchPrimitive
		Private m_Points As IAgStkGraphicsPointBatchPrimitive
		Private m_Intervals As List(Of Interval)
	End Class
End Namespace
