// JavaFX API
import javafx.animation.*;
import javafx.application.*;
import javafx.collections.*;
import javafx.embed.swing.*;
import javafx.event.*;
import javafx.geometry.*;
import javafx.scene.*;
import javafx.scene.control.*;
import javafx.scene.layout.*;
import javafx.scene.paint.*;
import javafx.util.*;

public final class AnimationEndModeJFXPanel
extends JFXPanel
{
	private static final long	serialVersionUID	= 2037161968256141091L;

	private	HBox m_HBox;
	private Scene m_Scene;

	/* package */AnimationEndModeButton	m_AnimEndModeContinuousButton;
	/* package */AnimationEndModeButton	m_AnimEndModeLoopButton;
	/* package */AnimationEndModeButton	m_AnimEndModeNoLoopButton;

	/* package */AnimationEndModeJFXPanel(java.awt.Color bg)
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

				AnimationEndModeJFXPanel.this.m_HBox = new HBox();
				AnimationEndModeJFXPanel.this.m_HBox.setPadding(new Insets(0, 10, 0, 10));
				AnimationEndModeJFXPanel.this.m_HBox.setSpacing(10);
				AnimationEndModeJFXPanel.this.m_HBox.setAlignment(Pos.CENTER);

				ObservableList<Node> c = AnimationEndModeJFXPanel.this.m_HBox.getChildren();

				AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton = new AnimationEndModeButton("Continuous", "AgAnimationEndModeContinuous.gif");
				AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton.setOpacity(startOpacity);
				AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton);

				AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton = new AnimationEndModeButton("Loop", "AgAnimationEndModeLoop.gif");
				AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton.setOpacity(startOpacity);
				AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton);

				AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton = new AnimationEndModeButton("NoLoop", "AgAnimationEndModeNoLoop.gif");
				AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton.setOpacity(startOpacity);
				AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton);

				int red = AnimationEndModeJFXPanel.this.getBackground().getRed();
				int green = AnimationEndModeJFXPanel.this.getBackground().getGreen();
				int blue = AnimationEndModeJFXPanel.this.getBackground().getBlue();
				
				Color jfxc = null;
				jfxc = Color.rgb(red, green, blue);
				
				AnimationEndModeJFXPanel.this.m_Scene = new Scene(AnimationEndModeJFXPanel.this.m_HBox, width, height, jfxc);
				AnimationEndModeJFXPanel.this.setScene(AnimationEndModeJFXPanel.this.m_Scene);
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

				addScaleKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeContinuousButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeLoopButton, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationEndModeJFXPanel.this.m_AnimEndModeNoLoopButton, stopOpacity, stopTime);

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