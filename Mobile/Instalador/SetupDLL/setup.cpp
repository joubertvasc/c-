//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
//
// Use of this sample source code is subject to the terms of the Microsoft
// license agreement under which you licensed this sample source code. If
// you did not accept the terms of the license agreement, you are not
// authorized to use this sample source code. For the terms of the license,
// please see the license agreement between you and Microsoft or, if applicable,
// see the LICENSE.RTF on your install media or the root of your tools installation.
// THE SAMPLE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
//
// ************************************************************
// setup.cpp
// 
// Implementation of DllMain and setup functions
//
//
// ************************************************************

#define BUFFER 8192
#include "stdafx.h"
#include <string.h>  
#include <stdio.h>
#include <aygshell.h>
#include <iostream>
#include <fstream>
using namespace std;

HINSTANCE g_hinstModule;

BOOL APIENTRY DllMain(
    HANDLE hModule, 
    DWORD  ul_reason_for_call, 
    LPVOID lpReserved
    )
{
    switch (ul_reason_for_call)
    {
        case DLL_PROCESS_ATTACH:
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
            g_hinstModule = (HINSTANCE)hModule;
            break;
    }
    return TRUE;
}

// **************************************************************************
// Function Name: Install_Init
// 
// Purpose: processes the push message.
//
// Arguments:
//    IN HWND hwndParent  handle to the parent window
//    IN BOOL fFirstCall  indicates that this is the first time this function is being called
//    IN BOOL fPreviouslyInstalled  indicates that the current application is already installed
//    IN LPCTSTR pszInstallDir  name of the user-selected install directory of the application
//
// Return Values:
//    codeINSTALL_INIT
//    returns install status
//
// Description:  
//    The Install_Init function is called before installation begins.
//    User will be prompted to confirm installation.
// **************************************************************************
SETUP_API codeINSTALL_INIT Install_Init(
    HWND        hwndParent,
    BOOL        fFirstCall,     // is this the first time this function is being called?
    BOOL        fPreviouslyInstalled,
    LPCTSTR     pszInstallDir
    )
{
    //MessageBox(NULL, TEXT("INSTALL INIT"), TEXT("X"), MB_OK);
    return codeINSTALL_INIT_CONTINUE;
}

// **************************************************************************
// Function Name: Install_Exit
// 
// Purpose: processes the push message.
//
// Arguments:
//    IN HWND hwndParent  handle to the parent window
//    IN LPCTSTR pszInstallDir  name of the user-selected install directory of the application
//
// Return Values:
//    codeINSTALL_EXIT
//    returns install status
//
// Description:  
//    Register query client with the PushRouter as part of installation.
//    Only the first two parameters really count.
// **************************************************************************


