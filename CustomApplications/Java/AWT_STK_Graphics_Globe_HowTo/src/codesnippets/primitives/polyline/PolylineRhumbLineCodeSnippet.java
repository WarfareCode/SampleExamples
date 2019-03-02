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

public class PolylineRhumbLineCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public PolylineRhumbLineCodeSnippet(Component c)
	{
		super(c, "Draw a rhumb line on the globe", "primitives", "polyline", "PolylineRhumbLineCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] positions = new Object[] 
        {
        	//New Orleans
        	new Double(29.98), 
        	new Double(-90.25), 
        	new Double(0.0),
            // San Jose 
        	new Double(37.37), 
        	new Double(-121.92), 
        	new Double(0.0)
        };

        IAgStkGraphicsPositionInterpolator interpolator = null;
        interpolator = (IAgStkGraphicsPositionInterpolator)manager.getInitializers().getRhumbLineInterpolator().initializeDefault();
        IAgStkGraphicsPolylinePrimitive line = manager.getInitializers().getPolylinePrimitive().initializeWithInterpolator(interpolator);
        line.setCartographic("Earth", positions);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)line);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)line;

        OverlayHelper.addTextBox(this, manager, "The PolylinePrimitive is initialized with a RhumbLineInterpolator to \r\nvisualize a rhumb line instead of a straight line.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
        scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

        double fit = 1.0; //for helping fit the line into the extent

        Object[] extent = new Object[]
        { 
        	new Double(-121.92 - fit),
        	new Double(29.98 - fit),
        	new Double(-90.25 + fit),
        	new Double(37.37 + fit)
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