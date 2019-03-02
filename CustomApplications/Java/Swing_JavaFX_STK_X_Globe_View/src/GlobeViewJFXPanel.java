// JavaFX API
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.Timeline;
import javafx.application.*;
import javafx.collections.*;
import javafx.embed.swing.*;
import javafx.event.*;
import javafx.geometry.*;
import javafx.scene.*;
import javafx.scene.control.*;
import javafx.scene.layout.*;
import javafx.scene.paint.*;
import javafx.util.Duration;

public final class GlobeViewJFXPanel
extends JFXPanel
{
	private static final long		serialVersionUID	= 2524416313657990937L;

	private HBox					m_HBox;
	private Scene					m_Scene;

	/* package */GlobeViewButton	m_GlobeViewHomeButton;
	/* package */GlobeViewButton	m_GlobeViewOrientFromTopButton;
	/* package */GlobeViewButton	m_GlobeViewOrientNorthButton;
	/* package */GlobeViewButton	m_GlobeViewZoomInButton;
	/* package */GlobeViewButton	m_StoredViewPreviousButton;
	/* package */GlobeViewButton	m_StoredViewNextButton;
	/* package */GlobeViewButton	m_StoredViewViewButton;
	/* package */ChoiceBox<String>	m_StoredViewsChoiceBox;

	/* package */GlobeViewJFXPanel(java.awt.Color bg)
	{
		this.setBackground(bg);
	}

	public void initScene(final int width, final int height, final EventHandler<ActionEvent> handler)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				double startOpacity = 0.0;

				GlobeViewJFXPanel.this.m_HBox = new HBox();
				GlobeViewJFXPanel.this.m_HBox.setPadding(new Insets(0, 10, 0, 10));
				GlobeViewJFXPanel.this.m_HBox.setSpacing(10);
				GlobeViewJFXPanel.this.m_HBox.setAlignment(Pos.CENTER);

				ObservableList<Node> c = GlobeViewJFXPanel.this.m_HBox.getChildren();

				GlobeViewJFXPanel.this.m_GlobeViewHomeButton = new GlobeViewButton("Home", "AgGlobeViewHome.gif");
				GlobeViewJFXPanel.this.m_GlobeViewHomeButton.addEventHandler(ActionEvent.ACTION, handler);
				GlobeViewJFXPanel.this.m_GlobeViewHomeButton.setOpacity(startOpacity);
				c.add(GlobeViewJFXPanel.this.m_GlobeViewHomeButton);

				GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton = new GlobeViewButton("Orient From Top", "AgGlobeViewOrientFromTop.gif");
				GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton.addEventHandler(ActionEvent.ACTION, handler);
				GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton.setOpacity(startOpacity);
				c.add(GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton);

				GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton = new GlobeViewButton("Orient North", "AgGlobeViewOrientNorth.gif");
				GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton.addEventHandler(ActionEvent.ACTION, handler);
				GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton.setOpacity(startOpacity);
				c.add(GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton);

				GlobeViewJFXPanel.this.m_GlobeViewZoomInButton = new GlobeViewButton("Zoom In", "AgGlobeViewZoomIn.gif");
				GlobeViewJFXPanel.this.m_GlobeViewZoomInButton.addEventHandler(ActionEvent.ACTION, handler);
				GlobeViewJFXPanel.this.m_GlobeViewZoomInButton.setOpacity(startOpacity);
				c.add(GlobeViewJFXPanel.this.m_GlobeViewZoomInButton);

				GlobeViewJFXPanel.this.m_StoredViewPreviousButton = new GlobeViewButton("Previous Stored View", "AgStoredViewPrevious.gif");
				GlobeViewJFXPanel.this.m_StoredViewPreviousButton.addEventHandler(ActionEvent.ACTION, handler);
				GlobeViewJFXPanel.this.m_StoredViewPreviousButton.setOpacity(startOpacity);
				c.add(GlobeViewJFXPanel.this.m_StoredViewPreviousButton);

				GlobeViewJFXPanel.this.m_StoredViewNextButton = new GlobeViewButton("Next Stored View", "AgStoredViewNext.gif");
				GlobeViewJFXPanel.this.m_StoredViewNextButton.addEventHandler(ActionEvent.ACTION, handler);
				GlobeViewJFXPanel.this.m_StoredViewNextButton.setOpacity(startOpacity);
				c.add(GlobeViewJFXPanel.this.m_StoredViewNextButton);

				GlobeViewJFXPanel.this.m_StoredViewViewButton = new GlobeViewButton("Stored Views", "AgStoredViews.gif");
				GlobeViewJFXPanel.this.m_StoredViewViewButton.addEventHandler(ActionEvent.ACTION, handler);
				GlobeViewJFXPanel.this.m_StoredViewViewButton.setOpacity(startOpacity);
				c.add(GlobeViewJFXPanel.this.m_StoredViewViewButton);

				GlobeViewJFXPanel.this.m_StoredViewsChoiceBox = new ChoiceBox<String>();
				GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.setPrefWidth(200);
				GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.setOpacity(startOpacity);
				c.add(GlobeViewJFXPanel.this.m_StoredViewsChoiceBox);

				int red = GlobeViewJFXPanel.this.getBackground().getRed();
				int green = GlobeViewJFXPanel.this.getBackground().getGreen();
				int blue = GlobeViewJFXPanel.this.getBackground().getBlue();

				Color jfxc = null;
				jfxc = Color.rgb(red, green, blue);

				GlobeViewJFXPanel.this.m_Scene = new Scene(GlobeViewJFXPanel.this.m_HBox, width, height, jfxc);
				GlobeViewJFXPanel.this.setScene(GlobeViewJFXPanel.this.m_Scene);
			}
		});
	}

	public void showScene()
	{
		fadeOfButtons(0.0, 1.0, 0.0 - this.getHeight() / 2, 0.0, 0.0, 1.0, 0, 2000);
	}

	public void hideScene()
	{
		fadeOfButtons(1.0, 0.0, 0.0, 0.0 - this.getHeight() / 2, 1.0, 0.0, 0, 2000);
	}

	private void fadeOfButtons(final double startScale, final double stopScale, final double startTranslateY, final double stopTranslateY, final double startOpacity, final double stopOpacity,
	final long startTime, final long stopTime)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				Timeline timeline = new Timeline();

				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewHomeButton, startScale, startTime);
				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewHomeButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewHomeButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewHomeButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewHomeButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewHomeButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton, startScale, startTime);
				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientFromTopButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton, startScale, startTime);
				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewOrientNorthButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewZoomInButton, startScale, startTime);
				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewZoomInButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewZoomInButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewZoomInButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewZoomInButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_GlobeViewZoomInButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewPreviousButton, startScale, startTime);
				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewPreviousButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewPreviousButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewPreviousButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewPreviousButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewPreviousButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewNextButton, startScale, startTime);
				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewNextButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewNextButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewNextButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewNextButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewNextButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewViewButton, startScale, startTime);
				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewViewButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewViewButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewViewButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewViewButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewViewButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewsChoiceBox, startScale, startTime);
				addScaleKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewsChoiceBox, stopScale, stopTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewsChoiceBox, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewsChoiceBox, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewsChoiceBox, startOpacity, startTime);
				addOpacityKeyFrame(timeline, GlobeViewJFXPanel.this.m_StoredViewsChoiceBox, stopOpacity, stopTime);

				timeline.play();
			}
		});
	}

	private void addOpacityKeyFrame(Timeline timeline, Control control, double opacityValue, double timeValue)
	{
		KeyValue kv = new KeyValue(control.opacityProperty(), opacityValue);
		KeyFrame kf = new KeyFrame(new Duration(timeValue), kv);
		timeline.getKeyFrames().add(kf);
	}

	private void addTranslateKeyFrame(Timeline timeline, Control control, double translateYValue, double timeValue)
	{
		KeyValue kv = new KeyValue(control.translateYProperty(), translateYValue);
		KeyFrame kf = new KeyFrame(new Duration(timeValue), kv);
		timeline.getKeyFrames().add(kf);
	}

	private void addScaleKeyFrame(Timeline timeline, Control control, double scaleValue, double timeValue)
	{
		KeyValue kv1 = new KeyValue(control.scaleXProperty(), scaleValue);
		KeyValue kv2 = new KeyValue(control.scaleYProperty(), scaleValue);
		KeyFrame kf = new KeyFrame(new Duration(timeValue), kv1, kv2);
		timeline.getKeyFrames().add(kf);
	}

	public void addStoredView(final String storedViewName)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				ObservableList<String> svcbc = GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.getItems();
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
				ObservableList<String> svcbc = GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.getItems();
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
				ObservableList<String> svcbc = GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.getItems();
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
				model = GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.getSelectionModel();
				model.select(storedViewName);
			}
		});
	}

	public String getSelectedStoredView()
	{
		SingleSelectionModel<String> model = null;
		model = GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.getSelectionModel();
		return model.getSelectedItem();
	}

	public String getNextStoredView()
	{
		SingleSelectionModel<String> model = null;
		model = GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.getSelectionModel();
		model.selectNext();
		return model.getSelectedItem();
	}

	public String getPreviousStoredView()
	{
		SingleSelectionModel<String> model = null;
		model = GlobeViewJFXPanel.this.m_StoredViewsChoiceBox.getSelectionModel();
		model.selectPrevious();
		return model.getSelectedItem();
	}
}