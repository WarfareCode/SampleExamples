import java.util.*;

public class TabCloseEvent 
extends EventObject
{
	private static final long serialVersionUID = 10L;
	protected int m_TabIndex;

	public TabCloseEvent(Object src, int tabIndex)
	{
		super(src);
		this.m_TabIndex = tabIndex;
	}

	public int getTabIndex()
	{
		return this.m_TabIndex;
	}
}