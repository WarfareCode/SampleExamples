package codesnippets.screenoverlays;

//#region Imports
import java.io.*;
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
// AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

// Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class OverlaysTextureCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsScreenOverlay	m_Overlay;

	public OverlaysTextureCodeSnippet(Component c)
	{
		super(c, "Add a company logo with a texture overlay", "screenoverlays", "OverlaysTextureCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			// #region CodeSnippet
            String fileSep = AgSystemPropertiesHelper.getFileSeparator();

			IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
			IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();
	
			IAgStkGraphicsScreenOverlayManager screenOverlayManager = null;
			screenOverlayManager = sceneManager.getScreenOverlays();
	
			IAgStkGraphicsScreenOverlayCollection screenOverlayCollection = null;
			screenOverlayCollection = screenOverlayManager.getOverlays();
	
			IAgStkGraphicsScreenOverlayCollectionBase screenOverlayCollectionBase = null;
			screenOverlayCollectionBase = (IAgStkGraphicsScreenOverlayCollectionBase)screenOverlayCollection;
	
			IAgStkGraphicsTexture2DFactory texture2DFactory = null;
			texture2DFactory = sceneManager.getTextures();
	
			File texturePath = null;
			texturePath = new File(DataPaths.getDataPaths().getSharedDataPath("Textures"+fileSep+"agi_logo_transparent.png"));
	
			IAgStkGraphicsRendererTexture2D texture2D = null;
			texture2D = texture2DFactory.loadFromStringUri(texturePath.getAbsolutePath());
	
			IAgStkGraphicsFactoryAndInitializers initrs = null;
			initrs = sceneManager.getInitializers();
	
			IAgStkGraphicsTextureScreenOverlayFactory textureScreenOverlayFactory = null;
			textureScreenOverlayFactory = initrs.getTextureScreenOverlay();
	
			IAgStkGraphicsRendererTextureTemplate2D textureTemplate2D = null;
			textureTemplate2D = texture2D.getTemplate();
	
			double xLocation = 10; // The X location of the screen overlay
			double yLocation = 0; // The yLocation of the screen overlay
			double width = textureTemplate2D.getWidth() / 2;
			double height = textureTemplate2D.getHeight() / 2;
	
			IAgStkGraphicsTextureScreenOverlay textureScreenOverlay = null;
			textureScreenOverlay = textureScreenOverlayFactory.initializeWithXYWidthHeight(xLocation, yLocation, width, height);
	
			// The translucency of the screen overlay
			((IAgStkGraphicsOverlay)textureScreenOverlay).setTranslucency(0.1f);
			// The origin of the screen overlay
			((IAgStkGraphicsOverlay)textureScreenOverlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_RIGHT);
	
			textureScreenOverlay.setTexture(texture2D);
	
			screenOverlayCollectionBase.add((IAgStkGraphicsScreenOverlay)textureScreenOverlay);
	
			// #endregion
	
			this.m_Overlay = (IAgStkGraphicsScreenOverlay)textureScreenOverlay;
	
			String text = "TextureScreenOverlay can be used to overlay a company logo by loading the \r\nlogo image into a Texture2D and providing it to a TextureScreenOverlay.";
			OverlayHelper.addTextBox(this, sceneManager, text);
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
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