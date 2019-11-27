using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text;

namespace JVUtils
{
    internal partial class PInvoke
    {
        const int MAX_PATH = 260;

        [DllImport("Coredll.dll", EntryPoint = "SystemParametersInfoW", CharSet = CharSet.Unicode)]
        static extern int SystemParametersInfo4Strings(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);

        public enum SystemParametersInfoActions : uint
        {
            SPI_GETPLATFORMTYPE = 257, // this is used elsewhere for Smartphone/PocketPC detection
            SPI_GETOEMINFO = 258,
        }

        public static string GetOemInfo()
        {
            StringBuilder oemInfo = new StringBuilder(50);
            if (SystemParametersInfo4Strings((uint)SystemParametersInfoActions.SPI_GETOEMINFO,
                (uint)oemInfo.Capacity, oemInfo, 0) == 0)
                throw new Exception("Error getting OEM info.");
            return oemInfo.ToString();
        }

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
        
        public static string GetPlatformType()
        {
            StringBuilder platformType = new StringBuilder(50);
            if (SystemParametersInfo4Strings((uint)SystemParametersInfoActions.SPI_GETPLATFORMTYPE,
                (uint)platformType.Capacity, platformType, 0) == 0)
                throw new Exception("Error getting platform type.");
            return platformType.ToString();
        }
    }

    internal partial class PlatformDetection
    {
        public static bool IsSmartphone()
        {
            return PInvoke.GetPlatformType() == "SmartPhone";
        }
        
        public static bool IsPocketPC()
        {
            return PInvoke.GetPlatformType() == "PocketPC";
        }

        private const string MicrosoftEmulatorOemValue = "Microsoft DeviceEmulator";
        public static bool IsEmulator()
        {
            return PInvoke.GetOemInfo() == MicrosoftEmulatorOemValue;
        }
        
        public static bool IsTouchScreen()
        {
            string driverFileName = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\Hardware\DeviceMap\Touch",
                "DriverName", "touch.dll").ToString();
            string windowsFolder = PInvoke.GetSpecialFolder(PInvoke.SpecialFolders.CSIDL_WINDOWS);
            string driverPath = Path.Combine(windowsFolder, driverFileName);
            bool driverExists = File.Exists(driverPath);

            return
                driverExists &&
                // Windows Mobile 5.0 Smartphone emulator and earlier has a driver, but no touch screen.
                !(IsSmartphone() && IsEmulator() && Environment.OSVersion.Version.Major < 6);
        }
    }
}