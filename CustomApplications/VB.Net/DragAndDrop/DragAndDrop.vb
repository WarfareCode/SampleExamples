Option Strict Off
Option Explicit On



Public Class Form1

#Region "Upgrade Support "
    Private Shared m_vb6FormDefInstance As Form1
    Private Shared m_InitializingDefInstance As Boolean
    Public Shared Property DefInstance() As Form1
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = New Form1()
                m_InitializingDefInstance = False
            End If
            DefInstance = m_vb6FormDefInstance
        End Get
        Set(ByVal value As Form1)
            m_vb6FormDefInstance = value
        End Set
    End Property
#End Region
    Private oldX As Single
    Private oldY As Single
    Private oldButton As Short

    Public Shared Sub Main()

        Dim STKXApp As AGI.STKX.AgSTKXApplication = Nothing
        Try

            STKXApp = New AGI.STKX.AgSTKXApplication()
            If (Not STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeEngineRuntime)) Then
                MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If

        Catch exception As System.Runtime.InteropServices.COMException

            If (exception.ErrorCode = &H80040154) Then

                Dim errorMessage As String = "Could not instantiate AgSTKXApplication."
                errorMessage += Environment.NewLine
                errorMessage += Environment.NewLine
                errorMessage += "You are trying to run in the x64 configuration. Check that STK Engine 64-bit is installed on this machine."

                MessageBox.Show(errorMessage, "STK Engine Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)

            Else
                Throw
            End If

        End Try

        If Not STKXApp Is Nothing Then

            Dim app As Form1
            app = New Form1

            If Not STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl) Then
                MessageBox.Show("Globe is disabled due to your current license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                app.AgUiAxVOCntrl1.Visible = False
                app.AgUiAx2DCntrl1.Visible = True
                app.MenuItem1.Checked = True
                app.MenuItem2.Enabled = False
                app.MenuItem2.Checked = False
                app.MenuItem8.Visible = False
                app.MenuItem9.Enabled = True
                app.MenuItem9.Visible = True
            End If

            Application.Run(app)

        End If

    End Sub


    Private ReadOnly Property stkRoot() As AGI.STKObjects.AgStkObjectRoot
        Get
            If stkRootObject Is Nothing Then
                stkRootObject = New AGI.STKObjects.AgStkObjectRoot
            End If
            stkRoot = stkRootObject
        End Get

    End Property

    Private Sub DoMouseDown(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef y As Single)
        oldX = x
        oldY = y
        oldButton = Button
    End Sub

    Private Sub DoMouseUp(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef y As Single)
        If oldButton = 2 And Shift = 0 And System.Math.Abs(oldX - x) < 10 And System.Math.Abs(oldY - y) < 10 Then
            Dim r As System.Drawing.Point
            r.X = x
            r.Y = y
            PopupMenu.Show(Me, r)

        End If
    End Sub

    Private Sub DoOLEDragDrop(ByVal Data As AGI.STKX.IAgDataObject, ByVal Effect As Integer, ByVal Button As Short, ByVal Shift As Short, ByVal x As Integer, ByVal y As Integer)
        Dim file As Object
        Dim hfile As Short
        Dim Line As String
        For Each file In Data.Files
            hfile = 1
            FileOpen(hfile, file, OpenMode.Input)
            Do While Not EOF(hfile)
                Line = LineInput(hfile)
                On Error Resume Next
                stkRoot.ExecuteCommand(Line)
                On Error GoTo 0
            Loop
            FileClose(hfile)
        Next file
    End Sub

    Private Sub AgUiAx2DCntrl1_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseDownEvent) Handles AgUiAx2DCntrl1.MouseDownEvent
        DoMouseDown(eventArgs.button, eventArgs.shift, eventArgs.x, eventArgs.y)
    End Sub

    Private Sub AgUiAx2DCntrl1_MouseUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseUpEvent) Handles AgUiAx2DCntrl1.MouseUpEvent
        DoMouseUp(eventArgs.button, eventArgs.shift, eventArgs.x, eventArgs.y)
    End Sub

    Private Sub AgUiAx2DCntrl1_OLEDragDrop(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_OLEDragDropEvent) Handles AgUiAx2DCntrl1.OLEDragDrop
        DoOLEDragDrop(eventArgs.data, eventArgs.effect, eventArgs.button, eventArgs.shift, eventArgs.x, eventArgs.y)
    End Sub

    Private Sub AgUiAxVOCntrl1_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent) Handles AgUiAxVOCntrl1.MouseDownEvent
        DoMouseDown(eventArgs.button, eventArgs.shift, eventArgs.x, eventArgs.y)
    End Sub

    Private Sub AgUiAxVOCntrl1_MouseUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent) Handles AgUiAxVOCntrl1.MouseUpEvent
        If oldButton = 2 And eventArgs.shift = 0 And System.Math.Abs(oldX - eventArgs.x) < 10 And System.Math.Abs(oldY - eventArgs.y) < 10 Then
            Dim r As System.Drawing.Point
            r.X = eventArgs.x
            r.Y = eventArgs.y
            PopupMenu.Show(Me, r)
        End If
    End Sub

    Private Sub AgUiAxVOCntrl1_OLEDragDrop(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_OLEDragDropEvent) Handles AgUiAxVOCntrl1.OLEDragDrop
        DoOLEDragDrop(eventArgs.data, eventArgs.effect, eventArgs.button, eventArgs.shift, eventArgs.x, eventArgs.y)
    End Sub

    Private Sub Form1_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        stkRoot.NewScenario("Test")
        stkRoot.ExecuteCommand("MapAttribs * ScenTime Display On Blue")
        Me.AgUiAx2DCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eManual
        Me.AgUiAxVOCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eManual
    End Sub

    Private Sub Form1_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Me.AgUiAxVOCntrl1.Left = 0
        Me.AgUiAxVOCntrl1.Top = 0
        Me.AgUiAxVOCntrl1.Width = Me.ClientRectangle.Width
        Me.AgUiAxVOCntrl1.Height = Me.ClientRectangle.Height

        Me.AgUiAx2DCntrl1.Left = 0
        Me.AgUiAx2DCntrl1.Top = 0
        Me.AgUiAx2DCntrl1.Width = Me.ClientRectangle.Width
        Me.AgUiAx2DCntrl1.Height = Me.ClientRectangle.Height
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        PopupMenu.MenuItems(0).Checked = True
        PopupMenu.MenuItems(1).Checked = False
        Me.AgUiAxVOCntrl1.Visible = False
        Me.AgUiAx2DCntrl1.Visible = True

        PopupMenu.MenuItems(5).Visible = True
        PopupMenu.MenuItems(5).Enabled = True
        PopupMenu.MenuItems(4).Visible = False
        PopupMenu.MenuItems(4).Enabled = False
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        PopupMenu.MenuItems(0).Checked = False
        PopupMenu.MenuItems(1).Checked = True

        Me.AgUiAxVOCntrl1.Visible = True
        Me.AgUiAx2DCntrl1.Visible = False

        PopupMenu.MenuItems(5).Visible = False
        PopupMenu.MenuItems(5).Enabled = False
        PopupMenu.MenuItems(4).Visible = True
        PopupMenu.MenuItems(4).Enabled = True
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        Me.AgUiAx2DCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eAutomatic
        Me.AgUiAxVOCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eAutomatic
        Me.PopupMenu.MenuItems(2).MenuItems(0).Checked = True
        Me.PopupMenu.MenuItems(2).MenuItems(1).Checked = False
        Me.PopupMenu.MenuItems(2).MenuItems(2).Checked = False
    End Sub

    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Me.AgUiAx2DCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eManual
        Me.AgUiAxVOCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eManual
        Me.PopupMenu.MenuItems(2).MenuItems(0).Checked = False
        Me.PopupMenu.MenuItems(2).MenuItems(1).Checked = True
        Me.PopupMenu.MenuItems(2).MenuItems(2).Checked = False
    End Sub

    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem6.Click
        Me.AgUiAx2DCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eNone
        Me.AgUiAxVOCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eNone
        Me.PopupMenu.MenuItems(2).MenuItems(0).Checked = False
        Me.PopupMenu.MenuItems(2).MenuItems(1).Checked = False
        Me.PopupMenu.MenuItems(2).MenuItems(2).Checked = True
    End Sub

    Private Sub MenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem7.Click
        If PopupMenu.MenuItems(0).Checked Then
            Me.AgUiAx2DCntrl1.ZoomIn()
        End If
        If PopupMenu.MenuItems(1).Checked Then
            Me.AgUiAxVOCntrl1.ZoomIn()
        End If
    End Sub

    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem8.Click
        stkRoot.ExecuteCommand("VO * View Home")
    End Sub

    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        Me.AgUiAx2DCntrl1.ZoomOut()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class