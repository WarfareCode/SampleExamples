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

public class TriangleMeshCircleCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public TriangleMeshCircleCodeSnippet(Component c)
	{
		super(c, "Draw a filled circle on the globe", "primitives", "trianglemesh", "TriangleMeshCircleCodeSnippet.java");
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
            	new Double(39.88), 
            	new Double(-75.25),
            	new Double(0.0)
            };

            IAgStkGraphicsSurfaceShapesResult shape = null;
            shape = sceneManager.getInitializers().getSurfaceShapes().computeCircleCartographic("Earth", center, 10000);
            
            Object[] positions = (Object[])shape.getPositions_AsObject();
            IAgStkGraphicsSurfaceTriangulatorResult triangles = sceneManager.getInitializers().getSurfacePolygonTriangulator().compute("Earth", positions);

            IAgStkGraphicsTriangleMeshPrimitive mesh = sceneManager.getInitializers().getTriangleMeshPrimitive().initializeDefault();
            mesh.setTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
            ((IAgStkGraphicsPrimitive)mesh).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
            ((IAgStkGraphicsPrimitive)mesh).setTranslucency(0.5f);

            sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);

			// #endregion

			m_Primitive = (IAgStkGraphicsPrimitive)mesh;
			OverlayHelper.addTextBox(this, sceneManager,"Boundary positions for a circle are computed with \r\nSurfaceShapes.computeCircleCartographic.  Triangles for the circle's \r\ninterior are then computed with SurfacePolygonTriangulator.compute \r\nand visualized with a TriangleMeshPrimitive.");
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