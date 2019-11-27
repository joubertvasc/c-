using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using CoreDLL;
using JVUtils;

namespace MobileTracking
{
    public partial class MainForm : Form
    {
        MTCore core;
        int count;
        bool activated = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.WorkingArea.Width > 240)
            {
                pbQVGA.Visible = false;
                pbVGA.Dock = DockStyle.Top;
                pbVGA.Visible = Screen.PrimaryScreen.WorkingArea.Width > 240;
            }
            else
            {
                pbVGA.Visible = false;
                pbQVGA.Dock = DockStyle.Top;
                pbQVGA.Visible = Screen.PrimaryScreen.WorkingArea.Width <= 240;
            }

            lblProjectName.Dock = DockStyle.Top;
            lblAuthor.Dock = DockStyle.Top;

            lblAuthor.Text = "Please wait...";
            miStartStop.Enabled = false;
            miExit.Enabled = false;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!activated)
            {
                activated = true;
                lblAuthor.Text = "Author: Joubert Vasconcelos";

                string appPath = Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase);
                appPath = appPath.Substring(0, appPath.LastIndexOf(@"\") + 1);

                Debug.SaveAfterEachAdd = true;
                Debug.StartLog(appPath + "debug.txt");
                Debug.Logging = true;

                core = new MTCore();
                core.CoreGPS.GetGPSEvent += new CoreDLL.CoreGPS.GetGPSEventHandler(GPSEvent);

                tbHost.Text = core.Config.Host;
                miStartStop.Enabled = true;
                miExit.Enabled = true;

                Power.DisableSleep(true);
                Debug.AddLog("Disabled sleep mode");
            }
        }
        private void miExit_Click(object sender, EventArgs e)
        {
            DoClose();
        }

        void DoClose()
        {
            if (core.CoreGPS.InUse)
                core.CoreGPS.Stop();

            Debug.EndLog();
            Debug.SaveLog();

            Power.DisableSleep(false);
            Debug.AddLog("Enabled sleep mode");
            
            Application.Exit();
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            DoClose();
        }

        private void tmSend_Tick(object sender, EventArgs e)
        {
            tmSend.Enabled = false;
            DoSend();
        }

        private void miStart_Click(object sender, EventArgs e)
        {
            if (miStartStop.Text.Equals("Start"))
            {
                miStartStop.Text = "Stop";
                core.Config.Host = tbHost.Text;

                DoSend();
            }
            else
            {
                miStartStop.Text = "Start";
                tmSend.Enabled = true;

                if (core.CoreGPS.InUse)
                    core.CoreGPS.Stop();
            }
        }

        void DoSend()
        {
            count = 0;
            core.CoreGPS.Start();
        }
    
        void GPSEvent(object sender, GetGPSEventArgs args)
        {
            count++;
            JVGPS.GPSData data = core.ProcessEvent (count, (args == null ? null : args.GPSData));

            if (data != null)
            {
//                lbLat.Text = "Lat: " + data.ShortLatitude.ToString();
//                lbLon.Text = "Lon: " + data.ShortLongitude.ToString();
//                lbSpeed.Text = "Speed: " + data.Speed.ToString();
//                lbAlt.Text = "Alt: " + data.Altitude.ToString();
//                lbSat.Text = "Sat: " + data.SatellitesInView.ToString();
                lbNote.Text = data.ShortLatitude.ToString() + ", " +
                              data.ShortLongitude.ToString() + "; " + 
                              (core.LastCoordinateType == CoordinateType.GPS ? "GPS" : "OpenCellID");
                tmSend.Enabled = true;
            }
        }

    }
}