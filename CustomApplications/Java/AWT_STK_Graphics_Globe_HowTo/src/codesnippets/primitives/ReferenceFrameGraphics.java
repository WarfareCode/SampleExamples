package codesnippets.primitives;

import java.awt.*;

import agi.core.*;
import agi.core.awt.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

import utils.*;

/**
 * Visualization for a reference frame.  Given a ReferenceFrame, this class
 * creates a polyline and text batch primitive to visualize the reference
 * frame's axes.
 */
public class ReferenceFrameGraphics
implements IDisposable
{
    public ReferenceFrameGraphics(IAgStkObjectRoot root, IAgCrdnSystem referenceFrame, double axesLength)
    throws AgCoreException
    {
        this(root, referenceFrame, axesLength, Color.RED);
    }

    public ReferenceFrameGraphics(IAgStkObjectRoot root, IAgCrdnSystem referenceFrame, double axesLength, Color color)
    throws AgCoreException
    {
    	this(root, referenceFrame, axesLength, color, Color.WHITE);
    }

    public ReferenceFrameGraphics(IAgStkObjectRoot root, IAgCrdnSystem referenceFrame, double axesLength, Color color, Color outlineColor) 
    throws AgCoreException
    {
    	IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
    	IAgStkGraphicsSceneManager sceneMgr = scenario.getSceneManager();
    	IAgStkGraphicsFactoryAndInitializers initrs = sceneMgr.getInitializers();
    	IAgStkGraphicsGraphicsFontFactory fontFactory = initrs.getGraphicsFont();
    	IAgStkGraphicsGraphicsFont font = fontFactory.initializeWithNameSizeFontStyleOutline("MS Sans Serif", 24, AgEStkGraphicsFontStyle.E_STK_GRAPHICS_FONT_STYLE_REGULAR, true);
    	initialize(root, referenceFrame, axesLength, color, outlineColor,font);
    }

    public ReferenceFrameGraphics(IAgStkObjectRoot root, IAgCrdnSystem referenceFrame, double axesLength, Color color, Color outlineColor, IAgStkGraphicsGraphicsFont font)
    throws AgCoreException
    {
    	initialize(root, referenceFrame, axesLength, color, outlineColor, font);
    }

    public void initialize(IAgStkObjectRoot root, IAgCrdnSystem referenceFrame, double axesLength, Color color, Color outlineColor, IAgStkGraphicsGraphicsFont font)
    throws AgCoreException
    {
        manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        int rgb = color.getRGB();

        Object[] lines = new Object[]
        {
                new Double(0),  new Double(0),  new Double(0), /* to */  new Double(axesLength),  new Double(0),  new Double(0),
                new Double(0),  new Double(0),  new Double(0), /* to */  new Double(0),  new Double(axesLength),  new Double(0),
                new Double(0),  new Double(0),  new Double(0), /* to */  new Double(0),  new Double(0),  new Double(axesLength)
        };
        Object[] colors = new Object[]
        {
        		new Long(rgb), new Long(rgb),
        		new Long(rgb), new Long(rgb),
        		new Long(rgb), new Long(rgb),
        };
        m_Lines = manager.getInitializers().getPolylinePrimitive().initializeWithType(AgEStkGraphicsPolylineType.E_STK_GRAPHICS_POLYLINE_TYPE_LINES);
        m_Lines.setWithColors(lines, colors);
        ((IAgStkGraphicsPrimitive)m_Lines).setReferenceFrame(referenceFrame);
        m_Lines.setWidth(2);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)m_Lines);

        Object[] textColors = new Object[]
        {
        	new Long(rgb),
        	new Long(rgb),
        	new Long(rgb)
        };

        Object[] textPositions = new Object[]
        {
            new Double(axesLength), new Double(0), new Double(0),
            new Double(0), new Double(axesLength), new Double(0),
            new Double(0), new Double(0), new Double(axesLength)
        };
    
        Object[] text = new Object[]
        {
            "+X",
            "+Y",
            "+Z",
        };
        m_Text = manager.getInitializers().getTextBatchPrimitive().initializeWithGraphicsFont(font);
        IAgStkGraphicsTextBatchPrimitiveOptionalParameters optionalParameters = null;
        optionalParameters = manager.getInitializers().getTextBatchPrimitiveOptionalParameters().initializeDefault();
        optionalParameters.setColors(textColors);

        m_Text.setWithOptionalParameters(textPositions, text, optionalParameters);

        m_Text.setOutlineColor(AgAwtColorTranslator.fromAWTtoCoreColor(outlineColor));
        ((IAgStkGraphicsPrimitive)m_Text).setReferenceFrame(referenceFrame);
        manager.getPrimitives().add((IAgStkGraphicsPrimitive)m_Text);
    }

    public void dispose()
    {
    	try
    	{
    		dispose(true);
    	}
    	catch(Throwable t)
    	{
    		t.printStackTrace();
    	}
    }

    protected void finalize()
    {
    	try
    	{
    		dispose(false);
    	}
    	catch(Throwable t)
    	{
    		t.printStackTrace();
    	}
    }

    protected void dispose(boolean disposing)
    throws AgCoreException
    {
        if (disposing)
        {
            if (m_Text != null)
            {
                manager.getPrimitives().remove((IAgStkGraphicsPrimitive)m_Text);
                m_Text.release();
                m_Text = null;
            }
            if (m_Lines != null)
            {
                manager.getPrimitives().remove((IAgStkGraphicsPrimitive)m_Lines);
                m_Lines.release();
                m_Lines = null;
            }
        }
    }

    private IAgStkGraphicsPolylinePrimitive m_Lines;
    private IAgStkGraphicsTextBatchPrimitive m_Text;
    private IAgStkGraphicsSceneManager manager;
}