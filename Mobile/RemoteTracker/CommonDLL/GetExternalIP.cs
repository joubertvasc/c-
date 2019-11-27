using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVUtils;

using System.Data;
using System.IO;
using System.Net;

namespace CommonDLL
{
    public class GetExternalIP
    {
        static string strAutoINET = "0"; // SrcID automatic INET connection
        static string strCradle = "0"; // Is Phone cradled ? Assumption: If yes, ActiveSync is running
        static string strWifi = "0";  // Is Phone via Wifi connected ?

        public static void checkConnections()
        {
            RegistryKey r1 = Registry.LocalMachine.OpenSubKey("System\\State\\Hardware");
            if (r1 != null)
            {
                strCradle = r1.GetValue("Cradled").ToString();
                r1.Close();
            }
            if (strCradle == "1")
            {
                Debug.AddLog("GIP: USB connected ? - Yes", true);
            }
            else
            {
                Debug.AddLog("GIP: USB connected ? - No", true);
            }

            RegistryKey r2 = Registry.LocalMachine.OpenSubKey("System\\State\\Network");
            if (r2 != null)
                {
                strWifi = r2.GetValue("Count", "").ToString();
                r2.Close();
                }
            if (strWifi == "1")
                {
                Debug.AddLog("GIP: WiFi connected ? - Yes", true);
                }
                else
                {
                Debug.AddLog("GIP: WiFi connected ? - No", true);
                }
        }

        // get automatic internet connection configuration
        public static string getCM_ProxyEntriesActual()
        {
            Debug.AddLog("GIP: Get automatic internet connection", true);
            string strProviders = "Comm\\ConnMgr\\Providers\\{EF097F4C-DC4B-4c98-8FF6-AEF805DC0E8E}";
            RegistryKey wReg = Registry.LocalMachine.OpenSubKey(strProviders);
            foreach (string wS in wReg.GetSubKeyNames())
            {
                Debug.AddLog("GIP Reg: " + wS, true);
                if (wS.Substring(0,5) == "HTTP-")
                {
                    RegistryKey wProv = Registry.LocalMachine.OpenSubKey(strProviders + "\\" + wS);
//                    if ((wProv.GetValue("Enable").ToString() == "1") && ((wProv.GetValue("Proxy").ToString()).Substring(0, 8) == "new-inet"))                    
                    if ((wProv.GetValue("Enable").ToString() == "1") && (wProv.GetValue("Type").ToString() == "0"))                    
                    {
                        Debug.AddLog("GIP: public Internet Connection configuration found", true);
                        Debug.AddLog("GIP: public Internet Connection - " + wProv.GetValue("SrcId").ToString(), true);
                        strAutoINET = (wProv.GetValue("SrcId").ToString()).Substring(1,36);
                        Debug.AddLog("GIP: public Internet Connection - " + strAutoINET , true);
                    }
                    foreach (string hS in wProv.GetValueNames())
                    {
                        Debug.AddLog("GIP HTTP-Reg-Key:" + hS + " - " + wProv.GetValue(hS).ToString(), true);
                    }
                    wProv.Close();
                }
            }
            wReg.Close();
            wReg = null;
            return strAutoINET;
        }


        // Force GPRS connection with strAutoINET
        public static void connectGPRS()
        {
            bool connected = false;
            int timeOut = 25; //seconds
            OpenNETCF.Net.ConnectionManager connMgr = new OpenNETCF.Net.ConnectionManager(); // open connection manager
            System.Guid gAutoINET = new Guid(strAutoINET);

            // Check connection status of GPRS
            if (strAutoINET == "0")
            {
                throw new Exception("No public destination info");
//                Debug.AddLog("GIP: No public destination info available", true); // code not reachable ????
            }
            else
            {
                Debug.AddLog("GIP: Force Internet connection - " + gAutoINET, true);
                connMgr.Connect(gAutoINET, true, OpenNETCF.Net.ConnectionMode.Asynchronous); // try to connect
                while (!connected)
                {
                    if (connMgr.Status != OpenNETCF.Net.ConnectionStatus.Connected)
                    {
                        System.Threading.Thread.Sleep(1000);
                        if (timeOut == 0)
                        {
                            throw new Exception("gprsAttachFailed");
                        }
                        timeOut -= 1;
                        continue;
                    }
                    connected = true;
                }
                Debug.AddLog("GIP: Try to connect to - " + gAutoINET, true);
                System.Threading.Thread.Sleep(5000); // wait 5 sec.
                Debug.AddLog("GIP: Internet connection exists ?" + connMgr.Status, true);
            }
        }
   

