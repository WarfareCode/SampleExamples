
#Region "UsingDirectives"
Imports System.IO
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace GlobeOverlays
    Class OpenStreetMapCodeSnippet
        Inherits CodeSnippet
        Public Sub New()
            MyBase.New("GlobeOverlays\OpenStreetMapCodeSnippet.vb")
        End Sub

        ' Name        
        ' Description 
        ' Category    
        ' References  
        ' Namespaces  
        ' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddCustomGlobeOverlayImagery", _
            "Add custom imagery to the globe", _
            "Graphics | GlobeOverlays", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsCustomImageGlobeOverlay" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            Dim manager2 As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Try
                '#Region "CodeSnippet"
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

                Dim activator As IAgStkGraphicsCustomImageGlobeOverlayPluginActivator = manager.Initializers.CustomImageGlobeOverlayPluginActivator.Initialize()
                Dim proxy As IAgStkGraphicsCustomImageGlobeOverlayPluginProxy = activator.CreateFromDisplayName(attachID("$pluginDisplayName$Display Name of the OpenStreetMapPlugin$", "OpenStreetMapPlugin.VBNET"))

                Dim overlay As IAgStkGraphicsCustomImageGlobeOverlay = proxy.CustomImageGlobeOverlay
                scene.CentralBodies.Earth.Imagery.Add(DirectCast(overlay, IAgStkGraphicsGlobeImageOverlay))
                '#End Region

                m_Overlay = DirectCast(overlay, IAgStkGraphicsGlobeImageOverlay)
            Catch ex As Exception
                If ex.Message.Contains("OpenStreetMapPlugin") Then
                    MessageBox.Show("A COM exception has occurred." & vbLf & vbLf & _
                                    "It is possible that one of the following may be the issue:" & vbLf & vbLf & _
                                    "1. OpenStreetMapPlugin.dll is not registered for COM interop." & vbLf & vbLf & _
                                    "2. That the plugin has not been added to the GfxPlugin category within a <install dir>\Plugins\*.xml file." & vbLf & vbLf & _
                                    "To resolve either of these issues:" & vbLf & vbLf & _
                                    "1. To register the plugin, open a Visual Studio " & _
                                    If(IntPtr.Size = 8, "x64 ", "") & "Command Prompt and execute the command:" & vbLf & vbLf & _
                                    vbTab & "regasm /codebase ""<install dir>\<CodeSamples>\Extend\Graphics\VB.Net\OpenStreetMapPlugin\bin\<Config>\OpenStreetMapPlugin.dll""" & vbLf & vbLf & _
                                    vbTab & "Note: if you do not have access to a Visual Studio Command Prompt regasm can be found here:" & vbLf & _
                                    vbTab & "C:\Windows\Microsoft.NET\Framework" & If(IntPtr.Size = 8, "64", "") & "\<.NET Version>\" & vbLf & vbLf & _
                                    "2. To add it to the GfxPlugins plugins registry category:" & vbLf & vbLf & _
                                    vbTab & "a. Copy the Graphics.xml from the <install dir>\CodeSamples\Extend\Graphics\Graphics.xml file to the <install dir>\Plugins directory." & vbLf & vbLf & _
                                    vbTab & "b. Then uncomment the plugin entry that contains a display name of OpenStreetMapPlugin.VBNET." & vbLf & vbLf, "Plugin Not Registered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show("Could not create globe overlay.  Your video card may not support this feature.", _
                                    "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Return
            End Try

            OverlayHelper.AddTextBox("Create an OpenStreetMapImageGlobeOverlay, with an " & vbLf & _
                                     "optional extent. This example requires an active " & vbLf & _
                                     "internet connection, otherwise no data is shown.", manager2)
        End Sub


        Public Overrides Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            scene.Render()
        End Sub

        Public Overrides Sub Remove(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            If m_Overlay IsNot Nothing Then
                scene.CentralBodies("Earth").Imagery.Remove(m_overlay)
                scene.Render()

                m_overlay = Nothing
            End If

            OverlayHelper.RemoveTextBox(DirectCast(root.CurrentScenario, IAgScenario).SceneManager)
            scene.Render()
        End Sub

        Dim m_overlay As IAgStkGraphicsGlobeImageOverlay

    End Class
End Namespace
