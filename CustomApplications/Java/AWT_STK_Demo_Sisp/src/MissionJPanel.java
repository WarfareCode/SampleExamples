import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;

public class MissionJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	public final static String s_MISSION 	= "Mission";
	public final static String s_ALL 		= "All";
	public final static String s_EO 		= "EO";
	public final static String s_IR 		= "IR";
	public final static String s_ELINT 		= "ELINT";

	private ButtonGroup		m_ButtonGroup;
	private JRadioButton 	m_AllJRadioButton;
	private JRadioButton 	m_EOJRadioButton;
	private JRadioButton 	m_IRJRadioButton;
	private JRadioButton 	m_ELINTJRadioButton;

	public MissionJPanel()
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
		Border b2 = BorderFactory.createTitledBorder( b1, s_MISSION, TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
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

		this.m_EOJRadioButton = new JRadioButton();
		this.m_EOJRadioButton.setText( s_EO );
		this.m_EOJRadioButton.setActionCommand( s_EO );
		this.m_EOJRadioButton.setSelected( false );
		this.m_EOJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_EOJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_EOJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_EOJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_EOJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_IRJRadioButton = new JRadioButton();
		this.m_IRJRadioButton.setText( s_IR );
		this.m_IRJRadioButton.setActionCommand( s_IR );
		this.m_IRJRadioButton.setSelected( false );
		this.m_IRJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_IRJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_IRJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_IRJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_IRJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_ELINTJRadioButton = new JRadioButton();
		this.m_ELINTJRadioButton.setText( s_ELINT );
		this.m_ELINTJRadioButton.setActionCommand( s_ELINT );
		this.m_ELINTJRadioButton.setSelected( false );
		this.m_ELINTJRadioButton.setPreferredSize( new Dimension( 100, 25 ) );
		this.m_ELINTJRadioButton.setMaximumSize( new Dimension( 100, 25 ) );
		this.m_ELINTJRadioButton.setMinimumSize( new Dimension( 100, 25 ) );
		this.m_ELINTJRadioButton.setAlignmentX( LEFT_ALIGNMENT );
		this.m_ELINTJRadioButton.setAlignmentY( TOP_ALIGNMENT );

		this.m_ButtonGroup = new ButtonGroup();
		this.m_ButtonGroup.add( this.m_AllJRadioButton );
		this.m_ButtonGroup.add( this.m_EOJRadioButton );
		this.m_ButtonGroup.add( this.m_IRJRadioButton );
		this.m_ButtonGroup.add( this.m_ELINTJRadioButton );
		this.setAlignmentY( TOP_ALIGNMENT );

		this.add( this.m_AllJRadioButton );
		this.add( this.m_EOJRadioButton );
		this.add( this.m_IRJRadioButton );
		this.add( this.m_ELINTJRadioButton );
	}

	public void addMissionListener( ActionListener listener )
	{
		this.m_AllJRadioButton.addActionListener( listener );
		this.m_EOJRadioButton.addActionListener( listener );
		this.m_IRJRadioButton.addActionListener( listener );
		this.m_ELINTJRadioButton.addActionListener( listener );
	}
}
