package codesnippets.displayconditions;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class CompositeDisplayConditionCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    private double m_Start1;
    private double m_End1;
    private double m_Start2;
    private double m_End2;

    public CompositeDisplayConditionCodeSnippet(Component c)
	{
		super(c, "Draw a primitive based on multiple conditions", "displayconditions", "CompositeDisplayConditionCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager1 = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
		OverlayHelper.addTextBox(this, manager1, "The primitive will be drawn on 5/30/2008 between 2:00:00\r\nPM and 2:30:00 PM and between 3:00:00 PM and 3:30:00 PM.\r\n\r\nTwo TimeIntervalDisplayConditions are created and added to a\r\nCompositeDisplayCondition, which is assigned to the model's \r\nDisplayCondition property. The composite is set to use the \r\nlogical or operator so the primitive is shown if the current\r\nanimation time is within one of the time intervals. \r\n\r\nNote - the display conditions that make up a composite \r\ndisplay condition need not be of the same type.");
		
        OverlayHelper.addTimeOverlay(this,root);

        //#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] position = new Object[] {new Double(29.98), new Double(-90.25), new Double(0.0) };

        String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Land"+fileSep+"facility.mdl");
        IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);
        model.setPositionCartographic("Earth", position);
        model.setScale(Math.pow(10, 1.5));

        IAgDate start1 = root.getConversionUtility().newDate("UTCG", "30 May 2008 14:00:00.000");
        IAgDate end1 = root.getConversionUtility().newDate("UTCG", "30 May 2008 14:30:00.000");
        ((IAgAnimation)root).setCurrentTime(Double.parseDouble(start1.format("epSec")));
        IAgDate start2 = root.getConversionUtility().newDate("UTCG", "30 May 2008 15:00:00.000");
        IAgDate end2 = root.getConversionUtility().newDate("UTCG", "30 May 2008 15:30:00.000");

        IAgStkGraphicsTimeIntervalDisplayCondition time1 = manager.getInitializers().getTimeIntervalDisplayCondition().initializeWithTimes(start1, end1);
        IAgStkGraphicsTimeIntervalDisplayCondition time2 = manager.getInitializers().getTimeIntervalDisplayCondition().initializeWithTimes(start2, end2);
        IAgStkGraphicsCompositeDisplayCondition composite = manager.getInitializers().getCompositeDisplayCondition().initializeDefault();

        composite.add((IAgStkGraphicsDisplayCondition)time1);
        composite.add((IAgStkGraphicsDisplayCondition)time2);
        composite.setLogicOperation(AgEStkGraphicsBinaryLogicOperation.E_STK_GRAPHICS_BINARY_LOGIC_OPERATION_OR);
        ((IAgStkGraphicsPrimitive)model).setDisplayCondition((IAgStkGraphicsDisplayCondition)composite);

        // Orient the model
        IAgPosition origin = root.getConversionUtility().newPositionOnEarth();
        origin.assignPlanetodetic((Double)position[0], (Double)position[1], ((Double)position[2]).doubleValue());
        IAgCrdnAxesFixed axes = STKVgtHelper.createAxes(root, "Earth", origin);

        AgVariant epoch = ((IAgScenario)root.getCurrentScenario()).getEpoch();
        IAgCrdnAxesFindInAxesResult result = root.getVgtRoot().getWellKnownAxes().getEarth().getFixed().findInAxes(epoch, ((IAgCrdnAxes)axes));
        model.setOrientation(result.getOrientation());

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);
        //#endregion

        m_Start1 = Double.parseDouble(start1.format("epSec"));
        m_End1 = Double.parseDouble(end1.format("epSec"));
        m_Start2 = Double.parseDouble(start2.format("epSec"));
        m_End2 = Double.parseDouble(end2.format("epSec"));
        OverlayHelper.getTimeDisplay().addInterval(this, m_Start1, m_End1);
        OverlayHelper.getTimeDisplay().addInterval(this, m_Start2, m_End2);

        m_Primitive = (IAgStkGraphicsPrimitive)model;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        // Set-up the animation for this specific example
        IAgAnimation animation = (IAgAnimation)root;
        animation.pause();
        STKObjectsHelper.setAnimationDefaults(root);
        animation.playForward();

        ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Primitive.getBoundingSphere());
        scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        ((IAgAnimation)root).rewind();

        if(m_Primitive != null)
        {
        	manager.getPrimitives().remove(m_Primitive);
            m_Primitive = null;
        }

        OverlayHelper.getTimeDisplay().removeInterval(this, m_Start1, m_End1);
        OverlayHelper.getTimeDisplay().removeInterval(this, m_Start2, m_End2);
        OverlayHelper.removeTimeOverlay(this, manager);
        OverlayHelper.removeTextBox(manager);

        scene.render();
	}
}