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
import agi.stk.core.swing.toolbars.animation.*;
import agi.stkengine.*;

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
	private final static String		s_TITLE				= "CustomApp_AWT_STK_Objects_HowTo";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;

	private JSplitPane				m_MainSplitPane;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;
	private AgMapCntrlClass			m_AgMapCntrlClass;

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
		AgAwt_JNI.initialize_AwtDelegate();
		AgStkCustomApplication_JNI.initialize(true); // true parameter allows for smart auto class cast
		AgAwt_JNI.initialize_AwtComponents();

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

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

		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();
		this.setRoot(this.m_AgStkObjectRootClass);

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
		this.m_AgGlobeCntrlClass.setBackground(awtColor);

		this.m_AgMapCntrlClass = new AgMapCntrlClass();
		this.m_AgMapCntrlClass.setBackColor(stkxColor);
		this.m_AgMapCntrlClass.setBackground(awtColor);

		this.m_MainSplitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, this.m_AgGlobeCntrlClass, this.m_AgMapCntrlClass);
		this.m_MainSplitPane.setResizeWeight(0.5);
		this.getContentPane().add(this.m_MainSplitPane, BorderLayout.CENTER);

		JPanel toolbarJPanel = new JPanel();
		toolbarJPanel.setLayout(new FlowLayout(FlowLayout.LEFT));
		
		this.m_AgAnimationJToolBar = new AgAnimationJToolBar();
		this.m_AgAnimationJToolBar.addAnimationJToolBarListener(this);
		toolbarJPanel.add(this.m_AgAnimationJToolBar);

		this.getContentPane().add(toolbarJPanel, BorderLayout.NORTH);

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

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
		
		this.m_SampleJPanel.reset();
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
				this.m_SampleCode.createScenario();
				
				this.m_SampleJPanel.getStepOne().setEnabled(false);
				this.m_SampleJPanel.getStepTwo().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_2))
			{
				String objectName = this.m_SampleCode.createHomeFacility();
				this.m_SampleCode.viewObject(objectName);

				this.m_SampleJPanel.getStepTwo().setEnabled(false);
				this.m_SampleJPanel.getStepThree().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_3))
			{
				this.m_SampleCode.createHomeFacilityVectors();

				this.m_SampleJPanel.getStepThree().setEnabled(false);
				this.m_SampleJPanel.getStepFour().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_4))
			{
				String objectName = this.m_SampleCode.createSamSite();
				this.m_SampleCode.viewObject(objectName);

				this.m_SampleJPanel.getStepFour().setEnabled(false);
				this.m_SampleJPanel.getStepFive().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_5))
			{
				this.m_SampleCode.createSamSiteRings();

				this.m_SampleJPanel.getStepFive().setEnabled(false);
				this.m_SampleJPanel.getStepSix().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_6))
			{
				this.m_SampleCode.createSamSpinningSensor();

				this.m_SampleJPanel.getStepSix().setEnabled(false);
				this.m_SampleJPanel.getStepSeven().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_7))
			{
				String objectName = this.m_SampleCode.createSamThreatDome();
				this.m_SampleCode.viewObject(objectName);

				this.m_SampleJPanel.getStepSeven().setEnabled(false);
				this.m_SampleJPanel.getStepEight().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_8))
			{
				String objectName = this.m_SampleCode.createSafeAirCorridor();
				this.m_SampleCode.viewObject(objectName);

				this.m_SampleJPanel.getStepEight().setEnabled(false);
				this.m_SampleJPanel.getStepNine().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_9))
			{
				String objectName = this.m_SampleCode.createF16FlightPath();
				this.m_SampleCode.viewObject(objectName);

				this.m_SampleJPanel.getStepNine().setEnabled(false);
				this.m_SampleJPanel.getStepTen().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_10))
			{
				this.m_SampleCode.createF16Rings();

				this.m_SampleJPanel.getStepTen().setEnabled(false);
				this.m_SampleJPanel.getStepEleven().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_11))
			{
				this.m_SampleCode.createF16Vectors();

				this.m_SampleJPanel.getStepEleven().setEnabled(false);
				this.m_SampleJPanel.getStepTwelve().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_12))
			{
				this.m_SampleCode.createF16DropDownLines();

				this.m_SampleJPanel.getStepTwelve().setEnabled(false);
				this.m_SampleJPanel.getStepThirteen().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_13))
			{
				this.m_SampleCode.createF16DataDisplay();

				this.m_SampleJPanel.getStepThirteen().setEnabled(false);
				this.m_SampleJPanel.getStepFourteen().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_14))
			{
				this.m_SampleCode.createTargetedSensor();

				this.m_SampleJPanel.getStepFourteen().setEnabled(false);
				this.m_SampleJPanel.getStepFifteen().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_15))
			{
				String objectName = this.m_SampleCode.createGroundVehicle();
				this.m_SampleCode.viewObject(objectName);

				this.m_SampleJPanel.getStepFifteen().setEnabled(false);
				this.m_SampleJPanel.getStepSixteen().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_16))
			{
				String objectName = this.m_SampleCode.createSatellite();
				this.m_SampleCode.viewObject(objectName);

				this.m_SampleJPanel.getStepSixteen().setEnabled(false);
				this.m_SampleJPanel.getStepSeventeen().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_17))
			{
				String objectName = this.m_SampleCode.createShip();
				this.m_SampleCode.viewObject(objectName);

				this.m_SampleJPanel.getStepSeventeen().setEnabled(false);
				this.m_SampleJPanel.getStepEighteen().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_18))
			{
				this.m_SampleCode.createMto();

				this.m_SampleJPanel.getStepEighteen().setEnabled(false);
				this.m_SampleJPanel.getStepNineteen().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_19))
			{
				this.m_SampleCode.createSamF16Access();

				this.m_SampleJPanel.getStepNineteen().setEnabled(false);
				this.m_SampleJPanel.getStepTwenty().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_20))
			{
				this.m_SampleCode.createDataProviders();

				this.m_SampleJPanel.getStepTwenty().setEnabled(false);
				this.m_SampleJPanel.getStepTwentyOne().setEnabled(true);
			}
			else if(command.equalsIgnoreCase(SampleJPanel.s_STEP_21))
			{
				this.m_SampleCode.reset();
				this.m_SampleJPanel.reset();
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

	private class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();
				MainWindow.this.m_AgMapCntrlClass.dispose();

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

	public void onAnimationJToolBarAction(AgAnimationJToolBarEvent e)
	{
		try
		{
			int action = e.getAnimationJToolBarAction();
			if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_REWIND)
			{
				this.m_AgStkObjectRootClass.rewind();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PLAYFORWARD)
			{
				this.m_AgStkObjectRootClass.playForward();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PLAYBACKWARD)
			{
				this.m_AgStkObjectRootClass.playBackward();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PAUSE)
			{
				this.m_AgStkObjectRootClass.pause();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_STEPFORWARD)
			{
				this.m_AgStkObjectRootClass.stepForward();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_STEPBACKWARD)
			{
				this.m_AgStkObjectRootClass.stepBackward();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_FASTER)
			{
				this.m_AgStkObjectRootClass.faster();
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_SLOWER)
			{
				this.m_AgStkObjectRootClass.slower();
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
}