using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AGI;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKX;
using GraphicsHowTo.Camera;
using GraphicsHowTo.DisplayConditions;
using GraphicsHowTo.GlobeOverlays;
using GraphicsHowTo.Imaging;
using GraphicsHowTo.Picking;
using GraphicsHowTo.Primitives;
using GraphicsHowTo.Primitives.Composite;
using GraphicsHowTo.Primitives.MarkerBatch;
using GraphicsHowTo.Primitives.Model;
using GraphicsHowTo.Primitives.OrderedComposite;
using GraphicsHowTo.Primitives.PointBatch;
using GraphicsHowTo.Primitives.Polyline;
using GraphicsHowTo.Primitives.Solid;
using GraphicsHowTo.Primitives.SurfaceMesh;
using GraphicsHowTo.Primitives.TextBatch;
using GraphicsHowTo.Primitives.TriangleMesh;
using GraphicsHowTo.ScreenOverlays;

namespace GraphicsHowTo
{
    public partial class HowToForm : Form
    {
        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        public HowToForm()
        {
            InitializeComponent();

            instance = this;

            string[] icons = new string[]
                                 {
                                     "Balloon.png",
                                     "Camera.png",
                                     "Composite.png",
                                     "OrderedComposite.png",
                                     "DisplayCondition.png",
                                     "Filtering.png",
                                     "GlobeOverlay.png",
                                     "Imaging.png",
                                     "KML.png",
                                     "MarkerBatch.png",
                                     "Models.png",
                                     "Path.png",
                                     "Picking.png",
                                     "PointBatch.png",
                                     "Polyline.png",
                                     "Primitives.png",
                                     "ScreenOverlay.png",
                                     "Solid.png",
                                     "SurfaceMesh.png",
                                     "TextBatch.png",
                                     "Tracking.png",
                                     "TriangleMesh.png",
                                     "Visualizer.png",
                                 };
            // load tree icons from embedded resource files, storing them under their base file name.
            foreach (string icon in icons)
            {
                ilTree.Images.Add(icon, new Bitmap(GetType(), "TreeIcons." + icon));
            }
        }

        private void HowToForm_Load(object sender, EventArgs e)
        {
            // See HowToForm_Shown
        }

