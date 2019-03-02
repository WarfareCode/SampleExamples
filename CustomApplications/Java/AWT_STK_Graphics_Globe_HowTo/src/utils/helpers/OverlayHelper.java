package utils.helpers;

// Java API
import java.awt.*;
import java.awt.font.*;
import java.io.*;
import java.text.AttributedCharacterIterator.Attribute;
import java.util.*;

import agi.core.AgSystemPropertiesHelper;
// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

// Sample API
import utils.*;
import utils.customtypes.*;
import codesnippets.*;

public final class OverlayHelper
{
	private static IAgStkGraphicsTextureScreenOverlay	m_OriginalImageOverlay;
	private static TimeOverlay							m_TimeOverlay;
	private static AltitudeOverlay						m_AltitudeOverlay;
	private static DistanceOverlay						m_DistanceOverlay;
	private static IAgStkGraphicsScreenOverlay			m_TextBox;

	private OverlayHelper()
	{
	}

	public static void addOriginalImageOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager) 
	throws Throwable
	{
		if(m_OriginalImageOverlay == null)
		{
			if(m_OriginalImageOverlay == null)
			{
				IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

	            String fileSep = AgSystemPropertiesHelper.getFileSeparator();
				String raptorPath = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"f-22a_raptor.png");
				IAgStkGraphicsRaster raster = manager.getInitializers().getRaster().initializeWithStringUri(raptorPath);
				IAgStkGraphicsRendererTexture2D texture = manager.getTextures().fromRaster(raster);
				m_OriginalImageOverlay = manager.getInitializers().getTextureScreenOverlay().initializeDefault();
				((IAgStkGraphicsOverlay)m_OriginalImageOverlay).setWidth(0.2);
				((IAgStkGraphicsOverlay)m_OriginalImageOverlay).setWidthUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
				((IAgStkGraphicsOverlay)m_OriginalImageOverlay).setHeight(0.2);
				((IAgStkGraphicsOverlay)m_OriginalImageOverlay).setHeightUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
				m_OriginalImageOverlay.setTexture(texture);
				((IAgStkGraphicsOverlay)m_OriginalImageOverlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER);
				labelOverlay( cs, manager, (IAgStkGraphicsScreenOverlay)m_OriginalImageOverlay, "Original");

				overlayManager.add((IAgStkGraphicsScreenOverlay)m_OriginalImageOverlay);
			}
		}
	}

	public static void removeOriginalImageOverlay(IAgStkGraphicsSceneManager manager)
	throws Throwable
	{
		if(m_OriginalImageOverlay != null)
		{
			IAgStkGraphicsScreenOverlayCollectionBase om = null;
			om = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
			om.remove((IAgStkGraphicsScreenOverlay)m_OriginalImageOverlay);
			m_OriginalImageOverlay = null;
		}
	}

	public static void addTimeOverlay(STKGraphicsCodeSnippet cs, AgStkObjectRootClass root) 
	throws Throwable
	{
		if(m_TimeOverlay == null)
		{
			IAgStkGraphicsSceneManager manager = null;
			manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
			m_TimeOverlay = new TimeOverlay(cs, root);
			m_TimeOverlay.setDefaultStyle();
			IAgStkGraphicsScreenOverlayCollectionBase om = null;
			om = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
			om.add(m_TimeOverlay.getRealScreenOverlay());
			positionStatusOverlays(cs, manager);
		}
	}

	public static void addAltitudeOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, AgStkGraphicsSceneClass scene) 
	throws Throwable
	{
		if(m_AltitudeOverlay == null)
		{
			m_AltitudeOverlay = new AltitudeOverlay(cs, manager, scene);
			m_AltitudeOverlay.setDefaultStyle();
			IAgStkGraphicsScreenOverlayCollectionBase om = null;
			om = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
			om.add(m_AltitudeOverlay.getRealScreenOverlay());
			positionStatusOverlays(cs, manager);
		}
	}

	public static void addDistanceOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		if(m_DistanceOverlay == null)
		{
			m_DistanceOverlay = new DistanceOverlay(cs, manager, scene);
			m_DistanceOverlay.setDefaultStyle();
			IAgStkGraphicsScreenOverlayCollectionBase om = null;
			om = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
			om.add(m_DistanceOverlay.getRealScreenOverlay());
			positionStatusOverlays(cs, manager);
		}
		positionStatusOverlays(cs, manager);
	}

	public static void removeTimeOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager)
	throws Throwable
	{
		if(m_TimeOverlay != null)
		{
			IAgStkGraphicsScreenOverlayCollectionBase om = null;
			om = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
			om.remove(m_TimeOverlay.getRealScreenOverlay());
			positionStatusOverlays(cs, manager);
			m_TimeOverlay = null;
		}
	}

	public static void removeAltitudeOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager) 
	throws Throwable
	{
		if(m_AltitudeOverlay != null)
		{
			IAgStkGraphicsScreenOverlayCollectionBase om = null;
			om = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
			om.remove(m_AltitudeOverlay.getRealScreenOverlay());
			positionStatusOverlays(cs, manager);
			m_AltitudeOverlay = null;
		}
	}

	public static void removeDistanceOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager)
	throws Throwable
	{
		if(m_DistanceOverlay != null)
		{
			IAgStkGraphicsScreenOverlayCollectionBase om = null;
			om = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
			om.remove(m_DistanceOverlay.getRealScreenOverlay());
			positionStatusOverlays(cs, manager);
			m_DistanceOverlay = null;
		}

		positionStatusOverlays(cs, manager);
	}

	public static TimeOverlay getTimeDisplay()
	{
		return m_TimeOverlay;
	}

	public static AltitudeOverlay getAltitudeDisplay()
	{
		return m_AltitudeOverlay;
	}

	public static DistanceOverlay getDistanceDisplay()
	{
		return m_DistanceOverlay;
	}

	public static void labelOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, IAgStkGraphicsScreenOverlay screenoverlay, String text) 
	throws AgCoreException, IOException
	{
		Map<Attribute, Object> attributes = new HashMap<Attribute, Object>();
		attributes.put(TextAttribute.FAMILY, "Arial");
		attributes.put(TextAttribute.SIZE, new Float(12F));
		attributes.put(TextAttribute.WEIGHT, TextAttribute.WEIGHT_BOLD);
		Font font = new Font(attributes);

		IAgStkGraphicsTextureScreenOverlay textOverlay = null;
		textOverlay = TextOverlayHelper.createTextOverlay(cs, manager, text, font);
		((IAgStkGraphicsOverlay)textOverlay).setClipToParent(false);

		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)screenoverlay;
		IAgStkGraphicsScreenOverlayCollectionBase socb = null;
		socb = (IAgStkGraphicsScreenOverlayCollectionBase)overlay.getOverlays();
		socb.add((IAgStkGraphicsScreenOverlay)textOverlay);
	}

	/**
	 * Displays a text box.
	 * @param text Text to display.
	 * @throws AgCoreException 
	 * @throws IOException 
	 */
	public static void addTextBox(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, String text)
	throws Throwable
	{
		addTextBox(cs, manager, text, 0D, 0D);
	}

	/**
	 * Displays a text box.
	 * @param text Text to display.
	 * @param xTranslation The x translation of the text box.
	 * @param yTranslation The y translation of the text box.
	 * @throws AgCoreException 
	 * @throws IOException 
	 */
	public static void addTextBox(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, String text, double xTranslation, double yTranslation) 
	throws Throwable
	{
		IAgStkGraphicsScreenOverlayCollectionBase overlayManager = null;
		overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

		Map<Attribute, Object> attributes = new HashMap<Attribute, Object>();
		attributes.put(TextAttribute.FAMILY, "Arial");
		attributes.put(TextAttribute.SIZE, new Float(12F));
		attributes.put(TextAttribute.WEIGHT, TextAttribute.WEIGHT_BOLD);
		Font font = new Font(attributes);

		IAgStkGraphicsTextureScreenOverlay overlay = null;
		overlay = TextOverlayHelper.createTextOverlay(cs, manager, text, font);
		((IAgStkGraphicsOverlay)overlay).setBorderSize(2);
		((IAgStkGraphicsOverlay)overlay).setBorderColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));

        Object[] overlayPosition = (Object[])((IAgStkGraphicsOverlay)overlay).getPosition_AsObject();
        Object[] overlaySize = (Object[])((IAgStkGraphicsOverlay)overlay).getSize_AsObject();

        IAgStkGraphicsScreenOverlay baseOverlay = manager.getInitializers().getScreenOverlay().initializeWithPosAndSize(overlayPosition, overlaySize);
        ((IAgStkGraphicsOverlay)baseOverlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_RIGHT);
        ((IAgStkGraphicsOverlay)baseOverlay).setTranslationX(xTranslation);
        ((IAgStkGraphicsOverlay)baseOverlay).setTranslationY(yTranslation);
        ((IAgStkGraphicsOverlay)baseOverlay).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLACK));
        ((IAgStkGraphicsOverlay)baseOverlay).setTranslucency(0.5f);

        IAgStkGraphicsScreenOverlayCollectionBase baseOverlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)(((IAgStkGraphicsOverlay)baseOverlay).getOverlays());
        baseOverlayManager.add((IAgStkGraphicsScreenOverlay)overlay);

        m_TextBox = baseOverlay;

        ((IAgStkGraphicsOverlay)m_TextBox).setPosition(new Object[] { new Integer(5), new Integer(5), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()) });
        overlayManager.add(m_TextBox);
	}

	/**
	 * Remove the text box associated with the given snippet.
	 * @throws AgCoreException 
	 */
	public static void removeTextBox(IAgStkGraphicsSceneManager manager)
	throws Throwable
	{
		IAgStkGraphicsScreenOverlayCollectionBase om = null;
		om = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
		if(m_TextBox != null)
		{
			om.remove(m_TextBox);
			m_TextBox = null;
		}
	}

	private static void positionStatusOverlays(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager)
	throws Throwable
	{
		if(m_TimeOverlay != null)
		{
			m_TimeOverlay.update(cs, manager, m_TimeOverlay.getValue());
			
			Object[] pos = new Object[] 
			{ 
				new Double(5D), 
				new Double(5D), 
				new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), 
				new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())
			};
			
			m_TimeOverlay.getOverlay().setPosition(pos);
		}
		if(m_AltitudeOverlay != null)
		{
			m_AltitudeOverlay.update(cs, manager, m_AltitudeOverlay.getValue());

			Object[] pos = new Object[] 
			{ 	
				new Double(5D), 
				new Double(5 + ((m_TimeOverlay == null) ? 0 : m_TimeOverlay.getOverlay().getHeight() + 5)),
				new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), 
				new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())
			};
			
			m_AltitudeOverlay.getOverlay().setPosition(pos);
		}
		if(m_DistanceOverlay != null)
		{
			m_DistanceOverlay.update(cs, manager, m_DistanceOverlay.getValue());
			
			Object[] pos = new Object[] 
			{ 	
				new Double(5D), 
				new Double(5 + ((m_TimeOverlay == null) ? 0 : m_TimeOverlay.getOverlay().getHeight() + 5) + ((m_AltitudeOverlay == null) ? 0 : m_AltitudeOverlay.getOverlay().getHeight() + 5)),
				new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), 
				new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue())
			};

			m_DistanceOverlay.getOverlay().setPosition(pos);
		}
	}
}