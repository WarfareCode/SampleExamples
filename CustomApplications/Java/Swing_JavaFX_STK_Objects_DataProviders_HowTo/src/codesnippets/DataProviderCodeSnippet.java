package codesnippets;

//AGI Java API
import agi.stkobjects.*;

//Samples API
import agi.samples.sharedresources.codesnippets.*;
import charts.IChartDisplayHelper;

public abstract class DataProviderCodeSnippet
extends CodeSnippet
{
	public DataProviderCodeSnippet(String name, String... fileParts)
	{
		super(name, fileParts);
	}

	/**
	 * Executes the code snippet.
	 */
	public abstract void execute(IChartDisplayHelper helper, AgStkObjectRootClass root)
	throws Throwable;
}