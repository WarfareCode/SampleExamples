package main;

import java.util.logging.ConsoleHandler;
import java.util.logging.Level;
import java.util.logging.Logger;

import org.eclipse.jface.resource.*;
import org.eclipse.swt.*;
import org.eclipse.swt.widgets.*;
import org.eclipse.ui.plugin.*;
import org.osgi.framework.*;

import agi.core.*;
import agi.core.swt.*;
import agi.core.logging.*;
import agi.stkobjects.*;
import agi.stkx.*;
import agi.stkengine.*;

/**
 * The activator class controls the plug-in life cycle
 */
public class MainActivator
extends AbstractUIPlugin
{

	private static AgSTKXApplicationClass	s_AgSTKXApplication;
	private static AgStkObjectRootClass		s_AgStkObjectRoot;

	// The plug-in ID
	public static final String				PLUGIN_ID	= "CustomApp_SWT_STK_X_Eclipse_RCP";	//$NON-NLS-1$

	// The shared instance
	private static MainActivator			plugin;

	/**
	 * The constructor
	 */
	public MainActivator()
	{
	}

	/*
	 * (non-Javadoc)
	 * @see org.eclipse.ui.plugin.AbstractUIPlugin#start(org.osgi.framework.BundleContext)
	 */
	public void start(BundleContext context)
	throws Exception
	{
		super.start(context);
		
		AgCore_JNI.xInitThreads();
		
		AgSwt_JNI.initialize_SwtDelegate();
		AgStkCustomApplication_JNI.initialize(true);
		AgSwt_JNI.initialize_SwtComponents();

		// ================================================
		// Set the logging level to Level.FINEST to get
		// all AGI java console logging
		// ================================================
		ConsoleHandler ch = new ConsoleHandler();
		ch.setLevel(Level.OFF);
		ch.setFormatter(new AgFormatter());
		Logger.getLogger("agi").setLevel(Level.OFF);
		Logger.getLogger("agi").addHandler(ch);

		if(s_AgSTKXApplication == null)
		{
			s_AgSTKXApplication = new AgSTKXApplicationClass();

			if(!s_AgSTKXApplication.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
			{
				String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
				MessageBox mb = new MessageBox(Display.getCurrent().getActiveShell(), SWT.ICON_ERROR | SWT.OK);
				mb.setText("License Error");
				mb.setMessage(msg);
				mb.open();
				System.exit(0);
			}

			if(!s_AgSTKXApplication.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
			{
				String msg = "You do not have the required STK Globe license.  The sample's globe will not display properly.";
				MessageBox mb = new MessageBox(Display.getCurrent().getActiveShell(), SWT.ICON_ERROR | SWT.OK);
				mb.setText("License Error");
				mb.setMessage(msg);
				mb.open();
			}
		}

		if(s_AgStkObjectRoot == null)
		{
			s_AgStkObjectRoot = new AgStkObjectRootClass();
		}

		plugin = this;
	}

	/*
	 * (non-Javadoc)
	 * @see org.eclipse.ui.plugin.AbstractUIPlugin#stop(org.osgi.framework.BundleContext)
	 */
	public void stop(BundleContext context)
	throws Exception
	{
		plugin = null;
		super.stop(context);
		if(AgStkCustomApplication_JNI.isInitialized())
		{
			AgSwt_JNI.uninitialize_SwtComponents();
			AgStkCustomApplication_JNI.uninitialize();
			AgSwt_JNI.uninitialize_SwtDelegate();
		}
	}

	/**
	 * Returns the shared instance
	 * 
	 * @return the shared instance
	 */
	public static MainActivator getDefault()
	{
		return plugin;
	}

	/**
	 * Returns an image descriptor for the image file at the given plug-in relative path
	 * 
	 * @param path the path
	 * @return the image descriptor
	 */
	public static ImageDescriptor getImageDescriptor(String path)
	{
		return imageDescriptorFromPlugin(PLUGIN_ID, path);
	}

	public static AgStkObjectRootClass getRoot()
	{
		return s_AgStkObjectRoot;
	}

	public static AgSTKXApplicationClass getSTKXApp()
	{
		return s_AgSTKXApplication;
	}
}
