using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;
using Microsoft.WindowsMobile.Telephony;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;
using JVUtils;
using JVGPS;

namespace CommonDLL
{
    public class Answer
    {
        static GPS gps;
        static bool bCharging = false;
        static string batteryLevel = "";
        static Configuration configuration;
        static bool doNotAddBatteryMetter = false;

        public static GPS GPS
        {
            get { return gps; }
            set { gps = value; }
        }
        public static bool Charging
        {
            get { return bCharging; }
            set { bCharging = value; }
        }
        public static string BatteryLevel
        {
            get { return batteryLevel; }
            set { batteryLevel = value; }
        }
        public static Configuration Configurations
        {
            get { return configuration; }
            set { configuration = value; }
        }
        public static bool DoNotAddBatteryMetter
        {
            get { return doNotAddBatteryMetter; }
            set { doNotAddBatteryMetter = value; }
        }
        
        // Send the message By SMS or EMAIL depending on command used by caller
        public static void SendAnswer(string telSMS, string messageToSend, AnswerType answerType, bool bExitAfterSend, bool bFake)
        {
            SendAnswer(telSMS, messageToSend, new string[0], answerType, bExitAfterSend, bFake);
        }

        public static void SendAnswer(string telSMS, string messageToSend, string[] Attachs, AnswerType answerType, bool bExitAfterSend, bool bFake)
        {
            Debug.AddLog("SendAnswer: Tel/E-Mail=" + telSMS + " Text=" + messageToSend +
                " Answer type=" + (answerType == AnswerType.SMS ? "SMS" : (answerType == AnswerType.EMAIL ? "EMAIL" : 
                (answerType == AnswerType.FTP ? "FTP" : "WEB"))) +
                " Fake? " + (bFake ? "Yes" : "No"), true);

            if (answerType == AnswerType.SMS)
                SendSMSMessage(telSMS, messageToSend, bExitAfterSend, bFake);
            else if (answerType == AnswerType.EMAIL)
                SendEMail(telSMS, messageToSend, Attachs, bExitAfterSend, bFake);
            else if (answerType == AnswerType.FTP)
                SendFTP(telSMS, messageToSend, Attachs, bExitAfterSend, bFake);
            else if (answerType == AnswerType.WEB)
                SendWEB(telSMS, messageToSend, Attachs, bExitAfterSend, bFake);
        }

/*        private static void SendBitMessage(string telSMS, string chunk)
        {
            SmsMessage s = new SmsMessage();
            Recipient r = new Recipient("RemoteTracker", telSMS);
            s.To.Add(r);

            s.Body = chunk;
            s.RequestDeliveryReport = false;
            try
            {
                Debug.AddLog("SendBitMessage: Tel/E-Mail=" + telSMS + " Text=" + chunk , true);

                s.Send();
            } catch (Exception ex)
            {
                Debug.AddLog("SendBitMessage: Error while sending message: " + ex.ToString(), true);
            }
        }

        public static void SendSMSMessage(string telSMS, string textSMS, bool bExitAfterSend, bool bFake)
        {
            if (GPS != null)
            {
                if (GPS.IsStarted)
                {
                    GPS.Stop();
                }

                if (!DoNotAddBatteryMetter)
                    textSMS += (Charging ?
                                " " + Messages.msg_Battery + " " + Messages.msg_Charging + "." :
                               (BatteryLevel != null && !BatteryLevel.Equals("") ?
                                " " + Messages.msg_Battery + ": " + BatteryLevel : ""));
            }

            Debug.AddLog("SendSMSMessage: Tel/E-Mail=" + telSMS + " Text=" + textSMS +
                " Fake? " + (bFake ? "Yes" : "No"), true);

            if (bFake)
            {
                if (configuration.ScreenOff)
                    Power.PowerOnOff(true);

                MessageBox.Show(textSMS, "RT Result");
            }
            else if (telSMS != null && !telSMS.Equals(""))
            {
                do
                {
                    if (textSMS.Length > 160)
                    {
                        SendBitMessage(telSMS, textSMS.Substring(0, 160));
                        textSMS = textSMS.Substring(160);
                    }
                    else
                    {
                        SendBitMessage(telSMS, textSMS);
                        textSMS = "";
                    }
                } while (textSMS != "");
            }

            // Ok, let's exit the application
            if (bExitAfterSend)
            {
                Application.Exit();
            }
        } /**/

