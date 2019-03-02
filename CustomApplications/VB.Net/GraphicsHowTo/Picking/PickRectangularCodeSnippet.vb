Imports System.IO
Imports System.Collections.Generic

#Region "UsingDirectives"

Imports System.Collections.ObjectModel
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt

#End Region

Namespace Picking
	Public Class PickRectangularCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Picking\PickRectangularCodeSnippet.vb")
		End Sub

		Public Sub MouseMove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot, mouseX As Integer, mouseY As Integer)
            Dim manager2 As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager2.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
            Dim SelectedModels As List(Of IAgStkGraphicsModelPrimitive) = m_SelectedModels
			If m_Models IsNot Nothing Then
				If Not overlayManager.Contains(m_Overlay) Then
					overlayManager.Add(m_Overlay)
				End If 
                DirectCast(m_Overlay, IAgStkGraphicsOverlay).Position = New Object() _
                    { _
                        mouseX, mouseY, _
                        AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, _
                        AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels _
                    }

                '#Region "CodeSnippet"
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
                '
                ' Get a collection of picked objects in a 100 by 100 rectangular region.
                ' The collection is sorted with the closest object at index zero.
                '
                Dim newModels As New List(Of IAgStkGraphicsModelPrimitive)()
                Dim collection As IAgStkGraphicsPickResultCollection = scene.PickRectangular(attachID("$PickX$The X position to pick at$", mouseX) - attachID("$halfRegionSize$Half of the region size to pick within$", 50), attachID("$PickY$The Y position to pick at$", mouseY) + attachID("$halfRegionSize$Half of the region size to pick within$", 50), attachID("$PickX$The X position to pick at$", mouseX) + attachID("$halfRegionSize$Half of the region size to pick within$", 50), attachID("$PickY$The Y position to pick at$", mouseY) - attachID("$halfRegionSize$Half of the region size to pick within$", 50))
                For Each pickResult As IAgStkGraphicsPickResult In collection
                    Dim objects As IAgStkGraphicsObjectCollection = pickResult.Objects
                    Dim composite As IAgStkGraphicsCompositePrimitive = TryCast(objects(0), IAgStkGraphicsCompositePrimitive)

                    '
                    ' Was a model in our composite picked?
                    '
                    If composite Is attachID("$desiredPrimitive$The primitive to apply the pick action to$", m_Models) Then
                        Dim model As IAgStkGraphicsModelPrimitive = TryCast(objects(1), IAgStkGraphicsModelPrimitive)

                        '
                        ' Selected Model
                        '
                        DirectCast(model, IAgStkGraphicsPrimitive).Color = attachID("$pickedColor$The System.Drawing.Color to change the primitive to when it's picked$", Color.Cyan)
                        newModels.Add(model)
                    End If
                Next
                ' 
                ' Reset color of models that were previous selected but were not in this pick. 
                '
                For Each selectedModel As IAgStkGraphicsModelPrimitive In SelectedModels
                    If Not newModels.Contains(selectedModel) Then
                        DirectCast(selectedModel, IAgStkGraphicsPrimitive).Color = attachID("$notPickedColor$The System.Drawing.Color to change the primitive to when it's not picked$", Color.Red)
                    End If
                Next
                SelectedModels = newModels


					'#End Region
				manager.Render()
			End If
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            ExecuteSnippet(scene, root, Nothing)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "ChangeModelColorInARegion", _
            "Change model colors within a rectangular region", _
            "Graphics | Picking", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPickResult" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("SelectedModels", "The previously selected models")> ByVal SelectedModels As List(Of IAgStkGraphicsModelPrimitive))
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            ' Create a screen overlay to visualize the 100 by 100 picking region.
            m_Overlay = manager.Initializers.ScreenOverlay.Initialize(0, 0, 100, 100)
            DirectCast(m_Overlay, IAgStkGraphicsOverlay).PinningOrigin = AgEStkGraphicsScreenOverlayPinningOrigin.eStkGraphicsScreenOverlayPinningOriginCenter
            DirectCast(m_Overlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft
            DirectCast(m_Overlay, IAgStkGraphicsOverlay).Translucency = 0.9F
            DirectCast(m_Overlay, IAgStkGraphicsOverlay).BorderSize = 2

            Dim r As New Random()

            Dim models As IAgStkGraphicsCompositePrimitive = manager.Initializers.CompositePrimitive.Initialize()
            m_SelectedModels = New List(Of IAgStkGraphicsModelPrimitive)()

            For i As Integer = 0 To 24
                Dim position As Array = New Object() {35 + r.NextDouble(), -(82 + r.NextDouble()), 0.0}

                models.Add(CreateModel(position, root))
            Next

            manager.Primitives.Add(DirectCast(models, IAgStkGraphicsPrimitive))

            OverlayHelper.AddTextBox("Move the rectangular box over models to change their color." & vbCrLf & vbCr & vbLf & _
                                     "This technique, ""roll over"" picking with a rectangular region," & vbCrLf & _
                                     "is implemented by calling Scene.PickRectangular in the 3D " & vbCrLf & _
                                     "window's MouseMove event to determine which primitives are " & vbCrLf & _
                                     "under the rectangular region associated with the mouse.", manager)

            m_Models = DirectCast(models, IAgStkGraphicsPrimitive)
        End Sub

		Private Shared Function CreateModel(position As Array, root As AgStkObjectRoot) As IAgStkGraphicsPrimitive
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

			Dim origin As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
			origin.AssignPlanetodetic(CDbl(position.GetValue(0)), CDbl(position.GetValue(1)), CDbl(position.GetValue(2)))
			Dim axes As IAgCrdnAxesFixed = CreateAxes(root, "Earth", origin)
			Dim system As IAgCrdnSystem = CreateSystem(root, "Earth", origin, axes)
			Dim result As IAgCrdnAxesFindInAxesResult = root.VgtRoot.WellKnownAxes.Earth.Fixed.FindInAxes(DirectCast(root.CurrentScenario, IAgScenario).Epoch, DirectCast(axes, IAgCrdnAxes))

            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility-colorless.mdl").FullPath)
			model.SetPositionCartographic("Earth", position)
			model.Orientation = result.Orientation
			model.Scale = Math.Pow(10, 2)
            DirectCast(model, IAgStkGraphicsPrimitive).Color = Color.Red

			Return DirectCast(model, IAgStkGraphicsPrimitive)
		End Function

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Models.BoundingSphere, -90, 15)
			scene.Camera.Distance *= 0.7
			'zoom in a bit
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

			overlayManager.Remove(m_Overlay)
			manager.Primitives.Remove(m_Models)
			OverlayHelper.RemoveTextBox(manager)
			scene.Render()

			m_Models = Nothing
			m_SelectedModels = Nothing
		End Sub

		Private m_Models As IAgStkGraphicsPrimitive
		Private m_SelectedModels As List(Of IAgStkGraphicsModelPrimitive)
		Private m_Overlay As IAgStkGraphicsScreenOverlay
	End Class
End Namespace
