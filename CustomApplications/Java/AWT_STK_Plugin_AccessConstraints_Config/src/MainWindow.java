// Java API
import java.io.*;
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.*;
import agi.core.logging.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.stk.plugin.*;
import agi.stkengine.*;

// CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame 
implements ActionListener
{
	private final static long		serialVersionUID		= 1L;

	private final static String s_TITLE = "CustomApp_AWT_STK_Plugins_AccessConstraints_Config";
	private final static String s_DESCFILENAME 	= "AppDescription.html";
	
	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;
	private JButton					m_RunAccessComputationJButton;
	
	private final static String 	s_JavaPluginName = "Java.Config";

	private AgScenarioClass			m_ScenObj;
    private AgFacilityClass			m_FacObj;
    private AgAircraftClass			m_AirObj;
    
    private RootEventsAdapter 		m_RootEventsAdapter;

	public MainWindow()
	throws Throwable
	{
		super(MainWindow.class.getResource(s_DESCFILENAME));
		
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
		AgStkExtension_JNI.initialize(true); // true parameter allows for smart auto class cast
		AgAwt_JNI.initialize_AwtComponents();

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

		this.m_AgSTKXApplicationClass = new AgSTKXApplicationClass();
		super.setApp(this.m_AgSTKXApplicationClass);

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
		{
			String msg = "You do not have the required STK Globe license.  Exiting!";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}

		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();
        this.m_RootEventsAdapter = new RootEventsAdapter();
        this.m_AgStkObjectRootClass.addIAgStkObjectRootEvents2(this.m_RootEventsAdapter);
		super.setRoot(this.m_AgStkObjectRootClass);

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);
		
		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
		this.m_AgGlobeCntrlClass.setBackground(awtColor);
		this.getContentPane().add(this.m_AgGlobeCntrlClass, BorderLayout.CENTER);

		this.m_RunAccessComputationJButton = new JButton();
		this.m_RunAccessComputationJButton.setText("Run Access Computation");
		this.m_RunAccessComputationJButton.addActionListener(this);
		this.getContentPane().add(this.m_RunAccessComputationJButton, BorderLayout.SOUTH);
		
		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		JMenu scenarioJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getScenarioJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(scenarioJMenu);
		JMenu vdfJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getVDFJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(vdfJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				MainWindow.this.m_AgStkObjectRootClass.removeIAgStkObjectRootEvents2(m_RootEventsAdapter);

				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();

				// Reverse of the initialization order
				AgAwt_JNI.uninitialize_AwtComponents();
				AgStkExtension_JNI.uninitialize(); 
				AgStkCustomApplication_JNI.uninitialize();
				AgAwt_JNI.uninitialize_AwtDelegate();
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	public void actionPerformed(ActionEvent e) 
	{
		try
		{
			Object src = e.getSource();
			
			if(src.equals(this.m_RunAccessComputationJButton))
			{
		    	this.createScenario();
		    	this.createFacility();
		    	this.createAircraft();
		    	this.configureJavaPlugin();
		    	this.computeAccess();
		    	this.saveAccessReport();
		    	this.saveScenario();
		    	this.unconfigureJavaPlugin();
		    	this.m_AgStkObjectRootClass.closeScenario();
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void createScenario() 
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.newScenario("Java_Auto_Stk_Plugin_AccCnstrnt_Config");
		m_ScenObj = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
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
				acp.setMax(new Double(100.0));

				acp.setEnableMin(true);
				acp.setMin(new Double(0.0));
				
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
		System.out.println("Java Properties");
		
		System.out.println("\tVendor = "+acp.getProperty_AsObject("Java Properties.Vendor"));
		System.out.println("\tVendor URL = "+acp.getProperty_AsObject("Java Properties.Vendor URL"));
		System.out.println("\tVersion = "+acp.getProperty_AsObject("Java Properties.Version"));
		System.out.println("\tSpecification.Name = "+acp.getProperty_AsObject("Java Properties.Specification.Name"));
		System.out.println("\tSpecification.Vendor = "+acp.getProperty_AsObject("Java Properties.Specification.Vendor"));
		System.out.println("\tSpecification.Version = "+acp.getProperty_AsObject("Java Properties.Specification.Version"));

		// etc.
		
		System.out.println("Dispatch Properties");
		
		System.out.println("\tBoolean Properties");
		System.out.println("\t\tBoolean Read Only = "+acp.getProperty_AsObject("Dispatch Properties.Boolean Properties.Boolean Read Only"));
		System.out.println("\t\tBoolean Read Write = "+acp.getProperty_AsObject("Dispatch Properties.Boolean Properties.Boolean Read Write"));
		System.out.println("\t\tBoolean Transient = "+acp.getProperty_AsObject("Dispatch Properties.Boolean Properties.Boolean Transient"));
		
		System.out.println("\tInteger Properties");
		System.out.println("\t\tInteger Read Only = "+acp.getProperty_AsObject("Dispatch Properties.Integer Properties.Integer Read Only"));
		System.out.println("\t\tInteger Read Write = "+acp.getProperty_AsObject("Dispatch Properties.Integer Properties.Integer Read Write"));
		System.out.println("\t\tInteger Transient = "+acp.getProperty_AsObject("Dispatch Properties.Integer Properties.Integer Transient"));
		System.out.println("\t\tInteger Min = "+acp.getProperty_AsObject("Dispatch Properties.Integer Properties.Integer Min"));
		System.out.println("\t\tInteger MinMax = "+acp.getProperty_AsObject("Dispatch Properties.Integer Properties.Integer MinMax"));

		// ...
		
		System.out.println("\tString Properties");
		System.out.println("\t\tString Read Only = "+acp.getProperty_AsObject("Dispatch Properties.String Properties.String Read Only"));
		System.out.println("\t\tString Read Write = "+acp.getProperty_AsObject("Dispatch Properties.String Properties.String Read Write"));
		System.out.println("\t\tString Transient = "+acp.getProperty_AsObject("Dispatch Properties.String Properties.String Transient"));
		System.out.println("\t\tString MultiLine = "+acp.getProperty_AsObject("Dispatch Properties.String Properties.String MultiLine"));
		
		// etc.
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
			this.m_AgStkObjectRootClass.executeCommand("ReportCreate */Facility/fac1 Type Save Style \"Access\" File \""+reportFile+"\" AccessObject */Aircraft/air1");
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
			this.m_AgStkObjectRootClass.saveScenarioAs(scFileName);
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