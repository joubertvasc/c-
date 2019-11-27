using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsMobile.Configuration;
using JVUtils;
using JVGPS;

namespace CommonDLL
{
    public class RTCommon
    {
        #region Variables
        // SMS Interceptor
        public MessageInterceptor mi;
        public string CLSID_RT = "{687A5082-B5CD-47c4-975D-A9CE20BE3726}";
        
        public string BlogAddress = "http://remotetracker.blogspot.com";

        //Configuration
        public string appPath;
        public string version;
        public int termOfServiceRevisionNumber;
        public Configuration configuration;
        public Languages languages;
        public LanguageXML languageXML;

        public string[] EMailAccounts = new string[0];
        public int selectedEMailAccount = -1;
        public GPS gps;
        public bool atm = false;

        public double lastCellIDLatitude = 0;
        public double lastCellIDLongitude = 0;

        // Internals
        public static string HiddenKey = "\\.rth";
        #endregion

        private void Constructor(string appName, bool bLoadWebConfig)
        {
            version = "0.4.3-8";
            termOfServiceRevisionNumber = 0;

            appPath = appName.Substring(0, appName.LastIndexOf(@"\") + 1);

            languages = new Languages();
            languageXML = new LanguageXML();
            configuration = new Configuration(appPath, languages);
            configuration.defaultRegistryKey = "\\Software\\Microsoft\\Windows\\CurrentVersion\\explorer";

            VerifyCustomizations();

            configuration.LoadConfiguration(bLoadWebConfig);

            // Load the list of e-mail accounts
            EMailAccounts = LoadAccountList();

            // Find default e-mail
            if (configuration.defaultEMailAccount != "")
            {
                for (int j = 0; j < EMailAccounts.Length; j++)
                {
                    if (EMailAccounts[j].ToString().ToLower().Equals(configuration.defaultEMailAccount.ToLower()))
                    {
                        selectedEMailAccount = j;
                        break;
                    }
                }
            }

            Debug.Logging = configuration.DebugMode;

            Commands.AppPath = appPath;
            Commands.configuration = configuration;
        }

        public RTCommon(string appName)
        {
            Constructor(appName, true);
        }

        public RTCommon(string appName, bool bLoadWebConfig)
        {
            Constructor(appName, bLoadWebConfig);
        }

        private void VerifyCustomizations()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey(configuration.defaultRegistryKey);
            if (r != null)
            {
                try
                {
                    if (r != null)
                        atm = System.Convert.ToInt32(r.GetValue("atm", 0)) == 1;
                }
                catch { }

                r.Close();
            }
        }

        public bool TermOfServiceWasAccepted()
        {
            bool result = true;

            if (File.Exists(appPath + "rtconfig.html"))
            {
                string contractName = "RT " + version.Substring(0, version.IndexOf('-'));
                result = JVUtils.JVUtils.Get_ContractWasAccepted("RT", termOfServiceRevisionNumber);

                if (!result)
                {
                    JVUtils.Forms.Contract c = new JVUtils.Forms.Contract();
                    c.ContractHTMLFileName = appPath + "rtconfig.html";

                    if (c.ShowDialog() == DialogResult.OK)
                    {
                        JVUtils.JVUtils.Set_ContractAccepted("RT", termOfServiceRevisionNumber);
                        result = true;
                    }
                }
            }

            if (result)
            {
                // Set SMS interceptor
                CreateInterceptor();

                // Create SMSLauncher startup link
                CreateStartupLink();

                // Create link to AppToDate
                CreateAppToDateLink();
            }

            return result;
        }

        public bool WEBToSWasAccepted()
        {
            bool result = true;

            if (File.Exists(appPath + "rtweb.html"))
            {
                string contractName = "RT-WEB " + version.Substring(0, version.IndexOf('-'));
                result = JVUtils.JVUtils.Get_ContractWasAccepted("RT-WEB", termOfServiceRevisionNumber);

                if (!result)
                {
                    JVUtils.Forms.Contract c = new JVUtils.Forms.Contract();
                    c.ContractHTMLFileName = appPath + "rtweb.html";

                    if (c.ShowDialog() == DialogResult.OK)
                    {
                        JVUtils.JVUtils.Set_ContractAccepted("RT-WEB", termOfServiceRevisionNumber);
                        result = true;
                    }
                }
            }

            return result;
        }

        public void ConfigureGPS()
        {
            JVGPS.Forms.GPSSettings settings = new JVGPS.Forms.GPSSettings();
            settings.SelectedSerialPort = configuration.ComPort;
            settings.SelectedBaudRate = Utils.ConvertStringToBaudRate(configuration.BaudRate);
            settings.UsingWindowsGPS = configuration.GpsType == GPSType.Windows;
            settings.SerialPortText = Messages.msg_SerialPort;
            settings.BaudRateText = Messages.msg_BaudRate;
            settings.Caption = Messages.msg_SelectGPSType;
            settings.MenuCancelText = Messages.msg_Cancel;
            settings.MenuOkText = Messages.msg_Ok;
            settings.MessageBaudRateNotSelected = Messages.msg_BaudRateNotSelected;
            settings.MessageSerialPortNotSelected = Messages.msg_ComPortNotSelected;
            settings.UseManualGPSText = Messages.msg_UseManualGPS;
            settings.UseWindowsGPSText = Messages.msg_UseManagedGPS;

            if (settings.ShowDialog() == DialogResult.OK)
            {
                configuration.GpsType = settings.UsingWindowsGPS ? GPSType.Windows : GPSType.Manual;
                configuration.ComPort = settings.SelectedSerialPort;
                configuration.BaudRate = Utils.ConvertBaudeRateToString(settings.SelectedBaudRate);
                Debug.AddLog("GPS configuration changed to: " +
                    (configuration.GpsType == GPSType.Manual ?
                    ("Manual " + " port " + configuration.ComPort + " baudrate " + configuration.BaudRate) :
                    "Windows"));
            }
        }

        public void CreateInterceptor()
        {
            CreateInterceptor("");
        }

        public void CreateInterceptor(string appName)
        {
            mi = new MessageInterceptor();
            mi.InterceptionAction = InterceptionAction.NotifyAndDelete;

            mi.MessageCondition = new MessageCondition(
              MessageProperty.Body, MessagePropertyComparisonType.Contains, "rt#");
            mi.MessageCondition.CaseSensitive = false;

            string path = appName;
            
            if (path.Equals(""))
                path = GetRTPath();

            if (path.Equals(""))
                path = appPath + "rt.exe";

            RemoveInterceptor();

            Debug.AddLog("Creating interceptor: " + path, true);

            mi.EnableApplicationLauncher("Configuration", path, "/hide");

            DeleteInvalidKeys();
            CreateInterceptorMethod2();
        }

        public void CreateInterceptorMethod2()
        {
            RemoveInterceptorMethod2();

            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\Inbox\\SVC\\SMS\\Rules");

            if (r != null)
            {
                try
                {
                    Debug.AddLog("Creating Rule Method 2", true);
                    r.SetValue(CLSID_RT, 1);
                    Debug.AddLog("Rule method 2 was created", true);
                }
                catch (Exception e)
                {
                    Debug.AddLog("Error creating Rule method 2: " + Utils.GetOnlyErrorMessage(e.Message), true);
                }

                r.Close();
            }

            RegistryKey r2 = Registry.ClassesRoot.CreateSubKey("\\CLSID\\" + CLSID_RT + "\\InprocServer32");

            if (r2 != null)
            {
                try
                {
                    Debug.AddLog("Creating CLSID Method 2", true);
                    
                    string path = Utils.ExtractFilePath(GetRTPath());
                    if (!path.EndsWith("\\"))
                        path += "\\";

                    r2.SetValue("Default", path + "RTRule.dll");
                    Debug.AddLog("CLSID method 2 was created: " + path + "RTRule.dll", true);
                }
                catch (Exception e)
                {
                    Debug.AddLog("Error creating CLSID method 2: " + Utils.GetOnlyErrorMessage(e.Message), true);
                }

                r2.Close();
            }
            
//            DeleteInvalidKeys();
        }

        public void RemoveInterceptor()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\Inbox\\Rules");

            if (r != null)
            {
                try
                {
                    Debug.AddLog("Removing Interceptor", true);
                    r.DeleteSubKey("Configuration");
                }
                catch (Exception e)
                {
                    Debug.AddLog("Error removing interceptor: " + Utils.GetOnlyErrorMessage(e.Message), true);
                }

                r.Close();
            }
        }

