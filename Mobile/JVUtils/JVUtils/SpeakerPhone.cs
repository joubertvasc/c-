using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JVUtils
{
    public static class SpeakerPhone
    {
        public static void Toggle()
        {
            Debug.AddLog("SpeackerPhone.Toggle. Before sending F16", true);
            //keydown
            NativeMethods.keybd_event((byte)Keys.F16, 0, 0, 0);

            Debug.AddLog("SpeackerPhone.Toggle. Before sending F16 with keyeventf_keyup", true);
            //keyup
            NativeMethods.keybd_event((byte)Keys.F16, 0, NativeMethods.KEYEVENTF_KEYUP, 0);
            Debug.AddLog("SpeackerPhone.Toggle. Finished", true);
        }
    }

    internal static class NativeMethods
    {
        internal const int KEYEVENTF_KEYUP = 0x0002;
        
        [System.Runtime.InteropServices.DllImport("coredll.dll")]
        internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
    }
}
