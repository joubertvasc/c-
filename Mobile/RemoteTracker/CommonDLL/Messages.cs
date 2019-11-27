using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVUtils;
using JVGPS;

namespace CommonDLL
{
    public class Messages
    {
        // Messages
        public static string msg_PasswordDoesNotMatch = "Password does not match.";
        public static string msg_CouldNotConnectGPS = "Could not connect to satellites.";
        public static string msg_InvalidCommand = "Invalid command.";
        public static string msg_Latitude = "Lat";
        public static string msg_Longitude = "Lon";
        public static string msg_Satellite_count = "Satellite count";
        public static string msg_Battery = "Bat";
        public static string msg_Charging = "Charging";
        public static string msg_Time = "Time";
        public static string msg_Author = "Author";
        public static string msg_Contributors = "Contributors";
        public static string msg_Translators = "Translators";
        public static string msg_Ok = "Ok";
        public static string msg_Cancel = "cancel";
        public static string msg_Command_Started = "Command Started: ";
        public static string msg_Initial_Coordinates = "Initial coordinates";
        public static string msg_Final_Coordinates = "Final coordinates";
        public static string msg_Name = "Name";
        public static string msg_Company = "Company";
        public static string msg_EMail = "E-Mail";
        public static string msg_Telephone = "Telephone";
        public static string msg_Address = "Address";
        public static string msg_Notes = "Notes";
        public static string msg_Not_Possible_Owner_Info = "Not possible to get owner information";
        public static string msg_Sim_Contacts_Deleted = "All SIM contacts was deleted";
        public static string msg_kmz_Deleted = "All KMZ files was deleted";
        public static string msg_not_installed = "not installed";
        public static string msg_incoming_calls = "Incoming calls";
        public static string msg_outgoing_calls = "Outgoing calls";
        public static string msg_findme_call = "is asking for your GPS coordinates. Can RemoteTracker send this information?";
        public static string msg_donation = " Please Donate ";
        public static string msg_alarmsound = "Alarm sound:";
        public static string msg_search = "Search";
        public static string msg_selectfile = "Select File";
        public static string msg_playsound = "Play Sound";
        public static string msg_invalidvalue = "The value of field {0} is not valid.";
        public static string msg_CellID = "Cell ID";
        public static string msg_CellLAC = "Location Area Code";
        public static string msg_CellCC = "Country Code";
        public static string msg_CellNC = "Network Code";
        public static string msg_CoordFromOpenCellId = "Coordinates from OpenCellID";
        public static string msg_CoordFromGoogleMaps = "Coordinates from Google Maps";
        public static string msg_CoordFromCellDB = "Coordinates from CellDB";
        public static string msg_PasswordWithIllegalChar = "The password must have only numbers or letters.";
        public static string msg_GPSDriverNotFound = "GPS driver not found.";
        public static string msg_GPSDeviceNotFound = "GPS device not found.";
        public static string msg_DebugMode = "Debug mode";
        public static string msg_ConfigGPS = "Config GPS";
        public static string msg_ComPortNotSelected = "The serial port was not selected.";
        public static string msg_BaudRateNotSelected = "The baud rate was not selected.";
        public static string msg_SerialPort = "Serial port:";
        public static string msg_BaudRate = "Baud rate:";
        public static string msg_UseManagedGPS = "Use Windows Managed GPS";
        public static string msg_UseManualGPS = "Use Custom GPS";
        public static string msg_SelectGPSType = "Please select the GPS type you want to use:";
        public static string msg_sim_card_changed = "SIM card was changed";
        public static string msg_Success = "Success!";

