package codesnippets.primitives.trianglemesh;

//#region Imports

//Java API
import java.awt.*;

import javax.swing.*;

//AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class TriangleMeshExtentCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public TriangleMeshExtentCodeSnippet(Component c)
	{
		super(c, "Draw a filled rectangular extent on the globe", "primitives", "trianglemesh", "TriangleMeshExtentCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			// #region CodeSnippet
	
			IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
			IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();
	
	        Object[] extent = new Object[]
	        {
	            new Double(-94), new Double(29),
	            new Double(-89), new Double(33)
	        };
	
	        IAgStkGraphicsSurfaceTriangulatorResult triangles = null;
	        triangles = sceneManager.getInitializers().getSurfaceExtentTriangulator().computeSimple(
	        "Earth", extent);
	
			IAgStkGraphicsTriangleMeshPrimitive mesh = null;
			mesh = sceneManager.getInitializers().getTriangleMeshPrimitive().initializeDefault();
			mesh.setTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
			((IAgStkGraphicsPrimitive)mesh).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(new Color(0xFA8072)));
			
            mesh.setLighting(false);  /* Turn off lighting for the mesh so the color we assigned will always be consistent */ 

            sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);
			
			//#endregion
			
			m_Primitive = (IAgStkGraphicsPrimitive)mesh;
			
			OverlayHelper.addTextBox(this, sceneManager, "SurfaceExtentTriangulator.compute computes triangles for a rectangular \r\nextent bounded by lines of constant latitude and longitude.");
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