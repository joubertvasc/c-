using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace JVUtils
{
    public static class ICCID
    {
        static IntPtr hSim;

        public static string GETICCID()
        {
            const int EF_ICCID = 0x2FE2;
            const int SIM_RECORDTYPE_TRANSPARENT = 0x1;

            byte[] iccid = new byte[10];
            int zero = 0;

            try
            {
                SimInitialize(0, IntPtr.Zero, 0, ref hSim);

                SimReadRecord(hSim,
                            EF_ICCID,
                            SIM_RECORDTYPE_TRANSPARENT,
                            0,
                            iccid,
                            (uint)iccid.Length,
                            ref zero);

                SimDeinitialize(hSim);

                string result = GetRawIccIDString(iccid);

                Debug.AddLog("GetICCID: " + result, true);
                return result;
            }
            catch (Exception ex)
            {
                Debug.AddLog("GETICCID: error: " + ex.Message.ToString(), true);
                return "";
            }
        }

        public static string FormatAsSimString(byte[] iccid)
        {
            string id = GetRawIccIDString(iccid);

            if (id.Length > 15)
            {
                return id.Substring(0, 5) + " " +
                       id.Substring(5, 5) + " " +
                       id.Substring(10, 5) + " " +
                       id.Substring(15);
            }
            else
            {
                return "";
            }
        }

        static string GetRawIccIDString(byte[] iccid)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            int i = 0;

            while (i < iccid.Length)
            {
                byte b = iccid[i];

                builder.Append(ConvertInt4PairToString(b));

                Math.Max(System.Threading.Interlocked.Increment(ref i), i - 1);
            }

            return builder.ToString();
        }

        static string ConvertInt4PairToString(byte byteValue)
        {
            return ((byte)(byteValue << 4) | (byteValue >> 4)).ToString("x2");
        }

        [DllImport("cellcore.dll")]
        static extern int SimInitialize(uint dwFlags, IntPtr lpfnCallback, uint dwParam, ref IntPtr lphSim);

        [DllImport("cellcore.dll")]
        static extern int SimDeinitialize(IntPtr hSim);

        [DllImport("cellcore.dll")]
        static extern int SimReadRecord(IntPtr hSim, uint dwAddress, uint dwRecordType, uint dwIndex, byte[] lpData, uint dwBufferSize, ref int dwSize);
    }
}
