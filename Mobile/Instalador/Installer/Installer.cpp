// Installer.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "stdafx.h"
#include <string.h>  
#include <stdio.h>
#include <aygshell.h>
#include <iostream>
#include <fstream>
using namespace std;

bool fexists(const char *filename)
{
  ifstream ifile(filename);
  return ifile;
}

int _tmain(int argc, _TCHAR* argv[])
{
	DeleteFile(TEXT("\\windows\\iniciar\\install.lnk"));
	DeleteFile(TEXT("\\windows\\startup\\install.lnk"));

    PROCESS_INFORMATION pi = {0};

	TCHAR szInstallDir[255];
    memset(szInstallDir,0,sizeof(szInstallDir));
    _tcscat(szInstallDir, TEXT("wceload.exe"));

    if (FALSE == CreateProcess(szInstallDir, TEXT("\\temp\\NETCFv35.wm.armv4i.cab"), NULL, NULL, NULL, 0, NULL, NULL, NULL, &pi))
    {
        MessageBox(NULL, TEXT("Não foi possível instalar o .net3.5. Por favor instale-o manualmente."), TEXT("Erro"), MB_OK);
    }
	else
	{
		WaitForSingleObject(pi.hProcess, INFINITE);

        PROCESS_INFORMATION pi2 = {0};

        if (FALSE == CreateProcess(szInstallDir, TEXT("\\temp\\apptodate.cab"), NULL, NULL, NULL, 0, NULL, NULL, NULL, &pi2))
        {
            MessageBox(NULL, TEXT("Não foi possível instalar o AppToDate. Por favor instale-o manualmente."), TEXT("Erro"), MB_OK);
        }
		else
		{
            WaitForSingleObject(pi2.hProcess, INFINITE);

            PROCESS_INFORMATION pi3 = {0};

			if (fexists("\\temp\\ATMSistemas.cab"))
			{
                if (FALSE == CreateProcess(szInstallDir, TEXT("\\temp\\ATMSistemas.cab"), NULL, NULL, NULL, 0, NULL, NULL, NULL, &pi3))
                {
                    MessageBox(NULL, TEXT("Não foi possível instalar o ATMSistemas. Por favor instale-o manualmente."), TEXT("Erro"), MB_OK);
			    }
			}

			if (fexists("\\temp\\ATMSistemas2.cab"))
			{
                if (FALSE == CreateProcess(szInstallDir, TEXT("\\temp\\ATMSistemas2.cab"), NULL, NULL, NULL, 0, NULL, NULL, NULL, &pi3))
                {
                    MessageBox(NULL, TEXT("Não foi possível instalar o ATMSistemas. Por favor instale-o manualmente."), TEXT("Erro"), MB_OK);
			    }
			}

			if (fexists("\\temp\\ATMSistemas3.cab"))
			{
                if (FALSE == CreateProcess(szInstallDir, TEXT("\\temp\\ATMSistemas3.cab"), NULL, NULL, NULL, 0, NULL, NULL, NULL, &pi3))
                {
                    MessageBox(NULL, TEXT("Não foi possível instalar o ATMSistemas. Por favor instale-o manualmente."), TEXT("Erro"), MB_OK);
			    }
			}

			if (fexists("\\temp\\ATMSistemas4.cab"))
			{
                if (FALSE == CreateProcess(szInstallDir, TEXT("\\temp\\ATMSistemas4.cab"), NULL, NULL, NULL, 0, NULL, NULL, NULL, &pi3))
                {
                    MessageBox(NULL, TEXT("Não foi possível instalar o ATMSistemas. Por favor instale-o manualmente."), TEXT("Erro"), MB_OK);
			    }
			}
		}
	}

	return 0;
}
