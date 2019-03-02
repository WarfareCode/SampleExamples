package utils.customtypes;

// Java API
import java.awt.*;
import java.text.*;
import java.util.*;

// AGI Java API
import agi.core.*;
import agi.stkgraphics.*;

// Sample API
import codesnippets.*;

public class AltitudeOverlay
extends StatusOverlay
implements IAgStkGraphicsSceneEvents
{
	private AgStkGraphicsSceneClass			m_Scene;
	private IAgStkGraphicsSceneManager	m_SceneManager;

	public AltitudeOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, AgStkGraphicsSceneClass scene)
	throws AgCoreException
	{
		super(cs, manager, false, new Double(0.0), new Double(10000000.0), "0", "10000");
		
		this.m_Scene = scene;
		this.m_SceneManager = manager;

		this.m_Scene.addIAgStkGraphicsSceneEvents(this);
	}

	public void onAgStkGraphicsSceneEvent(AgStkGraphicsSceneEvent e)
	{
		try
		{
			if(e.getType() == AgStkGraphicsSceneEvent.TYPE_RENDERING)
			{
				update(this.m_STKGraphicsCodeSnippet, this.m_SceneManager, getValue());
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public double valueTransform(Object value)
	{
		return valueTransform((Double)value);
	}
	
	public double valueTransform(Double value)
	{
		if(value == null)
			value = new Double(0.0);
		return (value.doubleValue() >= 1) ? Math.log10(value.doubleValue() / 10000.0) : value.doubleValue();
	}

	public Object getValue()
	throws AgCoreException
	{
		Object[] positions = (Object[])this.m_Scene.getCamera().getPosition_AsObject();
		double pos0 = ((Double)positions[0]).doubleValue();
		double pos1 = ((Double)positions[1]).doubleValue();
		double pos2 = ((Double)positions[2]).doubleValue();
		return new Double(getVectorMagnitude(pos0, pos1, pos2));
	}

	public String getText()
	throws AgCoreException
	{
		DecimalFormat df = new DecimalFormat("#.###");
		return "Current Altitude:\n" + df.format(((Double)getValue()).doubleValue() / 1000) + " km\n\n\n";
	}

	/**
	 * Add a list of intervals of doubles using a different color for each.
	 * @param intervals Collection of raw (non-ValueTransformed) Intervals.
	 * @throws AgCoreException 
	 */
	public final void addIntervals(ArrayList<Interval> intervals)
	throws Throwable
	{
		Color[] colors = new Color[] 
		{
			new Color(0x87CEEB), // sky blue
			new Color(0x90EE90), // light green
			Color.YELLOW, 
			new Color(0xFFA07A), // light salmon
			new Color(0x8B0000), // dark red
			new Color(0x9370DB) // medium purple
		};

		int i = 0;
		for(int j=0; j<intervals.size();j++)
		{
			Interval interval = (Interval)intervals.get(j);
			getIndicator(this.m_STKGraphicsCodeSnippet).addInterval(valueTransform(new Double(interval.getMinimum())), valueTransform(new Double(interval.getMaximum())), IndicatorStyle.BAR, colors[i], this.m_SceneManager);
			i = (i + 1) % colors.length;
		}
	}

	public final void removeIntervals(ArrayList<Interval> intervals)
	throws Throwable
	{
		for(int j=0; j<intervals.size();j++)
		{
			Interval interval = (Interval)intervals.get(j);
			getIndicator(this.m_STKGraphicsCodeSnippet).removeInterval(valueTransform(new Double(interval.getMinimum())), valueTransform(new Double(interval.getMaximum())));
		}
	}

	private static double getVectorMagnitude(double x, double y, double z)
	{
		return Math.sqrt((x * x) + (y * y) + (z * z));
	}
}