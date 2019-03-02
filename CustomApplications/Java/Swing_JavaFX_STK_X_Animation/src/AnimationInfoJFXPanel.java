// JavaFX API
import javafx.animation.*;
import javafx.application.*;
import javafx.collections.*;
import javafx.embed.swing.*;
import javafx.geometry.*;
import javafx.scene.*;
import javafx.scene.control.*;
import javafx.scene.layout.*;
import javafx.scene.paint.*;
import javafx.util.*;

public class AnimationInfoJFXPanel
extends JFXPanel
{
	private static final long			serialVersionUID	= -9027498277576120111L;

	private HBox						m_HBox;
	private Scene						m_Scene;

	/* package */Label					m_DateLabel;
	/* package */TextField				m_DateTextField;

	/* package */Label					m_TimeLabel;
	/* package */TextField				m_TimeTextField;

	/* package */Label					m_EpochLabel;
	/* package */TextField				m_EpochTextField;

	/* package */Label					m_FPSLabel;
	/* package */TextField				m_FPSTextField;

	/* package */AnimationInfoJFXPanel(java.awt.Color bg, java.awt.Color fg)
	{
		this.setBackground(bg);
		this.setForeground(fg);
	}

	public void initScene(final int width, final int height)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				double startOpacity = 0.0;

				AnimationInfoJFXPanel.this.m_HBox = new HBox();
				AnimationInfoJFXPanel.this.m_HBox.setPadding(new Insets(0, 10, 0, 10));
				AnimationInfoJFXPanel.this.m_HBox.setSpacing(10);
				AnimationInfoJFXPanel.this.m_HBox.setAlignment(Pos.CENTER);

				ObservableList<Node> c = AnimationInfoJFXPanel.this.m_HBox.getChildren();

				java.awt.Color awtfg = AnimationInfoJFXPanel.this.getForeground();
				int fgred = awtfg.getRed();
				int fggreen = awtfg.getGreen();
				int fgblue = awtfg.getBlue();
				Color jfxfg = Color.rgb(fgred, fggreen, fgblue); 
				
				AnimationInfoJFXPanel.this.m_DateLabel = new Label("Date:");
				AnimationInfoJFXPanel.this.m_DateLabel.setOpacity(startOpacity);
				AnimationInfoJFXPanel.this.m_DateLabel.setTextFill(jfxfg);
				c.add(AnimationInfoJFXPanel.this.m_DateLabel);

				AnimationInfoJFXPanel.this.m_DateTextField = new TextField();
				AnimationInfoJFXPanel.this.m_DateTextField.setOpacity(startOpacity);
				c.add(AnimationInfoJFXPanel.this.m_DateTextField);

				AnimationInfoJFXPanel.this.m_TimeLabel = new Label("Time:");
				AnimationInfoJFXPanel.this.m_TimeLabel.setOpacity(startOpacity);
				AnimationInfoJFXPanel.this.m_TimeLabel.setTextFill(jfxfg);
				c.add(AnimationInfoJFXPanel.this.m_TimeLabel);

				AnimationInfoJFXPanel.this.m_TimeTextField = new TextField();
				AnimationInfoJFXPanel.this.m_TimeTextField.setOpacity(startOpacity);
				c.add(AnimationInfoJFXPanel.this.m_TimeTextField);

				AnimationInfoJFXPanel.this.m_EpochLabel = new Label("Epoch:");
				AnimationInfoJFXPanel.this.m_EpochLabel.setOpacity(startOpacity);
				AnimationInfoJFXPanel.this.m_EpochLabel.setTextFill(jfxfg);
				c.add(AnimationInfoJFXPanel.this.m_EpochLabel);

				AnimationInfoJFXPanel.this.m_EpochTextField = new TextField();
				AnimationInfoJFXPanel.this.m_EpochTextField.setOpacity(startOpacity);
				c.add(AnimationInfoJFXPanel.this.m_EpochTextField);

				AnimationInfoJFXPanel.this.m_FPSLabel = new Label("FPS:");
				AnimationInfoJFXPanel.this.m_FPSLabel.setOpacity(startOpacity);
				AnimationInfoJFXPanel.this.m_FPSLabel.setTextFill(jfxfg);
				c.add(AnimationInfoJFXPanel.this.m_FPSLabel);

				AnimationInfoJFXPanel.this.m_FPSTextField = new TextField();
				AnimationInfoJFXPanel.this.m_FPSTextField.setOpacity(startOpacity);
				c.add(AnimationInfoJFXPanel.this.m_FPSTextField);

				int red = AnimationInfoJFXPanel.this.getBackground().getRed();
				int green = AnimationInfoJFXPanel.this.getBackground().getGreen();
				int blue = AnimationInfoJFXPanel.this.getBackground().getBlue();

				Color jfxc = null;
				jfxc = Color.rgb(red, green, blue);

				AnimationInfoJFXPanel.this.m_Scene = new Scene(AnimationInfoJFXPanel.this.m_HBox, width, height, jfxc);
				AnimationInfoJFXPanel.this.setScene(AnimationInfoJFXPanel.this.m_Scene);
			}
		});
	}

	public void showScene()
	{
		fadeOfButtons(0.0, 1.0, this.getHeight(), 0.0, 0.0, 1.0, 0, 2000);
	}

	public void hideScene()
	{
		fadeOfButtons(1.0, 0.0, 0.0, this.getHeight(), 1.0, 0.0, 0, 2000);
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

				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateLabel, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateLabel, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateLabel, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateLabel, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateLabel, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateLabel, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateTextField, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateTextField, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateTextField, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateTextField, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateTextField, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_DateTextField, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeLabel, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeLabel, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeLabel, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeLabel, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeLabel, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeLabel, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeTextField, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeTextField, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeTextField, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeTextField, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeTextField, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_TimeTextField, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochLabel, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochLabel, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochLabel, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochLabel, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochLabel, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochLabel, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochTextField, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochTextField, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochTextField, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochTextField, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochTextField, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_EpochTextField, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSLabel, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSLabel, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSLabel, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSLabel, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSLabel, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSLabel, stopOpacity, stopTime);

				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSTextField, startScale, startTime);
				addScaleKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSTextField, stopScale, stopTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSTextField, startTranslateY, startTime);
				addTranslateKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSTextField, stopTranslateY, stopTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSTextField, startOpacity, startTime);
				addOpacityKeyFrame(timeline, AnimationInfoJFXPanel.this.m_FPSTextField, stopOpacity, stopTime);

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

	public void setDate(final String text)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				AnimationInfoJFXPanel.this.m_DateTextField.setText(text);
			}
		});
	}

	public void setTime(final String text)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				AnimationInfoJFXPanel.this.m_TimeTextField.setText(text);
			}
		});
	}

	public void setEpoch(final String text)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				AnimationInfoJFXPanel.this.m_EpochTextField.setText(text);
			}
		});
	}

	public void setFrameRate(final String text)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				AnimationInfoJFXPanel.this.m_FPSTextField.setText(text);
			}
		});
	}
}