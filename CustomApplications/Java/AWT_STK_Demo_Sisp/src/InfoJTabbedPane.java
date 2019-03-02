import javax.swing.BorderFactory;
import javax.swing.JTabbedPane;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class InfoJTabbedPane
extends JTabbedPane
{
	private static final long serialVersionUID = 1L;

	public final static String s_SURVEILLANCE 	= "Surveillance";
	public final static String s_VEHICLE 		= "Vehicle";
	public final static String s_SATELLITE		= "Satellite";

	private InfoSurveillanceJPanel 	m_InfoSurJPanel;
	private InfoVehicleJPanel 		m_InfoVehJPanel;
	private InfoSatelliteJPanel 	m_InfoSatJPanel;

	public InfoJTabbedPane()
	{
		this.initialize();
	}

	private void initialize()
	{
		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, "Information", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_InfoSurJPanel = new InfoSurveillanceJPanel();
		this.addTab( s_SURVEILLANCE, this.m_InfoSurJPanel );

		this.m_InfoVehJPanel = new InfoVehicleJPanel();
		this.addTab( s_VEHICLE, this.m_InfoVehJPanel );

		this.m_InfoSatJPanel = new InfoSatelliteJPanel();
		this.addTab( s_SATELLITE, this.m_InfoSatJPanel );
	}

	public InfoSurveillanceJPanel getSurveillanceInfo()
	{
		return this.m_InfoSurJPanel;
	}

	public InfoVehicleJPanel getVehicleInfo()
	{
		return this.m_InfoVehJPanel;
	}

	public InfoSatelliteJPanel getSatelliteInfo()
	{
		return this.m_InfoSatJPanel;
	}
}
