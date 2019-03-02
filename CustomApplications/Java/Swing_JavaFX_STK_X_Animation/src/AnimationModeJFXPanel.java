// JavaFX API
import javafx.animation.*;
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

public final class AnimationModeJFXPanel
extends JFXPanel
{
	private static final long	serialVersionUID	= -9027498277576120111L;

	private	HBox m_HBox;
	private Scene m_Scene;

	/* package */AnimationModeButton	m_AnimModeNormalButton;
	/* package */AnimationModeButton	m_AnimModeRealtimeButton;
	/* package */AnimationModeButton	m_AnimModeXRealtimeButton;

	/* package */AnimationModeJFXPanel(java.awt.Color bg)
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
				
				AnimationModeJFXPanel.this.m_HBox = new HBox();
				AnimationModeJFXPanel.this.m_HBox.setPadding(new Insets(0, 10, 0, 10));
				AnimationModeJFXPanel.this.m_HBox.setSpacing(10);
				AnimationModeJFXPanel.this.m_HBox.setAlignment(Pos.CENTER);

				ObservableList<Node> c = AnimationModeJFXPanel.this.m_HBox.getChildren();

				AnimationModeJFXPanel.this.m_AnimModeNormalButton = new AnimationModeButton("Normal", "AgAnimationModeNormal.gif");
				AnimationModeJFXPanel.this.m_AnimModeNormalButton.setOpacity(startOpacity);
				AnimationModeJFXPanel.this.m_AnimModeNormalButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationModeJFXPanel.this.m_AnimModeNormalButton);

				AnimationModeJFXPanel.this.m_AnimModeRealtimeButton = new AnimationModeButton("Realtime", "AgAnimationModeRealtime.gif");
				AnimationModeJFXPanel.this.m_AnimModeRealtimeButton.setOpacity(startOpacity);
				AnimationModeJFXPanel.this.m_AnimModeRealtimeButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationModeJFXPanel.this.m_AnimModeRealtimeButton);

				AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton = new AnimationModeButton("X Realtime", "AgAnimationModeXRealtime.gif");
				AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton.setOpacity(startOpacity);
				AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton);

				int red = AnimationModeJFXPanel.this.getBackground().getRed();
				int green = AnimationModeJFXPanel.this.getBackground().getGreen();
				int blue = AnimationModeJFXPanel.this.getBackground().getBlue();
				
				Color jfxc = null;
				jfxc = Color.rgb(red, green, blue);
				
				AnimationModeJFXPanel.this.m_Scene = new Scene(AnimationModeJFXPanel.this.m_HBox, width, height, jfxc);
				AnimationModeJFXPanel.this.setScene(AnimationModeJFXPanel.this.m_Scene);
			}
		});
	}
	
	public void showScene()
	{
		fadeOfButtons(0.0, 1.0, 0.0-this.getHeight()/2, 0.0, 0.0, 1.0, 0, 2000);
	}

	public void hideScene()
	{
		fadeOfButtons(1.0, 0.0, 0.0, 0.0-this.getHeight()/2, 1.0, 0.0, 0, 2000);
	}

	private void fadeOfButtons(final double startScale, final double stopScale, 
								final double startTranslateY, final double stopTranslateY,
								final double startOpacity, final double stopOpacity,
								final long startTime, final long stopTime)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				Timeline timeline = new Timeline();

				addScaleKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeNormalButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeNormalButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeNormalButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeNormalButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeNormalButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeNormalButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeRealtimeButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeRealtimeButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeRealtimeButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeRealtimeButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeRealtimeButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeRealtimeButton, stopOpacity, stopTime);
				
				addScaleKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationModeJFXPanel.this.m_AnimModeXRealtimeButton, stopOpacity, stopTime);

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