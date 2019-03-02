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

public class ModelDynamicCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsPrimitive	m_Primitive;

	public ModelDynamicCodeSnippet(Component c)
	{
		super(c, "Draw a dynamically textured Collada model", "primitives", "model", "ModelDynamicCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		// #region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"hellfireflame.dae");
		IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);

		Object[] position = new Object[] {new Double(49.88), new Double(-77.25), new Double(5000.0)};
		model.setPositionCartographic("Earth", position);
		model.setScale(Math.pow(10, 2));

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);

		// hellfireflame.anc
		//
		// <?xml version = "1.0" standalone = "yes"?>
		// <ancillary_model_data version = "1.0">
		// <video_textures>
		// <video_texture image_id = "smoketex_tga" init_from = "smoke.avi" video_loop="true" video_framerate="60" />
		// <video_texture image_id = "flametex_tga" init_from = "flame.mov" video_loop="true" video_framerate="60" />
		// </video_textures>
		// </ancillary_model_data>
		// #endregion

		OverlayHelper.addTextBox(this, manager, "Dynamic textures, e.g. videos, can be used to create \r\neffects like fire and smoke.\r\n\r\nDynamic textures on models are created using an XML-based\r\nancillary file. The ancillary file has the same filename \r\nas the model but with an .anc extension. As shown in the \r\ncode window, the video_texture tag is used to define a \r\nvideo, which is referenced by the model. Once loaded \r\nusing a model primitive, the video will play in sync with\r\nInsight3D animation.");

		m_Primitive = (IAgStkGraphicsPrimitive)model;
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
		Object[] center = (Object[])m_Primitive.getBoundingSphere().getCenter_AsObject();
		IAgStkGraphicsBoundingSphere boundingSphere = null;
		boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(center, m_Primitive.getBoundingSphere().getRadius() * 0.055);

		ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere, -50, 15);
		((IAgAnimation)root).playForward();

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

		OverlayHelper.removeTextBox(manager);
		scene.render();
	}
}