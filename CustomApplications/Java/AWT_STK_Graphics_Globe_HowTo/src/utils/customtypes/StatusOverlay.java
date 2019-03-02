package utils.customtypes;

// Java API
import java.awt.*;

// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkgraphics.*;

// Sample API
import utils.helpers.*;
import codesnippets.*;

public abstract class StatusOverlay
// extends AgStkGraphicsScreenOverlay
{
	protected STKGraphicsCodeSnippet	m_STKGraphicsCodeSnippet;
	private IAgStkGraphicsOverlay	m_Overlay;

	protected StatusOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, boolean isIndicatorHorizontal, Object minimum, Object maximum)
	throws AgCoreException
	{
		this.initialize(cs, manager, isIndicatorHorizontal, minimum, maximum, null, null);
	}

	protected StatusOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, boolean isIndicatorHorizontal, Object minimum, Object maximum, String minimumLabel, String maximumLabel)
	throws AgCoreException
	{
		this.initialize(cs, manager, isIndicatorHorizontal, minimum, maximum, minimumLabel, maximumLabel);
	}

	protected void initialize(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, boolean isIndicatorHorizontal, Object minimum, Object maximum, String minimumLabel, String maximumLabel)
	throws AgCoreException
	{
		this.m_STKGraphicsCodeSnippet = cs;
		this.m_SceneManager = manager;

		IAgStkGraphicsFactoryAndInitializers initrs = this.m_SceneManager.getInitializers();
		IAgStkGraphicsScreenOverlayFactory so = initrs.getScreenOverlay();
		this.m_Overlay = (IAgStkGraphicsOverlay)so.initializeDefault(5, 5, 40, 40);

		this.m_IsIndicatorHorizontal = isIndicatorHorizontal;
		this.m_Minimum = minimum;
		this.m_Maximum = maximum;
		this.m_MinText = minimumLabel;
		this.m_MaxText = maximumLabel;
		
		this.m_LastValue = this.m_Minimum;
	}

	public final void setDefaultStyle()
	throws AgCoreException
	{
		this.m_Overlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_LEFT);
		this.m_Overlay.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLACK));
		this.m_Overlay.setTranslucency(0.5f);
		this.m_Overlay.setBorderColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
		this.m_Overlay.setBorderSize(2);
		this.m_Overlay.setBorderTranslucency(0f);
	}

	public IAgStkGraphicsOverlay getOverlay()
	{
		return this.m_Overlay;
	}

	public IAgStkGraphicsScreenOverlay getRealScreenOverlay()
	{
		return (IAgStkGraphicsScreenOverlay)this.m_Overlay;
	}

	public abstract double valueTransform(Object value)
	throws Throwable;

	public abstract Object getValue()
	throws Throwable;

	public abstract String getText()
	throws Throwable;

	public final void update(IAgStkGraphicsSceneManager manager, Object newValue)
	throws Throwable
	{
		this.update(this.m_STKGraphicsCodeSnippet, manager, newValue);
	}
	
	public final void update(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, Object newValue)
	throws Throwable
	{
		Object lastValue = this.getLastValue();
		double lastValueTransform = this.valueTransform(lastValue);
		double newValueTransform = this.valueTransform(newValue);
		double diffValueTransform = lastValueTransform - newValueTransform;
		if(diffValueTransform != 0)
		{
			Object value = this.getValue();
			double transformValue = this.valueTransform(value);

			IndicatorOverlay io = this.getIndicator(cs);
			io.setValue(transformValue);

			IAgStkGraphicsTextureScreenOverlay tso = this.getTextOverlay(cs);
			String text = this.getText();
			TextOverlayHelper.updateTextOverlay(cs, manager, tso, text, m_Font);

			this.setLastValue(newValue);

			if(this.m_MinTextOverlay != null)
			{
				((IAgStkGraphicsOverlay)this.m_MinTextOverlay).bringToFront();
			}

			if(this.m_MaxTextOverlay != null)
			{
				((IAgStkGraphicsOverlay)this.m_MaxTextOverlay).bringToFront();
			}
		}
	}

	protected final IndicatorOverlay getIndicator(STKGraphicsCodeSnippet cs)
	throws Throwable
	{
		if(this.m_Indicator == null)
		{
			if(this.m_IsIndicatorHorizontal)
			{
				IAgStkGraphicsOverlay o = (IAgStkGraphicsOverlay)getTextOverlay(cs);
				double w = o.getWidth();
				double h = o.getHeight();
				double vmin = valueTransform(this.m_Minimum);
				double vmax = valueTransform(this.m_Maximum);

				this.m_Indicator = new IndicatorOverlay(0D, 0D, w, 20D, vmin, vmax, m_IsIndicatorHorizontal, IndicatorStyle.MARKER, this.m_SceneManager);
				this.m_Indicator.getOverlay().setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_CENTER);

				double ih = this.m_Indicator.getOverlay().getHeight();
				Object[] oldSize = (Object[])this.m_Overlay.getSize_AsObject();
				Object[] newSize = new Object[] {new Double(w), new Double(h + ih), oldSize[2], oldSize[3]};
				this.m_Overlay.setSize(newSize);
			}
			else
			{
				m_Indicator = new IndicatorOverlay(0D, 0D, 30D, ((IAgStkGraphicsOverlay)this.getTextOverlay(cs)).getHeight(), valueTransform(m_Minimum), valueTransform(m_Maximum),
				m_IsIndicatorHorizontal, IndicatorStyle.MARKER, m_SceneManager);
				m_Indicator.getOverlay().setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER_LEFT);
				Object[] oldSize = (Object[])this.getOverlay().getSize_AsObject();
				Object[] newSize = new Object[] {new Double(((IAgStkGraphicsOverlay)getTextOverlay(cs)).getWidth() + m_Indicator.getOverlay().getWidth()), new Double(((IAgStkGraphicsOverlay)getTextOverlay(cs)).getHeight()),
				oldSize[2], oldSize[3]};
				this.getOverlay().setSize(newSize);
			}
			m_Indicator.setValue(valueTransform(getValue()));
			m_Indicator.getOverlay().setBorderSize(1);
			m_Indicator.getOverlay().setBorderColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
			m_Indicator.getOverlay().setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLACK));
			m_Indicator.getOverlay().setTranslucency(0.5f);
			((AgStkGraphicsScreenOverlayCollectionClass)this.getOverlay().getOverlays()).add((IAgStkGraphicsScreenOverlay)m_Indicator.getOverlay());
			if(m_MinText != null && m_MinText.length() > 0 && m_MaxText != null && m_MaxText.length() > 0)
			{
				m_MinTextOverlay = TextOverlayHelper.createTextOverlay(cs, this.m_SceneManager, m_MinText, m_LabelFont);
				m_MaxTextOverlay = TextOverlayHelper.createTextOverlay(cs, this.m_SceneManager, m_MaxText, m_LabelFont);

				((IAgStkGraphicsOverlay)m_MinTextOverlay).setOrigin(m_IsIndicatorHorizontal ? AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER_LEFT
				: AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_CENTER);
				((IAgStkGraphicsOverlay)m_MaxTextOverlay).setOrigin(m_IsIndicatorHorizontal ? AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER_RIGHT
				: AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_CENTER);

				IAgStkGraphicsScreenOverlayCollectionBase indicatorOverlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)m_Indicator.getOverlay().getOverlays();
				indicatorOverlayManager.add((IAgStkGraphicsScreenOverlay)m_MinTextOverlay);
				indicatorOverlayManager.add((IAgStkGraphicsScreenOverlay)m_MaxTextOverlay);
			}
		}
		return m_Indicator;
	}

	final protected void setIndicator(IndicatorOverlay value)
	{
		m_Indicator = value;
	}

	final protected IAgStkGraphicsTextureScreenOverlay getTextOverlay(STKGraphicsCodeSnippet cs)
	throws Throwable
	{
		if(m_TextOverlay == null)
		{
			m_TextOverlay = TextOverlayHelper.createTextOverlay(cs, this.m_SceneManager, getText(), m_Font);
			((IAgStkGraphicsOverlay)m_TextOverlay).setBorderSize(1);
			((IAgStkGraphicsOverlay)m_TextOverlay).setBorderColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
			if(m_IsIndicatorHorizontal)
			{
				((IAgStkGraphicsOverlay)m_TextOverlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_CENTER);
			}
			else
			{
				((IAgStkGraphicsOverlay)m_TextOverlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER_RIGHT);
			}
		}
		((AgStkGraphicsScreenOverlayCollectionClass)this.getOverlay().getOverlays()).add((IAgStkGraphicsScreenOverlay)m_TextOverlay);
		return m_TextOverlay;
	}

	final protected void setTextOverlay(IAgStkGraphicsTextureScreenOverlay value)
	{
		m_TextOverlay = value;
	}

	final protected Object getLastValue()
	{
		return m_LastValue;
	}

	final protected void setLastValue(Object value)
	{
		m_LastValue = value;
	}

	final protected Font getFont()
	{
		return m_Font;
	}

	final protected void setFont(Font value)
	{
		m_Font = value;
	}

	private boolean								m_IsIndicatorHorizontal	= false;
	private Font								m_Font					= new Font("Arial", Font.BOLD, 10);
	private Font								m_LabelFont				= new Font("Arial Narrow", Font.BOLD, 8);
	private String								m_MinText;
	private String								m_MaxText;

	private IAgStkGraphicsTextureScreenOverlay	m_MinTextOverlay		= null;
	private IAgStkGraphicsTextureScreenOverlay	m_MaxTextOverlay		= null;
	private IAgStkGraphicsTextureScreenOverlay	m_TextOverlay			= null;
	private IndicatorOverlay					m_Indicator				= null;

	private Object								m_LastValue;
	private Object								m_Minimum;
	private Object								m_Maximum;

	private IAgStkGraphicsSceneManager			m_SceneManager;
}