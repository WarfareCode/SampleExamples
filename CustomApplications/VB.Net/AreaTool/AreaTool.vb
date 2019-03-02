Imports System
Imports System.IO
Imports AGI.STKObjects
Imports AGI.STKVgt


Public Class frmAreaTool

    Dim stkRootObject As AGI.STKObjects.AgStkObjectRoot = Nothing
    Dim ReportFilePath As String
    Dim bFirstTime As Boolean

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
            Application.Run(New frmAreaTool)
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

    Private Sub frmAreaTool_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load        
        bFirstTime = True
        btnNewScenario.Enabled = True
        btnNewSat.Enabled = False
        btnCompute.Enabled = False
        btnCloseScenario.Enabled = False
        btnReport.Enabled = False
    End Sub

    Private Sub btnNewScenario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewScenario.Click
        Dim oScenario As IAgScenario
        stkRoot.NewScenario("AreaToolTest")
        stkRoot.UnitPreferences.SetCurrentUnit("DistanceUnit", "km")
        stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG")
        oScenario = stkRoot.CurrentScenario
        oScenario.SetTimePeriod("1 Jul 2007 12:00:00.000", "2 Jul 2007 12:00:00.000")
        oScenario.Epoch = "1 Jul 2007 12:00:00.000"
        oScenario.Animation.StartTime = "1 Jul 2007 12:00:00.000"
        stkRoot.Rewind()

        btnNewScenario.Enabled = False
        btnNewSat.Enabled = True
        btnCompute.Enabled = False
        btnCloseScenario.Enabled = True
        btnReport.Enabled = False
    End Sub

    Private Sub btnNewSat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewSat.Click
        Dim oSat As IAgSatellite
        Dim oTwobody As IAgVePropagatorTwoBody
        Dim oOrb As AGI.STKUtil.IAgOrbitState

        oSat = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Satellite1")
        oSat.VO.Model.ScaleValue = 0.0
        oSat.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody)
        oTwobody = CType(oSat.Propagator, IAgVePropagatorTwoBody)
        Dim interval As IAgCrdnEventIntervalSmartInterval = oTwobody.EphemerisInterval
        interval.SetExplicitInterval("1 Jul 2007 12:00:00.000", "2 Jul 2007 12:00:00.000")
        oTwobody.Step = 60
        oOrb = oTwobody.InitialState.Representation
        oOrb.Epoch = "1 Jul 2007 12:00:00.000"
        oTwobody.InitialState.Representation.AssignClassical(AGI.STKUtil.AgECoordinateSystem.eCoordinateSystemJ2000, 6878.14, 0.0, 45.0, 0.0, 0.0, 0.0)
        oTwobody.Propagate()

        btnNewScenario.Enabled = False
        btnNewSat.Enabled = False
        btnCompute.Enabled = True
        btnCloseScenario.Enabled = True
        btnReport.Enabled = False
    End Sub

    Private Sub btnCompute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompute.Click
        If (bFirstTime) Then
            bFirstTime = False
        Else
            stkRoot.ExecuteCommand("VO */Satellite/Satellite1 Area DeleteData")
        End If
        stkRoot.ExecuteCommand("VO */Satellite/Satellite1 Area Times ""1 Jul 2007 12:00:00.000"" ""2 Jul 2007 12:00:00.000"" 300.0")
        stkRoot.ExecuteCommand("VO */Satellite/Satellite1 Area BoundRadius On 10.0")
        stkRoot.ExecuteCommand("VO */Satellite/Satellite1 Area Compute")
        btnReport.Enabled = True
    End Sub

    Private Sub btnCloseScenario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseScenario.Click
        If (bFirstTime = False) Then
            bFirstTime = True
            stkRoot.ExecuteCommand("VO */Satellite/Satellite1 Area DeleteData")
        End If
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
        CleanReportFile()
        btnNewScenario.Enabled = True
        btnNewSat.Enabled = False
        btnCompute.Enabled = False
        btnCloseScenario.Enabled = False
        btnReport.Enabled = False
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        GetReportFilePath()
        stkRoot.ExecuteCommand("Report */Satellite/Satellite1 Export ""Model Area"" """ + ReportFilePath + """")
        ShowReport(ReportFilePath)
    End Sub

    Private Function GetReportFilePath() As String
        CleanReportFile()
        ReportFilePath = Path.GetTempFileName()
        Return ReportFilePath
    End Function

    Private Sub CleanReportFile()
        If File.Exists(ReportFilePath) Then
            File.Delete(ReportFilePath)
        End If
        ReportFilePath = ""
    End Sub

    Private Sub ShowReport(ByVal sReport As String)
        If File.Exists(sReport) Then
            Dim p As Process = Nothing
            Try
                p = New Process
                p.StartInfo.FileName = "wordpad"
                p.StartInfo.Arguments = """" + sReport + """"
                p.Start()
            Catch ex As Exception
                Dim msg As String = "Exception Occurred : " + ex.Message + ", " + ex.StackTrace.ToString()
                System.Windows.Forms.MessageBox.Show(msg)
            End Try
        End If
    End Sub
    Private Sub frmAreaTool_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        CleanReportFile()
    End Sub

    Private Sub frmAreaTool_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class
