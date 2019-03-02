/* ==============================================================
 * This sample was last tested with the following configuration:
 * ==============================================================
 * Eclipse SDK Version: 4.2.0 Build id: I20120608-1400
 * JRE 1.7.0_05 and greater
 * JavaFX 2.1.1 SDK or greater
 * STK 10.0
 * ==============================================================
 */

// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.border.*;
import javax.swing.plaf.metal.*;

// JavaFX API
import javafx.animation.*;
import javafx.application.*;
import javafx.event.*;
import javafx.util.*;
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
//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements EventHandler<javafx.event.ActionEvent>
{
	private static final long			serialVersionUID	= 1L;

	private final static String			s_TITLE				= "CustomApp_Swing_JavaFX_STK_X_Animation";
	private final static String			s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass		m_AgSTKXApplicationClass;
	private AgStkObjectRootClass		m_AgStkObjectRootClass;

	private JSplitPane					m_MainSplitPane;
	private AgGlobeJPanel				m_AgGlobeJPanel;
	private AgMapJPanel					m_AgMapJPanel;

	private AnimationInfoJFXPanel		m_AnimationInfoJFXPanel;

	private AnimationJFXPanel			m_AnimationJFXPanel;
	private AnimationEndModeJFXPanel	m_AnimationEndModeJFXPanel;
	private AnimationModeJFXPanel		m_AnimationModeJFXPanel;

	private AppEventsAdapter			m_AppEventsAdapter;

	private boolean						m_ScenarioLoaded	= false;

	public MainWindow()
	throws Throwable
	{
		super(MainWindow.class.getResource(s_DESCFILENAME));

		int width = 1000;
		int height = 618;

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
		this.m_AppEventsAdapter = new AppEventsAdapter();
		this.m_AgSTKXApplicationClass.addIAgSTKXApplicationEvents2(this.m_AppEventsAdapter);
		super.setApp(this.m_AgSTKXApplicationClass);

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}

		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();
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

		this.m_AnimationInfoJFXPanel = new AnimationInfoJFXPanel(mt.getControl(), mt.getControlTextColor());
		this.getContentPane().add(this.m_AnimationInfoJFXPanel, BorderLayout.SOUTH);
		this.m_AnimationInfoJFXPanel.initScene(width, 20);

		JPanel toolbars = new JPanel();
		toolbars.setLayout(new FlowLayout(FlowLayout.CENTER));
		this.getContentPane().add(toolbars, BorderLayout.NORTH);

		// Animation
		this.m_AnimationJFXPanel = new AnimationJFXPanel(mt.getControl());
		toolbars.add(this.m_AnimationJFXPanel);
		this.m_AnimationJFXPanel.initScene(375, 35, this);

		// EndMode
		this.m_AnimationEndModeJFXPanel = new AnimationEndModeJFXPanel(mt.getControl());
		toolbars.add(this.m_AnimationEndModeJFXPanel);
		this.m_AnimationEndModeJFXPanel.initScene(200, 35, this);

		// Mode
		this.m_AnimationModeJFXPanel = new AnimationModeJFXPanel(mt.getControl());
		toolbars.add(this.m_AnimationModeJFXPanel);
		this.m_AnimationModeJFXPanel.initScene(200, 35, this);

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(width, height);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();
	}

	private class AppEventsAdapter
	implements IAgSTKXApplicationEvents2
	{
		public void onAgSTKXApplicationEvent(AgSTKXApplicationEvent evt)
		{
			try
			{
				int type = evt.getType();
				Object[] params = evt.getParams();

				if(type == AgSTKXApplicationEvent.TYPE_ON_ANIM_UPDATE)
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

					MainWindow.this.m_AnimationInfoJFXPanel.setEpoch(sb.toString());

					String[] dateTime = getDateTime();

					MainWindow.this.m_AnimationInfoJFXPanel.setDate(dateTime[0] + " " + dateTime[1] + " " + dateTime[2]);
					MainWindow.this.m_AnimationInfoJFXPanel.setTime(dateTime[3]);

					MainWindow.this.m_AnimationInfoJFXPanel.setFrameRate(getFrameRate());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_LOAD)
				{
					MainWindow.this.m_ScenarioLoaded = true;
					showJFXPanels();
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_NEW)
				{
					MainWindow.this.m_ScenarioLoaded = true;
					showJFXPanels();
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_CLOSE)
				{
					MainWindow.this.m_ScenarioLoaded = false;
					hideJFXPanels();
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	public void showJFXPanels()
	{
		MainWindow.this.m_AnimationJFXPanel.showScene();
		MainWindow.this.m_AnimationModeJFXPanel.showScene();
		MainWindow.this.m_AnimationEndModeJFXPanel.showScene();
		MainWindow.this.m_AnimationInfoJFXPanel.showScene();
	}

	public void hideJFXPanels()
	{
		MainWindow.this.m_AnimationJFXPanel.hideScene();
		MainWindow.this.m_AnimationModeJFXPanel.hideScene();
		MainWindow.this.m_AnimationEndModeJFXPanel.hideScene();
		MainWindow.this.m_AnimationInfoJFXPanel.hideScene();
	}

	private String[] getDateTime()
	throws AgCoreException
	{
		String[] timeArray = new String[] {"N/A", "N/A", "N/A", "N/A"};
		String time = "N/A";
		agi.stkx.IAgExecCmdResult result = null;
		result = this.m_AgSTKXApplicationClass.executeCommand("GetAnimTime *");
		if(result.getCount() > 0)
		{
			time = result.getItem(0);
		}
		int length = time.length();
		time = time.substring(1, length - 2);
		time = time.trim();

		timeArray = time.split(" ");
		return timeArray;
	}

	private String getFrameRate()
	throws AgCoreException
	{
		String frameRate = "N/A";
		agi.stkx.IAgExecCmdResult result = null;
		result = this.m_AgSTKXApplicationClass.executeCommand("AnimFrameRate *");
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

		private JLabel				m_DateJLabel;
		private JTextField			m_DateJTextField;

		private JLabel				m_TimeJLabel;
		private JTextField			m_TimeJTextField;

		private JLabel				m_EpochJLabel;
		private JTextField			m_EpochJTextField;

		private JLabel				m_FrameRateJLabel;
		private JTextField			m_FrameRateJTextField;

		public AnimationInfoJPanel()
		{
			this.setLayout(new GridLayout(1, 8));

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

	@Override
	public void handle(javafx.event.ActionEvent event)
	{
		try
		{
			Object src = event.getSource();
			if(this.m_ScenarioLoaded)
			{
				if(src == this.m_AnimationJFXPanel.m_AnimPlayForwardButton)
				{
					processConnectCommandButton(this.m_AnimationJFXPanel.m_AnimPlayForwardButton, "Animate * Start");
				}
				else if(src == this.m_AnimationJFXPanel.m_AnimPlayBackwardButton)
				{
					processConnectCommandButton(this.m_AnimationJFXPanel.m_AnimPlayBackwardButton, "Animate * Start Reverse");
				}
				else if(src == this.m_AnimationJFXPanel.m_AnimPauseButton)
				{
					processConnectCommandButton(this.m_AnimationJFXPanel.m_AnimPauseButton, "Animate * Pause");
				}
				else if(src == this.m_AnimationJFXPanel.m_AnimStepForwardButton)
				{
					processConnectCommandButton(this.m_AnimationJFXPanel.m_AnimStepForwardButton, "Animate * Step Forward");
				}
				else if(src == this.m_AnimationJFXPanel.m_AnimStepBackwardButton)
				{
					processConnectCommandButton(this.m_AnimationJFXPanel.m_AnimStepBackwardButton, "Animate * Step Reverse");
				}
				else if(src == this.m_AnimationJFXPanel.m_AnimFasterButton)
				{
					processConnectCommandButton(this.m_AnimationJFXPanel.m_AnimFasterButton, "Animate * Faster");
				}
				else if(src == this.m_AnimationJFXPanel.m_AnimSlowerButton)
				{
					processConnectCommandButton(this.m_AnimationJFXPanel.m_AnimSlowerButton, "Animate * Slower");
				}
				else if(src == this.m_AnimationJFXPanel.m_AnimRewindButton)
				{
					processConnectCommandButton(this.m_AnimationJFXPanel.m_AnimRewindButton, "Animate * Reset");
				}
				else if(src == this.m_AnimationModeJFXPanel.m_AnimModeNormalButton)
				{
					processConnectCommandButton(this.m_AnimationModeJFXPanel.m_AnimModeNormalButton, "SetAnimation * AnimationMode Normal");
				}
				else if(src == this.m_AnimationModeJFXPanel.m_AnimModeRealtimeButton)
				{
					processConnectCommandButton(this.m_AnimationModeJFXPanel.m_AnimModeRealtimeButton, "SetAnimation * AnimationMode RealTime");
				}
				else if(src == this.m_AnimationModeJFXPanel.m_AnimModeXRealtimeButton)
				{
					processConnectCommandButton(this.m_AnimationModeJFXPanel.m_AnimModeXRealtimeButton, "SetAnimation * AnimationMode XRealTime");
				}
				else if(src == this.m_AnimationEndModeJFXPanel.m_AnimEndModeContinuousButton)
				{
					processConnectCommandButton(this.m_AnimationEndModeJFXPanel.m_AnimEndModeContinuousButton, "SetAnimation * EndMode Continuous");
				}
				else if(src == this.m_AnimationEndModeJFXPanel.m_AnimEndModeLoopButton)
				{
					processConnectCommandButton(this.m_AnimationEndModeJFXPanel.m_AnimEndModeLoopButton, "SetAnimation * EndMode Loop");
				}
				else if(src == this.m_AnimationEndModeJFXPanel.m_AnimEndModeNoLoopButton)
				{
					processConnectCommandButton(this.m_AnimationEndModeJFXPanel.m_AnimEndModeNoLoopButton, "SetAnimation * EndMode End");
				}
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void processConnectCommandButton(final javafx.scene.control.Button button, final String cnctCmd)
	throws AgCoreException
	{
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				try
				{
					Timeline timeline = new Timeline();

					KeyValue kfstartv1 = new KeyValue(button.scaleXProperty(), 1);
					KeyValue kfstartv2 = new KeyValue(button.scaleYProperty(), 1);
					KeyFrame kfstart = new KeyFrame(Duration.ZERO, kfstartv1, kfstartv2);
					timeline.getKeyFrames().add(kfstart);

					KeyValue kfmiddle1v1 = new KeyValue(button.scaleXProperty(), 1.15);
					KeyValue kfmiddle1v2 = new KeyValue(button.scaleYProperty(), 1.15);
					KeyFrame kfmiddle1 = new KeyFrame(new Duration(250), kfmiddle1v1, kfmiddle1v2);
					timeline.getKeyFrames().add(kfmiddle1);

					KeyValue kfrotate1v1 = new KeyValue(button.rotateProperty(), 0);
					KeyFrame kfrotate1 = new KeyFrame(new Duration(350), kfrotate1v1);
					timeline.getKeyFrames().add(kfrotate1);

					KeyValue kfrotate2v1 = new KeyValue(button.rotateProperty(), 360);
					KeyFrame kfrotate2 = new KeyFrame(new Duration(650), kfrotate2v1);
					timeline.getKeyFrames().add(kfrotate2);

					KeyValue kfrotate2v1a = new KeyValue(button.rotateProperty(), 0);
					KeyFrame kfrotate2a = new KeyFrame(new Duration(651), kfrotate2v1a);
					timeline.getKeyFrames().add(kfrotate2a);

					KeyValue kfmiddle2v1 = new KeyValue(button.scaleXProperty(), 1.15);
					KeyValue kfmiddle2v2 = new KeyValue(button.scaleYProperty(), 1.15);
					KeyFrame kfmiddle2 = new KeyFrame(new Duration(750), kfmiddle2v1, kfmiddle2v2);
					timeline.getKeyFrames().add(kfmiddle2);

					KeyValue kfstopv1 = new KeyValue(button.scaleXProperty(), 1);
					KeyValue kfstopv2 = new KeyValue(button.scaleYProperty(), 1);
					KeyFrame kfstop = new KeyFrame(new Duration(1000), kfstopv1, kfstopv2);
					timeline.getKeyFrames().add(kfstop);

					timeline.play();
				}
				catch(Throwable t)
				{
					t.printStackTrace();
				}
			}
		});
		MainWindow.this.m_AgSTKXApplicationClass.executeCommand(cnctCmd);
	}
}