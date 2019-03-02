// Java API
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;
import java.util.logging.*;

// AGI Java API
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.core.logging.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.stkengine.*;
import agi.stk.core.swing.menus.map.view.*;
import agi.stk.core.swing.toolbars.map.view.*;

//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements IAgMapViewJToolBarEventsListener, IAgMapViewJMenuEventsListener
{
	private static final long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Map_View";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;

	private AgMapCntrlClass			m_AgMapCntrlClass;
	private AgMapViewJMenu			m_AgMapViewJMenu;
	private AgMapViewJToolBar		m_AgMapViewJToolBar;

	private boolean					m_AllowPan;

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

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgMapCntrlClass = new AgMapCntrlClass();
		this.m_AgMapCntrlClass.setBackColor(stkxColor);
		this.m_AgMapCntrlClass.setBackground(awtColor);
		this.getContentPane().add(this.m_AgMapCntrlClass, BorderLayout.CENTER);

		this.m_AgMapViewJToolBar = new AgMapViewJToolBar();
		this.m_AgMapViewJToolBar.addMapViewJToolBarListener(this);
		this.getContentPane().add(this.m_AgMapViewJToolBar, BorderLayout.NORTH);
		
		this.m_AgMapViewJMenu = new AgMapViewJMenu();
		this.m_AgMapViewJMenu.addMapViewJMenuListener(this);
		super.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu().add(this.m_AgMapViewJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	public void onMapViewJMenuAction(AgMapViewJMenuEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int action = e.getMapViewJMenuAction();

			if(action == AgMapViewJMenuEvent.ACTION_VIEW_ZOOM_IN)
			{
				this.zoomIn();
			}
			else if(action == AgMapViewJMenuEvent.ACTION_VIEW_ZOOM_OUT)
			{
				this.zoomOut();
			}
			else if(action == AgMapViewJMenuEvent.ACTION_VIEW_ALLOW_PAN)
			{
				this.allowPan();
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

	public void onMapViewJToolBarAction(AgMapViewJToolBarEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int action = e.getMapViewJToolBarAction();

			if(action == AgMapViewJToolBarEvent.ACTION_VIEW_ZOOM_IN)
			{
				this.zoomIn();
			}
			else if(action == AgMapViewJToolBarEvent.ACTION_VIEW_ZOOM_OUT)
			{
				this.zoomOut();
			}
			else if(action == AgMapViewJToolBarEvent.ACTION_VIEW_ALLOW_PAN)
			{
				this.allowPan();
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

	/* package */void zoomIn()
	throws AgCoreException
	{
		this.m_AgMapCntrlClass.zoomIn();
	}

	/* package */void zoomOut()
	throws AgCoreException
	{
		this.m_AgMapCntrlClass.zoomOut();
	}

	/* package */void allowPan()
	throws AgCoreException
	{
		this.m_AllowPan = !this.m_AllowPan;
		if(this.m_AllowPan)
		{
			this.m_AgMapCntrlClass.setPanModeEnabled(true);
		}
		else
		{
			this.m_AgMapCntrlClass.setPanModeEnabled(false);
		}

		this.m_AgMapViewJMenu.getAllowPanJMenuItem().setSelected(this.m_AllowPan);
		this.m_AgMapViewJToolBar.getAllowPanJButton().setSelected(this.m_AllowPan);
	}

	class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				((Component)MainWindow.this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

				// Remove event listeners
				MainWindow.this.m_AgMapViewJMenu.removeMapViewJMenuListener(MainWindow.this);
				MainWindow.this.m_AgMapViewJToolBar.removeMapViewJToolBarListener(MainWindow.this);

				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgMapCntrlClass.dispose();

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