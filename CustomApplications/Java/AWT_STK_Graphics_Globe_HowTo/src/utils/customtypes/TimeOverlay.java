package utils.customtypes;

import java.text.*;
import java.util.*;

// AGI Java API
import agi.core.*;
import agi.stkutil.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

// Sample API
import codesnippets.*;

public class TimeOverlay
extends StatusOverlay
implements IAgStkObjectRootEvents2
{
	//January 1, 1970 00:00:00.000 GMT 
	private static final double s_JulianDaysFor_Jan_01_1970_00_00_00 = 2440587.500000;
	
	static
	{
	}
	
	private IAgStkObjectRoot	m_Root;
	private IAgDate				m_CurrentTime;

	public TimeOverlay(STKGraphicsCodeSnippet cs, AgStkObjectRootClass root) 
	throws NumberFormatException, AgCoreException
	{
		super
		(
			cs, 
			((IAgScenario)root.getCurrentScenario()).getSceneManager(),
			true, 
			(Double)((IAgScenario)root.getCurrentScenario()).getStartTime_AsObject(),
			(Double)((IAgScenario)root.getCurrentScenario()).getStopTime_AsObject(),
			root.getConversionUtility().newDate("epSec", ((Double)((IAgScenario)root.getCurrentScenario()).getStartTime_AsObject()).toString()).format("YYYY/MM/DD").substring(5, 10),
			root.getConversionUtility().newDate("epSec", ((Double)((IAgScenario)root.getCurrentScenario()).getStopTime_AsObject()).toString()).format("YYYY/MM/DD").substring(5, 10)
		);

		this.m_Root = root;
		Double start = (Double)((IAgScenario)root.getCurrentScenario()).getStartTime_AsObject();
		this.m_CurrentTime = root.getConversionUtility().newDate("epSec", start.toString());

		root.addIAgStkObjectRootEvents2(this);
	}

	public void onAgStkObjectRootEvent(AgStkObjectRootEvent e)
	{
		try
		{
			int type = e.getType();
			if(type == AgStkObjectRootEvent.TYPE_ON_ANIM_UPDATE)
			{
				Object[] params = e.getParams();
				double timeEpSec = ((Double)params[0]).doubleValue();
				stkTimeChanged(timeEpSec);
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void stkTimeChanged(double timeEpSec)
	throws Throwable
	{
		this.m_CurrentTime = this.m_Root.getConversionUtility().newDate("epSec", Double.toString(timeEpSec));
		Double value = this.getValueAsDouble();
		IAgScenario scen = (IAgScenario)this.m_Root.getCurrentScenario();
		IAgStkGraphicsSceneManager sm = scen.getSceneManager();
		this.update(sm, value);
	}

	public double valueTransform(Object value)
	throws Throwable
	{
		return valueTransform((Double)value);
	}

	public double valueTransform(Double value)
	throws Throwable
	{
		IAgScenario scen = (IAgScenario)this.m_Root.getCurrentScenario();
		Double startTime = (Double)scen.getStartTime_AsObject();
		double valueTransformed = value.doubleValue() - startTime.doubleValue();
		return valueTransformed;
	}

	public Object getValue()
	throws Throwable
	{
		return getValueAsDouble();
	}

	public Double getValueAsDouble()
	throws Throwable
	{
		return new Double(Double.parseDouble(this.m_CurrentTime.format("epSec")));
	}

	public String getText()
	throws Throwable
	{
		String[] date = this.m_CurrentTime.format("UTCG").split(" ");
		return "Current Time:\r\n" + date[0] + " " + date[1] + " " + date[2] + "\r\n" + date[3];
	}

	public final void addInterval(STKGraphicsCodeSnippet c, double start, double end)
	throws Throwable
	{
		IAgScenario scenario = (IAgScenario)this.m_Root.getCurrentScenario();
		IAgStkGraphicsSceneManager sm = scenario.getSceneManager();

		double vstart = valueTransform(new Double(start));
		double vend = valueTransform(new Double(end));

		IndicatorOverlay io = this.getIndicator(c);
		io.addInterval(vstart, vend, IndicatorStyle.BAR, sm);
	}

	public final void removeInterval(STKGraphicsCodeSnippet c, double start, double end)
	throws Throwable
	{
		double vstart = valueTransform(new Double(start));
		double vend = valueTransform(new Double(end));

		IndicatorOverlay io = this.getIndicator(c);
		io.removeInterval(vstart, vend);
	}
	
	static class AgDateToDateConverter
	{
		public static Date convert(IAgDate date) 
		throws AgCoreException
		{
			double days = date.getWholeDays() - s_JulianDaysFor_Jan_01_1970_00_00_00;
			double daysAsSecs = days * 86400.0;
			double secIntoDay = date.getSecIntoDay();
			double totalMilliseconds = (daysAsSecs + secIntoDay) * 1000.0;
			Calendar c = Calendar.getInstance();
			c.setTimeInMillis((long)totalMilliseconds);
			return c.getTime();
		}
	}
}