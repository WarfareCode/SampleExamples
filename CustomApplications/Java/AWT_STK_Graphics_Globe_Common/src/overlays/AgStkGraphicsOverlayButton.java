package overlays;

// Java API
import java.awt.*;
import java.net.*;

import javax.swing.event.*;

// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;
// Sample API
import overlays.toolbars.*;
import overlays.toolbars.images.AgStkGraphicsOverlayToolbarImagesHelper;

public class AgStkGraphicsOverlayButton
{
	// Members
	public final static int						DEFAULT_WIDTH	= 35;

	private float								m_MouseEnterTranslucency;
	private float								m_MouseExitTranslucency;
	private IAgStkGraphicsTextureScreenOverlay	m_Overlay;

	private boolean								m_State;
	private boolean								m_Enabled;
	private String								m_EnabledImage;
	private String								m_DisabledImage;

	private int									m_Offset;

	private IAgStkGraphicsSceneManager			m_Manager;

	private EventListenerList					m_EventListenerList;
	private int									m_EventType;

	public AgStkGraphicsOverlayButton(String imageName, int xOffset, double panelWidth, AgStkObjectRootClass root)
	throws AgCoreException
	{
		initialize(imageName, xOffset, panelWidth, root);
	}

	public AgStkGraphicsOverlayButton(String imageName, int xOffset, double panelWidth, double scale, double rotate, AgStkObjectRootClass root)
	throws AgCoreException
	{
		this.initialize(imageName, xOffset, panelWidth, root);

		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Overlay;
		overlay.setScale(scale);
		overlay.setRotationAngle(rotate);
	}

	private void initialize(String imageName, int xOffset, double panelWidth, AgStkObjectRootClass root)
	throws AgCoreException
	{
		try
		{
			URL imagePathURL = AgStkGraphicsOverlayToolbarImagesHelper.class.getResource(imageName);
			URI imagePathURI = new URI(imagePathURL.toString());
			String imagePath = imagePathURI.getRawPath();
			
			this.m_Manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

			this.m_MouseEnterTranslucency = 0.01f;
			this.m_MouseExitTranslucency = 0.25f;

			this.m_Overlay = this.m_Manager.getInitializers().getTextureScreenOverlay().initializeDefault();

			IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Overlay;
			overlay.setX(((double)xOffset) / panelWidth);
			overlay.setXUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
			overlay.setWidth(((double)DEFAULT_WIDTH) / panelWidth);
			overlay.setWidthUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
			overlay.setHeight(1D);
			overlay.setHeightUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
			overlay.setTranslucency(this.m_MouseExitTranslucency);

			IAgStkGraphicsTexture2DFactory textureFactory = this.m_Manager.getTextures();
			IAgStkGraphicsRendererTexture2D texture = textureFactory.loadFromStringUri("file://" + imagePath);
			this.m_Overlay.setTexture(texture);

			this.m_Enabled = true;
			this.m_DisabledImage = imagePath;
			this.m_EnabledImage = this.m_DisabledImage;

			this.m_Offset = xOffset;

			this.m_EventListenerList = new EventListenerList();
		}
		catch(URISyntaxException e)
		{
			throw new AgCoreException(e);
		}
	}

	public void setEventType(int eventType)
	{
		this.m_EventType = eventType;
	}

	public IAgStkGraphicsScreenOverlay getOverlay()
	{
		return (IAgStkGraphicsScreenOverlay)this.m_Overlay;
	}

	// Sets both the enabled image and disabled image to the input image
	public void setTexture(String imageName)
	throws AgCoreException
	{
		try
		{
			URL imagePathURL = AgStkGraphicsOverlayToolbarImagesHelper.class.getResource(imageName);
			URI imagePathURI = new URI(imagePathURL.toString());
			this.m_EnabledImage = imagePathURI.getRawPath();
			this.m_DisabledImage = this.m_EnabledImage;
		}
		catch(URISyntaxException e)
		{
			throw new AgCoreException(e);
		}
	}

	// Sets both an enabled image and a disabled image for an on/off button
	public void setTexture(String enabledImageName, String disabledImageName)
	throws AgCoreException
	{
		try
		{
			URL eimagePathURL = AgStkGraphicsOverlayToolbarImagesHelper.class.getResource(enabledImageName);
			URI eimagePathURI = new URI(eimagePathURL.toString());
			this.m_EnabledImage = eimagePathURI.getRawPath();

			URL dimagePathURL = AgStkGraphicsOverlayToolbarImagesHelper.class.getResource(disabledImageName);
			URI dimagePathURI = new URI(dimagePathURL.toString());
			this.m_DisabledImage = dimagePathURI.getRawPath();
		}
		catch(URISyntaxException e)
		{
			throw new AgCoreException(e);
		}
	}

