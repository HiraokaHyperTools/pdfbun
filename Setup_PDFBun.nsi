; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "PDFBun"
!define COM "HIRAOKA HYPERS TOOLS, Inc."
!define VER "0.7"

!searchreplace APV ${VER} "." "_"

; PROGID
!define PDFBun "HHT.PDFBun"
!define TIFBun "HHT.TIFBun"

; bin\DEBUG

; The name of the installer
Name "${APP} ${VER}"

; The file to write
OutFile "Setup_${APP}_${APV}.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel user

!define DOTNET_VERSION "2.0"

!include "DotNET.nsh"
!include "LogicLib.nsh"

;--------------------------------

; Pages

Page license
Page directory
Page components
Page instfiles

LicenseData GNUGPL2.rtf

;--------------------------------

!ifdef SHCNE_ASSOCCHANGED
!undef SHCNE_ASSOCCHANGED
!endif
!define SHCNE_ASSOCCHANGED 0x08000000
!ifdef SHCNF_FLUSH
!undef SHCNF_FLUSH
!endif
!define SHCNF_FLUSH        0x1000

!macro UPDATEFILEASSOC
; Using the system.dll plugin to call the SHChangeNotify Win32 API function so we
; can update the shell.
  System::Call "shell32::SHChangeNotify(i,i,i,i) (${SHCNE_ASSOCCHANGED}, ${SHCNF_FLUSH}, 0, 0)"
!macroend

;--------------------------------

Section ""
  !insertmacro CheckDotNET ${DOTNET_VERSION}
SectionEnd

; The stuff to install
Section "PDF分割ソフト" ;No components page, name is not important
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  ; Put file there
  File "PDFBun\1.ico"
  File /r /x "*.vshost.exe" "PDFBun\bin\DEBUG\*.*"
  
  WriteRegStr HKCU "Software\Classes\.pdf\OpenWithProgIDs" "${PDFBun}" ""
  WriteRegStr HKCU "Software\Classes\.tif\OpenWithProgIDs" "${PDFBun}" ""
  WriteRegStr HKCU "Software\Classes\.tiff\OpenWithProgIDs" "${PDFBun}" ""

  WriteRegStr HKCU "Software\Classes\${PDFBun}" "" "PDF分割"
  WriteRegStr HKCU "Software\Classes\${PDFBun}\DefaultIcon" "" "$OUTDIR\1.ico"
  WriteRegStr HKCU "Software\Classes\${PDFBun}\shell\open\command" "" '"$OUTDIR\PDFBun.exe" "%1"'

  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.pdf\OpenWithProgids" "${PDFBun}" ""
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.tif\OpenWithProgids" "${PDFBun}" ""
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.tiff\OpenWithProgids" "${PDFBun}" ""
SectionEnd ; end the section

Section "TIF分割ソフト"
  ; Set output path to the installation directory.
  SetOutPath "$INSTDIR\TIFBun"

  ; Put file there
  File "TIFBun\1.ico"
  File /r /x "*.vshost.exe" "TIFBun\bin\x86\DEBUG\*.*"

  WriteRegStr HKCU "Software\Classes\.pdf\OpenWithProgIDs" "${TIFBun}" ""
  WriteRegStr HKCU "Software\Classes\.tif\OpenWithProgIDs" "${TIFBun}" ""
  WriteRegStr HKCU "Software\Classes\.tiff\OpenWithProgIDs" "${TIFBun}" ""

  WriteRegStr HKCU "Software\Classes\${TIFBun}" "" "TIF分割"
  WriteRegStr HKCU "Software\Classes\${TIFBun}\DefaultIcon" "" "$OUTDIR\1.ico"
  WriteRegStr HKCU "Software\Classes\${TIFBun}\shell\open\command" "" '"$OUTDIR\TIFBun.exe" "%1"'
  
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.pdf\OpenWithProgids" "${TIFBun}" ""
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.tif\OpenWithProgids" "${TIFBun}" ""
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.tiff\OpenWithProgids" "${TIFBun}" ""
SectionEnd

Section
  DetailPrint "ファイル関連付けを更新中"
  !insertmacro UPDATEFILEASSOC
SectionEnd
