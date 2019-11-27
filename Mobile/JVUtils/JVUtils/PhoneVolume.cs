using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace JVUtils
{
    public static class PhoneVolume
    {
        public static bool SetRingerVibrate()
        {
            Kernel.SNDFILEINFO sfi = new Kernel.SNDFILEINFO();
            sfi.SstType = Kernel.SoundType.Vibrate;
            uint ret = Kernel.SndSetSound(Kernel.SoundEvent.All, ref sfi, true);
            if (ret != 0)
            {
                return false;
            }
            return true;
        }

        public static bool SetRingerOff()
        {
            Kernel.SNDFILEINFO sfi = new Kernel.SNDFILEINFO();
            sfi.SstType = Kernel.SoundType.None;
            uint ret = Kernel.SndSetSound(Kernel.SoundEvent.All, ref sfi, true);
            if (ret != 0)
            {
                return false;
            }
            return true;
        }

        public static bool SetRingerOn()
        {
            Kernel.SNDFILEINFO sfi = new Kernel.SNDFILEINFO();
            sfi.SstType = Kernel.SoundType.On;
            uint ret = Kernel.SndSetSound(Kernel.SoundEvent.All, ref sfi, true);
            if (ret != 0)
            {
                return false;
            }
            return true;
        }

        public static bool SaveSound()
        {
            uint ret = Kernel.SndGetSound(Kernel.SoundEvent.All, ref Kernel.mOldSoundFileInfo);
            if (ret != 0)
            {
                return false;
            }
            return true;
        }

        public static bool RestoreSound()
        {
            uint ret = Kernel.SndSetSound(Kernel.SoundEvent.All, ref Kernel.mOldSoundFileInfo, true);
            if (ret != 0)
            {
                return false;
            }
            return true;
        }

        public static bool GetCurrentSoundType(ref Kernel.SoundType SoundType)
        {
            Kernel.SNDFILEINFO sfi = new Kernel.SNDFILEINFO();
            uint ret = Kernel.SndGetSound(Kernel.SoundEvent.All, ref sfi);
            SoundType = sfi.SstType;
            if (ret != 0)
            {
                return false;
            }
            return true;
        }

        public static uint WaveOutGetVolume()
        {
            uint currentVolume;
            Kernel.waveOutGetVolume(IntPtr.Zero, out currentVolume);

            return currentVolume;
        }

        public static void WaveOutSetVolume(uint newVolume)
        {
            Kernel.waveOutSetVolume(IntPtr.Zero, newVolume);
        }
    }
}