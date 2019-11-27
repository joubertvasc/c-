using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.WindowsMobile.Telephony;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;
using JVUtils;
using JVGPS;

namespace CommonDLL
{
    public class Commands
    {
        #region Interval variables
        static bool bEMailSent = false;
        static AnswerType _answerType;
        static string telSMS;
        static string appPath;
        static string googleMapsFileName = "";
        static System.Windows.Forms.Timer tmELT;
        static bool bTimeOut;
        static GPS gps;
        static bool _fake;
        static SatelliteRecord sr;
        static string _lastCommand = "";
        static string lastLatitude;
        static string lastLongitude;
        static string initialCoordinates;
        static string finalCoordinates;
        #endregion

        #region Public variables
        public static RTCommon rtCommon;
        public static Configuration configuration;
        #endregion

        #region Public Properties
        public static AnswerType AnswerType
        {
            get { return _answerType; }
            set { _answerType = value; }
        }
        public static string TelSMS
        {
            get { return telSMS; }
            set { telSMS = value; }
        }
        public static string AppPath
        {
            get { return appPath; }
            set { appPath = value; }
        }
        public static string GoogleMapsFileName
        {
            get { return googleMapsFileName; }
            set { googleMapsFileName = value; }
        }
        public static System.Windows.Forms.Timer TimerELT
        {
            get { return tmELT; }
            set { tmELT = value; }
        }
        public static bool TimeOut
        {
            get { return bTimeOut; }
            set { bTimeOut = value; }
        }
        public static GPS GPS
        {
            get { return gps; }
            set { gps = value; }
        }
        public static bool Fake 
        {
            get { return _fake; }
            set { _fake = value; }
        }
        public static SatelliteRecord SatelliteRecord
        {
            get { return sr; }
            set { sr = value; }
        }
        public static bool CommandsWithoutPassword(string command)
        {
            return (command.ToLower().Equals("findme") ||
                    command.ToLower().Equals("alarm") ||
                    command.ToLower().Equals("msg") ||
                    command.ToLower().Equals("lostpass") ||
                    command.ToLower().Equals("secret") ||
                    command.ToLower().Equals("loopback") ||
                    command.ToLower().Equals("custommsgcoord") ||
                    command.ToLower().Equals("sendsms"));
        }
        public static bool CommandsWithoutPhoneNumber(string command)
        {
            return (AnswerType == AnswerType.FTP ||
                    command.ToLower().Equals("alarm") ||
                    command.ToLower().Equals("msg") ||
                    command.ToLower().Equals("rst") ||
                    command.ToLower().Equals("dsc") ||
                    command.ToLower().Equals("dsk") ||
                    command.ToLower().Equals("vnc") ||
                    command.ToLower().Equals("runapp") ||
                    command.ToLower().Equals("msrun"));
        }
        public static string LastCommand
        {
            get { return _lastCommand; }
            set { _lastCommand = value; }
        }
        #endregion

        //////////////
        // Commands //
        //////////////

        // Help command
        public static void SendHelp()
        {
            Debug.AddLog("Command started: help or ehelp", true);
            try
            {
                Answer.SendAnswer(TelSMS, Messages.Help(rtCommon.atm), AnswerType, true, Fake);
            } catch (Exception ex)
            {
                Debug.AddLog("SendHelp: Error message: " + ex.ToString(), true);
                Application.Exit();
            }
        }

        // CB command
        public static void CallBack()
        {
            Debug.AddLog("Command started: cb", true);

            try
            {
                Kernel.SoundType st = 0;
                try
                {
                    PhoneVolume.GetCurrentSoundType(ref st);
                }
                catch (Exception e)
                {
                    Debug.AddLog("CallBack: Error GetCurrentSoundType: " + e.ToString());
                }

                // Turn the sound on
                try
                {
                    PhoneVolume.SetRingerOn();
                }
                catch (Exception e)
                {
                    Debug.AddLog("CallBack: Error SetRingerOn: " + e.ToString());
                }

                // Set the same volume for both the left and the right channels as maximum
                uint newVolumeAllChannels = (((uint)ushort.MaxValue & 0x0000ffff) |
                                             ((uint)ushort.MaxValue << 16));

                try
                {
                    PhoneVolume.WaveOutSetVolume(newVolumeAllChannels);
                }
                catch (Exception e)
                {
                    Debug.AddLog("CallBack: Error WaveOutSetVolume: " + e.ToString());
                }

                Utils.PhoneMakeCall (telSMS);

                // speaker phone
                SpeakerPhone.Toggle();
                Utils.Sleep(15);               
                
                Application.Exit();
            }
            catch (Exception ex)
            {
                Debug.AddLog("CallBack: Error message: " + ex.ToString(), true);
                Application.Exit();
            }
        }

        // PB command
        public static void SendPhoneBook()
        {
            Debug.AddLog("Command started: pb or epb", true);
            try
            {
                Answer.SendAnswer(TelSMS, SIMContacts.GetAllContacts(), AnswerType, true, Fake);
            } catch (Exception ex)
            {
                Debug.AddLog("SendPhoneBook: Error message: " + ex.ToString(), true);
                Application.Exit();
            }
        }

        static void Emergency(string recipient, string message, AnswerType answerType)
        {
            Debug.AddLog("Emergency: Recipient=" + recipient + " Message=" + message + 
                         " AnswerType=" + answerType.ToString(), true);
            Answer.SendAnswer(recipient, message,
                              (GoogleMapsFileName.Equals("") ? null : new string[1] { GoogleMapsFileName }), 
                              answerType, false, false);
        }

        // GP command
        public static void SendCoordinates(string sTextToSend, bool bIsEmergency, bool doNotAddBatteryMetter)
        {
            Debug.AddLog("Command processed: gp or egp", true);

            try 
            {
                Answer.DoNotAddBatteryMetter = doNotAddBatteryMetter;

                if (!bIsEmergency)
                {
                    Answer.SendAnswer(telSMS, sTextToSend,
                                       (GoogleMapsFileName.Equals("") ? null :
                                        new string[1] { GoogleMapsFileName }), AnswerType,
                                  true, Fake);
                }
                else
                {
                    Debug.AddLog("Command processed: The command is an emergency!", true);
                    OwnerRecord or = JVUtils.OwnerInfo.GetOwnerRecord();
                    string owner = "";

                    if (or != null)
                    {
                        owner = or.UserName;
                    }

                    Debug.AddLog("Command processed: Owner=" + owner, true);

                    if (!configuration.defaultNumber1.Equals(""))
                        Emergency(configuration.defaultNumber1,
                                  configuration.EmergencyMessage + "\n" + owner + "\n" +
                                  sTextToSend, AnswerType.SMS);
                    if (!configuration.defaultNumber2.Equals(""))
                        Emergency(configuration.defaultNumber2,
                                  configuration.EmergencyMessage + "\n" + owner + "\n" +
                                  sTextToSend, AnswerType.SMS);
                    if (!configuration.defaultNumber3.Equals(""))
                        Emergency(configuration.defaultNumber3,
                                  configuration.EmergencyMessage + "\n" + owner + "\n" +
                                  sTextToSend, AnswerType.SMS);
                    if (!configuration.defaultNumber4.Equals(""))
                        Emergency(configuration.defaultNumber4,
                                  configuration.EmergencyMessage + "\n" + owner + "\n" +
                                  sTextToSend, AnswerType.SMS);

                    string to = (!configuration.emergencyEMail1.Equals("") ?
                                 configuration.emergencyEMail1 : "") + ";" +
                                (!configuration.emergencyEMail2.Equals("") ?
                                 configuration.emergencyEMail2 : "") + ";" +
                                (!configuration.emergencyEMail3.Equals("") ?
                                 configuration.emergencyEMail3 : "") + ";" +
                                (!configuration.emergencyEMail4.Equals("") ?
                                 configuration.emergencyEMail4 : "");

                    Emergency(to, configuration.EmergencyMessage + "\n" + owner + "\n" +
                                  sTextToSend, AnswerType.EMAIL);

                    Application.Exit();
                }
            } catch (Exception ex) {
               Debug.AddLog("SendCoordinates: Error message: " + ex.ToString(), true);
               Application.Exit();
            }
        }

