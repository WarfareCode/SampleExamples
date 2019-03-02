#Region "UsingDirectives"
Imports System.Collections.ObjectModel
Imports System.Drawing
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt
#End Region

Namespace Picking
	Class PickChangeColorCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Picking\PickChangeColorCodeSnippet.vb")
		End Sub

		Public Sub MouseMove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot, mouseX As Integer, mouseY As Integer)
			If m_Models IsNot Nothing Then
				'#Region "CodeSnippet"
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
                        Dim model As IAgStkGraphicsPrimitive = TryCast(objects(1), IAgStkGraphicsPrimitive)

                        '
                        ' Selected Model
                        '
                        model.Color = attachID("$pickedColor$The System.Drawing.Color to change the primitive to when it's picked$", Color.Cyan)

                        If model IsNot m_SelectedModel Then
                            '
                            ' Unselect previous model
                            '
                            If m_SelectedModel IsNot Nothing Then
                                m_SelectedModel.Color = attachID("$notPickedColor$The System.Drawing.Color to change the primitive to when it's not picked$", Color.Red)
                            End If
                            m_SelectedModel = model
                            scene.Render()
                        End If
                        Return
                    End If
				End If

				'
				' Unselect previous model
				'
				If m_SelectedModel IsNot Nothing Then
                    m_SelectedModel.Color = attachID("$notPickedColor$The System.Drawing.Color to change the primitive to when it's not picked$", Color.Red)
					m_SelectedModel = Nothing
					scene.Render()
					'#End Region
				End If
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
            "ChangeModelColorOnMouseOver", _
            "Change a model's color on mouse over", _
            "Graphics | Picking", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsPickResult" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("SelectedModel", "The previously selected model")> ByVal m_SelectedModel As IAgStkGraphicsPrimitive)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim r As New Random()

            Dim models As IAgStkGraphicsCompositePrimitive = manager.Initializers.CompositePrimitive.Initialize()

            For i As Integer = 0 To 24
                Dim position As Array = New Object() {35 + r.NextDouble(), -(82 + r.NextDouble()), 0.0}

                models.Add(CreateModel(position, root))
            Next

            manager.Primitives.Add(DirectCast(models, IAgStkGraphicsPrimitive))

            OverlayHelper.AddTextBox("Move the mouse over a model to change its color from red to cyan." & vbCrLf & vbCrLf & _
                                     "This technique, ""roll over"" picking, is implemented by calling " & vbCrLf & _
                                     "Scene.Pick in the 3D window's MouseMove event to determine " & vbCrLf & _
                                     "which primitive is under the mouse.", manager)

            m_Models = DirectCast(models, IAgStkGraphicsPrimitive)
            m_SelectedModel = Nothing
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
			manager.Primitives.Remove(m_Models)
			OverlayHelper.RemoveTextBox(manager)
			scene.Render()

			m_Models = Nothing
			m_SelectedModel = Nothing

		End Sub

		Private m_Models As IAgStkGraphicsPrimitive
		Private m_SelectedModel As IAgStkGraphicsPrimitive

	End Class
End Namespace
