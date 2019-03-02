package utils.customtypes;

public class Interval
{
	/**
	 * Initializes a new instance.
	 */
	public Interval()
	{
	}

	public final double getMinimum()
	{
		return m_Minimum;
	}

	public final void setMinimum(double value)
	{
		m_Minimum = value;
	}

	public final double getMaximum()
	{
		return m_Maximum;
	}

	public final void setMaximum(double value)
	{
		m_Maximum = value;
	}

	public Interval(double minimum, double maximum)
	{
		m_Minimum = minimum;
		m_Maximum = maximum;
	}

	public boolean equals(Object obj)
	{
		if(obj instanceof Interval)
		{
			Interval other = (Interval)obj;
			return other.getMaximum() == getMaximum() && other.getMinimum() == getMinimum();
		}
		else
		{
			return false;
		}
	}

	private double	m_Minimum;
	private double	m_Maximum;
}