using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IO.Serial;
using JVUtils;

namespace JVGPS
{
    public class GPS
    {
        private string version = "0.1.1-0";

        #region Internal variables
        private GPSType _gpsType;
        private bool isStarted;

        private WindowsGPS winGPS;
        private ExternalGPS extGPS;
        private GPSData gpsData;

        private bool logExtNMEAData;
        private string[] extNMEAStrings;

        private string commonGPSKey = JVUtils.JVUtils.JVSoftwareKey + "\\Common\\GPS";
        #endregion

        #region Events
        public delegate void GetGPSDataEventHandler(object sender, GetGPSDataEventArgs args);
        event GetGPSDataEventHandler getGPSDataEvent;
        public event GetGPSDataEventHandler GetGPSDataEvent
        {
            add { getGPSDataEvent += value; }
            remove { getGPSDataEvent -= value; }
        }

        public delegate void UpdateEventHandler(object sender, string args);
        event UpdateEventHandler updateEvent;
        public event UpdateEventHandler UpdateEvent
        {
            add { updateEvent += value; }
            remove { updateEvent -= value; }
        }
        
        #endregion

        #region Public properties
        /// <summary>
        /// Return true if the GPS is started
        /// </summary>
        public bool IsStarted
        {
            get { return isStarted; }
        }

        /// <summary>
        /// Return the version number of the DLL
        /// </summary>
        public string Version
        {
            get { return version; }
        }

        /// <summary>
        /// Define the type of the GPS to be used: Windows (using the Windows Intermediate Driver) or Manual (using Com port and baudrate)
        /// </summary>
        public GPSType GPSType
        {
            get { return _gpsType; }
            set { _gpsType = value; }
        }

        /// <summary>
        /// Define the COM port for external GPS. Return -1 if the GPS type is internal.
        /// </summary>
        public int ComPort
        {
            get 
            {
                if (_gpsType == GPSType.Windows)
                {
                    return -1;
                }
                else
                {
                    return extGPS.ComPort;
                }
            }
            set 
            {
                if (extGPS != null)
                {
                    extGPS.ComPort = value;
                }
            }
        }
        
        /// <summary>
        /// Define the BaudRate for external GPS. Return CBR_110 if the GPS type is internal.
        /// </summary>
        public OpenNETCF.IO.Serial.BaudRates BaudRate
        {
            get
            {
                if (_gpsType == GPSType.Windows)
                {
                    return OpenNETCF.IO.Serial.BaudRates.CBR_110;
                }
                else
                {
                    return extGPS.BaudRate;
                }
            }
            set
            {
                if (extGPS != null)
                {
                    extGPS.BaudRate = value;
                }
            }
        }

        public bool LogExtNMEAData
        {
            get { return logExtNMEAData; }
            set { logExtNMEAData = value; }
        }
        public string[] ExtNMEAStrings
        {
            get { return extNMEAStrings; }
        }
        public string CommonGPSKey
        {
            get { return commonGPSKey; }
        }
        #endregion

        #region Public declarations
        public GPS()
        {
            isStarted = false;

            gpsData = new GPSData();
            _gpsType = GPSType.Windows;

            // Windows Intermediate GPS
            winGPS = new WindowsGPS();
            winGPS.GetDataEvent += new WindowsGPS.GetDataEventHandler(WinGPS_GetDataEventHandler);

            // Manual configurated GPS
            extGPS = new ExternalGPS();
            extGPS.CheckConnectionEvent += new ExternalGPS.CheckConnectionEventHandler(extGPS_CheckConnectionEventHandler);
            extGPS.GetDataEvent += new ExternalGPS.GetDataEventHandler(extGPS_GetDataEventHandler);
            extGPS.UpdateEvent += new ExternalGPS.UpdateEventHandler(extGPS_UpdateEventHandler);
            extGPS.ComPort = 4;
            extGPS.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_57600;
        }

        /// <summary>
        /// Define the type of GPS the class will use. If IsStarted is true, this function returns false and
        /// the type is not changed.
        /// </summary>
        /// <param name="gpsType"></param>
        /// <returns></returns>
        public bool ChangeGPSType(GPSType gpsType)
        {
            if (isStarted)
            {
                return false;
            }
            else
            {
                _gpsType = gpsType;
                return true;
            }
        }

        /// <summary>
        /// Open COM port and start GPS device
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            if (!isStarted)
            {
                bool result;
                if (_gpsType == GPSType.Manual)
                {
                    extGPS.LogNMEA = logExtNMEAData;
                    result = extGPS.Start();
                }
                else
                {
                    result = winGPS.Start();
                }

                isStarted = result;
            }

            return isStarted;
        }

        /// <summary>
        /// Stop GPS device and stop COM port
        /// </summary>
        public void Stop()
        {
            if (isStarted)
            {
                isStarted = false;

                if (_gpsType == GPSType.Manual)
                {
                    extGPS.Stop();
                    extNMEAStrings = extGPS.NMEAStrings;
                }
                else
                {
                    winGPS.Stop();
                }
            }
        }