        // RST command
        public static void SoftReset()
        {
            Debug.AddLog("Command started: rst", true);
            try
            {
                Kernel.ResetPocketPC();
            }
            catch (Exception ex)
            {
                Debug.AddLog("SoftReset: Error message: " + ex.ToString(), true);
                Application.Exit();
            }
        }

        // GO command
        public static void GetOwner()
        {
            Debug.AddLog("Command started: go or ego", true);

            OwnerRecord or = JVUtils.OwnerInfo.GetOwnerRecord();

            if (or != null)
            {
                string msg = Messages.msg_Name + ": " + or.UserName /* r.GetValue("Name", "") */ + "\n" +
                             Messages.msg_Company + ": " + or.Company + "\n" +
                             Messages.msg_EMail + ": " + or.EMail /* r.GetValue("E-Mail", "") */ + "\n" +
                             Messages.msg_Telephone + ": " + or.Phone /* r.GetValue("Telephone", "") */ + "\n" +
                             Messages.msg_Address + ": " + or.Address + "\n" +
                             Messages.msg_Notes + ": " + or.Notes + "\n";

                Answer.SendAnswer(TelSMS, msg, AnswerType, true, Fake);
            }
            else
            {
                Answer.SendAnswer(TelSMS, Messages.msg_Not_Possible_Owner_Info + ".", AnswerType, true, Fake);
            }
        }

        // GI command
        public static void GetInfo()
        {
            Debug.AddLog("Command started: gi or egi", true);

            PhoneInfo pi = new PhoneInfo();

            string msg = "IMSI: " + pi.GetIMSI() + "\n" +
                         "IMEI: " + pi.GetIMEI() + "\n" + 
                         "ICCID: " + JVUtils.ICCID.GETICCID() + "\n";

            Debug.AddLog("GetInfo: " + msg, true);

            Answer.SendAnswer(TelSMS, msg, AnswerType, true, Fake);
        }

        // DSC command
        public static void DelSimContacts()
        {
            Debug.AddLog("Command started: dsc", true);
            try
            {
                if (SIMContacts.RemoveAllContacts())
                {
                    Answer.SendSMSMessage(TelSMS, Messages.msg_Sim_Contacts_Deleted + ".", true, Fake);
                }
            }
            catch (Exception ex)
            {
                Debug.AddLog("DelSimContacts: Error message: " + ex.ToString(), true);
            }
            Application.Exit();
        }

        // DKZ command
        public static void DelKMZFiles()
        {
            Debug.AddLog("Command started: dkz", true);
            try
            {
                string[] filesToDelete = System.IO.Directory.GetFiles(AppPath, "*.KMZ");

                foreach (string fileToDelete in filesToDelete)
                {
                    File.Delete(fileToDelete);
                }

                Answer.SendSMSMessage(TelSMS, Messages.msg_kmz_Deleted + ".", true, Fake);
            }
            catch (Exception ex)
            {
                Debug.AddLog("DelKMZFiles: Error message: " + ex.ToString(), true);
                Application.Exit();
            }
        }

        // ELT command
        public static void LogTrack()
        {
            Debug.AddLog("LogTrack: begin", true);
            if (sr.CollectionSet == null)
            {
                Debug.AddLog("LogTrack: creating CollectionSet", true);

                lastLatitude = "";
                lastLongitude = "";
                initialCoordinates = "";
                finalCoordinates = "";
                bEMailSent = false;

                if (sr.Connected && !TimerELT.Enabled)
                {
                    TimerELT.Enabled = true;
                    Debug.AddLog("LogTrack: TimerELT enabled", true);
                }
            }

            Debug.AddLog("LogTrack: Latitude is null?" + Utils.iif(sr.Latitude == 0, "Yes", "No"), true);
            Debug.AddLog("LogTrack: Longitude is null?" + Utils.iif(sr.Longitude == 0, "Yes", "No"), true);
            if (sr.Latitude != 0 && sr.Longitude != 0)
            {
                // Do not log if the GPS is stopped
                Debug.AddLog("LogTrack: is GPS stopped?" +
                  Utils.iif(lastLatitude.Equals(sr.Latitude) ||
                             lastLongitude.Equals(sr.Longitude), "Yes", "No"), true);
                if (!lastLatitude.Equals(sr.Latitude) ||
                    !lastLongitude.Equals(sr.Longitude))
                {
                    sr.AddToCollection();

                    lastLatitude = System.Convert.ToString (sr.Latitude);
                    lastLongitude = System.Convert.ToString (sr.Longitude);

                    Debug.AddLog("LogTrack: lastLatitude: " + lastLatitude, true);
                    Debug.AddLog("LogTrack: lastLongitude: " + lastLongitude, true);
                    if (initialCoordinates == "")
                    {
                        initialCoordinates = Messages.MessageToSend(AnswerType, sr);
                        Debug.AddLog("LogTrack: initialCoordinates: " + initialCoordinates, true);
                    }
                }
            }

            Debug.AddLog("LogTrack: Timeout? " + Utils.iif(TimeOut, "Yes", "No"), true);
            if (TimeOut) 
            {
                if (finalCoordinates == "" && sr.Latitude != 0 && sr.Longitude != 0)
                {
                    finalCoordinates = Messages.MessageToSend(AnswerType, sr);
                    Debug.AddLog("LogTrack: finalCoordinates: " + finalCoordinates, true);
                }

                StopLog();
            }

            SatelliteRecord.Clear();
            Debug.AddLog("LogTrack: end", true);
        }

