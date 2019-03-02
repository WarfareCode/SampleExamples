package codesnippets.screenoverlays;

//#region Imports
import java.awt.*;
import java.awt.image.*;
import java.io.File;

import javax.imageio.ImageIO;

//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class OverlaysTextCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsScreenOverlay	m_Overlay;

	public OverlaysTextCodeSnippet(Component c)
	{
		super(c, "Write text to a texture overlay", "screenoverlays", "OverlaysTextCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		// #region CodeSnippet
		IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
		IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();

		IAgStkGraphicsScreenOverlayManager screenOverlayManager = null;
		screenOverlayManager = sceneManager.getScreenOverlays();

		IAgStkGraphicsScreenOverlayCollection screenOverlayCollection = null;
		screenOverlayCollection = screenOverlayManager.getOverlays();

		IAgStkGraphicsScreenOverlayCollectionBase screenOverlayCollectionBase = null;
		screenOverlayCollectionBase = (IAgStkGraphicsScreenOverlayCollectionBase)screenOverlayCollection;

		Font font = new Font("Arial", Font.BOLD, 12);
		String text = "STK Engine\nAnalytical Graphics\nOverlays";
		Dimension textSize = measureString(text, font);
		BufferedImage textBitmap = new BufferedImage(textSize.width, textSize.height, BufferedImage.TYPE_4BYTE_ABGR);
		Graphics2D gfx = textBitmap.createGraphics();
		gfx.setColor(Color.WHITE);
		int maxAdvance = gfx.getFontMetrics().getMaxAdvance();
		drawString(text, maxAdvance, 0, font, gfx);
		gfx.dispose();

		IAgStkGraphicsFactoryAndInitializers initrs = null;
		initrs = sceneManager.getInitializers();

		IAgStkGraphicsTextureScreenOverlayFactory textureScreenOverlayFactory = null;
		textureScreenOverlayFactory = initrs.getTextureScreenOverlay();

		double xLocation = 10; // The X location of the screen overlay
		double yLocation = 10; // The yLocation of the screen overlay
		double width = textSize.getWidth();
		double height = textSize.getHeight();

		IAgStkGraphicsTextureScreenOverlay overlay = null;
		overlay = textureScreenOverlayFactory.initializeWithXYWidthHeight(xLocation, yLocation, width, height);
		((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_LEFT);

		// Any bitmap can be written to a texture by temporarily saving the texture to disk.
		String fmt = "png";
		String filePath = DataPaths.getDataPaths().getSampleProjectPath("TemoraryTextOverlay.bmp");
		File f = new File(filePath);
		ImageIO.write(textBitmap, fmt, f);

		overlay.setTexture(sceneManager.getTextures().loadFromStringUri(filePath));

		f.delete(); // The temporary file is not longer required and can be deleted

		overlay.setTextureFilter(sceneManager.getInitializers().getTextureFilter2D().getNearestClampToEdge());

		screenOverlayCollectionBase.add((IAgStkGraphicsScreenOverlay)overlay);

		// #endregion

		this.m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;

		String overlaytext = "Java's Graphics2D.DrawString method can be used to write \r\ntext into Bitmap for use for the TextureScreenOverlay.";
		OverlayHelper.addTextBox(this, sceneManager, overlaytext);
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			// Overlays are always fixed to the screen regardless of view
			scene.render();
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
			IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

			if(this.m_Overlay != null)
			{
				overlayManager.remove(this.m_Overlay);
				this.m_Overlay = null;
			}
			OverlayHelper.removeTextBox(manager);
			scene.render();
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
	}
}