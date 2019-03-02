//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
#if !defined(_AgUiPluginUtils_H_)
#define _AgUiPluginUtils_H_

// Retrieves a pointer to the STK Object Model root object using
// the given application model.
HRESULT AgGetStkObjectRoot(IDispatch* pApp, IDispatch** ppRetVal);

#endif // _AgUiPluginUtils_H_
