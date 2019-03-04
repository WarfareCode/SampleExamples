// Machine generated IDispatch wrapper class(es) created by Microsoft Visual C++

// NOTE: Do not modify the contents of this file.  If this class is regenerated by
//  Microsoft Visual C++, your modifications will be overwritten.


#include "stdafx.h"
#include "aguiax2dcntrl.h"

// Dispatch interfaces referenced by this interface
#include "Picture.h"
#include "AgPickInfoData.h"
#include "AgSTKXApplication.h"

/////////////////////////////////////////////////////////////////////////////
// CAgUiAx2DCntrl

IMPLEMENT_DYNCREATE(CAgUiAx2DCntrl, CWnd)

/////////////////////////////////////////////////////////////////////////////
// CAgUiAx2DCntrl properties

/////////////////////////////////////////////////////////////////////////////
// CAgUiAx2DCntrl operations

void CAgUiAx2DCntrl::SetBackColor(unsigned long newValue)
{
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(DISPID_BACKCOLOR, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, parms,
		 newValue);
}

unsigned long CAgUiAx2DCntrl::GetBackColor()
{
	unsigned long result;
	InvokeHelper(DISPID_BACKCOLOR, DISPATCH_PROPERTYGET, VT_I4, (void*)&result, NULL);
	return result;
}

void CAgUiAx2DCntrl::SetRefPicture(LPDISPATCH newValue)
{
	static BYTE parms[] =
		VTS_DISPATCH;
	InvokeHelper(0xfffffdf5, DISPATCH_PROPERTYPUTREF, VT_EMPTY, NULL, parms,
		 newValue);
}

void CAgUiAx2DCntrl::SetPicture(LPDISPATCH newValue)
{
	static BYTE parms[] =
		VTS_DISPATCH;
	InvokeHelper(0xfffffdf5, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, parms,
		 newValue);
}

CPicture CAgUiAx2DCntrl::GetPicture()
{
	LPDISPATCH pDispatch;
	InvokeHelper(0xfffffdf5, DISPATCH_PROPERTYGET, VT_DISPATCH, (void*)&pDispatch, NULL);
	return CPicture(pDispatch);
}

long CAgUiAx2DCntrl::GetWinID()
{
	long result;
	InvokeHelper(0x66, DISPATCH_PROPERTYGET, VT_I4, (void*)&result, NULL);
	return result;
}

void CAgUiAx2DCntrl::SetWinID(long nNewValue)
{
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x66, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, parms,
		 nNewValue);
}

void CAgUiAx2DCntrl::ZoomIn()
{
	InvokeHelper(0x1, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
}

void CAgUiAx2DCntrl::ZoomOut()
{
	InvokeHelper(0x2, DISPATCH_METHOD, VT_EMPTY, NULL, NULL);
}

CAgPickInfoData CAgUiAx2DCntrl::PickInfo(long x, long y)
{
	LPDISPATCH pDispatch;
	static BYTE parms[] =
		VTS_I4 VTS_I4;
	InvokeHelper(0x3, DISPATCH_METHOD, VT_DISPATCH, (void*)&pDispatch, parms,
		x, y);
	return CAgPickInfoData(pDispatch);
}

CAgSTKXApplication CAgUiAx2DCntrl::GetApplication()
{
	LPDISPATCH pDispatch;
	InvokeHelper(0x4, DISPATCH_PROPERTYGET, VT_DISPATCH, (void*)&pDispatch, NULL);
	return CAgSTKXApplication(pDispatch);
}

BOOL CAgUiAx2DCntrl::GetAllowScrollbars()
{
	BOOL result;
	InvokeHelper(0x67, DISPATCH_PROPERTYGET, VT_BOOL, (void*)&result, NULL);
	return result;
}

void CAgUiAx2DCntrl::SetAllowScrollbars(BOOL bNewValue)
{
	static BYTE parms[] =
		VTS_BOOL;
	InvokeHelper(0x67, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, parms,
		 bNewValue);
}

BOOL CAgUiAx2DCntrl::GetNoLogo()
{
	BOOL result;
	InvokeHelper(0x69, DISPATCH_PROPERTYGET, VT_BOOL, (void*)&result, NULL);
	return result;
}

void CAgUiAx2DCntrl::SetNoLogo(BOOL bNewValue)
{
	static BYTE parms[] =
		VTS_BOOL;
	InvokeHelper(0x69, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, parms,
		 bNewValue);
}

long CAgUiAx2DCntrl::GetOLEDropMode()
{
	long result;
	InvokeHelper(0x6a, DISPATCH_PROPERTYGET, VT_I4, (void*)&result, NULL);
	return result;
}

void CAgUiAx2DCntrl::SetOLEDropMode(long nNewValue)
{
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x6a, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, parms,
		 nNewValue);
}

void CAgUiAx2DCntrl::SetVendorID(LPCTSTR lpszNewValue)
{
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x68, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, parms,
		 lpszNewValue);
}

CString CAgUiAx2DCntrl::GetVendorID()
{
	CString result;
	InvokeHelper(0x68, DISPATCH_PROPERTYGET, VT_BSTR, (void*)&result, NULL);
	return result;
}

long CAgUiAx2DCntrl::GetMouseMode()
{
	long result;
	InvokeHelper(0x6b, DISPATCH_PROPERTYGET, VT_I4, (void*)&result, NULL);
	return result;
}

void CAgUiAx2DCntrl::SetMouseMode(long nNewValue)
{
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x6b, DISPATCH_PROPERTYPUT, VT_EMPTY, NULL, parms,
		 nNewValue);
}

void CAgUiAx2DCntrl::CopyFromWinID(long WinID)
{
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x5, DISPATCH_METHOD, VT_EMPTY, NULL, parms,
		 WinID);
}