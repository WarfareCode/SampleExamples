// EventsDlg.h : header file
//

#pragma once
#include "afxwin.h"
#include "ObjectModelEventSink.h"

// CEventsDlg dialog
class CEventsDlg : public CDialog
{
	DECLARE_DYNAMIC(CEventsDlg);

// Construction
public:
	CEventsDlg(CWnd* pParent = NULL);	// standard constructor
	virtual ~CEventsDlg();

// Dialog Data
	enum { IDD = IDD_EVENTS_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;
	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedNewscen();
	afx_msg void OnBnClickedAddsat();
	afx_msg void OnBnClickedAddfac();
	afx_msg void OnBnClickedClosescen();

private:
	IAgStkObjectRootPtr   m_pRoot;
	CObjectModelEventSink *m_pSink;
	DWORD                  m_dwCookie;
	// methods 
	void UpdateDisplayState();
	void LogEvent(const CString& sMsg);
	void LogMessage(const CString& sMsg);
	void InitObjectModel();
	void TerminateObjectModel();

	friend class CObjectModelEventSink;
public:
	// A container for custom messages
	CListBox m_pListBox;
	afx_msg void OnBnClickedAniPlayforward();
	afx_msg void OnBnClickedAniPlaybackward();
	afx_msg void OnBnClickedAniPause();
	afx_msg void OnBnClickedAniRewind();
};
