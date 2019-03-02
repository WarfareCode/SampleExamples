package utils;

// AGI Java API
import agi.core.*;

public class DataPaths
{
	private static DataPaths s_DataPaths;
	
	private String m_UserDir;
	private String m_FileSep;
	private String m_SharedDataDir;
	
	public static DataPaths getDataPaths()
	throws AgCoreException
	{
		if(s_DataPaths == null)
		{
			s_DataPaths = new DataPaths();
		}
		return s_DataPaths;
	}
	
	private DataPaths() 
	throws AgCoreException
	{
		this.m_FileSep = AgSystemPropertiesHelper.getFileSeparator();
		this.m_UserDir = AgSystemPropertiesHelper.getUserDir();
		this.m_SharedDataDir = this.m_UserDir + this.m_FileSep + ".." + this.m_FileSep + ".." + this.m_FileSep + "Data" + this.m_FileSep + "HowTo";
	}
	
    public String getSharedDataPath(String subDirPath)
    {
    	return this.m_SharedDataDir + this.m_FileSep + subDirPath;
    }

    public String getSampleProjectPath(String subDirPath)
    {
    	return this.m_UserDir + this.m_FileSep + subDirPath;
    }
}
