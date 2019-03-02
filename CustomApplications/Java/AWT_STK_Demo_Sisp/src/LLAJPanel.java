import java.awt.Dimension;
import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.Box;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.SwingConstants;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class LLAJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private JLabel m_LatJLabel;
	private JLabel m_LonJLabel;

	private JTextField m_LatJTextField;
	private JTextField m_LonJTextField;

	public LLAJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );

		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, "LLA", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_LatJLabel = new JLabel();
		this.m_LatJLabel.setText(" Latitude:");
		this.m_LatJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_LatJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_LatJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_LatJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_LatJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_LatJTextField = new JTextField();
		this.m_LatJTextField.setText("0.0");
		this.m_LatJTextField.setEditable( false );
		this.m_LatJTextField.setPreferredSize( new Dimension( 50, 25 ));
		this.m_LatJTextField.setMaximumSize( new Dimension( 50, 25 ));
		this.m_LatJTextField.setMinimumSize( new Dimension( 50, 25 ));
		this.m_LatJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_LatJTextField.setHorizontalAlignment( SwingConstants.TRAILING );

		JPanel latpanel = new JPanel();
		latpanel.setPreferredSize( new Dimension( 150, 25 ) );
		latpanel.setMaximumSize( new Dimension( 150, 25 ) );
		latpanel.setMinimumSize( new Dimension( 150, 25 ) );
		latpanel.setLayout( new BoxLayout( latpanel, BoxLayout.LINE_AXIS ) );
		latpanel.add( this.m_LatJLabel );
		latpanel.add( Box.createHorizontalGlue() );
		latpanel.add( this.m_LatJTextField );

		this.m_LonJLabel = new JLabel();
		this.m_LonJLabel.setText(" Longitude:");
		this.m_LonJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_LonJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_LonJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_LonJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_LonJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_LonJTextField = new JTextField();
		this.m_LonJTextField.setText("0.0");
		this.m_LonJTextField.setEditable( false );
		this.m_LonJTextField.setPreferredSize( new Dimension( 50, 25 ));
		this.m_LonJTextField.setMaximumSize( new Dimension( 50, 25 ));
		this.m_LonJTextField.setMinimumSize( new Dimension( 50, 25 ));
		this.m_LonJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_LonJTextField.setHorizontalAlignment( SwingConstants.TRAILING );

		JPanel lonpanel = new JPanel();
		lonpanel.setPreferredSize( new Dimension( 150, 25 ) );
		lonpanel.setMaximumSize( new Dimension( 150, 25 ) );
		lonpanel.setMinimumSize( new Dimension( 150, 25 ) );
		lonpanel.setLayout( new BoxLayout( lonpanel, BoxLayout.LINE_AXIS ) );
		lonpanel.add( this.m_LonJLabel );
		lonpanel.add( Box.createHorizontalGlue() );
		lonpanel.add( this.m_LonJTextField );

		this.add( Box.createVerticalGlue() );
		this.add( latpanel );
		this.add( Box.createVerticalGlue() );
		this.add( lonpanel );
		this.add( Box.createVerticalGlue() );
	}

	public String getLat()
	{
		return this.m_LatJTextField.getText();
	}

	public String getLon()
	{
		return this.m_LonJTextField.getText();
	}

	public void setLat( String lat )
	{
		this.m_LatJTextField.setText( lat );
	}

	public void setLon( String lon )
	{
		this.m_LonJTextField.setText( lon );
	}
}
