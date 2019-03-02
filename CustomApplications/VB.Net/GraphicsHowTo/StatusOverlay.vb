Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports AGI.STKGraphics

Public MustInherit Class StatusOverlay(Of T As Structure)
    Implements IAgStkGraphicsScreenOverlay
	Protected Sub New(isIndicatorHorizontal As Boolean, minimum As T, maximum As T, manager As IAgStkGraphicsSceneManager)

		Me.New(isIndicatorHorizontal, minimum, maximum, Nothing, Nothing, manager)
	End Sub

    Protected Sub New(isIndicatorHorizontal As Boolean, minimum As T, maximum As T, minimumLabel As String, maximumLabel As String, _
                      manager As IAgStkGraphicsSceneManager)
        m_SceneManager = manager
        m_Overlay = TryCast(m_SceneManager.Initializers.ScreenOverlay.Initialize(5, 5, 40, 40), IAgStkGraphicsOverlay)

        m_IsIndicatorHorizontal = isIndicatorHorizontal
        m_Minimum = minimum
        m_Maximum = maximum
        m_MinText = minimumLabel
        m_MaxText = maximumLabel
    End Sub

	Public Sub SetDefaultStyle()
		Me.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft
        Me.Color = System.Drawing.Color.Black
		Me.Translucency = 0.5F
        Me.BorderColor = System.Drawing.Color.White
		Me.BorderSize = 2
		Me.BorderTranslucency = 0F
	End Sub

	Public ReadOnly Property RealScreenOverlay() As IAgStkGraphicsScreenOverlay
		Get
			Return TryCast(m_Overlay, IAgStkGraphicsScreenOverlay)
		End Get
	End Property

	Public MustOverride Function ValueTransform(value As T) As Double

	Public MustOverride ReadOnly Property Value() As T

	Public MustOverride ReadOnly Property Text() As String

	Public Sub Update(newValue As T, manager As IAgStkGraphicsSceneManager)
		If ValueTransform(LastValue) - ValueTransform(newValue) <> 0 Then
			Indicator.Value = ValueTransform(Value)
			TextOverlayHelper.UpdateTextOverlay(TextOverlay, Text, m_Font, manager)
			LastValue = newValue
			If m_MinTextOverlay IsNot Nothing Then
				DirectCast(m_MinTextOverlay, IAgStkGraphicsOverlay).BringToFront()
			End If
			If m_MaxTextOverlay IsNot Nothing Then
				DirectCast(m_MaxTextOverlay, IAgStkGraphicsOverlay).BringToFront()
			End If
		End If
	End Sub

	Protected Property Indicator() As IndicatorOverlay
		Get
			If m_Indicator Is Nothing Then
				If m_IsIndicatorHorizontal Then
					m_Indicator = New IndicatorOverlay(0, 0, DirectCast(TextOverlay, IAgStkGraphicsOverlay).Width, 20, ValueTransform(m_Minimum), ValueTransform(m_Maximum), _
						m_IsIndicatorHorizontal, IndicatorStyle.Marker, m_SceneManager)
					m_Indicator.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomCenter
                    Me.Size = New Object() _
                        { _
                            DirectCast(TextOverlay, IAgStkGraphicsOverlay).Width, _
                            DirectCast(TextOverlay, IAgStkGraphicsOverlay).Height + m_Indicator.Height, _
                            Me.Size.GetValue(2), Me.Size.GetValue(3) _
                        }
				Else
					m_Indicator = New IndicatorOverlay(0, 0, 30, DirectCast(TextOverlay, IAgStkGraphicsOverlay).Height, ValueTransform(m_Minimum), ValueTransform(m_Maximum), _
						m_IsIndicatorHorizontal, IndicatorStyle.Marker, m_SceneManager)
					m_Indicator.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterLeft
                    Me.Size = New Object() _
                        { _
                            DirectCast(TextOverlay, IAgStkGraphicsOverlay).Width + m_Indicator.Width, _
                            DirectCast(TextOverlay, IAgStkGraphicsOverlay).Height, _
                            Me.Size.GetValue(2), Me.Size.GetValue(3) _
                        }
				End If

				m_Indicator.Value = ValueTransform(Value)
				m_Indicator.BorderSize = 1
                m_Indicator.BorderColor = System.Drawing.Color.White
                m_Indicator.Color = System.Drawing.Color.Black
				m_Indicator.Translucency = 0.5F
				Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(Me.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
				overlayManager.Add(m_Indicator.RealScreenOverlay)

                If Not String.IsNullOrEmpty(m_MinText) AndAlso Not String.IsNullOrEmpty(m_MaxText) Then
                    m_MinTextOverlay = TextOverlayHelper.CreateTextOverlay(m_MinText, m_LabelFont, m_SceneManager)
                    m_MaxTextOverlay = TextOverlayHelper.CreateTextOverlay(m_MaxText, m_LabelFont, m_SceneManager)

                    DirectCast(m_MinTextOverlay, IAgStkGraphicsOverlay).Origin = If(m_IsIndicatorHorizontal, AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterLeft, AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomCenter)
                    DirectCast(m_MaxTextOverlay, IAgStkGraphicsOverlay).Origin = If(m_IsIndicatorHorizontal, AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight, AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter)

                    Dim indicatorOverlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(m_Indicator.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
                    indicatorOverlayManager.Add(DirectCast(m_MinTextOverlay, IAgStkGraphicsScreenOverlay))
                    indicatorOverlayManager.Add(DirectCast(m_MaxTextOverlay, IAgStkGraphicsScreenOverlay))

                End If
			End If
			Return m_Indicator
		End Get
		Set
			m_Indicator = value
		End Set
	End Property

	Protected Property TextOverlay() As IAgStkGraphicsTextureScreenOverlay
		Get
			If m_TextOverlay Is Nothing Then
				m_TextOverlay = TextOverlayHelper.CreateTextOverlay(Text, m_Font, m_SceneManager)
				DirectCast(m_TextOverlay, IAgStkGraphicsOverlay).BorderSize = 1
                DirectCast(m_TextOverlay, IAgStkGraphicsOverlay).BorderColor = System.Drawing.Color.White

				If m_IsIndicatorHorizontal Then
					DirectCast(m_TextOverlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter
				Else
					DirectCast(m_TextOverlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight
				End If
			End If
			Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(Me.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
			overlayManager.Add(DirectCast(m_TextOverlay, IAgStkGraphicsScreenOverlay))
			Return m_TextOverlay
		End Get
		Set
			m_TextOverlay = value
		End Set
	End Property

	Protected Property LastValue() As T
		Get
			Return m_LastValue
		End Get
		Set
			m_LastValue = value
		End Set
	End Property

	Protected Property Font() As Font
		Get
			Return m_Font
		End Get
		Set
			m_Font = value
		End Set
	End Property

	Private m_IsIndicatorHorizontal As Boolean = False
	Private m_Font As New Font("Arial", 10, FontStyle.Bold)
	Private m_LabelFont As New Font("Arial Narrow", 8, FontStyle.Bold)
	Private m_MinText As String
	Private m_MaxText As String

	Private m_MinTextOverlay As IAgStkGraphicsTextureScreenOverlay = Nothing
	Private m_MaxTextOverlay As IAgStkGraphicsTextureScreenOverlay = Nothing
	Private m_TextOverlay As IAgStkGraphicsTextureScreenOverlay = Nothing
	Private m_Indicator As IndicatorOverlay = Nothing

	Private m_LastValue As New T()
	Private m_Minimum As New T()
	Private m_Maximum As New T()

	Private m_SceneManager As IAgStkGraphicsSceneManager

	#Region "_IAgStkGraphicsScreenOverlay Members"

    Public Property BorderColor() As System.Drawing.Color
        Get
            Return m_Overlay.BorderColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            m_Overlay.BorderColor = Value
        End Set
    End Property

	Public Property BorderSize() As Integer
		Get
			Return m_Overlay.BorderSize
		End Get
		Set
			m_Overlay.BorderSize = value
		End Set
	End Property

	Public Property BorderTranslucency() As Single
		Get
			Return m_Overlay.BorderTranslucency
		End Get
		Set
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
		Set
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

	Public Function ControlToOverlay(X As Double, Y As Double) As Array
		Return m_Overlay.ControlToOverlay(X, Y)
	End Function

	Public Property Display() As Boolean
		Get
			Return m_Overlay.Display
		End Get
		Set
			m_Overlay.Display = value
		End Set
	End Property

	Public Property DisplayCondition() As IAgStkGraphicsDisplayCondition
		Get
			Return m_Overlay.DisplayCondition
		End Get
		Set
			m_Overlay.DisplayCondition = value
		End Set
	End Property

	Public Property FlipX() As Boolean
		Get
			Return m_Overlay.FlipX
		End Get
		Set
			m_Overlay.FlipX = value
		End Set
	End Property

	Public Property FlipY() As Boolean
		Get
			Return m_Overlay.FlipY
		End Get
		Set
			m_Overlay.FlipY = value
		End Set
	End Property

	Public Property Height() As Double
		Get
			Return m_Overlay.Height
		End Get
		Set
			m_Overlay.Height = value
		End Set
	End Property

	Public Property HeightUnit() As AgEStkGraphicsScreenOverlayUnit
		Get
			Return m_Overlay.HeightUnit
		End Get
		Set
			m_Overlay.HeightUnit = value
		End Set
	End Property

	Public Property MaximumSize() As Array
		Get
			Return m_Overlay.MaximumSize
		End Get
		Set
			m_Overlay.MaximumSize = value
		End Set
	End Property

	Public Property MinimumSize() As Array
		Get
			Return m_Overlay.MinimumSize
		End Get
		Set
			m_Overlay.MinimumSize = value
		End Set
	End Property

	Public Property Origin() As AgEStkGraphicsScreenOverlayOrigin
		Get
			Return m_Overlay.Origin
		End Get
		Set
			m_Overlay.Origin = value
		End Set
	End Property

	Public Function OverlayToControl(X As Double, Y As Double) As Array
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
		Set
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
		Set
			m_Overlay.PickingEnabled = value
		End Set
	End Property

	Public Property PinningOrigin() As AgEStkGraphicsScreenOverlayPinningOrigin
		Get
			Return m_Overlay.PinningOrigin
		End Get
		Set
			m_Overlay.PinningOrigin = value
		End Set
	End Property

	Public Property PinningPosition() As Array
		Get
			Return m_Overlay.PinningPosition
		End Get
		Set
			m_Overlay.PinningPosition = value
		End Set
	End Property

	Public Property Position() As Array
		Get
			Return m_Overlay.Position
		End Get
		Set
			m_Overlay.Position = value
		End Set
	End Property

	Public Property RotationAngle() As Double
		Get
			Return m_Overlay.RotationAngle
		End Get
		Set
			m_Overlay.RotationAngle = value
		End Set
	End Property

	Public Property RotationPoint() As Array
		Get
			Return m_Overlay.RotationPoint
		End Get
		Set
			m_Overlay.RotationPoint = value
		End Set
	End Property

	Public Property Scale() As Double
		Get
			Return m_Overlay.Scale
		End Get
		Set
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
		Set
			m_Overlay.Size = value
		End Set
	End Property

	Public Property TranslationX() As Double
		Get
			Return m_Overlay.TranslationX
		End Get
		Set
			m_Overlay.TranslationX = value
		End Set
	End Property

	Public Property TranslationY() As Double
		Get
			Return m_Overlay.TranslationY
		End Get
		Set
			m_Overlay.TranslationY = value
		End Set
	End Property

	Public Property Translucency() As Single
		Get
			Return m_Overlay.Translucency
		End Get
		Set
			m_Overlay.Translucency = value
		End Set
	End Property

	Public Property Width() As Double
		Get
			Return m_Overlay.Width
		End Get
		Set
			m_Overlay.Width = value
		End Set
	End Property

	Public Property WidthUnit() As AgEStkGraphicsScreenOverlayUnit
		Get
			Return m_Overlay.WidthUnit
		End Get
		Set
			m_Overlay.WidthUnit = value
		End Set
	End Property

	Public Property X() As Double
		Get
			Return m_Overlay.X
		End Get
		Set
			m_Overlay.X = value
		End Set
	End Property

	Public Property XUnit() As AgEStkGraphicsScreenOverlayUnit
		Get
			Return m_Overlay.XUnit
		End Get
		Set
			m_Overlay.XUnit = value
		End Set
	End Property

	Public Property Y() As Double
		Get
			Return m_Overlay.Y
		End Get
		Set
			m_Overlay.Y = value
		End Set
	End Property

	Public Property YUnit() As AgEStkGraphicsScreenOverlayUnit
		Get
			Return m_Overlay.YUnit
		End Get
		Set
			m_Overlay.YUnit = value
		End Set
	End Property

	Private m_Overlay As IAgStkGraphicsOverlay

	#End Region
End Class
