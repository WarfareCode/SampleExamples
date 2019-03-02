package codesnippets.primitives.surfacemesh;

//#region Imports

//Java API
import java.awt.*;
import java.awt.geom.AffineTransform;

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

public class SurfaceMeshTransformationsCodeSnippet
extends STKGraphicsCodeSnippet 
implements IAgStkObjectRootEvents2
{
    private IAgStkGraphicsPrimitive m_Primitive;
    private float m_Translation;

    public SurfaceMeshTransformationsCodeSnippet(Component c)
	{
		super(c, "Draw a moving water texture using affine transformations", "primitives", "surfacemesh", "SurfaceMeshTransformationsCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        root.addIAgStkObjectRootEvents2(this);
		
        IAgStkGraphicsSceneManager sceneManager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if (!sceneManager.getInitializers().getSurfaceMeshPrimitive().supportedWithDefaultRenderingMethod())
        {
			JOptionPane.showMessageDialog(null, "Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.", "Unsupported", JOptionPane.WARNING_MESSAGE);
            return;
        }

        //#region CodeSnippet
        Object[] cartographicExtent = new Object[] 
        {
            new Double(-96),
            new Double(22),
            new Double(-85),
            new Double(28) 
        };

        IAgStkGraphicsSurfaceTriangulatorResult triangles =
            sceneManager.getInitializers().getSurfaceExtentTriangulator().computeSimple("Earth", cartographicExtent);

		String filePath = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"water.png");
        IAgStkGraphicsRendererTexture2D texture = sceneManager.getTextures().loadFromStringUri(filePath);

        IAgStkGraphicsSurfaceMeshPrimitive mesh = sceneManager.getInitializers().getSurfaceMeshPrimitive().initializeDefault();
        ((IAgStkGraphicsPrimitive)mesh).setTranslucency(0.3f);
        mesh.setTexture(texture);
        mesh.setTextureFilter(sceneManager.getInitializers().getTextureFilter2D().getLinearRepeat());
        mesh.set(triangles);
        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);

        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)mesh;
        m_Translation = 0;

        OverlayHelper.addTextBox(this, sceneManager, "Animation effects such as water can be created by modifying a surface \r\nmesh's TextureMatrix property over time.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgAnimation animation = (IAgAnimation)root;
        // Set-up the animation for this specific example
        animation.pause();
        STKObjectsHelper.setAnimationDefaults(root);
        ((IAgScenario)root.getCurrentScenario()).getAnimation().setAnimStepValue(1.0);
        animation.playForward();

        if (m_Primitive != null)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
            Object[] center = (Object[])m_Primitive.getBoundingSphere().getCenter_AsObject();
            IAgStkGraphicsBoundingSphere boundingSphere =
                manager.getInitializers().getBoundingSphere().initializeDefault(center, 500000);

            ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere, -90, 35);
            scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
            root.removeIAgStkObjectRootEvents2(this);
            IAgAnimation animation = (IAgAnimation)root;
            animation.rewind();
            STKObjectsHelper.setAnimationDefaults(root);

    		if(m_Primitive != null)
    		{
    			manager.getPrimitives().remove(m_Primitive);
    			m_Primitive = null;
    		}

    		OverlayHelper.removeTextBox(manager);
            scene.render();
	}

	// #region CodeSnippet
	public void onAgStkObjectRootEvent(AgStkObjectRootEvent e)
	{
		try
		{
            //  Translate the surface mesh every animation update
            if (m_Primitive != null)
            {
    			int type = e.getType();
    			AgStkObjectRootClass root = (AgStkObjectRootClass)e.getSource();

    			if(type == AgStkObjectRootEvent.TYPE_ON_ANIM_UPDATE)
    			{
    				Object[] params = e.getParams();
    				double timeEpSec = ((Double)params[0]).doubleValue();

                    IAgStkGraphicsSceneManager manager = null;
                    manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

                    m_Translation = (float)timeEpSec;
                    m_Translation /= 1000;

        			AffineTransform transformation = new AffineTransform();
                    transformation.translate(-m_Translation, 0); // Sign determines the direction of apparent flow

                    double[] flatMatrix = new double[6];
                    transformation.getMatrix(flatMatrix);

                    // Convert the matrix to an object array
                    Object[] transformationArray = new Object[flatMatrix.length];
                    for (int i = 0; i < transformationArray.length; ++i)
                    {
                    	transformationArray[i] = new Double(flatMatrix[i]);
                    }

                    IAgStkGraphicsTextureMatrix textureMatrix = null;
                    textureMatrix = manager.getInitializers().getTextureMatrix().initializeWithAffineTransform(transformationArray);
                    ((IAgStkGraphicsSurfaceMeshPrimitive)m_Primitive).setTextureMatrix(textureMatrix);
    			}
            }
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
	// #endregion
}