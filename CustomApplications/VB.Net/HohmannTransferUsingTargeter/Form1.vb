Imports AGI.STKObjects
Imports AGI.STKObjects.Astrogator
Imports AGI.STKUtil

Public Class Form1

    Private stkRootObject As IAgStkObjectRoot = Nothing
    Private _driver As IAgVADriverMCS

    Private ReadOnly Property stkRoot() As AGI.STKObjects.IAgStkObjectRoot
        Get
            If stkRootObject Is Nothing Then
                Dim STKXApp As AGI.STKX.AgSTKXApplication = New AGI.STKX.AgSTKXApplication

                If STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl) Then
                    stkRootObject = TryCast(New AgStkObjectRoot(), IAgStkObjectRoot)
                    If stkRootObject.AvailableFeatures.IsPropagatorTypeAvailable(AgEVePropagatorType.ePropagatorAstrogator) Then
                        stkRootObject = TryCast(New AgStkObjectRoot(), IAgStkObjectRoot)
                    Else
                        MessageBox.Show("You do not have the Astrogator license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Application.Exit()
                    End If
                Else
                    MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Application.Exit()
                End If
            End If
            stkRoot = stkRootObject
        End Get
    End Property

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
        button1.Enabled = False
        stkRoot.NewScenario("HohmannTransfer")
        Dim sat1 As IAgSatellite = TryCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.eSatellite, "Satellite1"), IAgSatellite)
        sat1.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator)
        _driver = TryCast(sat1.Propagator, IAgVADriverMCS)
        _driver.MainSequence.RemoveAll()
        button2.Enabled = True
    End Sub

    Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
        button2.Enabled = False
        Dim initState As IAgVAMCSInitialState = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeInitialState, "Inner Orbit", "-"), IAgVAMCSInitialState)
        initState.SetElementType(AgEVAElementType.eVAElementTypeKeplerian)
        Dim modKep As IAgVAElementKeplerian = TryCast(initState.Element, IAgVAElementKeplerian)
        modKep.PeriapsisRadiusSize = 6700
        modKep.ArgOfPeriapsis = 0
        modKep.Eccentricity = 0
        modKep.Inclination = 0
        modKep.RAAN = 0
        modKep.TrueAnomaly = 0
        button3.Enabled = True
    End Sub

    Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
        button3.Enabled = False
        Dim propagate As IAgVAMCSPropagate = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Propagate", "-"), IAgVAMCSPropagate)
        propagate.PropagatorName = "Earth Point Mass"
        DirectCast(propagate.StoppingConditions("Duration").Properties, IAgVAStoppingCondition).Trip = 7200
        button4.Enabled = True
    End Sub

    Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
        button4.Enabled = False
        Dim ts As IAgVAMCSTargetSequence = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "Start Transfer", "-"), IAgVAMCSTargetSequence)
        Dim dv1 As IAgVAMCSManeuver = TryCast(ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "DV1", "-"), IAgVAMCSManeuver)
        dv1.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive)
        Dim impulsive As IAgVAManeuverImpulsive = TryCast(dv1.Maneuver, IAgVAManeuverImpulsive)
        impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector)
        Dim thrustVector As IAgVAAttitudeControlImpulsiveThrustVector = TryCast(impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        thrustVector.ThrustAxesName = "Satellite/Satellite1 VNC(Earth)"
        dv1.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX)
        DirectCast(dv1, IAgVAMCSSegment).Results.Add("Keplerian Elems/Radius of Apoapsis")
        Dim dc As IAgVAProfileDifferentialCorrector = TryCast(ts.Profiles("Differential Corrector"), IAgVAProfileDifferentialCorrector)
        Dim xControlParam As IAgVADCControl = dc.ControlParameters.GetControlByPaths("DV1", "ImpulsiveMnvr.Cartesian.X")
        xControlParam.Enable = True
        xControlParam.MaxStep = 0.3
        Dim roaEquality As IAgVADCResult = dc.Results.GetResultByPaths("DV1", "Radius Of Apoapsis")
        roaEquality.Enable = True
        roaEquality.DesiredValue = 42238
        roaEquality.Tolerance = 0.1
        dc.MaxIterations = 50
        dc.EnableDisplayStatus = True
        dc.Mode = AgEVAProfileMode.eVAProfileModeIterate
        ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles
        button5.Enabled = True
    End Sub

    Private Sub button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button5.Click
        button5.Enabled = False
        Dim transferEllipse As IAgVAMCSPropagate = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Transfer Ellipse", "-"), IAgVAMCSPropagate)
        transferEllipse.PropagatorName = "Earth Point Mass"
        transferEllipse.StoppingConditions.Add("Apoapsis")
        transferEllipse.StoppingConditions.Remove("Duration")
        button6.Enabled = True
    End Sub

    Private Sub button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button6.Click
        button6.Enabled = False
        Dim ts As IAgVAMCSTargetSequence = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "Finish Transfer", "-"), IAgVAMCSTargetSequence)
        Dim dv2 As IAgVAMCSManeuver = TryCast(ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "DV2", "-"), IAgVAMCSManeuver)
        dv2.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive)
        Dim impulsive As IAgVAManeuverImpulsive = TryCast(dv2.Maneuver, IAgVAManeuverImpulsive)
        impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector)
        Dim thrustVector As IAgVAAttitudeControlImpulsiveThrustVector = TryCast(impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        thrustVector.ThrustAxesName = "Satellite/Satellite1 VNC(Earth)"
        dv2.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX)
        DirectCast(dv2, IAgVAMCSSegment).Results.Add("Keplerian Elems/Eccentricity")
        Dim dc As IAgVAProfileDifferentialCorrector = TryCast(ts.Profiles("Differential Corrector"), IAgVAProfileDifferentialCorrector)
        Dim xControlParam As IAgVADCControl = dc.ControlParameters.GetControlByPaths("DV2", "ImpulsiveMnvr.Cartesian.X")
        xControlParam.Enable = True
        xControlParam.MaxStep = 0.3
        Dim eccConstraint As IAgVADCResult = dc.Results.GetResultByPaths("DV2", "Eccentricity")
        eccConstraint.Enable = True
        eccConstraint.DesiredValue = 0
        dc.EnableDisplayStatus = True
        dc.Mode = AgEVAProfileMode.eVAProfileModeIterate
        ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles
        button7.Enabled = True
    End Sub

    Private Sub button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button7.Click
        button7.Enabled = False
        Dim outerOrbit As IAgVAMCSPropagate = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Outer Orbit", "-"), IAgVAMCSPropagate)
        outerOrbit.PropagatorName = "Earth Point Mass"
        DirectCast(outerOrbit.StoppingConditions("Duration").Properties, IAgVAStoppingCondition).Trip = 86400
        button8.Enabled = True
    End Sub

    Private Sub button8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button8.Click
        Me.button8.Enabled = False        
        _driver.RunMCS()
        Dim startTransfer As IAgVAMCSTargetSequence = TryCast(_driver.MainSequence("Start Transfer"), IAgVAMCSTargetSequence)
        Dim finishTransfer As IAgVAMCSTargetSequence = TryCast(_driver.MainSequence("Finish Transfer"), IAgVAMCSTargetSequence)
        Dim startDC As IAgVAProfileDifferentialCorrector = TryCast(startTransfer.Profiles("Differential Corrector"), IAgVAProfileDifferentialCorrector)
        Console.WriteLine(startDC.ControlParameters.GetControlByPaths("DV1", "ImpulsiveMnvr.Cartesian.X").FinalValue)
        Dim finishDC As IAgVAProfileDifferentialCorrector = TryCast(finishTransfer.Profiles("Differential Corrector"), IAgVAProfileDifferentialCorrector)
        Console.WriteLine(finishDC.ControlParameters.GetControlByPaths("DV2", "ImpulsiveMnvr.Cartesian.X").FinalValue)
        Dim dv1 As IAgVAMCSManeuver = TryCast(startTransfer.Segments("DV1"), IAgVAMCSManeuver)
        Dim dv1Impulsive As IAgVAManeuverImpulsive = TryCast(dv1.Maneuver, IAgVAManeuverImpulsive)
        Dim dv1ThrustVector As IAgVAAttitudeControlImpulsiveThrustVector = TryCast(dv1Impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        Dim cartesian As IAgCartesian = TryCast(dv1ThrustVector.DeltaVVector.ConvertTo(AgEPositionType.eCartesian), IAgCartesian)
        Console.WriteLine(cartesian.X)
        startTransfer.ApplyProfiles()
        cartesian = TryCast(dv1ThrustVector.DeltaVVector.ConvertTo(AgEPositionType.eCartesian), IAgCartesian)
        Console.WriteLine(cartesian.X)

        Dim dv2 As IAgVAMCSManeuver = TryCast(finishTransfer.Segments("DV2"), IAgVAMCSManeuver)
        Dim dv2Impulsive As IAgVAManeuverImpulsive = TryCast(dv2.Maneuver, IAgVAManeuverImpulsive)
        Dim dv2ThrustVector As IAgVAAttitudeControlImpulsiveThrustVector = TryCast(dv2Impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        cartesian = TryCast(dv2ThrustVector.DeltaVVector.ConvertTo(AgEPositionType.eCartesian), IAgCartesian)
        Console.WriteLine(cartesian.X)
        finishTransfer.ApplyProfiles()
        cartesian = TryCast(dv2ThrustVector.DeltaVVector.ConvertTo(AgEPositionType.eCartesian), IAgCartesian)
        Console.WriteLine(cartesian.X)
        Me.button10.Enabled = True
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub

    Private Sub button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button10.Click
        Dim dataForm As New Form2()
        Dim reportData As String = ""
        Dim outerOrbit As IAgVAMCSPropagate = TryCast(_driver.MainSequence("Outer Orbit"), IAgVAMCSPropagate)
        Dim result As IAgDrResult = DirectCast(outerOrbit, IAgVAMCSSegment).ExecSummary
        Console.WriteLine(result.Category)
        Dim intervals As IAgDrIntervalCollection = TryCast(result.Value, IAgDrIntervalCollection)
        For i As Integer = 0 To intervals.Count - 1
            Dim interval As IAgDrInterval = intervals(i)
            Dim datasets As IAgDrDataSetCollection = interval.DataSets
            For j As Integer = 0 To datasets.Count - 1
                Dim dataset As IAgDrDataSet = datasets(j)
                dataForm.FormTitle = dataset.ElementName
                Dim elements As System.Array = dataset.GetValues()
                For Each o As Object In elements
                    reportData += o.ToString() + "" & Chr(13) & "" & Chr(10) & ""
                Next
            Next
        Next
        dataForm.ReportData = reportData
        dataForm.ShowDialog()
    End Sub
End Class
