#Region "UsingDirectives"
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Drawing
Imports AGI.STKObjects
Imports AGI.STKGraphics
Imports AGI.STKUtil
#End Region

Namespace Primitives.TextBatch
    Class TextBatchColorsCodeSnippet
        Inherits CodeSnippet
        Public Sub New()
            MyBase.New("Primitives\TextBatch\TextBatchColorsCodeSnippet.vb")
        End Sub

        ' Name        
        ' Description 
        ' Category    
        ' References  
        ' Namespaces  
        ' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "DrawASetOfColoredStrings", _
            "Draw a set of uniquely colored strings", _
            "Graphics | Primitives | Text Batch Primitive", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTextBatchPrimitive" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim strings As Array = New Object() {attachID("$string1$The first string$", "San Francisco"), attachID("$string2$The second string$", "Sacramento"), attachID("$string3$The third string$", "Los Angeles"), attachID("$string4$The fourth string$", "San Diego")}

            Dim positions As Array = New Object() _
            { _
                attachID("$lat1$The latitude of the first string$", 37.62), attachID("$lon1$The longitude of the first string$", -122.38), attachID("$alt1$The altitude of the first string$", 0.0), _
                attachID("$lat2$The latitude of the second string$", 38.52), attachID("$lon2$The longitude of the second string$", -121.5), attachID("$alt2$The altitude of the second string$", 0.0), _
                attachID("$lat3$The latitude of the third string$", 33.93), attachID("$lon3$The longitude of the third string$", -118.4), attachID("$alt3$The altitude of the third string$", 0.0), _
                attachID("$lat4$The latitude of the fourth string$", 32.82), attachID("$lon4$The longitude of the fourth string$", -117.13), attachID("$alt4$The altitude of the fourth string$", 0.0) _
            }

            Dim colors As Array = New Object() _
            { _
                attachID("$color1$The color of the first string (a System.Drawing.Color converted to Argb)$", Color.Red.ToArgb()), _
                attachID("$color2$The color of the second string (a System.Drawing.Color converted to Argb)$", Color.Green.ToArgb()), _
                attachID("$color3$The color of the third string (a System.Drawing.Color converted to Argb)$", Color.Blue.ToArgb()), _
               attachID("$color4$The color of the fourth string (a System.Drawing.Color converted to Argb)$", Color.White.ToArgb()) _
            }

            Dim parameters As IAgStkGraphicsTextBatchPrimitiveOptionalParameters = manager.Initializers.TextBatchPrimitiveOptionalParameters.Initialize()
            parameters.SetColors(colors)

            Dim font As IAgStkGraphicsGraphicsFont = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline(attachID("$fontName$Name of the font to use$", "MS Sans Serif"), attachID("$fontSize$Size of the font$", 12), attachID("$fontStyle$The style of the font$", AgEStkGraphicsFontStyle.eStkGraphicsFontStyleBold), attachID("$showOutline$Whether or not to should an outline around the text$", False))
            Dim textBatch As IAgStkGraphicsTextBatchPrimitive = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font)
            textBatch.SetCartographicWithOptionalParameters(attachID("$planetName$The planet on which the text will be placed$", "Earth"), positions, strings, parameters)

            manager.Primitives.Add(DirectCast(textBatch, IAgStkGraphicsPrimitive))
            '#End Region

            m_Primitive = DirectCast(textBatch, IAgStkGraphicsPrimitive)
            OverlayHelper.AddTextBox("A collection of colors is provided to the TextBatchPrimitive, in addition to collections of " & vbCrLf & _
                                     "positions and strings, to visualize strings with different colors.", manager)
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
                m_Primitive = Nothing

                OverlayHelper.RemoveTextBox(manager)
                scene.Render()
            End If
        End Sub

        Private m_Primitive As IAgStkGraphicsPrimitive
    End Class
End Namespace