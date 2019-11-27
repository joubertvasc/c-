using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace JVGPS
{
    public class SatelliteRecord
    {
        #region Internal Variables
        bool connected = false;
        double latitude = 0;
        double longitude = 0;
        string longitudeDMS = "";
        string latitudeDMS = "";
        DateTime time = DateTime.MinValue;
        int satellitesCount = 0;
        double speed = 0;

        int satellitesInSoluction = 0;
        int satellitesinView = 0;
        int satellitesinViewCount = 0;
        double ellipsoidAltitude = 0;
        double seaLevelAltitude = 0;
        double heading = 0;
        double positionDilutionOfPrecision = 0;
        double horizontalDilutionOfPrecision = 0;
        double verticalDilutionOfPrecision = 0;
        FixQuality fixQuality = FixQuality.Unknown;
        FixType fixType = FixType.Unknown;
        FixSelection selectionType = FixSelection.Unknown;
        Satellite[] satellites;
        int timeZoneOffSet = 0;

        DataSet ds;
        DataTable dt;
        #endregion
        
        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public string LatitudeDMS
        {
            get { return latitudeDMS; }
            set { latitudeDMS = (TimeZoneOffSet < 0 ? "-" : "") + value; }
        }
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        public string LongitudeDMS
        {
            get { return longitudeDMS; }
            set { longitudeDMS = (TimeZoneOffSet < 0 ? "-" : "") + value; }
        }
        public double ShortLatitude
        {
            get 
            {
                string shortLatitude = System.Convert.ToString (latitude);

                if (shortLatitude.Length > 9)
                {
                    shortLatitude = shortLatitude.Substring(0, 9);
                }

                return System.Convert.ToDouble(shortLatitude);
            }
            
        }
        public double ShortLongitude
        {
            get
            {
                string shortLongitude = System.Convert.ToString (longitude);
                                
                if (shortLongitude.Length > 9)
                {
                   shortLongitude = shortLongitude.Substring(0, 9);
                }

                return System.Convert.ToDouble(shortLongitude);
            }
        }
        public string ShortLatitudeDMS
        {
            get
            {
                string shortLatitudeDMS = latitudeDMS;

                if (shortLatitudeDMS.Length > 17)
                {
                    shortLatitudeDMS = shortLatitudeDMS.Substring(0, 17);

                    if (!shortLatitudeDMS.EndsWith(@""""))
                    {
                        shortLatitudeDMS += "\"";
                    }
                }

                return shortLatitudeDMS;
            }
        }
        public string ShortLongitudeDMS
        {
            get 
            {
                string shortLongitudeDMS = longitudeDMS;

                if (shortLongitudeDMS.Length > 17)
                {
                    shortLongitudeDMS = shortLongitudeDMS.Substring(0, 17);

                    if (!shortLongitudeDMS.EndsWith(@""""))
                    {
                        shortLongitudeDMS += "\"";
                    }
                }

                return shortLongitudeDMS;            
            }
        }
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
        public int SatellitesCount
        {
            get { return satellitesCount; }
            set { satellitesCount = value; }
        }
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int SatellitesInSoluction
        {
            get { return satellitesInSoluction; }
            set { satellitesInSoluction = value; }
        }
        public int SatellitesinView
        {
            get { return satellitesinView; }
            set { satellitesinView = value; }
        }
        public int SatellitesinViewCount
        {
            get { return satellitesinViewCount; }
            set { satellitesinViewCount = value; }
        }
        public double EllipsoidAltitude
        {
            get { return ellipsoidAltitude; }
            set { ellipsoidAltitude = value; }
        }
        public double SeaLevelAltitude
        {
            get { return seaLevelAltitude; }
            set { seaLevelAltitude = value; }
        }
        public double Heading
        {
            get { return heading; }
            set { heading = value; }
        }
        public double PositionDilutionOfPrecision
        {
            get { return positionDilutionOfPrecision; }
            set { positionDilutionOfPrecision = value; }
        }
        public double HorizontalDilutionOfPrecision
        {
            get { return horizontalDilutionOfPrecision; }
            set { horizontalDilutionOfPrecision = value; }
        }
        public double VerticalDilutionOfPrecision
        {
            get { return verticalDilutionOfPrecision; }
            set { verticalDilutionOfPrecision = value; }
        }
        public DataSet CollectionSet
        {
            get { return ds; }
        }
        public DataRowCollection CollectionRows
        {
            get { return dt.Rows; }
        }
        public int TimeZoneOffSet
        {
            get { return timeZoneOffSet; }
            set { timeZoneOffSet = value; }
        }
        public FixQuality FixQuality
        {
            get { return fixQuality; }
            set { fixQuality = value; }
        }
        public FixType FixType
        {
            get { return fixType; }
            set { fixType = value; }
        }
        public FixSelection FixSelection
        {
            get { return selectionType; }
            set { selectionType = value; }
        }
        public Satellite[] SatellitesArray
        {
            get { return satellites; }
            set { satellites = value; }
        }
        
        public void Clear()
        {
            connected = false;
            latitude = 0;
            longitude = 0;
            longitudeDMS = "";
            latitudeDMS = "";
            time = DateTime.MinValue;
            speed = 0;
            satellitesInSoluction = 0;
            satellitesinView = 0;
            satellitesinViewCount = 0;
            satellitesCount = 0;
            ellipsoidAltitude = 0;
            seaLevelAltitude = 0;
            heading = 0;
            positionDilutionOfPrecision = 0;
            horizontalDilutionOfPrecision = 0;
            verticalDilutionOfPrecision = 0;
            fixQuality = FixQuality.Unknown;
            fixType = FixType.Unknown;
            selectionType = FixSelection.Unknown;

            if (satellites != null && satellites.Length > 0)
            {
                Array.Clear(satellites, 0, satellites.Length);
                Array.Resize(ref satellites, 0);
            }
        }

        public void AddToCollection ()
        {
            if (ds == null)
            {
                ds = new DataSet();
                dt = ds.Tables.Add();

                dt.Columns.Add("latitude", System.Type.GetType("System.String"));
                dt.Columns.Add("longitude", System.Type.GetType("System.String"));
                dt.Columns.Add("latitudeDMS", System.Type.GetType("System.String"));
                dt.Columns.Add("longitudeDMS", System.Type.GetType("System.String"));
                dt.Columns.Add("time", System.Type.GetType("System.String"));
                dt.Columns.Add("speed", System.Type.GetType("System.String"));
                dt.Columns.Add("satellitescount", System.Type.GetType("System.String"));
                dt.Columns.Add("satellitesinsoluction", System.Type.GetType("System.String"));
                dt.Columns.Add("satellitesinview", System.Type.GetType("System.String"));
                dt.Columns.Add("satellitesinviewcount", System.Type.GetType("System.String"));
                dt.Columns.Add("ellipsoidaltitude", System.Type.GetType("System.String"));
                dt.Columns.Add("sealevelaltitude", System.Type.GetType("System.String"));
                dt.Columns.Add("heading", System.Type.GetType("System.String"));
                dt.Columns.Add("positiondilutionofprecision", System.Type.GetType("System.String"));
                dt.Columns.Add("horizontaldilutionofprecision", System.Type.GetType("System.String"));
                dt.Columns.Add("verticaldilutionofprecision", System.Type.GetType("System.String"));
            }

            DataRow row;
            row = dt.NewRow();

            row["latitude"] = Latitude;
            row["longitude"] = Longitude;
            row["latitudeDMS"] = LatitudeDMS;
            row["longitudeDMS"] = longitudeDMS;
            row["time"] = Time;
            row["speed"] = Speed;
            row["satellitescount"] = SatellitesCount;
            row["satellitesinsoluction"] = SatellitesInSoluction;
            row["satellitesinview"] = SatellitesinView;
            row["satellitesinviewcount"] = SatellitesinViewCount;
            row["ellipsoidaltitude"] = EllipsoidAltitude;
            row["sealevelaltitude"] = SeaLevelAltitude;
            row["heading"] = Heading;
            row["positiondilutionofprecision"] = PositionDilutionOfPrecision;
            row["horizontaldilutionofprecision"] = HorizontalDilutionOfPrecision;
            row["verticaldilutionofprecision"] = VerticalDilutionOfPrecision;
            dt.Rows.Add(row);
        }

        public void ExportCollectionToXML(string fileName)
        {
            if (ds != null)
            {
                ds.WriteXml(fileName);
            }
        }
    }
}