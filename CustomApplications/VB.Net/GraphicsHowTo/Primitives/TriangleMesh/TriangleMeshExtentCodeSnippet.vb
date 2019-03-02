#Region "UsingDirectives"
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.TriangleMesh
	Class TriangleMeshExtentCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\TriangleMesh\TriangleMeshExtentCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAnExtent", _
            "Draw a filled rectangular extent on the globe", _
            "Graphics | Primitives | Triangle Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim extent As Array = New Object() {attachID("$westLon$Westernmost longitude$", -94), attachID("$southLat$Southernmost latitude$", 29), attachID("$eastLon$Easternmost longitude$", -89), attachID("$northLat$Northernmost latitude$", 33)}

            Dim triangles As IAgStkGraphicsSurfaceTriangulatorResult = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple(attachID("$planetName$The planet on which the triangle mesh will be placed$", "Earth"), extent)

            Dim mesh As IAgStkGraphicsTriangleMeshPrimitive = manager.Initializers.TriangleMeshPrimitive.Initialize()
            mesh.SetTriangulator(DirectCast(triangles, IAgStkGraphicsTriangulatorResult))
            DirectCast(mesh, IAgStkGraphicsPrimitive).Color = attachID("$color$The System.Drawing.Color of the triangle mesh$", Color.Salmon)
            mesh.Lighting = False

            manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("SurfaceExtentTriangulator.Compute computes triangles for a rectangular " & vbCrLf & _
                                     "extent bounded by lines of constant latitude and longitude.", manager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere)
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
