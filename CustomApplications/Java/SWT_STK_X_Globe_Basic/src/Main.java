/* ==============================================================
 * This sample was last tested with the following configuration:
 * ==============================================================
 * Eclipse SDK Version: 4.2.0 Build id: I20120608-1400
 * JRE 1.4.2_10 and greater
 * STK 10.0
 * ==============================================================
 */

// Java API
import java.io.*;
import java.util.logging.*;

// Eclipse SWT API
import org.eclipse.swt.*;
import org.eclipse.swt.layout.*;
import org.eclipse.swt.widgets.*;
import org.eclipse.swt.events.*;
import org.eclipse.swt.graphics.*;

// AGI Java API
import agi.core.*;
import agi.swt.*;
import agi.core.logging.*;
import agi.core.swt.*;
import agi.stkx.*;
import agi.stkx.swt.*;
import agi.stkobjects.*; // easier to just include all from stkobjects package
import agi.stkengine.*;

public final class Main
{
	private Display					m_Display;
	private Shell					m_Shell;

	private Menu					m_MenuBar;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;

	// ===========================================================
	// STKX Notes:
	// ===========================================================
	private AgSTKXApplicationClass	m_AgSTKXApplication;
	private AgStkObjectRootClass	m_AgStkObjectRoot;

	public static void main(String[] args)
	{
		try
		{
			AgCore_JNI.xInitThreads();
			
			// ================================================
			// Set the logging level to Level.FINEST to get
			// all AGI java console logging
			// ================================================
			ConsoleHandler ch = new ConsoleHandler();
			ch.setLevel(Level.OFF);
			ch.setFormatter(new AgFormatter());
			Logger.getLogger("agi").setLevel(Level.OFF);
			Logger.getLogger("agi").addHandler(ch);

			Display d = new Display();
			Main t = new Main(d);
			t.run();
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

	/* package */Main(Display d)
	throws AgException
	{
		this.m_Display = d;

		this.m_Shell = new Shell(this.m_Display);
		this.m_Shell.setText("CustomApp_SWT_STK_X_Globe_Basic");
		this.m_Shell.setSize(1000, 618);
		this.m_Shell.setImage(new AgAGIImage(this.m_Display).getImage());

		GridLayout layout = new GridLayout(1, false);
		this.m_Shell.setLayout(layout);

		AgSwt_JNI.initialize_SwtDelegate();
		AgStkCustomApplication_JNI.initialize();
		AgSwt_JNI.initialize_SwtComponents();

		this.m_Shell.addDisposeListener(new AgDisposeAdapter());

		this.m_AgSTKXApplication = new AgSTKXApplicationClass();

		if(!this.m_AgSTKXApplication.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			MessageBox mb = new MessageBox(this.m_Shell, SWT.ICON_ERROR | SWT.OK);
			mb.setText("License Error");
			mb.setMessage(msg);
			mb.open();
			System.exit(0);
		}

		if(!this.m_AgSTKXApplication.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
		{
			String msg = "You do not have the required STK Globe license.  The sample's globe will not display properly.";
			MessageBox mb = new MessageBox(this.m_Shell, SWT.ICON_ERROR | SWT.OK);
			mb.setText("License Error");
			mb.setMessage(msg);
			mb.open();
		}

		this.m_AgStkObjectRoot = new AgStkObjectRootClass();

		this.buildMenuBar();

		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass(this.m_Shell, SWT.NONE);
		this.m_AgGlobeCntrlClass.setLayoutData(new GridData(SWT.FILL, SWT.FILL, true, true, 1, 1));
	}

	private void buildMenuBar()
	{
		this.m_MenuBar = new Menu(this.m_Shell, SWT.BAR);
		this.m_Shell.setMenuBar(this.m_MenuBar);

		this.buildScenarioMenu();
		this.buildHelpMenu();
	}

	private void buildScenarioMenu()
	{
		MenuItem scenItem = new MenuItem(this.m_MenuBar, SWT.CASCADE);
		scenItem.setText("&Scenario");

		Menu submenu = new Menu(this.m_Shell, SWT.DROP_DOWN);
		scenItem.setMenu(submenu);

		MenuItem newitem = new MenuItem(submenu, SWT.PUSH);
		newitem.addListener(SWT.Selection, new NewScenarioListener());
		newitem.setText("&New...\tCtrl+N");
		newitem.setAccelerator(SWT.MOD1 + 'N');

		MenuItem openitem = new MenuItem(submenu, SWT.PUSH);
		openitem.addListener(SWT.Selection, new OpenScenarioListener());
		openitem.setText("&Open...\tCtrl+O");
		openitem.setAccelerator(SWT.MOD1 + 'O');

		MenuItem closeitem = new MenuItem(submenu, SWT.PUSH);
		closeitem.addListener(SWT.Selection, new CloseScenarioListener());
		closeitem.setText("&Close\tCtrl+C");
		closeitem.setAccelerator(SWT.MOD1 + 'C');
	}

	/* package */class NewScenarioListener
	implements Listener
	{
		public void handleEvent(Event e)
		{
			newScenario("SwtAgiCanvas");
		}
	}

	/* package */class OpenScenarioListener
	implements Listener
	{
		public void handleEvent(Event e)
		{
			openScenario();
		}
	}

	/* package */class CloseScenarioListener
	implements Listener
	{
		public void handleEvent(Event e)
		{
			closeScenario();
		}
	}

	private void buildHelpMenu()
	{
		MenuItem helpItem = new MenuItem(this.m_MenuBar, SWT.CASCADE);
		helpItem.setText("&Help");

		Menu submenu = new Menu(this.m_Shell, SWT.DROP_DOWN);
		helpItem.setMenu(submenu);

		MenuItem diritem = new MenuItem(submenu, SWT.PUSH);
		diritem.addListener(SWT.Selection, new DirectionsListener());
		diritem.setText("&Directions\tCtrl+D");
		diritem.setAccelerator(SWT.MOD1 + 'D');

		MenuItem installitem = new MenuItem(submenu, SWT.PUSH);
		installitem.addListener(SWT.Selection, new InstallationInfoListener());
		installitem.setText("&Installation Info\tCtrl+I");
		installitem.setAccelerator(SWT.MOD1 + 'I');

		MenuItem aboutitem = new MenuItem(submenu, SWT.PUSH);
		aboutitem.addListener(SWT.Selection, new AboutListener());
		aboutitem.setText("&About\tCtrl+A");
		aboutitem.setAccelerator(SWT.MOD1 + 'A');
	}

	/* package */class DirectionsListener
	implements Listener
	{
		public void handleEvent(Event e)
		{
			StringBuffer msg = new StringBuffer();
			msg.append("1. In the menubar, click the \"Scenario->New...\" or \"Scenario->Open...\" Menu Item");
			msg.append("\n");
			msg.append("2. Interact with the 3D Globe control");
			msg.append("\n");
			msg.append("3. In the menubar, click the \"Scenario->Close\" Menu Item");
			msg.append("\n");

			MessageBox mb = new MessageBox(Main.this.m_Shell, SWT.ICON_INFORMATION | SWT.OK);
			mb.setText("Sample Directions");
			mb.setMessage(msg.toString());
			mb.open();
		}
	}

	/* package */class InstallationInfoListener
	implements Listener
	{
		public void handleEvent(Event e)
		{
			String msg = null;
			int iconStyle = SWT.ICON_INFORMATION;
			try
			{
				IAgStkObject scenObj = Main.this.m_AgStkObjectRoot.getCurrentScenario();
				if(scenObj != null)
				{
					agi.stkutil.IAgExecCmdResult result = Main.this.m_AgStkObjectRoot.executeCommand("GetReport * \"InstallInfoCon\"");
					int cnt = result.getCount();

					StringBuffer sb = new StringBuffer();
					for(int i = 0; i < cnt; i++)
					{
						sb.append(result.getItem(i));
						sb.append("\r\n");
					}
					result = null;
					msg = sb.toString();
				}
				else
				{
					msg = "No scenario is loaded. Please load a scenario before retrieving installation info";
					iconStyle = SWT.ICON_WARNING;
				}
			}
			catch(Exception ex)
			{
				msg = ex.getMessage();
				iconStyle = SWT.ICON_ERROR;
			}

			if(msg == null)
			{
				msg = "Installation information is not available.";
				iconStyle = SWT.ICON_ERROR;
			}

			MessageBox mb = new MessageBox(Main.this.m_Shell, iconStyle | SWT.OK);
			mb.setText("Install Information");
			mb.setMessage(msg);
			mb.open();
		}
	}

	/* package */class AboutListener
	implements Listener
	{
		public void handleEvent(Event e)
		{
			String javaVer = System.getProperty("java.version");
			String javaClsVer = System.getProperty("java.class.version");

			String stkJavaApiVersion = null;
			try
			{
				stkJavaApiVersion = Main.this.m_AgSTKXApplication.getVersion();
			}
			catch(Exception ex)
			{
				ex.printStackTrace();
			}

			StringBuffer msg = new StringBuffer();
			msg.append("Name:\t\t\tCustomApp_SWT_STK_X_Globe_Basic");
			msg.append("\n");

			if(stkJavaApiVersion != null)
			{
				msg.append("Version:\t\t\t" + stkJavaApiVersion + "\t");
				msg.append("\n");
			}

			if(javaVer != null)
			{
				msg.append("Java Version:\t\t" + javaVer + "\t");
				msg.append("\n");
			}

			if(javaClsVer != null)
			{
				msg.append("Java Cls Version:\t\t" + javaClsVer + "\t");
				msg.append("\n");
			}

			MessageBox mb = new MessageBox(Main.this.m_Shell, SWT.ICON_INFORMATION | SWT.OK);
			mb.setText("Sample About");
			mb.setMessage(msg.toString());
			mb.open();
		}
	}

	public void run()
	throws AgException
	{
		this.centerAppOnScreen();
		this.m_Shell.layout();
		this.m_Shell.open();

		while(!this.m_Shell.isDisposed())
		{
			if(!this.m_Display.readAndDispatch())
			{
				this.m_Display.sleep();
			}
		}
		this.m_Display.dispose();
	}

	private void centerAppOnScreen()
	{
		Monitor primary = this.m_Display.getPrimaryMonitor();
		Rectangle bounds = primary.getBounds();
		Rectangle rect = this.m_Shell.getBounds();
		int x = bounds.x + (bounds.width - rect.width) / 2;
		int y = bounds.y + (bounds.height - rect.height) / 2;
		this.m_Shell.setLocation(x, y);
	}

	private void newScenario(String name)
	{
		try
		{
			this.m_AgStkObjectRoot.closeScenario();
			this.m_AgStkObjectRoot.newScenario(name);
			IAgStkObject scenObject = this.m_AgStkObjectRoot.getCurrentScenario();
			IAgStkObjectCollection scenChildren = scenObject.getChildren();
			scenChildren._new(AgESTKObjectType.E_FACILITY, "fac1");
		}
		catch(Exception e)
		{
			StringBuffer msg = new StringBuffer();
			msg.append("Failed to create new scenario!");
			msg.append("\n\n");
			msg.append("Scenario Name: " + name);
			msg.append("\n\n");
			msg.append("Exception Msg:      " + e.getMessage());
			msg.append("\n");
			msg.append("Exception Filename: " + e.getStackTrace()[1].getFileName());
			msg.append("\n");
			msg.append("Exception Line No:  " + e.getStackTrace()[1].getLineNumber());
			if(e instanceof AgCoreException)
			{
				msg.append("\n");
				msg.append("Exception HRESULT = " + ((AgCoreException)e).getHResultAsHexString());
			}

			MessageBox mb = new MessageBox(this.m_Shell, SWT.ICON_ERROR | SWT.OK);
			mb.setText("New Scenario Error");
			mb.setMessage(msg.toString());
			mb.open();
		}
	}

	private String getStkHomeDirPath()
	throws AgCoreException
	{
		agi.stkutil.IAgExecCmdResult res = null;
		res = this.m_AgStkObjectRoot.executeCommand("GetDirectory / STKHome");
		String homeDir = res.getItem(0);
		return homeDir;
	}

	private void openScenario()
	{
		String filePath = null;

		try
		{
			System.out.println("Opening scenario");
			String path = getStkHomeDirPath();
			FileDialog dialog = new FileDialog(this.m_Shell, SWT.OPEN);
			dialog.setFilterNames(new String[] {"Scenario Files"});
			dialog.setFilterExtensions(new String[] {"*.sc"});
			String filesep = System.getProperty("file.separator");
			String newpath = path + filesep + "CodeSamples" + filesep + "SharedResources" + filesep + "Scenarios" + filesep;
			dialog.setFilterPath(newpath);
			dialog.setText("Open scenario");

			// blocking call till user completes one of the following ...
			// 1. selects file and clicks ok, to dismiss the dialog.
			// 2. selects cancel, to dismiss the dialog.
			filePath = dialog.open();
			if(filePath != null)
			{
				File f = new File(filePath);
				if(f.exists() && f.isFile())
				{
					this.m_AgStkObjectRoot.closeScenario();
					this.m_AgStkObjectRoot.loadScenario(filePath);
				}
			}
		}
		catch(Exception e)
		{
			StringBuffer msg = new StringBuffer();
			msg.append("Failed to open scenario!");
			msg.append("\n\n");
			msg.append("Scenario File: " + filePath);
			msg.append("\n\n");
			msg.append("Exception Msg:      " + e.getMessage());
			msg.append("\n");
			msg.append("Exception Filename: " + e.getStackTrace()[1].getFileName());
			msg.append("\n");
			msg.append("Exception Line No:  " + e.getStackTrace()[1].getLineNumber());
			if(e instanceof AgCoreException)
			{
				msg.append("\n");
				msg.append("Exception HRESULT = " + ((AgCoreException)e).getHResultAsHexString());
			}

			MessageBox mb = new MessageBox(this.m_Shell, SWT.ICON_ERROR | SWT.OK);
			mb.setText("Open Scenario Error");
			mb.setMessage(msg.toString());
			mb.open();
		}
	}

	private void closeScenario()
	{
		try
		{
			this.m_AgStkObjectRoot.closeScenario();
		}
		catch(Exception e)
		{
			StringBuffer msg = new StringBuffer();
			msg.append("Failed to close scenario!");
			msg.append("\n\n");
			msg.append("Exception Msg:      " + e.getMessage());
			msg.append("\n");
			msg.append("Exception Filename: " + e.getStackTrace()[1].getFileName());
			msg.append("\n");
			msg.append("Exception Line No:  " + e.getStackTrace()[1].getLineNumber());
			if(e instanceof AgCoreException)
			{
				msg.append("\n");
				msg.append("Exception HRESULT = " + ((AgCoreException)e).getHResultAsHexString());
			}

			MessageBox mb = new MessageBox(this.m_Shell, SWT.ICON_ERROR | SWT.OK);
			mb.setText("Close Scenario Error");
			mb.setMessage(msg.toString());
			mb.open();
		}
	}

	class AgDisposeAdapter
	implements DisposeListener
	{
		public void widgetDisposed(DisposeEvent e)
		{
			try
			{
				Main.this.m_AgGlobeCntrlClass.dispose();
				
				// Reverse of the initialization order
				AgSwt_JNI.uninitialize_SwtComponents();
				AgStkCustomApplication_JNI.uninitialize();
				AgSwt_JNI.uninitialize_SwtDelegate();
			}
			catch(Exception ex)
			{
				ex.printStackTrace();
			}
		}
	}
}