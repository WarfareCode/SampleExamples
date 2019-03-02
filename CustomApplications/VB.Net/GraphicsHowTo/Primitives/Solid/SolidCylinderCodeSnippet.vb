#Region "UsingDirectives"
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt
#End Region

Namespace Primitives.Solid
    Class SolidCylinderCodeSnippet
        Inherits CodeSnippet
        Public Sub New()
            MyBase.New("Primitives\Solid\SolidCylinderCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim origin As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
            origin.AssignPlanetodetic(28.488889, -80.577778, 1000)
            Dim axes As IAgCrdnAxesFixed = CreateAxes(root, "Earth", origin)
            m_Axes = DirectCast(axes, IAgCrdnAxes)
            Dim system As IAgCrdnSystem = CreateSystem(root, "Earth", origin, axes)
            ExecuteSnippet(scene, root, system)
        End Sub

        ' Name        
        ' Description 
        ' Category    
        ' References  
        ' Namespaces  
        ' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawACylinder", _
            "Draw a cylinder", _
            "Graphics | Primitives | Solid Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsSolidPrimitive" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("system", "A system for the solid")> ByVal system As IAgCrdnSystem)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            
            Dim result As IAgStkGraphicsSolidTriangulatorResult = manager.Initializers.CylinderTriangulator.CreateSimple(attachID("$length$The length of the cylinder along the z axis$", 1000), attachID("$radius$The radius of the cylinder$", 2000))
            Dim solid As IAgStkGraphicsSolidPrimitive = manager.Initializers.SolidPrimitive.Initialize()
            DirectCast(solid, IAgStkGraphicsPrimitive).ReferenceFrame = system
            DirectCast(solid, IAgStkGraphicsPrimitive).Color = attachID("$color$The color of the cylinder$", Color.Yellow)
            solid.OutlineColor = attachID("$outlineColor$The System.Drawing.Color of the outline of the cylinder$", Color.Black)
            solid.OutlineWidth = attachID("$outlineWdith$the width of the outline$", 2)
            solid.OutlineAppearance = attachID("$outlineAppearance$The appearance of the outline$", AgEStkGraphicsOutlineAppearance.eStkGraphicsStylizeBackLines)
            solid.BackLineColor = attachID("$backLineColor$The System.Drawing.Color of the backLine of the cylinder$", Color.Black)
            solid.BackLineWidth = attachID("$backLineWidth$The width of the back line of the cylinder$", 1)
            solid.SetWithResult(result)

            manager.Primitives.Add(DirectCast(solid, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(solid, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("CylinderTriangulator.Compute is used to compute triangles for a cylinder, " & vbCrLf & _
                                     "which are visualized using a SolidPrimitive.", manager)
        End Sub

        Public Overrides Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim referenceAxes As IAgCrdnAxes = DirectCast(m_Axes, IAgCrdnAxesFixed).ReferenceAxes.GetAxes()
            Dim onSurface As IAgCrdnAxesOnSurface = DirectCast(referenceAxes, IAgCrdnAxesOnSurface)
            Dim offset As Array = New Object() {m_Primitive.BoundingSphere.Radius * 2.5, m_Primitive.BoundingSphere.Radius * 2.5, m_Primitive.BoundingSphere.Radius * 0.5}
            scene.Camera.ViewOffset(m_Axes, onSurface.ReferencePoint.GetPoint(), offset)
            scene.Render()
        End Sub

        Public Overrides Sub Remove(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            manager.Primitives.Remove(m_Primitive)

            m_Primitive = Nothing
            m_Axes = Nothing
            OverlayHelper.RemoveTextBox(manager)

            scene.Render()
        End Sub

        Private m_Primitive As IAgStkGraphicsPrimitive = Nothing
        Private m_Axes As IAgCrdnAxes = Nothing
    End Class
End Namespace