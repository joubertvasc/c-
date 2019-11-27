using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using JVGPS;

namespace CoreDLL
{
    public class MTCore
    {
        private string version = "0.0.0-1";
        private Configuration configuration;
        private Server server;
        private CoreGPS coreGPS;
        private CoordinateType lastCoordinateType = CoordinateType.GPS;

        public string Version
        {
            get { return version; }
        }
        public Configuration Config
        {
            get { return configuration; }
        }
        public Server Server
        {
            get { return server; }
        }
        public CoreGPS CoreGPS
        {
            get { return coreGPS; }
        }
        public CoordinateType LastCoordinateType
        {
            get { return lastCoordinateType; }
            set { lastCoordinateType = value; }
        }

        public MTCore()
        {
            configuration = new Configuration();
            server = new Server(configuration);
            coreGPS = new CoreGPS(configuration);
        }

        public GPSData ProcessEvent(int count, GPSData gpsData)
        {
            GPSData result = new GPSData();

            JVUtils.Debug.AddLog("ProcessEvent", true);
            if (gpsData != null && gpsData.IsValid)
            {
                if (coreGPS.InUse)
                    coreGPS.Stop();

                JVUtils.Debug.AddLog("ProcessEvent: sending data from GPS " + 
                    gpsData.Latitude.ToString() + ", " + gpsData.Longitude.ToString(), true);
                result = gpsData;
                LastCoordinateType = CoordinateType.GPS;

                if (!server.SendCoordinate(Phone.IMEI(),
                                           gpsData.Latitude.ToString(),
                                           gpsData.Longitude.ToString(),
                                           gpsData.Speed.ToString(),
                                           gpsData.Altitude.ToString(),
                                           gpsData.SatellitesInView.ToString(),
                                           CoordinateType.GPS))
                    MessageBox.Show(server.LastErrorMessage, "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            else
            {
                JVUtils.Debug.AddLog("ProcessEvent: coordinate not found. Counter=" + count.ToString(), true);
                if (count >= Config.MaxGPSInteractions)
                {
                    JVUtils.Debug.AddLog("ProcessEvent. Count > limit.", true);

                    if (coreGPS.InUse)
                        coreGPS.Stop();

                    LastCoordinateType = CoordinateType.OpenCellID;
                    
                    result = coreGPS.OpenCellID();

                    JVUtils.Debug.AddLog("ProcessEvent: sending data from OpenCellID " +
                        result.Latitude.ToString() + ", " + result.Longitude.ToString(), true);
                    if (!server.SendCoordinate(Phone.IMEI(),
                                               result.Latitude.ToString(),
                                               result.Longitude.ToString(),
                                               result.Speed.ToString(),
                                               result.Altitude.ToString(),
                                               result.SatellitesInView.ToString(),
                                               CoordinateType.OpenCellID))
                        MessageBox.Show(server.LastErrorMessage, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    JVUtils.Debug.AddLog("ProcessEvent. Sending null", true);
                    return null;
                }
            }

            return result;
        }
    }
}
