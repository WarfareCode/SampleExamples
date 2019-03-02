package codesnippets.primitives.trianglemesh;

//#region Imports

// Java API
import java.awt.*;
import javax.swing.*;

import agi.core.AgSystemPropertiesHelper;
// AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

// Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class TriangleMeshExtrusionCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public TriangleMeshExtrusionCodeSnippet(Component c)
	{
		super(c, "Draw an extrusion around a STK area target", "primitives", "trianglemesh", "TriangleMeshExtrusionCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			// #region CodeSnippet
            String fileSep = AgSystemPropertiesHelper.getFileSeparator();

			IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
			IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();

			String posFilePath = DataPaths.getDataPaths().getSharedDataPath("AreaTargets"+fileSep+"_pennsylvania_1.at");
            Object[] positions = STKUtilHelper.readAreaTargetPoints(root, posFilePath);

            IAgStkGraphicsFactoryAndInitializers initrs = null;
            initrs = sceneManager.getInitializers();
            
            IAgStkGraphicsExtrudedPolylineTriangulatorInitializer eptinitrs = null;
            eptinitrs = initrs.getExtrudedPolylineTriangulator();
            
            IAgStkGraphicsExtrudedPolylineTriangulatorResult triangles = null;
            triangles = eptinitrs.computeWithAltitudes("Earth", positions, 10000, 25000);

            IAgStkGraphicsTriangleMeshPrimitive mesh = null;
            mesh = initrs.getTriangleMeshPrimitive().initializeDefault();
            mesh.setTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
            ((IAgStkGraphicsPrimitive)mesh).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
            ((IAgStkGraphicsPrimitive)mesh).setTranslucency(0.4f);

            sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);
        	//#endregion

        	m_Primitive = (IAgStkGraphicsPrimitive)mesh;

			OverlayHelper.addTextBox(this, sceneManager, "ExtrudedPolylineTriangulator.compute computes triangles for an \r\nextrusion, which are visualized with a TriangleMeshPrimitive.");
		}
		catch(Throwable t)
		{
			t.printStackTrace();
			JOptionPane.showMessageDialog(null, t.toString(), "Exception", JOptionPane.WARNING_MESSAGE);
		}
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			IAgStkGraphicsBoundingSphere boundingSphere = this.m_Primitive.getBoundingSphere();
            ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere);
			scene.render();
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

            if(this.m_Primitive != null)
            {
            	manager.getPrimitives().remove(m_Primitive);
	            m_Primitive = null;
            }
            
	        OverlayHelper.removeTextBox(manager);
	        scene.render();
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
	}
}