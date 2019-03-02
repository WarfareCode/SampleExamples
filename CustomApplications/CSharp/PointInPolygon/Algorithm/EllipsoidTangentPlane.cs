using System;
using System.Collections.Generic;
using AGI.STKUtil;
using AGI.STKObjects;

namespace PointInPolygon
{
    public class EllipsoidTangentPlane
    {
        public EllipsoidTangentPlane(IEnumerable<Vector3D> positions, AgStkObjectRoot root)
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }

            if (positions == null)
            {
                throw new ArgumentNullException("positions");
            }

            if (!CollectionAlgorithms.EnumerableCountGreaterThanOrEqual(positions, 1))
            {
                throw new ArgumentOutOfRangeException("positions", "At least one position is required.");
            }

            AxisAlignedBoundingBox box = new AxisAlignedBoundingBox(positions);

            IAgPosition position = root.ConversionUtility.NewPositionOnCB("Earth");

            object lat = 0, lon = 0;
            double alt = 0;

            // convert to cartographic coordinates.
            position.AssignCartesian(box.Center.X, box.Center.Y, box.Center.Z);
            position.QueryPlanetocentric(out lat, out lon, out alt);

            // move to surface
            position.AssignPlanetocentric(lat, lon, 0.0);

            // convert back to cartesian coordinates
            double x = 0, y = 0, z = 0;
            position.QueryCartesian(out x, out y, out z);
            _origin = new Vector3D(x, y, z);

            x = root.CentralBodies["Earth"].Ellipsoid.A;
            y = root.CentralBodies["Earth"].Ellipsoid.B;
            z = root.CentralBodies["Earth"].Ellipsoid.C;
            Vector3D oneOverSemiAxisLengthsSquared = new Vector3D(
                1.0 / (x * x), 1.0 / (y * y), 1.0 / (z * z));

            _normal = new Vector3D(
                _origin.X * oneOverSemiAxisLengthsSquared.X,
                _origin.Y * oneOverSemiAxisLengthsSquared.Y,
                _origin.Z * oneOverSemiAxisLengthsSquared.Z).Normalize();

            _d = -_origin.Dot(_origin);
            _yAxis = _origin.Cross(_origin.MostOrthogonalAxis).Normalize();
            _xAxis = _yAxis.Cross(_origin).Normalize();
        }

        public ICollection<Vector2D> ComputePositionsOnPlane(IEnumerable<Vector3D> positions)
        {
            if (positions == null)
            {
                throw new ArgumentNullException("positions");
            }

            IList<Vector2D> positionsOnPlane = new List<Vector2D>(CollectionAlgorithms.EnumerableCount(positions));

            foreach (Vector3D position in positions)
            {
                Vector3D intersectionPoint;

                if (IntersectionTests.TryRayPlane(Vector3D.Zero, position.Normalize(), _normal, _d, out intersectionPoint))
                {
                    Vector3D v = intersectionPoint - _origin;
                    positionsOnPlane.Add(new Vector2D(_xAxis.Dot(v), _yAxis.Dot(v)));
                }
                else
                {
                    // Ray does not intersect plane
                }
            }

            return positionsOnPlane;
        }

        public Vector3D Origin { get { return _origin; } }
        public Vector3D Normal { get { return _normal; } }
        public double D { get { return _d; } }
        public Vector3D XAxis { get { return _xAxis; } }
        public Vector3D YAxis { get { return _yAxis; } }

        private Vector3D _origin;
        private Vector3D _normal;
        private double _d;
        private Vector3D _xAxis;
        private Vector3D _yAxis;
    }
}
