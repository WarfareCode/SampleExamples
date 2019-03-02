using System;
using System.Collections.Generic;
using System.Text;
using AGI.STKObjects;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using AGI.Ui.Application;

namespace ConnectToSpecificSTKInstance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter STK pid (process ID):");
            string pid = Console.ReadLine(); 

            AgUiApplication app;
            if (TryGetStkInstance(string.Format("!STK.Application:{0}", pid), out app))
            {
                //Get the object root for the specific instance of STK
                AgStkObjectRoot agStkObjectRoot = (AgStkObjectRoot)app.GetType().InvokeMember("Personality2",
                                                System.Reflection.BindingFlags.GetProperty, null, app, null);
                agStkObjectRoot.NewScenario("NewScenarioFromAutomation");
            }
            else
            {
                Console.WriteLine("Failed to get STK instance!");
            }
        }
        [DllImport("ole32.dll")]
        private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        [DllImport("ole32.dll")]
        private static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);

        public static bool TryGetStkInstance(string moniker, out AgUiApplication stkUIApp)
        {
            IRunningObjectTable runningObjectTable = null;
            IEnumMoniker enumMoniker = null;
            IntPtr pfetched = IntPtr.Zero;
            bool result = false;
            stkUIApp = null;
            try
            {
                IMoniker[] monikers = new IMoniker[1];

                if ((GetRunningObjectTable(0, out runningObjectTable) != 0) || (runningObjectTable == null))
                {
                    return false;
                }

                runningObjectTable.EnumRunning(out enumMoniker);
                enumMoniker.Reset();
                while (enumMoniker.Next(1, monikers, pfetched) == 0)
                {
                    string instanceName;
                    IBindCtx binCtx;
                    CreateBindCtx(0, out binCtx);
                    monikers[0].GetDisplayName(binCtx, null, out instanceName);
                    Marshal.ReleaseComObject(binCtx);
                    if (string.Compare(instanceName, moniker) == 0) //lookup by item moniker
                    {
                        Object obj;
                        runningObjectTable.GetObject(monikers[0], out obj);
                        stkUIApp = obj as AgUiApplication;
                        if (stkUIApp != null)
                            result = true;
                    }
                }
            }
            finally
            {
                if (runningObjectTable != null)
                {
                    Marshal.ReleaseComObject(runningObjectTable);
                }

                if (enumMoniker != null)
                {
                    Marshal.ReleaseComObject(enumMoniker);
                }
            }
            return result;
        }
    }
}
