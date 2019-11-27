using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVTracker
{
    static class Configuration
    {
        #region Internal Variables
        static bool _bUsingSMS;
        static string _sPhoneNumber;
        static string _sWebServer;
        static bool _bAutoStart;
        static int _nInterval;
        static string key = "\\Software\\JVSoftware\\JVTracker";
        #endregion

        #region Properties
        public static bool UseSMS
        {
            get { return _bUsingSMS; }
            set { _bUsingSMS = value; }
        }
        public static string PhoneNumber
        {
            get { return _sPhoneNumber; }
            set { _sPhoneNumber = value; }
        }
        public static string WebServer
        {
            get { return _sWebServer; }
            set { _sWebServer = value; }
        }
        public static bool AutoStart
        {
            get { return _bAutoStart; }
            set { _bAutoStart = value; }
        }
        public static int Interval
        {
            get { return _nInterval; }
            set { _nInterval = value; }
        }
        public static string Key
        {
            get { return key; }
        }
        #endregion

        public static void LoadConfiguration()
        {
            // Load configuration from Registry
            try
            {
                RegistryKey r = Registry.LocalMachine.OpenSubKey(key);

                UseSMS = ((string)r.GetValue("UseSMS", "Y")).Equals("Y") ? true : false;
                AutoStart = ((string)r.GetValue("AutoStart", "N")).Equals("Y") ? true : false;
                PhoneNumber = (string)r.GetValue("PhoneNumber", "");
                WebServer = (string)r.GetValue("WebServer", "");
                Interval = System.Convert.ToInt32(r.GetValue("Interval", "5"));
                r.Close();
            }
            catch
            {
                UseSMS = true;
                AutoStart = false;
                PhoneNumber = "";
                WebServer = "";
                Interval = 5;
            }
        }

        public static void SaveConfiguration()
        {
            // Save configuration to Registry
            try
            {
                RegistryKey r = Registry.LocalMachine.CreateSubKey(key);

                r.SetValue("UseSMS", UseSMS ? "Y" : "N");
                r.SetValue("AutoStart", AutoStart ? "Y" : "N");
                r.SetValue("PhoneNumber", PhoneNumber);
                r.SetValue("WebServer", WebServer);
                r.SetValue("Interval", Interval.ToString());
                r.Close();
            }
            catch
            {
            }
        }
    }
}
