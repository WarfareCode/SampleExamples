import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

import agi.core.*;
import agi.core.awt.*;
import agi.stkx.awt.*;
import agi.swing.plaf.metal.*;

public class MapJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	protected AgMapCntrlClass m_AgMapCntrlClass;

	public MapJPanel()
	throws Throwable
	{
		this.setLayout( new BorderLayout() );

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgMapCntrlClass = new AgMapCntrlClass();
		this.m_AgMapCntrlClass.setBackColor(stkxColor);
		this.m_AgMapCntrlClass.setBackground(awtColor);
		this.add( this.m_AgMapCntrlClass, BorderLayout.CENTER );

		this.addHierarchyListener( new MapJPanelHierarchyListener() );
	}

	public String getTitle()
	throws Throwable
	{
		String mapTitle = "Map/2D Window ";
		String winID = Integer.toString( this.m_AgMapCntrlClass.getWinID() );
		return mapTitle + winID;
	}

	private class MapJPanelHierarchyListener
	implements HierarchyListener
	{
		private boolean isParented = false;

		public void hierarchyChanged(HierarchyEvent evt)
		{
			try
			{
				if ((evt.getChangeFlags() & HierarchyEvent.PARENT_CHANGED) == HierarchyEvent.PARENT_CHANGED)
				{
					this.isParented = !this.isParented;
					if (!this.isParented)
					{
						MapJPanel.this.m_AgMapCntrlClass.dispose();
						MapJPanel.this.m_AgMapCntrlClass = null;
					}
				}
			}
			catch( Throwable t )
			{
				t.printStackTrace();
			}
		}
	}
}