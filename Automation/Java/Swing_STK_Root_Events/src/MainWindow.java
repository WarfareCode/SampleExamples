import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Component;
import java.awt.Cursor;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.logging.ConsoleHandler;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JPanel;
import javax.swing.JSplitPane;
import javax.swing.SwingConstants;
import javax.swing.border.BevelBorder;
import javax.swing.border.CompoundBorder;
import javax.swing.border.EmptyBorder;
import javax.swing.border.TitledBorder;

import agi.core.AgCoreException;
import agi.core.awt.AgAwt_JNI;
import agi.core.logging.AgFormatter;
import agi.stk.automation.application.swing.AutomationApplicationSTKSampleBaseJFrame;
import agi.stk.automation.application.swing.EventsFilterJPanel;
import agi.stk.ui.AgStkAutomation_JNI;
import agi.stk.ui.AgStkUi;
import agi.stkobjects.AgENotificationFilterMask;
import agi.stkobjects.AgStkObjectRootClass;
import agi.stkobjects.AgStkObjectRootEvent;
import agi.stkobjects.IAgStkObjectRoot;
import agi.stkobjects.IAgStkObjectRootEvents2;
import agi.swing.AgAGIImageIcon;
import agi.swing.msgviewer.AgMsgViewerJPanel;
import agi.swing.toolbars.msgviewer.AgMsgViewerJToolBarEvent;
import agi.swing.toolbars.msgviewer.IAgMsgViewerJToolBarEventsListener;


