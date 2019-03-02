// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.logging.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.swing.*;
import agi.stkobjects.*;
import agi.stkengine.*;
import agi.stk.core.swing.menus.animation.*;
import agi.stk.core.swing.menus.animation.endmode.*;
import agi.stk.core.swing.menus.animation.mode.*;
import agi.stk.core.swing.toolbars.animation.*;
import agi.stk.core.swing.toolbars.animation.endmode.*;
import agi.stk.core.swing.toolbars.animation.mode.*;

//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements IAgAnimationJToolBarEventsListener, IAgAnimationJMenuEventsListener, IAgAnimationEndModeJToolBarEventsListener, IAgAnimationEndModeJMenuEventsListener,
IAgAnimationModeJToolBarEventsListener, IAgAnimationModeJMenuEventsListener
{
	private static final long			serialVersionUID	= 1L;

	private final static String			s_TITLE				= "CustomApp_AWT_STK_Objects_Animation";
	private final static String			s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass		m_AgSTKXApplicationClass;
	private AgStkObjectRootClass		m_AgStkObjectRootClass;

	private JSplitPane					m_MainSplitPane;
	private AgGlobeJPanel				m_AgGlobeJPanel;
	private AgMapJPanel					m_AgMapJPanel;

	private AnimationInfoJPanel			m_AnimationInfoJPanel;

	private AgAnimationJMenu			m_AgAnimationJMenu;
	private AgAnimationJToolBar			m_AgAnimationJToolBar;
	private AgAnimationEndModeJMenu		m_AgAnimationEndModeJMenu;
	private AgAnimationEndModeJToolBar	m_AgAnimationEndModeJToolBar;
	private AgAnimationModeJMenu		m_AgAnimationModeJMenu;
	private AgAnimationModeJToolBar		m_AgAnimationModeJToolBar;

	private RootEventsAdapter			m_RootEventsAdapter;

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
		this.m_RootEventsAdapter = new RootEventsAdapter();
		this.m_AgStkObjectRootClass.addIAgStkObjectRootEvents2(this.m_RootEventsAdapter);
		super.setRoot(this.m_AgStkObjectRootClass);

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgGlobeJPanel = new AgGlobeJPanel();
		this.m_AgGlobeJPanel.setBorder(new BevelBorder(BevelBorder.LOWERED));
		this.m_AgGlobeJPanel.getControl().setBackColor(stkxColor);
		this.m_AgGlobeJPanel.getControl().setBackground(awtColor);

		this.m_AgMapJPanel = new AgMapJPanel();
		this.m_AgMapJPanel.setBorder(new BevelBorder(BevelBorder.LOWERED));
		this.m_AgMapJPanel.getControl().setBackColor(stkxColor);
		this.m_AgMapJPanel.getControl().setBackground(awtColor);

		this.m_MainSplitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, this.m_AgGlobeJPanel, this.m_AgMapJPanel);
		this.m_MainSplitPane.setResizeWeight(0.5);
		this.getContentPane().add(this.m_MainSplitPane, BorderLayout.CENTER);

		this.m_AnimationInfoJPanel = new AnimationInfoJPanel();
		this.getContentPane().add(this.m_AnimationInfoJPanel, BorderLayout.SOUTH);

		JPanel toolbars = new JPanel();
		toolbars.setLayout(new FlowLayout(FlowLayout.LEFT));
		this.getContentPane().add(toolbars, BorderLayout.NORTH);

		// Animation
		this.m_AgAnimationJToolBar = new AgAnimationJToolBar();
		this.m_AgAnimationJToolBar.addAnimationJToolBarListener(this);
		toolbars.add(this.m_AgAnimationJToolBar);

		this.m_AgAnimationJMenu = new AgAnimationJMenu();
		this.m_AgAnimationJMenu.addAnimationJMenuListener(this);
		super.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu().add(this.m_AgAnimationJMenu);

		// EndMode
		this.m_AgAnimationEndModeJToolBar = new AgAnimationEndModeJToolBar();
		this.m_AgAnimationEndModeJToolBar.addAnimationEndModeJToolBarListener(this);
		toolbars.add(this.m_AgAnimationEndModeJToolBar);

		this.m_AgAnimationEndModeJMenu = new AgAnimationEndModeJMenu();
		this.m_AgAnimationEndModeJMenu.addAnimationEndModeJMenuListener(this);
		super.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu().add(this.m_AgAnimationEndModeJMenu);

		// Mode
		this.m_AgAnimationModeJToolBar = new AgAnimationModeJToolBar();
		this.m_AgAnimationModeJToolBar.addAnimationModeJToolBarListener(this);
		toolbars.add(this.m_AgAnimationModeJToolBar);

		this.m_AgAnimationModeJMenu = new AgAnimationModeJMenu();
		this.m_AgAnimationModeJMenu.addAnimationModeJMenuListener(this);
		super.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu().add(this.m_AgAnimationModeJMenu);

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
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

	public void onAnimationEndModeJToolBarAction(AgAnimationEndModeJToolBarEvent e)
	{
		try
		{
			int action = e.getAnimationEndModeJToolBarAction();
			if(action == AgAnimationEndModeJToolBarEvent.ACTION_ANIMATION_ENDMODE_LOOP)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * EndMode Loop");
			}
			else if(action == AgAnimationEndModeJToolBarEvent.ACTION_ANIMATION_ENDMODE_NOLOOP)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * EndMode End");
			}
			else if(action == AgAnimationEndModeJToolBarEvent.ACTION_ANIMATION_ENDMODE_CONTINUOUS)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * EndMode Continuous");
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public void onAnimationModeJToolBarAction(AgAnimationModeJToolBarEvent e)
	{
		try
		{
			int action = e.getAnimationModeJToolBarAction();
			if(action == AgAnimationModeJToolBarEvent.ACTION_ANIMATION_MODE_NORMAL)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * AnimationMode Normal");
			}
			else if(action == AgAnimationModeJToolBarEvent.ACTION_ANIMATION_MODE_REALTIME)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * AnimationMode RealTime");
			}
			else if(action == AgAnimationModeJToolBarEvent.ACTION_ANIMATION_MODE_XREALTIME)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * AnimationMode XRealTime");
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public void onAnimationJMenuAction(AgAnimationJMenuEvent e)
	{
		try
		{
			int action = e.getAnimationJMenuAction();
			if(action == AgAnimationJMenuEvent.ACTION_ANIMATION_REWIND)
			{
				this.m_AgStkObjectRootClass.rewind();
			}
			else if(action == AgAnimationJMenuEvent.ACTION_ANIMATION_PLAYFORWARD)
			{
				this.m_AgStkObjectRootClass.playForward();
			}
			else if(action == AgAnimationJMenuEvent.ACTION_ANIMATION_PLAYBACKWARD)
			{
				this.m_AgStkObjectRootClass.playBackward();
			}
			else if(action == AgAnimationJMenuEvent.ACTION_ANIMATION_PAUSE)
			{
				this.m_AgStkObjectRootClass.pause();
			}
			else if(action == AgAnimationJMenuEvent.ACTION_ANIMATION_STEPFORWARD)
			{
				this.m_AgStkObjectRootClass.stepForward();
			}
			else if(action == AgAnimationJMenuEvent.ACTION_ANIMATION_STEPBACKWARD)
			{
				this.m_AgStkObjectRootClass.stepBackward();
			}
			else if(action == AgAnimationJMenuEvent.ACTION_ANIMATION_FASTER)
			{
				this.m_AgStkObjectRootClass.faster();
			}
			else if(action == AgAnimationJMenuEvent.ACTION_ANIMATION_SLOWER)
			{
				this.m_AgStkObjectRootClass.slower();
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public void onAnimationEndModeJMenuAction(AgAnimationEndModeJMenuEvent e)
	{
		try
		{
			int action = e.getAnimationEndModeJMenuAction();
			if(action == AgAnimationEndModeJMenuEvent.ACTION_ANIMATION_ENDMODE_LOOP)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * EndMode Loop");
			}
			else if(action == AgAnimationEndModeJMenuEvent.ACTION_ANIMATION_ENDMODE_NOLOOP)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * EndMode End");
			}
			else if(action == AgAnimationEndModeJMenuEvent.ACTION_ANIMATION_ENDMODE_CONTINUOUS)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * EndMode Continuous");
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public void onAnimationModeJMenuAction(AgAnimationModeJMenuEvent e)
	{
		try
		{
			int action = e.getAnimationModeJMenuAction();
			if(action == AgAnimationModeJMenuEvent.ACTION_ANIMATION_MODE_NORMAL)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * AnimationMode Normal");
			}
			else if(action == AgAnimationModeJMenuEvent.ACTION_ANIMATION_MODE_REALTIME)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * AnimationMode RealTime");
			}
			else if(action == AgAnimationModeJMenuEvent.ACTION_ANIMATION_MODE_XREALTIME)
			{
				this.m_AgStkObjectRootClass.executeCommand("SetAnimation * AnimationMode XRealTime");
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private class RootEventsAdapter
	implements IAgStkObjectRootEvents2
	{
		public void onAgStkObjectRootEvent(AgStkObjectRootEvent evt)
		{
			try
			{
				int type = evt.getType();
				Object[] params = evt.getParams();


				if(type == AgStkObjectRootEvent.TYPE_ON_ANIM_UPDATE)
				{
					StringBuffer sb = new StringBuffer();
					if(params != null)
					{
						for(int i = 0; i < params.length; i++)
						{
							sb.append(params[i].toString());
							sb.append(" ");
						}
					}

					MainWindow.this.m_AnimationInfoJPanel.setEpoch(sb.toString());
					
					String[] dateTime = getDateTime();
					
					MainWindow.this.m_AnimationInfoJPanel.setDate(dateTime[0]+" "+dateTime[1]+" "+dateTime[2]);
					MainWindow.this.m_AnimationInfoJPanel.setTime(dateTime[3]);

					MainWindow.this.m_AnimationInfoJPanel.setFrameRate(getFrameRate());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_FASTER)
				{
					MainWindow.this.m_AnimationInfoJPanel.setState("Faster");
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_PAUSE)
				{
					MainWindow.this.m_AnimationInfoJPanel.setState("Pause");
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_PLAYBACK)
				{
					MainWindow.this.m_AnimationInfoJPanel.setState("PlayBack");
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_REWIND)
				{
					MainWindow.this.m_AnimationInfoJPanel.setState("Rewind ");
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_SLOWER)
				{
					MainWindow.this.m_AnimationInfoJPanel.setState("Slower");
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_STEP)
				{
					MainWindow.this.m_AnimationInfoJPanel.setState("Step");
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_STEP_BACK)
				{
					MainWindow.this.m_AnimationInfoJPanel.setState("StepBack");
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private String[] getDateTime()
	throws AgCoreException
	{
		String[] timeArray = new String[]{"N/A","N/A","N/A","N/A"} ;
		String time = "N/A";
		agi.stkutil.IAgExecCmdResult result = null;
		result = this.m_AgStkObjectRootClass.executeCommand("GetAnimTime *");
		if(result.getCount() > 0)
		{
			time = result.getItem(0);
		}
		int length = time.length();
		time = time.substring(1, length-2);
		time = time.trim();
		
		timeArray = time.split(" ");
		return timeArray;
	}

	private String getFrameRate()
	throws AgCoreException
	{
		String frameRate = "N/A";
		agi.stkutil.IAgExecCmdResult result = null;
		result = this.m_AgStkObjectRootClass.executeCommand("AnimFrameRate *");
		if(result.getCount() > 0)
		{
			frameRate = result.getItem(0);
		}
		return frameRate;
	}

	class AnimationInfoJPanel
	extends JPanel
	{
		private static final long	serialVersionUID	= 1L;

		private JLabel		m_DateJLabel;
		private JTextField	m_DateJTextField;

		private JLabel		m_TimeJLabel;
		private JTextField	m_TimeJTextField;

		private JLabel		m_EpochJLabel;
		private JTextField	m_EpochJTextField;

		private JLabel		m_FrameRateJLabel;
		private JTextField	m_FrameRateJTextField;

		private JLabel		m_StateJLabel;
		private JTextField	m_StateJTextField;

		public AnimationInfoJPanel()
		{
			this.setLayout(new GridLayout(1, 10));

			// ========
			// Date
			// ========
			this.m_DateJLabel = new JLabel();
			this.m_DateJLabel.setText("Date:");
			this.m_DateJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
			this.m_DateJLabel.setHorizontalAlignment(JLabel.RIGHT);
			this.add(this.m_DateJLabel);

			this.m_DateJTextField = new JTextField();
			this.m_DateJTextField.setBorder(new BevelBorder(BevelBorder.LOWERED));
			this.m_DateJTextField.setHorizontalAlignment(JLabel.RIGHT);
			this.m_DateJTextField.setEnabled(false);
			this.add(this.m_DateJTextField);

			// ========
			// Time
			// ========
			this.m_TimeJLabel = new JLabel();
			this.m_TimeJLabel.setText("Time:");
			this.m_TimeJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
			this.m_TimeJLabel.setHorizontalAlignment(JLabel.RIGHT);
			this.add(this.m_TimeJLabel);

			this.m_TimeJTextField = new JTextField();
			this.m_TimeJTextField.setBorder(new BevelBorder(BevelBorder.LOWERED));
			this.m_TimeJTextField.setHorizontalAlignment(JLabel.RIGHT);
			this.m_TimeJTextField.setEnabled(false);
			this.add(this.m_TimeJTextField);

			// ===========
			// Epoch
			// ===========
			this.m_EpochJLabel = new JLabel();
			this.m_EpochJLabel.setText("Epoch (sec):");
			this.m_EpochJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
			this.m_EpochJLabel.setHorizontalAlignment(JLabel.RIGHT);
			this.add(this.m_EpochJLabel);

			this.m_EpochJTextField = new JTextField();
			this.m_EpochJTextField.setBorder(new BevelBorder(BevelBorder.LOWERED));
			this.m_EpochJTextField.setHorizontalAlignment(JLabel.RIGHT);
			this.m_EpochJTextField.setEnabled(false);
			this.add(this.m_EpochJTextField);

			// ========
			// FPS
			// ========
			this.m_FrameRateJLabel = new JLabel();
			this.m_FrameRateJLabel.setText("FPS:");
			this.m_FrameRateJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
			this.m_FrameRateJLabel.setHorizontalAlignment(JLabel.RIGHT);
			this.add(this.m_FrameRateJLabel);

			this.m_FrameRateJTextField = new JTextField();
			this.m_FrameRateJTextField.setBorder(new BevelBorder(BevelBorder.LOWERED));
			this.m_FrameRateJTextField.setHorizontalAlignment(JLabel.RIGHT);
			this.m_FrameRateJTextField.setEnabled(false);
			this.add(this.m_FrameRateJTextField);

			// ========
			// State
			// ========
			this.m_StateJLabel = new JLabel();
			this.m_StateJLabel.setText("State:");
			this.m_StateJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
			this.m_StateJLabel.setHorizontalAlignment(JLabel.RIGHT);
			this.add(this.m_StateJLabel);

			this.m_StateJTextField = new JTextField();
			this.m_StateJTextField.setBorder(new BevelBorder(BevelBorder.LOWERED));
			this.m_StateJTextField.setHorizontalAlignment(JLabel.RIGHT);
			this.add(this.m_StateJTextField);
		}

		public void setDate(String date)
		{
			this.m_DateJTextField.setText(date);
		}

		public void setTime(String time)
		{
			this.m_TimeJTextField.setText(time);
		}

		public void setEpoch(String epoch)
		{
			this.m_EpochJTextField.setText(epoch);
		}

		public void setFrameRate(String fps)
		{
			this.m_FrameRateJTextField.setText(fps);
		}

		public void setState(String state)
		{
			this.m_StateJTextField.setText(state);
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
				MainWindow.this.m_AgGlobeJPanel.getControl().dispose();
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