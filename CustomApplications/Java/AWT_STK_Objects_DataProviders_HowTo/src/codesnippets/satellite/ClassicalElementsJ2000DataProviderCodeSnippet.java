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
		Object[] semimajor = (Object[])dsc.getItem(1).getValues_AsObject();
		Object[] inclination = (Object[])dsc.getItem(2).getValues_AsObject();
		Object[] raan = (Object[])dsc.getItem(3).getValues_AsObject();
		Object[] aop = (Object[])dsc.getItem(4).getValues_AsObject();
		Object[] ma = (Object[])dsc.getItem(5).getValues_AsObject();
		
		ChartTimeAngleDistanceJPanel p = new ChartTimeAngleDistanceJPanel();
		String title = className +"-"+instanceName+": Classical Elements J2000 - "+ChartDataHelper.getCurrentTimeAsStkTime();
		p.setData(title, times, new Object[]{"Inclination", "RAAN", "Arg of Perigee", "Mean Anomaly"}, new Object[]{inclination, raan, aop, ma}, new Object[] {"Semi-major Axis"}, new Object[]{semimajor});
		helper.addTab(className +"-"+instanceName+"-" + "J2000 Classical Orbital Elements", p);

		//#endregion
	}
}