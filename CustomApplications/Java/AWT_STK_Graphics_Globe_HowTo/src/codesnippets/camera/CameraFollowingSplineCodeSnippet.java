package codesnippets.camera;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class CameraFollowingSplineCodeSnippet
extends STKGraphicsCodeSnippet
{
	private CatmullRomSpline		m_Spline;
	private CameraUpdater			m_CameraUpdater;

	private IAgStkGraphicsPrimitive	m_DebugPointBatch;
	private IAgStkGraphicsPrimitive	m_PointBatch;
	private IAgStkGraphicsPrimitive	m_TextBatch;

	public CameraFollowingSplineCodeSnippet(Component c)
	{
		super(c, "Perform a smooth transition between two points", "camera", "CameraFollowingSplineCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        //#region CodeSnippet
        IAgStkGraphicsSceneManager manager = null;
        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        // Create a camera transition from Washington D.C. to New Orleans
        Object[] startPosition = new Object[] {new Double(38.85), new Double(-77.04), new Double(0.0) };
        Object[] endPosition = new Object[] {new Double(29.98), new Double(-90.25), new Double(0.0) };
        CatmullRomSpline spline = new CatmullRomSpline(root, "Earth", startPosition, endPosition, 2000000);
        //#endregion

        m_Spline = spline;

        m_PointBatch = (IAgStkGraphicsPrimitive)createPointBatch(startPosition, endPosition, manager);
        m_TextBatch = (IAgStkGraphicsPrimitive)createTextBatch("Washington D.C.", "New Orleans", startPosition, endPosition, manager);

        manager.getPrimitives().add(m_PointBatch);
        manager.getPrimitives().add(m_TextBatch);
        
    	OverlayHelper.addTextBox(this, manager, "A Catmull-Rom spline is used to smoothly zoom from one \r\nlocation to another, over a given number of seconds, and \r\nreaching a specified maximum altitude.\r\n\r\nYou can use this technique in your applications by including \r\nthe CatmullRomSpline and CameraUpdater classes from the HowTo.");

        //m_DebugPointBatch = CreateDebugPoints(root) as IAgStkGraphicsPrimitive;
        //manager.Primitives.Add(m_DebugPointBatch);
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        if (m_CameraUpdater == null || !m_CameraUpdater.isRunning())
        {
            CatmullRomSpline spline = m_Spline;

            //#region CodeSnippet
            CameraUpdater cameraUpdater = new CameraUpdater(scene, root, spline.getInterpolatorPoints(), 6);
            //#endregion

            m_CameraUpdater = cameraUpdater;
        }
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if(m_CameraUpdater != null)
        {
        	m_CameraUpdater.dispose();
        	m_CameraUpdater = null;
        }
        
        if(m_Spline != null)
        {
        	m_Spline = null;
        }
        
        if (m_PointBatch != null)
        {
        	manager.getPrimitives().remove(m_PointBatch);
        	m_PointBatch = null;
        }
        
        if (m_TextBatch != null)
        {
        	manager.getPrimitives().remove(m_TextBatch);
        	m_TextBatch = null;
        }

        if (m_DebugPointBatch != null)
        {
            manager.getPrimitives().remove(m_DebugPointBatch);
            m_DebugPointBatch = null;
        }

        OverlayHelper.removeTextBox(manager);

        scene.render();
	}

    // Creates points for the interpolator curve (for debugging purposes)
//    private IAgStkGraphicsPointBatchPrimitive createDebugPoints(AgStkObjectRootClass root)
//    throws AgCoreException
//    {
//        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
//
//        ArrayList<Object[]> positions = new ArrayList<Object[]>();
//        ArrayList<Object[]> ip = m_Spline.getInterpolatorPoints();
//        for (int j=0; j<ip.size(); j++)
//        { 
//        	Object[] c = (Object[])ip.get(j);
//            positions.add(CatmullRomSpline.cartesianToCartographic(c, "Earth", root));
//        }
//        Object[] positionsArray = convertPositionListToArray(positions);
//
//        Object[] colorsArray = new Object[positionsArray.length / 3];
//        for (int i = 0; i < (positionsArray.length / 3); ++i)
//        {
//        	colorsArray[i] = new Long(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLUE));
//        }
//
//        IAgStkGraphicsPointBatchPrimitive debugPointBatch = manager.getInitializers().getPointBatchPrimitive().initializeDefault();
//        debugPointBatch.setCartographicWithColorsAndRenderPass("Earth", positionsArray, colorsArray, AgEStkGraphicsRenderPassHint.eStkGraphicsRenderPassHintOpaque);
//        debugPointBatch.setPixelSize(8);
//
//        return debugPointBatch;
//    }

    // Creates the points for the two cities
    private IAgStkGraphicsPointBatchPrimitive createPointBatch(Object[] start, Object[] end, IAgStkGraphicsSceneManager manager)
    throws AgCoreException
    {
        Object[] positionsArray = convertPositionArraysToArray(new Object[]{start, end});

        Object[] colors = new Object[]
        {
            new Long(AgAwtColorTranslator.fromAWTtoLong(Color.RED)),
            new Long(AgAwtColorTranslator.fromAWTtoLong(Color.RED))
        };

        IAgStkGraphicsPointBatchPrimitive pointBatch = manager.getInitializers().getPointBatchPrimitive().initializeDefault();
        pointBatch.setCartographicWithColorsAndRenderPass("Earth", positionsArray, colors, AgEStkGraphicsRenderPassHint.E_STK_GRAPHICS_RENDER_PASS_HINT_OPAQUE);
        pointBatch.setPixelSize(8);

        return pointBatch;
    }

    // Creates the text for the two cities
    private IAgStkGraphicsTextBatchPrimitive createTextBatch(String startName, String endName, Object[] start, Object[] end, IAgStkGraphicsSceneManager manager) 
    throws AgCoreException
    {
        Object[] text = new Object[]
        {
            startName,
            endName
        };

        Object[] positionsArray = convertPositionArraysToArray(new Object[]{start, end});

        IAgStkGraphicsTextBatchPrimitiveOptionalParameters parameters = manager.getInitializers().getTextBatchPrimitiveOptionalParameters().initializeDefault();

        Object[] pixelOffset = new Object[] { new Integer(3), new Integer(3) };
        
        parameters.setPixelOffset(pixelOffset);

        IAgStkGraphicsGraphicsFont font = manager.getInitializers().getGraphicsFont().initializeWithNameSizeFontStyleOutline("Arial", 12, AgEStkGraphicsFontStyle.E_STK_GRAPHICS_FONT_STYLE_REGULAR, true);
        IAgStkGraphicsTextBatchPrimitive textBatch = manager.getInitializers().getTextBatchPrimitive().initializeWithGraphicsFont(font);
        ((IAgStkGraphicsPrimitive)textBatch).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
        textBatch.setOutlineColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
        textBatch.setCartographicWithOptionalParameters("Earth", positionsArray, text, parameters);

        return textBatch;
    }
}