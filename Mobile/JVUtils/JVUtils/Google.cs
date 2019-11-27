using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Data;

// Google wrapper class
namespace JVUtils
{
    public static class Google
    {
        public static string GoogleMapsLink(string latitude, string longitude)
        {
            return "http://www.google.com/maps?q=" +
            Utils.ChangeChar(latitude, ',', '.') + "," +
            Utils.ChangeChar(longitude, ',', '.');
        }

        public static string GoogleMapsLink(double latitude, double longitude)
        {
            return GoogleMapsLink(latitude.ToString(), longitude.ToString());
        }

        public static string GoogleMapsLink(decimal latitude, decimal longitude)
        {
            return GoogleMapsLink(latitude.ToString(), longitude.ToString());
        }

        public static string GetMap(string latitude, string longitude)
        {
            string mapName = ShellFolders.TempFolder + 
                             "\\GoogleMaps-" + Utils.ChangeChar(latitude, ',', '.') + "-" + 
                                               Utils.ChangeChar(longitude, ',', '.') + ".jpg";

            if (File.Exists(mapName))
                File.Delete(mapName);

            if (GetGoogleMap(latitude, longitude, mapName))
                return mapName;
            else
                return null;
        }

        public static string GetMap(double latitude, double longitude)
        {
            return GetMap(latitude.ToString(), longitude.ToString());
        }

        public static string GetMap(decimal latitude, decimal longitude)
        {
            return GetMap(latitude.ToString(), longitude.ToString());
        }

        public static bool GetGoogleMap(string latitude, string longitude, string fileName)
        {
            Bitmap bm = Web.RequestImage("http://maps.google.com/staticmap?center=" +
                                          Utils.ChangeChar(latitude, ',', '.') + "," +
                                          Utils.ChangeChar(longitude, ',', '.') +
                                          "&zoom=14&size=" +
                                          System.Convert.ToString(480) + "x" +
                                          System.Convert.ToString(640) +
                                          "&maptype=mobile\\&markers=" +
                                          Utils.ChangeChar(latitude, ',', '.') + "," +
                                          Utils.ChangeChar(longitude, ',', '.') +
                                          ",bluea&key=" + JVUtils.GoogleMapsKey + "&sensor=false");

            if (bm != null)
            {
                bm.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                return true;
            }
            else
                return false;
        }

        public static bool GetGoogleMap(double latitude, double longitude, string fileName)
        {
            return GetGoogleMap(latitude.ToString(), longitude.ToString(), fileName);
        }

        public static bool GetGoogleMap(decimal latitude, decimal longitude, string fileName)
        {
            return GetGoogleMap(latitude.ToString(), longitude.ToString(), fileName);
        }

        public static bool CreateGoogleEarthKMZ(string name, string kmzFileName, DataRowCollection rows)
        {
            string kmlFileName = kmzFileName + ".kml";

            StreamWriter sw = File.CreateText(kmlFileName);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sw.WriteLine("<kml xmlns=\"http://earth.google.com/kml/2.2\">");
            sw.WriteLine("  <Document>");
            sw.WriteLine("    <name>Paths</name>");
            sw.WriteLine("    <description>RemoteTracker.</description>");
            sw.WriteLine("    <Style id=\"yellowLineGreenPoly\">");
            sw.WriteLine("      <LineStyle>");
            sw.WriteLine("        <color>7f00ffff</color>");
            sw.WriteLine("        <width>4</width>");
            sw.WriteLine("      </LineStyle>");
            sw.WriteLine("      <PolyStyle>");
            sw.WriteLine("        <color>7f00ff00</color>");
            sw.WriteLine("      </PolyStyle>");
            sw.WriteLine("    </Style>");
            sw.WriteLine("    <Placemark>");
            sw.WriteLine("      <name>RemoteTracker</name>");
            sw.WriteLine("      <description>RemoteTracker</description>");
            sw.WriteLine("      <styleUrl>#yellowLineGreenPoly</styleUrl>");
            sw.WriteLine("      <LineString>");
            sw.WriteLine("        <extrude>1</extrude>");
            sw.WriteLine("        <tessellate>1</tessellate>");
            sw.WriteLine("        <altitudeMode>absolute</altitudeMode>");
            sw.WriteLine("        <coordinates>");

            foreach (DataRow row in rows)
            {
                decimal altitude = System.Convert.ToDecimal(
                                   Utils.iif(row["sealevelaltitude"] == null ||
                                   (string)row["sealevelaltitude"] == "", "0",
                                   row["sealevelaltitude"].ToString())) + 50;

                sw.WriteLine(
                    Utils.ChangeChar((string)row["longitude"], ',', '.') + "," +
                    Utils.ChangeChar((string)row["latitude"], ',', '.') + "," +
                    Utils.ChangeChar(System.Convert.ToString(altitude), ',', '.'));
            }

            sw.WriteLine("        </coordinates>");
            sw.WriteLine("      </LineString>");
            sw.WriteLine("    </Placemark>");
            sw.WriteLine("  </Document>");
            sw.WriteLine("</kml>");

            sw.Flush();
            sw.Close();

            // Creates a new KMZ file
            ZipStorer zfw = new ZipStorer(kmzFileName, name);

            // Add the KML to zip file
            zfw.AddFile(kmlFileName, Utils.ExtractFileName(kmlFileName), "");

            // Updates and closes the KMZ file
            zfw.Close();
            File.Delete(kmlFileName);

            return true;
        }
    }
}
