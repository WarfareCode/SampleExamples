package codesnippets;

// Java API
import javax.swing.*;
import javax.swing.tree.*;

// AGI Java API
import agi.stk.core.images.objects.*;

// Sample API
import agi.samples.sharedresources.codesnippets.*;
import agi.samples.sharedresources.swing.codesnippets.*;
import codesnippets.satellite.*;

public class DataProviderCodeSnippetLoader
implements ICodeSnippetLoader
{
	private JTree	m_CodeSnippetSelectionJTree;

	public DataProviderCodeSnippetLoader()
	{
	}

	public void load(JTree tree)
	{
		this.m_CodeSnippetSelectionJTree = tree;

		DefaultTreeModel treeModel = (DefaultTreeModel)this.m_CodeSnippetSelectionJTree.getModel();
		DefaultMutableTreeNode root = (DefaultMutableTreeNode)treeModel.getRoot();

		DefaultMutableTreeNode satelliteRoot = addParentTreeNode(root, "AgSatellite.gif", "Satellite");
		addTreeNode(satelliteRoot, new BetaAngleDataProviderCodeSnippet());
		addTreeNode(satelliteRoot, new ClassicalElementsJ2000DataProviderCodeSnippet());
		this.m_CodeSnippetSelectionJTree.expandPath(new TreePath(satelliteRoot.getPath()));

		//DefaultMutableTreeNode advCATRoot = addParentTreeNode(root, "AgAdvCAT.gif", "Advanced CAT");
		//DefaultMutableTreeNode aircraftRoot = addParentTreeNode(root, "AgAircraft.gif", "Aircraft");
		//DefaultMutableTreeNode areaTargetRoot = addParentTreeNode(root, "AgAreaTarget.gif", "Area Target");
		//DefaultMutableTreeNode attCovRoot = addParentTreeNode(root, "AgAttCov.gif", "Att Cov");
		//DefaultMutableTreeNode attFomRoot = addParentTreeNode(root, "AgAttFom.gif", "Att Fom");
		//DefaultMutableTreeNode chainRoot = addParentTreeNode(root, "AgChain.gif", "Chain");
		//DefaultMutableTreeNode commSystemRoot = addParentTreeNode(root, "AgCommSystem.gif", "Comm System");
		//DefaultMutableTreeNode constellationRoot = addParentTreeNode(root, "AgConstellation.gif", "Constellation");
		//DefaultMutableTreeNode covdefRoot = addParentTreeNode(root, "AgCovDef.gif", "Coverage Definition");
		//DefaultMutableTreeNode emtAntRoot = addParentTreeNode(root, "AgEmitterAnt.gif", "Emitter Antenna");
		//DefaultMutableTreeNode facilityRoot = addParentTreeNode(root, "AgFacility.gif", "Facility");
		// etc.
		
		this.m_CodeSnippetSelectionJTree.setSelectionRow(0);
	}

	private final DefaultMutableTreeNode addParentTreeNode(DefaultMutableTreeNode parentCollection, String imageName, String text)
	{
		CodeSnippetSelectionJTreeNodeData data = new CodeSnippetSelectionJTreeNodeData(text, imageName);
		DefaultMutableTreeNode parentNode = new DefaultMutableTreeNode(data);
		parentCollection.add(parentNode);
		return parentNode;
	}

	private final void addTreeNode(DefaultMutableTreeNode parent, CodeSnippet snippet)
	{
		CodeSnippetSelectionJTreeNodeData parentNodeData = null;
		parentNodeData = (CodeSnippetSelectionJTreeNodeData)parent.getUserObject();

		String imageName = parentNodeData.getImageName();
		CodeSnippetSelectionJTreeNodeData childNodeData = null;
		childNodeData = new CodeSnippetSelectionJTreeNodeData(snippet, imageName);

		DefaultMutableTreeNode node = new DefaultMutableTreeNode(childNodeData);
		parent.add(node);
	}

	public String getRootName()
	{
		return "STK Objects Data Providers How To";
	}

	public Class<AgStkobjectsImageHelper> getImageHelperClass()
	{
		return AgStkobjectsImageHelper.class;
	}
}
