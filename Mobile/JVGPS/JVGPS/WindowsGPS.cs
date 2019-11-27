using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVUtils;

namespace JVGPS
{
    class WindowsGPS
    {
        #region Internal variables
        private InternalGpsDeviceState device = null;
        private InternalGpsPosition position = null;
        private SatelliteRecord sr = new SatelliteRecord();
        private System.Windows.Forms.Timer tmGetData = new System.Windows.Forms.Timer();
        int timeZoneOffSet;

        private InternapGPS gps = new InternapGPS();
        #endregion

        #region Events
        public delegate void GetDataEventHandler(object sender, GetDataEventEventArgs args);
        event GetDataEventHandler getDataEvent;
        public event GetDataEventHandler GetDataEvent
        {
            add { getDataEvent += value; }
            remove { getDataEvent -= value; }
        }
        #endregion

        #region Public Properties
        public bool IsStarted
        {
            get { return gps.Opened; }
        }
        #endregion

        #region Public Declaration
        public WindowsGPS()
        {
            timeZoneOffSet = Utils.CalcTimeZoneOffSet();
            gps.DeviceStateChanged += new DeviceStateChangedEventHandler(gps_DeviceStateChanged);
            gps.LocationChanged += new LocationChangedEventHandler(gps_LocationChanged);
//            gps.DeviceNotFound += new DeviceNotFoundEventHandler(gps_DeviceNotFound);

            tmGetData.Enabled = false;
            tmGetData.Tick += new System.EventHandler(tmGetDataTick);
        }

        ~WindowsGPS()
        {
            Stop();
        }

        public bool Start()
        {
            tmGetData.Interval = 0x1388;
            tmGetData.Enabled = true;

            if (!gps.Opened)
                gps.Open();

            return true;
        }

        public void Stop()
        {
            tmGetData.Enabled = false;

            if (gps.Opened)
                gps.Close();
        }
        #endregion

        #region Private Declaration
        private void gps_LocationChanged(object sender, InternapGPS.LocationChangedEventArgs args)
        {
            Debug.AddLog("gps_LocationChanged. args is null? " + (args == null ? "yes" : "no"), true);
            Debug.AddLog("gps_LocationChanged. Position is null? " + (args.Position == null ? "yes" : "no"), true);

            if (args != null && args.Position != null)
            {
                position = args.Position;
                Debug.AddLog("gps_LocationChanged. Before UpdateData", true);
            
                UpdateData();
            }
        }

