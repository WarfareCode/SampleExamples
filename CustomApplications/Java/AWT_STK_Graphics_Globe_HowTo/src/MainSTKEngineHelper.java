// Java API
import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkengine.*;
import agi.stkobjects.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkx.swing.*;
import agi.swing.plaf.metal.*;

// Sample Java API
import utils.helpers.*;
import overlays.toolbars.*;

public class MainSTKEngineHelper
{
	private AgSTKXApplicationClass			m_AgSTKXApplicationClass;
	private AgStkObjectRootClass			m_AgStkObjectRootClass;
	private AgGlobeJPanel					m_AgGlobeJPanel;
	private AgStkGraphicsOverlayToolbar		m_Overlay;

	/*package*/ AgSTKXApplicationClass getApp()
	{
		return this.m_AgSTKXApplicationClass;
	}
	
	/*package*/ AgStkObjectRootClass getRoot()
	{
		return this.m_AgStkObjectRootClass;
	}

	/*package*/ AgGlobeJPanel getGlobe()
	{
		return this.m_AgGlobeJPanel;
	}

	/*package*/ void initializeJavaAPI(AgProgressInfoEventsManager handler)
	throws Throwable
	{
		handler.setInfoMessage("AGI Java API Initialization Start...");

		AgAwt_JNI.initialize_AwtDelegate();
		AgStkCustomApplication_JNI.initialize(true); // true parameter allows for smart auto class cast
		AgAwt_JNI.initialize_AwtComponents();
	}
	
	/*package*/ void initializeSTKEngine(AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		handler.setInfoMessage("STK Engine Initialization started...");
		
		this.m_AgSTKXApplicationClass = new AgSTKXApplicationClass();
		this.m_AgSTKXApplicationClass.addIAgSTKXApplicationEvents2
		(
			new IAgSTKXApplicationEvents2()
			{
				public void onAgSTKXApplicationEvent(AgSTKXApplicationEvent e)
				{
					try
					{
						int type = e.getType();
						if(type == AgSTKXApplicationEvent.TYPE_ON_LOG_MESSAGE)
						{
							Object[] params = e.getParams();
							StringBuffer sb = new StringBuffer();
							if(params != null)
							{
								for(int i = 0; i < params.length; i++)
								{
									sb.append(params[i].toString());
									sb.append(" ");
								}
							}
							System.out.println(sb.toString());
						}
					}
					catch(Throwable t)
					{
						t.printStackTrace();
					}
				}
				
			}
		);
	}
	
	/*package*/ void initializeLicensing(Component parent, AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		handler.setInfoMessage("Licensing check started...");
		
		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			JOptionPane.showMessageDialog(parent, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
		{
			String msg = "You do not have the required STK Globe license.  Exiting!";
			JOptionPane.showMessageDialog(parent, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}
	}
	
	/*package*/ void initializeRoot(AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		handler.setInfoMessage("Object Model Root Initialization Started...");

		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();
	}
	
	/*package*/ void initializeScenario(AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		handler.setInfoMessage("Scenario Initialization Started...");

		this.m_AgStkObjectRootClass.newScenario("HowTo");
	}
	
	/*package*/ void initializeUnitPreferences(AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		handler.setInfoMessage("Unit Preferences Initialization Started...");
		
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DateFormat", "epSec");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("TimeUnit", "sec");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DistanceUnit", "m");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("AngleUnit", "deg");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("LongitudeUnit", "deg");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("LatitudeUnit", "deg");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("Percent", "unitValue");
	}
	
	/*package*/ void initializeAnimationDefaults(AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		handler.setInfoMessage("Animation Defaults Initialization Started...");

		STKObjectsHelper.setAnimationDefaults(this.m_AgStkObjectRootClass);
	}

	/*package*/ void initializeAnnotationDefaults(AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		handler.setInfoMessage("Annotation Defaults Initialization Started...");

        try
        {
    		this.m_AgStkObjectRootClass.beginUpdate();

    		this.m_AgStkObjectRootClass.executeCommand("VO * Annotation Time Show Off ShowTimeStep Off");
        	this.m_AgStkObjectRootClass.executeCommand("VO * Annotation Frame Show Off");
        	this.m_AgStkObjectRootClass.executeCommand("VO * Overlay Modify \"AGI_logo_small.ppm\" Show Off");
        }
        catch (Throwable t)
        {
        	handler.setErrorMessage(t.getMessage());
        }
        finally
        {
        	this.m_AgStkObjectRootClass.endUpdate();
        }
	}

	/*package*/ void initializeGlobe(AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		handler.setInfoMessage("Globe Initialization Started...");

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgGlobeJPanel = new AgGlobeJPanel();
		this.m_AgGlobeJPanel.setBorder(new BevelBorder(BevelBorder.LOWERED));
		AgGlobeCntrlClass globe = this.m_AgGlobeJPanel.getControl();
		globe.setBackColor(stkxColor);
		globe.setBackground(awtColor);

		handler.workStopped();
	}

	/*package*/ void initializeOverlayToobar(AgProgressInfoEventsManager handler)
	throws Throwable
	{	
		this.m_Overlay = new AgStkGraphicsOverlayToolbar(this.m_AgStkObjectRootClass, this.m_AgGlobeJPanel.getControl(), AgStkGraphicsOverlayToolbar.DOCK_LOCATION_BOTTOM);
		this.m_AgGlobeJPanel.getControl().addIAgGlobeCntrlEvents(new AgGlobeCntrlEventsAdapter());
	}
	
	private class AgGlobeCntrlEventsAdapter
	implements IAgGlobeCntrlEvents
	{
		public void onAgGlobeCntrlEvent(AgGlobeCntrlEvent e)
		{
			try
			{
				int type = e.getType();

				if(type == AgGlobeCntrlEvent.TYPE_DBL_CLICK)
				{
					if(m_Overlay != null)
					{
						m_Overlay.mouseDoubleClick(m_AgStkObjectRootClass);
					}
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_MOVE)
				{
					Object[] params = e.getParams();
					short button = ((Short)params[0]).shortValue();
					short shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();

					if(m_Overlay != null)
					{
						m_Overlay.mouseMove(m_AgStkObjectRootClass, button, shift, x, y);
					}
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_DOWN)
				{
					Object[] params = e.getParams();
					short button = ((Short)params[0]).shortValue();
					short shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();

					if(m_Overlay != null)
					{
						m_Overlay.mouseDown(m_AgStkObjectRootClass, button, shift, x, y);
					}
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_UP)
				{
					Object[] params = e.getParams();
					short button = ((Short)params[0]).shortValue();
					short shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();

					if(m_Overlay != null)
					{
						m_Overlay.mouseUp(m_AgStkObjectRootClass, button, shift, x, y);
					}
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}
}