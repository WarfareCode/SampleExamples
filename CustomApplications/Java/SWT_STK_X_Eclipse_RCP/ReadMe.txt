Note1: You must have the eclipse SDK and eclipse RCP software installed to use this sample.
Note2: Your Java architecture, Eclipse architecture, and STK architecture must all be the same for this sample to work.

1. This sample was last tested with the following technology versions:

   - Eclipse 4.3.2
   - JRE 1.6.0_45
   - STK 11.2

2. Copy the jar files (you don't need to copy any jar file with swing in the name) from your STK install bin directory to this samples lib directory.

3. Refresh the project

4. Build the project ( eclipse is set to automatically build the project by default upon java file save )

5. Find the CustomApp_SWT_STK_X_Eclipse_RCP.product file within the CustomApp_SWT_STK_X_Eclipse_RCP project and double click on it.

6. In the "CustomApp_SWT_STK_X_Eclipse_RCP.product" tab page locate the Launching tab at the bottom and click it.

7. Locate the "Launching Arguments" section, select the appropriate platform (win32 for windows).

8. In the VM Arguments text area, make sure the application runtime configuration passes the following arguments to the VM similar to below:

-Djava.library.path=<STK INSTALL DIR>\\bin

NOTE: paths with spaces may not work.  Copy JNI dll's from the install that conforms to your bit architecture to a non-space path for the configuration if you run into issues with loading the JNI dlls.
	
9. In the "CustomApp_SWT_STK_X_Eclipse_RCP.product" tab page locate the "Dependencies" tab at the bottom and click it.

10. Click the "Add Required Plug-ins" button on the right.  Remove any plug-ins listed that have a red X icon on them. This will happen depending on whether you are building for x64 or x86.

11. In the "CustomApp_SWT_STK_X_Eclipse_RCP.product" tab page locate the Overview tab at the bottom and click it.

12. Locate the "Testing" section.

13. In the "Testing" section click the "Synchronize" link.

14. In the "Testing" section click the "Launch an Eclipse Application".

15. When the application starts, use the application's menu bar to interact with the STK Engine functionality.

16. Close the application.

17. In the "CustomApp_SWT_STK_X_Eclipse_RCP.product" tab page locate the "Exporting" section.

18. In the "Exporting" section click the "Eclipse product export wizard" link.

19. Enter a destination directory and you can change the Root directory if you want.

Note: If you are working on linux you must uncheck "Generate p2 repository" in the export options section.

20. Click finish

21. Navigate to the deploy directory then to the root directory, find the CustomApp_SWT_STK_X_Eclipse_RCP executable and run it.

========
NOTE 1:
========
This samples that use this project use the Eclipse SWT libraries governed under the EPL.
They use the Eclipse SWT libraries to demonstrate the ability to host STK/STK Engine
within an SWT based application.  For license information on Eclipse SWT EPL refer
to the "Agreements" directory within the STK or STK Engine install.
