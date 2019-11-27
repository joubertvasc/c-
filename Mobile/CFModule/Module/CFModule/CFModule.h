// CFModule.h : main header file for the CFModule DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#ifdef POCKETPC2003_UI_MODEL
#include "resourceppc.h"
#endif 

typedef struct __CALLFORWARDING_INFO_TAG__
{
	DWORD	dwMode;
	int		nSeconds;
	CString	strNumber;
}CALLFORWARDING_INFO, *PCALLFORWARDING_INFO;

// CCFModuleApp
// See CFModule.cpp for the implementation of this class
//

class CCFModuleApp : public CWinApp
{
public:
	CCFModuleApp();

private:
	HLINEAPP				m_hLineApp;
	DWORD					m_dwDevices;
	DWORD					m_dwCellularId;
	DWORD					m_dwExtVersion;
	DWORD					m_dwLowAPIVersion;
	DWORD					m_dwAPIVersion;
	DWORD					m_dwAddressId;
	HLINE					m_hCellularLine;

private:
	long InitializeTAPI();
	HLINE OpenTAPILine(DWORD dwLineId);
	DWORD GetCurrentAddressId();
	void ShutdownTAPI();
	DWORD GetCellularLineId();
	LPLINEFORWARDLIST AllocateCallForwardList(PCALLFORWARDING_INFO pInfo,int nEntries);

public:
	LONG InitializeModule();
	LONG ForwardCalls(LPCTSTR lpszNumber,DWORD dwMode,int nSeconds);
	LONG ShutdownModule();

// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

