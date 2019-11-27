using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace JVUtils
{
    public enum CFGFlags : uint
    {
        Process = 0x0001,
        Metadata = 0x0002,
    }

    public enum CONFIG_E : uint
    {
        Ok = 0x00000000,
        ObjectBusy = 0x80042001,
        CancelTimeout = 0x80042002,
        EntryNotFound = 0x80042004,
        ProcessingCanceled = 0x00042005,
        CspException = 0x80042007,
        TransactioningFailure = 0x80042008,
        BadXml = 0x80042009,
    }

    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        Modkeyup = 0x1000,
    }

    public class Kernel
    {
        public static int SPI_GETPLATFORMTYPE = 257;
        public static int SPI_GETOEMINFO = 258;
        public const int NOTIFICATION_EVENT_WAKEUP = 11;
        public const UInt32 INFINITE = 0xFFFFFFFF;

        public const int SHFS_SHOWTASKBAR = 0x0001;
        public const int SHFS_HIDETASKBAR = 0x0002;
        public const int SHFS_SHOWSIPBUTTON = 0x0004;
        public const int SHFS_HIDESIPBUTTON = 0x0008;
        public const int SHFS_SHOWSTARTICON = 0x0010;
        public const int SHFS_HIDESTARTICON = 0x0020;

        public static long PMCF_DEFAULT = 0x00000001; // Used in PhoneMakeCall
        public static long PMCF_PROMPTBEFORECALLING = 0x00000002; // Used in PhoneMakeCall

        public static string[] GetMemoryCardsDirectories
        {
            get 
            {
                string[] result = new string[0];
                string[] folders = System.IO.Directory.GetDirectories(@"\");

                foreach (string folder in folders)
                {
                    DirectoryInfo di = new DirectoryInfo(folder);
                    if (((di.Attributes & FileAttributes.Directory) != 0) &&
                        ((di.Attributes & FileAttributes.Temporary) != 0))
                    {
                        Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = folder;
                    }
                }

                return result;
            }
        }

        public static void PlayFile(string fileName, bool bSync)
        {
            if (File.Exists (fileName)) {
                if (bSync) {
                   PlaySound(fileName, IntPtr.Zero, (int)(Flags.SND_SYNC | Flags.SND_FILENAME));
                } else {
                   PlaySound(fileName, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_FILENAME));
                }
            }
        }

        public static SNDFILEINFO mOldSoundFileInfo = new SNDFILEINFO();

        public struct SNDFILEINFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPathNameNative;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayNameNative;

            public SoundType SstType;
        }

        public enum SoundEvent
        {
            All = 0,
            RingLine1,
            RingLine2,
            KnownCallerLine1,
            RoamingLine1,
            RingVoip
        }

        public enum SoundType
        {
            On = 0,
            File = 1,
            Vibrate = 2,
            None = 3
        }

        private static int CTL_CODE(int DeviceType, int Func, int Method, int Access)
        {
            return (DeviceType << 16) | (Access << 14) | (Func << 2) | Method;
        }

        public static int ResetPocketPC()
        {
            const int FILE_DEVICE_HAL = 0x101;
            const int METHOD_BUFFERED = 0;
            const int FILE_ANY_ACCESS = 0;
            int bytesReturned = 0;
            int IOCTL_HAL_REBOOT;
            IOCTL_HAL_REBOOT = CTL_CODE(FILE_DEVICE_HAL, 15, METHOD_BUFFERED, FILE_ANY_ACCESS);

            return KernelIoControl(IOCTL_HAL_REBOOT, IntPtr.Zero, 0, IntPtr.Zero, 0, ref bytesReturned);
        }

        private enum Flags
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */
            SND_ASYNC = 0x0001,  /* play asynchronously */
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        [DllImport("coredll.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("coredll.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        [DllImport("coredll.dll", EntryPoint = "PlaySoundW", SetLastError = true)]
        private extern static bool PlaySound(string lpszName, IntPtr hModule, int dwFlags);

        [DllImport("coredll.dll")]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        [DllImport("coredll.dll")]
        private static extern int KernelIoControl(int dwIoControlCode, IntPtr lpInBuf, int nInBufSize, IntPtr lpOutBuf, int nOutBufSize,ref int lpBytesReturned);

        [DllImport("coredll.dll")]
        public static extern bool GetPasswordActive();

        [DllImport("coredll.dll")]
        public static extern bool CheckPassword([MarshalAs(UnmanagedType.LPWStr)] string lpszPassword);

        [DllImport("coredll.dll")]
        public static extern bool SetPassword(string lpszOldpassword, string lspzNewPassword);

        [DllImport("coredll.dll")]
        public static extern bool SetPasswordActive(bool bActive, string lpszPassword);

        [DllImport("coredll.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("coredll.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("coredll.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("coredll.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("aygshell.dll", SetLastError = true)]
        public static extern uint SndSetSound(SoundEvent seSoundEvent, ref SNDFILEINFO pSoundFileInfo, bool fSuppressUI);

        [DllImport("aygshell.dll", SetLastError = true)]
        public static extern uint SndGetSound(SoundEvent seSoundEvent, ref SNDFILEINFO pSoundFileInfo);

        [DllImport("aygshell.dll")]
        public extern static IntPtr SHDeviceLockAndPrompt();

        [DllImport("coredll.dll")]
        public static extern IntPtr ShowWindow(IntPtr hWnd, int visible);
        
        [DllImport("coredll.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("coredll.dll", EntryPoint = "EnableWindow")]
        public static extern bool EnableWindow(IntPtr hwnd, bool bEnable);

        [DllImport("aygshell.dll", SetLastError = true)]
        public static extern bool SHFullScreen(IntPtr hwnd, int state);

        [DllImport("coredll.dll", SetLastError = true)]
        public static extern IntPtr MoveWindow(IntPtr hwnd, int x, int y, int w, int l, int repaint);

        [DllImport("coredll.dll", SetLastError = true)]
        public static extern IntPtr FindWindowW(string lpClass, string lpWindow);

        [DllImport("coredll")]
        public extern static void SignalStarted(uint dword);

        [DllImport("coredll.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers Modifiers, int key);

        [DllImport("coredll.dll")]
        public static extern bool UnregisterFunc1(KeyModifiers modifiers, int keyID);
    
        [DllImport("coredll.dll", EntryPoint = "??_V@YAXPAXABUnothrow_t@std@@@Z")]
        public static extern void DeleteArray(IntPtr ptr, IntPtr junk);
        
        [DllImport("aygshell.dll", CharSet = CharSet.Unicode)]
        public static unsafe extern CONFIG_E DMProcessConfigXML(string pszWXMLin, CFGFlags dwFlags, out char* ppszwXMLout);
    
        [DllImport("sms.dll")]
        public static extern IntPtr SmsGetPhoneNumber(IntPtr psmsaAddress);

        [DllImport("cellcore.dll")]
        public static extern IntPtr SimInitialize(IntPtr dwFlags, IntPtr lpfnCallBack, IntPtr dwParam, out IntPtr lphSim);

        [DllImport("cellcore.dll")]
        public static extern IntPtr SimGetRecordInfo(IntPtr hSim, IntPtr dwAddress, ref SimRecord lpSimRecordInfo);

        [DllImport("cellcore.dll")]
        public static extern IntPtr SimReadRecord(IntPtr hSim, IntPtr
          dwAddress, IntPtr dwRecordType, IntPtr dwIndex, byte[] lpData,
          IntPtr dwBufferSize, ref IntPtr lpdwBytesRead);

        [DllImport("cellcore.dll")]
        public static extern IntPtr SimDeinitialize(IntPtr hSim);

        [DllImport("phone.dll")]
        public static extern IntPtr PhoneMakeCall(ref PhoneMakeCallInfo ppmci);

        [DllImport("cellcore.dll")]
        public static extern int lineSendUSSD(IntPtr hLine, byte[] lpbUSSD, int dwUSSDSize, int dwFlags);

        [DllImport("coredll.dll")]
        public static extern int lineNegotiateExtVersion(IntPtr hLineApp, int dwDeviceID, int dwAPIVersion, int dwExtLowVersion, int dwExtHighVersion, int lpdwExtVersion);

        [DllImport("coredll.dll")]
        public static extern int lineForward(IntPtr hLineApp, int bAllAddresses, int dwAddressID, ref LPLINEFOWARDLIST lpForwardList, int dwNumRingsNoAnswer, int lphConsultCall, ref LINECALLPARAMS lpCallParams);

        [DllImport("CoreDLL.dll")]
        public static extern int CeRunAppAtTime(string application, SystemTime startTime);

        [DllImport("CoreDLL.dll")]
        public static extern int FileTimeToSystemTime(ref long lpFileTime, SystemTime lpSystemTime);

        [DllImport("CoreDLL.dll")]
        public static extern int FileTimeToLocalFileTime(ref long lpFileTime, ref long lpLocalFileTime);

        [DllImport("coredll.dll")]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, string pvParam, int fWinIni);

        [DllImport("coredll")]
        public static extern uint GetSystemPowerStatusEx(SYSTEM_POWER_STATUS_EX lpSystemPowerStatus, bool fUpdate);

        [DllImport("coredll")]
        public static extern uint GetSystemPowerStatusEx2(SYSTEM_POWER_STATUS_EX2 lpSystemPowerStatus, uint dwLen, bool fUpdate);

        [DllImport("coredll.dll", EntryPoint = "CeSetUserNotificationEx", SetLastError = true)]
        public static extern int CeSetUserNotificationEx(int hNotification, byte[] lpTrigger, byte[] lpUserNotification);

        [DllImport("coredll.dll")]
        public static extern IntPtr CeSetUserNotificationEx(IntPtr notification, CE_NOTIFICATION_TRIGGER notificationTrigger, CE_USER_NOTIFICATION userNotification);

        [DllImport("coredll.dll")]
        public static extern bool CeRunAppAtEvent(string AppName, int WhichEvent);

        /*            string szOEMInfo = " ";
            string strOEMInfo = "";
            // Get OEM Info
            int ret = Kernel.SystemParametersInfo(Kernel.SPI_GETOEMINFO, szOEMInfo.Length, szOEMInfo, 0);

            if (ret != 0)
            {
                strOEMInfo = szOEMInfo.Substring(0, szOEMInfo.IndexOf('\0'));
            }
        /**/

    }
}

public struct LPLINEFOWARDLIST
{
    public int dwTotalSize;
    public int dwNumEntries;
    public LINEFORWARD[] ForwardList;

    void LPLINEFORWARDLIST()
    {
        ForwardList = new LINEFORWARD[1];
    }
}

public struct LINEFORWARD
{
    public int dwForwardMode;
    public int dwCallerAddressSize;
    public int dwCallerAddressOffset;
    public int dwDestCountryCode;
    public int dwDestAddressSize;
    public int dwDestAddressOffset;
    public int dwCallerAddressType;
    public int dwDestAddressType;
}

public struct LINECALLPARAMS
{
    public int dwTotalSize;
    public int dwBearerMode;
    public int dwMinRate;
    public int dwMaxRate;
    public int dwMediaMode;
    public int dwCallParamFlags;
    public int dwAddressMode;
    public int dwAddressID;
    public LINEDIALPARAMS DialParams;
    public int dwOrigAddressSize;
    public int dwOrigAddressOffset;
    public int dwDisplayableAddressSize;
    public int dwDisplayableAddressOffset;
    public int dwCalledPartySize;
    public int dwCalledPartyOffset;
    public int dwCommentSize;
    public int dwCommentOffset;
    public int dwUserUserInfoSize;
    public int dwUserUserInfoOffset;
    public int dwHighLevelCompSize;
    public int dwHighLevelCompOffset;
    public int dwLowLevelCompSize;
    public int dwLowLevelCompOffset;
    public int dwDevSpecificSize;
    public int dwDevSpecificOffset;
    public int dwPredictiveAutoTransferStates;
    public int dwTargetAddressSize;
    public int dwTargetAddressOffset;
    public int dwSendingFlowspecSize;
    public int dwSendingFlowspecOffset;
    public int dwReceivingFlowspecSize;
    public int dwReceivingFlowspecOffset;
    public int dwDeviceClassSize;
    public int dwDeviceClassOffset;
    public int dwDeviceConfigSize;
    public int dwDeviceConfigOffset;
    public int dwCallDataSize;
    public int dwCallDataOffset;
    public int dwNoAnswerTimeout;
    public int dwCallingPartyIDSize;
    public int dwCallingPartyIDOffset;
    public int dwAddressType;
}

public struct LINEDIALPARAMS
{
    public int dwDialPause;
    public int dwDialSpeed;
    public int dwDigitDuration;
    public int dwWaitForDialtone;
}

public struct PhoneMakeCallInfo
{
    public IntPtr cbSize;
    public IntPtr dwFlags;
    public IntPtr pszDestAddress;
    public IntPtr pszAppName;
    public IntPtr pszCalledParty;
    public IntPtr pszComment;
}

[StructLayout(LayoutKind.Sequential)]
public struct SimRecord
{
    public IntPtr cbSize;
    public IntPtr dwParams;
    public IntPtr dwRecordType;
    public IntPtr dwItemCount;
    public IntPtr dwSize;
}

public class CE_NOTIFICATION_TRIGGER
{
    public UInt32 Size = 0;
    public UInt32 Type = 0;
    public UInt32 Event = 0;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string pAppName;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string pArgs;
    public SYSTEMTIME StartTime;
    public SYSTEMTIME EndTime;
}

public class CE_USER_NOTIFICATION
{
    public UInt32 ActionFlags;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string pDialogTitle;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string DialogText;
    [MarshalAs(UnmanagedType.LPWStr)]
    public string Sound;
    public UInt32 MaxSound;
    public UInt32 Reserved;
}

[StructLayout(LayoutKind.Sequential)]
public struct SYSTEMTIME
{
    [MarshalAs(UnmanagedType.U2)]
    public short Year;
    [MarshalAs(UnmanagedType.U2)]
    public short Month;
    [MarshalAs(UnmanagedType.U2)]
    public short DayOfWeek;
    [MarshalAs(UnmanagedType.U2)]
    public short Day;
    [MarshalAs(UnmanagedType.U2)]
    public short Hour;
    [MarshalAs(UnmanagedType.U2)]
    public short Minute;
    [MarshalAs(UnmanagedType.U2)]
    public short Second;
    [MarshalAs(UnmanagedType.U2)]
    public short Milliseconds;

    public SYSTEMTIME(DateTime dt)
    {
        dt = dt.ToUniversalTime();  // SetSystemTime expects the SYSTEMTIME in UTC
        Year = (short)dt.Year;
        Month = (short)dt.Month;
        DayOfWeek = (short)dt.DayOfWeek;
        Day = (short)dt.Day;
        Hour = (short)dt.Hour;
        Minute = (short)dt.Minute;
        Second = (short)dt.Second;
        Milliseconds = (short)dt.Millisecond;
    }
}

