import javax.swing.*;
public class ViewJTabbedPane
	public synchronized void createGlobeTab()
			this.setSelectedComponent(gjp);
			this.setTitleAt(this.getSelectedIndex(), gjp.getTitle());
			this.setSelectedComponent(mjp);
			this.setTitleAt(this.getSelectedIndex(), mjp.getTitle());
		removeTabAt(evt.getTabIndex());