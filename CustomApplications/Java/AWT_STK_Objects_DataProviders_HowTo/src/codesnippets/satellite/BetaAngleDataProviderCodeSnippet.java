package codesnippets.satellite;

//#region Imports

// AGI Java API
import agi.stkobjects.*;

// Sample API
import charts.*;

//#endregion

public class BetaAngleDataProviderCodeSnippet
extends SatelliteDataProviderCodeSnippet
{
	public BetaAngleDataProviderCodeSnippet()
	{
		super("Beta Angle", "satellite", "BetaAngleDataProviderCodeSnippet.java");
	}

	public void execute(IChartDisplayHelper helper, AgStkObjectRootClass root, IAgStkObject object)
	throws Throwable
	{
		//#region CodeSnippet
		IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
		Object startTime = scenario.getStartTime_AsObject();
		Object stopTime = scenario.getStopTime_AsObject();
		String className = object.getClassName();
		String instanceName = object.getInstanceName();
		
		IAgDataProviderCollection dpc = object.getDataProviders();
		IAgDataPrvTimeVar timeVar = dpc.getDataPrvTimeVarFromPath("Beta Angle");
		IAgDrResult result = timeVar.execNativeTimes(startTime, stopTime);
		IAgDrDataSetCollection dsc = result.getDataSets();

		Object[] times = (Object[])dsc.getItem(0).getValues_AsObject();
		Object[] betaAngles = (Object[])dsc.getItem(1).getValues_AsObject();
		
		ChartTimeAngleJPanel p = new ChartTimeAngleJPanel();
		String title = className +"-"+instanceName+": Beta Angle - "+ChartDataHelper.getCurrentTimeAsStkTime();
		p.setData(title, "Time (UTCG)", times, "Beta Angle (deg)", betaAngles);
		helper.addTab(className +"-"+instanceName+"-" + "Beta Angle", p);

		//#endregion
	}
}