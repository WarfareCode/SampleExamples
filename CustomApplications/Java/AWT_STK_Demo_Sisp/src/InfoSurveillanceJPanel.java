import java.awt.*;
import javax.swing.*;

public class InfoSurveillanceJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private InfoAccessJPanel 	m_InfoAccessJPanel;
	private LLAJPanel 			m_LLAJPanel;
	private XYJPanel			m_XYJPanel;

	public InfoSurveillanceJPanel()
	{
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout( new BoxLayout( this, BoxLayout.PAGE_AXIS ) );

		this.m_InfoAccessJPanel = new InfoAccessJPanel();
		this.m_InfoAccessJPanel.setPreferredSize( new Dimension( 150, 60 ) );
		this.m_InfoAccessJPanel.setMaximumSize( new Dimension( 150, 60 ) );
		this.m_InfoAccessJPanel.setMinimumSize( new Dimension( 150, 60 ) );

		this.m_LLAJPanel = new LLAJPanel();
		this.m_LLAJPanel.setPreferredSize( new Dimension( 150, 100 ) );
		this.m_LLAJPanel.setMaximumSize( new Dimension( 150, 100 ) );
		this.m_LLAJPanel.setMinimumSize( new Dimension( 150, 100 ) );

		this.m_XYJPanel = new XYJPanel();
		this.m_XYJPanel.setPreferredSize( new Dimension( 150, 100 ) );
		this.m_XYJPanel.setMaximumSize( new Dimension( 150, 100 ) );
		this.m_XYJPanel.setMinimumSize( new Dimension( 150, 100 ) );

		this.add( this.m_InfoAccessJPanel );
		this.add( this.m_LLAJPanel );
		this.add( this.m_XYJPanel );
		this.add( Box.createVerticalGlue() );
	}

	public InfoAccessJPanel getAccess()
	{
		return this.m_InfoAccessJPanel;
	}

	public LLAJPanel getLLA()
	{
		return this.m_LLAJPanel;
	}

	public XYJPanel getXY()
	{
		return this.m_XYJPanel;
	}
}
