package codesnippets.primitives.solid;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class SolidCylinderCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive = null;
    private IAgCrdnAxes m_Axes = null;

    public SolidCylinderCodeSnippet(Component c)
	{
		super(c, "Draw a cylinder", "primitives", "solid", "SolidCylinderCodeSnippet.java");
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

        IAgStkGraphicsSolidTriangulatorResult result = manager.getInitializers().getCylinderTriangulator().createSimple(1000, 2000);
        IAgStkGraphicsSolidPrimitive solid = manager.getInitializers().getSolidPrimitive().initializeDefault();
        ((IAgStkGraphicsPrimitive)solid).setReferenceFrame(system);
        ((IAgStkGraphicsPrimitive)solid).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));
        solid.setOutlineColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLACK));
        solid.setOutlineWidth(2);
        solid.setOutlineAppearance(AgEStkGraphicsOutlineAppearance.E_STK_GRAPHICS_STYLIZE_BACK_LINES);
        solid.setBackLineColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLACK));
        solid.setBackLineWidth(1);
        solid.setWithResult(result);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)solid);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)solid;
        m_Axes = (IAgCrdnAxes)axes;

        OverlayHelper.addTextBox(this, manager, "CylinderTriangulator.compute is used to compute triangles for a cylinder, \r\nwhich are visualized using a SolidPrimitive.");
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