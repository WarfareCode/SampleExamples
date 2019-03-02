//========================================================//
//     Copyright 2005, Analytical Graphics, Inc.          //
//========================================================//
using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using AGI.Attr;
using AGI.Plugin;
using AGI.Astrogator;
using AGI.Astrogator.Plugin;
using AGI.STK.Plugin;

namespace AGI.Astrogator.Plugin.Examples.EngineModeling.CSharp
{
	/// <summary>
	/// Example1 Gator Engine Model
	/// </summary>
	// NOTE: Generate your own Guid using Microsoft's GuidGen.exe
	// If you used this plugin in STK 6, 7 you should create a new
	// copy of your plugin's source, and update it with a new GUID
	// for STK 8.  Then you will be able to make changes in the 
	// new STK 8 plugin and not affect your old STK 6,7 plugin.
	[Guid("89BD70D3-C12E-4bb7-9C85-27E2D5C2FFB5")]
	// NOTE: Create your own ProgId to match your plugin's namespace and name
	[ProgId("AGI.Astrogator.Plugin.Examples.EngineModeling.CSharp.Example1")]
	// NOTE: Specify the ClassInterfaceType.None enumeration, so the custom COM Interface 
	// you created, i.e. IExample1, is used instead of an autogenerated COM Interface.
	[ClassInterface(ClassInterfaceType.None)]
	public class Example1 :
		IExample1,
		IAgGatorPluginEngineModel,
		IAgUtPluginConfig
	{
		#region Data Members

		private IAgUtPluginSite					m_UtPluginSite	= null;
		private double							m_InitTime		= 0;
		private object							m_AttrScope		= null;
		private AgGatorPluginProvider			m_gatorPrv		= null;
		private AgGatorConfiguredCalcObject		m_argOfLat		= null;

		#endregion
		
		#region Life Cycle Methods
		/// <summary>
		/// Constructor
		/// </summary>
		public Example1()
		{
			try
			{
				Debug.WriteLine( "Entered", "Example1()");

			}
			finally
			{
				Debug.WriteLine( "Exited", "Example1()");
			}
		}

		/// <summary>
		/// Destructor
		/// </summary>
		~Example1()
		{
			try
			{
				Debug.WriteLine( "Entered", "~Example1()");
			}
			finally
			{
				Debug.WriteLine( "Exited", "~Example1()");
			}
		}

		private void Message( AgEUtLogMsgType msgType, string msg )
		{
			if( this.m_UtPluginSite != null )
			{
				this.m_UtPluginSite.Message( msgType, msg );
			}
		}
		#endregion

		#region IExample1 Interface Implementation

		private string	m_Name		= "Gator.EngMdl.Plugin.Example1";	// Plugin Significant
		private double	m_T0		= 0;
		private double	m_T1		= 0.0001;
		private double	m_T2		= 0.0000001;
		private double  m_Ts		= 0;
		private double  m_Tc		= 0;
		private double  m_Isp		= 3000;

		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		public double T0
		{
			get
			{
				return this.m_T0;
			}
			set
			{
				Debug.WriteLine( "New( "+this.GetHashCode()+" ) T0: " + value );
				this.m_T0 = value;
			}
		}

		public double T1
		{
			get
			{
				return this.m_T1;
			}
			set
			{
				this.m_T1 = value;
			}
		}

		public double T2
		{
			get
			{
				return this.m_T2;
			}
			set
			{
				this.m_T2 = value;
			}
		}
		
		public double Ts
		{
			get
			{
				return this.m_Ts;
			}
			set
			{
				this.m_Ts = value;
			}
		}

		public double Tc
		{
			get
			{
				return this.m_Tc;
			}
			set
			{
				this.m_Tc = value;
			}
		}

		public double Isp
		{
			get
			{
				return this.m_Isp;
			}
			set
			{
				this.m_Isp = value;
			}
		}	

		#endregion

		#region IAgGatorPluginEngineModel Interface Implementation
		public bool Init( IAgUtPluginSite site)
		{
			this.m_UtPluginSite = site;

			if (this.m_UtPluginSite != null)
			{
				this.m_gatorPrv = ( (IAgGatorPluginSite)(this.m_UtPluginSite) ).GatorProvider;

				if (this.m_gatorPrv !=null)
				{
					this.m_argOfLat = this.m_gatorPrv.ConfigureCalcObject("Argument_of_Latitude");

					if (this.m_argOfLat != null)
					{
						return true;
					}
				}
			}
	
			return false;
		}

		public bool PrePropagate( AgGatorPluginResultState result )
		{
			if( result != null )
			{
				int WholeDays = 0;
				double SecIntoDay = 0.0;
				result.DayCount( AgEUtTimeScale.eUtTimeScaleSTKEpochSec, ref WholeDays, ref SecIntoDay);
				this.m_InitTime = WholeDays * 86400.0 + SecIntoDay;
			}

			return true;
		}

		public bool PreNextStep( AgGatorPluginResultState state )
		{
			return true;
		}

		public bool Evaluate( AgGatorPluginResultEvalEngineModel result )
		{
			if( result != null )
			{
				double time;
				double thrust;
				double deltaT;
				double argOfLat;

				Debug.WriteLine(" Evaluate( "+this.GetHashCode()+" )");
				
				int WholeDays = 0;
				double SecIntoDay = 0;
				result.DayCount( AgEUtTimeScale.eUtTimeScaleSTKEpochSec, ref WholeDays, ref SecIntoDay);
				time = WholeDays * 86400.0 + SecIntoDay;
			
				deltaT = time - this.m_InitTime;

				argOfLat = this.m_argOfLat.Evaluate(result);
			
				thrust = T0 + ( T1 * deltaT ) + ( T2 * deltaT * deltaT ) + ( Ts * Math.Sin(argOfLat) ) + ( Tc * Math.Cos(argOfLat) );
			
				result.SetThrustAndIsp(thrust,Isp);
			}

			return true;
		}

		public void Free()
		{
		}
		#endregion

		#region IAgUtPluginConfig Interface Implementation
		public object GetPluginConfig( AgAttrBuilder builder )
		{
			try
			{
				Debug.WriteLine( "--> Entered", "GetPluginConfig()");

				if( builder != null )
				{
					if( this.m_AttrScope == null )
					{
						this.m_AttrScope = builder.NewScope();
				
						//====================
						// General Attributes
						//====================
						builder.AddStringDispatchProperty( this.m_AttrScope, "PluginName", "Human readable plugin name or alias", "Name", (int)AgEAttrAddFlags.eAddFlagReadOnly );
				
						//================
						// Thrust Attributes
						//================
						builder.AddDoubleDispatchProperty( this.m_AttrScope, "T0", "Initial Thrust", "T0", (int)AgEAttrAddFlags.eAddFlagNone );
						builder.AddDoubleDispatchProperty( this.m_AttrScope, "T1", "Linear Thrust Coefficient", "T1", (int)AgEAttrAddFlags.eAddFlagNone );
						builder.AddDoubleDispatchProperty( this.m_AttrScope, "T2", "Quadratic Thrust Coefficient", "T2", (int)AgEAttrAddFlags.eAddFlagNone );
						builder.AddDoubleDispatchProperty( this.m_AttrScope, "Ts", "Sine Thrust Coefficient", "Ts", (int)AgEAttrAddFlags.eAddFlagNone );
						builder.AddDoubleDispatchProperty( this.m_AttrScope, "Tc", "Cosine Thrust Coefficient", "Tc", (int)AgEAttrAddFlags.eAddFlagNone );

						builder.AddDoubleDispatchProperty( this.m_AttrScope, "Isp", "Specific Impulse", "Isp", (int)AgEAttrAddFlags.eAddFlagNone );
					}

					string config;
					config = builder.ToString( this, this.m_AttrScope );
					Debug.WriteLine( "\n" + config, "GetPluginConfig()" );
				}
			}
			finally
			{
				Debug.WriteLine( "<-- Exited", "GetPluginConfig()");
			}

			return this.m_AttrScope;
		}

		public void VerifyPluginConfig( AgUtPluginConfigVerifyResult result )
		{
			try
			{
				Debug.WriteLine( "Entered", "VerifyPluginConfig()");

				result.Result	= true;
				result.Message	= "Ok";
			}
			finally
			{
				Debug.WriteLine( "Exited", "VerifyPluginConfig()" );
			}
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
//========================================================//
//     Copyright 2005, Analytical Graphics, Inc.          //
//========================================================//