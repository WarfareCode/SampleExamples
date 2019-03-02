using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AGI.STKUtil;
using AGI.STKObjects;

namespace GraphicsHowTo
{
    public class PositionOrientationProvider
    {
        private const string Separator = "    ";

        public PositionOrientationProvider(string filename, AgStkObjectRoot root)
        {
            m_Dates = new List<double>();
            m_Positions = new List<Array>();
            m_Orientations = new List<Array>();
            m_root = root;

            using (StreamReader sr = new StreamReader(filename))
            {
                while (sr.Peek() >= 0)
                {
                    string[] sEntries = sr.ReadLine().Replace(Separator, ",").Split(',');
                    m_Dates.Add(double.Parse(root.ConversionUtility.NewDate("UTCG", sEntries[0]).Format("epSec")));

                    double x = Double.Parse(sEntries[1], CultureInfo.InvariantCulture);
                    double y = Double.Parse(sEntries[2], CultureInfo.InvariantCulture);
                    double z = Double.Parse(sEntries[3], CultureInfo.InvariantCulture);
                    Array pos = new object[] { x, y, z };
                    m_Positions.Add(pos);

                    x = Double.Parse(sEntries[4], CultureInfo.InvariantCulture);
                    y = Double.Parse(sEntries[5], CultureInfo.InvariantCulture);
                    z = Double.Parse(sEntries[6], CultureInfo.InvariantCulture);
                    double w = Double.Parse(sEntries[7], CultureInfo.InvariantCulture);
                    Array orientation = new object[] { x, y, z, w };
                    m_Orientations.Add(orientation);
                }
            }
        }

        public IList<double> Dates
        {
            get { return m_Dates; }
        }
        public IList<Array> Positions
        {
            get { return m_Positions; }
        }
        public IList<Array> Orientations
        {
            get { return m_Orientations; }
        }

        public int FindIndexOfClosestTime(double searchTime, int startIndex, int searchLength)
        {
            // Find the midpoint of the length
            int midpoint = startIndex + (searchLength / 2);

            // Base cases
            if (m_Dates[startIndex] == searchTime || searchLength == 1)
            {
                return startIndex;
            }
            if (searchLength == 2)
            {
                double diff1 = m_Dates[startIndex] - searchTime;
                double diff2 = m_Dates[startIndex + 1] - searchTime;

                if (Math.Abs(diff1) < Math.Abs(diff2))
                {
                    return startIndex;
                }
                else // Note: error on the larger time if equal
                {
                    return startIndex + 1;
                }
            }
            if (m_Dates[midpoint] == searchTime)
            {
                return midpoint;
            }

            // Normal case: binary search
            if (searchTime < m_Dates[midpoint])
            {
                return FindIndexOfClosestTime(searchTime, startIndex, midpoint - startIndex);
            }
            else
            {
                return FindIndexOfClosestTime(searchTime, midpoint + 1, startIndex + searchLength - (midpoint + 1));
            }
        }


        private readonly IList<double> m_Dates;
        private readonly IList<Array> m_Positions;
        private readonly IList<Array> m_Orientations;
        private readonly AgStkObjectRoot m_root;
    }
}
