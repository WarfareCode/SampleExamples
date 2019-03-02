// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

// AGI Java API
// TODO: #4 Add some STK Java API imports for agi logging, 
// AWT core, AWT STK X controls, and STK Engine Custom Application 
// initialization/uninitialization
import agi.core.logging.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkengine.*;

public class MainWindow
//TODO: #1 Derive the MainWindow class from a JFrame to create a Window container 
//that will host the AGI Globe and Map controls
extends JFrame
implements ActionListener
{
	private final static long		serialVersionUID	= 1L;
	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Tutorial_End";

	// TODO: #5 Declare a private STK X Application Class data member variable called m_AgSTKXApplicationClass
	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;

	// TODO: #6 Declare private STK Globe Cntrl class, use variable name of m_AgGlobeCntrlClass
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;

	// TODO: #7 Declare private STK Map Cntrl class, use variable name of m_AgMapCntrlClass
	private AgMapCntrlClass			m_AgMapCntrlClass;

	private JButton 				m_NewScenarioJButton;
	private JButton 				m_CloseScenarioJButton;
	private JButton 				m_MapZoomInJButton;
	private JButton 				m_MapZoomOutJButton;
	private JLabel					m_EventsJLabel;

	public MainWindow()
	throws Throwable
	{
		super();

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
		// Initialize the STK Engine Java application with the AgAwt_JNI initialization technique for STKX Custom Applications.
		// If you don't, none of the Java api calls will be successful.

		// TODO: #8 initialize the AWT Delegate
		AgAwt_JNI.initialize_AwtDelegate();

		// TODO: #9 initialize the JNI Custom Application with a parameter of true for auto class casting
		AgStkCustomApplication_JNI.initialize(true); // true parameter allows for smart auto class cast
		
		// TODO: #10 initialize the AWT Components
        AgAwt_JNI.initialize_AwtComponents();

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);

		// TODO: #11 Define the STK X Application data member
		this.m_AgSTKXApplicationClass = new AgSTKXApplicationClass();

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
	
		JPanel centerJPanel = new JPanel();
		centerJPanel.setLayout(new GridLayout(1,2));
		this.getContentPane().add(centerJPanel, BorderLayout.CENTER);

		// TODO: #12 Define the STK Globe Cntrl using m_AgGlobeCntrlClass variable
		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		
		// TODO: #13 Assign the m_AgGlobeCntrlClass variable for the Globe Cntrl as a component of the centerJPanel.
		centerJPanel.add(this.m_AgGlobeCntrlClass);

		// TODO: #14 Define the STK Map Cntrl using m_AgMapCntrlClass variable
		this.m_AgMapCntrlClass = new AgMapCntrlClass();
		
        // TODO: #15 Assign the m_AgMapCntrlClass variable for the Map Cntrl as a component of the frame's content pane., using a center borderlayout.
		centerJPanel.add(this.m_AgMapCntrlClass);

		JPanel westJPanel = new JPanel();
		westJPanel.setLayout(new GridLayout(2,1));
		this.getContentPane().add(westJPanel, BorderLayout.WEST);
		
		this.m_NewScenarioJButton = new JButton();
		this.m_NewScenarioJButton.setText("New Scenario");
		this.m_NewScenarioJButton.addActionListener(this);
		westJPanel.add(this.m_NewScenarioJButton);
		
		this.m_CloseScenarioJButton = new JButton();
		this.m_CloseScenarioJButton.setText("Close Scenario");
		this.m_CloseScenarioJButton.addActionListener(this);
		westJPanel.add(this.m_CloseScenarioJButton);

		JPanel eastJPanel = new JPanel();
		eastJPanel.setLayout(new GridLayout(2,1));
		this.getContentPane().add(eastJPanel, BorderLayout.EAST);
		
		this.m_MapZoomInJButton = new JButton();
		this.m_MapZoomInJButton.setText("Zoom In");
		this.m_MapZoomInJButton.addActionListener(this);
		eastJPanel.add(this.m_MapZoomInJButton);
		
		this.m_MapZoomOutJButton = new JButton();
		this.m_MapZoomOutJButton.setText("Zoom Out");
		this.m_MapZoomOutJButton.addActionListener(this);
		eastJPanel.add(this.m_MapZoomOutJButton);
		
		this.m_EventsJLabel = new JLabel();
		this.m_EventsJLabel.setText("");
		this.getContentPane().add(this.m_EventsJLabel, BorderLayout.SOUTH);
		
		// TODO: #3 Set the default close operation of the JFrame to EXIT_ON_CLOSE and add a
		// window listener.
		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
		
		// TODO: #24 Add an Event listener to the AgSTKXApplicationClass data member instance
		// and receive and report only New Scenario and Close Scenario events.
		this.m_AgSTKXApplicationClass.addIAgSTKXApplicationEvents2(
		new IAgSTKXApplicationEvents2()
		{
			public void onAgSTKXApplicationEvent(AgSTKXApplicationEvent e) 
			{
				// Catch all exceptions from events, it is poor java coding
				// to pass the exception back to the event dispatcher (JNI),
				// it can cause JVM crashes.
				try
				{
					int type = e.getType();
					if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_NEW)
					{
						String path = (String)e.getParams()[0];
						MainWindow.this.m_EventsJLabel.setText("Scenario created: "+ path);
					}
					else if(type == AgSTKXApplicationEvent.TYPE_ON_SCENARIO_CLOSE)
					{
						MainWindow.this.m_EventsJLabel.setText("Scenario closed");
					}
				}
				catch(Throwable t)
				{
					JOptionPane.showMessageDialog(MainWindow.this, t.getMessage());
				}
			}});		

		// TODO: #25 Add an Event listener to the AgGlobeCntrlClass data member instance
		// and receive only Mouse Move events to determine the "picked" Latitude, Longitude, and Altitude.
		this.m_AgGlobeCntrlClass.addIAgGlobeCntrlEvents(
		new IAgGlobeCntrlEvents()
		{
			public void onAgGlobeCntrlEvent(AgGlobeCntrlEvent e) 
			{
				// Catch all exceptions from events, it is poor java coding
				// to pass the exception back to the event dispatcher (JNI),
				// it can cause JVM crashes.
				try
				{
					int type = e.getType();
					if(type == AgGlobeCntrlEvent.TYPE_MOUSE_MOVE)
					{
						Object[] params = e.getParams();

						Integer x = (Integer)params[2];
						Integer y = (Integer)params[3];

						int xi = x.intValue();
						int yi = y.intValue();
						
						IAgPickInfoData pickInfoData = null;
						pickInfoData = MainWindow.this.m_AgGlobeCntrlClass.pickInfo(xi, yi);
						
						if(pickInfoData.getIsLatLonAltValid())
						{
							double lat = pickInfoData.getLat();
							double lon = pickInfoData.getLon();
							double alt = pickInfoData.getAlt();
							String text = "Globe LLA: "+lat+", "+lon+", "+alt;
							MainWindow.this.m_EventsJLabel.setText(text);
						}
					}
				}
				catch(Throwable t)
				{
					JOptionPane.showMessageDialog(MainWindow.this, t.getMessage());
				}
			}});

		// TODO: #26 Add an Event listener to the AgMapCntrlClass data member instance
		// and receive only Mouse Move events to determine the "picked" Latitude, Longitude, and Altitude.
		this.m_AgMapCntrlClass.addIAgMapCntrlEvents(
		new IAgMapCntrlEvents()
		{
			public void onAgMapCntrlEvent(AgMapCntrlEvent e) 
			{
				// Catch all exceptions from events, it is poor java coding
				// to pass the exception back to the event dispatcher (JNI),
				// it can cause JVM crashes.
				try
				{
					int type = e.getType();
					if(type == AgMapCntrlEvent.TYPE_MOUSE_MOVE)
					{
						Object[] params = e.getParams();

						Integer x = (Integer)params[2];
						Integer y = (Integer)params[3];

						int xi = x.intValue();
						int yi = y.intValue();

						IAgPickInfoData pickInfoData = null;
						pickInfoData = MainWindow.this.m_AgMapCntrlClass.pickInfo(xi, yi);
						
						if(pickInfoData.getIsLatLonAltValid())
						{
							double lat = pickInfoData.getLat();
							double lon = pickInfoData.getLon();
							double alt = pickInfoData.getAlt();
							String text = "Map LLA: "+lat+", "+lon+", "+alt;
							MainWindow.this.m_EventsJLabel.setText(text);
						}
					}
				}
				catch(Throwable t)
				{
					JOptionPane.showMessageDialog(MainWindow.this, t.getMessage());
				}
			}});
	}

	public void actionPerformed(ActionEvent event)
	{
		try
		{
			((Component)this).setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));

			Object src = event.getSource();

			if(src.equals(this.m_NewScenarioJButton))
			{
				// TODO: #16 Issue the "New Scenario" Connect command on the executeCommand 
				// method of the AgSTKXApplicationClass data member instance.  Refer to
				// the STK Connect Command Windows help file for help on the Connect syntax.
				this.m_AgSTKXApplicationClass.executeCommand("New / Scenario anyname");
			}
			else if(src.equals(this.m_CloseScenarioJButton))
			{
				// TODO: #17 Issue the "Unload/close Scenario" Connect command on the executeCommand 
				// method of the AgSTKXApplicationClass data member instance.  Refer to
				// the STK Connect Command Windows help file for help on the Connect syntax.
				this.m_AgSTKXApplicationClass.executeCommand("Unload / *");
			}
			else if(src.equals(this.m_MapZoomInJButton))
			{
				// TODO: #18 Issue the "zoom in" method call on the AgMapCntrlClass data member instance.  Refer to
				// the STK Java API javadocs or Eclipse intellisense if you have properly configured your samples
				// via Eclipse User Libraries that are highlighted in the STK Java API documentation in the Integration
				// Developer Kit windows help file.
				this.m_AgMapCntrlClass.zoomIn();
			}
			else if(src.equals(this.m_MapZoomOutJButton))
			{
				// TODO: #19 Issue the "zoom out" method call on the AgMapCntrlClass data member instance.  Refer to
				// the STK Java API javadocs or Eclipse intellisense if you have properly configured your samples
				// via Eclipse User Libraries that are highlighted in the STK Java API documentation in the Integration
				// Developer Kit windows help file.
				this.m_AgMapCntrlClass.zoomOut();
			}
		}
		catch(AgCoreException sce)
		{
			System.out.println();
			System.out.println("Description = " + sce.getDescription());
			System.out.println("HRESULT hr = 0x" + sce.getHResultAsHexString());
			sce.printStackTrace();
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
		finally
		{
			((Component)this).setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
		}
	}

	private class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
        		// TODO: #20 Since we used the EXIT_ON_CLOSE window close operation, we need to 
        		// dispose of our Globe/Map Control before uninitializing the Java API.  
        		// Call the dispose method on the m_AgGlobeCntrlClass and m_AgMapCntrlClass
				// You MUST dispose your control before uninitializing the API or a hang may occur
				MainWindow.this.m_AgGlobeCntrlClass.dispose();
				MainWindow.this.m_AgMapCntrlClass.dispose();

        		// TODO: #21 Uninitialize the STK Engine Java AWT Components.
				AgAwt_JNI.uninitialize_AwtComponents();

        		// TODO: #22 Uninitialize the STK Engine Java JNI interface.
				AgStkCustomApplication_JNI.uninitialize();

        		// TODO: #23 Uninitialize the STK Engine Java AWT Delegate.
				AgAwt_JNI.uninitialize_AwtDelegate();
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}
}