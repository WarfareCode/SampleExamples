#region UsingDirectives
using System;
using System.Collections.Generic;
using System.IO;
using AGI.STKUtil;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKVgt;
#endregion

namespace GraphicsHowTo.Primitives
{
    class PathDropLineCodeSnippet : CodeSnippet
    {
        public PathDropLineCodeSnippet(object epoch)
            : base(@"Primitives\Path\PathDropLineCodeSnippet.cs")
        {
            m_Epoch = epoch;
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string providerDataFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/f-35_jsf_cvData.txt").FullPath;
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/f-35_jsf_cv.mdl").FullPath;
            Execute(scene, root, providerDataFile, modelFile);
        }

        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("providerDataFile", "The file containing position and orientation data for the model")] string providerDataFile, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")] string modelFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            //
            // Get the position and orientation data for the model from the data file
            //
            PositionOrientationProvider provider = new PositionOrientationProvider(
                providerDataFile, root);

            //
            // Create the model for the aircraft
            //
            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                modelFile);
            model.Scale = Math.Pow(10, /*$scale$The scale of the model$*/1.5);
            model.Position = provider.Positions[0];
            IAgOrientation orientation = root.ConversionUtility.NewOrientation();
            orientation.AssignQuaternion(
                (double)provider.Orientations[0].GetValue(0),
                (double)provider.Orientations[0].GetValue(1),
                (double)provider.Orientations[0].GetValue(2),
                (double)provider.Orientations[0].GetValue(3));
            model.Orientation = orientation;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);

            //
            // Create the path primitive
            //
            IAgStkGraphicsPathPrimitive path = manager.Initializers.PathPrimitive.Initialize();
            path.PolylineType = /*$polylineType$The type of the polyline$*/AgEStkGraphicsPolylineType.eStkGraphicsPolylineTypeLines;
            path.UpdatePolicy = manager.Initializers.DurationPathPrimitiveUpdatePolicy.InitializeWithParameters(
                /*$duration$The amount of time that a location will be on the path$*/120, /*$removeLocation$The location to remove expired points from$*/AgEStkGraphicsPathPrimitiveRemoveLocation.eStkGraphicsRemoveLocationFront) as IAgStkGraphicsPathPrimitiveUpdatePolicy;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)path);
#endregion

            m_Provider = provider;
            m_Path = path;
            m_Model = model;
            m_PreviousPosition = model.Position;
            m_PreviousDrop = double.MinValue;
            m_StartTime = double.MinValue;
            m_StopTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:07:57.000").Format("epSec"));

            OverlayHelper.AddTextBox(
                @"Drop lines are added to the trail line of a model on a given interval.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgAnimation animationControl = root as IAgAnimation;
            IAgScAnimation animationSettings = ((IAgScenario)root.CurrentScenario).Animation;
            //
            // Set-up the animation for this specific example
            //
            animationControl.Pause();
            SetAnimationDefaults(root);
            animationSettings.AnimStepValue = 1.0;
            animationSettings.StartTime = m_Epoch;
            animationControl.PlayForward();

            IAgPosition centerPosition = root.ConversionUtility.NewPositionOnEarth();
            centerPosition.AssignPlanetodetic(39.615, -77.205, 3000);
            double x, y, z;
            centerPosition.QueryCartesian(out x, out y, out z);
            Array center = new object[] { x, y, z};
            IAgStkGraphicsBoundingSphere boundingSphere = manager.Initializers.BoundingSphere.Initialize(ref center, 1500);

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere,
                0, 15);

            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove((IAgStkGraphicsPrimitive)m_Model);

            m_Model = null;
            m_Provider = null;

            OverlayHelper.RemoveTextBox(manager);
            scene.Render();
        }

#region CodeSnippet
        internal void TimeChanged(AgStkObjectRoot root, double TimeEpSec)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            //
            // Record the animation start time.
            //
            if (m_StartTime == double.MinValue)
            {
                m_StartTime = TimeEpSec;
            }

            if ((/*$provider$The position and orientation provider$*/m_Provider != null) && (TimeEpSec <= /*$stopTime$The stop time$*/m_StopTime))
            {
                int index = m_Provider.FindIndexOfClosestTime(TimeEpSec, 0, m_Provider.Dates.Count);

                //
                // If the animation was restarted, the path must be cleared
                // and record of previous drop line and position must be reset.
                //
                if (TimeEpSec == m_StartTime)
                {
                    m_Path.Clear();
                    m_PreviousPosition = m_Provider.Positions[index];
                    m_PreviousDrop = TimeEpSec;
                }
                Array positionPathPoint = m_Provider.Positions[index];
                //
                // Update model's position and orientation every animation update
                //
                m_Model.Position = positionPathPoint;
                IAgOrientation orientation = root.ConversionUtility.NewOrientation();
                orientation.AssignQuaternion(
                    (double)m_Provider.Orientations[index].GetValue(0),
                    (double)m_Provider.Orientations[index].GetValue(1),
                    (double)m_Provider.Orientations[index].GetValue(2),
                    (double)m_Provider.Orientations[index].GetValue(3));
                m_Model.Orientation = orientation;

                //
                // Update path with model's new position and check
                // to add drop line at every animation update
                //
                m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDateAndPosition(
                    root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), ref m_PreviousPosition));
                m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDateAndPosition(
                    root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), ref positionPathPoint));                    

                m_PreviousPosition = positionPathPoint;

                // Add drop line
                if (Math.Abs(TimeEpSec - m_PreviousDrop) > 10)
                {
                    IAgPosition endpointPosition = root.ConversionUtility.NewPositionOnEarth();
                    endpointPosition.AssignCartesian((double)m_Model.Position.GetValue(0), (double)m_Model.Position.GetValue(1), (double)m_Model.Position.GetValue(2));
                    
                    object endpointLat, endpointLong;
                    double endpointAlt;
                    double  x, y, z;
                    endpointPosition.QueryPlanetodetic(out endpointLat, out endpointLong, out endpointAlt);

                    endpointPosition.AssignPlanetodetic(endpointLat, endpointLong, 0);

                    endpointPosition.QueryCartesian(out x, out y, out z);
                    Array endpoint = new object[] {x,y,z};

                    m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDateAndPosition(
                        root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), ref positionPathPoint));
                    m_Path.AddBack(manager.Initializers.PathPoint.InitializeWithDateAndPosition(
                        root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString()), ref endpoint));

                    m_PreviousDrop = TimeEpSec;
                }
            }
        }
#endregion

        private PositionOrientationProvider m_Provider = null;
        private object m_Epoch;
        private double m_StartTime;
        private double m_StopTime;
        private IAgStkGraphicsPathPrimitive m_Path;
        private IAgStkGraphicsModelPrimitive m_Model;
        private Array m_PreviousPosition;
        private double m_PreviousDrop;
    }
}
