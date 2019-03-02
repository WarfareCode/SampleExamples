package utils.helpers;

// Java API
import java.awt.*;
import java.awt.image.*;
import java.io.*;
import javax.imageio.*;

// AGI Java API
import agi.core.*;
import agi.stkgraphics.*;

// Sample API
import utils.*;
import codesnippets.*;

public final class TextOverlayHelper
{
	private TextOverlayHelper()
	{
	}

	public static IAgStkGraphicsTextureScreenOverlay createTextOverlay
	(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, String text, Font font)
	throws AgCoreException, IOException
	{
        String textBitmapPath = createTextBitmap(cs, text, font);

        IAgStkGraphicsRendererTexture2D textTexture = manager.getTextures().loadFromStringUri(textBitmapPath);
        IAgStkGraphicsRendererTextureTemplate2D textureTemplate = textTexture.getTemplate();
        int width = textureTemplate.getWidth();
        int height = textureTemplate.getHeight();

        IAgStkGraphicsFactoryAndInitializers initrs = manager.getInitializers();
        IAgStkGraphicsTextureScreenOverlayFactory tso = initrs.getTextureScreenOverlay();
    	IAgStkGraphicsTextureFilter2DFactory tf2d = initrs.getTextureFilter2D();
    	IAgStkGraphicsTextureFilter2D nc2e = tf2d.getNearestClampToEdge();
        
        IAgStkGraphicsTextureScreenOverlay textOverlay = tso.initializeWithXYWidthHeight(0, 0, width, height);
        textOverlay.setTexture(textTexture);
        textOverlay.setTextureFilter(nc2e);

        File f = new File(textBitmapPath);
        f.delete();

        return textOverlay;
	}

	public static void updateTextOverlay(STKGraphicsCodeSnippet cs, IAgStkGraphicsSceneManager manager, IAgStkGraphicsTextureScreenOverlay overlay, String text, Font font) 
	throws AgCoreException, IOException
	{
        String textBitmapPath = createTextBitmap(cs, text, font);
        IAgStkGraphicsRendererTexture2D textTexture = manager.getTextures().loadFromStringUri(textBitmapPath);

        IAgStkGraphicsRendererTextureTemplate2D textureTemplate = textTexture.getTemplate();
        int width = textureTemplate.getWidth();
        int height = textureTemplate.getHeight();
        
        File f = new File(textBitmapPath);
        f.delete();

        Object[] size = null;
        size = new Object[] { new Integer(width), new Integer(height), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()), new Integer(AgEStkGraphicsScreenOverlayUnit.E_STK_GRAPHICS_SCREEN_OVERLAY_UNIT_PIXELS.getValue()) };
        ((IAgStkGraphicsOverlay)overlay).setSize(size);
        
        IAgStkGraphicsRendererTexture2D temp = overlay.getTexture();
        overlay.setTexture(textTexture);
        temp.release(); // calling this here improves memory performance
	}

	public static String createTextBitmap(STKGraphicsCodeSnippet cs, String text, Font font) 
	throws IOException, AgCoreException
	{
		Dimension textSize = cs.measureString(text, font);
		BufferedImage textBitmap = new BufferedImage(textSize.width, textSize.height, BufferedImage.TYPE_INT_ARGB);
		Graphics2D gfx = textBitmap.createGraphics();
		gfx.setColor(Color.WHITE);
		gfx.setFont(font);
		String[] splitText = text.split("\n");
		int lineHeight = gfx.getFontMetrics().getAscent() + gfx.getFontMetrics().getDescent();
		int maxAdvance = gfx.getFontMetrics().getMaxAdvance();
		int currentLineY = lineHeight;
		
		for(int i=0; i<splitText.length; i++)
		{
			String textLine = splitText[i];
			gfx.drawString(textLine, maxAdvance, currentLineY);
			currentLineY += lineHeight;
		}

		String fmt = "png";
		String filePath = generateUniqueFilename(fmt);
		File f = new File(filePath);
		ImageIO.write(textBitmap, fmt, f);

	    return filePath;
	}
	
    private static String generateUniqueFilename(String ext) 
    throws AgCoreException
    {
        String filename = DataPaths.getDataPaths().getSampleProjectPath("TextOverlay") + fileNumber + "." + ext;
        fileNumber++;

        File f = new File(filename);
        
        if (!f.exists())
            return filename;
        else
            return generateUniqueFilename(ext);
    }

    private static int fileNumber = 0;
}