        // Stop Logging
        public static void StopLog()
        {
            Debug.AddLog("StopLog: is GPS opened? " + Utils.iif (GPS.IsStarted, "Yes", "No"), true);
            if (GPS.IsStarted)
            {
                Debug.AddLog("StopLog: Closing GPS", true);

                GPS.Stop();
                
                Debug.AddLog("StopLog: is GPS closed? " + Utils.iif(GPS.IsStarted, "No", "Yes"), true);
            }

            Debug.AddLog("StopLog: Has something to send? " + Utils.iif(SatelliteRecord.CollectionSet != null && !bEMailSent, "Yes", "No"), true);
            if (SatelliteRecord.CollectionSet != null && !bEMailSent)
            {
                bEMailSent = true;
                string data = System.DateTime.Now.ToString();
                data = Utils.ChangeChar(data, '/', '-');
                data = Utils.ChangeChar(data, ':', '-');

                string fn = "RTLOG-" + data;
                string fnz = fn + ".KMZ";

                if (Google.CreateGoogleEarthKMZ("RemoteTracker", appPath + fnz, SatelliteRecord.CollectionRows))
                {
                    Debug.AddLog("StopLog: Send file: " + AppPath + fnz, true);

                    Answer.SendAnswer(TelSMS, "RemoteTracker Track Log\n\n" +
                              Utils.iif(initialCoordinates != "", Messages.msg_Initial_Coordinates + ":\n" + initialCoordinates + "\n", "") +
                              Utils.iif(finalCoordinates != "", Messages.msg_Final_Coordinates + ":\n" + finalCoordinates + "\n", ""),
                        new string[1] { AppPath + fnz }, AnswerType, true, Fake);
                }
                else
                {
                    Debug.AddLog("StopLog: Could not create KMZ file, exiting.", true);
                    Answer.SendAnswer(telSMS, "RemoteTracker: Could not create KMZ file", AnswerType, true, Fake); 
                }
            }
            else
            {
                Debug.AddLog("StopLog: Nothing to send for ELT command.", true);

                string textToSend = rtCommon.GetTextToSendFromNonGPS();

                Answer.SendAnswer(TelSMS, "RemoteTracker Track Log\n\n" +
                                          textToSend, AnswerType, true, Fake);
            }
        }

        // GANFL command
        public static void GetAudioNotesFileList()
        {
            Debug.AddLog("Command started: eganfl", true);
            string result;

            if (AudioNotes.AudioNotesInstalled)
            {
                Debug.AddLog("GetAudioNotesFileList: Audio notes installed", true);

                DateTime dt;
                result = Messages.msg_incoming_calls + "\n";

                foreach (string inBox in AudioNotes.InBoxAudioFiles)
                {
                    dt = File.GetCreationTime(inBox);

                    result += dt.ToString() + " - " + Utils.ExtractFileName(inBox) + "\n";
                }

                result += "\n" + Messages.msg_outgoing_calls + "\n";

                foreach (string outBox in AudioNotes.OutBoxAudioFiles)
                {
                    dt = File.GetCreationTime(outBox);

                    result += dt.ToString() + " - " + Utils.ExtractFileName(outBox) + "\n";
                }

                result += "\n";
            }
            else
            {
                result = "VITO Audio Notes: " + Messages.msg_not_installed;
                Debug.AddLog("GetAudioNotesFileList: Audio notes not installed", true);
            }

            Debug.AddLog("GetAudioNotesFileList: File list: " + result, true);
            Answer.SendAnswer(TelSMS, result, AnswerType, true, Fake);
        }

        // EGANF command
        public static void GetAudioNotesFiles(int limit)
        {
            Debug.AddLog("Command started: eganf", true);

            string fn = "";
            string msg = "";

            if (AudioNotes.AudioNotesInstalled)
            {
                Debug.AddLog("Command started: Audio notes installed", true);
                string data = System.DateTime.Now.ToString();
                data = Utils.ChangeChar(data, '/', '-');
                data = Utils.ChangeChar(data, ':', '-');

                fn = "RTANLOGS-" + data + ".ZIP";

                // Creates a new ZIP file
                ZipStorer zfw = new ZipStorer(AppPath + fn, "RemoteTracker");

                // Add incoming logs to zip file
                int i = 0;

                foreach (string inBox in AudioNotes.InBoxAudioFiles)
                {
                    zfw.AddFile(inBox, Utils.ExtractFileName(inBox), "");

                    i++;

                    if (i == limit)
                        break;
                }

                // Add outgoing logs to zip file
                i = 0;
                foreach (string outBox in AudioNotes.OutBoxAudioFiles)
                {
                    zfw.AddFile(outBox, Utils.ExtractFileName(outBox), "");

                    i++;

                    if (i == limit)
                        break;
                }

                // Updates and closes the KMZ file
                zfw.Close();
            }
            else
            {
                msg = "VITO Audio Notes: " + Messages.msg_not_installed;
                Debug.AddLog("Command started: Audio notes not installed", true);
            }

            if (fn == "")
            {
                Answer.SendAnswer(TelSMS, "RemoteTracker Vito Audio Notes\n\n" + msg, AnswerType, true, Fake);
            }
            else
            {
                Debug.AddLog("Command started: file to be sent: " + AppPath + fn, true);

                Answer.SendAnswer(TelSMS, "RemoteTracker Vito Audio Notes\n\n" + msg,
                    new string[1] { AppPath + fn }, AnswerType, true, Fake);

                File.Delete(AppPath + fn);
                Application.Exit();
            }
        }

        // ALARM command
        public static void Alarm()
        {
            uint currVol = 0;

            Debug.AddLog("Command started: alarm", true);

            // Verify if the sound is off or the phone is vibrating
            Kernel.SoundType st = 0;
            try {
                PhoneVolume.GetCurrentSoundType(ref st);
            }
            catch (Exception e) {
                Debug.AddLog("Alarm: Error GetCurrentSoundType: " + e.ToString());
            }

            // Turn the sound on
            try {
                PhoneVolume.SetRingerOn();
            }
            catch (Exception e) {
                Debug.AddLog("Alarm: Error SetRingerOn: " + e.ToString());
            }

            // Save the actual volume level to restore after play the sound
            try {
                currVol = PhoneVolume.WaveOutGetVolume();
            }
            catch (Exception e) {
                Debug.AddLog("Alarm: Error WaveOutGetVolume: " + e.ToString());
            }

            // Set the same volume for both the left and the right channels as maximum
            uint newVolumeAllChannels = (((uint)ushort.MaxValue & 0x0000ffff) | 
                                         ((uint)ushort.MaxValue << 16));

            try {
                PhoneVolume.WaveOutSetVolume(newVolumeAllChannels);
            }
            catch (Exception e) {
                Debug.AddLog("Alarm: Error WaveOutSetVolume: " + e.ToString());
            }

            try {
                try {
                    Kernel.PlayFile(configuration.AlarmSound, true);
                    Kernel.PlayFile(configuration.AlarmSound, true);
                    Kernel.PlayFile(configuration.AlarmSound, true);
                }
                catch (Exception ex) {
                    Debug.AddLog("Alarm: Error message: " + ex.ToString(), true);
                    Application.Exit();
                }
            }
            finally {
                // After play the alarm, set volume to saved value level
                try
                {
                    PhoneVolume.WaveOutSetVolume(currVol);

                    if (st == Kernel.SoundType.None)
                    {
                        PhoneVolume.SetRingerOff();
                    }
                    else if (st == Kernel.SoundType.Vibrate)
                    {
                        PhoneVolume.SetRingerVibrate();
                    }
                }
                catch (Exception e)
                {
                    Debug.AddLog("Alarm: Error finally: " + e.ToString());
                }
            }
        
            Application.Exit();
        }

