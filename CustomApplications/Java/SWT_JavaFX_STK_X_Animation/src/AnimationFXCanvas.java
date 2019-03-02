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

public final class AnimationFXCanvas
extends FXCanvas
{
	/* package */AnimationButton	m_AnimPlayForwardButton;
	/* package */AnimationButton	m_AnimPlayBackwardButton;
	/* package */AnimationButton	m_AnimPauseButton;
	/* package */AnimationButton	m_AnimStepForwardButton;
	/* package */AnimationButton	m_AnimStepBackwardButton;
	/* package */AnimationButton	m_AnimFasterButton;
	/* package */AnimationButton	m_AnimSlowerButton;
	/* package */AnimationButton	m_AnimRewindButton;

	/* package */AnimationFXCanvas(Composite parent, int style)
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
				hbox.setSpacing(5);
				hbox.setAlignment(Pos.CENTER);

				ObservableList<Node> c = hbox.getChildren();

				AnimationFXCanvas.this.m_AnimRewindButton = new AnimationButton("Rewind", "AgAnimationRewind.gif");
				AnimationFXCanvas.this.m_AnimRewindButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationFXCanvas.this.m_AnimRewindButton);

				AnimationFXCanvas.this.m_AnimStepBackwardButton = new AnimationButton("Step Backward", "AgAnimationStepBackward.gif");
				AnimationFXCanvas.this.m_AnimStepBackwardButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationFXCanvas.this.m_AnimStepBackwardButton);

				AnimationFXCanvas.this.m_AnimPlayBackwardButton = new AnimationButton("Play Backward", "AgAnimationPlayBackward.gif");
				AnimationFXCanvas.this.m_AnimPlayBackwardButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationFXCanvas.this.m_AnimPlayBackwardButton);

				AnimationFXCanvas.this.m_AnimPauseButton = new AnimationButton("Pause", "AgAnimationPause.gif");
				AnimationFXCanvas.this.m_AnimPauseButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationFXCanvas.this.m_AnimPauseButton);

				AnimationFXCanvas.this.m_AnimPlayForwardButton = new AnimationButton("Play Forward", "AgAnimationPlayForward.gif");
				AnimationFXCanvas.this.m_AnimPlayForwardButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationFXCanvas.this.m_AnimPlayForwardButton);

				AnimationFXCanvas.this.m_AnimStepForwardButton = new AnimationButton("Step Forward", "AgAnimationStepForward.gif");
				AnimationFXCanvas.this.m_AnimStepForwardButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationFXCanvas.this.m_AnimStepForwardButton);

				AnimationFXCanvas.this.m_AnimSlowerButton = new AnimationButton("Slower", "AgAnimationSlower.gif");
				AnimationFXCanvas.this.m_AnimSlowerButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationFXCanvas.this.m_AnimSlowerButton);

				AnimationFXCanvas.this.m_AnimFasterButton = new AnimationButton("Faster", "AgAnimationFaster.gif");
				AnimationFXCanvas.this.m_AnimFasterButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationFXCanvas.this.m_AnimFasterButton);

				Scene scene = new Scene(hbox, width, height, Color.BLACK);
				AnimationFXCanvas.this.setScene(scene);
			}
		});
	}
}