package codesnippets.primitives.textbatch;

//#region Imports

//Java API
import java.awt.*;
import javax.swing.*;

//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import codesnippets.*;
import utils.helpers.*;

//#endregion

public class TextBatchColorsCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public TextBatchColorsCodeSnippet(Component c)
	{
		super(c, "Draw a set of uniquely colored strings", "primitives", "textbatch", "TextBatchColorsCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			// #region CodeSnippet

			IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
			IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();

            Object[] strings = new Object[]
            {
                "San Francisco",
                "Sacramento",
                "Los Angeles",
                "San Diego"
            };

            Object[] positions = new Object[]
            {
                new Double(37.62), new Double(-122.38), new Double(0.0),
                new Double(38.52), new Double(-121.50), new Double(0.0),
                new Double(33.93), new Double(-118.40), new Double(0.0),
                new Double(32.82), new Double(-117.13), new Double(0.0)
            };

            Object[] colors = new Object[]
            {
                new Long(Color.RED.getRGB()),
                new Long(Color.GREEN.getRGB()),
                new Long(Color.BLUE.getRGB()),
                new Long(Color.WHITE.getRGB())
            };

            IAgStkGraphicsTextBatchPrimitiveOptionalParameters parameters = sceneManager.getInitializers().getTextBatchPrimitiveOptionalParameters().initializeDefault();
            parameters.setColors(colors);

            IAgStkGraphicsGraphicsFont font = sceneManager.getInitializers().getGraphicsFont().initializeWithNameSizeFontStyleOutline("MS Sans Serif", 12, AgEStkGraphicsFontStyle.E_STK_GRAPHICS_FONT_STYLE_BOLD, false);
            IAgStkGraphicsTextBatchPrimitive textBatch = sceneManager.getInitializers().getTextBatchPrimitive().initializeWithGraphicsFont(font);
            textBatch.setCartographicWithOptionalParameters("Earth", positions, strings, parameters);

            sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)textBatch);

            //#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)textBatch;

			OverlayHelper.addTextBox(this, sceneManager, "A collection of colors is provided to the TextBatchPrimitive, in addition to collections of \r\npositions and strings, to visualize strings with different colors.");
		}
		catch(Throwable t)
		{
			t.printStackTrace();
			JOptionPane.showMessageDialog(null, t.toString(), "Exception", JOptionPane.WARNING_MESSAGE);
		}
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			IAgStkGraphicsBoundingSphere boundingSphere = this.m_Primitive.getBoundingSphere();
            ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere);
			scene.render();
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();

            if(this.m_Primitive != null)
            {
            	manager.getPrimitives().remove(this.m_Primitive);
            	this.m_Primitive = null;
            }
            
	        OverlayHelper.removeTextBox(manager);
	        scene.render();
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
	}
}