        // MSG command
        public static void Msg(string text)
        {
            if (configuration.ScreenOff)
                Power.PowerOnOff(true);

            Kernel.PlayFile(configuration.AlarmSound, true);

            MessageBox.Show(text, "RT MSG");
            Application.Exit();
        }

        private static string[] GetInternalIP()
        {
            try
            {
                string[] ip = JVUtils.Utils.GetIPAddress();
                Debug.AddLog("GetInternalIP: number of addresses: " + ip.Length.ToString(), true);

                // No ip address or Only activesync designed IP address force GPRS/3G connection
                if (ip.Length == 0 ||
                    (ip.Length == 1 && (ip[0].StartsWith("169.") || ip[0].Equals("127.0.0.1"))))
                {
                    Debug.AddLog("GetInternalIP: trying to access http://www.google.com to force a connection.", true);
                    try
                    {
                        Web.Request("http://www.google.com"); // Try to connect to google
                    }
                    catch (Exception e)
                    {
                        Debug.AddLog("GetInternalIP: error trying to force a connection: " + Utils.GetOnlyErrorMessage(e.Message), true);
                    }

                    ip = JVUtils.Utils.GetIPAddress();
                    Debug.AddLog("GetInternalIP: number of addresses: " + ip.Length.ToString(), true);
                }

                string[] validIP = new string[0];
                for (int i = 0; i < ip.Length; i++)
                {
                    // Ignore loopback and the IP designed by ActiveSync if there are more Address
                    if (!ip[i].Equals("127.0.0.1"))
                        if (!ip[i].StartsWith("169.") || (ip[i].StartsWith("169.") && ip.Length == 1))
                        {
                            Array.Resize(ref validIP, validIP.Length + 1);
                            validIP[validIP.Length - 1] = ip[i];
                        }
                }

                return validIP;
            }
            catch (Exception ex)
            {
                Debug.AddLog("GetInternalIP: Error message: " + ex.ToString(), true);

                return new string[0];
            }
        }

        // Start VNC Server command
        public static void StartVNC()
        {
            Debug.AddLog("Command started: vnc or evnc", true);
            string[] IPs = GetInternalIP();
            string currentIP = "";

            if (IPs.Length == 0)
                currentIP = Messages.msg_No_IP_Detected;
            else
                for (int i = 0; i < IPs.Length; i++)
                    currentIP += IPs[i] + "\n";

            Debug.AddLog("GetCurrentIP: addresses: " + currentIP, true);

            Debug.AddLog("GetCurrentIP: Trying to start VPNServer", true);

            bool vncok = ProcessStart.VPN();
            Debug.AddLog("GetCurrentIP: VPNServer was started? " + (vncok ? "Yes" : "No"), true);

            Answer.SendAnswer(TelSMS, "VNC: " + currentIP +
                (vncok ? Messages.msg_VNCStarted : Messages.msg_VNCNotStarted), AnswerType, true, Fake);
        }

        // Public IP command
        public static void GetCurrentIP()
        {
            Debug.AddLog("Command started: gip or egip", true);
            string[] IPs = GetInternalIP();
            string currentIP = "";

            if (IPs.Length == 0)
                currentIP = Messages.msg_No_IP_Detected;
            else
                for (int i = 0; i < IPs.Length; i++)
                    currentIP += IPs[i] + "\n";

            Debug.AddLog("GetCurrentIP: addresses: " + currentIP, true);

            Answer.SendAnswer(TelSMS, Messages.msg_CurrentIPAdrress + ": " +
                               currentIP, AnswerType, true, Fake);
        }

        // LISTAPP command
        public static void ListApp()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\System\\Explorer\\Shell Folders");
            if (r != null)
            {
                // Didn't find a way to get it directly from registry
//                string path = (string)r.GetValue("Windows", "") + "\\Start Menu";
                string path = ShellFolders.StartMenuFolder;
                r.Close();

                string[] files = Utils.ListFilesInFolder("*.lnk", path);
                string text = "";

                foreach (string f in files)
                {
                    int i = f.LastIndexOf("\\");
                    text += f.Substring(i + 1, f.Length-i - 5) + "\n";
                }

                Answer.SendAnswer(TelSMS, Messages.msg_FilesInFolder + ":\n" + text,
                    AnswerType, true, Fake);
            }
        }

        // RUNAPP command
        public static void RunApp(string appToRun, string parameters)
        {
            Debug.AddLog("RunAPP: " + appToRun + ", " + parameters);

            string app = "";
            if (appToRun.Equals(""))
            {
                Debug.AddLog("RunAPP: There is no application to run.");
                Answer.SendAnswer(TelSMS, Messages.msg_AppNotDefined + ".\n", AnswerType, true, Fake);
            }
            else
            {
                // Is an absolute path?
                if (appToRun.StartsWith("\\"))
                    app = appToRun;
                else
                {
                    RegistryKey r = Registry.LocalMachine.OpenSubKey("\\System\\Explorer\\Shell Folders");
                    if (r != null)
                    {
                        // Didn't find a way to get it directly from registry
                        //string path = (string)r.GetValue("Windows", "") + "\\Start Menu"; 
                        string path = ShellFolders.StartMenuFolder;
                        r.Close();

                        app = Utils.FindFileInFolders(appToRun + ".lnk", "*.lnk", path, true);
                    }
                }

                if (File.Exists(app))
                {
                    Debug.AddLog("RunAPP: Before start application");
                    System.Diagnostics.Process.Start(app, parameters);
                    Debug.AddLog("RunAPP: After start application");

                    Answer.SendAnswer(TelSMS, Messages.msg_AppStarted + ".\n", AnswerType, true, Fake);
                }
                else
                {
                    Debug.AddLog("RunAPP: Application not found");
                    Answer.SendAnswer(TelSMS,
                        String.Format(Messages.msg_AppNotFound, appToRun) + ".\n", AnswerType, true, Fake);
                }
            }
        }

        // CELLID command
        public static void CellID()
        {
            if (GPS.IsStarted)
            {
                Debug.AddLog("CellID: Closing GPS", true);

                GPS.Stop();

                Debug.AddLog("CellID: is GPS closed? " + Utils.iif(GPS.IsStarted, "No", "Yes"), true);
            }

            Answer.SendAnswer(TelSMS, rtCommon.GetTextToSendFromNonGPS(), AnswerType, true, Fake);            
                        
//            CellIDInformations cid = OpenCellID.RefreshData();

//            Answer.SendAnswer(TelSMS,
//                "CellID: " + cid.cellID + ", LAC: " + cid.localAreaCode +
//                ", MNC: " + cid.mobileNetworkCode + ", MCC: " + cid.mobileCountryCode +
//                ", Signal: " + cid.signalStrength, AnswerType, true, Fake);            
        }

