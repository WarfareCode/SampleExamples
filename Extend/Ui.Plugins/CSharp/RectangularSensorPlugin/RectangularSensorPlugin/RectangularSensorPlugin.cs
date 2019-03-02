using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using RectangularSensorStreamPluginProxy;
using Microsoft.Win32;
using System.Text;

namespace RectangularSensorPlugin
{
	[Guid("BDD76599-01DB-4a1a-8112-4F07E4A044EE")]
    [ProgId("AGI.RectangularSensorPlugin")]
    [ClassInterface(ClassInterfaceType.None)]
    public class RectangularSensorPlugin :  AGI.Ui.Plugins.IAgUiPlugin,
                                            AGI.Ui.Plugins.IAgUiPluginCommandTarget
    {
        private AGI.Ui.Plugins.IAgUiPluginSite m_pSite;

        private AGI.STKObjects.AgStkObjectRootClass m_root;
        public AGI.STKObjects.AgStkObjectRootClass STKRoot
        {
            get { return m_root; }
        }

        private string selectedSesnor;
        public string SelectedSensor
        {
            get { return selectedSesnor; }
        }

        private Hashtable sensorHashTable = new Hashtable();
        public Hashtable SensorHashtable
        {
            get { return sensorHashTable; }
        }

        #region IAgUiPlugin Members

        public void OnDisplayConfigurationPage(AGI.Ui.Plugins.IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            // No configuration page is necessary for this plugin.
        }

        public void OnDisplayContextMenu(AGI.Ui.Plugins.IAgUiPluginMenuBuilder MenuBuilder)
        {
            if (m_pSite.Selection.Count == 1)
            {
                IAgStkObject selectedObject = m_root.GetObjectFromPath(m_pSite.Selection[0].Path);
                // Only show this menu option when a rectangular sensor or the scenario is selected
                if ((selectedObject.ClassType == AgESTKObjectType.eSensor && ((IAgSensor)selectedObject).PatternType == AgESnPattern.eSnRectangular)
                    || selectedObject.ClassType == AgESTKObjectType.eScenario)
                {
                    MenuBuilder.AddMenuItem("AGI.RectangularSensorPlugin.Open", "Rectangular Sensor Tool", "Allows the configuration of a ProjectionStream for the sensor.", null);
                }
            }
        }

        public void OnInitializeToolbar(AGI.Ui.Plugins.IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            // No toolbar is used for this plugin.
        }

        public void OnShutdown()
        {
            m_pSite = null;
            Marshal.FinalReleaseComObject(m_root);
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
            if (string.Compare(CommandName, "AGI.RectangularSensorPlugin.Open", true) == 0)
            {
                OpenUserInterface();
            }
        }

        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            if (m_pSite.Selection.Count == 1)
            {
                IAgStkObject selectedObject = m_root.GetObjectFromPath(m_pSite.Selection[0].Path);
                // Only show this menu option when a rectangular sensors or the scenario is selected
                if ((selectedObject.ClassName.Equals("Sensor") && ((IAgSensor)selectedObject).PatternType == AgESnPattern.eSnRectangular)
                    || selectedObject.ClassName.Equals("Scenario"))
                {
                    if (string.Compare(CommandName, "AGI.RectangularSensorPlugin.Open", true) == 0)
                    {
                        return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
                    }
                }
            }

            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        #endregion

        #region User Control Window Open Methods and Properties
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
                @params.UserControlFullName = typeof(RectangularSensorControl).FullName;
                @params.Caption = "Rectangular Sensors";
                @params.DockStyle = AgEDockStyle.eDockStyleDockedBottom;
                @params.Height = 215;
                object obj = windows.CreateNetToolWindowParam(this, @params);
            }
        }

        public void OpenProjectionProperties(string sensorName)
        {
            AGI.Ui.Plugins.IAgUiPluginWindowSite windows = m_pSite as AGI.Ui.Plugins.IAgUiPluginWindowSite;

            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                // Set the senor whose properties window we are launching
                selectedSesnor = sensorName;

                // Open the window
                AGI.Ui.Plugins.IAgUiPluginWindowCreateParameters @params = windows.CreateParameters();
                @params.AllowMultiple = false;
                @params.AssemblyPath = this.GetType().Assembly.Location;
                @params.UserControlFullName = typeof(ProjectionProperties).FullName;
                @params.Caption = sensorName + " Projection Properties";
                @params.DockStyle = AgEDockStyle.eDockStyleFloating;
                @params.X = 100;
                @params.Y = 100;
                @params.Width = 400 + @params.X;
                @params.Height = 700 + @params.Y;
                object obj = windows.CreateNetToolWindowParam(this, @params);
            }
        }
        #endregion

        #region Sensor Attribute Notification

        public delegate void OnSensorAttributesChangedEventHandler(object sender, EventArgs e);

        /// <summary>
        /// The event is raised whenever the sensor attributes have been changed.
        /// </summary>
        public event OnSensorAttributesChangedEventHandler OnSensorAttributesChanged;

        public void RaiseSensorAttributeChanged(SensorAttributes attributes)
        {
            if (OnSensorAttributesChanged != null)
                OnSensorAttributesChanged(attributes, new EventArgs());
        }

        #endregion

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
