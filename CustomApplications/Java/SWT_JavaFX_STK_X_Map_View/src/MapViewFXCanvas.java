// JavaFX API
import javafx.application.*;
import javafx.collections.*;
import javafx.embed.swt.*;
import javafx.event.*;
import javafx.geometry.*;
import javafx.scene.*;
import javafx.scene.layout.*;
import javafx.scene.paint.*;

// Eclipse API
import org.eclipse.swt.SWT;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Display;

public final class MapViewFXCanvas
extends FXCanvas
{
	/* package */MapViewButton			m_MapViewZoomInButton;
	/* package */MapViewButton			m_MapViewZoomOutButton;
	/* package */MapViewToggleButton	m_MapViewAllowPanButton;

	/* package */MapViewFXCanvas(Composite parent, int style)
	{
		super(parent, style);

		this.setBackground(Display.getCurrent().getSystemColor(SWT.COLOR_BLACK));
	}

	public void drawScene(final int width, final int height, final EventHandler<ActionEvent> handler)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				HBox hbox = new HBox();
				hbox.setPadding(new Insets(15, 12, 15, 12));
				hbox.setSpacing(10);
				hbox.setAlignment(Pos.CENTER);

				ObservableList<Node> children = hbox.getChildren();

				MapViewFXCanvas.this.m_MapViewZoomInButton = new MapViewButton("Zoom In", "AgMapViewZoomIn.gif");
				MapViewFXCanvas.this.m_MapViewZoomInButton.addEventHandler(ActionEvent.ACTION, handler);
				children.add(MapViewFXCanvas.this.m_MapViewZoomInButton);

				MapViewFXCanvas.this.m_MapViewZoomOutButton = new MapViewButton("Zoom Out", "AgMapViewZoomOut.gif");
				MapViewFXCanvas.this.m_MapViewZoomOutButton.addEventHandler(ActionEvent.ACTION, handler);
				children.add(MapViewFXCanvas.this.m_MapViewZoomOutButton);

				MapViewFXCanvas.this.m_MapViewAllowPanButton = new MapViewToggleButton("Allow Pan", "AgMapViewAllowPan.gif");
				MapViewFXCanvas.this.m_MapViewAllowPanButton.addEventHandler(ActionEvent.ACTION, handler);
				children.add(MapViewFXCanvas.this.m_MapViewAllowPanButton);

				Scene scene = new Scene(hbox, width, height, Color.BLACK);
				MapViewFXCanvas.this.setScene(scene);
			}
		});
	}
}