        public bool GPSPresent()
        {
            return !GetCurrentDriver().Equals("");
        }

        public string GetCurrentDriver()
        {
            string result = "";

            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\System\\CurrentControlSet\\GPS Intermediate Driver\\Drivers");

            if (r != null)
            {
                try
                {
                    result = (string)r.GetValue("CurrentDriver", "");
                }
                finally
                {
                    r.Close();
                }
            }

            return result;
        }

        public GPSData GetLastStoredPosition()
        {
            GPSData g = new GPSData();

            // Load configuration from Registry
            RegistryKey r = Registry.LocalMachine.OpenSubKey(CommonGPSKey);
            if (r != null)
            { 
                g.Latitude = System.Convert.ToDouble (r.GetValue("LastLatitude", "0"));
                g.Longitude = System.Convert.ToDouble(r.GetValue("LastLongitude", "0"));
                g.Altitude = System.Convert.ToDouble (r.GetValue("LastAltitude", "0"));
                g.Speed = System.Convert.ToDouble (r.GetValue("LastSpeed", "0"));
                g.LatitudeDMS = (string)r.GetValue("LastLatDMS", "");
                g.LongitudeDMS = (string)r.GetValue("LastLongDMS", "");
                g.GPSDateTime = System.Convert.ToDateTime (r.GetValue("LastTime", DateTime.MinValue.ToString()));
                g.SatellitesInView = System.Convert.ToInt16 (r.GetValue("Count", "0"));
                r.Close();
            }

            return g;
        }
        #endregion

        #region Manual GPS events
        private void extGPS_UpdateEventHandler(object sender, string args)
        {
            if (updateEvent != null)
                updateEvent(sender, args);
        }

        private void extGPS_CheckConnectionEventHandler(object sender, CheckConnectionEventArgs args)
        {
            if (args != null)
            {
                gpsData.IsValid = args.DeviceState.Connected;
                gpsData.SatellitesInView = args.DeviceState.SatellitesInView;

                // Call the event
                if (getGPSDataEvent != null)
                {
                    GetGPSDataEventArgs argsOut = new GetGPSDataEventArgs(gpsData);
                    getGPSDataEvent(this, argsOut);
                }
            }
        }

        private void extGPS_GetDataEventHandler(object sender, GetDataEventArgs args)
        {
            if (args != null)
            {
                gpsData.Altitude = args.GPSPosition.altitude;
                gpsData.GPSDateTime = args.GPSPosition.dateTime;
                gpsData.Latitude = args.GPSPosition.latitude;
                gpsData.Longitude = args.GPSPosition.longitude;
                gpsData.Speed = args.GPSPosition.speed;
                gpsData.IsValid = args.GPSPosition.latitude != 0 && args.GPSPosition.longitude != 0;
                gpsData.LatitudeDMS = args.GPSPosition.latitudeDMS;
                gpsData.LongitudeDMS = args.GPSPosition.longitudeDMS;
                gpsData.FixType = args.GPSPosition.fixType;
                gpsData.Satellites = args.GPSPosition.satellites;
                gpsData.Heading = args.GPSPosition.heading;

                if (gpsData.IsValid)
                {
                    StoreLastPosition(gpsData);

                    Debug.AddLog("extGPS_GetDataEventHandler: NMEA string: " + args.GPSPosition.NMEAMessage + "\n" +
                                           " lat: " + System.Convert.ToString(gpsData.Latitude) + " (" + gpsData.LatitudeDMS + ") " +
                                           " lon: " + System.Convert.ToString(gpsData.Longitude) + " (" + gpsData.LongitudeDMS + ") " +
                                           " alt: " + System.Convert.ToString(gpsData.Altitude) +
                                           " speed: " + System.Convert.ToString(gpsData.Speed));
                }
                else
                {
                    Debug.AddLog("extGPS_GetDataEventHandler: NMEA string: " + args.GPSPosition.NMEAMessage + "\n INVALID.");
                }

                // Call the event
                if (getGPSDataEvent != null)
                {
                    GetGPSDataEventArgs argsOut = new GetGPSDataEventArgs(gpsData);
                    getGPSDataEvent(this, argsOut);
                }
            }
        }
        #endregion

        #region Windows GPS events
        private void WinGPS_GetDataEventHandler(object sender, GetDataEventEventArgs args)
        {
            if (args != null)
            {
                gpsData.Altitude = args.SatelliteRecord.SeaLevelAltitude;
                gpsData.GPSDateTime = args.SatelliteRecord.Time;
                gpsData.Latitude = args.SatelliteRecord.Latitude;
                gpsData.Longitude = args.SatelliteRecord.Longitude;
                gpsData.Speed = Utils.KnotsToKm (args.SatelliteRecord.Speed);
                gpsData.IsValid = args.SatelliteRecord.Connected;
                gpsData.SatellitesInView = args.SatelliteRecord.SatellitesCount;
                gpsData.LatitudeDMS = args.SatelliteRecord.LatitudeDMS;
                gpsData.LongitudeDMS = args.SatelliteRecord.LongitudeDMS;
                gpsData.Heading = args.SatelliteRecord.Heading;
                gpsData.Satellites = args.SatelliteRecord.SatellitesArray;
                gpsData.FixType = args.SatelliteRecord.FixType;

                StoreLastPosition(gpsData);

                // Call the event
                if (getGPSDataEvent != null)
                {
                    GetGPSDataEventArgs argsOut = new GetGPSDataEventArgs(gpsData);
                    getGPSDataEvent(this, argsOut);
                }
            }
        }
        #endregion

