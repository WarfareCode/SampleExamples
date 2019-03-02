Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Text
Imports AGI.STKGraphics
Imports AGI.STKObjects

Public NotInheritable Class OverlayHelper
	Private Sub New()
	End Sub
	Friend Shared Sub AddOriginalImageOverlay(manager As IAgStkGraphicsSceneManager)
		If m_OriginalImageOverlay Is Nothing Then
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

            Dim texture As IAgStkGraphicsRendererTexture2D = manager.Textures.FromRaster(manager.Initializers.Raster.InitializeWithStringUri( _
                         New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/f-22a_raptor.png").FullPath))
			m_OriginalImageOverlay = manager.Initializers.TextureScreenOverlay.Initialize()
			DirectCast(m_OriginalImageOverlay, IAgStkGraphicsOverlay).Width = 0.2
			DirectCast(m_OriginalImageOverlay, IAgStkGraphicsOverlay).WidthUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction
			DirectCast(m_OriginalImageOverlay, IAgStkGraphicsOverlay).Height = 0.2
			DirectCast(m_OriginalImageOverlay, IAgStkGraphicsOverlay).HeightUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction
			m_OriginalImageOverlay.Texture = texture
			DirectCast(m_OriginalImageOverlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenter
			LabelOverlay(DirectCast(m_OriginalImageOverlay, IAgStkGraphicsScreenOverlay), "Original", manager)

			overlayManager.Add(DirectCast(m_OriginalImageOverlay, IAgStkGraphicsScreenOverlay))
		End If
	End Sub

	Friend Shared Sub RemoveOriginalImageOverlay(manager As IAgStkGraphicsSceneManager)
		If m_OriginalImageOverlay IsNot Nothing Then
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Remove(DirectCast(m_OriginalImageOverlay, IAgStkGraphicsScreenOverlay))
			m_OriginalImageOverlay = Nothing
		End If
	End Sub

	Friend Shared Sub AddTimeOverlay(root As AgStkObjectRoot)
		If m_TimeOverlay Is Nothing Then
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			m_TimeOverlay = New TimeOverlay(root)
			m_TimeOverlay.SetDefaultStyle()
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Add(m_TimeOverlay.RealScreenOverlay)
			PositionStatusOverlays(manager)
		End If
	End Sub

	Friend Shared Sub AddAltitudeOverlay(scene As IAgStkGraphicsScene, manager As IAgStkGraphicsSceneManager)
		If m_AltitudeOverlay Is Nothing Then
			m_AltitudeOverlay = New AltitudeOverlay(scene, manager)
			m_AltitudeOverlay.SetDefaultStyle()
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Add(m_AltitudeOverlay.RealScreenOverlay)
			PositionStatusOverlays(manager)
		End If
	End Sub

	Friend Shared Sub AddDistanceOverlay(scene As IAgStkGraphicsScene, manager As IAgStkGraphicsSceneManager)
		If m_DistanceOverlay Is Nothing Then
			m_DistanceOverlay = New DistanceOverlay(scene, manager)
			m_DistanceOverlay.SetDefaultStyle()
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Add(m_DistanceOverlay.RealScreenOverlay)
			PositionStatusOverlays(manager)
		End If
		PositionStatusOverlays(manager)
	End Sub

	Friend Shared Sub RemoveTimeOverlay(manager As IAgStkGraphicsSceneManager)
		If m_TimeOverlay IsNot Nothing Then
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Remove(m_TimeOverlay.RealScreenOverlay)
			PositionStatusOverlays(manager)
			m_TimeOverlay = Nothing
		End If
	End Sub
	Friend Shared Sub RemoveAltitudeOverlay(manager As IAgStkGraphicsSceneManager)
		If m_AltitudeOverlay IsNot Nothing Then
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Remove(m_AltitudeOverlay.RealScreenOverlay)
			PositionStatusOverlays(manager)
			m_AltitudeOverlay = Nothing
		End If
	End Sub
	Friend Shared Sub RemoveDistanceOverlay(manager As IAgStkGraphicsSceneManager)
		If m_DistanceOverlay IsNot Nothing Then
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Remove(m_DistanceOverlay.RealScreenOverlay)
			PositionStatusOverlays(manager)
			m_DistanceOverlay = Nothing
		End If

		PositionStatusOverlays(manager)
	End Sub

	Friend Shared ReadOnly Property TimeDisplay() As TimeOverlay
		Get
			Return m_TimeOverlay
		End Get
	End Property
	Friend Shared ReadOnly Property AltitudeDisplay() As AltitudeOverlay
		Get
			Return m_AltitudeOverlay
		End Get
	End Property
	Friend Shared ReadOnly Property DistanceDisplay() As DistanceOverlay
		Get
			Return m_DistanceOverlay
		End Get
	End Property

	Public Shared Sub LabelOverlay(overlay As IAgStkGraphicsScreenOverlay, text As String, manager As IAgStkGraphicsSceneManager)
		Dim font As New Font("Arial", 12, FontStyle.Bold)

		Dim textOverlay As IAgStkGraphicsTextureScreenOverlay = TextOverlayHelper.CreateTextOverlay(text, font, manager)
		DirectCast(textOverlay, IAgStkGraphicsOverlay).ClipToParent = False

		Dim parentOverlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(DirectCast(overlay, IAgStkGraphicsOverlay).Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
		parentOverlayManager.Add(DirectCast(textOverlay, IAgStkGraphicsScreenOverlay))
	End Sub

	''' <summary>
	''' Displays a text box.
	''' </summary>
	''' <param name="text">Text to display.</param>
	Friend Shared Sub AddTextBox(text As String, manager As IAgStkGraphicsSceneManager)
		AddTextBox(text, 0, 0, manager)
	End Sub

	''' <summary>
	''' Displays a text box.
	''' </summary>
	''' <param name="text">Text to display.</param>
	''' /// <param name="xTranslation">The x translation of the text box.</param>
	''' /// <param name="yTranslation">The y translation of the text box.</param>
	Friend Shared Sub AddTextBox(text As String, xTranslation As Double, yTranslation As Double, manager As IAgStkGraphicsSceneManager)
		Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

		Dim font As New Font("Arial", 12, FontStyle.Bold)

		Dim overlay As IAgStkGraphicsTextureScreenOverlay = TextOverlayHelper.CreateTextOverlay(text, font, manager)
		DirectCast(overlay, IAgStkGraphicsOverlay).BorderSize = 2
        DirectCast(overlay, IAgStkGraphicsOverlay).BorderColor = Color.White

		Dim overlayPosition As Array = DirectCast(overlay, IAgStkGraphicsOverlay).Position
		Dim overlaySize As Array = DirectCast(overlay, IAgStkGraphicsOverlay).Size

		Dim baseOverlay As IAgStkGraphicsScreenOverlay = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(overlayPosition, overlaySize)
		DirectCast(baseOverlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight
		DirectCast(baseOverlay, IAgStkGraphicsOverlay).TranslationX = xTranslation
		DirectCast(baseOverlay, IAgStkGraphicsOverlay).TranslationY = yTranslation
        DirectCast(baseOverlay, IAgStkGraphicsOverlay).Color = Color.Black
		DirectCast(baseOverlay, IAgStkGraphicsOverlay).Translucency = 0.5F

		Dim baseOverlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(DirectCast(baseOverlay, IAgStkGraphicsOverlay).Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
		baseOverlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))

		m_TextBox = baseOverlay

        DirectCast(m_TextBox, IAgStkGraphicsOverlay).Position = New Object() { _
            5, 5, _
            AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, _
            AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}
		overlayManager.Add(m_TextBox)
	End Sub

	''' <summary>
	''' Remove the text box associated with the given snippet.
	''' </summary>
	Friend Shared Sub RemoveTextBox(manager As IAgStkGraphicsSceneManager)
		Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
		overlayManager.Remove(m_TextBox)
		m_TextBox = Nothing
	End Sub

	Private Shared Sub PositionStatusOverlays(manager As IAgStkGraphicsSceneManager)
		If m_TimeOverlay IsNot Nothing Then
			m_TimeOverlay.Update(m_TimeOverlay.Value, manager)
            m_TimeOverlay.Position = New Object() {5, 5, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}
		End If
		If m_AltitudeOverlay IsNot Nothing Then
			m_AltitudeOverlay.Update(m_AltitudeOverlay.Value, manager)
            m_AltitudeOverlay.Position = New Object() {5, 5 + (If((m_TimeOverlay Is Nothing), 0, m_TimeOverlay.Height + 5)), AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}
		End If
		If m_DistanceOverlay IsNot Nothing Then
			m_DistanceOverlay.Update(m_DistanceOverlay.Value, manager)
            m_DistanceOverlay.Position = New Object() {5, 5 + (If((m_TimeOverlay Is Nothing), 0, m_TimeOverlay.Height + 5)) + (If((m_AltitudeOverlay Is Nothing), 0, m_AltitudeOverlay.Height + 5)), AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}
		End If
	End Sub

	Private Shared m_OriginalImageOverlay As IAgStkGraphicsTextureScreenOverlay
	Private Shared m_TimeOverlay As TimeOverlay
	Private Shared m_AltitudeOverlay As AltitudeOverlay
	Private Shared m_DistanceOverlay As DistanceOverlay

	Private Shared m_TextBox As IAgStkGraphicsScreenOverlay
End Class
