import javax.swing.*;

public class VehicleFeedsJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private ForceCodeJPanel m_ForceCodeJPanel;
	private TheaterJPanel m_TheaterJPanel;
	private PlatformJPanel m_PlatformJPanel;

	public VehicleFeedsJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );

		this.m_ForceCodeJPanel = new ForceCodeJPanel();
		this.m_TheaterJPanel = new TheaterJPanel();
		this.m_PlatformJPanel = new PlatformJPanel();

		this.add( this.m_ForceCodeJPanel );
		this.add( this.m_TheaterJPanel );
		this.add( this.m_PlatformJPanel );
	}

	public ForceCodeJPanel getForceCode()
	{
		return this.m_ForceCodeJPanel;
	}

	public TheaterJPanel getTheater()
	{
		return this.m_TheaterJPanel;
	}

	public PlatformJPanel getPlatform()
	{
		return this.m_PlatformJPanel;
	}
}
