import java.awt.Dimension;

import javax.swing.BorderFactory;
import javax.swing.JTabbedPane;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class SelectionJTabbedPane
extends JTabbedPane
{
	private static final long serialVersionUID = 1L;

	private VehicleSelectionJPanel m_VehSelectionJPanel;
	private SatelliteSelectionJPanel m_SatSelectionJPanel;

	public SelectionJTabbedPane()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setPreferredSize( new Dimension( 900, 300 ) );

		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, "Selections", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_VehSelectionJPanel = new VehicleSelectionJPanel();
		this.addTab( "Vehicle", this.m_VehSelectionJPanel );

		this.m_SatSelectionJPanel = new SatelliteSelectionJPanel();
		this.addTab( "Satellite", this.m_SatSelectionJPanel );
	}

	public VehicleSelectionJPanel getVehs()
	{
		return this.m_VehSelectionJPanel;
	}

	public SatelliteSelectionJPanel getSats()
	{
		return this.m_SatSelectionJPanel;
	}
}
