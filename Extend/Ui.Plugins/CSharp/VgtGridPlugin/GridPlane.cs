using System;
using AGI.STKGraphics;

namespace AGI.Grid
{
    abstract class GridPlane
    {
        public bool Display { get; set; }
        public System.Drawing.Color Color { get; set; }
        public double Size 
        {
            get { return m_Size; }
            set
            {
                if (value > 0.0)
                    m_Size = value;
                else
                    throw new Exception("The size must be a positive value.");
            }
        }

        public double Spacing 
        {
            get { return m_Spacing; }
            set
            {
                if (value > 0.0 && value <= Size)
                    m_Spacing = value;
                else
                    throw new Exception("The spacing must be positive and less than or equal to the size.");
            }
        }

        public float LineWidth { get; set; }

        public float Translucency 
        {
            get { return m_Translucency; }
            set
            {
                if (value >= 0.0 && value <= 100.0)
                    m_Translucency = value;
                else
                    throw new Exception("The translucency must be between 0 and 100.");
            }
        }

        /// <summary>
        /// Generates an Array of points that can be used to represent the grid as a polyline primitive.
        /// </summary>
        protected abstract Array GenerateGridPoints();

        /// <summary>
        /// Creates a polyline primitive of the GridPlane from the currently configured grid settings.
        /// </summary>
        public IAgStkGraphicsPrimitive GetPrimitive(AGI.STKVgt.IAgCrdnSystem ReferenceFrame, IAgStkGraphicsSceneManager manager)
        {
            IAgStkGraphicsPolylinePrimitive grid = manager.Initializers.PolylinePrimitive.InitializeWithType(AgEStkGraphicsPolylineType.eStkGraphicsPolylineTypeLines);

            Array positions = this.GenerateGridPoints();
            grid.Set(ref positions);

            ((IAgStkGraphicsPrimitive)grid).ReferenceFrame = ReferenceFrame;
            ((IAgStkGraphicsPrimitive)grid).Color = this.Color;
            ((IAgStkGraphicsPrimitive)grid).Translucency = this.Translucency;
            ((IAgStkGraphicsPrimitive)grid).Display = this.Display;
            grid.Width = this.LineWidth;


            return (IAgStkGraphicsPrimitive)grid;
        }

        private double m_Size;
        private double m_Spacing;
        private float m_Translucency;
    }
}
