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
using System.IO.Compression;
using System.Xml;
using Microsoft.WindowsMobile.Status;
using JVUtils;
using JVUtils.Forms;
using JVGPS;
using JVGPS.Forms;

namespace JVTrip
{
    public partial class MainForm : Form
    {
        // Version control
        private string version = "0.1.0-4";
        private int termOfServiceRevisionNumber = 0;

        #region Internal Variables
        private bool inUse;
        private string appPath;
        private JVGPS.Forms.Compass compass;
        string registryKey = "\\Software\\JV Software\\JVTrip";
        Int64 selectedTrip = -1;
        bool activated = false;

        // GPS 
        private GPS gps;
        private GPSType gpsType;
        private GPSData gpsData;
        private string comPort = "";
        private string baudRate = "";
        private double lastShortLatitude;
        private double lastShortLongitude;

        // Battery Metter
        private BatteryMetter bm;

        // Database
        private JVTDataBase db = null;
        private TripDS tripDS;
        private CoordinatesDS coordinatesDS;
        private NotesDS notesDS;
        private CostsDS costsDS;
        private PicturesDS picturesDS;
        #endregion

        public MainForm()
        {
            InitializeComponent();
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
                    inUse = false;
                    bool bCanContinue = true;

                    // Verify if the ToS was accepted
                    if (File.Exists(appPath + "jvtrip.html"))
                    {
                        bCanContinue = JVUtils.JVUtils.Get_ContractWasAccepted("JVTrip",
                            termOfServiceRevisionNumber);

                        if (!bCanContinue)
                        {
                            JVUtils.Forms.Contract c = new JVUtils.Forms.Contract();
                            c.ContractHTMLFileName = appPath + "jvtrip.html";

                            if (c.ShowDialog() == DialogResult.OK)
                            {
                                JVUtils.JVUtils.Set_ContractAccepted("JVTrip", termOfServiceRevisionNumber);
                                bCanContinue = true;
                            }
                        }
                    }
                    //

                    if (bCanContinue)
                    {
                        // GPS
                        gps = new GPS();
                        gps.GetGPSDataEvent += new GPS.GetGPSDataEventHandler(GetGPSDataEventHandler);
                        gpsData = new GPSData();

                        // Configure AppToDate
                        if (AppToDate.IsInstalled())
                        {
                            AppToDate.CopyConfigFile(appPath, "jvtrip.xml", "Road.ico");
                        }

                        LoadConfiguration();

                        Debug.StartLog(appPath + "jvtrip.txt");
                        Debug.SaveAfterEachAdd = true;
                        Debug.AddLog("JVTrip Version: " + version + "\n" +
                                     "Instaled in: " + appPath + "\n" +
                                     "JVUtils version: " + JVUtils.JVUtils.Version + "\n" +
                                     "JVSQL version: " + JVSQL.JVSQL.Version + "\n" +
                                     "JVGPS version: " + gps.Version);

                        // Create database and load data
                        db = new JVTDataBase();
                        db.OpenDataBase(version);
                        tripDS = new TripDS();
                        tripDS.DB = db;
                        tripDS.TableName = "trip";
                        coordinatesDS = new CoordinatesDS();
                        coordinatesDS.DB = db;
                        coordinatesDS.TableName = "coordinates";
                        notesDS = new NotesDS();
                        notesDS.DB = db;
                        notesDS.TableName = "notes";
                        costsDS = new CostsDS();
                        costsDS.DB = db;
                        costsDS.TableName = "costs";
                        picturesDS = new PicturesDS();
                        picturesDS.DB = db;
                        picturesDS.TableName = "pictures";

                        LoadData();

                        // Battery Metter
                        bm = new BatteryMetter();
                        bm.BatteryEvent += new BatteryMetter.BatteryEventHandler(BatteryEventHandler);

                        // For VGA devices, resize the battery image. not beautiful... Just temporary.
                        if (Screen.PrimaryScreen.WorkingArea.Width > 240)
                        {
                            pbBattery.Size = new Size(32, 32);
                            pbBattery.Location = new Point(pbBattery.Location.X - 16, pbBattery.Location.Y);
                        }
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
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Version: " + version;
            lblCoordinates.Text = "";
            lblCost.Text = "";
            lblDistance.Text = "";
            lblSpeed.Text = "";
            lblAltitude.Text = "";
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            CloseTrip();
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            CloseTrip();
        }

        private void miMainScreen_Click(object sender, EventArgs e)
        {
        }

        private void lblURL_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(lblURL.Text, "");
        }

