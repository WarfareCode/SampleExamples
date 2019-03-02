/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/

This 'empty' C++ project is a bare bones example of a C++ HPOP Force Model plugin.

To create your own plugin project, based upon this one, do the following:

(1) Copy the entire MyPlugin folder to a new folder.
(2) Rename the folder to name of your plugin. 

	Below, we'll use the term YourPlugin to refer to the name you have chosen.

(3) In the YourPlugin folder, rename:
	MyPlugin.cpp		--> YourPlugin.cpp
	MyPlugin.def		--> YourPlugin.def
	MyPlugin.idl		--> YourPlugin.idl
	MyPluginDll.cpp		--> YourPluginDll.cpp
	MyPlugin.h			--> YourPlugin.h
	MyPlugin.rgs		--> YourPlugin.rgs
	MyPluginDll.rc		--> YourPluginDll.rc
	MyPluginDll.rgs		--> YourPluginDll.rgs
	MyPlugin.vcproj		--> YourPlugin.vcproj
	
Use a text editor to perform the changes to the contents of the files. WordPad is fine, but
notepad is limited and not recommended. TextPad is terrific, as is Ultra-Edit 32, and 
probably many others as well.

(4) Associated with your plugin name (YourPlugin), with be an interface IYourPlugin (i.e.,
	the name of your plugin prepended with a capital 'I') and a C++ class CYourPlugin
	(i.e., the name of your plugin prepended with a capital 'C').
	
(5) In Resource.h, rename:
	
	MyPluginDll.rc	--> YourPlugin.rc
	IDR_MYPLUGINDLL --> IDR_YourPluginDLL
	IDR_MYPLUGIN	--> IDR_YourPlugin

(6) In YourPluginDll.rgs, rename:

	MYPLUGIN		--> YourPlugin
	MYPLUGIN.DLL	--> YourPlugin.DLL
	
(7) In YourPluginDll.rc, rename:

	MYPLUGIN.tlb			-->	YourPlugin.tlb
	MYPLUGIN_COMPANYNAME	-->	your company name
	MYPLUGIN_DESCRIPTION	--> your description
	MYPLUGIN.dll			--> YourPlugin.dll
	MyPluginDll.rgs			--> YourPluginDll.rgs
	
(8) In YourPluginDll.rc, replace the lines:

	Replace: IDR_MYPLUGINDLL REGISTRY "MyPluginDll.rgs" with : IDR_YourPluginDLL REGISTRY "YourPluginDll.rgs"
	Replace: IDR_MYPLUGIN REGISTRY "MyPlugin.rgs" with: IDR_YourPlugin REGISTRY "YourPlugin.rgs"
	
(9) In YourPluginDll.cpp, replace:

	MYPLUGIN.h			--> YourPlugin.h
	CMYPLUGINModule		--> CYourPluginModule
	LIBID_MYPLUGINLib	--> LIBID_YourPluginLib
	IDR_MYPLUGINDLL		--> IDR_YourPluginDLL
	
(10) In YourPluginDll.cpp, generate a new GUID (use Tools->Create GUID from Visual Studio .NET to make and
	 copy a new one) and replace the one listed ({BEF232A0-0514-41C7-BC6F-422E2D525B25}).
	 
(11) In YourPlugin.rgs, replace:

	MYPLUGIN	--> YourPlugin
	
(12) In YourPlugin.rgs, create a new GUID for the coclass and replace {EFBC27D8-CB24-4EAE-86CF-745A0E594A2B}. 
	 Create a new GUID for the type library and replace {02AB9710-8D05-4FE4-8387-AE9818DFE82F}. DO NOT
	 replace the implemented category GUID. You will be copying these new GUIDs to other files below.
	 
(13) In YourPlugin.idl, replace:
	
	MYPLUGIN		--> YourPlugin
	IMYPLUGIN		--> IYourPlugin
	MYPLUGINLib		--> YourPluginLib
	
(14) In YourPlugin.idl, replace the GUID for the coclass with the GUID you egenrated for the coclass in step 12.
    Replace the GUID for the type library with the GUID you created in step 12 for the type library. 
    NOTE: in the idl file, the GUIDs are termed uuid's, and the notation does not use the enclosing braces.
    
(15) In YourPlugin.idl, create a new GUID for the IYourPlugin interface and reaplce the uuid there.

(16) In YourPlugin.def, replace:

	MyPlugin.def	--> YourPlugin.def
	MYPLUGIN.DLL	--> YourPlugin.DLL
	
(17) In YourPlugin.cpp, replace:

	MYPLUGIN.h		--> YourPlugin.h
	MYPLUGIN_i.c	-->	YourPlugin_i.c
	CMYPLUGIN		--> CYourPlugin
	MYPLUGIN		--> YourPlugin
	
(17) In YourPlugin.h, replace:

	MYPLUGINDll.h		-->	YourPluginDll.h
	CMYPLUGIN			--> CYourPlugin
	CLSID_MYPLUGIN		--> CLSID_YourPlugin
	IMYPLUGIN			--> IYourPlugin
	IID_IMYPLUGIN		--> IID_IYourPlugin
	LIBID_MYPLUGINLib	-->	LIBID_YourPluginLib
	IDR_MYPLUGIN		--> IDR_YourPlugin
	MYPLUGIN			--> YourPlugin
	
(18) In YourPlugin.vcproj, replace:

	Name="MyPlugin"		--> Name="YourPlugin"
	MYPLUGIN.dll		--> YourPlugin.dll
	MYPLUGIN.lib		--> YourPlugin.lib
	MYPLUGIN.tlb		--> YourPlugin.tlb
	MYPLUGINDll.h		--> YourPluginDll.h
	MYPLUGIN_i.c		--> YourPlugin_i.c
	MYPLUGIN_p.c		--> YourPlugin_p.c
	MyPlugin.cpp		--> YourPlugin.cpp
	MyPlugin.def		--> YourPlugin.def
	MyPlugin.idl		--> YourPlugin.idl
	MyPluginDll.cpp		--> YourPluginDll.cpp
	MyPlugin.h			--> YourPlugin.h
	MyPlugin.rgs		--> YourPlugin.rgs
	MyPluginDll.rc		--> YourPluginDll.rc
	MyPluginDll.rgs		--> YourPluginDll.rgs
	
(19) In YourPlugin.vcproj, replace the 4 GUIDs by creating new ones.
	
(20) Load the project YourPlugin.vcproj into Microsoft Visual Studio.NET . It should load properly.

(21) To build, STK must be installed so that certain type libraries are registered in the Windows registry.
	
(22) Your renamed project should be all set to go.  You can build it (and it can even be used in STK and ODTK,
	doing nothing but outputting messages to the Message Viewer if debug mode is true).
	
(23) To install on another machine:
	 - Copy the entire binary folder where YourPlugin.dll was built to the new machine.
	 - Use Regsvr32.exe to register YourPlugin.dll. STK must have already
	   been installed on that machine.

	   
/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/