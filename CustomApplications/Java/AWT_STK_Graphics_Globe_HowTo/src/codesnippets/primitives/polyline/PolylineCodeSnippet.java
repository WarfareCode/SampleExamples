package codesnippets.primitives.polyline;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class PolylineCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public PolylineCodeSnippet(Component c)
	{
		super(c, "Draw a line between two points", "primitives", "polyline", "PolylineCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] positions = new Object[]
        {
        	//philly
        	new Double(39.88),
        	new Double(-75.25),
        	new Double(3000.0),
            //DC
        	new Double(38.85),
        	new Double(-77.04),
        	new Double(3000.0)
        };

        IAgStkGraphicsPolylinePrimitive line = manager.getInitializers().getPolylinePrimitive().initializeDefault();
        line.setCartographic("Earth", positions);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)line);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)line;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
        scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

        ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Primitive.getBoundingSphere(),
            -40, 10);

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

        scene.render();
	}
}