using System;

using System.Collections.Generic;
using System.Text;
using JVUtils;

namespace SetLoc
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get device IMEI number
            PhoneInfo pi = new PhoneInfo();
            string sIMEI = pi.GetIMEI();

            // Get current CELLID informations
            CellIDInformations cid = OpenCellID.RefreshData();

            // Call PIE with parameters
            System.Diagnostics.Process.Start(
                "http://www.setloc.nl/app.php?cell=" + cid.cellID +
                "&mcc=" + cid.mobileCountryCode +
                "&mnc=" + cid.mobileNetworkCode +
                "&signal=" + cid.signalStrength +
                "&imei=" + sIMEI, "");
        }
    }
}
