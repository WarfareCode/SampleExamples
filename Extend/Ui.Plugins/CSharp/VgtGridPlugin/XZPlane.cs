﻿using System;

namespace AGI.Grid
{
    class XZPlane : GridPlane
    {
        public XZPlane()
        {
            this.Display = true;
            this.Color = System.Drawing.Color.Yellow;
            this.Size = 100.0;
            this.Spacing = 2.0;
            this.LineWidth = 2.0f;
            this.Translucency = 20.0f;
        }

        protected override Array GenerateGridPoints()
        {
            System.Collections.ArrayList points = new System.Collections.ArrayList();
            double halfGridSize = this.Size / 2.0;

            // Make the origin
            // Horizontal from
            points.Add(-halfGridSize);
            points.Add(0);
            points.Add(0);

            // Horizontal to
            points.Add(halfGridSize);
            points.Add(0);
            points.Add(0);

            // Vertical from
            points.Add(0);
            points.Add(0);
            points.Add(halfGridSize);

            // Vertical to
            points.Add(0);
            points.Add(0);
            points.Add(-halfGridSize);

            // Draw the rest of the grid lines away from the center
            for (int i = 1; i * this.Spacing <= halfGridSize; ++i)
            {
                // Horizontal above origin from
                points.Add(-halfGridSize);
                points.Add(0);
                points.Add(i * this.Spacing);

                // Horizontal above origin to
                points.Add(halfGridSize);
                points.Add(0);
                points.Add(i * this.Spacing);

                // Horizontal below origin from
                points.Add(-halfGridSize);
                points.Add(0);
                points.Add(-i * this.Spacing);

                // Horizontal below origin to
                points.Add(halfGridSize);
                points.Add(0);
                points.Add(-i * this.Spacing);

                // Vertical above origin from
                points.Add(i * this.Spacing);
                points.Add(0);
                points.Add(halfGridSize);

                // Vertical above origin to
                points.Add(i * this.Spacing);
                points.Add(0);
                points.Add(-halfGridSize);

                // Vertical below origin from
                points.Add(-i * this.Spacing);
                points.Add(0);
                points.Add(halfGridSize);

                // Vertical below origin to
                points.Add(-i * this.Spacing);
                points.Add(0);
                points.Add(-halfGridSize);
            }

            return points.ToArray();
        }
    }
}
