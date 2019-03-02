package codesnippets.primitives.model;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;
import codesnippets.primitives.*;

//#endregion

public class ModelOrientationCodeSnippet
extends STKGraphicsCodeSnippet
// implements IDisposable
{
	private IAgStkGraphicsPrimitive	m_Primitive;
	private ReferenceFrameGraphics	m_ReferenceFrameGraphics;
	private final double			s_AxesLength	= 2000;

	public ModelOrientationCodeSnippet(Component c)
	{
		super(c, "Orient a model", "primitives", "model", "ModelOrientationCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		// #region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		IAgPosition origin = STKUtilHelper.createPosition(root, 39.88, -75.25, 0);
		IAgCrdnAxesFixed axes = STKVgtHelper.createAxes(root, "Earth", origin);
		IAgCrdnSystem referenceFrame = STKVgtHelper.createSystem(root, "Earth", origin, axes);

		String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Land"+fileSep+"facility.mdl");
		IAgStkGraphicsModelPrimitive model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);
		((IAgStkGraphicsPrimitive)model).setReferenceFrame(referenceFrame);
		Object[] zero = new Object[] {new Double(0), new Double(0), new Double(0)}; // Origin of reference frame
		model.setPosition(zero);
		model.setScale(Math.pow(10, 1.5));

		manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);
		// #endregion

		OverlayHelper.addTextBox(this, manager, "To orient a model, use its ReferenceFrame property. \r\nIn this example, AxesEastNorthUp is used. X points east, \r\nY points north, and Z points along the detic surface normal \r\n(e.g. \"up\"). Like all STK .mdl facility models, this model\r\nwas authored such that Z points up so it is oriented \r\ncorrectly using AxesEastNorthUp.");
		m_Primitive = (IAgStkGraphicsPrimitive)model;

		m_ReferenceFrameGraphics = new ReferenceFrameGraphics(root, referenceFrame, s_AxesLength);
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		IAgPosition position = root.getConversionUtility().newPositionOnEarth();
		position.assignPlanetodetic(new Double(39.88), new Double(-75.25), 0);

		Object[] xyz = (Object[])position.queryCartesianArray_AsObject();
		IAgStkGraphicsBoundingSphere boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(xyz, s_AxesLength);

		ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere, 20, 15);

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
		
		if(m_ReferenceFrameGraphics != null)
		{
			m_ReferenceFrameGraphics.dispose();
			m_ReferenceFrameGraphics = null;
		}
		
		scene.render();
	}

	public final void dispose()
	{
		dispose(true);
	}

	protected void finalize()
	throws Throwable
	{
		try
		{
			dispose(false);
		}
		finally
		{
			super.finalize();
		}
	}

	protected void dispose(boolean disposing)
	{
		if(disposing)
		{
			if(m_ReferenceFrameGraphics != null)
			{
				m_ReferenceFrameGraphics.dispose();
				m_ReferenceFrameGraphics = null;
			}
		}
	}
}