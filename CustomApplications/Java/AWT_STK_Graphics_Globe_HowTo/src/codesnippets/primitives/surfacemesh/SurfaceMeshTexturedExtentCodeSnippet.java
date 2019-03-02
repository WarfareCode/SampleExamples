package codesnippets.primitives.surfacemesh;

//#region Imports

//Java API
import java.awt.*;
import javax.swing.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class SurfaceMeshTexturedExtentCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;
    private IAgStkGraphicsGlobeOverlay m_Overlay;

    public SurfaceMeshTexturedExtentCodeSnippet(Component c)
	{
		super(c, "Draw a filled, textured extent on terrain", "primitives", "surfacemesh", "SurfaceMeshTexturedExtentCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager sceneManager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if (!sceneManager.getInitializers().getSurfaceMeshPrimitive().supportedWithDefaultRenderingMethod())
        {
			JOptionPane.showMessageDialog(null, "Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.", "Unsupported", JOptionPane.WARNING_MESSAGE);
            return;
        }

        IAgStkGraphicsTerrainOverlay overlay = null;
        IAgStkGraphicsTerrainCollection overlays = scene.getCentralBodies().getEarth().getTerrain();
        int cnt = overlays.getCount();
        for(int i=0; i<cnt; i++)
        {
        	IAgStkGraphicsGlobeOverlay eachOverlay = (IAgStkGraphicsGlobeOverlay)overlays.getItem(i);
            if (eachOverlay.getUriAsString().endsWith("St Helens.pdtt"))
            {
                overlay = (IAgStkGraphicsTerrainOverlay)eachOverlay;
                break;
            }
        }

        // Don't load terrain if another code snippet already loaded it.
        if (overlay == null)
        {
        	String filePath = DataPaths.getDataPaths().getSharedDataPath("Textures"+fileSep+"St Helens.pdtt");
            overlay = scene.getCentralBodies().getEarth().getTerrain().addUriString(filePath);
        }

        //#region CodeSnippet
        Object[] overlayExtent = (Object[])((IAgStkGraphicsGlobeOverlay)overlay).getExtent_AsObject();
        IAgStkGraphicsSurfaceTriangulatorResult triangles = null;
        triangles = sceneManager.getInitializers().getSurfaceExtentTriangulator().computeSimple("Earth", overlayExtent);

    	String filePath = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"originalLogo.png");
        IAgStkGraphicsRendererTexture2D texture = sceneManager.getTextures().loadFromStringUri(filePath);

        IAgStkGraphicsSurfaceMeshPrimitive mesh = sceneManager.getInitializers().getSurfaceMeshPrimitive().initializeDefault();
        ((IAgStkGraphicsPrimitive)mesh).setTranslucency(0.3f);
        mesh.setTexture(texture);
        mesh.set(triangles);
        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);
        //#endregion

        m_Overlay = (IAgStkGraphicsGlobeOverlay)overlay;
        m_Primitive = (IAgStkGraphicsPrimitive)mesh;
		
		OverlayHelper.addTextBox(this, sceneManager, "The surface mesh's Texture property is used to visualize a texture \r\nconforming to terrain.  For high resolution imagery, it is recommended \r\nto use a globe overlay.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_Overlay != null)
        {
            scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
            scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

            ViewHelper.viewExtent(root, scene, "Earth", (Object[])m_Overlay.getExtent_AsObject(), -135, 30);
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