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

public class SolidEllipsoidCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive = null;
    private IAgCrdnAxes m_Axes = null;

    public SolidEllipsoidCodeSnippet(Component c)
	{
		super(c, "Draw an ellipsoid", "primitives", "solid", "SolidEllipsoidCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgPosition origin = root.getConversionUtility().newPositionOnEarth();
        origin.assignPlanetodetic(new Double(28.488889), new Double(-80.577778), 4000);
        IAgCrdnAxesFixed axes = STKVgtHelper.createAxes(root, "Earth", origin);
        IAgCrdnSystem system = STKVgtHelper.createSystem(root, "Earth", origin, axes);

        Object[] radii = new Object[] {new Double(2000), new Double(1000), new Double(1000) };
        IAgStkGraphicsSolidTriangulatorResult result = manager.getInitializers().getEllipsoidTriangulator().computeSimple(radii);
        IAgStkGraphicsSolidPrimitive solid = manager.getInitializers().getSolidPrimitive().initializeDefault();
        ((IAgStkGraphicsPrimitive)solid).setReferenceFrame(system);
        ((IAgStkGraphicsPrimitive)solid).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.ORANGE));
        ((IAgStkGraphicsPrimitive)solid).setTranslucency(0.3f);
        solid.setOutlineAppearance(AgEStkGraphicsOutlineAppearance.E_STK_GRAPHICS_FRONT_LINES_ONLY);
        solid.setOutlineColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLUE));
        solid.setOutlineWidth(2);
        solid.setDisplaySilhouette(true);
        solid.setWithResult(result);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)solid);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)solid;
        m_Axes = (IAgCrdnAxes)axes;
        
		OverlayHelper.addTextBox(this, manager, "EllipsoidTriangulator.compute is used to compute triangles for an \r\nellipsoid, which are visualized using a SolidPrimitive.  Its outline \r\nand silhouette appearance are customized.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgCrdnAxes referenceAxes = ((IAgCrdnAxesFixed)m_Axes).getReferenceAxes().getAxes();
        IAgCrdnAxesOnSurface onSurface = (IAgCrdnAxesOnSurface)referenceAxes;
        Object[] offset = new Object[] 
        {
        	new Double(m_Primitive.getBoundingSphere().getRadius() * 2), 
        	new Double(m_Primitive.getBoundingSphere().getRadius() * 2), 
        	new Double(m_Primitive.getBoundingSphere().getRadius() * 0.2)
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