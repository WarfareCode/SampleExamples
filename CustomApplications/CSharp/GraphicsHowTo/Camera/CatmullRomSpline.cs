using System;
using System.Collections.Generic;
using AGI.STKUtil;
using AGI.STKObjects;

namespace GraphicsHowTo.Camera
{
    public class CatmullRomSpline
    {
        //
        // Creates a spline given a shape and a list of cartographics
        //
        public CatmullRomSpline(string centralBody, ICollection<Array> positions, AgStkObjectRoot root)
        {
            CalculateControlPoints(root, centralBody, positions);
            CalculateInterpolationPoints(root);
        }

        //
        // Creates a spline given a central body, two start points and an altitude
        //
        public CatmullRomSpline(AgStkObjectRoot root, string centralBody, Array start, Array end, double altitude)
        {
            altitude = Math.Abs(altitude);
            double height = 0.8;

            Array aboveStart = new object[] { (double)start.GetValue(0), (double)start.GetValue(1), altitude * height };
            Array middle = new object[] { ((double)start.GetValue(0) + (double)end.GetValue(0)) / 2, ((double)start.GetValue(1) + (double)end.GetValue(1)) / 2, altitude };
            Array aboveEnd = new object[]{ (double)end.GetValue(0), (double)end.GetValue(1), altitude * height };

            List<Array> cartographics = new List<Array>();
            cartographics.Add(start);
            cartographics.Add(aboveStart);
            cartographics.Add(middle);
            cartographics.Add(aboveEnd);
            cartographics.Add(end);

            CalculateControlPoints(root, centralBody, cartographics);
            CalculateInterpolationPoints(root);
        }

        //
        // Returns a list of interpolator points
        //
        public ICollection<Array> InterpolatorPoints
        {
            get { return m_InterpolatorPoints; }
        }

        //
        // Sets the number of interpolation points that should be part of the spline
        //
        public void SetNumberOfInterpolationPoints(int numberOfPoints, AgStkObjectRoot root)
        {
            m_NumberOfInterpolatorPoints = numberOfPoints;

            m_NumberOfInterpolatorPoints = Math.Max(m_NumberOfInterpolatorPoints, (m_ControlPoints.Count - 3) * 2);
            m_NumberOfInterpolatorPoints = Math.Min(m_NumberOfInterpolatorPoints, 1000000);

            CalculateInterpolationPoints(root);
        }

        //
        // Calculates the control points for the spline
        //
        private void CalculateControlPoints(AgStkObjectRoot root, string centralBody, ICollection<Array> positions)
        {
            List<Array> positionsList = new List<Array>(positions);
            m_ControlPoints = new List<Array>();
            int numPoints = positionsList.Count;
            if (numPoints >= 2)
            {
                Array virtualStart = new object[] { (double)positionsList[0].GetValue(0) + ((double)positionsList[0].GetValue(0) - (double)positionsList[1].GetValue(0)),
                                                             (double)positionsList[0].GetValue(1) + ((double)positionsList[0].GetValue(1) - (double)positionsList[1].GetValue(1)),
                                                             (double)positionsList[0].GetValue(2) + ((double)positionsList[0].GetValue(2) - (double)positionsList[1].GetValue(2)) };

                Array virtualEnd = new object[] { (double)positionsList[numPoints - 1].GetValue(0) + ((double)positionsList[numPoints - 1].GetValue(0) - (double)positionsList[numPoints - 2].GetValue(0)),
                                                           (double)positionsList[numPoints - 1].GetValue(1) + ((double)positionsList[numPoints - 1].GetValue(1) - (double)positionsList[numPoints - 2].GetValue(1)),
                                                           (double)positionsList[numPoints - 1].GetValue(2) + ((double)positionsList[numPoints - 1].GetValue(2) - (double)positionsList[numPoints - 2].GetValue(2)) };

                m_ControlPoints.Add(CartographicToCartesian(virtualStart, centralBody, root));
                for (int i = 0; i < numPoints; i++)
                {
                    m_ControlPoints.Add(CartographicToCartesian(positionsList[i], centralBody, root));
                }
                m_ControlPoints.Add(CartographicToCartesian(virtualEnd, centralBody, root));
            }
        }

