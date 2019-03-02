// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
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
//CodeSample helper code
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

	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Globe_RubberBandSelect";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;

	private GlobeEventsAdapter		m_GlobeEventsAdapter;

	private final static String		s_SELECTEOBJECTS	= "Select Objects";
	private JButton					m_SelectJButton;

	private IAgDrawElemRect			m_IAgDrawElemRect;
	private int						m_PickMode;
	private int						m_X0;
	private int						m_Y0;

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

		this.m_SelectJButton = new JButton();
		this.m_SelectJButton.setText(s_SELECTEOBJECTS);
		this.m_SelectJButton.addActionListener(this);
		this.getContentPane().add(this.m_SelectJButton, BorderLayout.SOUTH);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	public void startRect(int x, int y)
	throws Throwable
	{
		if(this.m_PickMode == 1)
		{
			this.m_X0 = x;
			this.m_Y0 = y;
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
				this.m_IAgDrawElemRect.set(this.m_X0, this.m_Y0, x, y);
				this.m_IAgDrawElemRect = null;

				try
				{
					IAgRubberBandPickInfoData pickInfo = this.m_AgGlobeCntrlClass.rubberBandPickInfo(this.m_X0, this.m_Y0, x, y);
					IAgObjPathCollection paths = pickInfo.getObjPaths();
					String message;

					if(paths.getCount() == 0)
					{
						message = "No Objects Selected";
					}
					else
					{
						message = "Object(s):";

						for(int i = 0; i < paths.getCount(); ++i)
						{
							message += "\n" + paths.getItem(i);
						}
					}

					final JFrame frame = this;
					final String text = message;
					SwingUtilities.invokeLater(new Runnable()
					{
						public void run()
						{
							JOptionPane.showMessageDialog(frame, text, "Object Paths", JOptionPane.PLAIN_MESSAGE);
						}
					});
				}
				catch(Throwable t)
				{
					t.printStackTrace();
				}
			}

			this.m_PickMode = 0;
			this.m_AgGlobeCntrlClass.getDrawElements().clear();
			this.m_AgGlobeCntrlClass.setMouseMode(AgEMouseMode.E_MOUSE_MODE_AUTOMATIC);
		}
	}

	public void drawRect(int x, int y)
	throws Exception
	{
		if(this.m_PickMode == 2)
		{
			if(this.m_IAgDrawElemRect == null)
			{
				AgDrawElemRectClass rect = null;
				rect = (AgDrawElemRectClass)this.m_AgGlobeCntrlClass.getDrawElements().add("Rect");

				rect.setLineStyle(agi.stkx.AgELineStyle.E_SOLID);
				rect.setLineWidth(1);
				rect.setColor(0x0000FF);

				this.m_IAgDrawElemRect = rect;
			}

			this.m_IAgDrawElemRect.set(this.m_X0, this.m_Y0, x, y);
		}
	}

	public void actionPerformed(ActionEvent event)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			Object src = event.getSource();

			if(src == this.m_SelectJButton)
			{
				this.m_PickMode = 1;
				this.m_AgGlobeCntrlClass.setMouseMode(AgEMouseMode.E_MOUSE_MODE_MANUAL);
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

	class GlobeEventsAdapter
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