Option Strict Off
Option Explicit On



Public Class VbEventsForm

#Region "Upgrade Support "
    Private Shared m_vb6FormDefInstance As VbEventsForm
    Private Shared m_InitializingDefInstance As Boolean
    Public Shared Property DefInstance() As VbEventsForm
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = New VbEventsForm()
                m_InitializingDefInstance = False
            End If
            DefInstance = m_vb6FormDefInstance
        End Get
        Set(ByVal value As VbEventsForm)
            m_vb6FormDefInstance = Value
        End Set
    End Property
#End Region
    Dim WithEvents STKXApp As AGI.STKX.AgSTKXApplication
    Dim stkRootObject As AGI.STKObjects.AgStkObjectRoot = Nothing

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
            Application.Run(New VbEventsForm)
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

    Private Sub VbEventsForm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        STKXApp = Me.AgUiAxVOCntrl1.Application

        If STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeEngineRuntime) = False Then
            MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Application.Exit()
            Return
        ElseIf STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl) = False Then
            MessageBox.Show("Globe is disabled due to your current license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Command4.Enabled = False
        End If
    End Sub

    Function GetShiftAsStr(ByRef Shift As Short) As Object
        Dim res As String
        res = ""
        If (Shift And AGI.STKX.AgEShiftValues.eShiftPressed) <> 0 Then
            res = res & "Shift"
        End If
        If (Shift And AGI.STKX.AgEShiftValues.eCtrlPressed) <> 0 Then
            res = res & "Ctrl"
        End If
        If (Shift And AGI.STKX.AgEShiftValues.eAltPressed) <> 0 Then
            res = res & "Alt"
        End If
        GetShiftAsStr = res
    End Function

    Function GetButtonAsStr(ByRef Button As Short) As Object
        Dim res As String
        res = ""
        If (Button And AGI.STKX.AgEButtonValues.eLeftPressed) <> 0 Then
            res = res & "Left"
        End If
        If (Button And AGI.STKX.AgEButtonValues.eRightPressed) <> 0 Then
            res = res & "Right"
        End If
        If (Button And AGI.STKX.AgEButtonValues.eMiddlePressed) <> 0 Then
            res = res & "Middle"
        End If
        GetButtonAsStr = res
    End Function

    Sub VoPickInfo(ByRef str_Renamed As String, ByRef x As Single, ByRef y As Single)
        Dim srt As Object = ""

        Dim pickInfoData As AGI.STKX.AgPickInfoData
        pickInfoData = AgUiAxVOCntrl1.PickInfo(x, y)

        If pickInfoData.IsObjPathValid Then
            WriteMsg(srt & "Object: " & pickInfoData.ObjPath)
        End If

        If pickInfoData.IsLatLonAltValid Then
            WriteMsg(str_Renamed & "LLA: " & pickInfoData.Lat & " " & pickInfoData.Lon & " " & pickInfoData.Alt)
        End If

        pickInfoData = Nothing

    End Sub

    Sub GxPickInfo(ByRef str_Renamed As String, ByRef x As Single, ByRef y As Single)
        Dim srt As Object = ""

        Dim pickInfoData As AGI.STKX.AgPickInfoData
        pickInfoData = Me.AgUiAx2DCntrl1.PickInfo(x, y)

        If pickInfoData.IsObjPathValid Then
            WriteMsg(srt & "Object: " & pickInfoData.ObjPath)
        End If

        If pickInfoData.IsLatLonAltValid Then
            WriteMsg(str_Renamed & "LLA: " & pickInfoData.Lat & " " & pickInfoData.Lon & " " & pickInfoData.Alt)
        End If

        pickInfoData = Nothing

    End Sub

    Sub WriteMsg(ByRef msg As String)

        Dim count As Integer
        count = Len(Me.Trace.Text)

        If count > 30000 Then
            Me.Trace.Text = ""
            count = 0
        End If

        Me.Trace.SelectionStart = count
        Me.Trace.SelectedText = msg & vbCrLf

    End Sub

    Private Sub AgUiAx2DCntrl1_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AgUiAx2DCntrl1.ClickEvent

        WriteMsg("Ax2D - Click")

    End Sub

    Private Sub AgUiAx2DCntrl1_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AgUiAx2DCntrl1.DblClick

        WriteMsg("Ax2D - DblClick")

    End Sub

    Private Sub AgUiAx2DCntrl1_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyDownEvent) Handles AgUiAx2DCntrl1.KeyDownEvent

        WriteMsg("Ax2D - KeyDown " & eventArgs.keyCode & " " & GetShiftAsStr(eventArgs.shift))

    End Sub

    Private Sub AgUiAx2DCntrl1_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyPressEvent) Handles AgUiAx2DCntrl1.KeyPressEvent

        WriteMsg("Ax2D - KeyPress " & eventArgs.keyAscii)

    End Sub

    Private Sub AgUiAx2DCntrl1_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyUpEvent) Handles AgUiAx2DCntrl1.KeyUpEvent

        WriteMsg("Ax2D - KeyUp " & eventArgs.keyCode & " " & GetShiftAsStr(eventArgs.shift))

    End Sub

    Private Sub AgUiAx2DCntrl1_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseDownEvent) Handles AgUiAx2DCntrl1.MouseDownEvent

        WriteMsg("Ax2D - MouseDown " & eventArgs.x & " " & eventArgs.y & " " & GetShiftAsStr(eventArgs.shift) & " " & GetButtonAsStr(eventArgs.button))

    End Sub

    Private Sub AgUiAx2DCntrl1_MouseMoveEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseMoveEvent) Handles AgUiAx2DCntrl1.MouseMoveEvent

        WriteMsg("Ax2D - MouseMove " & eventArgs.x & " " & eventArgs.y & " " & GetShiftAsStr(eventArgs.shift) & " " & GetButtonAsStr(eventArgs.button))
        GxPickInfo("Ax2D - ", eventArgs.x, eventArgs.y)

    End Sub

    Private Sub AgUiAx2DCntrl1_MouseUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseUpEvent) Handles AgUiAx2DCntrl1.MouseUpEvent

        WriteMsg("Ax2D - MouseUp " & eventArgs.x & " " & eventArgs.y & " " & GetShiftAsStr(eventArgs.shift) & " " & GetButtonAsStr(eventArgs.button))

    End Sub

    Private Sub AgUiAxVOCntrl1_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AgUiAxVOCntrl1.ClickEvent

        WriteMsg("AxVO - Click")

    End Sub

    Private Sub AgUiAxVOCntrl1_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles AgUiAxVOCntrl1.DblClick

        WriteMsg("AxVO - DblClick")

    End Sub

    Private Sub AgUiAxVOCntrl1_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyDownEvent) Handles AgUiAxVOCntrl1.KeyDownEvent

        WriteMsg("AxVO - KeyDown " & eventArgs.keyCode & " " & GetShiftAsStr(eventArgs.shift))

    End Sub

    Private Sub AgUiAxVOCntrl1_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyPressEvent) Handles AgUiAxVOCntrl1.KeyPressEvent

        WriteMsg("AxVO - KeyPress " & eventArgs.keyAscii)

    End Sub

    Private Sub AgUiAxVOCntrl1_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyUpEvent) Handles AgUiAxVOCntrl1.KeyUpEvent

        WriteMsg("AxVO - KeyUp " & eventArgs.keyCode & " " & GetShiftAsStr(eventArgs.shift))

    End Sub

    Private Sub AgUiAxVOCntrl1_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent) Handles AgUiAxVOCntrl1.MouseDownEvent

        WriteMsg("AxVO - MouseDown " & eventArgs.x & " " & eventArgs.y & " " & GetShiftAsStr(eventArgs.shift) & " " & GetButtonAsStr(eventArgs.button))

    End Sub

    Private Sub AgUiAxVOCntrl1_MouseMoveEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent) Handles AgUiAxVOCntrl1.MouseMoveEvent

        WriteMsg("AxVO - MouseMove " & eventArgs.x & " " & eventArgs.y & " " & GetShiftAsStr(eventArgs.shift) & " " & GetButtonAsStr(eventArgs.button))
        VoPickInfo("AxVO -  ", eventArgs.x, eventArgs.y)

    End Sub

    Private Sub AgUiAxVOCntrl1_MouseUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent) Handles AgUiAxVOCntrl1.MouseUpEvent

        WriteMsg("AxVO - MouseUp " & eventArgs.x & " " & eventArgs.y & " " & GetShiftAsStr(eventArgs.shift) & " " & GetButtonAsStr(eventArgs.button))

    End Sub

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        If Command1.Text = "Open Scenario" Then
            Dim scFile As String
            scFile = My.Application.Info.DirectoryPath & "\..\..\..\..\..\..\SharedResources\Scenarios\events\TestEvents.sc"
            stkRoot.CloseScenario()
            stkRoot.LoadScenario(scFile)
            Command1.Text = "Close Scenario"
            CheckBox1.Enabled = True
            If (AgUiAx2DCntrl1.PanModeEnabled) Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
        ElseIf Command1.Text = "Close Scenario" Then
            CheckBox1.Checked = False
            CheckBox1.Enabled = False
            If Not (IsNothing(stkRootObject)) Then
                stkRootObject.CloseScenario()
            End If
            Command1.Text = "Open Scenario"
        End If
    End Sub

    Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
        Me.AgUiAx2DCntrl1.ZoomIn()
    End Sub


    Private Sub Command3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command3.Click
        Me.AgUiAx2DCntrl1.ZoomOut()
    End Sub

    Private Sub Command4_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command4.Click
        Me.AgUiAxVOCntrl1.ZoomIn()
    End Sub

    Private Sub Command6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Command6.Click
        Dim saveAsDialog As SaveFileDialog = New SaveFileDialog
        If (saveAsDialog.ShowDialog() = DialogResult.OK) Then
            stkRoot.SaveScenarioAs(saveAsDialog.FileName)
        End If
    End Sub

    Private Sub STKXApp_OnLogMessage(ByVal bstrMsg As String, ByVal eType As AGI.STKUtil.AgELogMsgType, ByVal lErrorCode As Integer, ByVal bstrFileName As String, ByVal lLineNo As Integer, ByVal eDispID As AGI.STKUtil.AgELogMsgDispID) Handles STKXApp.OnLogMessage
        WriteMsg("STK/X - Log: ")
        WriteMsg("     Message:    " & bstrMsg)

        Dim stype As String
        If eType = AGI.STKUtil.AgELogMsgType.eLogMsgAlarm Then
            stype = "Alarm"
        ElseIf eType = AGI.STKUtil.AgELogMsgType.eLogMsgDebug Then
            stype = "Debug"
        ElseIf eType = AGI.STKUtil.AgELogMsgType.eLogMsgForceInfo Then
            stype = "ForceInfo"
        ElseIf eType = AGI.STKUtil.AgELogMsgType.eLogMsgInfo Then
            stype = "Info"
        ElseIf eType = AGI.STKUtil.AgELogMsgType.eLogMsgWarning Then
            stype = "Warning"
        Else
            stype = "Unknown"
        End If
        WriteMsg("     Type:       " & stype)

        WriteMsg("     Error code: " & lErrorCode)

        If bstrFileName <> "" Then
            WriteMsg("     File name: " & bstrFileName)
            WriteMsg("     Line #: " & lLineNo)
        Else
            WriteMsg("     File name: Not specified")
        End If

        WriteMsg("     DispID #: " & eDispID)

    End Sub

    Private Sub STKXApp_OnScenarioNew(ByVal path As String) Handles STKXApp.OnScenarioNew
        WriteMsg("STK/X - New scenario: " & path)
    End Sub

    Private Sub STKXApp_OnScenarioLoad(ByVal path As String) Handles STKXApp.OnScenarioLoad
        WriteMsg("STK/X - Scenario loaded: " & path)
    End Sub

    Private Sub STKXApp_OnScenarioClose() Handles STKXApp.OnScenarioClose
        WriteMsg("STK/X - Scenario closed")
    End Sub

    Private Sub STKXApp_OnScenarioSave(ByVal path As String) Handles STKXApp.OnScenarioSave
        WriteMsg("STK/X - Scenario saved to: " & path)
    End Sub

    Private Sub VbEventsForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If (CheckBox1.Checked) Then
            AgUiAx2DCntrl1.PanModeEnabled = True
        Else
            AgUiAx2DCntrl1.PanModeEnabled = False
        End If
    End Sub
End Class