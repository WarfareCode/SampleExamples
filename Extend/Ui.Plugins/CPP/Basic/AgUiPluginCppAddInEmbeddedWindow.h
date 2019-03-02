//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
#pragma once

#include "Resource.h"
#include "ObjectModelEventSink.h"

///////////////////////////////////////////////////////////////////////////////
// CAgUiPluginCppAddInEmbeddedWindow dialog

class CAgUiPluginCppAddInEmbeddedWindow : public CDialog
{
	DECLARE_DYNAMIC(CAgUiPluginCppAddInEmbeddedWindow)

public:
	CAgUiPluginCppAddInEmbeddedWindow();
	virtual ~CAgUiPluginCppAddInEmbeddedWindow();

	// Dialog Data
	enum { IDD = IDD_CUSTOM_TOOL_WINDOW };
	// Creates a modeless dialog 
	BOOL Create(IAgUiPluginSite* pSite);
	// Destroy the current instance by deleting this pointer
	virtual void PostNcDestroy();

	DECLARE_INTERFACE_MAP()

	BEGIN_INTERFACE_PART(EmbeddedPlugin, IAgUiPluginEmbeddedWindowHandle)
		STDMETHOD(get_HWND)(OLE_HANDLE* pRetVal);
		STDMETHOD(Apply)(void);
	END_INTERFACE_PART(EmbeddedPlugin)

	BEGIN_INTERFACE_PART(EventLogger, IEventLogger)
		STDMETHOD(LogEvent)(const CString& rEvent, const CString& rString);
	END_INTERFACE_PART(EventLogger)

	BEGIN_INTERFACE_PART(ExtUIObject, IAgUiPluginEmbeddedControl)
		STDMETHOD(SetSite)(IAgUiPluginEmbeddedControlSite* pSite);
		STDMETHOD(OnClosing)(void);
		STDMETHOD(OnSaveModified)();
		STDMETHOD(GetIcon)(/*[out,retval]*/IPictureDisp** ppRetVal);
	END_INTERFACE_PART(ExtUIObject)

	virtual void OnSize(UINT nType, int cx, int cy);
	virtual BOOL OnInitDialog();
	afx_msg void OnPopupmenuClearEventLog();
	afx_msg void OnNMRclickObjectmodelEventViewer(NMHDR *pNMHDR, LRESULT *pResult);
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
private:
	CComPtr<IAgUiPluginEmbeddedControlSite> m_pContainerSite;
	CComPtr<IAgUiPluginSite> m_pSite;
	CComPtr<IDispatch> m_pRootDisp;
	CListCtrl m_eventLV;
	CObjectModelEventSink* m_pSink;
	DWORD m_dwCookie;

	HRESULT GetSTKObjectRoot(IDispatch** ppRetVal);
	void AdviseAutomationEvents();
	void UnadviseAutomationEvents();
};
