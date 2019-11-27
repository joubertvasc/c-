// CFModule.cpp : Defines the initialization routines for the DLL.
//

#include "stdafx.h"
#include "CFModule.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

void FAR PASCAL lineCallback(DWORD hDevice,DWORD dwMsg,DWORD dwCallbackInstance,
							 DWORD dwParam1,DWORD dwParam2,DWORD dwParam3)
{
	switch(dwMsg)
	{
		case LINE_DEVSPECIFIC:
		{
			break;
		}
		case LINE_REPLY:
		{
			break;
		}
	}
}

//
//TODO: If this DLL is dynamically linked against the MFC DLLs,
//		any functions exported from this DLL which call into
//		MFC must have the AFX_MANAGE_STATE macro added at the
//		very beginning of the function.
//
//		For example:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// normal function body here
//		}
//
//		It is very important that this macro appear in each
//		function, prior to any calls into MFC.  This means that
//		it must appear as the first statement within the 
//		function, even before any object variable declarations
//		as their constructors may generate calls into the MFC
//		DLL.
//
//		Please see MFC Technical Notes 33 and 58 for additional
//		details.
//


// CCFModuleApp

BEGIN_MESSAGE_MAP(CCFModuleApp, CWinApp)
END_MESSAGE_MAP()


// CCFModuleApp construction

CCFModuleApp::CCFModuleApp()
{
	m_dwAddressId	= 0;
}


// The one and only CCFModuleApp object

CCFModuleApp theApp;


// CCFModuleApp initialization

BOOL CCFModuleApp::InitInstance()
{
	CWinApp::InitInstance();

	

	return TRUE;
}

int CCFModuleApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}

long CCFModuleApp::InitializeTAPI()
{
	LINEINITIALIZEEXPARAMS	sLineParam;

	memset(&sLineParam,0,sizeof(LINEINITIALIZEEXPARAMS));

	sLineParam.dwTotalSize	= sizeof(LINEINITIALIZEEXPARAMS);
	sLineParam.dwOptions	= LINEINITIALIZEEXOPTION_USEHIDDENWINDOW; 

	m_dwLowAPIVersion		= TAPI_CURRENT_VERSION;

	long	lReturn	= lineInitializeEx(&m_hLineApp,
									   theApp.m_hInstance,
									   lineCallback,
									   TAPI_FRIENDLY_NAME,
									   &m_dwDevices,
									   &m_dwLowAPIVersion,
									   &sLineParam);

	return lReturn;
}


HLINE CCFModuleApp::OpenTAPILine(DWORD dwLineId)
{
	DWORD	dwMediaMode = LINEMEDIAMODE_INTERACTIVEVOICE;
	HLINE	hLine		= NULL;
	long	lReturn		= lineOpen(m_hLineApp,m_dwCellularId,&hLine,
								   TAPI_CURRENT_VERSION,0,(DWORD)this,
								   LINECALLPRIVILEGE_OWNER,dwMediaMode,0);

	lReturn				= lineNegotiateExtVersion(m_hLineApp,m_dwCellularId,m_dwAPIVersion,EXT_API_LOW_VERSION,EXT_API_HIGH_VERSION,&m_dwExtVersion);

	return hLine;
}

