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

public class ModelCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsPrimitive	m_Primitive;

	public ModelCodeSnippet(Component c)
	{
		super(c, "Draw a Collada or MDL model", "primitives", "model", "ModelCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		// #region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"hellfire.dae");
		IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);

		Object[] position = new Object[] {new Double(39.88), new Double(-75.25), new Double(5000.0)};
		model.setPositionCartographic("Earth", position);
		model.setScale(Math.pow(10, 2));

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);
		// #endregion

		m_Primitive = (IAgStkGraphicsPrimitive)model;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Primitive.getBoundingSphere(), -45, 3);

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