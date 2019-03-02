package agi.samples.sharedresources;

// Java API
import java.io.*;
import java.util.*;

public class FileUtilities
{
	public static String readAllText(String path)
	throws IOException
	{
		BufferedReader reader = new BufferedReader(new FileReader(path));
		char[] buffer = new char[8192];
		StringBuffer result = new StringBuffer(8192);

		int charsRead;
		while(-1 != (charsRead = reader.read(buffer)))
		{
			result.append(buffer, 0, charsRead);
		}

		reader.close();

		return result.toString();
	}

	public static ArrayList<String> readAllLines(String path) throws IOException 
	{
		BufferedReader reader = new BufferedReader(new FileReader(path));

		ArrayList<String> list = new ArrayList<String>();
		String line;
		while ((line = reader.readLine()) != null) 
		{
			list.add(line);
		}

		reader.close();

		return list;
	}
}
