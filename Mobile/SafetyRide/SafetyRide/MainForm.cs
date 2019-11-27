using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsMobile.PocketOutlook;
using JVUtils;
using JVGPS;

namespace SafetyRide
{
    public partial class MainForm : Form
    {
        // Version control
        private string version = "0.1.0-1";
        private int termOfServiceRevisionNumber = 0;

        #region Internal Variables
        private bool inUse = false;
        private string appPath = "";
        private JVGPS.Forms.Compass compass;
        string registryKey = JVUtils.JVUtils.JVSoftwareKey + "\\SafetyRide";
        bool activated = false;
        string phone = "";
        int interval = 5;
        string message = "";
        string rideLogFile = "";
        string soundFile = "";
        DataTable dt = null;
        DataSet ds = null;

        // GPS 
        private GPS gps;
        private GPSType gpsType;
        private GPSData gpsData;
        private string comPort = "";
        private string baudRate = "";
        private double lastLatitude;
        private double lastLongitude;
        private double lastSentLatitude;
        private double lastSentLongitude;
        private double distance;

        // Battery Metter
        private BatteryMetter bm;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            DoExit();
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            DoExit();
        }

        private void lblDonate_Click(object sender, EventArgs e)
        {
            Utils.Donate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tmMessage.Enabled = false;
            miCompass.Enabled = false;

            lblCoordinates.Text = "";
            lblDistance.Text = "";
            lblSpeed.Text = "";
            lblAltitude.Text = "";
            lblGPSStatus.Text = "Off";
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!activated)
            {
                activated = true;

                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                try
                {
                    string appName = Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    appPath = appName.Substring(0, appName.LastIndexOf(@"\") + 1);
                    rideLogFile = appPath + "lastride.xml";

                    inUse = false;
                    bool bCanContinue = true;

                    // Verify if the ToS was accepted
                    if (File.Exists(appPath + "safetyride.html"))
                    {
                        bCanContinue = JVUtils.JVUtils.Get_ContractWasAccepted("SafetyRide", termOfServiceRevisionNumber);

                        if (!bCanContinue)
                        {
                            JVUtils.Forms.Contract c = new JVUtils.Forms.Contract();
                            c.ContractHTMLFileName = appPath + "safetyride.html";

                            if (c.ShowDialog() == DialogResult.OK)
                            {
                                JVUtils.JVUtils.Set_ContractAccepted("SafetyRide", termOfServiceRevisionNumber);
                                bCanContinue = true;
                            }
                        }
                    }
                    //

                    if (bCanContinue)
                    {
                        // Register current path
                        RegistryKey r = Registry.LocalMachine.CreateSubKey(registryKey);
                        if (r != null)
                        {
                            r.SetValue("path", appPath);

                            r.Close();
                        }

                        // GPS
                        gps = new GPS();
                        gps.GetGPSDataEvent += new GPS.GetGPSDataEventHandler(GetGPSDataEventHandler);
                        gpsData = new GPSData();

                        // Configure AppToDate
                        if (AppToDate.IsInstalled())
                        {
                            AppToDate.CopyConfigFile(appPath, "safetyride.xml", "safetyride.ico");
                        }

                        LoadConfiguration();

                        Debug.StartLog(ShellFolders.TempFolder + "\\safetyride.debug.txt");
                        Debug.SaveAfterEachAdd = true;
                        Debug.AddLog("SafetyRide Version: " + version + "\n" +
                                     "Instaled in: " + appPath + "\n" +
                                     "JVUtils version: " + JVUtils.JVUtils.Version + "\n" +
                                     "JVGPS version: " + gps.Version);

                        // Battery Metter
                        bm = new BatteryMetter();
                        bm.BatteryEvent += new BatteryMetter.BatteryEventHandler(BatteryEventHandler);

                        // For VGA devices, resize the battery image. not beautiful... Just temporary.
                        if (Screen.PrimaryScreen.WorkingArea.Width > 240)
                        {
                            pbBattery.Size = new Size(32, 32);
                            pbBattery.Location = new Point(pbBattery.Location.X - 16, pbBattery.Location.Y);
                        }

                        CreateDataSet();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                    Kernel.SetCursor(IntPtr.Zero);
                }

                if (phone.Trim().Equals(""))
                {
                    MessageBox.Show("You must type your preferred configuration before use the application.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    DoConfigure(true);
                }
            }
        }

        private void miStartStop_Click(object sender, EventArgs e)
        {
            if (!inUse)
                Start();
            else
                Stop();
        }

        private void miCompass_Click(object sender, EventArgs e)
        {
            compass = new JVGPS.Forms.Compass();
            compass.Measurement = (miMetric.Checked ? Measurement.Metric : Measurement.Imperial);
            compass.ShowDialog();
            compass.Dispose();
            compass = null;
        }

        private void miPreventSleepMode_Click(object sender, EventArgs e)
        {
            miPreventSleepMode.Checked = !miPreventSleepMode.Checked;
        }

        private void miDebugMode_Click(object sender, EventArgs e)
        {
            miDebugMode.Checked = !miDebugMode.Checked;

            Debug.Logging = miDebugMode.Checked;
        }

        private void miGPS_Click(object sender, EventArgs e)
        {
            if (inUse)
            {
                MessageBox.Show("You must stop your trip before modify your GPS settings", "Warning");
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
                    gps.ChangeGPSType(gpsType == GPSType.Manual ? GPSType.Manual : GPSType.Windows);
                    Debug.AddLog("GPS configuration changed to: " +
                        (gpsType == GPSType.Manual ? ("Manual " + " port " + comPort + " baudrate " + baudRate) : "Windows"));
                }
            }
        }

        private void miMetric_Click(object sender, EventArgs e)
        {
            miMetric.Checked = true;
            miImperial.Checked = false;

            RefreshAltitude();
            RefreshSpeed();
            RefreshDistace();
        }

        private void miImperial_Click(object sender, EventArgs e)
        {
            miMetric.Checked = false;
            miImperial.Checked = true;

            RefreshAltitude();
            RefreshSpeed();
            RefreshDistace();
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version " + version + "\n" +
                            "Author: Joubert Vasconcelos\n\n" +
                            "Web: http://safetyride.sourceforge.net\n" +
                            "E-Mail: joubertvasc@gmail.com", "SafetyRide");
        }

        private void miConfigMessage_Click(object sender, EventArgs e)
        {
            DoConfigure(false);
        }

        private void miExportGoogle_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                if (sfKMZ.ShowDialog() == DialogResult.OK)
                {
                    if (inUse)
                        Stop();

                    Google.CreateGoogleEarthKMZ("SafetyRide KMZ Export", sfKMZ.FileName, dt.Rows);

                    MessageBox.Show("The file was created.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("There is not log file to be exported.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void tmMessage_Tick(object sender, EventArgs e)
        {
            Debug.AddLog("Tick", true);
            tmMessage.Enabled = false;
            try
            {
                Debug.AddLog("Tick: inUse: " + (inUse ? "Yes" : "No") +
                             " lastSentLatitude: " + lastSentLatitude.ToString() +
                             " lastSentLongitude: " + lastSentLongitude.ToString() +
                             " gpsData ok? " + (gpsData != null ? "yes" : "no") +
                             " gpsData valid? " + (gpsData.IsValid ? "yes" : "no") +
                             " latitude ok? " + (lastSentLatitude != gpsData.Latitude ? "yes" : "no") +
                             " longitude ok? " + (lastSentLongitude != gpsData.Longitude ? "yes" : "no"), true);
                if (inUse && gpsData != null && gpsData.IsValid &&
                    lastSentLatitude != gpsData.Latitude && lastSentLongitude != gpsData.Longitude)
                {
                    Debug.AddLog("Tick: preparing message to send.", true);
                    SendSMSMessage(phone, message + "\n" +
                                          "lat: " + gpsData.Latitude.ToString() + "\n" +
                                          "lon: " + gpsData.Longitude.ToString() + "\n" +
                                          Google.GoogleMapsLink(gpsData.Latitude, gpsData.Longitude));

                    lastSentLatitude = gpsData.Latitude;
                    lastSentLongitude = gpsData.Longitude;
                }
            }
            finally
            {
                tmMessage.Enabled = true;
            }
        }

        private void miPlaySound_Click(object sender, EventArgs e)
        {
            miPlaySound.Checked = !miPlaySound.Checked;

            miSelectSound.Enabled = miPlaySound.Checked;
        }

        private void miSelectSound_Click(object sender, EventArgs e)
        {
            ConfigureAlarmSound();
        }

        void CreateDataSet()
        {
            Debug.AddLog("CreateDataSet", true);
            ds = new DataSet();
            dt = ds.Tables.Add();
            dt.Columns.Add("latitude", System.Type.GetType("System.String"));
            dt.Columns.Add("longitude", System.Type.GetType("System.String"));
            dt.Columns.Add("sealevelaltitude", System.Type.GetType("System.String"));

            if (File.Exists(rideLogFile))
            {
                Debug.AddLog("CreateDataSet, loading XML.", true);
                ds.ReadXml(rideLogFile);
                miExportGoogle.Enabled = true;
            }
            else
                miExportGoogle.Enabled = false;
        }

        void AddRow(double lat, double lon, double alt)
        {
            Debug.AddLog("AddRow. Lat: " + lat.ToString() + " Lon: " + lon.ToString() + " Alt: " + alt.ToString(), true);
            DataRow row;
            row = dt.NewRow();

            row["latitude"] = lat;
            row["longitude"] = lon;
            row["sealevelaltitude"] = alt;
            dt.Rows.Add(row);
        }

        void DoExit()
        {
            if (inUse)
                Stop();

            SaveConfiguration();

            Debug.EndLog();
            Debug.SaveLog();

            Application.Exit();
        }

        void BatteryEventHandler(object sender, BatteryEventArgs args)
        {
            if (args.BatteryData.IsCharging)
            {
                pbBattery.Image = ilBattery.Images[0];
            }
            else
            {
                switch (args.BatteryData.BatteryLevel)
                {
                    case BatteryLevel.VeryLow:
                        pbBattery.Image = ilBattery.Images[1];
                        break;
                    case BatteryLevel.Low:
                        pbBattery.Image = ilBattery.Images[2];
                        break;
                    case BatteryLevel.Medium:
                        pbBattery.Image = ilBattery.Images[3];
                        break;
                    case BatteryLevel.High:
                        pbBattery.Image = ilBattery.Images[4];
                        break;
                    case BatteryLevel.VeryHigh:
                        pbBattery.Image = ilBattery.Images[5];
                        break;
                }
            }
        }

        void GetGPSDataEventHandler(object sender, GetGPSDataEventArgs args)
        {
            gpsData = args.GPSData;

            if (compass != null)
            {
                compass.gpsData = gpsData;
                compass.Measurement = (miMetric.Checked ? Measurement.Metric : Measurement.Imperial);
                compass.RedrawInfo();
            }

            if (gpsData.FixType == FixType.XyD)
            {
                lblGPSStatus.Text = "2D fixed";
                lblGPSStatus.ForeColor = Color.Blue;
            }
            else if (gpsData.FixType == FixType.XyzD)
            {
                lblGPSStatus.Text = "3D fixed";
                lblGPSStatus.ForeColor = Color.Green;
            }
            else
            {
                lblGPSStatus.Text = "Not fixed";
                lblGPSStatus.ForeColor = Color.Red;
            }

            if (gpsData.IsValid)
            {
                if (lastLatitude != gpsData.Latitude && lastLongitude != gpsData.Longitude)
                {
                    if (lastLatitude != 0 && lastLongitude != 0)
                        distance = distance + Utils.DistanceTo(lastLatitude, lastLongitude, gpsData.Latitude, gpsData.Longitude);

                    AddRow(gpsData.Latitude, gpsData.Longitude, gpsData.Altitude);

                    lastLatitude = gpsData.Latitude;
                    lastLongitude = gpsData.Longitude;
                }
            }

            RefreshCoordinate();
            RefreshAltitude();
            RefreshSpeed();
            RefreshDistace();
        }

        void LoadConfiguration()
        {
            Debug.AddLog("LoadConfiguration", true);
            RegistryKey r = Registry.LocalMachine.OpenSubKey(registryKey);

            if (r != null)
            {
                try
                {
                    miDebugMode.Checked = ((string)r.GetValue("debug", "N")).Equals("Y");
                    miPreventSleepMode.Checked = ((string)r.GetValue("preventSleep", "Y")).Equals("Y");
                    miPlaySound.Checked = r.GetValue("playsound", "N").ToString() == "Y";
                    soundFile = r.GetValue("soundfile", appPath + "safetyride.wav").ToString();
                    miMetric.Checked = ((string)r.GetValue("System", "M")).Equals("M");
                    gpsType = ((string)r.GetValue("gpstype", "W")).Equals("W") ? GPSType.Windows : GPSType.Manual;
                    comPort = (string)r.GetValue("gpscom", "");
                    baudRate = (string)r.GetValue("gpsbaud", "");
                    message = r.GetValue("message", "").ToString();
                    phone = r.GetValue("phone", "").ToString();
                    try
                    {
                        interval = System.Convert.ToInt32(r.GetValue("interval", 1));
                    }
                    catch
                    {
                        interval = 1;
                    }
                }
                finally
                {
                    r.Close();
                }
            }
            else
            {
                miDebugMode.Checked = false;
                miPreventSleepMode.Checked = false;
                miMetric.Checked = true;
                gpsType = GPSType.Windows;
            }

            miImperial.Checked = !miMetric.Checked;
            miSelectSound.Enabled = miPlaySound.Checked;

            Debug.Logging = miDebugMode.Checked;

            gps.ChangeGPSType(gpsType == GPSType.Manual ? GPSType.Manual : GPSType.Windows);
            Debug.AddLog("LoadConfiguration. GPS Type: " + (gpsType == GPSType.Manual ? "Manual" : "Windows"), true);
            Debug.AddLog("LoadConfiguration. ComPort: " + comPort, true);
            Debug.AddLog("LoadConfiguration. BaudRate: " + baudRate, true);
            Debug.AddLog("LoadConfiguration. PreventSleepMode: " + (miPreventSleepMode.Checked ? "Yes" : "No"), true);
            Debug.AddLog("LoadConfiguration. System: " + (miMetric.Checked ? "Metric" : "Imperial"), true);
            Debug.AddLog("LoadConfiguration. Phone: " + phone, true);
            Debug.AddLog("LoadConfiguration. Interval: " + interval.ToString(), true);
            Debug.AddLog("LoadConfiguration. Message: " + message, true);
            Debug.AddLog("LoadConfiguration. Play Sound: " + (miPlaySound.Checked ? "Metric" : "Imperial"), true);
            Debug.AddLog("LoadConfiguration. SoundFile: " + soundFile, true);
        }

        void SaveConfiguration()
        {
            Debug.AddLog("SaveConfiguration", true);
            RegistryKey r = Registry.LocalMachine.CreateSubKey(registryKey);

            if (r != null)
            {
                r.SetValue("debug", (miDebugMode.Checked ? "Y" : "N"));
                r.SetValue("preventSleep", (miPreventSleepMode.Checked ? "Y" : "N"));
                r.SetValue("System", (miMetric.Checked ? "M" : "I"));
                r.SetValue("playsound", (miPlaySound.Checked ? "Y" : "N"));
                r.SetValue("soundfile", soundFile);
                r.SetValue("gpstype", (gpsType == GPSType.Windows ? "W" : "M"));
                r.SetValue("gpscom", comPort);
                r.SetValue("gpsbaud", baudRate);
                r.SetValue("phone", phone);
                r.SetValue("message", message);
                r.SetValue("interval", System.Convert.ToString(interval));
                r.Close();
            }
        }

        void Start()
        {
            Debug.AddLog("Start", true);

            if (File.Exists(rideLogFile))
            {
                if (MessageBox.Show("Do you want to continue last ride?", "Question", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    File.Delete(rideLogFile);
            }

            if (miPreventSleepMode.Checked)
            {
                Power.DisableSleep(true);
                Debug.AddLog("Disabled sleep mode", true);
            }

            if (File.Exists(rideLogFile))
            {
                Debug.AddLog("Start. Loading XML.", true);

                ds.ReadXml(rideLogFile);
                RecalculeDistance();
            }

            inUse = true;
            miStartStop.Text = "Stop Ride";
            miOptions.Enabled = false;
            miCompass.Enabled = true;
            miExportGoogle.Enabled = false;

            lastLatitude = 0f;
            lastLongitude = 0f;
            lastSentLatitude = 0f;
            lastSentLongitude = 0f;
            gps.Start();

            tmMessage.Interval = interval * 60000;
            tmMessage.Enabled = true;
        }

        void Stop()
        {
            Debug.AddLog("Stop", true);
            tmMessage.Enabled = false;

            if (gps.IsStarted)
                gps.Stop();

            Power.DisableSleep(false);
            Debug.AddLog("Enabled sleep mode", true);

            inUse = false;
            miStartStop.Text = "Start Ride";
            miOptions.Enabled = true;
            miCompass.Enabled = false;

            lblGPSStatus.Text = "Off";
            lblGPSStatus.ForeColor = Color.Black;

            if (dt.Rows.Count > 0)
                ds.WriteXml(rideLogFile);
            else if (File.Exists(rideLogFile))
                File.Delete(rideLogFile);

            miExportGoogle.Enabled = File.Exists(rideLogFile);

            RefreshCoordinate();
            RefreshAltitude();
            RefreshSpeed();
            RefreshDistace();
        }

        void RefreshDistace()
        {
            lblDistance.Text =
                (miMetric.Checked ?
                 Utils.FormattedValue(distance / 1000) + " km" :
                 Utils.FormattedValue(Utils.KmToMiles(distance / 1000)) + " miles");
        }

        void RefreshCoordinate()
        {
            if (!inUse)
            {
                lblCoordinates.Text = "";
            }
            else if (!gpsData.IsValid)
            {
                lblCoordinates.Text = "Not fixed/Lost signal";
                lblCoordinates.ForeColor = Color.Red;
                lblCoordinates.Font =
                    new Font(lblCoordinates.Font.Name, lblCoordinates.Font.Size, FontStyle.Regular);
            }
            else
            {
                lblCoordinates.Text =
                    System.Convert.ToString(gpsData.ShortLatitude) + ", " +
                    System.Convert.ToString(gpsData.ShortLongitude);
                lblCoordinates.ForeColor = Color.Green;
                lblCoordinates.Font =
                    new Font(lblCoordinates.Font.Name, lblCoordinates.Font.Size, FontStyle.Regular);
            }
        }

        void RefreshAltitude()
        {
            if (!inUse || !gpsData.IsValid)
            {
                lblAltitude.Text = "";
            }
            else
            {
                double altitude =
                    (miMetric.Checked ? gpsData.Altitude : Utils.MetresToFeet(gpsData.Altitude));

                lblAltitude.Text =
                    (System.Convert.ToString(altitude).Length <= 8 ?
                     System.Convert.ToString(altitude) :
                     System.Convert.ToString(altitude).Substring(0, 8)) +
                     (miMetric.Checked ? " metres" : " feets");
            }
        }

        void RefreshSpeed()
        {
            if (!inUse || !gpsData.IsValid)
            {
                lblSpeed.Text = "";
            }
            else
            {
                double speed = (miMetric.Checked ? gpsData.Speed : Utils.KmToMiles(gpsData.Speed));

                lblSpeed.Text =
                    (System.Convert.ToString(speed).Length <= 6 ?
                     System.Convert.ToString(speed) :
                     System.Convert.ToString(speed).Substring(0, 6)) +
                    (miMetric.Checked ? " Km/h" : " Miles/h");
            }
        }

        void RecalculeDistance()
        {
            distance = 0;
            double lastLatitude = 0;
            double lastLongitude = 0;

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (lastLatitude != 0 && lastLongitude != 0)
                    {
                        distance = distance +
                            Utils.DistanceTo(lastLatitude, lastLongitude,
                                             Utils.StringToDouble((string)row["latitude"]), 
                                             Utils.StringToDouble((string)row["longitude"]));
                    }

                    lastLatitude = Utils.StringToDouble((string)row["latitude"]);
                    lastLongitude = Utils.StringToDouble((string)row["longitude"]);
                }
            }
        }

        void DoConfigure(bool forced)
        {
            ConfigMessage c = new ConfigMessage();
            c.Phone = phone;
            c.Interval = interval;
            c.Message = message;
            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
            {
                phone = c.Phone;
                interval = c.Interval;
                message = c.Message;
            }
            else if (forced)
                Application.Exit();
        }

        void SendBitMessage(string telSMS, string chunk)
        {
            SmsMessage s = new SmsMessage();
            Recipient r = new Recipient("SafetyRide", telSMS);
            s.To.Add(r);

            s.Body = chunk;
            s.RequestDeliveryReport = false;
            try
            {
                Debug.AddLog("SendBitMessage: phone=" + telSMS + " Text=" + chunk, true);

                s.Send();
            }
            catch (Exception ex)
            {
                Debug.AddLog("SendBitMessage: Error while sending message: " + ex.ToString(), true);
            }
        }

        void SendSMSMessage(string telSMS, string textSMS)
        {
            Debug.AddLog("SendSMSMessage: phone=" + telSMS + " Text=" + textSMS, true);

            if (telSMS != null && !telSMS.Equals(""))
            {
                do
                {
                    if (textSMS.Length > 160)
                    {
                        SendBitMessage(telSMS, textSMS.Substring(0, 160));
                        textSMS = textSMS.Substring(160);
                    }
                    else
                    {
                        SendBitMessage(telSMS, textSMS);
                        textSMS = "";
                    }
                } while (textSMS != "");

                if (miPlaySound.Checked && !soundFile.Trim().Equals("") && File.Exists(soundFile))
                    Kernel.PlayFile(soundFile, false);
            }
        }
 
        void ConfigureAlarmSound()
        {
            JVUtils.Forms.NewOpenDialog nod = new JVUtils.Forms.NewOpenDialog();
            nod.InitialDir = @"\";
            nod.FileMask = "*.wav";
            nod.InitialFileName = soundFile;

            if (nod.ShowDialog() == DialogResult.OK)
            {
                soundFile = nod.FileName;
            }
        }
    }
}