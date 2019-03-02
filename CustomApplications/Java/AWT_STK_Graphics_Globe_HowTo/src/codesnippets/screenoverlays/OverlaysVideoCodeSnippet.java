package codesnippets.screenoverlays;

//#region Imports

//Java API
import java.awt.*;
import javax.swing.*;

import utils.DataPaths;
import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;
//Sample API
import codesnippets.*;

//#endregion

public class OverlaysVideoCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsScreenOverlay m_Overlay;

    public OverlaysVideoCodeSnippet(Component c)
	{
		super(c, "Stream a video to a texture overlay", "screenoverlays", "OverlaysVideoCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		String fileSep = AgSystemPropertiesHelper.getFileSeparator();
    	String videoFile = DataPaths.getDataPaths().getSharedDataPath("Videos"+fileSep+"ShenzhouVII_BX1.wmv");
        try
        {
        	//#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = null;
            overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

            IAgStkGraphicsVideoStream videoStream = null;
            videoStream = manager.getInitializers().getVideoStream().initializeWithStringUri(videoFile);
            videoStream.setPlayback(AgEStkGraphicsVideoPlayback.E_STK_GRAPHICS_VIDEO_PLAYBACK_REAL_TIME);
            videoStream.setLoop(true);

            int width = ((IAgStkGraphicsRaster)videoStream).getWidth();
            int height = ((IAgStkGraphicsRaster)videoStream).getHeight();
            IAgStkGraphicsTextureScreenOverlay overlay = null;
            overlay = manager.getInitializers().getTextureScreenOverlay().initializeWithXYWidthHeight(0, 0, width/4, height/4);
            ((IAgStkGraphicsOverlay)overlay).setTranslucency(0.3f);
            ((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_RIGHT);
            ((IAgStkGraphicsOverlay)overlay).setBorderSize(1);
            ((IAgStkGraphicsOverlay)overlay).setBorderTranslucency(0.3f);
            IAgStkGraphicsRendererTexture2D texture = null;
            texture = manager.getTextures().fromRaster((IAgStkGraphicsRaster)videoStream);
            overlay.setTexture(texture);

            overlayManager.add((IAgStkGraphicsScreenOverlay)overlay);
            //#endregion

            m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
            ((IAgAnimation)root).playForward();
        }
        catch(Throwable t)
        {
            StringBuffer message = new StringBuffer();
            message.append("There was a problem accessing the online video file located at: ");
            message.append(videoFile);
            String title = "Error finding to video";
            JOptionPane.showMessageDialog(this.getComponent(), message, title, JOptionPane.ERROR_MESSAGE);
        }
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        // Overlays are always fixed to the screen regardless of view
        scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = null;
        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        IAgStkGraphicsScreenOverlayCollectionBase overlayManager = null;
        overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
        
        ((IAgAnimation)root).rewind();

        if (m_Overlay != null)
        {
        	overlayManager.remove(m_Overlay);
        	m_Overlay = null;
        }
        
        scene.render();
	}
}