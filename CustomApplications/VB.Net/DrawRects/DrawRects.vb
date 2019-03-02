Option Strict Off
Option Explicit On



Public Class DrawRects

#Region "Upgrade Support "
    Private Shared m_vb6FormDefInstance As DrawRects
    Private Shared m_InitializingDefInstance As Boolean
    Public Shared Property DefInstance() As DrawRects
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = New DrawRects
                m_InitializingDefInstance = False
            End If
            DefInstance = m_vb6FormDefInstance
        End Get
        Set(ByVal Value As DrawRects)
            m_vb6FormDefInstance = Value
        End Set
    End Property
#End Region
    Dim x0 As Single
    Dim y0 As Single
    Dim pickMode As Short
    Dim curRect As AGI.STKX.AgDrawElemRect
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
            Application.Run(New DrawRects)
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

    Private Sub AgUiAxVOCntrl1_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent) Handles AgUiAxVOCntrl1.MouseDownEvent
        If pickMode > 0 Then
            If pickMode = 1 Then
                x0 = eventArgs.x
                y0 = eventArgs.y
                pickMode = 2
            Else
                curRect.Set(x0, y0, eventArgs.x, eventArgs.y)
                curRect = Nothing
                pickMode = 0
                Me.AgUiAxVOCntrl1.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeAutomatic
            End If
        End If
    End Sub

    Private Sub AgUiAxVOCntrl1_MouseMoveEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent) Handles AgUiAxVOCntrl1.MouseMoveEvent
        Dim r As AGI.STKX.AgDrawElemRect
        If pickMode = 2 Then

            If curRect Is Nothing Then

                r = Me.AgUiAxVOCntrl1.DrawElements.Add("Rect")

                If Combo1.Text = "Solid" Then
                    r.LineStyle = AGI.STKUtil.AgELineStyle.eSolid
                ElseIf Combo1.Text = "Dashed" Then
                    r.LineStyle = AGI.STKUtil.AgELineStyle.eDashed
                ElseIf Combo1.Text = "Dotted" Then
                    r.LineStyle = AGI.STKUtil.AgELineStyle.eDotted
                ElseIf Combo1.Text = "DotDashed" Then
                    r.LineStyle = AGI.STKUtil.AgELineStyle.eDotDashed
                ElseIf Combo1.Text = "LongDashed" Then
                    r.LineStyle = AGI.STKUtil.AgELineStyle.eLongDashed
                ElseIf Combo1.Text = "DashDotDashed" Then
                    r.LineStyle = AGI.STKUtil.AgELineStyle.eDashDotDotted
                End If

                If Combo2.Text = "1 pt" Then
                    r.LineWidth = 1
                ElseIf Combo2.Text = "2 pt" Then
                    r.LineWidth = 2
                ElseIf Combo2.Text = "3 pt" Then
                    r.LineWidth = 3
                ElseIf Combo2.Text = "4 pt" Then
                    r.LineWidth = 4
                ElseIf Combo2.Text = "5 pt" Then
                    r.LineWidth = 5
                End If
                r.Color = System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(ColorPicker.Color()))
                curRect = r


            End If

            curRect.Set(x0, y0, eventArgs.x, eventArgs.y)
        End If
    End Sub

    Private Sub BtnNewScen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles BtnNewScen.Click
        stkRoot.CloseScenario()
        stkRoot.NewScenario("Test")
    End Sub

    Private Sub BtnAddRect_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles BtnAddRect.Click
        pickMode = 1
        Me.AgUiAxVOCntrl1.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeManual
    End Sub

    Private Sub BtnSelectColor_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles BtnSelectColor.Click
        Me.ColorPicker.ShowDialog()
    End Sub

    Private Sub BtnClearAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles BtnClearAll.Click
        Me.AgUiAxVOCntrl1.DrawElements.Clear()
    End Sub

    Private Sub BtnListAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles BtnListAll.Click
        Dim i As Object
        Dim msg As String
        msg = ""
        Dim r As AGI.STKX.AgDrawElemRect
        For i = 0 To Me.AgUiAxVOCntrl1.DrawElements.Count - 1
            r = Me.AgUiAxVOCntrl1.DrawElements(i)
            msg = msg & "[left, top, right, bottom]=[" & r.Left & ", " & r.Top & ", " & r.Right & ", " & r.Bottom & "] LineWidth=" & r.LineWidth & " LineStyle=" & r.LineStyle & " Color=" & System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(r.Color)).ToString() & vbCrLf
        Next
        MsgBox(msg)
    End Sub

    Private Sub BtnListAllForEach_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles BtnListAllForEach.Click
        Dim i As Object
        Dim msg As String
        msg = ""
        Dim r As AGI.STKX.AgDrawElemRect
        For Each i In Me.AgUiAxVOCntrl1.DrawElements
            r = i
            msg = msg & "[left, top, right, bottom]=[" & r.Left & ", " & r.Top & ", " & r.Right & ", " & r.Bottom & "] LineWidth=" & r.LineWidth & " LineStyle=" & r.LineStyle & " Color=" & System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(r.Color)).ToString() & vbCrLf
        Next i
        MsgBox(msg)
    End Sub

    Private Sub DrawRects_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        pickMode = 0

        stkRoot.NewScenario("Test")

        Combo1.Items.Add("Solid")
        Combo1.Items.Add("Dashed")
        Combo1.Items.Add("Dotted")
        Combo1.Items.Add("DotDashed")
        Combo1.Items.Add("LongDashed")
        Combo1.Items.Add("DashDotDashed")

        Combo1.Text = "Solid"

        Combo2.Items.Add("1 pt")
        Combo2.Items.Add("2 pt")
        Combo2.Items.Add("3 pt")
        Combo2.Items.Add("4 pt")
        Combo2.Items.Add("5 pt")

        Combo2.Text = "1 pt"

    End Sub

    Private Sub DrawRects_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class