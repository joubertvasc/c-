using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using JVUtils;

namespace CoreDLL
{
    public class CoreGPS
    {
        private Configuration config;

        // GPS 
        private JVGPS.GPS gps;
        private JVGPS.GPSData gpsData;
        private double lastShortLatitude;
        private double lastShortLongitude;

        private bool inUse = false;
        public bool InUse
        {
            get { return inUse; }
        }

        // Events
        public delegate void GetGPSEventHandler(object sender, GetGPSEventArgs args);
        event GetGPSEventHandler getGPSEvent;
        public event GetGPSEventHandler GetGPSEvent
        {
            add { getGPSEvent += value; }
            remove { getGPSEvent -= value; }
        }

        // Functions
        public CoreGPS(Configuration configuration)
        {
            config = configuration;

            // GPS
            gps = new JVGPS.GPS();
            gps.GPSType = config.GPSType;
            gps.ComPort = config.ComPort;
            gps.BaudRate = Utils.ConvertStringToBaudRate(config.BaudRate);

            gps.GetGPSDataEvent += new JVGPS.GPS.GetGPSDataEventHandler(GPSDataEvent);
            gpsData = new JVGPS.GPSData();
        }

        void GPSDataEvent(object sender, JVGPS.GetGPSDataEventArgs args)
        {
            gpsData = args.GPSData;

            Debug.AddLog("GPSDataEvent. Isvalid? " + (gpsData.IsValid ? "Y" : "N"), true);

            if (gpsData.IsValid)
            {
                Debug.AddLog("GPSDataEvent. New position? " + 
                    (lastShortLatitude != gpsData.ShortLatitude &&
                     lastShortLongitude != gpsData.ShortLongitude ? "Y" : "N"), true);
                if (lastShortLatitude != gpsData.ShortLatitude &&
                    lastShortLongitude != gpsData.ShortLongitude)
                {
                    Debug.AddLog("GPSDataEvent. Custom Event is set? " +
                        (getGPSEvent != null ? "Y" : "N"), true);
                    if (getGPSEvent != null)
                    {
                        GetGPSEventArgs argsOut = new GetGPSEventArgs(gpsData);
                        getGPSEvent(this, argsOut);

                        lastShortLatitude = gpsData.ShortLatitude;
                        lastShortLongitude = gpsData.ShortLongitude;

                        Debug.AddLog("GPSDataEvent. Last coordinate: " +
                            lastShortLatitude + ", " + lastShortLongitude, true);
                    }
                }
            }
            else
            {
                if (getGPSEvent != null)
                {
                    Debug.AddLog("GPSDataEvent. Going to custom event sending NULL as coordinate.", true);
                    getGPSEvent(this, null);
                }
            }
        }

        public JVGPS.GPSData OpenCellID()
        {
            Debug.AddLog("OpenCellID", true);
            JVGPS.GPSData data = new JVGPS.GPSData();

            CellIDInformations cid = JVUtils.OpenCellID.RefreshData();
            OPENCELLIDRESULT ocr = JVUtils.OpenCellID.ConvertDataToCoordinates(cid.mobileNetworkCode,
                                                                               cid.mobileCountryCode,
                                                                               cid.cellID,
                                                                               cid.localAreaCode);

            Debug.AddLog("OpenCellID coordinate: " + ocr.Latitude + ", " + ocr.Longitude, true);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;

//            data.Latitude = System.Convert.ToDouble(Utils.ChangeChar(ocr.Latitude, '.', System.Convert.ToChar(decimalSeparator)));
//            data.Longitude = System.Convert.ToDouble(Utils.ChangeChar(ocr.Longitude, '.', System.Convert.ToChar(decimalSeparator)));
            data.Latitude = Utils.StringToDouble(ocr.Latitude);
            data.Longitude = Utils.StringToDouble(ocr.Longitude);
            data.Speed = 0;
            data.Altitude = 0;
            data.SatellitesInView = 0;

            return data;
        }

        public void Start()
        {
            if (!inUse)
            {
                Debug.AddLog("START", true);

                inUse = true;
                gps.Start();
            }
        }

        public void Stop()
        {
            if (inUse)
            {
                Debug.AddLog("STOP", true);

                gps.Stop();
                inUse = false;
            }
        }
    }

    public class GetGPSEventArgs : EventArgs
    {
        private JVGPS.GPSData gpsData;

        public GetGPSEventArgs(JVGPS.GPSData gpsData)
        {
            this.gpsData = gpsData;
        }

        public JVGPS.GPSData GPSData
        {
            get { return gpsData; }
        }
    }
}
