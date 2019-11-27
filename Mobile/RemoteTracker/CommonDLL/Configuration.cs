using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using JVUtils;
using JVGPS;

namespace CommonDLL
{
    public class Configuration
    {
        #region Internal variables
        bool _atm = false;
        string _sIMSI1;
        string _sIMSI2;
        string _sIMSI3;
        string _sIMSI4;
        string _aIMSI1;
        string _aIMSI2;
        string _aIMSI3;
        string _aIMSI4;
        string _defaultRegistryKey;
        string _appPath;
        Languages _languages;
        string _alarmSound;

        int _timeELTCommand;
        int _defaultLanguage;
        int _defaultGPSInteractions;
        string _defaultNumber1;
        string _defaultNumber2;
        string _defaultNumber3;
        string _defaultNumber4;
        string _emergencyEMail1;
        string _emergencyEMail2;
        string _emergencyEMail3;
        string _emergencyEMail4;
        string _defaultPassword;
        string _secretQuestion;
        string _secretAnswer;
        bool _debug;
        bool _screenOff;
        bool _hidden = false;
        string _emergencyMessage;
        int _alertsInterval;
        bool _activeTopSecret;
        string _webEMailAccount;
        string _webPassword;

        CellIDProvider _cellIDProvider;

        string[] _memoryCards;

        //E-Mail
        string _defaultEMailAccount;
        string _defaultrecipientName;
        string _defaultrecipientEMail;
        string _defaultSubject;

        // GPS
        GPSType _gpsType;
        string _comPort;
        string _baudRate;

        // FTP
        string _ftpServer;
        string _ftpUser;
        string _ftpPassword;
        string _ftpRemoteDir;
        int _ftpPort;
        #endregion
        
        #region Properties
        public bool ATM
        {
            get { return _atm; }
            set { _atm = value; }
        }
        public string IMSI1
        {
            get { return _sIMSI1.Trim(); }
            set { _sIMSI1 = value; }
        }
        public string IMSI2
        {
            get { return _sIMSI2.Trim(); }
            set { _sIMSI2 = value.Trim(); }
        }
        public string IMSI3
        {
            get { return _sIMSI3.Trim(); }
            set { _sIMSI3 = value.Trim(); }
        }
        public string IMSI4
        {
            get { return _sIMSI4.Trim(); }
            set { _sIMSI4 = value.Trim(); }
        }
        public string AliasIMSI1
        {
            get { return _aIMSI1.Trim(); }
            set { _aIMSI1 = value.Trim(); }
        }
        public string AliasIMSI2
        {
            get { return _aIMSI2.Trim(); }
            set { _aIMSI2 = value.Trim(); }
        }
        public string AliasIMSI3
        {
            get { return _aIMSI3.Trim(); }
            set { _aIMSI3 = value.Trim(); }
        }
        public string AliasIMSI4
        {
            get { return _aIMSI4.Trim(); }
            set { _aIMSI4 = value.Trim(); }
        }
        public int TimeELTCommand
        {
            get { return _timeELTCommand; }
            set { _timeELTCommand = value; }
        }
        public string defaultRegistryKey
        {
            get { return _defaultRegistryKey.Trim(); }
            set { _defaultRegistryKey = value.Trim(); }
        }
        public int defaultLanguage
        {
            get { return _defaultLanguage; }
            set { _defaultLanguage = value; }
        }
        public int defaultGPSInteractions
        {
            get { return _defaultGPSInteractions; }
            set { _defaultGPSInteractions = value; }
        }
        public string defaultNumber1
        {
            get { return _defaultNumber1.Trim(); }
            set { _defaultNumber1 = value.Trim(); }
        }
        public string defaultNumber2
        {
            get { return _defaultNumber2.Trim(); }
            set { _defaultNumber2 = value.Trim(); }
        }
        public string defaultNumber3
        {
            get { return _defaultNumber3.Trim(); }
            set { _defaultNumber3 = value.Trim(); }
        }
        public string defaultNumber4
        {
            get { return _defaultNumber4.Trim(); }
            set { _defaultNumber4 = value.Trim(); }
        }
        public string emergencyEMail1
        {
            get { return _emergencyEMail1.Trim(); }
            set { _emergencyEMail1 = value.Trim(); }
        }
        public string emergencyEMail2
        {
            get { return _emergencyEMail2.Trim(); }
            set { _emergencyEMail2 = value.Trim(); }
        }
        public string emergencyEMail3
        {
            get { return _emergencyEMail3.Trim(); }
            set { _emergencyEMail3 = value.Trim(); }
        }        
        public string emergencyEMail4
        {
            get { return _emergencyEMail4.Trim(); }
            set { _emergencyEMail4 = value.Trim(); }
        }        
        public string defaultPassword
        {
            get { return _defaultPassword.Trim(); }
            set { _defaultPassword = value.Trim(); }
        }
        public string SecretQuestion
        {
            get { return _secretQuestion; }
            set { _secretQuestion = value; }
        }
        public string SecretAnswer
        {
            get { return _secretAnswer; }
            set { _secretAnswer = value; }
        }
        public string defaultEMailAccount
        {
            get { return _defaultEMailAccount.Trim(); }
            set { _defaultEMailAccount = value.Trim(); }
        }
        public string defaultrecipientName
        {
            get { return _defaultrecipientName.Trim(); }
            set { _defaultrecipientName = value.Trim(); }
        }
        public string defaultrecipientEMail
        {
            get { return _defaultrecipientEMail.Trim(); }
            set { _defaultrecipientEMail = value.Trim(); }
        }
        public string defaultSubject
        {
            get { return _defaultSubject.Trim(); }
            set { _defaultSubject = value.Trim(); }
        }
        public string AlarmSound
        {
            get { return _alarmSound.Trim(); }
            set { _alarmSound = value.Trim(); }
        }
        public bool DebugMode
        {
            get { return _debug; }
            set { _debug = value; }
        }
        public GPSType GpsType
        {
            get { return _gpsType; }
            set { _gpsType = value; }
        }
        public string ComPort
        {
            get { return _comPort; }
            set { _comPort = value; }
        }
        public string BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }
        public bool ScreenOff
        {
            get { return _screenOff; }
            set { _screenOff = value; }
        }
        public string FtpServer
        {
            get { return _ftpServer.Trim(); }
            set { _ftpServer = value.Trim(); }
        }
        public string FtpUser
        {
            get { return _ftpUser.Trim(); }
            set { _ftpUser = value.Trim(); }
        }
        public string FtpPassword
        {
            get { return _ftpPassword.Trim(); }
            set { _ftpPassword = value.Trim(); }
        }
        public string FtpRemoteDir
        {
            get { return _ftpRemoteDir.Trim(); }
            set { _ftpRemoteDir = value.Trim(); }
        }
        public int FtpPort
        {
            get { return _ftpPort; }
            set { _ftpPort = value; }
        }
        public bool Hidden
        {
            get { return _hidden; }
            set { _hidden = value; }
        }
        public string EmergencyMessage
        {
            get { return _emergencyMessage; }
            set { _emergencyMessage = value; }
        }
        public int AlertsInterval
        {
            get { return _alertsInterval; }
            set { _alertsInterval = value; }
        }
        public bool ActiveTopSecret
        {
            get { return _activeTopSecret; }
            set { _activeTopSecret = value; }
        }
        public string WebEMailAccount
        {
            get { return _webEMailAccount; }
            set { _webEMailAccount = value; }
        }
        public string WebPassword
        {
            get { return _webPassword; }
            set { _webPassword = value; }
        }
        public CellIDProvider CellIDConverterProvider
        {
            get { return _cellIDProvider; }
            set { _cellIDProvider = value; }
        }
        #endregion
        
