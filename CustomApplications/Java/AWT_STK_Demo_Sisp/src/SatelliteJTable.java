import javax.swing.*;

public class SatelliteJTable
extends JTable
{
	private static final long serialVersionUID = 1L;

	public SatelliteJTable( boolean initializeWithData )
	{
		this.initialize( initializeWithData );
	}

	private void initialize( boolean initializeWithData )
	{
		this.setModel( new SatelliteTableModel( initializeWithData ));
	}

	public SatelliteTableModel getSatModel()
	{
		return ( SatelliteTableModel )this.getModel();
	}
}
