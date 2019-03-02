import javax.swing.*;
public class ViewJTabbedPaneextends CloseableTabbedPaneimplements TabCloseListener{	private static final long serialVersionUID = 1L;	protected static final int MAX_VOTABS = 5;	protected static final int MAX_2DTABS = 5;	protected int numVoTabs;	protected int num2DTabs;		public ViewJTabbedPane()	{		this.numVoTabs = 0;		this.num2DTabs = 0;		addTabCloseListener(this);	}
	public synchronized void createGlobeTab()	throws Throwable	{		if (this.numVoTabs < MAX_VOTABS)		{			++this.numVoTabs;			GlobeJPanel gjp = new GlobeJPanel();			this.addTab( gjp.getTitle(), null, gjp, "Globe" );
			this.setSelectedComponent(gjp);
			this.setTitleAt(this.getSelectedIndex(), gjp.getTitle());		}		else		{			notifyUserOfMax( true );		}	}	public synchronized void createMapTab()	throws Throwable	{		if (this.num2DTabs < MAX_2DTABS)		{			++this.num2DTabs;			MapJPanel mjp = new MapJPanel();			this.addTab( mjp.getTitle(), null, mjp, "Map" );
			this.setSelectedComponent(mjp);
			this.setTitleAt(this.getSelectedIndex(), mjp.getTitle());		}		else		{			notifyUserOfMax( false );		}	}	private void notifyUserOfMax( boolean isVO )	{		String message = null;		String title = null;		if( isVO )		{			message = "A maximum of 5 Globe controls are allowed, please close a Globe first!";			title = "Too Many Globe Controls";		}		else		{			message = "A maximum of 5 Map controls are allowed, please close a Map first!";			title = "Too Many Map Controls";		}		JOptionPane.showMessageDialog(this, message, title, JOptionPane.PLAIN_MESSAGE);	}	public synchronized void close()	{		this.numVoTabs = 0;		this.num2DTabs = 0;		this.removeAll();	}	public synchronized void tabClosed(TabCloseEvent evt)	{		int index = evt.getTabIndex();		String title = this.getTitleAt( index );				if ((this.totalTabs() - 1) > index)		{			this.setSelectedIndex(index + 1);		}		else if (this.totalTabs() > 1)		{			this.setSelectedIndex(index - 1);		}		if( title.startsWith("Globe"))		{			--this.numVoTabs;		}		else if( title.startsWith("Map"))		{			--this.num2DTabs;		}
		removeTabAt(evt.getTabIndex());	}		public int totalTabs()	{		return this.num2DTabs + this.numVoTabs;	}}