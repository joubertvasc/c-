using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace JVUtils
{
    public class Power
    {
        private const uint DBGDRIVERSTAT = 0x1802;
        private const uint GETPOWERMANAGEMENT = 0x1804;
        private const uint GETVFRAMELEN = 0x1801;
        private const uint GETVFRAMEPHYSICAL = 0x1800;
        private static bool _isSleeping;
        private static System.Threading.Timer SleepTimer;
        private const uint QUERYESCSUPPORT = 8;
        private const uint SETPOWERMANAGEMENT = 0x1803;
        private static bool _sendKeyToKeepAlive = false;

        public static bool SendKeyTokeepAlive
        {
            get { return _sendKeyToKeepAlive; }
            set { _sendKeyToKeepAlive = value; }
        }

        public static void DisableSleep(bool goToSleep)
        {
            if (goToSleep)
            {
                if (!_isSleeping && SleepTimer == null)
                {
                    _isSleeping = true;
                    SleepTimer = new System.Threading.Timer(new TimerCallback(PokeDeviceToKeepAwake), null, 0, 0x7530);
                }
            }
            else
            {
                if (_isSleeping && SleepTimer != null)
                {
                    SleepTimer.Dispose();
                    SleepTimer = null;
                    _isSleeping = false;
                }
            }
        }

        private static void PokeDeviceToKeepAwake(object extra)
        {
            try
            {
                SystemIdleTimerReset();

                if (_sendKeyToKeepAlive)
                    SendKeys.Send ("X");
            }
            catch
            {
            }
        }

        public static void PowerOnOff(bool turnOn)
        {
            IntPtr dC = GetDC(IntPtr.Zero);
            uint num = 12;
            byte[] array = new byte[num];
            BitConverter.GetBytes(num).CopyTo(array, 0);
            BitConverter.GetBytes(1).CopyTo(array, 4);

            if (turnOn)
            {
                BitConverter.GetBytes((uint)1).CopyTo(array, 8);
            }
            else
            {
                BitConverter.GetBytes((uint)4).CopyTo(array, 8);
                ExtEscapeSet(dC, 0x1803, num, array, 0, IntPtr.Zero);
            }

            ExtEscapeSet(dC, 0x1803, num, array, 0, IntPtr.Zero);
        }

        private enum VideoPowerState : uint
        {
            VideoPowerOn = 1,
            VideoPowerStandBy = 2,
            VideoPowerSuspend = 3,
            VideoPowerOff = 4
        }

        [DllImport("coredll", EntryPoint = "ExtEscape")]
        private static extern int ExtEscapeSet(IntPtr hdc, uint nEscape, uint cbInput, byte[] lpszInData, int cbOutput, IntPtr lpszOutData);

        [DllImport("coredll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("CoreDll.dll")]
        public static extern void SystemIdleTimerReset();
    }
}