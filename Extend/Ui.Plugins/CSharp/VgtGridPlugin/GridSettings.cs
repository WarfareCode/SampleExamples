using System.Drawing;
using AGI.STKGraphics;

namespace Agi.Ui.Plugins.CSharp.VgtGridPlugin
{
    class GridSettings
    {
        public GridSettings()
        {
            // Default Values

            this.UseBodySystem = true;
            this.UseCustomSystem = false;
            this.CustomOriginName = string.Empty;
            this.CustomAxesName = string.Empty;

            this.XYVisible = true;
            this.XYColor = Color.Red;
            this.XYSize = 100.0;
            this.XYSpacing = 2.0;
            this.XYLineWidth = 2.0f;
            this.XYTranslucency = 20.0f;

            this.XZVisible = true;
            this.XZColor = Color.Yellow;
            this.XZSize = 100.0;
            this.XZSpacing = 2.0;
            this.XZLineWidth = 2.0f;
            this.XZTranslucency = 20.0f;

            this.YZVisible = true;
            this.YZColor = Color.Blue;
            this.YZSize = 100.0;
            this.YZSpacing = 2.0;
            this.YZLineWidth = 2.0f;
            this.YZTranslucency = 20.0f;

            this.GridPrimitive = null;
        }

        public GridSettings Copy()
        {
            GridSettings copy = (GridSettings)this.MemberwiseClone();
            return copy;
        }

        public bool UseBodySystem { get; set; }
        public bool UseCustomSystem { get; set; }
        public string CustomOriginName { get; set; }
        public string CustomAxesName { get; set; }

        public bool XYVisible { get; set; }
        public Color XYColor { get; set; }
        public double XYSize { get; set; }
        public double XYSpacing { get; set; }
        public float XYLineWidth { get; set; }
        public float XYTranslucency { get; set; }

        public bool XZVisible { get; set; }
        public Color XZColor { get; set; }
        public double XZSize { get; set; }
        public double XZSpacing { get; set; }
        public float XZLineWidth { get; set; }
        public float XZTranslucency { get; set; }

        public bool YZVisible { get; set; }
        public Color YZColor { get; set; }
        public double YZSize { get; set; }
        public double YZSpacing { get; set; }
        public float YZLineWidth { get; set; }
        public float YZTranslucency { get; set; }

        public IAgStkGraphicsPrimitive GridPrimitive { get; set; }

        public enum Planes { XY, XZ, YZ };
    }
}