	// Sets the on/off texture of a button
	public void setState(boolean state)
	throws AgCoreException
	{
		this.m_State = state;

		if(state)
		{
			IAgStkGraphicsTexture2DFactory textureFactory = this.m_Manager.getTextures();
			IAgStkGraphicsRendererTexture2D texture = textureFactory.loadFromStringUri("file://" + this.m_EnabledImage);
			this.m_Overlay.setTexture(texture);
		}
		else
		{
			IAgStkGraphicsTexture2DFactory textureFactory = this.m_Manager.getTextures();
			IAgStkGraphicsRendererTexture2D texture = textureFactory.loadFromStringUri("file://" + this.m_DisabledImage);
			this.m_Overlay.setTexture(texture);
		}
	}

	// Enables or disables a button
	public void setEnabled(boolean enabled)
	throws AgCoreException
	{
		this.m_Enabled = enabled;
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Overlay;

		if(enabled)
		{
			overlay.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
		}
		else
		{
			overlay.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GRAY));
		}
	}

	public void resize(double panelWidth)
	throws AgCoreException
	{
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Overlay;

		overlay.setWidth(((double)DEFAULT_WIDTH) / panelWidth);
		overlay.setWidthUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
		overlay.setHeight(1D);
		overlay.setHeightUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);

		overlay.setX(((double)this.m_Offset) / ((double)panelWidth));
		overlay.setXUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
	}

	// Event handlers
	public void mouseEnter()
	throws AgCoreException
	{
		if(this.m_Enabled)
		{
			IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Overlay;
			overlay.setTranslucency(this.m_MouseEnterTranslucency);
			if(this.m_Manager.getScenes().getCount() > 0)
			{
				this.m_Manager.getScenes().getItem(0).render();
			}
		}
	}

	public void mouseLeave()
	throws AgCoreException
	{
		if(this.m_Enabled)
		{
			IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Overlay;
			overlay.setTranslucency(this.m_MouseExitTranslucency);
			overlay.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
			if(this.m_Manager.getScenes().getCount() > 0)
			{
				this.m_Manager.getScenes().getItem(0).render();
			}
		}
	}

	public void mouseDown()
	throws AgCoreException
	{
		if(this.m_Enabled)
		{
			IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Overlay;
			overlay.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.DARK_GRAY));
			if(this.m_Manager.getScenes().getCount() > 0)
			{
				this.m_Manager.getScenes().getItem(0).render();
			}
		}
	}

	public void mouseUp()
	throws AgCoreException
	{
		if(this.m_Enabled)
		{
			IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Overlay;
			overlay.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
			if(this.m_Manager.getScenes().getCount() > 0)
			{
				this.m_Manager.getScenes().getItem(0).render();
			}
		}
	}

	public void mouseClick()
	throws AgCoreException
	{
		if(this.m_Enabled)
		{
			setState(!this.m_State);

			// Guaranteed to return a non-null array
			Object[] listeners = this.m_EventListenerList.getListenerList();

			// Process the listeners last to first, notifying
			// those that are interested in this event
			for(int i = listeners.length - 2; i >= 0; i -= 2)
			{
				if(listeners[i] == IAgStkGraphicsOverlayToolBarEventsListener.class)
				{
					// Lazily create the event:
					AgStkGraphicsOverlayToolBarEvent e = null;
					e = new AgStkGraphicsOverlayToolBarEvent(this, this.m_EventType);
					((IAgStkGraphicsOverlayToolBarEventsListener)listeners[i + 1]).onGraphicsOverlayToolBarAction(e);
				}
			}
		}
	}

	public void addIAgStkGraphicsOverlayToolBarEventsListener(IAgStkGraphicsOverlayToolBarEventsListener l)
	{
		if(l != null)
		{
			this.m_EventListenerList.add(IAgStkGraphicsOverlayToolBarEventsListener.class, l);
		}
	}

	public void removeIAgStkGraphicsOverlayToolBarEventsListener(IAgStkGraphicsOverlayToolBarEventsListener l)
	{
		if(l != null)
		{
			this.m_EventListenerList.remove(IAgStkGraphicsOverlayToolBarEventsListener.class, l);
		}
	}
}