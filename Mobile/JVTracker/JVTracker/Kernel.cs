using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace JVTracker
{
    public class Kernel
    {
        public static void PlayFile(string fileName, bool bSync)
        {
            if (File.Exists (fileName)) {
                if (bSync) {
                   PlaySound(fileName, IntPtr.Zero, (int)(Flags.SND_SYNC | Flags.SND_FILENAME));
                } else {
                   PlaySound(fileName, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_FILENAME));
                }
            }
        }

        public static SNDFILEINFO mOldSoundFileInfo = new SNDFILEINFO();

        public struct SNDFILEINFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPathNameNative;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayNameNative;

            public SoundType SstType;
        }

        public enum SoundEvent
        {
            All = 0,
            RingLine1,
            RingLine2,
            KnownCallerLine1,
            RoamingLine1,
            RingVoip
        }

        public enum SoundType
        {
            On = 0,
            File = 1,
            Vibrate = 2,
            None = 3
        }

        private static int CTL_CODE(int DeviceType, int Func, int Method, int Access)
        {
            return (DeviceType << 16) | (Access << 14) | (Func << 2) | Method;
        }

        private enum Flags
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */
            SND_ASYNC = 0x0001,  /* play asynchronously */
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        [DllImport("coredll.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("coredll.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        [DllImport("coredll.dll", EntryPoint = "PlaySoundW", SetLastError = true)]
        private extern static bool PlaySound(string lpszName, IntPtr hModule, int dwFlags);

        [DllImport("aygshell.dll", SetLastError = true)]
        public static extern uint SndSetSound(SoundEvent seSoundEvent, ref SNDFILEINFO pSoundFileInfo, bool fSuppressUI);

        [DllImport("aygshell.dll", SetLastError = true)]
        public static extern uint SndGetSound(SoundEvent seSoundEvent, ref SNDFILEINFO pSoundFileInfo);
    }
}