package overlays.toolbars;

// Java API
import java.util.*;

/**
 * This interface that must be implemented to recieve AgStkGraphicsOverlayToolbar event notification.
 * An instantiated object that implements this interface must be passed as the parameter to the
 * AgStkGraphicsOverlayToolbar.addAnimationJToolBarListener( AgStkGraphicsOverlayToolbarEventListener l ) and the
 * AgStkGraphicsOverlayToolbar.removeAnimationJToolBarListener( AgStkGraphicsOverlayToolbarEventListener l ) methods to
 * subscribe and unsubscribe from event notification.
 */
public interface IAgStkGraphicsOverlayToolBarEventsListener 
extends EventListener
{
	/**
	 * Called when a AgStkGraphicsOverlayToolbar's button has been clicked/pressed.
	 * @param e provides AgStkGraphicsOverlayToolbar information
	 */
	public abstract void onGraphicsOverlayToolBarAction(AgStkGraphicsOverlayToolBarEvent e);
}