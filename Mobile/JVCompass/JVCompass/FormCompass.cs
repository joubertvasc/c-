using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVGPS;
using JVGPS.Forms;
using JVUtils;

namespace JVCompass
{
    public partial class FormCompass : Compass
    {
        private string version = "0.1.0-0";
        string registryKey = "\\Software\\JV Software\\JVCompass";

        private bool inUse;
        private GPS gps;
        private GPSType gpsType;
        private string comPort;
        private string baudRate;

        public FormCompass()
        {
            InitializeComponent();
        }

        private void FormCompass_Closing(object sender, CancelEventArgs e)
        {
            CloseCompass();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            CloseCompass();
        }

        void CloseCompass()
        {
            Stop();

            SaveConfiguration();
                        
            Application.Exit();
        }

        private void FormCompass_Load(object sender, EventArgs e)
        {
            inUse = false;

            // GPS
            gps = new GPS();
            gps.GetGPSDataEvent += new GPS.GetGPSDataEventHandler(GetGPSDataEvent);
            LoadConfiguration();
            base.RedrawInfo();

            Text = Text + " " + version;
        }

        private bool Start()
        {
            // Verify if GPS is configured.
            if (gpsType == GPSType.Manual && (comPort.Equals("") || baudRate.Equals("")))
            {
                MessageBox.Show("Your GPS is not yet configured. Please select a serial port and baud rate.");
            }
            else
            {
                miConfigGPS.Enabled = false;
                miAbout.Enabled = false;

                inUse = true;
                miStart.Text = "Stop";

                // Configure and start GPS
                gps.ChangeGPSType(gpsType);

                if (gpsType == GPSType.Manual)
                {
                    gps.ComPort = Utils.ConvertStringToCOMPort(comPort);
                    gps.BaudRate = Utils.ConvertStringToBaudRate(baudRate);
                }

                base.lblFixType.Text = "Starting...";
                gps.Start();
            }

            return true;
        }

        private void Stop()
        {
            if (gps.IsStarted)
                gps.Stop();

            gpsData.IsValid = false;

            base.RedrawInfo();

            miConfigGPS.Enabled = true;
            miAbout.Enabled = true;

            miStart.Text = "Start";
            inUse = false;
        }

        private void LoadConfiguration()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey(registryKey);

            if (r != null)
            {
                try
                {
                    gpsType = ((string)r.GetValue("gpstype", "W")).Equals("W") ? GPSType.Windows : GPSType.Manual;
                    comPort = (string)r.GetValue("gpscom", "");
                    baudRate = (string)r.GetValue("gpsbaud", "");
                    miMetric.Checked = ((string)r.GetValue("System", "M")).Equals("M");
                }
                finally
                {
                    r.Close();
                }
            }
            else
            {
                gpsType = GPSType.Windows;
                comPort = "";
                baudRate = "";
                miMetric.Checked = true;
            }

            miImperial.Checked = !miMetric.Checked;
        }

        private void SaveConfiguration()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(registryKey);

            if (r != null)
            {
                r.SetValue("gpstype", (gpsType == GPSType.Windows ? "W" : "M"));
                r.SetValue("gpscom", comPort);
                r.SetValue("gpsbaud", baudRate);
                r.SetValue("system", (miMetric.Checked ? "M" : "I"));
                r.Close();
            }
        }

        private void miStart_Click(object sender, EventArgs e)
        {
            if (!inUse)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        private void miConfigGPS_Click(object sender, EventArgs e)
        {
            if (inUse)
            {
                MessageBox.Show("You must stop scanning before modify your GPS settings", "Warning");
            }
            else
            {
                JVGPS.Forms.GPSSettings settings = new JVGPS.Forms.GPSSettings();
                settings.SelectedSerialPort = comPort;
                settings.SelectedBaudRate = Utils.ConvertStringToBaudRate(baudRate);
                settings.UsingWindowsGPS = gpsType == GPSType.Windows;

                if (settings.ShowDialog() == DialogResult.OK)
                {
                    gpsType = settings.UsingWindowsGPS ? GPSType.Windows : GPSType.Manual;
                    comPort = settings.SelectedSerialPort;
                    baudRate = Utils.ConvertBaudeRateToString(settings.SelectedBaudRate);
                }
            }
        }

        private void miMetric_Click(object sender, EventArgs e)
        {
            miMetric.Checked = true;
            miImperial.Checked = false;
        }

        private void miImperial_Click(object sender, EventArgs e)
        {
            miMetric.Checked = false;
            miImperial.Checked = true;
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("JVCompass\n" +
                            "Version " + version + "\n\n" +
                            "Author: Joubert Vasconcelos\n\n" +
                            "Web: http://jvtrip.sourceforge.net\n\n" +
                            "E-Mail: joubertvasc@gmail.com", "About");
        }

        void GetGPSDataEvent(object sender, GetGPSDataEventArgs args)
        {
            base.gpsData = args.GPSData;
            base.Measurement = miMetric.Checked ? Measurement.Metric : Measurement.Imperial;
            base.RedrawInfo();
        }
    }
}