        public Configuration(string appPath, Languages languages)
        {
            _appPath = appPath;
            _languages = languages;

            _memoryCards = Kernel.GetMemoryCardsDirectories;
        }

        public int LoadConfiguration(bool useWebConfig)
        {
            int result = 0;

            defaultLanguage = 0;
            defaultGPSInteractions = 50;
            defaultNumber1 = "";
            defaultNumber2 = "";
            defaultNumber3 = "";
            defaultNumber4 = "";
            emergencyEMail1 = "";
            emergencyEMail2 = "";
            emergencyEMail3 = "";
            emergencyEMail4 = "";
            defaultPassword = "";
            SecretAnswer = "";
            SecretQuestion = "";
            defaultEMailAccount = "";
            defaultrecipientName = "";
            defaultrecipientEMail = "";
            defaultSubject = "";
            IMSI1 = "";
            IMSI2 = "";
            IMSI3 = "";
            IMSI4 = "";
            AliasIMSI1 = "";
            AliasIMSI2 = "";
            AliasIMSI3 = "";
            AliasIMSI4 = "";
            TimeELTCommand = 5;
            AlarmSound = _appPath + "alarmfindme.wav";
            DebugMode = false;
            ScreenOff = true;
            FtpServer = "";
            EmergencyMessage = Messages.msg_DefaultEmergencyMessage;
            FtpUser = "";
            FtpPassword = "";
            FtpPort = 21;
            FtpRemoteDir = "/";
            Hidden = false;
            AlertsInterval = 5;
            ActiveTopSecret = false;
            WebEMailAccount = "";
            WebPassword = "";

            CellIDConverterProvider = CellIDProvider.OpenCellID;

            // Load configuration from Registry
            RegistryKey r = Registry.LocalMachine.OpenSubKey(defaultRegistryKey);
            if (r != null)
            {
                try
                {
                    defaultLanguage = System.Convert.ToInt32(r.GetValue("language", "0"));
                }
                catch { defaultLanguage = 0; }
                try
                {
                    defaultGPSInteractions = System.Convert.ToInt32(r.GetValue("interactions", "50"));
                }
                catch { defaultGPSInteractions = 50; }
                defaultNumber1 = SimpleCryptography.DeCryptography((string)r.GetValue("defnum", ""));
                defaultNumber2 = SimpleCryptography.DeCryptography((string)r.GetValue("defnum2", ""));
                defaultNumber3 = SimpleCryptography.DeCryptography((string)r.GetValue("defnum3", ""));
                defaultNumber4 = SimpleCryptography.DeCryptography((string)r.GetValue("defnum4", ""));
                emergencyEMail1 = SimpleCryptography.DeCryptography((string)r.GetValue("defeem1", ""));
                emergencyEMail2 = SimpleCryptography.DeCryptography((string)r.GetValue("defeem2", ""));
                emergencyEMail3 = SimpleCryptography.DeCryptography((string)r.GetValue("defeem3", ""));
                emergencyEMail4 = SimpleCryptography.DeCryptography((string)r.GetValue("defeem4", ""));
                defaultPassword = SimpleCryptography.DeCryptography((string)r.GetValue("defpas", ""));
                SecretQuestion = SimpleCryptography.DeCryptography((string)r.GetValue("defqst", ""));
                SecretAnswer = SimpleCryptography.DeCryptography((string)r.GetValue("defans", ""));
                defaultEMailAccount = SimpleCryptography.DeCryptography((string)r.GetValue("account", "0"));
                defaultrecipientName = SimpleCryptography.DeCryptography((string)r.GetValue("defdes", ""));
                defaultrecipientEMail = SimpleCryptography.DeCryptography((string)r.GetValue("defema", ""));
                defaultSubject = SimpleCryptography.DeCryptography((string)r.GetValue("defsub", ""));
                try
                {
                    TimeELTCommand = System.Convert.ToInt32(r.GetValue("defelt", "5"));
                }
                catch { TimeELTCommand = 5; }
                IMSI1 = SimpleCryptography.DeCryptography((string)r.GetValue("defsmc", "")).Trim();
                IMSI2 = SimpleCryptography.DeCryptography((string)r.GetValue("defsmd", "")).Trim();
                IMSI3 = SimpleCryptography.DeCryptography((string)r.GetValue("defsme", "")).Trim();
                IMSI4 = SimpleCryptography.DeCryptography((string)r.GetValue("defsmf", "")).Trim();
                AliasIMSI1 = SimpleCryptography.DeCryptography((string)r.GetValue("defama", "")).Trim();
                AliasIMSI2 = SimpleCryptography.DeCryptography((string)r.GetValue("defamb", "")).Trim();
                AliasIMSI3 = SimpleCryptography.DeCryptography((string)r.GetValue("defamc", "")).Trim();
                AliasIMSI4 = SimpleCryptography.DeCryptography((string)r.GetValue("defamd", "")).Trim();
                AlarmSound = SimpleCryptography.DeCryptography((string)r.GetValue("almsnd", "")).Trim();
                WebEMailAccount = SimpleCryptography.DeCryptography((string)r.GetValue("defweb", "")).Trim();
                WebPassword = SimpleCryptography.DeCryptography((string)r.GetValue("defwpa", "")).Trim();
                DebugMode = ((string)r.GetValue("defdbg", "N")).Equals("Y");
                ScreenOff = ((string)r.GetValue("scroff", "Y")).Equals("Y");
                GpsType = ((string)r.GetValue("gpstype", "W")).Equals("W") ? GPSType.Windows : GPSType.Manual;
                ComPort = (string)r.GetValue("gpscom", "");
                BaudRate = (string)r.GetValue("gpsbaud", "");
                FtpServer = SimpleCryptography.DeCryptography((string)r.GetValue("ftpsrv", "")).Trim();
                EmergencyMessage = SimpleCryptography.DeCryptography((string)r.GetValue("defmm1", "")).Trim();
                FtpUser = SimpleCryptography.DeCryptography((string)r.GetValue("ftpusr", "")).Trim();
                FtpPassword = SimpleCryptography.DeCryptography((string)r.GetValue("ftppwd", "")).Trim();
                FtpRemoteDir = SimpleCryptography.DeCryptography((string)r.GetValue("ftpdir", "/")).Trim();
                try
                {
                    AlertsInterval = System.Convert.ToInt32(r.GetValue("defmm2", "5"));
                }
                catch
                {
                    AlertsInterval = 5;
                }
                ActiveTopSecret = ((string)r.GetValue("defats", "N")).Equals("Y");
                try
                {
                    FtpPort = System.Convert.ToInt32(r.GetValue("ftppor", "21"));
                }
                catch { FtpPort = 21; }
                Hidden = ((string)r.GetValue("hd", "N")).Equals("Y"); // DO NOT ADD THIS TO REGCREATOR!!!

                string cidp = (string)r.GetValue("cidp", "0");
                if (cidp.Equals("1"))
                    CellIDConverterProvider = CellIDProvider.Google;
                else if (cidp.Equals("2"))
                    CellIDConverterProvider = CellIDProvider.CellDB;
                else
                    CellIDConverterProvider = CellIDProvider.OpenCellID;

                r.Close();
            }

            // The software is not configured!!! Try to load backups!
            if (defaultNumber1.Equals("") && defaultLanguage == 0 && IMSI1.Equals(""))
            {
                // First backup, the same directory
                string[] mainBackup = LoadBackup(_appPath);

                if (mainBackup.Length > 0)
                {
                    FillConfiguration(mainBackup);
                }
                else
                {
                    bool backupLoaded = false;

                    foreach (string folder in _memoryCards)
                    {
                        string[] mcBackup = LoadBackup(folder);

                        if (mcBackup.Length > 0)
                        {
                            FillConfiguration(mcBackup);
                            backupLoaded = true;
                            break;
                        }
                    }

                    // Try to load backup from web
                    if (!backupLoaded && useWebConfig)
                        GetWebConfiguration();
                }
            }

            if (AlarmSound == "")
            {
                AlarmSound = _appPath + "alarmfindme.wav";
            }

            if (!_languages.LoadLanguages(_appPath))
            {
                result = 1;
            }

            return result;
        }

