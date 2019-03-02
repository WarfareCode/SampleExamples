package codesnippets.primitives.trianglemesh;

//#region Imports

//Java API
import java.awt.*;
import javax.swing.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class TriangleMeshAreaTargetCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public TriangleMeshAreaTargetCodeSnippet(Component c)
	{
		super(c, "Draw a filled STK area target on the globe", "primitives", "trianglemesh", "TriangleMeshAreaTargetCodeSnippet.java");
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

            IAgStkGraphicsSurfaceTriangulatorResult triangles = null;
            triangles = sceneManager.getInitializers().getSurfacePolygonTriangulator().compute("Earth", positions);

	        IAgStkGraphicsTriangleMeshPrimitive mesh = sceneManager.getInitializers().getTriangleMeshPrimitive().initializeDefault();
	        mesh.setTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
	        ((IAgStkGraphicsPrimitive)mesh).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
	        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);

	        //#endregion

			m_Primitive = (IAgStkGraphicsPrimitive)mesh;
			OverlayHelper.addTextBox(this, sceneManager, "Positions defining the boundary of an STK area target are read from \r\ndisk.  SurfacePolygonTriangulator.compute computes triangles for the area \r\ntarget's interior, which are then visualized with a TriangleMeshPrimitive.");
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
            	manager.getPrimitives().remove(this.m_Primitive);
            	this.m_Primitive = null;
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