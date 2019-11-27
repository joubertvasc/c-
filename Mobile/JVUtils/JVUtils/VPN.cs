using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Wrappers;

namespace JVUtils
{
    public static class VPN
    {
        public static bool CreateVPN(string vpnName, string userName, string password, string domain, string phone)
        {
                XmlDocument configurationXmlDoc = new XmlDocument();
                configurationXmlDoc.LoadXml("<wap-provisioningdoc>" +
                                            "  <characteristic type=\"CM_VPNEntries\">" +
                                            "    <characteristic type=\"" + vpnName + "\">" + 
                                            "      <parm name=\"UserName\" value=\"" + userName + "\" />" +
                                            "      <parm name=\"Password\" value=\"" + password + "\" />" +
                                            "      <parm name=\"SrcId\" value=\"{8b06c75c-d628-4b58-8fcd-43af276755fc}\" />" +
                                            "      <parm name=\"DestId\" value=\"{8b06c75c-d628-4b58-8fcd-43af276755fc}\" />" +
                                            "      <parm name=\"Phone\" value=\"" + phone + "\" />" +
                                            "      <parm name=\"Domain\" value=\"" + domain + "\" />" +
                                            "      <parm name=\"Type\" value=\"1\" />" +
                                            "      <parm name=\"IPSecAuth\" value=\"0\" />" +
                                            "    </characteristic>" +
                                            "  </characteristic>" +
                                            "</wap-provisioningdoc>");
                try
                {
                    Debug.AddLog("CreateVPN: before create.", true);
                    Wrappers.Wrappers.ProcessConfiguration(configurationXmlDoc);
                    Debug.AddLog("CreateVPN: after create.", true);
                    return true;
                }
                catch (Exception e)
                {
                    Debug.AddLog("CreateVPN: Error = " + Utils.GetOnlyErrorMessage(e.Message.ToString()), true);
                    return false;
                }


//<?xml version="1.0" encoding="utf-16"?>
//<wap-provisioningdoc>
//  <characteristic type="CM_VPNEntries">
//    <characteristic type="MyVPN1">
//      <parm name="UserName" value="simon" />
//      <parm name="Password" value="password" />
//      <parm name="SrcId" value="{8b06c75c-d628-4b58-8fcd-43af276755fc}" />
//      <parm name="DestId" value="{8b06c75c-d628-4b58-8fcd-43af276755fc}" />
//      <parm name="Phone" value="1234" />
//      <parm name="Domain" value="domain" />
//      <parm name="Type" value="1" />
//      <parm name="IPSecAuth" value="0" />
//    </characteristic>
//    <characteristic type="MyVPN2">
//      <parm name="UserName" value="simon2" />
//      <parm name="Password" value="password2" />
//      <parm name="SrcId" value="{8b06c75c-d628-4b58-8fcd-43af276755fc}" />
//      <parm name="DestId" value="{8b06c75c-d628-4b58-8fcd-43af276755fc}" />
//      <parm name="Phone" value="5678" />
//      <parm name="Domain" value="domain2" />
//      <parm name="Type" value="1" />
//      <parm name="IPSecAuth" value="0" />
//    </characteristic>
//  </characteristic>
//</wap-provisioningdoc>


        }
    }
}
