Imports System.Drawing
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKVgt
Imports AGI.STKUtil
Imports AGI.STKX
Imports AGI.STKX.Controls


	'
	' OverlayToolbar
	'
	Public Class OverlayToolbar
		Implements IDisposable
		'
		' Properties
		'
		Public ReadOnly Property Overlay() As IAgStkGraphicsScreenOverlay
			Get
				Return DirectCast(m_Panel, IAgStkGraphicsScreenOverlay)
			End Get
		End Property

    '
    ' Constructor
    '
    Public Sub New(root As AgStkObjectRoot, control As AxAgUiAxVOCntrl)
        m_Root = root
        m_Control3D = control
        Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager

        m_ButtonHolders = New List(Of OverlayButton)()
        Dim ScreenOverlayManager As IAgStkGraphicsScreenOverlayManager = manager.ScreenOverlays

        '
        ' Panel
        '
        m_Panel = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 0, m_PanelWidth, OverlayButton.ButtonSize)
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)
        overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft
        overlay.BorderSize = 2
        overlay.Color = Color.Transparent
        overlay.BorderColor = Color.Transparent
        overlay.Translucency = m_PanelTranslucencyRegular
        overlay.BorderTranslucency = m_PanelBorderTranslucencyRegular

        Dim managerOverlays As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
        managerOverlays.Add(DirectCast(m_Panel, IAgStkGraphicsScreenOverlay))

        '
        ' Buttons
        '
        Dim enabledImage As String, disabledImage As String
        Dim action As ActionDelegate

        '
        ' ShowHide button
        '
        enabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\visible.png")
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\invisible.png")
        action = New ActionDelegate(AddressOf ShowHideAction)
        AddButton(enabledImage, disabledImage, action)

        '
        ' Reset button
        '
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\reset.png")
        action = New ActionDelegate(AddressOf ResetAction)
        AddButton(disabledImage, action)

        ' DecreaseDelta button
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\decreasedelta.png")
        action = New ActionDelegate(AddressOf DecreaseDeltaAction)
        AddButton(disabledImage, action)

        '
        ' StepBack button
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\stepreverse.png")
        action = New ActionDelegate(AddressOf StepReverseAction)
        AddButton(disabledImage, action)
        m_StepReverseButton = m_ButtonHolders(m_ButtonHolders.Count - 1)

        '
        ' PlayBack button
        '
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\playreverse.png")
        action = New ActionDelegate(AddressOf PlayReverseAction)
        AddButton(disabledImage, action)

        '
        ' Pause button
        '
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\pause.png")
        action = New ActionDelegate(AddressOf PauseAction)
        AddButton(disabledImage, action)

        '
        ' Play button
        '
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\playforward.png")
        action = New ActionDelegate(AddressOf PlayForwardAction)
        AddButton(disabledImage, action)

        '
        ' StepForward button
        '
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\stepforward.png")
        action = New ActionDelegate(AddressOf StepForwardAction)
        AddButton(disabledImage, action)
        m_StepForwardButton = m_ButtonHolders(m_ButtonHolders.Count - 1)

        '
        ' IncreaseDelta button
        '
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\increasedelta.png")
        action = New ActionDelegate(AddressOf IncreaseDeltaAction)
        AddButton(disabledImage, action)

        '
        ' Zoom button
        '
        enabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\zoompressed.png")
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\zoom.png")
        action = New ActionDelegate(AddressOf ZoomAction)
        AddButton(enabledImage, disabledImage, action)
        m_ZoomButton = m_ButtonHolders(m_ButtonHolders.Count - 1)

        '
        ' Pan button
        '
        enabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\panpressed.png")
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\pan.png")
        action = New ActionDelegate(AddressOf PanAction)
        AddButton(enabledImage, disabledImage, action)

        '
        ' Home button
        '
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\home.png")
        action = New ActionDelegate(AddressOf HomeAction)
        AddButton(disabledImage, action)

        '
        ' Moon Button
        '
        disabledImage = Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\moon.png")
        action = New ActionDelegate(AddressOf MoonAction)
        AddButton(disabledImage, action)

        Dim collectionBase As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(overlay.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)

        '
        ' Scale button
        '
        m_ScaleButton = New OverlayButton(New ActionDelegate(AddressOf ScaleAction), Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\scale.png"), 0, m_PanelWidth, 0.5, 0, _
             m_Root)
        Dim scaleOverlay As IAgStkGraphicsOverlay = DirectCast(m_ScaleButton.Overlay, IAgStkGraphicsOverlay)
        scaleOverlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight
        collectionBase.Add(m_ScaleButton.Overlay)
        m_ButtonHolders.Add(m_ScaleButton)

        '
        ' Rotate button
        '
        m_RotateButton = New OverlayButton(New ActionDelegate(AddressOf RotateAction), Path.Combine(My.Application.Info.DirectoryPath, "ToolbarImages\rotate.png"), 0, m_PanelWidth, 0.5, 0, _
             m_Root)
        Dim rotateOverlay As IAgStkGraphicsOverlay = DirectCast(m_RotateButton.Overlay, IAgStkGraphicsOverlay)
        rotateOverlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomRight
        collectionBase.Add(m_RotateButton.Overlay)
        m_ButtonHolders.Add(m_RotateButton)
        DockBottom()
    End Sub

    '
    ' Public Methods
    '

    '
    ' Adds a one-way button to the panel
    '
    Public Sub AddButton(image As String, action As ActionDelegate)
        AddButton(image, image, action)
    End Sub

    '
    ' Adds a two-way button to the panel
    '
    Public Sub AddButton(enabledImage As String, disabledImage As String, action As ActionDelegate)
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)
        m_PanelWidth += OverlayButton.ButtonSize
        overlay.Width = m_PanelWidth

        Dim newButton As OverlayButton
        newButton = New OverlayButton(action, disabledImage, m_LocationOffset, m_PanelWidth, m_Root)
        newButton.SetTexture(enabledImage, disabledImage)

        Dim collectionBase As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(overlay.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
        collectionBase.Add(newButton.Overlay)
        m_ButtonHolders.Add(newButton)

        m_LocationOffset += OverlayButton.ButtonSize

        For Each button As OverlayButton In m_ButtonHolders
            button.Resize(m_PanelWidth)
        Next
    End Sub

    Public Sub DockRight()
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)
        overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight
        overlay.RotationAngle = Math.PI / 2
        OrientButtons()
        overlay.TranslationX = -((overlay.Width / 2) - OverlayButton.ButtonSize \ 2) * overlay.Scale
        m_BaseAnchorPoint = New Point(CInt(overlay.TranslationX), CInt(overlay.TranslationY))
    End Sub

    Public Sub DockBottom()
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)
        overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomCenter
        overlay.RotationAngle = 0
        OrientButtons()
        overlay.TranslationY = 0
        m_BaseAnchorPoint = New Point(CInt(overlay.TranslationX), CInt(overlay.TranslationY))
    End Sub

    Public Sub DockLeft()
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)
        overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterLeft
        overlay.RotationAngle = Math.PI / 2
        OrientButtons()
        overlay.TranslationX = -((overlay.Width / 2) - OverlayButton.ButtonSize \ 2) * overlay.Scale
        m_BaseAnchorPoint = New Point(CInt(overlay.TranslationX), CInt(overlay.TranslationY))
    End Sub

    Public Sub DockTop()
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)
        overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter
        overlay.RotationAngle = 0
        OrientButtons()
        overlay.TranslationY = 0
        m_BaseAnchorPoint = New Point(CInt(overlay.TranslationX), CInt(overlay.TranslationY))
    End Sub

    '
    ' Removes the panel from the scene manager
    '
    Public Sub Remove()
        Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager
        Dim collectionBase As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
        collectionBase.Remove(DirectCast(m_Panel, IAgStkGraphicsScreenOverlay))
    End Sub

    '
    ' Private Methods
    '

    '
    ' Orients all of the buttons on the Panel so that they do not rotate with the panel,
    ' but, rather, flip every 90 degrees in order to remain upright.
    '
    Private Sub OrientButtons()
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)
        If (overlay.RotationAngle <= -Math.PI / 4) OrElse (overlay.RotationAngle > 5 * Math.PI / 4) Then
            For Each buttonHolder As OverlayButton In m_ButtonHolders
                If buttonHolder IsNot m_RotateButton AndAlso buttonHolder IsNot m_ScaleButton Then
                    Dim buttonOverlay As IAgStkGraphicsOverlay = DirectCast(buttonHolder.Overlay, IAgStkGraphicsOverlay)
                    buttonOverlay.RotationAngle = Math.PI / 2
                End If
            Next
        ElseIf (overlay.RotationAngle > -Math.PI / 4) AndAlso (overlay.RotationAngle <= Math.PI / 4) Then
            For Each buttonHolder As OverlayButton In m_ButtonHolders
                If buttonHolder IsNot m_RotateButton AndAlso buttonHolder IsNot m_ScaleButton Then
                    Dim buttonOverlay As IAgStkGraphicsOverlay = DirectCast(buttonHolder.Overlay, IAgStkGraphicsOverlay)
                    buttonOverlay.RotationAngle = 0
                End If
            Next
        ElseIf (overlay.RotationAngle > Math.PI / 4) AndAlso (overlay.RotationAngle <= 3 * Math.PI / 4) Then
            For Each buttonHolder As OverlayButton In m_ButtonHolders
                If buttonHolder IsNot m_RotateButton AndAlso buttonHolder IsNot m_ScaleButton Then
                    Dim buttonOverlay As IAgStkGraphicsOverlay = DirectCast(buttonHolder.Overlay, IAgStkGraphicsOverlay)
                    buttonOverlay.RotationAngle = -Math.PI / 2
                End If
            Next
        ElseIf (overlay.RotationAngle > 3 * Math.PI / 4) AndAlso (overlay.RotationAngle <= 5 * Math.PI / 4) Then
            For Each buttonHolder As OverlayButton In m_ButtonHolders
                If buttonHolder IsNot m_RotateButton AndAlso buttonHolder IsNot m_ScaleButton Then
                    Dim buttonOverlay As IAgStkGraphicsOverlay = DirectCast(buttonHolder.Overlay, IAgStkGraphicsOverlay)
                    buttonOverlay.RotationAngle = -Math.PI
                End If
            Next
        End If
    End Sub

    '
    ' Finds a button using a pick result
    '
    Private Function FindButton(picked As IAgStkGraphicsScreenOverlayPickResultCollection) As OverlayButton
        For Each pickResult As IAgStkGraphicsScreenOverlayPickResult In picked
            Dim button As OverlayButton = FindButton(pickResult.Overlay)
            If button IsNot Nothing Then
                Return button
            End If
        Next
        Return Nothing
    End Function

    '
    ' Finds a button using an overlay
    '
    Private Function FindButton(Overlay As IAgStkGraphicsScreenOverlay) As OverlayButton
        For Each button As OverlayButton In m_ButtonHolders
            If button.Overlay Is Overlay Then
                Return button
            End If
        Next
        Return Nothing
    End Function

    '
    ' Finds an overlay panel using a pick result
    '
    Private Function OverlayPanelPicked(picked As IAgStkGraphicsScreenOverlayPickResultCollection) As Boolean
        For Each pickResult As IAgStkGraphicsScreenOverlayPickResult In picked
            If pickResult.Overlay Is m_Panel Then
                Return True
            End If
        Next
        Return False
    End Function

    '
    ' Enables/disables the buttons
    '
    Private Sub EnableButtons(excludeButton As OverlayButton, bPickingEnabled As Boolean)
        For Each button As OverlayButton In m_ButtonHolders
            If button IsNot excludeButton Then
                Dim buttonOverlay As IAgStkGraphicsOverlay = DirectCast(button.Overlay, IAgStkGraphicsOverlay)
                buttonOverlay.PickingEnabled = bPickingEnabled
            End If
        Next
    End Sub

    '
    ' Event handlers
    '

    '
    ' When the mouse is moved
    '
    Public Sub Control3D_MouseMove(sender As Object, e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent)
        Dim picked As IAgStkGraphicsScreenOverlayPickResultCollection = Nothing
        Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager
        If manager.Scenes.Count > 0 Then
            picked = manager.Scenes(0).PickScreenOverlays(e.x, e.y)
        End If

        Dim button As OverlayButton = FindButton(picked)

        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)

        If Not m_Tranforming Then
            If OverlayPanelPicked(picked) AndAlso Not m_PanelCurrentlyPicked Then
                overlay.BorderTranslucency = m_PanelBorderTranslucencyPicked
                overlay.Translucency = m_PanelTranslucencyPicked
                m_PanelCurrentlyPicked = True
                m_PanelTranslucencyChanged = True
            ElseIf Not OverlayPanelPicked(picked) AndAlso m_PanelCurrentlyPicked Then
                overlay.BorderTranslucency = m_PanelBorderTranslucencyRegular
                overlay.Translucency = m_PanelTranslucencyRegular
                m_PanelCurrentlyPicked = False
                m_PanelTranslucencyChanged = True
            End If
            If m_PanelTranslucencyChanged Then
                m_PanelTranslucencyChanged = False
                If Not m_Animating Then
                    If manager.Scenes.Count > 0 Then
                        manager.Scenes(0).Render()
                    End If
                End If
            End If
        End If

        If button IsNot Nothing Then
            If m_MouseOverButton IsNot Nothing AndAlso m_MouseOverButton IsNot button Then
                m_MouseOverButton.MouseLeave()
            End If
            m_MouseOverButton = button
            m_MouseOverButton.MouseEnter()
        Else
            If m_AnchorPoint <> Point.Empty Then
                Dim current As New Point(e.x, e.y)
                current.Offset(m_AnchorPoint)
                Dim offsetX As Integer = (e.x - m_AnchorPoint.X)
                Dim offsetY As Integer = (m_AnchorPoint.Y - e.y)

                ' This fixes the bug with the ScreenOverlayOrigin being different.
                ' Before, if you dragged left with +x to the left, the panel would
                ' have gone right.
                If overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomRight OrElse overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight OrElse overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight Then
                    overlay.TranslationX = m_BaseAnchorPoint.X - offsetX
                Else
                    overlay.TranslationX = m_BaseAnchorPoint.X + offsetX
                End If

                If overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight OrElse overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter OrElse overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft Then
                    overlay.TranslationY = m_BaseAnchorPoint.Y - offsetY
                Else
                    overlay.TranslationY = m_BaseAnchorPoint.Y + offsetY
                End If

                If Not m_Animating Then
                    If manager.Scenes.Count > 0 Then
                        manager.Scenes(0).Render()
                    End If
                End If
            ElseIf m_RotatePoint <> Point.Empty Then
                Dim current As New Point(e.x, e.y)
                current.Offset(m_RotatePoint)
                Dim bounds As Object() = DirectCast(overlay.ControlBounds, Object())
                Dim centerX As Double = CDbl(overlay.ControlPosition.GetValue(0)) + CInt(bounds.GetValue(2)) \ 2
                Dim centerY As Double = CDbl(overlay.ControlPosition.GetValue(1)) + CInt(bounds.GetValue(3)) \ 2
                Dim adjacent As Double = (e.x - centerX)
                Dim opposite As Double = ((m_Control3D.Bounds.Height - e.y) - centerY)

                If adjacent >= 0 Then
                    overlay.RotationAngle = Math.Atan(opposite / adjacent)
                Else
                    overlay.RotationAngle = Math.PI + Math.Atan(opposite / adjacent)
                End If

                OrientButtons()

                If Not m_Animating Then
                    If manager.Scenes.Count > 0 Then
                        manager.Scenes(0).Render()
                    End If
                End If
            ElseIf m_ScalePoint <> Point.Empty Then
                ' Complete rework of scaling..
                Dim scale As Double = 1

                ' Get the cos,sin and tan to make this easier to understand.
                Dim cos As Double = Math.Cos(overlay.RotationAngle)
                Dim sin As Double = Math.Sin(overlay.RotationAngle)
                Dim tan As Double = Math.Tan(overlay.RotationAngle)

                Dim xVector As Double = (e.x - m_ScalePoint.X)
                Dim yVector As Double = (m_ScalePoint.Y - e.y)

                ' Get the projection of e.X and e.Y in the direction
                ' of the toolbar's horizontal.
                Dim x As Double = ((xVector * cos + yVector * sin) * cos)
                Dim y As Double = ((xVector * cos + yVector * sin) * sin)

                ' Figure out if we are shrinking or growing the toolbar
                ' (This is dependant on the quadrant we are in)
                Dim magnitude As Double = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2))
                If sin >= 0 AndAlso cos >= 0 AndAlso tan >= 0 Then
                    magnitude = If((x < 0 Or y < 0), -magnitude, magnitude)
                ElseIf sin >= 0 Then
                    magnitude = If((x > 0 Or y < 0), -magnitude, magnitude)
                ElseIf tan >= 0 Then
                    magnitude = If((x > 0 Or y > 0), -magnitude, magnitude)
                ElseIf cos >= 0 Then
                    magnitude = If((x < 0 Or y > 0), -magnitude, magnitude)
                End If

                scale = ((magnitude + m_ScaleBounds.Width) / m_ScaleBounds.Width)

                If scale < 0 Then
                    scale = 0
                End If


                overlay.Scale = Math.Min(Math.Max(m_StartScale * scale, 0.5), 10)
                Dim width As Double = m_PanelWidth * overlay.Scale
                Dim startWidth As Double = m_PanelWidth * m_StartScale

                ' Translate the toolbar in order to account for the
                ' fact that rotation does not affect the location
                ' of the toolbar, but just rotates the texture.
                ' (This causes the toolbar, if +/-90 degrees to scale
                ' off the screen if not fixed).
                overlay.TranslationX = m_ScaleOffset - (((width / 2) - (startWidth / 2)) * Math.Abs(Math.Sin(overlay.RotationAngle)))

                If Not m_Animating Then
                    If manager.Scenes.Count > 0 Then
                        manager.Scenes(0).Render()
                    End If
                End If
            ElseIf m_MouseOverButton IsNot Nothing Then
                m_MouseOverButton.MouseLeave()
                m_MouseOverButton = Nothing
            End If
        End If
    End Sub

    '
    ' When the mouse pressed
    '
    Public Sub Control3D_MouseDown(sender As Object, e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent)
        Dim picked As IAgStkGraphicsScreenOverlayPickResultCollection = Nothing
        Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager
        If manager.Scenes.Count > 0 Then
            picked = manager.Scenes(0).PickScreenOverlays(e.x, e.y)
        End If

        Dim button As OverlayButton = FindButton(picked)

        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)

        If button Is Nothing AndAlso OverlayPanelPicked(picked) Then
            m_MouseDown = True
            m_Control3D.MouseMode = AgEMouseMode.eMouseModeManual
            m_AnchorPoint = New Point(e.x, e.y)

            overlay.Translucency = m_PanelTranslucencyClicked
            overlay.BorderTranslucency = m_PanelBorderTranslucencyClicked

            If manager.Scenes.Count > 0 Then
                manager.Scenes(0).Render()
            End If
        End If

        If button IsNot Nothing Then
            m_MouseDown = True
            m_MouseOverButton = button
            m_MouseDownButton = button
            m_MouseOverButton.MouseDown()

            If button Is m_RotateButton Then
                m_Tranforming = True
                m_Control3D.MouseMode = AgEMouseMode.eMouseModeManual
                m_RotatePoint = New Point(e.x, e.y)
                EnableButtons(m_RotateButton, False)
            End If
            If button Is m_ScaleButton Then
                m_Tranforming = True
                m_Control3D.MouseMode = AgEMouseMode.eMouseModeManual
                m_ScalePoint = New Point(e.x, e.y)
                m_StartScale = overlay.Scale
                m_ScaleOffset = overlay.TranslationX
                Dim bounds As Object() = DirectCast(overlay.ControlBounds, Object())
                m_ScaleBounds = New Rectangle(CInt(bounds.GetValue(0)), CInt(bounds.GetValue(1)), CInt(bounds.GetValue(2)), CInt(bounds.GetValue(3)))
                EnableButtons(m_ScaleButton, False)
            End If
        End If

        m_LastMouseClick = New Point(e.x, e.y)
    End Sub

    '
    ' When the mouse is unpressed
    '
    Public Sub Control3D_MouseUp(sender As Object, e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent)
        If m_MouseDown Then
            Dim picked As IAgStkGraphicsScreenOverlayPickResultCollection = Nothing
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager
            If manager.Scenes.Count > 0 Then
                picked = manager.Scenes(0).PickScreenOverlays(e.x, e.y)
            End If

            Dim button As OverlayButton = FindButton(picked)

            Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)

            If button Is Nothing AndAlso OverlayPanelPicked(picked) Then
                overlay.Translucency = m_PanelTranslucencyPicked
                overlay.BorderTranslucency = m_PanelBorderTranslucencyPicked
                If Not m_Animating Then
                    If manager.Scenes.Count > 0 Then
                        manager.Scenes(0).Render()
                    End If
                End If
            End If

            If button IsNot Nothing Then
                m_MouseOverButton = button
                m_MouseOverButton.MouseUp()

                ' Check if this button was under the mouse during both the mouse down and mouse up event
                ' (i.e. the button was clicked)
                If m_MouseOverButton Is m_MouseDownButton Then
                    m_MouseOverButton.MouseClick()
                End If
            End If

            m_AnchorPoint = Point.Empty
            m_RotatePoint = Point.Empty
            m_ScalePoint = Point.Empty

            EnableButtons(Nothing, True)
            m_BaseAnchorPoint = New Point(CInt(overlay.TranslationX), CInt(overlay.TranslationY))

            m_Tranforming = False
            m_Control3D.MouseMode = AgEMouseMode.eMouseModeAutomatic
            m_MouseDown = False
        End If
    End Sub

    '
    ' When the mouse is double clicked
    '
    Public Sub Control3D_MouseDoubleClick(sender As Object, e As EventArgs)
        Dim picked As IAgStkGraphicsScreenOverlayPickResultCollection = Nothing
        Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager
        If manager.Scenes.Count > 0 Then
            picked = manager.Scenes(0).PickScreenOverlays(m_LastMouseClick.X, m_LastMouseClick.Y)
        End If

        Dim button As OverlayButton = FindButton(picked)

        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)

        If button Is Nothing AndAlso OverlayPanelPicked(picked) Then
            overlay.TranslationX = 0
            overlay.TranslationY = 0

            overlay.RotationAngle = 0
            overlay.Scale = 1
            OrientButtons()
        End If
    End Sub

    '
    ' Button actions
    '
    Public Sub ShowHideAction()
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Panel, IAgStkGraphicsOverlay)
        If m_Visible Then
            Dim width As Double = CDbl(overlay.Size.GetValue(0))
            Dim height As Double = CDbl(overlay.Size.GetValue(1))
            Dim x As Double = overlay.Scale * ((width / 2) - OverlayButton.ButtonSize \ 2)
            Dim y As Double = overlay.Scale * ((height / 2) - OverlayButton.ButtonSize \ 2)
            Dim z As Double = Math.Sqrt(x * x + y * y)
            Dim panelWidth As Double = m_PanelWidth
            Dim panelHeight As Double = height

            m_PanelWidth = OverlayButton.ButtonSize
            overlay.Width = m_PanelWidth
            m_ButtonHolders(0).Resize(m_PanelWidth)

            For Each button As OverlayButton In m_ButtonHolders
                If button IsNot m_ButtonHolders(0) Then
                    Dim buttonOverlay As IAgStkGraphicsOverlay = DirectCast(button.Overlay, IAgStkGraphicsOverlay)
                    buttonOverlay.Translucency = 1
                End If
            Next

            overlay.TranslationX -= CInt(Math.Truncate(z * Math.Cos(overlay.RotationAngle) - overlay.Scale * panelWidth / 2 + overlay.Scale * OverlayButton.ButtonSize \ 2))
            overlay.TranslationY -= CInt(Math.Truncate(z * Math.Sin(overlay.RotationAngle) - overlay.Scale * panelHeight / 2 + overlay.Scale * OverlayButton.ButtonSize \ 2))
        Else
            m_PanelWidth = CInt(Math.Truncate((m_ButtonHolders.Count - 1.5) * OverlayButton.ButtonSize))
            overlay.Width = m_PanelWidth
            m_ButtonHolders(0).Resize(m_PanelWidth)

            Dim width As Double = CDbl(overlay.Size.GetValue(0))
            Dim height As Double = CDbl(overlay.Size.GetValue(1))
            Dim x As Double = overlay.Scale * ((width / 2) - OverlayButton.ButtonSize \ 2)
            Dim y As Double = overlay.Scale * ((height / 2) - OverlayButton.ButtonSize \ 2)
            Dim z As Double = Math.Sqrt(x * x + y * y)

            For Each button As OverlayButton In m_ButtonHolders
                If button IsNot m_ButtonHolders(0) Then
                    Dim buttonOverlay As IAgStkGraphicsOverlay = DirectCast(button.Overlay, IAgStkGraphicsOverlay)
                    buttonOverlay.Translucency = 0
                End If
            Next

            overlay.TranslationX += CInt(Math.Truncate(z * Math.Cos(overlay.RotationAngle) - overlay.Scale * width / 2 + overlay.Scale * OverlayButton.ButtonSize \ 2))
            overlay.TranslationY += CInt(Math.Truncate(z * Math.Sin(overlay.RotationAngle) - overlay.Scale * height / 2 + overlay.Scale * OverlayButton.ButtonSize \ 2))
        End If

        m_Visible = Not m_Visible
    End Sub

    Public Sub ResetAction()
        m_Root.Rewind()
        m_Animating = False
        EnableStepButtons()
    End Sub

    Public Sub StepReverseAction()
        m_Root.StepBackward()
    End Sub

    Public Sub PlayReverseAction()
        DisableStepButtons()
        m_Root.PlayBackward()
        m_Animating = True
    End Sub

    Public Sub PauseAction()
        m_Root.Pause()
        m_Animating = False
        EnableStepButtons()
    End Sub

    Public Sub PlayForwardAction()
        DisableStepButtons()
        m_Root.PlayForward()
        m_Animating = True
    End Sub

    Public Sub StepForwardAction()
        m_Root.StepForward()
    End Sub

    Public Sub DecreaseDeltaAction()
        m_Root.Slower()
    End Sub

    Public Sub IncreaseDeltaAction()
        m_Root.Faster()
    End Sub

    Public Sub ZoomAction()
        m_Control3D.ZoomIn()
    End Sub

    Public Sub PanAction()
        If Not m_Panning Then
            Try
                m_Root.ExecuteCommand("Window3D * InpDevMode Mode PanLLA")
                m_Panning = True
            Catch generatedExceptionName As Exception
                m_Panning = False
            End Try
        Else
            Try
                m_Root.ExecuteCommand("Window3D * InpDevMode Mode ViewLatLonAlt")
                m_Panning = False
            Catch generatedExceptionName As Exception
                m_Panning = True
            End Try
        End If
    End Sub

    Public Sub HomeAction()
        Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager
        Dim earthAxes As IAgCrdnAxes = DirectCast(m_Root.CentralBodies("Earth").Vgt.Systems("ICRF"), IAgCrdnSystemAssembled).ReferenceAxes.GetAxes()
        If manager.Scenes.Count > 0 Then
            manager.Scenes(0).Camera.ViewCentralBody("Earth", earthAxes)
        End If
        m_Panning = False
    End Sub

    Public Sub MoonAction()
        Dim manager As IAgStkGraphicsSceneManager = DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager
        Dim moonAxes As IAgCrdnAxes = DirectCast(m_Root.CentralBodies("Moon").Vgt.Systems("ICRF"), IAgCrdnSystemAssembled).ReferenceAxes.GetAxes()
        If manager.Scenes.Count > 0 Then
            manager.Scenes(0).Camera.ViewCentralBody("Moon", moonAxes)
        End If
        m_Panning = False
    End Sub

    Public Sub ScaleAction()
    End Sub

    Public Sub RotateAction()
    End Sub

    Private Sub EnableStepButtons()
        m_StepForwardButton.SetEnabled(True)
        m_StepReverseButton.SetEnabled(True)
    End Sub

    Private Sub DisableStepButtons()
        m_StepForwardButton.SetEnabled(False)
        m_StepReverseButton.SetEnabled(False)
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
            If m_Panel IsNot Nothing Then
                m_Panel = Nothing
            End If
            If m_RotateButton IsNot Nothing Then
                m_RotateButton.Dispose()
                m_RotateButton = Nothing
            End If
            If m_ScaleButton IsNot Nothing Then
                m_ScaleButton.Dispose()
                m_ScaleButton = Nothing
            End If
            If m_ZoomButton IsNot Nothing Then
                m_ZoomButton.Dispose()
                m_ZoomButton = Nothing
            End If
            If m_StepForwardButton IsNot Nothing Then
                m_StepForwardButton.Dispose()
                m_StepForwardButton = Nothing
            End If
            If m_StepReverseButton IsNot Nothing Then
                m_StepReverseButton.Dispose()
                m_StepReverseButton = Nothing
            End If
        End If
    End Sub

    '
    ' Members
    '
    Private m_PanelWidth As Integer = CInt(Math.Truncate(OverlayButton.ButtonSize * 0.5))
    Private m_LocationOffset As Integer

    Private m_PanelTranslucencyRegular As Single = 0.95F
    Private m_PanelTranslucencyPicked As Single = 0.85F
    Private m_PanelTranslucencyClicked As Single = 0.8F
    Private m_PanelBorderTranslucencyRegular As Single = 0.6F
    Private m_PanelBorderTranslucencyPicked As Single = 0.5F
    Private m_PanelBorderTranslucencyClicked As Single = 0.4F

    Private m_RotateButton As OverlayButton
    Private m_ScaleButton As OverlayButton
    Private m_ZoomButton As OverlayButton
    Private m_StepForwardButton As OverlayButton
    Private m_StepReverseButton As OverlayButton

    Private m_AnchorPoint As Point = Point.Empty
    Private m_RotatePoint As Point = Point.Empty
    Private m_ScalePoint As Point = Point.Empty
    Private m_BaseAnchorPoint As Point = Point.Empty
    Private m_Panel As IAgStkGraphicsTextureScreenOverlay

    Private m_ScaleBounds As Rectangle
    Private m_StartScale As Double
    Private m_ScaleOffset As Double

    Private m_PanelTranslucencyChanged As Boolean
    Private m_PanelCurrentlyPicked As Boolean
    Private m_Visible As Boolean = True
    Private m_Tranforming As Boolean
    Private m_Animating As Boolean = False
    Private m_Panning As Boolean = False

    Private m_MouseOverButton As OverlayButton
    Private m_MouseDownButton As OverlayButton
    Private m_MouseDown As Boolean
    Private m_LastMouseClick As Point
    Private m_ButtonHolders As List(Of OverlayButton)

    Private m_Root As AgStkObjectRoot
    Private m_Control3D As AxAgUiAxVOCntrl
End Class
