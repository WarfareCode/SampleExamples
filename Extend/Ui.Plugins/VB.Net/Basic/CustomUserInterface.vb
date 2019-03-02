Imports System.Windows.Forms
Imports AGI.Ui.Plugins
Imports System.Threading

Public Class CustomUserInterface
    Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl

    Private m_pEmbeddedControlSite As AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite
    Private m_root As AGI.STKObjects.AgStkObjectRoot
    Private m_uiPlugin As Agi.Ui.Plugins.VB_Net.Basic.BasicVBNetPlugin

    Public Function GetIcon() As stdole.IPictureDisp Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.GetIcon
        Return Nothing
    End Function

    Public Sub OnClosing() Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.OnClosing

    End Sub

    Public Sub OnSaveModified() Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.OnSaveModified

    End Sub

    Public Sub SetSite(ByVal Site As AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite) Implements AGI.Ui.Plugins.IAgUiPluginEmbeddedControl.SetSite
        m_pEmbeddedControlSite = Site
        m_uiPlugin = m_pEmbeddedControlSite.Plugin
        m_root = m_uiPlugin.STKRoot
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Example use of StkObjectRoot
        If m_root.CurrentScenario Is Nothing Then
            MessageBox.Show("I know that no scenario is open.")
        Else
            Dim strScenName As String = m_root.CurrentScenario.Path.ToString()
            MessageBox.Show("I know your scenario's Connect path is " + strScenName)
        End If
    End Sub

    Private Sub progressButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles progressButton.Click
        'Example use of Progress Bar
        Dim progress As IAgProgressTrackCancel = m_uiPlugin.ProgressBar
        progress.BeginTracking(AgEProgressTrackingOptions.eProgressTrackingOptionNone, AgEProgressTrackingType.eTrackAsProgressBar)

        Dim i As Integer
        For i = 0 To 100
            progress.SetProgress(i, "Testing the progress bar...")
            Thread.Sleep(100)
            If (Not progress.Continue) Then
                Exit For
            End If
        Next

        progress.EndTracking()
    End Sub
End Class
