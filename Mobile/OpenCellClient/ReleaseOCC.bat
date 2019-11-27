@echo off

if %1.==. goto help
if %2.==. goto help
if %3.==. goto help
if %4.==. goto help

d:
cd D:\C#\Mobile\OpenCellClient

echo Compactando arquivos para montar o release do OpenCellClient
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -r -rr -tk D:\C#\Releases\OpenCellClient-V%1-SourceCode.zip *.*
call D:\C#\Mobile\JVUtils\ReleaseJVUTILS.bat %2
call D:\C#\Mobile\JVGPS\ReleaseJVGPS.bat %3
call D:\C#\Mobile\JVSQL\ReleaseJVSQL.bat %4

echo Montando arquivo para envio de versao
xcopy D:\C#\Mobile\OpenCellClient\OpenCellClientCAB\Debug\OpenCellClient.CAB f:\temp /Y
xcopy D:\C#\Releases\OpenCellClient-V%1-SourceCode.zip f:\temp /Y
"c:\Arquivos de Programas\WinRAR\Rar.exe" d F:\Temp\OpenCellClient-V%1-SourceCode.zip "OpenCellClientDF" "Site" "OpenCellClient\bin" "OpenCellClient\obj" "OpenCellClientCAB\Debug" "OpenCellClientCAB\Release" "*.bat"

echo Enviando arquivos para o FTP do Sourceforge
"D:\Arquivos de Programas\CoreFTP\coreftp" -s -O -u f:\temp\OpenCellClient.cab -site Sourceforge_Upload -p uploads 
"D:\Arquivos de Programas\CoreFTP\coreftp" -s -O -u f:\temp\OpenCellClient-V%1-SourceCode.zip -site Sourceforge_Upload -p uploads 
goto fim

:help
echo .
echo Use ReleaseOCC [Versao] [VersaoJVUtils] [VersaoJVGPS] [VersaoJVSQL]
echo .
echo Ex. ReleaseOCC 0201 0010 0010 0010
echo .

:fim