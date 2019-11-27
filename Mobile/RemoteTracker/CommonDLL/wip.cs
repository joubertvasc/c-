using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVUtils;

using System.Web.Services;
using System.Data;
using System.IO;
using System.Net;

namespace CommonDLL
{
    public class wip
    {
        public static bool IsConnectedToInternet()
        {
            String strHostName = "";
             // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = System.Net.Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);

            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = System.Net.Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                Console.WriteLine("IP Address {0}: {1} ", i, addr[i].ToString());
            }

            string host = "http://www.google.com";
            bool result = false;
            OpenNETCF.Net.NetworkInformation.Ping p = new OpenNETCF.Net.NetworkInformation.Ping();
//            Ping p = new Ping();
            try
            {
                OpenNETCF.Net.NetworkInformation.PingReply reply = p.Send(host, 3000);
//                PingReply reply = p.Send(host, 3000);
//                if (reply.Status == IPStatus.Success)
                if (reply.Status == OpenNETCF.Net.NetworkInformation.IPStatus.Success)
                {
                    Debug.AddLog("WIP: InternetConnection available: Yes", true);
                    return true;
                }
                else
                {
                    Debug.AddLog("WIP: InternetConnection available: No", true);
                    Debug.AddLog("WIP: InternetConnection: will try to initiate", true);
                }
            }
            catch { }
            return result;
        }


        
        // 2nd way - not implemented
        public static bool CheckConnection(string targetAddress)
        {
            HttpWebRequest request;
            HttpWebResponse response;
            bool isConnected = false;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(targetAddress);
                response = (HttpWebResponse)request.GetResponse();
                request.Abort();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    isConnected = true;
                    Debug.AddLog("Send WIP: Connection exists", true);
                }
                response.GetResponseStream().Close();
            }
            catch (WebException we)
            {
                string errMsg = Utils.GetOnlyErrorMessage(we.Message);
                isConnected = false;
                Debug.AddLog("Send WIP: Connection does not exists", true);
            }
            catch (Exception ex)
            {
                string errMsg = Utils.GetOnlyErrorMessage(ex.Message);
                isConnected = false;
                Debug.AddLog("Send WIP: Connection does not exists", true);
            }
            finally
            {
                request = null;
                response = null;
            }
            return isConnected;
        }


        public static string GetWAN_IP()
        {
            string returnString = "";
            try
            {
                returnString = WebDownload.RetrieveString("http://whatismyip.com/automation/n09230945.asp");
                Debug.AddLog("WIP: Wait 10 sec. to get WAN-IP", true);
                Debug.AddLog("Send WIP: " + returnString, true);
            }
            catch (Exception ex)
            {
                Debug.AddLog("Send WIP error: " + Utils.GetOnlyErrorMessage(ex.Message));
            }
            return returnString;
        }
    }
}
