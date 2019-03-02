package agi.samples.sharedresources.codesnippets;

// Java API
import java.io.*;
import java.util.*;

// Samples API
import agi.samples.sharedresources.*;

/**
 * Reads #region sections from a source file and returns the using directives and codesnippets.
 */
public class CodeSnippetReader
{
	private String	m_filePath;
	private List<String>	m_fileContent;
	private String	m_codeSnippet;
	private String	m_imports;

	public CodeSnippetReader(String filePath)
	{
		m_filePath = filePath;
	}

	/**
	 * Returns the source code in the first '#region CodeSnippets' section. The string is cleaned up to display nicely in a rich text box.
	 * @throws IOException
	 */
	public final String getCode()
	throws IOException
	{
		if(m_codeSnippet == null)
		{
			m_codeSnippet = getRegion("CodeSnippet");
		}
		return m_codeSnippet;
	}

	/**
	 * Returns the source code in the first '#region Imports' section. The string is cleaned up to display nicely in a rich text box.
	 * @throws IOException
	 */
	public final String getImports()
	throws IOException
	{
		if(m_imports == null)
		{
			m_imports = getRegion("Imports");
		}
		return m_imports;
	}

	/**
	 * Returns the filename that contains the code snippet.
	 */
	public final String getFileName()
	{
		return new File(m_filePath).getName();
	}

	/**
	 * Returns the filepath that contains the code snippet.
	 */
	public final String getFilePath()
	{
		return m_filePath;
	}

	/**
	 * Loads the source file, if it is not already loaded, and extracts the source code in the first region defined by the input string.
	 * @throws IOException
	 */
	final private String getRegion(String region)
	throws IOException
	{
		if(m_fileContent == null)
		{
			m_fileContent = FileUtilities.readAllLines(m_filePath);
		}

		StringBuffer result = new StringBuffer();

		// Find all occurrences of the specified region.

		String startRegionToken = "#region " + region;
		String endRegionToken = "#endregion";
		boolean insideRegion = false;
		int spacesToRemove = -1;

		int cnt = m_fileContent.size();

		for(int i = 0; i < cnt; i++)
		{
			String line = m_fileContent.get(i);
			if(line.indexOf(startRegionToken) != -1)
			{
				// Start reading code snippet.
				insideRegion = true;
				spacesToRemove = -1;
				continue;
			}

			if(line.indexOf(endRegionToken) != -1)
			{
				// Stop reading code snippet.
				insideRegion = false;
				continue;
			}

			if(!insideRegion)
			{
				continue;
			}

			if(spacesToRemove < 0)
			{
				// Compute number of spaces before code on the first non-blank
				// line. All
				// following lines will have this many spaces removed from them
				// to
				// left-align the code in the rich text box.
				boolean isBlankLine = true;
				int spaces = 0;
				char[] ca = line.toCharArray();
				for(int j = 0; j < ca.length; j++)
				{
					char c = ca[j];
					if(c == ' ' || c == '\t')
					{
						spaces++;
					}
					else
					{
						isBlankLine = false;
						break;
					}
				}

				if(isBlankLine)
				{
					continue;
				}

				spacesToRemove = spaces;
			}

			// Remove spaces from the front of the line, if the line is long
			// enough.
			String lineOfCode = line;
			if(lineOfCode.length() > spacesToRemove)
			{
				lineOfCode = lineOfCode.substring(spacesToRemove);
			}

			result.append(lineOfCode);
			result.append('\n');
		}
		return result.toString();
	}
}