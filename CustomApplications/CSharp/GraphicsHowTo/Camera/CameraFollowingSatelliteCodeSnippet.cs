#region UsingDirectives

using System;
using System.IO;
using GraphicsHowTo.Primitives;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

#endregion

namespace GraphicsHowTo.Camera
{
    public class CameraFollowingSatelliteCodeSnippet : CodeSnippet, IDisposable
    {
        public CameraFollowingSatelliteCodeSnippet()
            : base(@"Camera\CameraFollowingSatelliteCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgSatellite satellite = CreateSatellite(root);
            string segmentFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/ISS.tle").FullPath;
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/hs601.mdl").FullPath;
            Execute(scene, root, satellite, segmentFile, modelFile);
        }

        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("satellite", "Satellite to follow")] IAgSatellite satellite, [AGI.CodeSnippets.CodeSnippet.Parameter("segmentFilePath", "Path of segment file")] string segmentFilePath, [AGI.CodeSnippets.CodeSnippet.Parameter("modelPath", "Path of model file")] string modelPath)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            //
            // Create the SGP4 Propogator from the TLE
            //
            satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4);
            IAgVePropagatorSGP4 propagator = satellite.Propagator as IAgVePropagatorSGP4;
            propagator.CommonTasks.AddSegsFromFile("25544",
                segmentFilePath);
            propagator.EphemerisInterval.SetImplicitInterval(root.CurrentScenario.Vgt.EventIntervals["AnalysisInterval"]);
            propagator.Propagate();
            double epoch = propagator.Segments[0].Epoch;

            //
            // Get the Vector Geometry Tool provider for the satellite and find its initial position and orientation.
            //
            IAgCrdnProvider provider = ((IAgStkObject)satellite).Vgt;

            IAgCrdnPointLocateInSystemResult positionResult = provider.Points["Center"].LocateInSystem(
                ((IAgScenario)root.CurrentScenario).StartTime, root.VgtRoot.WellKnownSystems.Earth.Inertial);
            IAgCartesian3Vector position = positionResult.Position;

            IAgCrdnAxesFindInAxesResult orientationResult = root.VgtRoot.WellKnownAxes.Earth.Inertial.FindInAxes(
                ((IAgScenario)root.CurrentScenario).StartTime, provider.Axes["Body"]);
            IAgOrientation orientation = orientationResult.Orientation;

            //
            // Create the satellite model
            //
            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                modelPath);
            ((IAgStkGraphicsPrimitive)model).ReferenceFrame = root.VgtRoot.WellKnownSystems.Earth.Inertial;
            model.Position = new object[] { position.X, position.Y, position.Z };
            model.Orientation = orientation;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);

            //
            // Set the time
            //
            ((IAgScenario)root.CurrentScenario).Animation.StartTime = epoch;
