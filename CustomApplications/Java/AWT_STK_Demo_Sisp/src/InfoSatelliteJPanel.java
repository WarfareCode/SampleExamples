import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;

public class InfoSatelliteJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private JLabel m_VehicleNameJLabel;
	private JTextField m_VehicleNameJTextField;

	private JLabel m_ForceCodeJLabel;
	private JTextField m_ForceCodeJTextField;

	private JLabel m_CountryOfOriginJLabel;
	private JTextField m_CountryOfOriginJTextField;

	private JLabel m_SizeJLabel;
	private JTextField m_SizeJTextField;

	private JLabel m_RCSJLabel;
	private JTextField m_RCSJTextField;

	private JLabel m_TotalMassJLabel;
	private JTextField m_TotalMassJTextField;

	private JLabel m_FuelMassJLabel;
	private JTextField m_FuelMassJTextField;

	private JLabel m_AvailableDVJLabel;
	private JTextField m_AvailableDVJTextField;

	private JLabel m_AttitudeJLabel;
	private JTextField m_AttitudeJTextField;

	private JLabel m_OpsCapacityJLabel;
	private JTextField m_OpsCapacityJTextField;

	private JTextPane m_NotesJTextPane;

	public InfoSatelliteJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );

		//==============
		// 1 Vehicle Name
		//==============
		this.m_VehicleNameJLabel = new JLabel();
		this.m_VehicleNameJLabel.setText( " Name:" );
		this.m_VehicleNameJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_VehicleNameJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_VehicleNameJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_VehicleNameJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_VehicleNameJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_VehicleNameJTextField = new JTextField();
		this.m_VehicleNameJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_VehicleNameJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_VehicleNameJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_VehicleNameJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_VehicleNameJTextField.setEditable( false );

		JPanel vnamepanel = new JPanel();
		vnamepanel.setLayout( new BoxLayout( vnamepanel, BoxLayout.LINE_AXIS ) );
		vnamepanel.setPreferredSize( new Dimension( 150, 25 ) );
		vnamepanel.setMaximumSize( new Dimension( 150, 25 ) );
		vnamepanel.setMinimumSize( new Dimension( 150, 25 ) );
		vnamepanel.add( this.m_VehicleNameJLabel );
		vnamepanel.add( Box.createHorizontalGlue() );
		vnamepanel.add( this.m_VehicleNameJTextField );

		//==============
		// 2 Force Code
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

		//==================
		// 3 Country Of Origin
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
		// 4 Size
		//==================
		this.m_SizeJLabel = new JLabel();
		this.m_SizeJLabel.setText( " Size:" );
		this.m_SizeJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_SizeJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_SizeJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_SizeJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_SizeJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_SizeJTextField = new JTextField();
		this.m_SizeJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_SizeJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_SizeJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_SizeJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_SizeJTextField.setEditable( false );

		JPanel sizepanel = new JPanel();
		sizepanel.setLayout( new BoxLayout( sizepanel, BoxLayout.LINE_AXIS ) );
		sizepanel.setPreferredSize( new Dimension( 150, 25 ) );
		sizepanel.setMaximumSize( new Dimension( 150, 25 ) );
		sizepanel.setMinimumSize( new Dimension( 150, 25 ) );
		sizepanel.add( this.m_SizeJLabel );
		sizepanel.add( Box.createHorizontalGlue() );
		sizepanel.add( this.m_SizeJTextField );

		//==================
		// 5 RCS
		//==================
		this.m_RCSJLabel = new JLabel();
		this.m_RCSJLabel.setText( " RCS:" );
		this.m_RCSJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_RCSJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_RCSJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_RCSJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_RCSJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_RCSJTextField = new JTextField();
		this.m_RCSJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_RCSJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_RCSJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_RCSJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_RCSJTextField.setEditable( false );

		JPanel rcspanel = new JPanel();
		rcspanel.setLayout( new BoxLayout( rcspanel, BoxLayout.LINE_AXIS ) );
		rcspanel.setPreferredSize( new Dimension( 150, 25 ) );
		rcspanel.setMaximumSize( new Dimension( 150, 25 ) );
		rcspanel.setMinimumSize( new Dimension( 150, 25 ) );
		rcspanel.add( this.m_RCSJLabel );
		rcspanel.add( Box.createHorizontalGlue() );
		rcspanel.add( this.m_RCSJTextField );

		//==================
		// 6 Total Mass
		//==================
		this.m_TotalMassJLabel = new JLabel();
		this.m_TotalMassJLabel.setText( " Total Mass:" );
		this.m_TotalMassJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_TotalMassJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_TotalMassJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_TotalMassJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_TotalMassJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_TotalMassJTextField = new JTextField();
		this.m_TotalMassJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_TotalMassJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_TotalMassJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_TotalMassJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_TotalMassJTextField.setEditable( false );

		JPanel tmasspanel = new JPanel();
		tmasspanel.setLayout( new BoxLayout( tmasspanel, BoxLayout.LINE_AXIS ) );
		tmasspanel.setPreferredSize( new Dimension( 150, 25 ) );
		tmasspanel.setMaximumSize( new Dimension( 150, 25 ) );
		tmasspanel.setMinimumSize( new Dimension( 150, 25 ) );
		tmasspanel.add( this.m_TotalMassJLabel );
		tmasspanel.add( Box.createHorizontalGlue() );
		tmasspanel.add( this.m_TotalMassJTextField );

		//==================
		// 7 Fuel Mass
		//==================
		this.m_FuelMassJLabel = new JLabel();
		this.m_FuelMassJLabel.setText( " Fuel Mass:" );
		this.m_FuelMassJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_FuelMassJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_FuelMassJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_FuelMassJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_FuelMassJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_FuelMassJTextField = new JTextField();
		this.m_FuelMassJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_FuelMassJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_FuelMassJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_FuelMassJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_FuelMassJTextField.setEditable( false );

		JPanel fmasspanel = new JPanel();
		fmasspanel.setLayout( new BoxLayout( fmasspanel, BoxLayout.LINE_AXIS ) );
		fmasspanel.setPreferredSize( new Dimension( 150, 25 ) );
		fmasspanel.setMaximumSize( new Dimension( 150, 25 ) );
		fmasspanel.setMinimumSize( new Dimension( 150, 25 ) );
		fmasspanel.add( this.m_FuelMassJLabel );
		fmasspanel.add( Box.createHorizontalGlue() );
		fmasspanel.add( this.m_FuelMassJTextField );

		//==================
		// 8 Available DV
		//==================
		this.m_AvailableDVJLabel = new JLabel();
		this.m_AvailableDVJLabel.setText( " AvailDV:" );
		this.m_AvailableDVJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_AvailableDVJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_AvailableDVJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_AvailableDVJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_AvailableDVJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_AvailableDVJTextField = new JTextField();
		this.m_AvailableDVJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_AvailableDVJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_AvailableDVJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_AvailableDVJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_AvailableDVJTextField.setEditable( false );

		JPanel advpanel = new JPanel();
		advpanel.setLayout( new BoxLayout( advpanel, BoxLayout.LINE_AXIS ) );
		advpanel.setPreferredSize( new Dimension( 150, 25 ) );
		advpanel.setMaximumSize( new Dimension( 150, 25 ) );
		advpanel.setMinimumSize( new Dimension( 150, 25 ) );
		advpanel.add( this.m_AvailableDVJLabel );
		advpanel.add( Box.createHorizontalGlue() );
		advpanel.add( this.m_AvailableDVJTextField );

		//==================
		// 9 Attitude
		//==================
		this.m_AttitudeJLabel = new JLabel();
		this.m_AttitudeJLabel.setText( " Attitude:" );
		this.m_AttitudeJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_AttitudeJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_AttitudeJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_AttitudeJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_AttitudeJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_AttitudeJTextField = new JTextField();
		this.m_AttitudeJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_AttitudeJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_AttitudeJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_AttitudeJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_AttitudeJTextField.setEditable( false );

		JPanel attpanel = new JPanel();
		attpanel.setLayout( new BoxLayout( attpanel, BoxLayout.LINE_AXIS ) );
		attpanel.setPreferredSize( new Dimension( 150, 25 ) );
		attpanel.setMaximumSize( new Dimension( 150, 25 ) );
		attpanel.setMinimumSize( new Dimension( 150, 25 ) );
		attpanel.add( this.m_AttitudeJLabel );
		attpanel.add( Box.createHorizontalGlue() );
		attpanel.add( this.m_AttitudeJTextField );

		//==================
		// 10 Ops Capacity
		//==================
		this.m_OpsCapacityJLabel = new JLabel();
		this.m_OpsCapacityJLabel.setText( " Ops Cap:" );
		this.m_OpsCapacityJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_OpsCapacityJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_OpsCapacityJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_OpsCapacityJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_OpsCapacityJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_OpsCapacityJTextField = new JTextField();
		this.m_OpsCapacityJTextField.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_OpsCapacityJTextField.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_OpsCapacityJTextField.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_OpsCapacityJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_OpsCapacityJTextField.setEditable( false );

		JPanel opscappanel = new JPanel();
		opscappanel.setLayout( new BoxLayout( opscappanel, BoxLayout.LINE_AXIS ) );
		opscappanel.setPreferredSize( new Dimension( 150, 25 ) );
		opscappanel.setMaximumSize( new Dimension( 150, 25 ) );
		opscappanel.setMinimumSize( new Dimension( 150, 25 ) );
		opscappanel.add( this.m_OpsCapacityJLabel );
		opscappanel.add( Box.createHorizontalGlue() );
		opscappanel.add( this.m_OpsCapacityJTextField );

		//==================
		// 11 Notes
		//==================
		this.m_NotesJTextPane = new JTextPane();
		this.m_NotesJTextPane.setEditable( false );
		this.m_NotesJTextPane.setEnabled( false );

		JPanel notespanel = new JPanel();
		notespanel.setLayout( new BorderLayout() );
		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, "Notes", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		notespanel.setBorder( b2 );
		notespanel.add( this.m_NotesJTextPane, BorderLayout.CENTER );

		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( vnamepanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( fcodepanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( coriginpanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( sizepanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( rcspanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( tmasspanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( fmasspanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( advpanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( attpanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( opscappanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( notespanel );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
	}

	public void setSatData( SatelliteData satdata )
	{
		this.m_VehicleNameJTextField.setText( satdata.Name );
		this.m_ForceCodeJTextField.setText( satdata.ForceCode );
		this.m_CountryOfOriginJTextField.setText( satdata.CountryOfOrigin );
		this.m_SizeJTextField.setText( satdata.Size );
		this.m_RCSJTextField.setText( satdata.RCS );
		this.m_TotalMassJTextField.setText( satdata.TotalMass );
		this.m_FuelMassJTextField.setText( satdata.FuelMass );
		this.m_AvailableDVJTextField.setText( satdata.DV );
		this.m_AttitudeJTextField.setText( satdata.Attitude );
		this.m_OpsCapacityJTextField.setText( satdata.OpCapacity );
		this.m_NotesJTextPane.setText( satdata.Notes );
	}
}
