// Utils.cs
// compile with: /unsafe
#region Using directives
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text;
using System.Threading;
using System.Globalization;
using OpenNETCF.IO.Serial;
using OpenNETCF.ToolHelp;
using System.Diagnostics;
using System.Security.Permissions;
using System.Security.Cryptography;
using Microsoft.WindowsMobile.Telephony;
using Microsoft.WindowsMobile.Status;
#endregion

namespace JVUtils
{
    public class Utils
    {
        public static string PegaLinhaAtual(string IMSI)
        {
            string linha = LineNumberStore.GetLineNumberForIMSI(IMSI);

            if (linha.Equals(""))
            {
                PhoneAddress pa = Sim.GetPhoneNumber();
                linha = pa.Address;
            }

            if (linha == null || linha.Equals(""))
                linha = "0";

            return linha;
        }

        public static string PegaIP()
        {
            string currentIP = "0";
            string[] IPs;

            try
            {
                Tweak.GPRSConnectionStatus(false);
                try
                {
                    IPs = Utils.GetInternalIP();
                }
                finally
                {
                    Tweak.GPRSConnectionStatus(true);
                }

                if (IPs != null && IPs.Length != 0)
                    currentIP = IPs[0];
            }
            catch
            {
                currentIP = "0";
            }

            return currentIP;
        }

        public static string DonateURL()
        {
            return "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=2138678";
        }

        public static void Donate()
        {
            System.Diagnostics.Process.Start(DonateURL(), "");
        }

        public static string SupportEMail()
        {
            return "joubertvasc@gmail.com";
        }

        public static void CallSupport()
        {
            System.Diagnostics.Process.Start("mailto:" + SupportEMail(), "");
        }

        public static double KnotsToKm(double knots)
        {
            return knots * 1.852;
        }

        public static double KmToKnots(double km)
        {
            return km / 1.852;
        }

        public static double KmToMiles(double km)
        {
            return km * 0.6214;
        }

        public static double MilesToKm(double miles)
        {
            return miles / 0.6214;
        }

        public static double MetresToFeet(double metres)
        {
            return metres * 3.28;
        }

        public static double FeetToMetres(double feets)
        {
            return feets / 3.28;
        }

        public static bool ValidPhoneNumber(string phoneNumber)
        {
            string x = RemoveChar(phoneNumber, '+').Trim();
            x = RemoveChar(x, '$');

            return IsNumberValid(x); 
        }

        public static double Truncate(double numberToTruncate)
        {
            string num = System.Convert.ToString(numberToTruncate);
            int pos = num.IndexOf ('.');

            if (pos < 0)
                pos = num.IndexOf (',');

            if (pos > -1)
                num = num.Substring (0, pos);

            return System.Convert.ToDouble (num);
        }

        public static double AngleTo(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 = DegreesToRadians(lat1);
            lon1 = DegreesToRadians(lon1);
            lat2 = DegreesToRadians(lat2);
            lon2 = DegreesToRadians(lon2);
            double radians = -Math.Atan2(Math.Sin(lon1 - lon2) * Math.Cos(lat2), (Math.Cos(lat1) * Math.Sin(lat2)) - ((Math.Sin(lat1) * Math.Cos(lat2)) * Math.Cos(lon1 - lon2)));

            if (radians < 0.0)
                radians += 6.2831853071795862;

            return RadiansToDegrees(radians);
        }

        private static double DegreesToRadians(double degrees)
        {
            return (degrees / 57.29578);
        }

