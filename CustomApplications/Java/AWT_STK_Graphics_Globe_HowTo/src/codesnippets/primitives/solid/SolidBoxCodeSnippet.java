package codesnippets.primitives.solid;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class SolidBoxCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive = null;
    private IAgCrdnAxes m_Axes = null;

    public SolidBoxCodeSnippet(Component c)
	{
		super(c, "Draw a box", "primitives", "solid", "SolidBoxCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgPosition origin = root.getConversionUtility().newPositionOnEarth();
        origin.assignPlanetodetic(new Double(28.488889), new Double(-80.577778), 1000);
        IAgCrdnAxesFixed axes = STKVgtHelper.createAxes(root, "Earth", origin);
        IAgCrdnSystem system = STKVgtHelper.createSystem(root, "Earth", origin, axes);

        Object[] size = new Object[] {new Double(1000), new Double(1000), new Double(2000) };
        IAgStkGraphicsSolidTriangulatorResult result = manager.getInitializers().getBoxTriangulator().compute(size);
        IAgStkGraphicsSolidPrimitive solid = manager.getInitializers().getSolidPrimitive().initializeDefault();
		((IAgStkGraphicsPrimitive)solid).setReferenceFrame(system);
        solid.setWithResult(result);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)solid);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)solid;
        m_Axes = (IAgCrdnAxes)axes;

        OverlayHelper.addTextBox(this, manager, "BoxTriangulator.compute is used to compute triangles for a box, \r\nwhich are visualized using a SolidPrimitive.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgCrdnAxes referenceAxes = ((IAgCrdnAxesFixed)m_Axes).getReferenceAxes().getAxes();
        IAgCrdnAxesOnSurface onSurface = (IAgCrdnAxesOnSurface)referenceAxes;
        Object[] offset = new Object[] 
        {
        	new Double(m_Primitive.getBoundingSphere().getRadius() * 2.5), 
        	new Double(m_Primitive.getBoundingSphere().getRadius() * 2.5), 
        	new Double(m_Primitive.getBoundingSphere().getRadius() * 0.5)
        };
        scene.getCamera().viewOffset(m_Axes, onSurface.getReferencePoint().getPoint(), offset);
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

        if(m_Axes != null)
        {
        	m_Axes = null;
        }

        OverlayHelper.removeTextBox(manager);
        scene.render();
	}
}