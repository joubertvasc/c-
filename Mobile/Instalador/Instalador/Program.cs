using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using OpenNETCF.ToolHelp;

namespace Instalador
{
    static class Program
    {
        static string appPath = "";

        static void CreateLink(string originalPath, string appName, string destinationPath, string linkName, string parameters)
        {
            if (!File.Exists(originalPath + linkName))
            {
                StreamWriter writer = File.CreateText(originalPath + linkName);
                writer.WriteLine("37#\"" + originalPath + appName + "\" " + parameters + " -a ");
                writer.Close();
            }

            try
            {
                File.Delete(destinationPath + "\\" + linkName);
                File.Copy(originalPath + linkName, destinationPath + "\\" + linkName, false);
                File.Delete(originalPath + "\\" + linkName);
            }
            catch
            {
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static int Main(string[] args)
        {
            appPath = Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase);
            appPath = appPath.Substring(0, appPath.LastIndexOf(@"\") + 1);

            CreateLink(appPath, "installer.exe",
                Environment.GetFolderPath(Environment.SpecialFolder.Startup), "installer.lnk", "");

            return 0;
        }
    }
}