        private void HowToForm_Shown(object sender, EventArgs e)
        {
            //
            // Set up global variables for STK Engine
            //
            this.Refresh();
            stkxApp = new AGI.STKX.AgSTKXApplication();
            root = new AgStkObjectRoot();
            root.NewScenario("HowTo");
            manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Scene = manager.Scenes[0];
            m_Control3D = this.axAgUiAxVOCntrl1;

            //
            // Set unit and animation preferences to be consistent throughout the application.
            //
            setupUnitPreferences();
            CodeSnippet.SetAnimationDefaults(root);

            //
            // Try to remove default STK annotations and overlays that obscure the toolbar
            //
            root.BeginUpdate();
            try
            {
                root.ExecuteCommand("VO * Annotation Time Show Off ShowTimeStep Off");
                root.ExecuteCommand("VO * Annotation Frame Show Off");
                root.ExecuteCommand("VO * Overlay Modify \"AGI_logo_small.ppm\" Show Off");
            }
            catch (Exception) { }
            root.EndUpdate();

            m_Overlay = new OverlayToolbar(root, this.axAgUiAxVOCntrl1);
            m_modelVectorOrientation = new ModelVectorOrientationCodeSnippet(((IAgScenario)root.CurrentScenario).StartTime);
            m_modelArticulation = new ModelArticulationCodeSnippet(((IAgScenario)root.CurrentScenario).StartTime);
            m_pathDropLines = new PathDropLineCodeSnippet(((IAgScenario)root.CurrentScenario).StartTime);
            m_surfaceMeshTransformations = new SurfaceMeshTransformationsCodeSnippet();
            m_cameraFollowingSatellite = new CameraFollowingSatelliteCodeSnippet();
            m_timeDisplayCondition = new TimeDisplayConditionCodeSnippet();
            m_compositeDisplayCondition = new CompositeDisplayConditionCodeSnippet();

            rtbCode.BackColor = Color.White;

            //
            // Add code snippets to treeview
            //
            TreeNode globeOverlayRoot = AddParentTreeNode(tvCode.Nodes, "GlobeOverlay.png", "Globe Overlays");
            AddTreeNode(globeOverlayRoot, "Add jp2 imagery to the globe", new GlobeImageOverlayCodeSnippet());
            AddTreeNode(globeOverlayRoot, "Add terrain to the globe", new TerrainOverlayCodeSnippet());
            AddTreeNode(globeOverlayRoot, "Add projected imagery to the globe", new ProjectedImageCodeSnippet());
            AddTreeNode(globeOverlayRoot, "Project imagery on models", new ProjectedImageModelsCodeSnippet());
            AddTreeNode(globeOverlayRoot, "Draw an image on top of another", new GlobeOverlayRenderOrderCodeSnippet());
            AddTreeNode(globeOverlayRoot, "Add custom imagery to the globe", new OpenStreetMapCodeSnippet());


            TreeNode primitiveRoot = AddParentTreeNode(tvCode.Nodes, "Primitives.png", "Primitives");
            primitiveRoot.ContextMenuStrip = cmParent;

            TreeNode modelRoot = AddParentTreeNode(primitiveRoot.Nodes, "Models.png", "Model");
            AddTreeNode(modelRoot, "Draw a Collada or MDL model", new ModelCodeSnippet());
            AddTreeNode(modelRoot, "Draw a Collada model with user defined lighting", new ModelColladaFXCodeSnippet());
            AddTreeNode(modelRoot, "Orient a model", new ModelOrientationCodeSnippet());
            AddTreeNode(modelRoot, "Orient a model along its velocity vector", m_modelVectorOrientation);
            AddTreeNode(modelRoot, "Draw a model with moving articulations", m_modelArticulation);
            AddTreeNode(modelRoot, "Draw a dynamically textured Collada model", new ModelDynamicCodeSnippet());

            TreeNode markerRoot = AddParentTreeNode(primitiveRoot.Nodes, "MarkerBatch.png", "Marker Batch");
            AddTreeNode(markerRoot, "Draw a set of markers", new MarkerBatchCodeSnippet());
            AddTreeNode(markerRoot, "Draw and combine sets of marker batches at various distances", new MarkerBatchAggregationDeaggregationCodeSnippet());

            TreeNode polylineRoot = AddParentTreeNode(primitiveRoot.Nodes, "Polyline.png", "Polyline");
            AddTreeNode(polylineRoot, "Draw a line between two points", new PolylineCodeSnippet());
            AddTreeNode(polylineRoot, "Draw a great arc on the globe", new PolylineGreatArcCodeSnippet());
            AddTreeNode(polylineRoot, "Draw a rhumb line on the globe", new PolylineRhumbLineCodeSnippet());
            AddTreeNode(polylineRoot, "Draw a STK area target outline on the globe", new PolylineAreaTargetCodeSnippet());
            AddTreeNode(polylineRoot, "Draw the outline of a circle on the globe", new PolylineCircleCodeSnippet());
            AddTreeNode(polylineRoot, "Draw the outline of an ellipse on the globe", new PolylineEllipseCodeSnippet());

            TreeNode pathRoot = AddParentTreeNode(primitiveRoot.Nodes, "Path.png", "Path");
            AddTreeNode(pathRoot, "Draw a trail line behind a satellite", new PathTrailLineCodeSnippet());
            AddTreeNode(pathRoot, "Draw lines dropping from a trail line to the surface", m_pathDropLines);

            TreeNode triangleMeshRoot = AddParentTreeNode(primitiveRoot.Nodes, "TriangleMesh.png", "Triangle Mesh");
            AddTreeNode(triangleMeshRoot, "Draw a filled STK area target on the globe", new TriangleMeshAreaTargetCodeSnippet());
            AddTreeNode(triangleMeshRoot, "Draw a filled polygon with a hole on the globe", new TriangleMeshWithHoleCodeSnippet());
            AddTreeNode(triangleMeshRoot, "Draw an extrusion around a STK area target", new TriangleMeshExtrusionCodeSnippet());
            AddTreeNode(triangleMeshRoot, "Draw a filled rectangular extent on the globe", new TriangleMeshExtentCodeSnippet());
            AddTreeNode(triangleMeshRoot, "Draw a filled circle on the globe", new TriangleMeshCircleCodeSnippet());
            AddTreeNode(triangleMeshRoot, "Draw a filled ellipse on the globe", new TriangleMeshEllipseCodeSnippet());

            TreeNode surfaceMeshRoot = AddParentTreeNode(primitiveRoot.Nodes, "SurfaceMesh.png", "Surface Mesh");
            AddTreeNode(surfaceMeshRoot, "Draw a filled STK area target on terrain", new SurfaceMeshAreaTargetCodeSnippet());
            AddTreeNode(surfaceMeshRoot, "Draw a filled, textured extent on terrain", new SurfaceMeshTexturedExtentCodeSnippet());
            AddTreeNode(surfaceMeshRoot, "Draw a filled, dynamically textured extent on terrain", new SurfaceMeshDynamicImageCodeSnippet());
            AddTreeNode(surfaceMeshRoot, "Draw a moving water texture using affine transformations", m_surfaceMeshTransformations);
            AddTreeNode(surfaceMeshRoot, "Draw a texture mapped to a trapezoid", new SurfaceMeshTrapezoidalTextureCodeSnippet());

            TreeNode solidRoot = AddParentTreeNode(primitiveRoot.Nodes, "Solid.png", "Solid");
            AddTreeNode(solidRoot, "Draw an ellipsoid", new SolidEllipsoidCodeSnippet());
            AddTreeNode(solidRoot, "Draw a box", new SolidBoxCodeSnippet());
            AddTreeNode(solidRoot, "Draw a cylinder", new SolidCylinderCodeSnippet());

            TreeNode pointbatchRoot = AddParentTreeNode(primitiveRoot.Nodes, "PointBatch.png", "Point Batch");
            AddTreeNode(pointbatchRoot, "Draw a set of points", new PointBatchCodeSnippet());
            AddTreeNode(pointbatchRoot, "Draw a set of uniquely colored points", new PointBatchColorsCodeSnippet());

            TreeNode textbatchRoot = AddParentTreeNode(primitiveRoot.Nodes, "TextBatch.png", "Text Batch");
            AddTreeNode(textbatchRoot, "Draw a set of strings", new TextBatchCodeSnippet());
            AddTreeNode(textbatchRoot, "Draw a set of uniquely colored strings", new TextBatchColorsCodeSnippet());
            AddTreeNode(textbatchRoot, "Draw a set of strings in various languages", new TextBatchUnicodeCodeSnippet());

            TreeNode compositeRoot = AddParentTreeNode(primitiveRoot.Nodes, "Composite.png", "Composite");
            AddTreeNode(compositeRoot, "Create layers of primitives", new CompositeLayersCodeSnippet());

            TreeNode orderedCompositeRoot = AddParentTreeNode(primitiveRoot.Nodes, "OrderedComposite.png", "Ordered Composite");
            AddTreeNode(orderedCompositeRoot, "Z-order primitives on the surface", new OrderedCompositeZOrderCodeSnippet());

            TreeNode displayConditionRoot = AddParentTreeNode(tvCode.Nodes, "DisplayCondition.png", "Display Conditions");
            AddTreeNode(displayConditionRoot, "Draw a primitive based on viewer altitude", new AltitudeDisplayConditionCodeSnippet());
            AddTreeNode(displayConditionRoot, "Draw a primitive based on viewer distance", new DistanceDisplayConditionCodeSnippet());
            AddTreeNode(displayConditionRoot, "Draw a globe overlay based on the current time", m_timeDisplayCondition);
            AddTreeNode(displayConditionRoot, "Draw a primitive based on multiple conditions", m_compositeDisplayCondition);
            AddTreeNode(displayConditionRoot, "Draw a screen overlay based on viewer distance", new ScreenOverlayDisplayConditionCodeSnippet());

            TreeNode screenOverlayRoot = AddParentTreeNode(tvCode.Nodes, "ScreenOverlay.png", "Screen Overlays");
            screenOverlayRoot.ContextMenuStrip = cmParent;
            AddTreeNode(screenOverlayRoot, "Add a company logo with a texture overlay", new OverlaysTextureCodeSnippet());
            AddTreeNode(screenOverlayRoot, "Write text to a texture overlay", new OverlaysTextCodeSnippet());
            AddTreeNode(screenOverlayRoot, "Add overlays to a panel overlay", new OverlaysPanelCodeSnippet());
            AddTreeNode(screenOverlayRoot, "Stream a video to a texture overlay", new OverlaysVideoCodeSnippet());
            AddTreeNode(screenOverlayRoot, "Draw an overlay using an image in memory", new OverlaysMemoryCodeSnippet());

            TreeNode pickingRoot = AddParentTreeNode(tvCode.Nodes, "Picking.png", "Picking");
            AddTreeNode(pickingRoot, "Change a model's color on mouse over", m_pickChangeColor);
            AddTreeNode(pickingRoot, "Change model colors within a rectangular region", m_pickRectangular);
            AddTreeNode(pickingRoot, "Zoom to a model on double click", m_pickZoom);
            AddTreeNode(pickingRoot, "Zoom to a particular marker in a batch", m_pickPerItem);

            TreeNode cameraRoot = AddParentTreeNode(tvCode.Nodes, "Camera.png", "Camera");
            AddTreeNode(cameraRoot, "Follow an Earth orbiting satellite", m_cameraFollowingSatellite);
            AddTreeNode(cameraRoot, "Perform a smooth transition between two points", new CameraFollowingSplineCodeSnippet());
            AddTreeNode(cameraRoot, "Change view mode to use Earth's fixed frame", new CameraFixedFrameCodeSnippet());
            AddTreeNode(cameraRoot, "Take a snapshot of the camera's view", new CameraRecordingSnapshotCodeSnippet());

            TreeNode imagingRoot = AddParentTreeNode(tvCode.Nodes, "Imaging.png", "Imaging");
            AddTreeNode(imagingRoot, "Flip an image", new ImageFlipCodeSnippet());
            AddTreeNode(imagingRoot, "Swizzle an image's components", new ImageSwizzleCodeSnippet());
            AddTreeNode(imagingRoot, "Extract the alpha component from an image", new ImageChannelExtractCodeSnippet());
            AddTreeNode(imagingRoot, "Adjust brightness, contrast, and gamma", new ImageColorCorrectionCodeSnippet());
            AddTreeNode(imagingRoot, "Adjust the color levels of an image", new ImageLevelsCodeSnippet());
            AddTreeNode(imagingRoot, "Blur an image with a convolution matrix", new ImageConvolutionMatrixCodeSnippet());
            AddTreeNode(imagingRoot, "Load and display a raster stream", new ImageDynamicCodeSnippet());

            //
            // Expand Tree
            //
            globeOverlayRoot.Expand();
            primitiveRoot.Expand();
            displayConditionRoot.Expand();
            screenOverlayRoot.Expand();

            tvCode.SelectedNode = tvCode.Nodes[0];

            ResizeSplitContainer();

            //
            // Icons for context menus
            //
            expandAllToolStripMenuItem.Image = ilContextMenu.Images["ExpandAll"];
            collapseAllToolStripMenuItem.Image = ilContextMenu.Images["CollapseAll"];
            expandToolStripMenuItem.Image = ilContextMenu.Images["ExpandAll"];
            collapseToolStripMenuItem.Image = ilContextMenu.Images["CollapseAll"];
            parentExpandAllToolStripMenuItem.Image = ilContextMenu.Images["ExpandAll"];
            parentCollapseAllToolStripMenuItem.Image = ilContextMenu.Images["CollapseAll"];
            selectAllToolStripMenuItem.Image = ilContextMenu.Images["SelectAll"];
            copyToolStripMenuItem.Image = ilContextMenu.Images["Copy"];


            root.OnAnimUpdate += new IAgStkObjectRootEvents_OnAnimUpdateEventHandler(StkTimeChanged);
        }

