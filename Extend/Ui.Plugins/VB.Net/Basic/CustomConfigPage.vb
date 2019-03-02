Imports AGI.Ui.Plugins

Public Class CustomConfigPage
    Implements IAgUiPluginConfigurationPageActions

    Private _site As IAgUiPluginConfigurationPageSite
    Private _plugin As Agi.Ui.Plugins.VB_Net.Basic.BasicVBNetPlugin

    Public Function OnApply() As Boolean Implements Agi.Ui.Plugins.IAgUiPluginConfigurationPageActions.OnApply
        _plugin.Integrate = CheckBox1.Checked
        Return True
    End Function

    Public Sub OnCancel() Implements AGI.Ui.Plugins.IAgUiPluginConfigurationPageActions.OnCancel
        'Intentionally left empty
    End Sub

    Public Sub OnCreated(ByVal Site As AGI.Ui.Plugins.IAgUiPluginConfigurationPageSite) Implements AGI.Ui.Plugins.IAgUiPluginConfigurationPageActions.OnCreated
        _site = Site
        _plugin = DirectCast(_site.Plugin, BasicVBNetPlugin)
        CheckBox1.Checked = _plugin.Integrate
    End Sub

    Public Sub OnOK() Implements AGI.Ui.Plugins.IAgUiPluginConfigurationPageActions.OnOK
        _plugin.Integrate = CheckBox1.Checked
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        'Enable apply button
        If Not (_site Is Nothing) Then
            _site.SetModified(True)
        End If
    End Sub
End Class