        public string[] GetWebConfiguration()
        {
            PhoneInfo pi = new PhoneInfo();
            string imei = pi.GetIMEI();
            Debug.AddLog("GetWebConfiguration: IMEI=" + imei, true);

            string res = Meedios.GetConfig(imei);

            if (res != string.Empty)
            {
                Debug.AddLog("GetWebConfiguration: GetConfig ok: " + res, true);
                char[] delimiterChars = { ';' };
                string[] items = res.Split(delimiterChars);

                foreach (string item in items)
                {
                    Debug.AddLog("GetWebConfiguration: GetConfig item= " + item, true);

                    if (item.ToLower().StartsWith("imsi"))
                    {
                        if (IMSI1.Equals(""))
                            IMSI1 = item.Substring(item.IndexOf("=") + 1);
                        else if (IMSI2.Equals(""))
                            IMSI2 = item.Substring(item.IndexOf("=") + 1);
                        else if (IMSI3.Equals(""))
                            IMSI3 = item.Substring(item.IndexOf("=") + 1);
                        else if (IMSI4.Equals(""))
                            IMSI4 = item.Substring(item.IndexOf("=") + 1);
                    }
                    else if (item.ToLower().StartsWith("phone"))
                    {
                        if (defaultNumber1.Equals(""))
                            defaultNumber1 = item.Substring(item.IndexOf("=") + 1);
                        else if (defaultNumber2.Equals(""))
                            defaultNumber2 = item.Substring(item.IndexOf("=") + 1);
                        else if (defaultNumber3.Equals(""))
                            defaultNumber3 = item.Substring(item.IndexOf("=") + 1);
                        else if (defaultNumber4.Equals(""))
                            defaultNumber4 = item.Substring(item.IndexOf("=") + 1);
                    }
                    else if (item.ToLower().StartsWith("email"))
                    {
                        if (emergencyEMail1.Equals(""))
                            emergencyEMail1 = item.Substring(item.IndexOf("=") + 1);
                        else if (emergencyEMail2.Equals(""))
                            emergencyEMail2 = item.Substring(item.IndexOf("=") + 1);
                        else if (emergencyEMail3.Equals(""))
                            emergencyEMail3 = item.Substring(item.IndexOf("=") + 1);
                        else if (emergencyEMail4.Equals(""))
                            emergencyEMail4 = item.Substring(item.IndexOf("=") + 1);
                    }
                }

                SaveConfiguration();

                return items;
            }
            else
                Debug.AddLog("GetWebConfiguration: GetConfig failed.", true);

            return null;
        }

