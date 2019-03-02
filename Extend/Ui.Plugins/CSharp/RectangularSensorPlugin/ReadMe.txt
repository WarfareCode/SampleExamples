/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

The following is a step by step procedure for building and using the 
Rectangular Sensor Plugin Sample written in C#.

1.  Install Visual Studio .NET.

2.  Install and register STK.

3.  This project's "Register for COM Interop" option to has already been set to "true". 
	When building your own plugins, you must also use this setting.  When the project 
	builds successfully, the assembly will be registered in the Windows Registry for 
	use through COM.

	If you are not running Visual Studio as Administrator, errors will occur because of this option.

4.  In the Solution browser, right click the RectangularSensorPlugin project and choose "Build".
	Make sure the project builds successfully.
 
5.	This plugin has already been registered in the "UiPlugins" Category in the
	"Ui Plugins.xml" plugin point registration file; however the registration lies
	in a comment block that will not be executed. You must edit the file, and move
	the following line outside of the comment block:

	<UiPlugin ProgID="AGI.RectangularSensorPlugin" File="C:\Program Files\AGI\STK 10\CodeSamples\Extend\Ui.Plugins\CSharp\RectangularSensorPlugin\bin\Debug\RectangularSensorPlugin.dll" DisplayName="Rectangular Sensor Plugin"/>
		Note: The file location may have to be updated based on what configuration you are building
		and where you are building the project from.
	
	You must also move the following line found under the "GfxPlugins" Category outside of the 
	comment block because it is used by the Rectangular Sensor Plugin:
	
	<Plugin ProgID="SensorStreamingPlugin.SensorStreamingPlugin" DisplayName="SensorStreamingPlugin"/>
	
	The registration file can be found in the Solution Explorer under the RectangularSensorPlugin folder. 
	When creating your own plugins, you need to register your plugin similarly, either
	in a separate xml file or added to another plugin point registration file.

6.	The plugin point registration file must be copied to a Plugins folder which will be
	searched by STK when it starts. STK looks in the following locations:
		- INSTALL_DIR\Plugins		
		  For example: C:\Program Files\AGI\STK 10\Plugins
		- CONFIG_DIR\Plugins
		  XP: C:\Documents and Settings\user_name\My Documents\STK 10\Config\Plugins
		  WIN7: C:\Users\user_name\Documents\STK 10\Config\Plugins
	
7.  Start STK.  Check the message viewer to see if any errors occurred when attempting to
	load the registration file.
	
8.	Add a satellite and attach a sensor to it.  The sensor must be setup as a rectangular sensor.

9.	Right click on the sensor in the object browser and select Sensor Plugins -> Rectangular Sensor Tool.

10.	In the Rectangular Sensors window that appears select your sensor and click the Set Projection Properties.
	Setup the different properties and click OK.
	
11. Click the Add Projection button and animate the scenario.

/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/