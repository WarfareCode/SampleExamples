Imports AGI.STKObjects.Astrogator
Imports AGI.STKObjects
Imports AGI.STKUtil

Public Class Form1

    Private stkRootObject As IAgStkObjectRoot = Nothing
    Private _driver As IAgVADriverMCS

    Private ReadOnly Property stkRoot() As AGI.STKObjects.IAgStkObjectRoot
        Get
            Dim STKXApp As AGI.STKX.AgSTKXApplication = New AGI.STKX.AgSTKXApplication
            If stkRootObject Is Nothing Then
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
        stkRoot.NewScenario("MarsProbe")
        Dim scene As IAgScenario = TryCast(stkRoot.CurrentScenario, IAgScenario)
        scene.StartTime = "1 Mar 1997 00:00:00.000"
        scene.StopTime = "1 Mar 1998 00:00:00.000"
        scene.Epoch = "1 Mar 1997 00:00:00.000"
        scene.Animation.AnimStepValue = 3600
        button2.Enabled = True
        scene.Graphics.PlanetOrbitsVisible = True
        scene.Graphics.SubPlanetPointsVisible = False
        scene.Graphics.SubPlanetLabelsVisible = False
    End Sub

    Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
        button2.Enabled = False
        Dim earth As IAgPlanet = TryCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.ePlanet, "Planet1"), IAgPlanet)
        earth.PositionSource = AgEPlPositionSourceType.ePosCentralBody
        Dim cb As IAgPlPosCentralBody = TryCast(earth.PositionSourceData, IAgPlPosCentralBody)
        cb.CentralBody = "Earth"
        cb.EphemSource = AgEEphemSourceType.eEphemJPLDE

        Dim mars As IAgPlanet = TryCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.ePlanet, "Planet2"), IAgPlanet)
        mars.PositionSource = AgEPlPositionSourceType.ePosCentralBody
        cb = TryCast(mars.PositionSourceData, IAgPlPosCentralBody)
        cb.CentralBody = "Mars"
        cb.EphemSource = AgEEphemSourceType.eEphemJPLDE
        button3.Enabled = True
    End Sub

    Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
        button3.Enabled = False
        Dim sat1 As IAgSatellite = TryCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.eSatellite, "Satellite1"), IAgSatellite)
        sat1.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator)
        _driver = TryCast(sat1.Propagator, IAgVADriverMCS)
        sat1.Graphics.PassData.Orbit.SetLeadDataType(AgELeadTrailData.eDataAll)
        stkRoot.ExecuteCommand("VO * CentralBody Sun 1")
        stkRoot.ExecuteCommand("VO * Celestial Moon Label Off WindowID 1")
        stkRoot.ExecuteCommand("VO * Grids Space ShowEcliptic On WindowID 1")
        stkRoot.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 2e15 WindowID 1")
        stkRoot.ExecuteCommand("VO * View Top WindowID 1")
        stkRoot.ExecuteCommand("VO * View North WindowID 1")

        stkRoot.ExecuteCommand("VO * CentralBody Mars 2")
        stkRoot.ExecuteCommand("VO * Celestial Moon Label Off WindowID 2")
        stkRoot.ExecuteCommand("VO * Grids Space ShowEcliptic On WindowID 2")
        stkRoot.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 2e15 WindowID 2")
        button4.Enabled = True
    End Sub

    Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
        button4.Enabled = False
        _driver.MainSequence.RemoveAll()
        Dim initState As IAgVAMCSInitialState = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeInitialState, "InitialState1", "-"), IAgVAMCSInitialState)
        initState.CoordSystemName = "CentralBody/Sun J2000"
        initState.SetElementType(AgEVAElementType.eVAElementTypeKeplerian)
        initState.OrbitEpoch = "1 Mar 1997 00:00:00.000"
        Dim kep As IAgVAElementKeplerian = TryCast(initState.Element, IAgVAElementKeplerian)
        kep.SemiMajorAxis = 193216365.381
        kep.Eccentricity = 0.236386
        kep.Inclination = 23.455
        kep.RAAN = 0.258
        kep.ArgOfPeriapsis = 71.347
        kep.TrueAnomaly = 85.152
        button5.Enabled = True
    End Sub

    Private Sub button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button5.Click
        button5.Enabled = False
        Dim propagate As IAgVAMCSPropagate = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Propagate", "-"), IAgVAMCSPropagate)
        propagate.PropagatorName = "Heliocentric"
        DirectCast(propagate, IAgVAMCSSegment).Properties.Color = System.Drawing.Color.Orange
        propagate.EnableMaxPropagationTime = False
        DirectCast(propagate.StoppingConditions.Add("Periapsis").Properties, IAgVAStoppingCondition).CentralBodyName = "Earth"
        propagate.StoppingConditions.Remove("Duration")
        _driver.RunMCS()
        button6.Enabled = True
    End Sub

    Private Sub button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button6.Click
        button6.Enabled = False
        Dim dataDisplay As New Form2()
        Dim data As String = ""
        Dim propagate As IAgVAMCSPropagate = TryCast(_driver.MainSequence("Propagate"), IAgVAMCSPropagate)
        DirectCast(propagate.StoppingConditions("Periapsis").Properties, IAgVAStoppingCondition).CentralBodyName = "Mars"
        _driver.RunMCS()
        DirectCast(propagate, IAgVAMCSSegment).Properties.DisplayCoordinateSystem = "CentralBody/Mars Inertial"
        Dim result As IAgDrResult = DirectCast(propagate, IAgVAMCSSegment).ExecSummary
        Console.WriteLine(result.Category)
        Dim intervals As IAgDrIntervalCollection = TryCast(result.Value, IAgDrIntervalCollection)
        For i As Integer = 0 To intervals.Count - 1
            Dim interval As IAgDrInterval = intervals(i)
            Dim datasets As IAgDrDataSetCollection = interval.DataSets
            For j As Integer = 0 To datasets.Count - 1
                Dim dataset As IAgDrDataSet = datasets(j)
                dataDisplay.FormTitle = dataset.ElementName

                Dim elements As System.Array = dataset.GetValues()
                For Each o As Object In elements
                    data += o.ToString() + "" & Chr(13) & "" & Chr(10) & ""
                Next
            Next
        Next
        dataDisplay.ReportData = data
        dataDisplay.ShowDialog()
        button7.Enabled = True
    End Sub

    Private Sub button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button7.Click
        button7.Enabled = False
        Dim scene As IAgScenario = TryCast(stkRoot.CurrentScenario, IAgScenario)
        Dim components As IAgComponentInfoCollection = scene.ComponentDirectory.GetComponents(AgEComponent.eComponentAstrogator).GetFolder("Propagators")
        Dim cloneable As IAgCloneable = TryCast(components("Earth Point Mass"), IAgCloneable)        
        Dim epmComponent As IAgComponentInfo = TryCast(cloneable.CloneObject(), IAgComponentInfo)
        epmComponent.Name = "Mars Point Mass"
        Dim epm As IAgVANumericalPropagatorWrapper = TryCast(epmComponent, IAgVANumericalPropagatorWrapper)
        epm.CentralBodyName = "Mars"
        button8.Enabled = True
    End Sub

    Private Sub button8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button8.Click
        button8.Enabled = False
        Dim ts As IAgVAMCSTargetSequence = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "TargetSequence", "-"), IAgVAMCSTargetSequence)
        Dim man1 As IAgVAMCSManeuver = TryCast(ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "Maneuver1", "-"), IAgVAMCSManeuver)
        man1.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive)
        Dim impulsive As IAgVAManeuverImpulsive = TryCast(man1.Maneuver, IAgVAManeuverImpulsive)
        impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector)
        Dim thrustVector As IAgVAAttitudeControlImpulsiveThrustVector = TryCast(impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)

        man1.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX)
        man1.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianZ)
        Dim eccentricity As IAgVAStateCalcEccentricity = TryCast(DirectCast(man1, IAgVAMCSSegment).Results.Add("Keplerian Elems/Eccentricity"), IAgVAStateCalcEccentricity)
        eccentricity.CentralBodyName = "Mars"
        Dim dc As IAgVAProfileDifferentialCorrector = TryCast(ts.Profiles("Differential Corrector"), IAgVAProfileDifferentialCorrector)
        For i As Integer = 0 To dc.ControlParameters.Count - 1
            dc.ControlParameters(i).Enable = True
        Next
        dc.Results(0).Enable = True
        dc.Results(0).Tolerance = 0.01
        dc.MaxIterations = 50
        dc.Mode = AgEVAProfileMode.eVAProfileModeIterate
        ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles
        button9.Enabled = True
    End Sub

    Private Sub button9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button9.Click
        button9.Enabled = False
        Dim prop2 As IAgVAMCSPropagate = TryCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Propagate2", "-"), IAgVAMCSPropagate)
        Dim periapsis As IAgVAStoppingCondition = TryCast(prop2.StoppingConditions.Add("Periapsis").Properties, IAgVAStoppingCondition)
        prop2.StoppingConditions.Remove("Duration")
        periapsis.CentralBodyName = "Mars"
        periapsis.RepeatCount = 2
        prop2.PropagatorName = "Mars Point Mass"
        _driver.RunMCS()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class
