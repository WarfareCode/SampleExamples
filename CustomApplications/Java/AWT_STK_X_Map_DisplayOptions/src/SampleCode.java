public class SampleCode
{
	/*package*/ static String getUserDefinedDefaultSplashFilePath()
	{
		String workingDirectory = System.getProperty("user.dir");
		String platFilePathSep = System.getProperty("file.separator");
		StringBuffer filePath = new StringBuffer();
		filePath.append(workingDirectory);
		filePath.append(platFilePathSep);
		filePath.append("src");
		filePath.append(platFilePathSep);
		filePath.append("AppSplashImage.bmp");

		return filePath.toString();
	}

	/*package*/ static String getUserDefinedDefaultProgressImageFilePath()
	{
		String workingDirectory = System.getProperty("user.dir");
		String platFilePathSep = System.getProperty("file.separator");
		StringBuffer filePath = new StringBuffer();
		filePath.append(workingDirectory);
		filePath.append(platFilePathSep);
		filePath.append("src");
		filePath.append(platFilePathSep);
		filePath.append("RedPlanetBlueTrail.gif");

		return filePath.toString();
	}
}