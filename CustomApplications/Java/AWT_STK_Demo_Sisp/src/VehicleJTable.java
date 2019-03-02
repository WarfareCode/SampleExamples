import javax.swing.*;

public class VehicleJTable
extends JTable
{
	private static final long serialVersionUID = 1L;

	public VehicleJTable( boolean initializeWithData )
	{
		this.initialize( initializeWithData );
	}

	private void initialize( boolean initializeWithData )
	{
		this.setModel( new VehicleTableModel( initializeWithData ));
	}

	public VehicleTableModel getVehModel()
	{
		return ( VehicleTableModel )this.getModel();
	}
}
