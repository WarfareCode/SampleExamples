package codesnippets.camera;

//#region Imports

//Java API
import java.util.*;
import java.awt.event.*;
import javax.swing.Timer;

//AGI Java API
import agi.core.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;

//#endregion

public class CameraUpdater
implements IDisposable
{
	// Member variables
	private int		m_Style	= CameraUpdaterStyle.getDefault();
	private AgStkObjectRootClass	m_Root;
	private IAgStkGraphicsScene		m_Scene;
	private ArrayList<Object[]>		m_Positions;
	private double					m_CurrentPosition;
	private double					m_PreviousPosition;
	private double					m_NumberOfSeconds;
	private Timer					m_Timer;
	private Stopwatch				m_Stopwatch;
	private boolean					m_Stop;

	// Creates a CameraUpdater
	public CameraUpdater(IAgStkGraphicsScene scene, AgStkObjectRootClass root, ArrayList<Object[]> positions, double numberOfSeconds)
	throws AgCoreException
	{
		initialize(scene, root, positions, numberOfSeconds, 60, CameraUpdaterStyle.FIXED);
	}

	// Creates a CameraUpdater with all of the options as parameters
	public CameraUpdater(IAgStkGraphicsScene scene, AgStkObjectRootClass root, ArrayList<Object[]> positions, double numberOfSeconds, double framerate, int style)
	throws AgCoreException
	{
		initialize(scene, root, positions, numberOfSeconds, framerate, style);
	}

	public final void dispose()
	throws AgCoreException
	{
		dispose(true);
	}

	protected void finalize()
	throws Throwable
	{
		try
		{
			dispose(false);
		}
		finally
		{
			super.finalize();
		}
	}

	protected void dispose(boolean disposing)
	throws AgCoreException
	{
		if(disposing)
		{
			if(m_Timer != null)
			{
				m_Timer.stop();
				m_Timer = null;
				m_Scene.getCamera().setLockViewDirection(false);
			}
		}
	}

	// Returns whether or not the animation is still running
	public final boolean isRunning()
	{
		return !m_Stop;
	}

	// Initialize method that is called by the constructors
	final private void initialize(IAgStkGraphicsScene scene, AgStkObjectRootClass root, ArrayList<Object[]> positions, double numberOfSeconds, double framerate, int style)
	throws AgCoreException
	{
		m_Style = style;

		// Initialize the scene and positions
		m_Root = root;
		m_Scene = scene;
		m_Scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
		m_Scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

		m_Positions = new ArrayList<Object[]>(positions);
		m_CurrentPosition = 0;
		m_PreviousPosition = 0;

		// Initialize the stopwatch and timer
		m_NumberOfSeconds = numberOfSeconds;
		m_Stopwatch = new Stopwatch();
		m_Stopwatch.start();
		m_Timer = new Timer((int)(1000 / framerate), new ActionListener()
		{
			public void actionPerformed(ActionEvent e)
			{
				try
				{
					timer_Elapsed(e);
				}
				catch(AgCoreException e1)
				{
					e1.printStackTrace();
				}
			}
		});
		m_Timer.start();
		m_Stop = false;
	}

	// Update the position of the camera for every timer elapsed event
	final private void timer_Elapsed(ActionEvent e)
	throws AgCoreException
	{
		// If simulation has not finished
		if(!m_Stop)
		{
			m_CurrentPosition = (m_Stopwatch.getElapsedMilliseconds() / (m_NumberOfSeconds * 1000)) * m_Positions.size();

			// Determine when to stop the simulation
			if(m_CurrentPosition > 0 && m_CurrentPosition < m_Positions.size())
			{
				// Calculate the camera position and direction
				m_PreviousPosition = m_CurrentPosition;

				Object[] fromPosition = (Object[])m_Positions.get((int)m_CurrentPosition);

				IAgCrdnProvider earth = m_Root.getCentralBodies().getEarth().getVgt();
				IAgCrdnSystem fixedSystem = m_Root.getVgtRoot().getWellKnownSystems().getEarth().getFixed();

				IAgCrdnPoint fromPoint = STKVgtHelper.createPoint(earth, AgECrdnPointType.E_CRDN_POINT_TYPE_FIXED_IN_SYSTEM);
				((IAgCrdnPointFixedInSystem)fromPoint).getReference().setSystem(fixedSystem);
				((IAgCrdnPointFixedInSystem)fromPoint).getFixedPoint().assignCartesian(((Double)fromPosition[0]).doubleValue(), ((Double)fromPosition[1]).doubleValue(),
				((Double)fromPosition[2]).doubleValue());

				Object[] toPosition = CatmullRomSpline.cartesianToCartographic(fromPosition, "Earth", m_Root);
				toPosition[2] = new Double(0.0); // We want to look at the Earth's surface.
				toPosition = CatmullRomSpline.cartographicToCartesian(toPosition, "Earth", m_Root);

				IAgCrdnPoint toPoint = STKVgtHelper.createPoint(earth, AgECrdnPointType.E_CRDN_POINT_TYPE_FIXED_IN_SYSTEM);
				((IAgCrdnPointFixedInSystem)toPoint).getReference().setSystem(fixedSystem);
				((IAgCrdnPointFixedInSystem)toPoint).getFixedPoint().assignCartesian(((Double)toPosition[0]).doubleValue(), ((Double)toPosition[1]).doubleValue(),
				((Double)toPosition[2]).doubleValue());

				IAgCrdnAxes fixedAxes = m_Root.getVgtRoot().getWellKnownAxes().getEarth().getFixed();
				m_Scene.getCamera().view(fixedAxes, fromPoint, toPoint);
				m_Scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
				m_Scene.getCamera().setAxes(fixedAxes);
				m_Scene.getCamera().setLockViewDirection(true);

				if(m_Style == CameraUpdaterStyle.ROTATING)
				{
					IAgCrdnAxes earthFixedAxes = STKVgtHelper.createAxes(earth, AgECrdnAxesType.E_CRDN_AXES_TYPE_FIXED);
					m_Scene.getCamera().setAxes(earthFixedAxes);
				}
			}
			else
			{
				// Stop the simulation and calculates the final camera position and direction
				m_Stop = true;
				m_Timer.stop();

				Object[] fromPosition = (Object[])m_Positions.get((int)m_PreviousPosition);

				IAgCrdnProvider earth = m_Root.getCentralBodies().getEarth().getVgt();
				IAgCrdnSystem earthFixedSystem = m_Root.getVgtRoot().getWellKnownSystems().getEarth().getFixed();
				IAgCrdnPoint fromPoint = STKVgtHelper.createPoint(earth, AgECrdnPointType.E_CRDN_POINT_TYPE_FIXED_IN_SYSTEM);
				((IAgCrdnPointFixedInSystem)fromPoint).getReference().setSystem(earthFixedSystem);
				((IAgCrdnPointFixedInSystem)fromPoint).getFixedPoint().assignCartesian(((Double)fromPosition[0]).doubleValue(), ((Double)fromPosition[1]).doubleValue(),
				((Double)fromPosition[2]).doubleValue());

				Object[] toPosition = (Object[])m_Positions.get(m_Positions.size() - 1);
				IAgCrdnPoint toPoint = STKVgtHelper.createPoint(earth, AgECrdnPointType.E_CRDN_POINT_TYPE_FIXED_IN_SYSTEM);
				((IAgCrdnPointFixedInSystem)toPoint).getReference().setSystem(earthFixedSystem);
				((IAgCrdnPointFixedInSystem)toPoint).getFixedPoint().assignCartesian(((Double)toPosition[0]).doubleValue(), ((Double)toPosition[1]).doubleValue(),
				((Double)toPosition[2]).doubleValue());

				m_Scene.getCamera().view(m_Root.getVgtRoot().getWellKnownAxes().getEarth().getFixed(), fromPoint, toPoint);
				m_Scene.getCamera().setAxes(STKVgtHelper.createAxes(m_Root.getCentralBodies().getEarth().getVgt(), AgECrdnAxesType.E_CRDN_AXES_TYPE_FIXED));
				m_Scene.getCamera().setLockViewDirection(false);
			}
			m_Scene.render();
		}
	}
}