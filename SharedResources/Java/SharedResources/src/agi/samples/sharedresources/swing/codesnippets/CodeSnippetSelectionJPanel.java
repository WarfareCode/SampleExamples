package agi.samples.sharedresources.swing.codesnippets;

// Java API
import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;

public class CodeSnippetSelectionJPanel
extends JPanel
{
	private static final long					serialVersionUID	= 1L;

	private CodeSnippetSelectionJTree			m_CodeSnippetSelectionJTree;

	public CodeSnippetSelectionJPanel()
	{
		this.setLayout(new BorderLayout());
		this.setBorder(new BevelBorder(BevelBorder.LOWERED));
	}

	public void load(ICodeSnippetLoader loader)
	{
		this.m_CodeSnippetSelectionJTree = new CodeSnippetSelectionJTree(loader.getRootName(), loader.getImageHelperClass());
		JScrollPane treeScroller = new JScrollPane(this.m_CodeSnippetSelectionJTree);
		this.add(treeScroller, BorderLayout.CENTER);
		loader.load(this.m_CodeSnippetSelectionJTree);
	}

	public CodeSnippetSelectionJTree getCodeSnippetSelectionJTree()
	{
		return this.m_CodeSnippetSelectionJTree;
	}
}