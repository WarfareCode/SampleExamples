package codesnippets.globeoverlays;

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

public class GlobeOverlayRenderOrderCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsGlobeImageOverlay m_TopOverlay;
    private IAgStkGraphicsGlobeImageOverlay m_BottomOverlay;

    public GlobeOverlayRenderOrderCodeSnippet(Component c)
	{
		super(c, "Draw an image on top of another", "globeoverlays", "GlobeOverlayRenderOrderCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        try
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

            //#region CodeSnippet
            String fileSep = AgSystemPropertiesHelper.getFileSeparator();

            String topOverlayPath = DataPaths.getDataPaths().getSharedDataPath("TerrainAndImagery"+fileSep+"top.jp2");
            IAgStkGraphicsGlobeImageOverlay topOverlay = scene.getCentralBodies().getEarth().getImagery().addUriString(topOverlayPath);
            String bottomOverlayPath = DataPaths.getDataPaths().getSharedDataPath("TerrainAndImagery"+fileSep+"bottom.jp2");
            IAgStkGraphicsGlobeImageOverlay bottomOverlay = scene.getCentralBodies().getEarth().getImagery().addUriString(bottomOverlayPath);

            // Since bottom.jp2 was added after top.jp2, bottom.jp2 will be 
            // drawn on top.  In order to draw top.jp2 on top, we swap the Overlays. 
            scene.getCentralBodies().getEarth().getImagery().swap(topOverlay, bottomOverlay);
            //#endregion

            m_TopOverlay = topOverlay;
            m_BottomOverlay = bottomOverlay;

    		OverlayHelper.addTextBox(this, manager, "The swap, bringToFront, and sendToBack methods are used \r\nto change the ordering of imagery on the globe.");
        }
        catch(Throwable t)
        {
			String message = "Could not create globe Overlay.  Your video card may not support this feature.";
        	throw new Throwable(message, t);
        }
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_TopOverlay != null)
        {
            Object[] top = (Object[])((IAgStkGraphicsGlobeOverlay)m_TopOverlay).getExtent_AsObject();
            Object[] bottom = (Object[])((IAgStkGraphicsGlobeOverlay)m_BottomOverlay).getExtent_AsObject();

            scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
            scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

            ViewHelper.viewExtent(root, scene, "Earth",
                                       Math.min(((Double)top[0]).doubleValue(), ((Double)bottom[0]).doubleValue()),
                                       Math.min(((Double)top[1]).doubleValue(), ((Double)bottom[1]).doubleValue()),
                                       Math.max(((Double)top[2]).doubleValue(), ((Double)bottom[2]).doubleValue()),
                                       Math.max(((Double)top[3]).doubleValue(), ((Double)bottom[3]).doubleValue()),
                                       -90,
                                       25);

            scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_TopOverlay != null)
        {
            scene.getCentralBodies().getItem("Earth").getImagery().remove(m_TopOverlay);
            m_TopOverlay = null;
        }
        
        if(m_BottomOverlay != null)
        {
            scene.getCentralBodies().getItem("Earth").getImagery().remove(m_BottomOverlay);
            m_BottomOverlay = null;
        }
        
        OverlayHelper.removeTextBox(((IAgScenario)root.getCurrentScenario()).getSceneManager());
        scene.render();
	}
}