DWORD CCFModuleApp::GetCellularLineId()
{
	DWORD				dwReturn		= 0xFFFFFFFF;
	long				lResult			= 0;
	LINEEXTENSIONID		sLineExt		= {0};
	LPLINEDEVCAPS		lpLineDevCaps	= NULL;	
	BOOL				bContinue		= TRUE;

	for(DWORD dwLine=0; dwLine<m_dwDevices && bContinue; ++dwLine)
	{
		lResult		= lineNegotiateAPIVersion(m_hLineApp,dwLine,m_dwLowAPIVersion,TAPI_CURRENT_VERSION,&m_dwAPIVersion,&sLineExt);

		if(0 == lResult)
		{
			lpLineDevCaps	= (LPLINEDEVCAPS)LocalAlloc(LPTR,sizeof(LINEDEVCAPS));
			lResult			= LINEERR_STRUCTURETOOSMALL;

			lpLineDevCaps->dwTotalSize	= sizeof(LINEDEVCAPS);
			lpLineDevCaps->dwNeededSize	= sizeof(LINEDEVCAPS);

			while(LINEERR_STRUCTURETOOSMALL == lResult)
			{
				lResult	= lineGetDevCaps(m_hLineApp,dwLine,TAPI_CURRENT_VERSION,0,lpLineDevCaps);

				if(LINEERR_STRUCTURETOOSMALL == lResult || lpLineDevCaps->dwTotalSize < lpLineDevCaps->dwNeededSize)
				{
					lpLineDevCaps	= (LPLINEDEVCAPS)LocalReAlloc(lpLineDevCaps,lpLineDevCaps->dwNeededSize,LMEM_MOVEABLE);
					lResult			= LINEERR_STRUCTURETOOSMALL;

					lpLineDevCaps->dwTotalSize	= lpLineDevCaps->dwNeededSize;
				}
			}

			if(0 == lResult)
			{
				TCHAR szName[512];

				memcpy((PVOID)szName,(PVOID)((BYTE*)lpLineDevCaps + lpLineDevCaps ->dwLineNameOffset), 
					    lpLineDevCaps->dwLineNameSize);

				szName[lpLineDevCaps->dwLineNameSize]	= 0;

				if(_tcscmp(szName,CELLTSP_LINENAME_STRING) == 0)
				{
					dwReturn	= dwLine;
					bContinue	= FALSE;
				}
			}

			LocalFree((HLOCAL)lpLineDevCaps);
		}
	}

	return dwReturn;
}

DWORD CCFModuleApp::GetCurrentAddressId()
{
	DWORD				dwAddressId	= 0;
	LINEADDRESSCAPS*	pAddress	=(LINEADDRESSCAPS*)LocalAlloc(LPTR,sizeof(LINEADDRESSCAPS));

	pAddress->dwTotalSize		= sizeof(LINEADDRESSCAPS);
	
	long	lReturn				= LINEERR_STRUCTURETOOSMALL;

	while(LINEERR_STRUCTURETOOSMALL == lReturn)
	{
		lReturn	= lineGetAddressCaps(m_hLineApp,m_dwCellularId,0,TAPI_CURRENT_VERSION,0,pAddress);

		if(LINEERR_STRUCTURETOOSMALL == lReturn || pAddress->dwTotalSize < pAddress->dwNeededSize)
		{
			pAddress				= (LINEADDRESSCAPS*)LocalReAlloc(pAddress,pAddress->dwNeededSize,LMEM_MOVEABLE);
			lReturn					= LINEERR_STRUCTURETOOSMALL;

			pAddress->dwTotalSize	= pAddress->dwNeededSize;
		}
	}

	if(0 == lReturn)
	{
		PWCHAR lpszAddress = (WCHAR*)(((BYTE*)pAddress)+ pAddress->dwAddressOffset);

		//CString	strAddress = TEXT("9847152741");

		lReturn = lineGetAddressID(m_hCellularLine,&dwAddressId,LINEADDRESSMODE_DIALABLEADDR,
								   lpszAddress,lstrlen(lpszAddress));
	}

	return dwAddressId;
}

void CCFModuleApp::ShutdownTAPI()
{
	if(m_hCellularLine)
	{
		lineClose(m_hCellularLine);
	}

	if(m_hLineApp)
	{
		lineShutdown(m_hLineApp);
	}

	m_hLineApp		= NULL;
	m_hCellularLine	= NULL;
}

