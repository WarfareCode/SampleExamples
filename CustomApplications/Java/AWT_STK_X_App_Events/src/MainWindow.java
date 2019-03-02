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
import agi.swing.msgviewer.*;
import agi.swing.plaf.metal.*;
import agi.swing.toolbars.msgviewer.*;
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
{
	private static final long		serialVersionUID	= 1L;
	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_App_Events";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgGlobeJPanel			m_AgGlobeJPanel;

	private JSplitPane				m_MainSplitPane;

	private AgMsgViewerJPanel		m_AgMsgViewerJPanelOldEvents;
	private AgMsgViewerJPanel		m_AgMsgViewerJPanelNewEvents;
	private JSplitPane				m_EventsJPanelSplitPane;

	private int						m_AppOldEventsIndex;
	private AppOldEventsAdapter		m_AppOldEventsAdapter;
	private AppNewEventsAdapter		m_AppNewEventsAdapter;


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

		this.m_AppOldEventsAdapter = new AppOldEventsAdapter();
		this.m_AppOldEventsIndex = this.m_AgSTKXApplicationClass.advise(this.m_AppOldEventsAdapter);

		this.m_AppNewEventsAdapter = new AppNewEventsAdapter();
		this.m_AgSTKXApplicationClass.addIAgSTKXApplicationEvents2(this.m_AppNewEventsAdapter);

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

		this.m_AgGlobeJPanel = new AgGlobeJPanel();
		this.m_AgGlobeJPanel.getControl().setBackColor(stkxColor);
		this.m_AgGlobeJPanel.getControl().setBackground(awtColor);

		JPanel oldp = new JPanel();
		oldp.setBorder(new TitledBorder("Old Events Notification"));
		oldp.setLayout(new BorderLayout());
		this.m_AgMsgViewerJPanelOldEvents = new AgMsgViewerJPanel();
		this.m_AgMsgViewerJPanelOldEvents.getMsgViewerJToolBar().addMsgViewerJToolBarListener(new AgMsgViewerJToolBarEventsListener(this.m_AgMsgViewerJPanelOldEvents));
		oldp.add(this.m_AgMsgViewerJPanelOldEvents, BorderLayout.CENTER);

		JPanel newp = new JPanel();
		newp.setBorder(new TitledBorder("New Events Notification"));
		newp.setLayout(new BorderLayout());
		this.m_AgMsgViewerJPanelNewEvents = new AgMsgViewerJPanel();
		this.m_AgMsgViewerJPanelNewEvents.getMsgViewerJToolBar().addMsgViewerJToolBarListener(new AgMsgViewerJToolBarEventsListener(this.m_AgMsgViewerJPanelNewEvents));
		newp.add(this.m_AgMsgViewerJPanelNewEvents, BorderLayout.CENTER);

		this.m_EventsJPanelSplitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, oldp, newp);
		this.m_EventsJPanelSplitPane.setResizeWeight(0.5);

		this.m_MainSplitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT, this.m_AgGlobeJPanel, this.m_EventsJPanelSplitPane);
		this.m_MainSplitPane.setResizeWeight(0.75);

		this.getContentPane().add(this.m_MainSplitPane, BorderLayout.CENTER);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	class AgMsgViewerJToolBarEventsListener
	implements IAgMsgViewerJToolBarEventsListener
	{
		private AgMsgViewerJPanel	m_AgMsgViewerJPanel;

		public AgMsgViewerJToolBarEventsListener(AgMsgViewerJPanel panel)
		{
			this.m_AgMsgViewerJPanel = panel;
		}

		public void onMsgViewerJToolBarAction(AgMsgViewerJToolBarEvent e)
		{
			try
			{
				((Component)MainWindow.this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

				int action = e.getMsgViewerJToolBarAction();
				if(action == AgMsgViewerJToolBarEvent.ACTION_MSGVIEWER_SAVEAS)
				{
					this.m_AgMsgViewerJPanel.saveMessages();
				}
				else if(action == AgMsgViewerJToolBarEvent.ACTION_MSGVIEWER_CLEAR)
				{
					this.m_AgMsgViewerJPanel.clearMessages();
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
			finally
			{
				((Component)MainWindow.this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
			}
		}
	}

	private class AppOldEventsAdapter
	implements IAgSTKXApplicationEvents
	{
		public void onAnimUpdate(double arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onAnimUpdate " + arg0);
		}

		public void onBeforeLoadScenario(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onBeforeLoadScenario " + arg0);
		}

		public void onBeforeNewScenario(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onBeforeNewScenario " + arg0);
		}

		public void onBeginScenarioClose()
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onBeginScenarioClose ");
		}

		public void onLogMessage(String arg0, int arg1, int arg2, String arg3, int arg4, int arg5)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onLogMessage " + arg0 + " " + arg1 + " " + arg2 + " " + arg3 + " " + arg4 + " " + arg5);
		}

		public void onNewGfxAnalysisCtrlRequest(int arg0, int arg1)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onNewGfxAnalysisCtrlRequest " + arg0 + " " + arg1);
		}

		public void onNewGlobeCtrlRequest(int arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onNewGlobeCtrlRequest " + arg0);
		}

		public void onNewMapCtrlRequest(int arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onNewMapCtrlRequest " + arg0);
		}

		public void onScenarioClose()
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onScenarioClose ");
		}

		public void onScenarioLoad(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onScenarioLoad " + arg0);
		}

		public void onScenarioNew(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onScenarioNew " + arg0);
		}

		public void onScenarioSave(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("onScenarioSave " + arg0);
		}
	}

	private class AppNewEventsAdapter
	implements IAgSTKXApplicationEvents2
	{
		public void onAgSTKXApplicationEvent(AgSTKXApplicationEvent evt)
		{
			try
			{
				int type = evt.getType();
				Object[] params = evt.getParams();

				StringBuffer sb = new StringBuffer();
				if(params != null)
				{
					for(int i = 0; i < params.length; i++)
					{
						sb.append(params[i].toString());
						sb.append(" ");
					}
				}

				if(type == AgSTKXApplicationEvent.TYPE_ON_ANIM_UPDATE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimUpdate " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_BEFORE_LOAD_SCENARIO)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onBeforeLoadScenario " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_BEFORE_NEW_SCENARIO)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onBeforeNewScenario " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_BEGIN_SCENARIO_CLOSE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onBeginScenarioClose " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_LOG_MESSAGE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onLogMessage " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_NEW_GFX_ANALYSIS_CTRL_REQUEST)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onNewGfxAnalysisCtrlRequest " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_NEW_GLOBE_CTRL_REQUEST)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onNewGlobeCtrlRequest " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_NEW_MAP_CTRL_REQUEST)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onNewMapCtrlRequest " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_CLOSE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioClose " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_LOAD)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioLoad " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_NEW)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioNew " + sb.toString());
				}
				else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_SAVE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioSave " + sb.toString());
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				MainWindow.this.m_AgSTKXApplicationClass.removeIAgSTKXApplicationEvents2(MainWindow.this.m_AppNewEventsAdapter);

				MainWindow.this.m_AgSTKXApplicationClass.unadvise(MainWindow.this.m_AppOldEventsIndex);

				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeJPanel.getControl().dispose();

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