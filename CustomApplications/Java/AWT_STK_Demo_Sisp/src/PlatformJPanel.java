import java.awt.Dimension;
import java.awt.event.ActionListener;

import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.BorderFactory;
import javax.swing.JRadioButton;
import javax.swing.JPanel;

import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class PlatformJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	public final static String s_PLATFORM 	= "Platform";
	public final static String s_ALL 		= "All";
	public final static String s_GROUND 	= "Ground";
	public final static String s_AIRCRAFT 	= "Aircraft";
	public final static String s_SHIP 		= "Ship";

	private ButtonGroup		m_ButtonGroup;
	private JRadioButton 	m_AllJRadioButton;
	private JRadioButton 	m_GroundJRadioButton;
	private JRadioButton 	m_AircraftJRadioButton;
	private JRadioButton 	m_ShipJRadioButton;

	public PlatformJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );
		this.setPreferredSize( new Dimension( 100, 130 ) );
		this.setMaximumSize( new Dimension( 100, 130 ) );
		this.setMinimumSize( new Dimension( 100, 130 ) );
		this.setAlignmentX( LEFT_ALIGNMENT );
		this.setAlignmentY( TOP_ALIGNMENT );

		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, s_PLATFORM, TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_AllJRadioButton = new JRadioButton();
		this.m_AllJRadioButton.setText( s_ALL );
		this.m_AllJRadioButton.setActionCommand( s_ALL );
		this.m_AllJRadioButton.setSelected( true );
		this.m_AllJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_AllJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_AllJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_AllJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_AllJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_GroundJRadioButton = new JRadioButton();
		this.m_GroundJRadioButton.setText( s_GROUND );
		this.m_GroundJRadioButton.setActionCommand( s_GROUND );
		this.m_GroundJRadioButton.setSelected( false );
		this.m_GroundJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_GroundJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_GroundJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_GroundJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_GroundJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_AircraftJRadioButton = new JRadioButton();
		this.m_AircraftJRadioButton.setText( s_AIRCRAFT );
		this.m_AircraftJRadioButton.setActionCommand( s_AIRCRAFT );
		this.m_AircraftJRadioButton.setSelected( false );
		this.m_AircraftJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_AircraftJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_AircraftJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_AircraftJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_AircraftJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_ShipJRadioButton = new JRadioButton();
		this.m_ShipJRadioButton.setText( s_SHIP );
		this.m_ShipJRadioButton.setActionCommand( s_SHIP );
		this.m_ShipJRadioButton.setSelected( false );
		this.m_ShipJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_ShipJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_ShipJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_ShipJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_ShipJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_ButtonGroup = new ButtonGroup();
		this.m_ButtonGroup.add( this.m_AllJRadioButton );
		this.m_ButtonGroup.add( this.m_GroundJRadioButton );
		this.m_ButtonGroup.add( this.m_AircraftJRadioButton );
		this.m_ButtonGroup.add( this.m_ShipJRadioButton );

		this.add( this.m_AllJRadioButton );
		this.add( this.m_GroundJRadioButton );
		this.add( this.m_AircraftJRadioButton );
		this.add( this.m_ShipJRadioButton );
	}

	public void addPlatformListener( ActionListener listener )
	{
		this.m_AllJRadioButton.addActionListener( listener );
		this.m_GroundJRadioButton.addActionListener( listener );
		this.m_AircraftJRadioButton.addActionListener( listener );
		this.m_ShipJRadioButton.addActionListener( listener );
	}
}
