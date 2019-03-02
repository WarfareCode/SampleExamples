package codesnippets.primitives.orderedcomposite;

//#region Imports

//Java API
import java.awt.*;

import agi.core.AgSystemPropertiesHelper;
//AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.*;
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class OrderedCompositeZOrderCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public OrderedCompositeZOrderCodeSnippet(Component c)
	{
		super(c, "Z-order primitives on the surface", "primitives", "orderedcomposite", "OrderedCompositeZOrderCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		//#region CodeSnippet
        String fileSep = AgSystemPropertiesHelper.getFileSeparator();

        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        IAgStkGraphicsTriangleMeshPrimitive pennsylvania = manager.getInitializers().getTriangleMeshPrimitive().initializeDefault();

        String filePath1 = DataPaths.getDataPaths().getSharedDataPath("AreaTargets"+fileSep+"_pennsylvania_1.at");
        Object[] pennsylvaniaPositions = STKUtilHelper.readAreaTargetPoints(root, filePath1);
        pennsylvania.setTriangulator((IAgStkGraphicsTriangulatorResult)manager.getInitializers().getSurfacePolygonTriangulator().compute(
            "Earth", pennsylvaniaPositions));
        ((IAgStkGraphicsPrimitive)pennsylvania).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));

        IAgStkGraphicsTriangleMeshPrimitive areaCode610 = manager.getInitializers().getTriangleMeshPrimitive().initializeDefault();
        String filePath2 = DataPaths.getDataPaths().getSharedDataPath("AreaTargets"+fileSep+"610.at");
        Object[] areaCode610Positions = STKUtilHelper.readAreaTargetPoints(root, filePath2);
        areaCode610.setTriangulator((IAgStkGraphicsTriangulatorResult)manager.getInitializers().getSurfacePolygonTriangulator().compute(
            "Earth", areaCode610Positions));
        ((IAgStkGraphicsPrimitive)areaCode610).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));

        IAgStkGraphicsTriangleMeshPrimitive areaCode215 = manager.getInitializers().getTriangleMeshPrimitive().initializeDefault();
        String filePath3 = DataPaths.getDataPaths().getSharedDataPath("AreaTargets"+fileSep+"215.at");
        Object[] areaCode215Positions = STKUtilHelper.readAreaTargetPoints(root, filePath3);
        areaCode215.setTriangulator((IAgStkGraphicsTriangulatorResult)manager.getInitializers().getSurfacePolygonTriangulator().compute(
            "Earth", areaCode215Positions));
        ((IAgStkGraphicsPrimitive)areaCode215).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN));

        IAgStkGraphicsPolylinePrimitive schuylkillRiver = manager.getInitializers().getPolylinePrimitive().initializeDefault();
        String filePath4 = DataPaths.getDataPaths().getSharedDataPath("LineTargets"+fileSep+"Schuylkill.lt");
        Object[] schuylkillPositions = STKUtilHelper.readLineTargetPoints(root, filePath4);
        schuylkillRiver.set(schuylkillPositions);
        ((IAgStkGraphicsPrimitive)schuylkillRiver).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLUE));
        schuylkillRiver.setWidth(2);

        IAgStkGraphicsCompositePrimitive composite = manager.getInitializers().getCompositePrimitive().initializeDefault();
        composite.add((IAgStkGraphicsPrimitive)pennsylvania);
        composite.add((IAgStkGraphicsPrimitive)areaCode610);
        composite.add((IAgStkGraphicsPrimitive)areaCode215);
        composite.add((IAgStkGraphicsPrimitive)schuylkillRiver);

        manager.getPrimitives().add((IAgStkGraphicsPrimitive)composite);
        //#endregion

        m_Primitive = (IAgStkGraphicsPrimitive)composite;

		OverlayHelper.addTextBox(this, manager, "Using an OrderedCompositePrimitive, the Schuylkill River polyline \r\nis drawn on top of the 215 and 610 area code triangle meshes, which \r\nare drawn on top of the Pennsylvania triangle mesh.\r\nPrimitives added to the composite last are drawn on top.  The order\r\nof primitives in the composite can be changed with methods such as\r\nBringToFront() and SendToBack().");

        Object[] text = new Object[]
        {
            "Pennsylvania",
            "610",
            "215",
            "Schuylkill River"
        };

        Object[] positions = new Object[12];
        Object[] ppos = (Object[])((IAgStkGraphicsPrimitive)pennsylvania).getBoundingSphere().getCenter_AsObject();
        Object[] a610pos = (Object[])((IAgStkGraphicsPrimitive)areaCode610).getBoundingSphere().getCenter_AsObject();
        Object[] a215pos = (Object[])((IAgStkGraphicsPrimitive)areaCode215).getBoundingSphere().getCenter_AsObject();
        Object[] srpos = (Object[])((IAgStkGraphicsPrimitive)schuylkillRiver).getBoundingSphere().getCenter_AsObject();
        for(int i=0; i<3; i++)
        {
        	positions[i] = ppos[i];
        	positions[i+3] = a610pos[i];
        	positions[i+6] = a215pos[i];
        	positions[i+9] = srpos[i];
        }
        
        IAgStkGraphicsGraphicsFont font = manager.getInitializers().getGraphicsFont().initializeWithNameSizeFontStyleOutline("MS Sans Serif", 16, AgEStkGraphicsFontStyle.E_STK_GRAPHICS_FONT_STYLE_BOLD, true);
        IAgStkGraphicsTextBatchPrimitive textBatch = manager.getInitializers().getTextBatchPrimitive().initializeWithGraphicsFont(font);
        ((IAgStkGraphicsPrimitive)textBatch).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
        textBatch.setOutlineColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLACK));
        textBatch.set(positions, text);

        composite.add((IAgStkGraphicsPrimitive)textBatch);
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
        Object[] center = (Object[])m_Primitive.getBoundingSphere().getCenter_AsObject();
        IAgStkGraphicsBoundingSphere boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(
            center, m_Primitive.getBoundingSphere().getRadius() * 0.35);
        ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere,
            -27, 3);

        scene.render();
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

        if(m_Primitive != null)
        {
        	manager.getPrimitives().remove(m_Primitive);
        	m_Primitive = null;
        }
        
        OverlayHelper.removeTextBox(manager);
        scene.render();
	}
}