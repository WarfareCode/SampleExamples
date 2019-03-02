//=====================================================//
//  Copyright 2006, Analytical Graphics, Inc.          //
//=====================================================//
using System;

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace AGI.Search.Plugin.Examples.CSharp.Bisection
{
	// NOTE:  Name your custom COM Interface with the identical name as your
	// plugin class's name, and append an I to the beginning of it.
	public interface IBisectionPlugin
	{
		// NOTE:  Add your custom COM Interface Property configuration settings here.
		//        Follow the standard C# rules for exposing properties.
		string Name	{ get; set; }
		int MaxIterations	{ get; set; }
	}
}
//=====================================================//
//  Copyright 2006, Analytical Graphics, Inc.          //
//=====================================================//