using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVUtils;

namespace JVGPS.Forms
{
    public partial class Compass : Form
    {
        #region Internal variables
        const float PI = 3.141592654F;
        private double degree;
        private Satellite[] satellite;
        private FixType fixType;
        private double altitude;
        private double speed;
        private string latitude;
        private string longitude;
        private string latDMS;
        private string lonDMS;
        private Measurement measurement = Measurement.Metric;
        private int offSet = 15;
        #endregion

        #region Public Properties
        public GPSData gpsData;
        public Measurement Measurement
        {
            get { return measurement; }
            set { measurement = value; }
        }
        #endregion

        #region Public declarations
        public Compass()
        {
            InitializeComponent();
        }

        public void ClearSatellites()
        {
            if (gpsData.Satellites != null)
            {
                gpsData.ClearSatellites();
            }
        }

        public void RedrawInfo()
        {
            if (!gpsData.IsValid)
            {
                gpsData.Heading = -1;
                gpsData.FixType = FixType.Unknown;
                gpsData.Altitude = 0;
                gpsData.Speed = 0;
                gpsData.Latitude = 0;
                gpsData.Longitude = 0;
                gpsData.LatitudeDMS = "";
                gpsData.LongitudeDMS = "";
                gpsData.GPSDateTime = DateTime.MinValue;
                gpsData.SatellitesInView = 0;
                ClearSatellites();
            }

            pbCompass.Refresh();

            // Show Fix type
            if (gpsData.FixType == FixType.XyD)
            {
                lblFixType.Text = "2D Fixed";
                lblFixType.ForeColor = Color.Blue;
                lblFixType.Font = new Font(lblFixType.Font.Name, lblFixType.Font.Size, FontStyle.Regular);
            }
            else if (gpsData.FixType == FixType.XyzD)
            {
                lblFixType.Text = "3D Fixed";
                lblFixType.ForeColor = Color.Green;
                lblFixType.Font = new Font(lblFixType.Font.Name, lblFixType.Font.Size, FontStyle.Bold);
            }
            else
            {
                lblFixType.Text = "Not Fixed";
                lblFixType.ForeColor = Color.Red;
                lblFixType.Font = new Font(lblFixType.Font.Name, lblFixType.Font.Size, FontStyle.Regular);
            }

            // Show Time
            lblTime.Text = (gpsData.GPSDateTime != DateTime.MinValue ? gpsData.GPSDateTime.ToShortTimeString() : "");

            // Show Altitude
            lblAltitude.Text = "Alt: " +
                Utils.FormattedValue(measurement == Measurement.Metric ? gpsData.Altitude : Utils.MetresToFeet(gpsData.Altitude)) + " " +
                (measurement == Measurement.Metric ? "Meters" : "Feets");

            // Show speed
            lblSpeed.Text = 
                Utils.FormattedValue(measurement == Measurement.Metric ? gpsData.Speed : Utils.KmToMiles(gpsData.Speed)) + " " +
                (measurement == Measurement.Metric ? "Km/h" : "Miles/h");

            // Show coordinates
            if (gpsData.Latitude == 0 && gpsData.Longitude == 0)
            {
                lblCoordinates.Text = "";
                lblDMS.Text = "";
            }
            else
            {
                lblCoordinates.Text = System.Convert.ToString(gpsData.ShortLatitude) + ", " +
                                      System.Convert.ToString(gpsData.ShortLongitude);
                lblDMS.Text = gpsData.ShortLatitudeDMS + ", " + gpsData.ShortLongitudeDMS;
            }

            DrawGSV();
            Application.DoEvents();
        }
        #endregion

        #region Private declarations
        private void Compass_Load(object sender, EventArgs e)
        {
            lblFixType.Text = "";
            lblAltitude.Text = "";
            lblSpeed.Text = "";
            lblCoordinates.Text = "";
            lblDMS.Text = "";
            lblTime.Text = "";

            if (Screen.PrimaryScreen.WorkingArea.Height > 320)
                offSet = 30;

            gpsData = new GPSData();
            RedrawInfo();
        }

