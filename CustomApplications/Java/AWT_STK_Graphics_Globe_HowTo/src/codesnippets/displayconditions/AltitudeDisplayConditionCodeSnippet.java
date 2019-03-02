package codesnippets.displayconditions;

//#region Imports

//Java API
import java.awt.*;
import java.util.*;

//AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.customtypes.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class AltitudeDisplayConditionCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;
	private ArrayList<Interval> 		m_Intervals;

    public AltitudeDisplayConditionCodeSnippet(Component c)
	{
		super(c, "Draw a primitive based on viewer altitude", "displayconditions", "AltitudeDisplayConditionCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] extent = new Object[]
        {
            new Double(-94), new Double(29),
            new Double(-89), new Double(33)
        };

        IAgStkGraphicsSurfaceTriangulatorResult triangles = manager.getInitializers().getSurfaceExtentTriangulator().computeSimple("Earth", extent);

        IAgStkGraphicsPolylinePrimitive line = manager.getInitializers().getPolylinePrimitive().initializeDefault();
        Object[] boundaryPositions = (Object[])triangles.getBoundaryPositions_AsObject();
        line.set(boundaryPositions);
        ((IAgStkGraphicsPrimitive)line).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));

        IAgStkGraphicsAltitudeDisplayCondition condition = manager.getInitializers().getAltitudeDisplayCondition().initializeWithAltitudes(500000, 2500000);
        ((IAgStkGraphicsPrimitive)line).setDisplayCondition((IAgStkGraphicsDisplayCondition)condition);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)line);
        //#endregion

		OverlayHelper.addTextBox(this, manager, "Zoom in and out to see the primitive disappear and \r\nreappear based on altitude. \r\n\r\nThis is implemented by assigning an \r\nAltitudeDisplayCondition to the primitive's \r\nDisplayCondition property.");

        OverlayHelper.addAltitudeOverlay(this, manager, scene);
        
        m_Intervals = new ArrayList<Interval>();
        m_Intervals.add(new Interval(500000, 2500000));
        OverlayHelper.getAltitudeDisplay().addIntervals(m_Intervals);

        m_Primitive = (IAgStkGraphicsPrimitive)line;
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

        if(m_Intervals != null)
        {
        	OverlayHelper.getAltitudeDisplay().removeIntervals(m_Intervals);
        }

        OverlayHelper.removeAltitudeOverlay(this, manager);
        
        scene.render();
	}
}