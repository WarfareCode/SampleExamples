Imports System.Windows.Forms
#Region "UsingDirectives"
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Picking
	Class PickPerItemCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Picking\PickPerItemCodeSnippet.vb")
		End Sub

		Public Sub DoubleClick(scene As IAgStkGraphicsScene, root As AgStkObjectRoot, mouseX As Integer, mouseY As Integer)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim markerPositions As IList(Of Array) = m_markerPositions

			If m_MarkerBatch IsNot Nothing Then
				'#Region "CodeSnippet"
				Dim selectedMarkerCartesianPosition As Array = Nothing
				'
				' Get a collection of picked objects under the mouse location.
				' The collection is sorted with the closest object at index zero.
				'
				Dim collection As IAgStkGraphicsPickResultCollection = scene.Pick(attachID("$PickX$The X position to pick at$", mouseX), attachID("$PickY$The Y position to pick at$", mouseY))
				If collection.Count <> 0 Then
					Dim objects As IAgStkGraphicsObjectCollection = collection(0).Objects
					Dim batchPrimitive As IAgStkGraphicsMarkerBatchPrimitive = TryCast(objects(0), IAgStkGraphicsMarkerBatchPrimitive)

					'
					' Was a marker in our marker batch picked?
					'
					If batchPrimitive Is attachID("$desiredMarkerBatch$The marker batch to apply the pick action to$", m_MarkerBatch) Then
						'
						' Get the index of the particular marker we picked
						'
						Dim markerIndex As IAgStkGraphicsBatchPrimitiveIndex = TryCast(objects(1), IAgStkGraphicsBatchPrimitiveIndex)
						'
						' Get the position of the particular marker we picked
						'
						Dim markerCartographic As Array = markerPositions(markerIndex.Index)

						Dim markerPosition As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
						markerPosition.AssignPlanetodetic(CDbl(markerCartographic.GetValue(0)), CDbl(markerCartographic.GetValue(1)), CDbl(markerCartographic.GetValue(2)))

						Dim x As Double, y As Double, z As Double
						markerPosition.QueryCartesian(x, y, z)

						selectedMarkerCartesianPosition = New Object() {x, y, z}
					End If
				End If
				'#End Region
                If Not selectedMarkerCartesianPosition.Equals(Nothing) Then
                    ViewHelper.ViewBoundingSphere(scene, root, attachID("$planetName$The name of the planet the marker is located on$", "Earth"), manager.Initializers.BoundingSphere.Initialize(selectedMarkerCartesianPosition, attachID("$radius$The radius of the bounding sphere to view$", 100)))
                    scene.Render()
                End If
            End If
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim markerFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/facility.png").FullPath
            ExecuteSnippet(scene, root, markerFile, Nothing)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "ZoomToAMarker", _
            "Zoom to a particular marker in a batch", _
            "Graphics | Picking", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPickResult" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("markerFile", "The file to use for the markers")> ByVal markerFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("markerPositions", "A list of the marker positions")> ByVal markerPositions As IList(Of Array))
				'#Region "CodeSnippet"
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

				Dim positions As IList(Of Array) = New List(Of Array)()
				positions.Add(New Object() {39.88, -75.25, 3000.0})
				positions.Add(New Object() {38.85, -77.04, 3000.0})
				positions.Add(New Object() {38.85, -77.04, 0.0})
				positions.Add(New Object() {29.98, -90.25, 0.0})
				positions.Add(New Object() {37.37, -121.92, 0.0})

				Dim positionsArray As Array = Array.CreateInstance(GetType(Object), positions.Count * 3)
				For i As Integer = 0 To positions.Count - 1
					Dim position As Array = positions(i)
					position.CopyTo(positionsArray, i * 3)
				Next

				Dim markerBatch As IAgStkGraphicsMarkerBatchPrimitive = manager.Initializers.MarkerBatchPrimitive.Initialize()
				markerBatch.Texture = manager.Textures.LoadFromStringUri( _
					markerFile)
				markerBatch.SetCartographic("Earth", positionsArray)

				' Save the positions of the markers for use in the pick event
				markerPositions = positions
				' Enable per item picking
				markerBatch.PerItemPickingEnabled = True

				manager.Primitives.Add(DirectCast(markerBatch, IAgStkGraphicsPrimitive))
				'#End Region

            m_MarkerBatch = DirectCast(markerBatch, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("Double click on a marker to zoom to it." & vbCrLf & vbCrLf & _
                                     "The PerItemPicking property is set to true for the " & vbCrLf & _
                                     "batch primitive.  Scene.Pick is called in response " & vbCrLf & _
                                     "to the 3D window's MouseDoubleClick event to determine" & vbCrLf & _
                                     "the primitive and item index under the mouse. Camera.ViewSphere" & vbCrLf & _
                                     "is then used to zoom to the marker at the picked index.", manager)
            m_markerPositions = markerPositions
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
        Private m_markerPositions As IList(Of Array)
	End Class
End Namespace
