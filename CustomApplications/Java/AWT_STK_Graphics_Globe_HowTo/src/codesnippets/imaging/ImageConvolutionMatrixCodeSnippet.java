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

public class ImageConvolutionMatrixCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsScreenOverlay m_Overlay;

    public ImageConvolutionMatrixCodeSnippet(Component c)
	{
		super(c, "Blur an image with a convolution matrix", "imaging", "ImageConvolutionMatrixCodeSnippet.java");
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

        // Set convolution matrix to blur
        Object[] kernel = new Object[]
        {
                /*$m1n1$m1n1$*/new Integer(1), /*$m1n2$m1n2$*/new Integer(1), /*$m1n3$m1n3$*/new Integer(1),
                /*$m2n1$m2n1$*/new Integer(1), /*$m2n2$m2n2$*/new Integer(1), /*$m2n3$m2n3$*/new Integer(1),
                /*$m3n1$m3n1$*/new Integer(1), /*$m3n2$m3n2$*/new Integer(1), /*$m3n3$m3n3$*/new Integer(1)
        };
        IAgStkGraphicsConvolutionFilter convolutionMatrix = manager.getInitializers().getConvolutionFilter().initializeWithKernelAndDivisor(kernel, 9.0);
        image.applyInPlace((IAgStkGraphicsRasterFilter)convolutionMatrix);

        IAgStkGraphicsRendererTexture2D texture = manager.getTextures().fromRaster(image);

        // Display the image using a screen overlay
        IAgStkGraphicsTextureScreenOverlay overlay = manager.getInitializers().getTextureScreenOverlay().initializeDefault();
        ((IAgStkGraphicsOverlay)overlay).setWidth(0.2);
        ((IAgStkGraphicsOverlay)overlay).setWidthUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
        ((IAgStkGraphicsOverlay)overlay).setHeight(0.2);
        ((IAgStkGraphicsOverlay)overlay).setHeightUnit(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_FRACTION);
        ((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_LEFT);
        overlay.setTexture(texture);

        overlayManager.add((IAgStkGraphicsScreenOverlay)overlay);
        //#endregion
        
        OverlayHelper.addOriginalImageOverlay(this, manager);
        OverlayHelper.labelOverlay(this, manager, (IAgStkGraphicsScreenOverlay)overlay, "Blurred");
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