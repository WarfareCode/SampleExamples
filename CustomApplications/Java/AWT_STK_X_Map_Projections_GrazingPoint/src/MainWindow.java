// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.*;
import agi.core.logging.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.stkengine.*;

//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
{
	private static final long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Map_Projections_GrazingPoint";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgMapCntrlClass			m_AgMapCntrlClass;
	private MapEventsAdapter		m_MapEventsAdapter;

	private IAgPosition				m_IAgPosition;
	private TestsJPanel				m_TestsJPanel;

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
		this.setApp(this.m_AgSTKXApplicationClass);
		
		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}

		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();
		this.setRoot(this.m_AgStkObjectRootClass);

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgMapCntrlClass = new AgMapCntrlClass();
		this.m_AgMapCntrlClass.setBackColor(stkxColor);
		this.m_AgMapCntrlClass.setBackground(awtColor);
		this.m_MapEventsAdapter = new MapEventsAdapter();
		this.m_AgMapCntrlClass.addIAgMapCntrlEvents(this.m_MapEventsAdapter);
		this.getContentPane().add(this.m_AgMapCntrlClass, BorderLayout.CENTER);

		this.m_TestsJPanel = new TestsJPanel();
		this.m_TestsJPanel.setRoot(this.m_AgStkObjectRootClass);
		this.getContentPane().add(this.m_TestsJPanel, BorderLayout.EAST);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	void createTargetIfNotExist()
	throws AgCoreException
	{
		IAgStkObject scenObj = this.m_AgStkObjectRootClass.getCurrentScenario();
		IAgStkObjectCollection children = scenObj.getChildren();

		IAgStkObject obj = null;

		try
		{
			obj = children.getItem("GrazePoint");
		}
		catch(AgCoreException e)
		{
			obj = null;
		}

		if(obj == null)
		{
			IAgStkObject targobj = children._new(AgESTKObjectType.E_TARGET, "GrazePoint");
			IAgTarget targ = new AgTarget(targobj);
			this.m_IAgPosition = targ.getPosition();
			IAgGeodetic targgeopos = new AgGeodetic(this.m_IAgPosition.convertTo(AgEPositionType.E_GEODETIC));
			targgeopos.setAlt(0.0);
			targgeopos.setLat(new Double(0.0));
			targgeopos.setLon(new Double(0.0));
			this.m_IAgPosition.assign(targgeopos);
		}
	}

	private class MapEventsAdapter
	implements IAgMapCntrlEvents
	{
		public void onAgMapCntrlEvent(AgMapCntrlEvent evt)
		{
			try
			{
				int type = evt.getType();
				Object[] params = evt.getParams();

				if(type == AgMapCntrlEvent.TYPE_MOUSE_MOVE)
				{
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();

					// System.out.println("x="+x+ ", y="+y);
					MainWindow.this.m_TestsJPanel.setTargetPoint(x, y);
				}
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
				MainWindow.this.m_AgMapCntrlClass.removeIAgMapCntrlEvents(MainWindow.this.m_MapEventsAdapter);

				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgMapCntrlClass.dispose();

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

	class TestsJPanel
	extends JPanel
	implements ActionListener
	{
		private static final long	serialVersionUID	= 1L;

		private IAgStkObjectRoot	m_IAgStkObjectRoot;

		private JButton				m_GetPixelsJButton;
		private JButton				m_GrazingPointJButton;
		private boolean				m_GrazingPointToggle;

		public TestsJPanel()
		throws Exception
		{
			this.initialize();
		}

		private void initialize()
		throws Exception
		{
			this.setLayout(new GridLayout(2,1));

			this.m_GetPixelsJButton = new JButton();
			this.m_GetPixelsJButton.setText("Get Pixels Test");
			this.m_GetPixelsJButton.setActionCommand("Get Pixels Test");
			this.m_GetPixelsJButton.addActionListener(this);
			this.add(this.m_GetPixelsJButton);

			this.m_GrazingPointJButton = new JButton();
			this.m_GrazingPointJButton.setText("Grazing Point Test");
			this.m_GrazingPointJButton.setActionCommand("Grazing Point Test");
			this.m_GrazingPointJButton.addActionListener(this);
			this.add(this.m_GrazingPointJButton);

			this.m_GrazingPointToggle = false;
		}

		protected void finalize()
		{
			this.m_IAgStkObjectRoot = null;
		}

		public void setRoot(IAgStkObjectRoot root)
		{
			this.m_IAgStkObjectRoot = root;
		}

		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

				checkStkInitialized();
				
				if(MainWindow.this.m_AgStkObjectRootClass.getCurrentScenario() != null)
				{
					String cmdText = ae.getActionCommand();
	
					if(cmdText.equalsIgnoreCase("Get Pixels Test"))
					{
						this.executeCommand("window2d_r * ProjectedPos Lat 45.0 lon -135.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 80.0 lon -170.0");
	
						this.executeCommand("window2d_r * ProjectedPos Lat -90.0 lon 0.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 0.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 90.0 lon 0.0");
	
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon -180.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon -179.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon -90.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 90.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 179.99");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 180.0");
	
						this.executeCommand("window2d_r * ProjectedPos Lat 90.0 lon -180.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 90.0 lon -179.99");
						this.executeCommand("window2d_r * ProjectedPos Lat 45.0 lon -90.0");
						this.executeCommand("window2d_r * ProjectedPos Lat -45.0 lon 90.0");
						this.executeCommand("window2d_r * ProjectedPos Lat -90.0 lon 179.0");
						this.executeCommand("window2d_r * ProjectedPos Lat -90.0 lon 180.0");
	
						this.executeCommand("MapProjection * EquiCylindrical Center 45.0");
	
						this.executeCommand("window2d_r * ProjectedPos Lat 45.0 lon -135.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 80.0 lon -170.0");
	
						this.executeCommand("window2d_r * ProjectedPos Lat -90.0 lon 0.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 0.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 90.0 lon 0.0");
	
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon -180.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon -179.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon -90.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 90.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 179.99");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 180.0");
	
						this.executeCommand("window2d_r * ProjectedPos Lat 90.0 lon -180.0");
						this.executeCommand("window2d_r * ProjectedPos Lat 90.0 lon -179.99");
						this.executeCommand("window2d_r * ProjectedPos Lat 45.0 lon -90.0");
						this.executeCommand("window2d_r * ProjectedPos Lat -45.0 lon 90.0");
						this.executeCommand("window2d_r * ProjectedPos Lat -90.0 lon 179.0");
						this.executeCommand("window2d_r * ProjectedPos Lat -90.0 lon 180.0");
	
						this.executeCommand("MapProjection * Perspective Center 45.0 54.0 1000000");
	
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon -180.0 alt 1000000");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon -90.0 alt 1000000");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 90.0 alt 1000000");
						this.executeCommand("window2d_r * ProjectedPos Lat 0.0 lon 180.0 alt 1000000");
	
					}
					else if(cmdText.equalsIgnoreCase("Grazing Point Test"))
					{
						this.m_GrazingPointToggle = !this.m_GrazingPointToggle;
						// System.out.println( "Grazing Point Toggle set to " + this.m_GrazingPointToggle );
	
						if(this.m_GrazingPointToggle)
						{
							this.m_IAgStkObjectRoot.getUnitPreferences().setCurrentUnit("Distance", "m");
							this.executeCommand("Graphics * BackgroundImage Show Off");
							this.executeCommand("MapProjection * Perspective Center 0.0 0.0 15000000");
						}
						else
						{
							this.executeCommand("MapProjection * EquiCylindrical Center 0.0");
							this.executeCommand("Graphics * BackgroundImage Show On ImageFile \"Basic.bmp\"");
							this.m_IAgStkObjectRoot.getUnitPreferences().setCurrentUnit("Distance", "km");
						}
					}
				}
				else
				{
					throw new AgException("An Scenario must be created, or opened first before using this feature.");
				}
			}
			catch(Throwable t)
			{
				JOptionPane.showMessageDialog(MainWindow.this, t.getMessage());
			}
			finally
			{
				((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
			}
		}

		public void setTargetPoint(int x, int y)
		{
			try
			{
				double lat = 0.0;
				double lon = 0.0;
				//double alt = 0.0;
				boolean validLLA = false;
				String returnData = null;

				if(this.m_GrazingPointToggle)
				{
					returnData = this.executeCommand("window2d_r * PerspectiveGrazingPtFromXY x " + x + " y " + y);

					if(returnData != null)
					{
						String[] LLA = returnData.split(" ");

						lat = Double.parseDouble(LLA[0]);
						lon = Double.parseDouble(LLA[1]);
						//alt = Double.parseDouble(LLA[2]);
						validLLA = true;
					}
				}

				if(returnData == null)
				{
					IAgPickInfoData info = MainWindow.this.m_AgMapCntrlClass.pickInfo(x, y);
					lat = info.getLat();
					lon = info.getLon();
					//alt = info.getAlt();
					validLLA = info.getIsLatLonAltValid();
				}

				if(validLLA)
				{
					// System.out.println( "Setting Facility Position to Lat="+lat+", Lon="+lon+", Alt="+alt);

					createTargetIfNotExist();

					IAgGeodetic targgeopos = new AgGeodetic(MainWindow.this.m_IAgPosition.convertTo(AgEPositionType.E_GEODETIC));
					targgeopos.setLat(new Double(lat));
					targgeopos.setLon(new Double(lon));
					targgeopos.setAlt(0.0/*alt*/);
					//System.out.println("lat="+lat+", lon="+lon+" alt="+alt);
					MainWindow.this.m_IAgPosition.assign(targgeopos);
				}
			}
			catch(AgCoreException e)
			{
				// System.out.println( "Failed to set GrazePoint LLA");
			}
		}

		public String executeCommand(String cmd)
		{
			String returnData = null;
			try
			{
				if(this.m_IAgStkObjectRoot != null && cmd != null && !cmd.equalsIgnoreCase(""))
				{
					agi.stkutil.IAgExecCmdResult result = this.m_IAgStkObjectRoot.executeCommand(cmd);

					if(result != null)
					{
						int count = result.getCount();

						for(int i = 0; i < count; i++)
						{
							returnData = result.getItem(i);
						}
					}
				}
			}
			catch(Throwable t)
			{
				// t.printStackTrace();
			}

			return returnData;
		}
	}
}