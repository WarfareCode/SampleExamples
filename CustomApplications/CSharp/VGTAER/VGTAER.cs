//-------------------------------------------------------------------------
//
//  This is part of the STK 10 Object Model Examples
//  Copyright (C) 2011 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 10 Object Model.
//
//  File: VGTAER.cs
//  VGTAER
//
//
//  This examples shows how the Vector Geometry Tool (VGT) can be used to
//  calculate azimuth, elevation, and range (AER) values of a STK Object
//  from the reference frame of another object. In this example an
//  aircraft's AER data is calculated from the point of view of a ground
//  facility.
//
//--------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AGI.STKObjects;
using AGI.STKGraphics;
using AGI.STKVgt;
using AGI.STKUtil;

namespace VGTAER
{
    public partial class VGTAER : Form
    {
        public VGTAER()
        {
            InitializeComponent();
        }

        private void VgtAERCalculator_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            root = new AgStkObjectRoot();
            root.NewScenario("VGTAER");
            manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            scene = manager.Scenes[0];
            root.ExecuteCommand("VO * Annotation Time Show Off ShowTimeStep Off");
            root.ExecuteCommand("VO * Annotation Frame Show Off");

            SetUnitPreferences();
            SetUpScenario();
            ViewScene();
            
            root.OnAnimUpdate +=new IAgStkObjectRootEvents_OnAnimUpdateEventHandler(UpdateAER);
        }

        private void SetUnitPreferences()
        {
            root.UnitPreferences.SetCurrentUnit("DateFormat", "epSec");
            root.UnitPreferences.SetCurrentUnit("TimeUnit", "sec");
            root.UnitPreferences.SetCurrentUnit("DistanceUnit", "m");
            root.UnitPreferences.SetCurrentUnit("AngleUnit", "deg");
            root.UnitPreferences.SetCurrentUnit("LongitudeUnit", "deg");
            root.UnitPreferences.SetCurrentUnit("LatitudeUnit", "deg");
            root.UnitPreferences.SetCurrentUnit("Percent", "unitValue");
        }

        private void SetUpScenario()
        {
            root.BeginUpdate();

            //
            // Create an aircraft and a facility
            //
            IAgAircraft aircraft = CreateAircraft();
            IAgFacility facility = CreateFacility();

            //
            // Create the required VGT vectors and angles
            //
            aircraftProvider = ((IAgStkObject)aircraft).Vgt;
            facilityProvider = ((IAgStkObject)facility).Vgt;

            // Displacement
            displacementVector = facilityProvider.Vectors.Factory.CreateDisplacementVector(displacementVectorName, facilityProvider.Points["Center"], aircraftProvider.Points["Center"]) as IAgCrdnVector;

            // Azimuth
            azimuthAngle = facilityProvider.Angles.Factory.Create(azimuthAngleName, String.Empty, AgECrdnAngleType.eCrdnAngleTypeDihedralAngle);
            ((IAgCrdnAngleDihedral)azimuthAngle).FromVector.SetVector(facilityProvider.Vectors["Body.X"]);
            ((IAgCrdnAngleDihedral)azimuthAngle).ToVector.SetVector(displacementVector);
            ((IAgCrdnAngleDihedral)azimuthAngle).PoleAbout.SetVector(facilityProvider.Vectors["Body.Z"]);

            // Elevation
            elevationAngle = facilityProvider.Angles.Factory.Create(elevationAngleName, String.Empty, AgECrdnAngleType.eCrdnAngleTypeToPlane);
            ((IAgCrdnAngleToPlane)elevationAngle).ReferencePlane.SetPlane(facilityProvider.Planes["BodyXY"]);
            ((IAgCrdnAngleToPlane)elevationAngle).ReferenceVector.SetVector(displacementVector);

            DisplayVGTVectors(facility.VO.Vector);

            root.EndUpdate();
        }

