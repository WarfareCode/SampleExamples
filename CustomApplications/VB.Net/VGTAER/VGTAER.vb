'-------------------------------------------------------------------------
'
'  This is part of the STK 10 Object Model Examples
'  Copyright (C) 2011 Analytical Graphics, Inc.
'
'  This source code is intended as a reference to users of the
'	STK 10 Object Model.
'
'  File: VGTAER.cs
'  VGTAER
'
'
'  This examples shows how the Vector Geometry Tool (VGT) can be used to
'  calculate azimuth, elevation, and range (AER) values of a STK Object
'  from the reference frame of another object. In this example an
'  aircraft's AER data is calculated from the point of view of a ground
'  facility.
'
'--------------------------------------------------------------------------

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Imports AGI.STKObjects
Imports AGI.STKGraphics
Imports AGI.STKVgt
Imports AGI.STKUtil

Public Partial Class VGTAER
	Inherits Form
	Public Sub New()
		InitializeComponent()
	End Sub

    Private Sub VgtAERCalculator_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        Me.Refresh()
        root = New AgStkObjectRoot()
        root.NewScenario("VGTAER")
        manager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
        scene = manager.Scenes(0)
        root.ExecuteCommand("VO * Annotation Time Show Off ShowTimeStep Off")
        root.ExecuteCommand("VO * Annotation Frame Show Off")

        SetUnitPreferences()
        SetUpScenario()
        ViewScene()

        AddHandler root.OnAnimUpdate, AddressOf UpdateAER
    End Sub

	Private Sub SetUnitPreferences()
		root.UnitPreferences.SetCurrentUnit("DateFormat", "epSec")
		root.UnitPreferences.SetCurrentUnit("TimeUnit", "sec")
		root.UnitPreferences.SetCurrentUnit("DistanceUnit", "m")
		root.UnitPreferences.SetCurrentUnit("AngleUnit", "deg")
		root.UnitPreferences.SetCurrentUnit("LongitudeUnit", "deg")
		root.UnitPreferences.SetCurrentUnit("LatitudeUnit", "deg")
        root.UnitPreferences.SetCurrentUnit("Percent", "unitValue")
    End Sub

	Private Sub SetUpScenario()
		root.BeginUpdate()

		'
		' Create an aircraft and a facility
		'
		Dim aircraft As IAgAircraft = CreateAircraft()
		Dim facility As IAgFacility = CreateFacility()

		'
		' Create the required VGT vectors and angles
		'
		aircraftProvider = DirectCast(aircraft, IAgStkObject).Vgt
		facilityProvider = DirectCast(facility, IAgStkObject).Vgt

		' Displacement
		displacementVector = TryCast(facilityProvider.Vectors.Factory.CreateDisplacementVector(displacementVectorName, facilityProvider.Points("Center"), aircraftProvider.Points("Center")), IAgCrdnVector)

		' Azimuth
        azimuthAngle = facilityProvider.Angles.Factory.Create(azimuthAngleName, String.Empty, AgECrdnAngleType.eCrdnAngleTypeDihedralAngle)
		DirectCast(azimuthAngle, IAgCrdnAngleDihedral).FromVector.SetVector(facilityProvider.Vectors("Body.X"))
		DirectCast(azimuthAngle, IAgCrdnAngleDihedral).ToVector.SetVector(displacementVector)
		DirectCast(azimuthAngle, IAgCrdnAngleDihedral).PoleAbout.SetVector(facilityProvider.Vectors("Body.Z"))

		' Elevation
        elevationAngle = facilityProvider.Angles.Factory.Create(elevationAngleName, String.Empty, AgECrdnAngleType.eCrdnAngleTypeToPlane)
		DirectCast(elevationAngle, IAgCrdnAngleToPlane).ReferencePlane.SetPlane(facilityProvider.Planes("BodyXY"))
		DirectCast(elevationAngle, IAgCrdnAngleToPlane).ReferenceVector.SetVector(displacementVector)

		DisplayVGTVectors(facility.VO.Vector)

		root.EndUpdate()
	End Sub

	Private Function CreateAircraft() As IAgAircraft
		Const  constantVelocity As Double = 20

        Dim aircraft As IAgAircraft = TryCast(root.CurrentScenario.Children.New(AgESTKObjectType.eAircraft, "Aircraft"), IAgAircraft)
		aircraft.VO.Model.ScaleValue = 1.1
		aircraft.VO.Model.RouteMarker.Visible = False
		aircraft.Graphics.UseInstNameLabel = False
        aircraft.Graphics.LabelName = String.Empty
		aircraft.VO.Route.TrackData.SetLeadDataType(AgELeadTrailData.eDataNone)
		aircraft.VO.Route.TrackData.SetTrailSameAsLead()

		'
		' Construct the waypoint propagator for the aircraft
		'
		Dim propagator As IAgVePropagatorGreatArc = TryCast(aircraft.Route, IAgVePropagatorGreatArc)
        propagator.Method = AgEVeWayPtCompMethod.eDetermineTimeAccFromVel
        Dim interval As IAgCrdnEventIntervalSmartInterval = propagator.EphemerisInterval
        interval.SetExplicitInterval(DirectCast(root.CurrentScenario, IAgScenario).StartTime, interval.FindStopTime())

		'
		' Create the start point of our route with a particular date, location, and velocity.
		'
		Dim waypoint As IAgVeWaypointsElement = propagator.Waypoints.Add()
		waypoint.Latitude = 39.6
		waypoint.Longitude = -77.2
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		'
		' Create the next few waypoints from a location, the same velocity, and the previous waypoint.
		'
		waypoint = propagator.Waypoints.Add()
		waypoint.Latitude = 39.6
		waypoint.Longitude = -77.21
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		waypoint = propagator.Waypoints.Add()
		waypoint.Latitude = 39.61
		waypoint.Longitude = -77.22
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		waypoint = propagator.Waypoints.Add()
		waypoint.Latitude = 39.62
		waypoint.Longitude = -77.22
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		waypoint = propagator.Waypoints.Add()
		waypoint.Latitude = 39.63
		waypoint.Longitude = -77.21
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		waypoint = propagator.Waypoints.Add()
		waypoint.Latitude = 39.63
		waypoint.Longitude = -77.2
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		waypoint = propagator.Waypoints.Add()
		waypoint.Latitude = 39.62
		waypoint.Longitude = -77.19
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		waypoint = propagator.Waypoints.Add()
		waypoint.Latitude = 39.61
		waypoint.Longitude = -77.19
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		waypoint = propagator.Waypoints.Add()
		waypoint.Latitude = 39.6
		waypoint.Longitude = -77.2
		waypoint.Altitude = 3000.0
		waypoint.Speed = constantVelocity

		propagator.Propagate()

		' Set the StopTime so that the scenario can loop
        StopTime = Double.Parse(interval.FindStopTime().ToString())

		Return aircraft
	End Function

	Private Function CreateFacility() As IAgFacility
        Dim facility As IAgFacility = TryCast(root.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Facility"), IAgFacility)
		facility.VO.Model.ScaleValue = 1.1
		facility.VO.Model.Marker.Visible = False
		facility.Graphics.LabelVisible = False


		'
		' Position the facility at the center of the aircraft's route.
		'
		facility.Position.AssignPlanetodetic(39.615, -77.205, 0)

		Return facility
	End Function

	Private Sub DisplayVGTVectors(vectorSettings As IAgVOVector)
		Dim voElement As IAgVORefCrdn = vectorSettings.RefCrdns.Add(AgEGeometricElemType.eVectorElem, TryCast(displacementVector, IAgCrdn).QualifiedPath)
		Dim voVector As IAgVORefCrdnVector = TryCast(voElement, IAgVORefCrdnVector)
        voVector.Color = Color.Yellow
		voVector.MagnitudeVisible = True
		voVector.MagnitudeUnitAbrv = "m"
		voVector.LabelVisible = True

		voElement = vectorSettings.RefCrdns.Add(AgEGeometricElemType.eAngleElem, TryCast(azimuthAngle, IAgCrdn).QualifiedPath)
		Dim voAngle As IAgVORefCrdnAngle = TryCast(voElement, IAgVORefCrdnAngle)
        voAngle.Color = Color.LimeGreen
		voAngle.AngleValueVisible = True
		voAngle.AngleUnitAbrv = "deg"
		voAngle.LabelVisible = True

		voElement = vectorSettings.RefCrdns.Add(AgEGeometricElemType.eAngleElem, TryCast(elevationAngle, IAgCrdn).QualifiedPath)
		voAngle = TryCast(voElement, IAgVORefCrdnAngle)
        voAngle.Color = Color.Red
		voAngle.AngleValueVisible = True
		voAngle.AngleUnitAbrv = "deg"
		voAngle.LabelVisible = True
	End Sub

	Private Sub ViewScene()
		'
		' Set-up the animation for this specific example
		'
		DirectCast(root.CurrentScenario, IAgScenario).Animation.EnableAnimCycleTime = True
		DirectCast(root.CurrentScenario, IAgScenario).Animation.AnimCycleType = AgEScEndLoopType.eLoopAtTime
		DirectCast(root.CurrentScenario, IAgScenario).Animation.AnimCycleTime = StopTime
		DirectCast(root.CurrentScenario, IAgScenario).Animation.AnimStepValue = 0.5
		DirectCast(root, IAgAnimation).PlayForward()

		Dim offset As Array = New Object() {-2845, -7235, 6450}
		scene.Camera.ViewOffset(facilityProvider.Axes("NorthWestUp"), facilityProvider.Points("Center"), offset)

        AddTextBox("VGT can be used instead of Data Providers when calculating AER." & vbLf & _
                   "Azimuth:  The angle between the Body X-Axis and the projection of" & vbLf & _
                   "                  the displacement vector onto the BodyXY plane." & vbLf & _
                   "Elevation:  The angle between the displacement vector and the" & vbLf & _
                   "                   BodyXY plane." & vbLf & _
                   "Range:  The magnitude of the displacement vector.")

		scene.Render()
	End Sub

	Private Sub AddTextBox(text As String)
		Dim font As New Font("Arial", 12, FontStyle.Bold)
		Dim textSize As Size = Me.STKControl3D.CreateGraphics().MeasureString(text, font).ToSize()
		Dim textBitmap As New Bitmap(textSize.Width, textSize.Height)
		Dim gfx As Graphics = Graphics.FromImage(textBitmap)
		gfx.DrawString(text, font, Brushes.White, New PointF(0, 0))

		Dim overlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 0, textSize.Width, textSize.Height)
		DirectCast(overlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft
		DirectCast(overlay, IAgStkGraphicsOverlay).BorderSize = 2
        DirectCast(overlay, IAgStkGraphicsOverlay).BorderColor = Color.White

		Dim filePath As String = Application.StartupPath & "TemoraryTextOverlay.bmp"
		textBitmap.Save(filePath)
		overlay.Texture = manager.Textures.LoadFromStringUri(filePath)
		System.IO.File.Delete(filePath)

		Dim overlayPosition As Array = DirectCast(overlay, IAgStkGraphicsOverlay).Position
		Dim overlaySize As Array = DirectCast(overlay, IAgStkGraphicsOverlay).Size

		Dim baseOverlay As IAgStkGraphicsScreenOverlay = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(overlayPosition, overlaySize)
		DirectCast(baseOverlay, IAgStkGraphicsOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft
        DirectCast(baseOverlay, IAgStkGraphicsOverlay).Color = Color.Black
		DirectCast(baseOverlay, IAgStkGraphicsOverlay).Translucency = 0.5F

		Dim baseOverlayManager As IAgStkGraphicsScreenOverlayCollectionBase = TryCast(DirectCast(baseOverlay, IAgStkGraphicsOverlay).Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
		baseOverlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))
        DirectCast(baseOverlay, IAgStkGraphicsOverlay).Position = New Object() {5, 10, DirectCast(baseOverlay, IAgStkGraphicsOverlay).Position.GetValue(2), DirectCast(baseOverlay, IAgStkGraphicsOverlay).Position.GetValue(3)}

		Dim overlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays.Overlays, IAgStkGraphicsScreenOverlayCollectionBase)
		overlayManager.Add(DirectCast(baseOverlay, IAgStkGraphicsScreenOverlay))
	End Sub

	Private Sub UpdateAER(TimeEpSec As Double)
		' Azimuth
		Dim azimuthResult As IAgCrdnAngleFindAngleResult = azimuthAngle.FindAngle(TimeEpSec)

		' Elevation
		Dim elevationResult As IAgCrdnAngleFindAngleResult = elevationAngle.FindAngle(TimeEpSec)

		' Range
		Dim rangeResult As IAgCrdnVectorFindInAxesResult = displacementVector.FindInAxes(TimeEpSec, facilityProvider.Axes("Body"))

		Dim azimuth As Double = DirectCast(azimuthResult.Angle, Double)
		Dim elevation As Double = DirectCast(elevationResult.Angle, Double)
		Dim range As Double = GetVectorMagnitude(rangeResult.Vector)

        azimuthValueLabel.Text = String.Format("{0:0.000}", azimuth) & " deg"
        elevationValueLabel.Text = String.Format("{0:0.000}", elevation) & " deg"
        rangeValueLabel.Text = String.Format("{0:0.000}", range) & " m"
	End Sub

	Private Shared Function GetVectorMagnitude(vector As IAgCartesian3Vector) As Double
		Return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z)
	End Function

	Private Shared Function RadiansToDegrees(radians As Double) As Double
		Return radians * (180.0 / Math.PI)
	End Function

	#Region "Animation Controls"
    Private Sub Play_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Play.Click
        DirectCast(root, IAgAnimation).PlayForward()
    End Sub

    Private Sub Pause_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Pause.Click
        DirectCast(root, IAgAnimation).Pause()
    End Sub

    Private Sub Rewind_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Rewind.Click
        DirectCast(root, IAgAnimation).Rewind()
    End Sub
	#End Region

	Private aircraftProvider As IAgCrdnProvider
	Private facilityProvider As IAgCrdnProvider

	Private displacementVector As IAgCrdnVector
	Private azimuthAngle As IAgCrdnAngle
	Private elevationAngle As IAgCrdnAngle

	Private StopTime As Double

	Private root As AgStkObjectRoot
	Private manager As IAgStkGraphicsSceneManager
	Private scene As IAgStkGraphicsScene

	Private Const displacementVectorName As String = "Range"
	Private Const azimuthAngleName As String = "Azimuth"
	Private Const elevationAngleName As String = "Elevation"

    Private Sub VGTAER_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If Not root Is Nothing Then
            root.CloseScenario()
        End If
    End Sub
End Class
