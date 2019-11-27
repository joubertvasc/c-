using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using OpenNETCF.Net;
using OpenNETCF.Tapi;

namespace JVUtils
{
    public class Radio
    {
        private static string KEY = "\\System\\CurrentControlSet\\Control\\Power\\State";

        public struct AccessPointAttributes
        {
            public string name;
            public string macAddress;
            public int signal;
            public string strength;
            public string infrastructureMode;
            public string networkTypeInUse;
            public int privacy;
            public byte[] supportedRates;
        }

        public enum CEDEVICE_POWER_STATE
        {
            D0 = 0,
            D1 = 1,
            D2 = 2,
            D3 = 3,
            D4 = 4,
            PwrDeviceMaximum = 5,
            PwrDeviceUnspecified = -1
        }

        public static void ActiveWifi(bool turnOn)
        {
            string device = "";

            try
            {
                if (RadioWiFiPresent())
                {
                    if (turnOn)
                    {
                        device = GetWifiID(true);
                        try
                        {
                            DevicePowerNotify(device, CEDEVICE_POWER_STATE.D0, 1);
                            Application.DoEvents();
                            SetDevicePower(device, 1, CEDEVICE_POWER_STATE.D0);
                            Application.DoEvents();
                        }
                        catch (Exception e)
                        {
                            Debug.AddLog("ActiveWifi: on: " + e.ToString(), true);
                        }
                    }
                    else
                    {
                        device = GetWifiID(false);
                        try
                        {
                            SetDevicePower(device, 1, CEDEVICE_POWER_STATE.D4);
                            Application.DoEvents();
                        }
                        catch (Exception e)
                        {
                            Debug.AddLog("ActiveWifi: off: " + e.ToString(), true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.AddLog("ActiveWifi: Error: " + e.ToString(), true);
            }
        }

        public static void ActiveBlueTooth(bool turnOn)
        {
            try
            {
                if (RadioBlueToothPresent())
                {
                    if (turnOn)
                    {
                        BthSetMode((long)1);
                    }
                    else
                    {
                        BthSetMode((long)0);
                    }
                }
                Application.DoEvents();
            }
            catch (Exception e)
            {
                Debug.AddLog("ActiveBlueTooth: Error: " + e.ToString(), true);
            }
        }

        public static bool RadioWiFiPresent()
        {
            bool result = false;

            RegistryKey key = Registry.LocalMachine.OpenSubKey(KEY, true);
            try
            {
                foreach (string keys in key.GetValueNames())
                {
                    if (Strings.InStr(keys, "{98C5250D-C29A-4985-AE5F-AFE5367E5006}", CompareMethod.Text) > 0 ||
                        Strings.InStr(keys, "Wireless", CompareMethod.Text) > 0)
                    {
                        result = true;
                    }
                }
            }
            finally
            {
                key.Close();
            }

            return result;
        }

        public static bool RadioBlueToothPresent()
        {
            bool result = false;

            RegistryKey key = Registry.LocalMachine.OpenSubKey(KEY, true);
            try
            {
                foreach (string keys in key.GetValueNames())
                {
                    if (Strings.InStr(keys, "Bluetooth", CompareMethod.Text) > 0)
                    {
                        result = true;
                    }
                }
            }
            finally
            {
                key.Close();
            }

            return result;
        }

        public static bool RadioGSMPresent()
        {
            bool result = false;

            RegistryKey key = Registry.LocalMachine.OpenSubKey(KEY, true);
            try
            {
                foreach (string keys in key.GetValueNames())
                {
                    if (Strings.InStr(keys, "T\x00e9l\x00e9phone", CompareMethod.Text) > 0 ||
                        Strings.InStr(keys, "Phone", CompareMethod.Text) > 0)
                    {
                        result = true;
                    }
                }
            }
            finally
            {
                key.Close();
            }

            return result;
        }

        public static string GetWifiID(bool Active)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(KEY, true);
            string result = "";
            try
            {
                foreach (string keys in key.GetValueNames())
                {
                    if (Strings.InStr(keys, "{98C5250D-C29A-4985-AE5F-AFE5367E5006}", CompareMethod.Text) > 0)
                    {
                        result = keys;

                        break;
                    }
                }
            }
            finally
            {
                key.Close();
            }

            return result;
        }

        public static AccessPointAttributes[] WifiScan()
        {
            AdapterCollection adptrColection = Networking.GetAdapters();

            AccessPointAttributes[] apAttrArray = new AccessPointAttributes[0];

            foreach (Adapter adapter in adptrColection)
            {
                if (adapter.IsWireless)
                {
                    foreach (AccessPoint ap in adapter.NearbyAccessPoints)
                    {
                        AccessPointAttributes apAttr = new AccessPointAttributes();
                        apAttr.name = ap.Name;
                        apAttr.macAddress = BitConverter.ToString(ap.MacAddress);
                        apAttr.signal = ap.SignalStrength.Decibels;
                        apAttr.strength = ap.SignalStrength.ToString();
                        apAttr.infrastructureMode = ap.InfrastructureMode.ToString();
                        apAttr.networkTypeInUse = ap.NetworkTypeInUse.ToString();
                        apAttr.privacy = ap.Privacy;
                        apAttr.supportedRates = ap.SupportedRates;
                        Array.Resize(ref apAttrArray, apAttrArray.Length + 1);
                        apAttrArray[apAttrArray.Length - 1] = apAttr;
                    }
                }
            }

            return apAttrArray;
        }

        public static string getPrivacy(int privacy)
        {
            switch (privacy)
            {
                case 0:
                    return "Open";

                case 1:
                    return "Shared-WEP";

                case 2:
                    return "AutoSwitch";

                case 3:
                    return "WPA";

                case 4:
                    return "WPA-PSK";

                case 5:
                    return "WPA-NONE";

                case 6:
                    return "MAX";
            }
            return "Unknow";
        }

        private static void tapi_LineMessage(LINEMESSAGE msg)
        {
        }

        public static string GetCurrentOperatorName()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("\\System\\State\\Phone", true);
            try
            {
                return (string)key.GetValue("Current Operator Name", "");
            }
            finally
            {
                key.Close();
            }
        }

        public static void SetFlightMode(bool flightMode)
        {
            OpenNETCF.Tapi.Line line = null;
            Tapi tapi = new Tapi();
            tapi.Initialize();
            tapi.LineMessage += new OpenNETCF.Tapi.Tapi.MessageHandler(tapi_LineMessage);

            for (int i = 0; i < tapi.NumDevices; i++)
            {
                LINEDEVCAPS dc;
                LINEERR ret = tapi.GetDevCaps(i, out dc);

                if (dc.ProviderName.Contains ("Cellular TAPI Service Provider")) 
                {
                    LINEMEDIAMODE mode = (LINEMEDIAMODE.INTERACTIVEVOICE | LINEMEDIAMODE.DATAMODEM) & dc.dwMediaModes;
                    try
                    {
                        line = tapi.CreateLine(i, mode, LINECALLPRIVILEGE.MONITOR | LINECALLPRIVILEGE.OWNER);

                        OpenNETCF.Tapi.LINEEQUIPSTATE es;
                        OpenNETCF.Tapi.LINERADIOSUPPORT rs;
                        NativeTapi.lineGetEquipmentState(line.hLine, out es, out rs);

                        if (!flightMode)
                            NativeTapi.lineSetEquipmentState(line.hLine, OpenNETCF.Tapi.LINEEQUIPSTATE.FULL);
                        else
                            NativeTapi.lineSetEquipmentState(line.hLine, OpenNETCF.Tapi.LINEEQUIPSTATE.NOTXRX);
                    }
                    catch (TapiException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            tapi.Shutdown();
        }

        public static void SetGPRSTimeout(bool useTimeout, int seconds)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Comm\\ConnMgr\\Planner\\Settings");

            if (r != null)
            {
                r.SetValue("SuspendResume", (useTimeout ? "GPRS_bye_if_device_off" : "~GPRS!"));
                r.SetValue("CacheTime", seconds);
                r.Close();
            }
        }

        [DllImport("coredll.dll")]
        public static extern int DevicePowerNotify(string device, CEDEVICE_POWER_STATE state, int flags);

        [DllImport("coredll.dll")]
        public static extern int SetDevicePower(string pvDevice, int df, CEDEVICE_POWER_STATE ds);

        [DllImport("BthUtil.dll")]
        public static extern int BthSetMode(long BluetoothMode);

        [DllImport("cellcore")]
        public static extern int lineSetEquipmentState(IntPtr hLine, LINEEQUIPSTATE dwState);
    }
}
