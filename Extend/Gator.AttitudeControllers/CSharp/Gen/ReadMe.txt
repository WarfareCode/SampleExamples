/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for building and using the 
Gator Attitude Control Plugin Samples written in C#.

1.  Install Visual Studio .NET.

2.  Install and register STK.

3.  This project's References folder must reference the following COM dll's
    which can be found in the bin directory of the STK install.
    
    AgAttrAutomation.dll
    AgUtPlugin.dll
    AgCrdnPlugin.dll
    AgGator.dll
    AgGatorPlugin.dll
    AgStkPlugin.dll

	If this is not done, compile errors will occur.
	
4.  This project's "Register for COM Interop" option to has already been set to "true". 
	When building your own plugins, you must also use this setting.  When the project 
	builds successfully, the assembly will be registered in the Windows Registry for 
	use through COM.

5.  In the Solution browser, right click the project and choose "Build".
	Make sure the project builds successfully.
 
6.	This plugin has already been registered in the "Attitude Controls" Category in the
	"Attitude Controls.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the following line outside of the comment block:

	<Plugin DisplayName = "CSharp Example1" ProgID = "AGI.Astrogator.Plugin.Examples.AttitudeControl.CSharp.Example1"/>
	
	The registration file can be found in the Solution Explorer under "Solution Items". 
	When creating your own plugins, you need to register your plugin similarly, either
	in a separate xml file or added to another plugin point registration file.

7.	The plugin point registration file must be copied to a Plugins folder which will be
	searched by STK when it starts. STK looks in the following locations:
		- INSTALL_DIR\Plugins		
		  For example: C:\Program Files\AGI\STK 10\Plugins
		- CONFIG_DIR\Plugins
		  XP: C:\Documents and Settings\user_name\My Documents\STK 10\Config\Plugins
		  WIN7: C:\Users\user_name\Documents\STK 10\Config\Plugins
	
8.  Start STK.  Check the message viewer to see if any errors occurred when attempting to
	load the registration file.

9.  Use STK documentation for directions on how to configure an Attitude Controller Plugin for a 
    given satellite.

/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/