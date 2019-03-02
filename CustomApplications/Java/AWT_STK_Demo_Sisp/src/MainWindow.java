// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;

// AGI Java API
import agi.swing.*;
import agi.core.*;
import agi.core.awt.*;
import agi.core.logging.*;
import agi.stkengine.*;
//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow 
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
{
	private static final long serialVersionUID = 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_Demo_Sisp";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

    private SispJSplitPane		m_SispJSplitPane;
	
	public MainWindow()
	throws Throwable
	{
		super(Main.class.getResource(s_DESCFILENAME));

		// ================================================
		// Set the logging level to Level.FINEST to get
		// all AGI java console logging
		// ================================================
		ConsoleHandler ch = new ConsoleHandler();
		ch.setLevel(Level.OFF);
		ch.setFormatter(new AgFormatter());
		Logger.getLogger("agi").setLevel(Level.OFF);
		Logger.getLogger("agi").addHandler(ch);

		// =========================================
		// This must be called before all
		// AWT/Swing/StkUtil/Stkx/StkObjects calls
		// =========================================
		AgAwt_JNI.initialize_AwtDelegate();
		AgStkCustomApplication_JNI.initialize(true); // true parameter allows for smart auto class cast
		AgAwt_JNI.initialize_AwtComponents();

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

		this.m_SispJSplitPane = new SispJSplitPane();
		this.setApp(this.m_SispJSplitPane.getSTKXApplication());
		this.setRoot(this.m_SispJSplitPane.getStkObjectRootClass());
		this.getContentPane().add( this.m_SispJSplitPane, BorderLayout.CENTER );

		//remove unneeded sample menu bar
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		JMenu scenarioJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getScenarioJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(scenarioJMenu);
		JMenu vdfJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getVDFJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(vdfJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

    private class MainWindowAdapter
    extends WindowAdapter
    {
		public void windowClosing( WindowEvent evt )
		{
			try
			{
				((Component)MainWindow.this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

				MainWindow.this.m_SispJSplitPane.getContent().getAnimationView().getGlobe().dispose();
				MainWindow.this.m_SispJSplitPane.getContent().getAnimationView().getMap().dispose();
				
	        	// Reverse of the initialization order
	            AgAwt_JNI.uninitialize_AwtComponents();
	            AgStkCustomApplication_JNI.uninitialize();
	            AgAwt_JNI.uninitialize_AwtDelegate();
			}
			catch(AgCoreException ce)
			{
				ce.printHexHresult();
				ce.printStackTrace();
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
			finally
			{
				((Component)MainWindow.this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
			}
		}
	}
}