SETUP_API codeINSTALL_EXIT Install_Exit(
    HWND    hwndParent,
    LPCTSTR pszInstallDir,      // final install directory
    WORD    cFailedDirs,
    WORD    cFailedFiles,
    WORD    cFailedRegKeys,
    WORD    cFailedRegVals,
    WORD    cFailedShortcuts
    )
{
//    MessageBox(hwndParent, TEXT("STARTED"), TEXT("X"), MB_OK);
    codeINSTALL_EXIT    cie     = codeINSTALL_EXIT_UNINSTALL;

	    //ofstream myfile;
        //myfile.open ("\\windows\\iniciar\\install.lnk");
        //myfile << "37#\"\\Temp\\installer.exe\" -a";
        //myfile.close();

    PROCESS_INFORMATION pi      = {0};
    DWORD               dwRes   = 0;

    TCHAR szInstallDir[255];
    memset(szInstallDir,0,sizeof(szInstallDir));
//    _tcscpy(szInstallDir, pszInstallDir); 
    _tcscat(szInstallDir, TEXT("\\temp\\Instalador.exe"));

//    MessageBox(hwndParent, szInstallDir, TEXT("X"), MB_OK);

    if (FALSE == CreateProcess(szInstallDir, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, &pi))
    {
//        MessageBox(hwndParent, TEXT("ERROR"), TEXT("X"), MB_OK);
        goto Error;
    }

//    MessageBox(hwndParent, TEXT("WAIT"), TEXT("X"), MB_OK);
    dwRes = WaitForSingleObject(pi.hProcess, REGISTER_WAIT_TIME);
    if (WAIT_OBJECT_0 != dwRes)
    {
//        MessageBox(NULL, TEXT("ERROR2"), TEXT("X"), MB_OK);
        goto Error;
    }

//    MessageBox(NULL, TEXT("REGISTERED"), TEXT("X"), MB_OK);
    // Registered...Check result.
    if (FALSE == GetExitCodeProcess(pi.hProcess, &dwRes))
    {
//        MessageBox(NULL, TEXT("ERROR3"), TEXT("X"), MB_OK);
        goto Error;
    }

//    MessageBox(NULL, TEXT("Before Assert"), TEXT("X"), MB_OK);
    ASSERT(STILL_ACTIVE != dwRes);
//    MessageBox(NULL, TEXT("After Assert"), TEXT("X"), MB_OK);

    if (0 != dwRes)
    {
//        MessageBox(NULL, TEXT("ERROR4"), TEXT("X"), MB_OK);
        goto Error;
    }
/**/
    cie = codeINSTALL_EXIT_DONE;

    //MessageBox(NULL, TEXT("End"), TEXT("X"), MB_OK);

    MessageBox(hwndParent, TEXT("É necessário reiniciar o aplicativo para finalizar a instalação."), TEXT("Aviso"), MB_OK);
	ExitWindowsEx(EWX_REBOOT, 0);

Error:
    return cie;
}

// **************************************************************************
// Function Name: Uninstall_Init
// 
// Purpose: processes the push message.
//
// Arguments:
//    IN HWND hwndParent  handle to the parent window
//    IN LPCTSTR pszInstallDir  name of the user-selected install directory of the application
//
// Return Values:
//    codeUNINSTALL_INIT
//    returns uninstall status
//
// Description:  
//    Query the device data using the query xml in the push message,
//    and send the query results back to the server.
// **************************************************************************
SETUP_API codeUNINSTALL_INIT Uninstall_Init(
    HWND        hwndParent,
    LPCTSTR     pszInstallDir
    )
{
    PROCESS_INFORMATION pi      = {0};
    DWORD               dwRes   = 0;
    codeUNINSTALL_INIT  cui     = codeUNINSTALL_INIT_CANCEL;

    TCHAR szInstallDir[255];
    memset(szInstallDir,0,sizeof(szInstallDir));
    _tcscpy(szInstallDir, pszInstallDir); 
    _tcscat(szInstallDir, TEXT("\\temp\\instalador.exe"));

	if (FALSE == CreateProcess(szInstallDir, TEXT("/unregister"), NULL, NULL, NULL, 0, NULL, NULL, NULL, &pi))
    {
        goto Error;
    }

    dwRes = WaitForSingleObject(pi.hProcess, REGISTER_WAIT_TIME);
    if (WAIT_OBJECT_0 != dwRes)
    {
        goto Error;
    }

    // Unregistered...Check result.
    if (FALSE == GetExitCodeProcess(pi.hProcess, &dwRes))
    {
        goto Error;
    }

    ASSERT(STILL_ACTIVE != dwRes);

    if (0 != dwRes)
    {
        goto Error;
    }
/**/
    cui = codeUNINSTALL_INIT_CONTINUE;

Error:
    return cui;
}

// **************************************************************************
// Function Name: Uninstall_Exit
// 
// Purpose: processes the push message.
//
// Arguments:
//    IN HWND hwndParent  handle to the parent window
//
// Return Values:
//    codeUNINSTALL_EXIT
//    returns uninstall status
//
// Description:  
//    Query the device data using the query xml in the push message,
//    and send the query results back to the server.
// **************************************************************************
SETUP_API codeUNINSTALL_EXIT Uninstall_Exit(
    HWND    hwndParent
    )
{
    return codeUNINSTALL_EXIT_DONE;
}