        public void RemoveInterceptorMethod2()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\Inbox\\SVC\\SMS\\Rules");

            if (r != null)
            {
                try
                {
                    Debug.AddLog("Deleting Rule Method 2", true);
                    r.DeleteValue(CLSID_RT);
                    Debug.AddLog("Rule method 2 was deleted", true);
                }
                catch (Exception e)
                {
                    Debug.AddLog("Error deleting Rule method 2: " + Utils.GetOnlyErrorMessage(e.Message), true);
                }

                r.Close();
            }

            RegistryKey r2 = Registry.ClassesRoot.CreateSubKey("\\CLSID");

            try
            {
                r2.DeleteSubKeyTree(CLSID_RT);
            }
            catch (Exception e)
            {
                Debug.AddLog("Error deleting Rule method 2: " + Utils.GetOnlyErrorMessage(e.Message), true);
            }

            if (r2 != null)
                r2.Close();
        }

        public void CreateStartupLink()
        {
            Utils.RemoveAppFromInit(GetSMSLauncherPath(), true);
            // TODO: how to make it working for any folder?
            Utils.RemoveAppFromInit(ShellFolders.WindowsFolder + "\\sle", false);

            if (!Utils.IsAppRegisteredInInit(GetSMSLauncherPath()))
            {
                Utils.AddAppToInit(GetSMSLauncherPath());

                // Some devices can't add entries in HKLM\Init, so, we need to add a link in startup folder
                if (!Utils.IsAppRegisteredInInit(GetSMSLauncherPath()))
                    Utils.CreateLink(appPath, "SMSLauncher.exe",
                     ShellFolders.StartUpFolder, "SMSLauncher.lnk", "");
            }
        }

        public void CreateStartMenuLinks()
        {
            Utils.CreateLink(appPath, "Config.exe",
                             ShellFolders.StartMenuProgramsFolder, "Config.lnk", "");
            Utils.CreateLink(appPath, "RTRemote.exe",
                            ShellFolders.StartMenuProgramsFolder, "RTRemote.lnk", "");
            Utils.CreateLink(appPath, "TopSecret.exe",
                            ShellFolders.StartMenuProgramsFolder, "TopSecret.lnk", "");

            if (File.Exists(appPath + "Config.lnk"))
                File.Delete(appPath + "Config.lnk");
            if (File.Exists(appPath + "RTRemote.lnk"))
                File.Delete(appPath + "RTRemote.lnk");
            if (File.Exists(appPath + "TopSecret.lnk"))
                File.Delete(appPath + "TopSecret.lnk");
        }

