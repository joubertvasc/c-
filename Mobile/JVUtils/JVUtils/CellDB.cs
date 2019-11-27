using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace JVUtils
{
    public class CellDB
    {
        private static string myUser = "remotetracke";
        private static string myHash = "81fa26c71dd994e30b231d0f33c313a2";
        private static string customUser = "";
        private static string customHash = "";
        private static bool usingCustomHash = false;

        public static void UseCustomHash(string user, string hash)
        {
            usingCustomHash = true;
            customUser = user;
            customHash = hash;
        }

        public static void UseInternalHash()
        {
            usingCustomHash = false;
            customUser = "";
            customHash = "";
        }

        public static string GetCellDBURL(string mnc, string mcc, string cellID, string lac)
        {
            string url = "http://celldb.org/api/?method=celldb.getcell" +
                    "&username=" + (usingCustomHash ? customUser : myUser) + 
                    "&hash=" + (usingCustomHash ? customHash : myHash) + 
                    "&mcc=" + mcc +
                    "&mnc=" + mnc +
                    "&lac=" + lac +
                    "&cellid=" + cellID;

            Debug.AddLog("URL to CellDB: " + url);
            return url;
        }

        public static CELLDBRESULT[] GetCoordinates(string mnc, string mcc, string cellID, string lac)
        {
            return GetCoordinates(mnc, mcc, cellID, lac, "");
        }

        public static CELLDBRESULT[] GetCoordinates(string mnc, string mcc, string cellID, string lac, string signalStrength)
        {
            CELLDBRESULT[] res = new CELLDBRESULT[0];

            string result = Web.Request(GetCellDBURL(mnc, mcc, cellID, lac));
            string data = System.DateTime.Now.ToString();
            data = Utils.ChangeChar(data, '/', '-');
            data = Utils.ChangeChar(data, ':', '-');
            string tmpFile = ShellFolders.TempFolder + "\\CellDB-" + data + ".XML";

            StreamWriter sw = new StreamWriter(tmpFile);
            sw.Write(result);
            sw.Flush();
            sw.Close();

            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(tmpFile);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = new CELLDBRESULT();
                    res[res.Length - 1].Latitude = System.Convert.ToDouble(row["latitude"]);
                    res[res.Length - 1].Longitude = System.Convert.ToDouble(row["longitude"]);
                    res[res.Length - 1].Signal = System.Convert.ToInt16(row["signalstrength"]);
                }

                if (!signalStrength.Equals(""))
                {
                    CELLDBRESULT[] res2 = new CELLDBRESULT[0];

                    foreach (CELLDBRESULT c in res)
                    {
                        if (c.Signal.ToString().Equals(signalStrength))
                        {
                            Array.Resize(ref res2, res2.Length + 1);
                            res2[res2.Length - 1] = new CELLDBRESULT();
                            res2[res2.Length - 1].Latitude = c.Latitude;
                            res2[res2.Length - 1].Longitude = c.Longitude;
                            res2[res2.Length - 1].Signal = c.Signal;
                            break;
                        }
                    }

                    if (res2.Length > 0)
                        return res2;
                    else
                        return res;
                }
                else
                    return res;
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }

        public static bool SendNewCellId(string mnc, string mcc, string cellID, string lac, string lat, string lon, string signal)
        {
            bool result = false;

            if (!mnc.Trim().Equals("") && !mcc.Trim().Equals("") && !cellID.Trim().Equals("") &&
                !lac.Trim().Equals("") && !lat.Trim().Equals("") && !lon.Trim().Equals("") &&
                !signal.Trim().Equals("") && System.Convert.ToInt16(signal) > 0)
            {
                string res = Web.Request("http://celldb.org/api/?method=celldb.addcell" + 
                    "&username=" + (usingCustomHash ? customUser : myUser) + 
                    "&hash=" + (usingCustomHash ? customHash : myHash) + 
                    "&mcc=" + mcc +
                    "&mnc=" + mnc +
                    "&lac=" + lac +
                    "&cellid=" + cellID +
                    "&latitude=" + Utils.ChangeChar (lat, ',', '.') +
                    "&longitude=" + Utils.ChangeChar (lon, ',', '.') +
                    "&timestamp=" + System.Convert.ToString(Utils.UnixTime (DateTime.UtcNow)) +
                    "&signalstrength=" + signal);

                result = res.Contains("<insert>OK</insert>");
            }

            return result;
        }
    }

    public class CELLDBRESULT
    {
        private double latitude;
        private double longitude;
        private int signal;

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        public int Signal
        {
            get { return signal; }
            set { signal = value; }
        }
    }
}
