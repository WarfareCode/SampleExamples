namespace PointInPolygon
{
    public static class ContainmentTests
    {
        /// <summary>
        /// The pyramid's base points should be in counterclockwise winding order.
        /// </summary>
        public static bool PointInsideThreeSidedInfinitePyramid(
            Vector3D point,
            Vector3D pyramidApex,
            Vector3D pyramidBase0,
            Vector3D pyramidBase1,
            Vector3D pyramidBase2)
        {
            Vector3D v0 = pyramidBase0 - pyramidApex;
            Vector3D v1 = pyramidBase1 - pyramidApex;
            Vector3D v2 = pyramidBase2 - pyramidApex;

            //
            // Face normals
            //
            Vector3D n0 = v1.Cross(v0);
            Vector3D n1 = v2.Cross(v1);
            Vector3D n2 = v0.Cross(v2);

            Vector3D planeToPoint = point - pyramidApex;

            return ((planeToPoint.Dot(n0) < 0) && (planeToPoint.Dot(n1) < 0) && (planeToPoint.Dot(n2) < 0));
        }
    }
}