Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports AGI.STKGraphics


Public Enum IndicatorStyle
	Bar
	Marker
End Enum

Public Enum IndicatorLabelType
	None
	Percent
	ValueOfMax
End Enum

Public Class IndicatorOverlay
    Implements IAgStkGraphicsScreenOverlay
    Public Sub New(ByVal position As Array, ByVal size As Array, ByVal range As Interval, _
                   ByVal isHorizontal As Boolean, ByVal indicatorStyle As IndicatorStyle, ByVal manager As IAgStkGraphicsSceneManager)
        m_SceneManager = manager
        m_Overlay = TryCast(m_SceneManager.Initializers.ScreenOverlay.InitializeWithPosAndSize(position, size), IAgStkGraphicsOverlay)

        m_IsHorizontal = isHorizontal
        m_Intervals = New List(Of IntervalOverlay)()
        m_Range = CheckValues(range)
        m_Value = m_Range.Minimum
        m_IndicatorStyle = IndicatorStyle
        m_MarkerSize = 1
    End Sub

    Public Sub New(ByVal xPixels As Double, ByVal yPixels As Double, ByVal widthPixels As Double, ByVal heightPixels As Double, _
                   ByVal minValue As Double, ByVal maxValue As Double, ByVal isHorizontal As Boolean, _
                   ByVal indicatorStyle As IndicatorStyle, ByVal manager As IAgStkGraphicsSceneManager)
        'convenience method
        Me.New(New Object() {xPixels, yPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}, _
               New Object() {widthPixels, heightPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}, _
               New Interval(minValue, maxValue), isHorizontal, indicatorStyle, manager)
    End Sub

    Public ReadOnly Property RealScreenOverlay() As IAgStkGraphicsScreenOverlay
        Get
            Return TryCast(m_Overlay, IAgStkGraphicsScreenOverlay)
        End Get
    End Property

    Public ReadOnly Property Range() As Interval
        Get
            Return m_Range
        End Get
    End Property

    Public Property Value() As Double
        Get
            Return m_Value
        End Get
        Set(ByVal value As Double)
            m_Value = If((Double.IsPositiveInfinity(value)), Double.MaxValue, (If((Double.IsNegativeInfinity(value)), Double.MinValue, value)))
            UpdateForeground()
            UpdateIntervals()
        End Set
    End Property

    Public Property ForegroundColor() As System.Drawing.Color
        Get
            Return DirectCast(m_Foreground, IAgStkGraphicsOverlay).Color
        End Get
        Set(ByVal value As System.Drawing.Color)
            DirectCast(m_Foreground, IAgStkGraphicsOverlay).Color = value
        End Set
    End Property

    Public Property MarkerSize() As Double
        Get
            Return m_MarkerSize
        End Get
        Set(ByVal value As Double)
            m_MarkerSize = value
            Select Case m_IndicatorStyle
                Case IndicatorStyle.Bar
                    Exit Select
                Case IndicatorStyle.Marker
                    DirectCast(m_Foreground, IAgStkGraphicsOverlay).Size = GetIndicationMarkerSize(m_MarkerSize)
                    Exit Select
                Case Else
                    Exit Select

            End Select
        End Set
    End Property

    Private Sub CreateForeground()
        Dim foregroundPosition As Array = New Object() {0, 0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}
        Dim foregroundSize As Array = New Object() {0, 0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}
        m_Foreground = m_SceneManager.Initializers.ScreenOverlay.InitializeWithPosAndSize(foregroundPosition, foregroundSize)
        Select Case m_IndicatorStyle
            Case IndicatorStyle.Bar
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).Color = System.Drawing.Color.Green
                Exit Select
            Case IndicatorStyle.Marker
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).Color = System.Drawing.Color.LimeGreen
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).BorderSize = 1
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).BorderColor = System.Drawing.Color.Black
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).BorderTranslucency = 0.5F
                Exit Select
            Case Else
                Exit Select
        End Select
        DirectCast(m_Foreground, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft
        Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(Me.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
        overlayManager.Add(m_Foreground)
        UpdateForeground()
    End Sub

    Private Sub UpdateForeground()
        If m_Foreground Is Nothing Then
            CreateForeground()
        End If
        Select Case m_IndicatorStyle
            Case IndicatorStyle.Bar
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).Size = GetIndicationBarSize(Value)
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).Position = GetIndicationPosition(0)
                Exit Select
            Case IndicatorStyle.Marker
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).Size = GetIndicationMarkerSize(MarkerSize)
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).Position = GetIndicationPosition(Value)
                DirectCast(m_Foreground, IAgStkGraphicsOverlay).BringToFront()
                Exit Select
            Case Else
                Exit Select
        End Select

    End Sub

    Private Sub UpdateIntervals()
        For Each interval As IntervalOverlay In m_Intervals
            Select Case interval.IntervalStyle
                Case IndicatorStyle.Bar
                    DirectCast(interval.Marker, IAgStkGraphicsOverlay).Translucency = 0.6F
                    If interval.Range.Minimum < Value AndAlso Value < interval.Range.Maximum Then
                        DirectCast(interval.Marker, IAgStkGraphicsOverlay).Translucency = 0.2F
                    End If
                    Exit Select
                Case IndicatorStyle.Marker
                    DirectCast(interval.Marker, IAgStkGraphicsOverlay).Size = GetIndicationMarkerSize(MarkerSize)
                    Exit Select
                Case Else
                    Exit Select
            End Select
        Next
    End Sub

    ''' <summary>
    ''' Turns a value into a fraction of this indication's entire range. Returns [0.0-1.0]
    ''' </summary>
    Private Function GetIndicationFraction(ByVal value As Double) As Double
        Return Math.Min(Math.Max(Range.Minimum, value), Range.Maximum) / (Range.Maximum - Range.Minimum)
    End Function

    ''' <summary>
    ''' Calculate indication bar size based on the given value.
    ''' </summary>
    Private Function GetIndicationBarSize(ByVal value As Double) As Array
        value = Math.Max(GetIndicationFraction(value), 0.0001)
        'can't be zero
        If m_IsHorizontal Then
            Return New Object() {value, 1.0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction}
        Else
            Return New Object() {1.0, value, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction}
        End If
    End Function

    ''' <summary>
    ''' Calculate indication marker size based on the given value.
    ''' </summary>
    Private Function GetIndicationMarkerSize(ByVal markerWidthPixels As Double) As Array
        markerWidthPixels = Math.Max(markerWidthPixels, 0.0001)
        'can't be zero
        If m_IsHorizontal Then
            Return New Object() {markerWidthPixels, 1.0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction}
        Else
            Return New Object() {1.0, markerWidthPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels}
        End If
    End Function

    ''' <summary>
    ''' Get the indication position based on value and direction.
    ''' </summary>
    Private Function GetIndicationPosition(ByVal value As Double) As Array
        If m_IsHorizontal Then
            Return New Object() {GetIndicationFraction(value), 0, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction}
        Else
            Return New Object() {0, GetIndicationFraction(value), AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction}
        End If
    End Function

    Private Structure IntervalOverlay
        Public Sub New(ByVal range As Interval, ByVal style As IndicatorStyle, ByVal color As System.Drawing.Color, ByVal parent As IndicatorOverlay, ByVal manager As IAgStkGraphicsSceneManager)
            m_Range = CheckValues(range)
            m_IntervalStyle = style

            Select Case m_IntervalStyle
                Case IndicatorStyle.Bar
                    Dim rangeSize As Double = Math.Min(m_Range.Maximum - Math.Max(m_Range.Minimum, 0), parent.Range.Maximum - Math.Max(m_Range.Minimum, 0))
                    Dim barPosition As Array = parent.GetIndicationPosition(m_Range.Minimum)
                    Dim barSize As Array = parent.GetIndicationBarSize(rangeSize)
                    m_Marker = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(barPosition, barSize)

                    DirectCast(m_Marker, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft
                    DirectCast(m_Marker, IAgStkGraphicsOverlay).Color = color
                    DirectCast(m_Marker, IAgStkGraphicsOverlay).Translucency = 0.6F
                    m_MarkerTwo = Nothing
                    Exit Select
                Case IndicatorStyle.Marker
                    'make start and end markers
                    Dim size As Array = parent.GetIndicationMarkerSize(parent.MarkerSize)
                    Dim markerPosition As Array = parent.GetIndicationPosition(m_Range.Minimum)
                    m_Marker = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(markerPosition, size)

                    DirectCast(m_Marker, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft
                    DirectCast(m_Marker, IAgStkGraphicsOverlay).Color = color

                    Dim markerTwoPosition As Array = parent.GetIndicationPosition(m_Range.Maximum)
                    m_MarkerTwo = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(markerTwoPosition, size)

                    DirectCast(m_MarkerTwo, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft
                    DirectCast(m_MarkerTwo, IAgStkGraphicsOverlay).Color = color
                    Exit Select
                Case Else
                    m_Marker = Nothing
                    m_MarkerTwo = Nothing
                    Exit Select

            End Select
        End Sub

        Friend ReadOnly Property Range() As Interval
            Get
                Return m_Range
            End Get
        End Property

        Friend ReadOnly Property Marker() As IAgStkGraphicsScreenOverlay
            Get
                Return m_Marker
            End Get
        End Property

        Friend ReadOnly Property MarkerTwo() As IAgStkGraphicsScreenOverlay
            Get
                Return m_MarkerTwo
            End Get
        End Property

        Friend ReadOnly Property IntervalStyle() As IndicatorStyle
            Get
                Return m_IntervalStyle
            End Get
        End Property

        Private ReadOnly m_Range As Interval
        Private ReadOnly m_Marker As IAgStkGraphicsScreenOverlay
        Private ReadOnly m_MarkerTwo As IAgStkGraphicsScreenOverlay
        Private ReadOnly m_IntervalStyle As IndicatorStyle
    End Structure

    Public Sub AddInterval(ByVal minimum As Double, ByVal maximum As Double, ByVal style As IndicatorStyle, ByVal manager As IAgStkGraphicsSceneManager)
        AddInterval(minimum, maximum, style, System.Drawing.Color.LightBlue, manager)
    End Sub

    Public Sub AddInterval(ByVal minimum As Double, ByVal maximum As Double, ByVal style As IndicatorStyle, ByVal color As System.Drawing.Color, ByVal manager As IAgStkGraphicsSceneManager)
        AddInterval(New Interval(minimum, maximum), style, color, manager)
    End Sub

    Public Sub AddInterval(ByVal range As Interval, ByVal style As IndicatorStyle, ByVal color As System.Drawing.Color, ByVal manager As IAgStkGraphicsSceneManager)
        range = CheckValues(range)
        Dim interval As New IntervalOverlay
        If Not TryGetInterval(range, interval) Then
            interval = New IntervalOverlay(range, style, color, Me, manager)
            m_Intervals.Add(interval)
            Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(Me.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
            overlayManager.Add(interval.Marker)
            If interval.MarkerTwo IsNot Nothing Then
                overlayManager.Add(interval.MarkerTwo)
            End If
        End If
    End Sub

    Public Sub RemoveInterval(ByVal minimum As Double, ByVal maximum As Double)
        RemoveInterval(New Interval(minimum, maximum))
    End Sub

    Public Sub RemoveInterval(ByVal range As Interval)
        range = CheckValues(range)
        Dim interval As New IntervalOverlay
        If TryGetInterval(range, interval) Then
            Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(Me.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
            overlayManager.Remove(interval.Marker)
            If interval.MarkerTwo IsNot Nothing Then
                overlayManager.Remove(interval.MarkerTwo)
            End If
            m_Intervals.Remove(interval)
        End If
    End Sub

    Private Function TryGetInterval(ByVal range As Interval, ByRef interval As IntervalOverlay) As Boolean
        For Each i As IntervalOverlay In m_Intervals
            If i.Range = range Then
                interval = i
                Return True
            End If
        Next
        interval = New IntervalOverlay()
        Return False
    End Function

    Private Shared Function CheckValues(ByVal range As Interval) As Interval
        If Double.IsInfinity(range.Minimum) Then
            range.Minimum = Double.MinValue
        End If
        If Double.IsInfinity(range.Maximum) Then
            range.Maximum = Double.MaxValue
        End If
        Return range
    End Function

    Private m_Foreground As IAgStkGraphicsScreenOverlay
    Private m_Intervals As List(Of IntervalOverlay)

    Private m_Value As Double
    Private m_MarkerSize As Double

    Private ReadOnly m_Range As Interval
    Private ReadOnly m_IndicatorStyle As IndicatorStyle
    Private ReadOnly m_IsHorizontal As Boolean

    Private m_SceneManager As IAgStkGraphicsSceneManager


#Region "_IAgStkGraphicsScreenOverlay Members"

    Public Property BorderColor() As System.Drawing.Color
        Get
            Return m_Overlay.BorderColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            m_Overlay.BorderColor = value
        End Set
    End Property

    Public Property BorderSize() As Integer
        Get
            Return m_Overlay.BorderSize
        End Get
        Set(ByVal value As Integer)
            m_Overlay.BorderSize = value
        End Set
    End Property

    Public Property BorderTranslucency() As Single
        Get
            Return m_Overlay.BorderTranslucency
        End Get
        Set(ByVal value As Single)
            m_Overlay.BorderTranslucency = value
        End Set
    End Property

    Public ReadOnly Property Bounds() As Array
        Get
            Return m_Overlay.Bounds
        End Get
    End Property

    Public Sub BringToFront()
        m_Overlay.BringToFront()
    End Sub

    Public Property ClipToParent() As Boolean
        Get
            Return m_Overlay.ClipToParent
        End Get
        Set(ByVal value As Boolean)
            m_Overlay.ClipToParent = value
        End Set
    End Property

    Public Property Color() As System.Drawing.Color
        Get
            Return m_Overlay.Color
        End Get
        Set(ByVal value As System.Drawing.Color)
            m_Overlay.Color = value
        End Set
    End Property

    Public ReadOnly Property ControlBounds() As Array
        Get
            Return m_Overlay.ControlBounds
        End Get
    End Property

    Public ReadOnly Property ControlPosition() As Array
        Get
            Return m_Overlay.ControlPosition
        End Get
    End Property

    Public ReadOnly Property ControlSize() As Array
        Get
            Return m_Overlay.ControlSize
        End Get
    End Property

    Public Function ControlToOverlay(ByVal X As Double, ByVal Y As Double) As Array
        Return m_Overlay.ControlToOverlay(X, Y)
    End Function

    Public Property Display() As Boolean
        Get
            Return m_Overlay.Display
        End Get
        Set(ByVal value As Boolean)
            m_Overlay.Display = value
        End Set
    End Property

    Public Property DisplayCondition() As IAgStkGraphicsDisplayCondition
        Get
            Return m_Overlay.DisplayCondition
        End Get
        Set(ByVal value As IAgStkGraphicsDisplayCondition)
            m_Overlay.DisplayCondition = value
        End Set
    End Property

    Public Property FlipX() As Boolean
        Get
            Return m_Overlay.FlipX
        End Get
        Set(ByVal value As Boolean)
            m_Overlay.FlipX = value
        End Set
    End Property

    Public Property FlipY() As Boolean
        Get
            Return m_Overlay.FlipY
        End Get
        Set(ByVal value As Boolean)
            m_Overlay.FlipY = value
        End Set
    End Property

    Public Property Height() As Double
        Get
            Return m_Overlay.Height
        End Get
        Set(ByVal value As Double)
            m_Overlay.Height = value
        End Set
    End Property

    Public Property HeightUnit() As AgEStkGraphicsScreenOverlayUnit
        Get
            Return m_Overlay.HeightUnit
        End Get
        Set(ByVal value As AgEStkGraphicsScreenOverlayUnit)
            m_Overlay.HeightUnit = value
        End Set
    End Property

    Public Property MaximumSize() As Array
        Get
            Return m_Overlay.MaximumSize
        End Get
        Set(ByVal value As Array)
            m_Overlay.MaximumSize = value
        End Set
    End Property

    Public Property MinimumSize() As Array
        Get
            Return m_Overlay.MinimumSize
        End Get
        Set(ByVal value As Array)
            m_Overlay.MinimumSize = value
        End Set
    End Property

    Public Property Origin() As AgEStkGraphicsScreenOverlayOrigin
        Get
            Return m_Overlay.Origin
        End Get
        Set(ByVal value As AgEStkGraphicsScreenOverlayOrigin)
            m_Overlay.Origin = value
        End Set
    End Property

    Public Function OverlayToControl(ByVal X As Double, ByVal Y As Double) As Array
        Return m_Overlay.OverlayToControl(X, Y)
    End Function

    Public ReadOnly Property Overlays() As IAgStkGraphicsScreenOverlayCollection
        Get
            Return m_Overlay.Overlays
        End Get
    End Property

    Public Property Padding() As Array
        Get
            Return m_Overlay.Padding
        End Get
        Set(ByVal value As Array)
            m_Overlay.Padding = value
        End Set
    End Property

    Public ReadOnly Property Parent() As IAgStkGraphicsScreenOverlayContainer
        Get
            Return m_Overlay.Parent
        End Get
    End Property

    Public Property PickingEnabled() As Boolean
        Get
            Return m_Overlay.PickingEnabled
        End Get
        Set(ByVal value As Boolean)
            m_Overlay.PickingEnabled = value
        End Set
    End Property

    Public Property PinningOrigin() As AgEStkGraphicsScreenOverlayPinningOrigin
        Get
            Return m_Overlay.PinningOrigin
        End Get
        Set(ByVal value As AgEStkGraphicsScreenOverlayPinningOrigin)
            m_Overlay.PinningOrigin = value
        End Set
    End Property

    Public Property PinningPosition() As Array
        Get
            Return m_Overlay.PinningPosition
        End Get
        Set(ByVal value As Array)
            m_Overlay.PinningPosition = value
        End Set
    End Property

    Public Property Position() As Array
        Get
            Return m_Overlay.Position
        End Get
        Set(ByVal value As Array)
            m_Overlay.Position = value
        End Set
    End Property

    Public Property RotationAngle() As Double
        Get
            Return m_Overlay.RotationAngle
        End Get
        Set(ByVal value As Double)
            m_Overlay.RotationAngle = value
        End Set
    End Property

    Public Property RotationPoint() As Array
        Get
            Return m_Overlay.RotationPoint
        End Get
        Set(ByVal value As Array)
            m_Overlay.RotationPoint = value
        End Set
    End Property

    Public Property Scale() As Double
        Get
            Return m_Overlay.Scale
        End Get
        Set(ByVal value As Double)
            m_Overlay.Scale = value
        End Set
    End Property

    Public Sub SendToBack()
        m_Overlay.SendToBack()
    End Sub

    Public Property Size() As Array
        Get
            Return m_Overlay.Size
        End Get
        Set(ByVal value As Array)
            m_Overlay.Size = value
        End Set
    End Property

    Public Property TranslationX() As Double
        Get
            Return m_Overlay.TranslationX
        End Get
        Set(ByVal value As Double)
            m_Overlay.TranslationX = value
        End Set
    End Property

    Public Property TranslationY() As Double
        Get
            Return m_Overlay.TranslationY
        End Get
        Set(ByVal value As Double)
            m_Overlay.TranslationY = value
        End Set
    End Property

    Public Property Translucency() As Single
        Get
            Return m_Overlay.Translucency
        End Get
        Set(ByVal value As Single)
            m_Overlay.Translucency = value
        End Set
    End Property

    Public Property Width() As Double
        Get
            Return m_Overlay.Width
        End Get
        Set(ByVal value As Double)
            m_Overlay.Width = value
        End Set
    End Property

    Public Property WidthUnit() As AgEStkGraphicsScreenOverlayUnit
        Get
            Return m_Overlay.WidthUnit
        End Get
        Set(ByVal value As AgEStkGraphicsScreenOverlayUnit)
            m_Overlay.WidthUnit = value
        End Set
    End Property

    Public Property X() As Double
        Get
            Return m_Overlay.X
        End Get
        Set(ByVal value As Double)
            m_Overlay.X = value
        End Set
    End Property

    Public Property XUnit() As AgEStkGraphicsScreenOverlayUnit
        Get
            Return m_Overlay.XUnit
        End Get
        Set(ByVal value As AgEStkGraphicsScreenOverlayUnit)
            m_Overlay.XUnit = value
        End Set
    End Property

    Public Property Y() As Double
        Get
            Return m_Overlay.Y
        End Get
        Set(ByVal value As Double)
            m_Overlay.Y = value
        End Set
    End Property

    Public Property YUnit() As AgEStkGraphicsScreenOverlayUnit
        Get
            Return m_Overlay.YUnit
        End Get
        Set(ByVal value As AgEStkGraphicsScreenOverlayUnit)
            m_Overlay.YUnit = value
        End Set
    End Property

    Private m_Overlay As IAgStkGraphicsOverlay

#End Region
End Class
