package codesnippets.globeoverlays;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;
//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class ProjectedImageCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsGlobeImageOverlay m_Overlay;
    private GlobeImageOverlayCodeSnippet m_Imagery;
    private TerrainOverlayCodeSnippet m_Terrain;

    public ProjectedImageCodeSnippet(Component c)
	{
		super(c, "Add projected imagery to the globe", "globeoverlays", "ProjectedImageCodeSnippet.java");
		m_Terrain = new TerrainOverlayCodeSnippet(c);
		m_Imagery = new GlobeImageOverlayCodeSnippet(c);
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        try
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
            IAgAnimation animation = (IAgAnimation)root;
            IAgScAnimation animationSettings = ((IAgScenario)root.getCurrentScenario()).getAnimation();
            IAgScenario scenario = ((IAgScenario)root.getCurrentScenario());

            // Set-up the animation for this specific example
            animation.pause();
            animationSettings.setAnimStepValue(1.0);
            animationSettings.setRefreshDelta(1.0 / 30.000);
            animationSettings.setRefreshDeltaType(AgEScRefreshDeltaType.E_REFRESH_DELTA);
            scenario.setStartTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:00:00.000").format("epSec"))));
            scenario.setStopTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:11:58.162").format("epSec"))));
            animationSettings.setStartTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:00:00.000").format("epSec"))));
            animationSettings.setEnableAnimCycleTime(true);
            animationSettings.setAnimCycleTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:11:58.162").format("epSec"))));
            animationSettings.setAnimCycleType(AgEScEndLoopType.E_LOOP_AT_TIME);
            animation.rewind();

            //#region CodeSnippet
            // Add projected raster globe overlay with a raster and projection stream
            String fileSep = AgSystemPropertiesHelper.getFileSeparator();
            String filePath = DataPaths.getDataPaths().getSharedDataPath("ProjectedImagery"+fileSep+"fig8.avi");
            IAgStkGraphicsVideoStream videoStream = manager.getInitializers().getVideoStream().initializeWithStringUri(filePath);
            videoStream.setPlayback(AgEStkGraphicsVideoPlayback.E_STK_GRAPHICS_VIDEO_PLAYBACK_TIME_INTERVAL);
            videoStream.setIntervalStartTime(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:00:00.000"));
            videoStream.setIntervalEndTime(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:11:58.162"));

            String filePath2 = DataPaths.getDataPaths().getSharedDataPath("ProjectedImagery"+fileSep+"fig8.txt");
            PositionOrientationHelper projectionProvider = new PositionOrientationHelper(root, filePath2);

            IAgStkGraphicsProjectionRasterStreamPluginActivator activator = null;
            activator = manager.getInitializers().getProjectionRasterStreamPluginActivator().initializeDefault();
            IAgStkGraphicsProjectionRasterStreamPluginProxy proxy = null;
            proxy = activator.createFromDisplayName("Java.ProjectionRasterStream.Basic");

            // Use DispatchHelper to set the plugin's properties
            IAgUnknown pluginUnk = proxy.getRealPluginObject();
            AgDispatchHelper pluginDH = new AgDispatchHelper(pluginUnk);
            pluginDH.set("NearPlane", new Double(20.0));
            pluginDH.set("FarPlane", new Double(10000.0));
            pluginDH.set("FieldOfViewHorizontal", new Double(0.230908805));
            pluginDH.set("FieldOfViewVertical", new Double(0.174532925));
            pluginDH.set("Dates", projectionProvider.getDatesArray());

            Double[][] pos = projectionProvider.getPositionsArray();
            pluginDH.set("Positions", pos);
            
            Double[][] orient = projectionProvider.getOrientationsArray();
            pluginDH.set("Orientations", orient);

            IAgStkGraphicsProjectionStream projectionStream = proxy.getProjectionStream();
            
            IAgStkGraphicsProjectedRasterOverlay rasterProjection = null;
            rasterProjection = manager.getInitializers().getProjectedRasterOverlay().initializeDefault((IAgStkGraphicsRaster)videoStream, (IAgStkGraphicsProjection)projectionStream);
            rasterProjection.setShowFrustum(true);
            rasterProjection.setShowShadows(true);

            scene.getCentralBodies().getEarth().getImagery().add((IAgStkGraphicsGlobeImageOverlay)rasterProjection);
            //#endregion

            m_Overlay = (IAgStkGraphicsGlobeImageOverlay)rasterProjection;

            // Add terrain and imagery
            m_Terrain.execute(root, scene);
            m_Imagery.execute(root, scene);

    		OverlayHelper.addTextBox(this, manager, "Video is projected onto terrain by first initializing a VideoStream \r\nobject with a video.  A ProjectedRasterOverlay is then created using \r\nthe video stream and a projection stream defining how to project the \r\nvideo onto terrain.");
        }
        catch (Throwable t)
        {
        	String message = t.getMessage();

        	if (t.getMessage().indexOf("Java.ProjectionStream.Basic") != -1)
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
                    sb.append("                b. Then uncomment the plugin entry that contains a display name of Java.ProjectionRasterStream.Basic.\n\n");
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
	                    sb.append("                b. Then uncomment the plugin entry under the comment of 'If Windows 64 bit OS (regardless of x86 JRE OR x64 JRE), then use ...' that contains a display name of Java.ProjectionRasterStream.Basic.\n\n");
	    			}
	    			else
	    			{
	    				sb.append("                a. Copy the Graphics.xml from the <stk install dir>\\CodeSamples\\Extend\\Graphics\\Graphics.xml file\n\t\t to the <windows user dir>\\Documents\\STK <version>\\Config\\Plugins directory.\n\n");
	                    sb.append("                b. Then uncomment the plugin entry under the comment of 'If Windows 32 bit OS (only x86 JRE) then use ...' that contains a display name of Java.ProjectionRasterStream.Basic.\n\n");
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
        IAgAnimation animation = (IAgAnimation)root;
        animation.playForward();

        if(m_Terrain != null)
        {
        	m_Terrain.view(root, scene);
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if(m_Imagery != null)
        {
        	m_Imagery.remove(root, scene);
        }
        
        if(m_Terrain != null)
        {
        	m_Terrain.remove(root, scene);
        }

        if (m_Overlay != null)
        {
            scene.getCentralBodies().getItem("Earth").getImagery().remove(m_Overlay);
            m_Overlay = null;
        }
        
        IAgAnimation animation = (IAgAnimation)root;
        animation.rewind();
        STKObjectsHelper.setAnimationDefaults(root);
        
        OverlayHelper.removeTextBox(((IAgScenario)root.getCurrentScenario()).getSceneManager());
        scene.render();
	}
}