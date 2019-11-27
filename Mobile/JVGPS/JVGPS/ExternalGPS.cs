using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Collections;
using OpenNETCF;
using OpenNETCF.IO.Serial;
using JVUtils;

namespace JVGPS
{
    class ExternalGPS
    {
        #region Interval Variables

        private OpenNETCF.IO.Serial.Port port;
        private string bufferRemaining;
        private GPSPosition position;
        private bool isStarted;
        private int portNumber;
        private OpenNETCF.IO.Serial.BaudRates baudRate;
        private string[] _NMEAStrings = new string[0];
        private int satInView;

        private bool logNMEA = false;

        private System.Windows.Forms.Timer tmGetData = new System.Windows.Forms.Timer();

        NmeaInterpreter nmeaInterpreter;
        #endregion

        #region Public properties
        public int ComPort
        {
            get { return portNumber; }
            set { portNumber = value; }
        }
        public OpenNETCF.IO.Serial.BaudRates BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }
        public bool LogNMEA
        {
            get { return logNMEA; }
            set { logNMEA = value; }
        }
        public string[] NMEAStrings
        {
            get { return _NMEAStrings; }
        }
        public bool IsStarted
        {
            get { return isStarted; }
        }
        #endregion

        #region Events
        public delegate void UpdateEventHandler(object sender, string args);
        public delegate void CheckConnectionEventHandler(object sender, CheckConnectionEventArgs args);
        public delegate void GetDataEventHandler(object sender, GetDataEventArgs args);

        event UpdateEventHandler updateEvent;
        event CheckConnectionEventHandler checkConnectionEvent;
        event GetDataEventHandler getDataEvent;
        
        public event UpdateEventHandler UpdateEvent
        {
            add { updateEvent += value; }
            remove { updateEvent -= value; }
        }
        public event CheckConnectionEventHandler CheckConnectionEvent
        {
            add { checkConnectionEvent += value; }
            remove { checkConnectionEvent -= value; }
        }
        public event GetDataEventHandler GetDataEvent
        {
            add { getDataEvent += value; }
            remove { getDataEvent -= value; }
        }
        #endregion

        #region Public declaration
        public ExternalGPS()
        {
            port = new OpenNETCF.IO.Serial.Port("COM4:");
            port.Settings.Parity = 0;
            port.Settings.StopBits = 0;
            port.DataReceived += DataReceived;
            
            portNumber = 4;
            baudRate = OpenNETCF.IO.Serial.BaudRates.CBR_57600;
            isStarted = false;

            tmGetData.Enabled = false;
            tmGetData.Tick += new System.EventHandler(tmGetDataTick);

//            position.satellites = new Satellite[];

            // NMEA events
            nmeaInterpreter = new NmeaInterpreter();
            nmeaInterpreter.DateTimeChanged += new NmeaInterpreter.DateTimeChangedEventHandler(DateTimeChangedEvent);
            nmeaInterpreter.FixLost += new NmeaInterpreter.FixLostEventHandler(FixLostEvent);
            nmeaInterpreter.FixObtained += new NmeaInterpreter.FixObtainedEventHandler(FixObtainedEvent);
            nmeaInterpreter.PositionReceived += new NmeaInterpreter.PositionReceivedEventHandler(PositionReceivedEvent);
            nmeaInterpreter.SatelliteReceived += new NmeaInterpreter.SatelliteReceivedEventHandler(SatelliteReceivedEvent);
            nmeaInterpreter.SpeedReceived += new NmeaInterpreter.SpeedReceivedEventHandler(SpeedReceivedEvent);
            nmeaInterpreter.AltitudeChanged += new NmeaInterpreter.AltitudeChangedEventHandler(AltitudeChangedEvent);
            nmeaInterpreter.BearingReceived += new NmeaInterpreter.BearingReceivedEventHandler(BearingReceivedEvent);
        }

        ~ExternalGPS()
        {
            if (port != null && port.IsOpen)
                Stop();
        }

