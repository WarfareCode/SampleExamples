package utils.customtypes;

public class IndicatorLabelType
{
	public final static int NONE = 0;
	public final static int PERCENT = 1;

	private IndicatorLabelType()
	{
	}

	public static int getDefault()
	{
		return NONE;
	}
}