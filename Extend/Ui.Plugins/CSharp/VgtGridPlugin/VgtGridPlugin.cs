using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using Microsoft.Win32;
using System.Text;

namespace Agi.Ui.Plugins.CSharp.VgtGridPlugin
{
	[Guid("17C6745C-CE9F-48bd-BDDA-1D9F00B2AC8F")]
    [ProgId("Agi.Ui.Plugins.CSharp.VgtGridPlugin")]
    [ClassInterface(ClassInterfaceType.None)]
    public class VgtGridPlugin :    AGI.Ui.Plugins.IAgUiPlugin,
                                    AGI.Ui.Plugins.IAgUiPluginCommandTarget
    {
        private AGI.Ui.Plugins.IAgUiPluginSite m_pSite;
        private AGI.STKObjects.AgStkObjectRootClass m_root;

        public AGI.STKObjects.AgStkObjectRootClass STKRoot
        {
            get { return m_root; }
        }

        #region IAgUiPlugin Members

        public void OnDisplayConfigurationPage(AGI.Ui.Plugins.IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            // Not required.
        }

        public void OnDisplayContextMenu(AGI.Ui.Plugins.IAgUiPluginMenuBuilder MenuBuilder)
        {
            if (m_pSite.Selection.Count == 1 && m_root.GetObjectFromPath(m_pSite.Selection[0].Path).IsVgtSupported())
                MenuBuilder.AddMenuItem("Agi.Ui.Plugins.CSharp.VgtGridPlugin.OpenUserInterface", "VGT Grid Tool", "Display the VGT Grid Manager.", null);
        }

        public void OnInitializeToolbar(AGI.Ui.Plugins.IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            // No toolbar.
        }

        public void OnShutdown()
        {
            m_pSite = null;
        }

        public void OnStartup(AGI.Ui.Plugins.IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRootClass;
        }

        #endregion

        #region IAgUiPluginCommandTarget Members

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            if (string.Compare(CommandName, "Agi.Ui.Plugins.CSharp.VgtGridPlugin.OpenUserInterface", true) == 0)
            {
                OpenUserInterface();
            }
        }


        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            if (string.Compare(CommandName, "Agi.Ui.Plugins.CSharp.VgtGridPlugin.OpenUserInterface", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }

            return AgEUiPluginCommandState.eUiPluginCommandStateNone;

        }

        #endregion

        public void OpenUserInterface()
        {
            AGI.Ui.Plugins.IAgUiPluginWindowSite windows = m_pSite as AGI.Ui.Plugins.IAgUiPluginWindowSite;

            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                AGI.Ui.Plugins.IAgUiPluginWindowCreateParameters @params = windows.CreateParameters();
                @params.AllowMultiple = false;
                @params.AssemblyPath = this.GetType().Assembly.Location;
                @params.UserControlFullName = typeof(VgtGridManager).FullName;
                @params.Caption = "VGT Grid Manager";
                @params.DockStyle = AgEDockStyle.eDockStyleFloating;
                @params.Height = 360;
                @params.Width = 475;
                object obj = windows.CreateNetToolWindowParam(this, @params);
            }
        }

        #region Registration functions
        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        [ComRegisterFunction]
        [ComVisible(false)]
        public static void RegisterFunction(Type t)
        {
            RemoveOtherVersions(t);
        }

        /// <summary>
        /// Called when the assembly is unregistered for use from COM.
        /// </summary>
        /// <param name="t">The type exposed to COM.</param>
        [ComUnregisterFunctionAttribute]
        [ComVisible(false)]
        public static void UnregisterFunction(Type t)
        {
            // Do nothing.
        }

        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// Eliminates the other versions present in the registry for
        /// this type.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        public static void RemoveOtherVersions(Type t)
        {
            try
            {
                using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
                {
                    StringBuilder guidString = new StringBuilder("{");
                    guidString.Append(t.GUID.ToString());
                    guidString.Append("}");
                    using (RegistryKey guidKey = clsidKey.OpenSubKey(guidString.ToString()))
                    {
                        if (guidKey != null)
                        {
                            using (RegistryKey inproc32Key = guidKey.OpenSubKey("InprocServer32", true))
                            {
                                if (inproc32Key != null)
                                {
                                    string currentVersion = t.Assembly.GetName().Version.ToString();
                                    string[] subKeyNames = inproc32Key.GetSubKeyNames();
                                    if (subKeyNames.Length > 1)
                                    {
                                        foreach (string subKeyName in subKeyNames)
                                        {
                                            if (subKeyName != currentVersion)
                                            {
                                                inproc32Key.DeleteSubKey(subKeyName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore all exceptions...
            }
        }
        #endregion
    }
}