        // Help variables
        public static string commands = "RemoteTrack command. send rt# followed by";
        public static string help_command = "help: send this message";
        public static string ehelp_command = "ehelp: send this message by e-mail";
        public static string fhelp_command = "fhelp: send this message to your FTP account";
        public static string cb_command = "cb: make a call to a number";
        public static string gp_command = "gp: send GPS coordinates";
        public static string egp_command = "egp: send GPS coordinates by e-mail";
        public static string fgp_command = "fgp: send GPS coordinates to your FTP account";
        public static string pb_command = "pb: send all contacts in SIM card";
        public static string epb_command = "epb: send all contacts in SIM card by e-mail";
        public static string fpb_command = "fpb: send all contacts in SIM card to your FTP account";
        public static string elt_command = "elt: log path and send a KML file by e-mail";
        public static string flt_command = "flt: log path and send a KML file to your FTP account";
        public static string go_command = "go: get owner information";
        public static string ego_command = "ego: get owner information and send by e-mail";
        public static string fgo_command = "fgo: get owner information and send to your FTP account";
        public static string rst_command = "rst: softreset device";
        public static string dsc_command = "dsc: delete SIM contacts";
        public static string dkz_command = "dkz: delete KMZ files";
        public static string ganfl_command = "ganfl: send a list of all incoming and outgoing calls if you have VITO audio notes installed";
        public static string eganfl_command = "eganfl: same as 'ganfl' but by e-mail";
        public static string fganfl_command = "fganfl: same as 'ganfl' but send to your FTP account";
        public static string eganf_command = "eganf: send all incoming and outgoing calls records if you have VITO audio notes installed";
        public static string fganf_command = "fganf: send to your FTP account all incoming and outgoing calls records if you have VITO audio notes installed";
        public static string findme_command = "findme: same as 'gp' command, but ask you if you want to answer the command";
        public static string alarm_command = "alarm: just play a sound. Useful when you don't remember where the device is, but you know it's your home";
        public static string gi_command = "gi: Get the device's IMEI number and the installed SIM card IMSI number";
        public static string egi_command = "egi: same as 'GI' command, but by e-mail";
        public static string fgi_command = "fgi: same as 'GI' command, and send to your FTP account";
        public static string msg_command = "msg: show a dialog box to user with a message";
        public static string gip_command = "gip: send the current IP address ";
        public static string egip_command = "egip: send the current IP address by e-mail";
        public static string fgip_command = "fgip: send the current IP address to your FTP account";
        public static string vnc_command = "vnc: start VNC server";
        public static string listapp_command = "listapp: list all installed applications";
        public static string elistapp_command = "elistapp: list all installed applications by e-mail";
        public static string flistapp_command = "flistapp: send a list of all installed applications to your FTP account";
        public static string runapp_command = "runapp: run a defined application";
        public static string cellid_command = "cellid: return informations from current cell tower";
        public static string ecellid_command = "ecellid: return informations from current cell tower by e-mail";
        public static string fcellid_command = "fcellid: return informations from current cell tower to your FTP account";
        public static string lock_command = "lock: lock your device";
        public static string unlock_command = "unlock: unlock your device";
        public static string callhist_command = "callhist: return the call history";
        public static string ecallhist_command = "ecallhist: return the call history by e-mail";
        public static string fcallhist_command = "fcallhist: return the call history to your FTP account";
        public static string ftp_command = "ftp: send files to a configured server";
        public static string mslist_command = "mslist: list the MortScript files found on device";
        public static string emslist_command = "emslist: list the MortScript files found on device by e-mail";
        public static string fmslist_command = "fmslist: send a list of all MortScript files found on device to your FTP account";
        public static string msrun_command = "msrun: run MortScript files found on device";
        public static string delcard_command = "delcard: delete all non protected files from storage card";
        public static string esms_command = "esms: send the full sms history by e-mail";
        public static string fsms_command = "fsms: send the full sms history by FTP";
        public static string eoutlook_command = "eoutlook: send appointments and contects from outlook by e-mail";
        public static string foutlook_command = "foutlook: send appointments and contects from outlook by FTP";
        public static string lostpass_command = "lostpass: send by SMS your secret question.";
        public static string secret_command = "secret: receive your secret answer, and if it is ok, send the password by SMS.";
        public static string rc_command = "rc: redirect calls to another phone";
        public static string cr_command = "cr: cancel the redirect command (rc)";
        public static string iehist_command = "iehist: return internet explorer history";
        public static string eiehist_command = "eiehist: return internet explorer history by e-mail";
        public static string fiehist_command = "fiehist: return internet explorer history by FTP";
        public static string df_command = "df: delete specific folder";

        // Languages
        public static string lang_english = "English";
        public static string lang_portuguesebrazil = "Portuguese";
        public static string lang_spanish = "Spanish";
        public static string lang_italian = "Italian";
        public static string lang_french = "French";
        public static string lang_german = "German";
        public static string lang_dutch = "Dutch";
        public static string lang_greek = "Greek";
        public static string lang_danish = "Danish";
        public static string lang_romanian = "Romanian";
        public static string lang_russian = "Russian";
        public static string lang_swedish = "Swedish";