        public void CreateAppToDateLink()
        {
            if (!configuration.Hidden)
            {
                RegistryKey r = Registry.CurrentUser.CreateSubKey("\\Software\\MoDaCo\\AppToDate\\XML");

                if (r != null)
                {
                    r.SetValue("RTConfig", appPath + "config.xml");

                    r.Close();
                }
            }
        }

        public void RemoveAppToDateLink()
        {
            RegistryKey r = Registry.CurrentUser.OpenSubKey("\\Software\\MoDaCo\\AppToDate\\XML");

            if (r != null)
            {
                try
                {
                    r.DeleteSubKey("RTConfig");
                    r.Close();
                }
                catch { }
            }
        }

        public string[] LoadAccountList()
        {
            string[] accounts = new string[0];

            OutlookSession session = new OutlookSession();

            foreach (EmailAccount account in session.EmailAccounts)
            {
                Array.Resize(ref accounts, accounts.Length+1);
                accounts[accounts.Length-1] = account.Name;
            }

            session.Dispose();

            return accounts;
        }

        public void LoadAccountList(ComboBox cbEMailAccount)
        {
            string[] accounts = LoadAccountList();
            cbEMailAccount.Items.Clear();

            foreach (string account in accounts)
            {
                cbEMailAccount.Items.Add(account);
            }
        }

        public string GetIMSI()
        {
            PhoneInfo pi = new PhoneInfo();
            return pi.GetIMSI();
        }

        public string GetIMEI()
        {
            PhoneInfo pi = new PhoneInfo();
            return pi.GetIMEI();
        }

        public string[] GetStaff()
        {
            string[] result = new string[13];

            result [0] = Messages.msg_Author + ": Joubert Vasconcelos";
            result [1] = "";
            result [2] = Messages.msg_Translators + ":";
            result [3] = "Cybermoose, " + Messages.lang_dutch;
            result [4] = "Popy, " + Messages.lang_spanish;
            result [5] = "Naboleo, " + Messages.lang_french;
            result [6] = "Kasjopaja, " + Messages.lang_german;
            result [7] = "Aspe, " + Messages.lang_italian;
            result [8] = "Drageloth, " + Messages.lang_greek;
            result [9] = "Lind, " + Messages.lang_danish;
            result [10] = "Nick Dragomir, " + Messages.lang_romanian;
            result[11] = "Tom, " + Messages.lang_swedish;
            result[12] = "xx, " + Messages.lang_russian;

            return result;
        }

        public void ShowCommandLog()
        {
            CommonDLL.Forms.RTCommandLogViwer lv = new CommonDLL.Forms.RTCommandLogViwer();
            lv.logPath = appPath + "RTCommandLog.txt";
            lv.Show();
        }

        public void ShowHelp()
        {
            // Help
            if (languages.count >= (configuration.defaultLanguage + 1))
            {
                if (File.Exists(appPath + languages.helpFileName(configuration.defaultLanguage)))
                {
                    try
                    {
                        JVUtils.Forms.Browser wbHelp = new JVUtils.Forms.Browser();
                        wbHelp.ChangeCaption(languageXML.getColumn("menu_help", "Help"));
                        wbHelp.miOk.Text = Messages.msg_Ok;
                        wbHelp.Show();
                        wbHelp.URL(appPath + languages.helpFileName(configuration.defaultLanguage));
                    }
                    catch
                    {
                        MessageBox.Show("Could not open help file. Please verify your PIE configuration.", Messages.msg_Error);
                    }
                }
            }
            else
                MessageBox.Show("There is no language file in folder.", Messages.msg_Error);
        }

        public void ConfigureAlarm()
        {
            JVUtils.Forms.NewOpenDialog nod = new JVUtils.Forms.NewOpenDialog();
            nod.InitialDir = @"\";
            nod.SetText = Messages.msg_selectfile;
            nod.ItemCancelCaption = Messages.msg_Cancel;
            nod.ItemOKCaption = Messages.msg_Ok;
            nod.FileMask = "*.wav";
            nod.ItemPlaySoundCaption = Messages.msg_playsound;
            nod.InitialFileName = configuration.AlarmSound;

            if (nod.ShowDialog() == DialogResult.OK)
            {
                configuration.AlarmSound = nod.FileName;
            }
        }

        public void RegisterBinaries(string rtExe, string smsLauncherExe, string lockExe)
        {
            RegistryKey r = Registry.ClassesRoot.CreateSubKey(HiddenKey);
            if (r != null)
            {
                r.SetValue("nm1", SimpleCryptography.Cryptography(rtExe));
                r.SetValue("nm2", SimpleCryptography.Cryptography(smsLauncherExe));
                r.SetValue("nm3", SimpleCryptography.Cryptography(lockExe));

                r.Close();
            }
        }

        public void UnRegisterBinaries()
        {
            try
            {
                Debug.AddLog("UnRegisterBinaries: Before delete key", true);

                Registry.ClassesRoot.DeleteSubKeyTree(HiddenKey);
                Debug.AddLog("UnRegisterBinaries: After delete key", true);
            }
            catch (Exception e)
            {
                Debug.AddLog("UnRegisterBinaries: Error deleting key: " + Utils.GetOnlyErrorMessage(e.Message), true);
            }
        }

        static string GetBinaryPath(string name)
        {
            string result = "";
            RegistryKey r = Registry.ClassesRoot.OpenSubKey(HiddenKey);
            if (r != null)
            {
                result = SimpleCryptography.DeCryptography((string)r.GetValue(name));
                r.Close();
            }

            return result;
        }

        public static string GetRTPath()
        {
            string result = "";
            RegistryKey r = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Inbox\\Rules\\Configuration");
            if (r != null)
            {
                result = (string)r.GetValue("Command");
                r.Close();
            }

            return result;
        }

