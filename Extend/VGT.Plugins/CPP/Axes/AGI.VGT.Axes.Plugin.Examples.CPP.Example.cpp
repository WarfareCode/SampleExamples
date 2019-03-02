/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"
#include "resource.h"
#include "AgStkUtil.tli"
#include "AgStkObjects.tli"
#include "Agi.VGT.Axes.Plugin.Examples.CPP.Example.h"

class CAgiVgtAxesPluginCPPExampleModule : public CAtlDllModuleT< CAgiVgtAxesPluginCPPExampleModule >
{
public :
	DECLARE_LIBID(LIBID_AgiVGTAxesPluginCPPExampleLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_AGIVGTAVXESPLUGINEXAMPLESCPPEXAMPLE, "{828F9C03-DB25-4182-BFC2-D8D92D96DE72}")
};

CAgiVgtAxesPluginCPPExampleModule _AtlModule;

extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllMain(): --> Entered\n");
	#endif

	BOOL br = FALSE;

	br = _AtlModule.DllMain(dwReason, lpReserved); 

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllMain(): <-- Exited\n");
	#endif
    
	return br;
}

STDAPI DllCanUnloadNow(void)
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllCanUnloadNow(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;

	hr = _AtlModule.DllCanUnloadNow();

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllCanUnloadNow(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllGetClassObject(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;

	hr = _AtlModule.DllGetClassObject(rclsid, riid, ppv);

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllGetClassObject(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllRegisterServer(void)
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllRegisterServer(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;
	
	hr = _AtlModule.DllRegisterServer();

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllRegisterServer(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllUnregisterServer(void)
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllUnregisterServer(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;
	
	hr = _AtlModule.DllUnregisterServer();

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.DllUnregisterServer(): <-- Exited\n");
	#endif

	return hr;
}
/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/