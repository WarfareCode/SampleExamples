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

public final class AnimationEndModeFXCanvas
extends FXCanvas
{
	/* package */AnimationEndModeButton	m_AnimEndModeContinuousButton;
	/* package */AnimationEndModeButton	m_AnimEndModeLoopButton;
	/* package */AnimationEndModeButton	m_AnimEndModeNoLoopButton;

	/* package */AnimationEndModeFXCanvas(Composite parent, int style)
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

				AnimationEndModeFXCanvas.this.m_AnimEndModeContinuousButton = new AnimationEndModeButton("Continuous", "AgAnimationEndModeContinuous.gif");
				AnimationEndModeFXCanvas.this.m_AnimEndModeContinuousButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationEndModeFXCanvas.this.m_AnimEndModeContinuousButton);

				AnimationEndModeFXCanvas.this.m_AnimEndModeLoopButton = new AnimationEndModeButton("Loop", "AgAnimationEndModeLoop.gif");
				AnimationEndModeFXCanvas.this.m_AnimEndModeLoopButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationEndModeFXCanvas.this.m_AnimEndModeLoopButton);

				AnimationEndModeFXCanvas.this.m_AnimEndModeNoLoopButton = new AnimationEndModeButton("NoLoop", "AgAnimationEndModeNoLoop.gif");
				AnimationEndModeFXCanvas.this.m_AnimEndModeNoLoopButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationEndModeFXCanvas.this.m_AnimEndModeNoLoopButton);

				Scene scene = new Scene(hbox, width, height, Color.BLACK);
				AnimationEndModeFXCanvas.this.setScene(scene);
			}
		});
	}
}