        private static double RadiansToDegrees(double radians)
        {
            return (radians * 57.29578);
        }

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            double num = 6371.0;
            double num2 = DegreesToRadians(lat2 - lat1);
            double num3 = DegreesToRadians(lon2 - lon1);
            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);
            double a = (Math.Sin(num2 / 2.0) * Math.Sin(num2 / 2.0)) + (((Math.Cos(lat1) * Math.Cos(lat2)) * Math.Sin(num3 / 2.0)) * Math.Sin(num3 / 2.0));
            double num5 = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            double num6 = num * num5;
            return (num6 * 1000.0);
        }

        public static string GetDecimalPart(double nValue, CultureInfo ci)
        {
            return nValue.ToString(ci).Split(new char[] { '.' })[1];
        }

        public static int GetIntegerPart(double nValue, CultureInfo ci)
        {
            return int.Parse(nValue.ToString(ci).Split(new char[] { '.' })[0]);
        }

        public static string iif(bool condicao, string condicaoVerdadeira, string condicaoFalsa)
        {
            if (condicao)
            {
                return condicaoVerdadeira;
            }
            else
            {
                return condicaoFalsa;
            }
        }

        public static int CalcTimeZoneOffSet()
        {
            TimeSpan timeSpan = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now) -
                                TimeZone.CurrentTimeZone.ToUniversalTime(DateTime.Now);

            return timeSpan.Hours;
        }

        public static IntPtr LocalAlloc(int byteCount)
        {
            IntPtr ptr = Win32.LocalAlloc(Win32.LMEM_ZEROINIT, byteCount);
            if (ptr == IntPtr.Zero)
            {
                throw new OutOfMemoryException();
            }

            return ptr;
        }

        public static void LocalFree(IntPtr hMem)
        {
            IntPtr ptr = Win32.LocalFree(hMem);
            if (ptr != IntPtr.Zero)
            {
                throw new ArgumentException();
            }
        }

        public static string RemoveChar(string original, char c)
        {
            string result = "";

            for (int i = 0; i < original.Length; i++)
            {
                if (original.Substring(i, 1) != c.ToString())
                {
                    result += original.Substring(i, 1);
                }
            }

            return result;
        }

        public static string ChangeChar(string original, char charToReplace, char replacingChar)
        {
            string result = "";
            
            for (int i = 0; i < original.Length; i++)
            {
                if (original.Substring(i, 1) == charToReplace.ToString()) {
                    result += replacingChar;
                } else
                {
                    result += original.Substring(i, 1);
                }
            }

            return result;
        }

        public static void Sleep(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public static string ExtractFileName(string fileName)
        {
            string result = "";

            for (int i = fileName.Length - 1; i >= 0; i--)
            {
                if (fileName.Substring(i, 1).Equals(@"\"))
                {
                    break;
                }
                else
                {
                    result = fileName.Substring(i, 1) + result;
                }
            }

            return result;
        }

        public static string ExtractFilePath(string fileName)
        {
            if (fileName.Trim().Equals("") || fileName.IndexOf("\\") == -1)
                return "";
            else
                return fileName.Substring(0, fileName.LastIndexOf("\\"));
        }

        public static string ChangeFileExt(string fileName, string newExt)
        {
            if (newExt.Trim().Equals(""))
            {
                return fileName;
            }
            else
            {
                string result = "";

                for (int i = 0; i < fileName.Length; i++)
                {
                    if (fileName.Substring(i, 1).Equals("."))
                    {
                        break;
                    }
                    else
                    {
                        result = result + fileName.Substring(i, 1);
                    }
                }

                return result + (newExt.Substring(0).Equals(".") ? newExt : "." + newExt);
            }
        }

        public static string GetFirstChars(string text, int chars)
        {
            if (text.Length < chars)
            {
                return text;
            }
            else
            {
                int pos = 0;

                for (int i = text.Length - 1; i >= 0; i--)
                {
                    if (text.Substring(i, 1).Equals(" "))
                    {
                        pos = i;
                        break;
                    }
                }

                if (pos > 2)
                {
                    return text.Substring(0, chars);
                }
                else
                {
                    return text.Substring(0, pos - 1) + "...";
                }                
            }
        }

        public static string FormattedValue(double value)
        {
            string result;

            if (value == 0)
            {
                result = "0,00";
            }
            else
            {
                result = value.ToString("###,000.00");
                string r = "";

                foreach (char c in result)
                {
                    if (!c.Equals('0') || !r.Equals(""))
                        r += c;
                }

                if (r.StartsWith("."))
                    r = "0" + r;

                result = r;
            }

            return result;
        }

        public static void CreateLink(string originalPath, string appName, string destinationPath, string linkName, string parameters)
        {
            if (!originalPath.EndsWith("\\"))
                originalPath += "\\";

            if (!destinationPath.EndsWith("\\"))
                destinationPath += "\\";

            if (!File.Exists(originalPath + linkName))
            {
                StreamWriter writer = File.CreateText(originalPath + linkName);
                writer.WriteLine("37#\"" + originalPath + appName + "\" " + parameters + " -a");
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

        public static bool IsNumberValid(string numericValue)
        {
            try
            {
                System.Convert.ToDouble(numericValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidEMail(string address)
        {
            return address.Contains("@") && address.Contains(".");
        }

        public static bool IsTouchScreen() 
        {
            return PlatformDetection.IsTouchScreen();
        }

        public static bool OnlyValidChars(string str)
        {
            bool result = true;
            string strTest = str.ToLower();
            char letter;

            // Accepting only characters from 0 to 9 and a to z
            for (int i = 0; i < strTest.Length; i++)
            {
                letter = System.Convert.ToChar(strTest.Substring(i, 1));
                if ((int)letter < 48 || (int)letter > 122 ||
                     ((int)letter > 57 && (int)letter < 97))
                {
                    result = false;
                    break;        
                }
            }

            return result;
        }

        public static string RemoveInvallidChars(string str)
        {
            string strTmp = "";
            char letter;

            // Accepting only characters from 0 to 9 and a to z
            for (int i = 0; i < str.Length; i++)
            {
                letter = System.Convert.ToChar(str.Substring(i, 1));
                if ((int)letter < 48 || (int)letter > 122 ||
                     ((int)letter > 57 && (int)letter < 97))
                {
                }
                else
                {
                    strTmp += str.Substring(i, 1);
                }
            }

            return strTmp;
        }

        public static double UnixTime(DateTime date)
        {
            TimeSpan ts = (date - new DateTime(1970, 1, 1, 0, 0, 0));
            return ts.TotalSeconds;
        }

        public static bool IsSQLServerCE35Installed(out int majorVersion, out int minorVersion, out int buildNumber)
        {
            majorVersion = 0;
            minorVersion = 0;
            buildNumber = 0;

            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Software\\Microsoft\\Microsoft SQL Server Compact Edition v3.5 Core");

            if (r != null)
            {
                try
                {
                    majorVersion = System.Convert.ToInt16(r.GetValue("MajorVersion", "0"));
                    minorVersion = System.Convert.ToInt16(r.GetValue("MinorVersion", "0"));
                    buildNumber = System.Convert.ToInt16(r.GetValue("BldNum", "0"));
                    r.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool IsDotNetInstalled(int targetVersion)
        {
            return IsDotNetInstalled(targetVersion, false);
        }

        public static bool IsDotNetInstalled(int targetVersion, bool exact)
        {
            bool result = false;
            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Software\\Microsoft\\.NetCompactFrameWork");

            if (r != null)
            {
                try
                {
                    string[] keys = r.GetValueNames();
                    int version = 0;
                    string sVersion = "";

                    foreach (string k in keys)
                    {
                        sVersion = RemoveChar(k.Substring(0, 3), '.');

                        if (!sVersion.Equals(""))
                        {
                            try
                            {
                                version = System.Convert.ToInt32(sVersion);
                            }
                            catch { 
                                version = 0; 
                            }

                            if ((exact && version == targetVersion) ||
                                (!exact && version >= targetVersion))
                            {
                                result = true;
                                break;
                            }
                        }
                    }

                    r.Close();
                }
                catch
                {
                }
            }

            return result;
        }
        
        public static string ConvertBaudeRateToString(BaudRates baudRate)
        {
            return baudRate.ToString().Substring(4);
        }

        public static BaudRates ConvertStringToBaudRate(string text)
        {
            if (text == null || text.Equals("") || text.Equals("110"))
            {
                return BaudRates.CBR_110;
            }
            else if (text.Equals("300"))
            {
                return BaudRates.CBR_300;
            }
            else if (text.Equals("600"))
            {
                return BaudRates.CBR_600;
            }
            else if (text.Equals("1200"))
            {
                return BaudRates.CBR_1200;
            }
            else if (text.Equals("2400"))
            {
                return BaudRates.CBR_2400;
            }
            else if (text.Equals("4800"))
            {
                return BaudRates.CBR_4800;
            }
            else if (text.Equals("9600"))
            {
                return BaudRates.CBR_9600;
            }
            else if (text.Equals("14400"))
            {
                return BaudRates.CBR_14400;
            }
            else if (text.Equals("19200"))
            {
                return BaudRates.CBR_19200;
            }
            else if (text.Equals("38400"))
            {
                return BaudRates.CBR_38400;
            }
            else if (text.Equals("56000"))
            {
                return BaudRates.CBR_56000;
            }
            else if (text.Equals("57600"))
            {
                return BaudRates.CBR_57600;
            }
            else if (text.Equals("115200"))
            {
                return BaudRates.CBR_115200;
            }
            else if (text.Equals("128000"))
            {
                return BaudRates.CBR_128000;
            }
            else if (text.Equals("230400"))
            {
                return BaudRates.CBR_230400;
            }
            else if (text.Equals("256000"))
            {
                return BaudRates.CBR_256000;
            }
            else if (text.Equals("460800"))
            {
                return BaudRates.CBR_460800;
            }
            else if (text.Equals("921600"))
            {
                return BaudRates.CBR_921600;
            }
            else
            {
                return BaudRates.CBR_110;
            }
        }

        public static int ConvertStringToCOMPort(string comPort)
        {
            string com = "";

            foreach (char c in comPort)
            {
                if (((int)c > 47 && (int)c < 58))
                    com += c.ToString();
            }

            if (com.Equals(""))
            {
                return 0;
            }
            else
            {
                return System.Convert.ToInt16(com);
            }
        }

        public static string ConvertCOMPortToString(int comPort)
        {
            return "COM" + System.Convert.ToString(comPort).Trim() + ":";
        }

        public static string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }

        public static string[] GetIPAddress()
        {
            string[] result = new string[0];

            try
            {
                IPHostEntry ipEntry = System.Net.Dns.GetHostByName(GetHostName());
                IPAddress[] addr = ipEntry.AddressList;

                for (int i = 0; i < addr.Length; i++)
                {
                    if (addr[i].ToString().Contains("."))
                    {
                        Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = addr[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.AddLog("GetIPAddress: Error: " + GetOnlyErrorMessage(ex.Message.ToString()), true);            
            }

            return result;
        }

        public static string[] GetInternalIP()
        {
            try
            {
                string[] ip = GetIPAddress();
                Debug.AddLog("GetInternalIP: number of addresses: " + ip.Length.ToString(), true);

                // No ip address or Only activesync designed IP address force GPRS/3G connection
                if (ip.Length == 0 || (ip.Length == 1 && (ip[0].StartsWith("169.") || ip[0].Equals("127.0.0.1"))))
                {
                    Debug.AddLog("GetInternalIP: trying to access http://www.google.com to force a connection.", true);
                    try
                    {
                        // Try to connect to google
                        string res = Web.Request("http://www.google.com");
                        Debug.AddLog("GetInternalIP: result = " + res, true);
                        if (res.Trim().Equals(""))
                        {
                            return new string[0];
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.AddLog("GetInternalIP: error trying to force a connection: " + Utils.GetOnlyErrorMessage(e.Message), true);
                    }

                    ip = Utils.GetIPAddress();
                    Debug.AddLog("GetInternalIP: number of addresses: " + ip.Length.ToString(), true);
                }

                string[] validIP = new string[0];
                for (int i = 0; i < ip.Length; i++)
                {
                    // Ignore loopback and the IP designed by ActiveSync if there are more Address
                    if (!ip[i].Equals("127.0.0.1"))
                        if (!ip[i].StartsWith("169.") || (ip[i].StartsWith("169.") && ip.Length == 1))
                        {
                            Array.Resize(ref validIP, validIP.Length + 1);
                            validIP[validIP.Length - 1] = ip[i];
                        }
                }

                return validIP;
            }
            catch (Exception ex)
            {
                Debug.AddLog("GetInternalIP: Error message: " + ex.ToString(), true);

                return new string[0];
            }
        }

        static string SearshInSubDir(DirectoryInfo dri, string app, string ext)
        {
            string result = "";

            if (dri != null)
            {
                try
                {
                    // Search app in current folder
                    DirectoryInfo dirInfo = new DirectoryInfo(dri.FullName);

                    FileInfo[] fileInfo = dirInfo.GetFiles(ext);
                    if (fileInfo.Length > 0)
                    {
                        for (int k = 0; k < fileInfo.Length; k++)
                        {
                            if (fileInfo[k].Name.ToLower().Trim().Equals(app.ToLower().Trim()))
                            {
                                result = fileInfo[k].DirectoryName + "\\" + fileInfo[k].Name;
                                break;
                            }
                        }
                    }

                    // Not found again? God, go find in some sub-folder...
                    if (result.Equals(""))
                    {
                        DirectoryInfo[] dInfo = dri.GetDirectories();
                        if (dInfo != null)
                        {
                            if (dInfo.Length > 0)
                            {
                                foreach (DirectoryInfo driSub in dInfo)
                                {
                                    result = SearshInSubDir(driSub, app, ext);

                                    if (!result.Equals(""))
                                        break;
                                }
                            }
                        }
                    }
                }
                catch { }
            }

            return result;
        }

        public static string FindFileInFolders(
            string fileName, string fileExtension, string startFolder, bool subFolders)
        {
            string result = "";

            DirectoryInfo dirInfo = new DirectoryInfo(startFolder);

            if (dirInfo != null)
            {
                // Search app in main Start Menu folder
                FileInfo[] fileInfo = dirInfo.GetFiles(fileExtension);
                if (fileInfo.Length > 0)
                {
                    for (int k = 0; k < fileInfo.Length; k++)
                    {
                        if (fileInfo[k].Name.ToLower().Trim().Equals(fileName.ToLower().Trim()))
                        {
                            result = fileInfo[k].DirectoryName + "\\" + fileInfo[k].Name;
                            break;
                        }
                    }
                }

                // Search app in sub-folders
                if (subFolders && result.Equals(""))
                {
                    DirectoryInfo[] subdirInfo = dirInfo.GetDirectories();

                    if (subdirInfo != null)
                    {
                        if (subdirInfo.Length > 0)
                        {
                            foreach (DirectoryInfo dri in subdirInfo)
                            {
                                result = SearshInSubDir(dri, fileName, fileExtension);
                                if (!result.Equals(""))
                                    break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        static string[] ListSubDir(DirectoryInfo dri, string ext)
        {
            string[] result = new string[0];

            if (dri != null)
            {
                try
                {
                    // Search app in current folder
                    DirectoryInfo dirInfo = new DirectoryInfo(dri.FullName);

                    DirectoryInfo[] dInfo = dri.GetDirectories();
                    if (dInfo != null && dInfo.Length > 0)
                        foreach (DirectoryInfo driSub in dInfo)
                        {
                            string[] sub = ListSubDir(driSub, ext);

                            foreach (string s in sub)
                            {
                                Array.Resize(ref result, result.Length+1);
                                result[result.Length-1] = s;
                            }
                        }

                    FileInfo[] fileInfo = dirInfo.GetFiles(ext);
                    if (fileInfo.Length > 0)
                        for (int k = 0; k < fileInfo.Length; k++)
                        {
                            Array.Resize(ref result, result.Length+1);
                            result[result.Length-1] = fileInfo[k].DirectoryName + "\\" + fileInfo[k].Name;
                        }
                }
                catch { }
            }

            return result;
        }

        public static string[] ListFilesInFolder(string fileExtension, string startFolder)
        {
            string[] result = new string[0];

            DirectoryInfo dirInfo = new DirectoryInfo(startFolder);

            if (dirInfo != null)
            {
                DirectoryInfo[] subdirInfo = dirInfo.GetDirectories();

                if (subdirInfo != null && subdirInfo.Length > 0)
                    foreach (DirectoryInfo dri in subdirInfo)
                    {
                        string[] sub = ListSubDir(dri, fileExtension);

                        foreach (string s in sub)
                        {
                            Array.Resize(ref result, result.Length+1);
                            result[result.Length-1] = s;
                        }
                    }
                
                // Search app in main Start Menu folder
                FileInfo[] fileInfo = dirInfo.GetFiles(fileExtension);
                if (fileInfo.Length > 0)
                    for (int k = 0; k < fileInfo.Length; k++)
                    {
                        Array.Resize(ref result, result.Length+1);
                        result[result.Length-1] = fileInfo[k].DirectoryName + "\\" + fileInfo[k].Name;
                    }
            }

            return result;
        }

        public static void AddAppToInit(string app)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Init");

            if (r != null)
            {
                for (int i = 99; i > 0; i--)
                {
                    if (r.GetValue("Launch" + System.Convert.ToString(i)) == null)
                    {
                        try
                        {
                            r.SetValue("Launch" + System.Convert.ToString(i), app);
                        }
                        catch
                        {
                            string path = app.Substring(0, app.LastIndexOf(@"\") + 1).ToLower();
                            string exe = app.Substring(app.LastIndexOf(@"\") + 1).ToLower();
                            string lnk = exe.Replace ("exe", "lnk").ToLower();
                            CreateLink(path, exe, ShellFolders.StartUpFolder, lnk, "");
                        }
                        break;
                    }
                }
            }
        }

        public static bool IsAppRegisteredInInit(string app)
        {
            bool result = false;
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Init");

            if (r != null)
            {
                string[] keys = r.GetValueNames();

                foreach (string k in keys)
                {
                    if (k.ToLower().StartsWith("launch") &&
                        ((string)r.GetValue(k)).ToLower().Equals(app.ToLower()))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        public static void RemoveAppFromInit(string app, bool completeName)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Init");

            if (r != null)
            {
                string[] keys = r.GetValueNames();

                foreach (string k in keys)
                {
                    if (k.ToLower().StartsWith("launch") && 
                        ((completeName && ((string)r.GetValue(k)).ToLower().Equals(app.ToLower())) || 
                         (!completeName && ((string)r.GetValue(k)).ToLower().StartsWith(app.ToLower()))))
                    {
                        try
                        {
                            r.DeleteValue(k);
                            r.Close();
                        }
                        catch (Exception e)
                        {
                            Debug.AddLog("RemoveAppFromInit error: " + e.ToString(), true);
                        }
                        break;
                    }
                }
            }
        }

        public static void KillProcess(ProcessEntry processToKill)
        {
            ProcessEntry pe = processToKill;

            try
            {
                try
                {
                    Process p = Process.GetProcessById((int)pe.ProcessID);

                    if (p.MainWindowHandle != IntPtr.Zero)
                        Kernel.SendMessage(p.MainWindowHandle, (uint)OpenNETCF.Win32.WM.CLOSE, IntPtr.Zero, IntPtr.Zero);

                    System.Threading.Thread.Sleep(500);

                    if (Process.GetProcessById((int)pe.ProcessID) != null)
                        pe.Kill();
                }
                catch (ArgumentException) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static ProcessEntry SearchProcess(string processName)
        {
            ProcessEntry[] pes = ProcessEntry.GetProcesses();

            foreach (ProcessEntry pe in pes)
            {
                if (pe.ExeFile.ToLower().Trim().Equals(processName.ToLower().Trim()))
                {
                    return pe;
                }
            }

            return null;
        }

        public static bool RemoveFromUninstallKey(string key)
        {
            bool result = false;

            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Software\\Apps\\");
            if (r != null)
            {
                try
                {
                    r.DeleteSubKeyTree(key);
                    r.Close();
                    result = true;
                }
                catch
                {
                    r.Close();
                }

                r = Registry.LocalMachine.OpenSubKey("\\Security\\AppInstall");
                if (r != null)
                {
                    try
                    {
                        r.DeleteSubKeyTree(key);
                        r.Close();
                        result = true;
                    }
                    catch
                    {
                        r.Close();
                    }
                }
            }

            if (Directory.Exists(ShellFolders.WindowsFolder + "\\AppMrg\\" + key))
            {
                Directory.Delete(ShellFolders.WindowsFolder + "\\AppMrg\\" + key, true);
            }

            return result;
        }

        public static string GenerateRandomName(string prefix, string extension)
        {
            string result = (prefix.Length <= 5 ? prefix : prefix.Substring(0, 4));

            Random r = new Random();

            for (int i = 0; i < (5 - result.Length); i++)
            {
                result += (char)r.Next(97, 122);
            }

            for (int i = 0; i < 3; i++)
            {
                result += (char)r.Next(48, 57);
            }

            return result + (extension.StartsWith(".") ? extension : "." + extension);
        }

        public static bool ForceCopyFile(string source, string copyto)
        {
            Debug.AddLog("ForceCopyFile source: '" + source + "' to: '" + copyto + "'", true);
            bool result = false;

            try
            {
                if (File.Exists(source))
                {
                    if (File.Exists(copyto))
                    {
                        Debug.AddLog("ForceCopyFile final file exists, deleting.", true);
                        File.Delete(copyto);
                    }

                    Debug.AddLog("ForceCopyFile copying file.", true);
                    File.Copy(source, copyto);

                    result = true;
                }
                else
                {
                    Debug.AddLog("ForceCopyFile source file does not exists, nothing to do.", true);
                }
            }
            catch (Exception e)
            {
                Debug.AddLog("ForceCopyFile exception: " + e.Message.ToString(), true);
            }

            return result;
        }

        public static string StringToBase64(string input)
        {
            byte[] data = System.Text.UnicodeEncoding.UTF8.GetBytes(input);
            Base64Encoder myEncoder = new Base64Encoder(data);
            StringBuilder sb = new StringBuilder();

            sb.Append(myEncoder.GetEncoded());

            return sb.ToString();
        }

        public static string Base64ToString(string input)
        {
            char[] data = input.ToCharArray();
            Base64Decoder myDecoder = new Base64Decoder(data);
            StringBuilder sb = new StringBuilder();

            byte[] temp = myDecoder.GetDecoded();
            sb.Append(System.Text.UTF8Encoding.UTF8.GetChars(temp));

            return sb.ToString();
        }

        static string ChangeDecimalSeparator(string value)
        {
            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;

            if (decimalSeparator.Equals("."))
                return ChangeChar(value, ',', System.Convert.ToChar(decimalSeparator));
            else
                return ChangeChar(value, '.', System.Convert.ToChar(decimalSeparator));
        }

        public static double StringToDouble(string value)
        {
            return System.Convert.ToDouble(ChangeDecimalSeparator(value));
        }

        public static decimal StringToDecimal(string value)
        {
            return System.Convert.ToDecimal(ChangeDecimalSeparator(value));
        }

        public static string GetOnlyErrorMessage(string errorMessage)
        {
            if (errorMessage.Contains(":"))
                return errorMessage.Substring(0, errorMessage.IndexOf(":"));
            else
                return errorMessage;
        }

        public static void ShowTodayScreen(int timer)
        {
            if (timer > 0)
                System.Threading.Thread.Sleep(timer * 1000);

            SendKeys.Send("{F4}");
        }

        public static bool Format(string storageCard)
        {
            try
            {
                if (System.IO.Directory.Exists(storageCard))
                {
                    System.IO.DirectoryInfo del = new DirectoryInfo(storageCard);
                    del.Delete(true);
                    return true;
                }
                else
                {
                    Debug.AddLog("Format error: directory " + storageCard + " does not exists.", true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.AddLog("Format error: " + ex.Message.ToString(), true);
                return false;
            }
        }

        private static void deleteFiles(DirectoryInfo folder)
        {
            if (folder != null)
            {
                try
                {
                    DirectoryInfo[] dInfo = folder.GetDirectories();
                    if (dInfo != null)
                    {
                        if (dInfo.Length > 0)
                        {
                            foreach (DirectoryInfo subFolder in dInfo)
                            {
                                deleteFiles(subFolder);
                            }
                        }

                        FileInfo[] subfileInfo = folder.GetFiles("*.*");
                        if (subfileInfo.Length > 0)
                        {
                            for (int j = 0; j < subfileInfo.Length; j++)
                            {
                                try
                                {
                                    subfileInfo[j].Attributes = FileAttributes.Normal;
                                    System.IO.File.Delete(subfileInfo[j].FullName);
                                }
                                catch (Exception ex)
                                {
                                    Debug.AddLog("Wipe: could not delete the file " + subfileInfo[j].FullName + 
                                                 ": " + ex.Message.ToString(), true);
                                }
                            }
                        }
                    }

                    try
                    {
                        folder.Attributes = FileAttributes.Normal;
                        System.IO.Directory.Delete(folder.FullName);
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("Wipe: could not delete the folder " + folder.FullName +
                                     ": " + ex.Message.ToString(), true);
                    }
                }
                catch { }
            }
        }

        public static bool Wipe(string folder)
        {
            if (System.IO.Directory.Exists(folder))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folder);

                if (dirInfo != null)
                {
                    DirectoryInfo[] subdirInfo = dirInfo.GetDirectories();

                    if (subdirInfo != null)
                    {
                        if (subdirInfo.Length > 0)
                        {
                            foreach (DirectoryInfo dri in subdirInfo)
                            {
                                deleteFiles(dri);
                            }
                        }

                        FileInfo[] fileInfo = dirInfo.GetFiles("*.*");
                        if (fileInfo.Length > 0)
                        {
                            for (int k = 0; k < fileInfo.Length; k++)
                            {
                                try
                                {
                                    fileInfo[k].Attributes = FileAttributes.Normal;
                                    System.IO.File.Delete(fileInfo[k].FullName);
                                }
                                catch (Exception ex)
                                {
                                    Debug.AddLog("Wipe: could not delete the file " + fileInfo[k].FullName + 
                                                 ": " + ex.Message.ToString(), true);
                                }
                            }
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static string FormatXMLString(string line)
        {
            return ChangeChar(ChangeChar(line, '<', '('), '>', ')');
        }

        public static string FormatXMLItem(string line)
        {
            return ChangeChar(ChangeChar(ChangeChar(line, '<', '('), '>', ')'), ' ', '_');
        }

        public static byte[] StringToByteArray(string text)
        {
            char[] c = new char[text.Length];
            byte[] b = new byte[text.Length];

            c = text.ToCharArray();

            for (int i = 0; i < c.Length; i++)
                b[i] = (byte)c[i];

            return b;
        }

        public static string ByteArrayToString(byte[] list)
        {
            string r = "";
            for (int i = 0; i < list.Length; i++)
                r += (char)list[i];

            return r;
        }

        public unsafe static void MakeCall(string number)
        {
            Debug.AddLog("MakeCall: " + number, true);

            IntPtr res;

            string PhoneNumber = number + '\0';
            char[] cPhoneNumber = PhoneNumber.ToCharArray();

            fixed (char* pAddr = cPhoneNumber)
            {
                PhoneMakeCallInfo info = new PhoneMakeCallInfo();
                info.cbSize = (IntPtr)Marshal.SizeOf(info);
                info.pszDestAddress = (IntPtr)pAddr;
                info.dwFlags = (IntPtr)Kernel.PMCF_DEFAULT;

                res = Kernel.PhoneMakeCall(ref info);
            }
        }

        public static void PhoneMakeCall(string number)
        {
            Debug.AddLog("PhoneMakeCall: " + number, true);

            Phone p = new Phone();
            p.Talk(number, false);
        }

        public static string Encode(string parameters)
        {
            Debug.AddLog("Encode: parameters = " + parameters, true);
            string par = "";

            // Change the '+' character to '%20', '=' to '%3D' and '@' to '%40'
            foreach (char c in parameters)
            {
                if (c == '+')
                    par += "%2B";
                else if (c == '=')
                    par += "%3D";
                else if (c == '@')
                    par += "%40";
                else
                    par += c;
            }

            Debug.AddLog("Encode: par= " + par, true);

            return par;
        }

        public static string Post(string url, string parameters)
        {
            return Post(url, parameters, "application/x-www-form-urlencoded");
        }

        public static string Post(string url, string parameters, string contentType)
        {
            Debug.AddLog("Post: URL=" + url + " Parameters=" + parameters + " ContentType=" + contentType, true); 

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(parameters);

            return Post(url, byte1, contentType);
        }

        public static string Post(string url, byte[] data)
        {
            return Post(url, data, "application/x-www-form-urlencoded");
        }

        public static string Post(string url, byte[] data, string contentType)
        {
            try
            {
                try
                {
                    ShowWaitCursor();

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    req.Method = "POST";

                    req.ContentLength = data.Length;
                    req.ContentType = contentType;
                    Stream outputStream = req.GetRequestStream();
                    outputStream.Write(data, 0, data.Length);
                    outputStream.Close();

                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    byte[] ps = new byte[res.ContentLength];
                    int totalBytesRead = 0;
                    while (totalBytesRead < ps.Length)
                        totalBytesRead += res.GetResponseStream().Read(ps, totalBytesRead, ps.Length - totalBytesRead);

                    if (res.StatusCode == HttpStatusCode.OK)
                        return Utils.ByteArrayToString(ps);
                    else
                        return string.Empty;
                }
                finally
                {
                    HideWaitCursor();
                }
            }
            catch (Exception e)
            {
                Debug.AddLog("Post: error: " + e.Message.ToString(), true);
                return string.Empty;
            }
        }

        public static string MD5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static void CopyByteArray(byte[] source, ref byte[] destination, int startPosition)
        {
            if (startPosition < destination.Length)
            {
                int j = 0;

                for (int i = startPosition; i < startPosition + source.Length; i++)
                {
                    if (i >= destination.Length)
                        return;

                    destination[i] = source[j];
                    j++;
                }
            }
        }

        public static int BatteryMetter()
        {
            if (SystemState.PowerBatteryState == BatteryState.Charging)
                return 100;
            else
            {
                BatteryLevel bl = SystemState.PowerBatteryStrength;
                if (bl == BatteryLevel.VeryLow)
                    return 10;
                else if (bl == BatteryLevel.Low)
                    return 30;
                else if (bl == BatteryLevel.Medium)
                    return 50;
                else if (bl == BatteryLevel.High)
                    return 70;
                else if (bl == BatteryLevel.VeryHigh)
                    return 90;
                else 
                    return 100;
            }
        }

        public static void ShowWaitCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public static void HideWaitCursor()
        {
            Cursor.Current = Cursors.Default;
            Kernel.SetCursor(IntPtr.Zero);
        }

        public static int ArrayIndexOf(string[] array, string textToFind)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(textToFind))
                    return i;
            }

            return -1;
        }

        public static string Capitalize(string original)
        {
            string result = "";

            if (!original.Trim().Equals(""))
            {
                string[] preps = new string[8] { "de", "da", "do", "das", "dos", "para", "S/A", "LTDA." };

                char[] delimiterChars = { ' ' };
                string[] words = original.Trim().Split(delimiterChars);

                foreach (string word in words)
                {
                    int x = ArrayIndexOf(preps, word);

                    if (word.Length == 1 || x > -1)
                        result += preps[x] + " ";
                    else
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                }
            }

            return result.Trim();
        }

        public static string FillZeros(string text, int size)
        {
            if (text.Length < size)
                for (int i = text.Length; i < size; i++)
                    text = "0" + text;

            return text;
        }

        public static string InvertedDate(DateTime date)
        {
            return FillZeros(date.Year.ToString(), 4) +
                   FillZeros(date.Month.ToString(), 2) +
                   FillZeros(date.Day.ToString(), 2);
        }

        public static bool CreateScheduledTask(DateTime schedule, string task)
        {
            Debug.AddLog("CreateScheduledTask: scheduling to " + schedule.ToString() + ". Task: " + task, true);
            long fileStartTime = schedule.ToFileTime();
            long localFileStartTime = 0;
            Kernel.FileTimeToLocalFileTime(ref fileStartTime, ref localFileStartTime);
            SystemTime systemStartTime = new SystemTime();

            try
            {
                Kernel.FileTimeToSystemTime(ref localFileStartTime, systemStartTime);
                Kernel.CeRunAppAtTime(task, systemStartTime);
                Debug.AddLog("CreateScheduledTask: success scheduled.", true);

                return true;
            }
            catch (Exception ex)
            {
                Debug.AddLog("CreateScheduledTask: scheduled failed: " + ex.Message.ToString(), true);

                return false;
            }
        }

        public static bool CancelScheduledTask(string task)
        {
            Debug.AddLog("CancelScheduledTask: canceling " + task, true);
            try
            {
                Kernel.CeRunAppAtTime(task, null);
                Debug.AddLog("CancelScheduledTask: canceled.", true);

                return true;
            }
            catch (Exception ex)
            {
                Debug.AddLog("CancelScheduledTask: failed: " + ex.Message.ToString(), true);

                return false;
            }
        }

        internal partial class PInvoke
        {
            const int MAX_PATH = 260;
            [DllImport("Coredll.dll")]
            static extern int SHGetSpecialFolderPath(IntPtr hwndOwner, StringBuilder lpszPath, int nFolder, int fCreate);

            public enum SpecialFolders : int
            {
                CSIDL_WINDOWS = 0x0024,
            }

            public static string GetSpecialFolder(SpecialFolders specialFolder)
            {
                StringBuilder path = new StringBuilder(MAX_PATH);

                if (SHGetSpecialFolderPath(IntPtr.Zero, path, (int)specialFolder, 0) == 0)
                    throw new Exception("Error getting Windows path.");

                return path.ToString();
            }
        }
    }

    public class Win32
    {
        public const int LMEM_ZEROINIT = 0x40;
        [System.Runtime.InteropServices.DllImport("coredll.dll", EntryPoint = "#33", SetLastError = true)]
        public static extern IntPtr LocalAlloc(int flags, int byteCount);

        [System.Runtime.InteropServices.DllImport("coredll.dll", EntryPoint = "#36", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);
    }
}
