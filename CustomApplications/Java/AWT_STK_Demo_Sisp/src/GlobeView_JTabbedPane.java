import java.awt.*;

import javax.swing.*;
import javax.swing.plaf.metal.MetalTheme;

import agi.core.*;
import agi.core.awt.*;
import agi.stkx.awt.*;
import agi.swing.plaf.metal.AgMetalThemeFactory;

public class GlobeView_JTabbedPane
extends JTabbedPane
{
	private static final long serialVersionUID = 1L;

	private AgGlobeCntrlClass	m_AgGlobeCntrl;
    private AgMapCntrlClass 		m_AgMapCntrl;

    public GlobeView_JTabbedPane()
    throws Exception
    {
    	super();
    	this.initialize();
    }

    private void initialize()
    throws Exception
    {
		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgMapCntrl = new AgMapCntrlClass();
        this.m_AgMapCntrl.setBackColor(stkxColor);
        this.m_AgMapCntrl.setBackground(awtColor);
        JPanel mapJPanel = new JPanel();
        mapJPanel.setLayout( new BorderLayout() );
        mapJPanel.add( this.m_AgMapCntrl, BorderLayout.CENTER  );
        this.add("2D", mapJPanel );

        this.m_AgGlobeCntrl = new AgGlobeCntrlClass();
        this.m_AgGlobeCntrl.setBackColor(stkxColor);
        this.m_AgGlobeCntrl.setBackground(awtColor);
        JPanel voJPanel = new JPanel();
        voJPanel.setLayout( new BorderLayout() );
        voJPanel.add( this.m_AgGlobeCntrl, BorderLayout.CENTER );
        this.addTab("3D", voJPanel );

        this.setSelectedIndex( this.indexOfTab("3D") );
    }

    public AgGlobeCntrlClass getVO()
    {
    	return this.m_AgGlobeCntrl;
    }

    public AgMapCntrlClass get2D()
    {
    	return this.m_AgMapCntrl;
    }
}