        // LOCK and UNLOCK commands
        public static void Lock(bool locking, string messageToLock)
        {
            if (locking)
            {
                if (!rtCommon.atm)
                {
                    if (messageToLock.Equals(""))
                        messageToLock = Messages.msg_DeviceLockedByOwner;

                    StreamWriter sw;

                    sw = File.CreateText(appPath + "Lock.txt");

                    sw.WriteLine(messageToLock);
                    sw.Flush();
                    sw.Close();

                    Utils.AddAppToInit(RTCommon.GetLockPath());
                }
                else
                    Utils.CreateLink(Utils.ExtractFilePath(RTCommon.GetLockPath()),
                                     Utils.ExtractFileName(RTCommon.GetLockPath()),
                                     ShellFolders.StartUpFolder,
                                     "wm6.lnk", "");

                System.Diagnostics.Process.Start(RTCommon.GetLockPath(), "");

                Answer.SendAnswer(TelSMS, Messages.msg_DeviceLocked, AnswerType, true, Fake);
            }
            else
            {
                Utils.RemoveAppFromInit(RTCommon.GetLockPath(), true);

                if (File.Exists(ShellFolders.StartUpFolder + "\\wm6.lnk"))
                    File.Delete(ShellFolders.StartUpFolder + "\\wm6.lnk");

                Answer.SendAnswer(TelSMS, Messages.msg_DeviceUnLocked, AnswerType, false, Fake);
                Kernel.ResetPocketPC();
            }
        }

        // CALLHIST command
        public static void CallHistory(int limit)
        {
            try
            {
                CallLogEntry[] cle = CallLog.Entries;
                string text = Messages.msg_StartTime + ";" +
                              Messages.msg_EndTime + ";" +
                              Messages.msg_Duration + ";" +
                              Messages.msg_CallerName + ";" +
                              Messages.msg_CallerNumber + ";" +
                              Messages.msg_OutgoingCall + "\n";

                int i = 0;
                foreach (CallLogEntry c in cle)
                {
                    text += c.StartTime + ";" + c.EndTime + ";" + c.Duration + ";" +
                            c.CallerName + ";" + c.CallerNumber + ";" +
                            (c.IsOutgoing ? Messages.msg_Yes : Messages.msg_No) + "\n";
                    i++;

                    if (i == limit)
                        break;
                }

                Answer.SendAnswer(TelSMS, text, AnswerType, true, Fake);
            }
            catch 
            {
                Answer.SendAnswer(TelSMS, Messages.msg_CallLogIsEmpty, AnswerType, true, Fake);
            }
        }

        // DEPRECATED!
        /*public static void GetCurrentIPxx()
        {
            Debug.AddLog("GIP: com - Checking USB and WiFi connection", true);
            CommonDLL.GetExternalIP.checkConnections();
            Debug.AddLog("GIP: com - getCM_ProxyEntriesActual", true);
            CommonDLL.GetExternalIP.getCM_ProxyEntriesActual();
            Debug.AddLog("GIP: com - connectGPRS", true);
            CommonDLL.GetExternalIP.connectGPRS();
            Debug.AddLog("GIP: com - getExternalIP", true);
            string ip = CommonDLL.GetExternalIP.getExternalIP();
            System.Threading.Thread.Sleep(5000); // wait 5 sec.
            Answer.SendAnswer(TelSMS, Messages.msg_CurrentIPAdrress + ": " + ip, AnswerType, true, Fake);
        } /**/

        // MSLIST command
        public static void MortScriptList()
        {
            string[] files = Utils.ListFilesInFolder("*.mscr", "\\");
            Debug.AddLog("MSFiles found: " + files.Length.ToString());

            string text = "";
            foreach (string f in files)
            {
                int i = f.LastIndexOf("\\");
                text += f.Substring(i + 1, f.Length - i - 5) + "\n";
                Debug.AddLog("MSFile: " + f);
            }

            Answer.SendAnswer(TelSMS, Messages.msg_MortScriptFiles + ":\n" + text,
                AnswerType, true, Fake);
        }

        // MSRUN command
        public static void MortScriptRun(string[] files)
        {
            Debug.AddLog("MSRUN: files to run: " + files.Length.ToString());

            foreach (string f in files)
            {
                Debug.AddLog("MSRUN: searching " + f);

                string path = Utils.FindFileInFolders(
                    (f.ToLower().EndsWith(".mscr") ? f : f + ".mscr"), "*.mscr", "\\", true);
                string success = "";
                string failed = "";

                if (File.Exists(path))
                {
                    Debug.AddLog("MSRUN: Before start " + path);
                    System.Diagnostics.Process.Start(path, "");
                    Debug.AddLog("MSRUN: After start " + path);

                    success += f + "\n";
                }
                else
                {
                    Debug.AddLog("MSRUN: the MortScript file " + f + " does not exists.");
                    failed += f + "\n";
                }

                string result = Messages.msg_MortScriptRunSuccessfully + ":\n" + success + "\n" +
                                Messages.msg_MortScriptRunUnsuccessfully + ":\n" + failed;


                Answer.SendAnswer(TelSMS, result, AnswerType, true, Fake);
            }
        }

        // FTP command
        public static string FTP(string[] files, bool externalUse)
        {
            FTP ftplib = new FTP();
            string answer = "";
            int total = 0;

            Debug.AddLog("FTP: configuration: server: " + configuration.FtpServer + " user: " + configuration.FtpUser +
                         " password: ***** port: " + configuration.FtpPort.ToString() +
                         " remote dir: " + configuration.FtpRemoteDir +
                         " files to upload: " + files.Length.ToString(), true);

            if (files.Length != 0)
            {
                if (!configuration.FtpServer.Trim().Equals(""))
                {
                    // Try to connect to internet before send files
                    Web.Request("http://remotetracker.sourceforge.net");

                    // Configure FTP parameters
                    ftplib.server = configuration.FtpServer;
                    ftplib.port = configuration.FtpPort;
                    ftplib.user = (configuration.FtpUser.Trim().Equals("") ? "anonymous" : configuration.FtpUser);
                    ftplib.pass = (configuration.FtpUser.Trim().Equals("") ? "server@ftp.com" : configuration.FtpPassword);

                    try
                    {
                        if (!configuration.FtpRemoteDir.Equals(""))
                        {
                            Debug.AddLog("FTP: Before change remote dir.", true);
                            ftplib.ChangeDir(configuration.FtpRemoteDir);
                            Debug.AddLog("FTP: After change remote dir. Before send files", true);
                        }

                        foreach (string file in files)
                        {
                            if (File.Exists(file))
                            {
                                Debug.AddLog("FTP: Before upload file " + file + ".", true);
                                ftplib.OpenUpload(file, Utils.ExtractFileName(file));

                                while (ftplib.DoUpload() > 0) {}

                                Debug.AddLog("FTP: After upload file " + file + ".", true);
                                total++;
                            }
                            else
                            {
                                Debug.AddLog("FTP: The file " + file + " does not exists.", true);
                                answer += Messages.msg_Error + ": " + String.Format(Messages.msg_FileDoesNotExists, file) + "\n";
                            }
                        }

                        Debug.AddLog("FTP: Before disconnect.", true);
                        ftplib.Disconnect();
                        Debug.AddLog("FTP: After disconnect.", true);
                    }
                    catch (Exception e)
                    {
                        Debug.AddLog("FTP: Error: " + Utils.GetOnlyErrorMessage(e.Message), true);
                        answer += Messages.msg_Error + ": " + Utils.GetOnlyErrorMessage(e.Message) + "\n";
                    }
                }
                else
                {
                    Debug.AddLog("FTP: Server not defined.", true);
                    answer += Messages.msg_Error + ": " + Messages.msg_FTPServerNotDefined + "\n";
                }
            }
            else
            {
                Debug.AddLog("FTP: No files to upload.", true);
                answer += Messages.msg_Error + ": " + Messages.msg_NoFilesToUpload + "\n";
            }

            if (total > 0)
                answer += String.Format(Messages.msg_FilesUploaded, total);
            else
                answer += Messages.msg_NoFilesUploaded;

            if (externalUse)
                Answer.SendAnswer(telSMS, answer, AnswerType, true, Fake);

            return answer;
        }

