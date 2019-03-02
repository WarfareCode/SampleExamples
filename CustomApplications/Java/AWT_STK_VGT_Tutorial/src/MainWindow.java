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

//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
public class MainWindow
extends CustomApplicationSTKSampleBaseJFrame
implements ActionListener, IAgAnimationJToolBarEventsListener
{
	private final static long		serialVersionUID	= 1L;
	private final static String		s_TITLE				= "CustomApp_AWT_STK_VGT_Tutorial";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;

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

	public void actionPerformed(ActionEvent e)
	{
		try
		{
			String cmd = e.getActionCommand();

			if(cmd.equals(SampleJPanel.s_TEXT_STEP1_SCENARIO))
			{
				this.m_SampleCode.createScenario();
				this.m_SampleJPanel.m_1JButton.setEnabled(false);
				this.m_SampleJPanel.m_2JButton.setEnabled(true);
				this.m_SampleJPanel.m_11JButton.setEnabled(false);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP2_SATELLITE))
			{
				this.m_SampleCode.createSatellite();
				this.m_SampleJPanel.m_2JButton.setEnabled(false);
				this.m_SampleJPanel.m_3JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP3_FACILITY))
			{
				this.m_SampleCode.createFacility();
				this.m_SampleJPanel.m_3JButton.setEnabled(false);
				this.m_SampleJPanel.m_4JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP4_VIEWPOINT))
			{
				this.m_SampleCode.viewPoint();
				this.m_SampleJPanel.m_4JButton.setEnabled(false);
				this.m_SampleJPanel.m_5JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP5_DISPLACEMENTVECTOR))
			{
				this.m_SampleCode.displacementVector();
				this.m_SampleJPanel.m_5JButton.setEnabled(false);
				this.m_SampleJPanel.m_6JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP6_VELOCITYVECTOR))
			{
				this.m_SampleCode.velocityVector();
				this.m_SampleJPanel.m_6JButton.setEnabled(false);
				this.m_SampleJPanel.m_7JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP7_AXES))
			{
				this.m_SampleCode.axes();
				this.m_SampleJPanel.m_7JButton.setEnabled(false);
				this.m_SampleJPanel.m_8JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP8_PLANE))
			{
				this.m_SampleCode.plane();
				this.m_SampleJPanel.m_8JButton.setEnabled(false);
				this.m_SampleJPanel.m_9JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP9_ANGLES))
			{
				this.m_SampleCode.angles();
				this.m_SampleJPanel.m_9JButton.setEnabled(false);
				this.m_SampleJPanel.m_10JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP10_SAVE))
			{
				this.m_SampleCode.saveScenario();
				this.m_SampleJPanel.m_10JButton.setEnabled(false);
				this.m_SampleJPanel.m_11JButton.setEnabled(true);
			}
			else if(cmd.equals(SampleJPanel.s_TEXT_STEP11_RESET))
			{
				this.m_SampleCode.reset();
				this.m_SampleJPanel.m_11JButton.setEnabled(false);
				this.m_SampleJPanel.m_1JButton.setEnabled(true);
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
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

	private class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();

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