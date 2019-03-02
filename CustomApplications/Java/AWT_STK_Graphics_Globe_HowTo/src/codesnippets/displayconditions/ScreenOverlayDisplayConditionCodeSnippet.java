package codesnippets.displayconditions;

//#region Imports

//Java API
import java.awt.*;
import java.awt.image.*;
import java.text.*;
import java.util.*;
import javax.imageio.*;
import java.io.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.customtypes.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class ScreenOverlayDisplayConditionCodeSnippet
extends STKGraphicsCodeSnippet
{
	private IAgStkGraphicsPrimitive		m_Model;
	private IAgStkGraphicsScreenOverlay	m_Overlay;
	private ArrayList<Interval> 		m_Intervals;
	
	public ScreenOverlayDisplayConditionCodeSnippet(Component c)
	{
		super(c, "Draw a screen overlay based on viewer distance", "displayconditions", "ScreenOverlayDisplayConditionCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        //#region CodeSnippet
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
        IAgStkGraphicsModelPrimitive model = createTankModel(root, manager);
        IAgPosition position = root.getConversionUtility().newPositionOnEarth();
        Object[] pos = (Object[])model.getPosition_AsObject();
        position.assignCartesian(((Double)pos[0]).doubleValue(), ((Double)pos[1]).doubleValue(), ((Double)pos[2]).doubleValue());
        Object[] planetocentricPosition = (Object[])position.queryPlanetocentricArray_AsObject();

        DecimalFormat latLonFormat = new DecimalFormat("0.0####");
        
        IAgStkGraphicsScreenOverlay overlay = createTextOverlay(manager, "Mobile SA-10 Launcher\n" +
            "Latitude: " + latLonFormat.format(((Double)planetocentricPosition[0]).doubleValue()) + "\n" +
            "Longitude: " + latLonFormat.format(((Double)planetocentricPosition[1]).doubleValue()));

        IAgStkGraphicsDistanceToPrimitiveDisplayCondition condition = null;
        condition = manager.getInitializers().getDistanceToPrimitiveDisplayCondition().initializeWithDistances((IAgStkGraphicsPrimitive)model, 0, 40000);
        ((IAgStkGraphicsOverlay)overlay).setDisplayCondition((IAgStkGraphicsDisplayCondition)condition);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)model);
        overlayManager.add(overlay);
        //#endregion

        m_Model = (IAgStkGraphicsPrimitive)model;
        m_Overlay = overlay;

        OverlayHelper.addTextBox(this, manager, "Zoom in to within 40 km to see the overlay appear.\r\n \r\nThis is implemented by assigning a \r\nDistanceToPrimitiveDisplayCondition to the overlay's \r\nDisplayCondition property.");

        OverlayHelper.addDistanceOverlay(this, manager, scene);

        m_Intervals = new ArrayList<Interval>();
        m_Intervals.add(new Interval(0, 40000));
        OverlayHelper.getDistanceDisplay().addIntervals(m_Intervals);
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		ViewHelper.viewBoundingSphere(root, scene, "Earth", m_Model.getBoundingSphere(), 225, 25);
		scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

		if(m_Overlay != null)
		{
			IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
			overlayManager.remove(m_Overlay);
			m_Overlay = null;
		}

		if(m_Model != null)
		{
			manager.getPrimitives().remove(m_Model);
			m_Model = null;
		}
		
		OverlayHelper.removeTextBox(manager);

		if(m_Intervals != null)
		{
			OverlayHelper.getDistanceDisplay().removeIntervals(m_Intervals);
			m_Intervals = null;
		}

		OverlayHelper.removeDistanceOverlay(this, manager);

		scene.render();
	}

	private static IAgStkGraphicsModelPrimitive createTankModel(AgStkObjectRootClass root, IAgStkGraphicsSceneManager manager)
	throws AgCoreException
    {
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();
		String filePath = DataPaths.getDataPaths().getSharedDataPath("Models"+fileSep+"Land"+fileSep+"sa10-mobile-a.mdl");
        IAgStkGraphicsModelPrimitive model = null;
        model = manager.getInitializers().getModelPrimitive().initializeWithStringUri(filePath);

        Object[] position = new Object[] { new Double(56), new Double(37), new Double(0.0) };
        model.setPositionCartographic("Earth", position);
        model.setScale(1000);


        IAgOrientation orientation = root.getConversionUtility().newOrientation();
        orientation.assignEulerAngles(AgEEulerOrientationSequence.E321, new Double(-37), new Double(-26), new Double(22));
        model.setOrientation(orientation);

        return model;
    }

	private IAgStkGraphicsScreenOverlay createTextOverlay(IAgStkGraphicsSceneManager manager, String text) 
	throws AgCoreException, IOException
    {
		Font font = new Font("Consolas", Font.BOLD, 12);
		Dimension textSize = measureString(text, font);
		BufferedImage textBitmap = new BufferedImage(textSize.width, textSize.height, BufferedImage.TYPE_INT_ARGB);
		
		Graphics2D gfx = textBitmap.createGraphics();
		gfx.setColor(Color.WHITE);
		int maxAdvance = gfx.getFontMetrics().getMaxAdvance();
		drawString(text, maxAdvance / 2, 0, font, gfx);
		gfx.dispose();

        String textBitmapFilepath = DataPaths.getDataPaths().getSampleProjectPath("SA-10TextOverlay.png");
        File f = new File(textBitmapFilepath);
        ImageIO.write(textBitmap, "png", f);

        IAgStkGraphicsRendererTexture2D texture = manager.getTextures().loadFromStringUri(textBitmapFilepath);
        IAgStkGraphicsTextureScreenOverlay overlay = null;
        overlay = manager.getInitializers().getTextureScreenOverlay().initializeWithXYWidthHeight(0, 60, texture.getTemplate().getWidth(), texture.getTemplate().getHeight());
        ((IAgStkGraphicsOverlay)overlay).setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_CENTER);
        overlay.setTexture(texture);
        ((IAgStkGraphicsOverlay)overlay).setBorderSize(2);
        ((IAgStkGraphicsOverlay)overlay).setBorderColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));

        f.delete();

        return (IAgStkGraphicsScreenOverlay)overlay;
    }
}