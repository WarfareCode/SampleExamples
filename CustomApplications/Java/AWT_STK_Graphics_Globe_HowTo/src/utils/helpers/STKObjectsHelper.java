package utils.helpers;

//AGI Java API
import agi.core.*;
import agi.stkobjects.*;

public class STKObjectsHelper
{
	/**
	*Setup defaults for code snippets in howto:
	* 2pm 5/30 - 5/31, time = start, step = 15, looped animation
	* This time will give good lighting on the side of the globe where most of the examples will be.
	*/
	public static void setAnimationDefaults(AgStkObjectRootClass root)
	throws AgCoreException
	{
		IAgAnimation animationControl = (IAgAnimation)root;
		IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
		IAgScAnimation animationSettings = ((IAgScenario)root.getCurrentScenario()).getAnimation();

		animationControl.rewind();
		scenario.setStartTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:00:00.000").format("epSec"))));
		scenario.setStopTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "31 May 2008 14:00:00.000").format("epSec"))));
		animationSettings.setStartTime(new Double(Double.parseDouble(root.getConversionUtility().newDate("UTCG", "30 May 2008 14:00:00.000").format("epSec"))));
		animationSettings.setAnimStepValue(15.0);
		animationSettings.setEnableAnimCycleTime(true);
		animationSettings.setAnimCycleType(AgEScEndLoopType.E_LOOP_AT_TIME);
		animationControl.rewind();
	}
	
	public static IAgAnimation getAnimation(IAgStkObjectRoot root) 
	{
		return (IAgAnimation)root;
	}
}