LPLINEFORWARDLIST CCFModuleApp::AllocateCallForwardList(PCALLFORWARDING_INFO pInfo,int nEntries)
{
	int					nTextLen	= 0;

	for(int nNumber=0; nNumber<nEntries; ++nNumber)
	{
		nTextLen	+= ((lstrlen(pInfo[nNumber].strNumber) + 1) * sizeof(TCHAR));
	}

	DWORD				dwSize	= (sizeof(LINEFORWARDLIST));

	dwSize	+= nTextLen;
	dwSize	+= (sizeof(LINEFORWARD) * (nEntries - 1));
								  
	LPLINEFORWARDLIST	pList		= (LPLINEFORWARDLIST)LocalAlloc(LPTR,dwSize);

	ZeroMemory(pList,dwSize);

	DWORD	dwOffset	= sizeof(LINEFORWARDLIST) + (sizeof(LINEFORWARD) * (nEntries - 1));

	pList->dwNumEntries	= nEntries;
	pList->dwTotalSize	= dwSize;
	
	for(int nNumber=0; nNumber<nEntries; ++nNumber)
	{
		pList->ForwardList[nNumber].dwCallerAddressOffset	= 0;
		pList->ForwardList[nNumber].dwCallerAddressSize		= 0;
		pList->ForwardList[nNumber].dwDestCountryCode		= 0;
		pList->ForwardList[nNumber].dwForwardMode			= pInfo[nNumber].dwMode;
		pList->ForwardList[nNumber].dwDestAddressSize		= (lstrlen(pInfo[nNumber].strNumber) + 1) * sizeof(TCHAR);
		pList->ForwardList[nNumber].dwDestAddressOffset		= dwOffset;

		wcsncpy((TCHAR*)((LPBYTE)pList + dwOffset),
				pInfo[nNumber].strNumber,
				pList->ForwardList[nNumber].dwDestAddressSize);

		dwOffset	+= ((lstrlen(pInfo[nNumber].strNumber) + 1) * sizeof(TCHAR));
	}

	return pList;
}

LONG CCFModuleApp::InitializeModule()
{
	LONG lError	= E_FAIL;

	if(0 == InitializeTAPI())
	{
		m_dwCellularId	= GetCellularLineId();

		if(0xFFFFFFFF != m_dwCellularId)
		{
			m_hCellularLine	= OpenTAPILine(m_dwCellularId);

			if(m_hCellularLine)
			{
				m_dwAddressId	= GetCurrentAddressId();

				lError			= 0;
			}
		}
	}

	return lError;
}

LONG CCFModuleApp::ShutdownModule()
{
	ShutdownTAPI();

	return 0;
}

LONG CCFModuleApp::ForwardCalls(LPCTSTR lpszNumber,DWORD dwMode,int nSeconds)
{
	LONG					lError		= E_FAIL;
	LPLINEFORWARDLIST		pInfo		= NULL;
	HCALL					hCall		= NULL;
	CALLFORWARDING_INFO		sInfo[1]	= {0};

	if(lpszNumber)
	{
		sInfo[0].dwMode		= dwMode;
		sInfo[0].nSeconds	= nSeconds;
		sInfo[0].strNumber	= lpszNumber;

		pInfo				= AllocateCallForwardList(sInfo,1);
	}

	if(pInfo)	lError		= lineForward(m_hCellularLine,FALSE,m_dwAddressId,pInfo,nSeconds,&hCall,NULL);
	else		lError		= lineForward(m_hCellularLine,FALSE,m_dwAddressId,NULL,nSeconds,&hCall,NULL);

	return lError;
}


extern "C" LONG ForwardCall(LPCTSTR lpszNumber,DWORD dwMode,int nSeconds)
{
	return theApp.ForwardCalls(lpszNumber,dwMode,nSeconds);
}

extern "C" LONG CancelForward()
{
	return theApp.ForwardCalls(NULL,0,0);
}

extern "C" LONG Initialize()
{
	return theApp.InitializeModule();
}

extern "C" LONG Shutdown()
{
	return theApp.ShutdownModule();
}