        internal static HowToForm Instance
        {
            get
            {
                return instance;
            }
        }

        public AGI.STKX.Controls.AxAgUiAxVOCntrl Control3D
        {
            get { return m_Control3D; }
            set { m_Control3D = value; }
        }

        private void setupUnitPreferences()
        {
            root.UnitPreferences.SetCurrentUnit("DateFormat", "epSec");
            root.UnitPreferences.SetCurrentUnit("TimeUnit", "sec");
            root.UnitPreferences.SetCurrentUnit("DistanceUnit", "m");
            root.UnitPreferences.SetCurrentUnit("AngleUnit", "deg");
            root.UnitPreferences.SetCurrentUnit("LongitudeUnit", "deg");
            root.UnitPreferences.SetCurrentUnit("LatitudeUnit", "deg");
            root.UnitPreferences.SetCurrentUnit("Percent", "unitValue");
        }

        private TreeNode AddParentTreeNode(TreeNodeCollection parentCollection, string imageName, string text)
        {
            TreeNode parentNode = parentCollection.Add(text, text, imageName, imageName);
            parentNode.ContextMenuStrip = cmParent;
            return parentNode;
        }

        private void AddTreeNode(TreeNode parent, string text, CodeSnippet snippet)
        {
            TreeNode node = parent.Nodes.Add(text, text, parent.ImageKey, parent.SelectedImageKey);
            node.Tag = snippet;
            node.ContextMenuStrip = cmZoomTo;
        }

