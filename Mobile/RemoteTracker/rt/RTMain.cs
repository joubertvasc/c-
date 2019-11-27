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
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;
using CommonDLL;
using JVUtils;
using JVGPS;

namespace rt
{
    public partial class RTMain : Form
    {
        #region Internal Variables
        //Configuration
        RTCommon rtCommon;
        Decimal count = 0;
        int timeZoneOffSet;
        string sTextToSend = "";
        public bool bTimeOut = false;
        bool bIsEmergency = false;
        string sFullMessage = "";

        // GPS 
        public GPS gps;
        private SatelliteRecord sr = new SatelliteRecord();
        double lastLatitude = 0;
        double lastLongitude = 0;

        // Command line parameters
        string commandAsParameter = "";
        string numberAsParameter = "";
        string passwordAsParameter = "";
        bool commandIsFake = false;
        bool bHideAfterInit = false;
        #endregion

        public RTMain(string[] parameters)
        {
            bHideAfterInit = parameters.Length != 0;
            InitializeComponent();

            bool bHide = false;

            Debug.SaveAfterEachAdd = true;
            Debug.StartLog(ShellFolders.TempFolder + "\\rtdebug.txt");

            if (bHideAfterInit) 
            {
                Debug.AddLog("Parameters: " + parameters.Length.ToString(), true);
                
                for (int i = 0; i < parameters.Length; i++)
                {
                    Debug.AddLog("Parameter" + i.ToString() + ": " + parameters[i].ToLower(), true);

                    if (parameters[i].ToLower().Equals("/test"))
                    {
                        PhoneInfo pi = new PhoneInfo();

                        MessageBox.Show(Meedios.GetConfig(pi.GetIMEI()));
                        Application.Exit();
                    }

                    if (parameters[i].ToLower().StartsWith("/message:"))
                    {
                        bHide = true;
                        sFullMessage = parameters[i].ToLower().Substring(parameters[i].IndexOf(':') + 1);
                        Debug.AddLog("Message passed as parameter: " + sFullMessage, true);
                    }

                    if (parameters[i].ToLower().StartsWith ("/c:"))
                    {
                        commandAsParameter = parameters[i].ToLower().Substring(parameters[i].IndexOf(':') + 1);
                        Debug.AddLog("Command passed as parameter: " + commandAsParameter, true);
                    }

                    if (parameters[i].ToLower().StartsWith("/n:") ||
                        parameters[i].ToLower().StartsWith("/e:"))
                    {
                        numberAsParameter = parameters[i].ToLower().Substring(parameters[i].IndexOf(':') + 1);
                        Debug.AddLog("Number passed as parameter: " + numberAsParameter, true);
                    }

                    if (parameters[i].ToLower().StartsWith("/p:"))
                    {
                        passwordAsParameter = parameters[i].ToLower().Substring(parameters[i].IndexOf(':') + 1);
                        Debug.AddLog("Password passed as parameter: ********", true);
                    }

                    if (parameters[i].ToLower().StartsWith("/f"))
                    {
                        commandIsFake = true;
                        Debug.AddLog("Fake Command mode" + commandAsParameter, true);
                    }

                    if (parameters[i].ToLower().StartsWith("/hide"))
                    {
                        bHide = true;
                    }

                    if (parameters[i].Equals("/EMERGENCY"))
                    {
                        bIsEmergency = true;
                        commandAsParameter = "gp";
                        Debug.AddLog("Command started by TopSecrect. The user has an emergency!", true);
                        break;
                    }
                }

                if (!bHide && commandAsParameter.Equals("") && sFullMessage.Equals(""))
                {
                    Application.Exit();
                }
            }
        }

        ~RTMain()
        {
            JVUtils.Power.DisableSleep(false);
            Debug.EndLog();
            Debug.SaveLog();
        }

