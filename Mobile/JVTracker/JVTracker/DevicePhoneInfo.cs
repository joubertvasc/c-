using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.WindowsMobile.Status;

namespace JVTracker
{
    public class PhoneInfo
    {
        private void GetPhoneInfo(out string manufacturer, out string model, out string revision, out string serialNumber, out string subsciberId)
        {
            IntPtr hLine;
            int dwNumDev;
            int num1 = 0x20000;
            LINEINITIALIZEEXPARAMS lineInitializeParams = new LINEINITIALIZEEXPARAMS();
            
            lineInitializeParams.dwTotalSize = (uint)Marshal.SizeOf(lineInitializeParams);
            lineInitializeParams.dwNeededSize = lineInitializeParams.dwTotalSize;
            lineInitializeParams.dwOptions = 2;
            lineInitializeParams.hEvent = IntPtr.Zero;
            lineInitializeParams.hCompletionPort = IntPtr.Zero;
            
            #region lineInitializeEx
            int result = Tapi.lineInitializeEx(out hLine, IntPtr.Zero, IntPtr.Zero, null, out dwNumDev, ref num1, 
                                               ref lineInitializeParams);
            if (result != 0)
            {
//                throw new ApplicationException(string.Format("lineInitializeEx failed!\n\nError Code:{0}", result.ToString()));
                manufacturer = "Error 1";
                model = "Error 1";
                revision = "Error 1";
                serialNumber = "Error 1";
                subsciberId = "Error 1";
            }
            #endregion

            #region lineNegotiateAPIVerison
            int version;
            int dwAPIVersionLow = 0x10004;
            int dwAPIVersionHigh = 0x20000;
            LINEEXTENSIONID lineExtensionID;
            result = Tapi.lineNegotiateAPIVersion(hLine, 0, dwAPIVersionLow, dwAPIVersionHigh, out version, out lineExtensionID);
            if (result != 0)
            {
//                throw new ApplicationException(string.Format("lineNegotiateAPIVersion failed!\n\nError Code: {0}", result.ToString()));
                manufacturer = "Error 2";
                model = "Error 2";
                revision = "Error 2";
                serialNumber = "Error 2";
                subsciberId = "Error 2";
            }
            #endregion

            #region lineOpen
            IntPtr hLine2 = IntPtr.Zero;
            result = Tapi.lineOpen(hLine, 0, out hLine2, version, 0,IntPtr.Zero, 0x00000002, 0x00000004, IntPtr.Zero);
            if (result != 0)
            {
//                throw new ApplicationException(string.Format("lineNegotiateAPIVersion failed!\n\nError Code: {0}", result.ToString()));
                manufacturer = "Error 3";
                model = "Error 3";
                revision = "Error 3";
                serialNumber = "Error 3";
                subsciberId = "Error 3";
            }
            #endregion

            #region lineGetGeneralInfo
            int structSize = Marshal.SizeOf(new LINEGENERALINFO());
            byte[] bytes = new byte[structSize];
            byte[] tmpBytes = BitConverter.GetBytes(structSize);

            for (int index = 0; index < tmpBytes.Length; index++)
            {
                bytes[index] = tmpBytes[index];
            }
            #endregion

            // make initial query to retrieve necessary size
            result = Tapi.lineGetGeneralInfo(hLine2, bytes);

            // get the needed size
            int neededSize = BitConverter.ToInt32(bytes, 4);

            // resize the array
            bytes = new byte[neededSize];

            // write out the new allocated size to the byte stream
            tmpBytes = BitConverter.GetBytes(neededSize);
            for (int index = 0; index < tmpBytes.Length; index++)
            {
                bytes[index] = tmpBytes[index];
            }

            // fetch the information with properly size buffer
            result = Tapi.lineGetGeneralInfo(hLine2, bytes);

            if (result != 0)
            {
                //                throw new ApplicationException(Marshal.GetLastWin32Error().ToString());
                manufacturer = "";
                model = "";
                revision = "";
                serialNumber = "";
                subsciberId = "";
            }
            else
            {
                int size;
                int offset;

                // manufacture
                try
                {
                    size = BitConverter.ToInt32(bytes, 12);
                    offset = BitConverter.ToInt32(bytes, 16);
                    manufacturer = Encoding.Unicode.GetString(bytes, offset, size);
                    manufacturer = manufacturer.Substring(0, manufacturer.IndexOf('\0'));
                }
                catch
                {
                    manufacturer = "";
                }

                // model
                try
                {
                    size = BitConverter.ToInt32(bytes, 20);
                    offset = BitConverter.ToInt32(bytes, 24);
                    model = Encoding.Unicode.GetString(bytes, offset, size);
                    model = model.Substring(0, model.IndexOf('\0'));
                }
                catch
                {
                    model = "";
                }

                // revision
                try
                {
                    size = BitConverter.ToInt32(bytes, 28);
                    offset = BitConverter.ToInt32(bytes, 32);
                    revision = Encoding.Unicode.GetString(bytes, offset, size);
                    revision = revision.Substring(0, revision.IndexOf('\0'));
                }
                catch
                {
                    revision = "";
                }

                // serial number
                try
                {
                    size = BitConverter.ToInt32(bytes, 36);
                    offset = BitConverter.ToInt32(bytes, 40);
                    serialNumber = Encoding.Unicode.GetString(bytes, offset, size);
                    serialNumber = serialNumber.Substring(0, serialNumber.IndexOf('\0'));
                }
                catch
                {
                    serialNumber = "";
                }

                // subscriber id
                try
                {
                    size = BitConverter.ToInt32(bytes, 44);
                    offset = BitConverter.ToInt32(bytes, 48);
                    subsciberId = Encoding.Unicode.GetString(bytes, offset, size);
                    subsciberId = subsciberId.Substring(0, subsciberId.IndexOf('\0'));
                }
                catch
                {
                    subsciberId = "";
                }
            }

            //tear down
            Tapi.lineClose(hLine2);            
            Tapi.lineShutdown(hLine);
        }

        public string GetIMSI()
        {
            string manufacturer;
            string model;
            string revision;
            string imsi;
            string imei;

            try
            {
                GetPhoneInfo(out manufacturer, out model, out revision, out imei, out imsi);
            }
            catch
            {
                imsi = "";
            }

            return imsi;
        }

        public string GetIMEI()
        {
            string manufacturer;
            string model;
            string revision;
            string imsi;
            string imei;

            try
            {
                GetPhoneInfo(out manufacturer, out model, out revision, out imei, out imsi);
            }
            catch
            {
                imei = "";
            }

            return imei;
        }

        public string GetManufacturer()
        {
            string manufacturer;
            string model;
            string revision;
            string imsi;
            string imei;

            try
            {
                GetPhoneInfo(out manufacturer, out model, out revision, out imei, out imsi);
            }
            catch
            {
                manufacturer = "";
            }

            return manufacturer;
        }

        public string GetModel()
        {
            string manufacturer;
            string model;
            string revision;
            string imsi;
            string imei;

            try
            {
                GetPhoneInfo(out manufacturer, out model, out revision, out imei, out imsi);
            }
            catch
            {
                model = "";
            }

            return model;
        }

        public string GetRevision()
        {
            string manufacturer;
            string model;
            string revision;
            string imsi;
            string imei;

            try
            {
                GetPhoneInfo(out manufacturer, out model, out revision, out imei, out imsi);
            }
            catch
            {
                revision = "";
            }

            return revision;
        }
    }
}