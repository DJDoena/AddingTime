[Setup]
AppName=AddingTime
AppId=AddingTime
AppVerName=AddingTime 4.0.0.2
AppCopyright=Copyright © Doena Soft. 2008 - 2016
AppPublisher=Doena Soft.
AppPublisherURL=http://doena-journal.net/en/dvd-profiler-tools/
DefaultDirName={pf32}\Doena Soft.\AddingTime
DefaultGroupName=AddingTime
DirExistsWarning=No
SourceDir=..\AddingTime\bin\x86\AddingTime
Compression=zip/9
AppMutex=AddingTime
OutputBaseFilename=AddingTimeSetup
OutputDir=..\..\..\..\AddingTimeSetup\Setup\AddingTime
MinVersion=0,5.1
PrivilegesRequired=admin
WizardImageFile=compiler:wizmodernimage-is.bmp
WizardSmallImageFile=compiler:wizmodernsmallimage-is.bmp
DisableReadyPage=yes
ShowLanguageDialog=no
VersionInfoCompany=Doena Soft.
VersionInfoCopyright=2008 - 2016
VersionInfoDescription=AddingTime Setup
VersionInfoVersion=4.0.0.2
UninstallDisplayIcon={app}\djdsoft.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Messages]
WinVersionTooLowError=This program requires Windows XP or above to be installed.%n%nWindows 9x, NT and 2000 are not supported.

[Types]
Name: "full"; Description: "Full installation"

[Files]
Source: "djdsoft.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "DVDProfilerHelper.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DVDProfilerHelper.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "AddingTime.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "AddingTime.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "AddingTimeLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "AddingTimeLib.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "BDInfoLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "BDInfoLib.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "DvdNavigatorCrm.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DvdNavigatorCrm.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "Xceed.Wpf.Toolkit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "AbstractionLayer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "AbstractionLayer.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "ToolBox.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "ToolBox.pdb"; DestDir: "{app}"; Flags: ignoreversion

Source: "de\DVDProfilerHelper.resources.dll"; DestDir: "{app}\de"; Flags: ignoreversion

Source: "Readme\readme.html"; DestDir: "{app}\Readme"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\AddingTime"; Filename: "{app}\AddingTime.exe"; WorkingDir: "{app}"; IconFilename: "{app}\djdsoft.ico"
Name: "{userdesktop}\AddingTime"; Filename: "{app}\AddingTime.exe"; WorkingDir: "{app}"; IconFilename: "{app}\djdsoft.ico"

[Run]

;[UninstallDelete]

[UninstallRun]

[Registry]

[Code]
function IsDotNET35Detected(): boolean;
// Function to detect dotNet framework version 2.0
// Returns true if it is available, false it's not.
var
dotNetStatus: boolean;
begin
dotNetStatus := RegKeyExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5');
Result := dotNetStatus;
end;

function InitializeSetup(): Boolean;
// Called at the beginning of the setup package.
begin

if not IsDotNET35Detected then
begin
MsgBox( 'The Microsoft .NET Framework version 3.5 is not installed. Please install it and try again.', mbInformation, MB_OK );
Result := false;
end
else
Result := true;
end;