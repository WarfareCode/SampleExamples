// EventsDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Events.h"
#include "EventsDlg.h"
#include ".\eventsdlg.h"
#include "ObjectModelHelper.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CEventsDlg dialog




IMPLEMENT_DYNAMIC(CEventsDlg, CDialog);
CEventsDlg::CEventsDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CEventsDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_pSink = new CObjectModelEventSink(this);
	m_dwCookie = -1;
}
CEventsDlg::~CEventsDlg()
{
	TerminateObjectModel();
	delete m_pSink;
}

void CEventsDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LOG, m_pListBox);
}

void ReportError(const CString& sErrorMsg, const CString& sCaption)
{
	::MessageBox(NULL, sErrorMsg, sCaption, 0);
}

void ReportComError(_com_error& ex)
{
	ReportError(ex.ErrorMessage(), _T("COM Exception"));
}


BEGIN_MESSAGE_MAP(CEventsDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_CLOSE()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_NEWSCEN, OnBnClickedNewscen)
	ON_BN_CLICKED(IDC_ADDSAT, OnBnClickedAddsat)
	ON_BN_CLICKED(IDC_ADDFAC, OnBnClickedAddfac)
	ON_BN_CLICKED(IDC_CLOSESCEN, OnBnClickedClosescen)
	ON_BN_CLICKED(IDC_ANI_PLAYFORWARD, OnBnClickedAniPlayforward)
	ON_BN_CLICKED(IDC_ANI_PLAYBACKWARD, OnBnClickedAniPlaybackward)
	ON_BN_CLICKED(IDC_ANI_PAUSE, OnBnClickedAniPause)
	ON_BN_CLICKED(IDC_ANI_REWIND, OnBnClickedAniRewind)
END_MESSAGE_MAP()

void CEventsDlg::InitObjectModel()
{
	// Create a new instance of Automation Object Model Root Object
	HRESULT hr = m_pRoot.CreateInstance(__uuidof(AgStkObjectRoot));
	if(SUCCEEDED(hr))
	{
		// Get a pointer to sink's IUnknown without AddRef'ing it. CObjectModelEventSink
		// implements only dispinterface, so the IUnknown and IDispatch pointers will be the same.
		LPUNKNOWN pUnkSink = m_pSink->GetIDispatch(FALSE);
		// Establish a connection between source and sink.
		// m_dwCookie is a cookie identifying the connection, and is needed
		// to terminate the connection.
		AfxConnectionAdvise(m_pRoot, __uuidof(STKObjects::IAgStkObjectRootEvents), 
			pUnkSink, FALSE, &m_dwCookie);
	}
	else
	{
		//
		// Extract the error information returned by the CreateInstance
		//
		IErrorInfoPtr pErrorInfo = 0;
		HRESULT hRes = ::GetErrorInfo(0, &pErrorInfo);
		if(SUCCEEDED(hRes))
		{
			BSTR bstrDesc = 0;
			if (pErrorInfo != 0)
			{
				pErrorInfo->GetDescription(&bstrDesc);
				CString sTemp(bstrDesc);
				ReportError(sTemp, _T("Object Model"));
			}
			if(bstrDesc)
			{
				::SysFreeString(bstrDesc);
			}
		}
		else
		{
			ReportError(_T("Failed to create a new instance of AgStkObjectRoot class"), _T("Object Model"));
		}
	}
}

void CEventsDlg::TerminateObjectModel()
{
	LPUNKNOWN pUnkSink = m_pSink->GetIDispatch(FALSE);
	// Terminate a connection between source and sink.
	// m_dwCookied is a value obtained through AfxConnectionAdvise.
	AfxConnectionUnadvise(m_pRoot, __uuidof(STKObjects::IAgStkObjectRootEvents), 
		pUnkSink, FALSE, m_dwCookie);
	// Release the object model's root object.
	m_pRoot.Release();

}

// CEventsDlg message handlers

BOOL CEventsDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	/* Seed the random-number generator with current time so that
	* the numbers will be different every time we run.
	*/
	srand( (unsigned)time( NULL ) );

	/*
	 * Initialize the STK Automation Object Model
	 */
	InitObjectModel();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CEventsDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CEventsDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CEventsDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CEventsDlg::LogEvent(const CString& sMsg)
{
	m_pListBox.AddString(sMsg);
}

void CEventsDlg::LogMessage(const CString& sMsg)
{
	LogEvent(sMsg);
}

void CEventsDlg::UpdateDisplayState()
{
	BOOL bNoScenario = m_pRoot->CurrentScenario == NULL;
	GetDlgItem(IDC_NEWSCEN)->EnableWindow(bNoScenario);
	GetDlgItem(IDC_ADDSAT)->EnableWindow(!bNoScenario);
	GetDlgItem(IDC_ADDFAC)->EnableWindow(!bNoScenario);
	GetDlgItem(IDC_CLOSESCEN)->EnableWindow(!bNoScenario);
}

/*************************************************
 * Create a new scenario
 *************************************************/
void CEventsDlg::OnBnClickedNewscen()
{
	try
	{
		CObjectModelHelper helper;
		helper.NewScenario(m_pRoot);
	}
	catch(_com_error& ex)
	{
		ReportComError(ex);
	}
	UpdateDisplayState();
}

/*************************************************
 * Add a satellite to the scenario
 *************************************************/
void CEventsDlg::OnBnClickedAddsat()
{
	try
	{
		CObjectModelHelper helper;
		helper.CreateSatellite(m_pRoot);
	}
	catch(_com_error& ex)
	{
		ReportComError(ex);
	}

	UpdateDisplayState();
}

/*************************************************
 * Add a factory to the scenario
 *************************************************/
void CEventsDlg::OnBnClickedAddfac()
{
	try
	{
		CObjectModelHelper helper;
		helper.CreateFacility(m_pRoot);
	}
	catch(_com_error& err)
	{
		ReportComError(err);
	}
	UpdateDisplayState();
}
/*************************************************
 * Close the current scenario
 *************************************************/
void CEventsDlg::OnBnClickedClosescen()
{
	try
	{
		CObjectModelHelper helper;
		helper.CloseScenario(m_pRoot);
	}
	catch(_com_error& err)
	{
		ReportComError(err);
	}
	UpdateDisplayState();
}

/*************************************************
 * Animate forward in time
 *************************************************/
void CEventsDlg::OnBnClickedAniPlayforward()
{
	try
	{
		CObjectModelHelper helper;
		helper.PlayForward(m_pRoot);
	}
	catch(_com_error& err)
	{
		ReportComError(err);
	}
	UpdateDisplayState();
}

/*************************************************
 * Animate backward in time
 *************************************************/
void CEventsDlg::OnBnClickedAniPlaybackward()
{
	try
	{
		CObjectModelHelper helper;
		helper.PlayBackward(m_pRoot);
	}
	catch(_com_error& err)
	{
		ReportComError(err);
	}
	UpdateDisplayState();
}

/*************************************************
 * Pause the animation
 *************************************************/
void CEventsDlg::OnBnClickedAniPause()
{
	try
	{
		CObjectModelHelper helper;
		helper.Pause(m_pRoot);
	}
	catch(_com_error& err)
	{
		ReportComError(err);
	}
	UpdateDisplayState();
}

/*************************************************
 * Rewind the animation
 *************************************************/
void CEventsDlg::OnBnClickedAniRewind()
{
	try
	{
		CObjectModelHelper helper;
		helper.Rewind(m_pRoot);
	}
	catch(_com_error& err)
	{
		ReportComError(err);
	}
	UpdateDisplayState();
}
