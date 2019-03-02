

Public Class Form1
    Private WithEvents stkxApp As AGI.STKX.AgSTKXApplication
    Private scenarioOpen As Boolean

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
            Application.Run(New Form1)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Button1.Text = "New Scenario" Then
            Me.AxAgUiAx2DCntrl1.Application.ExecuteCommand("New / Scenario Test")
            Button1.Text = "Close Scenario"
            scenarioOpen = True
        Else
            Me.AxAgUiAx2DCntrl1.Application.ExecuteCommand("Unload / *")
            Button1.Text = "New Scenario"
            scenarioOpen = False
        End If


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.AxAgUiAx2DCntrl1.ZoomIn()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.AxAgUiAx2DCntrl1.ZoomOut()
    End Sub

    Private Sub AxAgUiAx2DCntrl1_DblClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox("2D Map double-clicked.")
    End Sub

    Private Sub OnNewScenario(ByVal path As String) Handles stkxApp.OnScenarioNew
        MsgBox("New scenario created: " + path)
    End Sub

    Private Sub AxAgUiAxVOCntrl1_MouseMoveEvent(ByVal sender As Object, _
    ByVal e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent) Handles AxAgUiAxVOCntrl1.MouseMoveEvent
        Dim pickInfoData As AGI.STKX.AgPickInfoData

        pickInfoData = Me.AxAgUiAxVOCntrl1.PickInfo(e.x, e.y)
        If pickInfoData.IsLatLonAltValid Then

            Label1.Text = "Lat: " & pickInfoData.Lat & vbCrLf & "Lon: " & pickInfoData.Lon & vbCrLf & "Alt: " & pickInfoData.Alt

        Else

            Label1.Text = ""
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If scenarioOpen Then
            Me.AxAgUiAx2DCntrl1.Application.ExecuteCommand("Unload / *")
        End If
    End Sub
End Class
