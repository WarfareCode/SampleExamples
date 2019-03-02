// Java API
import java.util.logging.*;
import java.io.*;
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
import agi.stkengine.*;

// CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements ActionListener
{
	private static final long		serialVersionUID			= 1L;

	private final static String		s_TITLE						= "CustomApp_AWT_STK_X_Globe_DragAndDrop";
	private final static String		s_DESCFILENAME				= "AppDescription.html";

	private final static String		s_DROP_MODE					= "Drop Mode";
	private final static String		s_AUTOMATIC					= "Automatic";
	private final static String		s_MANUAL					= "Manual";
	private final static String		s_NONE						= "None";

	private final static String		s_CONNECT_FILE_EXTENSION	= "connect";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;

	private GlobeEventsAdapter		m_GlobeEventsAdapter;

	private JCheckBoxMenuItem		m_AutoMenuItem;
	private JCheckBoxMenuItem		m_ManualMenuItem;
	private JCheckBoxMenuItem		m_NoneMenuItem;

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

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
		{
			String msg = "You do not have the required STK Globe license.  Exiting!";
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
		this.m_GlobeEventsAdapter = new GlobeEventsAdapter();
		this.m_AgGlobeCntrlClass.addIAgGlobeCntrlEvents(this.m_GlobeEventsAdapter);
		this.getContentPane().add(this.m_AgGlobeCntrlClass, BorderLayout.CENTER);

		//remove unwanted sample menu bar
		JMenu scenarioJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getScenarioJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(scenarioJMenu);
		JMenu vdfJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getVDFJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(vdfJMenu);

		// ====================
		// Drop Mode Menu
		// ====================
		JMenu submenu = new JMenu(s_DROP_MODE);
		this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu().add(submenu);

		this.m_AutoMenuItem = new JCheckBoxMenuItem();
		this.m_AutoMenuItem.setText(s_AUTOMATIC);
		this.m_AutoMenuItem.addActionListener(this);
		submenu.add(this.m_AutoMenuItem);

		this.m_ManualMenuItem = new JCheckBoxMenuItem(s_MANUAL);
		this.m_ManualMenuItem.setText(s_MANUAL);
		this.m_ManualMenuItem.addActionListener(this);
		submenu.add(this.m_ManualMenuItem);

		this.m_NoneMenuItem = new JCheckBoxMenuItem(s_NONE);
		this.m_NoneMenuItem.setText(s_NONE);
		this.m_NoneMenuItem.addActionListener(this);
		submenu.add(this.m_NoneMenuItem);
		
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDragDropMode(AgEOLEDropMode.E_MANUAL);
		
		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	public void actionPerformed(ActionEvent event)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			String command = event.getActionCommand();

			if(command.equals(s_AUTOMATIC))
			{
				this.setDragDropMode(AgEOLEDropMode.E_AUTOMATIC);
			}
			else if(command.equals(s_MANUAL))
			{
				this.setDragDropMode(AgEOLEDropMode.E_MANUAL);
			}
			else if(command.equals(s_NONE))
			{
				this.setDragDropMode(AgEOLEDropMode.E_NONE);
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

	private class GlobeEventsAdapter
	implements IAgGlobeCntrlEvents
	{

		public void onAgGlobeCntrlEvent(AgGlobeCntrlEvent e)
		{
			try
			{
				int type = e.getType();

				Object[] params = e.getParams();

				if(type == AgGlobeCntrlEvent.TYPE_O_L_E_DRAG_DROP)
				{
					IAgDataObject data = (IAgDataObject)params[0];
					//int effect = ((Integer)params[1]).intValue();
					//short button = ((Short)params[1]).shortValue();
					//short shift = ((Short)params[1]).shortValue();
					//int x = ((Integer)params[2]).intValue();
					//int y = ((Integer)params[3]).intValue();

					IAgDataObjectFiles files = data.getFiles();

					for(int i = 0; i < files.getCount(); ++i)
					{
						String filePath = files.getItem(i);

						processFile(filePath);
					}
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}

		private void processFile(String fileName)
		{
			if(fileName.endsWith(s_CONNECT_FILE_EXTENSION))
			{
				this.processConnectFile(fileName);
			}
		}

		private void processConnectFile(String fileName)
		{
			try
			{
				File file = new File(fileName);
				FileReader fr = new FileReader(file);
				BufferedReader br = new BufferedReader(fr);

				String line;

				while((line = br.readLine()) != null)
				{
					// Even if you're not going to use the IAgExecCmdResult, its
					// better to have a non-anonymous instance rather than anonymous.
					// I.e Have a local object for the result and then set it to be
					// null, rather than having no return value object.
					agi.stkutil.IAgExecCmdResult res = MainWindow.this.m_AgStkObjectRootClass.executeCommand(line);
					if(!res.getIsSucceeded())
					{
						br.close();
						throw new Exception("Command did not succeed");
					}
				}
				
				br.close();
			}
			catch(Throwable t)
			{
				System.out.println("Could process connect file => " + fileName);
				t.printStackTrace();
			}
		}
	}

	public void setDragDropMode(AgEOLEDropMode mode)
	throws Exception
	{
		this.m_AgGlobeCntrlClass.setOLEDropMode(mode);

		if(mode == AgEOLEDropMode.E_MANUAL)
		{
			this.m_ManualMenuItem.setState(true);
			this.m_AutoMenuItem.setState(false);
			this.m_NoneMenuItem.setState(false);
		}
		else if(mode == AgEOLEDropMode.E_AUTOMATIC)
		{
			this.m_ManualMenuItem.setState(false);
			this.m_AutoMenuItem.setState(true);
			this.m_NoneMenuItem.setState(false);
		}
		else if(mode == AgEOLEDropMode.E_NONE)
		{
			this.m_ManualMenuItem.setState(false);
			this.m_AutoMenuItem.setState(false);
			this.m_NoneMenuItem.setState(true);
		}
	}

	public class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				MainWindow.this.m_AgGlobeCntrlClass.removeIAgGlobeCntrlEvents(MainWindow.this.m_GlobeEventsAdapter);

				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();

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