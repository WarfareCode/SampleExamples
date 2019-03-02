package codesnippets.displayconditions;

//#region Imports

//Java API
import java.awt.*;
import java.util.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.customtypes.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class DistanceDisplayConditionCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsPrimitive m_Primitive;
	private ArrayList<Interval> 		m_Intervals;

	public DistanceDisplayConditionCodeSnippet(Component c)
	{
		super(c, "Draw a primitive based on viewer distance", "displayconditions", "DistanceDisplayConditionCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"hellfire.dae");
        IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);
        Object[] position = new Object[] { new Double(29.98), new Double(-90.25), new Double(8000.0) };
        model.setPositionCartographic("Earth", position);
        model.setScale(Math.pow(10, 3));

        IAgStkGraphicsDistanceDisplayCondition condition = null;
        condition = manager.getInitializers().getDistanceDisplayCondition().initializeWithDistances(2000, 40000);
        ((IAgStkGraphicsPrimitive)model).setDisplayCondition((IAgStkGraphicsDisplayCondition)condition);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)model;

		OverlayHelper.addTextBox(this, manager, "Zoom in and out to see the primitive disappear and \r\nreappear based on distance. \r\n\r\nThis is implemented by assigning a DistanceDisplayCondition \r\nto the primitive's DisplayCondition property.");

        OverlayHelper.addDistanceOverlay(this, manager, scene);
        
        m_Intervals = new ArrayList<Interval>();
        m_Intervals.add(new Interval(2000, 40000));
        OverlayHelper.getDistanceDisplay().addIntervals(m_Intervals);
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Primitive.getBoundingSphere(), 45, 10);
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
        	OverlayHelper.getDistanceDisplay().removeIntervals(m_Intervals);
        	m_Intervals = null;
        }

        OverlayHelper.removeDistanceOverlay(this,manager);

        scene.render();
	}
}