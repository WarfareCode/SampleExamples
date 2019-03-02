
import java.util.EventListener;
import javax.swing.Icon;
import javax.swing.JTabbedPane;

public class CloseableTabbedPane extends JTabbedPane
{
	private static final long serialVersionUID = 10L;
	protected CloseableTabbedPaneUI m_paneUI;

	public CloseableTabbedPane()
	{
		super();
		setUI();
	}

	public CloseableTabbedPane(int placement)
	{
		super(placement);
		setUI();
	}

	public CloseableTabbedPane(int placement, int tabLayoutPolicy)
	{
		super(placement, tabLayoutPolicy);
		setUI();
	}

	public CloseableTabbedPane(Icon closeIcon)
	{
		super();
		setUI(closeIcon);
	}

	public CloseableTabbedPane(Icon closeIcon, int placement)
	{
		super(placement);
		setUI(closeIcon);
	}

	public CloseableTabbedPane(Icon closeIcon, int placement, int tabLayoutPolicy)
	{
		super(placement, tabLayoutPolicy);
		setUI(closeIcon);
	}

	public CloseableTabbedPane(Icon closeIcon, int iconPadding, int placement, int tabLayoutPolicy)
	{
		super(placement, tabLayoutPolicy);
		setUI(closeIcon, iconPadding);
	}

	public synchronized void addTabCloseListener(TabCloseListener listener)
	{
		this.listenerList.add(TabCloseListener.class, listener);
	}

	public synchronized void removeTabCloseListener(TabCloseListener listener)
	{
		this.listenerList.add(TabCloseListener.class, listener);
	}

	void fireTabCloseEvent(int tabIndex)
	{
		TabCloseEvent evt = null;
		EventListener[] listeners = getListeners(TabCloseListener.class);
		if (listeners.length > 0)
			 evt = new TabCloseEvent(this, tabIndex);

		for (int i = 0; i < listeners.length; ++i)
		{
			((TabCloseListener)listeners[i]).tabClosed(evt);
		}
	}

	private void setUI()
	{
		this.m_paneUI = new CloseableTabbedPaneUI();
		setUI(this.m_paneUI);
	}

	private void setUI(Icon closeIcon)
	{
		this.m_paneUI = new CloseableTabbedPaneUI(closeIcon);
		setUI(this.m_paneUI);
	}

	private void setUI(Icon closeIcon, int iconPadding)
	{
		this.m_paneUI = new CloseableTabbedPaneUI(closeIcon, iconPadding);
		setUI(this.m_paneUI);
	}
}
