#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
#endregion

namespace GraphicsHowTo.Primitives.Model
{
    class ModelOrientationCodeSnippet : CodeSnippet, IDisposable
    {
        public ModelOrientationCodeSnippet()
            : base(@"Primitives\Model\ModelOrientationCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string modelFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath;
            IAgPosition origin = CreatePosition(root, 39.88, -75.25, 0);
            IAgCrdnAxesFixed axes = CreateAxes(root, "Earth", origin);
            IAgCrdnSystem referenceFrame = CreateSystem(root, "Earth", origin, axes);
            Execute(scene, root, modelFile, referenceFrame);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "OrientAModel",
            /* Description */ "Orient a model",
            /* Category    */ "Graphics | Primitives | Model Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsModelPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "The model file")] string modelFile, [AGI.CodeSnippets.CodeSnippet.Parameter("referenceFrame", "A reference frame for the model")] IAgCrdnSystem referenceFrame)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                modelFile);
            ((IAgStkGraphicsPrimitive)model).ReferenceFrame = referenceFrame;  // Model is oriented using east-north-up axes.  Use model.Orientation for addition rotation.
            Array zero = new object[] { 0, 0, 0 }; // Origin of reference frame
            model.Position = zero;        
            model.Scale = Math.Pow(10, /*$scale$The scale of the model$*/1.5);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);
#endregion

            OverlayHelper.AddTextBox(
                "To orient a model, use its ReferenceFrame property. \r\n" +
                "In this example, AxesEastNorthUp is used. X points east, \r\n" +
                "Y points north, and Z points along the detic surface normal \r\n" +
                "(e.g. \"up\"). Like all STK .mdl facility models, this model\r\n" +
                "was authored such that Z points up so it is oriented \r\n" +
                "correctly using AxesEastNorthUp.", manager);
            m_Primitive = (IAgStkGraphicsPrimitive)model;

            m_ReferenceFrameGraphics = new ReferenceFrameGraphics(root, referenceFrame, s_AxesLength);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgPosition position = root.ConversionUtility.NewPositionOnEarth();
            position.AssignPlanetodetic(39.88, -75.25, 0);

            double x, y, z;
            position.QueryCartesian(out x, out y, out z);
            Array center = new object[] { x, y, z };
            IAgStkGraphicsBoundingSphere boundingSphere = manager.Initializers.BoundingSphere.Initialize(ref center, s_AxesLength);

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere,
                20, 15);

            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            manager.Primitives.Remove(m_Primitive);
            OverlayHelper.RemoveTextBox(manager);
            m_ReferenceFrameGraphics.Dispose();
            scene.Render();

            m_Primitive = null;
            m_ReferenceFrameGraphics = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ModelOrientationCodeSnippet()
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

        private IAgStkGraphicsPrimitive m_Primitive;
        private ReferenceFrameGraphics m_ReferenceFrameGraphics;
        private const double s_AxesLength = 2000;

    };
}
