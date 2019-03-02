package codesnippets.primitives.textbatch;

//#region Imports

// Java API
import java.awt.*;
import javax.swing.*;

// AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

// Sample API
import codesnippets.*;
import utils.helpers.*;

//#endregion

public class TextBatchUnicodeCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public TextBatchUnicodeCodeSnippet(Component c)
	{
		super(c, "Draw a set of strings in various languages", "primitives", "textbatch", "TextBatchUnicodeCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			// #region CodeSnippet

			// Create the strings
			char[] german = {'W', 'i', 'l', 'k', 'o', 'm', 'm', 'e', 'n'};
			char[] french = {'B', 'i', 'e', 'n', 'v', 'e', 'n', 'u', 'e', ' '};
			char[] portuguese = {'B', 'e', 'm', '-', 'v', 'i', 'n', 'd', 'o'};
			char[] english = {'W', 'e', 'l', 'c', 'o', 'm', 'e'};
			char[] russian = {'\u0414', '\u043e', '\u0431', '\u0440', '\u043e', ' ', '\u043f', '\u043e', '\u0436', '\u0430', '\u043b', '\u043e', '\u0432', '\u0430', '\u0442', '\u044a'};
			char[] arabic = {'\u0623', '\u0647', '\u0644', '\u0627', '\u064b', ' ', '\u0648', ' ', '\u0633', '\u0647', '\u0644', '\u0627', '\u064b'};
			Object[] text = new Object[]
			{
				new String(german),
				new String(french),
				new String(portuguese),
				new String(english),
				new String(russian),
				new String(arabic)
			};

			// Create the positions
            Object[] positions = new Object[]
            {
                new Double(51.00), new Double(09.00), new Double(0.0),
                new Double(46.00), new Double(02.00), new Double(0.0),
                new Double(39.50), new Double(-08.00), new Double(0.0),
                new Double(38.00), new Double(-97.00), new Double(0.0),
                new Double(60.00), new Double(100.00), new Double(0.0),
                new Double(25.00), new Double(45.00), new Double(0.0)
            };

			IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
			IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();

			IAgStkGraphicsGraphicsFontFactory fontFactory = null;
			fontFactory = sceneManager.getInitializers().getGraphicsFont();

			IAgStkGraphicsGraphicsFont font = null;
			font = fontFactory.initializeWithNameSizeFontStyleOutline("Unicode", 12, AgEStkGraphicsFontStyle.E_STK_GRAPHICS_FONT_STYLE_BOLD, true);
            
			IAgStkGraphicsTextBatchPrimitiveFactory tbpFactory = null;
			tbpFactory = sceneManager.getInitializers().getTextBatchPrimitive();

			IAgStkGraphicsTextBatchPrimitive textBatch = null;
			textBatch = tbpFactory.initializeWithGraphicsFont(font);

			textBatch.setCartographic("Earth", positions, text);

            sceneManager.getPrimitives().add((IAgStkGraphicsPrimitive)textBatch);
            //#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)textBatch;
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
			if(m_Primitive != null)
			{
	            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.getCurrentScenario()).getSceneManager();
	            Object[] center = (Object[])m_Primitive.getBoundingSphere().getCenter_AsObject();
	            IAgStkGraphicsBoundingSphere boundingSphere = null;
	            boundingSphere = manager.getInitializers().getBoundingSphere().initializeDefault(
	                center, m_Primitive.getBoundingSphere().getRadius() * 2);
	
	            ViewHelper.viewBoundingSphere(root, scene, "Earth", boundingSphere);
	            scene.render();
			}
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
            
	        scene.render();
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
	}
}