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

public class PolylineGreatArcCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public PolylineGreatArcCodeSnippet(Component c)
	{
		super(c, "Draw a great arc on the globe", "primitives", "polyline", "PolylineGreatArcCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] positions = new Object[] 
        {
        	//DC
        	new Double(38.85),
        	new Double(-77.04),
        	new Double(0.0),
            // New Orleans
        	new Double(29.98), 
        	new Double(-90.25),
        	new Double(0.0)
        };

        IAgStkGraphicsPositionInterpolator interpolator = null;
        interpolator = (IAgStkGraphicsPositionInterpolator)manager.getInitializers().getGreatArcInterpolator().initializeDefault();
        IAgStkGraphicsPolylinePrimitive line = manager.getInitializers().getPolylinePrimitive().initializeWithInterpolator(interpolator);
        line.setCartographic("Earth", positions);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)line);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)line;

		OverlayHelper.addTextBox(this, manager, "The PolylinePrimitive is initialized with a GreatArcInterpolator to \r\nvisualize a great arc instead of a straight line.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
        scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

        double fit = 1.0; //for helping fit the line into the extent

        Object[] extent = new Object[]
        { 
        	new Double(-90.25 - fit),
        	new Double(29.98 - fit),
        	new Double(-77.04 + fit),
        	new Double(38.85 + fit)
        };

        scene.getCamera().viewExtent("Earth", extent);

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