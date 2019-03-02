import java.io.File;
import java.util.logging.ConsoleHandler;
import java.util.logging.Level;
import java.util.logging.Logger;

import agi.core.AgException;
import agi.core.AgCoreException;
import agi.core.awt.AgAwt_JNI;
import agi.core.logging.AgFormatter;
import agi.stk.ui.AgStkAutomation_JNI;
import agi.stk.ui.AgStkUi;
import agi.stkobjects.AgAircraftClass;
import agi.stkobjects.AgEAccessConstraints;
import agi.stkobjects.AgESTKObjectType;
import agi.stkobjects.AgEVePropagatorType;
import agi.stkobjects.AgFacilityClass;
import agi.stkobjects.AgScenarioClass;
import agi.stkobjects.AgSensorClass;
import agi.stkobjects.AgStkObjectRootClass;
import agi.stkobjects.AgStkObjectRootEvent;
import agi.stkobjects.IAgAccessCnstrPluginMinMax;
import agi.stkobjects.IAgAccessConstraint;
import agi.stkobjects.IAgAccessConstraintCollection;
import agi.stkobjects.IAgDataProviderCollection;
import agi.stkobjects.IAgDataProviderInfo;
import agi.stkobjects.IAgDataPrvInterval;
import agi.stkobjects.IAgDrDataSetCollection;
import agi.stkobjects.IAgDrResult;
import agi.stkobjects.IAgStkAccess;
import agi.stkobjects.IAgStkObjectCollection;
import agi.stkobjects.IAgStkObjectRootEvents2;
import agi.stkobjects.IAgVePropagatorGreatArc;
import agi.stkobjects.IAgVeWaypointsCollection;
import agi.stkobjects.IAgVeWaypointsElement;
import agi.stkutil.AgELogMsgType;


public class MainWindow
{
	private final static String 	s_JavaPluginName = "Java.NIIRS";

	private AgStkUi					m_AgStkUi;
	private AgStkObjectRootClass	m_AgStkObjectRoot;

	private AgScenarioClass			m_ScenObj;
    private AgFacilityClass			m_FacObj;
    private AgAircraftClass			m_AirObj;
    private AgSensorClass			m_SenObj;
    
    private RootEventsAdapter 		m_RootEventsAdapter;

    public void run()
    throws AgException
    {
    	this.initialize();
    	this.createScenario();
    	this.createFacility();
    	this.createAircraft();
    	this.configureJavaPlugin();
    	this.computeAccess();
    	this.saveAccessReport();
    	this.saveScenario();
    	this.unconfigureJavaPlugin();
    	this.uninitialize();
    }

    private void initialize()
	throws AgException
	{
    	//================================================
    	// Set the logging level to Level.FINEST to get
		// all AGI java console logging
		//================================================
		ConsoleHandler ch = new ConsoleHandler();
		ch.setLevel(Level.OFF);
		ch.setFormatter(new AgFormatter());
		Logger.getLogger("agi").setLevel(Level.OFF);
		Logger.getLogger("agi").addHandler(ch);

        // =========================================================
        // The following methods must be called before all other 
		// STK Java API calls within a Custom Application
        // =========================================================
        AgAwt_JNI.initialize_AwtDelegate();
        AgStkAutomation_JNI.initialize(true);

        m_AgStkUi = new AgStkUi();
        m_AgStkUi.setVisible(true);
        m_AgStkUi.setUserControl(true);
        m_AgStkObjectRoot = (AgStkObjectRootClass)m_AgStkUi.getIAgStkObjectRoot();

        m_RootEventsAdapter = new RootEventsAdapter();
        m_AgStkObjectRoot.addIAgStkObjectRootEvents2(m_RootEventsAdapter);
	}
	
	private void createScenario() 
	throws AgCoreException
	{
		m_AgStkObjectRoot.newScenario("Java_Auto_Stk_Plugin_AccCnstrnt_NIIRS");
		m_ScenObj = (AgScenarioClass)m_AgStkObjectRoot.getCurrentScenario();
	}

	private void createFacility() 
	throws AgCoreException
	{
		IAgStkObjectCollection children = m_ScenObj.getChildren();
		m_FacObj = (AgFacilityClass)children._new(AgESTKObjectType.E_FACILITY, "fac1");
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
		
		IAgStkObjectCollection airchilds = m_AirObj.getChildren();
		m_SenObj = (AgSensorClass)airchilds._new(AgESTKObjectType.E_SENSOR, "sensor1");
		
	}

	private void configureJavaPlugin() 
	throws AgException
	{
		IAgAccessConstraintCollection acc = m_FacObj.getAccessConstraints();

		if(pluginIsAvailable(acc, s_JavaPluginName))
		{
			IAgAccessConstraint ac = acc.addNamedConstraint(s_JavaPluginName);
			AgEAccessConstraints constraintType = ac.getConstraintType_AsObject();
			if(constraintType == AgEAccessConstraints.E_CSTR_PLUGIN)
			{
				IAgAccessCnstrPluginMinMax acp = null;
				acp = (IAgAccessCnstrPluginMinMax)ac;
				
				acp.setEnableMax(true);
				acp.setMax(new Double(18.3));

				acp.setEnableMin(true);
				acp.setMin(new Double(14.0));
				
				dumpPluginConfiguration(acp);
			}
		}
		else
		{
			throw new AgException(s_JavaPluginName+" Java Plugin was not configured, please configure before running this sample!!");
		}
	}

