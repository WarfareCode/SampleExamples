Imports System.Runtime.InteropServices
Imports AGI.Ui.Plugins
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Drawing
Imports AGI.Ui.Core
Imports AGI.STKObjects
Imports AGI.Ui.Application

<Guid("6B984A64-C62B-42d5-91E0-CB25251E37A6"), _
ProgId("Agi.Ui.Plugins.VB_Net.Basic"), _
ClassInterface(ClassInterfaceType.None)> _
Public Class BasicVBNetPlugin
    Implements IAgUiPlugin
    Implements IAgUiPluginCommandTarget
    Implements IAgUiPlugin2

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
    Dim m_customUserInterface As CustomUserInterface
    Dim WithEvents m_root As AgStkObjectRootClass
    Dim m_progress As IAgProgressTrackCancel

    Dim m_integrate As Boolean = True

    Public Sub OnDisplayConfigurationPage(ByVal ConfigPageBuilder As Agi.Ui.Plugins.IAgUiPluginConfigurationPageBuilder) Implements Agi.Ui.Plugins.IAgUiPlugin.OnDisplayConfigurationPage
        'Add a Configuration Page
        ConfigPageBuilder.AddCustomUserControlPage(Me, Me.GetType().Assembly.Location, GetType(CustomConfigPage).FullName, "Basic VBNet Config Page")
    End Sub

    Public Sub OnDisplayContextMenu(ByVal MenuBuilder As Agi.Ui.Plugins.IAgUiPluginMenuBuilder) Implements Agi.Ui.Plugins.IAgUiPlugin.OnDisplayContextMenu
        Dim picture As stdole.IPictureDisp

        picture = IPictureDispHost.GetIPictureFromPicture(My.Resources.STK.ToBitmap())
        'Add a Menu Item
        If m_integrate Then
            MenuBuilder.AddMenuItem("AGI.BasicVBNetPlugin.MyFirstContextMenuCommand", "A VBNet Menu Item", "Open a Custom user interface.", picture)
        End If
    End Sub

    Public Sub OnInitializeToolbar(ByVal ToolbarBuilder As Agi.Ui.Plugins.IAgUiPluginToolbarBuilder) Implements Agi.Ui.Plugins.IAgUiPlugin.OnInitializeToolbar
        Dim picture As stdole.IPictureDisp

        'converting an ico file to be used as the image for toolbat button
        picture = IPictureDispHost.GetIPictureFromPicture(My.Resources.STK.ToBitmap())
        'Add a Toolbar Button
        ToolbarBuilder.AddButton("AGI.BasicVBNetPlugin.MyFirstCommand", "Example VBNet Ui Plugin Toolbar Button", "Open a Custom user interface.", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture)
    End Sub

    Public Sub OnShutdown() Implements Agi.Ui.Plugins.IAgUiPlugin.OnShutdown
        m_pSite = Nothing
    End Sub

    Public Sub OnStartup(ByVal PluginSite As Agi.Ui.Plugins.IAgUiPluginSite) Implements Agi.Ui.Plugins.IAgUiPlugin.OnStartup
        m_pSite = PluginSite
        'Get the AgStkObjectRoot
        Dim AgUiApp As IAgUiApplication = m_pSite.Application
        m_root = DirectCast(AgUiApp.Personality2, AgStkObjectRootClass)
    End Sub

    Public Sub OnDisplayMenu(ByVal MenuTitle As String, ByVal MenuBarKind As AgEUiPluginMenuBarKind, ByVal MenuBuilder As IAgUiPluginMenuBuilder2) Implements IAgUiPlugin2.OnDisplayMenu
        Dim picture As stdole.IPictureDisp

        picture = IPictureDispHost.GetIPictureFromPicture(My.Resources.STK.ToBitmap())
        If m_integrate Then
            If MenuTitle = "View" Then
                'Insert a Menu Item
                MenuBuilder.InsertMenuItem(0, "AGI.BasicVBNetPlugin.MyFirstTopLevelMenuCommand", "A VBNet Top Level Menu Item", "Open a simple message box.", picture)
            End If
            If MenuBarKind = AgEUiPluginMenuBarKind.eUiPluginMenuBarContextMenu Then
                'Add a Menu Item
                MenuBuilder.AddMenuItem("AGI.BasicVBNetPlugin.MyFirstTopLevelMenuCommand", "A VBNet Context Menu Item", "Open a simple message box.", picture)
            End If
        End If
    End Sub

    Public Sub Exec(ByVal CommandName As String, ByVal TrackCancel As Agi.Ui.Plugins.IAgProgressTrackCancel, ByVal Parameters As Agi.Ui.Plugins.IAgUiPluginCommandParameters) Implements Agi.Ui.Plugins.IAgUiPluginCommandTarget.Exec
        'Controls what a command does
        If (String.Compare(CommandName, "AGI.BasicVBNetPlugin.MyFirstCommand", True) = 0) Or _
        (String.Compare(CommandName, "AGI.BasicVBNetPlugin.MyFirstContextMenuCommand", True) = 0) Then
            m_progress = TrackCancel
            OpenUserInterface()
        End If
        If (String.Compare(CommandName, "AGI.BasicVBNetPlugin.MyFirstTopLevelMenuCommand", True) = 0) Then
            MessageBox.Show("A simple message box.", "Message Box")
        End If
    End Sub

    Public Function QueryState(ByVal CommandName As String) As Agi.Ui.Plugins.AgEUiPluginCommandState Implements Agi.Ui.Plugins.IAgUiPluginCommandTarget.QueryState
        'Enable commands
        If (String.Compare(CommandName, "AGI.BasicVBNetPlugin.MyFirstCommand", True) = 0) Or _
        (String.Compare(CommandName, "AGI.BasicVBNetPlugin.MyFirstContextMenuCommand", True) = 0) Or _
        (String.Compare(CommandName, "AGI.BasicVBNetPlugin.MyFirstTopLevelMenuCommand", True) = 0) Then
            Return AgEUiPluginCommandState.eUiPluginCommandStateEnabled Or AgEUiPluginCommandState.eUiPluginCommandStateSupported
        End If
        Return AgEUiPluginCommandState.eUiPluginCommandStateNone
    End Function

    Public Property customUI() As CustomUserInterface
        Get
            Return m_customUserInterface
        End Get
        Set(ByVal value As CustomUserInterface)
            m_customUserInterface = value
        End Set
    End Property

    Public Property Integrate() As Boolean
        Get
            Return m_integrate
        End Get
        Set(ByVal value As Boolean)
            m_integrate = value
        End Set
    End Property

    Public ReadOnly Property STKRoot() As AgStkObjectRootClass
        Get
            Return m_root
        End Get
    End Property

    Public ReadOnly Property ProgressBar() As IAgProgressTrackCancel
        Get
            Return m_progress
        End Get
    End Property

    Public Sub OpenUserInterface()
        'Open a User Interface
        Dim windows As IAgUiPluginWindowSite = TryCast(m_pSite, IAgUiPluginWindowSite)
        If windows Is Nothing Then
            MessageBox.Show("Host application is unable to open windows.")
        Else
            Dim params As AgUiPluginWindowCreateParameters = windows.CreateParameters()
            params.AllowMultiple = False
            params.AssemblyPath = Me.[GetType]().Assembly.Location
            params.UserControlFullName = GetType(CustomUserInterface).FullName
            params.Caption = "Basic User Interface"
            params.DockStyle = AgEDockStyle.eDockStyleDockedBottom
            params.Height = 150
            Dim obj As Object = windows.CreateNetToolWindowParam(Me, params)
        End If
    End Sub

End Class
