package codesnippets.imaging;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class ImageChannelExtractCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsScreenOverlay m_Overlay;   

    public ImageChannelExtractCodeSnippet(Component c)
	{
		super(c, "Extract the alpha component from an image", "imaging", "ImageChannelExtractCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

        // The URI can be a file path, http, https, or ftp location
        String filePath = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"f-22a_raptor.png");
        IAgStkGraphicsRaster image = manager.getInitializers().getRaster().initializeWithStringUri(filePath);

        // Extract the alpha channel from the image
        IAgStkGraphicsBandExtractFilter channelExtract = manager.getInitializers().getBandExtractFilter().initializeWithBand(AgEStkGraphicsRasterBand.E_STK_GRAPHICS_RASTER_BAND_ALPHA);
        image.applyInPlace((IAgStkGraphicsRasterFilter)channelExtract);

        IAgStkGraphicsRendererTexture2D texture = manager.getTextures().fromRaster(image);

        // Display the image using a screen overlay
        IAgStkGraphicsTextureScreenOverlay overlay = manager.getInitializers().getTextureScreenOverlay().initializeDefault();
        ((IAgStkGraphicsOverlay)overlay).setWidth(0.2);
        ((IAgStkGraphicsOverlay)overlay).setWidthUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
        ((IAgStkGraphicsOverlay)overlay).setHeight(0.2);
        ((IAgStkGraphicsOverlay)overlay).setHeightUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
        ((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_RIGHT);
        overlay.setTexture(texture);

        overlayManager.add((IAgStkGraphicsScreenOverlay)overlay);
        //#endregion
        
        OverlayHelper.addOriginalImageOverlay(this, manager);
        OverlayHelper.labelOverlay(this, manager, (IAgStkGraphicsScreenOverlay)overlay, "Alpha Channel");
        m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
    	IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if(m_Overlay != null)
        {
        	IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
        	overlayManager.remove(m_Overlay);
        	m_Overlay = null;
        }
        
    	OverlayHelper.removeOriginalImageOverlay(manager);
        scene.render();
	}
}