@echo off

if %1.==. goto help

echo Compactando arquivos...
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -r -rr -tk D:\C#\Releases\JVUTILS-V%1-SourceCode.rar "D:\C#\Mobile\JVUtils"
goto fim

:help
echo .
echo Use ReleaseJVUtils [Versao]
echo .
echo Ex. ReleaseJVUtils 0100 
echo .

:fim