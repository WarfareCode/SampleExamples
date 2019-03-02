// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;

// AGI Java API
import agi.core.logging.*;
import agi.swing.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stk.ui.core.*;
import agi.stk.ui.application.*;
import agi.stkutil.*;
import agi.stkobjects.*;
import agi.stkobjects.astrogator.*;
import agi.stkgraphics.*;
import agi.stkvgt.*;
import agi.stkx.*;
import agi.stkx.initialization.*;


public class MainWindow
extends MainBaseJFrame
implements ActionListener
{
	private static final long				serialVersionUID	= 1L;

	private final static String				s_TITLE				= "Automation_Swing_STK_CustomApp_Integration";
	private final static String				s_DESCFILENAME		= "AppDescription.html";

	private StkDesktopJPanel				m_StkDesktopJPanel;
	private StkIntegrationWorkFlowJPanel	m_WorkFlowJPanel;
	private StkEngineJPanel					m_StkEngineJPanel;

	public MainWindow()
	throws Throwable
	{
		super(MainWindow.class.getResource(s_DESCFILENAME));

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
		AgCore_JNI.initialize(true);
		AgStkutil_JNI.initialize();
		AgStkobjects_JNI.initialize();
		AgStkobjectsAstrogator_JNI.initialize();
		AgCrdn_JNI.initialize();
		AgStkGraphics_JNI.initialize();
		AgStkx_JNI.initialize();
		AgStkxInitialization_JNI.initialize();
		AgStkUiCore_JNI.initialize();
		AgStkUiApplication_JNI.initialize();
		AgAwt_JNI.initialize_AwtComponents();

		Container c = this.getContentPane();
		c.setLayout(new GridLayout(1, 3));
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

		this.m_StkDesktopJPanel = new StkDesktopJPanel();
		c.add("STK Desktop", this.m_StkDesktopJPanel);

		this.m_WorkFlowJPanel = new StkIntegrationWorkFlowJPanel();
		this.m_WorkFlowJPanel.addActionListener(this);
		c.add("Work Flow", this.m_WorkFlowJPanel);

		this.m_StkEngineJPanel = new StkEngineJPanel();
		c.add("STK Engine", this.m_StkEngineJPanel);

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new AgWindowAdapter());

		this.setSize(1000, 500);
	}

	public void actionPerformed(ActionEvent ae)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_DESKTOP_CREATE_STK_TEXT))
			{
				m_StkDesktopJPanel.startSTKInstance();
				m_WorkFlowJPanel.m_DesktopCreateStkButton.setEnabled(false);
				m_WorkFlowJPanel.m_DesktopCreateScenarioButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_DESKTOP_CREATE_SCENARIO_TEXT))
			{
				m_StkDesktopJPanel.newScenario();
				m_WorkFlowJPanel.m_DesktopCreateScenarioButton.setEnabled(false);
				m_WorkFlowJPanel.m_EngineCreateScenarioButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_ENGINE_CREATE_SCENARIO_TEXT))
			{
				m_StkEngineJPanel.newScenario();
				m_WorkFlowJPanel.m_EngineCreateScenarioButton.setEnabled(false);
				m_WorkFlowJPanel.m_DesktopExportFacilityButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_DESKTOP_EXPORT_FACILITY_TEXT))
			{
				m_StkDesktopJPanel.exportFacility();
				m_WorkFlowJPanel.m_DesktopExportFacilityButton.setEnabled(false);
				m_WorkFlowJPanel.m_EngineImportFacilityButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_ENGINE_IMPORT_FACILITY_TEXT))
			{
				m_StkEngineJPanel.importFacility();
				m_WorkFlowJPanel.m_EngineImportFacilityButton.setEnabled(false);
				m_WorkFlowJPanel.m_DesktopSaveScenarioButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_DESKTOP_SAVE_SCENARIO_TEXT))
			{
				m_StkDesktopJPanel.saveScenario();
				m_WorkFlowJPanel.m_DesktopSaveScenarioButton.setEnabled(false);
				m_WorkFlowJPanel.m_EngineSaveScenarioButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_ENGINE_SAVE_SCENARIO_TEXT))
			{
				m_StkEngineJPanel.saveScenario();
				m_WorkFlowJPanel.m_EngineSaveScenarioButton.setEnabled(false);
				m_WorkFlowJPanel.m_DesktopCloseScenarioButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_DESKTOP_CLOSE_SCENARIO_TEXT))
			{
				m_StkDesktopJPanel.closeScenario();
				m_WorkFlowJPanel.m_DesktopCloseScenarioButton.setEnabled(false);
				m_WorkFlowJPanel.m_EngineCloseScenarioButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_ENGINE_CLOSE_SCENARIO_TEXT))
			{
				m_StkEngineJPanel.closeScenario();
				m_WorkFlowJPanel.m_EngineCloseScenarioButton.setEnabled(false);
				m_WorkFlowJPanel.m_DesktopReleaseStkButton.setEnabled(true);
			}
			else if(ae.getActionCommand().equalsIgnoreCase(StkIntegrationWorkFlowJPanel.s_DESKTOP_RELEASE_STK_TEXT))
			{
				m_StkDesktopJPanel.releaseSTKInstance();
				m_WorkFlowJPanel.m_DesktopReleaseStkButton.setEnabled(false);
				m_WorkFlowJPanel.m_DesktopCreateStkButton.setEnabled(true);
			}
		}
		catch(AgCoreException e)
		{
			e.printHexHresult();
			e.printStackTrace();
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	class AgWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				m_StkDesktopJPanel.closeConnectionToSTK();

				AgAwt_JNI.uninitialize_AwtComponents();
				AgStkUiApplication_JNI.uninitialize();
				AgStkUiCore_JNI.uninitialize();
				AgStkxInitialization_JNI.uninitialize();
				AgStkx_JNI.uninitialize();
				AgStkGraphics_JNI.uninitialize();
				AgCrdn_JNI.uninitialize();
				AgStkobjectsAstrogator_JNI.uninitialize();
				AgStkobjects_JNI.uninitialize();
				AgStkutil_JNI.uninitialize();
				AgCore_JNI.uninitialize();
				AgAwt_JNI.uninitialize_AwtDelegate();

				System.runFinalization(); // Tell the JVM it should finalize classes.
				System.gc(); // Tell the JVM it should garbage collect.
				System.exit(0); // Tell the JVM to exit successfully.
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

}