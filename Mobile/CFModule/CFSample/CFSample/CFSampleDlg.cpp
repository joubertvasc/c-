// CFSampleDlg.cpp : implementation file
//

#include "stdafx.h"
#include "CFSample.h"
#include "CFSampleDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CCFSampleDlg dialog

CCFSampleDlg::CCFSampleDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CCFSampleDlg::IDD, pParent)
{
	m_hIcon				= AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hForwardModule	= NULL;
}

void CCFSampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CCFSampleDlg, CDialog)
	ON_WM_SIZE()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_FORWARD, &CCFSampleDlg::OnBnClickedForward)
	ON_EN_CHANGE(IDC_NUMBER, &CCFSampleDlg::OnEnChangeNumber)
END_MESSAGE_MAP()


// CCFSampleDlg message handlers

BOOL CCFSampleDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	CString	strModuleName	= TEXT("CFModule.dll");
	
	m_hForwardModule	 = LoadLibrary(strModuleName);

	if(NULL == m_hForwardModule)
	{
		GetDlgItem(IDC_STATIC_FORWARD)->EnableWindow(FALSE);
		GetDlgItem(IDC_NUMBER)->EnableWindow(FALSE);
		GetDlgItem(IDC_FORWARD)->EnableWindow(FALSE);

		CString	strError;

		strError.Format(TEXT("%s Error: %ld"),TEXT("Cannot load the CFModule.dll file."),GetLastError());
		DisplayInfo(strError);
	}
	else
	{
		OnEnChangeNumber();

		CComboBox*	pCombo	= (CComboBox*)GetDlgItem(IDC_CONDITION);

		if(pCombo)
		{
			int		nItem	= 0;

			nItem	= pCombo->AddString(TEXT("All calls"));
			pCombo->SetItemData(nItem,LINEFORWARDMODE_UNCOND);

			nItem	= pCombo->AddString(TEXT("If Busy"));
			pCombo->SetItemData(nItem,LINEFORWARDMODE_BUSY);

			nItem	= pCombo->AddString(TEXT("If No Answer"));
			pCombo->SetItemData(nItem,LINEFORWARDMODE_NOANSW);

			nItem	= pCombo->AddString(TEXT("Not Available"));
			pCombo->SetItemData(nItem,LINEFORWARDMODE_BUSYNA);

			pCombo->SetCurSel(0);
		}
	}
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CCFSampleDlg::DisplayInfo(LPCTSTR lpszInfo)
{
	GetDlgItem(IDC_INFO)->SetWindowText(lpszInfo);
}

void CCFSampleDlg::OnSize(UINT nType, int cx, int cy)
{
	CDialog::OnSize(nType,cx,cy);

	CWnd*	pWnd	= NULL;
	int		y		= Y_OFFSET;
	CRect	rc;

	if((pWnd = GetDlgItem(IDC_STATIC_FORWARD)))
	{
		pWnd->GetWindowRect(&rc);
		pWnd->SetWindowPos(NULL,X_OFFSET,y,(cx - X_OFFSET * 2),rc.Height(),SWP_NOZORDER);

		y		+= (rc.Height() + Y_GAP);
	}

	if((pWnd = GetDlgItem(IDC_NUMBER)))
	{
		pWnd->GetWindowRect(&rc);
		pWnd->SetWindowPos(NULL,X_OFFSET,y,(cx - X_OFFSET * 2),rc.Height(),SWP_NOZORDER);

		y		+= (rc.Height() + Y_GAP);
	}

	if((pWnd = GetDlgItem(IDC_CONDITION)))
	{
		pWnd->GetWindowRect(&rc);
		pWnd->SetWindowPos(NULL,X_OFFSET,y,(cx - X_OFFSET * 2),rc.Height(),SWP_NOZORDER);

		y		+= (rc.Height() + Y_GAP);
	}

	if((pWnd = GetDlgItem(IDC_FORWARD)))
	{
		pWnd->GetWindowRect(&rc);
		pWnd->SetWindowPos(NULL,(cx - X_OFFSET - rc.Width()),y,rc.Width(),rc.Height(),SWP_NOZORDER);

		y		+= (rc.Height() + Y_GAP);
	}

	if((pWnd = GetDlgItem(IDC_INFO)))
	{
		pWnd->GetWindowRect(&rc);
		pWnd->SetWindowPos(NULL,X_OFFSET,(cy - Y_OFFSET - rc.Height()),(cx - X_OFFSET * 2),rc.Height(),SWP_NOZORDER);

		y		+= (rc.Height() + Y_GAP);
	}
}

void CCFSampleDlg::OnDestroy()
{
	if(m_hForwardModule)
	{
		FreeLibrary(m_hForwardModule);
	}

	CDialog::OnDestroy();
}

void CCFSampleDlg::OnBnClickedForward()
{
	if(!m_hForwardModule)
	{
		return;
	}

	typedef LONG (*PFNFORWARDCALL)(LPCTSTR lpszNumber,DWORD dwMode,int nSeconds);
	typedef LONG (*PFNCANCELFORWARD)();
	typedef LONG (*PFNINTIALIZE)();
	typedef LONG (*PFNSHUTDOWN)();

	PFNFORWARDCALL		pfnForward		= (PFNFORWARDCALL)GetProcAddress(m_hForwardModule,TEXT("ForwardCall"));	
	PFNCANCELFORWARD	pfnCancel		= (PFNCANCELFORWARD)GetProcAddress(m_hForwardModule,TEXT("CancelForward"));	
	PFNINTIALIZE		pfnInitialize	= (PFNCANCELFORWARD)GetProcAddress(m_hForwardModule,TEXT("Initialize"));	
	PFNSHUTDOWN			pfnShutdown		= (PFNCANCELFORWARD)GetProcAddress(m_hForwardModule,TEXT("Shutdown"));	

	if(!pfnForward		|| 
	   !pfnCancel		||
	   !pfnInitialize	||
	   !pfnShutdown)
	{
		return;
	}

	CString		strNumber	= TEXT("");
	LONG		lError		= 0;
	CComboBox*	pCombo		= (CComboBox*)GetDlgItem(IDC_CONDITION);
	int			nSel		= pCombo->GetCurSel();
	DWORD		dwMode		= pCombo->GetItemData(nSel);

	GetDlgItemText(IDC_NUMBER,strNumber);

	if(0 == pfnInitialize())
	{
		if(strNumber.GetLength() > 0)	lError = pfnForward(strNumber,dwMode,2);
		else							lError = pfnCancel();

		pfnShutdown();
	}

	CString	strError;

	strError.Format(TEXT("%s%X"),TEXT("ForwardCall result: "),lError);
	DisplayInfo(strError);	
}

void CCFSampleDlg::OnEnChangeNumber()
{
	CString	strNumber	= TEXT("");

	GetDlgItemText(IDC_NUMBER,strNumber);

	if(strNumber.GetLength() > 0)	SetDlgItemText(IDC_FORWARD,TEXT("Forward"));
	else							SetDlgItemText(IDC_FORWARD,TEXT("Cancel Forward"));
}
