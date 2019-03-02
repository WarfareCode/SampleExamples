using System;
using System.Collections.Generic;
using System.Text;
using AGI.STKVgt;
using AGI.STKUtil;
using AGI.STKGraphics;
using AGI.STKObjects;

namespace GraphicsHowTo
{
    public static class ViewHelper
    {

        /// <summary>
        /// Changes the view of a scene such that the camera's field of view encompasses the specified bounding sphere.
        /// </summary>
        public static void ViewBoundingSphere(IAgStkGraphicsScene scene, AgStkObjectRoot root, string centralBody, IAgStkGraphicsBoundingSphere sphere)
        {
            ViewBoundingSphere(scene, root, centralBody, sphere, -90, 30);
        }
        public static void ViewBoundingSphere(IAgStkGraphicsScene scene, AgStkObjectRoot root, string centralBody,
            IAgStkGraphicsBoundingSphere sphere, double azimuthAngle, double elevationAngle)
        {
            IAgCrdnPoint referencePoint = VgtHelper.CreatePoint(root.CentralBodies[centralBody].Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem);

            Array centerArray = sphere.Center;
            ((IAgCrdnPointFixedInSystem)referencePoint).FixedPoint.AssignCartesian(
                (double)centerArray.GetValue(0),
                (double)centerArray.GetValue(1),
                (double)centerArray.GetValue(2));

            ((IAgCrdnPointFixedInSystem)referencePoint).Reference.SetSystem(root.VgtRoot.WellKnownSystems.Earth.Fixed);

            IAgPosition boundingSphereCenter = root.ConversionUtility.NewPositionOnEarth();
            boundingSphereCenter.AssignCartesian(
                (double)centerArray.GetValue(0),
                (double)centerArray.GetValue(1),
                (double)centerArray.GetValue(2));

            IAgCrdnAxes boundingSphereAxes = CodeSnippet.CreateAxes(root, centralBody, boundingSphereCenter) as IAgCrdnAxes;

            double r = scene.Camera.DistancePerRadius * sphere.Radius;

            string displayUnit = root.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit");
            string internalUnit = "rad";
            double elevationAngleInRad = root.ConversionUtility.ConvertQuantity("AngleUnit", displayUnit, internalUnit, elevationAngle);
            double azimuthAngleInRad = root.ConversionUtility.ConvertQuantity("AngleUnit", displayUnit, internalUnit, azimuthAngle);

            double phi = elevationAngleInRad;
            double theta = azimuthAngleInRad;

            Array offset = new object[] { r * Math.Cos(phi) * Math.Cos(theta), r * Math.Cos(phi) * Math.Sin(theta), r * Math.Sin(phi) };

            scene.Camera.ViewOffset(boundingSphereAxes, referencePoint, ref offset);
        }

        /// <summary>
        /// Change the view of a scene such that the camera's field of view encompasses the specified extent.
        /// </summary>
        /// <param name="extent">Extent as an Array of doubles in the order west, south, east, north.</param>
        public static void ViewExtent(IAgStkGraphicsScene scene, AgStkObjectRoot root, string centralBody,
                Array extent, double azimuthAngle, double elevationAngle)
        {
            double west, south, east, north;
            west = (double)extent.GetValue(0);
            south = (double)extent.GetValue(1);
            east = (double)extent.GetValue(2);
            north = (double)extent.GetValue(3);

            ViewExtent(scene, root, centralBody, west, south, east, north, azimuthAngle, elevationAngle);
        }
        public static void ViewExtent(IAgStkGraphicsScene scene, AgStkObjectRoot root, string centralBody,
                double west, double south, double east, double north, double azimuthAngle, double elevationAngle)
        {
            scene.Camera.ViewRectangularExtent(centralBody, west, south, east, north);

            IAgCartesian3Vector offset = root.ConversionUtility.NewCartesian3Vector();
            double r = scene.Camera.Distance;

            string displayUnit = root.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit");
            string internalUnit = "rad";
            double elevationAngleInRad = root.ConversionUtility.ConvertQuantity("AngleUnit", displayUnit, internalUnit, elevationAngle);
            double azimuthAngleInRad = root.ConversionUtility.ConvertQuantity("AngleUnit", displayUnit, internalUnit, azimuthAngle);

            double phi = elevationAngleInRad;
            double theta = azimuthAngleInRad;
            offset.Set(r * Math.Cos(phi) * Math.Cos(theta), r * Math.Cos(phi) * Math.Sin(theta), r * Math.Sin(phi));

            Array newCameraPosition = new object[] 
            {
                (double)scene.Camera.ReferencePoint.GetValue(0) + offset.X,
                (double)scene.Camera.ReferencePoint.GetValue(1) + offset.Y,
                (double)scene.Camera.ReferencePoint.GetValue(2) + offset.Z
            };

            scene.Camera.Position = newCameraPosition;
        }
    }
}
