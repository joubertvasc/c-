using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using OpenNETCF.IO.Serial;
using JVUtils;
using JVGPS;

namespace Tester
{
    public partial class Form1 : Form
    {
        private GPS gps;
        private GPSData data;
        private bool bShowWaiting;
        private bool bClosing = false;
        private JVGPS.Forms.Compass c;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            data = new GPSData();

            for (int i = 1; i < 15; i++)
            {
                comboBoxPort.Items.Add("COM" + i);
            }
            comboBoxPort.SelectedIndex = 3;

            comboBoxBaudRate.Items.Clear();
            comboBoxBaudRate.Items.Add("2400");
            comboBoxBaudRate.Items.Add("4800");
            comboBoxBaudRate.Items.Add("9600");
            comboBoxBaudRate.Items.Add("14400");
            comboBoxBaudRate.Items.Add("19200");
            comboBoxBaudRate.Items.Add("38400");
            comboBoxBaudRate.Items.Add("56000");
            comboBoxBaudRate.Items.Add("57600");
            comboBoxBaudRate.SelectedIndex = 7;

            gps = new GPS();
            gps.GetGPSDataEvent += new GPS.GetGPSDataEventHandler(GetGPSDataEventHandler);

            Debug.StartLog("\\temp\\tester.txt");
            Debug.SaveAfterEachAdd = true;
            Debug.Logging = true;

            btCompass.Enabled = false;
        }

        private void rbUseInternal_Click(object sender, EventArgs e)
        {
            if (!bClosing)
            {
                comboBoxPort.Enabled = !rbUseInternal.Checked;
                comboBoxBaudRate.Enabled = comboBoxPort.Enabled;
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            DoClose();
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            DoClose();
        }

        private void DoClose()
        {
            bClosing = true;

            Debug.EndLog();
            Debug.SaveLog("\\temp\tester.txt");

            if (gps.IsStarted)
                gps.Stop();

            Application.DoEvents();
            Application.Exit();
        }

        private void miStart_Click(object sender, EventArgs e)
        {
            if (!gps.IsStarted)
            {
                if (!gps.ChangeGPSType(rbUseInternal.Checked ? GPSType.Windows : GPSType.Manual))
                {
                    MessageBox.Show("GPS already started.", "Error");
                }

                if (!rbUseInternal.Checked)
                {
                    gps.ComPort = comboBoxPort.SelectedIndex + 1;
                    switch (comboBoxBaudRate.SelectedIndex)
                    {
                        case 0:
                            gps.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_2400;
                            break;

                        case 1:
                            gps.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_4800;
                            break;

                        case 2:
                            gps.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_9600;
                            break;

                        case 3:
                            gps.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_14400;
                            break;

                        case 4:
                            gps.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_19200;
                            break;

                        case 5:
                            gps.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_38400;
                            break;

                        case 6:
                            gps.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_56000;
                            break;

                        default:
                            gps.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_57600;
                            break;
                    }
                }

                miStart.Text = "Stop";
                bShowWaiting = true;
                gps.LogExtNMEAData = true;
                gps.Start();

                rbUseInternal.Enabled = false;
                rbUseManualGPS.Enabled = false;
                comboBoxPort.Enabled = false;
                comboBoxBaudRate.Enabled = false;
                btCompass.Enabled = true;
            }
            else
            {
                miStart.Text = "Start";
                gps.Stop();

                if (gps.ExtNMEAStrings != null)
                {
                    StreamWriter sw = File.CreateText("\\temp\\nmea.txt");
                    foreach (string x in gps.ExtNMEAStrings)
                    {
                        sw.WriteLine(x);
                    }
                    sw.Flush();
                    sw.Close();
                }

                rbUseInternal.Enabled = true;
                rbUseManualGPS.Enabled = true;
                comboBoxPort.Enabled = rbUseManualGPS.Checked;
                comboBoxBaudRate.Enabled = rbUseManualGPS.Checked;
                btCompass.Enabled = false;
            }
        }

        void GetGPSDataEventHandler(object sender, GetGPSDataEventArgs args)
        {
            data = args.GPSData;

            if (c != null)
            {
                c.gpsData = data;
                c.RedrawInfo();
            }
            
            if (args.GPSData.IsValid)
            {
                log.Items.Add(
                    "Satellites: " + System.Convert.ToString(args.GPSData.SatellitesInView) +
                    " altitude: " + System.Convert.ToString(args.GPSData.Altitude) +
                    " speed: " + System.Convert.ToString(args.GPSData.Speed));
                log.Items.Add(
                    "Lat: " + System.Convert.ToString(args.GPSData.ShortLatitude) +
                    " lon: " + System.Convert.ToString(args.GPSData.ShortLongitude));
            }
            else
            {
                if (bShowWaiting)
                {
                    log.Items.Add("Searching satellites...");
                    bShowWaiting = false;
                }
            }
        }

        private void btCompass_Click(object sender, EventArgs e)
        {
            c = new JVGPS.Forms.Compass();
            c.ShowDialog();
            c.Dispose();
            c = null;
        }
    }
}