//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//
using System;

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace AGI.EOMFunc.Plugin.Examples.Stk.CSharp
{
	// NOTE:  Name your custom COM Interface with the identical name as your
	// plugin class's name, and append an I to the beginning of it.
	public interface IMyEOMFunc
	{
		// NOTE:  Add your custom COM Interface Property configuration settings here.
		//        Follow the standard C# rules for exposing properties.
		string	DeltaVAxes	{ get; set; }
	
	}
}
//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//