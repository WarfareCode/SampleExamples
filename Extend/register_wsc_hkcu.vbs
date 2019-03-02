'--------------------------------------------------------------------------
' This script is used to register a Windows Script Component (*.wsc) file on systems where the
' user does not have administrative rights or the otherwise doesn't have permissions such that
' the standard use of regsrvr32.exe will fail.
'
' The script will create the appropriate entries into the HKEY_CURRENT_USER section of the registry
' instead of HKEY_LOCAL_MACHINE. This will allow the WSC to be used by that user. Other users on
' the machine will have to run this script for themselves to get the appropriate registry entries
' created for their user ID.  
'
' To run the script, open up a windows command prompt.
'
' Usage: cscript register_wsc_hkcu.vbs filename.wsc
'
' where filename.wsc is the full path to your windows script component. If
' the filename contains spaces you must enclose it in quotes "filename.wsc".
'--------------------------------------------------------------------------

option explicit

dim wscfilename, cmdlineargs, xmlDoc, myError, currNode, guid, viprogid
dim version, name, progid, wscfile, clsid, WshProcEnv, process_architecture
dim windir, sysroot, inprocserver

Set cmdlineargs = WScript.Arguments

wscfilename = cmdlineargs.Item(0)

set xmlDoc = CreateObject("Msxml2.DOMDocument.6.0")
xmlDoc.async = false
xmlDoc.load(wscfilename)

if (xmlDoc.parseError.errorcode <>  0) then

	set myError = xmlDoc.parseError
	wscript.echo("Error parsing file " & wscfilename & ". " & myError.reason)
	
else

	'--------------------------------------------------------------------------
	' Extract the necessary information from the header of the WSC file so that
	' we can register the component properly.
	'--------------------------------------------------------------------------
	
	xmlDoc.setProperty "SelectionLanguage", "XPath"
	
	set currNode = xmlDoc.selectSingleNode("//component/registration")
	
	guid     = currNode.getAttribute("classid")
	viprogid = currNode.getAttribute("progid")
	version  = currNode.getAttribute("version")
	name     = currNode.getAttribute("description")

	progid   = viprogid & "." & version
	wscfile  = "file:///" & wscfilename

	'--------------------------------------------------------------------------
	' Figure out if we have a 64-bit OS or not. It changes where we we need to 
	' put things in the registry. All WSC files are assumed to be 32-bit 
	' components since our applications using them are 32-bit.
	'--------------------------------------------------------------------------
	
	Dim WshShell
	Set WshShell = WScript.CreateObject("WScript.Shell")

    Set WshProcEnv = WshShell.Environment("Process")
    process_architecture = WshProcEnv("PROCESSOR_ARCHITECTURE")
	sysroot  			 = WshProcEnv("SYSTEMROOT")
		
    if process_architecture = "x86" then
	
		'----------------------------------------------------------------------
		' 32-bit registry info
		'----------------------------------------------------------------------
        
		clsid = "HKCU\Software\Classes\CLSID\"
		inprocserver = sysroot & "\system32\scrobj.dll"
		
    else
	
		'----------------------------------------------------------------------
		' 64-bit registry info
		'----------------------------------------------------------------------

        clsid = "HKCU\Software\Classes\Wow6432Node\CLSID\"
		inprocserver = sysroot & "\SysWow64\scrobj.dll"		
		
    end if
	
	'--------------------------------------------------------------------------
	' Update registry entries. 
	'--------------------------------------------------------------------------
	
	WshShell.RegWrite clsid & guid & "\", 							   name,		"REG_SZ"
	WshShell.RegWrite clsid & guid & "\VersionIndependentProgID\", 	   viprogid,	"REG_SZ"
	WshShell.RegWrite clsid & guid & "\ScriptletURL\", 				   wscfile, 	"REG_SZ"
	WshShell.RegWrite clsid & guid & "\ProgID\", 					   progid, 		"REG_SZ"
	WshShell.RegWrite clsid & guid & "\InprocServer32\", 			   inprocserver,"REG_SZ"
	WshShell.RegWrite clsid & guid & "\InprocServer32\ThreadingModel", "Apartment",	"REG_SZ"

	WshShell.RegWrite "HKCU\SOFTWARE\Classes\" & progid & "\", 			"",			"REG_SZ"
	WshShell.RegWrite "HKCU\SOFTWARE\Classes\" & progid & "\CLSID\", 	guid, 		"REG_SZ"
	WshShell.RegWrite "HKCU\SOFTWARE\Classes\" & progid & "\", 			name, 		"REG_SZ"
	WshShell.RegWrite "HKCU\SOFTWARE\Classes\" & viprogid & "\CLSID\", 	guid, 		"REG_SZ"
	WshShell.RegWrite "HKCU\SOFTWARE\Classes\" & viprogid & "\", 		name, 		"REG_SZ"
	
	wscript.echo wscfilename & " registered for current user."

end if


