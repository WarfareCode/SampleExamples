package codesnippets.primitives.surfacemesh;

//#region Imports

//Java API
import java.awt.*;

import javax.swing.*;

//AGI Java API
import agi.core.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;
//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class SurfaceMeshDynamicImageCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;
    private IAgStkGraphicsTerrainOverlay m_Overlay;

    public SurfaceMeshDynamicImageCodeSnippet(Component c)
	{
		super(c, "Draw a filled, dynamically textured extent on terrain", "primitives", "surfacemesh", "SurfaceMeshDynamicImageCodeSnippet.java");
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
        	IAgStkGraphicsTerrainOverlay eachOverlay = overlays.getItem(i);
            if (((IAgStkGraphicsGlobeOverlay)eachOverlay).getUriAsString().endsWith("St Helens.pdtt"))
            {
                overlay = eachOverlay;
                break;
            }
        }

        // Don't load terrain if another code snippet already loaded it.
        if (overlay == null)
        {
        	String filePath = DataPaths.getDataPaths().getSharedDataPath("Textures"+fileSep+"St Helens.pdtt");
            overlay = scene.getCentralBodies().getEarth().getTerrain().addUriString(filePath);
        }

        Object[] extent = (Object[])((IAgStkGraphicsGlobeOverlay)overlay).getExtent_AsObject();
        IAgStkGraphicsSurfaceTriangulatorResult triangles = sceneManager.getInitializers().getSurfaceExtentTriangulator().computeSimple("Earth", extent);

        try
        {
	        //#region CodeSnippet
	
	        IAgStkGraphicsProjectionRasterStreamPluginActivator activator = null;
	        activator = sceneManager.getInitializers().getProjectionRasterStreamPluginActivator().initializeDefault();
	
	        IAgStkGraphicsProjectionRasterStreamPluginProxy proxy = null;
	        proxy = activator.createFromDisplayName("Java.RasterStream.Basic");
	
	        // Use reflection to set the plugin's properties
            String filePath = DataPaths.getDataPaths().getSharedDataPath("Textures"+fileSep+"Lava.gif");
	        IAgUnknown plugin = proxy.getRealPluginObject();
	        AgDispatchHelper pluginDh = new AgDispatchHelper(plugin);
	        pluginDh.set("RasterPath", filePath);
	
	        IAgStkGraphicsRasterStream rasterStream = proxy.getRasterStream();
	        rasterStream.setUpdateDelta(0.025);
	
	        IAgStkGraphicsRendererTexture2D texture = sceneManager.getTextures().fromRaster((IAgStkGraphicsRaster)rasterStream);
	        IAgStkGraphicsSurfaceMeshPrimitive mesh = sceneManager.getInitializers().getSurfaceMeshPrimitive().initializeDefault();
	        ((IAgStkGraphicsPrimitive)mesh).setTranslucency(0.2f);
	        mesh.setTexture(texture);
	        mesh.set(triangles);
	        sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)mesh);
	        
	        //#endregion
	
	        m_Overlay = overlay;
	        m_Primitive = (IAgStkGraphicsPrimitive)mesh;
	
			OverlayHelper.addTextBox(this, sceneManager, "Dynamic textures are created by creating a class that derives \r\nfrom RasterStream that provides time dependent textures.");
        }
        catch (Throwable t)
        {
        	String message = t.getMessage();

        	if (t.getMessage().indexOf("Java.RasterStream.Basic") != -1)
            {
    			String osArch = agi.core.AgSystemPropertiesHelper.getOsArch();
   				StringBuilder sb = new StringBuilder();
   				if (AgOSHelper.isOnLinuxPlatform())
    			{
    				sb.append("A exception has occurred.\n\n");
	                sb.append("It is possible that the plugin has not been added to the GfxPlugin category within a Plugins xml file.\n\n");
	                sb.append("To resolve this issues:\n\n");
	                sb.append("        1. To add it to the GfxPlugins plugins registry category:\n\n");
    				sb.append("                a. Copy the Graphics.xml from the <STK_INSTALL_DIR>/CodeSamples/Extend/Graphics/Graphics.xml file\n\t\t to the <STK_CONFIG_DIR>/STK<version>/Config/Plugins directory.\n\n");
                    sb.append("                b. Then uncomment the plugin entry that contains a display name of Java.RasterStream.Basic.\n\n");
                    sb.append("                c. Then update the ClassPath for the plugin to point where the plugin was compiled.\n\n");
    			}
    			else
    			{
	                sb.append("A COM exception has occurred.\n\n");
	                sb.append("It is possible that one of the following may be the issue:\n\n");
	                sb.append("        1. AgJNIStkPluginGraphicsDriver.dll is not registered for COM interop.  This should be done as part of the install of STK or STK Engine.\n\n");
	                sb.append("        2. That the plugin has not been added to the GfxPlugin category within a Plugins xml file.\n\n");
	                sb.append("To resolve either of these issues:\n\n");
	                sb.append("        1. To register the plugin, open a Visual Studio Command Prompt and execute the command:\n\n");
	    			
	                if(osArch.toLowerCase().indexOf("64") != -1)
	    			{
	    				sb.append("                C:\\Windows\\SysWow64\\regsvr32.exe \"<stk install dir>\\bin\\JavaDevKit\\AgJNIStkPluginGraphicsDriver.dll\"\n\n");
	    			}
	    			else
	    			{
	    				sb.append("                C:\\Windows\\System32\\regsvr32.exe \"<stk install dir>\\bin\\JavaDevKit\\AgJNIStkPluginGraphicsDriver.dll\"\n\n");
	    			}
	                
	    			sb.append("        2. To add it to the GfxPlugins plugins registry category:\n\n");
	
	                if(osArch.toLowerCase().indexOf("64") != -1)
	    			{
	    				sb.append("                a. Copy the Graphics.xml from the <stk install dir>\\CodeSamples\\Extend\\Graphics\\Graphics.xml file\n\t\t to the <windows user dir>\\Documents\\STK <version> (x64)\\Config\\Plugins directory.\n\n");
	                    sb.append("                b. Then uncomment the plugin entry under the comment of 'If Windows 64 bit OS (regardless of x86 JRE OR x64 JRE), then use ...' that contains a display name of Java.RasterStream.Basic.\n\n");
	    			}
	    			else
	    			{
	    				sb.append("                a. Copy the Graphics.xml from the <stk install dir>\\CodeSamples\\Extend\\Graphics\\Graphics.xml file\n\t\t to the <windows user dir>\\Documents\\STK <version>\\Config\\Plugins directory.\n\n");
	                    sb.append("                b. Then uncomment the plugin entry under the comment of 'If Windows 32 bit OS (only x86 JRE) then use ...' that contains a display name of Java.RasterStream.Basic.\n\n");
	    			}
	    			
	    			if(osArch.toLowerCase().indexOf("64") != -1)
	    			{
	    				sb.append("NOTE: The above text of <stk intall dir>, for the current configuration (64 bit)");
	    				sb.append("\n\n");
	    				sb.append("        and if STK Engine was installed to the default location, would resolve to 'C:\\Program Files\\AGI\\STK <version>'\n\n");
	    			}
	    			else
	    			{
	    				sb.append("NOTE: The above text of <stk intall dir>, for the current configuration (32 bit)");
	    				sb.append("\n\n");
	    				sb.append("        and if STK UI or STK Engine was installed to the default location, would resolve to 'C:\\Program Files (x86)\\AGI\\STK <version>'\n\n");
	    			}
    			}
    			message = sb.toString();
            }
            else
            {
                message = "Could not create globe overlay.  Your video card may not support this feature.";
            }
        	throw new Throwable(message, t);
        }
	}
	
	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		if(m_Overlay != null)
		{
            IAgAnimation animation = (IAgAnimation)root;
            
            scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
            scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

            Object[] extents = (Object[])((IAgStkGraphicsGlobeOverlay)m_Overlay).getExtent_AsObject();
            ViewHelper.viewExtent(root, scene, "Earth", extents, -135, 30);

            animation.playForward();
            scene.render();
		}
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        IAgAnimation animation = (IAgAnimation)root;
        animation.rewind();

        if(m_Overlay != null)
        {
            scene.getCentralBodies().getItem("Earth").getTerrain().remove(m_Overlay);
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