using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.WindowsMobile.Telephony;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;

namespace JVUtils
{
    public enum AudioNotesType
    {
        None = 0,
        Classic = 1,
        Touch = 2
    }

    public static class AudioNotes
    {
        static string audioPath = "";
        static string installDir = "";
        static string fileFormat = "";
        static AudioNotesType audioNotesType = AudioNotesType.None;

        public static string InstallDir
        {
            get
            {
                try
                {
                    if (installDir == "" || installDir == null)
                    {
                        // Is Vito Audio Notes "classic" installed?
                        RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Security\\AppInstall\\VITO Audio Notes");

                        if (r != null)
                        {
                            installDir = (string)r.GetValue("InstallDir");

                            if (Directory.Exists(installDir))
                                audioNotesType = AudioNotesType.Classic;
                            else
                                installDir = "";

                            r.Close();
                        }
                        else
                        {
                            // Is Vito Audio Notes "classic" installed (new versions)?
                            RegistryKey r3 = Registry.LocalMachine.OpenSubKey("\\Security\\AppInstall\\VITO AudioNotes");

                            if (r3 != null)
                            {
                                installDir = (string)r3.GetValue("InstallDir");

                                if (Directory.Exists(installDir))
                                    audioNotesType = AudioNotesType.Classic;
                                else
                                    installDir = "";

                                r3.Close();
                            }
                            else
                            {
                                // Is Vito Audio Notes Touch installed?
                                RegistryKey r2 = Registry.LocalMachine.OpenSubKey("\\Security\\AppInstall\\VITO Audio Notes Touch");

                                if (r2 != null)
                                {
                                    installDir = (string)r2.GetValue("InstallDir");

                                    if (Directory.Exists(installDir))
                                        audioNotesType = AudioNotesType.Touch;
                                    else
                                        installDir = "";

                                    r2.Close();
                                }
                                else
                                {
                                    // None of them
                                    installDir = "";
                                    audioNotesType = AudioNotesType.None;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    installDir = "";
                    audioNotesType = AudioNotesType.None;
                }

                return installDir;
            }
        }

        public static bool AudioNotesInstalled
        {
            get
            {
                return (InstallDir != "");
            }
        }

        public static AudioNotesType AudioNotesType
        {
            get 
            { 
                string x;
                if (installDir.Equals(""))
                    x = InstallDir; 

                return audioNotesType; 
            }
        }

        public static string AudioPath
        {
            get
            {
                string result = "";

                if (!audioPath.Equals(""))
                {
                    result = audioPath;
                }
                else
                {
                    audioPath = "";

                    if (!InstallDir.Equals(""))
                    {
                        if (audioNotesType == AudioNotesType.Classic)
                        {
                            if (File.Exists(installDir + "\\Settings.ini"))
                            {
                                using (StreamReader sr = new StreamReader(installDir + "\\Settings.ini"))
                                {
                                    // Process every line in the file
                                    for (String Line = sr.ReadLine(); Line != null; Line = sr.ReadLine())
                                    {
                                        if (Line.Length > 6 && Line.Substring(0, 6).Equals("Format"))
                                        {
                                            if (System.Convert.ToInt16(Line.Substring(7)) < 4)
                                            {
                                                fileFormat = "mp3";
                                            }
                                            else
                                            {
                                                fileFormat = "wav";
                                            }
                                        }

                                        if (Line.Length > 10 && Line.Substring(0, 9).Equals("StorePath"))
                                        {
                                            audioPath = Line.Substring(10);
                                            result = audioPath;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            RegistryKey r = Registry.CurrentUser.OpenSubKey("\\Software\\VITO\\Audio Notes Touch");

                            if (r != null)
                            {
                                audioPath = (string)r.GetValue("Path");
                                audioPath = audioPath.Substring(0, audioPath.LastIndexOf("\\"));

                                if (audioPath.LastIndexOf("\\Incoming") > 0)
                                    audioPath = audioPath.Substring(0, audioPath.LastIndexOf("\\Incoming"));

                                if (audioPath.LastIndexOf("\\Outgoing") > 0)
                                    audioPath = audioPath.Substring(0, audioPath.LastIndexOf("\\Outgoing"));

                                if (!Directory.Exists(audioPath))
                                    audioPath = "";

                                if (!audioPath.Equals(""))
                                {
                                    fileFormat = (string)r.GetValue("Format");

                                    if (fileFormat.ToLower().StartsWith("mp3"))
                                        fileFormat = "mp3";
                                    else
                                        fileFormat = "wav";
                                }
                                else
                                    fileFormat = "";

                                r.Close();
                            }

                            result = audioPath;
                        }
                    }
                }

                return result;
            }
        }

        public static string[] InBoxAudioFiles
        {
            get
            {
                string[] result = new string[0];

                if (Directory.Exists(AudioPath + "\\Incoming"))
                    result = Directory.GetFiles(AudioPath + "\\Incoming", "*." + fileFormat);

                return result;
            }
        }

        public static string[] OutBoxAudioFiles
        {
            get
            {
                string[] result = new string[0];

                if (Directory.Exists(AudioPath + "\\Outgoing"))
                    result = Directory.GetFiles(AudioPath + "\\Outgoing", "*." + fileFormat);

                return result;
            }
        }
    }
}
