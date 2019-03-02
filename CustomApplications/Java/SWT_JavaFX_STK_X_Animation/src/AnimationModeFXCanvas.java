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

public final class AnimationModeFXCanvas
extends FXCanvas
{
	/* package */AnimationModeButton	m_AnimModeNormalButton;
	/* package */AnimationModeButton	m_AnimModeRealtimeButton;
	/* package */AnimationModeButton	m_AnimModeXRealtimeButton;

	/* package */AnimationModeFXCanvas(Composite parent, int style)
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

				AnimationModeFXCanvas.this.m_AnimModeNormalButton = new AnimationModeButton("Normal", "AgAnimationModeNormal.gif");
				AnimationModeFXCanvas.this.m_AnimModeNormalButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationModeFXCanvas.this.m_AnimModeNormalButton);

				AnimationModeFXCanvas.this.m_AnimModeRealtimeButton = new AnimationModeButton("Realtime", "AgAnimationModeRealtime.gif");
				AnimationModeFXCanvas.this.m_AnimModeRealtimeButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationModeFXCanvas.this.m_AnimModeRealtimeButton);

				AnimationModeFXCanvas.this.m_AnimModeXRealtimeButton = new AnimationModeButton("X Realtime", "AgAnimationModeXRealtime.gif");
				AnimationModeFXCanvas.this.m_AnimModeXRealtimeButton.addEventHandler(ActionEvent.ACTION, handler);
				c.add(AnimationModeFXCanvas.this.m_AnimModeXRealtimeButton);

				Scene scene = new Scene(hbox, width, height, Color.BLACK);
				AnimationModeFXCanvas.this.setScene(scene);
			}
		});
	}
}