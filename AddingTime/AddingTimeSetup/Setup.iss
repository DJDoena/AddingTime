[Setup]
AppName=AddingTime
AppId=AddingTime
AppVerName=AddingTime 4.0.0.7
AppCopyright=Copyright � Doena Soft. 2008 - 2022
AppPublisher=Doena Soft.
AppPublisherURL=http://doena-journal.net/en/dvd-profiler-tools/
DefaultDirName={commonpf32}\Doena Soft.\AddingTime
DefaultGroupName=AddingTime
DirExistsWarning=No
SourceDir=..\AddingTime\bin\x86\AddingTime
Compression=zip/9
AppMutex=AddingTime
OutputBaseFilename=AddingTimeSetup
OutputDir=..\..\..\..\AddingTimeSetup\Setup\AddingTime
MinVersion=0,6.1sp1
PrivilegesRequired=admin
WizardStyle=modern
DisableReadyPage=yes
ShowLanguageDialog=no
VersionInfoCompany=Doena Soft.
VersionInfoCopyright=2008 - 2022
VersionInfoDescription=AddingTime Setup
VersionInfoVersion=4.0.0.7
UninstallDisplayIcon={app}\djdsoft.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Messages]
WinVersionTooLowError=This program requires Windows XP or above to be installed.%n%nWindows 9x, NT and 2000 are not supported.

[Types]
Name: "full"; Description: "Full installation"

[Files]
Source: "djdsoft.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.DVDProfilerHelper.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "AddingTime.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "AddingTime.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.AddingTime.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.AddingTime.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.BDInfo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.BDInfo.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.DvdNavigator.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.DvdNavigator.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "Xceed.Wpf.Toolkit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.AbstractionLayer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.ToolBox.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "de\DoenaSoft.DVDProfilerHelper.resources.dll"; DestDir: "{app}\de"; Flags: ignoreversion

Source: "Readme\readme.html"; DestDir: "{app}\Readme"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\AddingTime"; Filename: "{app}\AddingTime.exe"; WorkingDir: "{app}"; IconFilename: "{app}\djdsoft.ico"
Name: "{commondesktop}\AddingTime"; Filename: "{app}\AddingTime.exe"; WorkingDir: "{app}"; IconFilename: "{app}\djdsoft.ico"

[Run]

;[UninstallDelete]

[UninstallRun]

[Registry]

[Code]
function IsDotNET4Detected(): boolean;
// Function to detect dotNet framework version 4
// Returns true if it is available, false it's not.
var
dotNetStatus: boolean;
begin
dotNetStatus := RegKeyExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4');
Result := dotNetStatus;
end;

function InitializeSetup(): Boolean;
// Called at the beginning of the setup package.
begin

if not IsDotNET4Detected then
begin
MsgBox( 'The Microsoft .NET Framework version 4 is not installed. Please install it and try again.', mbInformation, MB_OK );
Result := false;
end
else
Result := true;
end;