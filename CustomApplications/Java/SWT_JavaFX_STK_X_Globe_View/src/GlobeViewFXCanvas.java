// JavaFX API
import javafx.application.*;
import javafx.collections.*;
import javafx.embed.swt.*;
import javafx.event.*;
import javafx.geometry.*;
import javafx.scene.*;
import javafx.scene.control.*;
import javafx.scene.layout.*;
import javafx.scene.paint.*;

// Eclipse API
import org.eclipse.swt.SWT;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Display;

public final class GlobeViewFXCanvas
extends FXCanvas
{
	/* package */GlobeViewButton	m_GlobeViewHomeButton;
	/* package */GlobeViewButton	m_GlobeViewOrientFromTopButton;
	/* package */GlobeViewButton	m_GlobeViewOrientNorthButton;
	/* package */GlobeViewButton	m_GlobeViewZoomInButton;
	/* package */GlobeViewButton	m_StoredViewPreviousButton;
	/* package */GlobeViewButton	m_StoredViewNextButton;
	/* package */GlobeViewButton	m_StoredViewViewButton;
	/* package */ChoiceBox<String>	m_StoredViewsChoiceBox;

	/* package */GlobeViewFXCanvas(Composite parent, int style)
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
				hbox.setPadding(new Insets(0,10,0,10));
				hbox.setSpacing(20);
				hbox.setAlignment(Pos.CENTER);

				ObservableList<Node> c = hbox.getChildren();

				GlobeViewFXCanvas.this.m_GlobeViewHomeButton = new GlobeViewButton("Home", "AgGlobeViewHome.gif");
				GlobeViewFXCanvas.this.m_GlobeViewHomeButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(GlobeViewFXCanvas.this.m_GlobeViewHomeButton);

				GlobeViewFXCanvas.this.m_GlobeViewOrientFromTopButton = new GlobeViewButton("Orient From Top", "AgGlobeViewOrientFromTop.gif");
				GlobeViewFXCanvas.this.m_GlobeViewOrientFromTopButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(GlobeViewFXCanvas.this.m_GlobeViewOrientFromTopButton);

				GlobeViewFXCanvas.this.m_GlobeViewOrientNorthButton = new GlobeViewButton("Orient North", "AgGlobeViewOrientNorth.gif");
				GlobeViewFXCanvas.this.m_GlobeViewOrientNorthButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(GlobeViewFXCanvas.this.m_GlobeViewOrientNorthButton);

				GlobeViewFXCanvas.this.m_GlobeViewZoomInButton = new GlobeViewButton("Zoom In", "AgGlobeViewZoomIn.gif");
				GlobeViewFXCanvas.this.m_GlobeViewZoomInButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(GlobeViewFXCanvas.this.m_GlobeViewZoomInButton);

				GlobeViewFXCanvas.this.m_StoredViewPreviousButton = new GlobeViewButton("Previous Stored View", "AgStoredViewPrevious.gif");
				GlobeViewFXCanvas.this.m_StoredViewPreviousButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(GlobeViewFXCanvas.this.m_StoredViewPreviousButton);

				GlobeViewFXCanvas.this.m_StoredViewNextButton = new GlobeViewButton("Next Stored View", "AgStoredViewNext.gif");
				GlobeViewFXCanvas.this.m_StoredViewNextButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(GlobeViewFXCanvas.this.m_StoredViewNextButton);

				GlobeViewFXCanvas.this.m_StoredViewViewButton = new GlobeViewButton("Stored Views", "AgStoredViews.gif");
				GlobeViewFXCanvas.this.m_StoredViewViewButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(GlobeViewFXCanvas.this.m_StoredViewViewButton);

				GlobeViewFXCanvas.this.m_StoredViewsChoiceBox = new ChoiceBox<String>();
				GlobeViewFXCanvas.this.m_StoredViewsChoiceBox.setPrefWidth(200);
				c.add(GlobeViewFXCanvas.this.m_StoredViewsChoiceBox);

				Scene scene = new Scene(hbox, width, height, Color.BLACK);
				GlobeViewFXCanvas.this.setScene(scene);
			}
		});
	}
	
	
	public void addStoredView(final String storedViewName)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				ObservableList<String> svcbc = GlobeViewFXCanvas.this.m_StoredViewsChoiceBox.getItems();
				svcbc.add(storedViewName);
			}
		});
	}

	public void removeStoredView(final String storedViewName)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				ObservableList<String> svcbc = GlobeViewFXCanvas.this.m_StoredViewsChoiceBox.getItems();
				svcbc.remove(storedViewName);
			}
		});
	}

	public void removeStoredViews()
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				ObservableList<String> svcbc = GlobeViewFXCanvas.this.m_StoredViewsChoiceBox.getItems();
				svcbc.clear();
			}
		});
	}

	public void setSelectedStoredView(final String storedViewName)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				SingleSelectionModel<String> model = null;
				model = GlobeViewFXCanvas.this.m_StoredViewsChoiceBox.getSelectionModel();
				model.select(storedViewName);
			}
		});
	}
	
	public String getSelectedStoredView()
	{
		SingleSelectionModel<String> model = null;
		model = GlobeViewFXCanvas.this.m_StoredViewsChoiceBox.getSelectionModel();
		return model.getSelectedItem();
	}

	public String getNextStoredView()
	{
		SingleSelectionModel<String> model = null;
		model = GlobeViewFXCanvas.this.m_StoredViewsChoiceBox.getSelectionModel();
		model.selectNext();
		return model.getSelectedItem();
	}

	public String getPreviousStoredView()
	{
		SingleSelectionModel<String> model = null;
		model = GlobeViewFXCanvas.this.m_StoredViewsChoiceBox.getSelectionModel();
		model.selectPrevious();
		return model.getSelectedItem();
	}
}