using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WebSecurity
{
    public static class Debug
    {
        static string debug;
        static string _fileName;
        static bool bSaveAfterEachAdd = false;
        static bool bWriting = false;
        static bool bLogging = false;

        public static bool Logging
        {
            get { return bLogging; }
            set { bLogging = value; }
        }

        public static bool SaveAfterEachAdd
        {
            get { return bSaveAfterEachAdd; }
            set { bSaveAfterEachAdd = value; }
        }

        public static void StartLog()
        {
            if (bLogging)
            {
                StartLog("");
            }
        }

        public static void StartLog(string fileName)
        {
            _fileName = fileName;
            debug = "Start: " + DateTime.Now.ToString() + "\n";

            if (bSaveAfterEachAdd)
            {
                SaveLog();
            }
        }

        public static void EndLog()
        {
            if (bLogging)
            {
                debug += "End: " + DateTime.Now.ToString();

                if (bSaveAfterEachAdd)
                {
                    SaveLog();
                }
            }
        }

        public static void AddLog(string line)
        {
            if (bLogging)
            {
                debug += line + "\n";

                if (bSaveAfterEachAdd)
                {
                    SaveLog();
                }
            }
        }

        public static void AddLog(string line, bool addTimeStamp)
        {
            AddLog (DateTime.Now.ToString() + " - " + line);
        }

        public static void SaveLog()
        {
            if (bLogging)
            {
                SaveLog(_fileName);
            }
        }

        public static void SaveLog(string fileName)
        {
            if (bLogging)
            {
                if (fileName != "")
                {
                    if (!bWriting)
                    {
                        bWriting = true;
                        try
                        {
                            StreamWriter writer = File.CreateText(fileName);
                            writer.WriteLine(debug);
                            writer.Flush();
                            writer.Close();
                        }
                        catch
                        {
                        }
                        finally
                        {
                            bWriting = false;
                        }
                    }
                }
            }
        }
    }
}
