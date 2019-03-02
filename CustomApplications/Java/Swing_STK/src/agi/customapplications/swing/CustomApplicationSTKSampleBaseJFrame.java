package agi.customapplications.swing;

// Java API
import javax.swing.*;
import java.awt.*;
import java.io.File;
import java.net.*;

// AGI Java API
import agi.core.*;
import agi.stkx.*;
import agi.stkobjects.*;
import agi.swing.*;
import agi.swing.java.properties.*;
import agi.swing.java.runtime.*;
import agi.swing.menus.help.*;
import agi.swing.menus.java.*;
import agi.swing.toolbars.java.properties.*;
import agi.swing.toolbars.java.runtime.*;
import agi.stk.core.swing.*;
import agi.stk.core.swing.scenario.*;
import agi.stk.core.swing.vdf.*;
import agi.stk.core.swing.connect.cmd.*;
import agi.stk.core.swing.licensing.*;
import agi.stk.core.swing.opengl.*;
import agi.stk.core.swing.toolbars.connect.cmd.*;
import agi.stk.core.swing.menus.scenario.*;
import agi.stk.core.swing.menus.tools.*;
import agi.stk.core.swing.menus.vdf.*;

public abstract class CustomApplicationSTKSampleBaseJFrame
extends JFrame
implements IAgScenarioJMenuEventsListener, IAgVDFJMenuEventsListener, IAgToolsJMenuEventsListener, IAgJavaJMenuEventsListener, IAgHelpJMenuEventsListener, IAgConnectCmdJToolBarEventsListener
{
	private static final long						serialVersionUID	= 1L;

	// scenario life cycle data members
	private static AgScenarioJFileChooser			s_AgScenarioJFileChooser;

	// vdf life cycle data members
	private static AgVDFJFileChooser				s_AgVDFJFileChooser;

	static
	{
		try
		{
			String userDirPath = AgSystemPropertiesHelper.getUserDir();
	
			// Scenario lifecycle initialization
			s_AgScenarioJFileChooser = new AgScenarioJFileChooser("Open Scenario", userDirPath);
	
			// VDF lifecycle initialization
			s_AgVDFJFileChooser = new AgVDFJFileChooser("Open Scenario", userDirPath);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
	
	private URL										m_ApplicationDescriptionURL;

	private AgSTKXApplicationClass					m_AgSTKXApplicationClass;
	private AgStkObjectRootClass					m_AgStkObjectRootClass;

	private CustomApplicationSTKSampleBaseJMenuBar	m_CustomApplicationSTKSampleBaseJMenuBar;

	protected CustomApplicationSTKSampleBaseJFrame(URL appDescriptionUrl)
	throws Throwable
	{
		this.m_ApplicationDescriptionURL = appDescriptionUrl;

		// make sure the lightweight popups/menus are drawn over the heavyweight STK/X control
		JPopupMenu.setDefaultLightWeightPopupEnabled(false);

		// make sure the lightweight tooltips are drawn over the heavyweight STK/X control
		ToolTipManager.sharedInstance().setLightWeightPopupEnabled(false);

		this.m_CustomApplicationSTKSampleBaseJMenuBar = new CustomApplicationSTKSampleBaseJMenuBar();
		this.m_CustomApplicationSTKSampleBaseJMenuBar.addScenarioJMenuListener(this);
		this.m_CustomApplicationSTKSampleBaseJMenuBar.addVDFJMenuListener(this);
		this.m_CustomApplicationSTKSampleBaseJMenuBar.addToolsJMenuListener(this);
		this.m_CustomApplicationSTKSampleBaseJMenuBar.addJavaJMenuListener(this);
		this.m_CustomApplicationSTKSampleBaseJMenuBar.addHelpJMenuListener(this);

		this.setJMenuBar(this.m_CustomApplicationSTKSampleBaseJMenuBar);
	}

	protected void setApp(AgSTKXApplicationClass app)
	{
		this.m_AgSTKXApplicationClass = app;
	}

	protected AgSTKXApplicationClass getApp()
	{
		return this.m_AgSTKXApplicationClass;
	}

	protected AgStkObjectRootClass getRoot()
	{
		return this.m_AgStkObjectRootClass;
	}

	protected void setRoot(AgStkObjectRootClass root)
	{
		this.m_AgStkObjectRootClass = root;
	}

	protected void checkStkInitialized()
	throws AgException
	{
		if(this.m_AgSTKXApplicationClass == null) throw new AgException("The STK Engine must be initialized first before this feature can be used.");
		if(this.m_AgStkObjectRootClass == null) throw new AgException("The STK Root must be initialized first before this feature can be used.");
	}
	
	protected CustomApplicationSTKSampleBaseJMenuBar getCustomAppSTKSampleBaseJMenuBar()
	{
		return this.m_CustomApplicationSTKSampleBaseJMenuBar;
	}

	protected static AgScenarioJFileChooser getScenarioJFileChooser()
	{
		return s_AgScenarioJFileChooser;
	}

	protected static AgVDFJFileChooser getVDFJFileChooser()
	{
		return s_AgVDFJFileChooser;
	}

	private final static String	s_GETREPORT_INSTALLINFO_CMD	= "GetReport * \"InstallInfoCon\"";
	private final static String	s_GETREPORT_OPENGL_CMD		= "GetReport / \"OpenGL\"";

	public AgInstallInfoReportData getInstallInfoReport()
	throws AgException
	{
		checkStkInitialized();
		
		AgInstallInfoReportData ii = new AgInstallInfoReportData();

		IAgStkObject scenObj = this.m_AgStkObjectRootClass.getCurrentScenario();

		if(scenObj != null)
		{
			agi.stkutil.IAgExecCmdResult result = null;
			result = this.m_AgStkObjectRootClass.executeCommand(s_GETREPORT_INSTALLINFO_CMD);
			ii.Version = result.getItem(1);
			ii.STKHome = result.getItem(4);
			ii.ConfigDir = result.getItem(7);
			ii.STKDb = result.getItem(10);
		}

		return ii;
	}

	public String getOpenGLReport()
	throws AgException
	{
		checkStkInitialized();
		
		agi.stkutil.IAgExecCmdResult result = null;
		result = this.m_AgStkObjectRootClass.executeCommand(s_GETREPORT_OPENGL_CMD);
		StringBuffer sb = new StringBuffer();
		if(result.getIsSucceeded())
		{
			int count = result.getCount();
			for(int i = 0; i < count; i++)
			{
				sb.append(result.getItem(i));
				sb.append("\r\n");
			}
		}
		else
		{
			sb.append("Command failed");
		}
		return sb.toString();
	}

	public void onScenarioJMenuAction(AgScenarioJMenuEvent evt)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			checkStkInitialized();
			
			int action = evt.getScenarioJMenuAction();
			if(action == AgScenarioJMenuEvent.ACTION_SCENARIO_NEW)
			{
				this.m_AgStkObjectRootClass.closeScenario();
				this.m_AgStkObjectRootClass.newScenario("test");
			}
			else if(action == AgScenarioJMenuEvent.ACTION_SCENARIO_LOAD)
			{
				scenarioLoad();
			}
			else if(action == AgScenarioJMenuEvent.ACTION_SCENARIO_SAVE)
			{
				this.m_AgStkObjectRootClass.saveScenario();
			}
			else if(action == AgScenarioJMenuEvent.ACTION_SCENARIO_SAVEAS)
			{
				scenarioSaveAs();
			}
			else if(action == AgScenarioJMenuEvent.ACTION_SCENARIO_CLOSE)
			{
				this.m_AgStkObjectRootClass.closeScenario();
			}
		}
		catch(Throwable t)
		{
			JOptionPane.showMessageDialog(this, t.getMessage());
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void scenarioSaveAs()
	throws AgException
	{
		AgScenarioJFileChooser chooser = getScenarioJFileChooser();
		if(chooser != null)
		{
			int returnVal = chooser.showSaveDialog(this);
			if(returnVal == JFileChooser.APPROVE_OPTION)
			{
				File file = chooser.getSelectedFile();
				this.m_AgStkObjectRootClass.saveScenarioAs(file.getAbsolutePath());
			}
		}
		else
		{
			throw new AgException("AgScenarioJFileChooser was null");
		}
	}

	public void scenarioLoad()
	throws AgException
	{
		AgScenarioJFileChooser chooser = getScenarioJFileChooser();
		if(chooser != null)
		{
			int returnVal = chooser.showOpenDialog(this);
			if(returnVal == JFileChooser.APPROVE_OPTION)
			{
				File file = chooser.getSelectedFile();
				this.m_AgStkObjectRootClass.closeScenario();
				this.m_AgStkObjectRootClass.loadScenario(file.getAbsolutePath());
			}
		}
		else
		{
			throw new AgException("AgScenarioJFileChooser was null");
		}
	}

	public void onVDFJMenuAction(AgVDFJMenuEvent evt)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			checkStkInitialized();
			
			int action = evt.getVDFJMenuAction();
			if(action == AgVDFJMenuEvent.ACTION_VDF_LOAD)
			{
				vdfLoad();
			}
			else if(action == AgVDFJMenuEvent.ACTION_VDF_LOAD_URL)
			{
				throw new AgException("VDF URL's are currently not supported in this sample");
				// TODO: In next version, add vdf url download code to localPath and then load it using ... 
				//this.m_AgStkObjectRootClass.loadVDF(localPath, password);
			}
			else if(action == AgVDFJMenuEvent.ACTION_VDF_CLOSE)
			{
				this.m_AgStkObjectRootClass.closeScenario();
			}
		}
		catch(Throwable t)
		{
			JOptionPane.showMessageDialog(this, t.getMessage());
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void vdfLoad()
	throws AgException
	{
		String password = "";
		AgVDFJFileChooser chooser = getVDFJFileChooser();
		if(chooser != null)
		{
			int returnVal = chooser.showOpenDialog(this);
			if(returnVal == JFileChooser.APPROVE_OPTION)
			{
				File file = chooser.getSelectedFile();
				this.m_AgStkObjectRootClass.closeScenario();
				this.m_AgStkObjectRootClass.loadVDF(file.getAbsolutePath(), password);
			}
		}
		else
		{
			throw new AgException("AgScenarioJFileChooser was null");
		}
	}

	public void onToolsJMenuAction(AgToolsJMenuEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			checkStkInitialized();
			
			int action = e.getToolsJMenuAction();
			if(action == AgToolsJMenuEvent.ACTION_TOOLS_INSTALLINFOREPORT)
			{
				AgInstallInfoReportData data = null;
				data = getInstallInfoReport();

				AgInstallInfoReportJFrame f = new AgInstallInfoReportJFrame();
				f.getInstallInfoReportJPanel().setInstallInfoReport(data);
				f.setVisible(true);
				f.setLocationRelativeTo(this);
			}
			else if(action == AgToolsJMenuEvent.ACTION_TOOLS_LICENSEREPORT)
			{
				String report = this.m_AgSTKXApplicationClass.getLicensingReport();
				AgLicensingReportJFrame f = new AgLicensingReportJFrame();
				f.getLicensingReportJPanel().setLicensingReport(report);
				f.setVisible(true);
				f.setLocationRelativeTo(this);
			}
			else if(action == AgToolsJMenuEvent.ACTION_TOOLS_OPENGLREPORT)
			{
				String report = getOpenGLReport();
				AgOpenGLReportJFrame f = new AgOpenGLReportJFrame();
				f.getOpenGLReportJPanel().setOpenGLReport(report);
				f.setVisible(true);
				f.setLocationRelativeTo(this);
			}
			else if(action == AgToolsJMenuEvent.ACTION_TOOLS_CONNECTCMD)
			{
				JDialog.setDefaultLookAndFeelDecorated(true);
				AgConnectCmdJFrame f = new AgConnectCmdJFrame();
				f.getConnectCmdJPanel().getConnectCmdJToolBar().addConnectCmdJToolBarListener(this);
				f.setVisible(true);
				f.setLocationRelativeTo(this);
			}
		}
		catch(Throwable t)
		{
			JOptionPane.showMessageDialog(this, t.getMessage());
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void onJavaJMenuAction(AgJavaJMenuEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int action = e.getJavaJMenuAction();
			if(action == AgJavaJMenuEvent.ACTION_JAVA_JREPROPERTIES)
			{
				final AgJavaPropertiesJFrame d = new AgJavaPropertiesJFrame();
				d.setLocationRelativeTo(this);
				d.getJavaPropertiesJToolBar().addJavaPropertiesJToolBarListener(new IAgJavaPropertiesJToolBarEventsListener()
				{
					public void onJavaPropertiesJToolBarAction(AgJavaPropertiesJToolBarEvent e)
					{
						try
						{
							if(e.getJavaPropertiesJToolBarAction() == AgJavaPropertiesJToolBarEvent.ACTION_JAVA_PROPERTIES_REFRESH)
							{
								d.getJavaPropertiesJPanel().refreshProperties();
							}
							else if(e.getJavaPropertiesJToolBarAction() == AgJavaPropertiesJToolBarEvent.ACTION_JAVA_PROPERTIES_SAVEAS)
							{
								d.getJavaPropertiesJPanel().saveProperties();
							}
						}
						catch(Throwable t)
						{
							t.printStackTrace();
						}
					}
				});
				d.setVisible(true);
			}
			else if(action == AgJavaJMenuEvent.ACTION_JAVA_JRERUNTIME)
			{
				final AgJavaRuntimeJFrame f = new AgJavaRuntimeJFrame();
				f.setLocationRelativeTo(this);
				f.getJavaRuntimeJToolBar().addJavaRuntimeJToolBarListener(new IAgJavaRuntimeJToolBarEventsListener()
				{
					public void onJavaRuntimeJToolBarAction(AgJavaRuntimeJToolBarEvent e)
					{
						try
						{
							if(e.getJavaRuntimeJToolBarAction() == AgJavaRuntimeJToolBarEvent.ACTION_JAVA_RUNTIME_REFRESH)
							{
								f.getJavaRuntimeJPanel().refreshRuntime();
							}
							else if(e.getJavaRuntimeJToolBarAction() == AgJavaRuntimeJToolBarEvent.ACTION_JAVA_RUNTIME_SAVEAS)
							{
								f.getJavaRuntimeJPanel().saveRuntime();
							}
						}
						catch(Throwable t)
						{
							t.printStackTrace();
						}
					}
				});
				f.setVisible(true);
			}
		}
		catch(Throwable t)
		{
			JOptionPane.showMessageDialog(this, t.getMessage());
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void onHelpJMenuAction(AgHelpJMenuEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int action = e.getHelpJMenuAction();
			if(action == AgHelpJMenuEvent.ACTION_HELP_ABOUT)
			{
				AgAboutData data = new AgAboutData();
				data.setHtmlUrl(this.m_ApplicationDescriptionURL);
				AgAboutJFrame f = new AgAboutJFrame();
				f.getAboutJPanel().setInfo(data);
				f.setLocationRelativeTo(this);
				f.setVisible(true);
			}
		}
		catch(Throwable t)
		{
			JOptionPane.showMessageDialog(this, t.getMessage());
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void onConnectCmdJToolBarAction(AgConnectCmdJToolBarEvent evt)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			checkStkInitialized();
			
			int action = evt.getConnectCmdJToolBarAction();
			if(action == AgConnectCmdJToolBarEvent.ACTION_CONNECT_COMMAND_EXECUTE)
			{
				String cmd = (String)evt.getConnectCmdData();
				this.m_AgStkObjectRootClass.executeCommand(cmd);
			}
		}
		catch(Throwable t)
		{
			JOptionPane.showMessageDialog(this, t.getMessage());
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}
}