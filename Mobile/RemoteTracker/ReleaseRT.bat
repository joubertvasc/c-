@echo off

if %1.==. goto help
if %2.==. goto help
if %3.==. goto help
if %4.==. goto help

d:
cd D:\C#\Mobile\RemoteTracker

echo Compactando arquivos para montar o release do RemoteTracker
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -r -rr -tk D:\C#\Releases\RemoteTracker-V%1-SourceCode.zip *.*
call D:\C#\Mobile\JVUtils\ReleaseJVUTILS.bat %2
call D:\C#\Mobile\JVGPS\ReleaseJVGPS.bat %3
call D:\C#\Mobile\JVSQL\ReleaseJVSQL.bat %4

echo Montando arquivo para envio de versao
"c:\Arquivos de Programas\WinRAR\Rar.exe" d D:\C#\Releases\RemoteTracker-V%1-SourceCode.zip "RTConfigDF" "Site" "Config\bin" "Config\obj" "DelUS\bin" "DelUS\obj" "Setup\bin" "Setup\obj" "TopSecret\bin" "TopSecret\obj" "SetupDLL\Windows Mobile 6 Professional SDK (ARMV4I)" "CommonDLL\bin" "CommonDLL\obj" "RemoteTracker\bin" "RemoteTracker\obj" "RemoteTrackerCAB\Debug" "RemoteTrackerCAB\Release" "rt\bin" "rt\obj" "RTRegCreator\bin" "RTRegCreator\obj" "RTRemote\bin" "RTRemote\obj" "SMSLauncher\bin" "SMSLauncher\obj" "Lock\bin" "Lock\obj" "RTRule\Windows Mobile 6 Professional SDK (ARMV4I)" "RTRule\Windows Mobile 6 Standard SDK (ARMV4I)" "Windows Mobile 6 Professional SDK (ARMV4I)\Debug" "*.bat" "*.pdf" "*.txt"
xcopy D:\C#\Mobile\RemoteTracker\RemoteTrackerCAB\Debug\RemoteTracker.CAB e:\temp /Y
xcopy D:\C#\Releases\RemoteTracker-V%1-SourceCode.zip e:\temp /Y

rem echo Enviando arquivos para o FTP do Sourceforge
rem "D:\Arquivos de Programas\CoreFTP\coreftp" -s -O -u e:\temp\RemoteTracker.CAB -site Sourceforge_Upload -p uploads 
rem "D:\Arquivos de Programas\CoreFTP\coreftp" -s -O -u e:\Temp\RemoteTracker-V%1-SourceCode.zip -site Sourceforge_Upload -p uploads 
goto fim

:help
echo .
echo Use ReleaseRT [Versao] [VersaoJVUtils] [VersaoJVGPS] [VersaoJVSQL]
echo .
echo Ex. ReleaseRT 0320 0010 0010 0010
echo .

:fim