//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
#pragma once

#include "Resource.h"
#include "afxcmn.h"

///////////////////////////////////////////////////////////////////////////////
// CAgUiPluginCppAddInConfigPage dialog

class CAgUiPluginCppAddInConfigPage : public CDialog
{
	DECLARE_DYNAMIC(CAgUiPluginCppAddInConfigPage)

public:
	CAgUiPluginCppAddInConfigPage();
	virtual ~CAgUiPluginCppAddInConfigPage();

// Dialog Data
	enum { IDD = IDD_TOOLSOPTIONS_CONFIG_PAGE };
	// Creates a modeless dialog 
	BOOL Create(IAgUiPluginSite* pSite);
	// Destroy the current instance by deleting this pointer
	virtual void PostNcDestroy();

	DECLARE_INTERFACE_MAP()

	BEGIN_INTERFACE_PART(EmbeddedPlugin, IAgUiPluginEmbeddedWindowHandle)
		STDMETHOD(get_HWND)(OLE_HANDLE* pRetVal);
		STDMETHOD(Apply)(void);
	END_INTERFACE_PART(EmbeddedPlugin)

	BEGIN_INTERFACE_PART(PageActions, IAgUiPluginConfigurationPageActions2)
		STDMETHOD(OnCreated)(/*[in]*/ IAgUiPluginConfigurationPageSite* Site);
		STDMETHOD(OnCancel)(void);
		STDMETHOD(OnOK)(void);
		STDMETHOD(OnApply)(/*[out,retval]*/ VARIANT_BOOL* pRetVal);
		STDMETHOD(OnHelp)(void);
		CComPtr<IAgUiPluginSite2> m_pPluginSite2;
	END_INTERFACE_PART(PageActions)

	virtual void OnSize(UINT nType, int cx, int cy);
	virtual BOOL OnInitDialog();
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
private:
	CComPtr<IAgUiPluginSite> m_pSite;
public:
	CComboBoxEx m_scmProviderCB;
};
