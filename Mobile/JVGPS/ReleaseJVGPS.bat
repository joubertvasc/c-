@echo off

if %1.==. goto help

echo Compactando arquivos...
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -r -rr -tk D:\C#\Releases\JVGPS-V%1-SourceCode.rar "D:\C#\Mobile\JVGPS"
goto fim

:help
echo .
echo Use ReleaseJVGPS [Versao]
echo .
echo Ex. ReleaseJVGPS 0100 
echo .

:fim