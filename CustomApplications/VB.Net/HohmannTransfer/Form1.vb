Imports AGI.STKObjects
Imports AGI.STKObjects.Astrogator

Public Class Form1

    Private stkRootObject As IAgStkObjectRoot = Nothing
    Private _sat As IAgSatellite
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

    Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
        stkRoot.NewScenario("HohmannTransfer")
        Me.button1.Enabled = False
        Me.button2.Enabled = True
    End Sub

    Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
        _sat = TryCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.eSatellite, "Satellite1"), IAgSatellite)
        _sat.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator)
        _driver = TryCast(_sat.Propagator, IAgVADriverMCS)
        _driver.MainSequence.RemoveAll()
        Me.button2.Enabled = False
        Me.button3.Enabled = True
    End Sub

    Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
        Dim initialState As IAgVAMCSInitialState = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeInitialState, "Inner Orbit", "-"), IAgVAMCSInitialState)
        initialState.SetElementType(AgEVAElementType.eVAElementTypeKeplerian)
        Dim modKep As IAgVAElementKeplerian = TryCast(initialState.Element, IAgVAElementKeplerian)
        modKep.PeriapsisRadiusSize = 6700
        modKep.ArgOfPeriapsis = 0
        modKep.Eccentricity = 0
        modKep.Inclination = 0
        modKep.RAAN = 0
        modKep.TrueAnomaly = 0
        Me.button3.Enabled = False
        Me.button4.Enabled = True
    End Sub

    Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
        Dim propagate As IAgVAMCSPropagate = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Propagate", "-"), IAgVAMCSPropagate)
        propagate.PropagatorName = "Earth Point Mass"
        Dim sc As IAgVAStoppingCondition = TryCast(propagate.StoppingConditions("Duration").Properties, IAgVAStoppingCondition)
        sc.Trip = 7200
        Me.button4.Enabled = False
        Me.button5.Enabled = True
    End Sub

    Private Sub button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button5.Click
        Dim maneuver As IAgVAMCSManeuver = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "DV1", "-"), IAgVAMCSManeuver)
        maneuver.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive)
        Dim impulsive As IAgVAManeuverImpulsive = TryCast(maneuver.Maneuver, IAgVAManeuverImpulsive)
        impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector)
        Dim thrustVector As IAgVAAttitudeControlImpulsiveThrustVector = TryCast(impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        thrustVector.DeltaVVector.AssignCartesian(2.421, 0, 0)
        impulsive.UpdateMass = True
        Me.button5.Enabled = False
        Me.button6.Enabled = True
    End Sub

    Private Sub button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button6.Click
        Dim propagate As IAgVAMCSPropagate = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Transfer Ellipse", "-"), IAgVAMCSPropagate)
        propagate.PropagatorName = "Earth Point Mass"
        propagate.StoppingConditions.Add("Apoapsis")
        propagate.StoppingConditions.Remove("Duration")
        Me.button6.Enabled = False
        Me.button7.Enabled = True
    End Sub

    Private Sub button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button7.Click
        Dim maneuver As IAgVAMCSManeuver = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "DV2", "-"), IAgVAMCSManeuver)
        maneuver.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive)
        Dim impulsive As IAgVAManeuverImpulsive = TryCast(maneuver.Maneuver, IAgVAManeuverImpulsive)
        impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector)
        Dim thrustVector As IAgVAAttitudeControlImpulsiveThrustVector = TryCast(impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        thrustVector.DeltaVVector.AssignCartesian(1.465, 0, 0)
        impulsive.UpdateMass = True
        Me.button7.Enabled = False
        Me.button8.Enabled = True
    End Sub

    Private Sub button8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button8.Click
        Dim propagate As IAgVAMCSPropagate = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Outer Orbit", "-"), IAgVAMCSPropagate)
        propagate.PropagatorName = "Earth Point Mass"
        Dim sc As IAgVAStoppingCondition = TryCast(propagate.StoppingConditions("Duration").Properties, IAgVAStoppingCondition)
        sc.Trip = 86400
        Me.button8.Enabled = False
        Me.button9.Enabled = True
    End Sub

    Private Sub button9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button9.Click
        _driver.RunMCS()
        Me.button9.Enabled = False
        Me.button10.Enabled = True
    End Sub

    Private Sub button10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button10.Click
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

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class
