package codesnippets.globeoverlays;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;
//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class ProjectedImageModelsCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;
    private IAgStkGraphicsGlobeImageOverlay m_Overlay;

    public ProjectedImageModelsCodeSnippet(Component c)
	{
		super(c, "Project imagery on models", "globeoverlays", "ProjectedImageModelsCodeSnippet.java");
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
            animationSettings.setAnimStepValue(1.0 / 7.5);
            animationSettings.setRefreshDelta(1.0 / 15.0);
            animationSettings.setRefreshDeltaType(AgEScRefreshDeltaType.E_REFRESH_DELTA);
            scenario.setStopTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "05 Oct 2010 16:00:52.000").format("epSec"))));
            scenario.setStartTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "05 Oct 2010 16:00:00.000").format("epSec"))));
            animationSettings.setStartTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "05 Oct 2010 16:00:00.000").format("epSec"))));
            animationSettings.setEnableAnimCycleTime(true);
            animationSettings.setAnimCycleTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "05 Oct 2010 16:00:52.000").format("epSec"))));
            animationSettings.setAnimCycleType(AgEScEndLoopType.E_LOOP_AT_TIME);
            animation.rewind();

            //#region CodeSnippet
            // Enable Raster Model Projection
            scene.getGlobeOverlaySettings().setProjectedRasterModelProjection(true);

            // Add projected raster globe overlay with a raster and projection stream
            IAgStkGraphicsVideoStream videoStream = null;
            String fileSep = AgSystemPropertiesHelper.getFileSeparator();
            String filePath = DataPaths.getDataPaths().getSharedDataPath("ProjectedImagery"+fileSep+"buildings.avi");
            videoStream = manager.getInitializers().getVideoStream().initializeWithStringUri(filePath);
            videoStream.setPlayback(AgEStkGraphicsVideoPlayback.E_STK_GRAPHICS_VIDEO_PLAYBACK_TIME_INTERVAL);
            videoStream.setIntervalStartTime(root.getConversionUtility().newDate("UTCG", "05 Oct 2010 16:00:00.000"));
            videoStream.setIntervalEndTime(root.getConversionUtility().newDate("UTCG", "05 Oct 2010 16:00:52.000"));

            String filePath1 = DataPaths.getDataPaths().getSharedDataPath("ProjectedImagery"+fileSep+"buildings.txt");
            PositionOrientationHelper projectionProvider = new PositionOrientationHelper(root, filePath1);

            IAgStkGraphicsProjectionRasterStreamPluginActivator activator = null;
            activator = manager.getInitializers().getProjectionRasterStreamPluginActivator().initializeDefault();

            IAgStkGraphicsProjectionRasterStreamPluginProxy proxy = null;
            proxy = activator.createFromDisplayName("Java.ProjectionRasterStream.Basic");

            // Use DispatchHelper to set the plugin's properties
            IAgUnknown pluginUnk = proxy.getRealPluginObject();
            AgDispatchHelper pluginDH = new AgDispatchHelper(pluginUnk);
            pluginDH.set("NearPlane", new Double(20.0));
            pluginDH.set("FarPlane", new Double(300.0));
            pluginDH.set("FieldOfViewHorizontal", new Double(0.232709985));
            pluginDH.set("FieldOfViewVertical", new Double(0.175929193));
            pluginDH.set("Dates", projectionProvider.getDatesArray());

            Double[][] pos = projectionProvider.getPositionsArray();
            pluginDH.set("Positions", pos);
            
            Double[][] orient = projectionProvider.getOrientationsArray();
            pluginDH.set("Orientations", orient);

            IAgStkGraphicsProjectionStream projectionStream = proxy.getProjectionStream();
            
            IAgStkGraphicsProjectedRasterOverlay rasterProjection = null;
            rasterProjection = manager.getInitializers().getProjectedRasterOverlay().initializeDefault((IAgStkGraphicsRaster)videoStream, (IAgStkGraphicsProjection)projectionStream);
            rasterProjection.setShowFrustum(true);
            rasterProjection.setFrustumColor(AgCoreColor.BLACK);
            rasterProjection.setFrustumTranslucency(.5f);
            rasterProjection.setShowShadows(true);
            rasterProjection.setShadowColor(AgCoreColor.ORANGE);
            rasterProjection.setShadowTranslucency(.5f);
            rasterProjection.setShowFarPlane(true);
            rasterProjection.setFarPlaneColor(AgCoreColor.LIGHTBLUE);
            rasterProjection.setColor(AgCoreColor.LIGHTBLUE);
            ((IAgStkGraphicsGlobeImageOverlay)rasterProjection).setTranslucency(.2f);

            scene.getCentralBodies().getEarth().getImagery().add((IAgStkGraphicsGlobeImageOverlay)rasterProjection);

            //#endregion

            m_Overlay = (IAgStkGraphicsGlobeImageOverlay)rasterProjection;


            // Add model
            String filePath2 = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"phoenix_gray"+fileSep+"phoenix.dae");
            IAgStkGraphicsModelPrimitive model = null;
            model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath2);

            Object[] position = new Object[]
            {
                new Double(33.4918312268), 
                new Double(-112.0751720286), 
                new Double(0.0)
            };

            IAgPosition origin = root.getConversionUtility().newPositionOnEarth();
            origin.assignPlanetodetic((Double)position[0], (Double)position[1], ((Double)position[2]).doubleValue());
            IAgCrdnAxesFixed axes = STKVgtHelper.createAxes(root, "Earth", origin);
            
            Object epoch = ((IAgScenario)root.getCurrentScenario()).getEpoch_AsObject();
            IAgCrdnAxesFindInAxesResult result = null;
            result = root.getVgtRoot().getWellKnownAxes().getEarth().getFixed().findInAxes(epoch, ((IAgCrdnAxes)axes));
            
            model.setPositionCartographic("Earth", position);
            model.setOrientation(result.getOrientation());

            manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);

            m_Primitive = (IAgStkGraphicsPrimitive)model;            

    		OverlayHelper.addTextBox(this, manager, "Video is projected onto a model by first initializing a VideoStream object with a video.  A ProjectedRasterOverlay is then created using the video stream and a projection stream defining how to project the video onto the model. Shadows are visualized in orange.");
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
        if (m_Primitive != null)
        {
	        IAgAnimation animation = (IAgAnimation)root;
	        animation.playForward();
	
	        Object[] center = (Object[])m_Primitive.getBoundingSphere().getCenter_AsObject();
	
	        IAgStkGraphicsSceneManager manager = null;
	        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

	        IAgStkGraphicsBoundingSphere boundingSphere = null;
	        boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(center, 100);
	
	        ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere, 20, 25);
	
	        scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = null;
        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		if(m_Primitive != null)
		{
			manager.getPrimitives().remove(m_Primitive);
        	m_Primitive = null;
		}	

		if (m_Overlay != null)
        {
            scene.getCentralBodies().getItem("Earth").getImagery().remove(m_Overlay);
            m_Overlay = null;
        }
		
        IAgAnimation animation = (IAgAnimation)root;
        animation.rewind();
        STKObjectsHelper.setAnimationDefaults(root);

        // Disable Raster Model Projection
        scene.getGlobeOverlaySettings().setProjectedRasterModelProjection(false);

        OverlayHelper.removeTextBox(manager);
        scene.render();            
	}
}