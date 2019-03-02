package codesnippets.primitives.polyline;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class PolylineAreaTargetCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public PolylineAreaTargetCodeSnippet(Component c)
	{
		super(c, "Draw a STK area target outline on the globe", "primitives", "polyline", "PolylineAreaTargetCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        String filePath = DataPaths.getDataPaths().getSharedDataPath("AreaTargets"+fileSep+"_pennsylvania_1.at");
        Object[] positions = STKUtilHelper.readAreaTargetPoints(root, filePath);

        IAgStkGraphicsPolylinePrimitive line = manager.getInitializers().getPolylinePrimitive().initializeDefault();
        line.set(positions);
        line.setWidth(2);
        ((IAgStkGraphicsPrimitive)line).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));
        line.setDisplayOutline(true);
        line.setOutlineWidth(2);
        line.setOutlineColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLACK));

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)line);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)line;
        OverlayHelper.addTextBox(this, manager, "Positions defining the boundary of an STK area target are read from \r\ndisk and visualized with the polyline primitive.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Primitive.getBoundingSphere());
        scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if(m_Primitive != null)
        {
        	manager.getPrimitives().remove(m_Primitive);
            m_Primitive = null;
        }

        OverlayHelper.removeTextBox(manager);
        scene.render();
	}
}