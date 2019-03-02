// Java API
import java.util.*;
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
import agi.stkgraphics.*;
import agi.stkengine.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
//samples API
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKEngineSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKEngineSampleBaseJFrame
{
	private final static long					serialVersionUID	= 1L;

	private final static String					s_TITLE				= "CustomApp_AWT_STK_Graphics_Globe_PolylineDrawing";
	private final static String					s_DESCFILENAME		= "AppDescription.html";

	private IAgNtvAppEventsListener				m_IAgNtvAppEventsListener2;

	private AgGlobeCntrlClass					m_AgGlobeCntrlClass;
	private AgGlobeCntrlEventsAdapter			m_AgGlobeCntrlEventsAdapter;
	private RootEventsAdapter					m_RootEventsAdapter;

	// Members need for this specifc sample
	private IAgStkGraphicsPolylinePrimitive		m_editingPolyline;
	private IAgStkGraphicsPointBatchPrimitive	m_editingPointBatch;
	private Object[]							m_editingPoints;
	private Point								m_lastMousePosition;

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

		MainWindow.this.m_editingPoints = new Object[0];
		MainWindow.this.m_AgGlobeCntrlClass.requestFocusInWindow();
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
				else if(type == AgGlobeCntrlEvent.TYPE_KEY_UP)
				{
					// Pressing space bar creates a new polygon using the
					// points the user just clicked.
					Object[] params = e.getParams();
					short keyCode = ((Short)params[0]).shortValue();
					if(keyCode == (short)AgAsciiKeyDecimalValues.Space)
					{
						handleKeyUp();
					}
				}
			}
			catch(Throwable t)
			{
				// The mouse position does not intersect the Earth.
				// or
				// Maybe not be possible to triangulate some polygons like
				// those that cross over themselves.
				// t.printStackTrace();
			}
		}
	}

	private void handleGlobeDoubleClick()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();
		IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
		if(scenario != null)
		{
			IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
			IAgStkGraphicsSceneCollection scenes = manager.getScenes();
			IAgStkGraphicsScene scene0 = scenes.getItem(0);
			IAgStkGraphicsCamera camera = scene0.getCamera();

			Object[] position = new Object[2];
			position[0] = new Double(this.m_lastMousePosition.x);
			position[1] = new Double(this.m_lastMousePosition.y);

			Object[] clickedPosition = (Object[])camera.windowToCartographic_AsObject("Earth", position);

			if(clickedPosition != null)
			{
				Object[] newEditingPoints = new Object[this.m_editingPoints.length + 3];
				for(int i = 0; i < this.m_editingPoints.length; i++)
				{
					newEditingPoints[i] = this.m_editingPoints[i];
				}
				int nepLength = newEditingPoints.length;
				newEditingPoints[nepLength - 3] = clickedPosition[0];
				newEditingPoints[nepLength - 2] = clickedPosition[1];
				newEditingPoints[nepLength - 1] = clickedPosition[2];
	
				this.m_editingPoints = newEditingPoints;
	
				setEditingPrimitives(root, this.m_editingPoints);
				scene0.render();
			}
		}
	}

	private void handleGlobeMouseMove(short button, short shift, int x, int y)
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();
		IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
		if(scenario != null)
		{
			IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
			IAgStkGraphicsSceneCollection scenes = manager.getScenes();
			IAgStkGraphicsScene scene0 = scenes.getItem(0);
			IAgStkGraphicsCamera camera = scene0.getCamera();

			this.m_lastMousePosition = new Point(x, y);

			// When the mouse moves, draw a line from the polyline's last
			// position to the position on the globe under the cursor.
			Object[] position = new Object[2];
			position[0] = new Integer(x);
			position[1] = new Integer(y);

			Object[] clickedPosition = (Object[])camera.windowToCartographic_AsObject("Earth", position);

			if(clickedPosition != null)
			{
				Object[] inProgressEditingPoints = new Object[this.m_editingPoints.length + 3];
				for(int i = 0; i < this.m_editingPoints.length; i++)
				{
					inProgressEditingPoints[i] = this.m_editingPoints[i];
				}
				int ipepLength = inProgressEditingPoints.length;
				inProgressEditingPoints[ipepLength - 3] = clickedPosition[0];
				inProgressEditingPoints[ipepLength - 2] = clickedPosition[1];
				inProgressEditingPoints[ipepLength - 1] = clickedPosition[2];
	
				setEditingPrimitives(root, inProgressEditingPoints);
				scene0.render();
			}
		}
	}

	private void handleKeyUp()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();
		IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
		if(scenario != null)
		{
			IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
			IAgStkGraphicsSceneCollection scenes = manager.getScenes();
			IAgStkGraphicsScene scene0 = scenes.getItem(0);

			if(this.m_editingPoints.length > 1)
			{
				IAgStkGraphicsFactoryAndInitializers initrs = manager.getInitializers();
				IAgStkGraphicsPrimitiveManager primitives = manager.getPrimitives();

				IAgStkGraphicsGreatArcInterpolatorFactory gai = initrs.getGreatArcInterpolator();
				IAgStkGraphicsGreatArcInterpolator interp = gai.initializeDefault();
				IAgStkGraphicsPositionInterpolator posinterp = (IAgStkGraphicsPositionInterpolator)interp;
				IAgStkGraphicsPolylinePrimitiveFactory polylinePrim = initrs.getPolylinePrimitive();
				IAgStkGraphicsPolylinePrimitive newPolyline = polylinePrim.initializeWithInterpolatorAndSetHint(posinterp, AgEStkGraphicsSetHint.E_STK_GRAPHICS_SET_HINT_INFREQUENT);

				((IAgStkGraphicsPrimitive)newPolyline).setColor(((IAgStkGraphicsPrimitive)m_editingPolyline).getColor());
				newPolyline.setWidth(2);
				newPolyline.setCartographic("Earth", m_editingPoints);
				primitives.add((IAgStkGraphicsPrimitive)newPolyline);
			}

			// Clear list and primitives used for drawing the current polygon
			// so user can draw a fresh one.
			Object[] arrEditingPoints = new Object[0];
			m_editingPoints = arrEditingPoints;
			setEditingPrimitives(root, m_editingPoints);

			// Assign a random color to the next polygon
			Random r = new Random();
			Color color = new Color(r.nextInt(256), r.nextInt(256), r.nextInt(256));
			((IAgStkGraphicsPrimitive)m_editingPolyline).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(color));
			scene0.render();
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
					initializeGraphics();

					addInstructionsOverlay();
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private void setEditingPrimitives(AgStkObjectRootClass root, Object[] positions)
	throws AgCoreException
	{
		try
		{
			m_editingPolyline.setCartographic("Earth", positions);
		}
		catch(Throwable t)
		{
			// There is no unique geodesic curve connecting the initial and final points.
		}

		m_editingPointBatch.setCartographic("Earth", positions);
	}

	public void initializeGraphics()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();

		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		IAgStkGraphicsGreatArcInterpolator interp = manager.getInitializers().getGreatArcInterpolator().initializeDefault();
		m_editingPolyline = manager.getInitializers().getPolylinePrimitive().initializeWithInterpolator((IAgStkGraphicsPositionInterpolator)interp);
		m_editingPolyline.setWidth(2);
		manager.getPrimitives().add((IAgStkGraphicsPrimitive)m_editingPolyline);

		m_editingPointBatch = manager.getInitializers().getPointBatchPrimitive().initializeDefault();
		m_editingPointBatch.setPixelSize(4);
		manager.getPrimitives().add((IAgStkGraphicsPrimitive)m_editingPointBatch);
	}

	private void addInstructionsOverlay()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();

		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		String userDir = AgSystemPropertiesHelper.getUserDir();
		String fileSep = AgSystemPropertiesHelper.getFileSeparator();
		String filePath = userDir + fileSep + "Instructions.bmp";

		IAgStkGraphicsRendererTexture2D texture = manager.getTextures().loadFromStringUri(filePath);
		IAgStkGraphicsTextureScreenOverlay textureOverlay = manager.getInitializers().getTextureScreenOverlay().initializeWithXYTexture(10, 10, texture);
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)textureOverlay;
		overlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_LEFT);
		overlay.setBorderSize(1);

		IAgStkGraphicsScreenOverlayCollectionBase baseOverlays = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays();
		baseOverlays.add((IAgStkGraphicsScreenOverlay)textureOverlay);
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