        // NEW!
        public static string msg_CurrentIPAdrress = "Current IP Address";
        public static string msg_No_IP_Detected = "No IP address was detected";
        public static string msg_VNCStarted = "VNC Server started";
        public static string msg_VNCNotStarted = "VNC Server not started";
        public static string msg_AppStarted = "The application was started";
        public static string msg_AppNotFound = "The application {0} was not found";
        public static string msg_AppNotDefined = "The application was not defined";
        public static string msg_FilesInFolder = "Files in Start Menu folder";
        public static string msg_CommandLogEmpty = "The command log is empty";
        public static string msg_CommandLog = "Command Log";
        public static string msg_DeviceLocked = "The device was locked";
        public static string msg_DeviceUnLocked = "The device was unlocked";
        public static string msg_DeviceLockedByOwner = "This device was locked by its owner";
        public static string msg_StartTime = "Start time";
        public static string msg_EndTime = "End time";
        public static string msg_Duration = "Duration";
        public static string msg_CallerName = "Caller name";
        public static string msg_CallerNumber = "Caller number";
        public static string msg_OutgoingCall = "Outgoing call";
        public static string msg_Yes = "Yes";
        public static string msg_No = "No";
        public static string msg_Error = "Error";
        public static string msg_Warning = "Warning";
        public static string msg_Confirmation = "Confirmation";
        public static string msg_ConfirmSIMDeletion = "This SIM card will be excluded. Do you confirm?";
        public static string msg_InvalidIMSI = "You must detect a valid IMSI number.";
        public static string msg_ConfirmEmergencyDeletion = "This emergency registry will be excluded. Do you confirm?";
        public static string msg_InvalidEmergency = "The field Emergency is mandatory.";
        public static string msg_EmergencyNumber = "Emergency Number";
        public static string msg_EmergencyEMail = "Emergency E-Mail";
        public static string msg_CurrentPasswordDoesNotMatch = "The current password does not match";
        public static string msg_FTPServerNotDefined = "FTP Server was not defined.";
        public static string msg_NoFilesToUpload = "No files to upload.";
        public static string msg_FileDoesNotExists = "File {0} does not exists.";
        public static string msg_FilesUploaded = "{0} files was successfully uploaded.";
        public static string msg_NoFilesUploaded = "No files to upload.";
        public static string msg_HideRT = "Do you really want to hide RemoteTracker in your device? This operation can't be undone.";
        public static string msg_HideRTRestart = "To complete operation your device needs to restart.";
        public static string msg_MortScriptFiles = "MortScript files found";
        public static string msg_MortScriptRunSuccessfully = "Scripts executed successfully";
        public static string msg_MortScriptRunUnsuccessfully = "Scripts not executed";

        public static string msg_HelpEmergencyNumber = "Type a cellular phone number to be used if your SIM card was changed. This phone will receive a SMS with useful informations to find your device.";
        public static string msg_HelpEmergencyEMail = "Type an e-mail address to be used if your SIM card was changed. This e-mail will receive a message with useful informations to find your device.";
        public static string msg_HelpIMSI = "Configuring known Sim cards you will be able to change them without receive SMS alerting you about this exchange.";
        public static string msg_HelpPassword = "Define a password is a good idea to keep RemoteTracker safer.";
        public static string msg_HelpGPSAttempts = "Define the number of attempts to try to get the GPS signal for GPS commands (except ELT).";
        public static string msg_HelpELTTimeout = "Define the time in minutes for ELT command to log a track.";
        public static string msg_HelpEMail = "Setting an e-mail account you will be able to use several commands that reply by e-mail to the address specified here.";
        public static string msg_HelpFTP = "Configuring a personal FTP server you will be able to use commands to send files to you if you lost your device.";
        public static string msg_HelpEmergencyMessage = "This message will be sent to your emergency list if you need the TopSecret application.";
        public static string msg_DefaultEmergencyMessage = "This is a message sent by RemoteTracker to inform the person below is requesting urgent help!";

        public static string msg_CallLogIsEmpty = "The call log is empty";
        public static string msg_SecretQuestion = "Please type your secret question.";
        public static string msg_SecretAnswer = "Please type your secret answer.";

        public static string msg_Password = "Password";
        public static string msg_HelpConfigWeb = "Creating a WEB account you will be able to keep your emergency informations  (phone numbers, known SIM cards and emergency e-mails address) to be used after a hardreset.";
        public static string msg_ErrorCreatingWebAccount = "Could not create a new account. Please verify if you are already registered or your internet connection.";
        public static string msg_SendConfiguration = "This option will send your emergency informations. to your account on the web and it will need an internet connection. Do you agree?";
        public static string msg_WebLoginError = "Could not login to web server. Please verify your account information and your internet connection.";
        public static string msg_WebSendConfigError = "Could not send your configurations to web server. Please verify your internet connection.";
        public static string msg_WebSendConfigSuccess = "Your configurations was successfully sent to the web server.";

        // Meedios interface
        public static string meedios_UndefinedError = "Undefined error.";
        public static string meedios_OtherException = "Undefined exception.";
        public static string meedios_WrongUser = "Wrong user - a different user owns your IMEI.";
        public static string meedios_WrongPass = "Wrong pass - enter correct password from signup.";
        public static string meedios_InvalidUser = "Invalid user - you were trying to update someone elses IMEI without even having an account of which you specified.";
        public static string meedios_FailedGeneratedAccound = "Failed to generate new account - a server error has occurred.";
        public static string meedios_FailedStoreIMEI = "Failed to store IMEI - a server error has occurred.";
        public static string meedios_InvalidAPIKey = "Invalid API key - this application is not authorized to use this database.";

