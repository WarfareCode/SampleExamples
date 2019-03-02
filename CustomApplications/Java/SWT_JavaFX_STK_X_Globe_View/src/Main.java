//This sample requires Java 1.7 or higher to compile

// Java API
import java.io.*;
import java.util.logging.*;

// JavaFX API
import javafx.animation.*;
import javafx.application.*;
import javafx.util.*;

// Eclipse API
import org.eclipse.swt.*;
import org.eclipse.swt.layout.*;
import org.eclipse.swt.widgets.*;
import org.eclipse.swt.events.*;
import org.eclipse.swt.graphics.*;

// AGI Java API
import agi.core.*;
import agi.core.logging.*;
import agi.core.swt.*;
import agi.stkx.*;
import agi.stkx.swt.*;
import agi.stkengine.*;
import agi.swt.AgAGIImage;

public final class Main
implements javafx.event.EventHandler<javafx.event.ActionEvent>, IAgSTKXApplicationEvents2
{
	private Display						m_Display;
	private Shell						m_Shell;

	private Menu						m_MenuBar;
	private GlobeViewFXCanvas			m_gc;
	private GraphicsViewComposite		m_gvc;

	// ===========================================================
	// STKX Notes:
	// ===========================================================
	private AgSTKXApplicationClass		m_AgSTKXApplicationClass;
	private boolean						m_ScenarioLoaded;

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
		int width = 900;
		int height = 500;

		this.m_Display = d;

		this.m_Shell = new Shell(this.m_Display);
		this.m_Shell.setText("CustomApp_SWT_JavaFX_STK_X_Globe_View");
		this.m_Shell.setSize(width, height);
		this.m_Shell.setBackground(this.m_Display.getSystemColor(SWT.COLOR_BLACK));

		this.m_Shell.setImage(new AgAGIImage(this.m_Display).getImage());

		GridLayout layout = new GridLayout(1, false);
		this.m_Shell.setLayout(layout);

		AgSwt_JNI.initialize_SwtDelegate();
		AgStkCustomApplication_JNI.initialize();
		AgSwt_JNI.initialize_SwtComponents();

		this.m_Shell.addDisposeListener(new AgDisposeAdapter());

		this.m_AgSTKXApplicationClass = new AgSTKXApplicationClass();
		this.m_AgSTKXApplicationClass.addIAgSTKXApplicationEvents2(this);
		
		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			MessageBox mb = new MessageBox(this.m_Shell, SWT.ICON_ERROR | SWT.OK);
			mb.setText("License Error");
			mb.setMessage(msg);
			mb.open();
			System.exit(0);
		}

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
		{
			String msg = "You do not have the required STK Globe license.  The sample's globe will not display properly.";
			MessageBox mb = new MessageBox(this.m_Shell, SWT.ICON_ERROR | SWT.OK);
			mb.setText("License Error");
			mb.setMessage(msg);
			mb.open();
		}

		this.buildMenuBar();

		this.m_gc = new GlobeViewFXCanvas(this.m_Shell, SWT.NONE);
		this.m_gc.setLayoutData(new GridData(SWT.FILL, SWT.FILL, true, false, 1, 1));
		this.m_gc.drawScene(width, 35, this);

		this.m_gvc = new GraphicsViewComposite(this.m_Shell);
		this.m_gvc.setLayoutData(new GridData(SWT.FILL, SWT.FILL, true, true, 1, 1));
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
			msg.append("2. In the view bar, click any button");
			msg.append("\n");
			msg.append("3. In the menubar, click the \"Scenario->Close\" Menu Item");
			msg.append("\n");

			MessageBox mb = new MessageBox(m_Shell, SWT.ICON_INFORMATION | SWT.OK);
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
				if(m_ScenarioLoaded)
				{
					agi.stkx.IAgExecCmdResult result = Main.this.m_AgSTKXApplicationClass.executeCommand("GetReport * \"InstallInfoCon\"");
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

			MessageBox mb = new MessageBox(m_Shell, iconStyle | SWT.OK);
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
				stkJavaApiVersion = Main.this.m_AgSTKXApplicationClass.getVersion();
			}
			catch(Exception ex)
			{
				ex.printStackTrace();
			}

			StringBuffer msg = new StringBuffer();
			msg.append("Name:\t\t\tCustomApp_SWT_JavaFX_STK_X_Globe_View");
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

	public void onAgSTKXApplicationEvent(AgSTKXApplicationEvent evt)
	{
		try
		{
			int type = evt.getType();

			if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_CLOSE)
			{
				Main.this.m_ScenarioLoaded = false;
				clearStoredViews();
			}
			else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_LOAD)
			{
				Main.this.m_ScenarioLoaded = true;
				populateStoredViews();
			}
			else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_NEW)
			{
				Main.this.m_ScenarioLoaded = true;
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
	}

	public void clearStoredViews()
	{
		this.m_gc.removeStoredViews();
	}

	public void populateStoredViews()
	throws AgCoreException
	{
		String connectCommand1 = "VO_R * StoredViewNames";
		agi.stkx.IAgExecCmdResult result1 = this.m_AgSTKXApplicationClass.executeCommand(connectCommand1);
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
		String connectCommand = "VO * UseStoredView HomeView " + this.m_gvc.getGlobe().getWinID();
		processConnectCommandButton(this.m_gc.m_StoredViewViewButton, connectCommand);
		this.refreshCurrentStoredView();
	}

	public void addStoredView(String storedViewName)
	{
		this.m_gc.addStoredView(storedViewName);
	}

	public void refreshCurrentStoredView()
	throws AgCoreException
	{
		String connectCommand = "VO_R * 3dView " + this.m_gvc.getGlobe().getWinID();
		agi.stkx.IAgExecCmdResult result = this.m_AgSTKXApplicationClass.executeCommand(connectCommand);
		if(result != null)
		{
			String currentView = result.getItem(0).trim().split(" ")[0];
			this.m_gc.setSelectedStoredView(currentView);
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
				int winid = this.m_gvc.getGlobe().getWinID();
				if(src == this.m_gc.m_GlobeViewHomeButton)
				{
					processConnectCommandButton(this.m_gc.m_GlobeViewHomeButton, "VO * View Home WindowID " + winid);
				}
				else if(src == this.m_gc.m_GlobeViewOrientFromTopButton)
				{
					processConnectCommandButton(this.m_gc.m_GlobeViewOrientFromTopButton, "VO * View Top WindowID " + winid);
				}
				else if(src == this.m_gc.m_GlobeViewOrientNorthButton)
				{
					processConnectCommandButton(this.m_gc.m_GlobeViewOrientNorthButton, "VO * View North WindowID " + winid);
				}
				else if(src == this.m_gc.m_GlobeViewZoomInButton)
				{
					processConnectCommandButton(this.m_gc.m_GlobeViewZoomInButton, "Window3D * InpDevMode Mode RubberBandViewLLA WindowID " + winid);
				}
				else if(src == this.m_gc.m_StoredViewPreviousButton)
				{
					String currentSelectedView = this.m_gc.getPreviousStoredView();
					
					if(currentSelectedView != null)
					{
						String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
						processConnectCommandButton(this.m_gc.m_StoredViewPreviousButton, connectCommand);
					}
				}
				else if(src == this.m_gc.m_StoredViewNextButton)
				{
					String currentSelectedView = this.m_gc.getNextStoredView();
					
					if(currentSelectedView != null)
					{
						String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
						processConnectCommandButton(this.m_gc.m_StoredViewNextButton, connectCommand);
					}
				}
				else if(src == this.m_gc.m_StoredViewViewButton)
				{
					String currentSelectedView = this.m_gc.getSelectedStoredView();
					
					if(currentSelectedView != null)
					{
						String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
						processConnectCommandButton(this.m_gc.m_StoredViewViewButton, connectCommand);
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
			        
			        timeline.play();
				}
				catch(Throwable t)
				{
					t.printStackTrace();
				}
			}
		});
		Main.this.m_AgSTKXApplicationClass.executeCommand(cnctCmd);
	}

	private void newScenario(String name)
	{
		try
		{
			m_AgSTKXApplicationClass.executeCommand("Unload / *");
			m_AgSTKXApplicationClass.executeCommand("New / Scenario " + name);
			m_AgSTKXApplicationClass.executeCommand("New / */Facility fac1");
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
		agi.stkx.IAgExecCmdResult res = null;
		res = this.m_AgSTKXApplicationClass.executeCommand("GetDirectory / STKHome");
		String homeDir = (String)res.getItem(0);
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
					this.m_AgSTKXApplicationClass.executeCommand("Unload / *");
					this.m_AgSTKXApplicationClass.executeCommand("Load / " + filePath);
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
			m_AgSTKXApplicationClass.executeCommand("Unload / *");
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
				Main.this.m_gvc.getGlobe().dispose();

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

/* package */final class GraphicsViewComposite
extends Composite
{
	private AgGlobeCntrlClass	m_AgGlobeCntrl;

	/* package */GraphicsViewComposite(Composite parent)
	throws AgCoreException
	{
		super(parent, SWT.NONE);

		this.setLayout(new FillLayout(SWT.HORIZONTAL));

		this.m_AgGlobeCntrl = new AgGlobeCntrlClass(this, SWT.NONE);
		this.m_AgGlobeCntrl.setBackColor(0x00000000);
	}

	/* package */AgGlobeCntrlClass getGlobe()
	{
		return this.m_AgGlobeCntrl;
	}
}