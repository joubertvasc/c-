using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVUtils;

namespace JVUtils
{
    public static class LineNumberStore
    {
        public static void SetLineNumberForIMSI(string imsi, string lineNumber)
        {
            Debug.AddLog("SetLineNumberForIMSI: imsi=" + imsi + ", lineNumber=" + lineNumber, true);

            if (!imsi.Trim().Equals("") && !lineNumber.Trim().Equals(""))
            {
                RegistryKey r = Registry.LocalMachine.CreateSubKey(JVUtils.JVSoftwareKey + "\\LineNumbers");

                if (r != null)
                {
                    r.SetValue(imsi, lineNumber);

                    r.Close();
                    Debug.AddLog("SetLineNumberForIMSI: success", true);
                }
                else
                {
                    Debug.AddLog("SetLineNumberForIMSI: registry not found", true);
                }
            }
            else
            {
                Debug.AddLog("SetLineNumberForIMSI: imsi or lineNumber is empty", true);
            }
        }

        public static string GetLineNumberForIMSI(string imsi)
        {
            Debug.AddLog("GetLineNumberForIMSI: imsi=" + imsi, true);
            string result = "";

            if (!imsi.Trim().Equals(""))
            {
                RegistryKey r = Registry.LocalMachine.OpenSubKey(JVUtils.JVSoftwareKey + "\\LineNumbers");

                if (r != null)
                {
                    result = r.GetValue(imsi, "").ToString();

                    r.Close();
                    Debug.AddLog("GetLineNumberForIMSI: success. LineNumber is " + result, true);
                }
                else
                {
                    Debug.AddLog("GetLineNumberForIMSI: registry not found", true);
                }
            }
            else
            {
                Debug.AddLog("GetLineNumberForIMSI: imsi is empty", true);
            }

            return result;
        }
    }
}
