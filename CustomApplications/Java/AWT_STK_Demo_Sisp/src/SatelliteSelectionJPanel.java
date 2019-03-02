import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;

public class SatelliteSelectionJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private SatelliteJTable m_AvailableSatJTable;
	private SatelliteJTable m_ShownSatJTable;

	private JButton m_ShowJButton;
	private JButton m_NoShowJButton;

	public SatelliteSelectionJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.LINE_AXIS ) );

		this.m_AvailableSatJTable = new SatelliteJTable( true );
		JScrollPane ajsc = new JScrollPane( this.m_AvailableSatJTable );
		Border ab1 = BorderFactory.createLoweredBevelBorder();
		Border ab2 = BorderFactory.createTitledBorder( ab1, "Satellites Available", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		ajsc.setBorder( ab2 );

		this.m_ShownSatJTable = new SatelliteJTable( false );
		JScrollPane sjsc = new JScrollPane( this.m_ShownSatJTable );
		Border sb1 = BorderFactory.createLoweredBevelBorder();
		Border sb2 = BorderFactory.createTitledBorder( sb1, "Satellites Shown", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
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

	public SatelliteJTable getAvailable()
	{
		return this.m_AvailableSatJTable;
	}

	public SatelliteJTable getShown()
	{
		return this.m_ShownSatJTable;
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