        private void cbShowUsing_CheckedChanged(object sender, EventArgs e)
        {
            DisplayHighlightedCode();
        }

        private void DisplayHighlightedCode()
        {
            const int WM_SETREDRAW = 0x000B;
            const int WM_USER = 0x400;
            const int EM_GETEVENTMASK = (WM_USER + 59);
            const int EM_SETEVENTMASK = (WM_USER + 69);

            //
            // Prevent redrawing when updating the rich text box
            //
            SendMessage(rtbCode.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
            IntPtr prevEventMask = SendMessage(rtbCode.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);

            rtbCode.Clear();
            linkSourceFile.Text = string.Empty;

            if (m_CodeSnippet != null)
            {
                //
                // Show the code snippet for this example
                //
                if (cbShowUsing.Checked)
                {
                    DisplayHighlightedCode(m_CodeSnippet.UsingDirectives);
                }
                DisplayHighlightedCode(m_CodeSnippet.Code);

                linkSourceFile.Text = m_CodeSnippet.FileName;
            }

            SendMessage(rtbCode.Handle, EM_SETEVENTMASK, 0, prevEventMask);
            SendMessage(rtbCode.Handle, WM_SETREDRAW, 1, IntPtr.Zero);

            rtbCode.Refresh();
        }

        private void DisplayHighlightedCode(string code)
        {
            if (code.Length == 0)
            {
                return;
            }

            // Remove code snippet variable comments for clarity
            int snippetBegin = 0;
            while (code.Contains("/*$") && code.Contains("$*/"))
            {
                snippetBegin = code.IndexOf("/*$", snippetBegin, StringComparison.Ordinal);
                int snippetLength = code.IndexOf("$*/", snippetBegin, StringComparison.Ordinal) - snippetBegin + 3;
                code = code.Remove(snippetBegin, snippetLength);
            }

            Regex r = new Regex("\\n");
            string[] lines = r.Split(code);

            int lastLine = lines.Length - 1;
            for (int i = 0; i < lines.Length; ++i)
            {
                DisplayHightlightedLine(lines[i], i != lastLine);
            }
        }

        private void DisplayHightlightedLine(string line, bool includeNewLine)
        {         
            Regex r = new Regex("([ \\t{}():;])|(\\[\\])");
            string[] tokens = r.Split(line);

            for (int i = 0; i < tokens.Length; i++)
            {
                rtbCode.SelectionColor = Color.Black;
                rtbCode.SelectionFont = new Font("Courier New", 10, FontStyle.Regular);

                if (tokens[i].StartsWith("//", StringComparison.Ordinal))
                {
                    //
                    // Comment
                    //
                    rtbCode.SelectionColor = Color.ForestGreen;
                    rtbCode.SelectedText = line.Substring(line.IndexOf("//", StringComparison.Ordinal));
                    break;
                }
                else if (Matches(tokens[i]))
                {
                    //
                    // Types
                    //
                    rtbCode.SelectionColor = Color.DarkCyan;
                    rtbCode.SelectedText = tokens[i];
                }
                else if (tokens[i].StartsWith("\"", StringComparison.Ordinal))
                {
                    //
                    // Strings
                    //
                    rtbCode.SelectionColor = Color.Brown;
                    rtbCode.SelectedText = tokens[i];
                    while (!(tokens[i].EndsWith("\"", StringComparison.Ordinal) ||
                             (tokens[i].EndsWith("\",", StringComparison.Ordinal)) && i < tokens.Length))
                    {
                        i++;
                        if (tokens[i] != "")
                        {
                            rtbCode.SelectionColor = Color.Brown;
                            rtbCode.SelectedText += tokens[i];
                        }
                    }
                }
                else
                {
                    string[] keywords =
                        {
                            "abstract", "event", "new", "struct",
                            "as", "explicit", "null", "switch",
                            "base", "extern", "object", "this",
                            "bool", "false", "operator", "throw",
                            "break", "finally", "out", "true",
                            "byte", "fixed", "override", "try",
                            "case", "float", "params", "typeof",
                            "catch", "for", "private", "uint",
                            "char", "foreach", "protected", "ulong",
                            "checked", "goto", "public", "unchecked",
                            "class", "if", "readonly", "unsafe",
                            "const", "implicit", "ref", "ushort",
                            "continue", "in", "return", "using",
                            "decimal", "int", "sbyte", "virtual",
                            "default", "interface", "sealed", "volatile",
                            "delegate", "internal", "short", "void",
                            "do", "is", "sizeof", "while",
                            "double", "lock", "stackalloc",
                            "else", "long", "static",
                            "enum", "namespace", "string", "string",
                            "Array"
                        };

                    for (int j = 0; j < keywords.Length; j++)
                    {
                        if (tokens[i] == keywords[j])
                        {
                            //
                            // Keywords
                            //
                            rtbCode.SelectionColor = Color.Blue;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(tokens[i]))
                    {
                        rtbCode.SelectedText = tokens[i];
                    }
                }
            }

            if (includeNewLine)
            {
                rtbCode.SelectedText = "\n";
            }
        }

        private static bool Matches(string s)
        {
            string[] prefixes = new string[]
                                    {
                                        "IAg"
                                    };

            bool match = false;
            foreach (string pre in prefixes)
            {
                match |= s.StartsWith(pre, StringComparison.Ordinal);
            }
            return match;
        }

        private void rtbCode_MouseUp(object sender, MouseEventArgs e)
        {
            //
            // Right clicking in the textbox allows the user to copy the example code.
            //
            if (e.Button == MouseButtons.Right)
            {
                cmEdit.Show(rtbCode, e.X, e.Y);
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbCode.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbCode.Copy();
        }

        private void HowToForm_Resize(object sender, EventArgs e)
        {
            ResizeSplitContainer();
        }

        private void ResizeSplitContainer()
        {
            splitContainer.Width = Width - splitContainer.Left - 10;
            splitContainer.Height = cbShowUsing.Top - splitContainer.Top - 6;
        }

        private void zoomToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = tvCode.GetNodeAt(m_TreeviewMouse);

            if (node != null)
            {
                tvCode.SelectedNode = node;
                CodeSnippet codeSnippet = node.Tag as CodeSnippet;

                if (!node.Checked)
                {
                    //
                    // This will fire events that execute ExecuteCodeSnippet()
                    //
                    node.Checked = true;
                }
                else
                {
                    //
                    // Already executed the code snippet, just zoom to it
                    // and show code
                    //
                    codeSnippet.View(Scene, root);

                    m_CodeSnippet = codeSnippet;
                    DisplayHighlightedCode();
                }
            }
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvCode.ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvCode.CollapseAll();
        }

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = tvCode.GetNodeAt(m_TreeviewMouse);

            if (node != null)
            {
                node.Expand();
            }
        }

        private void openSourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = tvCode.GetNodeAt(m_TreeviewMouse);

            if (node != null)
            {
                CodeSnippet codeSnippet = node.Tag as CodeSnippet;
                if (codeSnippet != null)
                {
                    Process.Start(codeSnippet.FilePath);
                }
            }
        }