        public bool Start()
        {
            Array.Clear(_NMEAStrings, 0, _NMEAStrings.Length);

            Debug.AddLog("START: Starting...");
            Application.DoEvents();
            port.Close();

            if (!OpenCOMPort())
            {
                Debug.AddLog("START: GPS Not found.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Stop()
        {
            tmGetData.Enabled = false;

            if (port.IsOpen)
            {
                Debug.AddLog("STOP: Closing port...");
                port.Close();
                Debug.AddLog("STOP: Stopped.");
            }

            isStarted = false;
            Application.DoEvents();
        }

        public string[] Devices()
        {
            // lists all modems on the device
            ArrayList al = new ArrayList();
            RegistryKey defkey;
            string[] keyNames;
            string keyvalue = "";
            string strport = "";

            // now get the active
            defkey = Registry.LocalMachine.CreateSubKey(@"Drivers\Active");

            keyNames = defkey.GetSubKeyNames();
            foreach (string x in keyNames)
            {

                try
                {
                    keyvalue = defkey.CreateSubKey(x).GetValue("Key").ToString();
                    strport = defkey.CreateSubKey(x).GetValue("Name").ToString();

                    // fudge to remove rubbish off the end of the string
                    strport = strport.Substring(0, strport.IndexOf("'"));

                    RegistryKey thekey = Registry.LocalMachine.CreateSubKey(keyvalue);

                    if (strport.StartsWith("COM"))
                        al.Add(strport);
                }
                catch
                {

                }
            }
            return (string[])al.ToArray(typeof(string));
        }
        #endregion

        #region Private declaration
        private bool OpenCOMPort()
        {
            try
            {
                port.PortName = "COM" + System.Convert.ToString(portNumber) + ":";
                port.Settings.BaudRate = baudRate;

                isStarted = true;

                Debug.AddLog("OpenCOMPort: starting timers.");
                tmGetData.Interval = 0x1388;
                tmGetData.Enabled = true;

                Debug.AddLog ("Port: " + port.PortName + " BR: " + port.Settings.BaudRate.ToString());
                Debug.AddLog("Before open.");
                port.Open();
                Debug.AddLog("After open.");
            }
            catch (Exception exception)
            {
                if (exception.Message.StartsWith("CreateFile Failed"))
                {
                    Debug.AddLog("OpenCOMPort: Unable to open port " + port.PortName);
                }
                else
                {
                    Debug.AddLog("OpenCOMPort: " + exception.Message);
                }

                Stop();
                return false;
            }

            return true;
        }

        private void DataReceived()
        {
            if (port.InBufferCount > 0)
            {
                try
                {
                    byte[] bytes = new byte[0x800];
                    bytes = port.Input;

                    string str = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    
                    bufferRemaining = bufferRemaining + str;

                    for (int i = bufferRemaining.IndexOf("\r\n"); i != -1; i = bufferRemaining.IndexOf("\r\n"))
                    {
                        string nmea = bufferRemaining.Substring(0, i);
                        bufferRemaining = bufferRemaining.Substring(i + 2, (bufferRemaining.Length - i) - 2);

                        if (LogNMEA)
                        {
                            Array.Resize(ref _NMEAStrings, _NMEAStrings.Length + 1);
                            _NMEAStrings[_NMEAStrings.Length - 1] = nmea;
                        }

                        nmeaInterpreter.Parse(nmea);
                    }
                }
                catch (Exception exception)
                {
                    DoUpdate(exception.Message);
                }
            }
        }

        private void DoUpdate(string message)
        {
            Debug.AddLog("DoUpdate: " + message);

            if (updateEvent != null)
                updateEvent(this, message);
        }

        private void tmGetDataTick(object sender, EventArgs e)
        {
            // Call the event
            if (getDataEvent != null)
            {
                if (position.latitude == 0)
                    position.latitudeDMS = "";

                if (position.longitude == 0)
                    position.longitudeDMS = "";

                if (position.latitude != 0 && position.longitude != 0)
                {
                    Coordinate c = new Coordinate();
                    c.SetD((float)position.latitude, (float)position.longitude);

                    float latd, latm, lats, lond, lonm, lons;
                    bool north, east;
                    c.GetDMS(out latd, out latm, out lats, out north, out lond, out lonm, out lons, out east);
                    position.latitudeDMS = (!north ? "-" : "") +
                                           System.Convert.ToString(latd) + "d " +
                                           System.Convert.ToString(latm) + (char)39 + " " +
                                           System.Convert.ToString(lats) + (char)34;
                    position.longitudeDMS = (!east ? "-" : "") +
                                            System.Convert.ToString(lond) + "d " +
                                            System.Convert.ToString(lonm) + (char)39 + " " +
                                            System.Convert.ToString(lons) + (char)34;
                }

                GetDataEventArgs args = new GetDataEventArgs(position);
                getDataEvent(this, args);
            }
        }
        #endregion

        #region NMEA Data Parsed Events
        void PositionReceivedEvent(string latitude, string longitude)
        {
            position.latitude = System.Convert.ToDouble (latitude);
            position.longitude = System.Convert.ToDouble(longitude);

            Coordinate c = new Coordinate();

            c.SetD((float)position.latitude, (float)position.longitude);

            float latd, latm, lats, lond, lonm, lons;
            bool north, east;
            c.GetDMS(out latd, out latm, out lats, out north, out lond, out lonm, out lons, out east);
            position.latitudeDMS = (!north ? "-" : "") +
                                   System.Convert.ToString(latd) + "d " +
                                   System.Convert.ToString(latm) + (char)39 + " " +
                                   System.Convert.ToString(lats) + (char)34;
            position.longitudeDMS = (!east ? "-" : "") +
                                    System.Convert.ToString(lond) + "d " +
                                    System.Convert.ToString(lonm) + (char)39 + " " +
                                    System.Convert.ToString(lons) + (char)34;
            Debug.AddLog("PositionReceivedEvent: " + latitude + ", " + longitude + " (" +
                position.latitudeDMS + ", " + position.longitudeDMS + ")");
        }

        void AltitudeChangedEvent(double altitude)
        {
            position.altitude = altitude;
            Debug.AddLog("AltitudeChangedEvent: " + System.Convert.ToString(altitude));
        }

        void DateTimeChangedEvent(System.DateTime dateTime)
        {
            position.dateTime = dateTime;
            Debug.AddLog("DateTimeChangedEvent: " + dateTime.ToString());
        }

        void SpeedReceivedEvent(double speed)
        {
            position.speed = Utils.MilesToKm(speed);
            Debug.AddLog("SpeedReceivedEvent: " + System.Convert.ToString(position.speed));
        }

        void FixObtainedEvent(FixType fixType)
        {
            position.fixType = fixType;
            Debug.AddLog("FixObtainedEvent: " + (fixType == FixType.Unknown ? "Not Fixed" : (fixType == FixType.XyD ? "2D" : "3D")));
        }

        void FixLostEvent()
        {
            position.fixType = FixType.Unknown;
            Debug.AddLog("FixLostEvent");
        }

        void SatelliteReceivedEvent(Satellite[] Satellites)
        {
            position.satellites = Satellites;

            if (Debug.Logging)
            {
                int i = 0;
                foreach (Satellite s in Satellites)
                {
                    if (s == null)
                    {
                        Debug.AddLog("SatelliteReceivedEvent: Sat " + System.Convert.ToString(i) + " NULL", true);
                    }
                    else
                    {
                        Debug.AddLog("SatelliteReceivedEvent: Sat " + System.Convert.ToString(i) + 
                                     " ID: " + System.Convert.ToString(s.Id) +
                                     " Azimuth: " + System.Convert.ToString(s.Azimuth) +
                                     " Elevation: " + System.Convert.ToString(s.Elevation) +
                                     " Signal: " + System.Convert.ToString(s.SignalStrength), true);
                    }

                    i++;
                }
            }
        }

        void BearingReceivedEvent(double bearing)
        {
            position.heading = bearing;

            Debug.AddLog("BearingReceivedEvent: Bearing" + System.Convert.ToString(bearing));
        }
        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GPSPosition
    {
        public string NMEAMessage;
        public DateTime dateTime;
        public double latitude;
        public double longitude;
        public string latitudeDMS;
        public string longitudeDMS;
        public double heading;
        public double altitude;
        public double speed;
        public FixType fixType;
        public Satellite[] satellites;
    }

    public class GpsDeviceState
    {
        private bool connected;
        private int satellitesInView;

        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        public int SatellitesInView
        {
            get { return satellitesInView; }
            set { satellitesInView = value; }
        }
    }

    public class CheckConnectionEventArgs: EventArgs
    {
        private GpsDeviceState deviceState;

        public CheckConnectionEventArgs(GpsDeviceState deviceState)
        {
            this.deviceState = deviceState;
        }

        public GpsDeviceState DeviceState
        {
            get { return deviceState; }
        }
    }

    public class GetDataEventArgs: EventArgs
    {
        private GPSPosition gpsPosition;

        public GetDataEventArgs(GPSPosition gpsPosition)
        {
            this.gpsPosition = gpsPosition;
        }

        public GPSPosition GPSPosition
        {
            get { return gpsPosition; }
        }
    }
}
