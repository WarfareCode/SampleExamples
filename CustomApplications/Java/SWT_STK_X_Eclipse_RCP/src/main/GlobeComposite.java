package main;

import org.eclipse.swt.*;
import org.eclipse.swt.widgets.*;
import org.eclipse.swt.layout.*;
import agi.core.*;
import agi.stkx.swt.*;

public class GlobeComposite 
extends Composite 
{
	private AgGlobeCntrlClass 	m_AgGlobeCntrl;
	
	public GlobeComposite( Composite parent )
	throws AgException
	{
		super( parent, SWT.NONE );
		this.setLayout( new FillLayout( SWT.HORIZONTAL ));
		this.m_AgGlobeCntrl = new AgGlobeCntrlClass(this, SWT.NONE );
	}
	
	public AgGlobeCntrlClass getGlobe()
	{
		return this.m_AgGlobeCntrl;
	}
}