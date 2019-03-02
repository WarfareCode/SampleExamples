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

public class SurfaceMeshTrapezoidalTextureCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public SurfaceMeshTrapezoidalTextureCodeSnippet(Component c)
	{
		super(c, "Draw a texture mapped to a trapezoid", "primitives", "surfacemesh", "SurfaceMeshTrapezoidalTextureCodeSnippet.java");
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

        //#region CodeSnippet

        // Load the UAV image where each corner maps to a longitude and latitude defined 
        // in degrees below.
        //
        //    lower left  = (-0.386182, 42.938583)
        //    lower right = (-0.375100, 42.929871)
        //    upper right = (-0.333891, 42.944780)
        //    upper left  = (-0.359980, 42.973438)
        
		String filePath = DataPaths.getDataPaths().getSharedDataPath("TerrainAndImagery"+fileSep+"surfaceMeshTrapezoidalTexture.jpg");
		IAgStkGraphicsRendererTexture2D texture = sceneManager.getTextures().loadFromStringUri(filePath);

        // Define the bounding extent of the image.  Create a surface mesh that uses this extent.
        IAgStkGraphicsSurfaceMeshPrimitive mesh = sceneManager.getInitializers().getSurfaceMeshPrimitive().initializeDefault();
        mesh.setTexture(texture);

        Object[] cartographicExtent = new Object[] 
        {
        	new Double(-0.386182),
        	new Double(42.929871),
        	new Double(-0.333891),
        	new Double(42.973438) 
        };

        IAgStkGraphicsSurfaceTriangulatorResult triangles = sceneManager.getInitializers().getSurfaceExtentTriangulator().computeSimple("Earth", cartographicExtent);
        mesh.set(triangles);
        ((IAgStkGraphicsPrimitive)mesh).setTranslucency(0.0f);

        // Create the texture matrix that maps the image corner points to their actual
        // cartographic coordinates.  A few notes:
        //
        // 1. The TextureMatrix does not do any special processing on these values
        //    as if they were cartographic coordinates.
        //
        // 2. Because of 1., the values only have to be correct relative to each
        //    other, which is why they do not have to be converted to radians.
        //
        // 3. Because of 2., if your image straddles the +/- 180 degs longitude line, 
        //    ensure that longitudes east of the line are greater than those west of
        //    the line.  For example, if one point were 179.0 degs longitude and the
        //    other were to the east at -179.0 degs, the one to the east should be
        //    specified as 181.0 degs.

        Object[] c0 = new Object[] { new Double(-0.386182), new Double(42.938583) };
        Object[] c1 = new Object[] { new Double(-0.375100), new Double(42.929871) };
        Object[] c2 = new Object[] { new Double(-0.333891), new Double(42.944780) };
        Object[] c3 = new Object[] { new Double(-0.359980), new Double(42.973438) };

        mesh.setTextureMatrix(sceneManager.getInitializers().getTextureMatrix().initializeWithRectangles(c0, c1, c2, c3));
        
        // Enable the transparent texture border option on the mesh so that the texture will not
        // bleed outside of the trapezoid.
        mesh.setTransparentTextureBorder(true);

        // Add the surface mesh to the scene manager
        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);

        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)mesh;
		
		OverlayHelper.addTextBox(this, sceneManager, "The surface mesh's TextureMatrix is used \r\nto map a rectangular texture to a trapezoid.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
            scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
            scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

            Object[] extent = new Object[]
            {
            	new Double(-0.386182),
            	new Double(42.929871),
            	new Double(-0.333891),
            	new Double(42.973438) 
            };

            ViewHelper.viewExtent(root, scene, "Earth", extent, -135, 30);
            scene.render();
		}
		catch(Throwable t)
		{
			throw new Throwable(t);
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