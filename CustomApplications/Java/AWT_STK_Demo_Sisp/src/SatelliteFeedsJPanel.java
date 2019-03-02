import javax.swing.BoxLayout;

import javax.swing.JPanel;

public class SatelliteFeedsJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private ForceCodeJPanel m_ForceCodeJPanel;
	private MissionJPanel m_MissionJPanel;
	private MassTotalJPanel m_MassTotalJPanel;
	private MassFuelJPanel m_MassFuelJPanel;

	public SatelliteFeedsJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );

		this.m_ForceCodeJPanel = new ForceCodeJPanel();
		this.m_MissionJPanel = new MissionJPanel();
		this.m_MassTotalJPanel = new MassTotalJPanel();
		this.m_MassFuelJPanel = new MassFuelJPanel();

		this.add( this.m_ForceCodeJPanel );
		this.add( this.m_MissionJPanel );
		this.add( this.m_MassFuelJPanel );
		this.add( this.m_MassTotalJPanel );
	}

	public ForceCodeJPanel getForceCode()
	{
		return this.m_ForceCodeJPanel;
	}

	public MissionJPanel getMission()
	{
		return this.m_MissionJPanel;
	}

	public MassTotalJPanel getTotalMass()
	{
		return this.m_MassTotalJPanel;
	}

	public MassFuelJPanel getFuelMass()
	{
		return this.m_MassFuelJPanel;
	}
}
