Option Strict Off
Option Explicit On



Public Class MapView

#Region "Upgrade Support "
    Private Shared m_vb6FormDefInstance As MapView
    Private Shared m_InitializingDefInstance As Boolean
    Dim stkRootObject As AGI.STKObjects.AgStkObjectRoot = Nothing
    Public Shared Property DefInstance() As MapView
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = New MapView
                m_InitializingDefInstance = False
            End If
            DefInstance = m_vb6FormDefInstance
        End Get
        Set(ByVal Value As MapView)
            m_vb6FormDefInstance = Value
        End Set
    End Property
#End Region

    Private ReadOnly Property stkRoot() As AGI.STKObjects.AgStkObjectRoot
        Get
            If stkRootObject Is Nothing Then
                stkRootObject = New AGI.STKObjects.AgStkObjectRoot
            End If
            stkRoot = stkRootObject
        End Get

    End Property

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
            Application.Run(New MapView)
        End If
    End Sub

    Private Sub Check1_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Check1.CheckStateChanged
        If Not (IsNothing(stkRootObject)) Then
            If Me.Check1.CheckState Then
                Me.AgUiAx2DCntrl1.PanModeEnabled = True
            Else
                Me.AgUiAx2DCntrl1.PanModeEnabled = False
            End If
        End If
    End Sub

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click

        On Error GoTo ErrHandler

        OpenFileDialog1 = New OpenFileDialog
        Dim path As String
        path = Application.StartupPath + "\..\..\..\..\..\..\SharedResources\Scenarios\Events\"
        OpenFileDialog1.InitialDirectory = System.IO.Path.GetFullPath(path)
        OpenFileDialog1.Filter = "Scenario (.sc)|*.sc"
        OpenFileDialog1.Title = "Open STK scenario..."
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If result <> System.Windows.Forms.DialogResult.Cancel Then
            If OpenFileDialog1.CheckFileExists Then
                On Error Resume Next
                If Not (IsNothing(stkRootObject)) Then
                    stkRootObject.CloseScenario()
                End If
                On Error GoTo 0
                stkRoot.LoadScenario(OpenFileDialog1.FileName)
                If Me.Check1.CheckState Then
                    Me.AgUiAx2DCntrl1.PanModeEnabled = True
                Else
                    Me.AgUiAx2DCntrl1.PanModeEnabled = False
                End If
            End If
        End If

        Exit Sub

ErrHandler:

    End Sub

    Private Sub Command4_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command4.Click
        Me.AgUiAx2DCntrl1.ZoomIn()
    End Sub

    Private Sub Command5_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command5.Click
        Me.AgUiAx2DCntrl1.ZoomOut()
    End Sub

    Private Sub MapView_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Me.Check1.CheckState = False
    End Sub

    Private Sub MapView_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class