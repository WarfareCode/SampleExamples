#Region "UsingDirectives"
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace DisplayConditions
	Class ScreenOverlayDisplayConditionCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("DisplayConditions\ScreenOverlayDisplayConditionCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim model As IAgStkGraphicsModelPrimitive = CreateTankModel(root, manager)
            Dim position As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
            position.AssignCartesian(CDbl(model.Position.GetValue(0)), CDbl(model.Position.GetValue(1)), CDbl(model.Position.GetValue(2)))
            Dim planetocentricPosition As Array = position.QueryPlanetocentricArray()
            Dim overlay As IAgStkGraphicsScreenOverlay = CreateTextOverlay( _
                "Mobile SA-10 Launcher" & vbLf & _
                "Latitude: " & String.Format("{0:0}", RadiansToDegrees(CDbl(planetocentricPosition.GetValue(0)))) & vbLf & _
                "Longitude: " & String.Format("{0:0}", RadiansToDegrees(CDbl(planetocentricPosition.GetValue(1)))), manager)
            ExecuteSnippet(scene, root, model, overlay)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAScreenOverlayWithACondition", _
            "Draw a screen overlay based on viewer distance", _
            "Graphics | DisplayConditions", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsDistanceDisplayCondition" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("Model", "A model")> ByVal model As IAgStkGraphicsModelPrimitive, <AGI.CodeSnippets.CodeSnippet.Parameter("Overlay", "A overlay")> ByVal overlay As IAgStkGraphicsScreenOverlay)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

            Dim condition As IAgStkGraphicsDistanceToPrimitiveDisplayCondition = manager.Initializers.DistanceToPrimitiveDisplayCondition.InitializeWithDistances(DirectCast(model, IAgStkGraphicsPrimitive), attachID("$minDistance$Minimum distance at which the overlay is visible$", 0), attachID("$maxDistance$Maximum distance at which the overlay is visible$", 40000))
            DirectCast(overlay, IAgStkGraphicsOverlay).DisplayCondition = DirectCast(condition, IAgStkGraphicsDisplayCondition)

            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))
            overlayManager.Add(overlay)
            '#End Region

            m_Model = DirectCast(model, IAgStkGraphicsPrimitive)
            m_Overlay = overlay
            OverlayHelper.AddTextBox("Zoom in to within 40 km to see the overlay appear." & vbCrLf & vbCrLf & _
                                     "This is implemented by assigning a " & vbCrLf & _
                                     "DistanceToPrimitiveDisplayCondition to the overlay's " & vbCrLf & _
                                     "DisplayCondition property.", manager)

            OverlayHelper.AddDistanceOverlay(scene, manager)
            OverlayHelper.DistanceDisplay.AddIntervals(New Interval() {New Interval(0, 40000)})
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Model.BoundingSphere, _
                                          225, 25)
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			manager.Primitives.Remove(m_Model)
			overlayManager.Remove(m_Overlay)
			OverlayHelper.RemoveTextBox(manager)
			OverlayHelper.DistanceDisplay.RemoveIntervals(New Interval() {New Interval(0, 40000)})
			OverlayHelper.RemoveDistanceOverlay(manager)

			scene.Render()

			m_Model = Nothing
			m_Overlay = Nothing

		End Sub

		Private Shared Function CreateTankModel(root As AgStkObjectRoot, manager As IAgStkGraphicsSceneManager) As IAgStkGraphicsModelPrimitive
            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/sa10-mobile-a.mdl").FullPath)

            Dim position As Array = New Object() {56, 37, 0.0}
			model.SetPositionCartographic("Earth", position)
			model.Scale = 1000


			Dim orientation As IAgOrientation = root.ConversionUtility.NewOrientation()
            orientation.AssignEulerAngles(AgEEulerOrientationSequence.e321, _
                                          -37, -26, 22)
			model.Orientation = orientation

			Return model
		End Function

		Private Function CreateTextOverlay(text As String, manager As IAgStkGraphicsSceneManager) As IAgStkGraphicsScreenOverlay
			Dim font As New Font("Consolas", 12, FontStyle.Bold)
			Dim textSize As Size = CodeSnippet.MeasureString(text, font)
			Dim textBitmap As New Bitmap(textSize.Width, textSize.Height)
			Dim gfx As Graphics = Graphics.FromImage(textBitmap)
			gfx.DrawString(text, font, Brushes.White, New PointF(0, 0))

			Dim textBitmapFilepath As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "SA-10TextOverlay.bmp").FullPath
			textBitmap.Save(textBitmapFilepath)

			Dim texture As IAgStkGraphicsRendererTexture2D = manager.Textures.LoadFromStringUri(textBitmapFilepath)
            Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight( _
                0, 60, texture.Template.Width, texture.Template.Height)
			DirectCast(overlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomCenter
			overlay.Texture = texture
			DirectCast(overlay, IAgStkGraphicsOverlay).BorderSize = 2
            DirectCast(overlay, IAgStkGraphicsOverlay).BorderColor = Color.White

			System.IO.File.Delete(textBitmapFilepath)

			Return TryCast(overlay, IAgStkGraphicsScreenOverlay)
		End Function

		Private m_Model As IAgStkGraphicsPrimitive
		Private m_Overlay As IAgStkGraphicsScreenOverlay

	End Class
End Namespace
