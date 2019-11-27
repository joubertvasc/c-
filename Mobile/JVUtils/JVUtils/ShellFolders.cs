using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public class ShellFolders
    {
        // \Windows 
        public static string WindowsFolder
        {
            get { return GetSystemFolder("Windows"); }
        }
        // \Windows\Startup 
        public static string StartUpFolder
        {            
            get { return Environment.GetFolderPath(Environment.SpecialFolder.Startup); }
        }
        // \Program Files 
        public static string ProgramFilesFolder
        {
            get { return GetSystemFolder("Program Files"); }
        }
        // \My Documents 
        public static string MyDocumentsFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal); }
        }
        // \Windows\Start Menu 
        public static string StartMenuFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.StartMenu); }
        }
        // \Windows\Start Menu\Programs
        public static string StartMenuProgramsFolder
        {
            get
            {
                string x = "";

                try
                {
                    x = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                }
                catch 
                {
                    try
                    {
                        x = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                    }
                    catch
                    {
                        x = "\\Windows";
                    }
                }

                return x;
            }
        }
        // \Application Data
        public static string ApplicationDataFolder
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); }
        }
        // \Desktop 
        public static string DesktopFolder
        {
            get { return GetSystemFolder("Desktop"); }
        }
        // \Windows\Favorites
        public static string FavoritesFolder
        {
            get { return GetSystemFolder("Favorites"); }
        }
        // \Windows\Fonts
        public static string FontsFolder
        {
            get { return GetSystemFolder("Fonts"); }
        }
        // \Windows\Programs
        public static string ProgramsFolder
        {
            get { return GetSystemFolder("Programs"); }
        }
        // \Windows\Recent
        public static string RecentFolder
        {
            get { return GetSystemFolder("Recent"); }
        }
        // \Temp
        public static string TempFolder
        {
            get { return "\\Temp"; } // Not found in registry
        }

        static string GetSystemFolder(string key)
        {
            string result = "";

            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\System\\Explorer\\Shell folders");
            if (r != null)
            {
                result = (string)r.GetValue(key);
                r.Close();
            }

            return result;
        }
    }
}
