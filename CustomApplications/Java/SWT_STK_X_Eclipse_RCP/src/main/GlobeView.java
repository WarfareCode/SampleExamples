package main;

import org.eclipse.swt.widgets.Composite;
import org.eclipse.ui.part.*;
import agi.core.AgCoreException;

public class GlobeView 
extends ViewPart 
{
	public static final String ID = "main.GlobeView";
	private static GlobeComposite s_globe;

	public GlobeView(){}

	public void createPartControl(Composite parent) 
	{
		try
		{
			s_globe = new GlobeComposite( parent );
		}
		catch( AgCoreException ce )
		{
			System.out.println( "HRESULT = " + ce.getHResultAsHexString() );
			ce.printStackTrace();
		}
		catch( Exception e )
		{
			e.printStackTrace();
		}
	}

	public void setFocus() {s_globe.setFocus();}
	
	public static GlobeComposite getGlobeComposite()
	{
		return s_globe;
	}
}