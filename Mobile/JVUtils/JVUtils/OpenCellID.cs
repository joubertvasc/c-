using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public class OpenCellID
    {
        private static string myKey = "0b3f5cd62a69e22a085fb74b985c0d03";
        private static string customKey = "";
        private static bool usingCustomKey = false;

        public static void UseCustomKey(string openCellIdKey)
        {
            usingCustomKey = true;
            customKey = openCellIdKey;
        }

        public static void UseInternalKey()
        {
            usingCustomKey = false;
            customKey = "";
        }

        private static string GetKey()
        {
            if (usingCustomKey)
            {
                return customKey;
            }
            else
            {
                return myKey;
            }
        }

        public static CellIDInformations RefreshData()
        {
            CellIDInformations cid = RIL.GetAllInformations();

            return cid;
        }

        public static string ConvertToXMLURL(string mnc, string mcc, string cellID, string lac)
        {
            string url = "http://www.opencellid.org/cell/get?mcc=" + mcc + "&mnc=" + mnc + "&cellid=" + cellID +
                   "&lac=" + lac;
            Debug.AddLog("URL to OpenCellID: " + url);
            return url;
        }

        public static string ConvertToTxtURL(string mnc, string mcc, string cellID, string lac)
        {
            string url = ConvertToXMLURL (mnc, mcc, cellID, lac) + "&fmt=txt";
            Debug.AddLog("URL to OpenCellID: " + url);
            return url;
        }

        public static OPENCELLIDRESULT ConvertDataToCoordinates(string mnc, string mcc, string cellID, string lac)
        {
            OPENCELLIDRESULT res = new OPENCELLIDRESULT();
            res.Latitude = "";
            res.Longitude = "";

            string result = Web.Request(ConvertToTxtURL(mnc, mcc, cellID, lac));

            Debug.AddLog("OpenCellID result: " + result);

            char[] delimiterChars = { ',' };
            string[] coord = result.Split(delimiterChars);

            if (coord.Length >= 1)
            {
                res.Latitude = coord[0].Trim();
                Debug.AddLog("OpenCellID latitude: " + res.Latitude);
            }
            
            if (coord.Length >= 2)
            {
                res.Longitude = coord[1].Trim();
                Debug.AddLog("OpenCellID longitude: " + res.Longitude);
            }

            return res;
        }

        public static string GetList()
        {
            string res = Web.Request("http://www.opencellid.org/measure/list?key=" + GetKey());

            if (res.Equals("") || res.Contains("API Key not know"))
            {
                return "";
            }
            else
            {
                return res;
            }
        }

        public static bool SendNewCellId(string mnc, string mcc, string cellID, string lac, string lat, string lon)
        {
            bool result = false;

            if (!mnc.Trim().Equals("") && !mcc.Trim().Equals("") && !cellID.Trim().Equals("") &&
                !lac.Trim().Equals("") && !lat.Trim().Equals("") && !lon.Trim().Equals(""))
            {
                string res = Web.Request("http://www.opencellid.org/measure/add?key=" + GetKey() +
                                         "&mnc=" + mnc.Trim() +
                                         "&mcc=" + mcc.Trim() +
                                         "&lac=" + lac.Trim() +
                                         "&cellid=" + cellID.Trim() +
                                         "&lat=" + Utils.ChangeChar (lat.Trim(), ',', '.') +
                                         "&lon=" + Utils.ChangeChar (lon.Trim(), ',', '.'));

                result = res.Contains("stat=\"ok\"");
            }

            return result;
        }
    }

    public struct OPENCELLIDRESULT
    {
        string _latitude;
        string _longitude;

        public string Latitude
        {
            get { return _latitude; }
            set { _latitude = value;  }
        }

        public string Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        public string ShortLatitude
        {
            get
            {
                if (_latitude == null)
                {
                    return "";
                }
                else
                {
                    if (_latitude.Length > 9)
                    {
                        return _latitude.Substring(0, 9);
                    }
                    else
                    {
                        return _latitude;
                    }
                }
            }
        }

        public string ShortLongitude
        {
            get
            {
                if (_longitude == null)
                {
                    return "";
                }
                else
                {
                    if (_longitude.Length > 9)
                    {
                        return _longitude.Substring(0, 9);
                    }
                    else
                    {
                        return _longitude;
                    }
                }
            }
        }
    }
}
