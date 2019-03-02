package agi.samples.sharedresources.codesnippets;

// Java API
import java.io.*;

public abstract class CodeSnippet
{
	private String				m_name;
	private CodeSnippetReader	m_reader;

	public CodeSnippet(String name, String... fileParts)
	{
		this.m_name = name;

		File sourcePath = new File(System.getProperty("user.dir"));
		sourcePath = new File(sourcePath, "src");
		sourcePath = new File(sourcePath, "codesnippets");
		
		for (String part : fileParts) {
			sourcePath = new File(sourcePath, part);
		}
		
		this.m_reader = new CodeSnippetReader(sourcePath.getAbsolutePath());
	}

	/**
	 * Returns the filename that contains the code snippet.
	 */
	public final String getFileName()
	{
		return this.m_reader.getFileName();
	}

	/**
	 * Returns the filepath that contains the code snippet.
	 */
	public final String getFilePath()
	{
		return this.m_reader.getFilePath();
	}

	/**
	 * The name of this code snippet, for display in the tree.
	 */
	public String getName()
	{
		return this.m_name;
	}

	/**
	 * The short description of this code snippet.
	 */
	public String getShortDescription()
	{
		return "Not available";
	}
	
	/**
	 * The Long description of this code snippet.
	 */
	public String getLongDescription()
	{
		return "Not available";
	}

	/**
	 * Returns the source code for this snippet as a string.
	 * @throws IOException
	 */
	public final String getCode()
	throws IOException
	{
		return this.m_reader.getCode();
	}

	/**
	 * Returns the using directives required to run the example.
	 * @throws IOException
	 */
	public final String getImports()
	throws IOException
	{
		return this.m_reader.getImports();
	}
}