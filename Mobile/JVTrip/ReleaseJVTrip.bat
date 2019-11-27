@echo off

if %1.==. goto help
if %2.==. goto help
if %3.==. goto help
if %4.==. goto help

d:
cd D:\C#\Mobile\JVTrip

echo Compactando arquivos para montar o release do JVTrip
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -r -rr -tk D:\C#\Releases\JVTrip-V%1-SourceCode.zip *.*
call D:\C#\Mobile\JVUtils\ReleaseJVUTILS.bat %2
call D:\C#\Mobile\JVGPS\ReleaseJVGPS.bat %3
call D:\C#\Mobile\JVSQL\ReleaseJVSQL.bat %4

echo Montando arquivo para envio de versao
xcopy D:\C#\Mobile\JVTrip\JVTripCAB\Debug\JVTrip.CAB f:\temp /Y
xcopy D:\C#\Mobile\JVCompass\JVCompassCAB.CAB\Debug\JVCompass.CAB f:\temp /Y
"c:\Arquivos de Programas\WinRAR\Rar.exe" d F:\Temp\JVTrip-V%1-SourceCode.zip "JVTripDF" "Site" "JVTrip\bin" "JVTrip\obj" "JVTripCAB\Debug" "JVTripCAB\Release" "*.bat" "*.pdf" "*.txt"

echo Enviando arquivos para o FTP do Sourceforge
"D:\Arquivos de Programas\CoreFTP\coreftp" -s -O -u f:\temp\JVTrip.CAB -site Sourceforge_Upload -p uploads 
"D:\Arquivos de Programas\CoreFTP\coreftp" -s -O -u f:\temp\JVCompass.CAB -site Sourceforge_Upload -p uploads 
"D:\Arquivos de Programas\CoreFTP\coreftp" -s -O -u F:\Temp\JVTrip-V%1-SourceCode.zip -site Sourceforge_Upload -p uploads 
goto fim

:help
echo .
echo Use ReleaseJVTrip [Versao] [VersaoJVUtils] [VersaoJVGPS] [VersaoJVSQL]
echo .
echo Ex. ReleaseJVTrip 0100 0010 0010 0010
echo .

:fim