        public static void SendSMSMessage(string telSMS, string textSMS, bool bExitAfterSend, bool bFake)
        {
            if (GPS != null)
            {
                if (GPS.IsStarted)
                {
                    GPS.Stop();
                }
            }

            if (!DoNotAddBatteryMetter)
                textSMS += (Charging ?
                            " " + Messages.msg_Battery + " " + Messages.msg_Charging + "." :
                           (BatteryLevel != null && !BatteryLevel.Equals("") ?
                            " " + Messages.msg_Battery + ": " + BatteryLevel : ""));

            Debug.AddLog("SendSMSMessage: Tel/E-Mail=" + telSMS + " Text=" + textSMS + " Fake? " + (bFake ? "Yes" : "No"), true);

            if (bFake)
            {
                if (configuration.ScreenOff)
                    Power.PowerOnOff(true);

                MessageBox.Show(textSMS, "RT Result");
            }
            else if (telSMS != null && !telSMS.Equals(""))
            {
                SmsMessage s = new SmsMessage();
                Recipient r = new Recipient("RemoteTracker", telSMS);
                s.To.Add(r);

                s.Body = textSMS;
                s.RequestDeliveryReport = false;
                try
                {
                    Debug.AddLog("SendSMSMessage: Tel/E-Mail=" + telSMS + " Text=" + textSMS, true);

                    s.Send();
                }
                catch (Exception ex)
                {
                    Debug.AddLog("SendSMSMessage: Error while sending message: " + ex.ToString(), true);
                }
            }

            // Ok, let's exit the application
            if (bExitAfterSend)
            {
                Application.Exit();
            }
        }

        public static void SendEMail(string email, string message, string[] attachs, bool bExitAfterSend, bool bFake)
        {
            if (GPS != null)
            {
                if (GPS.IsStarted)
                {
                    GPS.Stop();
                }

                if (!DoNotAddBatteryMetter)
                    message += "\n " + (Charging ? " " + Messages.msg_Battery + " " + Messages.msg_Charging + "." :
                        (BatteryLevel != "" ? " " + Messages.msg_Battery + ": " + BatteryLevel : ""));
            }

            if (!bFake)
            {
                if (!email.Trim().Equals(""))
                {
                    if (!Configurations.defaultEMailAccount.Equals(""))
                    {
                        EmailAccount account = (new OutlookSession()).EmailAccounts[Configurations.defaultEMailAccount];
                        if (account != null)
                        {
                            EmailMessage msg = new EmailMessage();

                            if (msg != null)
                            {
                                if (Commands.rtCommon.atm)
                                {
                                    OwnerRecord or = OwnerInfo.GetOwnerRecord();
                                    msg.Subject = or.Company;
                                }
                                else
                                {
                                    msg.Subject = Configurations.defaultSubject;
                                }

                                if (email.ToLower().Equals(Configurations.defaultrecipientEMail.ToLower()))
                                {
                                    msg.To.Add(new Recipient(Configurations.defaultrecipientName,
                                                             Configurations.defaultrecipientEMail));
                                }
                                else
                                {
                                    char[] delimiterChars = { ',', ';', ' ' };
                                    string[] people = email.Split(delimiterChars);

                                    foreach (string person in people)
                                        if (!person.Trim().Equals(""))
                                            msg.To.Add(new Recipient(person.Trim()));
                                }

                                if (attachs != null)
                                {
                                    foreach (string attach in attachs)
                                    {
                                        if (attach != "" && File.Exists(attach))
                                        {
                                            Attachment a = new Attachment(attach);

                                            msg.Attachments.Add(a);
                                        }
                                    }
                                }

                                msg.BodyText = message;
                                msg.Importance = Importance.High;

                                try
                                {
                                    msg.Send(account);

                                    MessagingApplication.Synchronize(account);
                                }
                                catch (Exception e)
                                {
                                    Debug.AddLog("SendEMail: error while sending email. Original message: " + e.Message, true);
                                }
                                
//                                Utils.ShowTodayScreen (3);
                            }
                            else
                            {
                                Debug.AddLog("SendEMail: msg could not be created.", true);
                            }
                        }
                        else
                        {
                            Debug.AddLog("SendEMail: account could not be created.", true);
                        }
                    }
                    else
                    {
                        Debug.AddLog("SendEMail: defaultEMailAccount is not defined.", true);
                    }
                } else 
                {
                    Debug.AddLog ("SendEMail: No message to send.", true);
                }
            }
            else
            {
                if (configuration.ScreenOff)
                    Power.PowerOnOff(true);

                MessageBox.Show(message, "Alert!");
            }

            // Ok, let's exit the application
            if (bExitAfterSend)
            {
                Application.Exit();
            }
        }

