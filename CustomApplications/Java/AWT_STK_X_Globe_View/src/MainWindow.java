/* ==============================================================
 * This sample was last tested with the following configuration:
 * ==============================================================
 * Eclipse 3.7.0 Build id: I20110613-1736
 * JRE 1.4.2_10 and greater
 * STK 10.0
 * ==============================================================
 */

//Java
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

//AGI Java API
import agi.core.logging.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.stk.core.swing.menus.globe.view.*;
import agi.stk.core.swing.toolbars.globe.view.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.stkengine.*;

//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements IAgSTKXApplicationEvents2, IAgGlobeViewJToolBarEventsListener, IAgGlobeViewJMenuEventsListener
{
	private static final long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Globe_View";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;

	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;
	private AgGlobeViewJMenu		m_AgGlobeViewJMenu;
	private AgGlobeViewJToolBar		m_AgGlobeViewJToolBar;

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
		this.m_AgSTKXApplicationClass.addIAgSTKXApplicationEvents2(this);
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

		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
		this.m_AgGlobeCntrlClass.setBackground(awtColor);
		this.getContentPane().add(this.m_AgGlobeCntrlClass, BorderLayout.CENTER);

		this.m_AgGlobeViewJToolBar = new AgGlobeViewJToolBar();
		this.m_AgGlobeViewJToolBar.addGlobeViewJToolBarListener(this);
		this.getContentPane().add(this.m_AgGlobeViewJToolBar, BorderLayout.NORTH);

		this.m_AgGlobeViewJMenu = new AgGlobeViewJMenu();
		this.m_AgGlobeViewJMenu.addGlobeViewJMenuListener(this);
		super.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu().add(this.m_AgGlobeViewJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	public void onGlobeViewJMenuAction(AgGlobeViewJMenuEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int winid = this.m_AgGlobeCntrlClass.getWinID();

			int action = e.getGlobeViewJMenuAction();
			
			if(action == AgGlobeViewJMenuEvent.ACTION_VIEW_ZOOM_IN)
			{
				this.m_AgStkObjectRootClass.executeCommand("Window3D * InpDevMode Mode RubberBandViewLLA WindowID " + winid);
			}
			else if(action == AgGlobeViewJMenuEvent.ACTION_VIEW_HOME)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View Home WindowID " + winid);
			}
			else if(action == AgGlobeViewJMenuEvent.ACTION_VIEW_ORIENT_NORTH)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View North WindowID " + winid);
			}
			else if(action == AgGlobeViewJMenuEvent.ACTION_VIEW_ORIENT_FROM_TOP)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View Top WindowID " + winid);
			}
			else if(action == AgGlobeViewJMenuEvent.ACTION_VIEW_STOREDVIEW_PREVIOUS)
			{
				this.m_AgGlobeViewJMenu.getStoredViewViewJMenu().previousStoredView();
				String currentSelectedView = this.m_AgGlobeViewJMenu.getStoredViewViewJMenu().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
			}
			else if(action == AgGlobeViewJMenuEvent.ACTION_VIEW_STOREDVIEW_NEXT)
			{
				this.m_AgGlobeViewJMenu.getStoredViewViewJMenu().nextStoredView();
				String currentSelectedView = this.m_AgGlobeViewJMenu.getStoredViewViewJMenu().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
			}
			else if(action == AgGlobeViewJMenuEvent.ACTION_VIEW_STOREDVIEW_VIEW)
			{
				String currentSelectedView = this.m_AgGlobeViewJMenu.getStoredViewViewJMenu().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
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
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void onGlobeViewJToolBarAction(AgGlobeViewJToolBarEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int winid = this.m_AgGlobeCntrlClass.getWinID();

			int action = e.getGlobeViewJToolBarAction();
			
			if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_ZOOM_IN)
			{
				this.m_AgStkObjectRootClass.executeCommand("Window3D * InpDevMode Mode RubberBandViewLLA WindowID " + winid);
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_HOME)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View Home WindowID " + winid);
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_ORIENT_NORTH)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View North WindowID " + winid);
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_ORIENT_FROM_TOP)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View Top WindowID " + winid);
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_STOREDVIEW_PREVIOUS)
			{
				this.m_AgGlobeViewJToolBar.getStoredViewJComboBox().previousStoredView();
				String currentSelectedView = this.m_AgGlobeViewJToolBar.getStoredViewJComboBox().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_STOREDVIEW_NEXT)
			{
				this.m_AgGlobeViewJToolBar.getStoredViewJComboBox().nextStoredView();
				String currentSelectedView = this.m_AgGlobeViewJToolBar.getStoredViewJComboBox().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_STOREDVIEW_VIEW)
			{
				String currentSelectedView = this.m_AgGlobeViewJToolBar.getStoredViewJComboBox().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
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
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void onAgSTKXApplicationEvent(AgSTKXApplicationEvent evt)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int type = evt.getType();

			if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_CLOSE)
			{
				clearStoredViews();
			}
			else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_LOAD)
			{
				populateStoredViews();
			}
			else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_NEW)
			{
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
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void clearStoredViews()
	{
		this.m_AgGlobeViewJToolBar.getStoredViewJComboBox().clearStoredViews();
		this.m_AgGlobeViewJMenu.getStoredViewViewJMenu().clearStoredViews();
	}
	
	public void populateStoredViews() 
	throws AgCoreException
	{
		String connectCommand1 = "VO_R * StoredViewNames";
		agi.stkutil.IAgExecCmdResult result1 = this.m_AgStkObjectRootClass.executeCommand(connectCommand1);
		if(result1 != null)
		{										
			String item = result1.getItem(0);	
			item = item.trim();
			String parts[] = item.split("\" \"");
			for(int i = 0; i<parts.length; i++)
			{
				if(!parts[i].startsWith("\""))
					parts[i] = "\"" +parts[i];
				if(!parts[i].endsWith("\""))
					parts[i] = parts[i] + "\"";
				
				this.addStoredView(parts[i]);
			}			
		}
		this.refreshCurrentStoredView();
	}
	
	public void addStoredView(String storedViewName)
	{
		this.m_AgGlobeViewJToolBar.getStoredViewJComboBox().addStoredView(storedViewName);
		this.m_AgGlobeViewJMenu.getStoredViewViewJMenu().addStoredView(storedViewName);
	}
	
	public void refreshCurrentStoredView()
	throws AgCoreException
	{
		String connectCommand = "VO_R * 3dView " + this.m_AgGlobeCntrlClass.getWinID();
		agi.stkutil.IAgExecCmdResult result = this.m_AgStkObjectRootClass.executeCommand(connectCommand);
		if(result != null)
		{
			String currentView = result.getItem(0).trim().split(" ")[0];
			this.m_AgGlobeViewJToolBar.getStoredViewJComboBox().setSelectedStoredView(currentView);
			this.m_AgGlobeViewJMenu.getStoredViewViewJMenu().setSelectedStoredView(currentView);
		}
	}

	class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				((Component)MainWindow.this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

				// Remove event listener
				MainWindow.this.m_AgGlobeViewJMenu.removeGlobeViewJMenuListener(MainWindow.this);
				MainWindow.this.m_AgGlobeViewJToolBar.removeGlobeViewJToolBarListener(MainWindow.this);
				MainWindow.this.m_AgSTKXApplicationClass.removeIAgSTKXApplicationEvents2(MainWindow.this);
				
				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();

				// Release the root and then app just to be "clean"
				MainWindow.this.m_AgStkObjectRootClass.release();
				MainWindow.this.m_AgSTKXApplicationClass.release();

				// Reverse of the initialization order
				AgAwt_JNI.uninitialize_AwtComponents();
				AgStkCustomApplication_JNI.uninitialize();
				AgAwt_JNI.uninitialize_AwtDelegate();
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
				((Component)MainWindow.this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
			}
		}
	}
}