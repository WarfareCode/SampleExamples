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
import javafx.scene.control.Control;
import javafx.scene.layout.*;
import javafx.scene.paint.*;
import javafx.util.Duration;

public final class MapViewJFXPanel
extends JFXPanel
{
	private static final long			serialVersionUID	= 2524416313657990937L;

	private HBox					m_HBox;
	private Scene					m_Scene;

	/* package */MapViewButton			m_MapViewZoomInButton;
	/* package */MapViewButton			m_MapViewZoomOutButton;
	/* package */MapViewToggleButton	m_MapViewAllowPanButton;

	/* package */MapViewJFXPanel(java.awt.Color bg)
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

				MapViewJFXPanel.this.m_HBox = new HBox();
				MapViewJFXPanel.this.m_HBox.setPadding(new Insets(0, 10, 0, 10));
				MapViewJFXPanel.this.m_HBox.setSpacing(10);
				MapViewJFXPanel.this.m_HBox.setAlignment(Pos.CENTER);

				ObservableList<Node> c = MapViewJFXPanel.this.m_HBox.getChildren();

				MapViewJFXPanel.this.m_MapViewZoomInButton = new MapViewButton("Zoom In", "AgMapViewZoomIn.gif");
				MapViewJFXPanel.this.m_MapViewZoomInButton.addEventHandler(ActionEvent.ACTION, handler);
				MapViewJFXPanel.this.m_MapViewZoomInButton.setOpacity(startOpacity);
				c.add(MapViewJFXPanel.this.m_MapViewZoomInButton);

				MapViewJFXPanel.this.m_MapViewZoomOutButton = new MapViewButton("Zoom Out", "AgMapViewZoomOut.gif");
				MapViewJFXPanel.this.m_MapViewZoomOutButton.addEventHandler(ActionEvent.ACTION, handler);
				MapViewJFXPanel.this.m_MapViewZoomOutButton.setOpacity(startOpacity);
				c.add(MapViewJFXPanel.this.m_MapViewZoomOutButton);

				MapViewJFXPanel.this.m_MapViewAllowPanButton = new MapViewToggleButton("Allow Pan", "AgMapViewAllowPan.gif");
				MapViewJFXPanel.this.m_MapViewAllowPanButton.addEventHandler(ActionEvent.ACTION, handler);
				MapViewJFXPanel.this.m_MapViewAllowPanButton.setOpacity(startOpacity);
				c.add(MapViewJFXPanel.this.m_MapViewAllowPanButton);

				int red = MapViewJFXPanel.this.getBackground().getRed();
				int green = MapViewJFXPanel.this.getBackground().getGreen();
				int blue = MapViewJFXPanel.this.getBackground().getBlue();

				Color jfxc = null;
				jfxc = Color.rgb(red, green, blue);

				MapViewJFXPanel.this.m_Scene = new Scene(MapViewJFXPanel.this.m_HBox, width, height, jfxc);
				MapViewJFXPanel.this.setScene(MapViewJFXPanel.this.m_Scene);
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

				addScaleKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomInButton, startScale, startTime);
				addScaleKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomInButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomInButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomInButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomInButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomInButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomOutButton, startScale, startTime);
				addScaleKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomOutButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomOutButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomOutButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomOutButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewZoomOutButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewAllowPanButton, startScale, startTime);
				addScaleKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewAllowPanButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewAllowPanButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewAllowPanButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewAllowPanButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, MapViewJFXPanel.this.m_MapViewAllowPanButton, stopOpacity, stopTime);

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
}