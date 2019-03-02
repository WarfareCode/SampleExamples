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
import agi.core.*;

public class Main
{
	public static void main(String[] args)
    {
        try
        {
        	if(!System.getProperty("os.name").toLowerCase().startsWith("win"))
			{
				System.out.println("This sample application can only be run on a Microsoft Windows Flavor Operating System");
				return;
			}
        	
        	AgCore_JNI.xInitThreads();
        	
			Runnable r = new Runnable()
			{
				public void run()
				{
					try
					{
			        	MainWindow m = new MainWindow();
			        	m.run();
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
			SwingUtilities.invokeLater(r);
        }
        catch(Throwable t)
        {
        	t.printStackTrace();
        }
    }
}