        //
        // Calculates the interpolator points for the spline
        //
        private void CalculateInterpolationPoints(AgStkObjectRoot root)
        {
            m_InterpolatorPoints = new List<Array>();

            if (m_ControlPoints.Count >= 4)
            {
                for (int i = 1; i <= m_ControlPoints.Count - 3; i++)
                {
                    Array[] points = new Array[4];
                    points[0] = m_ControlPoints[i - 1];
                    points[1] = m_ControlPoints[i];
                    points[2] = m_ControlPoints[i + 1];
                    points[3] = m_ControlPoints[i + 2];

                    int end = m_NumberOfInterpolatorPoints / (m_ControlPoints.Count - 3);
                    for (int t = 0; t < end; t++)
                    {
                        double time = (double)t / (double)(end - 1);
                        double t1 = time;
                        double t2 = time * time;
                        double t3 = time * time * time;

                        Array position = new double[] {
                            0.5 * ((2 * (double)points[1].GetValue(0)) + (-(double)points[0].GetValue(0) + (double)points[2].GetValue(0)) * t1 +
                            (2 * (double)points[0].GetValue(0) - 5 * (double)points[1].GetValue(0) + 4 * (double)points[2].GetValue(0) - (double)points[3].GetValue(0)) * t2 +
                            (-(double)points[0].GetValue(0) + 3 * (double)points[1].GetValue(0) - 3 * (double)points[2].GetValue(0) + (double)points[3].GetValue(0)) * t3),
                            
                            0.5 * ((2 * (double)points[1].GetValue(1)) + (-(double)points[0].GetValue(1) + (double)points[2].GetValue(1)) * t1 +
                            (2 * (double)points[0].GetValue(1) - 5 * (double)points[1].GetValue(1) + 4 * (double)points[2].GetValue(1) - (double)points[3].GetValue(1)) * t2 +
                            (-(double)points[0].GetValue(1) + 3 * (double)points[1].GetValue(1) - 3 * (double)points[2].GetValue(1) + (double)points[3].GetValue(1)) * t3),

                            0.5 * ((2 * (double)points[1].GetValue(2)) + (-(double)points[0].GetValue(2) + (double)points[2].GetValue(2)) * t1 +
                            (2 * (double)points[0].GetValue(2) - 5 * (double)points[1].GetValue(2) + 4 * (double)points[2].GetValue(2) - (double)points[3].GetValue(2)) * t2 +
                            (-(double)points[0].GetValue(2) + 3 * (double)points[1].GetValue(2) - 3 * (double)points[2].GetValue(2) + (double)points[3].GetValue(2)) * t3) };

                        m_InterpolatorPoints.Add(position);
                    }
                }
            }
        }

        public static Array CartographicToCartesian(Array cartographic, string centralBody, AgStkObjectRoot root)
        {
            IAgPosition position = root.ConversionUtility.NewPositionOnCB(centralBody);
            position.AssignPlanetodetic((double)cartographic.GetValue(0), (double)cartographic.GetValue(1), Math.Max((double)cartographic.GetValue(2), 0));

            double x = 0, y = 0, z = 0;
            position.QueryCartesian(out x, out y, out z);

            return new double[] { x, y, z };
        }

        public static Array CartesianToCartographic(Array cartesian, string centralBody, AgStkObjectRoot root)
        {
            IAgPosition position = root.ConversionUtility.NewPositionOnCB(centralBody);
            position.AssignCartesian((double)cartesian.GetValue(0), (double)cartesian.GetValue(1), (double)cartesian.GetValue(2));

            object lat = 0, lon = 0;
            double alt = 0;
            position.QueryPlanetodetic(out lat, out lon, out alt);
            return new object[3] { lat, lon, alt };
        }

        private List<Array> m_ControlPoints;
        private List<Array> m_InterpolatorPoints;
        private int m_NumberOfInterpolatorPoints = 10000;
    }
}
