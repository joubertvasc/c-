using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVUtils;

namespace CoreDLL
{
    public class Server
    {
        private Configuration config;
        private string lastErrorMessage;

        public string LastErrorMessage
        {
            get { return lastErrorMessage; }
        }

        public Server(Configuration configuration)
        {
            config = configuration;
        }

        public bool SendCoordinate(string imei, 
                                   string latitude, string longitude,
                                   string speed, string altitude,
                                   string satelittes, CoordinateType type)
        {
            string url = config.Host + 
                         "/setposition.php?id=" + Utils.StringToBase64(imei) + 
                         "&la=" + Utils.ChangeChar(latitude, ',', '.') +
                         "&lo=" + Utils.ChangeChar(longitude, ',', '.') +
                         "&s=" + Utils.ChangeChar(speed, ',', '.') +
                         "&a=" + Utils.ChangeChar(altitude, ',', '.') +
                         "&sc=" + satelittes +
                         "&t=" + (type == CoordinateType.GPS ? "G" : 
                                  (type == CoordinateType.OpenCellID ? "O" : "C"));
            Debug.AddLog("SendCoordinate: IMEI=" + imei + " URL=" + url, true);

            string result = Web.Request(url);
            Debug.AddLog("SendCoordinate: result=" + result, true);

            if (result.ToLower().Equals("ok"))
            {
                lastErrorMessage = "";
                return true;
            }
            else
            {
                lastErrorMessage = result;

                if (lastErrorMessage.Equals(""))
                {
                    lastErrorMessage = "Error connecting to host.";
                    Debug.AddLog("SendCoordinate: " + lastErrorMessage, true);
                }

                return false;
            }
        }
    }
}
