// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.border.*;
import javax.swing.plaf.metal.*;

//AGI Java API
import agi.core.logging.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.swing.*;
import agi.stkobjects.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.swing.msgviewer.*;
import agi.swing.toolbars.msgviewer.*;
import agi.stkengine.*;
//CodeSample helper code
import agi.customapplications.swing.*;


class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
{
	private static final long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Globe_Events";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgGlobeJPanel			m_AgGlobeJPanel;

	private JSplitPane				m_MainSplitPane;

	private AgMsgViewerJPanel		m_AgMsgViewerJPanelOldEvents;
	private AgMsgViewerJPanel		m_AgMsgViewerJPanelNewEvents;
	private JSplitPane				m_EventsJPanelSplitPane;

	private int						m_GlobeOldEventsIndex;
	private GlobeOldEventsAdapter	m_GlobeOldEventsAdapter;
	private GlobeNewEventsAdapter	m_GlobeNewEventsAdapter;

	public MainWindow()
	throws Throwable
	{
		super(Main.class.getResource(s_DESCFILENAME));

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
		long stkxColor = AgAwtColorTranslator.fromAWTtoLong(awtColor);

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

		this.m_GlobeOldEventsAdapter = new GlobeOldEventsAdapter();
		this.m_GlobeOldEventsIndex = this.m_AgGlobeJPanel.getControl().advise(this.m_GlobeOldEventsAdapter);

		this.m_GlobeNewEventsAdapter = new GlobeNewEventsAdapter();
		this.m_AgGlobeJPanel.getControl().addIAgGlobeCntrlEvents(this.m_GlobeNewEventsAdapter);

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

	private class GlobeOldEventsAdapter
	implements IAgUiAxVOCntrlEvents
	{
		public void click()
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("Click ");
		}

		public void dblClick()
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("DblClick ");
		}

		public void keyDown(short arg0, short arg1)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("KeyDown " + arg0 + " " + arg1);
		}

		public void keyPress(short arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("KeyPress " + arg0);
		}

		public void keyUp(short arg0, short arg1)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("KeyUp " + arg0 + " " + arg1);
		}

		public void mouseDown(short arg0, short arg1, int arg2, int arg3)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("MouseDown " + arg0 + " " + arg1 + " " + arg2 + " " + arg3);
		}

		public void mouseMove(short arg0, short arg1, int arg2, int arg3)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("MouseMove " + arg0 + " " + arg1 + " " + arg2 + " " + arg3);
			int x = arg2;
			int y = arg3;
			try
			{
				IAgPickInfoData pickInfoData = MainWindow.this.m_AgGlobeJPanel.getControl().pickInfo(x, y);

				String objPath = pickInfoData.getObjPath();
				double lat = pickInfoData.getLat();
				double lon = pickInfoData.getLon();
				double alt = pickInfoData.getAlt();

				if(pickInfoData.getIsObjPathValid())
				{
					MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("\tObjectPath=" + objPath);
				}

				if(pickInfoData.getIsLatLonAltValid())
				{
					MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("\tLLA: " + lat + " " + lon + " " + alt);
				}
			}
			catch(AgCoreException ce)
			{
				ce.printHexHresult();
				ce.printStackTrace();
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}

		public void mouseUp(short arg0, short arg1, int arg2, int arg3)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("MouseUp " + arg0 + " " + arg1 + " " + arg2 + " " + arg3);
		}

		public void mouseWheel(short arg0, short arg1, short arg2, int arg3, int arg4)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("MouseWheel " + arg0 + " " + arg1 + " " + arg2 + " " + arg3 + " " + arg4);
		}

		public void oLEDragDrop(IAgDataObject arg0, int arg1, short arg2, short arg3, int arg4, int arg5)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("OLEDragDrop " + arg0 + " " + arg1 + " " + arg2 + " " + arg3 + " " + arg4 + " " + arg5);
		}

		public void onObjectEditingApply(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("ObjectEditingApply " + arg0);
		}

		public void onObjectEditingCancel(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("ObjectEditingCancel " + arg0);
		}

		public void onObjectEditingStart(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("ObjectEditingStart " + arg0);
		}

		public void onObjectEditingStop(String arg0)
		{
			MainWindow.this.m_AgMsgViewerJPanelOldEvents.writeMessage("ObjectEditingStop " + arg0);
		}
	}

	private class GlobeNewEventsAdapter
	implements IAgGlobeCntrlEvents
	{
		public void onAgGlobeCntrlEvent(AgGlobeCntrlEvent evt)
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

				if(type == AgGlobeCntrlEvent.TYPE_CLICK)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("Click " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_DBL_CLICK)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("DblClick " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_KEY_UP)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("KeyUp " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_KEY_PRESS)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("KeyPress " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_KEY_DOWN)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("KeyDown " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_UP)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("MouseUp " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_MOVE)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("MouseMove " + sb.toString());
					Integer x = (Integer)params[2];
					Integer y = (Integer)params[3];

					IAgPickInfoData pickInfoData = MainWindow.this.m_AgGlobeJPanel.getControl().pickInfo(x.intValue(), y.intValue());

					String objPath = pickInfoData.getObjPath();
					double lat = pickInfoData.getLat();
					double lon = pickInfoData.getLon();
					double alt = pickInfoData.getAlt();

					if(pickInfoData.getIsObjPathValid())
					{
						MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("\tObjectPath=" + objPath);
					}

					if(pickInfoData.getIsLatLonAltValid())
					{
						MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("\tLLA: " + lat + " " + lon + " " + alt);
					}
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_DOWN)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("MouseDown " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_O_L_E_DRAG_DROP)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("OLEDragDrop " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_WHEEL)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("MouseWheel " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_ON_OBJECT_EDITING_APPLY)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("ObjectEditingApply " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_ON_OBJECT_EDITING_START)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("ObjectEditingStart " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_ON_OBJECT_EDITING_CANCEL)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("ObjectEditingCancel " + sb.toString());
				}
				else if(type == AgGlobeCntrlEvent.TYPE_ON_OBJECT_EDITING_STOP)
				{
					MainWindow.this.m_AgMsgViewerJPanelNewEvents.writeMessage("ObjectEditingStop " + sb.toString());
				}
			}
			catch(AgCoreException ce)
			{
				ce.printHexHresult();
				ce.printStackTrace();
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
				MainWindow.this.m_AgGlobeJPanel.getControl().unadvise(MainWindow.this.m_GlobeOldEventsIndex);

				MainWindow.this.m_AgGlobeJPanel.getControl().removeIAgGlobeCntrlEvents(MainWindow.this.m_GlobeNewEventsAdapter);

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