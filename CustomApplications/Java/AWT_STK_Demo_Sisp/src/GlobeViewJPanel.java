import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;

import agi.stkx.awt.*;
import agi.stk.core.swing.toolbars.animation.*;
import agi.stk.core.swing.toolbars.globe.view.*;
import agi.stk.core.swing.toolbars.map.view.*;

public class GlobeViewJPanel
extends JPanel
{
	private static final long		serialVersionUID	= 1L;

	private GlobeView_JTabbedPane	m_GlobeView_JTabbedPane;
	private AgAnimationJToolBar		m_AgAnimationJToolBar;
	private AgGlobeViewJToolBar		m_AgGlobeViewJToolBar;
	private AgMapViewJToolBar		m_AgMapViewJToolBar;

	public GlobeViewJPanel()
	throws Exception
	{
		this.initialize();
	}

	private void initialize()
	throws Exception
	{
		// Border layout required so the toolbars
		// can be docked to the sides of this JPanel
		// container or made floating.
		this.setLayout(new BorderLayout());

		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder(b1, "Globe View", TitledBorder.LEFT, TitledBorder.ABOVE_TOP);
		this.setBorder(b2);

		this.m_GlobeView_JTabbedPane = new GlobeView_JTabbedPane();
		this.add(this.m_GlobeView_JTabbedPane, BorderLayout.CENTER);

		JPanel toolbars = new JPanel();
		toolbars.setLayout(new FlowLayout());
		
		this.m_AgAnimationJToolBar = new AgAnimationJToolBar();
		toolbars.add(this.m_AgAnimationJToolBar);

		this.m_AgGlobeViewJToolBar = new AgGlobeViewJToolBar(false);
		toolbars.add(this.m_AgGlobeViewJToolBar);
		
		this.m_AgMapViewJToolBar = new AgMapViewJToolBar(false);
		toolbars.add(this.m_AgMapViewJToolBar);
		
		this.add(toolbars, BorderLayout.NORTH);
	}

	public AgGlobeCntrlClass getGlobe()
	{
		return this.m_GlobeView_JTabbedPane.getVO();
	}

	public AgMapCntrlClass getMap()
	{
		return this.m_GlobeView_JTabbedPane.get2D();
	}

	public AgAnimationJToolBar getAnimationBar()
	{
		return this.m_AgAnimationJToolBar;
	}

	public AgGlobeViewJToolBar getGlobeViewBar()
	{
		return this.m_AgGlobeViewJToolBar;
	}

	public AgMapViewJToolBar getMapViewBar()
	{
		return this.m_AgMapViewJToolBar;
	}
}