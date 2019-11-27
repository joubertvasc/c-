using System;
using System.Runtime.InteropServices;

namespace JVUtils
{
    public enum CallerIDType
    {
        Unavailable,
        Blocked,
        Available
    }

    public enum CallLogFilter
    {
        AllCalls,
        Missed,
        Incoming,
        Outgoing
    }

    public enum CallLogSeek
    {
        Beginning = 2,
        End = 4
    }

    public enum Iom
    {
        Missed,
        Incoming,
        Outgoing
    }

    public class CallLogEntry
    {
        internal CallLogEntry() { }

        private DateTime _StartTime;
        private DateTime _EndTime;
        private Boolean _IsOutgoing;
        private Boolean _IsConnected;
        private Boolean _IsEnded;
        private Boolean _IsRoaming;
        private CallerIDType _CallerID;
        private String _CallerName;
        private String _CallerNumber;

        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
            internal set
            {
                _StartTime = value;
            }
        }
        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            internal set
            {
                _EndTime = value;
            }
        }
        public Int32 Duration
        {
            get
            {
                return _EndTime.Subtract(_StartTime).Seconds;
            }
        }
        public Boolean IsOutgoing
        {
            get
            {
                return _IsOutgoing;
            }
            internal set
            {
                _IsOutgoing = value;
            }
        }
        public Boolean IsConnected
        {
            get
            {
                return _IsConnected;
            }
            internal set
            {
                _IsConnected = value;
            }
        }
        public Boolean IsEnded
        {
            get
            {
                return _IsEnded;
            }
            internal set
            {
                _IsEnded = value;
            }
        }
        public Boolean IsRoaming
        {
            get
            {
                return _IsRoaming;
            }
            internal set
            {
                _IsRoaming = value;
            }
        }
        public CallerIDType CallerID
        {
            get
            {
                return _CallerID;
            }
            internal set
            {
                _CallerID = value;
            }
        }
        public String CallerName
        {
            get
            {
                return _CallerName;
            }
            internal set
            {
                _CallerName = value;
            }
        }
        public String CallerNumber
        {
            get
            {
                return _CallerNumber;
            }
            internal set
            {
                _CallerNumber = value;
            }
        }
    }

    public static class CallLog
    {
        public static CallLogEntry[] Entries
        {
            get
            {
                CallLogEntry[] entries = new CallLogEntry[0];

                unsafe
                {
                    IntPtr result = IntPtr.Zero;
                    IntPtr log = IntPtr.Zero;
                    uint lastEntryIndex = 0;
                    uint currentEntryIndex = 0;

                    result = PhoneOpenCallLog(out log);
                    if (result.ToInt64() != S_OK) throw new Exception("Failed to Open Call Log");

                    result = PhoneSeekCallLog(log, CALLLOGSEEK.CALLLOGSEEK_END, 0, ref lastEntryIndex);
                    if (result.ToInt64() != S_OK) throw new Exception("Failed to Seek Call Log");
                    result = PhoneSeekCallLog(log, CALLLOGSEEK.CALLLOGSEEK_BEGINNING, 0, ref currentEntryIndex);
                    if (result.ToInt64() != S_OK) throw new Exception("Failed to Seek Call Log");

                    entries = new CallLogEntry[lastEntryIndex + 1];
                    for (uint i = 0; i <= lastEntryIndex; i++)
                    {
                        CALLLOGENTRY entry = new CALLLOGENTRY();
                        entry.cbSize = (uint)Marshal.SizeOf(typeof(CALLLOGENTRY));

                        result = PhoneGetCallLogEntry(log, ref entry);
                        if (result.ToInt64() != S_OK) throw new Exception("Failed to Read Call Log Entry");

                        entries[i] = new CallLogEntry();

                        entries[i].StartTime = DateTime.FromFileTime(entry.ftStartTime);
                        entries[i].EndTime = DateTime.FromFileTime(entry.ftEndTime);
                        entries[i].IsOutgoing = (entry.flags & 0x1) != 0;
                        entries[i].IsConnected = (entry.flags & 0x2) != 0;
                        entries[i].IsEnded = (entry.flags & 0x4) != 0;
                        entries[i].IsRoaming = (entry.flags & 0x8) != 0;
                        entries[i].CallerID = (CallerIDType)(entry.cidt);
                        entries[i].CallerName = Marshal.PtrToStringUni(entry.pszName);
                        entries[i].CallerNumber = Marshal.PtrToStringUni(entry.pszNumber);
                    }

                    result = PhoneCloseCallLog(log);
                    if (result.ToInt64() != S_OK) throw new Exception("Failed to Close Call Log");
                }

                return entries;
            }
        }

        private const Int64 S_OK = 0x00000000;

        private enum CALLERIDTYPE
        {
            CALLERIDTYPE_UNAVAILABLE,
            CALLERIDTYPE_BLOCKED,
            CALLERIDTYPE_AVAILABLE
        }

        private enum CALLLOGFILTER
        {
            CALLLOGFILTER_ALL_CALLS,
            CALLLOGFILTER_MISSED,
            CALLLOGFILTER_INCOMING,
            CALLLOGFILTER_OUTGOING
        }

        private enum CALLLOGSEEK
        {
            CALLLOGSEEK_BEGINNING = 2,
            CALLLOGSEEK_END = 4
        }

        private enum IOM
        {
            IOM_MISSED,
            IOM_INCOMING,
            IOM_OUTGOING
        }

        [StructLayout(LayoutKind.Explicit, Size = 48)]
        private struct CALLLOGENTRY
        {
            [FieldOffset(0)]
            public uint cbSize;
            [FieldOffset(4)]
            public long ftStartTime;
            [FieldOffset(12)]
            public long ftEndTime;
            [FieldOffset(20)]
            public IOM iom;
            [FieldOffset(24)]
            public byte flags;
            [FieldOffset(28)]
            public CALLERIDTYPE cidt;
            [FieldOffset(32)]
            public IntPtr pszNumber;
            [FieldOffset(36)]
            public IntPtr pszName;
            [FieldOffset(40)]
            public IntPtr pszNameType;
            [FieldOffset(44)]
            public IntPtr pszNote;
        }

        [DllImport("Phone.dll")]
        private static extern IntPtr PhoneOpenCallLog(out IntPtr ph);

        [DllImport("Phone.dll")]
        private static extern IntPtr PhoneCloseCallLog(IntPtr h);

        [DllImport("Phone.dll")]
        private static extern IntPtr PhoneGetCallLogEntry(IntPtr h, ref CALLLOGENTRY pentry);

        [DllImport("Phone.dll")]
        private static extern IntPtr PhoneSeekCallLog(IntPtr hdb, CALLLOGSEEK seek, uint iRecord, ref uint piRecord);
    }
}