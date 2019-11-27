
#pragma once

//#include "stdafx.h"
#include <windows.h>
#include "cemapi.h"
#include "mapiutil.h"

#define AFX_EXT_CLASS __declspec(dllexport)

typedef void (*AdviseSinkCallback)(IMsgStore *store, LPNOTIFICATION lpNotifications);

class MAPIAdviseSink : public IMAPIAdviseSink
{
public:
	MAPIAdviseSink(IMsgStore *store, ULONG eventMask, AdviseSinkCallback callback);

	virtual ULONG OnNotify(ULONG cNotif, LPNOTIFICATION lpNotifications);

	virtual ULONG AddRef() { return ++refs; }

	virtual HRESULT QueryInterface( REFIID iid, void ** ppvObject) { return E_NOTIMPL; }

	virtual ULONG Release();

	HRESULT prevResult;

private:
	ULONG refs, msgNum, conNum;
	AdviseSinkCallback callback;
	IMsgStore *store;
};

extern "C" {

AFX_EXT_CLASS HRESULT MAPIlibAdvise(IMsgStore *store, ULONG eventMask, AdviseSinkCallback callback, IMAPIAdviseSink **adviser);

AFX_EXT_CLASS HRESULT MAPIlibUnadvise(MAPIAdviseSink *adviser);

AFX_EXT_CLASS HRESULT MAPIlibLogon(IMAPISession **pSession);

AFX_EXT_CLASS HRESULT Release(IUnknown *i);

/* IMAPISession */
AFX_EXT_CLASS HRESULT IMAPISessionLogoff(IMAPISession *pSession);

AFX_EXT_CLASS HRESULT IMAPISessionGetMsgStoresTable(IMAPISession *pSession, IMAPITable **table);

AFX_EXT_CLASS HRESULT IMAPISessionOpenMsgStore(IMAPISession *pSession, SBinary *entryId, IMsgStore **msgStore);

AFX_EXT_CLASS HRESULT IMAPISessionCompareEntryIDs(IMAPISession *pSession, SBinary *entryID1, SBinary *entryID2, ULONG * lpulResult);

/* IMAPITable */
AFX_EXT_CLASS HRESULT IMAPITableGetRowCount(IMAPITable *table, ULONG *lpulCount);

AFX_EXT_CLASS HRESULT IMAPITableSetColumns(IMAPITable *table, LPSPropTagArray columns);

AFX_EXT_CLASS HRESULT IMAPITableQueryRows(IMAPITable *table, LONG lRowCount, LPSRowSet *pRows);

AFX_EXT_CLASS HRESULT IMAPITableSortTable(IMAPITable *table, LPSSortOrderSet lpSortCriteria);

AFX_EXT_CLASS HRESULT IMAPITableSeekRow(IMAPITable *table, LONG lRowCount);

AFX_EXT_CLASS HRESULT IMAPITableQueryPosition(IMAPITable *table, ULONG *lpulRow);

/* IMAPIProp */
AFX_EXT_CLASS HRESULT IMAPIPropGetProps(IMAPIProp *prop, LPSPropTagArray tags, ULONG *count/*out*/, LPSPropValue *lppPropArray);

AFX_EXT_CLASS HRESULT IMAPIPropSetProps(IMAPIProp *prop, ULONG cValues, LPSPropValue lpPropArray);

AFX_EXT_CLASS HRESULT IMAPIPropOpenProperty(IMAPIProp *prop, ULONG ulPropTag, ULONG flags, IUnknown **lppUnk);

/* IMsgStore */
AFX_EXT_CLASS HRESULT IMsgStoreOpenEntry(IMsgStore *store, SBinary *entryId, ULONG *ulObjType, IUnknown **lppUnk);

AFX_EXT_CLASS HRESULT IMsgStoreGetReceiveFolder(IMsgStore *store, ULONG *lpcbEntryID, LPENTRYID *lppEntryID);

/* IMAPIContainer */
AFX_EXT_CLASS HRESULT IMAPIContainerGetContentsTable(IMAPIContainer *container, IMAPITable **table);

AFX_EXT_CLASS HRESULT IMAPIContainerGetHierarchyTable(IMAPIContainer *container, IMAPITable **table);

AFX_EXT_CLASS HRESULT IMAPIContainerOpenEntry(IMAPIContainer *container, SBinary *entryID, ULONG ulObjType, IUnknown **lppUnk);

/* IMAPIFolder */
AFX_EXT_CLASS HRESULT IMAPIFolderDeleteMessages(IMAPIFolder *folder, LPENTRYLIST lpMsgList);

AFX_EXT_CLASS HRESULT IMAPIFolderCreateFolder(IMAPIFolder *folder, LPTSTR lpszFolderName, ULONG flags, IMAPIFolder **newFolder);

AFX_EXT_CLASS HRESULT IMAPIFolderDeleteFolder(IMAPIFolder *folder, ULONG cbEntryID, LPENTRYID lpEntryID, ULONG flags);

AFX_EXT_CLASS HRESULT IMAPIFolderCopyMessages(IMAPIFolder *folder, LPENTRYLIST lpMsgList, LPVOID lpDestFolder, ULONG flags);

AFX_EXT_CLASS HRESULT IMAPIFolderEmptyFolder(IMAPIFolder *folder);

/* IMessage */
AFX_EXT_CLASS HRESULT IMessageGetRecipientTable(IMessage *msg, IMAPITable **table);

AFX_EXT_CLASS HRESULT IMessageGetAttachmentTable(IMessage *msg, IMAPITable **table);

AFX_EXT_CLASS HRESULT IMessageOpenAttach(IMessage *msg, ULONG num, IAttach **attachment);

} // extern "C"
