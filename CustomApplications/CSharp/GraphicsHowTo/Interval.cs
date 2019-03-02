using System;

namespace GraphicsHowTo
{
    public struct Interval : IEquatable<Interval>
    {
        public double Minimum
        {
            get { return m_Minimum; }
            set { m_Minimum = value; }
        }

        public double Maximum
        {
            get { return m_Maximum; }
            set { m_Maximum = value; }
        }

        public Interval(double minimum, double maximum)
        {
            m_Minimum = minimum;
            m_Maximum = maximum;
        }

        public override bool Equals(object obj)
        {
            if (obj is Interval)
            {
                Interval i = (Interval)obj;
                return this.Equals(i);
            }
            else { return false; }
        }

        public bool Equals(Interval other)
        {
            return (other.Maximum == Maximum && other.Minimum == Minimum);
        }

        public override int GetHashCode()
        {
            //based on GetHashCode inside the .Net libararies
            int finalHashCode = 0x61E04917; //sufficiently large random number
            finalHashCode = ((finalHashCode << 5) + finalHashCode) ^ Minimum.GetHashCode();
            finalHashCode = ((finalHashCode << 5) + finalHashCode) ^ Maximum.GetHashCode();
            return finalHashCode;
        }

        public static bool operator ==(Interval interval1, Interval interval2)
        {
            return interval1.Equals(interval2);
        }

        public static bool operator !=(Interval interval1, Interval interval2)
        {
            return !(interval1.Equals(interval2));
        }

        private double m_Minimum;
        private double m_Maximum;
    }
}
