package codesnippets.primitives.model;

//#region Imports
//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;
//#endregion

public class ModelArticulationCodeSnippet
extends STKGraphicsCodeSnippet
implements IAgStkObjectRootEvents2
{
    private IAgStkGraphicsPrimitive m_Model;

    public ModelArticulationCodeSnippet(Component c)
	{
		super(c, "Draw a model with moving articulations", "primitives", "model", "ModelArticulationCodeSnippet.java");
	}
	
	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
        
        IAgStkGraphicsSceneManager manager = null;
        manager = scenario.getSceneManager();

        // Create the model
        IAgStkGraphicsFactoryAndInitializers fai = null;
        fai = manager.getInitializers();

        IAgStkGraphicsModelPrimitiveFactory mpf = null;
        mpf = fai.getModelPrimitive();

        String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Air"+fileSep+"commuter.mdl");
        
		IAgStkGraphicsModelPrimitive model = null;
        model = mpf.initializeWithStringUri(filePath);

        Object[] position = new Object[] { new Double(36), new Double(-116.75), new Double(25000.0) };
        model.setPositionCartographic("Earth", position);

        // Rotate the model to be oriented correctly
        IAgStkGraphicsModelArticulationCollection mac = null;
        mac = model.getArticulations();

        IAgStkGraphicsModelArticulation ma = null;
        ma = mac.getByName("Commuter");

        IAgStkGraphicsModelTransformation rollMT = ma.getByName("Roll");
        rollMT.setCurrentValue(4.084070562);

        IAgStkGraphicsModelTransformation yawMT = ma.getByName("Yaw");
        yawMT.setCurrentValue(-0.436332325);

        IAgStkGraphicsPrimitiveManager pm = null;
        pm = manager.getPrimitives();
        
        pm.add((IAgStkGraphicsPrimitive)model);
        //#endregion

		root.addIAgStkObjectRootEvents2(this);

		m_Model = (IAgStkGraphicsPrimitive)model;

        OverlayHelper.addTextBox(this, manager, "The Articulations collection provides access to a model's moving parts.\r\nIn this example, the propellers' spin articulation is modified in the \r\nTimeChanged event based on the current time.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgAnimation animation = (IAgAnimation)root;

        // Set-up the animation for this specific example
        animation.pause();
        STKObjectsHelper.setAnimationDefaults(root);
        ((IAgScenario)root.getCurrentScenario()).getAnimation().setAnimStepValue(1.0);
        animation.playForward();

        ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Model.getBoundingSphere(), -15, 3);

        scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		root.removeIAgStkObjectRootEvents2(this);

		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
		
		if(m_Model != null)
		{
			manager.getPrimitives().remove(m_Model);
			m_Model = null;
		}
		
        OverlayHelper.removeTextBox(manager);
        scene.render();
	}
	
	// #region CodeSnippet
	public void onAgStkObjectRootEvent(AgStkObjectRootEvent e)
	{
		try
		{
			int type = e.getType();

			if(type == AgStkObjectRootEvent.TYPE_ON_ANIM_UPDATE)
			{
				Object[] params = e.getParams();
				double timeEpSec = ((Double)params[0]).doubleValue();

	            // Rotate the propellers every time the animation updates
	            if (m_Model != null)
	            {
	                double TwoPI = 2 * Math.PI;
	                ((IAgStkGraphicsModelPrimitive)m_Model).getArticulations().getByName("props").getByName("Spin").setCurrentValue(timeEpSec % TwoPI);
	            }
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
	// #endregion
}