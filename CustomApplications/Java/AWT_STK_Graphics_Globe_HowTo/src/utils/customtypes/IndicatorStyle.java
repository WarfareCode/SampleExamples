package utils.customtypes;

public class IndicatorStyle
{
	public final static int BAR = 0;
	public final static int MARKER = 1;

	private IndicatorStyle()
	{
	}

	public static int getDefault()
	{
		return BAR;
	}
}