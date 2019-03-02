
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

import agi.core.*;
import agi.core.awt.*;
import agi.stkx.awt.*;
import agi.swing.plaf.metal.*;

public class GlobeJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	protected AgGlobeCntrlClass m_AgGlobeCntrlClass;

	public GlobeJPanel()
	throws Throwable
	{
		this.setLayout( new BorderLayout() );

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
		this.m_AgGlobeCntrlClass.setBackground(awtColor);
		this.add( this.m_AgGlobeCntrlClass, BorderLayout.CENTER );

		this.addHierarchyListener( new GlobeJPanelHierarchyListener() );
	}

	public String getTitle()
	throws Throwable
	{
		String globeTitle = "Globe/VO/3D Window ";
		String winID = Integer.toString( this.m_AgGlobeCntrlClass.getWinID() );
		return globeTitle + winID;
	}

	private class GlobeJPanelHierarchyListener
	implements HierarchyListener
	{
		private boolean isParented = false;

		public void hierarchyChanged(HierarchyEvent evt)
		{
			try
			{
				if ((evt.getChangeFlags() & HierarchyEvent.PARENT_CHANGED) ==	HierarchyEvent.PARENT_CHANGED)
				{
					this.isParented = !this.isParented;
					if (!this.isParented)
					{
						GlobeJPanel.this.m_AgGlobeCntrlClass.dispose();
						GlobeJPanel.this.m_AgGlobeCntrlClass = null;
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