        #region Private declarations
        private void StoreLastPosition(GPSData g)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(CommonGPSKey);

            if (r != null)
            {
                r.SetValue("LastLatitude", System.Convert.ToString(g.Latitude));
                r.SetValue("LastLongitude", System.Convert.ToString(g.Longitude));
                r.SetValue("LastAltitude", System.Convert.ToString(g.Altitude));
                r.SetValue("LastSpeed", System.Convert.ToString(g.Speed));
                r.SetValue("LastLatDMS", g.LatitudeDMS);
                r.SetValue("LastLongDMS", g.LongitudeDMS);
                r.SetValue("LastTime", g.GPSDateTime.ToString());
                r.SetValue("Count", System.Convert.ToString(g.SatellitesInView));
                r.Close();
            }
        }
        #endregion
    }

    public class GetGPSDataEventArgs : EventArgs
    {
        private GPSData gpsData;

        public GetGPSDataEventArgs(GPSData gpsData)
        {
            this.gpsData = gpsData;
        }

        public GPSData GPSData
        {
            get { return gpsData; }
        }
    }

    /// <summary>
    /// Define the type of the GPS to be used: Windows (using the Windows Intermediate Driver) or Manul (using Com port and baudrate)
    /// </summary>
    public enum GPSType
    {
        Windows = 0, // Use Windows Intermediate Driver
        Manual = 1   // Use custom COM port and Baudrate 
    }

    /// <summary>
    /// Data collected from GPS
    /// </summary>
    public class GPSData
    {
        private bool isValid;
        private DateTime dateTime;
        private double latitude;
        private double longitude;
        private double altitude;
        private string latitudeDMS;
        private string longitudeDMS;
        private double speed;
        private int satellitesInView;
        private double heading;
        private Satellite[] satellites;
        private FixType fixType;

        /// <summary>
        /// Determine if the data is valid or not
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
        /// <summary>
        /// DateTime of the position
        /// </summary>
        public DateTime GPSDateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }
        /// <summary>
        /// Latitude in long format
        /// </summary>
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        /// <summary>
        /// Longitude in long format
        /// </summary>
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        /// <summary>
        /// Return the Latitude in format DD MM SS
        /// </summary>
        public string LatitudeDMS
        {
            get { return latitudeDMS; }
            set { latitudeDMS = value; }
        }
        /// <summary>
        /// Return the Longitude in format DD MM SS
        /// </summary>
        public string LongitudeDMS
        {
            get { return longitudeDMS; }
            set { longitudeDMS = value; }
        }
        /// <summary>
        /// Latitude in short format
        /// </summary>
        public double ShortLatitude
        {
            get 
            { 
                if (System.Convert.ToString (latitude).Length > 9)
                {
                    return System.Convert.ToDouble(System.Convert.ToString (latitude).Substring(0, 9));
                } 
                else 
                {
                    return latitude; 
                }
            }
        }
        /// <summary>
        /// Longitude in short format
        /// </summary>
        public double ShortLongitude
        {
            get
            {
                if (System.Convert.ToString(longitude).Length > 9)
                {
                    return System.Convert.ToDouble(System.Convert.ToString(longitude).Substring(0, 9));
                }
                else
                {
                    return longitude;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortLatitudeDMS
        {
            get
            {
                return (latitudeDMS.Length <= 14 ? latitudeDMS : latitudeDMS.Substring(0, 13) + (char)34);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortLongitudeDMS
        {
            get
            {
                return (longitudeDMS.Length <= 14 ? longitudeDMS : longitudeDMS.Substring(0, 13) + (char)34);
            }
        }
        /// <summary>
        /// Altitude
        /// </summary>
        public double Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }
        /// <summary>
        /// Speed
        /// </summary>
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        /// <summary>
        /// Satellites in view
        /// </summary>
        public int SatellitesInView
        {
            get { return satellitesInView; }
            set { satellitesInView = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Heading
        {
            get { return heading; }
            set { heading = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Satellite[] Satellites
        {
            get { return satellites; }
            set { satellites = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public FixType FixType
        {
            get { return fixType; }
            set { fixType = value; }
        }

        public void ClearSatellites()
        {
            Array.Clear(satellites, 0, satellites.Length);
            Array.Resize(ref satellites, 0);
        }
    }
}
