import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

public class ForceCodeJPanel
extends JPanel
{	
	private static final long serialVersionUID = 1L;

	public final static String s_FORCE_CODE = "Force Code";
	public final static String s_ALL = "All";
	public final static String s_BLUE = "Blue";
	public final static String s_RED = "Red";
	public final static String s_WHITE = "White";

	private ButtonGroup		m_ButtonGroup;
	private JRadioButton 	m_AllJRadioButton;
	private JRadioButton 	m_BlueJRadioButton;
	private JRadioButton 	m_RedJRadioButton;
	private JRadioButton 	m_WhiteJRadioButton;

	public ForceCodeJPanel()
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
		Border b2 = BorderFactory.createTitledBorder( b1, s_FORCE_CODE, TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
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

		this.m_BlueJRadioButton = new JRadioButton();
		this.m_BlueJRadioButton.setText( s_BLUE );
		this.m_BlueJRadioButton.setActionCommand( s_BLUE );
		this.m_BlueJRadioButton.setSelected( false );
		this.m_BlueJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_BlueJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_BlueJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_BlueJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_BlueJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_RedJRadioButton = new JRadioButton();
		this.m_RedJRadioButton.setText( s_RED );
		this.m_RedJRadioButton.setActionCommand( s_RED );
		this.m_RedJRadioButton.setSelected( false );
		this.m_RedJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_RedJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_RedJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_RedJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_RedJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_WhiteJRadioButton = new JRadioButton();
		this.m_WhiteJRadioButton.setText( s_WHITE );
		this.m_WhiteJRadioButton.setActionCommand( s_WHITE );
		this.m_WhiteJRadioButton.setSelected( false );
		this.m_WhiteJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_WhiteJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_WhiteJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_WhiteJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_WhiteJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_ButtonGroup = new ButtonGroup();
		this.m_ButtonGroup.add( this.m_AllJRadioButton );
		this.m_ButtonGroup.add( this.m_BlueJRadioButton );
		this.m_ButtonGroup.add( this.m_RedJRadioButton );
		this.m_ButtonGroup.add( this.m_WhiteJRadioButton );

		this.add( this.m_AllJRadioButton );
		this.add( this.m_BlueJRadioButton );
		this.add( this.m_RedJRadioButton );
		this.add( this.m_WhiteJRadioButton );
	}

	public void addForceCodeListener( ActionListener listener )
	{
		this.m_AllJRadioButton.addActionListener( listener );
		this.m_BlueJRadioButton.addActionListener( listener );
		this.m_RedJRadioButton.addActionListener( listener );
		this.m_WhiteJRadioButton.addActionListener( listener );
	}
}
