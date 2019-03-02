#region UsingDirectives
using System;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKVgt;
using AGI.STKUtil;
using System.Collections.Generic;
#endregion

namespace GraphicsHowTo.Primitives.Model
{
    class ModelVectorOrientationCodeSnippet : CodeSnippet
    {
        public ModelVectorOrientationCodeSnippet(object epoch)
            : base(@"Primitives\Model\ModelVectorOrientationCodeSnippet.cs")
        {
            m_Epoch = epoch;
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/f-35_jsf_cv.mdl").FullPath;
            Execute(scene, root, modelFile);
        }

        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")] string modelFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            //
            // Get the position and orientation data for the model from the data file
            //
            PositionOrientationProvider provider = new PositionOrientationProvider(
                /*$providerDataFile$The file containing position and orientation data for the model$*/new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/f-35_jsf_cvData.txt").FullPath, root);

            //
            // Create the model for the aircraft
            //
            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                modelFile);
            model.Scale = Math.Pow(10, /*$scale$The scale of the model$*/1.5);
            ((IAgStkGraphicsPrimitive)model).ReferenceFrame = /*$referenceSystem$The system to use as a reference frame$*/root.VgtRoot.WellKnownSystems.Earth.Fixed;
            model.Position = provider.Positions[0];
            IAgOrientation orientation = root.ConversionUtility.NewOrientation();
            orientation.AssignQuaternion(
                (double)provider.Orientations[0].GetValue(0),
                (double)provider.Orientations[0].GetValue(1),
                (double)provider.Orientations[0].GetValue(2),
                (double)provider.Orientations[0].GetValue(3));
            model.Orientation = orientation;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);
#endregion

            m_Model = model;
            m_Provider = provider;
            m_StopTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:07:57.000").Format("epSec"));
            OverlayHelper.AddTextBox(
@"The model's position and orientation are updated in the TimeChanged 
event based on a point and axes evaluator created from a waypoint propagator.", manager);
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
            animationSettings.EnableAnimCycleTime = true;
            animationSettings.AnimCycleTime = m_StopTime;
            animationSettings.AnimCycleType = AgEScEndLoopType.eEndTime;
            animationControl.PlayForward();

            IAgPosition centerPosition = root.ConversionUtility.NewPositionOnEarth();
            centerPosition.AssignPlanetodetic(39.615, -77.205, 3000);

            double x, y, z;
            centerPosition.QueryCartesian(out x, out y, out z);
            Array center = new object[] { x, y, z };
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
            if ((/*$provider$The position and orientation provider$*/m_Provider != null) && (TimeEpSec <= /*$stopTime$The stop time$*/m_StopTime))
            {
                int index = m_Provider.FindIndexOfClosestTime(TimeEpSec, 0, m_Provider.Dates.Count);
                
                //
                // Update model's position and orientation every animation update
                //
                m_Model.Position = m_Provider.Positions[index];
                IAgOrientation orientation = root.ConversionUtility.NewOrientation();
                orientation.AssignQuaternion(
                    (double)m_Provider.Orientations[index].GetValue(0),
                    (double)m_Provider.Orientations[index].GetValue(1),
                    (double)m_Provider.Orientations[index].GetValue(2),
                    (double)m_Provider.Orientations[index].GetValue(3));
                m_Model.Orientation = orientation;
            }
        }
#endregion

        private IAgStkGraphicsModelPrimitive m_Model;
        private PositionOrientationProvider m_Provider = null;
        private object m_Epoch;
        private double m_StopTime;
    };
}
