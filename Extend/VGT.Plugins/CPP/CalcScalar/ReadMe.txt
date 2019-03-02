/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for building and using the 
VGT CalcScalar Plugin Samples written in C++.

1.  Install Visual Studio .NET.

2.  Install and register STK.

3.   Ensure that the Active solution platform in the Build Configuration is correctly configured. If you are targeting STK Desktop or STK Engine x86, it should be set to x86. If you are targeting STK Engine x64, it should be set to x64. Then, right click the project and choose "Build". Make sure the project builds successfully.

4.	This plugin has already been registered in the "VGT CalcScalar Plugins" Category in the
	"VGT Plugins.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the following line outside of the comment block:
	
	<Plugin DisplayName = "CPP CalcScalar Example" ProgID = "Agi.VGT.CalcScalar.Plugin.Examples.CPP.Example1"/>

	The registration file can be found in the Solution Explorer under "Solution Items". 
	When creating your own plugins, you need to register your plugin similarly, either
	in a separate xml file or added to another plugin point registration file.
	
5.	The plugin point registration file must be copied to a Plugins folder which will be
	searched by STK when it starts. STK looks in the following locations:
		- INSTALL_DIR\Plugins		
		  For example: C:\Program Files\AGI\STK 10\Plugins
		- CONFIG_DIR\Plugins
		  XP: C:\Documents and Settings\user_name\My Documents\STK 10\Config\Plugins
		  WIN7: C:\Users\user_name\Documents\STK 10\Config\Plugins
	
6.  Start STK.  Check the message viewer to see if any errors occurred when attempting to
	load the registration file.

7.  Use STK documentation for directions on how to configure a VGT CalcScalar Plugin for a 
    given satellite.

/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/