//This sample requires Java 1.7 or higher to compile

import javax.swing.JFrame;
import javax.swing.SwingUtilities;
import javax.swing.UIManager;
import javax.swing.plaf.metal.MetalLookAndFeel;
import agi.core.AgCoreException;
import agi.core.AgCore_JNI;
import agi.swing.plaf.metal.AgMetalThemeFactory;


public class Main
{
	public static void main(String[] args)
	{
		try
		{
			AgCore_JNI.xInitThreads();
			
			MetalLookAndFeel.setCurrentTheme(AgMetalThemeFactory.getDefaultMetalTheme());
			UIManager.setLookAndFeel(MetalLookAndFeel.class.getName());
			JFrame.setDefaultLookAndFeelDecorated(true);

			Runnable r = new Runnable()
			{
				public void run()
				{
					try
					{
						MainWindow mw = new MainWindow();
						mw.setLocationRelativeTo(null);
						mw.setVisible(true);
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