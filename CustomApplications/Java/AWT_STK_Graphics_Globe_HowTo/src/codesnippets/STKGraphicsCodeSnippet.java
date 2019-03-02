package codesnippets;

// Java API
import java.awt.*;
import java.util.ArrayList;

// AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

// Samples API
import agi.samples.sharedresources.codesnippets.*;

public abstract class STKGraphicsCodeSnippet
extends CodeSnippet
{
	private Component	m_Component;

	public STKGraphicsCodeSnippet(Component c, String name, String... fileParts)
	{
		super(name, fileParts);

		this.m_Component = c;
	}

	/**
	 * Executes the code snippet.
	 */
	public abstract void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable;

	/**
	 * Zooms to the area with the example. For example, the Execute() method may turn on clouds and this method may zoom out so the entire globe and clouds are visible.
	 */
	public abstract void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable;

	/**
	 * Removes the effects of the code snippet. For example, if Execute() turned on clouds, this method turns clouds off.
	 */
	public abstract void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable;

	/**
	 * @return The UI component that this codesnippet is display in. AgGlobeCntrl
	 */
	public final Component getComponent()
	{
		return this.m_Component;
	}

	public Dimension measureString(String text, Font font)
	{
		Graphics graphics = this.m_Component.getGraphics();
		// Split string to take into account line breaks
		String[] splitText = text.split("\n");
		int maxWidth = 0;
		try
		{
			graphics.setFont(font);
			for(int i=0; i<splitText.length; i++)
			{
				String textLine = (String)splitText[i];
				int lineWidth = graphics.getFontMetrics().stringWidth(textLine);
				if(lineWidth > maxWidth)
					maxWidth = lineWidth;
			}
			int lineHeight = graphics.getFontMetrics().getAscent() + graphics.getFontMetrics().getDescent();
			int height = lineHeight * splitText.length;
			height += (graphics.getFontMetrics().getDescent() * 2);
			return new Dimension(maxWidth + (graphics.getFontMetrics().getMaxAdvance() * 2), height);
		}
		finally
		{
			graphics.dispose();
		}
	}

	public static void drawString(String text, int x, int y, Font font, Graphics gfx)
	{
		gfx.setFont(font);

		String[] splitText = text.split("\n");
		int lineHeight = gfx.getFontMetrics().getAscent() + gfx.getFontMetrics().getDescent();
		int currentLineY = y + lineHeight;
		
		for(int i=0; i<splitText.length; i++)
		{
			String textLine = (String)splitText[i];
			gfx.drawString(textLine, x, currentLineY);
			currentLineY += lineHeight;
		}
	}
	
    public Object[] convertPositionListToArray(ArrayList<Object[]> positions)
    {
    	int totalCount = positions.size()*3;
    	Object[] array = new Object[totalCount];
        for (int i = 0; i < positions.size(); ++i)
        {
            Object[] position = (Object[])positions.get(i);
            array[i*3] = position[0];
            array[i*3+1] = position[1];
            array[i*3+2] = position[2];
        }
        return array;
    }

    public Object[] convertPositionArraysToArray(Object[] positions)
    {
        Object[] array = new Object[positions.length * 3];
        for (int i = 0; i < positions.length; ++i)
        {
            Object[] position = (Object[])positions[i];
            array[i*3] = position[0];
            array[i*3+1] = position[1];
            array[i*3+2] = position[2];
        }
        return array;
    }
}