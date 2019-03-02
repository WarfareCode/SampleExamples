//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
// AgUiPluginCppAddIn.h : Declaration of the CAgUiPluginCppAddIn

#pragma once
#include "resource.h"       // main symbols

#include "AgUiPluginTutorial.h"
#include "AgUiPluginCppAddInStd.h"

// CAgUiPluginCppAddIn

class ATL_NO_VTABLE CAgUiPluginCppAddIn : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CAgUiPluginCppAddIn, &CLSID_AgUiPluginCppAddIn>,
	public ISupportErrorInfo,
	public IDispatchImpl<IAgUiPluginCppAddIn>,
	//--------------------------------
	// Interfaces to support 
	// AGI Ui Plugin Framework
	//--------------------------------
	public IAgUiPlugin,
	public IAgUiPluginLicense,
	public IAgUiPluginCommandTarget,
	public IAgUiPlugin2
{
public:
	CAgUiPluginCppAddIn();
	~CAgUiPluginCppAddIn();

DECLARE_REGISTRY_RESOURCEID(IDR_AgUiPluginCppAddIn)


BEGIN_COM_MAP(CAgUiPluginCppAddIn)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IAgUiPluginCppAddIn)
	COM_INTERFACE_ENTRY(ISupportErrorInfo)
	//--------------------------------
	// Interfaces to support 
	// AGI Ui Plugin Framework
	//--------------------------------
	COM_INTERFACE_ENTRY(IAgUiPlugin)
	COM_INTERFACE_ENTRY(IAgUiPluginLicense)
	COM_INTERFACE_ENTRY(IAgUiPluginCommandTarget)
	COM_INTERFACE_ENTRY(IAgUiPlugin2)
END_COM_MAP()

// ISupportsErrorInfo
	STDMETHOD(InterfaceSupportsErrorInfo)(REFIID riid);

	DECLARE_PROTECT_FINAL_CONSTRUCT()

public:
	//--------------------------------
	// IAgUiPlugin
	//--------------------------------
	STDMETHOD(OnStartup)(IAgUiPluginSite*);
	STDMETHOD(OnShutdown)();
	STDMETHOD(OnDisplayConfigurationPage)(/*[in]*/ IAgUiPluginConfigurationPageBuilder*);
	STDMETHOD(OnInitializeToolbar)(/*[in]*/ IAgUiPluginToolbarBuilder*);
	STDMETHOD(OnDisplayContextMenu)(/*[in]*/ IAgUiPluginMenuBuilder*);
	//--------------------------------
	// IAgUiPluginInfo
	//--------------------------------
	STDMETHOD(IsLicensed)(VARIANT_BOOL* pRetVal);
	//--------------------------------
	// IAgUiPluginCommandTarget
	//--------------------------------
	STDMETHOD(QueryState)(BSTR, AgEUiPluginCommandState*);
	STDMETHOD(Exec)(BSTR, IAgProgressTrackCancel*, IAgUiPluginCommandParameters*);
	//--------------------------------
	// IAgUiPlugin2
	//--------------------------------
	STDMETHOD(OnDisplayMenu)(BSTR, AgEUiPluginMenuBarKind, IAgUiPluginMenuBuilder2*);

	CComPtr<IAgUiPluginSite> m_pSite;
};

OBJECT_ENTRY_AUTO(__uuidof(AgUiPluginCppAddIn), CAgUiPluginCppAddIn)
