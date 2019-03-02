using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AGI.STKUtil;
using AGI.STKObjects;
using AGI.STKVgt;
using AGI.STKGraphics;

namespace GraphicsHowTo
{
    public static class STKUtil
    {
        /// <summary>
        /// Reads an STK area target file (*.at) and returns the points defining
        /// the area target's boundary as a list of Cartographic points.
        /// </summary>
        public static Array ReadAreaTargetCartographic(String fileName)
        {
            //
            // Open the file and read everything between "BEGIN PolygonPoints"
            // and "END PolygonPoints"
            //
            String areaTarget = File.ReadAllText(fileName);
            String startToken = "BEGIN PolygonPoints";
            String points = areaTarget.Substring(areaTarget.IndexOf(startToken, StringComparison.Ordinal) + startToken.Length);
            points = points.Substring(0, points.IndexOf("END PolygonPoints", StringComparison.Ordinal));

            String[] splitPoints = points.Split(new char[] { '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            object[] targetPoints = new object[splitPoints.Length];
            for (int i = 0; i < splitPoints.Length; i += 3)
            {
                //
                // Each line is [Latitude][Longitude][Altitude].  In the file,
                // latitude and longitude are in degrees and altitude is in
                // meters.
                //
                double latitude = Double.Parse(splitPoints[i], CultureInfo.InvariantCulture);
                double longitude = Double.Parse(splitPoints[i + 1], CultureInfo.InvariantCulture);
                double altitude = Double.Parse(splitPoints[i + 2], CultureInfo.InvariantCulture);

                targetPoints.SetValue(latitude, i);
                targetPoints.SetValue(longitude, i + 1);
                targetPoints.SetValue(altitude, i + 2);

            }

            return targetPoints;
        }

        /// <summary>
        /// Reads an STK area target file (*.at) and returns the points defining
        /// the area target's boundary as a list Cartesian points in the
        /// earth's fixed frame.
        /// This method assumes the file exists, that it is a valid area target 
        /// file, and the area target is on earth.
        /// </summary>
        public static Array ReadAreaTargetPoints(String fileName, AgStkObjectRoot root)
        {
            //
            // Open the file and read everything between "BEGIN PolygonPoints"
            // and "END PolygonPoints"
            //
            String areaTarget = File.ReadAllText(fileName);
            String startToken = "BEGIN PolygonPoints";
            String points = areaTarget.Substring(areaTarget.IndexOf(startToken, StringComparison.Ordinal) + startToken.Length);
            points = points.Substring(0, points.IndexOf("END PolygonPoints", StringComparison.Ordinal));

            String[] splitPoints = points.Split(new char[] { '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            object[] targetPoints = new object[splitPoints.Length];
            for (int i = 0; i < splitPoints.Length; i += 3)
            {
                //
                // Each line is [Latitude][Longitude][Altitude].  In the file,
                // latitude and longitude are in degrees and altitude is in
                // meters.
                //
                double latitude = Double.Parse(splitPoints[i], CultureInfo.InvariantCulture);
                double longitude = Double.Parse(splitPoints[i + 1], CultureInfo.InvariantCulture);
                double altitude = Double.Parse(splitPoints[i + 2], CultureInfo.InvariantCulture);
                IAgPosition pos = root.ConversionUtility.NewPositionOnEarth();
                pos.AssignPlanetodetic(latitude, longitude, altitude);

                pos.QueryCartesianArray().CopyTo(targetPoints, i);
            }

            return targetPoints;
        }

        public static Array ReadLineTargetPoints(String fileName, AgStkObjectRoot root)
        {
            String areaTarget = File.ReadAllText(fileName);
            String startToken = "BEGIN PolylinePoints";
            String points = areaTarget.Substring(areaTarget.IndexOf(startToken, StringComparison.Ordinal) + startToken.Length);
            points = points.Substring(0, points.IndexOf("END PolylinePoints", StringComparison.Ordinal));

            String[] splitPoints = points.Split(new char[] { '\t', '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            object[] targetPoints = new object[splitPoints.Length];
            for (int i = 0; i < splitPoints.Length; i += 3)
            {
                double longitude = Double.Parse(splitPoints[i + 1], CultureInfo.InvariantCulture);
                double latitude = Double.Parse(splitPoints[i], CultureInfo.InvariantCulture);
                double altitude = Double.Parse(splitPoints[i + 2], CultureInfo.InvariantCulture);
                IAgPosition pos = root.ConversionUtility.NewPositionOnEarth();
                pos.AssignPlanetodetic(latitude, longitude, altitude);

                pos.QueryCartesianArray().CopyTo(targetPoints, i);
            }

            return targetPoints;
        }
    }
}