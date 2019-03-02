import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.basic.*;

public class CloseableTabbedPaneUI
extends BasicTabbedPaneUI
{
	protected Icon	m_closeIcon;
	protected int	m_iconPadding;

	public CloseableTabbedPaneUI()
	{
		this(new ImageIcon(CloseableTabbedPaneUI.class.getResource("close-icon.gif")));
	}

	public CloseableTabbedPaneUI(Icon icon)
	{
		this(icon, icon.getIconHeight() / 2 + 1);
	}

	public CloseableTabbedPaneUI(Icon icon, int padding)
	{
		super();
		if(padding < 0)
			throw new IllegalArgumentException("Invalid icon padding value");

		this.m_closeIcon = icon;
		this.m_iconPadding = padding;
	}

	protected int calculateTabWidth(int tabPlacement, int tabIndex, FontMetrics metrics)
	{
		int result = super.calculateTabWidth(tabPlacement, tabIndex, metrics);
		result += this.m_closeIcon.getIconWidth() + this.m_iconPadding;
		return result;
	}

	protected int calculateTabHeight(int tabPlacement, int tabIndex, int fontHeight)
	{
		int result = super.calculateTabHeight(tabPlacement, tabIndex, fontHeight);
		return Math.max(result, this.m_closeIcon.getIconHeight() + this.m_iconPadding);
	}

	protected void paintTab(Graphics g, int tabPlacement, Rectangle[] rectarray, int tabIndex, Rectangle iconRect, Rectangle textRect)
	{
		super.paintTab(g, tabPlacement, rectarray, tabIndex, iconRect, textRect);

		Rectangle tabRect = rectarray[tabIndex];
		int x = tabRect.x + tabRect.width - this.m_closeIcon.getIconWidth() - (this.m_iconPadding / 2);
		int y = tabRect.y + tabRect.height - this.m_closeIcon.getIconHeight() - (this.m_iconPadding / 2);
		if(this.tabPane.getSelectedIndex() == tabIndex)
		{
			x -= this.m_iconPadding / 2;
			y -= this.m_iconPadding / 2;
		}

		this.m_closeIcon.paintIcon(this.tabPane, g, x, y);
	}

	public Icon getCloseIcon()
	{
		return this.m_closeIcon;
	}

	public void setCloseIcon(Icon icon)
	{
		if(icon == null)
			throw new IllegalArgumentException("Null icons not allowed");

		this.m_closeIcon = icon;
	}

	public int getIconPadding()
	{
		return this.m_iconPadding;
	}

	public void setIconPadding(int padding)
	{
		if(padding < 0)
			throw new IllegalArgumentException("Invalid icon padding value");

		this.m_iconPadding = padding;
	}

	protected void installListeners()
	{
		super.installListeners();
		if(this.tabPane instanceof CloseableTabbedPane)
		{
			this.tabPane.addMouseListener(new MouseAdapter()
			{
				public void mouseReleased(MouseEvent evt)
				{
					checkMouseReleaseForTabClose(evt);
				}
			});
		}
	}

	protected void checkMouseReleaseForTabClose(MouseEvent evt)
	{
		if(evt.isPopupTrigger() || evt.getButton() != MouseEvent.BUTTON1)
			return;

		int x = evt.getX();
		int y = evt.getY();
		int tabIndex = getTabAtLocation(x, y);

		if(tabIndex >= 0)
		{
			Rectangle tabRect = this.getTabBounds(this.tabPane, tabIndex);
			Rectangle iconBounds = new Rectangle();
			iconBounds.x = tabRect.x + tabRect.width - this.m_closeIcon.getIconWidth() - (this.m_iconPadding / 2);
			iconBounds.y = tabRect.y + tabRect.height - this.m_closeIcon.getIconHeight() - (this.m_iconPadding / 2);
			if(this.tabPane.getSelectedIndex() == tabIndex)
			{
				iconBounds.x -= this.m_iconPadding / 2;
				iconBounds.y -= this.m_iconPadding / 2;
			}
			iconBounds.width = this.m_closeIcon.getIconWidth();
			iconBounds.height = this.m_closeIcon.getIconHeight();

			if(iconBounds.contains(x, y))
				((CloseableTabbedPane)this.tabPane).fireTabCloseEvent(tabIndex);
		}
	}

	protected int getTabAtLocation(int x, int y)
	{
		int tabCount = this.tabPane.getTabCount();
		for(int i = 0; i < tabCount; i++)
		{
			if(this.rects[i].contains(x, y))
			{
				return i;
			}
		}
		return -1;
	}
}
