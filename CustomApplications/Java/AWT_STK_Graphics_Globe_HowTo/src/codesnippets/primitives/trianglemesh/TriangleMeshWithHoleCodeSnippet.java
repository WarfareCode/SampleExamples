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
import codesnippets.*;
import utils.DataPaths;
import utils.helpers.*;

//#endregion

public class TriangleMeshWithHoleCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;
    private IAgStkGraphicsPrimitive m_BoundaryLine;
    private IAgStkGraphicsPrimitive m_HoleLine;

	public TriangleMeshWithHoleCodeSnippet(Component c)
	{
		super(c, "Draw a filled polygon with a hole on the globe", "primitives", "trianglemesh", "TriangleMeshWithHoleCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	{
		try
		{
			// #region CodeSnippet
            String fileSep = AgSystemPropertiesHelper.getFileSeparator();

			IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
			IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();

			String posFilePath = DataPaths.getDataPaths().getSharedDataPath("AreaTargets"+fileSep+"LogoBoundary.at");
            Object[] positions = STKUtilHelper.readAreaTargetPoints(root, posFilePath);
			String holeFilePath = DataPaths.getDataPaths().getSharedDataPath("AreaTargets"+fileSep+"LogoHole.at");
            Object[] holePositions = STKUtilHelper.readAreaTargetPoints(root, holeFilePath);

	        IAgStkGraphicsSurfaceTriangulatorResult triangles = null;
	        triangles = sceneManager.getInitializers().getSurfacePolygonTriangulator().computeWithHole("Earth", positions, holePositions);
	
	        IAgStkGraphicsTriangleMeshPrimitive mesh = sceneManager.getInitializers().getTriangleMeshPrimitive().initializeDefault();
	        mesh.setTriangulator((IAgStkGraphicsTriangulatorResult)triangles);
	        ((IAgStkGraphicsPrimitive)mesh).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GRAY));
	        ((IAgStkGraphicsPrimitive)mesh).setTranslucency(0.5f);
	        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);
	
	        IAgStkGraphicsPolylinePrimitive boundaryLine = sceneManager.getInitializers().getPolylinePrimitive().initializeDefault();
	        Object[] boundaryPositionsArray = (Object[])triangles.getBoundaryPositions_AsObject();
	        boundaryLine.set(boundaryPositionsArray);
	        ((IAgStkGraphicsPrimitive)boundaryLine).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
	        boundaryLine.setWidth(2);
	        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)boundaryLine);
	
	        IAgStkGraphicsPolylinePrimitive holeLine = sceneManager.getInitializers().getPolylinePrimitive().initializeDefault();
	        holeLine.set(holePositions);
	        ((IAgStkGraphicsPrimitive)holeLine).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
	        holeLine.setWidth(2);
	        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)holeLine);
	        //#endregion
	
	        m_Primitive = (IAgStkGraphicsPrimitive)mesh;
	        m_BoundaryLine = (IAgStkGraphicsPrimitive)boundaryLine;
	        m_HoleLine = (IAgStkGraphicsPrimitive)holeLine;
	
			OverlayHelper.addTextBox(this, sceneManager, "SurfacePolygonTriangulator.compute has overloads that take the exterior \r\nboundary positions as well as positions for an interior hole.");
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
            
            if(this.m_BoundaryLine != null)
            {
            	manager.getPrimitives().remove(m_BoundaryLine);
    	        m_BoundaryLine = null;
            }
            
            if(this.m_HoleLine != null)
            {
            	manager.getPrimitives().remove(m_HoleLine);
    	        m_HoleLine = null;
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