        // FTPDOC command
        public static void FTPDoc()
        {
            Debug.AddLog("FTPDOC: Started", true);

            if (configuration.FtpServer.Equals(""))
            {
                Debug.AddLog("FTPDOC: There is not FTP account defined.", true);
                Answer.SendAnswer(TelSMS, Messages.msg_FTPServerNotDefined, AnswerType, true, Fake);
            }
            else
            {
                string[] files = Utils.ListFilesInFolder("*.*", ShellFolders.MyDocumentsFolder);
                Debug.AddLog("FTPDOC: files found: " + files.Length.ToString());

                if (files.Length == 0)
                {
                    Debug.AddLog("FTPDOC: No filed to zip", true);
                    Answer.SendAnswer(TelSMS, "There are no files in My Documents", AnswerType, true, Fake);
                }
                else
                {
                    string[] memoryCards = Kernel.GetMemoryCardsDirectories;
                    Debug.AddLog("FTPDOC: Memory cards: " + memoryCards.Length.ToString());

                    string folder = "";

                    if (memoryCards.Length == 0)
                        folder = ShellFolders.TempFolder;
                    else
                        folder = memoryCards[0];

                    bool ok = false;
                    Debug.AddLog("FTPDOC: Compacting to folder: " + folder);
                    try
                    {
                        JVUtils.Compress.Zip.SimpleZip.Compress(files, folder + "\\ftpdoc.zip");
                        Debug.AddLog("FTPDOC: zip file created");
                        ok = true;
                    }
                    catch (Exception e)
                    {
                        Debug.AddLog("FTPDOC: Error creating zip file: " + Utils.GetOnlyErrorMessage(e.Message), true);
                        Answer.SendAnswer(TelSMS, "Could not create the zip file: " + Utils.GetOnlyErrorMessage(e.Message),
                            AnswerType, true, Fake);
                    }

                    if (ok)
                    {
                        string[] zip = new string[1];
                        zip[0] = folder + "\\ftpdoc.zip";

                        try
                        {
                            string answer = FTP(zip, false);

                            if (File.Exists(folder + "\\ftpdoc.zip"))
                                File.Delete(folder + "\\ftpdoc.zip");

                            Answer.SendAnswer(TelSMS, answer, AnswerType, true, Fake);
                        }
                        catch (Exception e)
                        {
                            Debug.AddLog("FTPDOC: Error sending the zip file to FTP: " + Utils.GetOnlyErrorMessage(e.Message), true);
                            Answer.SendAnswer(TelSMS,
                                "Could not send the zip file to your FTP account: " + Utils.GetOnlyErrorMessage(e.Message), AnswerType, true, Fake);
                        }
                    }
                }
            }
        }

        // DELCARD command
        public static void WipeCard()
        {
            try
            {
                string[] removableDirectories = Kernel.GetMemoryCardsDirectories;
                foreach (string removableDirectory in removableDirectories)
                    Utils.Wipe(removableDirectory);

                Answer.SendAnswer(TelSMS, Messages.msg_Ok, AnswerType, true, Fake);
            }
            catch (Exception ex)
            {
                Debug.AddLog("WipeCard error: " + ex.Message.ToString(), true);
                Answer.SendAnswer(TelSMS, Messages.msg_Error + ": " + ex.Message.ToString(), AnswerType, true, Fake);
            }
        }

        // SMS, ESMS or FSMS commands
        public static void GetSMS()
        {
            string data = System.DateTime.Now.ToString();
            data = Utils.ChangeChar(Utils.ChangeChar(data, '/', '-'), ':', '-');

            string fileName = ShellFolders.TempFolder + "\\SMSHistory" + data + ".XML";
            string zipFile = Utils.ChangeFileExt(fileName, "zip");

            Outlook.ExportSMS(fileName);

            // Creates a new ZIP file
            ZipStorer zfw = new ZipStorer(zipFile, "SMS XML");

            // Add the XML to zip file
            zfw.AddFile(fileName, Utils.ExtractFileName(fileName), "");

            // Updates and closes the ZIP file
            zfw.Close();
            File.Delete(fileName);

            Answer.SendAnswer(TelSMS, "RemoteTracker SMS", new string[1] { zipFile }, AnswerType, true, Fake);

            File.Delete(zipFile);
            Application.Exit();
        }

        // EOUTLOOK or FOUTLOOK commands
        public static void GetOutlook()
        {
            string data = System.DateTime.Now.ToString();
            data = Utils.ChangeChar(Utils.ChangeChar(data, '/', '-'), ':', '-');

            string contactsFile = ShellFolders.TempFolder + "\\Contacts" + data + ".XML";
            string appointsFile = ShellFolders.TempFolder + "\\Appointments" + data + ".XML";
            string zipFile = ShellFolders.TempFolder + "\\Outlook" + data + ".ZIP";

            Outlook.ExportAppointments(appointsFile);
            Outlook.ExportContacts(contactsFile);

            // Creates a new ZIP file
            ZipStorer zfw = new ZipStorer(zipFile, "Outlook data");

            // Add the XML to zip file
            zfw.AddFile(contactsFile, Utils.ExtractFileName(contactsFile), "");
            zfw.AddFile(appointsFile, Utils.ExtractFileName(appointsFile), "");

            // Updates and closes the ZIP file
            zfw.Close();
            File.Delete(contactsFile);
            File.Delete(appointsFile);

            Answer.SendAnswer(TelSMS, "RemoteTracker Outlook", new string[1] { zipFile }, AnswerType, true, Fake);

            File.Delete(zipFile);
            Application.Exit();
        }

        // LOSTPASSWORD command
        public static void GetLostPassword()
        {
            if (!rtCommon.configuration.SecretQuestion.Trim().Equals(""))
                Answer.SendAnswer(TelSMS, rtCommon.configuration.SecretQuestion.Trim() + "\n", AnswerType, true, Fake);
            else
                Application.Exit();
        }

        // SECRET command
        public static void SendPassword(string secretAnswer)
        {
            if (!rtCommon.configuration.SecretAnswer.Trim().Equals("") &&
                (rtCommon.configuration.SecretAnswer.Trim().ToLower().Equals(secretAnswer.Trim().ToLower())))
                Answer.SendAnswer(TelSMS, rtCommon.configuration.defaultPassword.Trim() + "\n", AnswerType, true, Fake);
            else
                Application.Exit();
        }

