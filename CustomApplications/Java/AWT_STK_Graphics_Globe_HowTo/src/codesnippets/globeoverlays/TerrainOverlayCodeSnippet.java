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

public class TerrainOverlayCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsTerrainOverlay m_Overlay;

    public TerrainOverlayCodeSnippet(Component c)
	{
		super(c, "Add terrain to the globe", "globeoverlays", "TerrainOverlayCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
	        IAgStkGraphicsTerrainOverlay globeOverlay = null;
	        IAgStkGraphicsTerrainCollection overlays = scene.getCentralBodies().getEarth().getTerrain();
	        int cnt = overlays.getCount();
	        for(int i=0; i<cnt; i++)
	        {
	        	IAgStkGraphicsGlobeOverlay overlay = null;
	        	overlay = (IAgStkGraphicsGlobeOverlay)overlays.getItem(i);
	
	        	if (overlay.getUriAsString().endsWith("St Helens.pdtt"))
	            {
	                globeOverlay = (IAgStkGraphicsTerrainOverlay)overlay;
	                break;
	            }
	        }
	
	        // Don't load terrain if another code snippet already loaded it.
	        if (globeOverlay == null)
	        {
            	//#region CodeSnippet
	            String fileSep = AgSystemPropertiesHelper.getFileSeparator();
                String filePath = DataPaths.getDataPaths().getSharedDataPath("Textures"+fileSep+"St Helens.pdtt");
                IAgStkGraphicsTerrainOverlay overlay = null;
                overlay = scene.getCentralBodies().getEarth().getTerrain().addUriString(filePath);
                //#endregion

                m_Overlay = overlay;
	        }
	        else
	        {
	            m_Overlay = globeOverlay;
	        }
		}
		catch(Throwable t)
		{
        	String message = "Could not create globe overlay.  Your video card may not support this feature.";
			throw new Throwable(message, t);
		}
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_Overlay != null)
        {
            scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
            scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());
            Object[] extent = (Object[])((IAgStkGraphicsGlobeOverlay)m_Overlay).getExtent_AsObject();
            ViewHelper.viewExtent(root, scene, "Earth", extent, 45, 15);
            scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_Overlay != null)
        {
            scene.getCentralBodies().getItem("Earth").getTerrain().remove(m_Overlay);
            m_Overlay = null;
        }
        scene.render();
	}
}