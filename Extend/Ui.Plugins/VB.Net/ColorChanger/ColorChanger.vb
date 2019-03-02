Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing
Imports AGI.Ui.Plugins
Imports AGI.STKObjects
Imports AGI.Ui.Application

<Guid("A5077996-08ED-4382-B569-B920BC51CBF4"), _
ProgId("Agi.Ui.Plugins.VB_Net.ColorChanger"), _
ClassInterface(ClassInterfaceType.None)> _
Public Class ColorChanger
    Implements IAgUiPlugin
    Implements IAgUiPluginCommandTarget

    Friend NotInheritable Class IPictureDispHost
        Inherits AxHost

        Private Sub New()
            MyBase.New(String.Empty)
        End Sub

        Public Shared Shadows Function GetIPictureFromPicture(image As Image) As Object
            Return AxHost.GetIPictureFromPicture(image)
        End Function

        Public Shared Shadows Function GetPictureFromIPicture(picture As Object) As Image
            Return AxHost.GetPictureFromIPicture(picture)
        End Function

    End Class

    Dim m_pSite As IAgUiPluginSite
    Dim m_root As AgStkObjectRootClass
    Dim form As New ColorForm

    Public Sub OnDisplayConfigurationPage(ByVal ConfigPageBuilder As AGI.Ui.Plugins.IAgUiPluginConfigurationPageBuilder) Implements AGI.Ui.Plugins.IAgUiPlugin.OnDisplayConfigurationPage

    End Sub

    Public Sub OnDisplayContextMenu(ByVal MenuBuilder As AGI.Ui.Plugins.IAgUiPluginMenuBuilder) Implements AGI.Ui.Plugins.IAgUiPlugin.OnDisplayContextMenu
        Dim picture As stdole.IPictureDisp

        picture = IPictureDispHost.GetIPictureFromPicture(My.Resources.STK.ToBitmap())
        'Add a Menu Item
        MenuBuilder.AddMenuItem("AGI.ColorChangePlugin.ChangeColor", "Change Selected Objects Color", "Change objects color", picture)
    End Sub

    Public Sub OnInitializeToolbar(ByVal ToolbarBuilder As AGI.Ui.Plugins.IAgUiPluginToolbarBuilder) Implements AGI.Ui.Plugins.IAgUiPlugin.OnInitializeToolbar
        Dim picture As stdole.IPictureDisp

        'converting an ico file to be used as the image for toolbat button
        picture = IPictureDispHost.GetIPictureFromPicture(My.Resources.STK.ToBitmap())
        'Add a Toolbar Button
        ToolbarBuilder.AddButton("AGI.ColorChangePlugin.ChangeColor", "Change Selected Objects Color", "Change objects color", AgEToolBarButtonOptions.eToolBarButtonOptionNeedScenario, picture)
    End Sub

    Public Sub OnShutdown() Implements AGI.Ui.Plugins.IAgUiPlugin.OnShutdown
        m_pSite = Nothing
    End Sub

    Public Sub OnStartup(ByVal PluginSite As AGI.Ui.Plugins.IAgUiPluginSite) Implements AGI.Ui.Plugins.IAgUiPlugin.OnStartup
        m_pSite = PluginSite
        'Get the AgStkObjectRoot
        Dim AgUiApp As IAgUiApplication = m_pSite.Application
        m_root = DirectCast(AgUiApp.Personality2, AgStkObjectRootClass)
    End Sub

    Public Sub Exec(ByVal CommandName As String, ByVal TrackCancel As Agi.Ui.Plugins.IAgProgressTrackCancel, ByVal Parameters As Agi.Ui.Plugins.IAgUiPluginCommandParameters) Implements Agi.Ui.Plugins.IAgUiPluginCommandTarget.Exec
        'Controls what a command does
        If (String.Compare(CommandName, "AGI.ColorChangePlugin.ChangeColor", True) = 0) Then
            If (form.ShowDialog() = DialogResult.OK) Then
                ChangeObjectColor(m_pSite.Selection)
            End If
        End If
    End Sub

    Public Function QueryState(ByVal CommandName As String) As AGI.Ui.Plugins.AgEUiPluginCommandState Implements AGI.Ui.Plugins.IAgUiPluginCommandTarget.QueryState
        'Enable commands
        If (String.Compare(CommandName, "AGI.ColorChangePlugin.ChangeColor", True) = 0) Then
            Return AgEUiPluginCommandState.eUiPluginCommandStateEnabled Or AgEUiPluginCommandState.eUiPluginCommandStateSupported
        End If
        Return AgEUiPluginCommandState.eUiPluginCommandStateNone
    End Function

    Private Sub ChangeObjectColor(ByVal selected As IAgUiPluginSelectedObjectCollection)
        'Change object colors based on color method
        Dim obj As IAgUiPluginSelectedObject
        If (selected.Count > 1 And form.colorMethod = 0 And Not form.firstColor.IsEmpty And Not form.secondColor.IsEmpty) Then
            Dim rStep As Integer = (CType(form.secondColor.R, Integer) - CType(form.firstColor.R, Integer)) / (selected.Count - 1)
            Dim gStep As Integer = (CType(form.secondColor.G, Integer) - CType(form.firstColor.G, Integer)) / (selected.Count - 1)
            Dim bStep As Integer = (CType(form.secondColor.B, Integer) - CType(form.firstColor.B, Integer)) / (selected.Count - 1)
            For i As Integer = 0 To selected.Count - 1
                obj = selected.Item(i)
                m_root.ExecuteCommand("Graphics " + obj.Path + " SetColor %" + createRGBString(form.firstColor, rStep, gStep, bStep, i))
            Next
        ElseIf (Not form.firstColor.IsEmpty) Then
            For Each obj In selected
                m_root.ExecuteCommand("Graphics " + obj.Path + " SetColor %" + createRGBString(form.firstColor, 0, 0, 0, 1))
            Next
        End If
    End Sub

    Private Function createRGBString(ByVal baseColor As Color, ByVal rStep As Integer, ByVal gStep As Integer, ByVal bStep As Integer, ByVal count As Integer) As String
        'Create a string of the RGB values
        Dim r As Integer = baseColor.R + rStep * count
        Dim g As Integer = baseColor.G + gStep * count
        Dim b As Integer = baseColor.B + bStep * count
        Dim rgbString As String
        If (r < 10) Then
            rgbString = "00" + r.ToString()
        ElseIf (r < 100) Then
            rgbString = "0" + r.ToString()
        Else
            rgbString = r.ToString()
        End If

        If (g < 10) Then
            rgbString += "00" + g.ToString()
        ElseIf (g < 100) Then
            rgbString += "0" + g.ToString()
        Else
            rgbString += g.ToString()
        End If

        If (b < 10) Then
            rgbString += "00" + b.ToString()
        ElseIf (b < 100) Then
            rgbString += "0" + b.ToString()
        Else
            rgbString += b.ToString()
        End If

        Return rgbString
    End Function
End Class
