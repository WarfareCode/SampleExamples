package codesnippets.primitives.polyline;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class PolylineCircleCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public PolylineCircleCodeSnippet(Component c)
	{
		super(c, "Draw the outline of a circle on the globe", "primitives", "polyline", "PolylineCircleCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] center = new Object[] 
        { 
        	new Double(39.88), 
        	new Double(-75.25), 
        	new Double(0.0)
        }; // Philadelphia

        IAgStkGraphicsSurfaceShapesResult shape = manager.getInitializers().getSurfaceShapes().computeCircleCartographic("Earth", center, 10000);
        Object[] positions = (Object[])shape.getPositions_AsObject();

        IAgStkGraphicsPolylinePrimitive line = manager.getInitializers().getPolylinePrimitive().initializeWithType(shape.getPolylineType());
        line.set(positions);
        line.setWidth(2);
        ((IAgStkGraphicsPrimitive)line).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)line);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)line;
        OverlayHelper.addTextBox(this, manager, "SurfaceShapes.computeCircleCartographic is used to compute the positions \r\nof a circle on the surface, which is visualized with the polyline primitive.");

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