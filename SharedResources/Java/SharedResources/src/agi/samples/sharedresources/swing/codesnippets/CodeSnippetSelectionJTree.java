package agi.samples.sharedresources.swing.codesnippets;

//Java API
//import java.awt.*;
import javax.swing.*;
import javax.swing.tree.*;

public class CodeSnippetSelectionJTree
extends JTree
{
	private static final long	serialVersionUID	= 1L;

	public CodeSnippetSelectionJTree(String rootName, Class<?> imageHelperClass)
	{
		super(new DefaultMutableTreeNode(new CodeSnippetSelectionJTreeNodeData(rootName, null)));
		this.setCellRenderer(new CodeSnippetSelectionJTreeIconRenderer(imageHelperClass));
		this.setRootVisible(false);
		this.setShowsRootHandles(true);
		//this.setBackground(Color.WHITE);

		this.getSelectionModel().setSelectionMode(TreeSelectionModel.SINGLE_TREE_SELECTION);
	}
}
