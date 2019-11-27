@echo off

echo Empacotando ATM para empresa 1 em d:\temp
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -rr -tk -ep1 D:\TEMP\ATM1.RAR D:\C#\Mobile\ATMSistemas\ADMSistemasCAB\Debug\ATMSistemas.CAB D:\C#\Mobile\ATMSistemas\Resources\atmsistemas.xml D:\C#\Mobile\Instalador\InstaladorCAB\Debug\atm.CAB  
echo Empacotando ATM para empresa 2 em d:\temp
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -rr -tk -ep1 D:\TEMP\ATM2.RAR D:\C#\Mobile\ATMSistemas\ADMSistemas2CAB\Debug\ATMSistemas2.CAB D:\C#\Mobile\ATMSistemas\Resources\Empresa2\atmsistemas.xml D:\C#\Mobile\Instalador\Instalador2CAB\Debug\atm2.CAB
echo Empacotando ATM para empresa 3 em d:\temp
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -rr -tk -ep1 D:\TEMP\ATM3.RAR D:\C#\Mobile\ATMSistemas\ADMSistemas3CAB\Debug\ATMSistemas3.CAB D:\C#\Mobile\ATMSistemas\Resources\Empresa3\atmsistemas.xml D:\C#\Mobile\Instalador\Instalador3CAB\Debug\atm3.CAB
echo Empacotando ATM para empresa 4 em d:\temp
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -m5 -rr -tk -ep1 D:\TEMP\ATM4.RAR D:\C#\Mobile\ATMSistemas\ADMSistemas4CAB\Debug\ATMSistemas4.CAB D:\C#\Mobile\ATMSistemas\Resources\Empresa4\atmsistemas.xml D:\C#\Mobile\Instalador\Instalador4CAB\Debug\atm4.CAB
echo Fim