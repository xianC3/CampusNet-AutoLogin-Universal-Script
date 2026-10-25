#define MyAppName "校园网自动登录通用脚本"
#define MyAppVersion "1.1.0"

[Setup]
AppId={{B575F925-EB8D-4727-ACC3-60619A48C088}
AppName={#MyAppName}
AppVerName={#MyAppName} {#MyAppVersion}
VersionInfoVersion=1.1.0.0
VersionInfoProductVersion=1.1.0
DefaultDirName={autopf}\CampusNetUniversal
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
PrivilegesRequired=admin
OutputDir=dist
OutputBaseFilename=CampusNetUniversal-Setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
SetupIconFile=CampusNetManager\app.ico
UninstallDisplayName={#MyAppName}
UninstallDisplayIcon={app}\CampusNetUniversal.exe
MinVersion=10.0
CloseApplications=yes
RestartApplications=no

[Languages]
Name: "chinesesimplified"; MessagesFile: "ChineseSimplified.isl"

[Files]
Source: "publish\CampusNetUniversal.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "使用说明.txt"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\校园网自动登录通用脚本"; Filename: "{app}\CampusNetUniversal.exe"; WorkingDir: "{app}"
Name: "{group}\校园网10次网络诊断"; Filename: "{app}\CampusNetUniversal.exe"; Parameters: "--background --cycle-test"; WorkingDir: "{app}"
Name: "{group}\使用说明"; Filename: "{app}\使用说明.txt"
Name: "{group}\卸载校园网自动登录通用脚本"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\CampusNetUniversal.exe"; Parameters: "--install-startup-task"; Flags: runhidden waituntilterminated
Filename: "{app}\CampusNetUniversal.exe"; Description: "立即打开校园网自动登录通用脚本"; Flags: nowait postinstall skipifsilent

[UninstallRun]
Filename: "{app}\CampusNetUniversal.exe"; Parameters: "--uninstall-startup"; Flags: runhidden waituntilterminated; RunOnceId: "RemoveCampusNetStartup"
