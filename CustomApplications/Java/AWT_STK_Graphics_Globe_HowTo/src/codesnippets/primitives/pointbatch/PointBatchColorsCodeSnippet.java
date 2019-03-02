package codesnippets.primitives.pointbatch;

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

public class PointBatchColorsCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public PointBatchColorsCodeSnippet(Component c)
	{
		super(c, "Draw a set of uniquely colored points", "primitives", "pointbatch", "PointBatchColorsCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] positions = new Object[]
        {
        	new Double(37.62), new Double(-122.38), new Double(0.0),    // San Francisco
        	new Double(38.52), new Double(-121.50), new Double(0.0),    // Sacramento
        	new Double(33.93), new Double(-118.40), new Double(0.0),    // Los Angeles
        	new Double(32.82), new Double(-117.13), new Double(0.0)     // San Diego
        };

        Object[] colors = new Object[]
        {
            new Long(Color.RED.getRGB()),
            new Long(Color.ORANGE.getRGB()),
            new Long(Color.BLUE.getRGB()),
            new Long(Color.WHITE.getRGB())
        };

        IAgStkGraphicsPointBatchPrimitive pointBatch = null;
        pointBatch = manager.getInitializers().getPointBatchPrimitive().initializeDefault();
        pointBatch.setCartographicWithColors("Earth", positions, colors);
        //pointBatch.setCartographic("Earth", positions);
        pointBatch.setPixelSize(8);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)pointBatch);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)pointBatch;
        
        OverlayHelper.addTextBox(this, manager, "A collection of positions and a collection of colors are provided to\r\nthe PointBatchPrimitive to visualize points with unique colors.");
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