        private IAgAircraft CreateAircraft()
        {
            const double constantVelocity = 20;

            IAgAircraft aircraft = root.CurrentScenario.Children.New(AgESTKObjectType.eAircraft, "Aircraft") as IAgAircraft;
            aircraft.VO.Model.ScaleValue = 1.1;
            aircraft.VO.Model.RouteMarker.Visible = false;
            aircraft.Graphics.UseInstNameLabel = false;
            aircraft.Graphics.LabelName = String.Empty;
            aircraft.VO.Route.TrackData.SetLeadDataType(AgELeadTrailData.eDataNone);
            aircraft.VO.Route.TrackData.SetTrailSameAsLead();

            //
            // Construct the waypoint propagator for the aircraft
            //
            IAgVePropagatorGreatArc propagator = aircraft.Route as IAgVePropagatorGreatArc;
            propagator.Method = AgEVeWayPtCompMethod.eDetermineTimeAccFromVel;
            IAgCrdnEventIntervalSmartInterval interval = propagator.EphemerisInterval;
            interval.SetExplicitInterval(((IAgScenario)root.CurrentScenario).StartTime, propagator.EphemerisInterval.FindStopTime());

            //
            // Create the start point of our route with a particular date, location, and velocity.
            //
            IAgVeWaypointsElement waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.60;
            waypoint.Longitude = -77.20;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            //
            // Create the next few waypoints from a location, the same velocity, and the previous waypoint.
            //
            waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.60;
            waypoint.Longitude = -77.21;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.61;
            waypoint.Longitude = -77.22;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.62;
            waypoint.Longitude = -77.22;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.63;
            waypoint.Longitude = -77.21;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.63;
            waypoint.Longitude = -77.20;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.62;
            waypoint.Longitude = -77.19;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.61;
            waypoint.Longitude = -77.19;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            waypoint = propagator.Waypoints.Add();
            waypoint.Latitude = 39.60;
            waypoint.Longitude = -77.20;
            waypoint.Altitude = 3000.0;
            waypoint.Speed = constantVelocity;

            propagator.Propagate();

            // Set the StopTime so that the scenario can loop
            StopTime = double.Parse(interval.FindStopTime().ToString());

            return aircraft;
        }

        private IAgFacility CreateFacility()
        {
            IAgFacility facility = root.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Facility") as IAgFacility;
            facility.VO.Model.ScaleValue = 1.1;
            facility.VO.Model.Marker.Visible = false;
            facility.Graphics.LabelVisible = false;
            

            //
            // Position the facility at the center of the aircraft's route.
            //
            facility.Position.AssignPlanetodetic(39.615, -77.205, 0);

            return facility;
        }

        private void DisplayVGTVectors(IAgVOVector vectorSettings)
        {
            IAgVORefCrdn voElement = vectorSettings.RefCrdns.Add(AgEGeometricElemType.eVectorElem, (displacementVector as IAgCrdn).QualifiedPath);
            IAgVORefCrdnVector voVector = voElement as IAgVORefCrdnVector;
            voVector.Color = Color.Yellow;
            voVector.MagnitudeVisible = true;
            voVector.MagnitudeUnitAbrv = "m";
            voVector.LabelVisible = true;

            voElement = vectorSettings.RefCrdns.Add(AgEGeometricElemType.eAngleElem, (azimuthAngle as IAgCrdn).QualifiedPath);
            IAgVORefCrdnAngle voAngle = voElement as IAgVORefCrdnAngle;
            voAngle.Color = Color.LimeGreen;
            voAngle.AngleValueVisible = true;
            voAngle.AngleUnitAbrv = "deg";
            voAngle.LabelVisible = true;

            voElement = vectorSettings.RefCrdns.Add(AgEGeometricElemType.eAngleElem, (elevationAngle as IAgCrdn).QualifiedPath);
            voAngle = voElement as IAgVORefCrdnAngle;
            voAngle.Color = Color.Red;
            voAngle.AngleValueVisible = true;
            voAngle.AngleUnitAbrv = "deg";
            voAngle.LabelVisible = true;
        }

        private void ViewScene()
        {
            //
            // Set-up the animation for this specific example
            //
            ((IAgScenario)root.CurrentScenario).Animation.EnableAnimCycleTime = true;
            ((IAgScenario)root.CurrentScenario).Animation.AnimCycleType = AgEScEndLoopType.eLoopAtTime;
            ((IAgScenario)root.CurrentScenario).Animation.AnimCycleTime = StopTime;
            ((IAgScenario)root.CurrentScenario).Animation.AnimStepValue = 0.5;
            ((IAgAnimation)root).PlayForward();

            Array offset = new object[] { -2845, -7235, 6450 };
            scene.Camera.ViewOffset(facilityProvider.Axes["NorthWestUp"], facilityProvider.Points["Center"], ref offset);

            AddTextBox("VGT can be used instead of Data Providers when calculating AER.\n" +
                       "Azimuth:  The angle between the Body X-Axis and the projection of\n" + 
                       "                  the displacement vector onto the BodyXY plane.\n" +
                       "Elevation:  The angle between the displacement vector and the\n" + 
                       "                   BodyXY plane.\n" +
                       "Range:  The magnitude of the displacement vector.");
            
            scene.Render();
        }