        // Help commands
        public static string Help(bool isATM)
        {
            return commands + ":\n " +
                   help_command + ";\n " +
                   ehelp_command + ";\n " +
                   fhelp_command + ";\n " +
                   cb_command + ";\n " +
                   gp_command + ";\n " +
                   egp_command + ";\n " +
                   fgp_command + ";\n " +
                   pb_command + ";\n " +
                   epb_command + ";\n " +
                   fpb_command + ";\n " +
                   elt_command + ";\n " +
                   flt_command + ";\n " +
                   go_command + ";\n " +
                   ego_command + ";\n " +
                   fgo_command + ";\n " +
                   rst_command + ";\n " +
                   dsc_command + ";\n " +
                   dkz_command + ";\n " +
                   ganfl_command + ";\n " +
                   eganfl_command + ";\n " +
                   fganfl_command + ";\n " +
                   eganf_command + ";\n " +
                   fganf_command + ";\n " +
                   findme_command + ";\n" +
                   alarm_command + ";\n " +
                   gi_command + ";\n" +
                   egi_command + ";\n" +
                   fgi_command + ";\n" +
                   gip_command + ";\n" +
                   egip_command + ";\n" +
                   fgip_command + ";\n" +
                   msg_command + ";\n" +
                   vnc_command + ";\n" +
                   listapp_command + ";\n" +
                   elistapp_command + ";\n" +
                   flistapp_command + ";\n" +
                   runapp_command + ";\n" +
                   cellid_command + ";\n" +
                   ecellid_command + ";\n" +
                   fcellid_command + ";\n" +
                   lock_command + ";\n" +
                   unlock_command + ";\n" +
                   callhist_command + ";\n" +
                   ecallhist_command + ";\n" +
                   fcallhist_command + ";\n" +
                   mslist_command + ";\n" +
                   emslist_command + ";\n" +
                   fmslist_command + ";\n" +
                   msrun_command + ";\n" +
                   ftp_command + ";\n" +
                   delcard_command + ";\n" +
                   esms_command + ";\n" +
                   fsms_command + ";\n" +
                   eoutlook_command + ";\n" +
                   foutlook_command + ";\n" +
                   lostpass_command + ";\n" +
                   secret_command + ";\n" +
                   rc_command + ";\n" +
                   cr_command + ";\n" +
                   iehist_command + ";\n" +
                   eiehist_command + ";\n" +
                   fiehist_command + ";\n" +
                   df_command + ";\n" +
                   (!isATM ? "" : "AGP: retorna posição atual do aparelho no formado do aplicativo ATM;\n" +
                                  "AFTP/BFTP: envia ao servidor de FTP os arquivo gravados em determinada pasta;\n" +
                                  "ADA: Habilita aviso diário;\n" +
                                  "ADD: Desabilita aviso diário;\n" + 
                                  "RS0: Desliga o Rastreamento de Servidor;\n" +
                                  "RS1: Liga o Rastreamento de Servidor;\n");
        }