        private void RTMain_Load(object sender, EventArgs e)
        {
            if (bHideAfterInit)
            {
                this.Hide();
                this.Visible = false;
                this.Enabled = false;
                Application.DoEvents();
            }

            rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase));
            // Set SMS interceptor
            if (JVUtils.JVUtils.Get_ContractWasAccepted("RT", rtCommon.termOfServiceRevisionNumber))
            {
                rtCommon.CreateInterceptor();

                rtCommon.mi.MessageReceived += new MessageInterceptorEventHandler(mi_MessageReceived);

                int configErrorCode = rtCommon.configuration.LoadConfiguration(true);

                tmELT.Interval = rtCommon.configuration.TimeELTCommand * 60000;

                // GPS
                gps = new GPS();
                gps.GetGPSDataEvent += new GPS.GetGPSDataEventHandler(GetGPSDataEventHandler);
                gps.ChangeGPSType(rtCommon.configuration.GpsType);

                if (rtCommon.configuration.GpsType == GPSType.Manual)
                {
                    gps.ComPort = Utils.ConvertStringToCOMPort(rtCommon.configuration.ComPort);
                    gps.BaudRate = Utils.ConvertStringToBaudRate(rtCommon.configuration.BaudRate);
                }

                Debug.AddLog("RemoteTracker version " + rtCommon.version +
                             " debug log. Installation path: " + rtCommon.appPath + "\n" +
                             "JVGPS.DLL version " + gps.Version + "\n" +
                             "JVUtils.DLL version " + JVUtils.JVUtils.Version + "\n" +
                             "Configuration: " +
                             "AlarmSound: " + rtCommon.configuration.AlarmSound + "\n" +
                             "ELT Timeout: " + rtCommon.configuration.TimeELTCommand + "\n" +
                             "GPS Interactions: " + rtCommon.configuration.defaultGPSInteractions + "\n" +
                             "Language: " + rtCommon.configuration.defaultLanguage + "\n" +
                             "Is emergency number set? " + Utils.iif(rtCommon.configuration.defaultNumber1 != "", "Yes", "No") + "\n" +
                             "Is password set? " + Utils.iif(rtCommon.configuration.defaultPassword != "", "Yes", "No") + "\n" +
                             "Is E-Mail Account set? " + Utils.iif(rtCommon.configuration.defaultEMailAccount != "", "Yes", "No") + "\n" +
                             "Is recipient name set? " + Utils.iif(rtCommon.configuration.defaultrecipientName != "", "Yes", "No") + "\n" +
                             "Is recipient E-Mail set? " + Utils.iif(rtCommon.configuration.defaultrecipientEMail != "", "Yes", "No") + "\n" +
                             "Is Subject set? " + Utils.iif(rtCommon.configuration.defaultSubject != "", "Yes", "No") + "\n" +
                             "Is IMSI set? " + Utils.iif(rtCommon.configuration.IMSI1 != "", "Yes", "No") + "\n" +
                             "GPS Type=" + (rtCommon.configuration.GpsType == GPSType.Windows ? "Windows" :
                                            "Manual " + rtCommon.configuration.ComPort + " BaudRate: " + rtCommon.configuration.BaudRate), true);

                Commands.configuration = rtCommon.configuration;
                Commands.SatelliteRecord = sr;

                timeZoneOffSet = Utils.CalcTimeZoneOffSet();

                BatteryMetter();

                Commands.AppPath = rtCommon.appPath;
                Commands.TimerELT = tmELT;
                Commands.GPS = gps;
                Commands.rtCommon = rtCommon;
                rtCommon.gps = gps;
                Answer.GPS = gps;
                Answer.Configurations = rtCommon.configuration;

                if (!bIsEmergency)
                {
                    Debug.AddLog("ChangeLanguage: before load XML.", true);
                    if (rtCommon.configuration.defaultLanguage >= rtCommon.languages.count)
                        rtCommon.configuration.defaultLanguage = 0;

                    if (rtCommon.languageXML.LoadLanguageXML(rtCommon.appPath + 
                                                             rtCommon.languages.fileName(rtCommon.configuration.defaultLanguage)))
                        Messages.ChangeLanguage(rtCommon.languageXML);
                }

                Debug.AddLog("Full Message: " + sFullMessage, true);
                Debug.AddLog("Command as Parameter: " + commandAsParameter, true);
                Debug.AddLog("Number as Parameter: " + numberAsParameter, true);
                Debug.AddLog("Command is fake? " + (commandIsFake ? "Yes" : "No"), true);
                Debug.AddLog("Is Emergency? " + (bIsEmergency ? "Yes" : "No"), true);

                if (sFullMessage != "" || bIsEmergency || (commandAsParameter != "" && (numberAsParameter != "" || commandIsFake)))
                {
                    Debug.AddLog("Going to process the command", true);
                    mi_MessageReceived(null, null);
                }
            }
            else
            {
                Debug.AddLog("ToS not accepted, exiting...", true);
                Application.Exit();
            }
        }

        private void RTMain_Activated(object sender, EventArgs e)
        {
            if (bHideAfterInit)
            {
                this.Hide();
                this.Visible = false;
                this.Enabled = false;
                Application.DoEvents();
            }
        }

        string ExtractPhoneNumber(string address)
        {
            string result = "";

            foreach (char c in address)
            {
                if (c == '$' || c == '+' || ((int)c > 47 && (int)c < 58))
                    result += c;
            }

            if (result.IndexOf('+') > 0)
                result = result.Substring(result.IndexOf('+'));

            return result;
        }

        void WriteCommandLog(string command, string sender)
        {
            StreamWriter sw;

            if (File.Exists(rtCommon.appPath + "RTCommandLog.txt"))
                sw = File.AppendText(rtCommon.appPath + "RTCommandLog.txt");
            else
                sw = File.CreateText(rtCommon.appPath + "RTCommandLog.txt");

            sw.WriteLine(SimpleCryptography.Cryptography(DateTime.Now.ToString().Substring(0, 14) + 
                                                         " " + command + " " + sender));
            sw.Flush();
            sw.Close();
        }

        void mi_MessageReceived(object sender, MessageInterceptorEventArgs e)
        {
            Debug.AddLog("mi_MessageReceived: started", true);

            if (bIsEmergency)
                Debug.AddLog("THIS IS AN EMERGENCY!!!", true);

            // Remove the sleep mode.
            JVUtils.Power.DisableSleep(true);

            if (rtCommon.configuration.ScreenOff && !commandIsFake)
                Power.PowerOnOff(false);

            bTimeOut = false;
            Commands.TimeOut = false;
            Commands.GoogleMapsFileName = "";

            this.Hide();
            Application.DoEvents();
            string text = "";
            string originalText = "";
            string senderNumber = "";

            // Verify if this method was called by a SMS event or command line parameter
            if (e != null || !sFullMessage.Equals(""))
            {
                if (!sFullMessage.Equals(""))
                {
                    Debug.AddLog("mi_MessageReceived: command from RTRule.dll", true);
                    Debug.AddLog("mi_MessageReceived: pipe pipe position: " + sFullMessage.IndexOf("@@").ToString(), true);

                    senderNumber = ExtractPhoneNumber(sFullMessage.Substring(sFullMessage.IndexOf("@@") + 2));
                    Debug.AddLog("mi_MessageReceived: senderNumber: " + senderNumber, true);
                    Debug.AddLog("mi_MessageReceived: text(1): " + text, true);

                    text = sFullMessage.Replace("rt2", "rt");
                    Debug.AddLog("mi_MessageReceived: text(2): " + text, true);

                    if (sFullMessage.IndexOf("@@") > 0)
                        text = text.Substring(0, sFullMessage.IndexOf("@@") - 1);

                    Debug.AddLog("mi_MessageReceived: text(3): " + text, true);

                    if (!text.StartsWith("rt#"))
                        text = text.Substring(text.IndexOf("rt#"));

                    Debug.AddLog("mi_MessageReceived: text(4): " + text, true);

                    //if (text.IndexOf(" ") > 0)
                    //    text = text.Substring(0, text.IndexOf(" ")).Trim();

                    Debug.AddLog("mi_MessageReceived: text(5): " + text, true);
                }
                else
                {
                    Debug.AddLog("mi_MessageReceived: command from SMS.\nFrom: " + ((SmsMessage)e.Message).From.Address +
                                 "\nBody: " + ((SmsMessage)e.Message).Body.ToLower(), true);
                    text = ((SmsMessage)e.Message).Body.ToLower();
                    senderNumber = ExtractPhoneNumber(((SmsMessage)e.Message).From.Address);
                }

                // For WEB SMS ignore what is comming before rt#
                if (!text.StartsWith("rt#"))
                    text = text.Substring(text.IndexOf("rt#"));

                originalText = text;
            }
            else 
            {
                text = "#" + commandAsParameter +
                    (numberAsParameter != "" ? "#" + numberAsParameter : "#") +
                    (passwordAsParameter != "" ? "#" + passwordAsParameter : "");
                senderNumber = numberAsParameter;
                senderNumber = ExtractPhoneNumber(numberAsParameter);
                originalText = text;
                Debug.AddLog("mi_MessageReceived: fake command: " + commandAsParameter + 
                             ", number: " + numberAsParameter +
                             ", password set? " + (passwordAsParameter.Equals("") ? "No" : "Yes"), true);
            }

//            Debug.AddLog("mi_MessageReceived. Command received: " + text, true);

            char[] delimiterChars = { '#' };
            string[] words = text.Split(delimiterChars);

            // Getting command
            String command = "";
            String password = "";
            Commands.TelSMS = "";

            if (words.Length > 1)
            {
                command = words[1].Trim();
                if (command.StartsWith(System.Convert.ToString((char)34)) || 
                    command.StartsWith(System.Convert.ToString((char)39)))
                    command = command.Remove(0, 1);

                if (command.EndsWith(System.Convert.ToString((char)34)) ||
                    command.EndsWith(System.Convert.ToString((char)39)))
                    command = command.Remove(command.Length - 1, 1);

                if (command.IndexOf(" ") > 0)
                    command = command.Substring(0, command.IndexOf(" ")).Trim();

                if (words.Length > 2)
                {
                    Commands.TelSMS = words[2].Trim();

                    if (words.Length > 3)
                    {
                        // START - special check for SMS over eMail/WEB checking for first invalid character 
                        int lenpassword = 0;
                        password = (words[3].Trim().ToLower());

                        // 0 - 48; 9 - 57; a - 97; z - 122
                        foreach (char c in password)
                        {
                            if (((int)c > 47 && (int)c < 58) || ((int)c > 96 && (int)c < 123))
                            {
                                lenpassword = lenpassword + 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        password = words[3].Substring(0, lenpassword);
                        // END - special check for SMS over eMail/WEB
                    }
                }
            }

            // if no command was invoked, send HELP
            if (command == "")
            {
                command = "help";
            }

            Commands.LastCommand = command.ToLower();

            // The command invoke SMS, E-Mails or WEB?
            if (command.Substring(0, 1).ToLower().Equals("e"))
                Commands.AnswerType = AnswerType.EMAIL;
            else if (!command.ToLower().Equals("findme") && command.Substring(0, 1).ToLower().Equals("f"))
                Commands.AnswerType = AnswerType.FTP;
            else if (command.Substring(0, 1).ToLower().Equals("w"))
                Commands.AnswerType = AnswerType.WEB;
            else
                Commands.AnswerType = AnswerType.SMS;

            Debug.AddLog("mi_MessageReceived. Type of answer? " +
                (Commands.AnswerType == AnswerType.SMS ? "SMS" :
                (Commands.AnswerType == AnswerType.EMAIL ? "EMAIL" :
                (Commands.AnswerType == AnswerType.FTP ? "FTP" :
                (Commands.AnswerType == AnswerType.WEB ? "WEB" : "UNKNOW")))), true);

            // If number was passed as null, use sender number or e-mail
            if (Commands.TelSMS == "")
            {
                if (Commands.AnswerType == AnswerType.EMAIL)
                {
                    Commands.TelSMS = rtCommon.configuration.defaultrecipientEMail.Trim();
                }
                else
                {
                    Commands.TelSMS = senderNumber.Trim();
                }
            }

            // If there are no defined number, get out
            if (!Commands.CommandsWithoutPhoneNumber(Commands.LastCommand) && 
                Commands.TelSMS.Equals("") && !commandIsFake && !bIsEmergency)
            {
                Debug.AddLog("mi_MessageReceived. Empty TelSMS.", true);
                Application.Exit();
            }
            else
            {
                if (command.ToLower().Equals("msg") ||
                    command.ToLower().Equals("alarm") || 
                    Commands.AnswerType != AnswerType.SMS || commandIsFake || bIsEmergency ||
                    (Commands.AnswerType == AnswerType.SMS && Utils.ValidPhoneNumber(Commands.TelSMS)))
                {
                    Debug.AddLog("mi_MessageReceived. After parser: \nCommand = " + command +
                                 "\nTelSMS = " + Commands.TelSMS +
                                 "\nPassword = *******" /* + password /**/, true);

                    // Verify password ignoring case. If wrong, get out
                    if (!bIsEmergency &&
                        !Commands.CommandsWithoutPassword(Commands.LastCommand) &&
                        rtCommon.configuration.defaultPassword != "" &&
                        !rtCommon.configuration.defaultPassword.ToLower().Equals(password.ToLower()))
                    {
                        Debug.AddLog("mi_MessageReceived. Password does not match.", true);
                        Answer.SendAnswer(Commands.TelSMS, Messages.msg_PasswordDoesNotMatch,
                                          Commands.AnswerType, true, commandIsFake);
                    }
                    else
                    {
                        // Write a log of commands received
                        WriteCommandLog(Commands.LastCommand, Commands.TelSMS);

                        // Verify if the command is 'findme'. If so, ask the user if RT have to answer or not. If so, change the 
                        // command to 'gp'.
                        if (Commands.LastCommand.Equals("findme"))
                        {
                            if (rtCommon.configuration.ScreenOff && !commandIsFake)
                                Power.PowerOnOff(true);

                            Kernel.PlayFile(rtCommon.configuration.AlarmSound, true);

                            if (MessageBox.Show(Commands.TelSMS + " " + Messages.msg_findme_call, "RemoteTracker",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                Commands.LastCommand = "gp";
                            }
                            else
                            {
                                Commands.LastCommand = "noreply";
                            }
                        }

                        if (Commands.LastCommand.Equals("gp") ||
                            (Commands.LastCommand.Equals("agp") && rtCommon.atm) ||
                            Commands.LastCommand.Equals("egp") ||
                            Commands.LastCommand.Equals("fgp") ||
                            Commands.LastCommand.Equals("wgp") ||
                            Commands.LastCommand.Equals("getposition") ||
                            Commands.LastCommand.Equals("egetposition") ||
                            Commands.LastCommand.Equals("fgetposition") ||
                            Commands.LastCommand.Equals("wgetposition") ||
                            Commands.LastCommand.Equals("elt") ||
                            Commands.LastCommand.Equals("flt") ||
                            Commands.LastCommand.Equals("wlt") ||
                            Commands.LastCommand.Equals("elogtrack") ||
                            Commands.LastCommand.Equals("flogtrack") ||
                            Commands.LastCommand.Equals("wlogtrack") ||
                            Commands.LastCommand.StartsWith("custommsgcoord"))
                        {
                            Debug.AddLog("mi_MessageReceived. The command uses GPS.", true);

                            if (gps.GPSPresent())
                            {
                                if (Commands.LastCommand.Equals("elt") ||
                                    Commands.LastCommand.Equals("flt") ||
                                    Commands.LastCommand.Equals("wlt") ||
                                    Commands.LastCommand.Equals("elogtrack") ||
                                    Commands.LastCommand.Equals("flogtrack") ||
                                    Commands.LastCommand.Equals("wlogtrack"))
                                {
                                    Debug.AddLog("mi_MessageReceived. ELT timer enabled.", true);
                                    tmELT.Enabled = true;
                                }

                                count = 0;

                                Debug.AddLog("mi_MessageReceived. Count = 0.", true);
                                Debug.AddLog("mi_MessageReceived. gps is opened? " + Utils.iif(gps.IsStarted, "yes", "no"), true);
                                if (!gps.IsStarted)
                                {
                                    sr.Clear();

                                    Debug.AddLog("Before open GPS.", true);

                                    gps.Start();

                                    Debug.AddLog("After open GPS.", true);
                                }
                            }
                            else
                            {
                                Debug.AddLog("mi_MessageReceived. GPS driver not found.", true);

                                if (Commands.LastCommand.Equals("agp"))
                                    sTextToSend = MontaMensagemParaATM(0, 0);
                                else
                                    sTextToSend = rtCommon.GetTextToSendFromNonGPS();

                                ProcessCommand();
                            }
                        }
                        else
                        {
                            Debug.AddLog("mi_MessageReceived. The command does not use GPS.", true);
                            ProcessCommand();
                        }
                    }
                }
                else
                {
                    Debug.AddLog("mi_MessageReceived. " + Commands.TelSMS + " is not a valid phone number", true);
                    Application.Exit();
                }
            }
        }

        private void ProcessCommand()
        {
            // This parameter is used to show the result in a message 
            Commands.Fake = commandIsFake;
            Debug.AddLog("ProcessCommand. Command is fake?" + (Commands.Fake ? "Yes" : "No"), true);

            // Commands HELP: send the list of commands to caller
            if (Commands.LastCommand.Equals("help") || 
                Commands.LastCommand.Equals("ehelp") ||
                Commands.LastCommand.Equals("fhelp") || 
                Commands.LastCommand.Equals("whelp"))
            {
                Debug.AddLog("ProcessCommand. Help command.", true);
                Commands.SendHelp();
            }
            // Command CB: callback the caller
            else if (Commands.LastCommand.Equals("cb") || 
                     Commands.LastCommand.Equals("callback"))
            {
                Debug.AddLog("ProcessCommand. CB command.", true);
                Commands.CallBack();
            }
            // Commands PB: send the phonebook (stored in SIM card) to caller
            else if (Commands.LastCommand.Equals("pb") || 
                     Commands.LastCommand.Equals("epb") ||
                     Commands.LastCommand.Equals("fpb") || 
                     Commands.LastCommand.Equals("wpb") ||
                     Commands.LastCommand.Equals("phonebook") ||
                     Commands.LastCommand.Equals("ephonebook") ||
                     Commands.LastCommand.Equals("fphonebook") || 
                     Commands.LastCommand.Equals("wphonebook"))
            {
                Debug.AddLog("ProcessCommand. PB command.", true);
                Commands.SendPhoneBook();
            }
            // Commands GP: send the current GPS Position (coordinates) to caller
            else if (Commands.LastCommand.Equals("gp") ||
                     (Commands.LastCommand.Equals("agp") && rtCommon.atm) ||
                     Commands.LastCommand.Equals("egp") ||
                     Commands.LastCommand.Equals("wgp") || 
                     Commands.LastCommand.Equals("fgp") ||
                     Commands.LastCommand.Equals("getposition") ||
                     Commands.LastCommand.Equals("egetposition") ||
                     Commands.LastCommand.Equals("fgetposition") || 
                     Commands.LastCommand.Equals("wgetposition"))
            {
                Debug.AddLog("ProcessCommand. GP command.", true);
                Commands.SendCoordinates(sTextToSend, bIsEmergency, Commands.LastCommand.Equals("agp"));
            }
            // Command LT: make a Log Track
            else if (Commands.LastCommand.Equals("elt") || 
                     Commands.LastCommand.Equals("flt") ||
                     Commands.LastCommand.Equals("wlt") ||
                     Commands.LastCommand.Equals("elogtrack") ||
                     Commands.LastCommand.Equals("flogtrack") ||
                     Commands.LastCommand.Equals("wlogtrack"))
            {
                Debug.AddLog("ProcessCommand. ELT command.", true);
                Commands.LogTrack();
            }
            // Command RST: reset the device
            else if (Commands.LastCommand.Equals("rst") || 
                     Commands.LastCommand.Equals("reset"))
            {
                Debug.AddLog("ProcessCommand. RST command.", true);
                Commands.SoftReset();
            }
            // Commands GO: get the owner informations
            else if (Commands.LastCommand.Equals("go") || 
                     Commands.LastCommand.Equals("fgo") || 
                     Commands.LastCommand.Equals("wgo") ||
                     Commands.LastCommand.Equals("ego") ||
                     Commands.LastCommand.Equals("getowner") ||
                     Commands.LastCommand.Equals("egetowner") ||
                     Commands.LastCommand.Equals("fgetowner") ||
                     Commands.LastCommand.Equals("wgetowner"))
            {
                Debug.AddLog("ProcessCommand. GO command.", true);
                Commands.GetOwner();
            }
            // Commands GI: get phone informations
            else if (Commands.LastCommand.Equals("gi") || 
                     Commands.LastCommand.Equals("fgi") || 
                     Commands.LastCommand.Equals("wgi") ||
                     Commands.LastCommand.Equals("egi") ||
                     Commands.LastCommand.Equals("getinfo") ||
                     Commands.LastCommand.Equals("egetinfo") ||
                     Commands.LastCommand.Equals("fgetinfo") ||
                     Commands.LastCommand.Equals("wgetinfo"))
            {
                Debug.AddLog("ProcessCommand. GI command.", true);
                Commands.GetInfo();
            }
            // Command DSC: delete SIM contacts
            else if (Commands.LastCommand.Equals("dsc") || 
                     Commands.LastCommand.Equals("delsim "))
            {
                Debug.AddLog("ProcessCommand. DSC command.", true);
                Commands.DelSimContacts();
            }
            // Command dkz: delete all KMZ files
            else if (Commands.LastCommand.Equals("dkz") || 
                     Commands.LastCommand.Equals("delkmz"))
            {
                Debug.AddLog("ProcessCommand. DKZ command.", true);
                Commands.DelKMZFiles();
            }
            // Command GANFL: get audio notes file list
            else if (Commands.LastCommand.Equals("ganfl") || 
                     Commands.LastCommand.Equals("eganfl") ||
                     Commands.LastCommand.Equals("fganfl") || 
                     Commands.LastCommand.Equals("wganfl") ||
                     Commands.LastCommand.Equals("getanfilelist") ||
                     Commands.LastCommand.Equals("egetanfilelist") ||
                     Commands.LastCommand.Equals("fgetanfilelist") ||
                     Commands.LastCommand.Equals("wgetanfilelist"))
            {
                Debug.AddLog("ProcessCommand. GANFL or EGANFL command.", true);
                Commands.GetAudioNotesFileList();
            }
            // Command GANF: get an audio note file and send to caller
            else if (Commands.LastCommand.StartsWith("eganf") ||
                     Commands.LastCommand.StartsWith("fganf") ||
                     Commands.LastCommand.StartsWith("wganf") ||
                     Commands.LastCommand.StartsWith("egetanfile") ||
                     Commands.LastCommand.StartsWith("fgetanfile") ||
                     Commands.LastCommand.StartsWith("wgetanfile"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                int limit = 5;

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else if (i == 1)
                    {
                        try
                        {
                            limit = System.Convert.ToInt32(words[i]);
                        }
                        catch
                        {
                            limit = 5;
                        }
                    }
                }

                Debug.AddLog("ProcessCommand. EGANF command.", true);
                Commands.GetAudioNotesFiles(limit);
            }
            // Command alarm
            else if (Commands.LastCommand.Equals("alarm"))
            {
                Debug.AddLog("ProcessCommand. ALARM command.", true);
                Commands.Alarm();
            }
            // Command MSG
            else if (Commands.LastCommand.Equals("msg") || 
                     Commands.LastCommand.Equals("message"))
            {
                Debug.AddLog("ProcessCommand. MSG command.", true);
                Commands.Msg(Commands.TelSMS);
            }
            // Command IP
            else if (Commands.LastCommand.Equals("gip") || 
                     Commands.LastCommand.Equals("egip") ||
                     Commands.LastCommand.Equals("fgip") || 
                     Commands.LastCommand.Equals("wgip") ||
                     Commands.LastCommand.Equals("getip") ||
                     Commands.LastCommand.Equals("egetip") ||
                     Commands.LastCommand.Equals("fgetip") ||
                     Commands.LastCommand.Equals("wgetip"))
            {
                Commands.GetCurrentIP();
            }
            // Command VNC
            else if (Commands.LastCommand.Equals("vnc"))
            {
                Commands.StartVNC();
            }
            // Command LISTAPP
            else if (Commands.LastCommand.Equals("listapp") || 
                     Commands.LastCommand.Equals("elistapp") ||
                     Commands.LastCommand.Equals("flistapp") || 
                     Commands.LastCommand.Equals("wlistapp"))
            {
                Commands.ListApp();
            }
            // Command RUNAPP
            else if (Commands.LastCommand.StartsWith("runapp"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string app = "";
                string parameters = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else if (i == 1)
                    {
                        app = words[i]; // The application to run
                    }
                    else
                    {
                        parameters += words[i] + " ";
                    }
                }

                Commands.RunApp(app, parameters);
            }
            // Command CELLID
            else if (Commands.LastCommand.Equals("cellid") || 
                     Commands.LastCommand.Equals("ecellid") ||
                     Commands.LastCommand.Equals("fcellid") || 
                     Commands.LastCommand.Equals("wcellid"))
            {
                Commands.CellID();
            }
            // Command LOCK
            else if (Commands.LastCommand.StartsWith("lock"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string messageToLock = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else
                    {
                        messageToLock += words[i] + " ";
                    }
                }

                Commands.Lock(true, messageToLock);
            }
            // Command UNLOCK
            else if (Commands.LastCommand.Equals("unlock"))
            {
                Commands.Lock(false, "");
            }
            // Command CALLHIST
            else if (Commands.LastCommand.StartsWith("callhist") ||
                     Commands.LastCommand.StartsWith("ecallhist") ||
                     Commands.LastCommand.StartsWith("fcallhist") ||
                     Commands.LastCommand.StartsWith("wcallhist"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string[] files = new string[0];
                int limit = 5;

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else
                    {
                        try
                        {
                            limit = System.Convert.ToInt32(words[i]);
                        }
                        catch
                        {
                            limit = 5;
                        }
                    }
                }

                Commands.CallHistory(limit);
            }
            // Command MSLIST
            else if (Commands.LastCommand.Equals("mslist") || 
                     Commands.LastCommand.Equals("emslist") ||
                     Commands.LastCommand.Equals("fmslist") || 
                     Commands.LastCommand.Equals("wmslist"))
            {
                Commands.MortScriptList();
            }
            // Command MSRUN
            else if (Commands.LastCommand.StartsWith("msrun"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string[] files = new string[0];

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else
                    {
                        Array.Resize(ref files, files.Length + 1);
                        files[files.Length - 1] = words[i];
                    }
                }

                Commands.MortScriptRun(files);
            }
            // Command DELCARD
            else if (Commands.LastCommand.Equals("delcard"))
            {
                Commands.WipeCard();
            }
            // Command FTPDOC
            else if (Commands.LastCommand.Equals("ftpdoc"))
            {
                Commands.FTPDoc();
            }
            // Command FTP
            else if (Commands.LastCommand.StartsWith("ftp"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string[] files = new string[0];

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else
                    {
                        Array.Resize(ref files, files.Length + 1);
                        files[files.Length - 1] = words[i];
                    }
                }

                Commands.FTP(files, true);
            }
            // Command SMS, ESMS or FSMS: get a file with the full history of SMS
            else if (Commands.LastCommand.Equals("sms") || 
                     Commands.LastCommand.Equals("esms") ||
                     Commands.LastCommand.Equals("fsms"))
            {
                Debug.AddLog("ProcessCommand. SMS, ESMS or FSMS command.", true);
                Commands.GetSMS();
            }
            // Command EOUTLOOK or FOUTLOOK: get a file with the full outlook informations
            else if (Commands.LastCommand.Equals("eoutlook") ||
                     Commands.LastCommand.Equals("foutlook"))
            {
                Debug.AddLog("ProcessCommand. EOUTLOOK or FOUTLOOK command.", true);
                Commands.GetOutlook();
            }
            // Command LOSTPASS: send the secret question to user
            else if (Commands.LastCommand.Equals("lostpass"))
            {
                Debug.AddLog("ProcessCommand. LOSTPASS command.", true);
                Commands.GetLostPassword();
            }
            // Command SECRET: receive the secret answer and send the password if it is correct
            else if (Commands.LastCommand.StartsWith("secret"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string secretAnswer = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else
                    {
                        secretAnswer = words[i];
                        break;
                    }
                }

                Commands.SendPassword(secretAnswer);
            }
            // Commands RC and CR: redirect calls / cancel redirect calls
            else if (Commands.LastCommand.Equals("cr") || Commands.LastCommand.StartsWith("rc,"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string newNumber="";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                        Commands.LastCommand = words[i]; // Ignore, this is the command
                    else if (i == 1)
                        newNumber = words[i];
                }

                Debug.AddLog("ProcessCommand. RC or CR commands.", true);
                Commands.RedirectCalls(Commands.LastCommand.Equals("rc"), newNumber);
            }
            // LoopBack command. Used for custom applications
            else if (Commands.LastCommand.Equals("loopback"))
            {
                Debug.AddLog("ProcessCommand. LOOPBACK.", true);
                Commands.LoopBack(rtCommon.GetIMSI(), Commands.TelSMS);
            }
            // ALOG command. Used for custom applications
            else if (Commands.LastCommand.StartsWith("alog") && rtCommon.atm)
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string ano = "";
                string mes = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                        Commands.LastCommand = words[i]; // Ignore, this is the command
                    else if (i == 1 || i == 2)
                    {
                        if (words[i].Length > 2)
                            ano = words[i];
                        else
                            mes = words[i];
                    }
                }

                if (ano.Equals(""))
                    ano = Utils.FillZeros(DateTime.Today.Year.ToString(), 4);

                if (mes.Equals(""))
                    mes = Utils.FillZeros(DateTime.Today.Month.ToString(), 2);

                Debug.AddLog("ProcessCommand. ALOG. Ano=" + ano + ", mês=" + mes, true);
                Commands.ALOG(ano, mes, rtCommon.GetIMEI());
            }
            // AFTP command. Used for custom applications
            else if ((Commands.LastCommand.Equals("aftp") || Commands.LastCommand.Equals("bftp")) && rtCommon.atm)
            {
                Debug.AddLog("ProcessCommand: " + Commands.LastCommand, true);
                string path1 = "";
                string path2 = "";
                string atmVersao = "";

                RegistryKey r = Registry.LocalMachine.OpenSubKey(JVUtils.JVUtils.JVSoftwareKey + "\\ATM");

                if (r != null)
                {
                    if (Commands.LastCommand.Equals("aftp"))
                        path1 = r.GetValue("ftpfolder", "").ToString();
                    else
                    {
                        path1 = r.GetValue("Pasta1", "").ToString();
                        path2 = r.GetValue("Pasta2", "").ToString();
                    }

                    atmVersao = r.GetValue("Versao", "").ToString();
                    r.Close();
                }

                if (!path1.Equals("") || !path2.Equals(""))
                {
                    Debug.AddLog("ProcessCommand. AFTP/BFTP. Pastas: path1=" + path1 + ", pasta2=" + path2, true);
                    Commands.AFTP(rtCommon.GetIMEI(), Commands.LastCommand.Equals("aftp"), path1, path2, atmVersao);
                }
                else
                {
                    Debug.AddLog("ProcessCommand. AFTP/BFTP: nenhuma pasta definida.", true);
                    Application.Exit();
                }
            }
            // Commands IEHIST, EIEHIST, FIEHIST: send the Internet Explorer History
            else if (Commands.LastCommand.Equals("iehist") || Commands.LastCommand.Equals("eiehist") || Commands.LastCommand.Equals("fiehist"))
            {
                Debug.AddLog("ProcessCommand. IEHIST command.", true);
                Commands.IEHistory();
            }
            // Command DF
            else if (Commands.LastCommand.StartsWith("df"))
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string parameters = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else if (i == 1)
                    {
                        parameters = words[i]; // The folder to delete
                    }
                }

                if (File.Exists(parameters))
                    Commands.DelFolder(parameters);
                else
                    Application.Exit();
            }
            // Comandos ADA e ADD 
            else if ((Commands.LastCommand.Equals("ada") || Commands.LastCommand.Equals("add")) && rtCommon.atm)
            {
                Debug.AddLog("ProcessCommand: " + Commands.LastCommand, true);

                Commands.ADA(Commands.LastCommand.Equals("ada"));
            }
            // Command CUSTOMMSGCOORD: send currentposition with custom message
            else if (Commands.LastCommand.StartsWith("custommsgcoord"))
            {
                gps.Stop();

                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string msg = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else
                    {
                        msg = words[i];
                        break;
                    }
                }
                //  Energia desligada, bat. X%, posição WWW.google.com.br xx.xxx,yy.yyy
                Commands.CustomMSGCoord(msg + ". " +
                                        Google.GoogleMapsLink(lastLatitude, lastLongitude));
            }
            // RS0 & RS1 commands. Used for custom applications
            else if ((Commands.LastCommand.Equals("rs0") || Commands.LastCommand.Equals("rs1")) && rtCommon.atm)
            {
                Debug.AddLog("ProcessCommand. RS0 or RS1. " + Commands.LastCommand, true);
                Commands.RS(Commands.LastCommand.Equals("rs1"));
            }
            // RETORNOTIM command. Used for custom applications
            else if (Commands.LastCommand.StartsWith("retornotim") && rtCommon.atm)
            {
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string msg = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else
                    {
                        msg = msg + " " + words[i];
                    }
                }

                Debug.AddLog("ProcessCommand. retornotim. msg=" + msg, true);
                Commands.RetornoTIM(msg);
            }
            // Command FINDME with answered NO...
            else if (Commands.LastCommand.Equals("noreply"))
            {
                // Nothing todo, the users send NO in FINDME command
                Debug.AddLog("ProcessCommand. Exiting because the users typed NO when asked for FINDME command.", true);
                Application.Exit();
            }
            // SENDSMS command. Used to send sms
            else if (Commands.LastCommand.StartsWith("sendsms"))
            {
                Debug.AddLog("ProcessCommand. SENDSMS.", true);
                char[] delimiterChars = { ',' };
                string[] words = Commands.LastCommand.Split(delimiterChars);
                string msg = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (i == 0)
                    {
                        // Ignore, this is the command
                        Commands.LastCommand = words[i];
                    }
                    else
                    {
                        msg = words[i];
                        break;
                    }
                }
                Commands.SendSMS(msg);
            }
            // The requested command does not exists
            else
            {
                Debug.AddLog("ProcessCommand. The command does not exists.", true);
                Answer.SendAnswer(Commands.TelSMS, Messages.msg_InvalidCommand,
                                  Commands.AnswerType, true, commandIsFake);
            }
        }

        void BatteryMetter()
        {
            Answer.Charging = SystemState.PowerBatteryState == BatteryState.Charging;
/*            BatteryLevel bl = SystemState.PowerBatteryStrength;

            switch (bl)
            {
                case BatteryLevel.VeryLow:
                    Answer.BatteryLevel = "0-20%";
                    break;
                case BatteryLevel.Low:
                    Answer.BatteryLevel = "21-40%";
                    break;
                case BatteryLevel.Medium:
                    Answer.BatteryLevel = "41-60%";
                    break;
                case BatteryLevel.High:
                    Answer.BatteryLevel = "61-80%";
                    break;
                case BatteryLevel.VeryHigh:
                    Answer.BatteryLevel = "81-100%";
                    break;
            }
/**/
            BatteryMetter bm = new BatteryMetter();
            int level = bm.BatteryLifePercent();
            Answer.BatteryLevel = level.ToString() + "%";

            Debug.AddLog("BatteryMetter. Charging? " + (Answer.Charging ? "yes" : "no") + " Level: " +
                Answer.BatteryLevel, true);
        }

        private void tmELT_Tick(object sender, EventArgs e)
        {
            tmELT.Enabled = false;
            bTimeOut = true;
            Commands.TimeOut = true;

            Debug.AddLog("tmELT_Tick. ELT timed out.", true);

            ProcessCommand();
        }

        string MontaMensagemParaATM(double lat, double lon)
        {
            string currentIP = "0";

            try
            {
                string[] IPs = Utils.GetInternalIP();

                if (IPs != null && IPs.Length != 0)
                    currentIP = IPs[0];
            }
            catch
            {
                currentIP = "0";
            }

            OwnerRecord or = OwnerInfo.GetOwnerRecord();
            if (or == null)
                or = new OwnerRecord();

            string linha = LineNumberStore.GetLineNumberForIMSI(rtCommon.GetIMSI());

            if (linha.Equals(""))
                linha = "0";

            if (lat == 0 && lon == 0)
            {
                rtCommon.GetCoordinatesFromNonGPS(ref lat, ref lon);
            }

            Debug.AddLog("MontaMensagemParaATM: Lat: " + System.Convert.ToString(lat) + ", lon: " + System.Convert.ToString(lon), true);
            Debug.AddLog("MontaMensagemParaATM: Lat: " + Utils.ChangeChar(System.Convert.ToString(lat), ',', '.') + ", lon: " + Utils.ChangeChar(System.Convert.ToString(lon), ',', '.'), true);

            RegistryKey r = Registry.LocalMachine.OpenSubKey(JVUtils.JVUtils.JVSoftwareKey + "\\ATM");
            string atmVersao = "";

            if (r != null)
            {
                atmVersao = r.GetValue("Versao", "").ToString();
                r.Close();
            }

            return atmVersao + ";;" +
                   linha + ";" +
                   currentIP + ";" +
                   ";" +
                   Utils.RemoveChar(JVUtils.ICCID.GETICCID(), ' ') + ";" +
                   Utils.RemoveChar(rtCommon.GetIMEI(), ' ') + ";" +
                   System.Convert.ToString(Utils.BatteryMetter()) + ";" +
                   or.UserName + ";" +
                   or.Company + ";" +
                   or.Phone + ";" +
                   or.Address.Replace("\\", " ") + ";;" +
                   Utils.ChangeChar(lat.ToString(), ',', '.') + "," + Utils.ChangeChar(lon.ToString(), ',', '.');
        }

        void GetGPSDataEventHandler(object sender, GetGPSDataEventArgs args)
        {
            sr.Connected = args.GPSData.IsValid;

            if (args.GPSData.IsValid)
            {
                sr.SeaLevelAltitude = args.GPSData.Altitude;
                sr.Time = args.GPSData.GPSDateTime;
                sr.Latitude = args.GPSData.Latitude;
                sr.Longitude = args.GPSData.Longitude;
                sr.SatellitesCount = args.GPSData.SatellitesInView;
                sr.Speed = args.GPSData.Speed;
                sr.LatitudeDMS = args.GPSData.LatitudeDMS;
                sr.LongitudeDMS = args.GPSData.LongitudeDMS;

                lastLatitude = sr.Latitude;
                lastLongitude = sr.Longitude;

                if (Commands.LastCommand.Equals("agp"))
                {
                    sTextToSend = MontaMensagemParaATM(sr.ShortLatitude, sr.ShortLongitude);
                }
                else
                {
                    sTextToSend = Messages.MessageToSend(Commands.AnswerType, sr);
                }
                
                Debug.AddLog("UpdateData. TextToSend: " + sTextToSend, true);
                Debug.AddLog("UpdateData: is GPS opened? " + Utils.iif(gps.IsStarted, "Yes", "No"), true);

                if (!Commands.LastCommand.Equals("elt") &&
                    !Commands.LastCommand.Equals("flt") &&
                    !Commands.LastCommand.Equals("wlt") &&
                    !Commands.LastCommand.Equals("elogtrack") &&
                    !Commands.LastCommand.Equals("flogtrack") &&
                    !Commands.LastCommand.Equals("wlogtrack") && 
                    gps.IsStarted)
                {
                    Debug.AddLog("UpdateData: Closing GPS.", true);
                
                    gps.Stop();
                    if (Commands.AnswerType != AnswerType.SMS)
                    {
                        Commands.GoogleMapsFileName = Google.GetMap(sr.Latitude, sr.Longitude);

                        if (Commands.GoogleMapsFileName == null)
                            Commands.GoogleMapsFileName = "";
                    }
                
                    Debug.AddLog("UpdateData: is GPS closed? " + Utils.iif(gps.IsStarted, "No", "Yes"), true);
                }
                
                ProcessCommand();
            }
            else
            {
                count++;
                Debug.AddLog("UpdateData. Count: " + System.Convert.ToString(count), true);

                if (!Commands.LastCommand.Equals("elt") &&
                    !Commands.LastCommand.Equals("flt") &&
                    !Commands.LastCommand.Equals("wlt") &&
                    !Commands.LastCommand.Equals("elogtrack") &&
                    !Commands.LastCommand.Equals("flogtrack") &&
                    !Commands.LastCommand.Equals("wlogtrack"))
                    VerifyIfHasToStopGPS();
            }
        }

        void VerifyIfHasToStopGPS()
        {
            Debug.AddLog("VerifyIfHasToStopGPS. count > interactions? " + Utils.iif(count > rtCommon.configuration.defaultGPSInteractions, "yes", "no"), true);
            if (count > rtCommon.configuration.defaultGPSInteractions)
            {
                if (gps.IsStarted)
                {
                    Debug.AddLog("VerifyIfHasToStopGPS: Closing GPS.", true);

                    gps.Stop();

                    Debug.AddLog("VerifyIfHasToStopGPS: is GPS closed? " + Utils.iif(gps.IsStarted, "No", "Yes"), true);
                }
                
                if (Commands.LastCommand.Equals("agp"))
                    sTextToSend = MontaMensagemParaATM(0, 0);
                else
                    sTextToSend = rtCommon.GetTextToSendFromNonGPS();

                lastLatitude = rtCommon.lastCellIDLatitude;
                lastLongitude = rtCommon.lastCellIDLongitude;

                sr.Clear();

                Debug.AddLog("VerifyIfHasToStopGPS. TextToSend: " + sTextToSend, true);
                Debug.AddLog("VerifyIfHasToStopGPS: is GPS opened? " + Utils.iif(gps.IsStarted, "Yes", "No"), true);

                ProcessCommand();
            }
        }
    }
}