        private void AddTextBox(string text)
        {
            Font font = new Font("Arial", 12, FontStyle.Bold);
            Size textSize = this.STKControl3D.CreateGraphics().MeasureString(text, font).ToSize();
            Bitmap textBitmap = new Bitmap(textSize.Width, textSize.Height);
            Graphics gfx = Graphics.FromImage(textBitmap);
            gfx.DrawString(text, font, Brushes.White, new PointF(0, 0));

            IAgStkGraphicsTextureScreenOverlay overlay =
                manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 0, textSize.Width, textSize.Height);
            ((IAgStkGraphicsOverlay)overlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft;
            ((IAgStkGraphicsOverlay)overlay).BorderSize = 2;
            ((IAgStkGraphicsOverlay)overlay).BorderColor = Color.White;

            string filePath = Application.StartupPath + "TemoraryTextOverlay.bmp";
            textBitmap.Save(filePath);
            overlay.Texture = manager.Textures.LoadFromStringUri(filePath);
            System.IO.File.Delete(filePath); 

            Array overlayPosition = ((IAgStkGraphicsOverlay)overlay).Position;
            Array overlaySize = ((IAgStkGraphicsOverlay)overlay).Size;

            IAgStkGraphicsScreenOverlay baseOverlay = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(ref overlayPosition, ref overlaySize);
            ((IAgStkGraphicsOverlay)baseOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft;
            ((IAgStkGraphicsOverlay)baseOverlay).Color = Color.Black;
            ((IAgStkGraphicsOverlay)baseOverlay).Translucency = 0.5f;

            IAgStkGraphicsScreenOverlayCollectionBase baseOverlayManager = ((IAgStkGraphicsOverlay)baseOverlay).Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
            baseOverlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
            ((IAgStkGraphicsOverlay)baseOverlay).Position = new object[] 
                { 
                    5, 10, 
                    AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels,
                    AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels
                };

            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            overlayManager.Add((IAgStkGraphicsScreenOverlay)baseOverlay);
        }

        private void UpdateAER(double TimeEpSec)
        {
            // Azimuth
            IAgCrdnAngleFindAngleResult azimuthResult = azimuthAngle.FindAngle(TimeEpSec);

            // Elevation
            IAgCrdnAngleFindAngleResult elevationResult = elevationAngle.FindAngle(TimeEpSec);

            // Range
            IAgCrdnVectorFindInAxesResult rangeResult = displacementVector.FindInAxes(TimeEpSec, facilityProvider.Axes["Body"]);

            double azimuth = (double)azimuthResult.Angle;
            double elevation = (double)elevationResult.Angle;
            double range = GetVectorMagnitude(rangeResult.Vector);

            azimuthValueLabel.Text = String.Format("{0:0.000}", azimuth) + " deg";
            elevationValueLabel.Text = String.Format("{0:0.000}", elevation) + " deg";
            rangeValueLabel.Text = String.Format("{0:0.000}", range) + " m";
        }

        private static double GetVectorMagnitude(IAgCartesian3Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        }

        private static double RadiansToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        #region Animation Controls
        private void Play_Click(object sender, EventArgs e)
        {
            ((IAgAnimation)root).PlayForward();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            ((IAgAnimation)root).Pause();
        }

        private void Rewind_Click(object sender, EventArgs e)
        {
            ((IAgAnimation)root).Rewind();
        }
        #endregion

        private IAgCrdnProvider aircraftProvider;
        private IAgCrdnProvider facilityProvider;

        private IAgCrdnVector displacementVector;
        private IAgCrdnAngle azimuthAngle;
        private IAgCrdnAngle elevationAngle;

        private double StopTime;

        private AgStkObjectRoot root;
        private IAgStkGraphicsSceneManager manager;
        private IAgStkGraphicsScene scene;

        private const string displacementVectorName = "Range";
        private const string azimuthAngleName = "Azimuth";
        private const string elevationAngleName = "Elevation";

        private void VGTAER_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (root != null)
            {
                root.CloseScenario();
            }

        }
    }
}
