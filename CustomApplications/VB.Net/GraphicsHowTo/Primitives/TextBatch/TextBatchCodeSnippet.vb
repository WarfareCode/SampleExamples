#Region "UsingDirectives"
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.TextBatch
    Class TextBatchCodeSnippet
        Inherits CodeSnippet
        Public Sub New()
            MyBase.New("Primitives\TextBatch\TextBatchCodeSnippet.vb")
        End Sub

        ' Name        
        ' Description 
        ' Category    
        ' References  
        ' Namespaces  
        ' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawASetOfStrings", _
            "Draw a set of strings", _
            "Graphics | Primitives | Text Batch Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTextBatchPrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            Dim text As Array = New Object() {attachID("$string1$The first string$", "Philadelphia"), attachID("$string2$The second string$", "Washington, D.C."), attachID("$string3$The third string$", "New Orleans"), attachID("$string4$The fourthstring$", "San Jose")}

            Dim positions As Array = New Object() {attachID("$lat1$The latitude of the first string$", 39.88), attachID("$lon1$The longitude of the first string$", -75.25), attachID("$alt1$The altitude of the first string$", 0), _
                                                   attachID("$lat2$The latitude of the second string$", 38.85), attachID("$lon2$The longitude of the second string$", -77.04), attachID("$alt2$The altitude of the second string$", 0), _
                                                   attachID("$lat3$The latitude of the third string$", 29.98), attachID("$lon3$The longitude of the third string$", -90.25), attachID("$alt3$The altitude of the third string$", 0), _
                                                  attachID("$lat4$The latitude of the fourth string$", 37.37), attachID("$lon4$The longitude of the fourth string$", -121.92), attachID("$alt4$The altitude of the fourth string$", 0)}

            Dim font As IAgStkGraphicsGraphicsFont = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline(attachID("$fontName$Name of the font to use$", "MS Sans Serif"), attachID("$fontSize$Size of the font$", 12), attachID("$fontStyle$The style of the font$", AgEStkGraphicsFontStyle.eStkGraphicsFontStyleBold), attachID("$showOutline$Whether or not to should an outline around the text$", True))
            Dim textBatch As IAgStkGraphicsTextBatchPrimitive = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font)
            DirectCast(textBatch, IAgStkGraphicsPrimitive).Color = attachID("$textColor$The System.Drawing.Color of the text$", Color.White)
            textBatch.OutlineColor = attachID("$outlineColor$The System.Drawing.Color of the outline$", Color.Red)
            textBatch.SetCartographic(attachID("$planetName$The planet on which the text will be placed$", "Earth"), positions, text)

            manager.Primitives.Add(DirectCast(textBatch, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(textBatch, IAgStkGraphicsPrimitive)
        End Sub

        Public Overrides Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            If m_Primitive IsNot Nothing Then
                ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere)
                scene.Render()
            End If
        End Sub

        Public Overrides Sub Remove(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            If m_Primitive IsNot Nothing Then
                manager.Primitives.Remove(m_Primitive)
                scene.Render()

                m_Primitive = Nothing
            End If
        End Sub

        Private m_Primitive As IAgStkGraphicsPrimitive
    End Class
End Namespace