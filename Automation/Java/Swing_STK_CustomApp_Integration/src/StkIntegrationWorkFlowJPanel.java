//Java AWT
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

public class StkIntegrationWorkFlowJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	public final static String	s_TITLE			= "STK Desktop / Engine Integration Work Flow";

	/*package*/ final static String	s_DESKTOP_CREATE_STK_TEXT		= "<== Create STK";
	/*package*/ final static String	s_DESKTOP_CREATE_SCENARIO_TEXT	= "<== New Scenario";
	/*package*/ final static String	s_DESKTOP_EXPORT_FACILITY_TEXT	= "<== Export Facility";
	/*package*/ final static String	s_DESKTOP_SAVE_SCENARIO_TEXT	= "<== Save Scenario";
	/*package*/ final static String	s_DESKTOP_CLOSE_SCENARIO_TEXT	= "<== Close Scenario";
	/*package*/ final static String	s_DESKTOP_RELEASE_STK_TEXT		= "<== Release STK";

	/*package*/ final static String	s_ENGINE_CREATE_SCENARIO_TEXT	= "New Scenario ==>";
	/*package*/ final static String	s_ENGINE_IMPORT_FACILITY_TEXT	= "Import Facility ==>";
	/*package*/ final static String	s_ENGINE_SAVE_SCENARIO_TEXT		= "Save Scenario ==>";
	/*package*/ final static String	s_ENGINE_CLOSE_SCENARIO_TEXT	= "Close Scenario ==>";

	/*package*/ JButton				m_DesktopCreateStkButton;
	/*package*/ JButton				m_DesktopCreateScenarioButton;
	/*package*/ JButton				m_DesktopExportFacilityButton;
	/*package*/ JButton				m_DesktopSaveScenarioButton;
	/*package*/ JButton				m_DesktopCloseScenarioButton;
	/*package*/ JButton				m_DesktopReleaseStkButton;

	/*package*/ JButton				m_EngineCreateScenarioButton;
	/*package*/ JButton				m_EngineImportFacilityButton;
	/*package*/ JButton				m_EngineSaveScenarioButton;
	/*package*/ JButton				m_EngineCloseScenarioButton;

	public StkIntegrationWorkFlowJPanel()
	throws Exception
	{
		this.setLayout(new GridLayout(10,2));

		TitledBorder b1 = new TitledBorder(new BevelBorder(BevelBorder.LOWERED), s_TITLE);
		CompoundBorder cb1 = new CompoundBorder(new EmptyBorder(5,5,5,5), b1);
		this.setBorder(cb1);

		this.m_DesktopCreateStkButton = new JButton(s_DESKTOP_CREATE_STK_TEXT);
		this.m_DesktopCreateStkButton.setEnabled(true);
		this.m_DesktopCreateStkButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_DesktopCreateStkButton);

		this.add(new JLabel());
		
		this.m_DesktopCreateScenarioButton = new JButton(s_DESKTOP_CREATE_SCENARIO_TEXT);
		this.m_DesktopCreateScenarioButton.setEnabled(false);
		this.m_DesktopCreateScenarioButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_DesktopCreateScenarioButton);

		this.add(new JLabel());

		this.add(new JLabel());

		this.m_EngineCreateScenarioButton = new JButton(s_ENGINE_CREATE_SCENARIO_TEXT);
		this.m_EngineCreateScenarioButton.setEnabled(false);
		this.m_EngineCreateScenarioButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_EngineCreateScenarioButton);

		this.m_DesktopExportFacilityButton = new JButton(s_DESKTOP_EXPORT_FACILITY_TEXT);
		this.m_DesktopExportFacilityButton.setEnabled(false);
		this.m_DesktopExportFacilityButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_DesktopExportFacilityButton);

		this.add(new JLabel());

		this.add(new JLabel());

		this.m_EngineImportFacilityButton = new JButton(s_ENGINE_IMPORT_FACILITY_TEXT);
		this.m_EngineImportFacilityButton.setEnabled(false);
		this.m_EngineImportFacilityButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_EngineImportFacilityButton);

		this.m_DesktopSaveScenarioButton = new JButton(s_DESKTOP_SAVE_SCENARIO_TEXT);
		this.m_DesktopSaveScenarioButton.setEnabled(false);
		this.m_DesktopSaveScenarioButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_DesktopSaveScenarioButton);

		this.add(new JLabel());

		this.add(new JLabel());

		this.m_EngineSaveScenarioButton = new JButton(s_ENGINE_SAVE_SCENARIO_TEXT);
		this.m_EngineSaveScenarioButton.setEnabled(false);
		this.m_EngineSaveScenarioButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_EngineSaveScenarioButton);

		this.m_DesktopCloseScenarioButton = new JButton(s_DESKTOP_CLOSE_SCENARIO_TEXT);
		this.m_DesktopCloseScenarioButton.setEnabled(false);
		this.m_DesktopCloseScenarioButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_DesktopCloseScenarioButton);

		this.add(new JLabel());

		this.add(new JLabel());

		this.m_EngineCloseScenarioButton = new JButton(s_ENGINE_CLOSE_SCENARIO_TEXT);
		this.m_EngineCloseScenarioButton.setEnabled(false);
		this.m_EngineCloseScenarioButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_EngineCloseScenarioButton);

		this.m_DesktopReleaseStkButton = new JButton(s_DESKTOP_RELEASE_STK_TEXT);
		this.m_DesktopReleaseStkButton.setEnabled(false);
		this.m_DesktopReleaseStkButton.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(this.m_DesktopReleaseStkButton);
	}

	public void addActionListener(ActionListener al)
	{
		this.m_DesktopCreateStkButton.addActionListener(al);
		this.m_DesktopCreateScenarioButton.addActionListener(al);
		this.m_DesktopExportFacilityButton.addActionListener(al);
		this.m_DesktopSaveScenarioButton.addActionListener(al);
		this.m_DesktopCloseScenarioButton.addActionListener(al);
		this.m_DesktopReleaseStkButton.addActionListener(al);
		this.m_EngineCreateScenarioButton.addActionListener(al);
		this.m_EngineImportFacilityButton.addActionListener(al);
		this.m_EngineSaveScenarioButton.addActionListener(al);
		this.m_EngineCloseScenarioButton.addActionListener(al);
	}

	public void removeActionListener(ActionListener al)
	{
		this.m_DesktopCreateStkButton.removeActionListener(al);
		this.m_DesktopCreateScenarioButton.removeActionListener(al);
		this.m_DesktopExportFacilityButton.removeActionListener(al);
		this.m_DesktopSaveScenarioButton.removeActionListener(al);
		this.m_DesktopCloseScenarioButton.removeActionListener(al);
		this.m_DesktopReleaseStkButton.removeActionListener(al);
		this.m_EngineCreateScenarioButton.removeActionListener(al);
		this.m_EngineImportFacilityButton.removeActionListener(al);
		this.m_EngineSaveScenarioButton.removeActionListener(al);
		this.m_EngineCloseScenarioButton.removeActionListener(al);
	}
}