        public static void SendFTP(string telSMS, string message, string[] attachs, bool bExitAfterSend, bool bFake)
        {
            string answer = "";

            if (configuration.FtpServer.Equals(""))
            {
                Debug.AddLog("SendFTP: There is not FTP account defined.", true);
                answer = Messages.msg_FTPServerNotDefined;
            }
            else
            {
                OwnerRecord or = OwnerInfo.GetOwnerRecord();

                string fileName = 
                    Utils.ChangeChar(
                      Utils.ChangeChar(ShellFolders.TempFolder + "\\RTAnswer_" +
                                       Commands.LastCommand + "_" +
                                       (Commands.rtCommon.atm ? or.Company + "_" + Utils.InvertedDate(DateTime.Today) : "") + 
                                       DateTime.Now.ToString() + ".txt", '/', '-'), ':', '-');
                Debug.AddLog("SendFTP: Creating text file: " + fileName, true);

                if (File.Exists(fileName))
                    File.Delete(fileName);

                StreamWriter sw = File.CreateText(fileName);

                sw.WriteLine(message);
                sw.Flush();
                sw.Close();

                Debug.AddLog("SendFTP: Creating ZIP file " + fileName + ".zip", true);
                Array.Resize(ref attachs, attachs.Length + 1);
                attachs[attachs.Length-1] = fileName;
                JVUtils.Compress.Zip.SimpleZip.Compress(attachs, fileName + ".zip");

                Debug.AddLog("SendFTP: Sending to FTP...", true);
                answer = Commands.FTP(new string[1] { fileName + ".zip" }, false);

                Debug.AddLog("SendFTP: Erasing files.", true);
                if (File.Exists(fileName))
                    File.Delete(fileName);
                if (File.Exists(fileName + ".zip"))
                    File.Delete(fileName + ".zip");
            }
            
            // Sending feedback
            if (!telSMS.Trim().Equals(""))
                if (telSMS.Contains("@"))
                    SendEMail(telSMS, answer, attachs, bExitAfterSend, bFake);
                else
                    SendSMSMessage(telSMS, answer, bExitAfterSend, bFake);
        }

        public static void SendWEB(string telSMS, string message, string[] attachs, bool bExitAfterSend, bool bFake)
        {
            // NOT IMPLEMENTED
            if (!telSMS.Trim().Equals(""))
                if (telSMS.Contains("@"))
                    SendEMail(telSMS, "WEB COMMANDS NOT IMPLEMENTED!", attachs, bExitAfterSend, bFake);
                else
                    SendSMSMessage(telSMS, "WEB COMMANDS NOT IMPLEMENTED!", bExitAfterSend, bFake);
        }
    }
}
