package charts;

// Java API
import java.util.*;

public class ChartDataHelper
{
	public static String getCurrentTimeAsStkTime()
	{
		long now = System.currentTimeMillis();
		Calendar c = Calendar.getInstance();
		c.setTimeInMillis(now);
		
		StringBuilder sb = new StringBuilder();
		sb.append(c.get(Calendar.DAY_OF_MONTH));
		sb.append(" ");
		sb.append(convertJavaIntToStkMonthString(c.get(Calendar.MONTH)));
		sb.append(" ");
		sb.append(c.get(Calendar.YEAR));
		sb.append(" ");
		sb.append(c.get(Calendar.HOUR_OF_DAY));
		sb.append(":");
		sb.append(c.get(Calendar.MINUTE));
		sb.append(":");
		sb.append(c.get(Calendar.SECOND));
		
		return sb.toString();
	}
	public static double getMaxDoubleValue(Object[] values)
	{
		double max = ((Double)values[0]).doubleValue();
		for(int i=0; i<values.length; i++)
		{
			double newValue = ((Double)values[i]).doubleValue();
			if(newValue > max)
			{
				max = newValue;
			}
		}
		return max;
	}

	public static double getMinDoubleValue(Object[] values)
	{
		double min = ((Double)values[0]).doubleValue();
		for(int i=0; i<values.length; i++)
		{
			double newValue = ((Double)values[i]).doubleValue();
			if(newValue < min)
			{
				min = newValue;
			}
		}
		return min;
	}

	public static Date convertStkDateStringToDate(String stkDateTime)
	{
        return convertStkDateStringToCalendar(stkDateTime).getTime();
	}

	public static Calendar convertStkDateStringToCalendar(String stkDateTime)
	{
    	String[] dmyt = stkDateTime.split(" ");
        
        String day = dmyt[0];
        String month = dmyt[1];
        String year = dmyt[2];
        String time = dmyt[3];
        
        // parse the time
        String[] hms = time.split( ":" );
        String hour = hms[0];
        String min = hms[1];
        String sec = hms[2];
        int index = sec.indexOf(".");
        sec = sec.substring(0, index);
        
        // Create the calendar time
        Calendar c = Calendar.getInstance();
        c.set(Integer.parseInt(year), 
              ChartDataHelper.convertStkMonthStringToJavaInt(month),
              Integer.parseInt(day),
              Integer.parseInt(hour),
              Integer.parseInt(min),
              Integer.parseInt(sec));
		
        return c;
	}
	
	public static int convertStkMonthStringToJavaInt(String month)
	{
		int imonth = -1;
		String lmonth = month.toLowerCase();
		if(lmonth.startsWith("jan"))
		{
			imonth = Calendar.JANUARY;
		}
		else if(lmonth.startsWith("feb"))
		{
			imonth = Calendar.FEBRUARY;
		}
		else if(lmonth.startsWith("mar"))
		{
			imonth = Calendar.MARCH;
		}
		else if(lmonth.startsWith("apr"))
		{
			imonth = Calendar.APRIL;
		}
		else if(lmonth.startsWith("may"))
		{
			imonth = Calendar.MAY;
		}
		else if(lmonth.startsWith("jun"))
		{
			imonth = Calendar.JUNE;
		}
		else if(lmonth.startsWith("jul"))
		{
			imonth = Calendar.JULY;
		}
		else if(lmonth.startsWith("aug"))
		{
			imonth = Calendar.AUGUST;
		}
		else if(lmonth.startsWith("sep"))
		{
			imonth = Calendar.SEPTEMBER;
		}
		else if(lmonth.startsWith("oct"))
		{
			imonth = Calendar.OCTOBER;
		}
		else if(lmonth.startsWith("nov"))
		{
			imonth = Calendar.NOVEMBER;
		}
		else if(lmonth.startsWith("dec"))
		{
			imonth = Calendar.DECEMBER;
		}
		return imonth;
	}

	public static String convertJavaIntToStkMonthString(int month)
	{
		String smonth = "";
		if(month == Calendar.JANUARY)
		{
			smonth = "Jan";
		}
		else if(month == Calendar.FEBRUARY)
		{
			smonth = "Feb";
		}
		else if(month == Calendar.MARCH)
		{
			smonth = "Mar";
		}
		else if(month == Calendar.APRIL)
		{
			smonth = "Apr";
		}
		else if(month == Calendar.MAY)
		{
			smonth = "May";
		}
		else if(month == Calendar.JUNE)
		{
			smonth = "Jun";
		}
		else if(month == Calendar.JULY)
		{
			smonth = "Jul";
		}
		else if(month == Calendar.AUGUST)
		{
			smonth = "Aug";
		}
		else if(month == Calendar.SEPTEMBER)
		{
			smonth = "Sep";
		}
		else if(month == Calendar.OCTOBER)
		{
			smonth = "Oct";
		}
		else if(month == Calendar.NOVEMBER)
		{
			smonth = "Nov";
		}
		else if(month == Calendar.DECEMBER)
		{
			smonth = "Dec";
		}
		return smonth;
	}
}