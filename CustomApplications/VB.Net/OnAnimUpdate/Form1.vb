Imports System.Math
Imports System.Runtime.InteropServices


Public Class OnAnimUpdate
    Private WithEvents STKXApp As New AGI.STKX.AgSTKXApplication
    Private oldX As Single
    Private oldY As Single
    Private stkRootObject As AGI.STKObjects.AgStkObjectRoot = Nothing
    Private oldButton As Integer

    Public Shared Sub Main()
        Dim STKXApp As AGI.STKX.AgSTKXApplication = Nothing
        Try

            STKXApp = New AGI.STKX.AgSTKXApplication()
            If (Not STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl)) Then
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
            Application.Run(New OnAnimUpdate)
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


    Private Sub OnAnimeUpdate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.StatusBar1.Panels.Add("Panel 1")
        Me.StatusBar1.Panels.Add("Panel 2")
        Me.StatusBar1.Panels.Add("Panel 3")
        Me.StatusBar1.Panels(0).Width = Me.StatusBar1.Width / 3
        Me.StatusBar1.Panels(1).Width = Me.StatusBar1.Width / 3
        Me.StatusBar1.Panels(2).Width = Me.StatusBar1.Width / 3       
        stkRoot.NewScenario("Test")
        stkRoot.ExecuteCommand("ConControl / VerboseOff")
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        OpenScenario.ShowDialog()
    End Sub

    Private Sub AxAgUiAxVOCntrl1_MouseUpEvent(ByVal sender As Object, ByVal e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent) Handles AxAgUiAxVOCntrl1.MouseUpEvent
        DoMouseUp(e.button, e.shift, e.x, e.y)
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        stkRoot.PlayForward()
    End Sub

    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        stkRoot.Pause()
    End Sub

    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem6.Click
        stkRoot.Rewind()
    End Sub

    Private Sub MenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem7.Click
        stkRoot.Faster()
    End Sub

    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem8.Click
        stkRoot.Slower()
    End Sub

    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        Me.AxAgUiAxVOCntrl1.ZoomIn()
    End Sub

    Private Sub MenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem10.Click
        stkRoot.ExecuteCommand("VO * View Home")
    End Sub

    Private Sub OpenScenario_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenScenario.FileOk
        stkRoot.CloseScenario()
        stkRoot.LoadScenario(OpenScenario.FileName)
    End Sub

    Private Sub STKXApp_OnAnimUpdate(ByVal TimeEpSec As Double) Handles STKXApp.OnAnimUpdate
        Me.StatusBar1.Panels(0).Text = CStr(TimeEpSec) & " EpSec"

        Dim retList As AGI.STKUtil.AgExecCmdResult
        retList = stkRoot.ExecuteCommand("GetAnimTime *")

        If retList.Count > 0 Then
            Me.StatusBar1.Panels(2).Text = retList(0)
        Else
            Me.StatusBar1.Panels(2).Text = "N/A"
        End If

        retList = stkRoot.ExecuteCommand("AnimFrameRate *")

        If retList.Count > 0 And retList(0) > 0 Then
            Me.StatusBar1.Panels(1).Text = retList(0) & " fps"
        Else
            Me.StatusBar1.Panels(1).Text = "N/A"
        End If
    End Sub
    Private Sub DoMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal x As Single, ByVal y As Single)
        oldX = x
        oldY = y
        oldButton = Button
    End Sub
    Private Sub DoMouseUp(ByVal Button As Integer, ByVal Shift As Integer, ByVal x As Single, ByVal y As Single)
        If oldButton = 2 And Shift = 0 And Abs(oldX - x) < 10 And Abs(oldY - y) < 10 Then
            Dim r As System.Drawing.Point
            r.X = x
            r.Y = y
            PopupMenu.Show(Me, r)
        End If
    End Sub

    Private Sub AxAgUiAxVOCntrl1_MouseDownEvent(ByVal sender As Object, ByVal e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent) Handles AxAgUiAxVOCntrl1.MouseDownEvent
        DoMouseDown(e.button, e.shift, e.x, e.y)
    End Sub

    Private Sub OnAnimUpdate_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.AxAgUiAxVOCntrl1.Left = 0
        Me.AxAgUiAxVOCntrl1.Top = 0
        Me.AxAgUiAxVOCntrl1.Width = Me.ClientRectangle.Width
        Me.AxAgUiAxVOCntrl1.Height = Me.ClientRectangle.Height - Me.StatusBar1.Height
    End Sub

    Private Sub OnAnimUpdate_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class
