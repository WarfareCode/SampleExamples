//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
// CAgUiPluginCppAddInConfigPage.cpp : implementation file
//

#include "AgUiPluginCppAddInStd.h"
#include "AgUiPluginTutorial.h"
#include "AgUiPluginCppAddInConfigPage.h"
#include "AgUiPluginCppAddInCmds.h"
#include "AgUiPluginCppAddIn.h"

// CAgUiPluginCppAddInConfigPage dialog

IMPLEMENT_DYNAMIC(CAgUiPluginCppAddInConfigPage, CDialog)

BEGIN_MESSAGE_MAP(CAgUiPluginCppAddInConfigPage, CDialog)
	ON_WM_SIZE()
END_MESSAGE_MAP()


BEGIN_INTERFACE_MAP(CAgUiPluginCppAddInConfigPage, CCmdTarget)
	INTERFACE_PART(CAgUiPluginCppAddInConfigPage, __uuidof(IAgUiPluginEmbeddedWindowHandle), EmbeddedPlugin)
	INTERFACE_PART(CAgUiPluginCppAddInConfigPage, __uuidof(IAgUiPluginConfigurationPageActions), PageActions)
	INTERFACE_PART(CAgUiPluginCppAddInConfigPage, __uuidof(IAgUiPluginConfigurationPageActions2), PageActions)
END_INTERFACE_MAP()

CAgUiPluginCppAddInConfigPage::CAgUiPluginCppAddInConfigPage()
: CDialog(CAgUiPluginCppAddInConfigPage::IDD)
{
}

CAgUiPluginCppAddInConfigPage::~CAgUiPluginCppAddInConfigPage()
{
}

void CAgUiPluginCppAddInConfigPage::PostNcDestroy()
{
	// Destroy the instance when the reference count has reached zero.
	if (m_dwRef <= 0)
		delete this;
}

BOOL CAgUiPluginCppAddInConfigPage::OnInitDialog()
{
	if (!__super::OnInitDialog())
		return FALSE;

	COMBOBOXEXITEM cbi;
	cbi.mask = CBEIF_TEXT;

	TCHAR* pszStrings[] = { 
		_T("Source Safe Provider"),
		_T("Perforce Provider"),
		_T("ClearCase Provider"),
	};

	for (int i = 0; i < sizeof(pszStrings)/sizeof(pszStrings[0]); i++)
	{
		cbi.pszText = pszStrings[i];
		cbi.iItem = i;
		m_scmProviderCB.InsertItem(&cbi);
	}

	return TRUE;
}

BOOL CAgUiPluginCppAddInConfigPage::Create(IAgUiPluginSite* pSite)
{
	m_pSite.Release();
	m_pSite = pSite;
	// Create a modeless dialog using a given resource ID
	return CDialog::Create(CAgUiPluginCppAddInConfigPage::IDD);
}

void CAgUiPluginCppAddInConfigPage::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBOBOXEX1, m_scmProviderCB);
}


void CAgUiPluginCppAddInConfigPage::OnSize(UINT nType, int cx, int cy)
{
	__super::OnSize(nType, cx, cy);
}
// CAgUiPluginCppAddInConfigPage message handlers
STDMETHODIMP CAgUiPluginCppAddInConfigPage::XEmbeddedPlugin::get_HWND(OLE_HANDLE* pRetVal)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, EmbeddedPlugin)

	if (pRetVal == NULL)
		return E_POINTER;

	// Unsafe type cast! 
	*pRetVal = (OLE_HANDLE)(INT_PTR)pThis->GetSafeHwnd();

	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddInConfigPage::XEmbeddedPlugin::Apply(void)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, EmbeddedPlugin)
	
	return S_OK;
}

STDMETHODIMP_(ULONG) CAgUiPluginCppAddInConfigPage::XEmbeddedPlugin::AddRef()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, EmbeddedPlugin)
	return pThis->ExternalAddRef();
}

STDMETHODIMP_(ULONG) CAgUiPluginCppAddInConfigPage::XEmbeddedPlugin::Release()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, EmbeddedPlugin)
	return pThis->ExternalRelease();
}

STDMETHODIMP CAgUiPluginCppAddInConfigPage::XEmbeddedPlugin::QueryInterface(
          REFIID iid, LPVOID far* ppvObj)     
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, EmbeddedPlugin)
	return pThis->ExternalQueryInterface(&iid, ppvObj);
}

STDMETHODIMP CAgUiPluginCppAddInConfigPage::XPageActions::OnCreated(/*[in]*/ IAgUiPluginConfigurationPageSite* Site)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, PageActions)

	IAgUiPlugin *pPlugin;
	Site->get_Plugin( &pPlugin);

	CAgUiPluginCppAddIn *pExampleUiPlugin = (CAgUiPluginCppAddIn*)pPlugin;

	IAgUiPluginSite *pPluginSite;
	pPluginSite = pExampleUiPlugin->m_pSite;

	m_pPluginSite2.Release();
	m_pPluginSite2 = (IAgUiPluginSite2*)pPluginSite;

	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddInConfigPage::XPageActions::OnCancel(void)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, PageActions)
	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddInConfigPage::XPageActions::OnOK(void)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, PageActions)
	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddInConfigPage::XPageActions::OnApply(/*[out,retval]*/ VARIANT_BOOL* pRetVal)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, PageActions)
	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddInConfigPage::XPageActions::OnHelp(void)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, PageActions)

	CComBSTR bstrHelpFile("C:\\Program Files (x86)\\AGI\\STK 11\\Help\\stk.chm");
	CComBSTR bstrHelpKey("");
	m_pPluginSite2->ShowHelp( bstrHelpFile, bstrHelpKey, eUiPluginHelpTOC);

	return S_OK;
}
STDMETHODIMP_(ULONG) CAgUiPluginCppAddInConfigPage::XPageActions::AddRef()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, PageActions)
	return pThis->ExternalAddRef();
}

STDMETHODIMP_(ULONG) CAgUiPluginCppAddInConfigPage::XPageActions::Release()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, PageActions)
	return pThis->ExternalRelease();
}

STDMETHODIMP CAgUiPluginCppAddInConfigPage::XPageActions::QueryInterface(
          REFIID iid, LPVOID far* ppvObj)     
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInConfigPage, PageActions)
	return pThis->ExternalQueryInterface(&iid, ppvObj);
}
