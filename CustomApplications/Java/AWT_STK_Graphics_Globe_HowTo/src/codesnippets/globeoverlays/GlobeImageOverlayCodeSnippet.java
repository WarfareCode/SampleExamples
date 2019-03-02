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
import codesnippets.*;

//#endregion

public class GlobeImageOverlayCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsGlobeImageOverlay m_overlays;

    public GlobeImageOverlayCodeSnippet(Component c)
	{
		super(c, "Add jp2 imagery to the globe", "globeoverlays", "GlobeImageOverlayCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsGlobeImageOverlay globeOverlay = null;
        IAgStkGraphicsImageCollection overlays = scene.getCentralBodies().getEarth().getImagery();
        int cnt = overlays.getCount();
        for (int i=0; i<cnt; i++)
        {
        	IAgStkGraphicsGlobeOverlay overlay = (IAgStkGraphicsGlobeOverlay)overlays.getItem(i);
        	
            if (overlay.getUriAsString() != null && overlay.getUriAsString().endsWith("St Helens.jp2"))
            {
                globeOverlay = (IAgStkGraphicsGlobeImageOverlay)overlay;
                break;
            }
        }

        // Don't load imagery if another code snippet already loaded it.
        if (globeOverlay == null)
        {
            try
            {
                //#region CodeSnippet

                // Either jp2 or pdttx can be used here
                String fileSep = AgSystemPropertiesHelper.getFileSeparator();
                String overlayPath = DataPaths.getDataPaths().getSharedDataPath("Textures"+fileSep+"St Helens.jp2");
                IAgStkGraphicsGlobeImageOverlay overlay = scene.getCentralBodies().getEarth().getImagery().addUriString(overlayPath);

                //#endregion

                m_overlays = overlay;
            }
            catch(Throwable t)
            {
                String message = "Could not create globe overlays.  Your video card may not support this feature.";
            	throw new Throwable(message, t);
            }
        }
        else
        {
            m_overlays = globeOverlay;
        }
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_overlays != null)
        {
            scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
            scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());
            Object[] extent = (Object[])((IAgStkGraphicsGlobeOverlay)m_overlays).getExtent_AsObject();
            scene.getCamera().viewExtent("Earth", extent);
            scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_overlays != null)
        {
            scene.getCentralBodies().getItem("Earth").getImagery().remove(m_overlays);
            m_overlays = null;
        }

        scene.render();
	}
}