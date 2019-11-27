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
using System.Threading;
using CommonDLL;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;
using JVUtils;

namespace SMSLauncher
{
    class Program
    {
        static RTCommon rtCommon;
        static string sInstalledIMSI = "";
        static string sIMEI = "";

        static string PrepareMessageToSend()
        {
            // TODO: Get GPS position and Owner Information
            string message = "RemoteTracker: " + 
                rtCommon.languageXML.getColumn("msg_sim_card_changed", "SIM card was changed") + "!\n" +
                (Microsoft.WindowsMobile.Status.SystemState.PhoneOperatorName.Equals("") ||
                 Microsoft.WindowsMobile.Status.SystemState.PhoneOperatorName == null ? "" :
                 "(" + Microsoft.WindowsMobile.Status.SystemState.PhoneOperatorName + ")") + "\n" +
                "IMSI: " + sInstalledIMSI + "\n" +
                "IMEI: " + sIMEI;

            Debug.AddLog("PrepareMessageToSend: " + message, true);
            return message;
        }

        static void SendSMS(string number)
        {
            if (!number.Trim().Equals(""))
            {
                try
                {
                    Debug.AddLog("SendSMS: before send SMS to " + number, true);
                    Answer.SendSMSMessage(number, PrepareMessageToSend(), false, false);
                    Debug.AddLog("SendSMS: after send SMS", true);
                }
                catch (Exception e)
                {
                    Debug.AddLog("SendSMS: Error: " + Utils.GetOnlyErrorMessage(e.Message));
                }
            }
            else
            {
                Debug.AddLog("SendSMS: the number parameter is empty", true);
            }
        }

        static void SendEMail(string address)
        {
            if (!rtCommon.configuration.defaultEMailAccount.Equals("") && !address.Trim().Equals(""))
            {
                try
                {
                    Answer.Configurations = rtCommon.configuration;
                    Debug.AddLog("SendEMail: before send e-mail to " + address, true);
                    Answer.SendEMail(address, PrepareMessageToSend(), new string[0], false, false);
                    Debug.AddLog("SendEMail: after send e-mail", true);
                }
                catch (Exception e)
                {
                    Debug.AddLog("SendEMail: Error: " + Utils.GetOnlyErrorMessage(e.Message));
                }
            }
            else
            {
                Debug.AddLog("SendEMail: no address and default e-mail defined", true);
            }
        }

        static void SendToWeb()
        {
            try
            {
                // TODO
            }
            catch (Exception e)
            {
                Debug.AddLog("SendToWeb: Error: " + Utils.GetOnlyErrorMessage(e.Message));
            }
        }

        static void Main(string[] args)
        {
            Debug.StartLog();

            // Hide cursor
            Utils.HideWaitCursor();

            // Execute common taks 
            rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase));

            Debug.AddLog("DefaultNumber1 is set? " + (rtCommon.configuration.defaultNumber1.Equals("") ? "No" : "Yes") + "\n" +
                         "DefaultNumber2 is set? " + (rtCommon.configuration.defaultNumber2.Equals("") ? "No" : "Yes") + "\n" +
                         "DefaultNumber3 is set? " + (rtCommon.configuration.defaultNumber3.Equals("") ? "No" : "Yes") + "\n" +
                         "DefaultNumber4 is set? " + (rtCommon.configuration.defaultNumber4.Equals("") ? "No" : "Yes") + "\n" +
                         "Outgoing e-mail account is set? " + (rtCommon.configuration.defaultEMailAccount.Equals("") ? "No" : "Yes") + "\n" +
                         "Default recipient is set? " + (rtCommon.configuration.defaultrecipientEMail.Equals("") ? "No" : "Yes") + "\n" +
                         "EmergencyEMail1 is set? " + (rtCommon.configuration.emergencyEMail1.Equals("") ? "No" : "Yes") + "\n" +
                         "EmergencyEMail2 is set? " + (rtCommon.configuration.emergencyEMail2.Equals("") ? "No" : "Yes") + "\n" +
                         "EmergencyEMail3 is set? " + (rtCommon.configuration.emergencyEMail3.Equals("") ? "No" : "Yes") + "\n" +
                         "EmergencyEMail4 is set? " + (rtCommon.configuration.emergencyEMail4.Equals("") ? "No" : "Yes") + "\n" +
                         "Config IMSI1 is set? " + (rtCommon.configuration.IMSI1.Equals("") ? "No" : "Yes") + "\n" +
                         "Config IMSI2 is set? " + (rtCommon.configuration.IMSI2.Equals("") ? "No" : "Yes") + "\n" +
                         "Config IMSI3 is set? " + (rtCommon.configuration.IMSI3.Equals("") ? "No" : "Yes") + "\n" +
                         "Config IMSI4 is set? " + (rtCommon.configuration.IMSI4.Equals("") ? "No" : "Yes"));

