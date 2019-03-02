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

public class ModelColladaFXCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsPrimitive	m_Primitive;

	public ModelColladaFXCodeSnippet(Component c)
	{
		super(c, "Draw a Collada model with user defined lighting", "primitives", "model", "ModelColladaFXCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		// #region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Satellite.dae");
		IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);

		Object[] position = new Object[] {new Double(39.88), new Double(-75.25), new Double(500000.0)};
		model.setPositionCartographic("Earth", position);
		model.setScale(Math.pow(10, 2));

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);
		// #endregion

		OverlayHelper.addTextBox(this, manager,"Models can contain user defined lighting to give \r\nit properties of real world materials such as metal, \r\nglass, plastic, etc.\r\n \r\nThis model uses a shader that models the properties of \r\nmetal. It uses normal mapping to achieve the foil like \r\ntexture and it calculates lighting using a reflectance \r\nmodel based on the Ashikhmin-Shirley BRDF.");

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
		
		OverlayHelper.removeTextBox(manager);
		scene.render();
	}
}
