package codesnippets.primitives.textbatch;

//#region Imports

//Java API
import java.awt.*;
import javax.swing.*;

//AGI Java API
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class TextBatchCodeSnippet
extends STKGraphicsCodeSnippet
{
    private IAgStkGraphicsPrimitive m_Primitive;

    public TextBatchCodeSnippet(Component c)
	{
		super(c, "Draw a set of strings", "primitives", "textbatch", "TextBatchCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		try
		{
			//#region CodeSnippet

			IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
			IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();

            Object[] text = new Object[]
            {
                "Philadelphia",
                "Washington, D.C.",
                "New Orleans",
                "San Jose"
            };

            Object[] positions = new Object[]
            {
                new Double(39.88), new Double(-75.25), new Double(0),    // Philadelphia
                new Double(38.85), new Double(-77.04), new Double(0), // Washington, D.C.   
                new Double(29.98), new Double(-90.25), new Double(0), // New Orleans
                new Double(37.37), new Double(-121.92),new Double(0)    // San Jose
            };

            IAgStkGraphicsGraphicsFont font = sceneManager.getInitializers().getGraphicsFont().initializeWithNameSizeFontStyleOutline("MS Sans Serif", 12, AgEStkGraphicsFontStyle.E_STK_GRAPHICS_FONT_STYLE_BOLD, true);
            IAgStkGraphicsTextBatchPrimitive textBatch = sceneManager.getInitializers().getTextBatchPrimitive().initializeWithGraphicsFont(font);
            ((IAgStkGraphicsPrimitive)textBatch).setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
            textBatch.setOutlineColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
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
			
			scene.render();
		}
		catch(Exception e)
		{
			throw new Throwable(e);
		}
	}
}