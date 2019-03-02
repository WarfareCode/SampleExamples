Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports System.Drawing

Public Delegate Sub ActionDelegate()

'
' OverlayButtonHolder
'
Public Class OverlayButton
    Implements IDisposable
    '
    ' Properties
    '
    Public ReadOnly Property Overlay() As IAgStkGraphicsScreenOverlay
        Get
            Return DirectCast(m_Overlay, IAgStkGraphicsScreenOverlay)
        End Get
    End Property
    Public Shared ReadOnly Property ButtonSize() As Integer
        Get
            Return m_ButtonSize
        End Get
    End Property

    '
    ' Constructors
    '
    Public Sub New(ByVal action As ActionDelegate, ByVal image As String, ByVal xOffset As Integer, ByVal panelWidth As Double, ByVal root As AgStkObjectRoot)
        m_Manager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

        m_Action = action
        m_MouseEnterTranslucency = 0.01F
        m_MouseExitTranslucency = 0.25F

        m_Overlay = m_Manager.Initializers.TextureScreenOverlay.Initialize()

        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Overlay, IAgStkGraphicsOverlay)
        overlay.X = CDbl(xOffset) / panelWidth
        overlay.XUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction
        overlay.Width = CDbl(ButtonSize) / panelWidth
        overlay.WidthUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction
        overlay.Height = 1
        overlay.HeightUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction
        overlay.Translucency = m_MouseExitTranslucency

        m_Overlay.Texture = m_Manager.Textures.LoadFromStringUri(image)

        m_Enabled = True
        m_DisabledImage = image
        m_EnabledImage = m_DisabledImage

        m_Offset = xOffset
    End Sub

    Public Sub New(ByVal action As ActionDelegate, ByVal image As String, ByVal xOffset As Integer, ByVal panelWidth As Double, ByVal scale As Double, ByVal rotate As Double, _
     ByVal root As AgStkObjectRoot)
        Me.New(action, image, xOffset, panelWidth, root)
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Overlay, IAgStkGraphicsOverlay)
        overlay.Scale = scale
        overlay.RotationAngle = rotate
    End Sub

    '
    ' Public Methods
    '

    '
    ' Sets both the enabled image and disabled image to the input image
    '
    Public Sub SetTexture(ByVal image As String)
        m_EnabledImage = image
        m_DisabledImage = image
    End Sub

    '
    ' Sets both an enabled image and a disabled image for an on/off button
    '
    Public Sub SetTexture(ByVal enabledImage As String, ByVal disabledImage As String)
        m_EnabledImage = enabledImage
        m_DisabledImage = disabledImage
    End Sub

    '
    ' Sets the on/off texture of a button
    '
    Public Sub SetState(ByVal state As Boolean)
        m_State = state

        If state Then
            m_Overlay.Texture = m_Manager.Textures.LoadFromStringUri(m_EnabledImage)
        Else
            m_Overlay.Texture = m_Manager.Textures.LoadFromStringUri(m_DisabledImage)
        End If
    End Sub

    '
    ' Enables or disables a button
    '
    Public Sub SetEnabled(ByVal enabled As Boolean)
        m_Enabled = enabled
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Overlay, IAgStkGraphicsOverlay)

        If enabled Then
            overlay.Color = System.Drawing.Color.White
        Else
            overlay.Color = System.Drawing.Color.Gray
        End If
    End Sub

    Public Sub Resize(ByVal panelWidth As Double)
        Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Overlay, IAgStkGraphicsOverlay)

        overlay.Width = CDbl(ButtonSize) / panelWidth
        overlay.WidthUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction
        overlay.Height = 1
        overlay.HeightUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction

        overlay.X = CDbl(m_Offset) / CDbl(panelWidth)
        overlay.XUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction
    End Sub

    '
    ' Event handlers
    '
    Public Overridable Sub MouseEnter()
        If m_Enabled Then
            Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Overlay, IAgStkGraphicsOverlay)
            overlay.Translucency = m_MouseEnterTranslucency
            If m_Manager.Scenes.Count > 0 Then
                m_Manager.Scenes(0).Render()
            End If
        End If
    End Sub
    Public Overridable Sub MouseLeave()
        If m_Enabled Then
            Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Overlay, IAgStkGraphicsOverlay)
            overlay.Translucency = m_MouseExitTranslucency
            overlay.Color = System.Drawing.Color.White
            If m_Manager.Scenes.Count > 0 Then
                m_Manager.Scenes(0).Render()
            End If
        End If
    End Sub
    Public Overridable Sub MouseDown()
        If m_Enabled Then
            Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Overlay, IAgStkGraphicsOverlay)
            overlay.Color = System.Drawing.Color.DarkGray
            If m_Manager.Scenes.Count > 0 Then
                m_Manager.Scenes(0).Render()
            End If
        End If
    End Sub
    Public Overridable Sub MouseUp()
        If m_Enabled Then
            Dim overlay As IAgStkGraphicsOverlay = DirectCast(m_Overlay, IAgStkGraphicsOverlay)
            overlay.Color = System.Drawing.Color.White
            If m_Manager.Scenes.Count > 0 Then
                m_Manager.Scenes(0).Render()
            End If
        End If
    End Sub
    Public Overridable Sub MouseClick()
        If m_Enabled Then
            SetState(Not m_State)
            m_Action()
        End If
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

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            m_Overlay = Nothing
        End If
    End Sub

    '
    ' Members
    '
    Private Shared m_ButtonSize As Integer = 35

    Private m_MouseEnterTranslucency As Single
    Private m_MouseExitTranslucency As Single
    Private m_Overlay As IAgStkGraphicsTextureScreenOverlay

    Private m_Action As ActionDelegate

    Private m_State As Boolean
    Private m_Enabled As Boolean
    Private m_EnabledImage As String
    Private m_DisabledImage As String

    Private m_Offset As Integer

    Private m_Manager As IAgStkGraphicsSceneManager
End Class
