package codesnippets.camera;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class CameraRecordingSnapshotCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsTextureScreenOverlay m_Overlay;

    public CameraRecordingSnapshotCodeSnippet(Component c)
	{
		super(c, "Take a snapshot of the camera's view", "camera", "CameraRecordingSnapshotCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        
		IAgStkGraphicsSceneManager manager = null;
        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        // The snapshot can be saved to a file, texture, image, or the clipboard
        IAgStkGraphicsRendererTexture2D texture = scene.getCamera().getSnapshot().saveToTexture();

        IAgStkGraphicsTextureScreenOverlay overlay = createOverlayFromTexture(texture, root);
        IAgStkGraphicsScreenOverlayCollectionBase screenOverlayManager = null;
        screenOverlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays();

        screenOverlayManager.add((IAgStkGraphicsScreenOverlay)overlay);
        
        //#endregion

		OverlayHelper.addTextBox(this, manager, "A snapshot of the current view is saved to a texture,\r\nwhich is then used to create a screen overlay.  Snapshots \r\ncan also be saved to a file, image, or the clipboard.");
        
        scene.render();
        m_Overlay = overlay;
	}

    private static IAgStkGraphicsTextureScreenOverlay 
    createOverlayFromTexture(IAgStkGraphicsRendererTexture2D texture, AgStkObjectRootClass root)
    throws AgCoreException
    {
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgStkGraphicsTextureScreenOverlay textureScreenOverlay = manager.getInitializers().getTextureScreenOverlay().initializeWithXYTexture(0, 0, texture);
        IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)textureScreenOverlay;
        overlay.setBorderSize(2);
        overlay.setBorderColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
        overlay.setScale(0.2);
        overlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER);

        return textureScreenOverlay;
    }

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = null;
        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if (m_Overlay != null)
        {
            IAgStkGraphicsScreenOverlayCollectionBase screenOverlayManager = null;
            screenOverlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays();
            screenOverlayManager.remove((IAgStkGraphicsScreenOverlay)m_Overlay);
            m_Overlay = null;
        }
        
        OverlayHelper.removeTextBox(manager);

        scene.render();
	}
}