using System.Collections.Generic;
using System.Windows.Forms;
#region UsingDirectives
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.Camera
{
    class CameraFollowingSplineCodeSnippet : CodeSnippet
    {
        public CameraFollowingSplineCodeSnippet()
            : base(@"Camera\CameraFollowingSplineCodeSnippet.cs")
        {
        }

        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
            #region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            //
            // Create a camera transition from Washington D.C. to New Orleans
            //
            Array startPosition = new object[3] {/*$startLat$Start latitude$*/38.85, /*$startLong$Start longitude$*/-77.04, /*$startAlt$Start altitude$*/0.0 };
            Array endPosition = new object[3] {/*$endLat$End latitude$*/29.98, /*$endLong$End longitude$*/-90.25, /*$endAlt$End altitude$*/0.0 };
            CatmullRomSpline spline = new CatmullRomSpline(root, /*$planetName$Name of the planet to place the start and end points$*/"Earth", startPosition, endPosition, /*$transitionAlt$Max altitude of the transition$*/2000000);
            #endregion

            m_Spline = spline;

            m_PointBatch = CreatePointBatch(startPosition, endPosition, manager) as IAgStkGraphicsPrimitive;
            m_TextBatch = CreateTextBatch("Washington D.C.", "New Orleans", startPosition, endPosition, manager) as IAgStkGraphicsPrimitive;

            manager.Primitives.Add(m_PointBatch);
            manager.Primitives.Add(m_TextBatch);

            OverlayHelper.AddTextBox(
@"A Catmull-Rom spline is used to smoothly zoom from one 
location to another, over a given number of seconds, and 
reaching a specified maximum altitude.

You can use this technique in your applications by including 
the CatmullRomSpline and CameraUpdater classes from the HowTo.", manager);
            //m_DebugPointBatch = CreateDebugPoints(root) as IAgStkGraphicsPrimitive;
            //manager.Primitives.Add(m_DebugPointBatch);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_CameraUpdater == null || !m_CameraUpdater.IsRunning())
            {
                CatmullRomSpline spline = m_Spline;

                #region CodeSnippet
                CameraUpdater cameraUpdater = new CameraUpdater(scene, root, spline.InterpolatorPoints, 6);
                #endregion

                m_CameraUpdater = cameraUpdater;
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            m_CameraUpdater.Dispose();

            m_Spline = null;
            m_CameraUpdater = null;

            manager.Primitives.Remove(m_PointBatch);
            manager.Primitives.Remove(m_TextBatch);
            OverlayHelper.RemoveTextBox(manager);

            if (m_DebugPointBatch != null)
            {
                manager.Primitives.Remove(m_DebugPointBatch);
                m_DebugPointBatch = null;
            }

            m_PointBatch = null;
            m_TextBatch = null;

            scene.Render();
        }

        //
        // Creates points for the interpolator curve (for debugging purposes)
        //
        private IAgStkGraphicsPointBatchPrimitive CreateDebugPoints(AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IList<Array> positions = new List<Array>();
            foreach (Array c in m_Spline.InterpolatorPoints)
            {
                positions.Add(CatmullRomSpline.CartesianToCartographic(c, "Earth", root));
            }
            Array positionsArray = ConvertIListToArray(positions);

            Array colorsArray = Array.CreateInstance(typeof(object), positionsArray.Length / 3);
            for (int i = 0; i < (positionsArray.Length / 3); ++i)
                colorsArray.SetValue((uint)Color.Blue.ToArgb(), i);

            IAgStkGraphicsPointBatchPrimitive debugPointBatch = manager.Initializers.PointBatchPrimitive.Initialize();
            debugPointBatch.SetCartographicWithColorsAndRenderPass("Earth", ref positionsArray, ref colorsArray, AgEStkGraphicsRenderPassHint.eStkGraphicsRenderPassHintOpaque);
            debugPointBatch.PixelSize = 8;

            return debugPointBatch;
        }

        //
        // Creates the points for the two cities
        //
        private static IAgStkGraphicsPointBatchPrimitive CreatePointBatch(Array start, Array end, IAgStkGraphicsSceneManager manager)
        {
            Array positionsArray = ConvertIListToArray(start, end);

            Array colors = new object[]
            {
                (uint)Color.Red.ToArgb(),
                (uint)Color.Red.ToArgb()
            };

            IAgStkGraphicsPointBatchPrimitive pointBatch = manager.Initializers.PointBatchPrimitive.Initialize();
            pointBatch.SetCartographicWithColorsAndRenderPass("Earth", ref positionsArray, ref colors, AgEStkGraphicsRenderPassHint.eStkGraphicsRenderPassHintOpaque);
            pointBatch.PixelSize = 8;

            return pointBatch;
        }

        //
        // Creates the text for the two cities
        //
        private static IAgStkGraphicsTextBatchPrimitive CreateTextBatch(string startName, string endName, Array start, Array end, IAgStkGraphicsSceneManager manager)
        {
            Array text = new object[]
            {
                startName,
                endName
            };

            Array positionsArray = ConvertIListToArray(start, end);

            IAgStkGraphicsTextBatchPrimitiveOptionalParameters parameters = manager.Initializers.TextBatchPrimitiveOptionalParameters.Initialize();

            Array pixelOffset = new object[] { 3, 3 };
            
            parameters.PixelOffset = pixelOffset;

            IAgStkGraphicsGraphicsFont font = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline("Arial", 12, AgEStkGraphicsFontStyle.eStkGraphicsFontStyleRegular, true);
            IAgStkGraphicsTextBatchPrimitive textBatch = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font);
            ((IAgStkGraphicsPrimitive)textBatch).Color = Color.White;
            textBatch.OutlineColor = Color.Red;
            textBatch.SetCartographicWithOptionalParameters("Earth", ref positionsArray, ref text, parameters);

            return textBatch;
        }

        //
        // Member variables
        //
        private CatmullRomSpline m_Spline;
        private CameraUpdater m_CameraUpdater;

        IAgStkGraphicsPrimitive m_DebugPointBatch;
        IAgStkGraphicsPrimitive m_PointBatch;
        IAgStkGraphicsPrimitive m_TextBatch;
    };
}
