/* ==============================================================
 * This sample was last tested with the following configuration:
 * ==============================================================
 * Eclipse SDK Version: 4.2.0 Build id: I20120608-1400
 * JRE 1.4.2_10 and greater
 * STK 10.0
 * ==============================================================
 */

// Java API
import javax.swing.*;

// AGI Java API
// TODO: #4 Add some STK Java API imports for agi logging, 
// AWT core, AWT STK X controls, and STK Engine Custom Application 
// initialization/uninitialization
import agi.core.*;

public class Main
{
	public static void main(String[] args)
	{
		try
		{
			AgCore_JNI.xInitThreads();
			
			Runnable r = new Runnable()
			{

				public void run()
				{
					try
					{
						MainWindow mw = new MainWindow();
						mw.setVisible(true);
						mw.setLocationRelativeTo(null);
					}
					catch(AgCoreException ce)
					{
						ce.printHexHresult();
						ce.printStackTrace();
					}
					catch(Throwable t)
					{
						t.printStackTrace();
					}
				}
			};
			// TODO:  #2 Place the MainRunnable on the AWT Event Queue for processing in
			// a non-blocking invocation using javax.swing.SwingUtilities class.
			SwingUtilities.invokeLater(r);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

}