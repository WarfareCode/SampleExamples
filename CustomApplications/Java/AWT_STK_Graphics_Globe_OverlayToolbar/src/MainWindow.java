// Java API
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.ntvapp.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.stkengine.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;

// samples API
import agi.customapplications.swing.*;
import overlays.toolbars.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKEngineSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKEngineSampleBaseJFrame
{
	private final static long			serialVersionUID	= 1L;

	private final static String			s_TITLE				= "CustomApp_AWT_STK_Graphics_Globe_OverlayToolbar";
	private final static String			s_DESCFILENAME		= "AppDescription.html";

	private IAgNtvAppEventsListener		m_IAgNtvAppEventsListener2;

	private AgGlobeCntrlClass			m_AgGlobeCntrlClass;
	private AgGlobeCntrlEventsAdapter	m_AgGlobeCntrlEventsAdapter;
	private RootEventsAdapter			m_RootEventsAdapter;

	// Members need for this specifc sample
	private AgStkGraphicsOverlayToolbar	m_Overlay;

	protected MainWindow()
	throws Throwable
	{
		super(MainWindow.class.getResource(s_DESCFILENAME));

		this.initialize();
	}

	private void initialize()
	throws AgException
	{
		try
		{
			this.setTitle(s_TITLE);
			this.setIconImage(new AgAGIImageIcon().getImage());
			this.setDefaultCloseOperation(EXIT_ON_CLOSE);
            this.addWindowListener(new MainWindowAdapter());
			this.setSize(new Dimension(800, 600));

			this.m_IAgNtvAppEventsListener2 = new IAgNtvAppEventsListener()
			{
				public void onAgNtvAppEvent(AgNtvAppEvent e)
				{
					try
					{
						final int type = e.getType();
						
						SwingUtilities.invokeAndWait(new Runnable() 
						{
							public void run() 
							{
								if (type == AgNtvAppEvent.TYPE_ON_THREAD_START_END)
								{
									initApp();
								}
								else if (type == AgNtvAppEvent.TYPE_ON_THREAD_STOP_BEGIN)
								{
									uninitApp();
								}
							}
						});
					}
					catch(Throwable t)
					{
						t.printStackTrace();
					}
				}
			};
			this.addNtvAppEventsListener(this.m_IAgNtvAppEventsListener2);
		}
		catch(Throwable t)
		{
			throw new AgException(t);
		}
	}

	protected void finalize()
	{
		try
		{
			this.removeNtvAppEventsListener(this.m_IAgNtvAppEventsListener2);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void initApp()
	throws AgCoreException
	{
		this.m_AgGlobeCntrlEventsAdapter = new AgGlobeCntrlEventsAdapter();
		MainWindow.this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		MainWindow.this.m_AgGlobeCntrlClass.addIAgGlobeCntrlEvents(this.m_AgGlobeCntrlEventsAdapter);
		if(AgMetalThemeFactory.getEnabled())
		{
			MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
			Color awtColor = mt.getPrimaryControl();
			AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);
			this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
			this.m_AgGlobeCntrlClass.setBackground(awtColor);
		}
		MainWindow.this.getStkEngineJPanel().add(MainWindow.this.m_AgGlobeCntrlClass, BorderLayout.CENTER);
		IAgStkEngine stkengine = this.getStkEngine();

		this.m_RootEventsAdapter = new RootEventsAdapter();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();
		root.addIAgStkObjectRootEvents2(this.m_RootEventsAdapter);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();
	}

	private void uninitApp()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();
		root.addIAgStkObjectRootEvents2(this.m_RootEventsAdapter);

		MainWindow.this.m_AgGlobeCntrlClass.removeIAgGlobeCntrlEvents(this.m_AgGlobeCntrlEventsAdapter);
		MainWindow.this.getStkEngineJPanel().remove(MainWindow.this.m_AgGlobeCntrlClass);
		MainWindow.this.m_AgGlobeCntrlClass.dispose();
	}

	private class AgGlobeCntrlEventsAdapter
	implements IAgGlobeCntrlEvents
	{
		public void onAgGlobeCntrlEvent(AgGlobeCntrlEvent e)
		{
			try
			{
				int type = e.getType();

				if(type == AgGlobeCntrlEvent.TYPE_DBL_CLICK)
				{
					handleGlobeDoubleClick();
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_MOVE)
				{
					Object[] params = e.getParams();
					short button = ((Short)params[0]).shortValue();
					short shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();
					handleGlobeMouseMove(button, shift, x, y);
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_DOWN)
				{
					Object[] params = e.getParams();
					short button = ((Short)params[0]).shortValue();
					short shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();
					handleGlobeMouseDown(button, shift, x, y);
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_UP)
				{
					Object[] params = e.getParams();
					short button = ((Short)params[0]).shortValue();
					short shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();
					handleGlobeMouseUp(button, shift, x, y);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private void handleGlobeDoubleClick()
	throws AgCoreException
	{
		if(this.m_Overlay != null)
		{
			IAgStkEngine stkengine = this.getStkEngine();
			AgStkObjectRootClass root = stkengine.getStkObjectRoot();
			this.m_Overlay.mouseDoubleClick(root);
		}
	}

	private void handleGlobeMouseMove(short button, short shift, int x, int y)
	throws AgCoreException
	{
		if(this.m_Overlay != null)
		{
			IAgStkEngine stkengine = this.getStkEngine();
			AgStkObjectRootClass root = stkengine.getStkObjectRoot();
			this.m_Overlay.mouseMove(root, button, shift, x, y);
		}
	}

	private void handleGlobeMouseDown(short button, short shift, int x, int y)
	throws AgCoreException
	{
		if(this.m_Overlay != null)
		{
			IAgStkEngine stkengine = this.getStkEngine();
			AgStkObjectRootClass root = stkengine.getStkObjectRoot();
			this.m_Overlay.mouseDown(root, button, shift, x, y);
		}
	}

	private void handleGlobeMouseUp(short button, short shift, int x, int y)
	throws AgCoreException
	{
		if(this.m_Overlay != null)
		{
			IAgStkEngine stkengine = this.getStkEngine();
			AgStkObjectRootClass root = stkengine.getStkObjectRoot();
			this.m_Overlay.mouseUp(root, button, shift, x, y);
		}
	}

	class RootEventsAdapter
	implements IAgStkObjectRootEvents2
	{
		public void onAgStkObjectRootEvent(AgStkObjectRootEvent e)
		{
			try
			{
				int type = e.getType();

				if(type == AgStkObjectRootEvent.TYPE_ON_SCENARIO_NEW)
				{
					IAgStkEngine stkengine = MainWindow.this.getStkEngine();
					AgStkObjectRootClass root = stkengine.getStkObjectRoot();

					// Make sure Units are set properly for the RotationAngle values
					root.getUnitPreferences().setCurrentUnit("AngleUnit", "rad");

					// Make sure Units are set properly for Translucency values
					root.getUnitPreferences().setCurrentUnit("Percent", "unitValue");

					// Turn off default annotations so they don't overlap with dfault overlay toolbar
					// at the bottom of the 3D globe control
					root.executeCommand("VO * Annotation Time Show Off ShowTimeStep Off");
					root.executeCommand("VO * Annotation Frame Show Off");

					MainWindow.this.m_Overlay = new AgStkGraphicsOverlayToolbar(root, MainWindow.this.m_AgGlobeCntrlClass, AgStkGraphicsOverlayToolbar.DOCK_LOCATION_BOTTOM);
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