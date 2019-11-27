using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVGPS;
using JVUtils;

namespace CoreDLL
{
    public class Configuration
    {
        string host;
        GPSType gpsType;
        int comPort;
        string baudRate;
        int maxGPSInteractions;

        public string Host
        {
            get { return host; }
            set { host = value; }
        }
        public GPSType GPSType
        {
            get { return gpsType; }
            set { gpsType = value; }
        }
        public int ComPort
        {
            get { return comPort; }
            set { comPort = value; }
        }
        public string BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }
        public int MaxGPSInteractions
        {
            get { return maxGPSInteractions; }
            set { maxGPSInteractions = value; }
        }

        public Configuration()
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            Host = "http://joubertvasc.dnsalias.net:8080";
            GPSType = GPSType.Windows;
            ComPort = 4;
            BaudRate = "9600";
            MaxGPSInteractions = 50;

            Debug.AddLog("Configuration.LoadConfiguration: host=" + Host +
                          " GPSType=" + GPSType.ToString() +
                          " COMPort=" + ComPort.ToString() +
                          " Baudrate=" + BaudRate +
                          " GPSInteractions=" + MaxGPSInteractions.ToString(), true);
        }
    }
}
