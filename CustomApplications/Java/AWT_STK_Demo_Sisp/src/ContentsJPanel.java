import java.awt.*;
import javax.swing.*;

public class ContentsJPanel
extends JPanel
{
	private static final long serialVersionUID = 1L;

	private GlobeViewJPanel 		m_GlobeViewJPanel;
	private InfoJTabbedPane 		m_InfoJTabbedPane;
	private FeedsJTabbedPane		m_FeedsJTabbedPane;

	public ContentsJPanel()
	throws Exception
	{
		this.initialize();
	}

	private void initialize()
	throws Exception
	{
		this.setLayout( new BorderLayout() );
		this.setPreferredSize( new Dimension( 900, 500 ) );

		this.m_InfoJTabbedPane = new InfoJTabbedPane();
		this.add( this.m_InfoJTabbedPane, BorderLayout.WEST );

		this.m_GlobeViewJPanel = new GlobeViewJPanel();
		this.add( this.m_GlobeViewJPanel, BorderLayout.CENTER );

		this.m_FeedsJTabbedPane = new FeedsJTabbedPane();
		this.add( this.m_FeedsJTabbedPane, BorderLayout.EAST );
	}

	public GlobeViewJPanel getAnimationView()
	{
		return this.m_GlobeViewJPanel;
	}

	public InfoJTabbedPane getInfo()
	{
		return this.m_InfoJTabbedPane;
	}

	public FeedsJTabbedPane getFeeds()
	{
		return this.m_FeedsJTabbedPane;
	}
}
