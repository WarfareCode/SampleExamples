import java.awt.Dimension;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.Box;
import javax.swing.JCheckBox;
import javax.swing.JPanel;

import javax.swing.border.Border;
import javax.swing.border.TitledBorder;

public class InfoAccessJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private final static String s_ACCESS = "Access";
	private final static String s_FIND_ACCESSES = "Find Accesses";

	private JCheckBox m_FindAccessesJCheckBox;

	public InfoAccessJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );

		Border b1 = BorderFactory.createLoweredBevelBorder();
		Border b2 = BorderFactory.createTitledBorder( b1, s_ACCESS, TitledBorder.LEFT, TitledBorder.ABOVE_TOP );
		this.setBorder( b2 );

		this.m_FindAccessesJCheckBox = new JCheckBox();
		this.m_FindAccessesJCheckBox.setText( s_FIND_ACCESSES );
		this.m_FindAccessesJCheckBox.setActionCommand( s_FIND_ACCESSES );
		this.m_FindAccessesJCheckBox.setPreferredSize( new Dimension( 125, 25 ) );
		this.m_FindAccessesJCheckBox.setMaximumSize( new Dimension( 125, 25 ) );
		this.m_FindAccessesJCheckBox.setMinimumSize( new Dimension( 125, 25 ) );
		this.m_FindAccessesJCheckBox.setAlignmentX( CENTER_ALIGNMENT );

		this.add( Box.createVerticalGlue() );
		this.add( this.m_FindAccessesJCheckBox );
		this.add( Box.createVerticalGlue() );
	}

	public JCheckBox getFindAccessesJCheckBox()
	{
		return this.m_FindAccessesJCheckBox;
	}
}
