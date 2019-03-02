//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//
using System;

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace AGI.Astrogator.Plugin.Examples.EngineModeling.CSharp
{
	// NOTE:  Name your custom COM Interface with the identical name as your
	// plugin class's name, and append an I to the beginning of it.
	public interface IExample1
	{
		// NOTE:  Add your custom COM Interface Property configuration settings here.
		//        Follow the standard C# rules for exposing properties.
		string Name	{ get; set; }
		double T0	{ get; set; }
		double T1	{ get; set; }
		double T2	{ get; set; }
		double Ts	{ get; set; }
		double Tc	{ get; set;	}
		double Isp	{ get; set; }	
	}
}
//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//