// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.core.logging.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.swing.*;
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
	private static final long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_Astrogator_MoonMissionWithBPlaneTargeting";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;

	private JSplitPane				m_PrimarySplitPane;
	private AgGlobeJPanel			m_AgGlobeJPanel1;
	private AgGlobeJPanel			m_AgGlobeJPanel2;

	private JSplitPane				m_SecondarySplitPane;
	private AgMapJPanel				m_AgMapJPanel;

	private SampleCode				m_SampleCode;
	private SampleJPanel			m_SampleJPanel;

	private AgAnimationJToolBar		m_AgAnimationJToolBar;

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
		super.setApp(this.m_AgSTKXApplicationClass);

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}

		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();
		super.setRoot(this.m_AgStkObjectRootClass);

		this.m_SampleCode = new SampleCode(this.m_AgStkObjectRootClass);

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgGlobeJPanel1 = new AgGlobeJPanel();
		this.m_AgGlobeJPanel1.getControl().setBackColor(stkxColor);
		this.m_AgGlobeJPanel1.getControl().setBackground(awtColor);

		this.m_AgGlobeJPanel2 = new AgGlobeJPanel();
		this.m_AgGlobeJPanel2.getControl().setBackColor(stkxColor);
		this.m_AgGlobeJPanel2.getControl().setBackground(awtColor);

		this.m_PrimarySplitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, this.m_AgGlobeJPanel1, this.m_AgGlobeJPanel2);
		this.m_PrimarySplitPane.setResizeWeight(0.5);

		this.m_SampleJPanel = new SampleJPanel();
		this.m_SampleJPanel.addActionListener(this);
		this.getContentPane().add(this.m_SampleJPanel, BorderLayout.EAST);

		this.m_AgMapJPanel = new AgMapJPanel();
		this.m_AgMapJPanel.getControl().setBackColor(stkxColor);
		this.m_AgMapJPanel.getControl().setBackground(awtColor);

		this.m_SecondarySplitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT, this.m_PrimarySplitPane, this.m_AgMapJPanel);
		this.m_SecondarySplitPane.setResizeWeight(0.5);
		this.getContentPane().add(this.m_SecondarySplitPane, BorderLayout.CENTER);

		this.m_AgAnimationJToolBar = new AgAnimationJToolBar();
		this.m_AgAnimationJToolBar.addAnimationJToolBarListener(this);
		this.getContentPane().add(this.m_AgAnimationJToolBar, BorderLayout.NORTH);

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

	public void actionPerformed(ActionEvent ae)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			Object source = ae.getSource();
			if(source.equals(this.m_SampleJPanel.m_Step1JButton))
			{
				this.m_SampleCode.createScenario();

				this.m_SampleJPanel.m_Step1JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step2JButton.setEnabled(true);
				this.m_SampleJPanel.m_Step17JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step2JButton))
			{
				this.m_SampleCode.createPlanets();

				this.m_SampleJPanel.m_Step2JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step3JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step3JButton))
			{
				this.m_SampleCode.setSpacecraftGraphics();

				this.m_SampleJPanel.m_Step3JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step4JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step4JButton))
			{
				this.m_SampleCode.set2DGraphics();

				this.m_SampleJPanel.m_Step4JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step5JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step5JButton))
			{
				this.m_SampleCode.set3DGraphicsWindowEarthCentered();

				this.m_SampleJPanel.m_Step5JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step6JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step6JButton))
			{
				this.m_SampleCode.set3DGraphicsWindowMoonCentered();

				this.m_SampleJPanel.m_Step6JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step7JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step7JButton))
			{
				this.m_SampleCode.setupMCS();

				this.m_SampleJPanel.m_Step7JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step8JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step8JButton))
			{
				this.m_SampleCode.transLunnarInjectionFirstGuess();

				this.m_SampleJPanel.m_Step8JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step9JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step9JButton))
			{
				this.m_SampleCode.setupTheTargeterToCalculateLaunchAndCoastTimes();

				this.m_SampleJPanel.m_Step9JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step10JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step10JButton))
			{
				this.m_SampleCode.runTheTargeter();

				this.m_SampleJPanel.m_Step10JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step11JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step11JButton))
			{
				this.m_SampleCode.settingUpTheTargeterToTargetOnTheBPlane();

				this.m_SampleJPanel.m_Step11JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step12JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step12JButton))
			{
				this.m_SampleCode.drawingTheBPlane();

				this.m_SampleJPanel.m_Step12JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step13JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step13JButton))
			{
				this.m_SampleCode.runningTheTargeterToAchieveBPlaneParams();

				this.m_SampleJPanel.m_Step13JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step14JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step14JButton))
			{
				this.m_SampleCode.targetingAltitudeAndInclination();

				this.m_SampleJPanel.m_Step14JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step15JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step15JButton))
			{
				this.m_SampleCode.approachingTheMoon();

				this.m_SampleJPanel.m_Step15JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step16JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step16JButton))
			{
				this.m_SampleCode.lunarOrbitInsertion();

				this.m_SampleJPanel.m_Step16JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step17JButton.setEnabled(true);
			}
			else if(source.equals(this.m_SampleJPanel.m_Step17JButton))
			{
				this.m_SampleCode.closeScenario();

				this.m_SampleJPanel.m_Step17JButton.setEnabled(false);
				this.m_SampleJPanel.m_Step1JButton.setEnabled(true);
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
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

	class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				// Must dispose your control(s) before uninitializing the API
				MainWindow.this.m_AgGlobeJPanel1.getControl().dispose();
				MainWindow.this.m_AgGlobeJPanel2.getControl().dispose();
				MainWindow.this.m_AgMapJPanel.getControl().dispose();

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