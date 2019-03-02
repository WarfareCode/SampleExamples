package codesnippets.primitives.pointbatch;

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

public class PointBatchCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public PointBatchCodeSnippet(Component c)
	{
		super(c, "Draw a set of points", "primitives", "pointbatch", "PointBatchCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        Object[] positions = new Object[]
        {
        	new Double(39.88), new Double(-75.25), new Double(0),    // Philadelphia
        	new Double(38.85), new Double(-77.04), new Double(0), // Washington, D.C.   
        	new Double(29.98), new Double(-90.25), new Double(0), // New Orleans
        	new Double(37.37), new Double(-121.92), new Double(0)    // San Jose
        };

        IAgStkGraphicsPointBatchPrimitive pointBatch = manager.getInitializers().getPointBatchPrimitive().initializeDefault();
        pointBatch.setCartographic("Earth", positions);
        pointBatch.setPixelSize(5);
        ((IAgStkGraphicsPrimitive)pointBatch).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
        pointBatch.setDisplayOutline(true);
        pointBatch.setOutlineWidth(2);
        pointBatch.setOutlineColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)pointBatch);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)pointBatch;
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

        scene.render();
	}
}