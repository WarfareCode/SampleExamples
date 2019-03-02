using System;
using System.Runtime.InteropServices;
using AGI.STKObjects;
using System.Collections.Generic;

namespace SensorStreamPlugin
{
    internal class ObjectModelHelper : IDisposable
    {
        AgStkObjectRoot _root;
        Dictionary<string, string> _savedUnits;

        internal ObjectModelHelper(AgStkObjectRoot root)
        {
            _root = root;

            _savedUnits = new Dictionary<string, string>();
            _savedUnits["DateFormat"] = "";
            _savedUnits["TimeUnit"] = "";
            _savedUnits["AngleUnit"] = "";
            _savedUnits["LongitudeUnit"] = "";
            _savedUnits["LatitudeUnit"] = "";
            _savedUnits["DistanceUnit"] = "";
        }

        ~ObjectModelHelper()
        {
            Dispose(false);
        }

        internal void ExecuteSetDefaultAngleInDegrees(Action action)
        {
            SaveCurrentUnits();

            _root.UnitPreferences.SetCurrentUnit("DateFormat", "epSec");
            _root.UnitPreferences.SetCurrentUnit("TimeUnit", "sec");
            _root.UnitPreferences.SetCurrentUnit("AngleUnit", "deg");
            _root.UnitPreferences.SetCurrentUnit("LongitudeUnit", "deg");
            _root.UnitPreferences.SetCurrentUnit("LatitudeUnit", "deg");
            _root.UnitPreferences.SetCurrentUnit("DistanceUnit", "m");
            try
            {
                action();
            }
            finally
            {
                RestorePrevUnits();
            }
        }

        internal void ExecuteInInternalUnits(Action action)
        {
            SaveCurrentUnits();

            _root.UnitPreferences.SetCurrentUnit("DateFormat", "epSec");
            _root.UnitPreferences.SetCurrentUnit("TimeUnit", "sec");
            _root.UnitPreferences.SetCurrentUnit("AngleUnit", "rad");
            _root.UnitPreferences.SetCurrentUnit("LongitudeUnit", "rad");
            _root.UnitPreferences.SetCurrentUnit("LatitudeUnit", "rad");
            _root.UnitPreferences.SetCurrentUnit("DistanceUnit", "m");

            try
            {
                action();
            }
            finally
            {
                RestorePrevUnits();
            }
        }

        internal void SaveCurrentUnits()
        {
            string[] tempKeys = new string[_savedUnits.Keys.Count];
            _savedUnits.Keys.CopyTo(tempKeys, 0);
            foreach (string key in tempKeys)
            {
                _savedUnits[key] = _root.UnitPreferences.GetCurrentUnitAbbrv(key);
            }
        }

        internal void RestorePrevUnits()
        {
            foreach (KeyValuePair<string, string> pair in _savedUnits)
            {
                _root.UnitPreferences.SetCurrentUnit(pair.Key, pair.Value);
            }
        }

        #region IDisposable Members

        bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                Marshal.FinalReleaseComObject(_root);
                if (disposing)
                    GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
