// Java API
import java.io.*;

// JavaFX 2.0 API
import javafx.scene.control.*;
import javafx.scene.image.*;

// AGI Java API
import agi.stk.core.images.animation.mode.*;

public final class AnimationModeButton
extends Button
{
	/* package */AnimationModeButton(String tooltipText, String imageName)
	{
		super();

		// Tooltip
		Tooltip tt = new Tooltip(tooltipText);
		this.setTooltip(tt);

		// Graphic
		InputStream is = AgAnimationModeImageHelper.class.getResourceAsStream(imageName);
		Image i = new Image(is);
		ImageView iv = new ImageView(i);
		this.setGraphic(iv);
	}
}