using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using System.Reflection;
using System.Drawing;
using AGI.Ui.Core;
using AGI.STKObjects;
using AGI.Ui.Application;
using Microsoft.Win32;

namespace Agi.Ui.Plugins.CSharp.GfxAnalysis
{
	[Guid("79F36D85-F25D-4aea-998D-36EB05054E42")]
    [ProgId("Agi.Ui.Plugins.CSharp.GfxAnalysis")]
    [ClassInterface(ClassInterfaceType.None)]
    public class GfxAnalysisPlugin : IAgUiPlugin, IAgUiPluginCommandTarget
    {
        internal sealed class IPictureDispHost : AxHost
        {
            private IPictureDispHost() : base(string.Empty) { }

            public new static object GetIPictureDispFromPicture(Image image)
            {
                return AxHost.GetIPictureDispFromPicture(image);
            }

            public new static Image GetPictureFromIPicture(object picture)
            {
                return AxHost.GetPictureFromIPicture(picture);
            }
        }

        private AGI.Ui.Plugins.IAgUiPluginSite m_pSite;
        private CustomUserInterface m_customUserInterface;
        private AgStkObjectRootClass m_root;
        private bool m_WasOpen = false;

        #region IAgUiPlugin Members

        public void OnDisplayConfigurationPage(AGI.Ui.Plugins.IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
        }

        public void OnDisplayContextMenu(AGI.Ui.Plugins.IAgUiPluginMenuBuilder MenuBuilder)
        {
            stdole.IPictureDisp picture;
            string imageResource = "Agi.Ui.Plugins.CSharp.GfxAnalysis.STK.ico";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
            picture = (stdole.IPictureDisp)IPictureDispHost.GetIPictureDispFromPicture(icon.ToBitmap());
            //Add a Menu Item
            MenuBuilder.AddMenuItem("Sample.SampleUiPlugin.MyFirstContextMenuCommand", "Open Gfx Analysis user interface", "Open my custom user interface.", picture);
        }

        public void OnInitializeToolbar(AGI.Ui.Plugins.IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            //converting an ico file to be used as the image for toolbat button
            stdole.IPictureDisp picture;
            string imageResource = "Agi.Ui.Plugins.CSharp.GfxAnalysis.STK.ico";
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Icon icon = new Icon(currentAssembly.GetManifestResourceStream(imageResource));
            picture = (stdole.IPictureDisp)IPictureDispHost.GetIPictureDispFromPicture(icon.ToBitmap());
            //Add a Toolbar Button
            ToolbarBuilder.AddButton("Sample.SampleUiPlugin.MyFirstCommand", "Open Gfx Analysis user interface", "Open my custom user interface.", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, picture);
        }

        public void OnShutdown()
        {
            m_root.OnScenarioNew -= new IAgStkObjectRootEvents_OnScenarioNewEventHandler(m_root_OnScenarioNew);
            m_pSite = null;
        }

        public void OnStartup(AGI.Ui.Plugins.IAgUiPluginSite PluginSite)
        {
            m_pSite = PluginSite;
            //Get the AgStkObjectRoot
            IAgUiApplication AgUiApp = m_pSite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRootClass;

            //adding the event so the user interface can be re-opened when a new scenario is created
            m_root.OnScenarioNew += new IAgStkObjectRootEvents_OnScenarioNewEventHandler(m_root_OnScenarioNew);
        }

        void m_root_OnScenarioNew(string Path)
        {
            if (m_WasOpen)
                OpenUserInterface();
        }

        #endregion

        #region IAgUiPluginCommandTarget Members

        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            //Controls what a command does
            if (string.Compare(CommandName, "Sample.SampleUiPlugin.MyFirstCommand", true) == 0 || string.Compare(CommandName, "Sample.SampleUiPlugin.MyFirstContextMenuCommand", true) == 0)
            {
                OpenUserInterface();
            }
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            //Enable commands
            if (string.Compare(CommandName, "Sample.SampleUiPlugin.MyFirstCommand", true) == 0 || string.Compare(CommandName, "Sample.SampleUiPlugin.MyFirstContextMenuCommand", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        #endregion

        public CustomUserInterface customUI
        {
            get { return m_customUserInterface; }
            set { m_customUserInterface = value; }
        }

        public AgStkObjectRootClass STKRoot
        {
            get { return m_root; }
        }

        public bool InterfaceWasOpen
        {
            get { return m_WasOpen; }
            set { m_WasOpen = value; }
        }

        public void OpenUserInterface()
        {
            //Open a User Interface
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
                @params.UserControlFullName = typeof(CustomUserInterface).FullName;
                @params.Caption = "Gfx Analysis Interface";
                @params.DockStyle = AgEDockStyle.eDockStyleDockedBottom;
                @params.Height = 200;
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
