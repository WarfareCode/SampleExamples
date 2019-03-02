package overlays.toolbars;

// Java API
import java.util.*;
import java.awt.*;

// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;
import agi.stkvgt.*;
import agi.stkx.*;
import agi.stkx.awt.*;

// Sample API
import overlays.*;

public class AgStkGraphicsOverlayToolbar
implements IAgStkGraphicsOverlayToolBarEventsListener
{
	public final static int							DOCK_LOCATION_TOP					= 101;
	public final static int							DOCK_LOCATION_BOTTOM				= 102;
	public final static int							DOCK_LOCATION_RIGHT					= 103;
	public final static int							DOCK_LOCATION_LEFT					= 104;

	private final static String						s_CENTRALBODY_NAME_EARTH			= "Earth";
	private final static String						s_CENTRALBODY_NAME_MOON				= "Moon";

	// Members
	private int										m_PanelWidth						= (int)(AgStkGraphicsOverlayButton.DEFAULT_WIDTH * 0.5);
	private int										m_LocationOffset;

	private float									m_PanelTranslucencyRegular			= 0.95f;
	private float									m_PanelTranslucencyPicked			= 0.85f;
	private float									m_PanelTranslucencyClicked			= 0.8f;
	private float									m_PanelBorderTranslucencyRegular	= 0.6f;
	private float									m_PanelBorderTranslucencyPicked		= 0.5f;
	private float									m_PanelBorderTranslucencyClicked	= 0.4f;

	private AgStkGraphicsOverlayButton				m_RotateButton;
	private AgStkGraphicsOverlayButton				m_ScaleButton;
	private AgStkGraphicsOverlayButton				m_StepForwardButton;
	private AgStkGraphicsOverlayButton				m_StepReverseButton;

	private Point									m_AnchorPoint						= new Point();
	private Point									m_RotatePoint						= new Point();
	private Point									m_ScalePoint						= new Point();
	private Point									m_BaseAnchorPoint					= new Point();
	private IAgStkGraphicsTextureScreenOverlay		m_Panel;

	private Rectangle								m_ScaleBounds;
	private double									m_StartScale;

	private boolean									m_PanelTranslucencyChanged;
	private boolean									m_PanelCurrentlyPicked;
	private boolean									m_Visible							= true;
	private boolean									m_Tranforming;
	private boolean									m_Animating							= false;
	private boolean									m_Panning							= false;

	private AgStkGraphicsOverlayButton				m_MouseOverButton;
	private AgStkGraphicsOverlayButton				m_MouseDownButton;
	private boolean									m_MouseDown;
	private Point									m_LastMouseClick;
	private ArrayList<AgStkGraphicsOverlayButton>	m_ButtonHolders;

	private AgStkObjectRootClass					m_Root;
	private AgGlobeCntrlClass						m_Control3D;

	private String									m_CurrentCentralBodyName;

	public IAgStkGraphicsScreenOverlay getOverlay()
	{
		return (IAgStkGraphicsScreenOverlay)this.m_Panel;
	}

	public AgStkGraphicsOverlayToolbar(AgStkObjectRootClass root, AgGlobeCntrlClass control, int dockLocation)
	throws AgCoreException
	{
		this.m_ButtonHolders = new ArrayList<AgStkGraphicsOverlayButton>();

		this.m_Root = root;
		this.m_Control3D = control;

		IAgScenario scenario = (IAgScenario)this.m_Root.getCurrentScenario();
		IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
		IAgStkGraphicsFactoryAndInitializers initrs = manager.getInitializers();
		IAgStkGraphicsTextureScreenOverlayFactory tsof = initrs.getTextureScreenOverlay();

		// Panel
		this.m_Panel = tsof.initializeWithXYWidthHeight(0, 0, this.m_PanelWidth, AgStkGraphicsOverlayButton.DEFAULT_WIDTH);
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;
		overlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_LEFT);
		overlay.setBorderSize(2);
		overlay.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GRAY));
		overlay.setBorderColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GRAY));
		overlay.setTranslucency(this.m_PanelTranslucencyRegular);
		overlay.setBorderTranslucency(this.m_PanelBorderTranslucencyRegular);

		IAgStkGraphicsScreenOverlayCollectionBase managerOverlays = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();
		managerOverlays.add((IAgStkGraphicsScreenOverlay)this.m_Panel);

		addButton("visible.png", "invisible.png", AgStkGraphicsOverlayToolBarEvent.ACTION_DISPLAY);
		addButton("reset.png", "reset.png", AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_REWIND);
		addButton("decreasedelta.png", "decreasedelta.png", AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_SLOWER);
		m_StepReverseButton = addButton("stepreverse.png", "stepreverse.png", AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_STEPBACKWARD);
		addButton("playreverse.png", "playreverse.png", AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PLAYBACKWARD);
		addButton("pause.png", "pause.png", AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PAUSE);
		addButton("playforward.png", "playforward.png", AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PLAYFORWARD);
		m_StepForwardButton = addButton("stepforward.png", "stepforward.png", AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_STEPFORWARD);
		addButton("increasedelta.png", "increasedelta.png", AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_FASTER);
		addButton("zoompressed.png", "zoom.png", AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_ZOOM);
		addButton("panpressed.png", "pan.png", AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_PAN);
		addButton("home.png", "home.png", AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_EARTH);
		addButton("moon.png", "moon.png", AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_MOON);

		IAgStkGraphicsScreenOverlayCollectionBase collectionBase = (IAgStkGraphicsScreenOverlayCollectionBase)overlay.getOverlays();

		// Rotate button
		this.m_RotateButton = new AgStkGraphicsOverlayButton("rotate.png", 0, this.m_PanelWidth, 0.5, 0, this.m_Root);
		IAgStkGraphicsOverlay rotateOverlay = (IAgStkGraphicsOverlay)this.m_RotateButton.getOverlay();
		rotateOverlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_RIGHT);
		collectionBase.add(this.m_RotateButton.getOverlay());
		this.m_ButtonHolders.add(this.m_RotateButton);
		this.m_RotateButton.setEventType(AgStkGraphicsOverlayToolBarEvent.ACTION_ROTATE);

		// Scale button
		this.m_ScaleButton = new AgStkGraphicsOverlayButton("scale.png", 0, this.m_PanelWidth, 0.5, 0, this.m_Root);
		IAgStkGraphicsOverlay scaleOverlay = (IAgStkGraphicsOverlay)this.m_ScaleButton.getOverlay();
		scaleOverlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_RIGHT);
		collectionBase.add(this.m_ScaleButton.getOverlay());
		this.m_ButtonHolders.add(m_ScaleButton);
		this.m_ScaleButton.setEventType(AgStkGraphicsOverlayToolBarEvent.ACTION_SCALE);

		this.m_CurrentCentralBodyName = "Earth";

		dock(dockLocation);
	}

	private void dock(int dockLocation)
	throws AgCoreException
	{
		switch(dockLocation)
		{
			case DOCK_LOCATION_TOP:
			{
				dockTop();
				break;
			}
			case DOCK_LOCATION_BOTTOM:
			{
				dockBottom();
				break;
			}
			case DOCK_LOCATION_LEFT:
			{
				dockLeft();
				break;
			}
			case DOCK_LOCATION_RIGHT:
			{
				dockRight();
				break;
			}
			default:
			{
				dockTop();
				break;
			}
		}
	}

	private void dockRight()
	throws AgCoreException
	{
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;
		overlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER_RIGHT);
		overlay.setRotationAngle(Math.PI / 2);
		orientButtons();
		overlay.setTranslationX(-((overlay.getWidth() / 2) - AgStkGraphicsOverlayButton.DEFAULT_WIDTH / 2) * overlay.getScale());
		this.m_BaseAnchorPoint = new Point((int)overlay.getTranslationX(), (int)overlay.getTranslationY());
	}

	private void dockBottom()
	throws AgCoreException
	{
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;
		overlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_CENTER);
		overlay.setRotationAngle(0);
		orientButtons();
		overlay.setTranslationY(0);
		this.m_BaseAnchorPoint = new Point((int)overlay.getTranslationX(), (int)overlay.getTranslationY());
	}

	private void dockLeft()
	throws AgCoreException
	{
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;
		overlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER_LEFT);
		overlay.setRotationAngle(Math.PI / 2);
		orientButtons();
		overlay.setTranslationX(-((overlay.getWidth() / 2) - AgStkGraphicsOverlayButton.DEFAULT_WIDTH / 2) * overlay.getScale());
		this.m_BaseAnchorPoint = new Point((int)overlay.getTranslationX(), (int)overlay.getTranslationY());
	}

	private void dockTop()
	throws AgCoreException
	{
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;
		overlay.setOrigin(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_CENTER);
		overlay.setRotationAngle(0);
		orientButtons();
		overlay.setTranslationY(0);
		this.m_BaseAnchorPoint = new Point((int)overlay.getTranslationX(), (int)overlay.getTranslationY());
	}

	// Adds a two-way button to the panel
	private AgStkGraphicsOverlayButton addButton(String enabledImageName, String disabledImageName, int eventType)
	throws AgCoreException
	{
		// Update the panel width
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;
		this.m_PanelWidth = this.m_PanelWidth + AgStkGraphicsOverlayButton.DEFAULT_WIDTH;
		overlay.setWidth(this.m_PanelWidth);

		// Create a new button
		AgStkGraphicsOverlayButton b = null;
		b = new AgStkGraphicsOverlayButton(disabledImageName, this.m_LocationOffset, this.m_PanelWidth, this.m_Root);
		b.setTexture(enabledImageName, disabledImageName);
		b.setEventType(eventType);
		b.addIAgStkGraphicsOverlayToolBarEventsListener(this);

		IAgStkGraphicsScreenOverlayCollectionBase collectionBase = (IAgStkGraphicsScreenOverlayCollectionBase)overlay.getOverlays();
		collectionBase.add(b.getOverlay());
		this.m_ButtonHolders.add(b);

		this.m_LocationOffset += AgStkGraphicsOverlayButton.DEFAULT_WIDTH;

		for(int i=0; i<this.m_ButtonHolders.size(); i++)
		{
			AgStkGraphicsOverlayButton button = null;
			button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);
			button.resize(this.m_PanelWidth);
		}

		return b;
	}

	// Removes the panel from the scene manager
	public void remove()
	throws AgCoreException
	{
		IAgStkGraphicsSceneManager manager = null;
		manager = ((IAgScenario)m_Root.getCurrentScenario()).getSceneManager();

		IAgStkGraphicsScreenOverlayCollectionBase collectionBase = null;
		collectionBase = (IAgStkGraphicsScreenOverlayCollectionBase)manager.getScreenOverlays().getOverlays();

		collectionBase.remove((IAgStkGraphicsScreenOverlay)m_Panel);
	}

	// Orients all of the buttons on the Panel so that they do not rotate with the panel,
	// but, rather, flip every 90 degrees in order to remain upright.
	private void orientButtons()
	throws AgCoreException
	{
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
		if((overlay.getRotationAngle() <= -Math.PI / 4) || (overlay.getRotationAngle() > 5 * Math.PI / 4))
		{
			for(int i=0; i<this.m_ButtonHolders.size(); i++)
			{
				AgStkGraphicsOverlayButton button = null;
				button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);

				if(button != m_RotateButton && button != m_ScaleButton)
				{
					IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.getOverlay();
					buttonOverlay.setRotationAngle(Math.PI / 2);
				}
			}
		}
		else if((overlay.getRotationAngle() > -Math.PI / 4) && (overlay.getRotationAngle() <= Math.PI / 4))
		{
			for(int i=0; i<this.m_ButtonHolders.size(); i++)
			{
				AgStkGraphicsOverlayButton button = null;
				button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);

				if(button != m_RotateButton && button != m_ScaleButton)
				{
					IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.getOverlay();
					buttonOverlay.setRotationAngle(0);
				}
			}
		}
		else if((overlay.getRotationAngle() > Math.PI / 4) && (overlay.getRotationAngle() <= 3 * Math.PI / 4))
		{
			for(int i=0; i<this.m_ButtonHolders.size(); i++)
			{
				AgStkGraphicsOverlayButton button = null;
				button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);

				if(button != m_RotateButton && button != m_ScaleButton)
				{
					IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.getOverlay();
					buttonOverlay.setRotationAngle(-Math.PI / 2);
				}
			}
		}
		else if((overlay.getRotationAngle() > 3 * Math.PI / 4) && (overlay.getRotationAngle() <= 5 * Math.PI / 4))
		{
			for(int i=0; i<this.m_ButtonHolders.size(); i++)
			{
				AgStkGraphicsOverlayButton button = null;
				button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);

				if(button != m_RotateButton && button != m_ScaleButton)
				{
					IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.getOverlay();
					buttonOverlay.setRotationAngle(-Math.PI);
				}
			}
		}
	}

	// Finds a button using a pick result
	private AgStkGraphicsOverlayButton findButton(IAgStkGraphicsScreenOverlayPickResultCollection picked)
	throws AgCoreException
	{
		AgStkGraphicsOverlayButton button = null;

		int cnt = picked.getCount();
		for(int i = 0; i < cnt; i++)
		{
			IAgStkGraphicsScreenOverlayPickResult pickResult = null;
			pickResult = picked.getItem(i);

			IAgStkGraphicsScreenOverlay so = null;
			so = pickResult.getOverlay();

			button = findButton(so);

			if(button != null)
			{
				i = cnt;
				break;
			}
		}

		return button;
	}

	// Finds a button using an overlay
	private AgStkGraphicsOverlayButton findButton(IAgStkGraphicsScreenOverlay so)
	{
		AgStkGraphicsOverlayButton result = null;
		for(int i=0; i<this.m_ButtonHolders.size(); i++)
		{
			AgStkGraphicsOverlayButton button = null;
			button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);

			if(button.getOverlay().equals(so))
			{
				result = button;
				break;
			}
		}
		return result;
	}

	// Finds an overlay panel using a pick result
	private boolean overlayPanelPicked(IAgStkGraphicsScreenOverlayPickResultCollection picked)
	throws AgCoreException
	{
		int cnt = picked.getCount();
		for(int i = 0; i < cnt; i++)
		{
			IAgStkGraphicsScreenOverlayPickResult pickResult = null;
			pickResult = picked.getItem(i);

			if(pickResult.getOverlay().equals(this.m_Panel))
			{
				return true;
			}
		}
		return false;
	}

	// Enables/disables the buttons
	private void enableButtons(AgStkGraphicsOverlayButton excludeButton, boolean bPickingEnabled)
	throws AgCoreException
	{
		for(int i=0; i<this.m_ButtonHolders.size(); i++)
		{
			AgStkGraphicsOverlayButton button = null;
			button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);

			if(!button.equals(excludeButton))
			{
				IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.getOverlay();
				buttonOverlay.setPickingEnabled(bPickingEnabled);
			}
		}
	}

	// When the mouse is moved
	public void mouseMove(AgStkObjectRootClass root, short button, short shift, int x, int y)
	throws AgCoreException
	{
		IAgScenario scenario = (IAgScenario)this.m_Root.getCurrentScenario();
		if(scenario != null)
		{
			IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;

			IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
			IAgStkGraphicsSceneCollection scenes = manager.getScenes();
			IAgStkGraphicsScene scene0 = scenes.getItem(0);
	
			IAgStkGraphicsScreenOverlayPickResultCollection picked = null;
			picked = scene0.pickScreenOverlays(x, y);
	
			AgStkGraphicsOverlayButton pickedbutton = findButton(picked);
	
			if(!m_Tranforming)
			{
				if(overlayPanelPicked(picked) && !this.m_PanelCurrentlyPicked)
				{
					overlay.setBorderTranslucency(this.m_PanelBorderTranslucencyPicked);
					overlay.setTranslucency(this.m_PanelTranslucencyPicked);
					this.m_PanelCurrentlyPicked = true;
					this.m_PanelTranslucencyChanged = true;
				}
				else if(!overlayPanelPicked(picked) && this.m_PanelCurrentlyPicked)
				{
					overlay.setBorderTranslucency(this.m_PanelBorderTranslucencyRegular);
					overlay.setTranslucency(this.m_PanelTranslucencyRegular);
					this.m_PanelCurrentlyPicked = false;
					this.m_PanelTranslucencyChanged = true;
				}
				if(this.m_PanelTranslucencyChanged)
				{
					this.m_PanelTranslucencyChanged = false;
					if(!this.m_Animating)
					{
						scene0.render();
					}
				}
			}
	
			if(pickedbutton != null)
			{
				if(this.m_MouseOverButton != null && this.m_MouseOverButton != pickedbutton)
				{
					this.m_MouseOverButton.mouseLeave();
				}
				this.m_MouseOverButton = pickedbutton;
				this.m_MouseOverButton.mouseEnter();
			}
			else
			{
				if(this.m_AnchorPoint.x != 0 || this.m_AnchorPoint.y != 0)
				{
					translateOverlayToolBar(x, y, overlay, scene0);
				}
				else if(this.m_RotatePoint.x != 0 || this.m_RotatePoint.y != 0)
				{
					rotateOverlayToolbar(x, y, overlay, scene0);
				}
				else if(this.m_ScalePoint.x != 0 || this.m_ScalePoint.y != 0)
				{
					scaleOverlayToolbar(x, y, overlay, scene0);
				}
				else if(this.m_MouseOverButton != null)
				{
					this.m_MouseOverButton.mouseLeave();
					this.m_MouseOverButton = null;
				}
			}
		}
	}

	private void translateOverlayToolBar(int x, int y, IAgStkGraphicsOverlay overlay, IAgStkGraphicsScene scene0)
	throws AgCoreException
	{
		int offsetX = (x - this.m_AnchorPoint.x);
		int offsetY = (this.m_AnchorPoint.y - y);

		// This fixes the bug with the ScreenOverlayOrigin being different.
		// Before, if you dragged left with +x to the left, the panel would
		// have gone right.
		if(overlay.getOrigin_AsObject().equals(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_BOTTOM_RIGHT)
		|| overlay.getOrigin_AsObject().equals(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_CENTER_RIGHT)
		|| overlay.getOrigin_AsObject().equals(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_RIGHT))
		{
			overlay.setTranslationX(this.m_BaseAnchorPoint.x - offsetX);
		}
		else
		{
			overlay.setTranslationX(this.m_BaseAnchorPoint.x + offsetX);
		}

		if(overlay.getOrigin_AsObject().equals(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_RIGHT)
		|| overlay.getOrigin_AsObject().equals(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_CENTER)
		|| overlay.getOrigin_AsObject().equals(AgEStkGraphicsScreenOverlayOrigin.E_STK_GRAPHICS_SCREEN_OVERLAY_ORIGIN_TOP_LEFT))
		{
			overlay.setTranslationY(this.m_BaseAnchorPoint.y - offsetY);
		}
		else
		{
			overlay.setTranslationY(this.m_BaseAnchorPoint.y + offsetY);
		}

		if(!this.m_Animating)
		{
			scene0.render();
		}
	}

	private void scaleOverlayToolbar(int x, int y, IAgStkGraphicsOverlay overlay, IAgStkGraphicsScene scene0)
	throws AgCoreException
	{
		// Complete rework of scaling..
		double scale = 1;

		// Get the cos,sin and tan to make this easier to understand.
		double cos = Math.cos(overlay.getRotationAngle());
		double sin = Math.sin(overlay.getRotationAngle());
		double tan = Math.tan(overlay.getRotationAngle());

		double xVector = (x - this.m_ScalePoint.x);
		double yVector = (this.m_ScalePoint.y - y);

		// Get the projection of x and y in the direction
		// of the toolbar's horizontal.
		double newX = ((xVector * cos + yVector * sin) * cos);
		double newY = ((xVector * cos + yVector * sin) * sin);

		// Figure out if we are shrinking or growing the toolbar
		// (This is dependant on the quadrant we are in)
		double magnitude = Math.sqrt(Math.pow(newX, 2) + Math.pow(newY, 2));
		if(sin >= 0 && cos >= 0 && tan >= 0)
		{
			magnitude = (newX < 0 | newY < 0) ? -magnitude : magnitude;
		}
		else if(sin >= 0)
		{
			magnitude = (newX > 0 | newY < 0) ? -magnitude : magnitude;
		}
		else if(tan >= 0)
		{
			magnitude = (newX > 0 | newY > 0) ? -magnitude : magnitude;
		}
		else if(cos >= 0)
		{
			magnitude = (newX < 0 | newY > 0) ? -magnitude : magnitude;
		}

		scale = ((magnitude + this.m_ScaleBounds.getWidth()) / this.m_ScaleBounds.getWidth());

		if(scale < 0)
		{
			scale = 0;
		}

		overlay.setScale(Math.min(Math.max(this.m_StartScale * scale, 0.5), 10));

		if(!this.m_Animating)
		{
			scene0.render();
		}
	}

	private void rotateOverlayToolbar(int x, int y, IAgStkGraphicsOverlay overlay, IAgStkGraphicsScene scene0)
	throws AgCoreException
	{
		Point current = new Point(x, y);
		current.translate(this.m_RotatePoint.x, this.m_RotatePoint.y);
		Object[] bounds = (Object[])overlay.getControlBounds_AsObject();
		Object[] controlPositions = (Object[])overlay.getControlPosition_AsObject();
		double centerX = ((Double)controlPositions[0]).doubleValue() + ((Integer)bounds[2]).intValue() / 2;
		double centerY = ((Double)controlPositions[1]).doubleValue() + ((Integer)bounds[3]).intValue() / 2;
		double adjacent = (x - centerX);
		double opposite = ((this.m_Control3D.getBounds().height - y) - centerY);

		if(adjacent >= 0)
		{
			overlay.setRotationAngle(Math.atan(opposite / adjacent));
		}
		else
		{
			overlay.setRotationAngle(Math.PI + Math.atan(opposite / adjacent));
		}

		orientButtons();

		if(!this.m_Animating)
		{
			scene0.render();
		}
	}

	// When the mouse pressed
	public void mouseDown(AgStkObjectRootClass root, short button, short shift, int x, int y)
	throws AgCoreException
	{
		IAgScenario scenario = (IAgScenario)this.m_Root.getCurrentScenario();
		if(scenario != null)
		{
			IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
			IAgStkGraphicsSceneCollection scenes = manager.getScenes();
			IAgStkGraphicsScene scene0 = scenes.getItem(0);

			IAgStkGraphicsScreenOverlayPickResultCollection picked = null;
			picked = scene0.pickScreenOverlays(x, y);

			AgStkGraphicsOverlayButton pickedbutton = findButton(picked);

			IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;

			if(pickedbutton == null && overlayPanelPicked(picked))
			{
				this.m_MouseDown = true;
				this.m_Control3D.setMouseMode(AgEMouseMode.E_MOUSE_MODE_MANUAL);
				this.m_AnchorPoint = new Point(x, y);

				overlay.setTranslucency(this.m_PanelTranslucencyClicked);
				overlay.setBorderTranslucency(this.m_PanelBorderTranslucencyClicked);

				scene0.render();
			}

			if(pickedbutton != null)
			{
				this.m_MouseDown = true;
				this.m_MouseOverButton = pickedbutton;
				this.m_MouseDownButton = pickedbutton;
				this.m_MouseOverButton.mouseDown();

				if(pickedbutton == this.m_RotateButton)
				{
					this.m_Tranforming = true;
					this.m_Control3D.setMouseMode(AgEMouseMode.E_MOUSE_MODE_MANUAL);
					this.m_RotatePoint = new Point(x, y);
					enableButtons(this.m_RotateButton, false);
				}

				if(pickedbutton == this.m_ScaleButton)
				{
					this.m_Tranforming = true;
					this.m_Control3D.setMouseMode(AgEMouseMode.E_MOUSE_MODE_MANUAL);
					this.m_ScalePoint = new Point(x, y);
					this.m_StartScale = overlay.getScale();
					Object[] bounds = (Object[])overlay.getControlBounds_AsObject();
					int bx = ((Integer)bounds[0]).intValue();
					int by = ((Integer)bounds[1]).intValue();
					int bw = ((Integer)bounds[2]).intValue();
					int bh = ((Integer)bounds[3]).intValue();
					this.m_ScaleBounds = new Rectangle(bx, by, bw, bh);
					enableButtons(this.m_ScaleButton, false);
				}
			}

			this.m_LastMouseClick = new Point(x, y);
		}
	}

	// When the mouse is unpressed
	public void mouseUp(AgStkObjectRootClass root, short button, short shift, int x, int y)
	throws AgCoreException
	{
		if(this.m_MouseDown)
		{
			IAgScenario scenario = (IAgScenario)this.m_Root.getCurrentScenario();
			if(scenario != null)
			{
				IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
				IAgStkGraphicsSceneCollection scenes = manager.getScenes();
				IAgStkGraphicsScene scene0 = scenes.getItem(0);

				IAgStkGraphicsScreenOverlayPickResultCollection picked = null;
				picked = scene0.pickScreenOverlays(x, y);

				AgStkGraphicsOverlayButton pickedbutton = findButton(picked);

				IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;

				if(pickedbutton == null && overlayPanelPicked(picked))
				{
					overlay.setTranslucency(this.m_PanelTranslucencyPicked);
					overlay.setBorderTranslucency(this.m_PanelBorderTranslucencyPicked);
					if(!this.m_Animating)
					{
						scene0.render();
					}
				}

				if(pickedbutton != null)
				{
					this.m_MouseOverButton = pickedbutton;
					this.m_MouseOverButton.mouseUp();

					// Check if this button was under the mouse during both the mouse down and mouse up event
					// (i.e. the button was clicked)
					if(this.m_MouseOverButton == this.m_MouseDownButton)
					{
						this.m_MouseOverButton.mouseClick();
					}
				}

				this.m_AnchorPoint = new Point();
				this.m_RotatePoint = new Point();
				this.m_ScalePoint = new Point();

				enableButtons(null, true);
				this.m_BaseAnchorPoint = new Point((int)overlay.getTranslationX(), (int)overlay.getTranslationY());

				this.m_Tranforming = false;
				this.m_Control3D.setMouseMode(AgEMouseMode.E_MOUSE_MODE_AUTOMATIC);
				this.m_MouseDown = false;
			}
		}
	}

	// When the mouse is double clicked
	public void mouseDoubleClick(AgStkObjectRootClass root)
	throws AgCoreException
	{
		IAgScenario scenario = (IAgScenario)m_Root.getCurrentScenario();
		if(scenario != null)
		{
			IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
			IAgStkGraphicsSceneCollection scenes = manager.getScenes();
			IAgStkGraphicsScene scene0 = scenes.getItem(0);

			IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)this.m_Panel;

			double x = this.m_LastMouseClick.getX();
			double y = this.m_LastMouseClick.getY();

			IAgStkGraphicsScreenOverlayPickResultCollection picked = null;
			picked = scene0.pickScreenOverlays((int)x, (int)y);

			AgStkGraphicsOverlayButton button = findButton(picked);

			if(button == null && overlayPanelPicked(picked))
			{
				overlay.setTranslationX(0D);
				overlay.setTranslationY(0D);

				overlay.setRotationAngle(0D);
				overlay.setScale(1D);
				orientButtons();
			}
		}
	}

	public void onGraphicsOverlayToolBarAction(AgStkGraphicsOverlayToolBarEvent e)
	{
		try
		{
			int action = e.getAction();

			if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_REWIND)
			{
				this.m_Root.rewind();
				this.m_Animating = false;
				enableStepButtons();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_SLOWER)
			{
				this.m_Root.slower();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_STEPBACKWARD)
			{
				this.m_Root.stepBackward();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PLAYBACKWARD)
			{
				disableStepButtons();
				this.m_Root.playBackward();
				this.m_Animating = true;
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PAUSE)
			{
				this.m_Root.pause();
				this.m_Animating = false;
				enableStepButtons();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_PLAYFORWARD)
			{
				disableStepButtons();
				this.m_Root.playForward();
				this.m_Animating = true;
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_STEPFORWARD)
			{
				this.m_Root.stepForward();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ANIMATION_FASTER)
			{
				this.m_Root.faster();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_ZOOM)
			{
				this.m_Control3D.zoomIn();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_PAN)
			{
				handlePan();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_EARTH)
			{
				viewCentralBody(s_CENTRALBODY_NAME_EARTH);
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_VIEW_MOON)
			{
				viewCentralBody(s_CENTRALBODY_NAME_MOON);
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_DISPLAY)
			{
				showHideAction();
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_SCALE)
			{
			}
			else if(action == AgStkGraphicsOverlayToolBarEvent.ACTION_ROTATE)
			{
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void handlePan()
	{
		if(!this.m_Panning)
		{
			try
			{
				this.m_Root.executeCommand("Window3D * InpDevMode Mode PanLLA");
				this.m_Panning = true;
			}
			catch(Exception ex)
			{
				this.m_Panning = false;
			}
		}
		else
		{
			try
			{
				this.m_Root.executeCommand("Window3D * InpDevMode Mode ViewLatLonAlt");
				this.m_Panning = false;
			}
			catch(Exception ex)
			{
				this.m_Panning = true;
			}
		}
	}

	private void viewCentralBody(String centralBodyName)
	throws AgCoreException
	{
		this.m_CurrentCentralBodyName = centralBodyName;

		IAgScenario scenario = (IAgScenario)this.m_Root.getCurrentScenario();
		if(scenario != null)
		{
			IAgStkGraphicsSceneManager manager = scenario.getSceneManager();
			IAgStkGraphicsSceneCollection scenes = manager.getScenes();
			IAgStkGraphicsScene scene0 = scenes.getItem(0);

			IAgStkCentralBodyCollection cbs = this.m_Root.getCentralBodies();
			IAgStkCentralBody cb = cbs.getItem(this.m_CurrentCentralBodyName);
			IAgCrdnProvider cbCrdn = cb.getVgt();
			IAgCrdnSystemGroup cbSystems = cbCrdn.getSystems();
			IAgCrdnSystem cbSystemICRF = cbSystems.getItem("ICRF");
			IAgCrdnSystemAssembled cbSysICRFAssem = (IAgCrdnSystemAssembled)cbSystemICRF;
			IAgCrdnAxesRefTo cbRefAxes = cbSysICRFAssem.getReferenceAxes();
			IAgCrdnAxes cbAxes = cbRefAxes.getAxes();

			IAgStkGraphicsCamera camera = scene0.getCamera();
			camera.viewCentralBody(this.m_CurrentCentralBodyName, cbAxes);

			this.m_Panning = false;
		}
	}
	
	public void showHideAction()
	throws AgCoreException
	{
		IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
		if(this.m_Visible)
		{
			Object[] size = (Object[])overlay.getSize_AsObject();
			double width = ((Double)size[0]).doubleValue();
			double height = ((Double)size[1]).doubleValue();
			double x = overlay.getScale() * ((width / 2) - AgStkGraphicsOverlayButton.DEFAULT_WIDTH / 2);
			double y = overlay.getScale() * ((height / 2) - AgStkGraphicsOverlayButton.DEFAULT_WIDTH / 2);
			double z = Math.sqrt(x * x + y * y);

			m_PanelWidth = AgStkGraphicsOverlayButton.DEFAULT_WIDTH;
			overlay.setWidth(m_PanelWidth);

			((AgStkGraphicsOverlayButton)m_ButtonHolders.get(0)).resize(m_PanelWidth);

			for(int i=0; i<this.m_ButtonHolders.size(); i++)
			{
				AgStkGraphicsOverlayButton button = null;
				button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);

				if(button != m_ButtonHolders.get(0))
				{
					IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.getOverlay();
					buttonOverlay.setTranslucency(1F);
				}
			}

			double transX = overlay.getTranslationX();
			double transY = overlay.getTranslationY();
			overlay.setTranslationX(transX - (int)(z * Math.cos(overlay.getRotationAngle())));
			overlay.setTranslationY(transY - (int)(z * Math.sin(overlay.getRotationAngle())));
		}
		else
		{
			this.m_PanelWidth = (int)((m_ButtonHolders.size() - 1.5) * AgStkGraphicsOverlayButton.DEFAULT_WIDTH);
			overlay.setWidth(m_PanelWidth);
			((AgStkGraphicsOverlayButton)m_ButtonHolders.get(0)).resize(m_PanelWidth);

			Object[] size = (Object[])overlay.getSize_AsObject();
			double width = ((Double)size[0]).doubleValue();
			double height = ((Double)size[1]).doubleValue();
			double x = overlay.getScale() * ((width / 2) - AgStkGraphicsOverlayButton.DEFAULT_WIDTH / 2);
			double y = overlay.getScale() * ((height / 2) - AgStkGraphicsOverlayButton.DEFAULT_WIDTH / 2);
			double z = Math.sqrt(x * x + y * y);

			for(int i=0; i<this.m_ButtonHolders.size(); i++)
			{
				AgStkGraphicsOverlayButton button = null;
				button = (AgStkGraphicsOverlayButton)this.m_ButtonHolders.get(i);

				if(button != m_ButtonHolders.get(0))
				{
					IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.getOverlay();
					buttonOverlay.setTranslucency(0F);
				}
			}

			double transX = overlay.getTranslationX();
			double transY = overlay.getTranslationY();
			overlay.setTranslationX(transX + (int)(z * Math.cos(overlay.getRotationAngle())));
			overlay.setTranslationY(transY + (int)(z * Math.sin(overlay.getRotationAngle())));
		}

		m_Visible = !m_Visible;
	}

	private void enableStepButtons()
	throws AgCoreException
	{
		m_StepForwardButton.setEnabled(true);
		m_StepReverseButton.setEnabled(true);
	}

	private void disableStepButtons()
	throws AgCoreException
	{
		m_StepForwardButton.setEnabled(false);
		m_StepReverseButton.setEnabled(false);
	}
}