        public static string GetSMSLauncherPath()
        {
            return GetBinaryPath("nm2");
        }

        public static string GetLockPath()
        {
            return GetBinaryPath("nm3");
        }

        public void DeleteLinks()
        {
            Debug.AddLog("Deleting links", true);

            try
            {
                if (File.Exists(ShellFolders.StartMenuFolder + "\\Config.lnk"))
                    File.Delete(ShellFolders.StartMenuFolder + "\\Config.lnk");
            }
            catch (Exception e)
            {
                Debug.AddLog("DeleteLinks: error: " + Utils.GetOnlyErrorMessage(e.Message), true);
            }

            try
            {
                if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\Config.lnk"))
                    File.Delete(ShellFolders.StartMenuProgramsFolder + "\\Config.lnk");
            }
            catch (Exception e)
            {
                Debug.AddLog("DeleteLinks: error: " + Utils.GetOnlyErrorMessage(e.Message), true);
            }

            try
            {
                if (File.Exists(ShellFolders.StartMenuFolder + "\\RTRemote.lnk"))
                    File.Delete(ShellFolders.StartMenuFolder + "\\RTRemote.lnk");
            }
            catch (Exception e)
            {
                Debug.AddLog("DeleteLinks: error: " + Utils.GetOnlyErrorMessage(e.Message), true);
            }

            try
            {
                if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\RTRemote.lnk"))
                    File.Delete(ShellFolders.StartMenuProgramsFolder + "\\RTRemote.lnk");
            }
            catch (Exception e)
            {
                Debug.AddLog("DeleteLinks: error: " + Utils.GetOnlyErrorMessage(e.Message), true);
            }

            try
            {
                if (File.Exists(ShellFolders.StartMenuFolder + "\\TopSecret.lnk"))
                    File.Delete(ShellFolders.StartMenuFolder + "\\TopSecret.lnk");
            }
            catch (Exception e)
            {
                Debug.AddLog("DeleteLinks: error: " + Utils.GetOnlyErrorMessage(e.Message), true);
            }

            try
            {
                if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\TopSecret.lnk"))
                    File.Delete(ShellFolders.StartMenuProgramsFolder + "\\TopSecret.lnk");
            }
            catch (Exception e)
            {
                Debug.AddLog("DeleteLinks: error: " + Utils.GetOnlyErrorMessage(e.Message), true);
            }
        }

        public static bool Uninstall(string applicationName)
        {
            Debug.AddLog("Uninstall: is the application '" + applicationName + "' installed?", true);
            if (Directory.Exists(ShellFolders.WindowsFolder + "\\AppMgr\\" + applicationName))
            {
                Debug.AddLog("Uninstall: YES! Removing...", true);

                XmlDocument configDoc = new XmlDocument();
                configDoc.LoadXml(UninstallApp.CreateUnistallXML(applicationName));

                try
                {
                    ConfigurationManager.ProcessConfiguration(configDoc, false);
                    Debug.AddLog("Uninstall: Removed!", true);
                    return true;
                }
                catch (Exception e)
                {
                    Debug.AddLog("Uninstall: Error trying to remove RTConfig: " + Utils.GetOnlyErrorMessage(e.Message), true);
                    return false;
                }
            }
            else
            {
                Debug.AddLog("Uninstall: No.", true);
                return true;
            }
        }

        public void UninstallRemoteTracker(string app)
        {
            if (Uninstall(app))
            {
                // Removing old files from apptodate
                Debug.AddLog("Removing old AppToDate XMLs.", true);
                if (File.Exists("\\Application Data\\AppToDate\\remotetracker.xml"))
                    File.Delete("\\Application Data\\AppToDate\\remotetracker.xml");
                if (File.Exists("\\Application Data\\AppToDate\\remotetrackersp.xml"))
                    File.Delete("\\Application Data\\AppToDate\\remotetrackersp.xml");
                if (File.Exists("\\Application Data\\AppToDate\\remotetracker.ico"))
                    File.Delete("\\Application Data\\AppToDate\\remotetracker.ico");

                // Remove SMSLauncher link from StartUp
                Debug.AddLog("Removing old SMSLauncher startup link", true);
                if (File.Exists(ShellFolders.StartUpFolder + "\\SMSLauncher.lnk"))
                    File.Delete(ShellFolders.StartUpFolder + "\\SMSLauncher.lnk");

                // Remove RTConfig link from Start Menu
                Debug.AddLog("Removing old Start Menu links", true);
                if (File.Exists(ShellFolders.StartMenuFolder + "\\RTConfig.lnk"))
                    File.Delete(ShellFolders.StartMenuFolder + "\\RTConfig.lnk");
                if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\RTConfig.lnk"))
                    File.Delete(ShellFolders.StartMenuProgramsFolder + "\\RTConfig.lnk");

                // Remove RTConfigSP link from Start Menu
                if (File.Exists(ShellFolders.StartMenuFolder + "\\RTConfigSP.lnk"))
                    File.Delete(ShellFolders.StartMenuFolder + "\\RTConfigSP.lnk");
                if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\RTConfigSP.lnk"))
                    File.Delete(ShellFolders.StartMenuProgramsFolder + "\\RTConfigSP.lnk");

                // Remove RTConfig folder from Program files folder
                Debug.AddLog("Removing old RTConfig folder", true);
                if (Directory.Exists(ShellFolders.ProgramFilesFolder + "\\RTConfig"))
                    Directory.Delete(ShellFolders.ProgramFilesFolder + "\\RTConfig", true);

                // Remove Inbox Rules (Interceptors)
                Debug.AddLog("Removing old interceptors", true);
                RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Software\\Microsoft\\Inbox\\Rules");
                if (r != null)
                {
                    try
                    {
                        r.DeleteSubKey("RemoteTracker");
                        Debug.AddLog("The old interceptor (RemoteTracker) was removed.", true);
                    }
                    catch
                    {
                        Debug.AddLog("The old interceptor (RemoteTracker) does not exist.", true);
                    }

                    try
                    {
                        r.DeleteSubKey("RTConfig");
                        Debug.AddLog("The old interceptor (RTConfig) was removed.", true);
                    }
                    catch
                    {
                        Debug.AddLog("The old interceptor (RTConfig) does not exist.", true);
                    }

                    r.Close();
                }
            }
        }

