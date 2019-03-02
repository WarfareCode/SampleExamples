// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.logging.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.stkengine.*;
import agi.stk.core.swing.toolbars.animation.*;

//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements ActionListener, IAgAnimationJToolBarEventsListener
{
	private final static long		serialVersionUID	= 1L;
	private final static String		s_TITLE				= "CustomApp_AWT_STK_Objects_Tutorial_End";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	// TODO: Declare private STK application class, use variable name of m_AgSTKXApplicationClass
	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	// TODO: Declare private STK object root class, use variable name of m_AgStkObjectRootClass
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	// TODO: Declare private STK Globe Cntrl class, use variable name of m_AgGlobeCntrlClass
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;

	private RootEventsAdapter		m_RootEventsAdapter;

	private AgAnimationJToolBar		m_AgAnimationJToolBar;
	private SampleJPanel			m_SampleJPanel;
	private SampleCode				m_SampleCode;

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
		// Initialize the STK Engine Java application with the AgAwt_JNI initialization technique for STKX Custom Applications.
		// If you don't, none of the Java api calls will be successful.

		// TODO: initialize the AWT Delegate
		AgAwt_JNI.initialize_AwtDelegate();

		// TODO: initialize the JNI Custom Application with a parameter of true for auto class casting
		AgStkCustomApplication_JNI.initialize(true); // true parameter allows for smart auto class cast
		
		// TODO: initialize the AWT Components
        AgAwt_JNI.initialize_AwtComponents();

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

		// TODO: define the STK application
		this.m_AgSTKXApplicationClass = new AgSTKXApplicationClass();
		this.setApp(this.m_AgSTKXApplicationClass);

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
		{
			String msg = "You do not have the required STK Globe license.  The sample's globe will not display properly.";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
		}

        // TODO: Define the STK object root using m_AgStkObjectRootClass variable
		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();
        // TODO: Create an instance of the RootEventsAdapter
		this.m_RootEventsAdapter = new RootEventsAdapter();
        // TODO: Add a listener for STK Object Root events with the RootEventsAdapter using addIAgStkObjectRootEvents2 method
		this.m_AgStkObjectRootClass.addIAgStkObjectRootEvents2(this.m_RootEventsAdapter);
		this.setRoot(this.m_AgStkObjectRootClass);

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		// TODO: define the STK Globe Cntrl using m_AgGlobeCntrlClass variable
		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
		this.m_AgGlobeCntrlClass.setBackground(awtColor);
        // TODO: Assign the m_AgGlobeCntrlClass variable for the Globe Cntrl as a component of the frame's content pane., using a center borderlayout.
		this.getContentPane().add(this.m_AgGlobeCntrlClass, BorderLayout.CENTER);

		this.m_AgAnimationJToolBar = new AgAnimationJToolBar();
		this.m_AgAnimationJToolBar.addAnimationJToolBarListener(this);
		this.getContentPane().add(this.m_AgAnimationJToolBar, BorderLayout.NORTH);

		this.m_SampleJPanel = new SampleJPanel();
		this.m_SampleJPanel.addActionListener(this);
		this.getContentPane().add(this.m_SampleJPanel, BorderLayout.EAST);

		this.m_SampleCode = new SampleCode(this.m_AgStkObjectRootClass);

		// Remove unwanted menu bars for this sample
		JMenu scenarioJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getScenarioJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(scenarioJMenu);
		JMenu vdfJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getVDFJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(vdfJMenu);
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	public void actionPerformed(ActionEvent event)
	{
		String command = event.getActionCommand();

		try
		{
			((Component)this).setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));

			if(command.equalsIgnoreCase(SampleJPanel.s_STEP_1))
			{
				this.m_SampleCode.setUnits();
				this.m_SampleCode.getStkHomeDir();
				this.m_SampleCode.createScenario();
				this.m_SampleJPanel.getStepOne().setEnabled(false);
				this.m_SampleJPanel.getStepTwo().setEnabled(true);
				this.m_SampleJPanel.getStepEight().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_2))
			{
				this.m_SampleCode.createFacility();
				this.m_SampleJPanel.getStepTwo().setEnabled(false);
				this.m_SampleJPanel.getStepThree().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_3))
			{
				this.m_SampleCode.createSatellite();
				this.m_SampleJPanel.getStepThree().setEnabled(false);
				this.m_SampleJPanel.getStepFour().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_4))
			{
				this.m_SampleCode.createAccess();
				this.m_SampleJPanel.getStepFour().setEnabled(false);
				this.m_SampleJPanel.getStepFive().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_5))
			{
				this.m_SampleCode.createVectors();
				this.m_SampleJPanel.getStepFive().setEnabled(false);
				this.m_SampleJPanel.getStepSix().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_6))
			{
				this.m_SampleCode.createDataDisplay();
				this.m_SampleJPanel.getStepSix().setEnabled(false);
				this.m_SampleJPanel.getStepSeven().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_7))
			{
				this.m_SampleCode.createDataProviders();
				this.m_SampleJPanel.getStepSeven().setEnabled(false);
				this.m_SampleJPanel.getStepEight().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_8))
			{
				this.m_SampleCode.saveAndUnloadScenario();
				this.m_SampleJPanel.getStepEight().setEnabled(false);
				this.m_SampleJPanel.getStepOne().setEnabled(true);
			}
		}
		catch(AgCoreException sce)
		{
			System.out.println();
			System.out.println("Description = " + sce.getDescription());
			System.out.println("HRESULT hr = 0x" + sce.getHResultAsHexString());
			sce.printStackTrace();
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
		finally
		{
			((Component)this).setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public class RootEventsAdapter
	implements IAgStkObjectRootEvents2
	{
		public void onAgStkObjectRootEvent(AgStkObjectRootEvent e)
		{
			try
			{
				Object src = e.getSource();
				int type = e.getType();
				Object[] params = e.getParams();

				StringBuffer sb = new StringBuffer();
				if(params != null)
				{
					for(int i = 0; i < params.length; i++)
					{
						sb.append(params[i].toString());
						sb.append(" ");
					}
				}

				System.out.println("AgStkObjectRootEvent(src=" + src.toString() + ", type=" + type + ", " + sb.toString());
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	public void onAnimationJToolBarAction(AgAnimationJToolBarEvent e)
	{
		try
		{
			int action = e.getAnimationJToolBarAction();
			if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_REWIND)
			{
				// TODO: call the rewind/reset method on the m_AgStkObjectRootClass variable;
				this.m_AgStkObjectRootClass.rewind();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PLAYFORWARD)
			{
				// TODO: call the playForward method on the m_AgStkObjectRootClass variable;
				this.m_AgStkObjectRootClass.playForward();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PLAYBACKWARD)
			{
				// TODO: call the playBackward method on the m_AgStkObjectRootClass variable;
				this.m_AgStkObjectRootClass.playBackward();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PAUSE)
			{
				// TODO: call the pause method on the m_AgStkObjectRootClass variable;
				this.m_AgStkObjectRootClass.pause();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_STEPFORWARD)
			{
				// TODO: call the stepForward method on the m_AgStkObjectRootClass variable;
				this.m_AgStkObjectRootClass.stepForward();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_STEPBACKWARD)
			{
				// TODO: call the stepBackward method on the m_AgStkObjectRootClass variable;
				this.m_AgStkObjectRootClass.stepBackward();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_FASTER)
			{
				// TODO: call the faster method on the m_AgStkObjectRootClass variable;
				this.m_AgStkObjectRootClass.faster();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_SLOWER)
			{
				// TODO: call the slower method on the m_AgStkObjectRootClass variable;
				this.m_AgStkObjectRootClass.slower();
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
	            // TODO: call removeIAgStkObjectRootEvents2 on the m_AgStkObjectRootClass and pass
        		// in the member variable of the m_RootEventsAdapter that was created when this application
        		// was starting up
				MainWindow.this.m_AgStkObjectRootClass.removeIAgStkObjectRootEvents2(MainWindow.this.m_RootEventsAdapter);

        		// TODO:  Since we used the EXIT_ON_CLOSE window close operation, we need to 
        		// dispose of our Globe Control before uninitializing the Java API.  
        		// Call the dispose method on the m_AgGlobeCntrlClass
				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();

        		// TODO: Uninitialize the STK Engine Java libraries.
        		// This ensure's that all Native resources are cleaned properly.
        		// If you don't you will get error reported upon application exit
				// Reverse of the initialization order
				AgAwt_JNI.uninitialize_AwtComponents();
				AgStkCustomApplication_JNI.uninitialize();
				AgAwt_JNI.uninitialize_AwtDelegate();
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}
}