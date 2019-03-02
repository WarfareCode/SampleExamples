import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;

public class InfoVehicleJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private JLabel m_NameJLabel;
	private JTextField m_NameJTextField;

	private JLabel m_TypeJLabel;
	private JTextField m_TypeJTextField;

	private JLabel m_ForceCodeJLabel;
	private JTextField m_ForceCodeJTextField;

	private JLabel m_MissionJLabel;
	private JTextField m_MissionJTextField;

	private JLabel m_StateJLabel;
	private JTextField m_StateJTextField;

	private JLabel m_CountryOfOriginJLabel;
	private JTextField m_CountryOfOriginJTextField;

	private JLabel m_WeaponsJLabel;
	private JTextField m_WeaponsJTextField;

	private JLabel m_TheaterJLabel;
	private JTextField m_TheaterJTextField;

	private JLabel m_OpCapacityJLabel;
	private JTextField m_OpCapacityJTextField;

	private JLabel m_LoadedJLabel;
	private JTextField m_LoadedJTextField;

	private JTextPane m_NotesJTextPane;

	public InfoVehicleJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );

		//==============
		// 1 Vehicle Name
		//==============
		this.m_NameJLabel = new JLabel();
		this.m_NameJLabel.setText( " Name:" );
		this.m_NameJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_NameJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_NameJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_NameJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_NameJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_NameJTextField = new JTextField();
		this.m_NameJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_NameJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_NameJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_NameJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_NameJTextField.setEditable( false );

		JPanel namepanel = new JPanel();
		namepanel.setLayout( new BoxLayout( namepanel, BoxLayout.LINE_AXIS ) );
		namepanel.setPreferredSize( new Dimension( 150, 25 ) );
		namepanel.setMaximumSize( new Dimension( 150, 25 ) );
		namepanel.setMinimumSize( new Dimension( 150, 25 ) );
		namepanel.add( this.m_NameJLabel );
		namepanel.add( Box.createHorizontalGlue() );
		namepanel.add( this.m_NameJTextField );

		//==============
		// 2 Vehicle Type
		//==============
		this.m_TypeJLabel = new JLabel();
		this.m_TypeJLabel.setText( " Type:" );
		this.m_TypeJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_TypeJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_TypeJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_TypeJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_TypeJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_TypeJTextField = new JTextField();
		this.m_TypeJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_TypeJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_TypeJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_TypeJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_TypeJTextField.setEditable( false );

		JPanel typepanel = new JPanel();
		typepanel.setLayout( new BoxLayout( typepanel, BoxLayout.LINE_AXIS ) );
		typepanel.setPreferredSize( new Dimension( 150, 25 ) );
		typepanel.setMaximumSize( new Dimension( 150, 25 ) );
		typepanel.setMinimumSize( new Dimension( 150, 25 ) );
		typepanel.add( this.m_TypeJLabel );
		typepanel.add( Box.createHorizontalGlue() );
		typepanel.add( this.m_TypeJTextField );

		//==============
		// 3 Force Code
		//==============
		this.m_ForceCodeJLabel = new JLabel();
		this.m_ForceCodeJLabel.setText( " Force Code:" );
		this.m_ForceCodeJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_ForceCodeJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_ForceCodeJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_ForceCodeJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_ForceCodeJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_ForceCodeJTextField = new JTextField();
		this.m_ForceCodeJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_ForceCodeJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_ForceCodeJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_ForceCodeJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_ForceCodeJTextField.setEditable( false );

		JPanel fcodepanel = new JPanel();
		fcodepanel.setLayout( new BoxLayout( fcodepanel, BoxLayout.LINE_AXIS ) );
		fcodepanel.setPreferredSize( new Dimension( 150, 25 ) );
		fcodepanel.setMaximumSize( new Dimension( 150, 25 ) );
		fcodepanel.setMinimumSize( new Dimension( 150, 25 ) );
		fcodepanel.add( this.m_ForceCodeJLabel );
		fcodepanel.add( Box.createHorizontalGlue() );
		fcodepanel.add( this.m_ForceCodeJTextField );

		//==============
		// 4 Mission
		//==============
		this.m_MissionJLabel = new JLabel();
		this.m_MissionJLabel.setText( " Mission:" );
		this.m_MissionJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_MissionJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_MissionJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_MissionJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_MissionJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_MissionJTextField = new JTextField();
		this.m_MissionJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_MissionJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_MissionJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_MissionJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_MissionJTextField.setEditable( false );

		JPanel missionpanel = new JPanel();
		missionpanel.setLayout( new BoxLayout( missionpanel, BoxLayout.LINE_AXIS ) );
		missionpanel.setPreferredSize( new Dimension( 150, 25 ) );
		missionpanel.setMaximumSize( new Dimension( 150, 25 ) );
		missionpanel.setMinimumSize( new Dimension( 150, 25 ) );
		missionpanel.add( this.m_MissionJLabel );
		missionpanel.add( Box.createHorizontalGlue() );
		missionpanel.add( this.m_MissionJTextField );

		//==============
		// 5 State
		//==============
		this.m_StateJLabel = new JLabel();
		this.m_StateJLabel.setText( " State:" );
		this.m_StateJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_StateJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_StateJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_StateJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_StateJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_StateJTextField = new JTextField();
		this.m_StateJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_StateJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_StateJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_StateJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_StateJTextField.setEditable( false );

		JPanel statepanel = new JPanel();
		statepanel.setLayout( new BoxLayout( statepanel, BoxLayout.LINE_AXIS ) );
		statepanel.setPreferredSize( new Dimension( 150, 25 ) );
		statepanel.setMaximumSize( new Dimension( 150, 25 ) );
		statepanel.setMinimumSize( new Dimension( 150, 25 ) );
		statepanel.add( this.m_StateJLabel );
		statepanel.add( Box.createHorizontalGlue() );
		statepanel.add( this.m_StateJTextField );

		//==================
		// 6 Country Of Origin
		//==================
		this.m_CountryOfOriginJLabel = new JLabel();
		this.m_CountryOfOriginJLabel.setText( " Origin:" );
		this.m_CountryOfOriginJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_CountryOfOriginJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_CountryOfOriginJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_CountryOfOriginJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_CountryOfOriginJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_CountryOfOriginJTextField = new JTextField();
		this.m_CountryOfOriginJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_CountryOfOriginJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_CountryOfOriginJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_CountryOfOriginJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_CountryOfOriginJTextField.setEditable( false );

		JPanel coriginpanel = new JPanel();
		coriginpanel.setLayout( new BoxLayout( coriginpanel, BoxLayout.LINE_AXIS ) );
		coriginpanel.setPreferredSize( new Dimension( 150, 25 ) );
		coriginpanel.setMaximumSize( new Dimension( 150, 25 ) );
		coriginpanel.setMinimumSize( new Dimension( 150, 25 ) );
		coriginpanel.add( this.m_CountryOfOriginJLabel );
		coriginpanel.add( Box.createHorizontalGlue() );
		coriginpanel.add( this.m_CountryOfOriginJTextField );

		//==================
		// 7 Weapons
		//==================
		this.m_WeaponsJLabel = new JLabel();
		this.m_WeaponsJLabel.setText( " Weapons:" );
		this.m_WeaponsJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_WeaponsJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_WeaponsJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_WeaponsJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_WeaponsJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_WeaponsJTextField = new JTextField();
		this.m_WeaponsJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_WeaponsJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_WeaponsJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_WeaponsJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_WeaponsJTextField.setEditable( false );

		JPanel weaponspanel = new JPanel();
		weaponspanel.setLayout( new BoxLayout( weaponspanel, BoxLayout.LINE_AXIS ) );
		weaponspanel.setPreferredSize( new Dimension( 150, 25 ) );
		weaponspanel.setMaximumSize( new Dimension( 150, 25 ) );
		weaponspanel.setMinimumSize( new Dimension( 150, 25 ) );
		weaponspanel.add( this.m_WeaponsJLabel );
		weaponspanel.add( Box.createHorizontalGlue() );
		weaponspanel.add( this.m_WeaponsJTextField );

		//==================
		// 8 Theater
		//==================
		this.m_TheaterJLabel = new JLabel();
		this.m_TheaterJLabel.setText( " Theater:" );
		this.m_TheaterJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_TheaterJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_TheaterJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_TheaterJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_TheaterJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_TheaterJTextField = new JTextField();
		this.m_TheaterJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_TheaterJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_TheaterJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_TheaterJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_TheaterJTextField.setEditable( false );

		JPanel theaterpanel = new JPanel();
		theaterpanel.setLayout( new BoxLayout( theaterpanel, BoxLayout.LINE_AXIS ) );
		theaterpanel.setPreferredSize( new Dimension( 150, 25 ) );
		theaterpanel.setMaximumSize( new Dimension( 150, 25 ) );
		theaterpanel.setMinimumSize( new Dimension( 150, 25 ) );
		theaterpanel.add( this.m_TheaterJLabel );
		theaterpanel.add( Box.createHorizontalGlue() );
		theaterpanel.add( this.m_TheaterJTextField );

		//==================
		// 9 Ops Capacity
		//==================
		this.m_OpCapacityJLabel = new JLabel();
		this.m_OpCapacityJLabel.setText( " Ops Cap:" );
		this.m_OpCapacityJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_OpCapacityJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_OpCapacityJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_OpCapacityJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_OpCapacityJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_OpCapacityJTextField = new JTextField();
		this.m_OpCapacityJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_OpCapacityJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_OpCapacityJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_OpCapacityJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_OpCapacityJTextField.setEditable( false );

		JPanel opcappanel = new JPanel();
		opcappanel.setLayout( new BoxLayout( opcappanel, BoxLayout.LINE_AXIS ) );
		opcappanel.setPreferredSize( new Dimension( 150, 25 ) );
		opcappanel.setMaximumSize( new Dimension( 150, 25 ) );
		opcappanel.setMinimumSize( new Dimension( 150, 25 ) );
		opcappanel.add( this.m_OpCapacityJLabel );
		opcappanel.add( Box.createHorizontalGlue() );
		opcappanel.add( this.m_OpCapacityJTextField );

		//==================
		// 10 Loaded
		//==================
		this.m_LoadedJLabel = new JLabel();
		this.m_LoadedJLabel.setText( " Loaded:" );
		this.m_LoadedJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_LoadedJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_LoadedJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_LoadedJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_LoadedJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_LoadedJTextField = new JTextField();
		this.m_LoadedJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_LoadedJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_LoadedJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_LoadedJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_LoadedJTextField.setEditable( false );

		JPanel loadedpanel = new JPanel();
		loadedpanel.setLayout( new BoxLayout( loadedpanel, BoxLayout.LINE_AXIS ) );
		loadedpanel.setPreferredSize( new Dimension( 150, 25 ) );
		loadedpanel.setMaximumSize( new Dimension( 150, 25 ) );
		loadedpanel.setMinimumSize( new Dimension( 150, 25 ) );
		loadedpanel.add( this.m_LoadedJLabel );
		loadedpanel.add( Box.createHorizontalGlue() );
		loadedpanel.add( this.m_LoadedJTextField );

		//==================
		// 11 Notes
		//==================
		this.m_NotesJTextPane = new JTextPane();
		this.m_NotesJTextPane.setEditable( false );

		JPanel notespanel = new JPanel();
		notespanel.setLayout( new BorderLayout() );
		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, "Notes", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		notespanel.setBorder( b2 );
		notespanel.add( this.m_NotesJTextPane, BorderLayout.CENTER );

		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( namepanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( typepanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( fcodepanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( missionpanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( statepanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( coriginpanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( weaponspanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( theaterpanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( opcappanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( loadedpanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( notespanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
	}

	public void setVehData( VehicleData vehdata )
	{
		this.m_NameJTextField.setText( vehdata.Name );
		this.m_TypeJTextField.setText( vehdata.Type );
		this.m_ForceCodeJTextField.setText( vehdata.ForceCode );
		this.m_MissionJTextField.setText( vehdata.Mission );
		this.m_StateJTextField.setText( vehdata.State );
		this.m_CountryOfOriginJTextField.setText( vehdata.CountryOfOrigin );
		this.m_WeaponsJTextField.setText( vehdata.Weapons );
		this.m_TheaterJTextField.setText( vehdata.Theater );
		this.m_OpCapacityJTextField.setText( vehdata.OpCapacity );
		this.m_LoadedJTextField.setText( vehdata.Loaded );
		this.m_NotesJTextPane.setText( vehdata.Notes );
	}
}
