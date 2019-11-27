@echo off

if %1.==. goto help

echo Compactando arquivos...
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -r -rr -tk D:\C#\Releases\JVSQL-V%1-SourceCode.rar "D:\C#\Mobile\JVSQL"
goto fim

:help
echo .
echo Use ReleaseJVSQL [Versao]
echo .
echo Ex. ReleaseJVSQL 0100 
echo .

:fim