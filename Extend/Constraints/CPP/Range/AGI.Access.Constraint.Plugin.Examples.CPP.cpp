/**********************************************************************/
/*           Copyright 2007, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"
#include "resource.h"
#include "AgStkUtil.tli"
#include "AgStkObjects.tli"

#include "AGI.Access.Constraint.Plugin.Examples.CPP.h"

class CAGIAccessConstraintPluginExamplesCPPModule : 
public CAtlDllModuleT< CAGIAccessConstraintPluginExamplesCPPModule >
{
	public :
		DECLARE_LIBID(LIBID_AGIAccessConstraintPluginExamplesCPPLib)
		DECLARE_REGISTRY_APPID_RESOURCEID(IDR_AGIACCESSCONSTRAINTPLUGINEXAMPLESCPP, 
			"{DD7DE1BE-9342-4838-98B2-23954C28CB96}")
};

CAGIAccessConstraintPluginExamplesCPPModule _AtlModule;

extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	BOOL br = _AtlModule.DllMain(dwReason, lpReserved); 
    
	return br;
}

STDAPI DllCanUnloadNow(void)
{
	HRESULT hr = _AtlModule.DllCanUnloadNow();

	return hr;
}

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
	HRESULT hr = _AtlModule.DllGetClassObject(rclsid, riid, ppv);

	return hr;
}

STDAPI DllRegisterServer(void)
{	
	HRESULT hr = _AtlModule.DllRegisterServer();

	return hr;
}

STDAPI DllUnregisterServer(void)
{	
	HRESULT hr = _AtlModule.DllUnregisterServer();

	return hr;
}

/**********************************************************************/
/*           Copyright 2007, Analytical Graphics, Inc.                */
/**********************************************************************/