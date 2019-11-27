@echo off

if %1.==. goto help
if %2.==. goto help
if %3.==. goto help
if %4.==. goto help
if %5.==. goto help

d:
cd D:\C#\Mobile\ATMSistemas

echo Compactando arquivos para montar o release do ATMSistemas
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -r -rr -tk D:\C#\Releases\ATMSistemas-V%1.zip *.*

echo Limpa o backup dos fontes retirando arquivos inúteis
"c:\Arquivos de Programas\WinRAR\Rar.exe" d D:\C#\Releases\ATMSistemas-V%1.zip "ADMSistemasCAB\Debug" "ADMSistemas2CAB\Debug" "ADMSistemas3CAB\Debug" "ADMSistemas4CAB\Debug" "ATMConnVerify\bin" "ATMConnVerify\obj" "ATMDiario\bin" "ATMDiario\obj" "ATMDLL\bin" "ATMDLL\obj" "ATMSistemas\bin" "ATMSistemas\obj" "ATMWakeUp\bin" "ATMWakeUp\obj" "DF\Dotfuscated" "RTRule\Windows Mobile 6 Professional SDK (ARMV4I)" "RTRule\Windows Mobile 6 Standard SDK (ARMV4I)" "setup\bin" "setup\obj" "SetupDLL\Windows Mobile 6 Professional SDK (ARMV4I)" "SetupDLL\Windows Mobile 6 Standard SDK (ARMV4I)" "SMSLauncher\bin" "SMSLauncher\obj" "Windows Mobile 6 Professional SDK (ARMV4I)"

echo Compactando fontes dependentes
call D:\C#\Mobile\RemoteTracker\ReleaseRT.bat %5 %2 %3 %4

echo Removendo arquivos inúveis
del e:\temp\RemoteTracker.CAB
del e:\temp\RemoteTracker-V%5-SourceCode.zip

goto fim

:help
echo .
echo Use ReleaseATM [VersaoATM] [VersaoJVUtils] [VersaoJVGPS] [VersaoJVSQL] [VersaoRT]
echo .
echo Ex. ReleaseATM 10018 0144 0110 0210 0437
echo .

:fim