        // RC and CR commands
        public static void RedirectCalls(bool redirect, string newNumber)
        {
            Debug.AddLog("RedirectCalls: is redirecting? " + (redirect ? "yes" : "no") + " newNumber: " + newNumber, true);

            int res = 0;
            res = CFModule.Initialize();
            Debug.AddLog("RedirectCalls initialize result = " + System.Convert.ToString(res), true);

            if (res == 0)
            {
                if (redirect)
                {
                    res = CFModule.ForwardCall(newNumber, LINEFORWARDMODE.LINEFORWARDMODE_UNCOND, 0);
                    Debug.AddLog("RedirectCalls ForwardCall result = " + System.Convert.ToString(res), true);
                }
                else
                {
                    res = CFModule.CancelForward();
                    Debug.AddLog("RedirectCalls CancelForward result = " + System.Convert.ToString(res), true);
                }

                CFModule.Shutdown();
                Debug.AddLog("RedirectCalls Shutdown result = " + System.Convert.ToString(res), true);
            }
            Answer.SendAnswer(TelSMS, Messages.msg_Success, AnswerType, true, Fake);
        }

        // LOOPBACK command
        public static void LoopBack(string imsi, string lineNumber)
        {
            LineNumberStore.SetLineNumberForIMSI(imsi, lineNumber);
            Application.Exit();
        }

        // ALOG command
        public static void ALOG(string ano, string mes, string imei)
        {
            Debug.AddLog("ALOG: Started", true);

            string path = "";
            RegistryKey r = Registry.LocalMachine.OpenSubKey(JVUtils.JVUtils.JVSoftwareKey + "\\ATM");

            if (r != null)
            {
                path = r.GetValue("path", "").ToString();

                r.Close();
            }

            if (!path.Equals(""))
            {
                Debug.AddLog("ALOG: caminho = " + path, true);
                if (configuration.FtpServer.Equals(""))
                {
                    Debug.AddLog("ALOG: There is not FTP account defined.", true);
                    Answer.SendAnswer(TelSMS, Messages.msg_FTPServerNotDefined, AnswerType, true, Fake);
                }
                else
                {
                    string[] files = Utils.ListFilesInFolder(ano + mes + "*.txt", path);
                    Debug.AddLog("ALOG: files found: " + files.Length.ToString());

                    if (files.Length == 0)
                    {
                        Debug.AddLog("ALOG: No filed to send", true);
                        Answer.SendAnswer(TelSMS, "Nenhum arquivo de log encontrado.", AnswerType, true, Fake);
                    }
                    else
                    {
                        bool ok = false;
                        string arquivoZip = ShellFolders.TempFolder + "\\" + imei + ".zip";

                        Debug.AddLog("ALOG: Compacting to folder: " + ShellFolders.TempFolder, true);
                        try
                        {
                            JVUtils.Compress.Zip.SimpleZip.Compress(files, arquivoZip);
                            Debug.AddLog("ALOG: zip file created");
                            ok = true;
                        }
                        catch (Exception e)
                        {
                            Debug.AddLog("ALOG: Error creating zip file: " + Utils.GetOnlyErrorMessage(e.Message), true);
                            Answer.SendAnswer(TelSMS, "Arquivo não pode ser criado: " + Utils.GetOnlyErrorMessage(e.Message),
                                AnswerType, true, Fake);
                        }

                        if (ok)
                        {
                            string[] zip = new string[1];
                            zip[0] = arquivoZip;

                            try
                            {
                                string answer = FTP(zip, false);

                                if (File.Exists(arquivoZip))
                                    File.Delete(arquivoZip);

                                Answer.SendAnswer(TelSMS, answer, AnswerType, true, Fake);
                            }
                            catch (Exception e)
                            {
                                Debug.AddLog("ALOG: Error sending the zip file to FTP: " + Utils.GetOnlyErrorMessage(e.Message), true);
                                Answer.SendAnswer(TelSMS,
                                    "Não foi possível enviar arquivo ZIP para a conta de FTP: " + Utils.GetOnlyErrorMessage(e.Message), AnswerType, true, Fake);
                            }
                        }
                    }
                }

            }
            else
            {
                Debug.AddLog("ALOG: ATM não encontrado.", true);
                Application.Exit();
            }
        }