        private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = tvCode.GetNodeAt(m_TreeviewMouse);

            if (node != null)
            {
                node.Collapse();
            }
        }

        private void parentExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvCode.ExpandAll();
        }

        private void parentCollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvCode.CollapseAll();
        }

        private void tvCode_MouseMove(object sender, MouseEventArgs e)
        {
            m_TreeviewMouse = new Point(e.X, e.Y);
        }

        private void tvCode_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (tvCode.SelectedNode != null)
            {
                CodeSnippet codeSnippet = tvCode.SelectedNode.Tag as CodeSnippet;
                if (codeSnippet != null)
                {
                    ExecuteCodeSnippet(codeSnippet, false);
                    tvCode.SelectedNode.Checked = false;
                    rtbCode.Clear();
                    linkSourceFile.Text = string.Empty;
                }
            }
        }

        private void tvCode_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CodeSnippet codeSnippet = e.Node.Tag as CodeSnippet;
            if (codeSnippet != null)
            {
                m_CodeSnippet = codeSnippet;
                DisplayHighlightedCode();
                ExecuteCodeSnippet(codeSnippet, true);
                e.Node.Checked = true;
            }
            else
            {
                rtbCode.Clear();
                linkSourceFile.Text = string.Empty;
            }
        }

        private void ExecuteCodeSnippet(CodeSnippet codeSnippet, bool show)
        {
            if (codeSnippet != null)
            {
                if (show)
                {
                    //
                    // Execute the example code and zoom to that part of the globe
                    //
                    codeSnippet.Execute(Scene, root);
                    codeSnippet.View(Scene, root);
                }
                else
                {
                    //
                    // Remove this example from the 3D window
                    //
                    codeSnippet.Remove(Scene, root);
                }
            }
        }

        private void linkSourceFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(m_CodeSnippet.FilePath);
        }

        private void HowToForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Scene = null;
            manager = null;
            if (root.CurrentScenario != null)
                root.CloseScenario();
            stkxApp = null;
            m_Control3D = null;
        }

        private void StkMouseUp(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent e)
        {
            // Store the mouse click position
            LastMouseClickX = e.x;
            LastMouseClickY = e.y;

            if (m_Overlay != null)
                m_Overlay.Control3D_MouseUp(sender, e);
        }

        private void StkMouseMove(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
        {
            m_pickChangeColor.MouseMove(Scene, root, e.x, e.y);
            m_pickRectangular.MouseMove(Scene, root, e.x, e.y);

            if (m_Overlay != null)
                m_Overlay.Control3D_MouseMove(sender, e);
        }

        private void StkMouseDoubleClick(object sender, EventArgs e)
        {
            m_pickZoom.DoubleClick(Scene, root, LastMouseClickX, LastMouseClickY);
            m_pickPerItem.DoubleClick(Scene, root, LastMouseClickX, LastMouseClickY);

            if (m_Overlay != null)
                m_Overlay.Control3D_MouseDoubleClick(sender, e);
        }

        private void StkMouseDown(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent e)
        {
            if (m_Overlay != null)
                m_Overlay.Control3D_MouseDown(sender, e);
        }

        private void StkTimeChanged(double TimeEpSec)
        {
            // For code snippets that use OnAnimationUpdate, update each code snippet that is not null

            if (m_modelVectorOrientation != null)
            {
                m_modelVectorOrientation.TimeChanged(root, TimeEpSec);
            }
            if (m_surfaceMeshTransformations != null)
            {
                m_surfaceMeshTransformations.TimeChanged(Scene, root, TimeEpSec);
            }
            if (m_cameraFollowingSatellite != null)
            {
                m_cameraFollowingSatellite.TimeChanged(root, TimeEpSec);
            }
            if (m_modelArticulation != null)
            {
                m_modelArticulation.TimeChanged(TimeEpSec);
            }
            if (m_pathDropLines != null)
            {
                m_pathDropLines.TimeChanged(root, TimeEpSec);
            }
        }

        private static HowToForm instance;

        private AgSTKXApplication stkxApp;
        private AgStkObjectRoot root;
        private IAgStkGraphicsSceneManager manager;
        private IAgStkGraphicsScene Scene;
        private CodeSnippet m_CodeSnippet;
        private Point m_TreeviewMouse;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl m_Control3D;
        private int LastMouseClickX, LastMouseClickY;

        private PickChangeColorCodeSnippet m_pickChangeColor = new PickChangeColorCodeSnippet();
        private PickRectangularCodeSnippet m_pickRectangular = new PickRectangularCodeSnippet();
        private PickZoomCodeSnippet m_pickZoom = new PickZoomCodeSnippet();
        private PickPerItemCodeSnippet m_pickPerItem = new PickPerItemCodeSnippet();
        private ModelVectorOrientationCodeSnippet m_modelVectorOrientation;
        private ModelArticulationCodeSnippet m_modelArticulation;
        private CameraFollowingSatelliteCodeSnippet m_cameraFollowingSatellite;
        private SurfaceMeshTransformationsCodeSnippet m_surfaceMeshTransformations;
        private TimeDisplayConditionCodeSnippet m_timeDisplayCondition;
        private CompositeDisplayConditionCodeSnippet m_compositeDisplayCondition;
        private PathDropLineCodeSnippet m_pathDropLines;
        private OverlayToolbar m_Overlay;
    }
}
