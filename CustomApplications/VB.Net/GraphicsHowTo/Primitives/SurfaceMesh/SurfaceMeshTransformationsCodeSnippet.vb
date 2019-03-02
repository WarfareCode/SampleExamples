#Region "UsingDirectives"
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Primitives.SurfaceMesh
	Class SurfaceMeshTransformationsCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\SurfaceMesh\SurfaceMeshTransformationsCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim textureFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/water.png").FullPath
            ExecuteSnippet(scene, root, textureFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAMovingTexture", _
            "Draw a moving water texture using affine transformations", _
            "Graphics | Primitives | Surface Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("textureFile", "The file to use as the texture of the surface mesh")> ByVal textureFile As String)
            Dim manager2 As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            If Not manager2.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod() Then
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            Else
				'#Region "CodeSnippet"
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
				Dim cartographicExtent As Array = New Object() {attachID("$westLon$Westernmost longitude$", -96), attachID("$southLat$Southernmost latitude$", 22), attachID("$eastLon$Easternmost longitude$", -85), attachID("$northLat$Northernmost latitude$", 28)}

				Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple(attachID("$planetName$The planet on which the surface mesh will be placed$", "Earth"), cartographicExtent)

				Dim texture As IAgStkGraphicsRendererTexture2D = manager.Textures.LoadFromStringUri( _
					textureFile)
				Dim mesh As IAgStkGraphicsSurfaceMeshPrimitive = manager.Initializers.SurfaceMeshPrimitive.Initialize()
				DirectCast(mesh, IAgStkGraphicsPrimitive).Translucency = attachID("$translucency$The translucency of the surface mesh$", 0.3F)
				mesh.Texture = texture
				mesh.TextureFilter = attachID("$textureFilter$The type of filter to use for the texture$", manager.Initializers.TextureFilter2D.LinearRepeat)
				mesh.Set(triangles)
				manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
				'#End Region

                m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
                m_Translation = 0
            End If

            OverlayHelper.AddTextBox("Animation effects such as water can be created by modifying a surface " & vbCrLf & _
                                     "mesh's TextureMatrix property over time.", manager2)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
			'
			' Set-up the animation for this specific example
			'
			animation.Pause()
			SetAnimationDefaults(root)
			DirectCast(root.CurrentScenario, IAgScenario).Animation.AnimStepValue = 1.0
			animation.PlayForward()

			If m_Primitive IsNot Nothing Then
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
				Dim center As Array = m_Primitive.BoundingSphere.Center
				Dim boundingSphere As IAgStkGraphicsBoundingSphere = manager.Initializers.BoundingSphere.Initialize(center, 500000)

                ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere, -90, 35)
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Primitive IsNot Nothing Then
				Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
				Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
				animation.Rewind()
				SetAnimationDefaults(root)

				manager.Primitives.Remove(m_Primitive)
				m_Primitive = Nothing

				OverlayHelper.RemoveTextBox(manager)
				scene.Render()
			End If
		End Sub

				'#Region "CodeSnippet"
				Friend Sub TimeChanged(scene As IAgStkGraphicsScene, root As AgStkObjectRoot, TimeEpSec As Double)
					'
					'  Translate the surface mesh every animation update
					'
					If m_Primitive IsNot Nothing Then
						Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

						m_Translation = CSng(TimeEpSec)
						m_Translation /= 1000

						Dim transformation As New Matrix()
						transformation.Translate(-m_Translation, 0)
						' Sign determines the direction of apparent flow
						' Convert the matrix to an object array
						Dim transformationArray As Array = Array.CreateInstance(GetType(Object), transformation.Elements.Length)
						For i As Integer = 0 To transformationArray.Length - 1
							transformationArray.SetValue(DirectCast(transformation.Elements.GetValue(i), Object), i)
						Next

						DirectCast(m_Primitive, IAgStkGraphicsSurfaceMeshPrimitive).TextureMatrix = manager.Initializers.TextureMatrix.InitializeWithAffineTransform(transformationArray)
					End If
				End Sub
				'#End Region

		Private m_Primitive As IAgStkGraphicsPrimitive
		Private m_Translation As Single
	End Class
End Namespace
