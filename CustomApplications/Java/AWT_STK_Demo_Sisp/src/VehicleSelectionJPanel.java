import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;

public class VehicleSelectionJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private VehicleJTable m_AvailableVehJTable;
	private VehicleJTable m_ShownVehJTable;

	private JButton m_ShowJButton;
	private JButton m_NoShowJButton;

	public VehicleSelectionJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.LINE_AXIS ) );

		this.m_AvailableVehJTable = new VehicleJTable( true );
		JScrollPane ajsc = new JScrollPane( this.m_AvailableVehJTable );
		Border ab1 = BorderFactory.createLoweredBevelBorder();
		Border ab2 = BorderFactory.createTitledBorder( ab1, "Vehicles Available", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		ajsc.setBorder( ab2 );

		this.m_ShownVehJTable = new VehicleJTable( false );
		JScrollPane sjsc = new JScrollPane( this.m_ShownVehJTable );
		Border sb1 = BorderFactory.createLoweredBevelBorder();
		Border sb2 = BorderFactory.createTitledBorder( sb1, "Vehicles Shown", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		sjsc.setBorder( sb2 );

		JPanel buttons = new JPanel();
		buttons.setPreferredSize( new Dimension( 50, 200 ) );
		buttons.setLayout( new BoxLayout( buttons, BoxLayout.PAGE_AXIS ) );

		this.m_ShowJButton = new JButton();
		this.m_ShowJButton.setPreferredSize( new Dimension( 25, 25 ) );
		this.m_ShowJButton.setMaximumSize( new Dimension( 25, 25 ) );
		this.m_ShowJButton.setMinimumSize( new Dimension( 25, 25 ) );
		this.m_ShowJButton.setAlignmentX( CENTER_ALIGNMENT );
		this.m_ShowJButton.setIcon( new ImageIcon(VehicleSelectionJPanel.class.getResource( "moveright-icon.gif" )) );

		this.m_NoShowJButton = new JButton();
		this.m_NoShowJButton.setPreferredSize( new Dimension( 25, 25 ) );
		this.m_NoShowJButton.setMaximumSize( new Dimension( 25, 25 ) );
		this.m_NoShowJButton.setMinimumSize( new Dimension( 25, 25 ) );
		this.m_NoShowJButton.setAlignmentX( CENTER_ALIGNMENT );
		this.m_NoShowJButton.setIcon( new ImageIcon(VehicleSelectionJPanel.class.getResource( "moveleft-icon.gif" )) );

		buttons.add( Box.createVerticalGlue() );
		buttons.add( this.m_ShowJButton );
		buttons.add( Box.createRigidArea( new Dimension( 0, 10 ) ) );
		buttons.add( this.m_NoShowJButton );
		buttons.add( Box.createVerticalGlue() );

		this.add( ajsc );
		this.add( buttons );
		this.add( sjsc );
	}

	public VehicleJTable getAvailable()
	{
		return this.m_AvailableVehJTable;
	}

	public VehicleJTable getShown()
	{
		return this.m_ShownVehJTable;
	}

	public JButton getShowButton()
	{
		return this.m_ShowJButton;
	}

	public JButton getNoShowButton()
	{
		return this.m_NoShowJButton;
	}
}
