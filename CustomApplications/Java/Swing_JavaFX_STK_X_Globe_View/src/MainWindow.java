// Java API
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

import javax.swing.*;
import javax.swing.plaf.metal.*;

// JavaFX API
import javafx.animation.*;
import javafx.application.*;
import javafx.event.*;
import javafx.util.*;
//AGI Java API
import agi.core.*;
import agi.ntvapp.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.stkengine.*;
//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKengineSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKEngineSampleBaseJFrame
implements EventHandler<ActionEvent>, IAgSTKXApplicationEvents2
{
	private final static long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_Swing_JavaFX_STK_X_Globe_View";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private IAgNtvAppEventsListener	m_IAgNtvAppEventsListener2;

	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;
	private boolean					m_ScenarioLoaded;
	private GlobeViewJFXPanel		m_GlobeViewJFXPanel;

	protected MainWindow()
	throws Throwable
	{
		super(MainWindow.class.getResource(s_DESCFILENAME));

		this.initialize();
	}

	private void initialize()
	throws AgException
	{
		try
		{
			final int width = 800;
			final int height = 600;

			this.setTitle(s_TITLE);
			this.setIconImage(new AgAGIImageIcon().getImage());
			this.setDefaultCloseOperation(EXIT_ON_CLOSE);
			this.addWindowListener(new MainWindowAdapter());
			this.setSize(width, height);

			this.m_IAgNtvAppEventsListener2 = new IAgNtvAppEventsListener()
			{
				public void onAgNtvAppEvent(AgNtvAppEvent e)
				{
					try
					{
						final int type = e.getType();
						
						SwingUtilities.invokeAndWait(new Runnable() 
						{
							public void run() 
							{
								if (type == AgNtvAppEvent.TYPE_ON_THREAD_START_END)
								{
									initApp(width, height);
								}
								else if (type == AgNtvAppEvent.TYPE_ON_THREAD_STOP_BEGIN)
								{
									uninitApp();
								}
							}
						});
					}
					catch(Throwable t)
					{
						t.printStackTrace();
					}
				}
			};
			this.addNtvAppEventsListener(this.m_IAgNtvAppEventsListener2);
		}
		catch(Throwable t)
		{
			throw new AgException(t);
		}
	}

	protected void finalize()
	{
		try
		{
			this.removeNtvAppEventsListener(this.m_IAgNtvAppEventsListener2);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void initApp(int width, int height)
	throws AgCoreException
	{
		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();

		MainWindow.this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		if(AgMetalThemeFactory.getEnabled())
		{
			java.awt.Color awtColor = mt.getPrimaryControl();
			AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);
			this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
			this.m_AgGlobeCntrlClass.setBackground(awtColor);
		}
		MainWindow.this.getStkEngineJPanel().add(MainWindow.this.m_AgGlobeCntrlClass, java.awt.BorderLayout.CENTER);

		// JFXPanel
		MainWindow.this.m_GlobeViewJFXPanel = new GlobeViewJFXPanel(mt.getControl());
		MainWindow.this.getStkEngineJPanel().add(MainWindow.this.m_GlobeViewJFXPanel, java.awt.BorderLayout.NORTH);
		MainWindow.this.m_GlobeViewJFXPanel.initScene(width, 35, this);

		IAgStkEngine stkengine = this.getStkEngine();
		AgSTKXApplicationClass app = stkengine.getSTKXApplication();
		app.addIAgSTKXApplicationEvents2(this);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();
	}

	private void uninitApp()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgSTKXApplicationClass app = stkengine.getSTKXApplication();
		app.removeIAgSTKXApplicationEvents2(this);

		MainWindow.this.getStkEngineJPanel().remove(MainWindow.this.m_AgGlobeCntrlClass);
		MainWindow.this.m_AgGlobeCntrlClass.dispose();
	}

	public void onAgSTKXApplicationEvent(AgSTKXApplicationEvent evt)
	{
		try
		{
			((java.awt.Component)this).setCursor(new java.awt.Cursor(java.awt.Cursor.WAIT_CURSOR));

			int type = evt.getType();

			if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_CLOSE)
			{
				MainWindow.this.m_ScenarioLoaded = false;
				clearStoredViews();
				hideJFXPanels();
			}
			else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_LOAD)
			{
				MainWindow.this.m_ScenarioLoaded = true;
				showJFXPanels();
				populateStoredViews();
			}
			else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_NEW)
			{
				MainWindow.this.m_ScenarioLoaded = true;
				showJFXPanels();
				populateStoredViews();
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
		finally
		{
			((java.awt.Component)this).setCursor(new java.awt.Cursor(java.awt.Cursor.DEFAULT_CURSOR));
		}
	}

	public void showJFXPanels()
	{
		MainWindow.this.m_GlobeViewJFXPanel.showScene();
	}

	public void hideJFXPanels()
	{
		MainWindow.this.m_GlobeViewJFXPanel.hideScene();
	}

	public void clearStoredViews()
	{
		this.m_GlobeViewJFXPanel.removeStoredViews();
	}

	public void populateStoredViews()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgSTKXApplicationClass app = stkengine.getSTKXApplication();

		String connectCommand1 = "VO_R * StoredViewNames";
		agi.stkx.IAgExecCmdResult result1 = app.executeCommand(connectCommand1);
		if(result1 != null)
		{
			String item = result1.getItem(0);
			item = item.trim();
			String parts[] = item.split("\" \"");
			for(int i = 0; i < parts.length; i++)
			{
				if(!parts[i].startsWith("\""))
					parts[i] = "\"" + parts[i];
				if(!parts[i].endsWith("\""))
					parts[i] = parts[i] + "\"";

				this.addStoredView(parts[i]);
			}
		}
		
		//Setup initial view
		String connectCommand = "VO * UseStoredView HomeView " + this.m_AgGlobeCntrlClass.getWinID();
		processConnectCommandButton(this.m_GlobeViewJFXPanel.m_StoredViewViewButton, connectCommand);
		this.refreshCurrentStoredView();
	}

	public void addStoredView(String storedViewName)
	{
		this.m_GlobeViewJFXPanel.addStoredView(storedViewName);
	}

	public void refreshCurrentStoredView()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgSTKXApplicationClass app = stkengine.getSTKXApplication();

		String connectCommand = "VO_R * 3dView " + this.m_AgGlobeCntrlClass.getWinID();
		agi.stkx.IAgExecCmdResult result = app.executeCommand(connectCommand);
		if(result != null)
		{
			String currentView = result.getItem(0).trim().split(" ")[0];
			this.m_GlobeViewJFXPanel.setSelectedStoredView(currentView);
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
				int winid = this.m_AgGlobeCntrlClass.getWinID();
				if(src == this.m_GlobeViewJFXPanel.m_GlobeViewHomeButton)
				{
					processConnectCommandButton(this.m_GlobeViewJFXPanel.m_GlobeViewHomeButton, "VO * View Home WindowID " + winid);
				}
				else if(src == this.m_GlobeViewJFXPanel.m_GlobeViewOrientFromTopButton)
				{
					processConnectCommandButton(this.m_GlobeViewJFXPanel.m_GlobeViewOrientFromTopButton, "VO * View Top WindowID " + winid);
				}
				else if(src == this.m_GlobeViewJFXPanel.m_GlobeViewOrientNorthButton)
				{
					processConnectCommandButton(this.m_GlobeViewJFXPanel.m_GlobeViewOrientNorthButton, "VO * View North WindowID " + winid);
				}
				else if(src == this.m_GlobeViewJFXPanel.m_GlobeViewZoomInButton)
				{
					processConnectCommandButton(this.m_GlobeViewJFXPanel.m_GlobeViewZoomInButton, "Window3D * InpDevMode Mode RubberBandViewLLA WindowID " + winid);
				}
				else if(src == this.m_GlobeViewJFXPanel.m_StoredViewPreviousButton)
				{
					String currentSelectedView = this.m_GlobeViewJFXPanel.getPreviousStoredView();
					
					if(currentSelectedView != null)
					{
						String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
						processConnectCommandButton(this.m_GlobeViewJFXPanel.m_StoredViewPreviousButton, connectCommand);
					}
				}
				else if(src == this.m_GlobeViewJFXPanel.m_StoredViewNextButton)
				{
					String currentSelectedView = this.m_GlobeViewJFXPanel.getNextStoredView();
					
					if(currentSelectedView != null)
					{
						String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
						processConnectCommandButton(this.m_GlobeViewJFXPanel.m_StoredViewNextButton, connectCommand);
					}
				}
				else if(src == this.m_GlobeViewJFXPanel.m_StoredViewViewButton)
				{
					String currentSelectedView = this.m_GlobeViewJFXPanel.getSelectedStoredView();
					
					if(currentSelectedView != null)
					{
						String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
						processConnectCommandButton(this.m_GlobeViewJFXPanel.m_StoredViewViewButton, connectCommand);
					}
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
			        
			        timeline.playFromStart();
			        timeline.getKeyFrames().clear();
				}
				catch(Throwable t)
				{
					t.printStackTrace();
				}
			}
		});
		MainWindow.this.getStkEngine().getSTKXApplication().executeCommand(cnctCmd);
	}
	
	class MainWindowAdapter
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