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
import agi.stk.core.swing.toolbars.globe.objectediting.*;
import agi.stkengine.*;

//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements IAgSTKXApplicationEvents2, IAgGlobeObjectEditingJToolBarEventsListener
{
	private static final long				serialVersionUID	= 1L;

	private final static String				s_TITLE				= "CustomApp_AWT_STK_X_Globe_ObjectEditing";
	private final static String				s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass			m_AgSTKXApplicationClass;
	private AgStkObjectRootClass			m_AgStkObjectRootClass;

	private AgGlobeCntrlClass				m_AgGlobeCntrlClass;
	private AgGlobeObjectEditingJToolBar	m_AgGlobeObjectEditingJToolBar;

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

		this.m_AgGlobeObjectEditingJToolBar = new AgGlobeObjectEditingJToolBar();
		this.m_AgGlobeObjectEditingJToolBar.addGlobeObjectEditingJToolBarListener(this);
		this.getContentPane().add(this.m_AgGlobeObjectEditingJToolBar, BorderLayout.NORTH);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	public void onGlobeObjectEditingJToolBarAction(AgGlobeObjectEditingJToolBarEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int action = e.getGlobeObjectEditingJToolBarAction();

			if(action == AgGlobeObjectEditingJToolBarEvent.ACTION_OBJECTEDITING_START)
			{
				String objectPath = (String)e.getData()[0];
				this.m_AgGlobeCntrlClass.startObjectEditing(objectPath);
			}
			else if(action == AgGlobeObjectEditingJToolBarEvent.ACTION_OBJECTEDITING_APPLY)
			{
				this.m_AgGlobeCntrlClass.stopObjectEditing(false);
			}
			else if(action == AgGlobeObjectEditingJToolBarEvent.ACTION_OBJECTEDITING_CANCEL)
			{
				this.m_AgGlobeCntrlClass.stopObjectEditing(true);
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
				this.m_AgGlobeObjectEditingJToolBar.getObjectPathComboBox().removeAllItems();
			}
			else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_LOAD)
			{
				this.m_AgStkObjectRootClass.getCurrentScenario().getChildren()._new(AgESTKObjectType.E_FACILITY, "FacilityTo3DObjectEdit");

				this.m_AgStkObjectRootClass.executeCommand("New / */Aircraft Aircraft1");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 47.1 -120.8 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 41.8 -111.5 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 33.5 -110.0 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 45.8 -94.6 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 40.2 -49.1 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 34.8 -91.2 3000.0 200");

				populateObjectPaths();
			}
			else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_NEW)
			{
				this.m_AgStkObjectRootClass.getCurrentScenario().getChildren()._new(AgESTKObjectType.E_FACILITY, "FacilityTo3DObjectEdit");

				this.m_AgStkObjectRootClass.executeCommand("New / */Aircraft Aircraft1");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 47.1 -120.8 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 41.8 -111.5 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 33.5 -110.0 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 45.8 -94.6 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 40.2 -49.1 3000.0 200");
				this.m_AgStkObjectRootClass.executeCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 34.8 -91.2 3000.0 200");

				populateObjectPaths();
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

	/*package*/ void populateObjectPaths() 
	throws AgCoreException
	{
		IAgStkObjectCollection children = null;
		children = this.m_AgStkObjectRootClass.getCurrentScenario().getChildren();
		int count = children.getCount();
		for(int i=0; i<count; i++)
		{
			IAgStkObject child = children.getItem(new AgVariant(i));
			String path = child.getPath();
			if(child instanceof IAgFacility ||
			child instanceof IAgAircraft ||
			child instanceof IAgGroundVehicle || 
			child instanceof IAgShip ||
			child instanceof IAgTarget ||
			child instanceof IAgAreaTarget ||
			child instanceof IAgLineTarget)
			{
				this.m_AgGlobeObjectEditingJToolBar.getObjectPathComboBox().addItem(path);
			}
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
				MainWindow.this.m_AgGlobeObjectEditingJToolBar.removeGlobeObjectEditingJToolBarListener(MainWindow.this);

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