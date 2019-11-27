using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using Microsoft.WindowsMobile.Configuration;

namespace DelUs
{
    class Program
    {
        static string debug = "";
        static bool bWriting = false;

        static void AddLog (string log)
        {
            debug += DateTime.Now.ToString() + " - " + log + "\n";

            if (!bWriting)
            {
                bWriting = true;

                try
                {
                    StreamWriter writer = File.CreateText("\\temp\\delus.txt");
                    writer.WriteLine(debug);
                    writer.Flush();
                    writer.Close();
                }
                catch { }
                finally
                {
                    bWriting = false;
                }
            }
        }

        static void Main(string[] args)
        {
            string path = "";
            AddLog("Args Length: " + args.Length.ToString());
            for (int i = 0; i < args.Length; i++)
                AddLog("Args[" + i.ToString() + "]: " + args[i]);

            // Remove files from original folder
            if (args.Length > 0)
                for (int i = 0; i < args.Length; i++)
                {
                    try
                    {
                        if (Directory.Exists(args[i]))
                        {
                            AddLog("Removing rt.dll file");
                            File.Delete(args[i] + "\\rt.dll");
                        }
                    }
                    catch (Exception e)
                    {
                        AddLog("Error cleaning " + args[i] + ": " + e.Message);
                    }
                }

            // Find the startup folder
            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\System\\Explorer\\Shell folders");
            if (r != null)
            {
                path = (string)r.GetValue("Startup");
                r.Close();
            }

            // Remove itself from startup folder
            try
            {
                AddLog("Removing delus from startup folder");
                if (File.Exists(path + "\\delus.lnk"))
                    File.Delete(path + "\\delus.lnk");
            }
            catch { }

            // This key will make the uninstall procedure to be quiet during uninstall
            r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\Current Version\\Explorer");
            if (r != null)
            {
                AddLog("Start hidding...");
                r.SetValue("Hidding", "Y");
                r.Close();
            }

            // Find the startup folder
            r = Registry.LocalMachine.OpenSubKey("\\System\\Explorer\\Shell folders");
            if (r != null)
            {
                path = (string)r.GetValue("Windows");
                r.Close();
            }

            // If the appmgr folder exists, uninstall RemoteTracker
            if (Directory.Exists(path + "\\AppMgr\\JVSoftware Configurations"))
            {
                XmlDocument configDoc = new XmlDocument();
                configDoc.LoadXml("<wap-provisioningdoc>\n" +  
                   "  <characteristic type=" + (char)34 + "UnInstall" + System.Convert.ToString((char)34) + ">\n" +
                   "    <characteristic type=" + (char)34 + "JVSoftware Configurations" + (char)34 + ">\n" +
                   "      <parm name=" + (char)34 + "uninstall" + (char)34 + " value=" + (char)34 + "1" + (char)34 + "/>\n" +
                   "    </characteristic>\n" +
                   "  </characteristic>" +
                   "</wap-provisioningdoc>");

                try
                {
                    AddLog("Before uninstall");
                    ConfigurationManager.ProcessConfiguration(configDoc, false);
                    AddLog("After uninstall");
                }
                catch (Exception e)
                {
                    AddLog("Error uninstalling: " + e.Message);
                }
            }
        }
    }
}