        // AFTP/BFTP commands
        public static void AFTP(string imei, bool aftp, string path1, string path2, string atmVersao)
        {
            Debug.AddLog("AFTP/BFTP: iniciado. Path1=" + path1 + ", Path2=" + path2, true);

            if (!path1.Equals("") || !path2.Equals(""))
            {
                if (configuration.FtpServer.Equals(""))
                {
                    Debug.AddLog("AFTP/BFTP: There is not FTP account defined.", true);
                    Answer.SendAnswer(TelSMS, Messages.msg_FTPServerNotDefined, AnswerType, true, Fake);
                }
                else
                {
                    string path = "";

                    if (Directory.Exists(path1))
                        path = path1;
                    else if (Directory.Exists(path2))
                        path = path2;

                    if (!path.Equals(""))
                    {
                        string[] files = Utils.ListFilesInFolder("*.*", path);
                        Debug.AddLog("AFTP/BFTP: files found: " + files.Length.ToString());

                        if (files.Length == 0)
                        {
                            Debug.AddLog("AFTP/BFTP: No filed to send", true);
                            Answer.SendAnswer(TelSMS, "Nenhum arquivo de log encontrado.", AnswerType, true, Fake);
                        }
                        else
                        {
                            bool ok = false;
                            string arquivoZip = "";

                            if (aftp)
                            {
                                arquivoZip = ShellFolders.TempFolder + "\\" +
                                             atmVersao + "_" +
                                             Utils.InvertedDate(DateTime.Today) + imei + ".zip";
                            }
                            else
                            {
//                                arquivoZip = ShellFolders.TempFolder + "\\" + 
//                                             path.Substring(1, path.Substring(1).IndexOf("\\")) +
//                                             Utils.InvertedDate(DateTime.Today) + imei + ".zip";

                                OwnerRecord or = OwnerInfo.GetOwnerRecord();
                                
                                // LOG_F1741_DDMMAA.ZIP
                                arquivoZip = ShellFolders.TempFolder +
                                    "\\" + atmVersao + "_" +
//                                    "\\LOG_" +
                                    (or.Company.Trim().Equals("") ? rtCommon.GetIMEI() : or.Company) +
                                             Utils.InvertedDate(DateTime.Today) + ".zip";
                            }

                            Debug.AddLog("AFTP/BFTP: Compacting to file: " + arquivoZip, true);
                            try
                            {
                                JVUtils.Compress.Zip.SimpleZip.Compress(files, arquivoZip);
                                Debug.AddLog("AFTP/BFTP: zip file created");
                                ok = true;
                            }
                            catch (Exception e)
                            {
                                Debug.AddLog("AFTP/BFTP: Error creating zip file: " + Utils.GetOnlyErrorMessage(e.Message), true);
                                Answer.SendAnswer(TelSMS, "Arquivo zip não pode ser criado: " + Utils.GetOnlyErrorMessage(e.Message),
                                    AnswerType, true, Fake);
                            }

                            if (ok)
                            {
                                string[] zip = new string[1];
                                zip[0] = arquivoZip;

                                try
                                {
                                    string answer = FTP(zip, false);

                                    if (File.Exists(arquivoZip))
                                        File.Delete(arquivoZip);

                                    Answer.SendAnswer(TelSMS, answer, AnswerType, true, Fake);
                                }
                                catch (Exception e)
                                {
                                    Debug.AddLog("AFTP/BFTP: Error sending the zip file to FTP: " + Utils.GetOnlyErrorMessage(e.Message), true);
                                    Answer.SendAnswer(TelSMS,
                                        "Não foi possível enviar arquivo ZIP para a conta de FTP: " + Utils.GetOnlyErrorMessage(e.Message), AnswerType, true, Fake);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.AddLog("AFTP/BTFP: Directory does not exists", true);
                    }
                }

            }
            else
            {
                Debug.AddLog("AFTP: pasta não definida.", true);
                Application.Exit();
            }
        }

        // ADA/ADD commands
        public static void ADA(bool habilitar)
        {
            string path = "";

            RegistryKey r = Registry.LocalMachine.OpenSubKey(JVUtils.JVUtils.JVSoftwareKey + "\\ATM");

            if (r != null)
            {
                path = r.GetValue("Path", "").ToString();

                r.Close();
            }

            if (!path.Equals("") && File.Exists(path + "\atmdiario.exe"))
            {
                if (habilitar)
                {
                    DateTime proxExecucao = DateTime.Today.AddDays(1);
                    proxExecucao = proxExecucao + new TimeSpan(8, 0, 0);

                    Debug.AddLog("ADA command: " + proxExecucao.ToString(), true);

                    Utils.CreateScheduledTask(proxExecucao, path + "\atmdiario.exe");
                    Answer.SendAnswer(TelSMS, "Aviso diário habilitado.", AnswerType, true, Fake);
                }
                else
                {
                    Debug.AddLog("ADD command", true);
                    Utils.CancelScheduledTask(path + "\atmdiario.exe");
                    Answer.SendAnswer(TelSMS, "Aviso diário desabilitado.", AnswerType, true, Fake);
                }
            }
            else
                Answer.SendAnswer(TelSMS, "ATM não instalado ou atmdiario.exe não encontrado.", AnswerType, true, Fake);
        }

        // IEHIST/EIEHIST/FIEHIST commands
        public static void IEHistory()
        {
            string[] history = IExplorer.GetHistory();

            if (history != null && history.Length > 0)
            {
                string text = "";
                foreach (string h in history)
                    text = text + h + "\n";

                Answer.SendAnswer(TelSMS, text, AnswerType, true, Fake);
            }
            else
                Application.Exit();
        }

        // DF command
        public static void DelFolder(string folder)
        {
            try
            {
                Utils.Wipe(folder);

                Answer.SendAnswer(TelSMS, Messages.msg_Ok, AnswerType, true, Fake);
            }
            catch (Exception ex)
            {
                Debug.AddLog("DelFolder error: " + ex.Message.ToString(), true);
                Answer.SendAnswer(TelSMS, Messages.msg_Error + ": " + ex.Message.ToString(), AnswerType, true, Fake);
            }
        }

        // CustomMSGCoord command
        public static void CustomMSGCoord(string msg)
        {
            Debug.AddLog("CustomMSGCoord: " + msg + " para: " + Commands.TelSMS, true);

            char[] delimiterChars = { '$' };
            string[] numbers = Commands.TelSMS.Split(delimiterChars);

            foreach (string n in numbers)
            {
                Debug.AddLog("CustomMSGCoord: enviando mensagem para " + n, true);
                Answer.SendAnswer(n, msg, AnswerType, false, Fake);
            }

            Debug.AddLog("CustomMSGCoord: terminando", true);
            Application.Exit();
        }

        // RS0 e RS1 commands
        public static void RS(bool ligar)
        {
            Debug.AddLog("RS: ligar? " + (ligar ? "sim" : "não"), true);

            RegistryKey r = Registry.LocalMachine.CreateSubKey(JVUtils.JVUtils.JVSoftwareKey + "\\ATM");

            if (r != null)
            {
                Debug.AddLog("RS: Alterando chave.", true);
                r.SetValue("rs", ligar.ToString());

                r.Close();
                Debug.AddLog("RS: Chave alterada.", true);

                Answer.SendAnswer(TelSMS,
                    "O rastreamento foi " + (ligar ? "ligado" : "desligado") + ".", AnswerType, true, Fake);
            }
            else
            {
                Debug.AddLog("RS: Erro ao gravar na chave do ATM.", true);
                Answer.SendAnswer(TelSMS,
                    "Não foi possível " + (ligar ? "ligar" : "desligar") + " o rastreamento.", AnswerType, true, Fake);
            }
        }

        public static void RetornoTIM(string msg)
        {
            Debug.AddLog("RetornoTIM: inicio. Buscando configuração.", true);
            RegistryKey r = Registry.LocalMachine.OpenSubKey(JVUtils.JVUtils.JVSoftwareKey + "\\ATM\\Config");
            string tel = "";
            string versao = "";
            string palm = "";

            if (r != null)
            {
                tel = SimpleCryptography.DeCryptography((string)r.GetValue("NumeroChamado", "")).Trim();
                Debug.AddLog("RetornoTIM: tel: " + tel, true);
            }
            r.Close();

            r = Registry.LocalMachine.OpenSubKey(JVUtils.JVUtils.JVSoftwareKey + "\\ATM");
            if (r != null)
            {
                versao = ((string)r.GetValue("Versao", "")).Trim();
                palm = ((string)r.GetValue("PALM", "")).Trim();
                Debug.AddLog("RetornoTIM: versão: " + versao + " PALM: " + palm, true);
            }
            r.Close();

            if (tel.Equals(""))
            {
                Debug.AddLog("RetornoTIM: nada a fazer.", true);
                Application.Exit();
            }
            else
            {
                OwnerRecord Proprietario = OwnerInfo.GetOwnerRecord();
                string mensagem =
                       versao + ";12;" +
                       Utils.PegaLinhaAtual(rtCommon.GetIMSI()) + ";" +
                       Utils.PegaIP() + ";" +
                       Proprietario.EMail + ";" +
                       JVUtils.ICCID.GETICCID() + ";" +
                       rtCommon.GetIMEI() + ";" +
                       System.Convert.ToString(Utils.BatteryMetter()) + ";" +
                       Proprietario.UserName + ";" +
                       palm + ";" +
                       Proprietario.Phone + ";" +
                       Proprietario.Address.Replace("\\", " ") + ";;" + msg;

                Debug.AddLog("RetornoTIM: enviar mensagem: " + mensagem, true);
                Answer.SendAnswer(tel, mensagem, AnswerType.SMS, true, Fake);
            }

            Debug.AddLog("RetornoTIM: finalizando", true);
        }

        // SendSMS command
        public static void SendSMS(string msg)
        {
            Debug.AddLog("SendSMS: " + msg + " para: " + Commands.TelSMS, true);

            char[] delimiterChars = { ';' };
            string[] numbers = Commands.TelSMS.Split(delimiterChars);

            foreach (string n in numbers)
            {
                Debug.AddLog("SendSMS: enviando mensagem para " + n, true);
                Answer.DoNotAddBatteryMetter = true;
                Answer.SendAnswer(n, msg, AnswerType, false, Fake);
            }

            Debug.AddLog("SendSMS: terminando", true);
            Application.Exit();
        }
    }
}