        // Get public WAN IP over GPRS    
        public static string getExternalIP()
        {
            string returnString = "";
            try
            {
                Debug.AddLog("GIP: Get external IP", true);
                // This makes sure the over GPRS for this URL
                if ((strCradle == "1") || (strWifi == "1")) //USB or Wifi connected
                {
                    SetINET_Prov();
                }
                
                System.Threading.Thread.Sleep(3000); // wait 10 sec.
                returnString = RetrieveString("http://whatismyip.com/automation/n09230945.asp");
                Debug.AddLog("GIP: external IP: Wait 10 sec. to get WAN-IP", true);
                System.Threading.Thread.Sleep(10000); // wait 10 sec.
                Debug.AddLog("GIP: external IP: " + returnString, true);

                if ((strCradle == "1") || (strWifi == "1")) //USB or Wifi connected
                {
                    DeleteINET_Prov();
                }
            }
            catch (Exception ex)
            {
                Debug.AddLog("GIP: external IP: error: " + Utils.GetOnlyErrorMessage(ex.Message));
            }
            return returnString;
        }

        // Force webrequest over GPRS
        //Uses CM_Mappings Provisioning to force the connection manager 
        //to use the internet and not the local network to update DDNS
        public static string SetINET_Prov()
        {
            Debug.AddLog("GIP: Set CM_Mappings for externalIP", true);
            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            //My Way of making strings with lots of quotes more legible 
            //by avoiding control characters for quotes(Works for VB too)
            String provString = "<wap-provisioningdoc>";
            provString += "<characteristic type='CM_Mappings'>";
            provString += "<characteristic type='900'>"; // This needs to be less than 1000 (first default mapping)
            provString += "<parm name='Pattern' value='http://whatismyip.com/automation/n09230945.asp'/>";
//            provString += "<parm name='Pattern' value='*://whatismyip.com/*'/>";
//            provString += "<parm name='Network' value='{A1182988-0D73-439E-87AD-2A5B369F808B}'/>";
//            provString += "<parm name='Network' value='{436EF144-B4FB-4863-A041-8F905A62C572}'/>";
            provString += "<parm name='Network' value='{" + strAutoINET + "}'/>";
            //            provString += "<parm name='Network' value='{adb0b001-10b5-3f39-27c6-9742e785fcd4}'/>";
            provString += "</characteristic>";
            provString += "</characteristic>";
            provString += "</wap-provisioningdoc>";
            provString = provString.Replace("'", Convert.ToChar(34).ToString());
            Debug.AddLog("GIP: provString = " + provString);
            d.LoadXml(provString);
            try
            {
                System.Xml.XmlDocument d2 = Microsoft.WindowsMobile.Configuration.ConfigurationManager.ProcessConfiguration(d, true);
                return d2.OuterXml;
            }
            catch (Exception ex)
            {
                Debug.AddLog("Error caught at SetINET_Prov - " + Utils.GetOnlyErrorMessage(ex.Message));
            }
            return "failed";
        }

        public static string DeleteINET_Prov()
        {
            Debug.AddLog("GIP: Delete CM_Mappings for externalIP", true);
            System.Xml.XmlDocument d = new System.Xml.XmlDocument();
            //My Way of making strings with lots of quotes more legible 
            //by avoiding control characters for quotes(Works for VB too)
            String provString = "<wap-provisioningdoc>";
            provString += "<characteristic type='CM_Mappings'>";
            provString += "<nocharacteristic type='900'/>";
            provString += "</characteristic>";
            provString += "</wap-provisioningdoc>";
            provString = provString.Replace("'", Convert.ToChar(34).ToString());
            Debug.AddLog("GIP: provString = " + provString);
            d.LoadXml(provString);
            try
            {
                System.Xml.XmlDocument d2 = Microsoft.WindowsMobile.Configuration.ConfigurationManager.ProcessConfiguration(d, true);
                return d2.OuterXml;
            }
            catch (Exception ex)
            {
                Debug.AddLog("Error caught at DeleteINET_Prov - " + Utils.GetOnlyErrorMessage(ex.Message));
            }
            return "failed";
        }

        /// Download a file as a string from the web.
        /// url - URL of the file to download
        /// returns - non empty string if it was successful - empty string otherwise
        public static string RetrieveString(string url)
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;

            System.IO.Stream strm = null;
            System.IO.StreamReader sr = null;

            String responseText = "";

            try
            {
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "OpenControl";
                request.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy();

                request.Timeout = 10000; // 10 seconds
                response = (System.Net.HttpWebResponse)request.GetResponse();

                if (response != null)
                {
                    strm = response.GetResponseStream();
                    sr = new System.IO.StreamReader(strm);

                    responseText = sr.ReadToEnd();

                    strm.Close();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                // something went terribly wrong.
                Debug.AddLog("error at RetrieveString - " + Utils.GetOnlyErrorMessage(ex.Message));
            }
            finally
            {
                // cleanup all potentially open streams.

                if (null != strm)
                    strm.Close();

                if (null != sr)
                    sr.Close();

                if (null != response)
                    response = null;

                if (null != request)
                    request = null;
            }

            return responseText;
        }

     }
}
