// Java AWT
import java.awt.*;
import java.io.File;

import javax.swing.*;
import javax.swing.border.*;

// STK
import agi.core.*;
import agi.stk.ui.*;
import agi.stkobjects.*;

public class StkDesktopJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	public final static String	s_TITLE			= "STK Desktop";

	private AgStkUi					m_AgStkUi;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;

	private final static String	s_CONNECTED			= "CONNECTED";
	private final static String	s_DISCONNECTED		= "DISCONNECTED";

	private JLabel				m_StatusLabel;

	public StkDesktopJPanel()
	throws Exception
	{
		this.setLayout(new GridLayout(1,1));

		TitledBorder b1 = new TitledBorder(new BevelBorder(BevelBorder.LOWERED), s_TITLE);
		CompoundBorder cb1 = new CompoundBorder(new EmptyBorder(5,5,5,5), b1);
		this.setBorder(cb1);

		JPanel phantomPanel = new JPanel();
		CompoundBorder phantomBorder = new CompoundBorder(new BevelBorder(BevelBorder.RAISED), new EmptyBorder(7,7,7,7));
		phantomPanel.setBorder(phantomBorder);
		phantomPanel.setLayout(new BorderLayout());
		this.add(phantomPanel);
		
		this.m_StatusLabel = new JLabel();
		this.m_StatusLabel.setOpaque(true);
		this.m_StatusLabel.setBackground(Color.RED);
		this.m_StatusLabel.setText(s_DISCONNECTED);
		this.m_StatusLabel.setHorizontalTextPosition(SwingConstants.CENTER);
		this.m_StatusLabel.setHorizontalAlignment(SwingConstants.CENTER);
		this.m_StatusLabel.setBorder(new BevelBorder(BevelBorder.LOWERED));
		phantomPanel.add(this.m_StatusLabel);
	}

	public void setConnectToStk(boolean connected)
	{
		if(connected)
		{
			this.m_StatusLabel.setBackground(Color.GREEN);
			this.m_StatusLabel.setText(s_CONNECTED);
		}
		else
		{
			this.m_StatusLabel.setBackground(Color.RED);
			this.m_StatusLabel.setText(s_DISCONNECTED);
		}
	}

	public void releaseSTKInstance()
	{
		if(this.m_AgStkUi != null)
		{
			this.closeConnectionToSTK();

			this.setConnectToStk(false);
		}
	}

	public void startSTKInstance()
	{
		try
		{
			this.m_AgStkUi = new AgStkUi();

			if(this.m_AgStkUi != null)
			{
				this.m_AgStkUi.setVisible(true);
				this.m_AgStkUi.setUserControl(false);
				this.m_AgStkObjectRootClass = (AgStkObjectRootClass)this.m_AgStkUi.getIAgStkObjectRoot();
				this.setConnectToStk(true);
			}
		}
		catch(AgCoreException ex)
		{
			ex.printHexHresult();
			ex.printStackTrace();
			this.setConnectToStk(false);
			closeConnectionToSTK();
		}
	}

	public void closeConnectionToSTK()
	{
		try
		{
			if(m_AgStkUi != null)
			{
				this.m_AgStkUi.release();
				this.m_AgStkUi = null;
			}
		}
		catch(AgCoreException e)
		{
			e.printHexHresult();
			e.printStackTrace();
		}
	}
	
	public void newScenario()
	throws Throwable
	{
		if(this.m_AgStkObjectRootClass != null)
		{
			this.m_AgStkObjectRootClass.newScenario("auto_custom_integration_1");
			AgScenarioClass scenario = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
			if(scenario != null)
			{
				IAgStkObjectCollection children = scenario.getChildren();
				AgFacilityClass facility = (AgFacilityClass)children._new(AgESTKObjectType.E_FACILITY, "Facility1");
				facility.getPosition().assignGeodetic("20.0", "20.0", 1000);
			}
		}
	}

	public void exportFacility()
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
				AgFacilityClass facility = (AgFacilityClass)children.getItem("Facility1");
				facility.export(filePath);
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