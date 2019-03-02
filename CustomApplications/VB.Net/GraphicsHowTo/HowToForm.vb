Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Diagnostics
Imports AGI
Imports AGI.STKX
Imports AGI.STKX.Controls
Imports AGI.STKObjects
Imports AGI.STKGraphics
Imports GraphicsHowTo.Primitives
Imports GraphicsHowTo.Primitives.Solid
Imports GraphicsHowTo.Primitives.MarkerBatch
Imports GraphicsHowTo.Primitives.PointBatch
Imports GraphicsHowTo.Primitives.Polyline
Imports GraphicsHowTo.Primitives.TextBatch
Imports GraphicsHowTo.Primitives.TriangleMesh
Imports GraphicsHowTo.Primitives.SurfaceMesh
Imports GraphicsHowTo.Primitives.Model
Imports GraphicsHowTo.Camera
Imports GraphicsHowTo.GlobeOverlays
Imports GraphicsHowTo.Primitives.Composite
Imports GraphicsHowTo.Primitives.OrderedComposite
Imports GraphicsHowTo.DisplayConditions
Imports GraphicsHowTo.Imaging
Imports GraphicsHowTo.Picking
Imports GraphicsHowTo.ScreenOverlays

Partial Public Class HowToForm
    Inherits Form
    <DllImport("user32", CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr
    End Function

    Public Sub New()
        InitializeComponent()

        s_instance = Me

        Dim icons As String() = New String() {"Balloon.png", "Camera.png", "Composite.png", "OrderedComposite.png", "DisplayCondition.png", "Filtering.png", _
         "GlobeOverlay.png", "Imaging.png", "KML.png", "MarkerBatch.png", "Models.png", "Path.png", _
         "Picking.png", "PointBatch.png", "Polyline.png", "Primitives.png", "ScreenOverlay.png", "Solid.png", _
         "SurfaceMesh.png", "TextBatch.png", "Tracking.png", "TriangleMesh.png", "Visualizer.png"}

        ' load tree icons from embedded resource files, storing them under their base file name.
        For Each icon As String In icons
            ilTree.Images.Add(icon, New Bitmap([GetType](), icon))
        Next
    End Sub

    Private Sub HowToForm_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        '
        ' Set up global variables for STK Engine
        '
        Me.Refresh()
        stkxApp = New AgSTKXApplication()
        root = New AgStkObjectRoot()
        root.NewScenario("HowTo")
        manager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
        Scene = manager.Scenes(0)
        m_Control3D = Me.AxAgUiAxVOCntrl1

        '
        ' Set unit and animation preferences to be consistent throughout the application.
        '
        setupUnitPreferences()
        CodeSnippet.SetAnimationDefaults(root)

        '
        ' Try to remove default STK annoatations and overlays that obscure the toolbar
        '
        root.BeginUpdate()
        Try
            root.ExecuteCommand("VO * Annotation Time Show Off ShowTimeStep Off")
            root.ExecuteCommand("VO * Annotation Frame Show Off")
            root.ExecuteCommand("VO * Overlay Modify ""AGI_logo_small.ppm"" Show Off")
        Catch generatedExceptionName As Exception
        End Try
        root.EndUpdate()

        m_Overlay = New OverlayToolbar(root, Me.AxAgUiAxVOCntrl1)
        m_modelVectorOrientation = New ModelVectorOrientationCodeSnippet(DirectCast(root.CurrentScenario, IAgScenario).StartTime)
        m_modelArticulation = New ModelArticulationCodeSnippet(DirectCast(root.CurrentScenario, IAgScenario).StartTime)
        m_pathDropLines = New PathDropLineCodeSnippet(DirectCast(root.CurrentScenario, IAgScenario).StartTime)
        m_surfaceMeshTransformations = New SurfaceMeshTransformationsCodeSnippet()
        m_cameraFollowingSatellite = New CameraFollowingSatelliteCodeSnippet()
        m_timeDisplayCondition = New TimeDisplayConditionCodeSnippet()
        m_compositeDisplayCondition = New CompositeDisplayConditionCodeSnippet()

        rtbCode.BackColor = Color.White

        '
        ' Add code snippets to treeview
        '
        Dim globeOverlayRoot As TreeNode = AddParentTreeNode(tvCode.Nodes, "GlobeOverlay.png", "Globe Overlays")
        AddTreeNode(globeOverlayRoot, "Add jp2 imagery to the globe", New GlobeImageOverlayCodeSnippet())
        AddTreeNode(globeOverlayRoot, "Add terrain to the globe", New TerrainOverlayCodeSnippet())
        AddTreeNode(globeOverlayRoot, "Add projected imagery to the globe", New ProjectedImageCodeSnippet())
        AddTreeNode(globeOverlayRoot, "Project imagery on models", New ProjectedImageModelsCodeSnippet())
        AddTreeNode(globeOverlayRoot, "Draw an image on top of another", New GlobeOverlayRenderOrderCodeSnippet())
        AddTreeNode(globeOverlayRoot, "Add custom imagery to the globe", New OpenStreetMapCodeSnippet())

        Dim primitiveRoot As TreeNode = AddParentTreeNode(tvCode.Nodes, "Primitives.png", "Primitives")
        primitiveRoot.ContextMenuStrip = cmParent

        Dim modelRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "Models.png", "Model")
        AddTreeNode(modelRoot, "Draw a Collada or MDL model", New ModelCodeSnippet())
        AddTreeNode(modelRoot, "Draw a Collada model with user defined lighting", New ModelColladaFXCodeSnippet())
        AddTreeNode(modelRoot, "Orient a model", New ModelOrientationCodeSnippet())
        AddTreeNode(modelRoot, "Orient a model along its velocity vector", m_modelVectorOrientation)
        AddTreeNode(modelRoot, "Draw a model with moving articulations", m_modelArticulation)
        AddTreeNode(modelRoot, "Draw a dynamically textured Collada model", New ModelDynamicCodeSnippet())

        Dim markerRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "MarkerBatch.png", "Marker Batch")
        AddTreeNode(markerRoot, "Draw a set of markers", New MarkerBatchCodeSnippet())
        AddTreeNode(markerRoot, "Draw and combine sets of marker batches at various distances", New MarkerBatchAggregationDeaggregationCodeSnippet())

        Dim polylineRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "Polyline.png", "Polyline")
        AddTreeNode(polylineRoot, "Draw a line between two points", New PolylineCodeSnippet())
        AddTreeNode(polylineRoot, "Draw a great arc on the globe", New PolylineGreatArcCodeSnippet())
        AddTreeNode(polylineRoot, "Draw a rhumb line on the globe", New PolylineRhumbLineCodeSnippet())
        AddTreeNode(polylineRoot, "Draw a STK area target outline on the globe", New PolylineAreaTargetCodeSnippet())
        AddTreeNode(polylineRoot, "Draw the outline of a circle on the globe", New PolylineCircleCodeSnippet())
        AddTreeNode(polylineRoot, "Draw the outline of an ellipse on the globe", New PolylineEllipseCodeSnippet())

        Dim pathRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "Path.png", "Path")
        AddTreeNode(pathRoot, "Draw a trail line behind a satellite", New PathTrailLineCodeSnippet())
        AddTreeNode(pathRoot, "Draw lines dropping from a trail line to the surface", m_pathDropLines)

        Dim triangleMeshRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "TriangleMesh.png", "Triangle Mesh")
        AddTreeNode(triangleMeshRoot, "Draw a filled STK area target on the globe", New TriangleMeshAreaTargetCodeSnippet())
        AddTreeNode(triangleMeshRoot, "Draw a filled polygon with a hole on the globe", New TriangleMeshWithHoleCodeSnippet())
        AddTreeNode(triangleMeshRoot, "Draw an extrusion around a STK area target", New TriangleMeshExtrusionCodeSnippet())
        AddTreeNode(triangleMeshRoot, "Draw a filled rectangular extent on the globe", New TriangleMeshExtentCodeSnippet())
        AddTreeNode(triangleMeshRoot, "Draw a filled circle on the globe", New TriangleMeshCircleCodeSnippet())
        AddTreeNode(triangleMeshRoot, "Draw a filled ellipse on the globe", New TriangleMeshEllipseCodeSnippet())

        Dim surfaceMeshRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "SurfaceMesh.png", "Surface Mesh")
        AddTreeNode(surfaceMeshRoot, "Draw a filled STK area target on terrain", New SurfaceMeshAreaTargetCodeSnippet())
        AddTreeNode(surfaceMeshRoot, "Draw a filled, textured extent on terrain", New SurfaceMeshTexturedExtentCodeSnippet())
        AddTreeNode(surfaceMeshRoot, "Draw a filled, dynamically textured extent on terrain", New SurfaceMeshDynamicImageCodeSnippet())
        AddTreeNode(surfaceMeshRoot, "Draw a moving water texture using affine transformations", m_surfaceMeshTransformations)
        AddTreeNode(surfaceMeshRoot, "Draw a texture mapped to a trapezoid", New SurfaceMeshTrapezoidalTextureCodeSnippet())

        Dim solidRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "Solid.png", "Solid")
        AddTreeNode(solidRoot, "Draw an ellipsoid", New SolidEllipsoidCodeSnippet())
        AddTreeNode(solidRoot, "Draw a box", New SolidBoxCodeSnippet())
        AddTreeNode(solidRoot, "Draw a cylinder", New SolidCylinderCodeSnippet())

        Dim pointbatchRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "PointBatch.png", "Point Batch")
        AddTreeNode(pointbatchRoot, "Draw a set of points", New PointBatchCodeSnippet())
        AddTreeNode(pointbatchRoot, "Draw a set of uniquely colored points", New PointBatchColorsCodeSnippet())

        Dim textbatchRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "TextBatch.png", "Text Batch")
        AddTreeNode(textbatchRoot, "Draw a set of strings", New TextBatchCodeSnippet())
        AddTreeNode(textbatchRoot, "Draw a set of uniquely colored strings", New TextBatchColorsCodeSnippet())
        AddTreeNode(textbatchRoot, "Draw a set of strings in various languages", New TextBatchUnicodeCodeSnippet())

        Dim compositeRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "Composite.png", "Composite")
        AddTreeNode(compositeRoot, "Create layers of primitives", New CompositeLayersCodeSnippet())

        Dim orderedCompositeRoot As TreeNode = AddParentTreeNode(primitiveRoot.Nodes, "OrderedComposite.png", "Ordered Composite")
        AddTreeNode(orderedCompositeRoot, "Z-order primitives on the surface", New OrderedCompositeZOrderCodeSnippet())

        Dim displayConditionRoot As TreeNode = AddParentTreeNode(tvCode.Nodes, "DisplayCondition.png", "Display Conditions")
        AddTreeNode(displayConditionRoot, "Draw a primitive based on viewer altitude", New AltitudeDisplayConditionCodeSnippet())
        AddTreeNode(displayConditionRoot, "Draw a primitive based on viewer distance", New DistanceDisplayConditionCodeSnippet())
        AddTreeNode(displayConditionRoot, "Draw a globe overlay based on the current time", m_timeDisplayCondition)
        AddTreeNode(displayConditionRoot, "Draw a primitive based on multiple conditions", m_compositeDisplayCondition)
        AddTreeNode(displayConditionRoot, "Draw a screen overlay based on viewer distance", New ScreenOverlayDisplayConditionCodeSnippet())

        Dim screenOverlayRoot As TreeNode = AddParentTreeNode(tvCode.Nodes, "ScreenOverlay.png", "Screen Overlays")
        screenOverlayRoot.ContextMenuStrip = cmParent
        AddTreeNode(screenOverlayRoot, "Add a company logo with a texture overlay", New OverlaysTextureCodeSnippet())
        AddTreeNode(screenOverlayRoot, "Write text to a texture overlay", New OverlaysTextCodeSnippet())
        AddTreeNode(screenOverlayRoot, "Add overlays to a panel overlay", New OverlaysPanelCodeSnippet())
        AddTreeNode(screenOverlayRoot, "Stream a video to a texture overlay", New OverlaysVideoCodeSnippet())
        AddTreeNode(screenOverlayRoot, "Draw an overlay using an image in memory", New OverlaysMemoryCodeSnippet())

        Dim pickingRoot As TreeNode = AddParentTreeNode(tvCode.Nodes, "Picking.png", "Picking")
        AddTreeNode(pickingRoot, "Change a model's color on mouse over", m_pickChangeColor)
        AddTreeNode(pickingRoot, "Change model colors within a rectangular region", m_pickRectangular)
        AddTreeNode(pickingRoot, "Zoom to a model on double click", m_pickZoom)
        AddTreeNode(pickingRoot, "Zoom to a particular marker in a batch", m_pickPerItem)

        Dim cameraRoot As TreeNode = AddParentTreeNode(tvCode.Nodes, "Camera.png", "Camera")
        AddTreeNode(cameraRoot, "Follow an Earth orbiting satellite", m_cameraFollowingSatellite)
        AddTreeNode(cameraRoot, "Perform a smooth transition between two points", New CameraFollowingSplineCodeSnippet())
        AddTreeNode(cameraRoot, "Change view mode to use Earth's fixed frame", New CameraFixedFrameCodeSnippet())
        AddTreeNode(cameraRoot, "Take a snapshot of the camera's view", New CameraRecordingSnapshotCodeSnippet())

        Dim imagingRoot As TreeNode = AddParentTreeNode(tvCode.Nodes, "Imaging.png", "Imaging")
        AddTreeNode(imagingRoot, "Flip an image", New ImageFlipCodeSnippet())
        AddTreeNode(imagingRoot, "Swizzle an image's components", New ImageSwizzleCodeSnippet())
        AddTreeNode(imagingRoot, "Extract the alpha component from an image", New ImageChannelExtractCodeSnippet())
        AddTreeNode(imagingRoot, "Adjust brightness, contrast, and gamma", New ImageColorCorrectionCodeSnippet())
        AddTreeNode(imagingRoot, "Adjust the color levels of an image", New ImageLevelsCodeSnippet())
        AddTreeNode(imagingRoot, "Blur an image with a convolution matrix", New ImageConvolutionMatrixCodeSnippet())
        AddTreeNode(imagingRoot, "Load and display a raster stream", New ImageDynamicCodeSnippet())

        '
        ' Expand Tree
        '
        globeOverlayRoot.Expand()
        primitiveRoot.Expand()
        displayConditionRoot.Expand()
        screenOverlayRoot.Expand()

        tvCode.SelectedNode = tvCode.Nodes(0)

        ResizeSplitContainer()

        '
        ' Icons for context menus
        '
        expandAllToolStripMenuItem.Image = ilContextMenu.Images("ExpandAll")
        collapseAllToolStripMenuItem.Image = ilContextMenu.Images("CollapseAll")
        expandToolStripMenuItem.Image = ilContextMenu.Images("ExpandAll")
        collapseToolStripMenuItem.Image = ilContextMenu.Images("CollapseAll")
        parentExpandAllToolStripMenuItem.Image = ilContextMenu.Images("ExpandAll")
        parentCollapseAllToolStripMenuItem.Image = ilContextMenu.Images("CollapseAll")
        selectAllToolStripMenuItem.Image = ilContextMenu.Images("SelectAll")
        copyToolStripMenuItem.Image = ilContextMenu.Images("Copy")

        AddHandler root.OnAnimUpdate, AddressOf StkTimeChanged
    End Sub

    Friend Shared ReadOnly Property Instance() As HowToForm
        Get
            Return s_instance
        End Get
    End Property

    Public Property Control3D() As AxAgUiAxVOCntrl
        Get
            Return m_Control3D
        End Get
        Set(ByVal value As AxAgUiAxVOCntrl)
            m_Control3D = value
        End Set
    End Property

    Private Sub setupUnitPreferences()
        root.UnitPreferences.SetCurrentUnit("DateFormat", "epSec")
        root.UnitPreferences.SetCurrentUnit("TimeUnit", "sec")
        root.UnitPreferences.SetCurrentUnit("DistanceUnit", "m")
        root.UnitPreferences.SetCurrentUnit("AngleUnit", "deg")
        root.UnitPreferences.SetCurrentUnit("LongitudeUnit", "deg")
        root.UnitPreferences.SetCurrentUnit("LatitudeUnit", "deg")
        root.UnitPreferences.SetCurrentUnit("Percent", "unitValue")
    End Sub

    Private Function AddParentTreeNode(ByVal parentCollection As TreeNodeCollection, ByVal imageName As String, ByVal text As String) As TreeNode
        Dim parentNode As TreeNode = parentCollection.Add(text, text, imageName, imageName)
        parentNode.ContextMenuStrip = cmParent
        Return parentNode
    End Function

    Private Sub AddTreeNode(ByVal parent As TreeNode, ByVal text As String, ByVal snippet As CodeSnippet)
        Dim node As TreeNode = parent.Nodes.Add(text, text, parent.ImageKey, parent.SelectedImageKey)
        node.Tag = snippet
        node.ContextMenuStrip = cmZoomTo
    End Sub

    Private Sub cbShowUsing_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbShowUsing.CheckedChanged
        DisplayHighlightedCode()
    End Sub

    Private Sub DisplayHighlightedCode()
        Const WM_SETREDRAW As Integer = &HB
        Const WM_USER As Integer = &H400
        Const EM_GETEVENTMASK As Integer = (WM_USER + 59)
        Const EM_SETEVENTMASK As Integer = (WM_USER + 69)

        '
        ' Prevent redrawing when updating the rich text box
        '
        SendMessage(rtbCode.Handle, WM_SETREDRAW, 0, IntPtr.Zero)
        Dim prevEventMask As IntPtr = SendMessage(rtbCode.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero)

        rtbCode.Clear()
        linkSourceFile.Text = String.Empty

        If m_CodeSnippet IsNot Nothing Then
            '
            ' Show the code snippet for this example
            '
            If cbShowUsing.Checked Then
                DisplayHighlightedCode(m_CodeSnippet.UsingDirectives)
            End If
            DisplayHighlightedCode(m_CodeSnippet.Code)

            linkSourceFile.Text = m_CodeSnippet.FileName
        End If

        SendMessage(rtbCode.Handle, EM_SETEVENTMASK, 0, prevEventMask)
        SendMessage(rtbCode.Handle, WM_SETREDRAW, 1, IntPtr.Zero)

        rtbCode.Refresh()
    End Sub

    Private Sub DisplayHighlightedCode(ByVal code As String)
        If code.Length = 0 Then
            Return
        End If

        Dim r As New Regex("\n")
        Dim lines As String() = r.Split(code)

        Dim lastLine As Integer = lines.Length - 1
        For i As Integer = 0 To lines.Length - 1
            lines(i) = removeAttachID(lines(i))
            DisplayHightlightedLine(lines(i), i <> lastLine)
        Next
    End Sub

    Private Sub DisplayHightlightedLine(ByVal line As String, ByVal includeNewLine As Boolean)
        Dim r As New Regex("([ \t{}():;])|(\[\])")
        Dim tokens As String() = r.Split(line)

        For i As Integer = 0 To tokens.Length - 1
            rtbCode.SelectionColor = Color.Black
            rtbCode.SelectionFont = New Font("Courier New", 10, FontStyle.Regular)

            If tokens(i).StartsWith("'", StringComparison.Ordinal) Then
                '
                ' Comment
                '
                rtbCode.SelectionColor = Color.ForestGreen
                rtbCode.SelectedText = line.Substring(line.IndexOf("'", StringComparison.Ordinal))
                Exit For
            ElseIf Matches(tokens(i)) Then
                '
                ' Types
                '
                rtbCode.SelectionColor = Color.DarkCyan
                rtbCode.SelectedText = tokens(i)
            ElseIf tokens(i).StartsWith("""", StringComparison.Ordinal) Then
                '
                ' Strings
                '
                rtbCode.SelectionColor = Color.Brown
                rtbCode.SelectedText = tokens(i)
                While Not (tokens(i).EndsWith("""", StringComparison.Ordinal) OrElse (tokens(i).EndsWith(""",", StringComparison.Ordinal)) AndAlso i < tokens.Length)
                    i += 1
                    If tokens(i) <> "" Then
                        rtbCode.SelectionColor = Color.Brown
                        rtbCode.SelectedText += tokens(i)
                    End If
                End While
            Else
                Dim keywords As String() = _
                { _
                    "AddHandler", "AddressOf", "Alias", "And", _
                    "AndAlso", "Ansi", "As", "Assembly", _
                    "Auto", "Boolean", "ByRef", "Byte", _
                    "ByVal", "Call", "Case", "Catch", _
                    "CBool", "CByte", "CChar", "CDate", _
                    "CDec", "CDbl", "Char", "CInt", _
                    "Class", "CLng", "CObj", "Const", _
                    "CShort", "CSng", "CStr", "CType", _
                    "Date", "Decimal", "Declare", "Default", _
                    "Delegate", "Dim", "DirectCast", "Do", _
                    "Double", "Each", "Else", "ElseIf", _
                    "End", "Enum", "Erase", "Error", _
                    "Event", "Exit", "False", "Finally", _
                    "For", "Friend", "Function", "Get", _
                    "GetType", "GoSub", "GoTo", "Handles", _
                    "If", "Implements", "Imports", "In", _
                    "Inherits", "Integer", "Interface", "Is", _
                    "Let", "Lib", "Like", "Long", _
                    "Loop", "Me", "Mod", "Module", _
                    "MustInherit", "MustOverride", "MyBase", "MyClass", _
                    "Namespace", "New", "Next", "Not", _
                    "Nothing", "NotInheritable", "NotOverridable", "Object", _
                    "On", "Option", "Optional", "Or", _
                    "OrElse", "Overloads", "Overridable", "Overrides", _
                    "ParamArray", "Preserve", "Private", "Property", _
                    "Protected", "Public", "RaiseEvent", "ReadOnly", _
                    "ReDim", "REM", "RemoveHandler", "Resume", _
                    "Return", "Select", "Set", "Shadows", _
                    "Shared", "Short", "Single", "Static", _
                    "Step", "Stop", "String", "Structure", _
                    "Sub", "SyncLock", "Then", "Throw", _
                    "To", "True", "Try", "TypeOf", _
                    "Unicode", "Until", "Variant", "When", _
                    "While", "With", "WithEvents", "WriteOnly", _
                    "Xor" _
                }

                For j As Integer = 0 To keywords.Length - 1
                    If tokens(i) = keywords(j) Then
                        '
                        ' Keywords
                        '
                        rtbCode.SelectionColor = Color.Blue
                        Exit For
                    End If
                Next

                If Not String.IsNullOrEmpty(tokens(i)) Then
                    rtbCode.SelectedText = tokens(i)
                End If
            End If
        Next

        If includeNewLine Then
            rtbCode.SelectedText = vbLf
        End If
    End Sub

    Private Shared Function Matches(ByVal s As String) As Boolean
        Dim prefixes As String() = New String() {"IAg"}

        Dim match As Boolean = False
        For Each pre As String In prefixes
            match = match Or s.StartsWith(pre, StringComparison.Ordinal)
        Next
        Return match
    End Function

    Private Sub rtbCode_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles rtbCode.MouseUp
        '
        ' Right clicking in the textbox allows the user to copy the example code.
        '
        If e.Button = MouseButtons.Right Then
            cmEdit.Show(rtbCode, e.X, e.Y)
        End If
    End Sub

    Private Sub selectAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles selectAllToolStripMenuItem.Click
        rtbCode.SelectAll()
    End Sub

    Private Sub copyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles copyToolStripMenuItem.Click
        rtbCode.Copy()
    End Sub

    Private Sub HowToForm_Resize(ByVal sender As Object, ByVal e As EventArgs)
        ResizeSplitContainer()
    End Sub

    Private Sub ResizeSplitContainer()
        SplitContainer.Width = Width - SplitContainer.Left - 10
        SplitContainer.Height = cbShowUsing.Top - SplitContainer.Top - 6
    End Sub

    Private Sub zoomToToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles zoomToToolStripMenuItem.Click
        Dim node As TreeNode = tvCode.GetNodeAt(m_TreeviewMouse)

        If node IsNot Nothing Then
            tvCode.SelectedNode = node
            Dim codeSnippet As CodeSnippet = TryCast(node.Tag, CodeSnippet)

            If Not node.Checked Then
                '
                ' This will fire events that execute ExecuteCodeSnippet()
                '
                node.Checked = True
            Else
                '
                ' Already executed the code snippet, just zoom to it
                ' and show code
                '
                codeSnippet.View(Scene, root)

                m_CodeSnippet = codeSnippet
                DisplayHighlightedCode()
            End If
        End If
    End Sub

    Private Sub expandAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles expandAllToolStripMenuItem.Click
        tvCode.ExpandAll()
    End Sub

    Private Sub collapseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles collapseAllToolStripMenuItem.Click
        tvCode.CollapseAll()
    End Sub

    Private Sub expandToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles expandToolStripMenuItem.Click
        Dim node As TreeNode = tvCode.GetNodeAt(m_TreeviewMouse)

        If node IsNot Nothing Then
            node.Expand()
        End If
    End Sub

    Private Sub openSourceFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openSourceFileToolStripMenuItem.Click
        Dim node As TreeNode = tvCode.GetNodeAt(m_TreeviewMouse)

        If node IsNot Nothing Then
            Dim codeSnippet As CodeSnippet = TryCast(node.Tag, CodeSnippet)
            If codeSnippet IsNot Nothing Then
                Process.Start(codeSnippet.FilePath)
            End If
        End If
    End Sub

    Private Sub collapseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles collapseToolStripMenuItem.Click
        Dim node As TreeNode = tvCode.GetNodeAt(m_TreeviewMouse)

        If node IsNot Nothing Then
            node.Collapse()
        End If
    End Sub

    Private Sub parentExpandAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles parentExpandAllToolStripMenuItem.Click
        tvCode.ExpandAll()
    End Sub

    Private Sub parentCollapseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles parentCollapseAllToolStripMenuItem.Click
        tvCode.CollapseAll()
    End Sub

    Private Sub tvCode_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles tvCode.MouseMove
        m_TreeviewMouse = New Point(e.X, e.Y)
    End Sub

    Private Sub tvCode_BeforeSelect(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles tvCode.BeforeSelect
        If tvCode.SelectedNode IsNot Nothing Then
            Dim codeSnippet As CodeSnippet = TryCast(tvCode.SelectedNode.Tag, CodeSnippet)
            If codeSnippet IsNot Nothing Then
                ExecuteCodeSnippet(codeSnippet, False)
                tvCode.SelectedNode.Checked = False
                rtbCode.Clear()
                linkSourceFile.Text = String.Empty
            End If
        End If
    End Sub

    Private Sub tvCode_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvCode.AfterSelect
        Dim codeSnippet As CodeSnippet = TryCast(e.Node.Tag, CodeSnippet)
        If codeSnippet IsNot Nothing Then
            m_CodeSnippet = codeSnippet
            DisplayHighlightedCode()
            ExecuteCodeSnippet(codeSnippet, True)
            e.Node.Checked = True
        Else
            rtbCode.Clear()
            linkSourceFile.Text = String.Empty
        End If
    End Sub

    Private Sub ExecuteCodeSnippet(ByVal codeSnippet As CodeSnippet, ByVal show As Boolean)
        If codeSnippet IsNot Nothing Then
            If show Then
                '
                ' Execute the example code and zoom to that part of the globe
                '
                codeSnippet.Execute(Scene, root)
                codeSnippet.View(Scene, root)
            Else
                '
                ' Remove this example from the 3D window
                '
                codeSnippet.Remove(Scene, root)
            End If
        End If
    End Sub

    Private Sub linkSourceFile_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles linkSourceFile.LinkClicked
        Process.Start(m_CodeSnippet.FilePath)
    End Sub

    Private Sub HowToForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        Scene = Nothing
        manager = Nothing
        If root.CurrentScenario IsNot Nothing Then
            root.CloseScenario()
        End If
        stkxApp = Nothing
        m_Control3D = Nothing
    End Sub

    Private Sub StkMouseUp(ByVal sender As Object, ByVal e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent) Handles AxAgUiAxVOCntrl1.MouseUpEvent
        ' Store the mouse click position
        LastMouseClickX = e.x
        LastMouseClickY = e.y

        If m_Overlay IsNot Nothing Then
            m_Overlay.Control3D_MouseUp(sender, e)
        End If
    End Sub

    Private Sub StkMouseMove(ByVal sender As Object, ByVal e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent) Handles AxAgUiAxVOCntrl1.MouseMoveEvent
        m_pickChangeColor.MouseMove(Scene, root, e.x, e.y)
        m_pickRectangular.MouseMove(Scene, root, e.x, e.y)

        If m_Overlay IsNot Nothing Then
            m_Overlay.Control3D_MouseMove(sender, e)
        End If
    End Sub

    Private Sub StkMouseDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles AxAgUiAxVOCntrl1.DblClick
        m_pickZoom.DoubleClick(Scene, root, LastMouseClickX, LastMouseClickY)
        m_pickPerItem.DoubleClick(Scene, root, LastMouseClickX, LastMouseClickY)

        If m_Overlay IsNot Nothing Then
            m_Overlay.Control3D_MouseDoubleClick(sender, e)
        End If
    End Sub

    Private Sub StkMouseDown(ByVal sender As Object, ByVal e As AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent) Handles AxAgUiAxVOCntrl1.MouseDownEvent
        If m_Overlay IsNot Nothing Then
            m_Overlay.Control3D_MouseDown(sender, e)
        End If
    End Sub

    Private Sub StkTimeChanged(ByVal TimeEpSec As Double)
        ' For code snippets that use OnAnimationUpdate, update each code snippet that is not null

        If m_modelVectorOrientation IsNot Nothing Then
            m_modelVectorOrientation.TimeChanged(root, TimeEpSec)
        End If
        If m_surfaceMeshTransformations IsNot Nothing Then
            m_surfaceMeshTransformations.TimeChanged(Scene, root, TimeEpSec)
        End If
        If m_cameraFollowingSatellite IsNot Nothing Then
            m_cameraFollowingSatellite.TimeChanged(root, TimeEpSec)
        End If
        If m_modelArticulation IsNot Nothing Then
            m_modelArticulation.TimeChanged(TimeEpSec)
        End If
        If m_pathDropLines IsNot Nothing Then
            m_pathDropLines.TimeChanged(root, TimeEpSec)
        End If
    End Sub

    Private Function removeAttachID(ByVal line As String) As String
        Dim foundAttachID As Boolean = True
        Dim startOfAttachID As Integer
        Dim endOfAttachID As Integer
        Dim endParanthesis As Integer

        While (foundAttachID)
            startOfAttachID = InStr(line, "attachID(")
            If (startOfAttachID > 0) Then
                endOfAttachID = InStr(startOfAttachID - 1, line, ",")
                endParanthesis = InStr(endOfAttachID, line, ")")
                line = line.Remove(endParanthesis - 1, 1)
                line = line.Remove(startOfAttachID - 1, endOfAttachID - startOfAttachID + 1)
            End If
            If (InStr(line, "attachID(") > 0) Then
                foundAttachID = True
            Else
                foundAttachID = False
            End If
        End While
        Return line
    End Function


    Private Shared s_instance As HowToForm

    Private stkxApp As AgSTKXApplication
    Private root As AgStkObjectRoot
    Private manager As IAgStkGraphicsSceneManager
    Private Scene As IAgStkGraphicsScene
    Private m_CodeSnippet As CodeSnippet
    Private m_TreeviewMouse As Point
    Private m_Control3D As AxAgUiAxVOCntrl
    Private LastMouseClickX As Integer, LastMouseClickY As Integer

    Private m_pickChangeColor As New PickChangeColorCodeSnippet()
    Private m_pickRectangular As New PickRectangularCodeSnippet()
    Private m_pickZoom As New PickZoomCodeSnippet()
    Private m_pickPerItem As New PickPerItemCodeSnippet()
    Private m_modelVectorOrientation As ModelVectorOrientationCodeSnippet
    Private m_modelArticulation As ModelArticulationCodeSnippet
    Private m_cameraFollowingSatellite As CameraFollowingSatelliteCodeSnippet
    Private m_surfaceMeshTransformations As SurfaceMeshTransformationsCodeSnippet
    Private m_timeDisplayCondition As TimeDisplayConditionCodeSnippet
    Private m_compositeDisplayCondition As CompositeDisplayConditionCodeSnippet
    Private m_pathDropLines As PathDropLineCodeSnippet
    Private m_Overlay As OverlayToolbar
End Class