        void FillConfiguration(string[] conf)
        {
            int pos;

            foreach (string line in conf)
            {
                pos = line.IndexOf('=');

                if (line.Substring(0, pos).ToLower().Equals("language"))
                    defaultLanguage = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("interactions"))
                    defaultGPSInteractions = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defnum"))
                    defaultNumber1 = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defnum2"))
                    defaultNumber2 = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defnum3"))
                    defaultNumber3 = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defnum4"))
                    defaultNumber4 = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defeem1"))
                    emergencyEMail1 = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defeem2"))
                    emergencyEMail2 = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defeem3"))
                    emergencyEMail3 = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defeem4"))
                    emergencyEMail4 = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defpas"))
                    defaultPassword = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defqst"))
                    SecretQuestion = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defans"))
                    SecretAnswer = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("account"))
                    defaultEMailAccount = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defdes"))
                    defaultrecipientName = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defema"))
                    defaultrecipientEMail = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defsub"))
                    defaultSubject = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defelt"))
                    TimeELTCommand = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defsmc"))
                    IMSI1 = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defsmd"))
                    IMSI2 = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defsme"))
                    IMSI3 = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defsmf"))
                    IMSI4 = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defama"))
                    AliasIMSI1 = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defamb"))
                    AliasIMSI2 = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defamc"))
                    AliasIMSI3 = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defamd"))
                    AliasIMSI4 = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("almsnd"))
                    AlarmSound = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("gpstype"))
                    GpsType = (line.Substring(pos + 1).Equals("W") ? GPSType.Windows : GPSType.Windows);
                else if (line.Substring(0, pos).ToLower().Equals("gpscom"))
                    ComPort = line.Substring(pos + 1);
                else if (line.Substring(0, pos).ToLower().Equals("gpsbaud"))
                    BaudRate = line.Substring(pos + 1);
                else if (line.Substring(0, pos).ToLower().Equals("defdbg"))
                    DebugMode = line.Substring(pos + 1).Equals("Y");
                else if (line.Substring(0, pos).ToLower().Equals("scroff"))
                    ScreenOff = line.Substring(pos + 1).Equals("Y");
                else if (line.Substring(0, pos).ToLower().Equals("ftpsrv"))
                    FtpServer = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("ftpusr"))
                    FtpUser = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("ftppwd"))
                    FtpPassword = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("ftpdir"))
                    FtpRemoteDir = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("ftppor"))
                    FtpPort = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("hd"))
                    Hidden = line.Substring(pos + 1).Equals("Y");
                else if (line.Substring(0, pos).ToLower().Equals("defmm1"))
                    EmergencyMessage = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defmm2"))
                    AlertsInterval = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defats"))
                    ActiveTopSecret = line.Substring(pos + 1).Equals("Y");
                else if (line.Substring(0, pos).ToLower().Equals("defweb"))
                    WebEMailAccount = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defwpa"))
                    WebPassword = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("cidp"))
                {
                    string cidp = line.Substring(pos + 1);
                    if (cidp.Equals("1"))
                        CellIDConverterProvider = CellIDProvider.Google;
                    else if (cidp.Equals("2"))
                        CellIDConverterProvider = CellIDProvider.CellDB;
                    else
                        CellIDConverterProvider = CellIDProvider.OpenCellID;
                }
            }

            SaveConfiguration();
        }

        public void SaveConfiguration()
        {
            // Save configuration to Registry
            try
            {
                RegistryKey r = Registry.LocalMachine.CreateSubKey(defaultRegistryKey);

                if (r != null)
                {
                    r.SetValue("language", System.Convert.ToString(defaultLanguage));
                    r.SetValue("interactions", defaultGPSInteractions.ToString());
                    r.SetValue("defnum", SimpleCryptography.Cryptography(defaultNumber1));
                    r.SetValue("defnum2", SimpleCryptography.Cryptography(defaultNumber2));
                    r.SetValue("defnum3", SimpleCryptography.Cryptography(defaultNumber3));
                    r.SetValue("defnum4", SimpleCryptography.Cryptography(defaultNumber4));
                    r.SetValue("defeem1", SimpleCryptography.Cryptography(emergencyEMail1));
                    r.SetValue("defeem2", SimpleCryptography.Cryptography(emergencyEMail2));
                    r.SetValue("defeem3", SimpleCryptography.Cryptography(emergencyEMail3));
                    r.SetValue("defeem4", SimpleCryptography.Cryptography(emergencyEMail4));
                    r.SetValue("defpas", SimpleCryptography.Cryptography(defaultPassword));
                    r.SetValue("defqst", SimpleCryptography.Cryptography(SecretQuestion));
                    r.SetValue("defans", SimpleCryptography.Cryptography(SecretAnswer));
                    r.SetValue("account", SimpleCryptography.Cryptography(defaultEMailAccount));
                    r.SetValue("defdes", SimpleCryptography.Cryptography(defaultrecipientName));
                    r.SetValue("defema", SimpleCryptography.Cryptography(defaultrecipientEMail));
                    r.SetValue("defsub", SimpleCryptography.Cryptography(defaultSubject));
                    r.SetValue("defelt", TimeELTCommand.ToString());
                    r.SetValue("defsmc", SimpleCryptography.Cryptography(IMSI1.Trim()));
                    r.SetValue("defsmd", SimpleCryptography.Cryptography(IMSI2.Trim()));
                    r.SetValue("defsme", SimpleCryptography.Cryptography(IMSI3.Trim()));
                    r.SetValue("defsmf", SimpleCryptography.Cryptography(IMSI4.Trim()));
                    r.SetValue("defama", SimpleCryptography.Cryptography(AliasIMSI1.Trim()));
                    r.SetValue("defamb", SimpleCryptography.Cryptography(AliasIMSI2.Trim()));
                    r.SetValue("defamc", SimpleCryptography.Cryptography(AliasIMSI3.Trim()));
                    r.SetValue("defamd", SimpleCryptography.Cryptography(AliasIMSI4.Trim()));
                    r.SetValue("almsnd", SimpleCryptography.Cryptography(AlarmSound));
                    r.SetValue("defdbg", (DebugMode ? "Y" : "N"));
                    r.SetValue("scroff", (ScreenOff ? "Y" : "N"));
                    r.SetValue("defweb", SimpleCryptography.Cryptography(WebEMailAccount.Trim()));
                    r.SetValue("defwpa", SimpleCryptography.Cryptography(WebPassword.Trim()));
                    r.SetValue("gpstype", (GpsType == GPSType.Windows ? "W" : "M"));
                    r.SetValue("gpscom", ComPort);
                    r.SetValue("gpsbaud", BaudRate);
                    r.SetValue("ftpsrv", SimpleCryptography.Cryptography(FtpServer.Trim()));
                    r.SetValue("ftpusr", SimpleCryptography.Cryptography(FtpUser.Trim()));
                    r.SetValue("ftppwd", SimpleCryptography.Cryptography(FtpPassword.Trim()));
                    r.SetValue("ftpdir", SimpleCryptography.Cryptography(FtpRemoteDir.Trim()));
                    r.SetValue("ftppor", FtpPort.ToString());
                    r.SetValue("defmm1", SimpleCryptography.Cryptography(EmergencyMessage.Trim()));
                    r.SetValue("defmm2", System.Convert.ToString(AlertsInterval).Trim());
                    r.SetValue("defats", ActiveTopSecret ? "Y" : "N");
                    r.SetValue("hd", Hidden ? "Y" : "N"); // DO NOT ADD THIS TO REGCREATOR!!!
                    r.SetValue("cidp", (CellIDConverterProvider == CellIDProvider.Google ? "1" : (CellIDConverterProvider == CellIDProvider.CellDB ? "2" : "0")));

                    r.Close();
                }

                // Save backups //

                // First backup, the same directory
                CreateBackup(_appPath);

                // So, let's save backups in storage cards if present
                foreach (string folder in _memoryCards)
                {
                    CreateBackup(folder);
                }
            }
            catch
            {
            }
        }

        string[] LoadBackup(string folder)
        {
            string[] result = new string[0];

            try
            {
                string file = "rt.dll";

                if (!folder.EndsWith(@"\"))
                {
                    file = @"\" + file;
                }

                if (File.Exists(folder + file))
                {
                    DirectoryInfo di = new DirectoryInfo(folder + file);

                    if ((di.Attributes & FileAttributes.Hidden) != 0)
                    {
                        di.Attributes = FileAttributes.Normal;
                    }
                }
                
                using (StreamReader sr = new StreamReader(folder + file))
                {
                    // Process every line in the file
                    for (String Line = sr.ReadLine(); Line != null; Line = sr.ReadLine())
                    {
                        Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = SimpleCryptography.DeCryptography(Line);
                    }
                }

                DirectoryInfo d2 = new DirectoryInfo(folder + file);
                d2.Attributes = FileAttributes.Hidden;
            }
            catch
            {
            }

            return result;
        }

        void CreateBackup(string folder)
        {
            try
            {
                string file = "rt.dll";

                if (!folder.EndsWith(@"\"))
                {
                    file = @"\" + file;
                }

                if (File.Exists(folder + file))
                {
                    DirectoryInfo di = new DirectoryInfo(folder + file);

                    if ((di.Attributes & FileAttributes.Hidden) != 0)
                    {
                        di.Attributes = FileAttributes.Normal;
                    }

                    File.Delete(folder + file);
                }

                StreamWriter sw = File.CreateText(folder + file);
                sw.WriteLine(SimpleCryptography.Cryptography("language=" + System.Convert.ToString(defaultLanguage)));
                sw.WriteLine(SimpleCryptography.Cryptography("interactions=" + defaultGPSInteractions.ToString()));
                sw.WriteLine(SimpleCryptography.Cryptography("defnum=" + SimpleCryptography.Cryptography(defaultNumber1)));
                sw.WriteLine(SimpleCryptography.Cryptography("defnum2=" + SimpleCryptography.Cryptography(defaultNumber2)));
                sw.WriteLine(SimpleCryptography.Cryptography("defnum3=" + SimpleCryptography.Cryptography(defaultNumber3)));
                sw.WriteLine(SimpleCryptography.Cryptography("defnum4=" + SimpleCryptography.Cryptography(defaultNumber4)));
                sw.WriteLine(SimpleCryptography.Cryptography("defeem1=" + SimpleCryptography.Cryptography(emergencyEMail1)));
                sw.WriteLine(SimpleCryptography.Cryptography("defeem2=" + SimpleCryptography.Cryptography(emergencyEMail2)));
                sw.WriteLine(SimpleCryptography.Cryptography("defeem3=" + SimpleCryptography.Cryptography(emergencyEMail3)));
                sw.WriteLine(SimpleCryptography.Cryptography("defeem4=" + SimpleCryptography.Cryptography(emergencyEMail4)));
                sw.WriteLine(SimpleCryptography.Cryptography("defpas=" + SimpleCryptography.Cryptography(defaultPassword)));
                sw.WriteLine(SimpleCryptography.Cryptography("defqst=" + SimpleCryptography.Cryptography(SecretQuestion)));
                sw.WriteLine(SimpleCryptography.Cryptography("defans=" + SimpleCryptography.Cryptography(SecretAnswer)));
                sw.WriteLine(SimpleCryptography.Cryptography("account=" + SimpleCryptography.Cryptography(defaultEMailAccount)));
                sw.WriteLine(SimpleCryptography.Cryptography("defdes=" + SimpleCryptography.Cryptography(defaultrecipientName)));
                sw.WriteLine(SimpleCryptography.Cryptography("defema=" + SimpleCryptography.Cryptography(defaultrecipientEMail)));
                sw.WriteLine(SimpleCryptography.Cryptography("defsub=" + SimpleCryptography.Cryptography(defaultSubject)));
                sw.WriteLine(SimpleCryptography.Cryptography("defelt=" + TimeELTCommand.ToString()));
                sw.WriteLine(SimpleCryptography.Cryptography("defsmc=" + SimpleCryptography.Cryptography(IMSI1.Trim())));
                sw.WriteLine(SimpleCryptography.Cryptography("defsmd=" + SimpleCryptography.Cryptography(IMSI2.Trim())));
                sw.WriteLine(SimpleCryptography.Cryptography("defsme=" + SimpleCryptography.Cryptography(IMSI3.Trim())));
                sw.WriteLine(SimpleCryptography.Cryptography("defsmf=" + SimpleCryptography.Cryptography(IMSI4.Trim())));
                sw.WriteLine(SimpleCryptography.Cryptography("defama=" + SimpleCryptography.Cryptography(AliasIMSI1.Trim())));
                sw.WriteLine(SimpleCryptography.Cryptography("defamb=" + SimpleCryptography.Cryptography(AliasIMSI2.Trim())));
                sw.WriteLine(SimpleCryptography.Cryptography("defamc=" + SimpleCryptography.Cryptography(AliasIMSI3.Trim())));
                sw.WriteLine(SimpleCryptography.Cryptography("defamd=" + SimpleCryptography.Cryptography(AliasIMSI4.Trim())));
                sw.WriteLine(SimpleCryptography.Cryptography("almsnd=" + SimpleCryptography.Cryptography(AlarmSound)));
                sw.WriteLine(SimpleCryptography.Cryptography("defdbg=" + (DebugMode ? "Y" : "N")));
                sw.WriteLine(SimpleCryptography.Cryptography("scroff=" + (ScreenOff ? "Y" : "N")));
                sw.WriteLine(SimpleCryptography.Cryptography("gpstype=" + (GpsType == GPSType.Windows ? "W" : "M")));
                sw.WriteLine(SimpleCryptography.Cryptography("gpscom=" + ComPort));
                sw.WriteLine(SimpleCryptography.Cryptography("gpsbaud=" + BaudRate));
                sw.WriteLine(SimpleCryptography.Cryptography("ftpsrv=" + SimpleCryptography.Cryptography(FtpServer)));
                sw.WriteLine(SimpleCryptography.Cryptography("ftpusr=" + SimpleCryptography.Cryptography(FtpUser)));
                sw.WriteLine(SimpleCryptography.Cryptography("ftppwd=" + SimpleCryptography.Cryptography(FtpPassword)));
                sw.WriteLine(SimpleCryptography.Cryptography("ftpdir=" + SimpleCryptography.Cryptography(FtpRemoteDir)));
                sw.WriteLine(SimpleCryptography.Cryptography("ftppor=" + FtpPort.ToString()));
                sw.WriteLine(SimpleCryptography.Cryptography("hd=" + (Hidden ? "Y" : "N")));
                sw.WriteLine(SimpleCryptography.Cryptography("defmm1=" + SimpleCryptography.Cryptography(EmergencyMessage)));
                sw.WriteLine(SimpleCryptography.Cryptography("defmm2=" + System.Convert.ToString(AlertsInterval)));
                sw.WriteLine(SimpleCryptography.Cryptography("defats=" + (ActiveTopSecret ? "Y" : "N")));
                sw.WriteLine(SimpleCryptography.Cryptography("defweb=" + SimpleCryptography.Cryptography(WebEMailAccount)));
                sw.WriteLine(SimpleCryptography.Cryptography("defwpa=" + SimpleCryptography.Cryptography(WebPassword)));
                sw.WriteLine(SimpleCryptography.Cryptography("cidp=" + (CellIDConverterProvider == CellIDProvider.Google ? "1" : (CellIDConverterProvider == CellIDProvider.CellDB ? "2" : "0"))));
                sw.Flush();
                sw.Close();
            
                DirectoryInfo d2 = new DirectoryInfo(folder + file);
                d2.Attributes = FileAttributes.Hidden;
            }        
            catch 
            { 
            }
        }

        void DeleteBackup(string folder)
        {
            string file = "rt.dll";

            if (!folder.EndsWith(@"\"))
            {
                file = @"\" + file;
            }

            if (File.Exists(folder + file))
            {
                DirectoryInfo di = new DirectoryInfo(folder + file);

                if ((di.Attributes & FileAttributes.Hidden) != 0)
                {
                    di.Attributes = FileAttributes.Normal;
                }

                File.Delete(folder + file);
            }
        }

        public void RemoveConfigurations()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(defaultRegistryKey);

            if (r != null)
            {
                try { r.DeleteValue("language"); } catch { }
                try { r.DeleteValue("interactions"); } catch { }
                try { r.DeleteValue("defnum"); }  catch { }
                try { r.DeleteValue("defnum2"); } catch { }
                try { r.DeleteValue("defnum3"); } catch { }
                try { r.DeleteValue("defnum4"); } catch { }
                try { r.DeleteValue("defeem1"); } catch { }
                try { r.DeleteValue("defeem2"); } catch { }
                try { r.DeleteValue("defeem3"); } catch { }
                try { r.DeleteValue("defeem4"); } catch { }
                try { r.DeleteValue("defpas"); }  catch { }
                try { r.DeleteValue("defqst"); }  catch { }
                try { r.DeleteValue("defans"); }  catch { }
                try { r.DeleteValue("account"); } catch { }
                try { r.DeleteValue("defdes"); }  catch { }
                try { r.DeleteValue("defema"); }  catch { }
                try { r.DeleteValue("defsub"); }  catch { }
                try { r.DeleteValue("defelt"); }  catch { }
                try { r.DeleteValue("defsmc"); }  catch { }
                try { r.DeleteValue("defsmd"); }  catch { }
                try { r.DeleteValue("defsme"); }  catch { }
                try { r.DeleteValue("defsmf"); }  catch { }
                try { r.DeleteValue("defama"); }  catch { }
                try { r.DeleteValue("defamb"); }  catch { }
                try { r.DeleteValue("defamc"); }  catch { }
                try { r.DeleteValue("defamd"); }  catch { }
                try { r.DeleteValue("defats"); }  catch { }
                try { r.DeleteValue("defwpa"); }  catch { }
                try { r.DeleteValue("almsnd"); }  catch { }
                try { r.DeleteValue("defdbg"); }  catch { }
                try { r.DeleteValue("scroff"); }  catch { }
                try { r.DeleteValue("gpstype"); }  catch { }
                try { r.DeleteValue("gpscom"); }  catch { }
                try { r.DeleteValue("gpsbaud"); }  catch { }
                try { r.DeleteValue("ftpsrv"); }  catch { }
                try { r.DeleteValue("ftpusr"); }  catch { }
                try { r.DeleteValue("ftppwd"); }  catch { }
                try { r.DeleteValue("ftpdir"); }  catch { }
                try { r.DeleteValue("ftppor"); }  catch { }
                try { r.DeleteValue("defmm1"); }  catch { }
                try { r.DeleteValue("defmm2"); }  catch { }
                try { r.DeleteValue("cidp"); }  catch { }
                try { r.DeleteValue("defweb"); } catch { }
                try { r.DeleteValue("redir"); } catch { }
                try { r.DeleteValue("canred"); } catch { }
                try { r.DeleteValue("hd"); } catch { } 

                r.Close();
            }

            // First backup, the same directory
            DeleteBackup(_appPath);

            // So, let's save backups in storage cards if present
            foreach (string folder in _memoryCards)
            {
                DeleteBackup(folder);
            }        
        }
    }
}
