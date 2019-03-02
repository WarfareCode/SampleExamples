package agi.samples.sharedresources.swing.codesnippets;

import agi.samples.sharedresources.codesnippets.*;

public class CodeSnippetSelectionJTreeNodeData
{
	private final String					m_imageName;
	private final String					m_text;
	private final CodeSnippet				m_codeSnippet;

	public CodeSnippetSelectionJTreeNodeData(String nodeText, String imageName)
	{
		this.m_imageName = imageName;
		this.m_text = nodeText;
		this.m_codeSnippet = null;
	}

	public CodeSnippetSelectionJTreeNodeData(CodeSnippet snippet, String imageName)
	{
		this.m_codeSnippet = snippet;
		this.m_imageName = imageName;
		this.m_text = snippet.getName();
	}

	public String getNodeText()
	{
		return this.m_text;
	}

	public String getImageName()
	{
		return this.m_imageName;
	}

	public CodeSnippet getCodeSnippet()
	{
		return this.m_codeSnippet;
	}
}