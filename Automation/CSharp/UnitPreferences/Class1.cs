using System;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.Ui.Application;
using System.Reflection;
using System.Runtime.InteropServices;
namespace UnitPreferences
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
        private AgUiApplication AGI_STK;
		private AgStkObjectRoot AGI_APP;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Class1 class1 = new Class1();
			class1.Run();
		}

		public Class1()
		{
			try
			{
                AGI_STK = Marshal.GetActiveObject("STK11.Application") as AgUiApplication;
			}
			catch
			{
                Console.Write("Creating a new STK 11 instance... ");
                Guid clsID = typeof(AgUiApplicationClass).GUID;
                Type t = Type.GetTypeFromCLSID(clsID);
                AGI_STK = Activator.CreateInstance(t) as AgUiApplication;
                try
                {
                    AGI_STK.LoadPersonality("STK");
                }
                catch(System.Runtime.InteropServices.COMException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
			}

            try
            {
                AGI_APP = AGI_STK.Personality2 as AgStkObjectRoot;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
                Environment.Exit(0);
            }
		}

		~Class1()
		{
		}

		public void Run()
		{
			IAgUnitPrefsDimCollection dimCol = AGI_APP.UnitPreferences;
			foreach (IAgUnitPrefsDim dim in dimCol)
			{
				Console.WriteLine("Dimension name is {0}", dim.Name);
				Console.WriteLine("\tCurrent unit abbrv for {0} is {1}", dim.Name, dim.CurrentUnit.Abbrv);
				Console.WriteLine("\tAvailable units for {0}:", dim.Name);
				foreach(IAgUnitPrefsUnit unit in dim.AvailableUnits)
				{
					Console.WriteLine("\t\t" + unit.Abbrv);
				}
			}
			Console.WriteLine("Press Enter key to exit....");			
			Console.ReadLine();
			Console.WriteLine("Exiting ....");
			AGI_APP = null;
			AGI_STK = null;
		}
	}
}