#endregion

            //
            // Set the member variables
            //
            m_Satellite = satellite;
            m_Model = model;
            m_ReferenceFrameGraphics = new ReferenceFrameGraphics(root, provider.Systems["Body"], 25);
            m_Provider = provider;
            m_Point = provider.Points["Center"];
            m_Axes = provider.Axes["Body"];

            OverlayHelper.AddTextBox(
            @"The SGP4 propagator is used to propagate a satellite from a TLE.
A model primitive that automatically follows the propagator's 
point is created to visualize the satellite. Camera.ViewOffset 
and Camera.Constrained are used to view the model.", manager);
        }

        private IAgSatellite CreateSatellite(AgStkObjectRoot root)
        {
            IAgSatellite satellite;
            if (root.CurrentScenario.Children.Contains(AgESTKObjectType.eSatellite, objectName))
                satellite = root.GetObjectFromPath("Satellite/" + objectName) as IAgSatellite;
            else
                satellite = root.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, objectName) as IAgSatellite;
            satellite.VO.Pass.TrackData.PassData.Orbit.SetLeadDataType(AgELeadTrailData.eDataNone);
            satellite.VO.Pass.TrackData.PassData.Orbit.SetTrailSameAsLead();
            satellite.Graphics.UseInstNameLabel = false;
            satellite.Graphics.LabelName = String.Empty;
            satellite.VO.Model.OrbitMarker.Visible = false;
            satellite.VO.Model.Visible = false;

            return satellite;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgAnimation animationControl = root as IAgAnimation;
            IAgScAnimation animationSettings = ((IAgScenario)root.CurrentScenario).Animation;
            //
            // Set-up the animation for this specific example
            //
            animationControl.Pause();
            SetAnimationDefaults(root);
            animationSettings.AnimStepValue = 1.0;
            animationSettings.EnableAnimCycleTime = true;
            animationSettings.AnimCycleTime = double.Parse(animationSettings.StartTime.ToString()) + 3600.0;
            animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime;
            animationControl.PlayForward();

            #region CodeSnippet

            //
            // Create the viewer point, which is an offset from near the satellite position 
            // looking towards the satellite.  Set the camera to look from the viewer to the 
            // satellite.
            //
            Array offset = new object[] {50.0, 50.0, -50.0};
            scene.Camera.ViewOffset(m_Axes, m_Point, ref offset);
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisNegativeZ;

            #endregion
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove((IAgStkGraphicsPrimitive)m_Model);
            m_ReferenceFrameGraphics.Dispose();
            OverlayHelper.RemoveTextBox(manager);

            m_Satellite = null;
            m_Provider = null;
            m_ReferenceFrameGraphics = null;
            m_Model = null;
            m_Point = null;
            m_Axes = null;

            root.CurrentScenario.Children.Unload(AgESTKObjectType.eSatellite, objectName);
            scene.Camera.ViewCentralBody("Earth", root.VgtRoot.WellKnownAxes.Earth.Inertial);
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;


            ((IAgAnimation)root).Pause();
            SetAnimationDefaults(root);
            ((IAgAnimation)root).Rewind();

            scene.Render();
        }

        #region CodeSnippet

        public void TimeChanged(AgStkObjectRoot root, double TimeEpSec)
        {
            if (m_Satellite != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                //
                // Get vector pointing from the satellite to the sun.
                //

                IAgCrdnVectorFindInAxesResult result = m_Provider.Vectors["Sun"].FindInAxes(TimeEpSec, m_Axes);
                double xNormalizedIn2D = result.Vector.X / (Math.Sqrt(Math.Pow(result.Vector.X, 2.0) + Math.Pow(result.Vector.Z, 2.0)));
                m_NewAngle = Math.Acos(xNormalizedIn2D);

                //
                // Initialize tracking of angle changes.  To when the angle reaches 180 degrees and starts to
                // fall back down to 0, the panel rotation must use a slightly different calculation.
                //
                if (m_FirstRun)
                {
                    m_StartTime = TimeEpSec;
                    m_OldTime = TimeEpSec;
                    m_OldAngle = m_NewAngle;
                    m_FirstRun = false;
                }

                double TwoPI = Math.PI * 2;
                double HalfPI = Math.PI * .5;

                //
                // Rotates the satellite panels. The panel rotation is reversed when the animation is reversed.
                // Set boolean flag for update to path.
                //
                if (TimeEpSec - m_StartTime >= m_OldTime - m_StartTime)
                {
                    if (m_NewAngle < m_OldAngle)
                    {
                        m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + m_NewAngle) % TwoPI;
                    }
                    else if (m_NewAngle > m_OldAngle)
                    {
                        m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + (TwoPI - m_NewAngle)) % TwoPI;
                    }
                }
                else
                {
                    if (m_NewAngle > m_OldAngle)
                    {
                        m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + m_NewAngle) % TwoPI;
                    }
                    else if (m_NewAngle < m_OldAngle)
                    {
                        m_Model.Articulations.GetByName("SolarArrays").GetByName("Rotate").CurrentValue = (-HalfPI + (TwoPI - m_NewAngle)) % TwoPI;
                    }
                }

                //
                // Sets the old angle to current (new) angle.
                //
                m_OldAngle = m_NewAngle;
                m_OldTime = TimeEpSec;

                //
                // Update the position and orientation of the model 
                //
                IAgCrdnPointLocateInSystemResult positionResult = m_Point.LocateInSystem(
                    TimeEpSec, root.VgtRoot.WellKnownSystems.Earth.Inertial);
                IAgCrdnAxesFindInAxesResult orientationResult = root.VgtRoot.WellKnownAxes.Earth.Inertial.FindInAxes(
                    TimeEpSec, m_Axes);

                m_Model.Position = new object[] {positionResult.Position.X, positionResult.Position.Y, positionResult.Position.Z};
                m_Model.Orientation = orientationResult.Orientation;
            }
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CameraFollowingSatelliteCodeSnippet()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_ReferenceFrameGraphics != null)
                {
                    m_ReferenceFrameGraphics.Dispose();
                    m_ReferenceFrameGraphics = null;
                }
            }
        }

        private IAgSatellite m_Satellite;
        private IAgStkGraphicsModelPrimitive m_Model;
        private ReferenceFrameGraphics m_ReferenceFrameGraphics;
        private IAgCrdnProvider m_Provider;
        private IAgCrdnPoint m_Point;
        private IAgCrdnAxes m_Axes;

        private double m_NewAngle;
        private double m_OldAngle;
        private bool m_FirstRun = true;

        private double m_OldTime;
        private double m_StartTime;

        private const string objectName = "CameraFollowingSatellite";
    };
}
