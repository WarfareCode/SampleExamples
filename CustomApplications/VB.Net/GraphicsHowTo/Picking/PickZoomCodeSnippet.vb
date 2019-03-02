Imports System.IO
#Region "UsingDirectives"
Imports System.Collections.ObjectModel
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt
#End Region

Namespace Picking
	Class PickZoomCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Picking\PickZoomCodeSnippet.vb")
		End Sub

		Public Sub DoubleClick(scene As IAgStkGraphicsScene, root As AgStkObjectRoot, mouseX As Integer, mouseY As Integer)
			If m_Models IsNot Nothing Then
                '#Region "CodeSnippet"
                Dim selectedModel As IAgStkGraphicsPrimitive = Nothing
                '
                ' Get a collection of picked objects under the mouse location.
                ' The collection is sorted with the closest object at index zero.
                '
                Dim collection As IAgStkGraphicsPickResultCollection = scene.Pick(attachID("$PickX$The X position to pick at$", mouseX), attachID("$PickY$The Y position to pick at$", mouseY))
                If collection.Count <> 0 Then
                    Dim objects As IAgStkGraphicsObjectCollection = collection(0).Objects
                    Dim composite As IAgStkGraphicsCompositePrimitive = TryCast(objects(0), IAgStkGraphicsCompositePrimitive)

                    '
                    ' Was a model in our composite picked?
                    '
                    If composite Is attachID("$desiredPrimitive$The primitive to apply the pick action to$", m_Models) Then
                        selectedModel = TryCast(objects(1), IAgStkGraphicsPrimitive)
                    End If
                    '#End Region
                    If Not selectedModel.Equals(Nothing) Then
                        ViewHelper.ViewBoundingSphere(scene, root, attachID("$planetName$The planet on which the primitive lays$", "Earth"), selectedModel.BoundingSphere)
                        scene.Render()
                    End If
                End If
            End If
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "ZoomToAModelOnDoubleClick", _
            "Zoom to a model on double click", _
            "Graphics | Picking", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPickResult" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim models As IAgStkGraphicsCompositePrimitive = manager.Initializers.CompositePrimitive.Initialize()

            '
            ' Create the positions
            '
            Dim p0 As Array = New Object() {39.88, -75.25, 3000.0}
            Dim p1 As Array = New Object() {38.85, -77.04, 0.0}
            Dim p2 As Array = New Object() {29.98, -90.25, 0.0}
            Dim p3 As Array = New Object() {37.37, -121.92, 0.0}

            models.Add(CreateModel(p0, root))
            models.Add(CreateModel(p1, root))
            models.Add(CreateModel(p2, root))
            models.Add(CreateModel(p3, root))

            manager.Primitives.Add(DirectCast(models, IAgStkGraphicsPrimitive))

            OverlayHelper.AddTextBox("Double click on a model to zoom to it." & vbCrLf & vbCrLf & _
                                     "Scene.Pick is called in response to the 3D window's " & vbCrLf & _
                                     "MouseDoubleClick event to determine the primitive under the " & vbCrLf & _
                                     "mouse. Camera.ViewSphere is then used to zoom to the primitive.", manager)

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
                New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath)
			model.SetPositionCartographic("Earth", position)
			model.Orientation = result.Orientation
			model.Scale = Math.Pow(10, 3.5)

			Return DirectCast(model, IAgStkGraphicsPrimitive)
		End Function

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Models.BoundingSphere)
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Models)
			OverlayHelper.RemoveTextBox(manager)
			scene.Render()

			m_Models = Nothing

		End Sub

		Private m_Models As IAgStkGraphicsPrimitive

	End Class
End Namespace
