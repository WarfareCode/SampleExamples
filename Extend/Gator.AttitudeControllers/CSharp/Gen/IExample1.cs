//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//
using System;

// NOTE: Indicate that your Interface for your plugin is within
// the same namespace as your plugin.
namespace AGI.Astrogator.Plugin.Examples.AttitudeControl.CSharp
{
	// NOTE:  Name your custom COM Interface with the identical name as your
	// plugin class's name, and append an I to the beginning of it.
	public interface IExample1
	{
		// NOTE:  Add your custom COM Interface Property configuration settings here.
		//        Follow the standard C# rules for exposing properties.
		string Name	{ get; set; }
		double y0	{ get; set; }
		double y1	{ get; set; }
		double y2	{ get; set; }
		double ys	{ get; set; }
		double yc	{ get; set; }
		double p0	{ get; set; }
		double p1	{ get; set; }
		double p2	{ get; set; }
		double ps	{ get; set;	}
		double pc	{ get; set;	}
	}
}
//=====================================================//
//  Copyright 2005, Analytical Graphics, Inc.          //
//=====================================================//