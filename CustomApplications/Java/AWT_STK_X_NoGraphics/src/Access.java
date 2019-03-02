// Java API
import java.util.logging.*;

//AGI Java API
import agi.core.AgCoreException;
import agi.core.AgException;
import agi.core.logging.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkobjects.*;
import agi.stkengine.*;


class Access
{
	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgScenarioClass			m_ScenObj;
	private AgFacilityClass			m_FacObj;
	private AgAircraftClass			m_AirObj;

	public void compute()
	throws Throwable
	{
		initialize();
		createScenario();
		createFacility();
		createAircraft();
		computeAccess();
		uninitialize();
	}
	
	private void initialize()
	throws AgException
	{
		// ================================================
		// Set the logging level to Level.FINEST to get
		// all AGI java console logging
		// ================================================
		ConsoleHandler ch = new ConsoleHandler();
		ch.setLevel(Level.OFF);
		ch.setFormatter(new AgFormatter());
		Logger.getLogger("agi").setLevel(Level.OFF);
		Logger.getLogger("agi").addHandler(ch);

		// =========================================
		// This must be called before all
		// AWT/Swing/StkUtil/Stkx/StkObjects calls
		// =========================================
		AgAwt_JNI.initialize_AwtDelegate();
		AgStkCustomApplication_JNI.initialize(true); // true parameter allows for smart auto class cast
		AgAwt_JNI.initialize_AwtComponents();

		//You must set NoGraphics to true before you construct AgStkObjectRootClass
		m_AgSTKXApplicationClass = new AgSTKXApplicationClass();
		m_AgSTKXApplicationClass.setNoGraphics(true);

		if(!m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			System.out.println(msg);
			System.exit(0);
		}

		if(!m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
		{
			String msg = "You do not have the required STK Globe license.  Exiting!";
			System.out.println(msg);
			System.exit(0);
		}

		m_AgStkObjectRootClass = new AgStkObjectRootClass();
	}
	
	private void createScenario() 
	throws AgCoreException
	{
		m_AgStkObjectRootClass.newScenario("NoGraphics");
		m_ScenObj = (AgScenarioClass)m_AgStkObjectRootClass.getCurrentScenario();
	}

	private void createFacility() 
	throws AgCoreException
	{
		IAgStkObjectCollection children = m_ScenObj.getChildren();
		m_FacObj = (AgFacilityClass)children._new(AgESTKObjectType.E_FACILITY, "MyFac");
	}
	
	private void createAircraft()
	throws AgCoreException
	{
		IAgStkObjectCollection children = m_ScenObj.getChildren();
		m_AirObj = (AgAircraftClass)children._new(AgESTKObjectType.E_AIRCRAFT, "air1");

		m_AirObj.setRouteType(AgEVePropagatorType.E_PROPAGATOR_GREAT_ARC);
		IAgVePropagatorGreatArc garc = (IAgVePropagatorGreatArc)m_AirObj.getRoute();
		IAgVeWaypointsCollection waypoints = garc.getWaypoints();

		IAgVeWaypointsElement wp1 = waypoints.add();
		wp1.setAltitude(2.0);
		wp1.setLatitude(new Double(39.842));
		wp1.setLongitude(new Double(-75.596));

		IAgVeWaypointsElement wp2 = waypoints.add();
		wp2.setAltitude(2.0);
		wp2.setLatitude(new Double(40.393));
		wp2.setLongitude(new Double(-75.632));
		
		garc.propagate();
	}
	
	private void computeAccess() 
	throws AgCoreException
	{
		Object startTime = m_ScenObj.getStartTime_AsObject();
		Object stopTime = m_ScenObj.getStopTime_AsObject();
		
		IAgStkAccess access = m_FacObj.getAccess("Aircraft/air1");
		access.computeAccess();
		
		IAgDataProviderCollection dpc = null;
		dpc = access.getDataProviders();
		
		IAgDataProviderInfo dpi = null;
		dpi = dpc.getItem("Access Data");
		
		IAgDataPrvInterval dpintv = null;
		dpintv = (IAgDataPrvInterval)dpi;
		
		IAgDrResult result = null;
		result = dpintv.exec(startTime, stopTime);

		IAgDrDataSetCollection ddsc = result.getDataSets();

		//=================================
		// Print column names
		//=================================
		Object elementNames = ddsc.getElementNames_AsObject();
		if(elementNames instanceof Object[])
		{
			Object[] columnNames = (Object[])elementNames;
			int namecount = columnNames.length;
			for(int nameindex = 0; nameindex < namecount; nameindex++)
			{
				Object name = columnNames[nameindex];
				System.out.print(name);
				System.out.print("\t");
			}
			System.out.println();
		}

		//=================================
		// Print column values for each row
		//=================================
		int rowcount = ddsc.getRowCount();
		for(int rowindex = 0; rowindex < rowcount; rowindex++)
		{
			Object row = ddsc.getRow_AsObject(rowindex);
			if(row instanceof Object[])
			{
				Object[] columnValues = (Object[])row;
	
				for(int i=0; i < columnValues.length; i++)
				{
					Object obj = columnValues[i];
					System.out.print(obj);
					System.out.print("\t");
				}
				System.out.println();
			}
		}
	}
	
	private void uninitialize()
	throws AgException
	{ 
		m_AgStkObjectRootClass.closeScenario();
		m_AgStkObjectRootClass.release();
		
		// Tell the JVM it should finalize classes
		// and garbage collect.  However, there is 
		// no guarantee of this.
        System.runFinalization();
        System.gc();
		
		AgAwt_JNI.uninitialize_AwtComponents();
		AgStkCustomApplication_JNI.uninitialize();
		AgAwt_JNI.uninitialize_AwtDelegate();
	}
}