            if (JVUtils.JVUtils.Get_ContractWasAccepted("RT", rtCommon.termOfServiceRevisionNumber))
            {
                Debug.AddLog("ToS was accepted. Creating interceptor.", true);
                // Set SMS interceptor
                rtCommon.CreateInterceptor();

                if (rtCommon.configuration.defaultNumber1 != "" &&
                    (rtCommon.configuration.IMSI1 != "" || rtCommon.configuration.IMSI2 != "" ||
                     rtCommon.configuration.IMSI3 != "" || rtCommon.configuration.IMSI4 != ""))
                {
                    // If not, prepare the message to be sent to emergency number
                    if (rtCommon.configuration.defaultLanguage >= rtCommon.languages.count)
                        rtCommon.configuration.defaultLanguage = 0;

                    if (rtCommon.languageXML.LoadLanguageXML(rtCommon.appPath + 
                                                             rtCommon.languages.fileName(rtCommon.configuration.defaultLanguage)))
                        Messages.ChangeLanguage(rtCommon.languageXML);

                    int nCount = 0;
                    bool bCanSendMessage = false;

                    // Verify if the Phone Service is ok. If not, try 30 times each 10 seconds
                    do
                    {
                        // Get the installed IMSI;
                        sInstalledIMSI = rtCommon.GetIMSI();
                        sIMEI = rtCommon.GetIMEI();

                        Debug.AddLog("Installed IMSI ok? " + Utils.iif(sInstalledIMSI != "", "Yes", "No"));

                        // Verify if SIM card was changed.
                        if (sInstalledIMSI != "")
                        {
                            Debug.AddLog("IMSI1 is equal than installed? " + Utils.iif(rtCommon.configuration.IMSI1.Equals(sInstalledIMSI), "Yes", "No"));
                            Debug.AddLog("IMSI2 is equal than installed? " + Utils.iif(rtCommon.configuration.IMSI2.Equals(sInstalledIMSI), "Yes", "No"));
                            Debug.AddLog("IMSI3 is equal than installed? " + Utils.iif(rtCommon.configuration.IMSI3.Equals(sInstalledIMSI), "Yes", "No"));
                            Debug.AddLog("IMSI4 is equal than installed? " + Utils.iif(rtCommon.configuration.IMSI4.Equals(sInstalledIMSI), "Yes", "No"));

                            if (!rtCommon.configuration.IMSI1.Equals(sInstalledIMSI) &&
                                !rtCommon.configuration.IMSI2.Equals(sInstalledIMSI) &&
                                !rtCommon.configuration.IMSI3.Equals(sInstalledIMSI) &&
                                !rtCommon.configuration.IMSI4.Equals(sInstalledIMSI))
                            {
                                bCanSendMessage = false;
                                Debug.AddLog("Count = " + System.Convert.ToString(nCount) + "\n" +
                                              "RadioOff? " + Utils.iif(Microsoft.WindowsMobile.Status.SystemState.PhoneRadioOff, "Yes", "No") + "\n" +
                                              "NoService? " + Utils.iif(Microsoft.WindowsMobile.Status.SystemState.PhoneNoService, "Yes", "No") + "\n" +
                                              "Searching? " + Utils.iif(Microsoft.WindowsMobile.Status.SystemState.PhoneSearchingForService, "Yes", "No"));
                                if ((!Microsoft.WindowsMobile.Status.SystemState.PhoneRadioOff &&
                                     !Microsoft.WindowsMobile.Status.SystemState.PhoneNoService &&
                                     !Microsoft.WindowsMobile.Status.SystemState.PhoneSearchingForService))
                                {
                                    bCanSendMessage = true;
                                }

                                // If the phone line is ok, send the message.
                                Debug.AddLog("Can send message? " + Utils.iif(bCanSendMessage, "Yes", "No"));
                                if (bCanSendMessage)
                                {
                                    // Send SMS...
                                    SendSMS(rtCommon.configuration.defaultNumber1);
                                    SendSMS(rtCommon.configuration.defaultNumber2);
                                    SendSMS(rtCommon.configuration.defaultNumber3);
                                    SendSMS(rtCommon.configuration.defaultNumber4);

                                    // Send e-mail...
                                    SendEMail(rtCommon.configuration.defaultrecipientEMail);
                                    SendEMail(rtCommon.configuration.emergencyEMail1);
                                    SendEMail(rtCommon.configuration.emergencyEMail2);
                                    SendEMail(rtCommon.configuration.emergencyEMail3);
                                    SendEMail(rtCommon.configuration.emergencyEMail4);

                                    //Debug.AddLog("Before call Show Today Screen", true);
                                    //Utils.ShowTodayScreen(1);
                                    //Debug.AddLog("After call Show Today Screen", true);

                                    // Send to web...
                                    SendToWeb();

                                    break;
                                }
                                else
                                {
                                    Debug.AddLog("Sleeping, phone not ok...");
                                    System.Threading.Thread.Sleep(10000);

                                    nCount++;
                                    Debug.AddLog("Count = " + nCount.ToString(), true);
                                }
                            }
                            else
                            {
                                Debug.AddLog("Breaking. The Installed SIM Card is known.", true);
                                break;
                            }
                        }
                        else
                        {
                            Debug.AddLog("Sleeping, new IMSI not found...");
                            System.Threading.Thread.Sleep(10000);

                            nCount++;
                            Debug.AddLog("Count = " + nCount.ToString(), true);
                        }
                    } while (nCount < 30);
                    Debug.AddLog("After DO looping", true);
                }
                else
                {
                    Debug.AddLog("DefaultNumert1 or any IMSI are not defined.", true);
                }
            }
            else
            {
                Debug.AddLog("ToS was not accepted. Exiting...", true);
            }

            Debug.EndLog();
            Debug.SaveLog(ShellFolders.TempFolder + "\\SMSLauncher.Debug.txt");

            // Signal to kernel the application was loaded
            Kernel.SignalStarted(uint.Parse("0"));
        }
    }
}
