#region UsingDirectives
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using GraphicsHowTo;
using AGI.STKObjects;
using AGI.STKGraphics;
using AGI.STKUtil;
using AGI.STKVgt;

#endregion

namespace GraphicsHowTo.Primitives.Composite
{
    public class CompositeLayersCodeSnippet : CodeSnippet
    {
        public CompositeLayersCodeSnippet()
            : base(@"Primitives\Composite\CompositeLayerCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath;
            string markerFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/facility.png").FullPath;
            Execute(scene, root, modelFile, markerFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "CreateALayerOfPrimitives",
            /* Description */ "Create layers of primitives",
            /* Category    */ "Graphics | Primitives | Composite Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsCompositePrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "Location of the model file")] string modelFile, [AGI.CodeSnippets.CodeSnippet.Parameter("markerFile", "The file to use for the marker batch")] string markerFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            Random r = new Random();
            const int modelCount = /*$numberOfModels$The number of models to create$*/25;

            Array positions = Array.CreateInstance(typeof(object), modelCount * 3);

            //
            // Create the models
            //
            IAgStkGraphicsCompositePrimitive models = manager.Initializers.CompositePrimitive.Initialize();

            for (int i = 0; i < modelCount; ++i)
            {
                double latitude = /*$centralLat$A central latitude that the models will be positioned close to$*/35 + 1.5 * r.NextDouble();
                double longitude = -(/*$centralLon$A central longitude that the models will be positioned close to$*/80 + 1.5 * r.NextDouble());
                double altitude = /*$alt$The altitude of the models$*/0;
                Array position = new object[3]{latitude, longitude, altitude};

                positions.SetValue(latitude, 3 * i);
                positions.SetValue(longitude, (3 * i) + 1);
                positions.SetValue(altitude, (3 * i) + 2);

                IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(modelFile);
                model.SetPositionCartographic("Earth", ref position);
                model.Scale = Math.Pow(10, 2);
                models.Add((IAgStkGraphicsPrimitive)model);
            }

            //
            // Create the markers
            //
            IAgStkGraphicsMarkerBatchPrimitive markers = manager.Initializers.MarkerBatchPrimitive.Initialize();
            markers.RenderPass = /*$renderPass$The pass during which the marker batch is rendered$*/AgEStkGraphicsMarkerBatchRenderPass.eStkGraphicsMarkerBatchRenderPassTranslucent;

            markers.Texture = manager.Textures.LoadFromStringUri(markerFile);
            markers.SetCartographic(/*$planetName$The planet to place the markers on$*/"Earth", ref positions);

            //
            // Create the points
            //
            IAgStkGraphicsPointBatchPrimitive points = manager.Initializers.PointBatchPrimitive.Initialize();
            points.PixelSize = /*$pointSize$The size of the points in pixels$*/5;
            Array colors = Array.CreateInstance(typeof(object), modelCount);
            for (int i = 0; i < colors.Length; i++)
                colors.SetValue(/*$pointColor$The System.Drawing.Color of the points$*/Color.Orange.ToArgb(), i);
            points.SetCartographicWithColorsAndRenderPass(/*$planetName$The planet to place the markers on$*/"Earth", ref positions, ref colors, /*$renderHint$For efficiency, how the points will be rendered$*/AgEStkGraphicsRenderPassHint.eStkGraphicsRenderPassHintOpaque);

            //
            // Set the display Conditions
            //
            IAgStkGraphicsAltitudeDisplayCondition near = manager.Initializers.AltitudeDisplayCondition.InitializeWithAltitudes(/*$modelMinAlt$Minimum altitude at which the models will be displayed$*/0, /*$modelMaxAlt$Maximum altitude at which the models will be displayed$*/500000);
            ((IAgStkGraphicsPrimitive)models).DisplayCondition = (IAgStkGraphicsDisplayCondition)near;

            IAgStkGraphicsAltitudeDisplayCondition medium = manager.Initializers.AltitudeDisplayCondition.InitializeWithAltitudes(/*$markerMinAlt$Minimum altitude at which the markers will be displayed$*/500000, /*$markerMaxAlt$Maximum altitude at which the models will be displayed$*/2000000);
            ((IAgStkGraphicsPrimitive)markers).DisplayCondition = (IAgStkGraphicsDisplayCondition)medium;

            IAgStkGraphicsAltitudeDisplayCondition far = manager.Initializers.AltitudeDisplayCondition.InitializeWithAltitudes(/*$pointMinAlt$Minimum altitude at which the points will be displayed$*/2000000, /*$pointMaxAlt$Maximum altitude at which the points will be displayed$*/4000000);
            ((IAgStkGraphicsPrimitive)points).DisplayCondition = (IAgStkGraphicsDisplayCondition)far;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)models);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)markers);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)points);
#endregion

            OverlayHelper.AddTextBox(
                @"Zoom in and out to see layers of primitives based on altitude.
Models are shown when zoomed in closest. As you zoom out, 
models switch to markers, then to points.

This level of detail technique is implemented by adding each
ModelPrimitive to a CompositePrimitive. A different
AltitudeDisplayCondition is assigned to the composite, 
a marker batch, and a point batch.", manager);

            OverlayHelper.AddAltitudeOverlay(scene, manager);
            m_Intervals = new List<Interval>();

            m_Intervals.Add(new Interval(0, 500000));
            m_Intervals.Add(new Interval(500000, 2000000));
            m_Intervals.Add(new Interval(2000000, 4000000));

            OverlayHelper.AltitudeDisplay.AddIntervals(m_Intervals);

            m_Models = (IAgStkGraphicsPrimitive)models;
            m_Markers = markers;
            m_Points = points;
        }

        private static IAgStkGraphicsPrimitive CreateModel(Array position, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgPosition origin = root.ConversionUtility.NewPositionOnEarth();
            origin.AssignPlanetodetic((double)position.GetValue(0), (double)position.GetValue(1), (double)position.GetValue(2));
            IAgCrdnAxesFixed axes = CreateAxes(root, "Earth", origin);
            IAgCrdnSystem system = CreateSystem(root, "Earth", origin, axes);
            IAgCrdnAxesFindInAxesResult result = root.VgtRoot.WellKnownAxes.Earth.Fixed.FindInAxes(((IAgScenario)root.CurrentScenario).Epoch, ((IAgCrdnAxes)axes));

            string modelPath = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath;

            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(modelPath);
            model.SetPositionCartographic("Earth", ref position);
            model.Orientation = result.Orientation;
            model.Scale = Math.Pow(10, 2);

            return (IAgStkGraphicsPrimitive)model;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Models != null)
            {
                ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Models.BoundingSphere);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            if (m_Models != null)
            {
                manager.Primitives.Remove(m_Models);
                manager.Primitives.Remove((IAgStkGraphicsPrimitive)m_Markers);
                manager.Primitives.Remove((IAgStkGraphicsPrimitive)m_Points);

                OverlayHelper.RemoveTextBox(manager);
                OverlayHelper.AltitudeDisplay.RemoveIntervals(m_Intervals);
                OverlayHelper.RemoveAltitudeOverlay(manager);
                scene.Render();

                m_Models = null;
                m_Markers = null;
                m_Points = null;
            }
        }

        private IAgStkGraphicsPrimitive m_Models;
        private IAgStkGraphicsMarkerBatchPrimitive m_Markers;
        private IAgStkGraphicsPointBatchPrimitive m_Points;
        private List<Interval> m_Intervals;
    }
}
