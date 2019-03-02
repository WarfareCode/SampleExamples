#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt
#End Region

Namespace Primitives.Model
	Class ModelOrientationCodeSnippet
		Inherits CodeSnippet
		Implements IDisposable
		Public Sub New()
            MyBase.New("Primitives\Model\ModelOrientationCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath
            Dim origin As IAgPosition = CreatePosition(root, 39.88, -75.25, 0)
            Dim axes As IAgCrdnAxesFixed = CreateAxes(root, "Earth", origin)
            Dim referenceFrame As IAgCrdnSystem = CreateSystem(root, "Earth", origin, axes)
            ExecuteSnippet(scene, root, modelFile, referenceFrame)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "OrientAModel", _
            "Orient a model", _
            "Graphics | Primitives | Model Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsModelPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")> ByVal modelFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("referenceFrame", "A reference frame for the model")> ByVal referenceFrame As IAgCrdnSystem)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                modelFile)
            DirectCast(model, IAgStkGraphicsPrimitive).ReferenceFrame = referenceFrame
            ' Model is oriented using east-north-up axes.  Use model.Orientation for addition rotation.
            Dim zero As Array = New Object() {0, 0, 0}
            ' Origin of reference frame
            model.Position = zero
            model.Scale = Math.Pow(10, attachID("$scale$The scale of the model$", 1.5))

            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))
            '#End Region

            OverlayHelper.AddTextBox("To orient a model, use its ReferenceFrame property. " & vbCrLf & _
                                     "In this example, AxesEastNorthUp is used. X points east, " & vbCrLf & _
                                     "Y points north, and Z points along the detic surface normal " & vbCrLf & _
                                     "(e.g. ""up""). Like all STK .mdl facility models, this model" & vbCrLf & _
                                     "was authored such that Z points up so it is oriented " & vbCrLf & _
                                     "correctly using AxesEastNorthUp.", manager)
            m_Primitive = DirectCast(model, IAgStkGraphicsPrimitive)

            m_ReferenceFrameGraphics = New ReferenceFrameGraphics(root, referenceFrame, s_AxesLength)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

			Dim position As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
            position.AssignPlanetodetic(39.88, -75.25, 0)

			Dim x As Double, y As Double, z As Double
			position.QueryCartesian(x, y, z)
			Dim center As Array = New Object() {x, y, z}
			Dim boundingSphere As IAgStkGraphicsBoundingSphere = manager.Initializers.BoundingSphere.Initialize(center, s_AxesLength)

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere, 20, 15)

			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

			manager.Primitives.Remove(m_Primitive)
			OverlayHelper.RemoveTextBox(manager)
			m_ReferenceFrameGraphics.Dispose()
			scene.Render()

			m_Primitive = Nothing
			m_ReferenceFrameGraphics = Nothing
		End Sub

		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		Protected Overrides Sub Finalize()
			Try
				Dispose(False)
			Finally
				MyBase.Finalize()
			End Try
		End Sub

		Protected Overridable Sub Dispose(disposing As Boolean)
			If disposing Then
				If m_ReferenceFrameGraphics IsNot Nothing Then
					m_ReferenceFrameGraphics.Dispose()
					m_ReferenceFrameGraphics = Nothing
				End If
			End If
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
		Private m_ReferenceFrameGraphics As ReferenceFrameGraphics
		Private Const s_AxesLength As Double = 2000

	End Class
End Namespace
