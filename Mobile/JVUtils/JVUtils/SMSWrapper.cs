using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.PocketOutlook;
using System.Windows.Forms;

namespace JVUtils
{
    public static class SMSWrapper
    {
        public static bool SendSMS(string toNumber, string message)
        {
            SmsMessage s = new SmsMessage();
            Recipient r = new Recipient("JVUtils", toNumber);
            s.To.Add(r);

            s.Body = message;
            s.RequestDeliveryReport = false;
            try
            {
                Debug.AddLog("SendSMS: number=" + toNumber + " Text=" + message, true);

                s.Send();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Debug.AddLog("SendSMS: Error sending message: " + ex.ToString(), true);
                return false;
            }

            Debug.AddLog("SendSMS: message sent.", true);
            return true;
        }

        private static bool SendChunk(string toNumber, string chunk)
        {
            SmsMessage s = new SmsMessage();
            Recipient r = new Recipient("RemoteTracker", toNumber);
            s.To.Add(r);

            s.Body = chunk;
            s.RequestDeliveryReport = false;
            try
            {
                Debug.AddLog("EnviaPedacoSMS: número=" + toNumber + " Text=" + chunk, true);

                s.Send();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Debug.AddLog("EnviaPedacoSMS: Erro enviando mensagem: " + ex.ToString(), true);
                return false;
            }

            Debug.AddLog("EnviaPedacoSMS: enviado.", true);
            return true;
        }

        public static bool SendSeparatedSMS(string toNumber, string message)
        {
            do
            {
                if (message.Length > 160)
                {
                    if (!SendChunk(toNumber, message.Substring(0, 160)))
                        return false;

                    message = message.Substring(160);
                }
                else
                {
                    if (!SendChunk(toNumber, message))
                        return false;

                    message = "";
                }
            } while (message != "");

            return true;
        }
    }
}
