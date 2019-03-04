# script to look for every *.vdf file in the folder $extractFromFolder
# and extract them to individually named Folders
Clear

# kind of obvious, but change this to whatever folder you throw the VDFs into
$extractFromFolder = "C:\VDF"

# need this later when cleaning up the COM objects so we don't leave zombie processes hanging around
$marshall = [System.Runtime.InteropServices.Marshal]

# spin up a running instance of STK-X v11, used to unpack the *.vdf's
$stkXApp = new-object -comobject "STKX11.Application"

# if using Desktop STK (not STK-X) uncomment the three lines below and comment the above line
#$stkXApp = new-object -comobject "STK11.Application"
#$stkApp.visible = $true
#$root=$stkApp.Personality2

# use windows Powershell "magic" to find all files with *.vdf extension
Get-ChildItem $extractFromFolder -Filter *.vdf | 
Foreach-Object {
   $folderName = $_.DirectoryName + "\" + $_.BaseName

   #$_.FullName
   # check if folder with same base name of vdf exists, if not, create it
   if (!(Test-Path -PathType Container -Path $folderName)) {
        New-Item -ItemType Directory -Force -Path $folderName
   }
   # command to use Connect to extract a vdf into a particular path
   $cmdString = 'Viewer / ExtractViewerDataFile "' + $_.FullName + '" "' + $folderName + '\"' 
   $response = $stkXApp.ExecuteCommand($cmdString)
   # no error checking, if this call fails, ignore the response, then move on
}


$stkXApp.Terminate()
# powershell cleanup of a COM object variable
$marshall::ReleaseComObject($stkXApp)
# force a garbage collection, cleaning up released variable
[gc]::Collect()
[gc]::WaitForPendingFinalizers()
# remove named variable from Powershell context.
Remove-Variable -Name stkXApp
# we're clean, shouldn't be any zombie AgUiApplication.exe processes, finished

pause
# end of script