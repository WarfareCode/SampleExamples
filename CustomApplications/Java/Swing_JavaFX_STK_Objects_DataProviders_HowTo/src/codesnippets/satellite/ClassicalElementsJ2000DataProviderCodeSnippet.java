package codesnippets.satellite;

//#region Imports

//AGI Java API
import agi.stkobjects.*;

//Sample API
import charts.*;

//#endregion

public class ClassicalElementsJ2000DataProviderCodeSnippet
extends SatelliteDataProviderCodeSnippet
{
	public ClassicalElementsJ2000DataProviderCodeSnippet()
	{
		super("Classical Elements J2000", "satellite", "ClassicalElementsJ2000DataProviderCodeSnippet.java");
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
		IAgDataPrvTimeVar timeVar = dpc.getDataPrvTimeVarFromPath("Classical Elements/J2000");
		IAgDrResult result = timeVar.execElementsNativeTimes(startTime, stopTime, new Object[]{"Time", "Semi-major Axis", "Inclination", "RAAN", "Arg of Perigee", "Mean Anomaly"});
		//IAgDrResult result = timeVar.execNativeTimes(startTime, stopTime);
		
		IAgDrDataSetCollection dsc = result.getDataSets();

		Object[] times = (Object[])dsc.getItem(0).getValues_AsObject();
		Object[] inclination = (Object[])dsc.getItem(2).getValues_AsObject();
		Object[] raan = (Object[])dsc.getItem(3).getValues_AsObject();
		Object[] aop = (Object[])dsc.getItem(4).getValues_AsObject();
		Object[] ma = (Object[])dsc.getItem(5).getValues_AsObject();
		
		CategoryNumberLineChartJFXPanel p = new CategoryNumberLineChartJFXPanel();
		p.initScene(-50, 400, 10);
		String title = className +"-"+instanceName+": Classical Elements J2000 - "+ChartDataHelper.getCurrentTimeAsStkTime();
		String xTitle = "Time (UTCG)";
		String yTitle = "Angle (deg)";

		p.setData(title, xTitle, yTitle, times, new Object[]{"Inclination", "RAAN", "Arg of Perigee", "Mean Anomaly"}, new Object[]{inclination, raan, aop, ma});
		helper.addTab(className +"-"+instanceName+"-" + "J2000 Classical Orbital Elements", p);

		//#endregion
	}
}