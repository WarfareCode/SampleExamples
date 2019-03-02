package codesnippets.screenoverlays;

//#region Imports

//Java API
import java.awt.*;
import java.awt.color.*;
import java.awt.image.*;
import javax.imageio.*;
import java.io.*;

//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import codesnippets.*;

//#endregion

public class OverlaysMemoryCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsScreenOverlay m_Overlay;
    
    public OverlaysMemoryCodeSnippet(Component c)
	{
		super(c, "Draw an overlay using an image in memory", "screenoverlays", "OverlaysMemoryCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		String imagePath = DataPaths.getDataPaths().getSampleProjectPath("OverlayFromMemory.png");
		
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgStkGraphicsScreenOverlayCollectionBase overlayManager = null;
        overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

		int width = 256;
        int height = 256;

        writeImage(imagePath, width, height);
        
        IAgStkGraphicsRasterFactory rf = null;
		rf = manager.getInitializers().getRaster();

		IAgStkGraphicsRaster img = null;
        img = rf.initializeWithStringUriXYWidthAndHeight(imagePath, 0, 0, width, height);

        IAgStkGraphicsTextureScreenOverlay overlay = manager.getInitializers().getTextureScreenOverlay().initializeWithXYWidthHeight(0, 0, width, height);
        overlay.setTexture(manager.getTextures().fromRaster((IAgStkGraphicsRaster)img));
        ((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER);

        overlayManager.add((IAgStkGraphicsScreenOverlay)overlay);
        scene.render();
        //#endregion
        m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
        File f = new File(imagePath);
        if(f.exists()) f.delete();
	}

	private void writeImage(String imagePath, int width, int height) 
	throws IOException
	{
        ProcedurallyGeneratedTexture tex = new ProcedurallyGeneratedTexture(width);
		byte[] pixels = tex.next();
		
		ComponentSampleModel sm = new ComponentSampleModel(DataBuffer.TYPE_BYTE, width, width, 4, 4 * width, new int[] {
	    0,
	    1,
	    2,
	    3,
		});
	
		DataBufferByte db = new DataBufferByte(pixels, pixels.length);
		WritableRaster raster = WritableRaster.createWritableRaster(sm, db, null);
	
		ColorSpace colorSpace = ColorSpace.getInstance(ColorSpace.CS_sRGB);
		ColorModel cm = new ComponentColorModel(colorSpace, true, false, Transparency.TRANSLUCENT, DataBuffer.TYPE_BYTE);
	
		BufferedImage image = new BufferedImage(cm, raster, false, null);
	
		ImageIO.write(image, "PNG", new File(imagePath));
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        // Use current view.
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

		if(m_Overlay != null)
		{
			overlayManager.remove(m_Overlay);
			m_Overlay = null;
		}
		
		scene.render();
	}
}