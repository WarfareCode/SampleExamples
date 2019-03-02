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

public final class AnimationJFXPanel
extends JFXPanel
{
	private static final long	serialVersionUID	= 358406993735265428L;

	private	HBox m_HBox;
	private Scene m_Scene;

	/* package */AnimationButton	m_AnimPlayForwardButton;
	/* package */AnimationButton	m_AnimPlayBackwardButton;
	/* package */AnimationButton	m_AnimPauseButton;
	/* package */AnimationButton	m_AnimStepForwardButton;
	/* package */AnimationButton	m_AnimStepBackwardButton;
	/* package */AnimationButton	m_AnimFasterButton;
	/* package */AnimationButton	m_AnimSlowerButton;
	/* package */AnimationButton	m_AnimRewindButton;

	/* package */AnimationJFXPanel(java.awt.Color bg)
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
				
				AnimationJFXPanel.this.m_HBox = new HBox();
				AnimationJFXPanel.this.m_HBox.setPadding(new Insets(0, 10, 0, 10));
				AnimationJFXPanel.this.m_HBox.setSpacing(10);
				AnimationJFXPanel.this.m_HBox.setAlignment(Pos.CENTER);

				ObservableList<Node> c = AnimationJFXPanel.this.m_HBox.getChildren();

				AnimationJFXPanel.this.m_AnimRewindButton = new AnimationButton("Rewind", "AgAnimationRewind.gif");
				AnimationJFXPanel.this.m_AnimRewindButton.setOpacity(startOpacity);
				AnimationJFXPanel.this.m_AnimRewindButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationJFXPanel.this.m_AnimRewindButton);

				AnimationJFXPanel.this.m_AnimStepBackwardButton = new AnimationButton("Step Backward", "AgAnimationStepBackward.gif");
				AnimationJFXPanel.this.m_AnimStepBackwardButton.setOpacity(startOpacity);
				AnimationJFXPanel.this.m_AnimStepBackwardButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationJFXPanel.this.m_AnimStepBackwardButton);

				AnimationJFXPanel.this.m_AnimPlayBackwardButton = new AnimationButton("Play Backward", "AgAnimationPlayBackward.gif");
				AnimationJFXPanel.this.m_AnimPlayBackwardButton.setOpacity(startOpacity);
				AnimationJFXPanel.this.m_AnimPlayBackwardButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationJFXPanel.this.m_AnimPlayBackwardButton);

				AnimationJFXPanel.this.m_AnimPauseButton = new AnimationButton("Pause", "AgAnimationPause.gif");
				AnimationJFXPanel.this.m_AnimPauseButton.setOpacity(startOpacity);
				AnimationJFXPanel.this.m_AnimPauseButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationJFXPanel.this.m_AnimPauseButton);

				AnimationJFXPanel.this.m_AnimPlayForwardButton = new AnimationButton("Play Forward", "AgAnimationPlayForward.gif");
				AnimationJFXPanel.this.m_AnimPlayForwardButton.setOpacity(startOpacity);
				AnimationJFXPanel.this.m_AnimPlayForwardButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationJFXPanel.this.m_AnimPlayForwardButton);

				AnimationJFXPanel.this.m_AnimStepForwardButton = new AnimationButton("Step Forward", "AgAnimationStepForward.gif");
				AnimationJFXPanel.this.m_AnimStepForwardButton.setOpacity(startOpacity);
				AnimationJFXPanel.this.m_AnimStepForwardButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationJFXPanel.this.m_AnimStepForwardButton);

				AnimationJFXPanel.this.m_AnimSlowerButton = new AnimationButton("Slower", "AgAnimationSlower.gif");
				AnimationJFXPanel.this.m_AnimSlowerButton.setOpacity(startOpacity);
				AnimationJFXPanel.this.m_AnimSlowerButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationJFXPanel.this.m_AnimSlowerButton);

				AnimationJFXPanel.this.m_AnimFasterButton = new AnimationButton("Faster", "AgAnimationFaster.gif");
				AnimationJFXPanel.this.m_AnimFasterButton.setOpacity(startOpacity);
				AnimationJFXPanel.this.m_AnimFasterButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationJFXPanel.this.m_AnimFasterButton);

				int red = AnimationJFXPanel.this.getBackground().getRed();
				int green = AnimationJFXPanel.this.getBackground().getGreen();
				int blue = AnimationJFXPanel.this.getBackground().getBlue();
				
				Color jfxc = null;
				jfxc = Color.rgb(red, green, blue);

				AnimationJFXPanel.this.m_Scene = new Scene(AnimationJFXPanel.this.m_HBox, width, height, jfxc);
				AnimationJFXPanel.this.setScene(AnimationJFXPanel.this.m_Scene);
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

				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimRewindButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimRewindButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimRewindButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimRewindButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimRewindButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimRewindButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepBackwardButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepBackwardButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepBackwardButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepBackwardButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepBackwardButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepBackwardButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayBackwardButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayBackwardButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayBackwardButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayBackwardButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayBackwardButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayBackwardButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPauseButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPauseButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPauseButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPauseButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPauseButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPauseButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayForwardButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayForwardButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayForwardButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayForwardButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayForwardButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimPlayForwardButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepForwardButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepForwardButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepForwardButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepForwardButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepForwardButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimStepForwardButton, stopOpacity, stopTime);
				
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimSlowerButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimSlowerButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimSlowerButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimSlowerButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimSlowerButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimSlowerButton, stopOpacity, stopTime);
				
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimFasterButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationJFXPanel.this.m_AnimFasterButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimFasterButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationJFXPanel.this.m_AnimFasterButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimFasterButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationJFXPanel.this.m_AnimFasterButton, stopOpacity, stopTime);

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