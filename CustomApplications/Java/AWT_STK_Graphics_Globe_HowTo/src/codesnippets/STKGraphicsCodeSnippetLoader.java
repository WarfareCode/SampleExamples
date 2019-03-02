package codesnippets;

// Java API
import java.awt.*;

import javax.swing.*;
import javax.swing.tree.*;

// Sample API
import codesnippets.camera.*;
import codesnippets.displayconditions.*;
import codesnippets.globeoverlays.*;
import codesnippets.imaging.*;
import codesnippets.picking.*;
import codesnippets.primitives.composite.*;
import codesnippets.primitives.markerbatch.*;
import codesnippets.primitives.model.*;
import codesnippets.primitives.orderedcomposite.*;
import codesnippets.primitives.path.*;
import codesnippets.primitives.pointbatch.*;
import codesnippets.primitives.polyline.*;
import codesnippets.primitives.solid.*;
import codesnippets.primitives.surfacemesh.*;
import codesnippets.primitives.textbatch.*;
import codesnippets.primitives.trianglemesh.*;
import codesnippets.screenoverlays.*;
import codesnippets.ui.selection.images.*;
import agi.core.AgOSHelper;
import agi.samples.sharedresources.swing.codesnippets.*;

public class STKGraphicsCodeSnippetLoader
implements ICodeSnippetLoader
{
	private Component	m_Component;
	private JTree		m_CodeSnippetSelectionJTree;

	public STKGraphicsCodeSnippetLoader(Component c)
	{
		this.m_Component = c;
	}

	public void load(JTree tree)
	{
		this.m_CodeSnippetSelectionJTree = tree;

		DefaultTreeModel treeModel = (DefaultTreeModel)this.m_CodeSnippetSelectionJTree.getModel();
		DefaultMutableTreeNode root = (DefaultMutableTreeNode)treeModel.getRoot();

		DefaultMutableTreeNode globeOverlayRoot = addParentTreeNode(root, "GlobeOverlay.png", "Globe Overlays");
		addTreeNode(globeOverlayRoot, new GlobeImageOverlayCodeSnippet(this.m_Component));
		addTreeNode(globeOverlayRoot, new TerrainOverlayCodeSnippet(this.m_Component));
		if (!AgOSHelper.isOnLinuxPlatform())
		{
			//Streaming video is currently not supported on linux so don't display code snippets using it
			addTreeNode(globeOverlayRoot, new ProjectedImageCodeSnippet(this.m_Component));
			addTreeNode(globeOverlayRoot, new ProjectedImageModelsCodeSnippet(this.m_Component));
		}
		addTreeNode(globeOverlayRoot, new GlobeOverlayRenderOrderCodeSnippet(this.m_Component));
        addTreeNode(globeOverlayRoot, new OpenStreetMapCodeSnippet(this.m_Component));

		DefaultMutableTreeNode primitiveRoot = addParentTreeNode(root, "Primitives.png", "Primitives");

		DefaultMutableTreeNode modelRoot = addParentTreeNode(primitiveRoot, "Models.png", "Model");
		addTreeNode(modelRoot, new ModelCodeSnippet(this.m_Component));
		addTreeNode(modelRoot, new ModelColladaFXCodeSnippet(this.m_Component));
		addTreeNode(modelRoot, new ModelOrientationCodeSnippet(this.m_Component));
		addTreeNode(modelRoot, new ModelVectorOrientationCodeSnippet(this.m_Component));
		addTreeNode(modelRoot, new ModelArticulationCodeSnippet(this.m_Component));
		if (!AgOSHelper.isOnLinuxPlatform())
		{
			//Streaming video is currently not supported on linux so don't display code snippets using it
			addTreeNode(modelRoot, new ModelDynamicCodeSnippet(this.m_Component));
		}

		DefaultMutableTreeNode markerRoot = addParentTreeNode(primitiveRoot, "MarkerBatch.png", "Marker Batch");
		addTreeNode(markerRoot, new MarkerBatchCodeSnippet(this.m_Component));
		addTreeNode(markerRoot, new MarkerBatchAggregationDeaggregationCodeSnippet(this.m_Component));

		DefaultMutableTreeNode polylineRoot = addParentTreeNode(primitiveRoot, "Polyline.png", "Polyline");
		addTreeNode(polylineRoot, new PolylineCodeSnippet(this.m_Component));
		addTreeNode(polylineRoot, new PolylineGreatArcCodeSnippet(this.m_Component));
		addTreeNode(polylineRoot, new PolylineRhumbLineCodeSnippet(this.m_Component));
		addTreeNode(polylineRoot, new PolylineAreaTargetCodeSnippet(this.m_Component));
		addTreeNode(polylineRoot, new PolylineCircleCodeSnippet(this.m_Component));
		addTreeNode(polylineRoot, new PolylineEllipseCodeSnippet(this.m_Component));

		DefaultMutableTreeNode pathRoot = addParentTreeNode(primitiveRoot, "Path.png", "Path");
		addTreeNode(pathRoot, new PathDropLineCodeSnippet(this.m_Component));
		addTreeNode(pathRoot, new PathTrailLineCodeSnippet(this.m_Component));

		DefaultMutableTreeNode triangleMeshRoot = addParentTreeNode(primitiveRoot, "TriangleMesh.png", "Triangle Mesh");
		addTreeNode(triangleMeshRoot, new TriangleMeshAreaTargetCodeSnippet(this.m_Component));
		addTreeNode(triangleMeshRoot, new TriangleMeshWithHoleCodeSnippet(this.m_Component));
		addTreeNode(triangleMeshRoot, new TriangleMeshExtrusionCodeSnippet(this.m_Component));
		addTreeNode(triangleMeshRoot, new TriangleMeshExtentCodeSnippet(this.m_Component));
		addTreeNode(triangleMeshRoot, new TriangleMeshCircleCodeSnippet(this.m_Component));
		addTreeNode(triangleMeshRoot, new TriangleMeshEllipseCodeSnippet(this.m_Component));

		DefaultMutableTreeNode surfaceMeshRoot = addParentTreeNode(primitiveRoot, "SurfaceMesh.png", "Surface Mesh");
		addTreeNode(surfaceMeshRoot, new SurfaceMeshAreaTargetCodeSnippet(this.m_Component));
		addTreeNode(surfaceMeshRoot, new SurfaceMeshTexturedExtentCodeSnippet(this.m_Component));
		addTreeNode(surfaceMeshRoot, new SurfaceMeshDynamicImageCodeSnippet(this.m_Component));
		addTreeNode(surfaceMeshRoot, new SurfaceMeshTransformationsCodeSnippet(this.m_Component));
		addTreeNode(surfaceMeshRoot, new SurfaceMeshTrapezoidalTextureCodeSnippet(this.m_Component));

		DefaultMutableTreeNode solidRoot = addParentTreeNode(primitiveRoot, "Solid.png", "Solid");
		addTreeNode(solidRoot, new SolidEllipsoidCodeSnippet(this.m_Component));
		addTreeNode(solidRoot, new SolidBoxCodeSnippet(this.m_Component));
		addTreeNode(solidRoot, new SolidCylinderCodeSnippet(this.m_Component));

		DefaultMutableTreeNode pointbatchRoot = addParentTreeNode(primitiveRoot, "PointBatch.png", "Point Batch");
		addTreeNode(pointbatchRoot, new PointBatchCodeSnippet(this.m_Component));
		addTreeNode(pointbatchRoot, new PointBatchColorsCodeSnippet(this.m_Component));

		DefaultMutableTreeNode textbatchRoot = addParentTreeNode(primitiveRoot, "TextBatch.png", "Text Batch");
		addTreeNode(textbatchRoot, new TextBatchCodeSnippet(this.m_Component));
		addTreeNode(textbatchRoot, new TextBatchColorsCodeSnippet(this.m_Component));
		addTreeNode(textbatchRoot, new TextBatchUnicodeCodeSnippet(this.m_Component));

		DefaultMutableTreeNode compositeRoot = addParentTreeNode(primitiveRoot, "Composite.png", "Composite");
		addTreeNode(compositeRoot, new CompositeLayersCodeSnippet(this.m_Component));

		DefaultMutableTreeNode orderedCompositeRoot = addParentTreeNode(primitiveRoot, "OrderedComposite.png", "Ordered Composite");
		addTreeNode(orderedCompositeRoot, new OrderedCompositeZOrderCodeSnippet(this.m_Component));

		DefaultMutableTreeNode displayConditionRoot = addParentTreeNode(root, "DisplayCondition.png", "Display Conditions");
		addTreeNode(displayConditionRoot, new AltitudeDisplayConditionCodeSnippet(this.m_Component));
		addTreeNode(displayConditionRoot, new DistanceDisplayConditionCodeSnippet(this.m_Component));
		addTreeNode(displayConditionRoot, new TimeDisplayConditionCodeSnippet(this.m_Component));
		addTreeNode(displayConditionRoot, new CompositeDisplayConditionCodeSnippet(this.m_Component));
		addTreeNode(displayConditionRoot, new ScreenOverlayDisplayConditionCodeSnippet(this.m_Component));

		DefaultMutableTreeNode screenOverlayRoot = addParentTreeNode(root, "ScreenOverlay.png", "Screen Overlays");
		addTreeNode(screenOverlayRoot, new OverlaysTextureCodeSnippet(this.m_Component));
		addTreeNode(screenOverlayRoot, new OverlaysTextCodeSnippet(this.m_Component));
		addTreeNode(screenOverlayRoot, new OverlaysPanelCodeSnippet(this.m_Component));
		if (!AgOSHelper.isOnLinuxPlatform())
		{
			//Streaming video is currently not supported on linux so don't display code snippets using it
			addTreeNode(screenOverlayRoot, new OverlaysVideoCodeSnippet(this.m_Component));
		}
		addTreeNode(screenOverlayRoot, new OverlaysMemoryCodeSnippet(this.m_Component));

		DefaultMutableTreeNode pickingRoot = addParentTreeNode(root, "Picking.png", "Picking");
		addTreeNode(pickingRoot, new PickRectangularCodeSnippet(this.m_Component));
		addTreeNode(pickingRoot, new PickChangeColorCodeSnippet(this.m_Component));
		addTreeNode(pickingRoot, new PickPerItemCodeSnippet(this.m_Component));
		addTreeNode(pickingRoot, new PickZoomCodeSnippet(this.m_Component));

		DefaultMutableTreeNode cameraRoot = addParentTreeNode(root, "Camera.png", "Camera");
		addTreeNode(cameraRoot, new CameraFollowingSatelliteCodeSnippet(this.m_Component));
		addTreeNode(cameraRoot, new CameraFollowingSplineCodeSnippet(this.m_Component));
		addTreeNode(cameraRoot, new CameraFixedFrameCodeSnippet(this.m_Component));
		addTreeNode(cameraRoot, new CameraRecordingSnapshotCodeSnippet(this.m_Component));

		DefaultMutableTreeNode imagingRoot = addParentTreeNode(root, "Imaging.png", "Imaging");
		addTreeNode(imagingRoot, new ImageFlipCodeSnippet(this.m_Component));
		addTreeNode(imagingRoot, new ImageSwizzleCodeSnippet(this.m_Component));
		addTreeNode(imagingRoot, new ImageChannelExtractCodeSnippet(this.m_Component));
		addTreeNode(imagingRoot, new ImageColorCorrectionCodeSnippet(this.m_Component));
		addTreeNode(imagingRoot, new ImageLevelsCodeSnippet(this.m_Component));
		addTreeNode(imagingRoot, new ImageConvolutionMatrixCodeSnippet(this.m_Component));
		addTreeNode(imagingRoot, new ImageDynamicCodeSnippet(this.m_Component));

		// Expand Tree
		this.m_CodeSnippetSelectionJTree.expandPath(new TreePath(globeOverlayRoot.getPath()));
		this.m_CodeSnippetSelectionJTree.expandPath(new TreePath(primitiveRoot.getPath()));

		this.m_CodeSnippetSelectionJTree.setSelectionRow(0);
	}

	private final DefaultMutableTreeNode addParentTreeNode(DefaultMutableTreeNode parentCollection, String imageName, String text)
	{
		CodeSnippetSelectionJTreeNodeData data = new CodeSnippetSelectionJTreeNodeData(text, imageName);
		DefaultMutableTreeNode parentNode = new DefaultMutableTreeNode(data);
		parentCollection.add(parentNode);
		return parentNode;
	}

	private final void addTreeNode(DefaultMutableTreeNode parent, STKGraphicsCodeSnippet snippet)
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
		return "STK Graphics How To";
	}

	public Class<CodeSnippetSelectionImageHelper> getImageHelperClass()
	{
		return CodeSnippetSelectionImageHelper.class;
	}
}
