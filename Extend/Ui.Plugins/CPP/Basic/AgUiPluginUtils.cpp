//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
#include "AgUiPluginCppAddInStd.h"
#include "AgUiPluginUtils.h"

///////////////////////////////////////////////////////////////////////////////

HRESULT AgGetStkObjectRoot(IDispatch* pDisp, IDispatch** ppRetVal)
{
	USES_CONVERSION;

	CComQIPtr<IAgUiApplication> pApp (pDisp);
	if (!pApp)
		return E_FAIL;

	return pApp->get_Personality2(ppRetVal);
}
