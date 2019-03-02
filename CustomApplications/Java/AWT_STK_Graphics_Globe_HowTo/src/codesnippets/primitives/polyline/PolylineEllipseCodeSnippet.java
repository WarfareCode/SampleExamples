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

public class PolylineEllipseCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public PolylineEllipseCodeSnippet(Component c)
	{
		super(c, "Draw the outline of an ellipse on the globe", "primitives", "polyline", "PolylineEllipseCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] center = new Object[] 
        { 
        	new Double(38.85), 
        	new Double(-77.04), 
        	new Double(3000.0)
        };

        IAgStkGraphicsSurfaceShapesResult shape = null;
        shape = manager.getInitializers().getSurfaceShapes().computeEllipseCartographic("Earth", center, 45000, 30000, 45);
        Object[] positions = (Object[])shape.getPositions_AsObject();

        IAgStkGraphicsPolylinePrimitive line = null;
        line = manager.getInitializers().getPolylinePrimitive().initializeWithType(shape.getPolylineType());
        line.set(positions);
        ((IAgStkGraphicsPrimitive)line).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.CYAN));

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)line);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)line;
        OverlayHelper.addTextBox(this, manager, "SurfaceShapes.computeEllipseCartographic is used to compute the \r\npositions of an ellipse on the surface, which is visualized with \r\nthe polyline primitive.");
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