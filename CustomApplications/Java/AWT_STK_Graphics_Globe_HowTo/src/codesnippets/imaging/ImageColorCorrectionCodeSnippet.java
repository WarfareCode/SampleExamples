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

public class ImageColorCorrectionCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsScreenOverlay m_Overlay;

    public ImageColorCorrectionCodeSnippet(Component c)
	{
		super(c, "Adjust brightness, contrast, and gamma", "imaging", "ImageColorCorrectionCodeSnippet.java");
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

        // Add brightness, contrast, and gamma correction filters to sequence
        IAgStkGraphicsSequenceFilter sequenceFilter = manager.getInitializers().getSequenceFilter().initializeDefault();
        sequenceFilter.add((IAgStkGraphicsRasterFilter)manager.getInitializers().getBrightnessFilter().initializeWithAdjustment(.1));
        sequenceFilter.add((IAgStkGraphicsRasterFilter)manager.getInitializers().getContrastFilter().initializeWithAdjustment(.2));
        sequenceFilter.add((IAgStkGraphicsRasterFilter)manager.getInitializers().getGammaCorrectionFilter().initializeWithGamma(.9));
        image.applyInPlace((IAgStkGraphicsRasterFilter)sequenceFilter);

        IAgStkGraphicsRendererTexture2D texture = manager.getTextures().fromRaster(image);

        // Display the image using a screen overlay
        IAgStkGraphicsTextureScreenOverlay overlay = manager.getInitializers().getTextureScreenOverlay().initializeDefault();
        ((IAgStkGraphicsOverlay)overlay).setWidth(0.2);
        ((IAgStkGraphicsOverlay)overlay).setWidthUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
        ((IAgStkGraphicsOverlay)overlay).setHeight(0.2);
        ((IAgStkGraphicsOverlay)overlay).setHeightUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
        ((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER_RIGHT);
        overlay.setTexture(texture);

        overlayManager.add((IAgStkGraphicsScreenOverlay)overlay);
        
        //#endregion

        OverlayHelper.addOriginalImageOverlay(this, manager);
        OverlayHelper.labelOverlay(this, manager, (IAgStkGraphicsScreenOverlay)overlay, "Correction Filters");
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