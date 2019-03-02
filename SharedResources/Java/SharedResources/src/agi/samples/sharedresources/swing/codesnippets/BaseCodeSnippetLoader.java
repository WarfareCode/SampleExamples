package agi.samples.sharedresources.swing.codesnippets;

import javax.swing.tree.DefaultMutableTreeNode;

import agi.samples.sharedresources.codesnippets.CodeSnippet;

public abstract class BaseCodeSnippetLoader
implements ICodeSnippetLoader
{
	protected final DefaultMutableTreeNode addParentTreeNode(DefaultMutableTreeNode parentCollection, String imageName, String text)
	{
		CodeSnippetSelectionJTreeNodeData data = new CodeSnippetSelectionJTreeNodeData(text, imageName);
		DefaultMutableTreeNode parentNode = new DefaultMutableTreeNode(data);
		parentCollection.add(parentNode);
		return parentNode;
	}

	protected final void addTreeNode(DefaultMutableTreeNode parent, CodeSnippet snippet)
	{
		CodeSnippetSelectionJTreeNodeData parentNodeData = null;
		parentNodeData = (CodeSnippetSelectionJTreeNodeData)parent.getUserObject();

		String imageName = parentNodeData.getImageName();
		CodeSnippetSelectionJTreeNodeData childNodeData = null;
		childNodeData = new CodeSnippetSelectionJTreeNodeData(snippet, imageName);

		DefaultMutableTreeNode node = new DefaultMutableTreeNode(childNodeData);
		parent.add(node);
	}
}