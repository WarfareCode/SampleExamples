import javax.swing.BorderFactory;
import javax.swing.JTabbedPane;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class FeedsJTabbedPane
extends JTabbedPane
{
	private static final long serialVersionUID = 1L;

	private VehicleFeedsJPanel 		m_VehFeedsJPanel;
	private SatelliteFeedsJPanel	m_SatFeedsJPanel;

	public FeedsJTabbedPane()
	{
		this.initialize();
	}

	private void initialize()
	{
		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, "Feeds", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_VehFeedsJPanel = new VehicleFeedsJPanel();
		this.addTab( "Vehicle", this.m_VehFeedsJPanel );

		this.m_SatFeedsJPanel = new SatelliteFeedsJPanel();
		this.addTab( "Satellite", this.m_SatFeedsJPanel );
	}

	public VehicleFeedsJPanel getVehicleFeeds()
	{
		return this.m_VehFeedsJPanel;
	}

	public SatelliteFeedsJPanel getSatelliteFeeds()
	{
		return this.m_SatFeedsJPanel;
	}
}
