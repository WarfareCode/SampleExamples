// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.border.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.logging.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
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
	private static final long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Globe_DrawRects";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private final static String		s_ADD_RECT			= "Add Rectangle";
	private final static String		s_CLEAR_ALL			= "Clear All";
	private final static String		s_LIST_ALL			= "List All";
	private final static String		s_STYLE				= "Style:";
	private final static String		s_WIDTH				= "Width:";
	private final static String		s_COLOR				= "Color:";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;

	private IAgDrawElemRect			m_IAgDrawElemRect;

	private GlobeEventsAdapter		m_GlobeEventsAdapter;

	private JPanel					m_RectangleAttributesPanel;
	private JLabel					m_StyleJLabel;
	private JComboBox				m_StyleJComboBox;
	private JLabel					m_WidthJLabel;
	private JComboBox				m_WidthJComboBox;
	private JLabel					m_ColorJLabel;
	private JButton					m_ColorJButton;

	private JPanel					m_RectangleActionsPanel;
	private JButton					m_AddRectButton;
	private JButton					m_ClearAllButton;
	private JButton					m_ListAllButton;

	private AgCoreColor				m_coreColor			= AgCoreColor.WHITE_COLOR;
	private int						m_PickMode			= 0;
	private int						m_X;
	private int						m_Y;

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

		JPanel bottomJPanel = new JPanel();
		bottomJPanel.setLayout(new BorderLayout());
		bottomJPanel.setBorder(new BevelBorder(BevelBorder.RAISED));
		
		this.m_RectangleAttributesPanel = new JPanel();
		this.m_RectangleAttributesPanel.setBorder(new TitledBorder(new BevelBorder(BevelBorder.LOWERED), "Rectangle"));
		this.m_RectangleAttributesPanel.setLayout(new GridLayout(2, 3));

		this.m_StyleJLabel = new JLabel();
		this.m_StyleJLabel.setText(s_STYLE);
		this.m_StyleJLabel.setHorizontalAlignment(SwingConstants.CENTER);
		this.m_StyleJLabel.setBorder(new EmptyBorder(0,5,0,5));
		this.m_RectangleAttributesPanel.add(this.m_StyleJLabel);

		this.m_WidthJLabel = new JLabel();
		this.m_WidthJLabel.setText(s_WIDTH);
		this.m_WidthJLabel.setHorizontalAlignment(SwingConstants.CENTER);
		this.m_WidthJLabel.setBorder(new EmptyBorder(0,5,0,5));
		this.m_RectangleAttributesPanel.add(this.m_WidthJLabel);

		this.m_ColorJLabel = new JLabel();
		this.m_ColorJLabel.setText(s_COLOR);
		this.m_ColorJLabel.setHorizontalAlignment(SwingConstants.CENTER);
		this.m_ColorJLabel.setBorder(new EmptyBorder(0,5,0,5));
		this.m_RectangleAttributesPanel.add(this.m_ColorJLabel);

		this.m_StyleJComboBox = new JComboBox();
		this.m_StyleJComboBox.addItem("Solid");
		this.m_StyleJComboBox.addItem("Dashed");
		this.m_StyleJComboBox.addItem("Dotted");
		this.m_StyleJComboBox.addItem("DotDashed");
		this.m_StyleJComboBox.addItem("LongDashed");
		this.m_StyleJComboBox.addItem("DashDotDotted");
		this.m_RectangleAttributesPanel.add(this.m_StyleJComboBox);

		this.m_WidthJComboBox = new JComboBox();
		this.m_WidthJComboBox.addItem("1 pt");
		this.m_WidthJComboBox.addItem("2 pt");
		this.m_WidthJComboBox.addItem("3 pt");
		this.m_WidthJComboBox.addItem("4 pt");
		this.m_WidthJComboBox.addItem("5 pt");
		this.m_RectangleAttributesPanel.add(this.m_WidthJComboBox);

		this.m_ColorJButton = new JButton();
		this.m_ColorJButton.setBackground(AgAwtColorTranslator.fromCoreColortoAWT((this.m_coreColor)));
		this.m_ColorJButton.addActionListener(this);
		this.m_RectangleAttributesPanel.add(this.m_ColorJButton);

		this.m_RectangleActionsPanel = new JPanel();
		this.m_RectangleActionsPanel.setLayout(new GridLayout(1, 3));

		this.m_AddRectButton = new JButton(s_ADD_RECT);
		this.m_AddRectButton.addActionListener(this);
		this.m_RectangleActionsPanel.add(this.m_AddRectButton);

		this.m_ListAllButton = new JButton(s_LIST_ALL);
		this.m_ListAllButton.addActionListener(this);
		this.m_RectangleActionsPanel.add(this.m_ListAllButton);

		this.m_ClearAllButton = new JButton(s_CLEAR_ALL);
		this.m_ClearAllButton.addActionListener(this);
		this.m_RectangleActionsPanel.add(this.m_ClearAllButton);

		bottomJPanel.add(this.m_RectangleAttributesPanel, BorderLayout.WEST);
		bottomJPanel.add(this.m_RectangleActionsPanel, BorderLayout.CENTER);
		
		this.getContentPane().add(this.m_AgGlobeCntrlClass, BorderLayout.CENTER);
		this.getContentPane().add(bottomJPanel, BorderLayout.SOUTH);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	public void actionPerformed(ActionEvent event)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			Object src = event.getSource();

			
			if(src == m_AddRectButton)
			{
				if(this.m_AgStkObjectRootClass.getCurrentScenario() == null) return;
				this.m_PickMode = 1;
				this.m_AgGlobeCntrlClass.setMouseMode(AgEMouseMode.E_MOUSE_MODE_MANUAL);
			}
			else if(src == m_ClearAllButton)
			{
				if(this.m_AgStkObjectRootClass.getCurrentScenario() == null) return;
				this.m_AgGlobeCntrlClass.getDrawElements().clear();
			}
			else if(src == m_ListAllButton)
			{
				if(this.m_AgStkObjectRootClass.getCurrentScenario() == null) return;
				listAll();
			}
			else if(src == m_ColorJButton)
			{
				Color currentAwtColor = this.m_ColorJButton.getBackground();
				Color newAwtColor = JColorChooser.showDialog(this, "Line Color", currentAwtColor);
				this.m_ColorJButton.setBackground(newAwtColor);
				this.m_coreColor = AgAwtColorTranslator.fromAWTtoCoreColor(newAwtColor);
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

	public void startRect(int x, int y)
	throws Throwable
	{
		if(this.m_PickMode == 1)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_PickMode = 2;
		}
	}

	public void stopRect(int x, int y)
	throws Throwable
	{
		if(this.m_PickMode == 2)
		{
			if(this.m_IAgDrawElemRect != null)
			{
				this.m_IAgDrawElemRect.set(this.m_X, this.m_Y, x, y);
				this.m_IAgDrawElemRect = null;
			}

			this.m_PickMode = 0;
			this.m_AgGlobeCntrlClass.setMouseMode(AgEMouseMode.E_MOUSE_MODE_AUTOMATIC);
		}
	}

	public void drawRect(int x, int y)
	throws Throwable
	{
		if(this.m_PickMode == 2)
		{
			if(this.m_IAgDrawElemRect == null)
			{
				AgDrawElemRectClass rect = null;
				rect = (AgDrawElemRectClass)this.m_AgGlobeCntrlClass.getDrawElements().add("Rect");

				String style = (String)this.m_StyleJComboBox.getSelectedItem();

				if(style.equals("Solid"))
				{
					rect.setLineStyle(agi.stkx.AgELineStyle.E_SOLID);
				}
				else if(style.equals("Dashed"))
				{
					rect.setLineStyle(agi.stkx.AgELineStyle.E_DASHED);
				}
				else if(style.equals("Dotted"))
				{
					rect.setLineStyle(agi.stkx.AgELineStyle.E_DOTTED);
				}
				else if(style.equals("DotDashed"))
				{
					rect.setLineStyle(agi.stkx.AgELineStyle.E_DOT_DASHED);
				}
				else if(style.equals("LongDashed"))
				{
					rect.setLineStyle(agi.stkx.AgELineStyle.E_LONG_DASHED);
				}
				else if(style.equals("DashDotDotted"))
				{
					rect.setLineStyle(agi.stkx.AgELineStyle.E_DASH_DOT_DOTTED);
				}

				String size = (String)this.m_WidthJComboBox.getSelectedItem();

				if(size.equals("1 pt"))
				{
					rect.setLineWidth(1);
				}
				else if(size.equals("2 pt"))
				{
					rect.setLineWidth(2);
				}
				else if(size.equals("3 pt"))
				{
					rect.setLineWidth(3);
				}
				else if(size.equals("4 pt"))
				{
					rect.setLineWidth(4);
				}
				else if(size.equals("5 pt"))
				{
					rect.setLineWidth(5);
				}

				rect.setColor(this.m_coreColor);

				this.m_IAgDrawElemRect = rect;
			}

			this.m_IAgDrawElemRect.set(this.m_X, this.m_Y, x, y);
		}
	}

	private void listAll()
	throws Throwable
	{
		String message = "";
		IAgDrawElemRect rect;

		IAgDrawElemCollection col = this.m_AgGlobeCntrlClass.getDrawElements();
		for(int i = 0; i < col.getCount(); ++i)
		{
			IAgDrawElem elem = col.getItem(i);
			rect = (IAgDrawElemRect)elem;

			if(i > 0)
			{
				message += "\n";
			}

			message += "[left, top, right, bottom]=[" + rect.getLeft() + ", " + rect.getTop() + ", " + rect.getRight() + ", " + rect.getBottom() + "] LineWidth=" + rect.getLineWidth() + " LineStyle="
			+ rect.getLineStyle() + " Color=" + rect.getColor();
		}

		if(message.equals(""))
		{
			message = "No Rectangles Found";
		}

		JOptionPane.showMessageDialog(this, message, "Rectangles", JOptionPane.PLAIN_MESSAGE);
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

				if(type == AgGlobeCntrlEvent.TYPE_MOUSE_DOWN)
				{
					//int button = ((Short)params[0]).shortValue();
					//int shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();
					startRect(x, y);
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_MOVE)
				{
					//int button = ((Short)params[0]).shortValue();
					//int shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();
					drawRect(x, y);
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_UP)
				{
					//int button = ((Short)params[0]).shortValue();
					//int shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();
					stopRect(x, y);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
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
				MainWindow.this.m_AgGlobeCntrlClass.removeIAgGlobeCntrlEvents(MainWindow.this.m_GlobeEventsAdapter);

				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();

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
}