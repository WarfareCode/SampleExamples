package codesnippets.displayconditions;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;
import agi.stkutil.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class TimeDisplayConditionCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsGlobeImageOverlay m_Overlay;

    private IAgDate m_Start;
    private IAgDate m_End;

    public TimeDisplayConditionCodeSnippet(Component c)
	{
		super(c, "Draw a globe overlay based on the current time", "displayconditions", "TimeDisplayConditionCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
    	//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        String overlayPath = DataPaths.getDataPaths().getSharedDataPath("TerrainAndImagery"+fileSep+"DisplayConditionExample.jp2");
        IAgStkGraphicsGeospatialImageGlobeOverlay overlay = null;
        overlay = manager.getInitializers().getGeospatialImageGlobeOverlay().initializeWithString(overlayPath);

        IAgDate start = root.getConversionUtility().newDate("UTCG", "30 May 2008 14:30:00.000");
        IAgDate end = root.getConversionUtility().newDate("UTCG", "30 May 2008 15:00:00.000");

        ((IAgScenario)root.getCurrentScenario()).getAnimation().setStartTime(Double.valueOf(start.subtract("sec", 3600).format("epSec")));

        IAgStkGraphicsTimeIntervalDisplayCondition condition = null;
        condition = manager.getInitializers().getTimeIntervalDisplayCondition().initializeWithTimes(start, end);
        ((IAgStkGraphicsGlobeOverlay)overlay).setDisplayCondition((IAgStkGraphicsDisplayCondition)condition);

        scene.getCentralBodies().getEarth().getImagery().add((IAgStkGraphicsGlobeImageOverlay)overlay);
        //#endregion

        m_Overlay = (IAgStkGraphicsGlobeImageOverlay)overlay;

		OverlayHelper.addTextBox(this, manager, "The overlay will be drawn on 5/30/2008 between \r\n2:30:00 PM and 3:00:00 PM. \r\n\r\nThis is implemented by assigning a \r\nTimeIntervalDisplayCondition to the overlay's \r\nDisplayCondition property.");

        OverlayHelper.addTimeOverlay(this, root);

        m_Start = start;
        m_End = end;
        double dstart = Double.parseDouble(m_Start.format("epSec"));
        double dstop = Double.parseDouble(m_End.format("epSec"));
        OverlayHelper.getTimeDisplay().addInterval(this, dstart, dstop);
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_Overlay != null)
        {
            // Set-up the animation for this specific example
            IAgAnimation animation = (IAgAnimation)root;

            animation.pause();
            STKObjectsHelper.setAnimationDefaults(root);

            animation.playForward();

            scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
            scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());
            Object[] extent = (Object[])((IAgStkGraphicsGlobeOverlay)m_Overlay).getExtent_AsObject();
            scene.getCamera().viewExtent("Earth", extent);
            scene.render();
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        ((IAgAnimation)root).rewind();
        double dstart = Double.parseDouble(m_Start.format("epSec"));
        double dstop = Double.parseDouble(m_End.format("epSec"));
        OverlayHelper.getTimeDisplay().removeInterval(this,dstart, dstop);
        OverlayHelper.removeTimeOverlay(this,((IAgScenario)root.getCurrentScenario()).getSceneManager());
        OverlayHelper.removeTextBox(((IAgScenario)root.getCurrentScenario()).getSceneManager());

        if (m_Overlay != null)
        {
	        scene.getCentralBodies().getEarth().getImagery().remove(m_Overlay);
	        m_Overlay = null;
        }
        scene.render();
	}
}