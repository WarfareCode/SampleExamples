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

public class ImageFlipCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsScreenOverlay m_Overlay;

    public ImageFlipCodeSnippet(Component c)
	{
		super(c, "Flip an image", "imaging", "ImageFlipCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

		IAgStkGraphicsSceneManager manager = null;
        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgStkGraphicsScreenOverlayCollectionBase overlayManager = null;
        overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

        // The URI can be a file path, http, https, or ftp location
        String filePath = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"f-22a_raptor.png");
        IAgStkGraphicsRaster image = manager.getInitializers().getRaster().initializeWithStringUri(filePath);
        image.flip(AgEStkGraphicsFlipAxis.E_STK_GRAPHICS_FLIP_AXIS_VERTICAL);

        IAgStkGraphicsRendererTexture2D texture = manager.getTextures().fromRaster(image);

        IAgStkGraphicsTextureScreenOverlay overlay = null;
        overlay = manager.getInitializers().getTextureScreenOverlay().initializeDefault();
        Object[] size = new Object[] {new Double(0.2), new Double(0.2), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION.getValue())};
        ((IAgStkGraphicsOverlay)overlay).setSize(size);
        ((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_LEFT);
        overlay.setTexture(texture);

        overlayManager.add((IAgStkGraphicsScreenOverlay)overlay);

        //#endregion
        
        OverlayHelper.addOriginalImageOverlay(this, manager);
        OverlayHelper.labelOverlay(this, manager, (IAgStkGraphicsScreenOverlay)overlay, "Flipped");
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
        IAgStkGraphicsSceneManager manager = null;
        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if(m_Overlay != null)
        {
	        IAgStkGraphicsScreenOverlayCollectionBase overlayManager = null;
	        overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
        	overlayManager.remove(m_Overlay);
	        m_Overlay = null;
        }
        
        OverlayHelper.removeOriginalImageOverlay(manager);
        scene.render();
	}
}