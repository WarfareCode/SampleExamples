import java.awt.*;
import javax.swing.*;

import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class MassTotalJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private JCheckBox m_MinJCheckBox;
	private JCheckBox m_MaxJCheckBox;

	private JTextField m_MinJTextField;
	private JTextField m_MaxJTextField;

	public MassTotalJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );
		this.setPreferredSize( new Dimension( 100, 85 ) );
		this.setMaximumSize( new Dimension( 100, 85 ) );
		this.setMinimumSize( new Dimension( 100, 85 ) );
		this.setAlignmentX( LEFT_ALIGNMENT );
		this.setAlignmentY( TOP_ALIGNMENT );

		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, "Total Mass", TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_MinJCheckBox = new JCheckBox();
		this.m_MinJCheckBox.setText("Min");
		this.m_MinJCheckBox.setPreferredSize( new Dimension( 45, 25 ) );
		this.m_MinJCheckBox.setMaximumSize( new Dimension( 45, 25 ) );
		this.m_MinJCheckBox.setMinimumSize( new Dimension( 45, 25 ) );
		this.m_MinJCheckBox.setAlignmentX( LEFT_ALIGNMENT );

		this.m_MinJTextField = new JTextField();
		this.m_MinJTextField.setText("0.0");
		this.m_MinJTextField.setPreferredSize( new Dimension( 45, 25 ) );
		this.m_MinJTextField.setMaximumSize( new Dimension( 45, 25 ) );
		this.m_MinJTextField.setMinimumSize( new Dimension( 45, 25 ) );
		this.m_MinJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_MinJTextField.setHorizontalAlignment( SwingConstants.TRAILING );

		this.m_MaxJCheckBox = new JCheckBox();
		this.m_MaxJCheckBox.setText("Max");
		this.m_MaxJCheckBox.setPreferredSize( new Dimension( 45, 25 ) );
		this.m_MaxJCheckBox.setMaximumSize( new Dimension( 45, 25 ) );
		this.m_MaxJCheckBox.setMinimumSize( new Dimension( 45, 25 ) );
		this.m_MaxJCheckBox.setAlignmentX( LEFT_ALIGNMENT );

		this.m_MaxJTextField = new JTextField();
		this.m_MaxJTextField.setText("1000.0");
		this.m_MaxJTextField.setPreferredSize( new Dimension( 45, 25 ) );
		this.m_MaxJTextField.setMaximumSize( new Dimension( 45, 25 ) );
		this.m_MaxJTextField.setMinimumSize( new Dimension( 45, 25 ) );
		this.m_MaxJTextField.setAlignmentX( RIGHT_ALIGNMENT );
		this.m_MaxJTextField.setHorizontalAlignment( SwingConstants.TRAILING );

		JPanel min = new JPanel();
		min.setLayout( new BoxLayout( min, BoxLayout.LINE_AXIS ) );
		min.setPreferredSize( new Dimension( 100, 25 ) );
		min.setMaximumSize( new Dimension( 100, 25 ) );
		min.setMinimumSize( new Dimension( 100, 25 ) );
		min.setAlignmentX( LEFT_ALIGNMENT );

		min.add( this.m_MinJCheckBox );
		min.add( this.m_MinJTextField );

		JPanel max = new JPanel();
		max.setLayout( new BoxLayout( max, BoxLayout.LINE_AXIS ) );
		max.setPreferredSize( new Dimension( 100, 25 ) );
		max.setMaximumSize( new Dimension( 100, 25 ) );
		max.setMinimumSize( new Dimension( 100, 25 ) );
		max.setAlignmentX( LEFT_ALIGNMENT );

		max.add( this.m_MaxJCheckBox );
		max.add( this.m_MaxJTextField );

		this.add( min );
		this.add( Box.createRigidArea( new Dimension( 0, 2 ) ) );
		this.add( max );
	}

	public JCheckBox getMinJCheckBox()
	{
		return this.m_MinJCheckBox;
	}

	public JCheckBox getMaxJCheckBox()
	{
		return this.m_MaxJCheckBox;
	}

	public String getMin()
	{
		return this.m_MinJTextField.getText();
	}

	public String getMax()
	{
		return this.m_MaxJTextField.getText();
	}
}
