package agi.samples.sharedresources.swing.codesnippets;

// Java API
import java.net.*;
import java.awt.*;

import javax.swing.*;
import javax.swing.tree.*;

// Provides custom icons for our tree
public class CodeSnippetSelectionJTreeIconRenderer
extends DefaultTreeCellRenderer
{
	private static final long	serialVersionUID	= 1L;

	private Class<?> m_ImageHelperClass;
	
	public CodeSnippetSelectionJTreeIconRenderer(Class<?> imageHelperClass)
	{
		this.m_ImageHelperClass = imageHelperClass;
	}

	public Component getTreeCellRendererComponent(JTree tree, Object value, boolean sel, boolean expanded, boolean leaf, int row, boolean hasFocus)
	{
		super.getTreeCellRendererComponent(tree, value, sel, expanded, leaf, row, hasFocus);

		DefaultMutableTreeNode node = (DefaultMutableTreeNode)value;
		CodeSnippetSelectionJTreeNodeData nodeData = (CodeSnippetSelectionJTreeNodeData)node.getUserObject();

		if(nodeData != null)
		{
			String nodeText = nodeData.getNodeText();
			if(nodeText != null) this.setText(nodeText);

			String imageName = nodeData.getImageName();
			if(imageName != null)
			{
				URL res = this.m_ImageHelperClass.getResource(imageName);
				ImageIcon ii = new ImageIcon(res);
				setIcon(ii);
			}
		}
		return this;
	}

//	@Override
//	public Color getBackgroundNonSelectionColor()
//	{
//		return Color.WHITE;
//	}
//
//	@Override
//	public Color getTextSelectionColor()
//	{
//		return Color.WHITE;
//	}
}
