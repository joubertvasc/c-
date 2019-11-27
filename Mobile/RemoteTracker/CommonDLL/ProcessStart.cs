using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Web.Services;
using System.Data;
using System.IO;
using System.Net;
using System.Diagnostics;
using JVUtils;

namespace CommonDLL
{
    public class ProcessStart
    {
        public static bool VPN()
        {            
            string appPath;
            appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            Process VPN = new Process();
            if (System.IO.File.Exists(appPath + "\\PocketVNC.exe"))
            {
                VPN.StartInfo.FileName = appPath + "\\PocketVNC.exe";
                VPN.Start();
                JVUtils.Debug.AddLog("VNC: Starting VPN", true); 
                return true;
            }
            else
            {
                return false;
            }
        }
               
    }
}