// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.border.*;
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
import agi.stk.core.swing.menus.map.view.*;
import agi.stk.core.swing.toolbars.map.view.*;
import agi.stkengine.*;
//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements ActionListener, IAgMapViewJToolBarEventsListener, IAgMapViewJMenuEventsListener
{
	private static final long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Map_Projections";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;

	private AgMapCntrlClass			m_AgMapCntrlClass;
	private AgMapViewJMenu			m_AgMapViewJMenu;
	private AgMapViewJToolBar		m_AgMapViewJToolBar;
	private boolean					m_AllowPan;

	private Projections_JPanel		m_Projections_JPanel;

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
		this.getContentPane().add(this.m_AgMapCntrlClass, BorderLayout.CENTER);

		this.m_Projections_JPanel = new Projections_JPanel();
		this.m_Projections_JPanel.addActionListener(this);
		this.getContentPane().add(this.m_Projections_JPanel, BorderLayout.EAST);

		this.m_AgMapViewJMenu = new AgMapViewJMenu();
		this.m_AgMapViewJMenu.addMapViewJMenuListener(this);
		super.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu().add(this.m_AgMapViewJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.m_AgMapViewJToolBar = new AgMapViewJToolBar();
		this.m_AgMapViewJToolBar.addMapViewJToolBarListener(this);
		this.getContentPane().add(this.m_AgMapViewJToolBar, BorderLayout.NORTH);

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
				boolean selected = this.allowPan();
				this.m_AgMapViewJToolBar.getAllowPanJButton().setSelected(selected);
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
				boolean selected = this.allowPan();
				this.m_AgMapViewJMenu.getAllowPanJMenuItem().setSelected(selected);
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

	/* package */boolean allowPan()
	throws AgCoreException
	{
		this.m_AllowPan = !this.m_AllowPan;
		if(this.m_AllowPan)
		{
			this.m_AgSTKXApplicationClass.executeCommand("Window2d * InpDevMode EnablePickMode Off");
			this.m_AgSTKXApplicationClass.executeCommand("Window2d * InpDevMode EnablePanMode On");
		}
		else
		{
			this.m_AgSTKXApplicationClass.executeCommand("Window2d * InpDevMode EnablePanMode Off");
			this.m_AgSTKXApplicationClass.executeCommand("Window2d * InpDevMode EnablePickMode On");
		}
		return this.m_AllowPan;
	}

	public void actionPerformed(ActionEvent ae)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			if(ae.getActionCommand().equalsIgnoreCase(Projections_JPanel.s_CHANGE_PROJECTION_TEXT))
			{
				this.changeProjection();
			}
		}
		catch(InvalidRangeException late)
		{
			JOptionPane.showMessageDialog(this, late.getMessage(), "Invalid Range Exception", JOptionPane.ERROR_MESSAGE);
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

	private void changeProjection()
	throws Throwable
	{
		String projection = this.m_Projections_JPanel.getProjPanel().getProjection();
		String lat = this.m_Projections_JPanel.getLatPanel().getLat();
		String lon = this.m_Projections_JPanel.getLonPanel().getLon();
		String alt = this.m_Projections_JPanel.getAltPanel().getAlt();

		Projections_JPanel.ProjOption_JPanel opt = this.m_Projections_JPanel.getOptionPanel();

		String coordFrame = opt.getCoordFrame();
		String dispHeight = opt.getDispHeight();
		String fov = opt.getFOV();

		StringBuffer sb = new StringBuffer();

		sb.append("MapProjection");
		sb.append(" ");
		sb.append("*");
		sb.append(" ");
		sb.append(projection);
		sb.append(" ");

		if(projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_EQUI_CYLINDRICAL_TEXT) || projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_MERCATOR_TEXT)
		|| projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_MILLER_TEXT) || projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_MOLLWEIDE_TEXT)
		|| projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_SINUSOIDAL_TEXT) || projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_HAMMERAITOFF_TEXT))
		{
			checkLonRange(lon);

			sb.append("Center");
			sb.append(" ");
			sb.append(lon);
		}
		else if(projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_AZIMUTH_EQUI_TEXT) || projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_STEREOGRAPHIC_TEXT))
		{
			checkLonRange(lon);
			checkLatRange(lat);

			sb.append("Center");
			sb.append(" ");
			sb.append(lat);
			sb.append(" ");
			sb.append(lon);
		}
		else if(projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_ORTHOGRAPHIC_TEXT))
		{
			String option = opt.getOption();

			if(option.equalsIgnoreCase(Projections_JPanel.ProjOption_JPanel.s_OPTION_CENTER_TEXT))
			{
				checkLonRange(lon);
				checkLatRange(lat);

				sb.append("Center");
				sb.append(" ");
				sb.append(lat);
				sb.append(" ");
				sb.append(lon);
			}
			else if(option.equalsIgnoreCase(Projections_JPanel.ProjOption_JPanel.s_OPTION_FORMAT_TEXT))
			{
				checkDispHeight(dispHeight);

				sb.append("Format");
				sb.append(" ");
				sb.append(coordFrame);
				sb.append(" ");
				sb.append(dispHeight);
			}
		}
		else if(projection.equalsIgnoreCase(Projections_JPanel.s_PROJ_PERSPECTIVE_TEXT))
		{
			String option = opt.getOption();

			if(option.equalsIgnoreCase(Projections_JPanel.ProjOption_JPanel.s_OPTION_CENTER_TEXT))
			{
				checkLonRange(lon);
				checkLatRange(lat);
				checkAltRange(alt);

				sb.append("Center");
				sb.append(" ");
				sb.append(lat);
				sb.append(" ");
				sb.append(lon);
				sb.append(" ");
				sb.append(alt);
			}
			else if(option.equalsIgnoreCase(Projections_JPanel.ProjOption_JPanel.s_OPTION_FORMAT_TEXT))
			{
				checkFovRange(fov);

				sb.append("Format");
				sb.append(" ");
				sb.append(coordFrame);
				sb.append(" ");
				sb.append(fov);
			}
		}

		System.out.println(sb.toString());
		this.m_AgStkObjectRootClass.executeCommand(sb.toString());
	}

	private void checkLonRange(String lon)
	throws Throwable
	{
		double l = Double.parseDouble(lon);

		if(l < -180 || l > 360)
		{
			throw new InvalidLonRangeException(l);
		}
	}

	private void checkLatRange(String lat)
	throws Throwable
	{
		double l = Double.parseDouble(lat);

		if(l < -90 || l > 90)
		{
			throw new InvalidLatRangeException(l);
		}
	}

	private void checkAltRange(String alt)
	throws Throwable
	{
		double a = Double.parseDouble(alt);

		if(a < 1)
		{
			throw new InvalidAltRangeException(a);
		}
	}

	private void checkDispHeight(String height)
	throws Throwable
	{
		double h = Double.parseDouble(height);

		if(h < 1)
		{
			throw new InvalidHeightRangeException(h);
		}
	}

	private void checkFovRange(String fov)
	throws Throwable
	{
		double f = Double.parseDouble(fov);

		if(f < 1 || f > 180)
		{
			throw new InvalidFovRangeException(f);
		}
	}

	private class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
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

	class Projections_JPanel
	extends JPanel
	implements ItemListener
	{
		private static final long	serialVersionUID				= 1L;

		public final static String	s_CHANGE_PROJECTION_TEXT		= "Change Projection";

		// Center <LonCenter>
		public final static String	s_PROJ_EQUI_CYLINDRICAL_TEXT	= "EquiCylindrical";
		public final static String	s_PROJ_MERCATOR_TEXT			= "Mercator";
		public final static String	s_PROJ_MILLER_TEXT				= "Miller";
		public final static String	s_PROJ_MOLLWEIDE_TEXT			= "Mollweide";
		public final static String	s_PROJ_SINUSOIDAL_TEXT			= "Sinusoidal";

		// Center <LatCenter>
		public final static String	s_PROJ_HAMMERAITOFF_TEXT		= "HammerAitoff";

		// Center <LatCenter> <LonCenter>
		public final static String	s_PROJ_AZIMUTH_EQUI_TEXT		= "AzimuthEqui";
		public final static String	s_PROJ_STEREOGRAPHIC_TEXT		= "Stereographic";

		// {ProjOption} <Parameters>
		public final static String	s_PROJ_ORTHOGRAPHIC_TEXT		= "Orthographic";
		public final static String	s_PROJ_PERSPECTIVE_TEXT			= "Perspective";

		private Proj_JPanel			m_Projection_JPanel;
		private Lon_JPanel			m_Lon_JPanel;
		private Lat_JPanel			m_Lat_JPanel;
		private Alt_JPanel			m_Alt_JPanel;
		private ProjOption_JPanel	m_ProjOption_JPanel;

		private JButton				m_ChangeProj_JButton;

		public Projections_JPanel()
		{
			this.initialize();
		}

		private void initialize()
		{
			this.setLayout(new BoxLayout(this, BoxLayout.PAGE_AXIS));

			Border b1 = BorderFactory.createLoweredBevelBorder();
			Border b2 = BorderFactory.createTitledBorder(b1, "Map Properties", TitledBorder.LEFT, TitledBorder.ABOVE_TOP);
			this.setBorder(b2);

			int panelWidth = 300;
			int panelHeight = 25;

			this.m_Projection_JPanel = new Proj_JPanel();
			this.m_Projection_JPanel.addItemListener(this);
			this.m_Projection_JPanel.setSize(new Dimension(panelWidth, panelHeight));
			this.m_Projection_JPanel.setPreferredSize(new Dimension(panelWidth, panelHeight));
			this.m_Projection_JPanel.setMinimumSize(new Dimension(panelWidth, panelHeight));
			this.m_Projection_JPanel.setMaximumSize(new Dimension(panelWidth, panelHeight));
			this.add(this.m_Projection_JPanel);

			this.add(Box.createVerticalGlue());

			this.m_ProjOption_JPanel = new ProjOption_JPanel();
			this.m_ProjOption_JPanel.addItemListener(this);
			this.m_ProjOption_JPanel.setSize(new Dimension(panelWidth, panelHeight * 4));
			this.m_ProjOption_JPanel.setPreferredSize(new Dimension(panelWidth, panelHeight * 4));
			this.m_ProjOption_JPanel.setMinimumSize(new Dimension(panelWidth, panelHeight * 4));
			this.m_ProjOption_JPanel.setMaximumSize(new Dimension(panelWidth, panelHeight * 4));
			this.add(this.m_ProjOption_JPanel);

			this.add(Box.createVerticalGlue());

			this.m_Lon_JPanel = new Lon_JPanel();
			this.m_Lon_JPanel.setSize(new Dimension(panelWidth, panelHeight));
			this.m_Lon_JPanel.setPreferredSize(new Dimension(panelWidth, panelHeight));
			this.m_Lon_JPanel.setMinimumSize(new Dimension(panelWidth, panelHeight));
			this.m_Lon_JPanel.setMaximumSize(new Dimension(panelWidth, panelHeight));
			this.m_Lon_JPanel.setEnabled(true);
			this.add(this.m_Lon_JPanel);

			this.add(Box.createVerticalGlue());

			this.m_Lat_JPanel = new Lat_JPanel();
			this.m_Lat_JPanel.setSize(new Dimension(panelWidth, panelHeight));
			this.m_Lat_JPanel.setPreferredSize(new Dimension(panelWidth, panelHeight));
			this.m_Lat_JPanel.setMinimumSize(new Dimension(panelWidth, panelHeight));
			this.m_Lat_JPanel.setMaximumSize(new Dimension(panelWidth, panelHeight));
			this.add(this.m_Lat_JPanel);

			this.add(Box.createVerticalGlue());

			this.m_Alt_JPanel = new Alt_JPanel();
			this.m_Alt_JPanel.setSize(new Dimension(panelWidth, panelHeight));
			this.m_Alt_JPanel.setPreferredSize(new Dimension(panelWidth, panelHeight));
			this.m_Alt_JPanel.setMinimumSize(new Dimension(panelWidth, panelHeight));
			this.m_Alt_JPanel.setMaximumSize(new Dimension(panelWidth, panelHeight));
			this.add(this.m_Alt_JPanel);

			this.add(Box.createVerticalGlue());

			JPanel changePanel = new JPanel();
			changePanel.setSize(new Dimension(panelWidth, panelHeight + 10));
			changePanel.setPreferredSize(new Dimension(panelWidth, panelHeight + 10));
			changePanel.setMinimumSize(new Dimension(panelWidth, panelHeight + 10));
			changePanel.setMaximumSize(new Dimension(panelWidth, panelHeight + 10));

			this.m_ChangeProj_JButton = new JButton();
			this.m_ChangeProj_JButton.setText(s_CHANGE_PROJECTION_TEXT);
			this.m_ChangeProj_JButton.setSize(new Dimension(panelWidth, panelHeight));
			this.m_ChangeProj_JButton.setPreferredSize(new Dimension(panelWidth, panelHeight));
			this.m_ChangeProj_JButton.setMinimumSize(new Dimension(panelWidth, panelHeight));
			this.m_ChangeProj_JButton.setMaximumSize(new Dimension(panelWidth, panelHeight));
			changePanel.add(this.m_ChangeProj_JButton);
			this.add(changePanel);
		}

		public void itemStateChanged(ItemEvent ie)
		{
			String item = (String)ie.getItem();

			if(item.equalsIgnoreCase(Proj_JPanel.s_PROJ_EQUI_CYLINDRICAL_TEXT) || item.equalsIgnoreCase(Proj_JPanel.s_PROJ_MERCATOR_TEXT) || item.equalsIgnoreCase(Proj_JPanel.s_PROJ_MILLER_TEXT)
			|| item.equalsIgnoreCase(Proj_JPanel.s_PROJ_MOLLWEIDE_TEXT) || item.equalsIgnoreCase(Proj_JPanel.s_PROJ_SINUSOIDAL_TEXT) || item.equalsIgnoreCase(Proj_JPanel.s_PROJ_HAMMERAITOFF_TEXT))
			{
				this.m_ProjOption_JPanel.setEnabled(false);
				this.m_Lon_JPanel.setEnabled(true);
				this.m_Lat_JPanel.setEnabled(false);
				this.m_Alt_JPanel.setEnabled(false);
			}
			else if(item.equalsIgnoreCase(Proj_JPanel.s_PROJ_AZIMUTH_EQUI_TEXT) || item.equalsIgnoreCase(Proj_JPanel.s_PROJ_STEREOGRAPHIC_TEXT))
			{
				this.m_ProjOption_JPanel.setEnabled(false);

				this.m_Lon_JPanel.setEnabled(true);
				this.m_Lat_JPanel.setEnabled(true);
				this.m_Alt_JPanel.setEnabled(false);
			}
			else if(item.equalsIgnoreCase(Proj_JPanel.s_PROJ_ORTHOGRAPHIC_TEXT))
			{
				this.m_ProjOption_JPanel.m_ProjOption_JComboBox.setEnabled(true);

				if(this.m_ProjOption_JPanel.getOption().equalsIgnoreCase(Projections_JPanel.ProjOption_JPanel.s_OPTION_CENTER_TEXT))
				{
					this.m_ProjOption_JPanel.m_DispHeight_JTextField.setEnabled(false);
					this.m_ProjOption_JPanel.m_DispHeight_JTextField.setText("");
					this.m_Lon_JPanel.setEnabled(true);
					this.m_Lat_JPanel.setEnabled(true);
					this.m_Alt_JPanel.setEnabled(false);
				}
				else
				{
					this.m_ProjOption_JPanel.m_DispHeight_JTextField.setEnabled(true);
					this.m_ProjOption_JPanel.m_DispHeight_JTextField.setText("10000000.0");
					this.m_ProjOption_JPanel.m_CoordFrame_JComboBox.setEnabled(true);
					this.m_Lon_JPanel.setEnabled(false);
					this.m_Lat_JPanel.setEnabled(false);
					this.m_Alt_JPanel.setEnabled(false);
				}

				this.m_ProjOption_JPanel.m_FOV_JTextField.setEnabled(false);
				this.m_ProjOption_JPanel.m_FOV_JTextField.setText("");
			}
			else if(item.equalsIgnoreCase(Proj_JPanel.s_PROJ_PERSPECTIVE_TEXT))
			{
				this.m_ProjOption_JPanel.m_ProjOption_JComboBox.setEnabled(true);

				if(this.m_ProjOption_JPanel.getOption().equalsIgnoreCase(Projections_JPanel.ProjOption_JPanel.s_OPTION_CENTER_TEXT))
				{
					this.m_ProjOption_JPanel.m_FOV_JTextField.setEnabled(false);
					this.m_ProjOption_JPanel.m_FOV_JTextField.setText("");
					this.m_Alt_JPanel.setEnabled(true);
					this.m_Lon_JPanel.setEnabled(true);
					this.m_Lat_JPanel.setEnabled(true);
					this.m_Alt_JPanel.setAlt("15000000.0");
				}
				else
				{
					this.m_ProjOption_JPanel.m_FOV_JTextField.setEnabled(true);
					this.m_ProjOption_JPanel.m_FOV_JTextField.setText("10.0");
					this.m_ProjOption_JPanel.m_CoordFrame_JComboBox.setEnabled(true);
					this.m_Alt_JPanel.setEnabled(false);
					this.m_Lon_JPanel.setEnabled(false);
					this.m_Lat_JPanel.setEnabled(false);
				}

				this.m_ProjOption_JPanel.m_DispHeight_JTextField.setEnabled(false);
				this.m_ProjOption_JPanel.m_DispHeight_JTextField.setText("");
			}
			else if(item.equalsIgnoreCase(ProjOption_JPanel.s_OPTION_CENTER_TEXT))
			{
				String projection = this.m_Projection_JPanel.getProjection();

				if(projection.equalsIgnoreCase(Proj_JPanel.s_PROJ_ORTHOGRAPHIC_TEXT))
				{
					this.m_Alt_JPanel.setEnabled(false);
				}
				else if(projection.equalsIgnoreCase(Proj_JPanel.s_PROJ_PERSPECTIVE_TEXT))
				{
					this.m_Alt_JPanel.setEnabled(true);
				}

				this.m_ProjOption_JPanel.m_CoordFrame_JComboBox.setEnabled(false);

				this.m_ProjOption_JPanel.m_DispHeight_JTextField.setEnabled(false);
				this.m_ProjOption_JPanel.m_DispHeight_JTextField.setText("");

				this.m_ProjOption_JPanel.m_FOV_JTextField.setEnabled(false);
				this.m_ProjOption_JPanel.m_FOV_JTextField.setText("");

				this.m_Lon_JPanel.setEnabled(true);
				this.m_Lat_JPanel.setEnabled(true);
			}
			else if(item.equalsIgnoreCase(ProjOption_JPanel.s_OPTION_FORMAT_TEXT))
			{
				String projection = this.m_Projection_JPanel.getProjection();

				if(projection.equalsIgnoreCase(Proj_JPanel.s_PROJ_ORTHOGRAPHIC_TEXT))
				{
					this.m_ProjOption_JPanel.m_DispHeight_JTextField.setEnabled(true);
					this.m_ProjOption_JPanel.m_DispHeight_JTextField.setText("10000000.0");

					this.m_ProjOption_JPanel.m_FOV_JTextField.setEnabled(false);
					this.m_ProjOption_JPanel.m_FOV_JTextField.setText("");
				}
				else if(projection.equalsIgnoreCase(Proj_JPanel.s_PROJ_PERSPECTIVE_TEXT))
				{
					this.m_ProjOption_JPanel.m_DispHeight_JTextField.setEnabled(false);
					this.m_ProjOption_JPanel.m_DispHeight_JTextField.setText("");

					this.m_ProjOption_JPanel.m_FOV_JTextField.setEnabled(true);
					this.m_ProjOption_JPanel.m_FOV_JTextField.setText("10.0");
				}

				this.m_ProjOption_JPanel.m_CoordFrame_JComboBox.setEnabled(true);

				this.m_Alt_JPanel.setEnabled(false);
				this.m_Lon_JPanel.setEnabled(false);
				this.m_Lat_JPanel.setEnabled(false);
			}
		}

		public void addActionListener(ActionListener al)
		{
			this.m_ChangeProj_JButton.addActionListener(al);
		}

		public void removeActionListener(ActionListener al)
		{
			this.m_ChangeProj_JButton.removeActionListener(al);
		}

		public Proj_JPanel getProjPanel()
		{
			return this.m_Projection_JPanel;
		}

		public Lon_JPanel getLonPanel()
		{
			return this.m_Lon_JPanel;
		}

		public Lat_JPanel getLatPanel()
		{
			return this.m_Lat_JPanel;
		}

		public Alt_JPanel getAltPanel()
		{
			return this.m_Alt_JPanel;
		}

		public ProjOption_JPanel getOptionPanel()
		{
			return this.m_ProjOption_JPanel;
		}

		class Proj_JPanel
		extends JPanel
		{
			private static final long	serialVersionUID				= 1L;

			public final static String	s_PROJECTION_TEXT				= " Projection :";

			// Center <LonCenter>
			public final static String	s_PROJ_EQUI_CYLINDRICAL_TEXT	= "EquiCylindrical";
			public final static String	s_PROJ_MERCATOR_TEXT			= "Mercator";
			public final static String	s_PROJ_MILLER_TEXT				= "Miller";
			public final static String	s_PROJ_MOLLWEIDE_TEXT			= "Mollweide";
			public final static String	s_PROJ_SINUSOIDAL_TEXT			= "Sinusoidal";

			// Center <LatCenter>
			public final static String	s_PROJ_HAMMERAITOFF_TEXT		= "HammerAitoff";

			// Center <LatCenter> <LonCenter>
			public final static String	s_PROJ_AZIMUTH_EQUI_TEXT		= "AzimuthEqui";
			public final static String	s_PROJ_STEREOGRAPHIC_TEXT		= "Stereographic";

			// {ProjOption} <Parameters>
			public final static String	s_PROJ_ORTHOGRAPHIC_TEXT		= "Orthographic";
			public final static String	s_PROJ_PERSPECTIVE_TEXT			= "Perspective";

			private JLabel				m_Projection_JLabel;
			private JComboBox			m_ProjectionComboBox;

			public Proj_JPanel()
			{
				this.initialize();
			}

			private void initialize()
			{
				this.setLayout(new GridLayout(1, 2));

				Border b1 = BorderFactory.createLineBorder(Color.GRAY);
				this.setBorder(b1);

				Dimension size = new Dimension(150, 25);

				this.m_Projection_JLabel = new JLabel();
				this.m_Projection_JLabel.setText(s_PROJECTION_TEXT);
				this.m_Projection_JLabel.setSize(size);
				this.m_Projection_JLabel.setPreferredSize(size);
				this.m_Projection_JLabel.setMinimumSize(size);
				this.m_Projection_JLabel.setMaximumSize(size);
				this.add(this.m_Projection_JLabel);

				this.m_ProjectionComboBox = new JComboBox();
				this.m_ProjectionComboBox.setSize(size);
				this.m_ProjectionComboBox.setPreferredSize(size);
				this.m_ProjectionComboBox.setMinimumSize(size);
				this.m_ProjectionComboBox.setMaximumSize(size);
				this.m_ProjectionComboBox.addItem(s_PROJ_EQUI_CYLINDRICAL_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_MERCATOR_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_MILLER_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_MOLLWEIDE_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_SINUSOIDAL_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_HAMMERAITOFF_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_AZIMUTH_EQUI_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_STEREOGRAPHIC_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_ORTHOGRAPHIC_TEXT);
				this.m_ProjectionComboBox.addItem(s_PROJ_PERSPECTIVE_TEXT);
				this.add(this.m_ProjectionComboBox);
			}

			public void addItemListener(ItemListener il)
			{
				this.m_ProjectionComboBox.addItemListener(il);
			}

			public void removeItemListener(ItemListener il)
			{
				this.m_ProjectionComboBox.removeItemListener(il);
			}

			public String getProjection()
			{
				return (String)this.m_ProjectionComboBox.getSelectedItem();
			}

			public void setEnabled(boolean enabled)
			{
				this.m_ProjectionComboBox.setEnabled(enabled);
				super.setEnabled(enabled);
			}
		}

		class ProjOption_JPanel
		extends JPanel
		{
			private static final long	serialVersionUID		= 1L;

			public final static String	s_OPTION_TEXT			= " Option Type :";
			public final static String	s_COORDFRAME_TEXT		= " Coord. Frame :";
			public final static String	s_DISP_HEIGHT_TEXT		= " Display Height :";
			public final static String	s_FOV_TEXT				= " Field Of View (deg.) :";

			public final static String	s_COORD_ECI_TEXT		= "ECI";
			public final static String	s_COORD_ECF_TEXT		= "ECF";

			public final static String	s_OPTION_CENTER_TEXT	= "Center";
			public final static String	s_OPTION_FORMAT_TEXT	= "Format";
			public final static String	s_OPTION_GRID_TEXT		= "Grid";

			private JLabel				m_ProjOption_JLabel;
			private JComboBox			m_ProjOption_JComboBox;

			private JLabel				m_CoordFrame_JLabel;
			private JComboBox			m_CoordFrame_JComboBox;

			private JLabel				m_DispHeight_JLabel;
			private JTextField			m_DispHeight_JTextField;

			private JLabel				m_FOV_JLabel;
			private JTextField			m_FOV_JTextField;

			public ProjOption_JPanel()
			{
				this.initialize();
			}

			private void initialize()
			{
				this.setLayout(new GridLayout(4, 2));

				Border b1 = BorderFactory.createLineBorder(Color.GRAY);
				this.setBorder(b1);

				Dimension size = new Dimension(150, 25);

				this.m_ProjOption_JLabel = new JLabel();
				this.m_ProjOption_JLabel.setText(s_OPTION_TEXT);
				this.m_ProjOption_JLabel.setSize(size);
				this.m_ProjOption_JLabel.setPreferredSize(size);
				this.m_ProjOption_JLabel.setMinimumSize(size);
				this.m_ProjOption_JLabel.setMaximumSize(size);
				this.add(this.m_ProjOption_JLabel);

				this.m_ProjOption_JComboBox = new JComboBox();
				this.m_ProjOption_JComboBox.setSize(size);
				this.m_ProjOption_JComboBox.setPreferredSize(size);
				this.m_ProjOption_JComboBox.setMinimumSize(size);
				this.m_ProjOption_JComboBox.setMaximumSize(size);
				this.m_ProjOption_JComboBox.addItem(s_OPTION_CENTER_TEXT);
				this.m_ProjOption_JComboBox.addItem(s_OPTION_FORMAT_TEXT);
				this.add(this.m_ProjOption_JComboBox);

				this.m_CoordFrame_JLabel = new JLabel();
				this.m_CoordFrame_JLabel.setText(s_COORDFRAME_TEXT);
				this.m_CoordFrame_JLabel.setSize(size);
				this.m_CoordFrame_JLabel.setPreferredSize(size);
				this.m_CoordFrame_JLabel.setMinimumSize(size);
				this.m_CoordFrame_JLabel.setMaximumSize(size);
				this.add(this.m_CoordFrame_JLabel);

				this.m_CoordFrame_JComboBox = new JComboBox();
				this.m_CoordFrame_JComboBox.setSize(size);
				this.m_CoordFrame_JComboBox.setPreferredSize(size);
				this.m_CoordFrame_JComboBox.setMinimumSize(size);
				this.m_CoordFrame_JComboBox.setMaximumSize(size);
				this.m_CoordFrame_JComboBox.addItem(s_COORD_ECF_TEXT);
				this.m_CoordFrame_JComboBox.addItem(s_COORD_ECI_TEXT);
				this.add(this.m_CoordFrame_JComboBox);

				this.m_DispHeight_JLabel = new JLabel();
				this.m_DispHeight_JLabel.setSize(size);
				this.m_DispHeight_JLabel.setPreferredSize(size);
				this.m_DispHeight_JLabel.setMinimumSize(size);
				this.m_DispHeight_JLabel.setMaximumSize(size);
				this.m_DispHeight_JLabel.setText(s_DISP_HEIGHT_TEXT);
				this.add(this.m_DispHeight_JLabel);

				this.m_DispHeight_JTextField = new JTextField();
				this.m_DispHeight_JTextField.setSize(size);
				this.m_DispHeight_JTextField.setPreferredSize(size);
				this.m_DispHeight_JTextField.setMinimumSize(size);
				this.m_DispHeight_JTextField.setMaximumSize(size);
				this.m_DispHeight_JTextField.setEnabled(false);
				this.add(this.m_DispHeight_JTextField);

				this.m_FOV_JLabel = new JLabel();
				this.m_FOV_JLabel.setText(s_FOV_TEXT);
				this.m_FOV_JLabel.setSize(size);
				this.m_FOV_JLabel.setPreferredSize(size);
				this.m_FOV_JLabel.setMinimumSize(size);
				this.m_FOV_JLabel.setMaximumSize(size);
				this.add(this.m_FOV_JLabel);

				this.m_FOV_JTextField = new JTextField();
				this.m_FOV_JTextField.setEnabled(false);
				this.m_FOV_JTextField.setSize(size);
				this.m_FOV_JTextField.setPreferredSize(size);
				this.m_FOV_JTextField.setMinimumSize(size);
				this.m_FOV_JTextField.setMaximumSize(size);
				this.add(this.m_FOV_JTextField);

				this.setEnabled(false);
			}

			public void addItemListener(ItemListener il)
			{
				this.m_ProjOption_JComboBox.addItemListener(il);
				this.m_CoordFrame_JComboBox.addItemListener(il);
			}

			public void removeItemListener(ItemListener il)
			{
				this.m_ProjOption_JComboBox.removeItemListener(il);
				this.m_CoordFrame_JComboBox.removeItemListener(il);
			}

			public String getOption()
			{
				return (String)this.m_ProjOption_JComboBox.getSelectedItem();
			}

			public String getCoordFrame()
			{
				return (String)this.m_CoordFrame_JComboBox.getSelectedItem();
			}

			public String getDispHeight()
			{
				return this.m_DispHeight_JTextField.getText();
			}

			public void setDispHeight(String height)
			{
				this.m_DispHeight_JTextField.setText(height);
			}

			public String getFOV()
			{
				return this.m_FOV_JTextField.getText();
			}

			public void setFOV(String fov)
			{
				this.m_FOV_JTextField.setText(fov);
			}

			public void setEnabled(boolean enabled)
			{
				this.m_CoordFrame_JComboBox.setEnabled(enabled);
				this.m_ProjOption_JComboBox.setEnabled(enabled);
				this.m_DispHeight_JTextField.setEnabled(enabled);
				this.m_FOV_JTextField.setEnabled(enabled);
				super.setEnabled(enabled);

				if(enabled)
				{
					this.m_DispHeight_JTextField.setText("0.0");
					this.m_FOV_JTextField.setText("0.0");
				}
				else
				{
					this.m_DispHeight_JTextField.setText("");
					this.m_FOV_JTextField.setText("");
				}
			}
		}

		class Lon_JPanel
		extends JPanel
		{
			private static final long	serialVersionUID	= 1L;

			public final static String	s_LON_TEXT			= " Longitude (deg.) :";

			private JLabel				m_Lon_JLabel;
			private JTextField			m_Lon_JTextField;

			public Lon_JPanel()
			{
				this.initialize();
			}

			private void initialize()
			{
				this.setLayout(new GridLayout(1, 2));

				Border b1 = BorderFactory.createLineBorder(Color.GRAY);
				this.setBorder(b1);

				Dimension size = new Dimension(150, 25);

				this.m_Lon_JLabel = new JLabel();
				this.m_Lon_JLabel.setText(s_LON_TEXT);
				this.m_Lon_JLabel.setSize(size);
				this.m_Lon_JLabel.setPreferredSize(size);
				this.m_Lon_JLabel.setMinimumSize(size);
				this.m_Lon_JLabel.setMaximumSize(size);
				this.add(this.m_Lon_JLabel);

				this.m_Lon_JTextField = new JTextField();
				this.m_Lon_JTextField.setSize(size);
				this.m_Lon_JTextField.setPreferredSize(size);
				this.m_Lon_JTextField.setMinimumSize(size);
				this.m_Lon_JTextField.setMaximumSize(size);
				this.add(this.m_Lon_JTextField);

				this.setEnabled(false);
			}

			public String getLon()
			{
				return this.m_Lon_JTextField.getText();
			}

			public void setLon(String lon)
			{
				this.m_Lon_JTextField.setText(lon);
			}

			public void setEnabled(boolean enabled)
			{
				this.m_Lon_JTextField.setEnabled(enabled);
				super.setEnabled(enabled);

				if(enabled)
				{
					this.m_Lon_JTextField.setText("0.0");
				}
				else
				{
					this.m_Lon_JTextField.setText("");
				}
			}
		}

		class Lat_JPanel
		extends JPanel
		{
			private static final long	serialVersionUID	= 1L;

			public final static String	s_LAT_TEXT			= " Latitude (deg.) :";

			private JLabel				m_Lat_JLabel;
			private JTextField			m_Lat_JTextField;

			public Lat_JPanel()
			{
				this.initialize();
			}

			private void initialize()
			{
				this.setLayout(new GridLayout(1, 2));

				Border b1 = BorderFactory.createLineBorder(Color.GRAY);
				this.setBorder(b1);

				Dimension size = new Dimension(150, 25);

				this.m_Lat_JLabel = new JLabel();
				this.m_Lat_JLabel.setText(s_LAT_TEXT);
				this.m_Lat_JLabel.setSize(size);
				this.m_Lat_JLabel.setPreferredSize(size);
				this.m_Lat_JLabel.setMinimumSize(size);
				this.m_Lat_JLabel.setMaximumSize(size);
				this.add(this.m_Lat_JLabel);

				this.m_Lat_JTextField = new JTextField();
				this.m_Lat_JTextField.setSize(size);
				this.m_Lat_JTextField.setPreferredSize(size);
				this.m_Lat_JTextField.setMinimumSize(size);
				this.m_Lat_JTextField.setMaximumSize(size);
				this.add(this.m_Lat_JTextField);

				this.setEnabled(false);
			}

			public String getLat()
			{
				return this.m_Lat_JTextField.getText();
			}

			public void setLat(String lat)
			{
				this.m_Lat_JTextField.setText(lat);
			}

			public void setEnabled(boolean enabled)
			{
				this.m_Lat_JTextField.setEnabled(enabled);
				super.setEnabled(enabled);

				if(enabled)
				{
					this.m_Lat_JTextField.setText("0.0");
				}
				else
				{
					this.m_Lat_JTextField.setText("");
				}
			}
		}

		class Alt_JPanel
		extends JPanel
		{
			private static final long	serialVersionUID	= 1L;

			public final static String	s_ALT_TEXT			= " Altitude :";

			private JLabel				m_Alt_JLabel;
			private JTextField			m_Alt_JTextField;

			public Alt_JPanel()
			{
				this.initialize();
			}

			private void initialize()
			{
				this.setLayout(new GridLayout(1, 2));

				Border b1 = BorderFactory.createLineBorder(Color.GRAY);
				this.setBorder(b1);

				Dimension size = new Dimension(150, 25);

				this.m_Alt_JLabel = new JLabel();
				this.m_Alt_JLabel.setText(s_ALT_TEXT);
				this.m_Alt_JLabel.setSize(size);
				this.m_Alt_JLabel.setPreferredSize(size);
				this.m_Alt_JLabel.setMinimumSize(size);
				this.m_Alt_JLabel.setMaximumSize(size);
				this.add(this.m_Alt_JLabel);

				this.m_Alt_JTextField = new JTextField();
				this.m_Alt_JTextField.setSize(size);
				this.m_Alt_JTextField.setPreferredSize(size);
				this.m_Alt_JTextField.setMinimumSize(size);
				this.m_Alt_JTextField.setMaximumSize(size);
				this.add(this.m_Alt_JTextField);

				this.setEnabled(false);
			}

			public String getAlt()
			{
				return this.m_Alt_JTextField.getText();
			}

			public void setAlt(String alt)
			{
				this.m_Alt_JTextField.setText(alt);
			}

			public void setEnabled(boolean enabled)
			{
				this.m_Alt_JTextField.setEnabled(enabled);
				super.setEnabled(enabled);

				if(enabled)
				{
					this.m_Alt_JTextField.setText("0.0");
				}
				else
				{
					this.m_Alt_JTextField.setText("");
				}
			}
		}
	}

	class InvalidRangeException
	extends Exception
	{
		private static final long	serialVersionUID	= 1L;

	}

	class InvalidLatRangeException
	extends InvalidRangeException
	{
		private static final long	serialVersionUID	= 1L;

		private double				m_d;

		public InvalidLatRangeException(double d)
		{
			this.m_d = d;
		}

		public String getMessage()
		{
			return "Invalid Latitude value = " + this.m_d + ", range is -90 to 90";
		}
	}

	class InvalidLonRangeException
	extends InvalidRangeException
	{
		private static final long	serialVersionUID	= 1L;

		private double				m_d;

		public InvalidLonRangeException(double d)
		{
			this.m_d = d;
		}

		public String getMessage()
		{
			return "Invalid Longitude value = " + this.m_d + ", range is -180 to 360";
		}
	}

	class InvalidAltRangeException
	extends InvalidRangeException
	{
		private static final long	serialVersionUID	= 1L;

		private double				m_d;

		public InvalidAltRangeException(double d)
		{
			this.m_d = d;
		}

		public String getMessage()
		{
			return "Invalid Altitude value = " + this.m_d + ", must be greater than 0";
		}
	}

	class InvalidHeightRangeException
	extends InvalidRangeException
	{
		private static final long	serialVersionUID	= 1L;

		private double				m_d;

		public InvalidHeightRangeException(double d)
		{
			this.m_d = d;
		}

		public String getMessage()
		{
			return "Invalid Height value = " + this.m_d + ", must be greater than 0 ";
		}
	}

	class InvalidFovRangeException
	extends InvalidRangeException
	{
		private static final long	serialVersionUID	= 1L;

		private double				m_d;

		public InvalidFovRangeException(double d)
		{
			this.m_d = d;
		}

		public String getMessage()
		{
			return "Invalid Field of View value = " + this.m_d + ", range is 1 to 180 ";
		}
	}
}