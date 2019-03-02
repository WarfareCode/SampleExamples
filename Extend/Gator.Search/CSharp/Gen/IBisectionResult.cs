//=====================================================//
//  Copyright 2006, Analytical Graphics, Inc.          //
//=====================================================//
using System;

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace AGI.SearchResult.Plugin.Examples.CSharp.BisectionResult
{
	// NOTE:  Name your custom COM Interface with the identical name as your
	// plugin class's name, and append an I to the beginning of it.
	public interface IBisectionResult
	{
		// NOTE:  Add your custom COM Interface Property configuration settings here.
		//        Follow the standard C# rules for exposing properties.
		double DesiredValue { get; set; }
		double Tolerance { get; set; }
		bool IsActive { get; set; }
	}
}
//=====================================================//
//  Copyright 2006, Analytical Graphics, Inc.          //
//=====================================================//