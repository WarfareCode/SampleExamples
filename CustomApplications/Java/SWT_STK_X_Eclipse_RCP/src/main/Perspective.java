package main;

import org.eclipse.ui.*;

public class Perspective
implements IPerspectiveFactory
{
	public void createInitialLayout(IPageLayout layout)
	{
		String editorArea = layout.getEditorArea();
		layout.setEditorAreaVisible(false);
		layout.setFixed(true);
		IFolderLayout folder = layout.createFolder("Views", IPageLayout.TOP, 0.5f, editorArea);
		folder.addPlaceholder(GlobeView.ID + ":*");
		folder.addView(GlobeView.ID);
		folder.addPlaceholder(MapView.ID + ":*");
		folder.addView(MapView.ID);

	}
}
