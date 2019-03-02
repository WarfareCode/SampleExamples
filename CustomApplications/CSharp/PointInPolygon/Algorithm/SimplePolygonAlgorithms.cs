﻿using System;
using System.Collections.Generic;

namespace PointInPolygon
{
    public enum PolygonWindingOrder
    {
        Clockwise,
        Counterclockwise
    }

    public static class SimplePolygonAlgorithms
    {
        /// <summary>
        /// Cleans up a simple polygon by removing duplicate adjacent positions and making
        /// the first position not equal the last position
        /// </summary>
        public static IList<Vector3D> Cleanup(Array positions)
        {
            IList<Vector3D> positionsList = CollectionAlgorithms.ArrayOfVectorsToList(positions);

            if (positionsList.Count < 3)
            {
                throw new ArgumentOutOfRangeException("positions", "At least three positions are required.");
            }

            List<Vector3D> cleanedPositions = new List<Vector3D>(positionsList.Count);

            for (int i0 = positionsList.Count - 1, i1 = 0; i1 < positionsList.Count; i0 = i1++)
            {
                Vector3D v0 = positionsList[i0];
                Vector3D v1 = positionsList[i1];

                if (!v0.Equals(v1))
                {
                    cleanedPositions.Add(v1);
                }
            }

            cleanedPositions.TrimExcess();
            return cleanedPositions;
        }

        public static double ComputeArea(IEnumerable<Vector2D> positions)
        {
            IList<Vector2D> positionsList = CollectionAlgorithms.EnumerableToList(positions);

            if (positionsList.Count < 3)
            {
                throw new ArgumentOutOfRangeException("positions", "At least three positions are required.");
            }

            double area = 0.0;

            //
            // Compute the area of the polygon.  The sign of the area determines the winding order.
            //
            for (int i0 = positionsList.Count - 1, i1 = 0; i1 < positionsList.Count; i0 = i1++)
            {
                Vector2D v0 = positionsList[i0];
                Vector2D v1 = positionsList[i1];

                area += (v0.X * v1.Y) - (v1.X * v0.Y);
            }

            return area * 0.5;
        }

        public static PolygonWindingOrder ComputeWindingOrder(IEnumerable<Vector2D> positions)
        {
            return (ComputeArea(positions) >= 0.0) ? PolygonWindingOrder.Counterclockwise : PolygonWindingOrder.Clockwise;
        }
    }
}