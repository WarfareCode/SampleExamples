package codesnippets.screenoverlays;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;
//Sample API
import utils.*;
import codesnippets.*;

//#endregion

public class OverlaysPanelCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsScreenOverlay m_Overlay;

    public OverlaysPanelCodeSnippet(Component c)
	{
		super(c, "Add overlays to a panel overlay", "screenoverlays", "OverlaysPanelCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        try
        {
            //#region CodeSnippet
            String fileSep = AgSystemPropertiesHelper.getFileSeparator();
            IAgStkGraphicsSceneManager manager = null;
            manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = null;
            overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

            IAgStkGraphicsTextureScreenOverlay overlay = null;
            overlay = manager.getInitializers().getTextureScreenOverlay().initializeWithXYWidthHeight(0, 0, 188, 200);
            ((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_LEFT);
            ((IAgStkGraphicsOverlay)overlay).setColor(AgCoreColor.LIGHTSKYBLUE);
            ((IAgStkGraphicsOverlay)overlay).setTranslucency(0.7f);
            ((IAgStkGraphicsOverlay)overlay).setBorderTranslucency(0.3f);
            ((IAgStkGraphicsOverlay)overlay).setBorderSize(2);
            ((IAgStkGraphicsOverlay)overlay).setBorderColor(AgCoreColor.AQUA);

            IAgStkGraphicsTextureScreenOverlay childOverlay =
                manager.getInitializers().getTextureScreenOverlay().initializeWithXYWidthHeight(0, 0, ((IAgStkGraphicsOverlay)overlay).getWidth(), ((IAgStkGraphicsOverlay)overlay).getHeight());
            ((IAgStkGraphicsOverlay)childOverlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER);

            String imagePath = DataPaths.getDataPaths().getSharedDataPath("Markers"+fileSep+"originalLogo.png");
            childOverlay.setTexture(manager.getTextures().loadFromStringUri(imagePath));

            // Create the RasterStream from the plugin the same way as in ImageDynamicCodeSnippet.cs
            IAgStkGraphicsProjectionRasterStreamPluginActivator activator = null;
            activator = manager.getInitializers().getProjectionRasterStreamPluginActivator().initializeDefault();

            IAgStkGraphicsProjectionRasterStreamPluginProxy proxy = null;
            proxy = activator.createFromDisplayName("Java.RasterStream.Basic");

            String filePath = DataPaths.getDataPaths().getSharedDataPath("Textures"+fileSep+"SpinSat_256.gif");
            IAgUnknown plugin = proxy.getRealPluginObject();
            AgDispatchHelper pluginDH = new AgDispatchHelper(plugin);
            pluginDH.set("RasterPath", filePath);

            IAgStkGraphicsRasterStream rasterStream = proxy.getRasterStream();
            rasterStream.setUpdateDelta(0.01667);
            IAgStkGraphicsRendererTexture2D texture2D = null;
            texture2D = manager.getTextures().fromRaster((IAgStkGraphicsRaster)rasterStream);

            IAgStkGraphicsTextureScreenOverlay secondChildOverlay =
                manager.getInitializers().getTextureScreenOverlay().initializeWithXYWidthHeight(0, 0, 128, 128);
            ((IAgStkGraphicsOverlay)secondChildOverlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_RIGHT);
            ((IAgStkGraphicsOverlay)secondChildOverlay).setTranslationX(-36);
            ((IAgStkGraphicsOverlay)secondChildOverlay).setTranslationY(-18);
            ((IAgStkGraphicsOverlay)secondChildOverlay).setClipToParent(false);
            secondChildOverlay.setTexture(texture2D);

            overlayManager.add((IAgStkGraphicsScreenOverlay)overlay);

            IAgStkGraphicsScreenOverlayCollectionBase parentOverlayManager = null;
            parentOverlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)((IAgStkGraphicsOverlay)overlay).getOverlays();
            parentOverlayManager.add((IAgStkGraphicsScreenOverlay)childOverlay);

            IAgStkGraphicsScreenOverlayCollectionBase childOverlayManager = null;
            childOverlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)((IAgStkGraphicsOverlay)childOverlay).getOverlays();
            childOverlayManager.add((IAgStkGraphicsScreenOverlay)secondChildOverlay);
            //#endregion

            m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
            ((IAgAnimation)root).playForward();
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
        // Overlays are always fixed to the screen regardless of view
        scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

        ((IAgAnimation)root).rewind();

        if (m_Overlay != null)
        {
        	overlayManager.remove(m_Overlay);
        	m_Overlay = null;
        }
        
        scene.render();
	}
}
