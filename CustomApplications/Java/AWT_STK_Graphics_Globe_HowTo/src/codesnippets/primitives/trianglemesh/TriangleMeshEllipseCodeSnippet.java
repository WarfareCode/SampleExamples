package codesnippets.primitives.trianglemesh;

//#region Imports

//Java API
import java.awt.*;

import javax.swing.JOptionPane;

//AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class TriangleMeshEllipseCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public TriangleMeshEllipseCodeSnippet(Component c)
	{
		super(c, "Draw a filled ellipse on the globe", "primitives", "trianglemesh", "TriangleMeshEllipseCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			// #region CodeSnippet
	
			IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
			IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();
	
	        Object[] center = new Object[] 
	        { 
	        	new Double(38.85), 
	        	new Double(-77.04), 
	        	new Double(3000.0) 
	        }; // Washington, DC
	
	        IAgStkGraphicsSurfaceShapesResult shape = null;
	        shape = sceneManager.getInitializers().getSurfaceShapes().computeEllipseCartographic(
	            "Earth", center, 45000, 30000, 45);
	        
	        Object[] positions = (Object[])shape.getPositions_AsObject();
	
	        IAgStkGraphicsSurfaceTriangulatorResult triangles = null;
	        triangles = sceneManager.getInitializers().getSurfacePolygonTriangulator().compute("Earth", positions);
	
	        IAgStkGraphicsTriangleMeshPrimitive mesh = null;
	        mesh = sceneManager.getInitializers().getTriangleMeshPrimitive().initializeDefault();
	        mesh.setTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
	        ((IAgStkGraphicsPrimitive)mesh).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.CYAN));
	
	        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);
	        
	        //#endregion
	
	        m_Primitive = (IAgStkGraphicsPrimitive)mesh;
			
			OverlayHelper.addTextBox(this, sceneManager, "Boundary positions for an ellipse are computed with \r\nSurfaceShapes.computeEllipseCartographic.  Triangles for the ellipse's\r\ninterior are then computed with SurfacePolygonTriangulator.compute and \r\nvisualized with a TriangleMeshPrimitive.");
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