public class MainWindow
//NOTE:  This sample derives/extends from AutomationApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends AutomationApplicationSTKSampleBaseJFrame
implements ActionListener
{
	private static final long	serialVersionUID	= 1L;

	private final static String	s_TITLE				= "Automation_Swing_STK_Root_Events";
	private final static String	s_DESCFILENAME		= "AppDescription.html";

	private JSplitPane				m_EventsJPanelSplitPane;
	private StkAppJPanel			m_StkAppJPanel;
	private AgMsgViewerJPanel		m_AgMsgViewerJPanelNewEvents;
	private RootNewEventsAdapter	m_RootNewEventsAdapter;

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
		AgStkAutomation_JNI.initialize(true);
		AgAwt_JNI.initialize_AwtComponents();

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

		this.m_StkAppJPanel = new StkAppJPanel();
		this.m_StkAppJPanel.addActionListener(this);

		JPanel newp = new JPanel();
		newp.setBorder(new TitledBorder("Event Notifications"));
		newp.setLayout(new BorderLayout());
		this.m_AgMsgViewerJPanelNewEvents = new AgMsgViewerJPanel();
		this.m_AgMsgViewerJPanelNewEvents.getMsgViewerJToolBar().addMsgViewerJToolBarListener(new AgMsgViewerJToolBarEventsListener(this.m_AgMsgViewerJPanelNewEvents));
		newp.add(this.m_AgMsgViewerJPanelNewEvents, BorderLayout.CENTER);

		this.m_EventsJPanelSplitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT, this.m_StkAppJPanel, newp);
		this.m_EventsJPanelSplitPane.setResizeWeight(0.5);

		this.getContentPane().add(this.m_EventsJPanelSplitPane, BorderLayout.CENTER);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getAutomationAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getAutomationAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getAutomationAppSTKSampleBaseJMenuBar().invalidate();
		this.getAutomationAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new AgWindowAdapter());

		this.setSize(700, 600);
		this.setResizable(false);
	}

	public void actionPerformed(ActionEvent ae)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			String cmd = ae.getActionCommand();
			Object src = ae.getSource();
			
			if(cmd.equalsIgnoreCase(StkConnectionJPanel.s_START_STK_TEXT))
			{
				startSTKInstance();
			}
			else if(cmd.equalsIgnoreCase(StkConnectionJPanel.s_CONNECT_STK_TEXT))
			{
				connectToSTKInstance();
			}
			else if(cmd.equalsIgnoreCase(StkConnectionJPanel.s_RELEASE_STK_TEXT))
			{
				releaseSTKInstance();
			}
			else if(this.m_StkAppJPanel.getEventsFilter().isUpdateJButton(src))
			{
				AgENotificationFilterMask filter = this.m_StkAppJPanel.getEventsFilter().getFilter();
				IAgStkObjectRoot root = this.getStkRoot();
				if(root != null)
				{
					root.setNotificationFilter(filter);
				}
				
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

	private void releaseSTKInstance()
	{
		if(this.getStkUi() != null)
		{
			this.closeConnectionToSTK();

			this.m_StkAppJPanel.getConnection().setConnectToStk(false);
		}
	}

	private void connectToSTKInstance()
	{
		try
		{
			AgStkUi stkui = AgStkUi.getStkUiInstance();
			if(stkui != null)
			{
				this.setStkUi(stkui);

				this.getStkUi().setUserControl(true);
				
				//TODO: Verify
				AgENotificationFilterMask filter = AgENotificationFilterMask.getFromValue(this.getStkRoot().getNotificationFilter());
				this.m_StkAppJPanel.getEventsFilter().setFilter(filter);

				this.m_RootNewEventsAdapter = new RootNewEventsAdapter();
				this.getStkRoot().addIAgStkObjectRootEvents2(this.m_RootNewEventsAdapter);

				this.m_StkAppJPanel.getConnection().setConnectToStk(true);
			}
		}
		catch(AgCoreException ex)
		{
			ex.printHexHresult();
			ex.printStackTrace();
			this.m_StkAppJPanel.getEventsFilter().setEnabled(false);
			this.m_StkAppJPanel.getConnection().setConnectToStk(false);
			closeConnectionToSTK();
		}
	}

	private void startSTKInstance()
	{
		try
		{
			AgStkUi stkui = new AgStkUi();

			if(stkui != null)
			{
				this.setStkUi(stkui);

				this.getStkUi().setVisible(true);
				this.getStkUi().setUserControl(false);
				
				//TODO: Verify
				AgENotificationFilterMask filter = AgENotificationFilterMask.getFromValue(this.getStkRoot().getNotificationFilter());
				this.m_StkAppJPanel.getEventsFilter().setFilter(filter);

				this.m_RootNewEventsAdapter = new RootNewEventsAdapter();
				this.getStkRoot().addIAgStkObjectRootEvents2(this.m_RootNewEventsAdapter);

				this.m_StkAppJPanel.getConnection().setConnectToStk(true);
			}
		}
		catch(AgCoreException ex)
		{
			ex.printHexHresult();
			ex.printStackTrace();
			this.m_StkAppJPanel.getEventsFilter().setEnabled(false);
			this.m_StkAppJPanel.getConnection().setConnectToStk(false);
			closeConnectionToSTK();
		}
	}

	public void closeConnectionToSTK()
	{
		try
		{
			AgStkObjectRootClass root = this.getStkRoot();
			if(root != null)
			{
				root.removeIAgStkObjectRootEvents2(this.m_RootNewEventsAdapter);
				this.m_RootNewEventsAdapter = null;
			}
			
			this.releaseStkUi();
		}
		catch(AgCoreException e)
		{
			e.printHexHresult();
			e.printStackTrace();
		}
	}

	class AgWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				closeConnectionToSTK();

				AgAwt_JNI.uninitialize_AwtComponents();
				AgStkAutomation_JNI.uninitialize();
				AgAwt_JNI.uninitialize_AwtDelegate();

				System.runFinalization(); // Tell the JVM it should finalize classes.
				System.gc(); // Tell the JVM it should garbage collect.
				System.exit(0); // Tell the JVM to exit successfully.
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	class StkAppJPanel
	extends JPanel
	{
		private static final long	serialVersionUID	= 1L;

		public final static String	s_STK_APP			= "STK Application";

		private StkConnectionJPanel	m_StkConnectionJPanel;
		private EventsFilterJPanel	m_EventsFilterJPanel;

		public StkAppJPanel()
		throws Exception
		{
			this.setLayout(new BorderLayout());
			this.setBorder(new EmptyBorder(5,5,5,5));

			this.m_StkConnectionJPanel = new StkConnectionJPanel();
			this.add(this.m_StkConnectionJPanel, BorderLayout.WEST);

			TitledBorder b1 = new TitledBorder(new BevelBorder(BevelBorder.LOWERED), "Event Filtering");
			CompoundBorder cb1 = new CompoundBorder(new EmptyBorder(5,5,5,5), b1);

			this.m_EventsFilterJPanel = new EventsFilterJPanel();
			this.m_EventsFilterJPanel.setBorder(cb1);
			this.add(this.m_EventsFilterJPanel, BorderLayout.CENTER);
		}

		public StkConnectionJPanel getConnection()
		{
			return this.m_StkConnectionJPanel;
		}

		public EventsFilterJPanel getEventsFilter()
		{
			return this.m_EventsFilterJPanel;
		}

		public void addActionListener(ActionListener al)
		{
			this.m_StkConnectionJPanel.addActionListener(al);
			this.m_EventsFilterJPanel.addActionListener(al);
		}

		public void removeActionListener(ActionListener al)
		{
			this.m_StkConnectionJPanel.removeActionListener(al);
			this.m_EventsFilterJPanel.removeActionListener(al);
		}
	}

	class StkConnectionJPanel
	extends JPanel
	{
		private static final long	serialVersionUID	= 1L;

		public final static String	s_CONNECTION		= "Connection";
		public final static String	s_START_STK_TEXT	= "Create New ...";
		public final static String	s_CONNECT_STK_TEXT	= "Get Running ...";
		public final static String	s_RELEASE_STK_TEXT	= "Release";

		private final static String	s_CONNECTED			= "CONNECTED";
		private final static String	s_DISCONNECTED		= "DISCONNECTED";

		private JButton				m_StartStkButton;
		private JButton				m_ConnectToStkButton;
		private JButton				m_ReleaseStkButton;
		private JLabel				m_StatusLabel;

		public StkConnectionJPanel()
		throws Exception
		{
			this.setLayout(new GridLayout(4, 1));

			TitledBorder b1 = new TitledBorder(new BevelBorder(BevelBorder.LOWERED), s_CONNECTION);
			CompoundBorder cb1 = new CompoundBorder(new EmptyBorder(5,5,5,5), b1);
			this.setBorder(cb1);

			this.m_StartStkButton = new JButton(s_START_STK_TEXT);
			this.m_StartStkButton.setEnabled(true);
			this.m_StartStkButton.setBorder(new BevelBorder(BevelBorder.RAISED));
			this.add(this.m_StartStkButton);

			this.m_ConnectToStkButton = new JButton(s_CONNECT_STK_TEXT);
			this.m_ConnectToStkButton.setEnabled(true);
			this.m_ConnectToStkButton.setBorder(new BevelBorder(BevelBorder.RAISED));
			this.add(this.m_ConnectToStkButton);

			this.m_ReleaseStkButton = new JButton(s_RELEASE_STK_TEXT);
			this.m_ReleaseStkButton.setEnabled(false);
			this.m_ReleaseStkButton.setBorder(new BevelBorder(BevelBorder.RAISED));
			this.add(this.m_ReleaseStkButton);

			JPanel phantomPanel = new JPanel();
			CompoundBorder phantomBorder = new CompoundBorder(new BevelBorder(BevelBorder.RAISED), new EmptyBorder(7,7,7,7));
			phantomPanel.setBorder(phantomBorder);
			phantomPanel.setLayout(new BorderLayout());
			this.add(phantomPanel);
			
			this.m_StatusLabel = new JLabel();
			this.m_StatusLabel.setOpaque(true);
			this.m_StatusLabel.setBackground(Color.RED);
			this.m_StatusLabel.setText(s_DISCONNECTED);
			this.m_StatusLabel.setHorizontalTextPosition(SwingConstants.CENTER);
			this.m_StatusLabel.setHorizontalAlignment(SwingConstants.CENTER);
			this.m_StatusLabel.setBorder(new BevelBorder(BevelBorder.LOWERED));
			phantomPanel.add(this.m_StatusLabel);
		}

		public void addActionListener(ActionListener al)
		{
			this.m_StartStkButton.addActionListener(al);
			this.m_ConnectToStkButton.addActionListener(al);
			this.m_ReleaseStkButton.addActionListener(al);
		}

		public void removeActionListener(ActionListener al)
		{
			this.m_StartStkButton.removeActionListener(al);
			this.m_ConnectToStkButton.removeActionListener(al);
			this.m_ReleaseStkButton.removeActionListener(al);
		}

		public void setConnectToStk(boolean connected)
		{
			this.m_StartStkButton.setEnabled(!connected);
			this.m_ConnectToStkButton.setEnabled(!connected);
			this.m_ReleaseStkButton.setEnabled(connected);

			if(connected)
			{
				this.m_StatusLabel.setBackground(Color.GREEN);
				this.m_StatusLabel.setText(s_CONNECTED);
			}
			else
			{
				this.m_StatusLabel.setBackground(Color.RED);
				this.m_StatusLabel.setText(s_DISCONNECTED);
			}
		}
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

	private class RootNewEventsAdapter
	implements IAgStkObjectRootEvents2
	{
		public void onAgStkObjectRootEvent(AgStkObjectRootEvent evt)
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

				if(type == AgStkObjectRootEvent.TYPE_ON_ANIM_UPDATE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimUpdate " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_FASTER)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimationFaster " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_PAUSE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimationPause " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_PLAYBACK)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimationPlayback " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_REWIND)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimationRewind " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_SLOWER)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimationSlower " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_STEP)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimationStep " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_ANIMATION_STEP_BACK)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onAnimationStepBack " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_LOG_MESSAGE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onLogMessage " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_PERCENT_COMPLETE_BEGIN)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onPercentCompleteBegin " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_PERCENT_COMPLETE_END)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onPercentCompleteEnd " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_PERCENT_COMPLETE_UPDATE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onPercentCompleteUpdate " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_SCENARIO_BEFORE_CLOSE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioBeforeClose " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_SCENARIO_BEFORE_SAVE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioBeforeSave " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_SCENARIO_CLOSE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioClose " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_SCENARIO_LOAD)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioLoad " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_SCENARIO_NEW)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioNew " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_SCENARIO_SAVE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onScenarioSave " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_STK_OBJECT_ADDED)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onStkObjectAdded " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_STK_OBJECT_CHANGED)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onStkObjectChanged " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_STK_OBJECT_DELETED)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onStkObjectDeleted " + sb.toString());
				}
				else if(type == AgStkObjectRootEvent.TYPE_ON_STK_OBJECT_RENAMED)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("onStkObjectRenamed " + sb.toString());
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}
}