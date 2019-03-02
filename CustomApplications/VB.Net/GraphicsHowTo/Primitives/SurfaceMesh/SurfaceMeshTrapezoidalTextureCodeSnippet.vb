#Region "UsingDirectives"
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.SurfaceMesh
	Class SurfaceMeshTrapezoidalTextureCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\SurfaceMesh\SurfaceMeshTrapezoidalTextureCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim textureFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/surfaceMeshTrapezoidalTexture.jpg").FullPath
            ExecuteSnippet(scene, root, textureFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAFilledAreaTargetOnGlobe", _
            "Draw a filled STK area target on the globe", _
            "Graphics | Primitives | Surface Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("textureFile", "The texture file")> ByVal textureFile As String)
            Dim videoCheck As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            If Not videoCheck.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod() Then
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            '
            ' Load the UAV image where each corner maps to a longitude and latitude defined 
            ' in degrees below.
            '
            '    lower left  = (-0.386182, 42.938583)
            '    lower right = (-0.375100, 42.929871)
            '    upper right = (-0.333891, 42.944780)
            '    upper left  = (-0.359980, 42.973438)
            '

            Dim texture As IAgStkGraphicsRendererTexture2D = manager.Textures.LoadFromStringUri( _
                textureFile)

            '
            ' Define the bounding extent of the image.  Create a surface mesh that uses this 
            ' extent.
            '
            Dim mesh As IAgStkGraphicsSurfaceMeshPrimitive = manager.Initializers.SurfaceMeshPrimitive.Initialize()
            mesh.Texture = texture

            Dim cartographicExtent As Array = New Object() {attachID("$westLon$Westernmost longitude$", -0.386182), attachID("$southLat$Southernmost latitude$", 42.929871), attachID("$eastLon$Easternmost longitude$", -0.333891), attachID("$northLat$Northernmost latitude$", 42.973438)}

            Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple(attachID("$planetName$The name of the planet on which the surface mesh will be placed$", "Earth"), cartographicExtent)
            mesh.Set(triangles)
            DirectCast(mesh, IAgStkGraphicsPrimitive).Translucency = attachID("$translucency$The translucency of the surface mesh$", 0.0F)

            '
            ' Create the texture matrix that maps the image corner points to their actual
            ' cartographic coordinates.  A few notes:
            '
            ' 1. The TextureMatrix does not do any special processing on these values
            '    as if they were cartographic coordinates.
            '
            ' 2. Because of 1., the values only have to be correct relative to each
            '    other, which is why they do not have to be converted to radians.
            '
            ' 3. Because of 2., if your image straddles the +/- 180 degs longitude line, 
            '    ensure that longitudes east of the line are greater than those west of
            '    the line.  For example, if one point were 179.0 degs longitude and the
            '    other were to the east at -179.0 degs, the one to the east should be
            '    specified as 181.0 degs.
            '

            Dim c0 As Array = New Object() {attachID("$c0Lon$Longitude of the lower left corner$", -0.386182), attachID("$c0Lat$Latitude of the lower left corner$", 42.938583)}
            Dim c1 As Array = New Object() {attachID("$c1Lon$Longitude of the lower right corner$", -0.3751), attachID("$c1Lat$Latitude of the lower right corner$", 42.929871)}
            Dim c2 As Array = New Object() {attachID("$c2Lon$Longitude of the upper right corner$", -0.333891), attachID("$c2Lat$Latitude of the upper right corner$", 42.94478)}
            Dim c3 As Array = New Object() {attachID("$c3Lon$Longitude of the upper left corner$", -0.35998), attachID("$c3Lat$Latitude of the upper left corner$", 42.973438)}

            mesh.TextureMatrix = manager.Initializers.TextureMatrix.InitializeWithRectangles(c0, c1, c2, c3)

            '
            ' Enable the transparent texture border option on the mesh so that the texture will not
            ' bleed outside of the trapezoid.
            '
            mesh.TransparentTextureBorder = True

            '
            ' Add the surface mesh to the Scene manager
            '
            manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
            '#End Region
            m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("The surface mesh's TextureMatrix is used " & vbCrLf & _
                                     "to map a rectangular texture to a trapezoid.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
			scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

            Dim extent As Array = New Double() {-0.386182, 42.929871, -0.333891, 42.973438}

            ViewHelper.ViewExtent(scene, root, "Earth", extent, -135, 30)
			scene.Render()
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			manager.Primitives.Remove(m_Primitive)
			m_Primitive = Nothing

			OverlayHelper.RemoveTextBox(manager)
			scene.Render()
		End Sub

		Private m_Primitive As IAgStkGraphicsPrimitive
	End Class
End Namespace
