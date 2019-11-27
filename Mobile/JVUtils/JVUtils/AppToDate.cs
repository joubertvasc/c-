using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JVUtils
{
    public static class AppToDate
    {
        public static bool IsInstalled()
        {
            return (File.Exists ("\\Windows\\AppToDate.exe")) && 
                   (Directory.Exists ("\\Application Data\\AppToDate"));
        }

        public static void CopyConfigFile(string appPath, string XMLFile, string ICONFile)
        {
            try
            {
                File.Copy(appPath + XMLFile, @"\Application Data\AppToDate\" + XMLFile, true);
                File.Copy(appPath + ICONFile, @"\Application Data\AppToDate\" + ICONFile, true);
            }
            catch { }
        }
    }
}