        private void gps_DeviceStateChanged(object sender, InternapGPS.DeviceStateChangedEventArgs args)
        {
            Debug.AddLog("gps_DeviceStateChanged. args is null?" + (args == null ? "Yes" : "No"), true);
            Debug.AddLog("gps_DeviceStateChanged. DeviceState is null?" + (args.DeviceState == null ? "Yes" : "No"), true);

            if (args != null && args.DeviceState != null)
            {
                device = args.DeviceState;

                Debug.AddLog("gps_DeviceStateChanged. Before UpdateData", true);
                UpdateData();
            }
        }

/*        private void gps_DeviceNotFound(object sender, EventArgs args)
        {
            if (gps.Opened)
            {
                gps.Close();
            }

            Debug.AddLog("gps_DeviceNotFound. GPS device not found.");
        }
/**/
        private void UpdateData()
        {
            Debug.AddLog("UpdateData. gps is null? " + (gps == null ? "yes" : "no"), true);
            Debug.AddLog("UpdateData. gps is open? " + (gps.Opened ? "yes" : "no"), true);
            if (gps != null && gps.Opened)
            {
                Debug.AddLog("UpdateData. position is null? " + (position == null ? "yes" : "no"), true);
                if (position != null)
                {
                    sr.FixQuality = position.fixQuality;
                    sr.FixType = position.fixType;
                    sr.FixSelection = position.selectionType;
                    Debug.AddLog("UpdateData. FixQuality: " + (sr.FixQuality == FixQuality.Unknown ? "Unknown" : (sr.FixQuality == FixQuality.Gps ? "GPS" : "DGPS")));
                    Debug.AddLog("UpdateData. FixType: " + (sr.FixType == FixType.Unknown ? "Unknown" : (sr.FixType == FixType.XyD ? "2D" : "3D")));
                    Debug.AddLog("UpdateData. FixSelection: " + (sr.FixSelection == FixSelection.Unknown ? "Unknown" : (sr.FixSelection == FixSelection.Auto ? "Auto" : "Manual")));

                    sr.SatellitesArray = position.GetSatellitesInSolution();

                    if (sr.SatellitesArray != null && sr.SatellitesArray.Length > 0)
                    {
                        foreach (Satellite s in sr.SatellitesArray)
                        {
                            Debug.AddLog ("SatellitesInSolution: ID: " + System.Convert.ToString(s.Id) + 
                                          " Azimuth: " + System.Convert.ToString(s.Azimuth) + 
                                          " Elevation: " +System.Convert.ToString (s.Elevation) + 
                                          " Signal: " + System.Convert.ToString(s.SignalStrength));
                        }
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. LatitudeValid? " + (position.LatitudeValid ? "yes" : "no"), true);
                        if (position.LatitudeValid)
                        {
                            sr.Latitude = position.Latitude;
                            sr.LatitudeDMS = (timeZoneOffSet < 0 ? "-" : "") + position.LatitudeInDegreesMinutesSeconds;
                            Debug.AddLog("UpdateData. Latitude: " + System.Convert.ToString(sr.Latitude), true);
                            Debug.AddLog("UpdateData. LatitudeDMS: " + sr.LatitudeDMS, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get LatitudeValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. LongitudeValid? " + (position.LongitudeValid ? "yes" : "no"), true);
                        if (position.LongitudeValid)
                        {
                            sr.Longitude = position.Longitude;
                            sr.LongitudeDMS = (timeZoneOffSet < 0 ? "-" : "") + position.LongitudeInDegreesMinutesSeconds.ToString();
                            Debug.AddLog("UpdateData. Latitude: " + System.Convert.ToString(sr.Longitude), true);
                            Debug.AddLog("UpdateData. LatitudeDMS: " + sr.LongitudeDMS, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get LongitudeValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. SatellitesInSolutionValid: " + (position.SatellitesInSolutionValid ? "yes" : "no"), true);
                        Debug.AddLog("UpdateData. SatellitesInViewValid: " + (position.SatellitesInViewValid ? "yes" : "no"), true);
                        Debug.AddLog("UpdateData. SatelliteCountValid: " + (position.SatelliteCountValid ? "yes" : "no"), true);
                        if (position.SatellitesInSolutionValid &&
                            position.SatellitesInViewValid &&
                            position.SatelliteCountValid)
                        {
                            try
                            {
                                sr.SatellitesInSoluction = position.GetSatellitesInSolution().Length;
                                Debug.AddLog("UpdateData. SatellitesInSoluction: " + System.Convert.ToString(sr.SatellitesInSoluction), true);
                            }
                            catch (Exception ex)
                            {
                                Debug.AddLog("UpdateData. Error trying to get SatellitesInSolution. error: " + ex.ToString(), true);
                            }

                            try
                            {
                                sr.SatellitesinView = position.GetSatellitesInView().Length;
                                Debug.AddLog("UpdateData. SatellitesinView: " + System.Convert.ToString(sr.SatellitesinView), true);
                            }
                            catch (Exception ex)
                            {
                                Debug.AddLog("UpdateData. Error trying to get SatellitesinView. error: " + ex.ToString(), true);
                            }

                            try
                            {
                                sr.SatellitesCount = position.SatelliteCount;
                                Debug.AddLog("UpdateData. SatellitesCount: " + System.Convert.ToString(sr.SatellitesCount), true);
                            }
                            catch (Exception ex)
                            {
                                Debug.AddLog("UpdateData. Error trying to get SatellitesCount. error: " + ex.ToString(), true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get SatellitesCount. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. TimeValid? " + (position.TimeValid ? "yes" : "no"), true);
                        if (position.TimeValid)
                        {
                            sr.Time = TimeZone.CurrentTimeZone.ToLocalTime(position.Time);
                            Debug.AddLog("UpdateData. Time: " + System.Convert.ToString(sr.Time), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get TimeValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. SpeedValid? " + (position.SpeedValid ? "yes" : "no"), true);
                        if (position.SpeedValid)
                        {
                            sr.Speed = position.Speed;
                            Debug.AddLog("UpdateData. Speed: " + System.Convert.ToString(sr.Speed), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get SpeedValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. SatellitesInViewCountValid? " + (position.SatellitesInViewCountValid ? "yes" : "no"), true);
                        if (position.SatellitesInViewCountValid)
                        {
                            sr.SatellitesinViewCount = position.SatellitesInViewCount;
                            Debug.AddLog("UpdateData. SatellitesinViewCount: " + System.Convert.ToString(sr.SatellitesinViewCount), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get SatellitesInViewCountValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. EllipsoidAltitudeValid? " + (position.EllipsoidAltitudeValid ? "yes" : "no"), true);
                        if (position.EllipsoidAltitudeValid)
                        {
                            sr.EllipsoidAltitude = position.EllipsoidAltitude;
                            Debug.AddLog("UpdateData. EllipsoidAltitude: " + System.Convert.ToString(sr.EllipsoidAltitude), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get EllipsoidAltitudeValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. SeaLevelAltitudeValid? " + (position.SeaLevelAltitudeValid ? "yes" : "no"), true);
                        if (position.SeaLevelAltitudeValid)
                        {
                            sr.SeaLevelAltitude = position.SeaLevelAltitude;
                            Debug.AddLog("UpdateData. SeaLevelAltitude: " + System.Convert.ToString(sr.SeaLevelAltitude), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get SeaLevelAltitudeValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. HeadingValid? " + (position.HeadingValid ? "yes" : "no"), true);
                        if (position.HeadingValid)
                        {
                            sr.Heading = position.Heading;
                            Debug.AddLog("UpdateData. Heading: " + System.Convert.ToString(sr.Heading), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get HeadingValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. PositionDilutionOfPrecisionValid? " + (position.PositionDilutionOfPrecisionValid ? "yes" : "no"), true);
                        if (position.PositionDilutionOfPrecisionValid)
                        {
                            sr.PositionDilutionOfPrecision = position.PositionDilutionOfPrecision;
                            Debug.AddLog("UpdateData. PositionDilutionOfPrecision: " +
                                System.Convert.ToString(sr.PositionDilutionOfPrecision), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get PositionDilutionOfPrecisionValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. HorizontalDilutionOfPrecisionValid? " + (position.HorizontalDilutionOfPrecisionValid ? "yes" : "no"), true);
                        if (position.HorizontalDilutionOfPrecisionValid)
                        {
                            sr.HorizontalDilutionOfPrecision = position.HorizontalDilutionOfPrecision;
                            Debug.AddLog("UpdateData. HorizontalDilutionOfPrecision: " +
                                System.Convert.ToString(sr.HorizontalDilutionOfPrecision), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get HorizontalDilutionOfPrecisionValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
                        Debug.AddLog("UpdateData. VerticalDilutionOfPrecisionValid? " + (position.VerticalDilutionOfPrecisionValid ? "yes" : "no"), true);
                        if (position.VerticalDilutionOfPrecisionValid)
                        {
                            sr.VerticalDilutionOfPrecision = position.VerticalDilutionOfPrecision;
                            Debug.AddLog("UpdateData. VerticalDilutionOfPrecision: " +
                                System.Convert.ToString(sr.VerticalDilutionOfPrecision), true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData. Error trying to get VerticalDilutionOfPrecisionValid. error: " + ex.ToString(), true);
                    }

                    try
                    {
//                        Debug.AddLog("UpdateData. Can Process GPS command? " +
//                            (position.SatellitesInSolutionValid &&
//                             position.SatellitesInViewValid &&
//                             position.SatelliteCountValid &&
//                             position.LatitudeValid &&
//                             position.LongitudeValid &&
//                             position.TimeValid &&
//                             position.SatelliteCountValid ? "yes" : "no"), true);
//                        if (position.SatellitesInSolutionValid &&
//                            position.SatellitesInViewValid &&
//                            position.SatelliteCountValid &&
//                            position.LatitudeValid &&
//                            position.LongitudeValid &&
//                            position.TimeValid &&
//                            position.SatelliteCountValid)
//                        {
//                            SatelliteRecord.Connected = true;
//
//                            // TODO event!
//                        }

                        // At least longitude and latitude must be ok
                        Debug.AddLog("UpdateData. Can Process GPS command? " +
                            (position.LatitudeValid && position.LongitudeValid ? "yes" : "no"), true);
                        if (position.LatitudeValid && position.LongitudeValid)
                        {
                            sr.Connected = true;
                        } else
                        {
                            sr.Connected = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.AddLog("UpdateData Error. Can Process GPS command?. Error: " + ex.ToString(), true);
                    }
                }
            }
        }

        private void tmGetDataTick(object sender, EventArgs e)
        {
            // Call the event
            tmGetData.Enabled = false;
            try
            {
                if (gps.Opened && getDataEvent != null)
                {
                    GetDataEventEventArgs args = new GetDataEventEventArgs(sr);
                    getDataEvent(this, args);
                }
            }
            finally
            {
                tmGetData.Enabled = true;
            }
        }
        #endregion
    }

    public class GetDataEventEventArgs : EventArgs
    {
        private SatelliteRecord satelliteRecord;

        public GetDataEventEventArgs(SatelliteRecord satelliteRecord)
        {
            this.satelliteRecord = satelliteRecord;
        }

        public SatelliteRecord SatelliteRecord
        {
            get { return satelliteRecord; }
        }
    }
}
