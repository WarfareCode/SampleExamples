import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Component;
import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.logging.ConsoleHandler;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JTextField;
import javax.swing.SwingConstants;
import javax.swing.border.BevelBorder;
import javax.swing.border.CompoundBorder;
import javax.swing.border.EmptyBorder;
import javax.swing.border.TitledBorder;
import agi.core.AgCoreException;
import agi.core.awt.AgAwt_JNI;
import agi.core.logging.AgFormatter;
import agi.stk.automation.application.swing.AutomationApplicationSTKSampleBaseJFrame;
import agi.stk.ui.AgStkAutomation_JNI;
import agi.stk.ui.AgStkUi;
import agi.swing.AgAGIImageIcon;


public class MainWindow
// NOTE:  This sample derives/extends from AutomationApplicationSTKSampleBaseJFrame in order to provide
// common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
// You application is not required to derive from this class or have the same features it provides, but rather
// from the standard JFrame, Frame, or other preference.
extends AutomationApplicationSTKSampleBaseJFrame
implements ActionListener
{
	private static final long	serialVersionUID	= 1L;

	private final static String	s_TITLE				= "Automation_Swing_STK_Basic";
	private final static String	s_DESCFILENAME		= "AppDescription.html";

	private StkAppJPanel		m_StkAppJPanel;

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
		AgStkAutomation_JNI.initialize(true);
		AgAwt_JNI.initialize_AwtComponents();

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

		this.m_StkAppJPanel = new StkAppJPanel();
		this.m_StkAppJPanel.addActionListener(this);

		this.getContentPane().add(this.m_StkAppJPanel, BorderLayout.CENTER);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getAutomationAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getAutomationAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getAutomationAppSTKSampleBaseJMenuBar().invalidate();
		this.getAutomationAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new AgWindowAdapter());

		this.setSize(600, 345);
		this.setResizable(false);
	}

	public void actionPerformed(ActionEvent ae)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			if(ae.getActionCommand().equalsIgnoreCase(StkConnectionJPanel.s_START_STK_TEXT))
			{
				startSTKInstance();
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkConnectionJPanel.s_CONNECT_STK_TEXT))
			{
				connectToSTKInstance();
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkDetailsJPanel.s_UPDATE))
			{
				updateSTK();
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkConnectionJPanel.s_RELEASE_STK_TEXT))
			{
				releaseSTKInstance();
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

	private void releaseSTKInstance()
	{
		if(this.getStkUi() != null)
		{
			this.m_StkAppJPanel.getDetails().setEnabled(false);
			this.m_StkAppJPanel.getDetails().clear();
			this.m_StkAppJPanel.getPath().clear();

			this.closeConnectionToSTK();

			this.m_StkAppJPanel.getConnection().setConnectToStk(false);
		}
	}

	private void updateSTK()
	{
		try
		{
			if(this.getStkUi() != null)
			{
				this.getStkUi().setTop(this.m_StkAppJPanel.getDetails().getStkTop());
				this.getStkUi().setLeft(this.m_StkAppJPanel.getDetails().getStkLeft());
				this.getStkUi().setHeight(this.m_StkAppJPanel.getDetails().getStkHeight());
				this.getStkUi().setWidth(this.m_StkAppJPanel.getDetails().getStkWidth());
				this.getStkUi().setUserControl(this.m_StkAppJPanel.getDetails().getUserCtrld());
				this.getStkUi().setVisible(this.m_StkAppJPanel.getDetails().getStkVisible());
			}
		}
		catch(AgCoreException ex)
		{
			ex.printHexHresult();
			ex.printStackTrace();
		}
	}

	private void connectToSTKInstance()
	{
		try
		{
			AgStkUi stkui = AgStkUi.getStkUiInstance();
			if(stkui != null)
			{
				this.setStkUi(stkui);

				this.getStkUi().setUserControl(true);

				this.m_StkAppJPanel.getDetails().setStkTop(this.getStkUi().getTop());
				this.m_StkAppJPanel.getDetails().setStkLeft(this.getStkUi().getLeft());
				this.m_StkAppJPanel.getDetails().setStkHeight(this.getStkUi().getHeight());
				this.m_StkAppJPanel.getDetails().setStkWidth(this.getStkUi().getWidth());
				this.m_StkAppJPanel.getDetails().setStkVisible(this.getStkUi().getVisible());
				this.m_StkAppJPanel.getDetails().setUserCtrld(this.getStkUi().getUserControl());
				this.m_StkAppJPanel.getPath().setStkPath(this.getStkUi().getPath());
				this.m_StkAppJPanel.getDetails().setEnabled(true);

				this.m_StkAppJPanel.getConnection().setConnectToStk(true);
			}
		}
		catch(AgCoreException ex)
		{
			ex.printHexHresult();
			ex.printStackTrace();
			this.m_StkAppJPanel.getDetails().setEnabled(false);
			this.m_StkAppJPanel.getConnection().setConnectToStk(false);
			closeConnectionToSTK();
		}
	}

	private void startSTKInstance()
	{
		try
		{
			AgStkUi stkui = new AgStkUi();

			if(stkui != null)
			{
				this.setStkUi(stkui);

				this.getStkUi().setVisible(true);
				this.getStkUi().setUserControl(false);

				this.m_StkAppJPanel.getDetails().setStkTop(this.getStkUi().getTop());
				this.m_StkAppJPanel.getDetails().setStkLeft(this.getStkUi().getLeft());
				this.m_StkAppJPanel.getDetails().setStkHeight(this.getStkUi().getHeight());
				this.m_StkAppJPanel.getDetails().setStkWidth(this.getStkUi().getWidth());
				this.m_StkAppJPanel.getDetails().setStkVisible(this.getStkUi().getVisible());
				this.m_StkAppJPanel.getDetails().setUserCtrld(this.getStkUi().getUserControl());
				this.m_StkAppJPanel.getPath().setStkPath(this.getStkUi().getPath());
				this.m_StkAppJPanel.getDetails().setEnabled(true);

				this.m_StkAppJPanel.getConnection().setConnectToStk(true);
			}
		}
		catch(AgCoreException ex)
		{
			ex.printHexHresult();
			ex.printStackTrace();
			this.m_StkAppJPanel.getDetails().setEnabled(false);
			this.m_StkAppJPanel.getConnection().setConnectToStk(false);
			closeConnectionToSTK();
		}
	}

	public void closeConnectionToSTK()
	{
		try
		{
			this.releaseStkUi();
		}
		catch(AgCoreException e)
		{
			e.printHexHresult();
			e.printStackTrace();
		}
	}

	class AgWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				closeConnectionToSTK();

				AgAwt_JNI.uninitialize_AwtComponents();
				AgStkAutomation_JNI.uninitialize();
				AgAwt_JNI.uninitialize_AwtDelegate();

				System.runFinalization(); // Tell the JVM it should finalize classes.
				System.gc(); // Tell the JVM it should garbage collect.
				System.exit(0); // Tell the JVM to exit successfully.
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	class StkAppJPanel
	extends JPanel
	{
		private static final long	serialVersionUID	= 1L;

		public final static String	s_STK_APP			= "STK Application";

		private StkConnectionJPanel	m_StkConnectionJPanel;
		private StkDetailsJPanel	m_StkDetailsJPanel;
		private StkPathJPanel		m_StkPathJPanel;

		public StkAppJPanel()
		throws Exception
		{
			this.setLayout(new BorderLayout());

			TitledBorder b1 = new TitledBorder(new BevelBorder(BevelBorder.LOWERED), s_STK_APP);
			CompoundBorder cb1 = new CompoundBorder(new EmptyBorder(5,5,5,5), b1);
			this.setBorder(cb1);

			JPanel phantom1 = new JPanel();
			phantom1.setLayout(new GridLayout(1, 2));

			this.m_StkConnectionJPanel = new StkConnectionJPanel();
			this.m_StkDetailsJPanel = new StkDetailsJPanel();
			this.m_StkPathJPanel = new StkPathJPanel();

			phantom1.add(this.m_StkConnectionJPanel);
			phantom1.add(this.m_StkDetailsJPanel);
			this.add(phantom1, BorderLayout.CENTER);
			this.add(this.m_StkPathJPanel, BorderLayout.SOUTH);
		}

		public StkConnectionJPanel getConnection()
		{
			return this.m_StkConnectionJPanel;
		}

		public StkDetailsJPanel getDetails()
		{
			return this.m_StkDetailsJPanel;
		}

		public StkPathJPanel getPath()
		{
			return this.m_StkPathJPanel;
		}

		public void addActionListener(ActionListener al)
		{
			this.m_StkConnectionJPanel.addActionListener(al);
			this.m_StkDetailsJPanel.addActionListener(al);
		}

		public void removeActionListener(ActionListener al)
		{
			this.m_StkConnectionJPanel.removeActionListener(al);
			this.m_StkDetailsJPanel.removeActionListener(al);
		}
	}

	class StkConnectionJPanel
	extends JPanel
	{
		private static final long	serialVersionUID	= 1L;

		public final static String	s_CONNECTION		= "Connection";
		public final static String	s_START_STK_TEXT	= "Create New ...";
		public final static String	s_CONNECT_STK_TEXT	= "Get Running ...";
		public final static String	s_RELEASE_STK_TEXT	= "Release";

		private final static String	s_CONNECTED			= "CONNECTED";
		private final static String	s_DISCONNECTED		= "DISCONNECTED";

		private JButton				m_StartStkButton;
		private JButton				m_ConnectToStkButton;
		private JButton				m_ReleaseStkButton;
		private JLabel				m_StatusLabel;

		public StkConnectionJPanel()
		throws Exception
		{
			this.setLayout(new GridLayout(4, 1));

			TitledBorder b1 = new TitledBorder(new BevelBorder(BevelBorder.RAISED), s_CONNECTION);
			CompoundBorder cb1 = new CompoundBorder(new EmptyBorder(5,5,5,5), b1);
			this.setBorder(cb1);

			this.m_StartStkButton = new JButton(s_START_STK_TEXT);
			this.m_StartStkButton.setEnabled(true);
			this.m_StartStkButton.setBorder(new BevelBorder(BevelBorder.RAISED));
			this.add(this.m_StartStkButton);

			this.m_ConnectToStkButton = new JButton(s_CONNECT_STK_TEXT);
			this.m_ConnectToStkButton.setEnabled(true);
			this.m_ConnectToStkButton.setBorder(new BevelBorder(BevelBorder.RAISED));
			this.add(this.m_ConnectToStkButton);

			this.m_ReleaseStkButton = new JButton(s_RELEASE_STK_TEXT);
			this.m_ReleaseStkButton.setEnabled(false);
			this.m_ReleaseStkButton.setBorder(new BevelBorder(BevelBorder.RAISED));
			this.add(this.m_ReleaseStkButton);

			JPanel phantomPanel = new JPanel();
			CompoundBorder phantomBorder = new CompoundBorder(new BevelBorder(BevelBorder.RAISED), new EmptyBorder(7,7,7,7));
			phantomPanel.setBorder(phantomBorder);
			phantomPanel.setLayout(new BorderLayout());
			this.add(phantomPanel);
			
			this.m_StatusLabel = new JLabel();
			this.m_StatusLabel.setOpaque(true);
			this.m_StatusLabel.setBackground(Color.RED);
			this.m_StatusLabel.setText(s_DISCONNECTED);
			this.m_StatusLabel.setHorizontalTextPosition(SwingConstants.CENTER);
			this.m_StatusLabel.setHorizontalAlignment(SwingConstants.CENTER);
			this.m_StatusLabel.setBorder(new BevelBorder(BevelBorder.LOWERED));
			phantomPanel.add(this.m_StatusLabel);
		}

		public void addActionListener(ActionListener al)
		{
			this.m_StartStkButton.addActionListener(al);
			this.m_ConnectToStkButton.addActionListener(al);
			this.m_ReleaseStkButton.addActionListener(al);
		}

		public void removeActionListener(ActionListener al)
		{
			this.m_StartStkButton.removeActionListener(al);
			this.m_ConnectToStkButton.removeActionListener(al);
			this.m_ReleaseStkButton.removeActionListener(al);
		}

		public void setConnectToStk(boolean connected)
		{
			this.m_StartStkButton.setEnabled(!connected);
			this.m_ConnectToStkButton.setEnabled(!connected);
			this.m_ReleaseStkButton.setEnabled(connected);

			if(connected)
			{
				this.m_StatusLabel.setBackground(Color.GREEN);
				this.m_StatusLabel.setText(s_CONNECTED);
			}
			else
			{
				this.m_StatusLabel.setBackground(Color.RED);
				this.m_StatusLabel.setText(s_DISCONNECTED);
			}
		}
	}

	class StkDetailsJPanel
	extends JPanel
	{
		private static final long	serialVersionUID	= 1L;

		public final static String	s_DETAILS			= "Details";
		public final static String	s_TOP_TEXT			= "Top:";
		public final static String	s_LEFT_TEXT			= "Left:";
		public final static String	s_HEIGHT_TEXT		= "Height:";
		public final static String	s_WIDTH_TEXT		= "Width:";
		public final static String	s_UPDATE			= "Update";

		private JLabel				m_TopLabel;
		private JTextField			m_TopTextField;

		private JLabel				m_LeftLabel;
		private JTextField			m_LeftTextField;

		private JLabel				m_HeightLabel;
		private JTextField			m_HeightTextField;

		private JLabel				m_WidthLabel;
		private JTextField			m_WidthTextField;

		private JRadioButton		m_UserCtrld_JR;
		private JRadioButton		m_AutoCtrld_JR;
		private ButtonGroup			m_CtrlBG;

		private JRadioButton		m_Visible_JR;
		private JRadioButton		m_Invisible_JR;
		private ButtonGroup			m_VisBG;

		private JButton				m_UpdateDetails;

		public StkDetailsJPanel()
		throws Exception
		{
			this.setLayout(new GridLayout(7, 2));

			TitledBorder b1 = new TitledBorder(new BevelBorder(BevelBorder.RAISED), s_DETAILS);
			CompoundBorder cb1 = new CompoundBorder(new EmptyBorder(5,5,5,5), b1);
			this.setBorder(cb1);

			this.m_TopLabel = new JLabel();
			this.m_TopLabel.setText(s_TOP_TEXT);
			this.m_TopLabel.setBorder(new EmptyBorder(2, 10, 2, 10));
			this.add(this.m_TopLabel);

			this.m_TopTextField = new JTextField();
			this.m_TopTextField.setEnabled(false);
			this.m_TopTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED),new EmptyBorder(0, 10, 0, 10)));
			this.m_TopTextField.setHorizontalAlignment(SwingConstants.RIGHT);
			this.add(this.m_TopTextField);

			this.m_LeftLabel = new JLabel();
			this.m_LeftLabel.setText(s_LEFT_TEXT);
			this.m_LeftLabel.setBorder(new EmptyBorder(2, 10, 2, 10));
			this.add(this.m_LeftLabel);

			this.m_LeftTextField = new JTextField();
			this.m_LeftTextField.setEnabled(false);
			this.m_LeftTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED),new EmptyBorder(0, 10, 0, 10)));
			this.m_LeftTextField.setHorizontalAlignment(SwingConstants.RIGHT);
			this.add(this.m_LeftTextField);

			this.m_HeightLabel = new JLabel();
			this.m_HeightLabel.setText(s_HEIGHT_TEXT);
			this.m_HeightLabel.setBorder(new EmptyBorder(2, 10, 2, 10));
			this.add(this.m_HeightLabel);

			this.m_HeightTextField = new JTextField();
			this.m_HeightTextField.setEnabled(false);
			this.m_HeightTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED),new EmptyBorder(0, 10, 0, 10)));
			this.m_HeightTextField.setHorizontalAlignment(SwingConstants.RIGHT);
			this.add(this.m_HeightTextField);

			this.m_WidthLabel = new JLabel();
			this.m_WidthLabel.setText(s_WIDTH_TEXT);
			this.m_WidthLabel.setBorder(new EmptyBorder(2, 10, 2, 10));
			this.add(this.m_WidthLabel);

			this.m_WidthTextField = new JTextField();
			this.m_WidthTextField.setEnabled(false);
			this.m_WidthTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED),new EmptyBorder(0, 10, 0, 10)));
			this.m_WidthTextField.setHorizontalAlignment(SwingConstants.RIGHT);
			this.add(this.m_WidthTextField);

			this.m_UserCtrld_JR = new JRadioButton();
			this.m_UserCtrld_JR.setText("User Ctrld");
			this.m_UserCtrld_JR.setEnabled(false);
			this.add(this.m_UserCtrld_JR);

			this.m_AutoCtrld_JR = new JRadioButton();
			this.m_AutoCtrld_JR.setText("Auto Ctrld");
			this.m_AutoCtrld_JR.setEnabled(false);
			this.add(this.m_AutoCtrld_JR);

			this.m_CtrlBG = new ButtonGroup();
			this.m_CtrlBG.add(this.m_UserCtrld_JR);
			this.m_CtrlBG.add(this.m_AutoCtrld_JR);

			this.m_Visible_JR = new JRadioButton();
			this.m_Visible_JR.setText("Visible");
			this.m_Visible_JR.setEnabled(false);
			this.add(this.m_Visible_JR);

			this.m_Invisible_JR = new JRadioButton();
			this.m_Invisible_JR.setText("Invisible");
			this.m_Invisible_JR.setEnabled(false);
			this.add(this.m_Invisible_JR);

			this.m_VisBG = new ButtonGroup();
			this.m_VisBG.add(this.m_Visible_JR);
			this.m_VisBG.add(this.m_Invisible_JR);

			JLabel phantom = new JLabel();
			this.add(phantom);

			this.m_UpdateDetails = new JButton();
			this.m_UpdateDetails.setText(s_UPDATE);
			this.m_UpdateDetails.setEnabled(false);
			this.m_UpdateDetails.setBorder(new BevelBorder(BevelBorder.RAISED));
			this.add(this.m_UpdateDetails);
		}

		public void addActionListener(ActionListener al)
		{
			this.m_UpdateDetails.addActionListener(al);
		}

		public void removeActionListener(ActionListener al)
		{
			this.m_UpdateDetails.removeActionListener(al);
		}

		public void setEnabled(boolean enabled)
		{
			this.m_HeightTextField.setEnabled(enabled);
			this.m_WidthTextField.setEnabled(enabled);
			this.m_TopTextField.setEnabled(enabled);
			this.m_LeftTextField.setEnabled(enabled);
			this.m_UpdateDetails.setEnabled(enabled);
			this.m_AutoCtrld_JR.setEnabled(enabled);
			this.m_UserCtrld_JR.setEnabled(enabled);
			this.m_Visible_JR.setEnabled(enabled);
			this.m_Invisible_JR.setEnabled(enabled);
		}

		public void setStkHeight(long height)
		{
			this.m_HeightTextField.setText(new Long(height).toString());
		}

		public void setStkWidth(long width)
		{
			this.m_WidthTextField.setText(new Long(width).toString());
		}

		public void setStkTop(long top)
		{
			this.m_TopTextField.setText(new Long(top).toString());
		}

		public void setStkLeft(long left)
		{
			this.m_LeftTextField.setText(new Long(left).toString());
		}

		public void setUserCtrld(boolean ctrld)
		{
			if(ctrld)
			{
				this.m_UserCtrld_JR.setSelected(ctrld);
			}
			else
			{
				this.m_AutoCtrld_JR.setSelected(!ctrld);
			}
		}

		public void setStkVisible(boolean vis)
		{
			if(vis)
			{
				this.m_Visible_JR.setSelected(vis);
			}
			else
			{
				this.m_Invisible_JR.setSelected(!vis);
			}
		}

		public int getStkHeight()
		{
			return Integer.parseInt(this.m_HeightTextField.getText());
		}

		public int getStkWidth()
		{
			return Integer.parseInt(this.m_WidthTextField.getText());
		}

		public int getStkTop()
		{
			return Integer.parseInt(this.m_TopTextField.getText());
		}

		public int getStkLeft()
		{
			return Integer.parseInt(this.m_LeftTextField.getText());
		}

		public boolean getStkVisible()
		{
			return this.m_Visible_JR.isSelected();
		}

		public boolean getUserCtrld()
		{
			return this.m_UserCtrld_JR.isSelected();
		}

		public void clear()
		{
			this.m_HeightTextField.setText("");
			this.m_WidthTextField.setText("");
			this.m_LeftTextField.setText("");
			this.m_TopTextField.setText("");
		}
	}

	class StkPathJPanel
	extends JPanel
	{
		private static final long	serialVersionUID	= 1L;

		public final static String	s_PATH				= "Installation Path";

		private JLabel				m_PathLabel;
		private JTextField			m_PathTextField;

		public StkPathJPanel()
		throws Exception
		{
			this.setLayout(new BorderLayout());

			this.setBorder(new CompoundBorder(new EmptyBorder(5,5,5,5), new BevelBorder(BevelBorder.RAISED)));

			this.m_PathLabel = new JLabel();
			this.m_PathLabel.setText(s_PATH);
			this.m_PathLabel.setEnabled(false);
			this.m_PathLabel.setHorizontalAlignment(SwingConstants.LEFT);
			this.m_PathLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
			this.add(this.m_PathLabel, BorderLayout.WEST);
			
			this.m_PathTextField = new JTextField();
			this.m_PathTextField.setMaximumSize(new Dimension(300, 25));
			this.m_PathTextField.setMinimumSize(new Dimension(100, 25));
			this.m_PathTextField.setPreferredSize(new Dimension(300, 25));
			this.m_PathTextField.setEnabled(false);
			this.m_PathTextField.setHorizontalAlignment(SwingConstants.LEFT);
			BevelBorder border4 = new BevelBorder(BevelBorder.LOWERED);
			EmptyBorder border5 = new EmptyBorder(0, 5, 0, 5);
			CompoundBorder border6 = new CompoundBorder(border4, border5);
			this.m_PathTextField.setBorder(border6);
			this.add(this.m_PathTextField, BorderLayout.CENTER);
		}

		public void setStkPath(String path)
		{
			this.m_PathTextField.setText(path);
		}

		public void clear()
		{
			this.m_PathTextField.setText("");
		}
	}
}