        private void lblContact_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:joubertvasc.gmail.com", "");
        }

        private void lblDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=2138678", "");
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

        private void miAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("JVTrip\n" +
                            "Version " + version + "\n\n" +
                            "Author: Joubert Vasconcelos\n\n" +
                            "Web: http://jvtrip.sourceforge.net\n\n" +
                            "E-Mail: joubertvasc@gmail.com", "About");
        }

        private void cbTrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSelectedTrip();
        }

        private void miStartStop_Click(object sender, EventArgs e)
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

        private void miAddTrip_Click(object sender, EventArgs e)
        {
            AddNewTrip();
        }

        void CloseTrip()
        {
            Stop();

            if (db != null)
                db.CloseDataBase();

            SaveConfiguration();

            Debug.EndLog();
            Debug.SaveLog();

            Application.Exit();
        }

        void PopulateTripCombo()
        {
            cbTrips.Items.Clear();

            foreach (DataRow row in tripDS.DataTable.Rows)
            {
                cbTrips.Items.Add(new TripString(System.Convert.ToInt64(row["id"]), (string)row["nmtrip"]));
            }
        }

        void LoadConfiguration()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey(registryKey);

            if (r != null)
            {
                try
                {
                    miDebugMode.Checked = ((string)r.GetValue("debug", "N")).Equals("Y");
                    miPreventSleepMode.Checked = ((string)r.GetValue("preventSleep", "Y")).Equals("Y");
                    miMetric.Checked = ((string)r.GetValue("System", "M")).Equals("M");
                    gpsType = ((string)r.GetValue("gpstype", "W")).Equals("W") ? GPSType.Windows : GPSType.Manual;
                    comPort = (string)r.GetValue("gpscom", "");
                    baudRate = (string)r.GetValue("gpsbaud", "");
                    selectedTrip = System.Convert.ToInt64(r.GetValue("trip", "-1"));
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
                selectedTrip = -1;
            }

            miImperial.Checked = !miMetric.Checked;

            Debug.Logging = miDebugMode.Checked;

            gps.ChangeGPSType(gpsType == GPSType.Manual ? GPSType.Manual : GPSType.Windows);
            Debug.AddLog("GPS Type: " + (gpsType == GPSType.Manual ? "Manual" : "Windows"));
        }

        void SaveConfiguration()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(registryKey);

            r.SetValue("debug", (miDebugMode.Checked ? "Y" : "N"));
            r.SetValue("preventSleep", (miPreventSleepMode.Checked ? "Y" : "N"));
            r.SetValue("System", (miMetric.Checked ? "M" : "I"));
            r.SetValue("gpstype", (gpsType == GPSType.Windows ? "W" : "M"));
            r.SetValue("gpscom", comPort);
            r.SetValue("gpsbaud", baudRate);
            r.SetValue("trip", System.Convert.ToString(selectedTrip));
            r.Close();
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

        private void miCompass_Click(object sender, EventArgs e)
        {
            compass = new JVGPS.Forms.Compass();
            compass.Measurement = (miMetric.Checked ? Measurement.Metric : Measurement.Imperial);
            compass.ShowDialog();
            compass.Dispose();
            compass = null;
        }

        private void miBackup_Click(object sender, EventArgs e)
        {
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (db != null)
                    db.CloseDataBase();

                string[] dataFile = new string[1];
                dataFile[0] = db.DataBaseFileName;
                JVUtils.Compress.Zip.SimpleZip.Compress(dataFile, saveDialog.FileName);

                if (db.OpenDataBase(version))
                {
                    LoadData();
                }
            }
        }

        private void miRestore_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string dataBaseFileName = db.DataBaseFileName;

                if (db != null)
                    db.CloseDataBase();

                JVUtils.Compress.Zip.SimpleZip.Decompress(openDialog.FileName, dataBaseFileName);

                if (db.OpenDataBase(version))
                {
                    LoadData();
                }
            }
        }

        private void miClear_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult =
                MessageBox.Show("Do you really want to clear all history?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dlgResult == DialogResult.Yes)
            {
                picturesDS.DeleteAll();
                costsDS.DeleteAll();
                notesDS.DeleteAll();
                coordinatesDS.DeleteAll();
                tripDS.DeleteAll();
                LoadData();
            }
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

        void ChangeSelectedTrip()
        {
            selectedTrip = -1;

            if (cbTrips.SelectedIndex > -1)
            {
                TripString ts = (TripString)cbTrips.Items[cbTrips.SelectedIndex];

                coordinatesDS.SelectAll("id=" + System.Convert.ToString(ts.ID), "coordinate");
                notesDS.SelectAll("id=" + System.Convert.ToString(ts.ID), "note");
                costsDS.SelectAll("id=" + System.Convert.ToString(ts.ID), "cost");
                picturesDS.SelectAll("id=" + System.Convert.ToString(ts.ID), "picture");

                selectedTrip = ts.ID;
            }
            else
            {
                coordinatesDS.SelectAll("id=-1", "coordinate");
                notesDS.SelectAll("id=-1", "note");
                costsDS.SelectAll("id=-1", "cost");
                picturesDS.SelectAll("id=-1", "picture");
            }

            RefreshDistace();
            RefreshTotalCost();
        }

        void SelectTripByID(Int64 trip)
        {
            cbTrips.SelectedIndex = -1;

            for (int i = 0; i < cbTrips.Items.Count; i++)
            {
                TripString ts = (TripString)cbTrips.Items[i];
                if (ts.ID == trip)
                {
                    cbTrips.SelectedIndex = i;
                    break;
                }
            }
        }

        void LoadData()
        {
            tripDS.SelectAll("", "nmtrip");
            PopulateTripCombo();

            SelectTripByID(selectedTrip);

            if (cbTrips.SelectedIndex == -1)
                ChangeSelectedTrip();

            RefreshCoordinate();
            RefreshAltitude();
            RefreshSpeed();
            RefreshDistace();
            RefreshTotalCost();
        }

        void RefreshDistace()
        {
            lblDistance.Text =
                (miMetric.Checked ?
                 Utils.FormattedValue(coordinatesDS.Distance / 1000) + " km" :
                 Utils.FormattedValue(Utils.KmToMiles(coordinatesDS.Distance / 1000)) + " miles");
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

        void RefreshTotalCost()
        {
            lblCost.Text = Utils.FormattedValue(costsDS.TotalCost);
        }

        void AskForNewTrip()
        {
            if (tripDS.DataTable.Rows.Count == 0)
            {
                DialogResult dr = MessageBox.Show(
                    "You don't have any trip yet. Do you want to add a new trip now? ",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (dr == DialogResult.Yes)
                {
                    AddNewTrip();
                }
            }
            else
            {
                MessageBox.Show("Please select a trip first.", "Error");
            }
        }

        void Start()
        {
            if (selectedTrip == -1)
            {
                AskForNewTrip();
            }
            else
            {
                if (miPreventSleepMode.Checked)
                {
                    Power.DisableSleep(true);
                    Debug.AddLog("Disabled sleep mode");
                }

                inUse = true;
                miStartStop.Text = "Pause Trip";
                cbTrips.Enabled = false;
                miAddTrip.Enabled = false;
                miDatabase.Enabled = false;
                miOptions.Enabled = false;
                miCompass.Enabled = true;
                miWhereAmI.Enabled = true;

                lastShortLatitude = 0f;
                lastShortLongitude = 0f;
                gps.Start();
            }
        }

        void Stop()
        {
            if (gps.IsStarted)
                gps.Stop();

            Power.DisableSleep(false);
            Debug.AddLog("Enabled sleep mode");

            inUse = false;
            miStartStop.Text = "Start Trip";
            cbTrips.Enabled = true;
            miAddTrip.Enabled = true;
            miDatabase.Enabled = true;
            miOptions.Enabled = true;
            miCompass.Enabled = false;
            miWhereAmI.Enabled = false;

            RefreshCoordinate();
            RefreshAltitude();
            RefreshSpeed();
            RefreshDistace();
            RefreshTotalCost();
        }

        void AddNewTrip()
        {
            TripForm tf = new TripForm();
            tf.Data(db, tripDS);
            tf.ShowDialog();

            Int64 oldSelected = selectedTrip;
            PopulateTripCombo();

            SelectTripByID(oldSelected);
            ipMain.Enabled = false;

            LoadData();
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

            if (gpsData.IsValid)
            {
                if (lastShortLatitude != gpsData.ShortLatitude &&
                    lastShortLongitude != gpsData.ShortLongitude)
                {
                    coordinatesDS.Add(selectedTrip, gpsData.Latitude, gpsData.Longitude, gpsData.Altitude, gpsData.Speed);
                }
            }

            RefreshCoordinate();
            RefreshAltitude();
            RefreshSpeed();
            RefreshDistace();
        }

        private void miAddNote_Click(object sender, EventArgs e)
        {
            if (selectedTrip == -1)
            {
                AskForNewTrip();
            }
            else
            {
                NoteForm nf = new NoteForm();
                if (gpsData.IsValid)
                {
                    nf.Data(db, notesDS, selectedTrip, gpsData.Latitude, gpsData.Longitude);
                }
                else
                {
                    nf.Data(db, notesDS, selectedTrip, 0d, 0d);
                }

                nf.ShowDialog();
                ipMain.Enabled = false;
                Refresh();
            }
        }

        private void miAddCost_Click(object sender, EventArgs e)
        {
            if (selectedTrip == -1)
            {
                AskForNewTrip();
            }
            else
            {
                CostForm cf = new CostForm();
                if (gpsData.IsValid)
                {
                    cf.Data(db, costsDS, selectedTrip, gpsData.Latitude, gpsData.Longitude);
                }
                else
                {
                    cf.Data(db, costsDS, selectedTrip, 0d, 0d);
                }

                cf.ShowDialog();
                ipMain.Enabled = false;

                RefreshTotalCost();
                Refresh();
            }
        }

        private void miAddPicture_Click(object sender, EventArgs e)
        {
            if (selectedTrip == -1)
            {
                AskForNewTrip();
            }
            else
            {
                PictureForm pf = new PictureForm();
                if (gpsData.IsValid)
                {
                    pf.Data(db, picturesDS, selectedTrip, gpsData.Latitude, gpsData.Longitude);
                }
                else
                {
                    pf.Data(db, picturesDS, selectedTrip, 0d, 0d);
                }

                pf.ShowDialog();
                ipMain.Enabled = false;
                Refresh();
            }
        }

        string CreateKMZ(string kml)
        {
            string fnz = Utils.ChangeFileExt(kml, "kmz");
            
            // Creates a new KMZ file
            ZipStorer zfw = new ZipStorer(fnz, "JVTrip");

            // Add the KML to zip file
            zfw.AddFile(kml, Utils.ExtractFileName(kml), "");

            // Add images
            foreach (DataRow row in picturesDS.DataTable.Rows)
            {
                if (File.Exists((string)row["depathpicture"]))
                {
                    zfw.AddFile((string)row["depathpicture"], Utils.ExtractFileName((string)row["depathpicture"]), "");
                }
            }

            // Updates and closes the KMZ file
            zfw.Close();

            return fnz;
        }

        void CreateKML(string fileName)
        {
            DataRow rowTrip = tripDS.FindTripByID(selectedTrip);
            StreamWriter sw = File.CreateText(fileName);

            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sw.WriteLine("<kml xmlns=\"http://earth.google.com/kml/2.2\">");
            sw.WriteLine("  <Document>");
            sw.WriteLine("    <name>Paths</name>");
            sw.WriteLine("    <description>" + (string)rowTrip["nmtrip"] + "</description>");
            sw.WriteLine("	<Style id=\"yellowLineGreenPoly\">");
            sw.WriteLine("		<LineStyle>");
            sw.WriteLine("			<color>7f00ffff</color>");
            sw.WriteLine("			<width>4</width>");
            sw.WriteLine("		</LineStyle>");
            sw.WriteLine("		<PolyStyle>");
            sw.WriteLine("			<color>7f00ff00</color>");
            sw.WriteLine("		</PolyStyle>");
            sw.WriteLine("	</Style>");
            sw.WriteLine("	<StyleMap id=\"msn_camera\">");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>normal</key>");
            sw.WriteLine("			<styleUrl>#sn_camera</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>highlight</key>");
            sw.WriteLine("			<styleUrl>#sh_camera</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("	</StyleMap>");
            sw.WriteLine("	<StyleMap id=\"msn_post_office0\">");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>normal</key>");
            sw.WriteLine("			<styleUrl>#sn_post_office</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>highlight</key>");
            sw.WriteLine("			<styleUrl>#sh_post_office</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("	</StyleMap>");
            sw.WriteLine("	<StyleMap id=\"msn_camera0\">");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>normal</key>");
            sw.WriteLine("			<styleUrl>#sn_camera</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>highlight</key>");
            sw.WriteLine("			<styleUrl>#sh_camera</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("	</StyleMap>");
            sw.WriteLine("	<Style id=\"sn_camera\">");
            sw.WriteLine("		<IconStyle>");
            sw.WriteLine("			<scale>1.2</scale>");
            sw.WriteLine("			<Icon>");
            sw.WriteLine("				<href>http://maps.google.com/mapfiles/kml/shapes/camera.png</href>");
            sw.WriteLine("			</Icon>");
            sw.WriteLine("			<hotSpot x=\"0.5\" y=\"0\" xunits=\"fraction\" yunits=\"fraction\"/>");
            sw.WriteLine("		</IconStyle>");
            sw.WriteLine("	</Style>");
            sw.WriteLine("	<Style id=\"sn_post_office\">");
            sw.WriteLine("		<IconStyle>");
            sw.WriteLine("			<scale>1.2</scale>");
            sw.WriteLine("			<Icon>");
            sw.WriteLine("				<href>http://maps.google.com/mapfiles/kml/shapes/post_office.png</href>");
            sw.WriteLine("			</Icon>");
            sw.WriteLine("			<hotSpot x=\"0.5\" y=\"0\" xunits=\"fraction\" yunits=\"fraction\"/>");
            sw.WriteLine("		</IconStyle>");
            sw.WriteLine("	</Style>");
            sw.WriteLine("	<Style id=\"sh_post_office\">");
            sw.WriteLine("		<IconStyle>");
            sw.WriteLine("			<scale>1.4</scale>");
            sw.WriteLine("			<Icon>");
            sw.WriteLine("				<href>http://maps.google.com/mapfiles/kml/shapes/post_office.png</href>");
            sw.WriteLine("			</Icon>");
            sw.WriteLine("			<hotSpot x=\"0.5\" y=\"0\" xunits=\"fraction\" yunits=\"fraction\"/>");
            sw.WriteLine("		</IconStyle>");
            sw.WriteLine("	</Style>");
            sw.WriteLine("	<Style id=\"sh_camera\">");
            sw.WriteLine("		<IconStyle>");
            sw.WriteLine("			<scale>1.4</scale>");
            sw.WriteLine("			<Icon>");
            sw.WriteLine("				<href>http://maps.google.com/mapfiles/kml/shapes/camera.png</href>");
            sw.WriteLine("			</Icon>");
            sw.WriteLine("			<hotSpot x=\"0.5\" y=\"0\" xunits=\"fraction\" yunits=\"fraction\"/>");
            sw.WriteLine("		</IconStyle>");
            sw.WriteLine("	</Style>");
            sw.WriteLine("	<StyleMap id=\"msn_post_office\">");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>normal</key>");
            sw.WriteLine("			<styleUrl>#sn_post_office</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>highlight</key>");
            sw.WriteLine("			<styleUrl>#sh_post_office</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("	</StyleMap>");
            sw.WriteLine("	<Style id=\"sh_dollar\">");
            sw.WriteLine("		<IconStyle>");
            sw.WriteLine("			<scale>1.4</scale>");
            sw.WriteLine("			<Icon>");
            sw.WriteLine("				<href>http://maps.google.com/mapfiles/kml/shapes/dollar.png</href>");
            sw.WriteLine("			</Icon>");
            sw.WriteLine("			<hotSpot x=\"0.5\" y=\"0\" xunits=\"fraction\" yunits=\"fraction\"/>");
            sw.WriteLine("		</IconStyle>");
            sw.WriteLine("		<ListStyle>");
            sw.WriteLine("		</ListStyle>");
            sw.WriteLine("	</Style>");
            sw.WriteLine("	<StyleMap id=\"msn_dollar\">");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>normal</key>");
            sw.WriteLine("			<styleUrl>#sn_dollar</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("		<Pair>");
            sw.WriteLine("			<key>highlight</key>");
            sw.WriteLine("			<styleUrl>#sh_dollar</styleUrl>");
            sw.WriteLine("		</Pair>");
            sw.WriteLine("	</StyleMap>");
            sw.WriteLine("	<Style id=\"sn_dollar\">");
            sw.WriteLine("		<IconStyle>");
            sw.WriteLine("			<scale>1.2</scale>");
            sw.WriteLine("			<Icon>");
            sw.WriteLine("				<href>http://maps.google.com/mapfiles/kml/shapes/dollar.png</href>");
            sw.WriteLine("			</Icon>");
            sw.WriteLine("			<hotSpot x=\"0.5\" y=\"0\" xunits=\"fraction\" yunits=\"fraction\"/>");
            sw.WriteLine("		</IconStyle>");
            sw.WriteLine("		<ListStyle>");
            sw.WriteLine("		</ListStyle>");
            sw.WriteLine("	</Style>");
            sw.WriteLine("    <Placemark>");
            sw.WriteLine("      <name>" + (string)rowTrip["nmtrip"] + "</name>");
            sw.WriteLine("      <description>" + (string)rowTrip["nmtrip"] + "</description>");
            sw.WriteLine("      <styleUrl>#yellowLineGreenPoly</styleUrl>");
            sw.WriteLine("      <LineString>");
            sw.WriteLine("        <extrude>1</extrude>");
            sw.WriteLine("        <tessellate>1</tessellate>");
            sw.WriteLine("        <altitudeMode>relativeToGround</altitudeMode>");
            sw.WriteLine("        <coordinates>");

            // Export rote
            foreach (DataRow row in coordinatesDS.DataTable.Rows)
            {
                sw.WriteLine(
                    Utils.ChangeChar(System.Convert.ToString(row["longitude"]), ',', '.') + "," +
                    Utils.ChangeChar(System.Convert.ToString(row["latitude"]), ',', '.') + "," +
                    Utils.ChangeChar(System.Convert.ToString((double)row["altitude"] + 50), ',', '.'));
            }

            sw.WriteLine("        </coordinates>");
            sw.WriteLine("      </LineString>");
            sw.WriteLine("    </Placemark>");
            
            // Export notes
            foreach (DataRow row in notesDS.DataTable.Rows)
            {
                if ((double)row["latitude"] != 0 && (double)row["longitude"] != 0)
                {
                    sw.WriteLine(
                        "<Placemark>" +
                        "  <name>" + Utils.GetFirstChars((string)row["denote"], 20) + "</name>" +
                        "  <description>" + ((DateTime)row["dtcreated"]).ToString() + " - " +
                                            (string)row["denote"] + "</description>" +
                        "  <styleUrl>#msn_post_office0</styleUrl>" +
                        "  <Point>" +
                        "    <coordinates>" +
                                Utils.ChangeChar(System.Convert.ToString(row["longitude"]), ',', '.') + "," +
                                Utils.ChangeChar(System.Convert.ToString(row["latitude"]), ',', '.') + ",0" +
                        "    </coordinates>" +
                        "  </Point>" +
                        "</Placemark>");
                }
            }

            // Export Costs
            foreach (DataRow row in costsDS.DataTable.Rows)
            {
                if ((double)row["latitude"] != 0 && (double)row["longitude"] != 0)
                {
                    sw.WriteLine(
                        "<Placemark>" +
                        "  <name>" + Utils.GetFirstChars((string)row["decost"], 20) + "</name>" +
                        "  <description>" + ((DateTime)row["dtcreated"]).ToString() + " - Value: " +
                                            System.Convert.ToString(row["vlcost"]) + " - " +
                                            (string)row["decost"] + "</description>" +
                        "  <styleUrl>#msn_dollar</styleUrl>" +
                        "  <Point>" +
                        "    <coordinates>" +
                                Utils.ChangeChar(System.Convert.ToString(row["longitude"]), ',', '.') + "," +
                                Utils.ChangeChar(System.Convert.ToString(row["latitude"]), ',', '.') + ",0" +
                        "    </coordinates>" +
                        "  </Point>" +
                        "</Placemark>");
                }
            }

            // Export files
            foreach (DataRow row in picturesDS.DataTable.Rows)
            {
                if ((double)row["latitude"] != 0 && (double)row["longitude"] != 0)
                {
                    sw.WriteLine(
                        "<Placemark>" +
                        "  <name>" + Utils.GetFirstChars((string)row["depicture"], 20) + "</name>" +
                        "  <description><![CDATA[<div align=\"center\" dir=\"ltr\"><table width=\"500\">" +
                           "<tr><td colspan=\"2\"><h2><font size=\"5\">" +
                           "<b>" + ((DateTime)row["dtcreated"]).ToString() + " - " +
                                   (string)row["depicture"] + 
                           "</b></font></h2></td></tr><tr><td colspan=\"2\"><img src=\"" +
                           Utils.ExtractFileName((string)row["depathpicture"]) + 
                           "\" width=\"500\" height=\"375\"/></td></tr><tr><td><p align=\"left\">" +
                           "<font size=\"4\"><a href=\"http://jvtrip.sourceforge.net\">By JVTrip</a></font>" +
                           "</p></td><td><p align=\"right\"></p></td></tr><tr><td><p align=\"left\">" +
                           "</p></td><td><p align=\"right\"><b></b></p></td></tr></table>" +
                           "<img src=\"http://mw2.google.com/mw-earth-vectordb/pics/stat.gif?" +
                           "n=panoramio&id=928611&v=20081114&x=n&lod=17&lat" +
                                    Utils.ChangeChar(System.Convert.ToString(row["latitude"] ), ',', '.') +
                           "&lon" + Utils.ChangeChar(System.Convert.ToString(row["longitude"]), ',', '.') +
                           " width=\"1\" height=\"1\"></div>]]></description>" +
                        "  <styleUrl>#msn_camera0</styleUrl>" +
                        "  <Point>" +
                        "    <coordinates>" +
                                Utils.ChangeChar(System.Convert.ToString(row["longitude"]), ',', '.') + "," +
                                Utils.ChangeChar(System.Convert.ToString(row["latitude"]), ',', '.') + ",0" +
                        "    </coordinates>" +
                        "  </Point>" +
                        "</Placemark>");
                }
            }

            sw.WriteLine("  </Document>");
            sw.WriteLine("</kml>");

            sw.Flush();
            sw.Close();
        }

        private void miExportGoogle_Click(object sender, EventArgs e)
        {
            if (selectedTrip == -1)
            {
                AskForNewTrip();
            }
            else
            {
                if (coordinatesDS.DataTable.Rows.Count == 0)
                {
                    MessageBox.Show(
                       "The selected trip does not have any coordinate stored.",
                       "Warning",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Asterisk,
                       MessageBoxDefaultButton.Button1);
                }
                else
                {
                    if (sfgKMZ.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Application.DoEvents();
                        try
                        {
                            string kml = Utils.ChangeFileExt(sfgKMZ.FileName, "kml");
                            CreateKML(kml);
                            CreateKMZ(kml);
                            File.Delete(kml);
                        }
                        finally
                        {
                            Kernel.SetCursor(IntPtr.Zero);
                        }

                        MessageBox.Show("The file was successfuly created.", "Success");
                    }
                }
            }
            Refresh();
        }

        private void miWhereAmI_Click(object sender, EventArgs e)
        {
            if (!inUse || !gpsData.IsValid)
            {
                MessageBox.Show(
                   "The GPS is not started or not fixed.",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Asterisk,
                   MessageBoxDefaultButton.Button1);
            }
            else
            {
                GoogleMaps gm = new GoogleMaps();
                gm.ViewMap(gpsData.Latitude, gpsData.Longitude);
                gm.ShowDialog();
                Refresh();
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
    }

    public class TripString
    {
        private Int64 _id;
        private string _name;

        public TripString(Int64 id, string name)
        {
            _id = id;
            _name = name;
        }

        public Int64 ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}