        public static void ChangeLanguage(LanguageXML languageXML)
        {
            msg_PasswordDoesNotMatch = languageXML.getColumn("msg_PasswordDoesNotMatch", msg_PasswordDoesNotMatch);
            msg_CouldNotConnectGPS = languageXML.getColumn("msg_CouldNotConnectGPS", msg_CouldNotConnectGPS);
            msg_InvalidCommand = languageXML.getColumn("msg_InvalidCommand", msg_InvalidCommand);
            msg_Latitude = languageXML.getColumn("msg_Latitude", msg_Latitude);
            msg_Longitude = languageXML.getColumn("msg_Longitude", msg_Longitude);
            msg_Satellite_count = languageXML.getColumn("msg_Satellite_count", msg_Satellite_count);
            msg_Battery = languageXML.getColumn("msg_Battery", msg_Battery);
            msg_Charging = languageXML.getColumn("msg_Charging", msg_Charging);
            msg_Time = languageXML.getColumn("msg_Time", msg_Time);
            msg_Author = languageXML.getColumn("msg_Author", msg_Author);
            msg_Contributors = languageXML.getColumn("msg_Contributors", msg_Contributors);
            msg_Translators = languageXML.getColumn("msg_Translators", msg_Translators);
            msg_Command_Started = languageXML.getColumn("msg_Command_Started", msg_Command_Started);
            msg_Initial_Coordinates = languageXML.getColumn("msg_Initial_Coordinates", msg_Initial_Coordinates);
            msg_Final_Coordinates = languageXML.getColumn("msg_Final_Coordinates", msg_Final_Coordinates);
            msg_Name = languageXML.getColumn("msg_Name", msg_Name);
            msg_Company = languageXML.getColumn("msg_Company", msg_Company);
            msg_EMail = languageXML.getColumn("msg_EMail", msg_EMail);
            msg_Telephone = languageXML.getColumn("msg_Telephone", msg_Telephone);
            msg_Address = languageXML.getColumn("msg_Address", msg_Address);
            msg_Notes = languageXML.getColumn("msg_Notes", msg_Notes);
            msg_Not_Possible_Owner_Info = languageXML.getColumn("msg_Not_Possible_Owner_Info", msg_Not_Possible_Owner_Info);
            msg_Sim_Contacts_Deleted = languageXML.getColumn("msg_Sim_Contacts_Deleted", msg_Sim_Contacts_Deleted);
            msg_kmz_Deleted = languageXML.getColumn("msg_kmz_Deleted", msg_kmz_Deleted);
            msg_not_installed = languageXML.getColumn("msg_not_installed", msg_not_installed);
            msg_incoming_calls = languageXML.getColumn("msg_incoming_calls", msg_incoming_calls);
            msg_outgoing_calls = languageXML.getColumn("msg_outgoing_calls", msg_outgoing_calls);
            msg_findme_call = languageXML.getColumn("msg_findme_call", msg_findme_call);
            msg_donation = languageXML.getColumn("msg_donation", msg_donation);
            msg_alarmsound = languageXML.getColumn("msg_alarmsound", msg_alarmsound);
            msg_search = languageXML.getColumn("msg_search", msg_search);
            msg_selectfile = languageXML.getColumn("msg_selectfile", msg_selectfile);
            msg_playsound = languageXML.getColumn("msg_playsound", msg_playsound);
            msg_Cancel = languageXML.getColumn("msg_Cancel", msg_Cancel);
            msg_Ok = languageXML.getColumn("msg_Ok", msg_Ok);
            msg_invalidvalue = languageXML.getColumn("msg_invalidvalue", msg_invalidvalue);
            msg_CellID = languageXML.getColumn("msg_CellID", msg_CellID);
            msg_CellLAC = languageXML.getColumn("msg_CellLAC", msg_CellLAC);
            msg_CellCC = languageXML.getColumn("msg_CellCC", msg_CellCC);
            msg_CellNC = languageXML.getColumn("msg_CellNC", msg_CellNC);
            msg_CoordFromOpenCellId = languageXML.getColumn("msg_CoordFromOpenCellId", msg_CoordFromOpenCellId);
            msg_CoordFromGoogleMaps = languageXML.getColumn("msg_CoordFromGoogleMaps", msg_CoordFromGoogleMaps);
            msg_CoordFromCellDB = languageXML.getColumn("msg_CoordFromCellDB", msg_CoordFromCellDB);
            msg_PasswordWithIllegalChar = languageXML.getColumn("msg_PasswordWithIllegalChar", msg_PasswordWithIllegalChar);
            msg_GPSDriverNotFound = languageXML.getColumn("msg_GPSDriverNotFound", msg_GPSDriverNotFound);
            msg_GPSDeviceNotFound = languageXML.getColumn("msg_GPSDeviceNotFound", msg_GPSDeviceNotFound);
            msg_DebugMode = languageXML.getColumn("msg_DebugMode", msg_DebugMode);
            msg_ConfigGPS = languageXML.getColumn("msg_ConfigGPS", msg_ConfigGPS);
            msg_ComPortNotSelected = languageXML.getColumn("msg_ComPortNotSelected", msg_ComPortNotSelected);
            msg_BaudRateNotSelected = languageXML.getColumn("msg_BaudRateNotSelected", msg_BaudRateNotSelected);
            msg_SerialPort = languageXML.getColumn("msg_SerialPort", msg_SerialPort);
            msg_BaudRate = languageXML.getColumn("msg_BaudRate", msg_BaudRate);
            msg_UseManagedGPS = languageXML.getColumn("msg_UseManagedGPS", msg_UseManagedGPS);
            msg_UseManualGPS = languageXML.getColumn("msg_UseManualGPS", msg_UseManualGPS);
            msg_SelectGPSType = languageXML.getColumn("msg_SelectGPSType", msg_SelectGPSType);
            msg_CurrentIPAdrress = languageXML.getColumn("msg_CurrentIPAdrress", msg_CurrentIPAdrress);
            msg_No_IP_Detected = languageXML.getColumn("msg_No_IP_Detected", msg_No_IP_Detected);
            msg_VNCStarted = languageXML.getColumn("msg_VNCStarted", msg_VNCStarted);
            msg_VNCNotStarted = languageXML.getColumn("msg_VNCNotStarted", msg_VNCNotStarted);
            msg_AppStarted = languageXML.getColumn("msg_AppStarted", msg_AppStarted);
            msg_AppNotFound = languageXML.getColumn("msg_AppNotFound", msg_AppNotFound);
            msg_AppNotDefined = languageXML.getColumn("msg_AppNotDefined", msg_AppNotDefined);
            msg_FilesInFolder = languageXML.getColumn("msg_FilesInFolder", msg_FilesInFolder);
            msg_CommandLogEmpty = languageXML.getColumn("msg_CommandLogEmpty", msg_CommandLogEmpty);
            msg_CommandLog = languageXML.getColumn("msg_CommandLog", msg_CommandLog);
            msg_DeviceLocked = languageXML.getColumn("msg_DeviceLocked", msg_DeviceLocked);
            msg_DeviceUnLocked = languageXML.getColumn("msg_DeviceUnLocked", msg_DeviceUnLocked);
            msg_DeviceLockedByOwner = languageXML.getColumn("msg_DeviceLockedByOwner", msg_DeviceLockedByOwner);
            msg_StartTime = languageXML.getColumn("msg_StartTime", msg_StartTime);
            msg_EndTime = languageXML.getColumn("msg_EndTime", msg_EndTime);
            msg_Duration = languageXML.getColumn("msg_Duration", msg_Duration);
            msg_CallerName = languageXML.getColumn("msg_CallerName", msg_CallerName);
            msg_CallerNumber = languageXML.getColumn("msg_CallerNumber", msg_CallerNumber);
            msg_OutgoingCall = languageXML.getColumn("msg_OutgoingCall", msg_OutgoingCall);
            msg_Yes = languageXML.getColumn("msg_Yes", msg_Yes);
            msg_No = languageXML.getColumn("msg_No", msg_No);
            msg_Error = languageXML.getColumn("msg_Error", msg_Error);
            msg_Warning = languageXML.getColumn("msg_Warning", msg_Warning);
            msg_Confirmation = languageXML.getColumn("msg_Confirmation", msg_Confirmation);
            msg_ConfirmSIMDeletion = languageXML.getColumn("msg_ConfirmSIMDeletion", msg_ConfirmSIMDeletion);
            msg_InvalidIMSI = languageXML.getColumn("msg_InvalidIMSI", msg_InvalidIMSI);
            msg_ConfirmEmergencyDeletion = languageXML.getColumn("msg_ConfirmEmergencyDeletion", msg_ConfirmEmergencyDeletion);
            msg_InvalidEmergency = languageXML.getColumn("msg_InvalidEmergency", msg_InvalidEmergency);
            msg_EmergencyNumber = languageXML.getColumn("msg_EmergencyNumber", msg_EmergencyNumber);
            msg_EmergencyEMail = languageXML.getColumn("msg_EmergencyEMail", msg_EmergencyEMail);
            msg_CurrentPasswordDoesNotMatch = languageXML.getColumn("msg_CurrentPasswordDoesNotMatch", msg_CurrentPasswordDoesNotMatch);
            msg_FTPServerNotDefined = languageXML.getColumn("msg_FTPServerNotDefined", msg_FTPServerNotDefined);
            msg_NoFilesToUpload = languageXML.getColumn("msg_NoFilesToUpload", msg_NoFilesToUpload);
            msg_FileDoesNotExists = languageXML.getColumn("msg_FileDoesNotExists", msg_FileDoesNotExists);
            msg_FilesUploaded = languageXML.getColumn("msg_FilesUploaded", msg_FilesUploaded);
            msg_NoFilesUploaded = languageXML.getColumn("msg_NoFilesUploaded", msg_NoFilesUploaded);
            msg_HideRT = languageXML.getColumn("msg_HideRT", msg_HideRT);
            msg_HideRTRestart = languageXML.getColumn("msg_HideRTRestart", msg_HideRTRestart);
            msg_HelpEmergencyNumber = languageXML.getColumn("msg_HelpEmergencyNumber", msg_HelpEmergencyNumber);
            msg_HelpEmergencyEMail = languageXML.getColumn("msg_HelpEmergencyEMail", msg_HelpEmergencyEMail);
            msg_HelpIMSI = languageXML.getColumn("msg_HelpIMSI", msg_HelpIMSI);
            msg_HelpPassword = languageXML.getColumn("msg_HelpPassword", msg_HelpPassword);
            msg_HelpFTP = languageXML.getColumn("msg_HelpFTP", msg_HelpFTP);
            msg_HelpGPSAttempts = languageXML.getColumn("msg_HelpGPSAttempts", msg_HelpGPSAttempts);
            msg_HelpELTTimeout = languageXML.getColumn("msg_HelpELTTimeout", msg_HelpELTTimeout);
            msg_HelpEMail = languageXML.getColumn("msg_HelpEMail", msg_HelpEMail);
            msg_MortScriptFiles = languageXML.getColumn("msg_MortScriptFiles", msg_MortScriptFiles);
            msg_MortScriptRunSuccessfully = languageXML.getColumn("msg_MortScriptRunSuccessfully", msg_MortScriptRunSuccessfully);
            msg_MortScriptRunUnsuccessfully = languageXML.getColumn("msg_MortScriptRunUnsuccessfully", msg_MortScriptRunUnsuccessfully);
            msg_HelpEmergencyMessage = languageXML.getColumn("msg_HelpEmergencyMessage", msg_HelpEmergencyMessage);
            msg_DefaultEmergencyMessage = languageXML.getColumn("msg_DefaultEmergencyMessage", msg_DefaultEmergencyMessage);
            msg_CallLogIsEmpty = languageXML.getColumn("msg_CallLogIsEmpty", msg_CallLogIsEmpty);
            msg_SecretQuestion = languageXML.getColumn("msg_SecretQuestion", msg_SecretQuestion);
            msg_SecretAnswer = languageXML.getColumn("msg_SecretAnswer", msg_SecretAnswer);
            msg_Password = languageXML.getColumn("msg_Password", msg_Password);
            msg_HelpConfigWeb = languageXML.getColumn("msg_HelpConfigWeb", msg_HelpConfigWeb);
            msg_ErrorCreatingWebAccount = languageXML.getColumn("msg_ErrorCreatingWebAccount", msg_ErrorCreatingWebAccount);
            msg_SendConfiguration = languageXML.getColumn("msg_SendConfiguration", msg_SendConfiguration);
            msg_WebLoginError = languageXML.getColumn("msg_WebLoginError", msg_WebLoginError);
            msg_WebSendConfigError = languageXML.getColumn("msg_WebSendConfigError", msg_WebSendConfigError);
            msg_WebSendConfigSuccess = languageXML.getColumn("msg_WebSendConfigSuccess", msg_WebSendConfigSuccess);
            msg_Success = languageXML.getColumn("msg_success", msg_Success);

            // Languages
            lang_english = languageXML.getColumn("lang_english", lang_english);
            lang_portuguesebrazil = languageXML.getColumn("lang_portuguesebrazil", lang_portuguesebrazil);
            lang_spanish = languageXML.getColumn("lang_spanish", lang_spanish);
            lang_italian = languageXML.getColumn("lang_italian", lang_italian);
            lang_french = languageXML.getColumn("lang_french", lang_french);
            lang_german = languageXML.getColumn("lang_german", lang_german);
            lang_dutch = languageXML.getColumn("lang_dutch", lang_dutch);
            lang_greek = languageXML.getColumn("lang_greek", lang_greek);
            lang_danish = languageXML.getColumn("lang_danish", lang_danish);
            lang_romanian = languageXML.getColumn("lang_romanian", lang_romanian);
            lang_russian = languageXML.getColumn("lang_russian", lang_russian);
            lang_swedish = languageXML.getColumn("lang_swedish", lang_swedish);

            // help
            commands = languageXML.getColumn("commands", commands);
            help_command = languageXML.getColumn("help_command", help_command);
            ehelp_command = languageXML.getColumn("ehelp_command", ehelp_command);
            fhelp_command = languageXML.getColumn("fhelp_command", fhelp_command);
            cb_command = languageXML.getColumn("cb_command", cb_command);
            gp_command = languageXML.getColumn("gp_command", gp_command);
            egp_command = languageXML.getColumn("egp_command", egp_command);
            fgp_command = languageXML.getColumn("fgp_command", fgp_command);
            pb_command = languageXML.getColumn("pb_command", pb_command);
            epb_command = languageXML.getColumn("epb_command", epb_command);
            fpb_command = languageXML.getColumn("fpb_command", fpb_command);
            elt_command = languageXML.getColumn("elt_command", elt_command);
            flt_command = languageXML.getColumn("flt_command", flt_command);
            go_command = languageXML.getColumn("go_command", go_command);
            ego_command = languageXML.getColumn("ego_command", ego_command);
            fgo_command = languageXML.getColumn("fgo_command", fgo_command);
            rst_command = languageXML.getColumn("rst_command", rst_command);
            dsc_command = languageXML.getColumn("dsc_command", dsc_command);
            dkz_command = languageXML.getColumn("dkz_command", dkz_command);
            ganfl_command = languageXML.getColumn("ganfl_command", ganfl_command);
            eganfl_command = languageXML.getColumn("eganfl_command", eganfl_command);
            fganfl_command = languageXML.getColumn("fganfl_command", fganfl_command);
            eganf_command = languageXML.getColumn("eganf_command", eganf_command);
            fganf_command = languageXML.getColumn("fganf_command", fganf_command);
            findme_command = languageXML.getColumn("findme_command", findme_command);
            alarm_command = languageXML.getColumn("alarm_command", alarm_command);
            gi_command = languageXML.getColumn("gi_command", gi_command);
            egi_command = languageXML.getColumn("egi_command", egi_command);
            fgi_command = languageXML.getColumn("fgi_command", fgi_command);
            msg_command = languageXML.getColumn("msg_command", msg_command);
            gip_command = languageXML.getColumn("gip_command", gip_command);
            egip_command = languageXML.getColumn("egip_command", egip_command);
            fgip_command = languageXML.getColumn("fgip_command", fgip_command);
            vnc_command = languageXML.getColumn("vnc_command", vnc_command);
            listapp_command = languageXML.getColumn("listapp_command", listapp_command);
            elistapp_command = languageXML.getColumn("elistapp_command", elistapp_command);
            flistapp_command = languageXML.getColumn("flistapp_command", flistapp_command);
            runapp_command = languageXML.getColumn("runapp_command", runapp_command);
            cellid_command = languageXML.getColumn("cellid_command", cellid_command);
            ecellid_command = languageXML.getColumn("ecellid_command", ecellid_command);
            fcellid_command = languageXML.getColumn("fcellid_command", fcellid_command);
            lock_command = languageXML.getColumn("lock_command", lock_command);
            unlock_command = languageXML.getColumn("unlock_command", unlock_command);
            callhist_command = languageXML.getColumn("callhist_command", callhist_command);
            ecallhist_command = languageXML.getColumn("ecallhist_command", ecallhist_command);
            fcallhist_command = languageXML.getColumn("fcallhist_command", fcallhist_command);
            ftp_command = languageXML.getColumn("ftp_command", ftp_command);
            mslist_command = languageXML.getColumn("mslist_command", mslist_command);
            emslist_command = languageXML.getColumn("emslist_command", emslist_command);
            fmslist_command = languageXML.getColumn("fmslist_command", fmslist_command);
            msrun_command = languageXML.getColumn("msrun_command", msrun_command);
            delcard_command = languageXML.getColumn("delcard_command", delcard_command);
            esms_command = languageXML.getColumn("esms_command", esms_command);
            fsms_command = languageXML.getColumn("fsms_command", fsms_command);
            eoutlook_command = languageXML.getColumn("eoutlook_command", eoutlook_command);
            foutlook_command = languageXML.getColumn("foutlook_command", foutlook_command);
            lostpass_command = languageXML.getColumn("lostpass_command", lostpass_command);
            secret_command = languageXML.getColumn("secret_command", secret_command);

            // Medios interface
            meedios_UndefinedError = languageXML.getColumn("meedios_UndefinedError", meedios_UndefinedError);
            meedios_OtherException = languageXML.getColumn("meedios_OtherException", meedios_OtherException);
            meedios_WrongUser = languageXML.getColumn("meedios_WrongUser", meedios_WrongUser);
            meedios_WrongPass = languageXML.getColumn("meedios_WrongPass", meedios_WrongPass);
            meedios_InvalidUser = languageXML.getColumn("meedios_InvalidUser", meedios_InvalidUser);
            meedios_FailedGeneratedAccound = languageXML.getColumn("meedios_FailedGeneratedAccound", meedios_FailedGeneratedAccound);
            meedios_FailedStoreIMEI = languageXML.getColumn("meedios_FailedStoreIMEI", meedios_FailedStoreIMEI);
            meedios_InvalidAPIKey = languageXML.getColumn("meedios_InvalidAPIKey", meedios_InvalidAPIKey);
        }

