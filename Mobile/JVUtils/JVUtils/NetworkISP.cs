using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Wrappers;

namespace JVUtils
{
    public static class NetworkISP
    {
        public static bool CreateVPN(string vpnName, string userName, string password, string domain, string phone)
        {
            Guid g = System.Guid.NewGuid();

            return CreateVPN(vpnName, userName, password, domain, phone, g.ToString());
        }

        public static bool CreateVPN(string vpnName, string userName, string password, string domain, string phone, string guid)
        {
            string xml = "<wap-provisioningdoc>" +
                         "  <characteristic type=\"CM_VPNEntries\">" +
                         "    <characteristic type=\"" + vpnName + "\">" +
                         "      <parm name=\"UserName\" value=\"" + userName + "\" />" +
                         "      <parm name=\"Password\" value=\"" + password + "\" />" +
                         "      <parm name=\"SrcId\" value=\"" + guid + "\" />" +
                         "      <parm name=\"DestId\" value=\"{A1182988-0D73-439e-87AD-2A5B369F808B}\" />" +
                         "      <parm name=\"Phone\" value=\"" + phone + "\" />" +
                         "      <parm name=\"Domain\" value=\"" + domain + "\" />" +
                         "      <parm name=\"Type\" value=\"1\" />" +
                         "      <parm name=\"IPSecAuth\" value=\"0\" />" +
                         "    </characteristic>" +
                         "  </characteristic>" +
                         "</wap-provisioningdoc>";

            Debug.AddLog("CreateVPN", true);
            return ExecuteConfiguration(xml);
        }

        public static void RemoveVPN(string name)
        {
            string xml = "<wap-provisioningdoc>" +
                         "  <characteristic type=\"CM_VPNEntries\">" +
                         "    <nocharacteristic type=\"" + name + "\"/>" +
                         "  </characteristic>" +
                         "</wap-provisioningdoc>";

            Debug.AddLog("RemoveGPRS", true);
            ExecuteConfiguration(xml);
        }

        public static bool CreateGPRS(string apnName, string apnAddress, string username, string password, bool alwaysOn)
        {
            Guid g = System.Guid.NewGuid();

            return CreateGPRS(apnName, apnAddress, username, password, alwaysOn, g.ToString());
        }

        public static bool CreateGPRS(string apnName, string apnAddress, string username, string password, bool alwaysOn, string guid)
        {
            string xml = "<wap-provisioningdoc> " +
                         "  <characteristic type=\"CM_GPRSEntries\"> " +
                         "    <characteristic type=\"" + apnName + "\"> " +
                         "      <parm name=\"DestId\" value=\"{ADB0B001-10B5-3F39-27C6-9742E785FCD4}\" /> " +
                         "      <parm name=\"AlwaysOn\" value=\"" + (alwaysOn ? "1" : "0") + "\" /> " +
                         "      <parm name=\"DeviceName\" value=\"Cellular Line\" /> " +
                         "      <parm name=\"DeviceType\" value=\"modem\" /> " +
                         "      <parm name=\"Enabled\" value=\"1\" /> " +
                         "      <parm name=\"Password\" value=\"" + password + "\" /> " +
                         "      <parm name=\"RequirePw\" value=\"1\" /> " +
                         "      <parm name=\"Phone\" value=\"~GPRS!" + apnAddress + "\"/> " +
                         "      <parm name=\"Secure\" value=\"0\" /> " +
                         "      <parm name=\"UserName\" value=\"" + username + "\" /> " +
                         "      <parm name=\"Domain\" value=\"\" /> " +
                         "    </characteristic> " +
                         "  </characteristic> " +
                         "</wap-provisioningdoc> ";

            Debug.AddLog("CreateGPRS", true);
            return ExecuteConfiguration(xml);
        }

        public static void RemoveGPRS(string name)
        {
            string xml = "<wap-provisioningdoc>" +
                         "  <characteristic type=\"CM_GPRSEntries\">" +
                         "    <nocharacteristic type=\"" + name + "\"/>" +
                         "  </characteristic>" +
                         "</wap-provisioningdoc>";

            Debug.AddLog("RemoveGPRS", true);
            ExecuteConfiguration(xml);
        }

        private static bool ExecuteConfiguration(string xml)
        {
            XmlDocument configurationXmlDoc = new XmlDocument();
            configurationXmlDoc.LoadXml(xml);
            try
            {
                Debug.AddLog("ExecuteConfiguration: XML=" + xml, true);
                Wrappers.Wrappers.ProcessConfiguration(configurationXmlDoc);
                Debug.AddLog("ExecuteConfiguration: done.", true);
                return true;
            }
            catch (Exception e)
            {
                Debug.AddLog("ExecuteConfiguration: Error = " + Utils.GetOnlyErrorMessage(e.Message.ToString()), true);
                return false;
            }
        }
    }
}
