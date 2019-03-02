// Java API
import java.io.*;

// JavaFX 2.0 API
import javafx.scene.control.*;
import javafx.scene.image.*;

// AGI Java API
import agi.stk.core.images.globe.view.*;

public final class GlobeViewButton
extends Button
{
	/* package */GlobeViewButton(String tooltipText, String imageName)
	{
		super();

		// Tooltip
		Tooltip tt = new Tooltip(tooltipText);
		this.setTooltip(tt);

		// Graphic
		InputStream is = AgGlobeViewImageHelper.class.getResourceAsStream(imageName);
		Image i = new Image(is);
		ImageView iv = new ImageView(i);
		this.setGraphic(iv);
	}
}