        public static string GetCellIdMessage(string mnc, string mcc, string cellID, string lac)
        {
            return msg_CellID + ": " + cellID + ", " +
                   msg_CellLAC + ": " + lac + "\n" +
                   msg_CellNC + ": " + mnc + ", " +
                   msg_CellCC + ": " + mcc;
        }

        public static string GetShortCellIdMessage(string mnc, string mcc, string cellID, string lac)
        {
            return "CellID " + cellID + " LAC " + lac + " MNC " + mnc + " MCC " + mcc;
        }

        public static string MessageToSend(AnswerType answerType, SatelliteRecord sr)
        {
            CellIDInformations cid = OpenCellID.RefreshData();

            if (answerType != AnswerType.SMS)
            {
                return GetCellIdMessage(cid.mobileNetworkCode, cid.mobileCountryCode,
                                        cid.cellID, cid.localAreaCode) + "\n" +
                       msg_Latitude + ": " + System.Convert.ToString(sr.Latitude) + "\n" +
                       (!sr.LatitudeDMS.Equals("") ? " (" + sr.LatitudeDMS + ")" : "") + "\n" +
                       msg_Longitude + ": " + System.Convert.ToString(sr.Longitude) + "\n" +
                       (!sr.LongitudeDMS.Equals("") ? " (" + System.Convert.ToString(sr.LongitudeDMS) + ")" : "") + "\n" +
                       msg_Satellite_count + ": " + System.Convert.ToString(sr.SatellitesCount) + "\n" +
                       msg_Time + ": " + sr.Time.ToLongDateString() + "\n" +
                       Google.GoogleMapsLink(sr.Latitude, sr.Longitude) + "\n";
            }
            else
            {
                return msg_Latitude + ": " +
                       Utils.ChangeChar(System.Convert.ToString(sr.ShortLatitude), ',', '.') +
                         (!sr.LatitudeDMS.Equals("") ? " (" + sr.LatitudeDMS + ")" : "") + "\n" +
                       msg_Longitude + ": " +
                       Utils.ChangeChar(System.Convert.ToString(sr.ShortLongitude), ',', '.') +
                         (!sr.LongitudeDMS.Equals("") ? " (" + System.Convert.ToString(sr.LongitudeDMS) + ")" : "") + "\n" +
                       Google.GoogleMapsLink(sr.ShortLatitude, sr.ShortLongitude) + "\n";
            }
        }
    }
}
