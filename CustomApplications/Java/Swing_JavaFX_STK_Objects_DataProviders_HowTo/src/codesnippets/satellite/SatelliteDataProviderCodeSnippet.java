package codesnippets.satellite;

// AGI Java API
import charts.IChartDisplayHelper;

import agi.stkobjects.*;

// Sample API
import codesnippets.*;

public abstract class SatelliteDataProviderCodeSnippet
extends DataProviderCodeSnippet
{
	private final static String s_OBJECT_NAME = "Satellite1";
	
	public SatelliteDataProviderCodeSnippet(String name, String... fileParts)
	{
		super(name, fileParts);
	}

	public void execute(IChartDisplayHelper helper, AgStkObjectRootClass root)
	throws Throwable
	{
		AgScenarioClass scenario = (AgScenarioClass)root.getCurrentScenario();
		IAgStkObjectCollection children = scenario.getChildren();
		IAgStkObject object = children.getItem(s_OBJECT_NAME);
		execute(helper, root, object);
	}
	
	public abstract void execute(IChartDisplayHelper helper, AgStkObjectRootClass root, IAgStkObject object) throws Throwable;
}