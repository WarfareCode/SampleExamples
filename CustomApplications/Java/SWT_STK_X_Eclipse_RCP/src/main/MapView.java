package main;

import org.eclipse.swt.widgets.Composite;
import org.eclipse.ui.part.ViewPart;
import agi.core.AgCoreException;

public class MapView
extends ViewPart
{
	public static final String	ID	= "main.MapView";
	private static MapComposite	s_map;

	public MapView()
	{
	}

	public void createPartControl(Composite parent)
	{
		try
		{
			s_map = new MapComposite(parent);
		}
		catch(AgCoreException ce)
		{
			System.out.println("HRESULT = " + ce.getHResultAsHexString());
			ce.printStackTrace();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}

	public void setFocus()
	{
		s_map.setFocus();
	}

	public static MapComposite getMapComposite()
	{
		return s_map;
	}
}
