import java.awt.Dimension;
import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.SwingConstants;
import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class XYJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private JLabel m_XJLabel;
	private JLabel m_YJLabel;

	private JTextField m_XJTextField;
	private JTextField m_YJTextField;

	public XYJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );

		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, "XY", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_XJLabel = new JLabel();
		this.m_XJLabel.setText(" X:");
		this.m_XJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_XJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_XJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_XJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_XJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_XJTextField = new JTextField();
		this.m_XJTextField.setText("0");
		this.m_XJTextField.setEditable( false );
		this.m_XJTextField.setPreferredSize( new Dimension( 50, 25 ));
		this.m_XJTextField.setMaximumSize( new Dimension( 50, 25 ));
		this.m_XJTextField.setMinimumSize( new Dimension( 50, 25 ));
		this.m_XJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_XJTextField.setHorizontalAlignment( SwingConstants.TRAILING );

		JPanel xpanel = new JPanel();
		xpanel.setPreferredSize( new Dimension( 150, 25 ) );
		xpanel.setMaximumSize( new Dimension( 150, 25 ) );
		xpanel.setMinimumSize( new Dimension( 150, 25 ) );
		xpanel.setLayout( new BoxLayout( xpanel, BoxLayout.LINE_AXIS ) );
		xpanel.add( this.m_XJLabel );
		xpanel.add( Box.createHorizontalGlue() );
		xpanel.add( this.m_XJTextField );

		this.m_YJLabel = new JLabel();
		this.m_YJLabel.setText(" Y:");
		this.m_YJLabel.setPreferredSize( new Dimension( 75, 25 ) );
		this.m_YJLabel.setMaximumSize( new Dimension( 75, 25 ) );
		this.m_YJLabel.setMinimumSize( new Dimension( 75, 25 ) );
		this.m_YJLabel.setAlignmentX( LEFT_ALIGNMENT );
		this.m_YJLabel.setHorizontalAlignment( SwingConstants.LEADING );

		this.m_YJTextField = new JTextField();
		this.m_YJTextField.setText("0");
		this.m_YJTextField.setEditable( false );
		this.m_YJTextField.setPreferredSize( new Dimension( 50, 25 ));
		this.m_YJTextField.setMaximumSize( new Dimension( 50, 25 ));
		this.m_YJTextField.setMinimumSize( new Dimension( 50, 25 ));
		this.m_YJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_YJTextField.setHorizontalAlignment( SwingConstants.TRAILING );

		JPanel ypanel = new JPanel();
		ypanel.setPreferredSize( new Dimension( 150, 25 ) );
		ypanel.setMaximumSize( new Dimension( 150, 25 ) );
		ypanel.setMinimumSize( new Dimension( 150, 25 ) );
		ypanel.setLayout( new BoxLayout( ypanel, BoxLayout.LINE_AXIS ) );
		ypanel.add( this.m_YJLabel );
		ypanel.add( Box.createHorizontalGlue() );
		ypanel.add( this.m_YJTextField );

		this.add( Box.createVerticalGlue() );
		this.add( xpanel );
		this.add( Box.createVerticalGlue() );
		this.add( ypanel );
		this.add( Box.createVerticalGlue() );
	}

	public String getMouseX()
	{
		return this.m_XJTextField.getText();
	}

	public String getMouseY()
	{
		return this.m_YJTextField.getText();
	}

	public void setMouseX( String x )
	{
		this.m_XJTextField.setText( x );
	}

	public void setMouseY( String y )
	{
		this.m_YJTextField.setText( y );
	}
}
