package utils.customtypes;

// Java API
import java.awt.*;
import java.util.*;

// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkgraphics.*;

public class IndicatorOverlay
{
	private IAgStkGraphicsScreenOverlay	m_Foreground;
	private ArrayList<IntervalOverlay>	m_Intervals;

	private double						m_Value;
	private double						m_MarkerSize;

	private Interval					m_Range				= new Interval();
	private int							m_IndicatorStyle	= IndicatorStyle.getDefault();
	private boolean						m_IsHorizontal;

	private IAgStkGraphicsSceneManager	m_SceneManager;
	private IAgStkGraphicsOverlay		m_Overlay;

	public IndicatorOverlay(Object[] position, Object[] size, Interval range, boolean isHorizontal, int indicatorStyle, IAgStkGraphicsSceneManager manager)
	throws AgCoreException
	{
		this.initialize(position, size, range, isHorizontal, indicatorStyle, manager);
	}

	public IndicatorOverlay(double xPixels, double yPixels, double widthPixels, double heightPixels, double minValue, double maxValue, boolean isHorizontal, int indicatorStyle,
	IAgStkGraphicsSceneManager manager)
	throws AgCoreException
	{
		Object[] position = new Object[] {new Double(xPixels), new Double(yPixels), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())};
		Object[] size = new Object[] {new Double(widthPixels), new Double(heightPixels), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()),
		new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())};

		this.initialize(position, size, new Interval(minValue, maxValue), isHorizontal, indicatorStyle, manager);
	}

	public void initialize(Object[] position, Object[] size, Interval range, boolean isHorizontal, int indicatorStyle, IAgStkGraphicsSceneManager manager)
	throws AgCoreException
	{
		this.m_SceneManager = manager;
		IAgStkGraphicsFactoryAndInitializers fai = this.m_SceneManager.getInitializers();
		IAgStkGraphicsScreenOverlayFactory sof = fai.getScreenOverlay();
		IAgStkGraphicsScreenOverlay so = sof.initializeWithPosAndSize(position, size);
		this.m_Overlay = (IAgStkGraphicsOverlay)so;

		this.m_IsHorizontal = isHorizontal;
		this.m_Intervals = new ArrayList<IntervalOverlay>();
		this.m_Range = checkValues(range);
		this.m_Value = m_Range.getMinimum();
		this.m_IndicatorStyle = indicatorStyle;
		this.m_MarkerSize = 1D;
	}

	public IAgStkGraphicsOverlay getOverlay()
	{
		return this.m_Overlay;
	}

	public IAgStkGraphicsScreenOverlay getRealScreenOverlay()
	{
		return (IAgStkGraphicsScreenOverlay)this.m_Overlay;
	}

	public final Interval getRange()
	{
		return this.m_Range;
	}

	public final double getValue()
	{
		return this.m_Value;
	}

	public final void setValue(double value)
	throws AgCoreException
	{
		if(value == Double.POSITIVE_INFINITY)
			m_Value = Double.MAX_VALUE;
		else if(value == Double.NEGATIVE_INFINITY)
			m_Value = -Double.MAX_VALUE;
		else
			m_Value = value;
		updateForeground();
		updateIntervals();
	}

	public final Color getForegroundColor()
	throws AgCoreException
	{
		IAgStkGraphicsOverlay o = null;
		o = (IAgStkGraphicsOverlay)this.m_Foreground;
		long coreColor = o.getColor();
		Color awtColor = AgAwtColorTranslator.fromLongtoAWT(coreColor);
		return awtColor;
	}

	public final void setForegroundColor(Color awtColor)
	throws AgCoreException
	{
		AgCoreColor coreColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);
		IAgStkGraphicsOverlay o = null;
		o = (IAgStkGraphicsOverlay)this.m_Foreground;
		o.setColor(coreColor);
	}

	public final double getMarkerSize()
	{
		return this.m_MarkerSize;
	}

	public final void setMarkerSize(double value)
	throws AgCoreException
	{
		this.m_MarkerSize = value;
		switch(this.m_IndicatorStyle)
		{
			case IndicatorStyle.BAR:
				break;
			case IndicatorStyle.MARKER:
				Object[] size = this.getIndicationMarkerSize(this.m_MarkerSize);
				IAgStkGraphicsOverlay o = null;
				o = (IAgStkGraphicsOverlay)this.m_Foreground;
				o.setSize(size);
				break;
			default:
				break;
		}
	}

	private final void createForeground()
	throws AgCoreException
	{
		Object[] foregroundPosition = new Object[] {new Double(0), new Double(0), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())};
		Object[] foregroundSize = new Object[] {new Double(0), new Double(0), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())};
		m_Foreground = m_SceneManager.getInitializers().getScreenOverlay().initializeWithPosAndSize(foregroundPosition, foregroundSize);

		AgCoreColor coreColor = AgCoreColor.WHITE_COLOR;
		switch(m_IndicatorStyle)
		{
			case IndicatorStyle.BAR:
				coreColor = AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN);
				((IAgStkGraphicsOverlay)m_Foreground).setColor(coreColor);
				break;
			case IndicatorStyle.MARKER:
				coreColor =  AgAwtColorTranslator.fromAWTtoCoreColor(new Color(0x32CD32)); // lime green
				((IAgStkGraphicsOverlay)m_Foreground).setColor(coreColor);
				((IAgStkGraphicsOverlay)m_Foreground).setBorderSize(1);
				coreColor = AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLACK);
				((IAgStkGraphicsOverlay)m_Foreground).setBorderColor(coreColor);
				((IAgStkGraphicsOverlay)m_Foreground).setBorderTranslucency(0.5f);
				break;
			default:
				break;
		}
		((IAgStkGraphicsOverlay)m_Foreground).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_LEFT);
		IAgStkGraphicsScreenOverlayCollectionBase om = null;
		om = (IAgStkGraphicsScreenOverlayCollectionBase)this.m_Overlay.getOverlays();
		om.add(m_Foreground);
		updateForeground();
	}

	private final void updateForeground()
	throws AgCoreException
	{
		if(this.m_Foreground == null)
		{
			createForeground();
		}
		switch(this.m_IndicatorStyle)
		{
			case IndicatorStyle.BAR:
				((IAgStkGraphicsOverlay)this.m_Foreground).setSize(getIndicationBarSize(getValue()));
				((IAgStkGraphicsOverlay)this.m_Foreground).setPosition(getIndicationPosition(0D));
				break;
			case IndicatorStyle.MARKER:
				((IAgStkGraphicsOverlay)this.m_Foreground).setSize(getIndicationMarkerSize(getMarkerSize()));
				((IAgStkGraphicsOverlay)this.m_Foreground).setPosition(getIndicationPosition(getValue()));
				((IAgStkGraphicsOverlay)this.m_Foreground).bringToFront();
				break;
			default:
				break;
		}
	}

	private final void updateIntervals() 
	throws AgCoreException
	{
		for(int i=0; i<m_Intervals.size(); i++)
		{
			IntervalOverlay interval = (IntervalOverlay)m_Intervals.get(i);
			switch(interval.getIntervalStyle())
			{
				case IndicatorStyle.BAR:
					((IAgStkGraphicsOverlay)interval.getMarker()).setTranslucency(0.6f);
					if(interval.getRange().getMinimum() < getValue() && getValue() < interval.getRange().getMaximum())
					{
						((IAgStkGraphicsOverlay)interval.getMarker()).setTranslucency(0.2f);
					}
					break;
				case IndicatorStyle.MARKER:
					((IAgStkGraphicsOverlay)interval.getMarker()).setSize(getIndicationMarkerSize(getMarkerSize()));
					break;
				default:
					break;
			}
		}
	}

	/**
	 * Turns a value into a fraction of this indication's entire range. Returns [0.0-1.0]
	 */
	private final double getIndicationFraction(double value)
	{
		return Math.min(Math.max(getRange().getMinimum(), value), getRange().getMaximum()) / (getRange().getMaximum() - getRange().getMinimum());
	}

	/**
	 * Calculate indication bar size based on the given value.
	 */
	private final Object[] getIndicationBarSize(double value)
	{
		value = Math.max(getIndicationFraction(value), 0.0001);
		// can't be zero
		if(m_IsHorizontal)
		{
			return new Object[] {new Double(value), new Double(1.0), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue())};
		}
		else
		{
			return new Object[] {new Double(1.0), new Double(value), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue())};
		}
	}

	/**
	 * Calculate indication marker size based on the given value.
	 */
	private final Object[] getIndicationMarkerSize(double markerWidthPixels)
	{
		markerWidthPixels = Math.max(markerWidthPixels, 0.0001);

		// can't be zero
		if(this.m_IsHorizontal)
		{
			return new Object[] {new Double(markerWidthPixels), new Double(1.0), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue())};
		}
		else
		{
			return new Object[] {new Double(1.0), new Double(markerWidthPixels), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())};
		}
	}

	/**
	 * Get the indication position based on value and direction.
	 */
	final private Object[] getIndicationPosition(double value)
	{
		if(this.m_IsHorizontal)
		{
			return new Object[] {new Double(getIndicationFraction(value)), new Double(0), 
			new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue()),
			new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue())};
		}
		else
		{
			return new Object[] {new Double(0), new Double(getIndicationFraction(value)), 
			new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue()),
			new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue())};
		}
	}

	static class IntervalOverlay
	{
		private Interval					m_Range;
		private IAgStkGraphicsScreenOverlay	m_Marker;
		private IAgStkGraphicsScreenOverlay	m_MarkerTwo;
		private int							m_IndicatorStyle;

		public IntervalOverlay()
		{

		}

		public IntervalOverlay(Interval range, int style, Color color, IndicatorOverlay parent, IAgStkGraphicsSceneManager manager)
		throws AgCoreException
		{
			this.m_Range = checkValues(range);
			this.m_IndicatorStyle = style;

			// make start and end markers
			switch(m_IndicatorStyle)
			{
				case IndicatorStyle.BAR:
				{
					double rangeSize = Math.min(this.m_Range.getMaximum() - Math.max(this.m_Range.getMinimum(), 0D), parent.getRange().getMaximum() - Math.max(this.m_Range.getMinimum(), 0D));
					Object[] barPosition = parent.getIndicationPosition(this.m_Range.getMinimum());
					Object[] barSize = parent.getIndicationBarSize(rangeSize);
					this.m_Marker = manager.getInitializers().getScreenOverlay().initializeWithPosAndSize(barPosition, barSize);
					((IAgStkGraphicsOverlay)this.m_Marker).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_LEFT);
					((IAgStkGraphicsOverlay)this.m_Marker).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(color));
					((IAgStkGraphicsOverlay)this.m_Marker).setTranslucency(0.6f);
					this.m_MarkerTwo = null;
					break;
				}
				case IndicatorStyle.MARKER:
				{
					Object[] size = parent.getIndicationMarkerSize(parent.getMarkerSize());
					Object[] markerPosition = parent.getIndicationPosition(this.m_Range.getMinimum());

					this.m_Marker = manager.getInitializers().getScreenOverlay().initializeWithPosAndSize(markerPosition, size);
					((IAgStkGraphicsOverlay)this.m_Marker).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_LEFT);
					((IAgStkGraphicsOverlay)this.m_Marker).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(color));

					Object[] markerTwoPosition = parent.getIndicationPosition(m_Range.getMaximum());

					this.m_MarkerTwo = manager.getInitializers().getScreenOverlay().initializeWithPosAndSize(markerTwoPosition, size);
					((IAgStkGraphicsOverlay)this.m_MarkerTwo).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_LEFT);
					((IAgStkGraphicsOverlay)this.m_MarkerTwo).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(color));
					break;
				}
				default:
				{
					m_Marker = null;
					m_MarkerTwo = null;
					break;
				}
			}
		}

		public final Interval getRange()
		{
			return this.m_Range;
		}

		public final IAgStkGraphicsScreenOverlay getMarker()
		{
			return this.m_Marker;
		}

		public final IAgStkGraphicsScreenOverlay getMarkerTwo()
		{
			return this.m_MarkerTwo;
		}

		public final int getIntervalStyle()
		{
			return this.m_IndicatorStyle;
		}

		// public final IntervalOverlay clone()
		// {
		// try
		// {
		// return (IntervalOverlay)super.clone();
		// }
		// catch(CloneNotSupportedException e)
		// {
		// throw new AssertionError();
		// }
		// }
		//
		// @Override
		// public boolean equals(Object obj)
		// {
		// if(obj == this)
		// {
		// return true;
		// }
		// if(!(obj instanceof IntervalOverlay))
		// {
		// return false;
		// }
		// IntervalOverlay that = (IntervalOverlay)obj;
		// return (m_Range == null ? that.m_Range == null : m_Range.equals(that.m_Range)) && (m_Marker == null ? that.m_Marker == null : m_Marker.equals(that.m_Marker))
		// && (m_MarkerTwo == null ? that.m_MarkerTwo == null : m_MarkerTwo.equals(that.m_MarkerTwo)) && m_IntervalStyle == that.m_IntervalStyle;
		// }
		//
		// @Override
		// public int hashCode()
		// {
		// int result = 17;
		// result = 31 * result + (m_Range == null ? 0 : m_Range.hashCode());
		// result = 31 * result + (m_Marker == null ? 0 : m_Marker.hashCode());
		// result = 31 * result + (m_MarkerTwo == null ? 0 : m_MarkerTwo.hashCode());
		// result = 31 * result + (m_IntervalStyle.getValue());
		// return result;
		// }
	}

	public final void addInterval(double minimum, double maximum, int style, IAgStkGraphicsSceneManager manager)
	throws AgCoreException
	{
		Color lightBlue = new Color(0xADD8E6);
		addInterval(minimum, maximum, style, lightBlue, manager);
	}

	public final void addInterval(double minimum, double maximum, int style, Color color, IAgStkGraphicsSceneManager manager)
	throws AgCoreException
	{
		addInterval(new Interval(minimum, maximum), style, color, manager);
	}

	public final void addInterval(Interval range, int style, Color color, IAgStkGraphicsSceneManager manager)
	throws AgCoreException
	{
		range = checkValues(range);
		IntervalOverlay interval = new IntervalOverlay();
		IndicatorOverlay.IntervalOverlay[] out_interval_1 = new IndicatorOverlay.IntervalOverlay[] {null};
		boolean m_Boolean_0 = tryGetInterval(range, out_interval_1);
		interval = out_interval_1[0];
		if(!m_Boolean_0)
		{
			interval = new IntervalOverlay(range, style, color, this, manager);
			m_Intervals.add(interval);

			IAgStkGraphicsScreenOverlayCollectionBase om = null;
			om = (IAgStkGraphicsScreenOverlayCollectionBase)this.m_Overlay.getOverlays();
			om.add(interval.getMarker());

			if(interval.getMarkerTwo() != null)
			{
				om.add(interval.getMarkerTwo());
			}
		}
	}

	public final void removeInterval(double minimum, double maximum)
	throws AgCoreException
	{
		removeInterval(new Interval(minimum, maximum));
	}

	public final void removeInterval(Interval range)
	throws AgCoreException
	{
		range = checkValues(range);
		IntervalOverlay interval = new IntervalOverlay();
		IndicatorOverlay.IntervalOverlay[] out_interval_3 = new IndicatorOverlay.IntervalOverlay[] {null};
		boolean m_Boolean_2 = tryGetInterval(range, out_interval_3);
		interval = out_interval_3[0];
		if(m_Boolean_2)
		{
			((AgStkGraphicsScreenOverlayCollectionClass)this.m_Overlay.getOverlays()).remove(interval.getMarker());
			if(interval.getMarkerTwo() != null)
			{
				((AgStkGraphicsScreenOverlayCollectionClass)this.m_Overlay.getOverlays()).remove(interval.getMarkerTwo());
			}
			m_Intervals.remove(interval);
		}
	}

	final private boolean tryGetInterval(Interval range, IntervalOverlay[] interval)
	{
		for(int index=0; index<m_Intervals.size(); index++)
		{
			IntervalOverlay i = (IntervalOverlay)m_Intervals.get(index);

			if(i.getRange().equals(range))
			{
				interval[0] = i;
				return true;
			}
		}
		interval[0] = new IntervalOverlay();
		return false;
	}

	static private Interval checkValues(Interval range)
	{
		if(Double.isInfinite(range.getMinimum()))
		{
			range.setMinimum(-Double.MAX_VALUE);
		}
		if(Double.isInfinite(range.getMaximum()))
		{
			range.setMaximum(Double.MAX_VALUE);
		}
		return range;
	}
}