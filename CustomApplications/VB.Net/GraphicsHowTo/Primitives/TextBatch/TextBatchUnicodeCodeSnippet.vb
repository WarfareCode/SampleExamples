#Region "UsingDirectives"
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Primitives.TextBatch
    Class TextBatchUnicodeCodeSnippet
        Inherits CodeSnippet
        Public Sub New()
            MyBase.New("Primitives\TextBatch\TextBatchUnicodeCodeSnippet.vb")
        End Sub

        ' Name        
        ' Description 
        ' Category    
        ' References  
        ' Namespaces  
        ' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawASetOfUnicodeStrings", _
            "Draw a set of strings in various languages", _
            "Graphics | Primitives | Text Batch Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTextBatchPrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            '
            ' Create the strings
            '
            Dim german As Char() = {ChrW(87), ChrW(105), ChrW(108), ChrW(107), ChrW(111), ChrW(109), ChrW(109), ChrW(101), ChrW(110)}
            Dim french As Char() = {ChrW(66), ChrW(105), ChrW(101), ChrW(110), ChrW(118), ChrW(101), ChrW(110), ChrW(117), ChrW(101), ChrW(32)}
            Dim portuguese As Char() = {ChrW(66), ChrW(101), ChrW(109), ChrW(45), ChrW(118), ChrW(105), ChrW(110), ChrW(100), ChrW(111)}
            Dim english As Char() = {ChrW(87), ChrW(101), ChrW(108), ChrW(99), ChrW(111), ChrW(109), ChrW(101)}
            Dim russian As Char() = {ChrW(1044), ChrW(1086), ChrW(1073), ChrW(1088), ChrW(1086), ChrW(32), ChrW(1087), ChrW(1086), _
                                     ChrW(1078), ChrW(1072), ChrW(1083), ChrW(1086), ChrW(1074), ChrW(1072), ChrW(1090), ChrW(1100)}
            Dim arabic As Char() = {ChrW(1571), ChrW(1607), ChrW(1604), ChrW(1575), ChrW(1611), ChrW(32), ChrW(1608), ChrW(32), _
                                    ChrW(1587), ChrW(1607), ChrW(1604), ChrW(1575), ChrW(1611)}

            Dim text As Array = New Object() _
            { _
                New String(german), _
                New String(french), _
                New String(portuguese), _
                New String(english), _
                New String(russian), _
                New String(arabic) _
            }

            '
            ' Create the positions
            '
            Dim positions As Array = New Object() {attachID("$lat1$The latitude of the first string$", 51.0), attachID("$lon1$The longitude of the first string$", 9.0), attachID("$alt1$The altitude of the first string$", 0.0), _
                                                   attachID("$lat2$The latitude of the second string$", 46.0), attachID("$lon2$The longitude of the second string$", 2.0), attachID("$alt2$The altitude of the second string$", 0.0), _
                                                   attachID("$lat3$The latitude of the third string$", 39.5), attachID("$lon3$The longitude of the third string$", -8.0), attachID("$alt3$The altitude of the third string$", 0.0), _
                                                   attachID("$lat4$The latitude of the fourth string$", 38.0), attachID("$lon4$The longitude of the fourth string$", -97.0), attachID("$alt4$The altitude of the fourth string$", 0.0), _
                                                   attachID("$lat5$The latitude of the fifth string$", 60.0), attachID("$lon5$The longitude of the fifth string$", 100.0), attachID("$alt5$The altitude of the fifth string$", 0.0), _
                                                   attachID("$lat5$The latitude of the sixth string$", 25.0), attachID("$lon6$The longitude of the sixth string$", 45.0), attachID("$alt6$The altitude of the sixth string$", 0.0)}

            Dim font As IAgStkGraphicsGraphicsFont = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline(attachID("$fontName$Name of the font to use$", "Unicode"), attachID("$fontSize$Size of the font$", 12), attachID("$fontStyle$The style of the font$", AgEStkGraphicsFontStyle.eStkGraphicsFontStyleBold), attachID("$showOutline$Whether or not to should an outline around the text$", True))
            Dim textBatch As IAgStkGraphicsTextBatchPrimitive = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font)
            textBatch.SetCartographic(attachID("$planetName$The planet on which the text will be placed$", "Earth"), positions, text)

            manager.Primitives.Add(DirectCast(textBatch, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(textBatch, IAgStkGraphicsPrimitive)
        End Sub

        Public Overrides Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            If m_Primitive IsNot Nothing Then
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
                Dim center As Array = m_Primitive.BoundingSphere.Center
                Dim boundingSphere As IAgStkGraphicsBoundingSphere = manager.Initializers.BoundingSphere.Initialize(center, m_Primitive.BoundingSphere.Radius * 2)

                ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere)
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