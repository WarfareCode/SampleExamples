//=====================================================//
//  Copyright 2006-2007, Analytical Graphics, Inc.     //
//=====================================================//
using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using AGI.Attr;
using AGI.Plugin;
using AGI.Access.Constraint.Plugin;
using AGI.STK.Plugin;
using AGI.STKObjects;
using AGI.STKUtil;

namespace AGI.Access.Constraint.Plugin.Examples.CSharp.RangeExample
{
	/// <summary>
	/// RangeExample uses the (LightPathRange) range between objects
	/// as its metric. Of course, STK already provides Range as an Access
	/// Constraint. This example demonstrates registration rather than
	/// computational complexity.
	/// 
	/// This constraint is registered for:
	///		Aircraft to Facility/GroundVehicle/Satellite
	///		Facility to Aircraft/Satellite
	///		GroundVehicle to Aircraft/Satellite
	///		Satellite to Aircraft/Facility/GroundVehicle
	///		
	/// </summary>
	// NOTE: Generate your own Guid using Microsoft's GuidGen.exe
	[Guid("2D73A444-CA85-4b63-88F1-3187E6253DC8")]
	// NOTE: Create your own ProgId to match your plugin's namespace and name
	[ProgId("AGI.Access.Constraint.Plugin.Examples.CSharp.RangeExample")]
	// NOTE: Specify the ClassInterfaceType.None enumeration, so the custom COM Interface 
	// you created is used instead of an autogenerated COM Interface.
	[ClassInterface(ClassInterfaceType.None)]
	public class RangeExample : IAgAccessConstraintPlugin
	{
		#region Data Members
		private string				m_DisplayName = "CSharp.RangeExample";
		private AgStkPluginSite		m_Site = null;
		private AgStkObjectRoot		m_StkRootObject = null;
		#endregion
		
		public RangeExample()
		{
			Debug.WriteLine(  m_DisplayName+".RangeExample()", "Entered:" );
			Debug.WriteLine(  m_DisplayName+".RangeExample()", "Exited:" );
		}

		private void Message(AGI.Plugin.AgEUtLogMsgType severity, string msg)
		{
			if(m_Site != null)
			{
				m_Site.Message( severity, msg );
			}
		}
		
		#region IAgAccessConstraintPlugin implementation
		public string DisplayName
		{
			get
			{
				return m_DisplayName;
			}
		}

		public void Register( AgAccessConstraintPluginResultRegister Result )
		{
			Result.BaseObjectType = AgEAccessConstraintObjectType.eAircraft;
			Result.BaseDependency = (int)AgEAccessConstraintDependencyFlags.eDependencyRelativePosVel;
			Result.Dimension = "Distance";	
			Result.MinValue = 0.0;
		
			Result.TargetDependency = (int)AgEAccessConstraintDependencyFlags.eDependencyRelativePosVel;
			Result.AddTarget(AgEAccessConstraintObjectType.eFacility);
			Result.AddTarget(AgEAccessConstraintObjectType.eGroundVehicle);
			Result.AddTarget(AgEAccessConstraintObjectType.eSatellite);		
			Result.Register();

			Result.Message(AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, 
				m_DisplayName+": Register(Aircraft to Facility/GroundVehicle/Satellite)");
		
			Result.BaseObjectType = AgEAccessConstraintObjectType.eFacility;
			Result.ClearTargets();
			Result.AddTarget(AgEAccessConstraintObjectType.eAircraft);
			Result.AddTarget(AgEAccessConstraintObjectType.eSatellite);	
			Result.Register();

			Result.Message(AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, 
				m_DisplayName+": Register(Facility to Aircraft/Satellite)");

			Result.BaseObjectType = AgEAccessConstraintObjectType.eGroundVehicle;
			Result.Register();

			Result.Message(AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, 
				m_DisplayName+": Register(GroundVehicle to Aircraft/Satellite)");

			Result.BaseObjectType = AgEAccessConstraintObjectType.eSatellite;
			Result.ClearTargets();
			Result.AddTarget(AgEAccessConstraintObjectType.eAircraft);
			Result.AddTarget(AgEAccessConstraintObjectType.eFacility);
			Result.AddTarget(AgEAccessConstraintObjectType.eGroundVehicle);	
			Result.Register();

			Result.Message(AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, 
				m_DisplayName+": Register(Satellite to Aircraft/Facility/GroundVehicle)");
		}

		public bool Init( IAgUtPluginSite site )
		{
			Debug.WriteLine(  m_DisplayName+".Init()", "Entered:" );

			m_Site = (AgStkPluginSite)site;
			
			Message( AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, m_DisplayName+": Init()" );

			// Demonstrate getting ObjectModel handle

			if(m_Site != null)
			{
				//----------------------------------------------------
				// Get a pointer to the STK Object Model root object
				//----------------------------------------------------
				
				m_StkRootObject = (AgStkObjectRoot)m_Site.StkRootObject;
			}

			Debug.WriteLine(  m_DisplayName+".Init()", "Exited:" );

			return true;
		}

		public bool PreCompute( AgAccessConstraintPluginResultPreCompute Result )
		{
			Debug.WriteLine(  m_DisplayName+".PreCompute()", "Entered:" );

			Message( AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, m_DisplayName+": PreCompute()" );

			// Demonstrate using ObjectModel handle

			if(m_StkRootObject != null)
			{
				IAgStkObject scenObj = m_StkRootObject.CurrentScenario;

				if(scenObj != null)
				{
					string currentScenario = scenObj.InstanceName;
					
					Message(AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, 
						"Current Scenario is "+ currentScenario);
				}
			}

			Debug.WriteLine(  m_DisplayName+".PreCompute()", "Exited:" );

			return true;
		}

		public bool Evaluate( 
			AgAccessConstraintPluginResultEval Result, 
			AgAccessConstraintPluginObjectData fromObject, 
			AgAccessConstraintPluginObjectData toObject )
		{
			if(Result != null)
			{
				Result.Value = Result.LightPathRange;
			}

			return true;
		}
		
		public bool PostCompute(AgAccessConstraintPluginResultPostCompute Result)
		{
			Debug.WriteLine(  m_DisplayName+".PostCompute()", "Entered:" );
		
			Message( AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, m_DisplayName+": PostCompute()" );

			Debug.WriteLine(  m_DisplayName+".PostCompute()", "Exited:" );
			
			return true;
		}

		public void Free()
		{
			Debug.WriteLine(  m_DisplayName+".Free()", "Entered:" );

			Message( AGI.Plugin.AgEUtLogMsgType.eUtLogMsgInfo, m_DisplayName+": Free()" );

			Debug.WriteLine(  m_DisplayName+".Free()", "Exited:" );

			m_Site = null;
			m_StkRootObject = null;
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
//=====================================================//
//  Copyright 2006-2007, Analytical Graphics, Inc.     //
//=====================================================//