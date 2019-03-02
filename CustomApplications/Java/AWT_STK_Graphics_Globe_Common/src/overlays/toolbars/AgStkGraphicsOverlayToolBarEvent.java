package overlays.toolbars;

// Java API
import java.util.*;

public class AgStkGraphicsOverlayToolBarEvent
extends EventObject
{
	private static final long	serialVersionUID	= 1L;

	public final static int	ACTION_ANIMATION_REWIND			= 0;
	public final static int	ACTION_ANIMATION_PLAYBACKWARD	= 1;
	public final static int	ACTION_ANIMATION_PLAYFORWARD	= 2;
	public final static int	ACTION_ANIMATION_PAUSE			= 3;
	public final static int	ACTION_ANIMATION_STEPBACKWARD	= 4;
	public final static int	ACTION_ANIMATION_STEPFORWARD	= 5;
	public final static int	ACTION_ANIMATION_FASTER			= 6;
	public final static int	ACTION_ANIMATION_SLOWER			= 7;
	public final static int	ACTION_VIEW_ZOOM				= 8;
	public final static int	ACTION_VIEW_PAN					= 9;
	public final static int	ACTION_VIEW_EARTH				= 10;
	public final static int	ACTION_VIEW_MOON				= 11;
	public final static int	ACTION_DISPLAY					= 12;
	public final static int	ACTION_SCALE					= 13;
	public final static int	ACTION_ROTATE					= 14;

	private int				m_Action;

	/**
	 * @param src The object that created this event.
	 */
	public AgStkGraphicsOverlayToolBarEvent(Object src, int action)
	{
		super(src);
		this.m_Action = action;
	}

	/**
	 * @return One of ACTION_ANIMATION_REWIND, ACTION_ANIMATION_PLAYBACKWARD, ACTION_ANIMATION_PLAYFORWARD, ACTION_ANIMATION_PAUSE, ACTION_ANIMATION_STEPBACKWARD, ACTION_ANIMATION_STEPFORWARD,
	 *         ACTION_ANIMATION_FASTER, ACTION_ANIMATION_SLOWER, ACTION_VIEW_ZOOM, ACTION_VIEW_PAN, ACTION_VIEW_EARTH, ACTION_VIEW_MOON
	 */
	public int getAction()
	{
		return this.m_Action;
	}

	/**
	 * @param mode One of ACTION_ANIMATION_REWIND, ACTION_ANIMATION_PLAYBACKWARD, ACTION_ANIMATION_PLAYFORWARD, ACTION_ANIMATION_PAUSE, ACTION_ANIMATION_STEPBACKWARD, ACTION_ANIMATION_STEPFORWARD,
	 *            ACTION_ANIMATION_FASTER, ACTION_ANIMATION_SLOWER, ACTION_VIEW_ZOOM, ACTION_VIEW_PAN, ACTION_VIEW_EARTH, ACTION_VIEW_MOON
	 * @return String description of the animation which is not guaranteed to be the same across versions, i.e. may not be backward compatible.
	 */
	public static String getActionDescription(int mode)
	{
		String desc = "unknown";

		if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_FASTER)
		{
			desc = "Animation Faster";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PAUSE)
		{
			desc = "Animation Pause";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PLAYBACKWARD)
		{
			desc = "Animation Play Backward";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PLAYFORWARD)
		{
			desc = "Animation Play Forward";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_REWIND)
		{
			desc = "Animation Rewind";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_SLOWER)
		{
			desc = "Animation Slower";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_STEPBACKWARD)
		{
			desc = "Animation Step Backward";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_STEPFORWARD)
		{
			desc = "Animation Step Forward";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_ZOOM)
		{
			desc = "View Zoom";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_PAN)
		{
			desc = "View Pan";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_EARTH)
		{
			desc = "View Earth";
		}
		else if(mode == AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_MOON)
		{
			desc = "View Moon";
		}

		return desc;
	}

	public String toString()
	{
		StringBuffer sb = new StringBuffer();
		sb.append(super.toString());
		sb.append(", action=" + this.getAction());
		sb.append(", actionDescription=" + AgStkGraphicsOverlayToolBarEvent.getActionDescription(this.getAction()));
		return sb.toString();
	}
}