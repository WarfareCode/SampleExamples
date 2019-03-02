// Java API
import java.awt.event.*;

// AGI Java API
import agi.core.awt.*;
import agi.stk.plugin.AgStkExtension_JNI;
import agi.stkengine.*;


public class MainWindowAdapter
extends WindowAdapter
{
	private MainJFrame m_Frame;
	
	public MainWindowAdapter(MainJFrame frame)
	{
		this.m_Frame = frame;
	}

	public void windowClosing(WindowEvent evt)
	{
		try
		{
			// Must dispose your control before uninitializing the API
			this.m_Frame.getGlobeJPanel().getControl().dispose();

			// Reverse of the initialization order
			AgStkExtension_JNI.uninitialize();
			AgAwt_JNI.uninitialize_AwtComponents();
			AgStkCustomApplication_JNI.uninitialize();
			AgAwt_JNI.uninitialize_AwtDelegate();
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
}
