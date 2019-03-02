#Region "UsingDirectives"
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.TriangleMesh
	Class TriangleMeshExtrusionCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\TriangleMesh\TriangleMeshExtrusionCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim positions As Array = STKUtil.ReadAreaTargetPoints(New AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/_pennsylvania_1.at").FullPath, root)
            ExecuteSnippet(scene, root, positions)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawAnExtrudedAreaTarget", _
            "Draw an extrusion around a STK area target", _
            "Graphics | Primitives | Triangle Mesh Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTriangleMeshPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("positions", "The positions used to compute triangulation")> ByVal positions As Array)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim triangles As IAgStkGraphicsExtrudedPolylineTriangulatorResult = manager.Initializers.ExtrudedPolylineTriangulator.ComputeWithAltitudes(attachID("$planetName$The planet on which the triangle mesh will be placed$", "Earth"), positions, attachID("$bottomAlt$The altitude of the bottom of the extrusion$", 10000), attachID("$topAlt$The altitude of the top of the extrusion$", 25000))
            Dim mesh As IAgStkGraphicsTriangleMeshPrimitive = manager.Initializers.TriangleMeshPrimitive.Initialize()
            mesh.SetTriangulator(DirectCast(triangles, IAgStkGraphicsTriangulatorResult))
            DirectCast(mesh, IAgStkGraphicsPrimitive).Color = attachID("$color$The System.Drawing.Color of the triangle mesh$", Color.Red)
            DirectCast(mesh, IAgStkGraphicsPrimitive).Translucency = attachID("$translucency$The translucency of the triangle mesh$", 0.4F)

            manager.Primitives.Add(DirectCast(mesh, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(mesh, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("ExtrudedPolylineTriangulator.Compute computes triangles for an " & vbCrLf & _
                                     "extrusion, which are visualized with a TriangleMeshPrimitive.", manager)
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
