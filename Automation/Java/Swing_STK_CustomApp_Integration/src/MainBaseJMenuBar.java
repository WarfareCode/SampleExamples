//Java
import javax.swing.*;

//AGI Java API
import agi.swing.menus.help.*;
import agi.swing.menus.java.*;

public class MainBaseJMenuBar
extends JMenuBar
{
	private static final long	serialVersionUID	= 1L;

	public AgJavaJMenu							m_AgJavaJMenu;
	public AgHelpJMenu							m_AgHelpJMenu;

	public MainBaseJMenuBar()
	{
		JPopupMenu.setDefaultLightWeightPopupEnabled(false);

		this.m_AgJavaJMenu = new AgJavaJMenu();
		this.add(this.m_AgJavaJMenu);

		this.m_AgHelpJMenu = new AgHelpJMenu();
		this.add(this.m_AgHelpJMenu);
	}

	public AgJavaJMenu getJavaJMenu()
	{
		return this.m_AgJavaJMenu;
	}

	public AgHelpJMenu getHelpJMenu()
	{
		return this.m_AgHelpJMenu;
	}

	public void addJavaJMenuListener(IAgJavaJMenuEventsListener l)
	{
		this.m_AgJavaJMenu.addJavaJMenuListener(l);
	}

	public void removeJavaJMenuListener(IAgJavaJMenuEventsListener l)
	{
		this.m_AgJavaJMenu.removeJavaJMenuListener(l);
	}

	public void addHelpJMenuListener(IAgHelpJMenuEventsListener l)
	{
		this.m_AgHelpJMenu.addHelpJMenuListener(l);
	}

	public void removeHelpJMenuListener(IAgHelpJMenuEventsListener l)
	{
		this.m_AgHelpJMenu.removeHelpJMenuListener(l);
	}
}