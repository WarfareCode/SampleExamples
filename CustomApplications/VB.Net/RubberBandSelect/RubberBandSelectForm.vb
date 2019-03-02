Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic


Public Class RubberBandSelectForm

#Region "Upgrade Support "
    Private Shared m_vb6FormDefInstance As RubberBandSelectForm
    Private Shared m_InitializingDefInstance As Boolean
    Public Shared Property DefInstance() As RubberBandSelectForm
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = New RubberBandSelectForm()
                m_InitializingDefInstance = False
            End If
            DefInstance = m_vb6FormDefInstance
        End Get
		Set
            m_vb6FormDefInstance = Value
        End Set
    End Property
#End Region

    Private x0 As Single
    Private y0 As Single
    Private pickMode As Short
    Private curRect As AGI.STKX.AgDrawElemRect
    Private List As AGI.STKUtil.AgExecCmdResult
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
            Application.Run(New RubberBandSelectForm)
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

    Private Sub BtnLoadISRScen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles BtnLoadISRScen.Click
        Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim scFile As String
        scFile = My.Application.Info.DirectoryPath & "\..\..\..\..\..\..\SharedResources\Scenarios\ISR\ISR.sc"

        stkRoot.CloseScenario()
        stkRoot.LoadScenario(scFile)


        Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub AgUiAxVOCntrl1_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent) Handles AgUiAxVOCntrl1.MouseDownEvent
        Dim pickInfo As AGI.STKX.AgRubberBandPickInfoData
        Dim msg As String
        Dim obj As Object
        If pickMode > 0 Then
            If pickMode = 1 Then
                x0 = eventArgs.x
                y0 = eventArgs.y
                pickMode = 2
            Else
                curRect.Set(x0, y0, eventArgs.x, eventArgs.y)
                curRect = Nothing

                pickInfo = Me.AgUiAxVOCntrl1.RubberBandPickInfo(x0, y0, eventArgs.x, eventArgs.y)
                If pickInfo.ObjPaths.Count Then
                    msg = pickInfo.ObjPaths.Count & " Object(s):" & vbCrLf

                    For Each obj In pickInfo.ObjPaths
                        msg = msg & "   " & obj & vbCrLf
                    Next obj
                    MsgBox(msg)
                Else
                    MsgBox("No Object Selected")
                End If

                Me.AgUiAxVOCntrl1.DrawElements.Clear()
                Me.AgUiAxVOCntrl1.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeAutomatic
                pickMode = 0
            End If
        End If
    End Sub

    Private Sub AgUiAxVOCntrl1_MouseMoveEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent) Handles AgUiAxVOCntrl1.MouseMoveEvent
        Dim r As AGI.STKX.AgDrawElemRect
        If pickMode = 2 Then

            If curRect Is Nothing Then

                r = Me.AgUiAxVOCntrl1.DrawElements.Add("Rect")

                r.LineStyle = AGI.STKUtil.AgELineStyle.eSolid
                r.LineWidth = 1
                r.Color = System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red))

                curRect = r

            End If

            curRect.Set(x0, y0, eventArgs.x, eventArgs.y)
        End If
    End Sub

    Private Sub BtnSelectObjs_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles BtnSelectObjs.Click
        pickMode = 1
        Me.AgUiAxVOCntrl1.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeManual
    End Sub

    Private Sub RubberBandSelectForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class