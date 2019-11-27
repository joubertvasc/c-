// CFSampleDlg.h : header file
//

#pragma once

#define X_OFFSET		10
#define Y_OFFSET		10
#define Y_GAP			3

// CCFSampleDlg dialog
class CCFSampleDlg : public CDialog
{
// Construction
public:
	CCFSampleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_CFSAMPLE_DIALOG };

private:
	HMODULE m_hForwardModule;

private:
	void DisplayInfo(LPCTSTR lpszInfo);

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	
	afx_msg void OnDestroy();
	afx_msg void OnSize(UINT nType, int cx, int cy);

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedForward();
	afx_msg void OnEnChangeNumber();
};