	private boolean pluginIsAvailable(IAgAccessConstraintCollection acc, String pluginDisplayName)
	throws AgCoreException
	{
		Object[][] ac = acc.availableConstraints().getJavaObject2DArray();
		for(int rowIndex = 0; rowIndex < ac.length; rowIndex++)
		{
			String accname = (String)ac[rowIndex][0];
			//Integer acctype = (Integer)ac[rowIndex][1];
			if(accname.equals(pluginDisplayName))
			{
				return true;
			}
		}
		return false;
	}
	
	private void dumpPluginConfiguration(IAgAccessCnstrPluginMinMax acp)
	throws AgCoreException
	{
		System.out.println("Plugin Properties");
		System.out.println("\tDiameter = "+acp.getProperty_AsObject("Plugin Properties.Diameter"));
		System.out.println("\tWavelength = "+acp.getProperty_AsObject("Plugin Properties.Wavelength"));
		System.out.println("\tOpticalRatio = "+acp.getProperty_AsObject("Plugin Properties.OpticalRatio"));
		System.out.println("\tNIIRS_a = "+acp.getProperty_AsObject("Plugin Properties.NIIRS_a"));
		System.out.println("\tNIIRS_b = "+acp.getProperty_AsObject("Plugin Properties.NIIRS_b"));
		System.out.println("\tNIIRS_RER = "+acp.getProperty_AsObject("Plugin Properties.NIIRS_RER"));
		System.out.println("Debug Properties");
		System.out.println("\tDebugMode = "+acp.getProperty_AsObject("Debug Properties.DebugMode"));
		System.out.println("\tMessageInterval = "+acp.getProperty_AsObject("Debug Properties.MessageInterval"));
	}

	private void computeAccess() 
	throws AgCoreException
	{
		Object startTime = m_ScenObj.getStartTime_AsObject();
		Object stopTime = m_ScenObj.getStopTime_AsObject();
		
		IAgStkAccess access = m_FacObj.getAccessToObject(m_SenObj);
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
			if(columnNames != null)
			{
				int namecount = columnNames.length;
				for(int nameindex = 0; nameindex < namecount; nameindex++)
				{
					Object name = columnNames[nameindex];
					System.out.print(name);
					System.out.print("\t");
				}
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
	
	private String getUserSampleDir()
	{
		String dir = System.getProperty("user.dir");
		String filesep = System.getProperty("file.separator");
		String dataDir = dir + filesep + "data" + filesep;
		File f = new File(dataDir);
		if(!f.exists()) f.mkdir();
		return dataDir;
	}
	
	private void saveAccessReport()
	throws AgCoreException
	{
		String dataDir = getUserSampleDir();
		if(new File(dataDir).exists())
		{
			String reportFile = dataDir + "AccessReportFromSTK.txt";
			m_AgStkObjectRoot.executeCommand("ReportCreate */Facility/fac1 Type Save Style \"Access\" File \""+reportFile+"\" AccessObject */Aircraft/air1/Sensor/sensor1");
		}
		else
		{
			System.err.println("Warning! Did not save access report");
		}
	}

	private void saveScenario() 
	throws AgCoreException
	{
		String dataDir = getUserSampleDir();
		if(new File(dataDir).exists())
		{
			String scFileName = dataDir + m_ScenObj.getInstanceName() + ".sc";
			m_AgStkObjectRoot.saveScenarioAs(scFileName);
		}
		else
		{
			System.err.println("Warning! Did not save example scenario");
		}
	}

	private void unconfigureJavaPlugin() 
	throws AgCoreException
	{
		IAgAccessConstraintCollection acc = m_FacObj.getAccessConstraints();

		if(pluginIsAvailable(acc, s_JavaPluginName))
		{
			acc.removeNamedConstraint(s_JavaPluginName);
		}
	}

	private void uninitialize()
	throws AgException
	{ 
		m_AgStkObjectRoot.removeIAgStkObjectRootEvents2(m_RootEventsAdapter);
		m_AgStkObjectRoot.release();
		m_AgStkUi.release();
		
		// Tell the JVM it should finalize classes
		// and garbage collect.  However, there is 
		// no guarantee of this.
        System.runFinalization();
        System.gc();
        
        // Reverse of the initialization order
        AgStkAutomation_JNI.uninitialize();
        AgAwt_JNI.uninitialize_AwtDelegate();
	}
	
	class RootEventsAdapter
	implements IAgStkObjectRootEvents2
	{
		public void onAgStkObjectRootEvent(AgStkObjectRootEvent e)
		{
			try
			{
				int type = e.getType();
				if(type == AgStkObjectRootEvent.TYPE_ON_LOG_MESSAGE)
				{
					Object[] params = e.getParams();
					String message = (String)params[0];
					Integer msgType = (Integer)params[1];
					Integer errorCode = (Integer)params[2];
					String fileName = (String)params[3];
					Integer lineNo = (Integer)params[4];
					Integer dispID = (Integer)params[5];

					StringBuffer sb = new StringBuffer();
					sb.append(msgType);	// AgELogMsgType
					sb.append(" ");
					sb.append(errorCode);	
					sb.append(" ");
					sb.append(message);
					sb.append(" ");
					sb.append(fileName);
					sb.append(" ");
					sb.append(lineNo);
					sb.append(" ");
					sb.append(dispID);	// AgELogMsgDispId
					sb.append(" ");
					
					if(AgELogMsgType.getFromValue(msgType).equals(AgELogMsgType.E_LOG_MSG_ALARM) || 
							AgELogMsgType.getFromValue(msgType).equals(AgELogMsgType.E_LOG_MSG_WARNING))
					{
						System.err.println(sb.toString());
					}
					else
					{
						System.out.println(sb.toString());
					}
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}
}