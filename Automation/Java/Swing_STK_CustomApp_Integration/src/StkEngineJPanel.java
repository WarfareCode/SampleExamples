// Java API
import java.awt.*;
import java.io.File;

import javax.swing.*;
import javax.swing.border.*;

// AGI Java API
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;

public class StkEngineJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	public final static String	s_STK_APP			= "STK Engine";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	
	private AgGlobeCntrlClass	m_AgGlobeCntrlClass;
	private AgMapCntrlClass		m_AgMapCntrlClass;

	public StkEngineJPanel()
	throws Exception
	{
		this.setLayout(new BorderLayout());

		TitledBorder b1 = new TitledBorder(new BevelBorder(BevelBorder.LOWERED), s_STK_APP);
		CompoundBorder cb1 = new CompoundBorder(new EmptyBorder(5,5,5,5), b1);
		this.setBorder(cb1);

		this.m_AgSTKXApplicationClass = new AgSTKXApplicationClass();
		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();
		
		JLabel versionJLabel = new JLabel();
		versionJLabel.setText(this.m_AgSTKXApplicationClass.getVersion());
		
		JTabbedPane tp = new JTabbedPane();

		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		this.m_AgMapCntrlClass = new AgMapCntrlClass();

		tp.addTab("Globe", this.m_AgGlobeCntrlClass);
		tp.addTab("Map", this.m_AgMapCntrlClass);
		this.add(tp, BorderLayout.CENTER);
	}
	
	public void newScenario()
	throws Throwable
	{
		if(this.m_AgStkObjectRootClass != null)
		{
			this.m_AgStkObjectRootClass.newScenario("auto_custom_integration_2");
		}
	}

	public void importFacility()
	throws Throwable
	{
		if(this.m_AgStkObjectRootClass != null)
		{
			AgScenarioClass scenario = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
			if(scenario != null)
			{
				String userDir = System.getProperty("user.dir");
				String fileSep = System.getProperty("file.separator");
				String filePath = userDir + fileSep + "facility.f";
				IAgStkObjectCollection children = scenario.getChildren();
				children.importObject(filePath);
			}
		}
	}

	public void saveScenario()
	throws Throwable
	{
		if(this.m_AgStkObjectRootClass != null)
		{
			String userDir = System.getProperty("user.dir");
			String fileSep = System.getProperty("file.separator");
			String filePath = userDir + fileSep + System.currentTimeMillis();
			File f = new File(filePath);
			if(!f.exists())
			{
				f.mkdir();
			}
			filePath = filePath + fileSep + "scenario";
			this.m_AgStkObjectRootClass.saveScenarioAs(filePath);
		}
	}

	public void closeScenario()
	throws Throwable
	{
		if(this.m_AgStkObjectRootClass != null)
		{
			this.m_AgStkObjectRootClass.closeScenario();
		}
	}
}