        public void HideRemoteTracker()
        {
            ///////////////////////////////////////////////
            // Preparing RemoteTracker for hidden format //
            ///////////////////////////////////////////////

            string smsLauncherExe = "";
            string lockExe = "";
            string rtExe = "";
            
            // By default, copy RT to \Windows. But some OS Images can't use this folder. So, let's make a test and use another one.
            string newAppPath = ShellFolders.WindowsFolder + "\\";

            // TODO: test if the user can write in \Windows folder and use another one if not
            bool ok = false;
            do
            {
            } while (ok);

            // Generate random name for rt.exe
            do
            {
                Debug.AddLog("HideRemoteTracker: Generating random names for rt.exe");
                rtExe = Utils.GenerateRandomName("", ".exe");
            } while (File.Exists(newAppPath + rtExe));

            // Generate random name for smslauncher.exe
            do
            {
                Debug.AddLog("HideRemoteTracker: Generating random names for smslauncher.exe");
                smsLauncherExe = Utils.GenerateRandomName("sle", ".exe");
            }
            while (File.Exists(newAppPath + smsLauncherExe) || smsLauncherExe.Equals(rtExe));

            // Generate random name from lock.exe
            do
            {
                Debug.AddLog("HideRemoteTracker: Generating random names for lock.exe");
                lockExe = Utils.GenerateRandomName("", ".exe");
            } while (File.Exists(newAppPath + lockExe) || lockExe.Equals(rtExe) || lockExe.Equals(smsLauncherExe));

            Debug.AddLog("HideRemoteTracker: new names: rt=" + rtExe + 
                         " smslauncher=" + smsLauncherExe + " lock=" + lockExe, true);

            // Copy the necessary files to \Windows folder
            Debug.AddLog("HideRemoteTracker: Copy files. *.dll");
            string[] files = Utils.ListFilesInFolder("*.dll", appPath);
            foreach(string file in files)
                Utils.ForceCopyFile(file, newAppPath + Utils.ExtractFileName(file));

            Debug.AddLog("HideRemoteTracker: Copy files. *.xml");
            Utils.ForceCopyFile(appPath + "languages.xml", newAppPath + "languages.xml");
            Utils.ForceCopyFile(appPath + languages.fileName(configuration.defaultLanguage), 
                                newAppPath + languages.fileName(configuration.defaultLanguage));

            Debug.AddLog("HideRemoteTracker: Copy files. *.wav");
            files = Utils.ListFilesInFolder("*.wav", appPath);
            foreach(string file in files)
                Utils.ForceCopyFile(file, newAppPath + Utils.ExtractFileName(file));

            // Copy exe files
            Debug.AddLog("HideRemoteTracker: Copy files. rt.exe");
            Utils.ForceCopyFile(appPath + "rt.exe", newAppPath + rtExe);

            Debug.AddLog("HideRemoteTracker: Copy files. smslauncher.exe");
            Utils.ForceCopyFile(appPath + "SMSLauncher.exe", newAppPath + smsLauncherExe);
            
            Debug.AddLog("HideRemoteTracker: Copy files. lock.exe");
            Utils.ForceCopyFile(appPath + "lock.exe", newAppPath + lockExe);

            Debug.AddLog("HideRemoteTracker: Copy files. delus.exe");
            Utils.ForceCopyFile(appPath + "delus.exe", newAppPath + "delus.exe");

            Debug.AddLog("HideRemoteTracker: Copy files. RTRemote.exe");
            Utils.ForceCopyFile(appPath + "RTRemote.exe", newAppPath + "RTRemote.exe");

            Debug.AddLog("HideRemoteTracker: Copy files. TopSecret.exe");
            Utils.ForceCopyFile(appPath + "TopSecret.exe", newAppPath + "TopSecret.exe");

            ///////////////////////////////////////
            // Removing old RemoteTracker format //
            ///////////////////////////////////////

            // Remove links from Start Menu
            Debug.AddLog("HideRemoteTracker: removing link in start menu");
            if (File.Exists(ShellFolders.StartMenuFolder + "\\Config.lnk"))
                File.Delete(ShellFolders.StartMenuFolder + "\\Config.lnk");
            if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\Config.lnk"))
                File.Delete(ShellFolders.StartMenuProgramsFolder + "\\Config.lnk");
            if (File.Exists(ShellFolders.StartMenuFolder + "\\RTRemote.lnk"))
                File.Delete(ShellFolders.StartMenuFolder + "\\RTRemote.lnk");
            if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\RTRemote.lnk"))
                File.Delete(ShellFolders.StartMenuProgramsFolder + "\\RTRemote.lnk");
            if (File.Exists(ShellFolders.StartMenuFolder + "\\TopSecret.lnk"))
                File.Delete(ShellFolders.StartMenuFolder + "\\TopSecret.lnk");
            if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\TopSecret.lnk"))
                File.Delete(ShellFolders.StartMenuProgramsFolder + "\\TopSecret.lnk");

            // Remove RemoteTracker from AppToDate list
            Debug.AddLog("HideRemoteTracker: Removing link with apptodate");
            RemoveAppToDateLink();

            // Remove SMSLauncher from HKLM\Init
            Debug.AddLog("HideRemoteTracker: removing smslauncher from init");
            Utils.RemoveAppFromInit(appPath + "SMSLauncher.exe", true);

            ////////////////////////////////////////////////////////////
            // Reconfiguring RemoteTracker with the new hidden format //
            ////////////////////////////////////////////////////////////

            // Reconfigure default sound
            Debug.AddLog("HideRemoteTracker: reconfiguring alarm sound if needed");
            if (configuration.AlarmSound.ToLower().Trim().Equals(appPath.ToLower() + "alarmfindme.wav"))
                configuration.AlarmSound = newAppPath + "alarmfindme.wav";

            // Recreate link to RTRemote from start menu
            Debug.AddLog("HideRemoteTracker: recreating link to RTRemote");
            Utils.CreateLink(newAppPath, "RTRemote.exe",
                             ShellFolders.StartMenuProgramsFolder, "RTRemote.lnk", "");

            // Recreate link to TopSecret from start menu
            Debug.AddLog("HideRemoteTracker: recreating link to TopSecret");
            Utils.CreateLink(newAppPath, "TopSecret.exe",
                             ShellFolders.StartMenuProgramsFolder, "TopSecret.lnk", "");

            // Save the path to the new names.
            Debug.AddLog("HideRemoteTracker: Registering file names.");
            RegisterBinaries(newAppPath + rtExe,
                             newAppPath + smsLauncherExe,
                             newAppPath + lockExe);

            // Reconfigure links
            Debug.AddLog("HideRemoteTracker: recreating interceptor");
            CreateInterceptor(newAppPath + rtExe);

            Debug.AddLog("HideRemoteTracker: recreating smslauncher link to init");
            CreateStartupLink();
            
            // Configure startup folder to run DelUS to complete the hidden procedures
            Debug.AddLog("HideRemoteTracker: create startup link to delus.exe");
            Utils.CreateLink(newAppPath, "delus.exe", 
                             ShellFolders.StartUpFolder, "delus.lnk",
                             System.Convert.ToString((char)34) + appPath + 
                             System.Convert.ToString((char)34));

            // Mark RT as hidden
            configuration.Hidden = true;
            configuration.SaveConfiguration();
        }

