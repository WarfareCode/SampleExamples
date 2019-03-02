#region UsingDirectives
using System;
using System.IO;
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKVgt;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives
{
    class PathTrailLineCodeSnippet : CodeSnippet
    {
        public PathTrailLineCodeSnippet()
            : base(@"Primitives\Path\PathTrailLineCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string segmentFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/ISS.tle").FullPath;
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/hs601.mdl").FullPath;
            IAgSatellite sat = CreateSatellite(root);
            Execute(scene, root, segmentFile, modelFile, sat);
        }

        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("segmentFile", "The segment file")] string segmentFile, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")] string modelFile, [AGI.CodeSnippets.CodeSnippet.Parameter("satellite", "A Satellite")] IAgSatellite satellite)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            //
            // Create the SGP4 Propogator from the TLE
            //
            satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorSGP4);
            IAgVePropagatorSGP4 propagator = satellite.Propagator as IAgVePropagatorSGP4;
            propagator.CommonTasks.AddSegsFromFile("25544",
                segmentFile);
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
                modelFile);
            ((IAgStkGraphicsPrimitive)model).ReferenceFrame = root.VgtRoot.WellKnownSystems.Earth.Inertial;
            model.Position = new object[] { position.X, position.Y, position.Z };
            model.Orientation = orientation;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);

            //
            // Create the path primitive
            //
            IAgStkGraphicsPathPrimitive path = manager.Initializers.PathPrimitive.Initialize();
            ((IAgStkGraphicsPrimitive)path).ReferenceFrame = root.VgtRoot.WellKnownSystems.Earth.Inertial;
            path.UpdatePolicy = manager.Initializers.DurationPathPrimitiveUpdatePolicy.InitializeWithParameters(
                60, AgEStkGraphicsPathPrimitiveRemoveLocation.eStkGraphicsRemoveLocationFront) as IAgStkGraphicsPathPrimitiveUpdatePolicy;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)path);

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
            m_Path = path;
            m_Provider = provider;
            m_Point = provider.Points["Center"];
            m_Axes = provider.Axes["Body"];
            m_Root = root;

            root.OnAnimUpdate += TimeChanged;
            OverlayHelper.AddTextBox("Points are added to the path in the TimeChanged event.\n" +
                "The DurationPathPrimitiveUpdatePolicy will remove points after the given duration.", manager);
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

            //
            // Create the viewer point, which is an offset from near the model position looking
            // towards the model.  Set the camera to look from the viewer to the model.
            //
            Array offset = new object[] { 50.0, 50.0, -50.0 };
            scene.Camera.ViewOffset(m_Axes, m_Point, ref offset);
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisNegativeZ;
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove((IAgStkGraphicsPrimitive)m_Model);
            manager.Primitives.Remove((IAgStkGraphicsPrimitive)m_Path);
            root.CurrentScenario.Children.Unload(AgESTKObjectType.eSatellite, objectName);
            root.OnAnimUpdate -= TimeChanged;
            OverlayHelper.RemoveTextBox(manager);

            m_Satellite = null;
            m_Provider = null;
            m_Path = null;
            m_Model = null;
            m_Point = null;
            m_Axes = null;

            scene.Camera.ViewCentralBody("Earth", root.VgtRoot.WellKnownAxes.Earth.Inertial);
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;


            ((IAgAnimation)root).Pause();
            SetAnimationDefaults(root);
            ((IAgAnimation)root).Rewind();

            scene.Render();
        }

#region CodeSnippet
        private void TimeChanged(double TimeEpSec)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
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

                if (!m_AnimateForward)
                {
                    m_AnimateForward = true;
                    m_AnimateDirectionChanged = true;
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

                if (m_AnimateForward)
                {
                    m_AnimateForward = false;
                    m_AnimateDirectionChanged = true;
                }
            }

            if (m_AnimateDirectionChanged)
            {
                m_Path.Clear();
                m_AnimateDirectionChanged = false;
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
                TimeEpSec, m_Root.VgtRoot.WellKnownSystems.Earth.Inertial);
            IAgCrdnAxesFindInAxesResult orientationResult = m_Root.VgtRoot.WellKnownAxes.Earth.Inertial.FindInAxes(
                TimeEpSec, m_Axes);

            Array positionPathPoint = new object[] { positionResult.Position.X, positionResult.Position.Y, positionResult.Position.Z };

            m_Model.Position = positionPathPoint;
            m_Model.Orientation = orientationResult.Orientation;

            m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDatePositionAndColor(
                m_Root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), ref positionPathPoint, Color.LightGreen));
        }
#endregion

        private IAgSatellite m_Satellite;
        private IAgStkGraphicsPathPrimitive m_Path;
        private IAgStkGraphicsModelPrimitive m_Model;
        private IAgCrdnProvider m_Provider;
        private IAgCrdnPoint m_Point;
        private IAgCrdnAxes m_Axes;
        private AgStkObjectRoot m_Root;

        private double m_NewAngle;
        private double m_OldAngle;
        private bool m_FirstRun = true;

        private double m_OldTime;
        private double m_StartTime;

        private bool m_AnimateForward = true;
        private bool m_AnimateDirectionChanged = false;

        private const string objectName = "PathTrailLineSatellite";
    };
}
