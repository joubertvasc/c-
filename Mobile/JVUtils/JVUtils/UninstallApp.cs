using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JVUtils
{
    public class UninstallApp
    {
        public static CONFIG_E Uninstall(string xml, out string resultXml)
        {
            return Uninstall(xml, CFGFlags.Process, out resultXml);        
        }

        public static CONFIG_E Uninstall(string xml, CFGFlags flags, out string resultXml)
        {
            CONFIG_E result;
            unsafe
            {
                char* xmlOutPointer;
                result = Kernel.DMProcessConfigXML(xml, flags, out xmlOutPointer);
                resultXml = new string(xmlOutPointer);
                Kernel.DeleteArray((IntPtr)xmlOutPointer, IntPtr.Zero);
            }

            return result;
        }

        public static string CreateUnistallXML(string applicationName)
        {
            return "<wap-provisioningdoc>\n" +  
                   "  <characteristic type=" + (char)34 + "UnInstall" + System.Convert.ToString((char)34) + ">\n" +
                   "    <characteristic type=" + (char)34 + applicationName + (char)34 + ">\n" +
                   "      <parm name=" + (char)34 + "uninstall" + (char)34 + " value=" + (char)34 + "1" + (char)34 + "/>\n" +
                   "    </characteristic>\n" +
                   "  </characteristic>" +
                   "</wap-provisioningdoc>";
        }
    }
}
