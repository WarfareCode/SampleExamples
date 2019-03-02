#Region "UsingDirectives"
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Primitives.OrderedComposite
	Class OrderedCompositeZOrderCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Primitives\OrderedComposite\OrderedCompositeZOrderCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim pennsylvaniaPositions As Array = STKUtil.ReadAreaTargetPoints( _
                New AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/_pennsylvania_1.at").FullPath, root)
            Dim areaCode610Positions As Array = STKUtil.ReadAreaTargetPoints( _
                New AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/610.at").FullPath, root)
            Dim areaCode215Positions As Array = STKUtil.ReadAreaTargetPoints( _
                New AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/215.at").FullPath, root)
            Dim schuylkillPositions As Array = STKUtil.ReadLineTargetPoints( _
                New AGI.DataPath(AGI.DataPathRoot.Relative, "LineTargets/Schuylkill.lt").FullPath, root)

            ExecuteSnippet(scene, root, pennsylvaniaPositions, areaCode610Positions, areaCode215Positions, schuylkillPositions)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawASetOfOrderedPrimitives", _
            "Z-order primitives on the surface", _
            "Graphics | Primitives | Composite Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsSolidPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("pennsylvaniaPositions", "Pennsylvania positions")> ByVal pennsylvaniaPositions As Array, <AGI.CodeSnippets.CodeSnippet.Parameter("areaCode610Positions", "Area Code 610 positions")> ByVal areaCode610Positions As Array, <AGI.CodeSnippets.CodeSnippet.Parameter("areaCode215Positions", "Area Code 215 positions")> ByVal areaCode215Positions As Array, <AGI.CodeSnippets.CodeSnippet.Parameter("schuylkillPositions", "Schuylkill positions")> ByVal schuylkillPositions As Array)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim pennsylvania As IAgStkGraphicsTriangleMeshPrimitive = manager.Initializers.TriangleMeshPrimitive.Initialize()
            pennsylvania.SetTriangulator(DirectCast(manager.Initializers.SurfacePolygonTriangulator.Compute("Earth", pennsylvaniaPositions), IAgStkGraphicsTriangulatorResult))
            DirectCast(pennsylvania, IAgStkGraphicsPrimitive).Color = attachID("$color$System.Drawing.Color of the primitive$", Color.Yellow)

            Dim areaCode610 As IAgStkGraphicsTriangleMeshPrimitive = manager.Initializers.TriangleMeshPrimitive.Initialize()
            areaCode610.SetTriangulator(DirectCast(manager.Initializers.SurfacePolygonTriangulator.Compute("Earth", areaCode610Positions), IAgStkGraphicsTriangulatorResult))
            DirectCast(areaCode610, IAgStkGraphicsPrimitive).Color = attachID("$color$System.Drawing.Color of the primitive$", Color.DarkRed)

            Dim areaCode215 As IAgStkGraphicsTriangleMeshPrimitive = manager.Initializers.TriangleMeshPrimitive.Initialize()
            areaCode215.SetTriangulator(DirectCast(manager.Initializers.SurfacePolygonTriangulator.Compute("Earth", areaCode215Positions), IAgStkGraphicsTriangulatorResult))
            DirectCast(areaCode215, IAgStkGraphicsPrimitive).Color = attachID("$color$System.Drawing.Color of the primitive$", Color.Green)

            Dim schuylkillRiver As IAgStkGraphicsPolylinePrimitive = manager.Initializers.PolylinePrimitive.Initialize()
            schuylkillRiver.Set(schuylkillPositions)
            DirectCast(schuylkillRiver, IAgStkGraphicsPrimitive).Color = attachID("$color$System.Drawing.Color of the primitive$", Color.Blue)
            schuylkillRiver.Width = 2

            Dim composite As IAgStkGraphicsCompositePrimitive = manager.Initializers.CompositePrimitive.Initialize()
            composite.Add(DirectCast(pennsylvania, IAgStkGraphicsPrimitive))
            composite.Add(DirectCast(areaCode610, IAgStkGraphicsPrimitive))
            composite.Add(DirectCast(areaCode215, IAgStkGraphicsPrimitive))
            composite.Add(DirectCast(schuylkillRiver, IAgStkGraphicsPrimitive))

            manager.Primitives.Add(DirectCast(composite, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(composite, IAgStkGraphicsPrimitive)

            OverlayHelper.AddTextBox("Using an OrderedCompositePrimitive, the Schuylkill River polyline " & vbCrLf & _
                                     "is drawn on top of the 215 and 610 area code triangle meshes, which " & vbCrLf & _
                                     "are drawn on top of the Pennsylvania triangle mesh." & vbCrLf & vbCrLf & _
                                     "Primitives added to the composite last are drawn on top.  The order" & vbCrLf & _
                                     "of primitives in the composite can be changed with methods such as" & vbCrLf & _
                                     "BringToFront() and SendToBack().", manager)

            Dim text As Array = New Object() {"Pennsylvania", "610", "215", "Schuylkill River"}

            Dim positions As Array = New Object(11) {}
            DirectCast(pennsylvania, IAgStkGraphicsPrimitive).BoundingSphere.Center.CopyTo(positions, 0)
            DirectCast(areaCode610, IAgStkGraphicsPrimitive).BoundingSphere.Center.CopyTo(positions, 3)
            DirectCast(areaCode215, IAgStkGraphicsPrimitive).BoundingSphere.Center.CopyTo(positions, 6)
            DirectCast(schuylkillRiver, IAgStkGraphicsPrimitive).BoundingSphere.Center.CopyTo(positions, 9)

            Dim font As IAgStkGraphicsGraphicsFont = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline("MS Sans Serif", 16, AgEStkGraphicsFontStyle.eStkGraphicsFontStyleBold, True)
            Dim textBatch As IAgStkGraphicsTextBatchPrimitive = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font)
            DirectCast(textBatch, IAgStkGraphicsPrimitive).Color = Color.White
            textBatch.OutlineColor = Color.Black
            textBatch.Set(positions, text)

            composite.Add(DirectCast(textBatch, IAgStkGraphicsPrimitive))
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			Dim center As Array = m_Primitive.BoundingSphere.Center
			Dim boundingSphere As IAgStkGraphicsBoundingSphere = manager.Initializers.BoundingSphere.Initialize(center, m_Primitive.BoundingSphere.Radius * 0.35)
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere, -27, 3)

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
