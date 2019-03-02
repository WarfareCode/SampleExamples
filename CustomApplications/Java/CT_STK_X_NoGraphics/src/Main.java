// AGI Java API
import agi.core.*;

public class Main
{	
	public static void main(String[] args)
	{
		try
		{
			AgCore_JNI.xInitThreads();
			
			Access access = new Access();
			access.compute();
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
}