Imports AGI.STKObjects
Imports AGI.STKObjects.Astrogator
Imports AGI.STKUtil

Public Class Form1

    Private stkRootObject As AgStkObjectRoot = Nothing
    Private _driver As IAgVADriverMCS
    Private _lunarProbe As IAgSatellite
    Private _launch As IAgVAMCSLaunch
    Private _coast As IAgVAMCSPropagate
    Private _toPersilene As IAgVAMCSPropagate
    Private _ts As IAgVAMCSTargetSequence
    Private _transLunarInjection As IAgVAMCSManeuver
    Private _dcCopy As IAgVAProfileDifferentialCorrector

    Private ReadOnly Property stkRoot() As AGI.STKObjects.AgStkObjectRoot
        Get
            If stkRootObject Is Nothing Then
                Dim STKXApp As AGI.STKX.AgSTKXApplication = New AGI.STKX.AgSTKXApplication

                If STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl) Then
                    stkRootObject = TryCast(New AgStkObjectRootClass(), AgStkObjectRoot)
                    If stkRootObject.AvailableFeatures.IsPropagatorTypeAvailable(AgEVePropagatorType.ePropagatorAstrogator) Then
                        stkRootObject = TryCast(New AgStkObjectRootClass(), AgStkObjectRoot)
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
        stkRoot.NewScenario("Test")
        stkRoot.UnitPreferences.SetCurrentUnit("DistanceUnit", "km")
        ' 
        Dim scene As IAgScenario = DirectCast(stkRoot.CurrentScenario, IAgScenario)
        scene.StartTime = "1 Jan 1993 00:00:00.00"
        scene.StopTime = "1 Jan 1994 00:00:00.00"
        scene.Animation.StartTime = "1 Jan 1993 00:00:00.00"
        scene.Animation.EnableAnimCycleTime = True
        scene.Animation.AnimCycleType = AgEScEndLoopType.eEndTime
        scene.Animation.AnimCycleTime = "1 Jan 1994 00:00:00.00"
        stkRoot.Rewind()
        button2.Enabled = True
    End Sub

    Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button2.Click
        button2.Enabled = False
        Dim sun As IAgPlanet = DirectCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.ePlanet, "Sun"), IAgPlanet)
        sun.PositionSource = AgEPlPositionSourceType.ePosCentralBody
        Dim pos As IAgPlPosCentralBody = DirectCast(sun.PositionSourceData, IAgPlPosCentralBody)
        pos.AutoRename = True
        pos.CentralBody = "Sun"

        Dim moon As IAgPlanet = DirectCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.ePlanet, "Moon"), IAgPlanet)
        moon.PositionSource = AgEPlPositionSourceType.ePosCentralBody
        pos = DirectCast(moon.PositionSourceData, IAgPlPosCentralBody)
        pos.CentralBody = "Moon"

        Dim earth As IAgPlanet = DirectCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.ePlanet, "Earth"), IAgPlanet)
        earth.PositionSource = AgEPlPositionSourceType.ePosCentralBody
        pos = DirectCast(earth.PositionSourceData, IAgPlPosCentralBody)
        pos.CentralBody = "Earth"
        button3.Enabled = True
    End Sub

    Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button3.Click
        button3.Enabled = False
        _lunarProbe = DirectCast(stkRoot.CurrentScenario.Children.[New](AgESTKObjectType.eSatellite, "LunarProbe"), IAgSatellite)
        _lunarProbe.SetPropagatorType(AgEVePropagatorType.ePropagatorAstrogator)
        _driver = DirectCast(_lunarProbe.Propagator, IAgVADriverMCS)
        _driver.Options.DrawTrajectoryIn2D = True
        _driver.Options.DrawTrajectoryIn3D = True
        _driver.Options.UpdateAnimationTimeForAllObjects = True
        _lunarProbe.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesBasic)
        ' 
        _lunarProbe.Graphics.PassData.GroundTrack.SetLeadDataType(AgELeadTrailData.eDataNone)
        _lunarProbe.Graphics.PassData.Orbit.SetLeadDataType(AgELeadTrailData.eDataAll)
        _lunarProbe.VO.Pass.TrackData.InheritFrom2D = True
        _lunarProbe.VO.Model.OrbitMarker.MarkerType = AgEMarkerType.eShape
        Dim markerData As IAgVOMarkerShape = DirectCast(_lunarProbe.VO.Model.OrbitMarker.MarkerData, IAgVOMarkerShape)
        markerData.Style = AgE3dMarkerShape.e3dShapePoint
        _lunarProbe.VO.Model.OrbitMarker.PixelSize = 7
        _lunarProbe.VO.Model.DetailThreshold.MarkerLabel = 1000000000000
        _lunarProbe.VO.Model.DetailThreshold.Marker = 1000000000000
        _lunarProbe.VO.Model.DetailThreshold.Point = 1000000000000
        button4.Enabled = True
    End Sub

    Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button4.Click
        button4.Enabled = False
        stkRoot.ExecuteCommand("MapDetails * Background Image None")
        stkRoot.ExecuteCommand("MapDetails * Map RWDB2_Coastlines State Off")
        stkRoot.ExecuteCommand("MapDetails * Map RWDB2_International_Borders State Off")
        stkRoot.ExecuteCommand("MapDetails * Map RWDB2_Islands State Off")
        stkRoot.ExecuteCommand("MapDetails * LatLon Lat Off")
        stkRoot.ExecuteCommand("MapDetails * LatLon Lon Off")
        stkRoot.ExecuteCommand("MapProjection * Orthographic Center 89 -90 Format BBR 900000 Sun")

        button5.Enabled = True
    End Sub

    Private Sub button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button5.Click
        button5.Enabled = False
        stkRoot.ExecuteCommand("VO * Celestial Moon Label Off WindowID 2")
        stkRoot.ExecuteCommand("VO * Grids Space ShowECI On ShowRadial On WindowID 2")
        stkRoot.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 1000000000000 WindowID 2")
        button6.Enabled = True
    End Sub

    Private Sub button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button6.Click
        button6.Enabled = False
        stkRoot.ExecuteCommand("VO * CentralBody Moon 1")
        stkRoot.ExecuteCommand("VO * Celestial Moon Label Off WindowID 1")
        stkRoot.ExecuteCommand("VO * Grids Space ShowECI On ShowRadial On WindowID 1")
        stkRoot.ExecuteCommand("Window3D * ViewVolume MaxVisibleDist 1000000000000 WindowID 1")
        button7.Enabled = True
    End Sub

    Private Sub button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button7.Click
        button7.Enabled = False
        _driver.MainSequence.RemoveAll()
        _ts = DirectCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "Target Sequence", "-"), IAgVAMCSTargetSequence)
        _launch = DirectCast(_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeLaunch, "Launch", "-"), IAgVAMCSLaunch)
        _launch.Epoch = "1 Jan 1993 00:00:00.00"
        ' 
        _coast = DirectCast(_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Coast", "-"), IAgVAMCSPropagate)
        DirectCast(_coast.StoppingConditions(0).Properties, IAgVAStoppingCondition).Trip = 2700
        ' 
        _transLunarInjection = DirectCast(_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "TransLunarInjection", "-"), IAgVAMCSManeuver)
        _transLunarInjection.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive)
        Dim impulsive As IAgVAManeuverImpulsive = DirectCast(_transLunarInjection.Maneuver, IAgVAManeuverImpulsive)
        impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector)
        Dim thrustVector As IAgVAAttitudeControlImpulsiveThrustVector = DirectCast(impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        ' 
        Dim toSwingBy As IAgVAMCSPropagate = DirectCast(_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypePropagate, "ToSwingBy", "-"), IAgVAMCSPropagate)
        toSwingBy.PropagatorName = "CisLunar"
        DirectCast(toSwingBy.StoppingConditions.Add("R Magnitude").Properties, IAgVAStoppingCondition).Trip = 300000
        toSwingBy.StoppingConditions.Remove("Duration")
        ' 
        _toPersilene = DirectCast(_ts.Segments.Insert(AgEVASegmentType.eVASegmentTypePropagate, "ToPersilene", "-"), IAgVAMCSPropagate)
        _toPersilene.PropagatorName = "CisLunar"
        DirectCast(_toPersilene.StoppingConditions(0).Properties, IAgVAStoppingCondition).Trip = 864000
        Dim alt As IAgVAStoppingCondition = DirectCast(_toPersilene.StoppingConditions.Add("Altitude").Properties, IAgVAStoppingCondition)
        alt.Trip = 0
        alt.CentralBodyName = "Moon"
        ' 
        Dim periapsis As IAgVAStoppingCondition = DirectCast(_toPersilene.StoppingConditions.Add("Periapsis").Properties, IAgVAStoppingCondition)
        periapsis.CentralBodyName = "Moon"
        button8.Enabled = True
    End Sub

    Private Sub button8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button8.Click
        button8.Enabled = False
        Dim impulsive As IAgVAManeuverImpulsive = DirectCast(_transLunarInjection.Maneuver, IAgVAManeuverImpulsive)
        Dim thrustVector As IAgVAAttitudeControlImpulsiveThrustVector = DirectCast(impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        thrustVector.DeltaVVector.AssignCartesian(3.15, 0, 0)
        _driver.RunMCS()
        button9.Enabled = True
    End Sub

    Private Sub button9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button9.Click
        button9.Enabled = False
        _launch.EnableControlParameter(AgEVAControlLaunch.eVAControlLaunchEpoch)
        _coast.StoppingConditions(0).EnableControlParameter(AgEVAControlStoppingCondition.eVAControlStoppingConditionTripValue)
        ' 
        Dim calcObject As IAgComponentInfo = DirectCast(_toPersilene, IAgVAMCSSegment).Results.Add("MultiBody/Delta Right Asc")
        DirectCast(_toPersilene, IAgVAMCSSegment).Results.Add("MultiBody/Delta Declination")
        ' 
        ' 
        Dim diffCorrector As IAgVAProfileDifferentialCorrector = DirectCast(_ts.Profiles(0), IAgVAProfileDifferentialCorrector)
        diffCorrector.Name = "Delta RA and Dec"
        diffCorrector.ControlParameters.GetControlByPaths("Launch", "Launch.Epoch").Perturbation = 60
        diffCorrector.ControlParameters.GetControlByPaths("Launch", "Launch.Epoch").MaxStep = 3600
        diffCorrector.ControlParameters.GetControlByPaths("Launch", "Launch.Epoch").Enable = True
        diffCorrector.ControlParameters.GetControlByPaths("Coast", "StoppingConditions.Duration.TripValue").Perturbation = 60
        diffCorrector.ControlParameters.GetControlByPaths("Coast", "StoppingConditions.Duration.TripValue").MaxStep = 300
        diffCorrector.ControlParameters.GetControlByPaths("Coast", "StoppingConditions.Duration.TripValue").Enable = True
        ' 
        diffCorrector.Results(0).DesiredValue = 0
        diffCorrector.Results(0).Enable = True
        ' 
        diffCorrector.Results(1).DesiredValue = 0
        diffCorrector.Results(1).Enable = True
        button10.Enabled = True
    End Sub

    Private Sub button10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button10.Click
        button10.Enabled = False
        _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfilesOnce
        _driver.RunMCS()
        _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles
        _driver.RunMCS()
        ' 
        _ts.ApplyProfiles()
        _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunNominalSeq
        _driver.RunMCS()
        button11.Enabled = True
    End Sub

    Private Sub button11_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button11.Click
        button11.Enabled = False
        _dcCopy = DirectCast(_ts.Profiles(0).Copy(), IAgVAProfileDifferentialCorrector)
        _dcCopy.Name = "B_Plane_Targeting"
        _ts.Profiles(0).Mode = AgEVAProfileMode.eVAProfileModeNotActive
        ' 
        DirectCast(_toPersilene, IAgVAMCSSegment).Results.Add("Epoch")
        DirectCast(_toPersilene, IAgVAMCSSegment).Results.Add("MultiBody/BDotT")
        DirectCast(_toPersilene, IAgVAMCSSegment).Results.Add("MultiBody/BDotR")
        ' 
        _transLunarInjection.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX)
        ' 
        _dcCopy.ControlParameters.GetControlByPaths("TransLunarInjection", "ImpulsiveMnvr.Cartesian.X").Enable = True
        ' 
        _dcCopy.Results.GetResultByPaths("ToPersilene", "Delta Declination").Enable = False
        _dcCopy.Results.GetResultByPaths("ToPersilene", "Delta Right Asc").Enable = False
        ' 
        _dcCopy.Results.GetResultByPaths("ToPersilene", "BDotR").Enable = True
        _dcCopy.Results.GetResultByPaths("ToPersilene", "BDotT").Enable = True
        _dcCopy.Results.GetResultByPaths("ToPersilene", "Epoch").Enable = True
        _dcCopy.Results.GetResultByPaths("ToPersilene", "BDotR").DesiredValue = 5000
        _dcCopy.Results.GetResultByPaths("ToPersilene", "BDotT").DesiredValue = 0
        _dcCopy.Results.GetResultByPaths("ToPersilene", "Epoch").DesiredValue = "4 Jan 1993 00:00:00.00"
        button12.Enabled = True
    End Sub

    Private Sub button12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button12.Click
        button12.Enabled = False
        Dim template As IAgVeVOBPlaneTemplate = DirectCast(_lunarProbe.VO.BPlanes.Templates.Add(), IAgVeVOBPlaneTemplate)
        template.Name = "Lunar_B-Plane"
        template.CentralBody = "Moon"
        template.ReferenceVector = "CentralBody/Moon Orbit_Normal Vector"
        Dim bPlane As IAgVeVOBPlaneInstance = DirectCast(_lunarProbe.VO.BPlanes.Instances.Add("Lunar_B-Plane"), IAgVeVOBPlaneInstance)
        bPlane.Name = "LunarBPlane"
        ' 
        DirectCast(_toPersilene, IAgVAMCSSegment).Properties.BPlanes.Add("LunarBPlane")
        DirectCast(_toPersilene, IAgVAMCSSegment).Properties.ApplyFinalStateToBPlanes()
        button13.Enabled = True
    End Sub

    Private Sub button13_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button13.Click
        button13.Enabled = False
        _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfilesOnce
        _driver.RunMCS()
        ' 
        _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles
        _driver.RunMCS()

        _ts.ApplyProfiles()
        _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunNominalSeq
        _driver.RunMCS()
        button14.Enabled = True
    End Sub

    Private Sub button14_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button14.Click
        button14.Enabled = False
        stkRoot.ExecuteCommand("VectorTool * Moon Create Axes True_Moon_Equator ""Aligned and Constrained"" Cartesian 0 0 1 ""CentralBody/Moon Angular_Velocity"" Cartesian 1 0 0 ""CentralBody/Moon VernalEquinox""")
        stkRoot.ExecuteCommand("VectorTool * Moon Create System True_Lunar_Equatorial ""Assembled"" ""CentralBody/Moon Center"" ""CentralBody/Moon True_Moon_Equator""")
        ' 
        ' 
        Dim altInc As IAgVAProfileDifferentialCorrector = DirectCast(_dcCopy.Copy(), IAgVAProfileDifferentialCorrector)
        altInc.Name = "Altitude and Inclination"
        ' 
        _ts.Profiles(0).Mode = AgEVAProfileMode.eVAProfileModeNotActive
        _dcCopy.Mode = AgEVAProfileMode.eVAProfileModeNotActive
        ' 
        ' 
        Dim calcAlt As IAgVAStateCalcGeodeticElem = DirectCast(DirectCast(_toPersilene, IAgVAMCSSegment).Results.Add("Geodetic/Altitude"), IAgVAStateCalcGeodeticElem)
        calcAlt.CentralBodyName = "Moon"
        Dim calcInc As IAgVAStateCalcInclination = DirectCast(DirectCast(_toPersilene, IAgVAMCSSegment).Results.Add("Keplerian Elems/Inclination"), IAgVAStateCalcInclination)
        calcInc.CoordSystemName = "CentralBody/Moon True_Lunar_Equatorial"
        For count As Integer = 0 To altInc.Results.Count - 1
            ' 

            altInc.Results(count).Enable = False
        Next
        altInc.Results.GetResultByPaths("ToPersilene", "Altitude").Enable = True
        altInc.Results.GetResultByPaths("ToPersilene", "Epoch").Enable = True
        altInc.Results.GetResultByPaths("ToPersilene", "Inclination").Enable = True
        ' 
        altInc.Results.GetResultByPaths("ToPersilene", "Altitude").DesiredValue = 100
        altInc.Results.GetResultByPaths("ToPersilene", "Inclination").DesiredValue = 90
        altInc.Results.GetResultByPaths("ToPersilene", "Epoch").DesiredValue = "4 Jan 1993 00:00:00.00"
        ' 
        _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles
        _driver.RunMCS()
        _ts.ApplyProfiles()
        _ts.Action = AgEVATargetSeqAction.eVATargetSeqActionRunNominalSeq
        _driver.RunMCS()

        button15.Enabled = True
    End Sub

    Private Sub button15_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button15.Click
        button15.Enabled = False
        Dim prop3Day As IAgVAMCSPropagate = DirectCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypePropagate, "Prop3Days", "-"), IAgVAMCSPropagate)
        prop3Day.PropagatorName = "CisLunar"
        DirectCast(prop3Day.StoppingConditions(0).Properties, IAgVAStoppingCondition).Trip = 259200
        ' 
        _driver.RunMCS()
        button16.Enabled = True
    End Sub

    Private Sub button16_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button16.Click
        button16.Enabled = False
        Dim ts2 As IAgVAMCSTargetSequence = DirectCast(_driver.MainSequence.Insert(AgEVASegmentType.eVASegmentTypeTargetSequence, "Target Sequence2", "Prop3Days"), IAgVAMCSTargetSequence)
        Dim loi As IAgVAMCSManeuver = DirectCast(ts2.Segments.Insert(AgEVASegmentType.eVASegmentTypeManeuver, "LOI", "-"), IAgVAMCSManeuver)
        loi.SetManeuverType(AgEVAManeuverType.eVAManeuverTypeImpulsive)
        Dim impulsive As IAgVAManeuverImpulsive = DirectCast(loi.Maneuver, IAgVAManeuverImpulsive)
        impulsive.SetAttitudeControlType(AgEVAAttitudeControl.eVAAttitudeControlThrustVector)
        Dim thrust As IAgVAAttitudeControlImpulsiveThrustVector = DirectCast(impulsive.AttitudeControl, IAgVAAttitudeControlImpulsiveThrustVector)
        thrust.ThrustAxesName = "Satellite VNC(Moon)"
        loi.EnableControlParameter(AgEVAControlManeuver.eVAControlManeuverImpulsiveCartesianX)
        Dim ecc As IAgVAStateCalcEccentricity = DirectCast(DirectCast(loi, IAgVAMCSSegment).Results.Add("Keplerian Elems/Eccentricity"), IAgVAStateCalcEccentricity)
        ecc.CentralBodyName = "Moon"
        ' 
        Dim diffCorrector As IAgVAProfileDifferentialCorrector = DirectCast(ts2.Profiles(0), IAgVAProfileDifferentialCorrector)
        diffCorrector.ControlParameters(0).Enable = True
        diffCorrector.Results(0).Enable = True
        ' 
        ts2.Action = AgEVATargetSeqAction.eVATargetSeqActionRunActiveProfiles
        _driver.RunMCS()
        ts2.ApplyProfiles()
        ts2.Action = AgEVATargetSeqAction.eVATargetSeqActionRunNominalSeq
        _driver.RunMCS()
    End Sub

    Private Sub button17_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button17.Click
        stkRoot.PlayForward()
    End Sub

    Private Sub button18_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button18.Click
        stkRoot.Rewind()
    End Sub

    Private Sub button19_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button19.Click
        stkRoot.Slower()
    End Sub

    Private Sub button20_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button20.Click
        stkRoot.Faster()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not (IsNothing(stkRootObject)) Then
            stkRootObject.CloseScenario()
        End If
    End Sub
End Class
