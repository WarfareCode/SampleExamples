package codesnippets.primitives.markerbatch;

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

public class MarkerBatchCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_MarkerBatch;

    public MarkerBatchCodeSnippet(Component c)
	{
		super(c, "Draw a set of markers", "primitives", "markerbatch", "MarkerBatchCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        Object[] positions = new Object[]
        {
            new Double(39.88), new Double(-75.25), new Double(0),    	// Philadelphia
            new Double(38.85), new Double(-77.04), new Double(0), 	// Washington, D.C.   
            new Double(29.98), new Double(-90.25), new Double(0), 	// New Orleans
            new Double(37.37), new Double(-121.92), new Double(0)    	// San Jose
        };

        IAgStkGraphicsMarkerBatchPrimitive markerBatch = null;
        markerBatch = manager.getInitializers().getMarkerBatchPrimitive().initializeDefault();
        String filePath = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"facility.png");
        markerBatch.setTexture(manager.getTextures().loadFromStringUri(filePath));
        markerBatch.setCartographic("Earth", positions);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)markerBatch);
        //#endregion

        m_MarkerBatch = (IAgStkGraphicsPrimitive)markerBatch;

		OverlayHelper.addTextBox(this, manager, "A collection of positions is passed to the MarkerBatchPrimitive to \r\nvisualize markers for each position.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_MarkerBatch != null)
        {
            ViewHelper.viewBoundingSphere(root, scene, "Earth", m_MarkerBatch.getBoundingSphere());
            scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

            if (m_MarkerBatch != null)
            {
            	manager.getPrimitives().remove(m_MarkerBatch);
            	m_MarkerBatch = null;
            }

            OverlayHelper.removeTextBox(manager);
            scene.render();
	}
}