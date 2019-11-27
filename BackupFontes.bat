@echo off
echo Compactando arquivos...
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -agdd-mm-yyyy -hpaakernak -v50000 -m5 -r -rr -tk "D:\Backups\Backupc#" "D:\C#\Mobile" "D:\C#\Desktop" "D:\C#\Testes"
"c:\Arquivos de Programas\WinRAR\Rar.exe" a -agdd-mm-yyyy -hpaakernak -v50000 -m5 -r -rr -tk "D:\Backups\BackupJava" "D:\Java\Android" "D:\Java\Mobile"
echo Copiando arquivos para drive C...
xcopy d:\c#\*.* i:\c#\ /e /i /q /r /y
xcopy d:\java\*.* i:\java\ /e /i /q /r /y