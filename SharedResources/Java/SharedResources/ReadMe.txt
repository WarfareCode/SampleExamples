========
NOTE 1:
========
This Eclipse project is not a completed application.  It contains code components that are reused 
by several of the other Eclipse projects.

========
NOTE 2:
========
To compile these sample Eclipse projects, you must create several Path Variables pointing to the 
correct location of external files.  To create these variables, go to Window - Preferences, then 
General - Workspace - Linked Resources.  For each variable listed below, click New, enter the 
name, then click File or Folder as indicated below, and find the correct location described below. 

---------------------------
| Path Variable |  Type   |
---------------------------
| JavaDevKit    |  Folder |
| JavaFXJar     |  File   |
| SwtJar        |  File   |
---------------------------

JavaDevKit

This folder variable should be set to the bin folder in your STK installation directory.
The default directory for Windows is: 
C:\Program Files (x86)\AGI\STK 11\bin.

JavaFXJar

This file variable should be set to the location of the jfxrt.jar file from your Java 1.7 installation 
directory.  The default directory for Windows is: 
C:\Program Files (x86)\Java\<Java Version>\jre\lib\jfxrt.jar

SwtJar

This file variable should be set to the location of the correct SWT runtime jar for your platform.
These jars are located in your STK installation directory, in the 
CodeSamples\SharedResources\Java\SharedResources\ThirdParty\eclipse4.2\lib subfolder. 
Select the jar file that matches your operating system and STK architecture.

----------------------------------------------------------------------------
| Platform        | Jar                                                    |
----------------------------------------------------------------------------
| Windows 32-bit  | org.eclipse.swt.win32.win32.x86_3.100.0.v4233d.jar     |
| Windows 64-bit  | org.eclipse.swt.win32.win32.x86_64_3.100.0.v4233d.jar  |
| Linux 32-bit    | org.eclipse.swt.gtk.linux.x86_3.100.0.v4233d.jar       |
| Linux 64-bit    | org.eclipse.swt.gtk.linux.x86_64_3.100.0.v4233d.jar    |
----------------------------------------------------------------------------
