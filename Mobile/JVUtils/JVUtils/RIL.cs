using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace JVUtils
{
    public class RIL
    {
        private static RILCELLTOWERINFO _cellTowerInfo;
        private static RILEQUIPMENTINFO _equipmentInfo;
        private static RILSIGNALQUALITY _signalQuality;
        private static RILSUBSCRIBERINFO _subscriberInfo;
        private static bool _vInitialized = false;
        private static RILOPERATORNAMES _vOperatore;
        private static int _vTimeOut = 4;
        public static RIL_EVENTS EVENTS = new RIL_EVENTS();
        private static IntPtr hRil;
        private const int MAXLENGTH_ADDRESS = 0x100;
        private const int MAXLENGTH_BCCH = 0x30;
        private const int MAXLENGTH_DESCRIPTION = 0x100;
        private const int MAXLENGTH_EQUIPINFO = 0x80;
        private const int MAXLENGTH_NMR = 0x10;
        private const int MAXLENGTH_OPERATOR_COUNTRY_CODE = 8;
        private const int MAXLENGTH_OPERATOR_LONG = 0x20;
        private const int MAXLENGTH_OPERATOR_NUMERIC = 0x10;
        private const int MAXLENGTH_OPERATOR_SHORT = 0x10;
        private static RILNOTIFYCALLBACK NotifyCallback;
        private static IntPtr NotifyCallbackPointer;
        private static RILRESULTCALLBACK ResultCallback;
        private static IntPtr ResultCallbackPointer;
        private const string RIL_CMD_TIMEOUT = "Timeout";
        private const int RIL_NCLASS_MISC = 0x400000;
        private const int RIL_NOTIFY_SIGNALQUALITY = 0x400005;
        private const int RIL_PARAM_A_ADDRESS = 4;
        private const int RIL_PARAM_A_ALL = 7;
        private const int RIL_PARAM_A_NUMPLAN = 2;
        private const int RIL_PARAM_A_TYPE = 1;
        private const int RIL_PARAM_CTI_ALL = 0x7ffff;
        private const int RIL_PARAM_CTI_BASESTATIONID = 0x10;
        private const int RIL_PARAM_CTI_BCCH = 0x40000;
        private const int RIL_PARAM_CTI_BROADCASTCONTROLCHANNEL = 0x20;
        private const int RIL_PARAM_CTI_CELLID = 8;
        private const int RIL_PARAM_CTI_GPRSBASESTATIONID = 0x8000;
        private const int RIL_PARAM_CTI_GPRSCELLID = 0x4000;
        private const int RIL_PARAM_CTI_IDLETIMESLOT = 0x1000;
        private const int RIL_PARAM_CTI_LOCATIONAREACODE = 4;
        private const int RIL_PARAM_CTI_MOBILECOUNTRYCODE = 1;
        private const int RIL_PARAM_CTI_MOBILENETWORKCODE = 2;
        private const int RIL_PARAM_CTI_NMR = 0x20000;
        private const int RIL_PARAM_CTI_NUMBCCH = 0x10000;
        private const int RIL_PARAM_CTI_RXLEVEL = 0x40;
        private const int RIL_PARAM_CTI_RXLEVELFULL = 0x80;
        private const int RIL_PARAM_CTI_RXLEVELSUB = 0x100;
        private const int RIL_PARAM_CTI_RXQUALITY = 0x200;
        private const int RIL_PARAM_CTI_RXQUALITYFULL = 0x400;
        private const int RIL_PARAM_CTI_RXQUALITYSUB = 0x800;
        private const int RIL_PARAM_CTI_TIMINGADVANCE = 0x2000;
        private const int RIL_PARAM_EI_ALL = 15;
        private const int RIL_PARAM_EI_MANUFACTURER = 1;
        private const int RIL_PARAM_EI_MODEL = 2;
        private const int RIL_PARAM_EI_REVISION = 4;
        private const int RIL_PARAM_EI_SERIALNUMBER = 8;
        private const string RIL_PARAM_NOT_AVAILABLE = "0";
        private const int RIL_PARAM_ON_ALL = 0x7f;
        private const int RIL_PARAM_ON_COUNTRY_CODE = 8;
        private const int RIL_PARAM_ON_GSM_ACT = 0x10;
        private const int RIL_PARAM_ON_GSMCOMPACT_ACT = 0x20;
        private const int RIL_PARAM_ON_LONGNAME = 1;
        private const int RIL_PARAM_ON_NUMNAME = 4;
        private const int RIL_PARAM_ON_SHORTNAME = 2;
        private const int RIL_PARAM_ON_UMTS_ACT = 0x40;
        private const int RIL_PARAM_SI_ADDRESS = 1;
        private const int RIL_PARAM_SI_ADDRESSID = 0x20;
        private const int RIL_PARAM_SI_ALL = 0x3f;
        private const int RIL_PARAM_SI_DESCRIPTION = 2;
        private const int RIL_PARAM_SI_ITC = 0x10;
        private const int RIL_PARAM_SI_SERVICE = 8;
        private const int RIL_PARAM_SI_SPEED = 4;
        private const int RIL_PARAM_SQ_ALL = 0x3f;
        private const int RIL_PARAM_SQ_BITERRORRATE = 8;
        private const int RIL_PARAM_SQ_HIGHSIGNALSTRENGTH = 0x20;
        private const int RIL_PARAM_SQ_LOWSIGNALSTRENGTH = 0x10;
        private const int RIL_PARAM_SQ_MAXSIGNALSTRENGTH = 4;
        private const int RIL_PARAM_SQ_MINSIGNALSTRENGTH = 2;
        private const int RIL_PARAM_SQ_SIGNALSTRENGTH = 1;
        private static RIL_RESULT ril_result;
        private static RIL_CMD rilCmd = new RIL_CMD(0, RIL_CMD_TYPE.NONE);

        private static void _NotifyCallback(IntPtr dwCode, IntPtr lpData, IntPtr cbData, IntPtr dwParam)
        {
            int CSS0S0000 = (int) dwCode;
            if (CSS0S0000 == 0x400005)
            {
                RILSIGNALQUALITY _signalQuality = (RILSIGNALQUALITY) Marshal.PtrToStructure(lpData, typeof(RILSIGNALQUALITY));
                EVENTS.RiseSignalQualityChanged(new SIGNALQUALITY(
                    ((_signalQuality.dwParams & 1) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nSignalStrength.ToString(), 
                    ((_signalQuality.dwParams & 2) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nMinSignalStrength.ToString(), 
                    ((_signalQuality.dwParams & 4) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nMaxSignalStrength.ToString(), 
                    ((_signalQuality.dwParams & 8) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.dwBitErrorRate.ToString(), 
                    ((_signalQuality.dwParams & 0x10) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nLowSignalStrength.ToString(), 
                    ((_signalQuality.dwParams & 0x20) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nHighSignalStrength.ToString()));
            }
        }

        private static void _ResultCallback(IntPtr dwCode, IntPtr hrCmdID, IntPtr lpData, IntPtr cbData, IntPtr dwParam)
        {
            if ((rilCmd.CmdType != RIL_CMD_TYPE.NONE) && (rilCmd.CmdId == ((int) hrCmdID)))
            {
                if (((int) dwCode) == 1)
                {
                    switch (rilCmd.CmdType)
                    {
                        case RIL_CMD_TYPE.OPERATOR:
                            _vOperatore = (RILOPERATORNAMES) Marshal.PtrToStructure(lpData, typeof(RILOPERATORNAMES));
                            break;

                        case RIL_CMD_TYPE.CELLTOWERINFO:
                            _cellTowerInfo = (RILCELLTOWERINFO) Marshal.PtrToStructure(lpData, typeof(RILCELLTOWERINFO));
                            break;

                        case RIL_CMD_TYPE.SIGNALQUALITY:
                            _signalQuality = (RILSIGNALQUALITY) Marshal.PtrToStructure(lpData, typeof(RILSIGNALQUALITY));
                            break;

                        case RIL_CMD_TYPE.EQUIPMENTINFO:
                            _equipmentInfo = (RILEQUIPMENTINFO) Marshal.PtrToStructure(lpData, typeof(RILEQUIPMENTINFO));
                            break;

                        case RIL_CMD_TYPE.SUBSCRIBERINFO:
                            _subscriberInfo = (RILSUBSCRIBERINFO) Marshal.PtrToStructure(lpData, typeof(RILSUBSCRIBERINFO));
                            break;
                    }
                }
                ril_result = (RIL_RESULT) ((int) dwCode);
                rilCmd.CmdType = RIL_CMD_TYPE.NONE;
            }
        }

        public static void Deinitialize()
        {
            if (_vInitialized)
            {
                if (((int)RIL_Deinitialize(hRil)) >= 0)
                {
                    _vInitialized = false;
                }
                else
                {
                    Debug.AddLog("ERROR deinitializing RIL");
                }
            }
            else
            {
                Debug.AddLog("RIL not initialized");
            }
        }

        public static CELLINFO GetCellTowerInfo()
        {
            IntPtr res = IntPtr.Zero;
            res = RIL_GetCellTowerInfo(hRil);
            if (((int) res) <= 0)
            {
                Debug.AddLog("ERROR getting GetCellTowerInfo");
                return null;
            }

            rilCmd.CmdId = (int) res;
            rilCmd.CmdType = RIL_CMD_TYPE.CELLTOWERINFO;
            for (int mult = 0; (mult < (_vTimeOut * 0x3e8)) && (rilCmd.CmdType != RIL_CMD_TYPE.NONE); mult += 100)
            {
                Thread.Sleep(100);
            }
            
            if (rilCmd.CmdType != RIL_CMD_TYPE.NONE)
            {
                Debug.AddLog("ERROR GetCellTowerInfo Timeout");
                return new CELLINFO("Timeout", "Timeout", "Timeout", "Timeout", "Timeout", "Timeout", "Timeout");
            }
            
            if (ril_result != RIL_RESULT.OK)
            {
                Debug.AddLog("GetCellTowerInfo ok: " + ril_result.ToString());
                return new CELLINFO(ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString());
            }

            string cellID = (_cellTowerInfo.dwCellID.ToString().Equals("0") ? _cellTowerInfo.dwGPRSCellID.ToString() : _cellTowerInfo.dwCellID.ToString());
            string lac = (_cellTowerInfo.dwLocationAreaCode.ToString().Equals("0") ? _cellTowerInfo.dwGPRSBaseStationID.ToString() : _cellTowerInfo.dwLocationAreaCode.ToString());

            Debug.AddLog("GetCellTowerInfo: " +
                (((_cellTowerInfo.dwParams & 1) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwMobileCountryCode.ToString()) + ", " +
                (((_cellTowerInfo.dwParams & 2) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwMobileNetworkCode.ToString()) + ", " +
                (((_cellTowerInfo.dwParams & 4) == 0) ? RIL_PARAM_NOT_AVAILABLE : lac) + ", " +
                (((_cellTowerInfo.dwParams & 8) == 0) ? RIL_PARAM_NOT_AVAILABLE : cellID) + ", " +
                (((_cellTowerInfo.dwParams & 0x40) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwRxLevel.ToString()) + ", " +
                (((_cellTowerInfo.dwParams & 0x200) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwRxQuality.ToString()) + ", " +
                (((_cellTowerInfo.dwParams & 0x40000) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwNumBCCH.ToString()));
            Debug.AddLog("GetCellTowerInfo data: " +
                         "MCC: " + _cellTowerInfo.dwMobileCountryCode.ToString() + ", " +
                         "MNC: " + _cellTowerInfo.dwMobileNetworkCode.ToString() + ", " +
                         "CellID: " + _cellTowerInfo.dwCellID.ToString() + ", " +
                         "GPRSCellID: " + _cellTowerInfo.dwGPRSCellID.ToString() + ", " +
                         "LAC: " + _cellTowerInfo.dwLocationAreaCode.ToString() + ", " +
                         "GPRSBSID: " + _cellTowerInfo.dwGPRSBaseStationID.ToString() + ", " +
                         "Level: " + _cellTowerInfo.dwRxLevel.ToString() + ", " +
                         "Quality: " + _cellTowerInfo.dwRxQuality.ToString() + ", " +
                         "NumBCCH: " + _cellTowerInfo.dwNumBCCH.ToString());

            return new CELLINFO(((_cellTowerInfo.dwParams & 1) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwMobileCountryCode.ToString(),
                                ((_cellTowerInfo.dwParams & 2) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwMobileNetworkCode.ToString(),
                                ((_cellTowerInfo.dwParams & 4) == 0) ? RIL_PARAM_NOT_AVAILABLE : lac,
                                ((_cellTowerInfo.dwParams & 8) == 0) ? RIL_PARAM_NOT_AVAILABLE : cellID,
                                ((_cellTowerInfo.dwParams & 0x40) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwRxLevel.ToString(),
                                ((_cellTowerInfo.dwParams & 0x200) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwRxQuality.ToString(),
                                ((_cellTowerInfo.dwParams & 0x40000) == 0) ? RIL_PARAM_NOT_AVAILABLE : _cellTowerInfo.dwNumBCCH.ToString());
        }

        public static OPERATORNAMES GetCurrentOperator(RIL_OPFORMAT format)
        {
            IntPtr res = IntPtr.Zero;
            res = RIL_GetCurrentOperator(hRil, new IntPtr((int) format));
            if (((int) res) <= 0)
            {
                Debug.AddLog("ERROR GetCurrentOperator");
                return null;
            }

            rilCmd.CmdId = (int) res;
            rilCmd.CmdType = RIL_CMD_TYPE.OPERATOR;
            for (int mult = 0; (mult < (_vTimeOut * 0x3e8)) && (rilCmd.CmdType != RIL_CMD_TYPE.NONE); mult += 100)
            {
                Thread.Sleep(100);
            }

            if (rilCmd.CmdType != RIL_CMD_TYPE.NONE)
            {
                Debug.AddLog("ERROR GetCurrentOperator Timeout");
                return new OPERATORNAMES("Timeout", "Timeout", "Timeout", "Timeout");
            }
            
            if (ril_result != RIL_RESULT.OK)
            {
                Debug.AddLog("GetCurrentOperator Ok: " + ril_result.ToString());
                return new OPERATORNAMES(ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString());
            }

            Debug.AddLog("GetCurrentOperator: " +
                (((_vOperatore.dwParams & 1) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_vOperatore.szLongName, 0, _vOperatore.szLongName.Length).Replace("\0", "")) + ", " +
                (((_vOperatore.dwParams & 2) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_vOperatore.szShortName, 0, _vOperatore.szShortName.Length).Replace("\0", "")) + ", " +
                (((_vOperatore.dwParams & 4) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_vOperatore.szNumName, 0, _vOperatore.szNumName.Length).Replace("\0", "")) + ", " +
                (((_vOperatore.dwParams & 8) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_vOperatore.szCountryCode, 0, _vOperatore.szCountryCode.Length).Replace("\0", "")));
            Debug.AddLog("GetCurrentOperator data: " +
                "LongName: " + Encoding.ASCII.GetString(_vOperatore.szLongName, 0, _vOperatore.szLongName.Length).Replace("\0", "") + ", " +
                "ShortName: " + Encoding.ASCII.GetString(_vOperatore.szShortName, 0, _vOperatore.szShortName.Length).Replace("\0", "") + ", " +
                "NumName: " + Encoding.ASCII.GetString(_vOperatore.szNumName, 0, _vOperatore.szNumName.Length).Replace("\0", "") + ", " +
                "CountryCode: " + Encoding.ASCII.GetString(_vOperatore.szCountryCode, 0, _vOperatore.szCountryCode.Length).Replace("\0", ""));

            return new OPERATORNAMES(((_vOperatore.dwParams & 1) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_vOperatore.szLongName, 0, _vOperatore.szLongName.Length).Replace("\0", ""),
                                     ((_vOperatore.dwParams & 2) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_vOperatore.szShortName, 0, _vOperatore.szShortName.Length).Replace("\0", ""),
                                     ((_vOperatore.dwParams & 4) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_vOperatore.szNumName, 0, _vOperatore.szNumName.Length).Replace("\0", ""),
                                     ((_vOperatore.dwParams & 8) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_vOperatore.szCountryCode, 0, _vOperatore.szCountryCode.Length).Replace("\0", ""));
        }

        public static EQUIPMENTINFO GetEquipmentInfo()
        {
            IntPtr res = IntPtr.Zero;
            res = RIL_GetEquipmentInfo(hRil);
            if (((int) res) <= 0)
            {
                return null;
            }
            rilCmd.CmdId = (int) res;
            rilCmd.CmdType = RIL_CMD_TYPE.EQUIPMENTINFO;
            for (int mult = 0; (mult < (_vTimeOut * 0x3e8)) && (rilCmd.CmdType != RIL_CMD_TYPE.NONE); mult += 100)
            {
                Thread.Sleep(100);
            }
            if (rilCmd.CmdType != RIL_CMD_TYPE.NONE)
            {
                return new EQUIPMENTINFO("Timeout", "Timeout", "Timeout", "Timeout");
            }
            if (ril_result != RIL_RESULT.OK)
            {
                return new EQUIPMENTINFO(ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString());
            }
            return new EQUIPMENTINFO(
                ((_equipmentInfo.dwParams & 1) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_equipmentInfo.szManufacturer, 0, _equipmentInfo.szManufacturer.Length).Replace("\0", ""),
                ((_equipmentInfo.dwParams & 2) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_equipmentInfo.szModel, 0, _equipmentInfo.szModel.Length).Replace("\0", ""),
                ((_equipmentInfo.dwParams & 4) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_equipmentInfo.szRevision, 0, _equipmentInfo.szRevision.Length).Replace("\0", ""),
                ((_equipmentInfo.dwParams & 8) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_equipmentInfo.szSerialNumber, 0, _equipmentInfo.szSerialNumber.Length).Replace("\0", ""));
        }

        public static SIGNALQUALITY GetSignalQuality()
        {
            IntPtr res = IntPtr.Zero;
            res = RIL_GetSignalQuality(hRil);
            if (((int) res) <= 0)
            {
                return null;
            }
            rilCmd.CmdId = (int) res;
            rilCmd.CmdType = RIL_CMD_TYPE.SIGNALQUALITY;
            for (int mult = 0; (mult < (_vTimeOut * 0x3e8)) && (rilCmd.CmdType != RIL_CMD_TYPE.NONE); mult += 100)
            {
                Thread.Sleep(100);
            }
            if (rilCmd.CmdType != RIL_CMD_TYPE.NONE)
            {
                return new SIGNALQUALITY("Timeout", "Timeout", "Timeout", "Timeout", "Timeout", "Timeout");
            }
            if (ril_result != RIL_RESULT.OK)
            {
                return new SIGNALQUALITY(ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString());
            }
            return new SIGNALQUALITY(
                ((_signalQuality.dwParams & 1) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nSignalStrength.ToString(),
                ((_signalQuality.dwParams & 2) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nMinSignalStrength.ToString(),
                ((_signalQuality.dwParams & 4) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nMaxSignalStrength.ToString(),
                ((_signalQuality.dwParams & 8) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.dwBitErrorRate.ToString(),
                ((_signalQuality.dwParams & 0x10) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nLowSignalStrength.ToString(),
                ((_signalQuality.dwParams & 0x20) == 0) ? RIL_PARAM_NOT_AVAILABLE : _signalQuality.nHighSignalStrength.ToString());
        }

        public static SUBSCRIBERINFO GetSubscriberInfo()
        {
            IntPtr res = IntPtr.Zero;
            res = RIL_GetSubscriberNumbers(hRil);
            if (((int) res) <= 0)
            {
                return null;
            }
            rilCmd.CmdId = (int) res;
            rilCmd.CmdType = RIL_CMD_TYPE.SUBSCRIBERINFO;
            for (int mult = 0; (mult < (_vTimeOut * 0x3e8)) && (rilCmd.CmdType != RIL_CMD_TYPE.NONE); mult += 100)
            {
                Thread.Sleep(100);
            }
            if (rilCmd.CmdType != RIL_CMD_TYPE.NONE)
            {
                return new SUBSCRIBERINFO("Timeout", "Timeout", "Timeout", "Timeout", "Timeout", "Timeout");
            }
            if (ril_result != RIL_RESULT.OK)
            {
                return new SUBSCRIBERINFO(ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString(), ril_result.ToString());
            }
            return new SUBSCRIBERINFO(
                ((_subscriberInfo.dwParams & 1) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_subscriberInfo.wszAddress, 0, _subscriberInfo.wszAddress.Length).Replace("\0", ""),
                ((_subscriberInfo.dwParams & 2) == 0) ? RIL_PARAM_NOT_AVAILABLE : Encoding.ASCII.GetString(_subscriberInfo.wszDescription, 0, _subscriberInfo.wszDescription.Length).Replace("\0", ""),
                ((_subscriberInfo.dwParams & 4) == 0) ? RIL_PARAM_NOT_AVAILABLE : _subscriberInfo.dwSpeed.ToString(),
                ((_subscriberInfo.dwParams & 8) == 0) ? RIL_PARAM_NOT_AVAILABLE : _subscriberInfo.dwService.ToString(),
                ((_subscriberInfo.dwParams & 0x10) == 0) ? RIL_PARAM_NOT_AVAILABLE : _subscriberInfo.dwITC.ToString(),
                ((_subscriberInfo.dwParams & 0x20) == 0) ? RIL_PARAM_NOT_AVAILABLE : _subscriberInfo.dwAddressId.ToString());
        }

        public static CellIDInformations GetAllInformations()
        {
            CellIDInformations cid = new CellIDInformations();
            cid.cellID = "";
            cid.mobileCountryCode = "";
            cid.mobileNetworkCode = "";
            cid.operatorName = "";
            cid.localAreaCode = "";

            if (!isInitialized)
            {
                Debug.AddLog("Initialing RIL", true);
                Initialize();
            }

            CELLINFO CID = GetCellTowerInfo();
            if (CID != null)
            {
                cid.cellID = CID.CellID;
                cid.localAreaCode = CID.LocationAreaCode;
                cid.mobileCountryCode = CID.MobileCountryCode;
                cid.mobileNetworkCode = CID.MobileNetworkCode;

                try
                {
                    SIGNALQUALITY sq = GetSignalQuality();

                    Debug.AddLog("Signal Quality:" +
                                  " HighSignalStrength: " + sq.HighSignalStrength.ToString() +
                                  " LowSignalStrength: " + sq.LowSignalStrength.ToString() +
                                  " MaxSignalStrength: " + sq.MaxSignalStrength.ToString() +
                                  " MinSignalStrength: " + sq.MinSignalStrength.ToString() +
                                  " SignalStrength: " + sq.SignalStrength.ToString());
                    cid.highSignalStrength = Utils.RemoveChar(sq.HighSignalStrength.ToString(), '-');
                    cid.lowSignalStrength = Utils.RemoveChar(sq.LowSignalStrength.ToString(), '-');
                    cid.maxSignalStrength = Utils.RemoveChar(sq.MaxSignalStrength.ToString(), '-');
                    cid.minSignalStrength = Utils.RemoveChar(sq.MinSignalStrength.ToString(), '-');
                    cid.signalStrength = Utils.RemoveChar(sq.SignalStrength.ToString(), '-');
                }
                catch (Exception e)
                {
                    Debug.AddLog("Signal Quality: ERROR: " + e.Message);
                    cid.highSignalStrength = "0";
                    cid.lowSignalStrength = "0";
                    cid.maxSignalStrength = "0";
                    cid.minSignalStrength = "0";
                    cid.signalStrength = "0";
                }
            }
            else
            {
                Debug.AddLog("ERROR: cellid not found");
            }

            if (cid.mobileCountryCode.Equals(RIL_PARAM_NOT_AVAILABLE) || 
                cid.mobileNetworkCode.Equals(RIL_PARAM_NOT_AVAILABLE))
            {
                OPERATORNAMES network = GetCurrentOperator(RIL_OPFORMAT.NUM);

                if (network != null)
                {
                    cid.operatorName = network.ShortName;
                    cid.mobileCountryCode = network.NumName.Substring(0, 3);
                    cid.mobileNetworkCode = network.NumName.Substring(3);
                }
                else
                {
                    Debug.AddLog("ERROR: network not found");
                }
            }

            Debug.AddLog("Deinitialing RIL", true);
            Deinitialize();

            return cid;
        }

        public static bool Initialize()
        {
            if (_vInitialized)
            {
                Debug.AddLog("RIL already initialized");
                return false;
            }

            IntPtr port = new IntPtr(1);
            hRil = IntPtr.Zero;
            NotifyCallback = new RILNOTIFYCALLBACK(RIL._NotifyCallback);
            NotifyCallbackPointer = Marshal.GetFunctionPointerForDelegate(NotifyCallback);
            ResultCallback = new RILRESULTCALLBACK(RIL._ResultCallback);
            ResultCallbackPointer = Marshal.GetFunctionPointerForDelegate(ResultCallback);
            IntPtr dwNotif = new IntPtr(0); // (0xff0000);
            IntPtr dwParam = new IntPtr(0); // (0x55aa55aa);

            if (RIL_Initialize(port, ResultCallbackPointer, NotifyCallbackPointer, dwNotif, dwParam, out hRil) != IntPtr.Zero)
            {
                Debug.AddLog("ERROR initializing RIL");
                return false;
            }

            _vInitialized = true;
            return true;
        }

        [DllImport("ril.dll")]
        private static extern IntPtr RIL_Deinitialize(IntPtr lphRil);
        [DllImport("ril.dll")]
        private static extern IntPtr RIL_GetCellTowerInfo(IntPtr hRil);
        [DllImport("ril.dll")]
        private static extern IntPtr RIL_GetCurrentOperator(IntPtr hRil, IntPtr dwFormat);
        [DllImport("ril.dll")]
        private static extern IntPtr RIL_GetEquipmentInfo(IntPtr hRil);
        [DllImport("ril.dll")]
        private static extern IntPtr RIL_GetSignalQuality(IntPtr hRil);
        [DllImport("ril.dll")]
        private static extern IntPtr RIL_GetSubscriberNumbers(IntPtr hRil);
        [DllImport("ril.dll")]
        private static extern IntPtr RIL_Initialize(IntPtr dwIndex, IntPtr pfnResult, IntPtr pfnNotify, IntPtr dwNotificationClasses, IntPtr dwParam, out IntPtr lphRil);

        public static bool isInitialized
        {
            get
            {
                return _vInitialized;
            }
        }

        public class CELLINFO
        {
            public string BCCH;
            public string CellID;
            public string LocationAreaCode;
            public string MobileCountryCode;
            public string MobileNetworkCode;
            public string RxLevel;
            public string RxQuality;

            public CELLINFO(string MobileCountryCode, string MobileNetworkCode, string LocationAreaCode, string CellID, string RxLevel, string RxQuality, string BCCH)
            {
                this.MobileCountryCode = MobileCountryCode;
                this.MobileNetworkCode = MobileNetworkCode;
                this.LocationAreaCode = LocationAreaCode;
                this.CellID = CellID;
                this.RxLevel = RxLevel;
                this.RxQuality = RxQuality;
                this.BCCH = BCCH;
            }
        }

        public class EQUIPMENTINFO
        {
            public string Manufacturer;
            public string Model;
            public string Revision;
            public string SerialNumber;

            public EQUIPMENTINFO(string Manufacturer, string Model, string Revision, string SerialNumber)
            {
                this.Manufacturer = Manufacturer;
                this.Model = Model;
                this.Revision = Revision;
                this.SerialNumber = SerialNumber;
            }
        }

        public class OPERATORNAMES
        {
            public string CountryCode;
            public string LongName;
            public string NumName;
            public string ShortName;

            public OPERATORNAMES(string LongName, string ShortName, string NumName, string CountryCode)
            {
                this.LongName = LongName;
                this.ShortName = ShortName;
                this.NumName = NumName;
                this.CountryCode = CountryCode;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RIL_CMD
        {
            private int _cmdId;
            private RIL.RIL_CMD_TYPE _cmdType;
            public int CmdId
            {
                get
                {
                    return this._cmdId;
                }
                set
                {
                    this._cmdId = value;
                }
            }
            public RIL.RIL_CMD_TYPE CmdType
            {
                get
                {
                    return this._cmdType;
                }
                set
                {
                    this._cmdType = value;
                }
            }
            public RIL_CMD(int cmdId, RIL.RIL_CMD_TYPE cmdType)
            {
                this._cmdId = cmdId;
                this._cmdType = cmdType;
            }
        }

        private enum RIL_CMD_TYPE
        {
            NONE,
            OPERATOR,
            CELLTOWERINFO,
            SIGNALQUALITY,
            EQUIPMENTINFO,
            SUBSCRIBERINFO
        }

        public enum RIL_OPFORMAT
        {
            LONG = 1,
            NUM = 3,
            SHORT = 2
        }

        private enum RIL_RESULT
        {
            BUSY = 5,
            CALLABORTED = 7,
            ERROR = 3,
            NOANSWER = 6,
            NOCARRIER = 2,
            NODIALTONE = 4,
            OK = 1
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RILADDRESS
        {
            public uint cbSize;
            public uint dwParams;
            public uint dwType;
            public uint dwNumPlan;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x100)]
            public byte[] wszAddress;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RILCELLTOWERINFO
        {
            public uint cbSize;
            public uint dwParams;
            public uint dwMobileCountryCode;
            public uint dwMobileNetworkCode;
            public uint dwLocationAreaCode;
            public uint dwCellID;
            public uint dwBaseStationID;
            public uint dwBroadcastControlChannel;
            public uint dwRxLevel;
            public uint dwRxLevelFull;
            public uint dwRxLevelSub;
            public uint dwRxQuality;
            public uint dwRxQualityFull;
            public uint dwRxQualitySub;
            public uint dwIdleTimeSlot;
            public uint dwTimingAdvance;
            public uint dwGPRSCellID;
            public uint dwGPRSBaseStationID;
            public uint dwNumBCCH;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x30)]
            public byte[] rgbBCCH;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
            public byte[] rgbNMR;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RILEQUIPMENTINFO
        {
            public uint cbSize;
            public uint dwParams;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x80)]
            public byte[] szManufacturer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x80)]
            public byte[] szModel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x80)]
            public byte[] szRevision;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x80)]
            public byte[] szSerialNumber;
        }

        private delegate void RILNOTIFYCALLBACK(IntPtr dwCode, IntPtr lpData, IntPtr cbData, IntPtr dwParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct RILOPERATORNAMES
        {
            public int cbSize;
            public int dwParams;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x20)]
            public byte[] szLongName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
            public byte[] szShortName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
            public byte[] szNumName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
            public byte[] szCountryCode;
        }

        private delegate void RILRESULTCALLBACK(IntPtr dwCode, IntPtr hrCmdID, IntPtr lpData, IntPtr cbData, IntPtr dwParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct RILSIGNALQUALITY
        {
            public uint cbSize;
            public uint dwParams;
            public int nSignalStrength;
            public int nMinSignalStrength;
            public int nMaxSignalStrength;
            public uint dwBitErrorRate;
            public int nLowSignalStrength;
            public int nHighSignalStrength;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RILSUBSCRIBERINFO
        {
            public uint cbSize;
            public uint dwParams;
            public uint cbSize2;
            public uint dwParams2;
            public uint dwType2;
            public uint dwNumPlan;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x100)]
            public byte[] wszAddress;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x100)]
            public byte[] wszDescription;
            public uint dwSpeed;
            public uint dwService;
            public uint dwITC;
            public uint dwAddressId;
        }

        public class SUBSCRIBERINFO
        {
            public string Address;
            public string AddressID;
            public string Description;
            public string ITC;
            public string Service;
            public string Speed;

            public SUBSCRIBERINFO(string Address, string Description, string Speed, string Service, string ITC, string AddressID)
            {
                this.Address = Address;
                this.Description = Description;
                this.Speed = Speed;
                this.Service = Service;
                this.ITC = ITC;
                this.AddressID = AddressID;
            }
        }
    }

    public struct CellIDInformations
    {
        // CellID Info
        public string cellID;
        public string localAreaCode;
        public string mobileCountryCode;
        public string mobileNetworkCode;
        public string operatorName;
        // Signal Strength Info
        public string highSignalStrength;
        public string lowSignalStrength;
        public string maxSignalStrength;
        public string minSignalStrength;
        public string signalStrength;
    }
}

