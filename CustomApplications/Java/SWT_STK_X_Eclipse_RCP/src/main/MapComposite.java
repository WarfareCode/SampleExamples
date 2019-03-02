package main;

import org.eclipse.swt.*;
import org.eclipse.swt.widgets.*;
import org.eclipse.swt.layout.*;
import agi.core.*;
import agi.stkx.swt.*;

public class MapComposite
extends Composite
{
	private AgMapCntrlClass	m_AgMapCntrl;

	public MapComposite(Composite parent)
	throws AgException
	{
		super(parent, SWT.NONE);
		this.setLayout(new FillLayout(SWT.HORIZONTAL));
		this.m_AgMapCntrl = new AgMapCntrlClass(this, SWT.NONE);
	}

	public AgMapCntrlClass getMap()
	{
		return this.m_AgMapCntrl;
	}
}
