using System.Collections.Generic;
using AGI.STKObjects;
using System;

namespace PointInPolygon
{
    /// <summary>
    /// A point in polygon test for polygons on the surface of an ellipsoid.
    /// </summary>
    public class PointInPolygonTest
    {
        /// <summary>
        /// Initializes an instance of this type.  If several point in polygon tests will
        /// be performed using the same polygon, this object only needs to be constructed once.
        /// </summary>
        /// <param name="positions">The polygon's boundary positions.</param>
        public PointInPolygonTest(Array positions, AgStkObjectRoot root)
        {
            IList<Vector3D> cleanedPositions = SimplePolygonAlgorithms.Cleanup(positions);

            EllipsoidTangentPlane plane = new EllipsoidTangentPlane(cleanedPositions, root);
            ICollection<Vector2D> positionsOnPlane = plane.ComputePositionsOnPlane(cleanedPositions);

            if (SimplePolygonAlgorithms.ComputeWindingOrder(positionsOnPlane) == PolygonWindingOrder.Clockwise)
            {
                (cleanedPositions as List<Vector3D>).Reverse();
            }

            IList<int> indices = EarClippingOnEllipsoid.Triangulate(cleanedPositions);

            _normals = new List<Vector3D>(indices.Count);
            for (int i = 0; i < indices.Count; i += 3)
            {
                Vector3D v0 = cleanedPositions[indices[i]];
                Vector3D v1 = cleanedPositions[indices[i + 1]];
                Vector3D v2 = cleanedPositions[indices[i + 2]];

                //
                // Face normals
                //
                _normals.Add(v1.Cross(v0));
                _normals.Add(v2.Cross(v1));
                _normals.Add(v0.Cross(v2));
            }

            ///////////////////////////////////////////////////////////////////

            double maximumZ = cleanedPositions[0].Z;
            double minimumZ = cleanedPositions[0].Z;
            Vector3D unitZ = new Vector3D(0, 0, 1);
            Vector3D west = unitZ.Cross(cleanedPositions[0]);
            Vector3D east = -west;

            for (int i = 1; i < cleanedPositions.Count; ++i)
            {
                double z = cleanedPositions[i].Z;

                if (z > maximumZ)
                {
                    maximumZ = z;
                }

                if (z < minimumZ)
                {
                    minimumZ = z;
                }

                if (cleanedPositions[i].Dot(west) > 0)
                {
                    west = unitZ.Cross(cleanedPositions[i]);
                }
                else if (cleanedPositions[i].Dot(east) > 0)
                {
                    east = -unitZ.Cross(cleanedPositions[i]);
                }
            }

            _maximumZ = maximumZ;
            _minimumZ = minimumZ;
            _east = east;
            _west = west;
        }

        /// <summary>
        /// Tests if a position on the surface of the ellipsoid is in the polygon.
        /// </summary>
        /// <param name="position">A position on the surface of the ellipsoid to test.</param>
        /// <returns>Returns true if the position is in the polygon.</returns>
        /// <remarks>
        /// The boundary points of the polygon are connected with geodesic curves.
        /// </remarks>
        public bool Test(Vector3D position)
        {
            if ((position.Z < _minimumZ) ||
                (position.Z > _maximumZ) ||
                (position.Dot(_west) > 0) ||
                (position.Dot(_east) > 0))
            {
                return false;
            }

            for (int i = 0; i != _normals.Count; i += 3)
            {
                if ((position.Dot(_normals[i]) <= 0) &&
                    (position.Dot(_normals[i + 1]) <= 0) &&
                    (position.Dot(_normals[i + 2]) <= 0))
                {
                    return true;
                }
            }

            return false;
        }

        private IList<Vector3D> _normals;

        //
        // Planes bounding polygon for quick reject
        //
        private double _maximumZ;
        private double _minimumZ;
        private Vector3D _east;
        private Vector3D _west;
    }
}