        public void DeleteInvalidKeys()
        {
            Debug.AddLog("DeleteInvalidKeys", true);
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\Inbox\\svc\\sms\\rules");

            if (r != null)
            {
                try
                {
                    string[] values = r.GetValueNames();

// HKEY_LOCAL_MACHINE\Software\Microsoft\Inbox\Svc\SM S\Rules:{1000BC1C-F4A3-4210-B197-4AEBF2CEE6F5}
// HKEY_LOCAL_MACHINE\Software\Microsoft\Inbox\Svc\SM S\Rules:{3AB4C10E-673C-494c-98A2-CC2E91A48115}
// HKEY_LOCAL_MACHINE\Software\Microsoft\Inbox\Svc\SM S\Rules:{77990A0E-60B8-4103-B9AF-17157E4274FD}
// HKEY_LOCAL_MACHINE\Software\Microsoft\Inbox\Svc\SM S\Rules:{A0C65276-77C8-48ef-B2AF-049DCB4171CD}
                    
                    foreach (string v in values)
                    {
                        if (v.Equals("{1000BC1C-F4A3-4210-B197-4AEBF2CEE6F5}") ||
                            v.Equals("{3AB4C10E-673C-494c-98A2-CC2E91A48115}") ||
                            v.Equals("{77990A0E-60B8-4103-B9AF-17157E4274FD}") ||
                            v.Equals("{A0C65276-77C8-48ef-B2AF-049DCB4171CD}"))
                        {
                            Debug.AddLog("DeleteInvalidKeys: Deleting key: " + v, true);
                            r.DeleteValue(v);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.AddLog("DeleteInvalidKeys: Error removing invalid rule: " + Utils.GetOnlyErrorMessage(e.Message), true);
                }

                r.Close();
            }
            else
            {
                Debug.AddLog("DeleteInvalidKeys: Nothing to do.", true);
            }
        }

        public string GetTextToSendFromNonGPS()
        {
            if (gps.IsStarted)
            {
                Debug.AddLog("GetCoordinatesFromNonGPS: Closing GPS.", true);

                gps.Stop();

                Debug.AddLog("GetCoordinatesFromNonGPS: is GPS closed? " + Utils.iif(gps.IsStarted, "No", "Yes"), true);
            }

            if (configuration.CellIDConverterProvider == CellIDProvider.OpenCellID)
                return GetMessageFromOpenCellId();
            else if (configuration.CellIDConverterProvider == CellIDProvider.Google)
                return GetMessageFromGoogle();
            else if (configuration.CellIDConverterProvider == CellIDProvider.CellDB)
                return GetMessageFromCellDB();
            else
                return string.Empty;
        }

        private string GetMessageFromOpenCellId()
        {
            CellIDInformations cid = OpenCellID.RefreshData();
            OPENCELLIDRESULT ocr = OpenCellID.ConvertDataToCoordinates(cid.mobileNetworkCode,
                                                                       cid.mobileCountryCode,
                                                                       cid.cellID,
                                                                       cid.localAreaCode);

            string result =
                Messages.GetShortCellIdMessage(cid.mobileNetworkCode, cid.mobileCountryCode,
                                               cid.cellID, cid.localAreaCode) + "\n" +
                Messages.msg_CoordFromOpenCellId + ": " +
                Messages.msg_Latitude + ": " + ocr.ShortLatitude + ", " +
                Messages.msg_Longitude + ": " + ocr.ShortLongitude + "\n" +
                Google.GoogleMapsLink(ocr.ShortLatitude, ocr.ShortLongitude);

            if (Commands.AnswerType != AnswerType.SMS)
            {
                Commands.GoogleMapsFileName = Google.GetMap(ocr.Latitude, ocr.Longitude);

                if (Commands.GoogleMapsFileName == null)
                    Commands.GoogleMapsFileName = "";
            }

            lastCellIDLatitude = Utils.StringToDouble(ocr.Latitude);
            lastCellIDLongitude = Utils.StringToDouble(ocr.Longitude);

            Debug.AddLog("GetCoordinatesFromOpenCellId: " + result, true);
            return result;
        }

        private string GetMessageFromGoogle()
        {
            double latitude = 0;
            double longitude = 0;
            string result = string.Empty;

            CellIDInformations cid = OpenCellID.RefreshData();
            Debug.AddLog("GetCoordinatesFromGoogle: CELLID: " + cid.cellID +
                         " MCC: " + cid.mobileCountryCode +
                         " MNC: " + cid.mobileNetworkCode +
                         " LAC: " + cid.localAreaCode, true);

            if (GoogleCellID.GetLatLng(cid.mobileCountryCode, cid.mobileNetworkCode, cid.localAreaCode, cid.cellID, ref latitude, ref longitude))
            {
                Debug.AddLog("GetCoordinatesFromGoogle: Latitude: " + System.Convert.ToString(latitude) + ", " + System.Convert.ToString(longitude), true);

                result =
                    Messages.GetShortCellIdMessage(cid.mobileNetworkCode, cid.mobileCountryCode, cid.cellID, cid.localAreaCode) + "\n" +
                    Messages.msg_CoordFromGoogleMaps + ": " +
                    Messages.msg_Latitude + ": " + System.Convert.ToString(latitude) + ", " +
                    Messages.msg_Longitude + ": " + System.Convert.ToString(longitude) + "\n" +
                    Google.GoogleMapsLink(latitude, longitude);

                if (Commands.AnswerType != AnswerType.SMS)
                {
                    Commands.GoogleMapsFileName = Google.GetMap(latitude, longitude);

                    if (Commands.GoogleMapsFileName == null)
                        Commands.GoogleMapsFileName = "";
                }

                Debug.AddLog("GetCoordinatesFromGoogle: " + result, true);
            }
            else
            {
                Debug.AddLog("GetCoordinatesFromGoogle: failed.", true);
            }

            lastCellIDLatitude = latitude;
            lastCellIDLongitude = longitude;

            return result;
        }

        private string GetMessageFromCellDB()
        {
            double latitude = 0;
            double longitude = 0;
            string result = string.Empty;

            CellIDInformations cid = OpenCellID.RefreshData();

            Debug.AddLog("GetCoordinatesFromCellDB: CELLID: " + cid.cellID +
                         " MCC: " + cid.mobileCountryCode +
                         " MNC: " + cid.mobileNetworkCode +
                         " LAC: " + cid.localAreaCode +
                         " Signal: " + cid.signalStrength, true);

            CELLDBRESULT[] cdb = CellDB.GetCoordinates(cid.mobileNetworkCode, cid.mobileCountryCode, cid.cellID, cid.localAreaCode, cid.signalStrength);
            latitude = cdb[0].Latitude;
            longitude = cdb[0].Longitude;

            Debug.AddLog("GetCoordinatesFromCellDB: Latitude: " + System.Convert.ToString(latitude) + ", " + System.Convert.ToString(longitude), true);

            result =
                Messages.GetShortCellIdMessage(cid.mobileNetworkCode, cid.mobileCountryCode, cid.cellID, cid.localAreaCode) + "\n" +
                Messages.msg_CoordFromCellDB + ": " +
                Messages.msg_Latitude + ": " + System.Convert.ToString(latitude) + ", " +
                Messages.msg_Longitude + ": " + System.Convert.ToString(longitude) + "\n" +
                Google.GoogleMapsLink(latitude, longitude);

            if (Commands.AnswerType != AnswerType.SMS)
            {
                Commands.GoogleMapsFileName = Google.GetMap(latitude, longitude);

                if (Commands.GoogleMapsFileName == null)
                    Commands.GoogleMapsFileName = "";
            }

            Debug.AddLog("GetCoordinatesFromCellDB: " + result, true);

            lastCellIDLatitude = latitude;
            lastCellIDLongitude = longitude;
            return result;
        }

        public void GetCoordinatesFromNonGPS(ref double latitude, ref double longitude)
        {
            if (gps.IsStarted)
            {
                Debug.AddLog("GetCoordinatesFromNonGPS: Closing GPS.", true);

                gps.Stop();

                Debug.AddLog("GetCoordinatesFromNonGPS: is GPS closed? " + Utils.iif(gps.IsStarted, "No", "Yes"), true);
            }

            latitude = 0;
            longitude = 0;

            if (configuration.CellIDConverterProvider == CellIDProvider.OpenCellID)
            {
                OPENCELLIDRESULT o = GetCoordinatesFromOpenCellID();

                if (!o.Latitude.Trim().Equals(""))
                    latitude = Utils.StringToDouble(o.Latitude);

                if (!o.Longitude.Trim().Equals(""))
                    longitude = Utils.StringToDouble(o.Longitude);
            }
            else if (configuration.CellIDConverterProvider == CellIDProvider.Google)
                GetCoordinatesFromGoogle(ref latitude, ref longitude);
            else if (configuration.CellIDConverterProvider == CellIDProvider.CellDB)
                GetCoordinatesFromCellDB(ref latitude, ref longitude);

            lastCellIDLatitude = latitude;
            lastCellIDLongitude = longitude;
        }

        private OPENCELLIDRESULT GetCoordinatesFromOpenCellID()
        {
            CellIDInformations cid = OpenCellID.RefreshData();
            return OpenCellID.ConvertDataToCoordinates(cid.mobileNetworkCode, cid.mobileCountryCode,
                                                       cid.cellID, cid.localAreaCode);
        }

        private bool GetCoordinatesFromGoogle(ref double latitude, ref double longitude)
        {
            CellIDInformations cid = OpenCellID.RefreshData();
            return GoogleCellID.GetLatLng(cid.mobileCountryCode, cid.mobileNetworkCode, cid.localAreaCode, cid.cellID, ref latitude, ref longitude);
        }

        private void GetCoordinatesFromCellDB(ref double latitude, ref double longitude)
        {
            CellIDInformations cid = OpenCellID.RefreshData();
            CELLDBRESULT[] cdb = CellDB.GetCoordinates(cid.mobileNetworkCode, cid.mobileCountryCode, cid.cellID, cid.localAreaCode, cid.signalStrength);
            latitude = cdb[0].Latitude;
            longitude = cdb[0].Longitude;
        }

        public bool CreateWebAccount(string email, string password)
        {
            try
            {
                return Meedios.RegisterNewAccount(email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Messages.msg_Error + ": " + Utils.GetOnlyErrorMessage(ex.Message.ToString()), Messages.msg_Error);
                Debug.AddLog("CreateWebAccount: Error trying to create new account: " + ex.Message.ToString(), true);
                return false;
            }
        }

        public bool LoginWebAccount(string email, string password)
        {
            try
            {
                return Meedios.Login(email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Messages.msg_Error + ": " + Utils.GetOnlyErrorMessage(ex.Message.ToString()), Messages.msg_Error);
                Debug.AddLog("LoginWebAccount: Error trying to login: " + ex.Message.ToString(), true);
                return false;
            }
        }

        public MeediosResult SendWebConfig(string email, string password)
        {
            try
            {
                PhoneInfo pi = new PhoneInfo();
                string imei = pi.GetIMEI();
                string values = Utils.Encode(
                                (!configuration.IMSI1.Trim().Equals("") ? "imsi=" + configuration.IMSI1.Trim() + ";" : "") +
                                (!configuration.IMSI2.Trim().Equals("") ? "imsi=" + configuration.IMSI2.Trim() + ";" : "") +
                                (!configuration.IMSI3.Trim().Equals("") ? "imsi=" + configuration.IMSI3.Trim() + ";" : "") +
                                (!configuration.IMSI4.Trim().Equals("") ? "imsi=" + configuration.IMSI4.Trim() + ";" : "") +
                                (!configuration.defaultNumber1.Trim().Equals("") ? "phone=" + configuration.defaultNumber1.Trim() + ";" : "") +
                                (!configuration.defaultNumber2.Trim().Equals("") ? "phone=" + configuration.defaultNumber2.Trim() + ";" : "") +
                                (!configuration.defaultNumber3.Trim().Equals("") ? "phone=" + configuration.defaultNumber3.Trim() + ";" : "") +
                                (!configuration.defaultNumber4.Trim().Equals("") ? "phone=" + configuration.defaultNumber4.Trim() + ";" : "") +
                                (!configuration.emergencyEMail1.Trim().Equals("") ? "email=" + configuration.emergencyEMail1.Trim() + ";" : "") +
                                (!configuration.emergencyEMail2.Trim().Equals("") ? "email=" + configuration.emergencyEMail2.Trim() + ";" : "") +
                                (!configuration.emergencyEMail3.Trim().Equals("") ? "email=" + configuration.emergencyEMail3.Trim() + ";" : "") +
                                (!configuration.emergencyEMail4.Trim().Equals("") ? "email=" + configuration.emergencyEMail4.Trim() + ";" : ""));
                Debug.AddLog("SendWebConfig: imei=" + imei + "; values=" + values, true);

                return Meedios.SendConfig(configuration.WebEMailAccount, configuration.WebPassword, imei, values);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Messages.msg_Error + ": " + Utils.GetOnlyErrorMessage(ex.Message.ToString()), Messages.msg_Error);
                Debug.AddLog("SendWebConfig: error : " + ex.Message.ToString(), true);
                return MeediosResult.OtherException;
            }
        }

        public string MeediosError(MeediosResult mr)
        {
            if (mr == MeediosResult.Ok)
                return Messages.msg_WebSendConfigSuccess;
            else if (mr == MeediosResult.UndefinedError)
                return Messages.meedios_UndefinedError;
            else if (mr == MeediosResult.OtherException)
                return Messages.meedios_OtherException;
            else if (mr == MeediosResult.WrongUser)
                return Messages.meedios_WrongUser;
            else if (mr == MeediosResult.WrongPass)
                return Messages.meedios_WrongPass;
            else if (mr == MeediosResult.InvalidUser)
                return Messages.meedios_InvalidUser;
            else if (mr == MeediosResult.FailedGeneratedAccound)
                return Messages.meedios_FailedGeneratedAccound;
            else if (mr == MeediosResult.FailedStoreIMEI)
                return Messages.meedios_FailedStoreIMEI;
            else
                return Messages.meedios_InvalidAPIKey;
        }
    }
}
