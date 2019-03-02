//Java
import javax.swing.*;
import java.awt.*;
import java.net.*;

//AGI Java API
import agi.core.*;
import agi.swing.*;
import agi.swing.java.properties.*;
import agi.swing.java.runtime.*;
import agi.swing.menus.help.*;
import agi.swing.menus.java.*;
import agi.swing.toolbars.java.properties.*;
import agi.swing.toolbars.java.runtime.*;
import agi.stkutil.*;
import agi.stkobjects.*;
import agi.stk.core.swing.*;
import agi.stk.core.swing.toolbars.connect.cmd.*;
import agi.stk.core.swing.connect.cmd.*;
import agi.stk.core.swing.licensing.*;
import agi.stk.core.swing.opengl.*;
import agi.stk.core.swing.menus.tools.*;
import agi.stk.ui.*;

public abstract class MainBaseJFrame
extends JFrame
implements IAgToolsJMenuEventsListener, IAgJavaJMenuEventsListener, IAgHelpJMenuEventsListener, IAgConnectCmdJToolBarEventsListener
{
	private static final long							serialVersionUID	= 1L;

	private URL											m_ApplicationDescriptionURL;

	private AgStkUi										m_AgStkUi;
	private AgStkObjectRootClass						m_AgStkObjectRootClass;
	
	private MainBaseJMenuBar	m_AutomationApplicationSTKSampleBaseJMenuBar;

	protected MainBaseJFrame(URL appDescriptionUrl)
	throws Throwable
	{
		this.m_ApplicationDescriptionURL = appDescriptionUrl;

		// make sure the lightweight popups/menus are drawn over the heavyweight STK/X control
		JPopupMenu.setDefaultLightWeightPopupEnabled(false);

		// make sure the lightweight tooltips are drawn over the heavyweight STK/X control
		ToolTipManager.sharedInstance().setLightWeightPopupEnabled(false);

		this.m_AutomationApplicationSTKSampleBaseJMenuBar = new MainBaseJMenuBar();
		this.m_AutomationApplicationSTKSampleBaseJMenuBar.addJavaJMenuListener(this);
		this.m_AutomationApplicationSTKSampleBaseJMenuBar.addHelpJMenuListener(this);

		this.setJMenuBar(this.m_AutomationApplicationSTKSampleBaseJMenuBar);
	}

	protected void setStkUi(AgStkUi stkui) 
	throws AgCoreException
	{
		this.m_AgStkUi = stkui;
		this.m_AgStkObjectRootClass = (AgStkObjectRootClass)this.m_AgStkUi.getIAgStkObjectRoot();
	}

	protected MainBaseJMenuBar getAutomationAppSTKSampleBaseJMenuBar()
	{
		return this.m_AutomationApplicationSTKSampleBaseJMenuBar;
	}

	private final static String	s_GETREPORT_INSTALLINFO_CMD	= "GetReport * \"InstallInfoCon\"";
	private final static String	s_GETREPORT_OPENGL_CMD		= "GetReport / \"OpenGL\"";

	public static AgInstallInfoReportData getInstallInfoReport(IAgStkObjectRoot root)
	throws AgCoreException
	{
		AgInstallInfoReportData ii = new AgInstallInfoReportData();

		IAgStkObject scenObj = root.getCurrentScenario();

		if(scenObj != null)
		{
			IAgExecCmdResult result = null;
			result = root.executeCommand(s_GETREPORT_INSTALLINFO_CMD);
			ii.Version = result.getItem(1);
			ii.STKHome = result.getItem(4);
			ii.ConfigDir = result.getItem(7);
			ii.STKDb = result.getItem(10);
		}

		return ii;
	}

	public static String getOpenGLReport(IAgStkObjectRoot root)
	throws AgCoreException
	{
		IAgExecCmdResult result = null;
		result = root.executeCommand(s_GETREPORT_OPENGL_CMD);
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

	public void onToolsJMenuAction(AgToolsJMenuEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int action = e.getToolsJMenuAction();
			if(action == AgToolsJMenuEvent.ACTION_TOOLS_INSTALLINFOREPORT)
			{
				AgInstallInfoReportData data = null;
				data = getInstallInfoReport(this.m_AgStkObjectRootClass);

				AgInstallInfoReportJFrame f = new AgInstallInfoReportJFrame();
				f.getInstallInfoReportJPanel().setInstallInfoReport(data);
				f.setVisible(true);
				f.setLocationRelativeTo(this);
			}
			else if(action == AgToolsJMenuEvent.ACTION_TOOLS_LICENSEREPORT)
			{
				String report = this.m_AgStkUi.getIAgStkObjectRoot().getLicensingReport();
				AgLicensingReportJFrame f = new AgLicensingReportJFrame();
				f.getLicensingReportJPanel().setLicensingReport(report);
				f.setVisible(true);
				f.setLocationRelativeTo(this);
			}
			else if(action == AgToolsJMenuEvent.ACTION_TOOLS_OPENGLREPORT)
			{
				String report = getOpenGLReport(this.m_AgStkObjectRootClass);
				AgOpenGLReportJFrame f = new AgOpenGLReportJFrame();
				f.getOpenGLReportJPanel().setOpenGLReport(report);
				f.setVisible(true);
				f.setLocationRelativeTo(this);
			}
			else if(action == AgToolsJMenuEvent.ACTION_TOOLS_CONNECTCMD)
			{
				AgConnectCmdJFrame f = new AgConnectCmdJFrame();
				f.getConnectCmdJPanel().getConnectCmdJToolBar().addConnectCmdJToolBarListener(this);
				f.setVisible(true);
				f.setLocationRelativeTo(this);
			}
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

	public void onJavaJMenuAction(AgJavaJMenuEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int action = e.getJavaJMenuAction();
			if(action == AgJavaJMenuEvent.ACTION_JAVA_JREPROPERTIES)
			{
				final AgJavaPropertiesJFrame f = new AgJavaPropertiesJFrame();
				f.setLocationRelativeTo(this);
				f.getJavaPropertiesJToolBar().addJavaPropertiesJToolBarListener(new IAgJavaPropertiesJToolBarEventsListener()
				{
					public void onJavaPropertiesJToolBarAction(AgJavaPropertiesJToolBarEvent e)
					{
						try
						{
							if(e.getJavaPropertiesJToolBarAction() == AgJavaPropertiesJToolBarEvent.ACTION_JAVA_PROPERTIES_REFRESH)
							{
								f.getJavaPropertiesJPanel().refreshProperties();
							}
							else if(e.getJavaPropertiesJToolBarAction() == AgJavaPropertiesJToolBarEvent.ACTION_JAVA_PROPERTIES_SAVEAS)
							{
								f.getJavaPropertiesJPanel().saveProperties();
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
			t.printStackTrace();
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
			t.printStackTrace();
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

			int action = evt.getConnectCmdJToolBarAction();
			if(action == AgConnectCmdJToolBarEvent.ACTION_CONNECT_COMMAND_EXECUTE)
			{
				String cmd = (String)evt.getConnectCmdData();
				this.m_AgStkObjectRootClass.executeCommand(cmd);
			}
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
}