        private void pbCompass_Paint(object sender, PaintEventArgs e)
        {
            if (gpsData.Heading > -1)
            {
                Rectangle r = e.ClipRectangle;

                float fCenterX = r.Width / 2;
                float fCenterY = r.Height / 2;
                float fLength = (float)r.Height / 3 / 1.15F;
                float fRadians = ((float)(gpsData.Heading / 6) * 6 * PI / 180);

                e.Graphics.DrawLine(new Pen(Color.Red, 2),
                    System.Convert.ToInt32(fCenterX - (float)(fLength / 9 * System.Math.Sin(fRadians))),
                    System.Convert.ToInt32(fCenterY + (float)(fLength / 9 * System.Math.Cos(fRadians))),
                    System.Convert.ToInt32(fCenterX + (float)(fLength * System.Math.Sin(fRadians))),
                    System.Convert.ToInt32(fCenterY - (float)(fLength * System.Math.Cos(fRadians))));
            }
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void DrawGSV()
        {
            //Generate signal level readout
            int SatCount;
            try
            {
                SatCount = gpsData.Satellites.Length;
            }
            catch
            {
                SatCount = 0;
            }
            Bitmap imgSignals = new Bitmap(picGSVSignals.Width, picGSVSignals.Height);
            Graphics g = Graphics.FromImage(imgSignals);
            g.Clear(Color.White);
            Pen penBlack = new Pen(Color.Black, 1);
            Pen penGray = new Pen(Color.LightGray, 1);
            int iMargin = 4; //Distance to edge of image
            int iPadding = 4; //Distance between signal bars
            g.DrawRectangle(penBlack, 0, 0, imgSignals.Width - 1, imgSignals.Height - 1);

            StringFormat sFormat = new StringFormat();
            int barWidth = 1;
            if (SatCount > 0)
                barWidth = (imgSignals.Width - 2 * iMargin - iPadding * (12 /*SatCount*/ - 1)) / 12; // SatCount;

            //Draw horisontal lines
            for (int i = imgSignals.Height - 15; i > iMargin; i -= (imgSignals.Height - 15 - iMargin) / 5)
                g.DrawLine(penGray, 1, i, imgSignals.Width - 2, i);
            sFormat.Alignment = StringAlignment.Center;
            //Draw satellites
            for (int i = 0; i < SatCount; i++)
            {
                Satellite sat = gpsData.Satellites[i];
                if (sat != null)
                {
                    int startx = i * (barWidth + iPadding) + iMargin;
                    int starty = imgSignals.Height - offSet;
                    int height = (imgSignals.Height - offSet - iMargin) / 50 * sat.SignalStrength;

                    SolidBrush blueBrush = new SolidBrush(sat.SignalStrength < 10 ? Color.Red : (sat.SignalStrength < 20 ? Color.Yellow : Color.Green));
                    g.FillRectangle(blueBrush, startx, starty, barWidth, -height + 1);
                    g.DrawRectangle(penBlack, startx, starty, barWidth, -height);

                    sFormat.LineAlignment = StringAlignment.Near;
                    g.DrawString(System.Convert.ToString(sat.Id), new Font("Verdana", 9, FontStyle.Regular), new System.Drawing.SolidBrush(Color.Black), startx + barWidth / 2, imgSignals.Height - offSet, sFormat);
                    sFormat.LineAlignment = StringAlignment.Far;
                    g.DrawString(sat.SignalStrength.ToString(), new Font("Verdana", 9, FontStyle.Regular), new System.Drawing.SolidBrush(Color.Black), startx + barWidth / 2, starty - height, sFormat);
                }
            }
            picGSVSignals.Image = imgSignals;

            //Generate sky view
            Bitmap imgSkyview = new Bitmap(picGSVSkyview.Width, picGSVSkyview.Height);
            g = Graphics.FromImage(imgSkyview);
            g.Clear(Color.White);
            g.DrawEllipse(penBlack, 0, 0, imgSkyview.Width - 1, imgSkyview.Height - 1);
            g.DrawEllipse(penBlack, imgSkyview.Width / 4, imgSkyview.Height / 4, imgSkyview.Width / 2, imgSkyview.Height / 2);
            g.DrawLine(penBlack, imgSkyview.Width / 2, 0, imgSkyview.Width / 2, imgSkyview.Height);
            g.DrawLine(penBlack, 0, imgSkyview.Height / 2, imgSkyview.Width, imgSkyview.Height / 2);
            sFormat.LineAlignment = StringAlignment.Near;
            sFormat.Alignment = StringAlignment.Near;

            for (int i = 0; i < SatCount; i++)
            {
                Satellite sat = gpsData.Satellites[i];
                if (sat != null)
                {
                    double ang = 90.0 - sat.Azimuth;
                    ang = ang / 180.0 * Math.PI;
                    int x = imgSkyview.Width / 2 + (int)Math.Round((Math.Cos(ang) * ((90.0 - sat.Elevation) / 90.0) * (imgSkyview.Width / 2.0 - iMargin)));
                    int y = imgSkyview.Height / 2 - (int)Math.Round((Math.Sin(ang) * ((90.0 - sat.Elevation) / 90.0) * (imgSkyview.Height / 2.0 - iMargin)));

                    SolidBrush blueBrush = new SolidBrush(sat.SignalStrength < 7 ? Color.Red : (sat.SignalStrength < 15 ? Color.Yellow : Color.Green));

                    g.FillEllipse(blueBrush, x - 1, y - 1, 3, 3);
                    g.DrawString(System.Convert.ToString(sat.Id), new Font("Verdana", 9, FontStyle.Regular), new System.Drawing.SolidBrush(Color.Black), x, y, sFormat);
                }
            }

            picGSVSkyview.Image = imgSkyview;
        }    
    }
}