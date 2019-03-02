/* ==============================================================
 * This sample was last tested with the following configuration:
 * ==============================================================
 * Eclipse SDK Version: 4.2.0 Build id: I20120608-1400
 * JRE 1.6.0 and greater
 * STK 10.0
 * ==============================================================
 */

// Java API
import java.util.logging.*;

import javax.swing.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.*;
import agi.core.logging.*;
import agi.swing.plaf.metal.*;
// Samples API
import jsyntaxpane.*;

public class Main
{
	public static void main(String[] args)
	{
		try
		{
			AgCore_JNI.xInitThreads();
			
			if(AgMetalThemeFactory.getEnabled())
			{
				MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
				MetalLookAndFeel.setCurrentTheme(mt);
				UIManager.setLookAndFeel(MetalLookAndFeel.class.getName());
				JFrame.setDefaultLookAndFeelDecorated(true);
			}

			ConsoleHandler ch = new ConsoleHandler();
			ch.setLevel(Level.OFF);
			ch.setFormatter(new AgFormatter());
			Logger.getLogger("agi").setLevel(Level.OFF);
			Logger.getLogger("agi").addHandler(ch);

			Runnable r = new Runnable()
			{
				public void run()
				{
					try
					{
						DefaultSyntaxKit.initKit();
						DefaultSyntaxKit.setProperty("JavaSyntaxKit.RightMarginColumn", "0");
						DefaultSyntaxKit.setProperty("JavaSyntaxKit.Components", "jsyntaxpane.components.PairsMarker, jsyntaxpane.components.TokenMarker");

						MainWindow mw = new MainWindow();
						mw.setLocationRelativeTo(null);
						mw.setVisible(true);
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