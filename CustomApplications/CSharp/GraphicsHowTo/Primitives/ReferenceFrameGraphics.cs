using System;
using System.Drawing;
using AGI.STKVgt;
using AGI.STKObjects;
using AGI.STKGraphics;

namespace GraphicsHowTo.Primitives
{
    /// <summary>
    /// Visualization for a reference frame.  Given a ReferenceFrame, this class
    /// creates a polyline and text batch primitive to visualize the reference
    /// frame's axes.
    /// </summary>
    class ReferenceFrameGraphics : IDisposable
    {
        public ReferenceFrameGraphics(AgStkObjectRoot root, IAgCrdnSystem referenceFrame, double axesLength)
            : this(
                root,
                referenceFrame,
                axesLength,
                Color.Red)
        {
        }

        public ReferenceFrameGraphics(
            AgStkObjectRoot root,
            IAgCrdnSystem referenceFrame,
            double axesLength,
            Color color)
            : this(
                root,
                referenceFrame,
                axesLength,
                color,
                Color.White)
        {
        }

        public ReferenceFrameGraphics(
            AgStkObjectRoot root,
            IAgCrdnSystem referenceFrame,
            double axesLength,
            Color color,
            Color outlineColor)
            : this(
                root,
                referenceFrame,
                axesLength,
                color,
                outlineColor,
                ((IAgScenario)root.CurrentScenario).SceneManager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline("MS Sans Serif", 24, AgEStkGraphicsFontStyle.eStkGraphicsFontStyleRegular, true))
        {
        }

        public ReferenceFrameGraphics(
            AgStkObjectRoot root,
            IAgCrdnSystem referenceFrame,
            double axesLength,
            Color color,
            Color outlineColor,
            IAgStkGraphicsGraphicsFont font)
        {
            manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            uint colorAsUint = (uint)color.ToArgb();

            m_Lines = manager.Initializers.PolylinePrimitive.InitializeWithType(AgEStkGraphicsPolylineType.eStkGraphicsPolylineTypeLines);
            Array lines = new object[]
                {
                    0, 0, 0, /* to */ axesLength, 0, 0,
                    0, 0, 0, /* to */ 0, axesLength, 0,
                    0, 0, 0, /* to */ 0, 0, axesLength
                };
            Array lineColors = new object[]
                {
                    colorAsUint, colorAsUint,
                    colorAsUint, colorAsUint,
                    colorAsUint, colorAsUint,
                };
            m_Lines.SetWithColors(ref lines, ref lineColors);

            ((IAgStkGraphicsPrimitive)m_Lines).ReferenceFrame = referenceFrame;
            m_Lines.Width = 2;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)m_Lines);

            m_Text = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font);
            IAgStkGraphicsTextBatchPrimitiveOptionalParameters optionalParameters = manager.Initializers.TextBatchPrimitiveOptionalParameters.Initialize();
            Array textColors = new object[]
                {
                    colorAsUint,
                    colorAsUint,
                    colorAsUint
                };
            optionalParameters.SetColors(ref textColors);

            Array textPositions = new object[]
                {
                    axesLength, 0, 0,
                    0, axesLength, 0,
                    0, 0, axesLength
                };
            Array text = new object[]
                {
                    "+X",
                    "+Y",
                    "+Z",
                };

            m_Text.SetWithOptionalParameters(ref textPositions, ref text, optionalParameters);

            m_Text.OutlineColor = outlineColor;
            ((IAgStkGraphicsPrimitive)m_Text).ReferenceFrame = referenceFrame;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)m_Text);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ReferenceFrameGraphics()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_Text != null)
                {
                    manager.Primitives.Remove((IAgStkGraphicsPrimitive)m_Text);
                    //m_Text.Dispose();
                    m_Text = null;
                }
                if (m_Lines != null)
                {
                    manager.Primitives.Remove((IAgStkGraphicsPrimitive)m_Lines);
                    //m_Lines.Dispose();
                    m_Lines = null;
                }
            }
        }

        private IAgStkGraphicsPolylinePrimitive m_Lines;
        private IAgStkGraphicsTextBatchPrimitive m_Text;
        private IAgStkGraphicsSceneManager manager;
    }
}
