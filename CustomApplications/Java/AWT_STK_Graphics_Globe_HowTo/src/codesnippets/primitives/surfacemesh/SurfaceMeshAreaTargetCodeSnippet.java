package codesnippets.primitives.surfacemesh;

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

public class SurfaceMeshAreaTargetCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsPrimitive		m_Primitive;
	private IAgStkGraphicsGlobeOverlay	m_Overlay;

	public SurfaceMeshAreaTargetCodeSnippet(Component c)
	{
		super(c, "Draw a filled STK area target on terrain", "primitives", "surfacemesh", "SurfaceMeshAreaTargetCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
	        IAgStkGraphicsSceneManager sceneManager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

	        if (!sceneManager.getInitializers().getSurfaceMeshPrimitive().supportedWithDefaultRenderingMethod())
	        {
				JOptionPane.showMessageDialog(null, "Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.", "Unsupported", JOptionPane.WARNING_MESSAGE);
	            return;
	        }

	        //#region CodeSnippet
            String fileSep = AgSystemPropertiesHelper.getFileSeparator();
            
        	String filePath1 = DataPaths.getDataPaths().getSharedDataPath("TerrainAndImagery"+fileSep+"925.pdtt");
	        IAgStkGraphicsTerrainOverlay overlay = scene.getCentralBodies().getEarth().getTerrain().addUriString(filePath1);

        	String filePath2 = DataPaths.getDataPaths().getSharedDataPath("AreaTargets"+fileSep+"925.at");
            Object[] positions = STKUtilHelper.readAreaTargetPoints(root, filePath2);

            IAgStkGraphicsSurfaceTriangulatorResult triangles = null;
            triangles = sceneManager.getInitializers().getSurfacePolygonTriangulator().compute("Earth", positions);

            IAgStkGraphicsSurfaceMeshPrimitive mesh = sceneManager.getInitializers().getSurfaceMeshPrimitive().initializeDefault();
            ((IAgStkGraphicsPrimitive)mesh).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(new Color(0x800080)));
            mesh.set(triangles);
            sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);
            //#endregion

            m_Overlay = (IAgStkGraphicsGlobeOverlay)overlay;
            m_Primitive = (IAgStkGraphicsPrimitive)mesh;

			OverlayHelper.addTextBox(this, sceneManager, "Similar to the triangle mesh example, triangles for the interior of an \r\nSTK area target are computed using SurfacePolygonTriangulator.compute.  \r\nThis is used an input to a SurfaceMeshPrimitive, which makes the \r\nvisualization conform to terrain.");
		}
		catch(Exception e)
		{
			e.printStackTrace();
			JOptionPane.showMessageDialog(null, e.toString(), "Exception", JOptionPane.WARNING_MESSAGE);
		}
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_Overlay != null)
        {
            scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
            scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

            Object[] extent = (Object[])m_Overlay.getExtent_AsObject();
            ViewHelper.viewExtent(root, scene, "Earth", extent, -45, 30);
            scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if(m_Overlay != null)
        {
        	scene.getCentralBodies().getItem("Earth").getTerrain().remove((IAgStkGraphicsTerrainOverlay)m_Overlay);
        	m_Overlay = null;
        }
        
        if(m_Primitive != null)
        {
        	manager.getPrimitives().remove(m_Primitive);
            m_Primitive = null;
        }

        OverlayHelper.removeTextBox(manager);
        scene.render();
	}
}