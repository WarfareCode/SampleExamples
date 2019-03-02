import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

public class TheaterJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	public final static String s_THEATER 	= "Theater";
	public final static String s_ALL 		= "All";
	public final static String s_USPACOM 	= "USPACOM";
	public final static String s_USNORTHCOM = "USNORTHCOM";
	public final static String s_USEUCOM 	= "USEUCOM";
	public final static String s_USSOUTHCOM = "USSOUTHCOM";
	public final static String s_USCENTCOM 	= "USCENTCOM";

	private ButtonGroup		m_ButtonGroup;
	private JRadioButton 	m_AllJRadioButton;
	private JRadioButton 	m_USPACOMJRadioButton;
	private JRadioButton 	m_USNORTHCOMJRadioButton;
	private JRadioButton 	m_USEUCOMJRadioButton;
	private JRadioButton 	m_USSOUTHCOMJRadioButton;
	private JRadioButton 	m_USCENTCOMJRadioButton;

	public TheaterJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );
		this.setPreferredSize( new Dimension( 100, 180 ) );
		this.setMaximumSize( new Dimension( 100, 180 ) );
		this.setMinimumSize( new Dimension( 100, 180 ) );
		this.setAlignmentY( TOP_ALIGNMENT );

		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, s_THEATER, TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_AllJRadioButton = new JRadioButton();
		this.m_AllJRadioButton.setText( s_ALL );
		this.m_AllJRadioButton.setActionCommand( s_ALL );
		this.m_AllJRadioButton.setSelected( true );
		this.m_AllJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_AllJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_AllJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_AllJRadioButton.setAlignmentX( LEFT_ALIGNMENT );

		this.m_USPACOMJRadioButton = new JRadioButton();
		this.m_USPACOMJRadioButton.setText( s_USPACOM );
		this.m_USPACOMJRadioButton.setActionCommand( s_USPACOM );
		this.m_USPACOMJRadioButton.setSelected( false );
		this.m_USPACOMJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_USPACOMJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_USPACOMJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_USPACOMJRadioButton.setAlignmentX( LEFT_ALIGNMENT );

		this.m_USNORTHCOMJRadioButton = new JRadioButton();
		this.m_USNORTHCOMJRadioButton.setText( s_USNORTHCOM );
		this.m_USNORTHCOMJRadioButton.setActionCommand( s_USNORTHCOM );
		this.m_USNORTHCOMJRadioButton.setSelected( false );
		this.m_USNORTHCOMJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_USNORTHCOMJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_USNORTHCOMJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_USNORTHCOMJRadioButton.setAlignmentX( LEFT_ALIGNMENT );

		this.m_USEUCOMJRadioButton = new JRadioButton();
		this.m_USEUCOMJRadioButton.setText( s_USEUCOM );
		this.m_USEUCOMJRadioButton.setActionCommand( s_USEUCOM );
		this.m_USEUCOMJRadioButton.setSelected( false );
		this.m_USEUCOMJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_USEUCOMJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_USEUCOMJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_USEUCOMJRadioButton.setAlignmentX( LEFT_ALIGNMENT );

		this.m_USSOUTHCOMJRadioButton = new JRadioButton();
		this.m_USSOUTHCOMJRadioButton.setText( s_USSOUTHCOM );
		this.m_USSOUTHCOMJRadioButton.setActionCommand( s_USSOUTHCOM );
		this.m_USSOUTHCOMJRadioButton.setSelected( false );
		this.m_USSOUTHCOMJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_USSOUTHCOMJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_USSOUTHCOMJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_USSOUTHCOMJRadioButton.setAlignmentX( LEFT_ALIGNMENT );

		this.m_USCENTCOMJRadioButton = new JRadioButton();
		this.m_USCENTCOMJRadioButton.setText( s_USCENTCOM );
		this.m_USCENTCOMJRadioButton.setActionCommand( s_USCENTCOM );
		this.m_USCENTCOMJRadioButton.setSelected( false );
		this.m_USCENTCOMJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_USCENTCOMJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_USCENTCOMJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_USCENTCOMJRadioButton.setAlignmentX( LEFT_ALIGNMENT );

		this.m_ButtonGroup = new ButtonGroup();
		this.m_ButtonGroup.add( this.m_AllJRadioButton );
		this.m_ButtonGroup.add( this.m_USPACOMJRadioButton );
		this.m_ButtonGroup.add( this.m_USNORTHCOMJRadioButton );
		this.m_ButtonGroup.add( this.m_USEUCOMJRadioButton );
		this.m_ButtonGroup.add( this.m_USSOUTHCOMJRadioButton );
		this.m_ButtonGroup.add( this.m_USCENTCOMJRadioButton );

		this.add( this.m_AllJRadioButton );
		this.add( this.m_USPACOMJRadioButton );
		this.add( this.m_USNORTHCOMJRadioButton );
		this.add( this.m_USEUCOMJRadioButton );
		this.add( this.m_USSOUTHCOMJRadioButton );
		this.add( this.m_USCENTCOMJRadioButton );
	}

	public void addTheaterListener( ActionListener listener )
	{
		this.m_AllJRadioButton.addActionListener( listener );
		this.m_USPACOMJRadioButton.addActionListener( listener );
		this.m_USNORTHCOMJRadioButton.addActionListener( listener );
		this.m_USEUCOMJRadioButton.addActionListener( listener );
		this.m_USSOUTHCOMJRadioButton.addActionListener( listener );
		this.m_USCENTCOMJRadioButton.addActionListener( listener );
	}
}
