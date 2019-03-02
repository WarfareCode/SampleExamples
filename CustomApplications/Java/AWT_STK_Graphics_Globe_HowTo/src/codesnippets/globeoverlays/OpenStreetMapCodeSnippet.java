package codesnippets.globeoverlays;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgOSHelper;
//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;
//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class OpenStreetMapCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsGlobeImageOverlay m_Overlay;

    public OpenStreetMapCodeSnippet(Component c)
    {
        super(c, "Add custom imagery to the globe", "globeoverlays", "OpenStreetMapCodeSnippet.java");
    }

    public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
    {
        try
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

            // #region CodeSnippet
            IAgStkGraphicsCustomImageGlobeOverlayPluginActivator activator = null;
            activator = manager.getInitializers().getCustomImageGlobeOverlayPluginActivator().initializeDefault();
            IAgStkGraphicsCustomImageGlobeOverlayPluginProxy proxy = null;
            proxy = activator.createFromDisplayName("Java.CustomImageGlobeOverlay.OpenStreetMap");

            IAgStkGraphicsCustomImageGlobeOverlay overlay = proxy.getCustomImageGlobeOverlay();
            scene.getCentralBodies().getEarth().getImagery().add((IAgStkGraphicsGlobeImageOverlay)overlay);
            //#endregion

            m_Overlay = (IAgStkGraphicsGlobeImageOverlay)overlay;

            OverlayHelper.addTextBox(this, manager, "Create an OpenStreetMapImageGlobeOverlay, with an optional extent. \nThis example requires an active internet connection, \notherwise no data is shown.");
        }
        catch (Throwable t)
        {
        	String message = t.getMessage();

        	if (t.getMessage().indexOf("Java.CustomImageGlobeOverlay.OpenStreetMap") != -1)
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
                    sb.append("                b. Then uncomment the plugin entry that contains a display name of Java.CustomImageGlobeOverlay.OpenStreetMap.\n\n");
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
	                    sb.append("                b. Then uncomment the plugin entry under the comment of 'If Windows 64 bit OS (regardless of x86 JRE OR x64 JRE), then use ...' that contains a display name of Java.CustomImageGlobeOverlay.OpenStreetMap.\n\n");
	    			}
	    			else
	    			{
	    				sb.append("                a. Copy the Graphics.xml from the <stk install dir>\\CodeSamples\\Extend\\Graphics\\Graphics.xml file\n\t\t to the <windows user dir>\\Documents\\STK <version>\\Config\\Plugins directory.\n\n");
	                    sb.append("                b. Then uncomment the plugin entry under the comment of 'If Windows 32 bit OS (only x86 JRE) then use ...' that contains a display name of Java.CustomImageGlobeOverlay.OpenStreetMap.\n\n");
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
        scene.render();
    }

    public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
    {
        if (m_Overlay != null)
        {
            IAgStkGraphicsCentralBodyGraphicsIndexer indexer = null;
            indexer = scene.getCentralBodies();

            IAgStkGraphicsCentralBodyGraphics cbg = null;
            cbg = indexer.getItem("Earth");

            IAgStkGraphicsImageCollection collection = null;
            collection = cbg.getImagery();

            collection.remove(m_Overlay);

            OverlayHelper.removeTextBox(((IAgScenario)root.getCurrentScenario()).getSceneManager());

            scene.render();

            m_Overlay = null;
        }
    }
}