using System;
using System.Runtime.InteropServices;
using AGI.STKGraphics.Plugins;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
using RectangularSensorStreamPluginProxy;
using Microsoft.Win32;
using System.Text;


namespace SensorStreamPlugin
{
	[Guid("5AEDB5C1-F126-423f-8017-A9B0D882C939")]
    [ClassInterface( ClassInterfaceType.None)]
    [ProgId("SensorStreamingPlugin.SensorStreamingPlugin")]
    public class SensorStreamPlugin : 
        IAgStkGraphicsPluginProjectionStream,
        IAgStkGraphicsPluginWithSite
    {
        public IAgStkGraphicsPluginSite Site { get; private set; }
        private IAgCrdnProvider _provider;
        ObjectModelHelper _helper;

        public SensorAttributes sensorAttributes { get; set; }

        public SensorStreamPlugin()
        {
            // Call into static proxy to get the shared sensor attributes            
            sensorAttributes = RectangularSensorStreamPluginProxy.PluginProxy.ProxySensorAttributes;
        }

        #region IAgStkGraphicsPluginProjectionStream Members

        bool IAgStkGraphicsPluginProjectionStream.OnGetFirstProjection(IAgDate Time, IAgStkGraphicsPluginProjectionStreamContext Context)
        {
            Context.NearPlane = 1.0;
            Context.FarPlane = 2000000.0;
            Context.FieldOfViewHorizontal = sensorAttributes.HorizontalHalfAngle;
            Context.FieldOfViewVertical = sensorAttributes.VerticalHalfAngle;

            // Arrays to hold position and orientation data for time Time
            Array xyz = new object[3];
            Array quat = new object[4];

            _helper.ExecuteInInternalUnits(() =>
            {
                GetSensorPositionOrientation(Time, out xyz, out quat);
            });

            Context.SetPosition(ref xyz);
            Context.SetOrientation(ref quat);

            return true;
        }

        bool IAgStkGraphicsPluginProjectionStream.OnGetNextProjection(IAgDate Time, IAgDate NextTime, IAgStkGraphicsPluginProjectionStreamContext Context)
        {
            Context.NearPlane = 1.0;
            Context.FarPlane = 2000000.0;
            Context.FieldOfViewHorizontal = sensorAttributes.HorizontalHalfAngle;
            Context.FieldOfViewVertical = sensorAttributes.VerticalHalfAngle;

            // Arrays to hold position and orientation data for time Time
            Array xyz = new object[3];
            Array quat = new object[4];

            _helper.ExecuteInInternalUnits(() =>
            {
                GetSensorPositionOrientation(Time, out xyz, out quat);
            });

            Context.SetPosition(ref xyz);
            Context.SetOrientation(ref quat);

            return true;
        }
        #endregion

        #region IAgStkGraphicsPluginWithSite Members

        void IAgStkGraphicsPluginWithSite.OnShutDown()
        {
            if (Site != null)
                Marshal.FinalReleaseComObject(Site);
        }

        void IAgStkGraphicsPluginWithSite.OnStartUp(IAgStkGraphicsPluginSite site)
        {
            this.Site = site;
            AgStkObjectRoot root = (AgStkObjectRoot)Site.StkRootObject;
            IAgStkObject sensor = root.GetObjectFromPath(sensorAttributes.Path);
            _provider = sensor.Vgt;
            _helper = new ObjectModelHelper(root);
        }
        #endregion

        /// <summary>
        /// Uses VGT to query the sensor's position and orientation at the given time
        /// </summary>
        private void GetSensorPositionOrientation(IAgDate time, out Array positionArray, out Array orientationArray)
        {
            double epSecs = Double.Parse(time.Format("epSec"));
            positionArray = new object[3];
            orientationArray = new object[4];

            if (_provider != null)
            {
                // Position
                IAgCrdnPoint sensorCenterPoint = _provider.Points["Center"];
                IAgCrdnSystem earthFixedSystem = _provider.WellKnownSystems.Earth.Fixed;
                IAgCrdnPointLocateInSystemResult locationResult = sensorCenterPoint.LocateInSystem(epSecs, earthFixedSystem);
                if (locationResult.IsValid)
                {
                    positionArray.SetValue(locationResult.Position.X, 0);
                    positionArray.SetValue(locationResult.Position.Y, 1);
                    positionArray.SetValue(locationResult.Position.Z, 2);
                }

                // Orientation
                IAgCrdnAxes sensorBodyAxes = _provider.Axes[sensorAttributes.SensorBodyAxes];
                IAgCrdnAxes earthFixedAxes = _provider.WellKnownAxes.Earth.Fixed;
                IAgCrdnAxesFindInAxesResult orientationResult = earthFixedAxes.FindInAxes(epSecs, sensorBodyAxes);
                if (orientationResult.IsValid)
                {
                    orientationArray = orientationResult.Orientation.QueryQuaternionArray();
                }
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
