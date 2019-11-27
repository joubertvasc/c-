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
using JVGPS;
using JVGPS.Forms;

namespace OpenCellClient
{
    public partial class OpenCellClientMain : Form
    {
        // Version control
        private string version = "1.0.0-0";
        private int termOfServiceRevisionNumber = 0;

        string cell2xy = "http://www.cell2xy.nl:80/gps/getcells.php";

        #region Internal Variables
        private bool inUse;
        private string appPath;
        private string sLastCellID = "";

        private string longLatitude = "";
        private string longLongitude = "";
        private string latitudeDMS = "";
        private string longitudeDMS = "";
        private string shortLatitude = "";
        private string shortLongitude = "";
        private JVGPS.Forms.Compass compass;
        private double degree = 0;
        private Satellite[] satellites;
        private FixType fixType;
        private double altitude;
        private double speed;
        private string imei = "";

        private Panels currentPanel;

        private bool autoStart = false;
        private bool activated = false;

        string registryKey = "\\Software\\JV Software\\OpenCellClient";

        // GPS 
        private GPS gps;
        private GPSType gpsType;
        private string comPort = "COM4";
        private string baudRate = "57600";

        // Battery Metter
        private BatteryMetter bm;

        // Database
        private OCCDataBase db = null;

        // CellID
        CellIdDS cellIdDS = null;
        private bool onlyNewCells;
        #endregion

        public OpenCellClientMain()
        {
            InitializeComponent();
        }

        public OpenCellClientMain(string[] parameters)
        {
            InitializeComponent();

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ToLower().StartsWith("/start"))
                {
                    autoStart = true;
                }
            }
        }

        ~OpenCellClientMain()
        {
            if (gps != null && gps.IsStarted)
                gps.Stop();

            if (db != null)
                db.CloseDataBase();
        }

        private void RTScannerMain_Load(object sender, EventArgs e)
        {
            ChangePanel(Panels.Main);

            if (!Utils.IsTouchScreen())
            {
                miDataBase.MenuItems.Remove(miBackup);
                miDataBase.MenuItems.Remove(miRestore);
                miDataBase.MenuItems.Remove(miSeparatorDataBase);
            }

            lblVersion.Text = "Version: " + version;
        }

        private void LoadData(bool onlyNew)
        {
            cellIdDS.SelectAll(onlyNew);
            UpdateDataGridCellID();
        }

        private void RTScannerMain_Closing(object sender, CancelEventArgs e)
        {
            CloseScanner();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            CloseScanner();
        }

        private void CloseScanner()
        {
            if (inUse)
                Stop();

            if (db != null)
                db.CloseDataBase();

            SaveConfiguration();

            Debug.EndLog();
            Debug.SaveLog();

            Application.Exit();
        }

        private void ChangePanel(Panels page)
        {
            currentPanel = page;

            pnlOptions.Dock = DockStyle.Fill;
            pnlData.Dock = DockStyle.Fill;

            miDeleteCellTower.Enabled = page == Panels.Grid;
            miViewCellTower.Enabled = page == Panels.Grid;

            if (page == Panels.Main) // Main
            {
                pnlOptions.Visible = true;
                pnlData.Visible = false;
                Menu = mmMain;
            }
            else if (page == Panels.Grid) // Data Grid
            {
                pnlOptions.Visible = false;
                pnlData.Visible = true;
                Menu = mmMain;
            }
        }

        private void miStartStop_Click(object sender, EventArgs e)
        {
            if (!inUse)
            {
                if (Start())
                {
                    ChangePanel(Panels.Grid);
                }
            }
            else
            {
                Stop();
                ChangePanel(Panels.Main);
            }
        }

        private void RefreshTotals()
        {
            lblCellTowers.Text = "Cell Towers Found " + System.Convert.ToString(cellIdDS.Total) + ". Current ID: " + sLastCellID;
        }

        private bool Start()
        {
            bool canContinue = true; 

            // Verify if GPS is configured.
            if (gpsType == GPSType.Manual && (comPort.Equals("") || baudRate.Equals("")))
            {
                MessageBox.Show("Your GPS is not yet configured. Please select a serial port and baud rate.");
                canContinue = false;
            }

            // Need to reset device?
            if (canContinue && miResetDevice.Checked && !autoStart)
            {
                canContinue = false;

                DialogResult dlgResult =
                    MessageBox.Show("Your device will be restarted. Do you confirm?", "Confirmation",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dlgResult == DialogResult.Yes)
                {
                    Debug.AddLog("Reset necessary before start");

                    if (db != null)
                        db.CloseDataBase();

                    Debug.AddLog("Create link on startup");
                    Utils.CreateLink(appPath, "opencellclient.exe", "\\Windows\\Startup", "OpenCellClient.lnk", "/start");
                    Debug.AddLog("Restarting device.");
                    SaveConfiguration();
                    Debug.SaveLog();

                    Application.DoEvents();
                    Kernel.ResetPocketPC();
                }
                else if (dlgResult == DialogResult.Cancel)
                {
                    canContinue = false;
                }
            }

            if (canContinue && miRestartGSM.Checked)
            {
                lblStatus.Text = "Turning off GSM radio...";
                Application.DoEvents();
                Debug.AddLog("Turning off GSM radio");
                Radio.SetFlightMode(true);
                System.Threading.Thread.Sleep(5000);

                lblStatus.Text = "Turning on GSM radio...";
                Application.DoEvents();
                Debug.AddLog("Turning on GSM radio");
                Radio.SetFlightMode(false);

                int nCount = 0;
                do
                {
                    Debug.AddLog("RadioOff? " + Utils.iif(Microsoft.WindowsMobile.Status.SystemState.PhoneRadioOff, "Yes", "No") + "\n" +
                                 "NoService? " + Utils.iif(Microsoft.WindowsMobile.Status.SystemState.PhoneNoService, "Yes", "No") + "\n" +
                                 "Searching? " + Utils.iif(Microsoft.WindowsMobile.Status.SystemState.PhoneSearchingForService, "Yes", "No"));
                    if ((Microsoft.WindowsMobile.Status.SystemState.PhoneRadioOff ||
                         Microsoft.WindowsMobile.Status.SystemState.PhoneNoService ||
                         Microsoft.WindowsMobile.Status.SystemState.PhoneSearchingForService))
                    {
                        nCount++;
                        Debug.AddLog("Waiting GSM radio: " + System.Convert.ToString(nCount));
                        System.Threading.Thread.Sleep(5000);

                    }
                    else
                    {
                        break;
                    }
                } while (nCount < 15);

                lblStatus.Text = "";

                if (nCount >= 15)
                {
                    canContinue = false;
                    lblStatus.Text = "GSM radio can't be turned on.";
                }

                Application.DoEvents();
            }

            if (canContinue)
            {
                autoStart = false;

                if (miPreventSleep.Checked)
                {
                    Power.DisableSleep(true);
                    Debug.AddLog("Disabled sleep mode");
                }

                miView.Enabled = false;
                miDataBase.Enabled = false;
                miOptions.Enabled = false;
                miAbout.Enabled = false;
                miCompass.Enabled = true;

                sLastCellID = "";
                RefreshTotals();
                pb.Value = 0;
                lblStatus.Text = "";
                miDataBase.Enabled = false;

                Debug.AddLog("Start scanning");
                inUse = true;
                miStartStop.Text = "Stop";

                // Configure and start GPS
                Debug.AddLog("Before open GPS");
                gps.ChangeGPSType(gpsType);

                if (gpsType == GPSType.Manual)
                {
                    gps.ComPort = Utils.ConvertStringToCOMPort(comPort);
                    gps.BaudRate = Utils.ConvertStringToBaudRate(baudRate);
                }

                gps.Start();
                Debug.AddLog("After open GPS");

                CellIdTimer.Enabled = true;
            }

            return canContinue;
        }

        private void Stop()
        {
            sLastCellID = "";

            Debug.AddLog("Stopping GPS");

            CellIdTimer.Enabled = false;

            if (gps.IsStarted)
                gps.Stop();

            miView.Enabled = true;
            miDataBase.Enabled = true;
            miOptions.Enabled = true;
            miAbout.Enabled = true;
            miCompass.Enabled = false;

            lblGPS.Text = "GPS: ";

            miStartStop.Text = "Start";
            inUse = false;
            miDataBase.Enabled = true;
            
            Debug.AddLog("Scanner stopped");

            if (miPreventSleep.Checked)
            {
                Power.DisableSleep(false);
                Debug.AddLog("Enabled sleep mode");
            }
        }

        private void CellIdTimer_Tick(object sender, EventArgs e)
        {
            Debug.AddLog("CellID Timer begin");
            CellIdTimer.Enabled = false;
            try
            {
                if (inUse)
                    GetCurrentCellID();
            }
            finally
            {
                CellIdTimer.Enabled = true;
                Debug.AddLog("CellID Timer end");
            }
        }

        private void GetCurrentCellID()
        {
            Debug.AddLog("Get Current CellID");
            CellIDInformations cid = RIL.GetAllInformations();

            if (cid.cellID.Equals(""))
            {
                sLastCellID = "";
                Debug.AddLog("ERROR: RIL.GetAllInformations returned null");
            }
            else
            {
                if (Utils.IsNumberValid(cid.cellID) && Utils.IsNumberValid(cid.localAreaCode) &&
                    Utils.IsNumberValid(cid.mobileCountryCode) && Utils.IsNumberValid(cid.mobileNetworkCode))
                {
                    sLastCellID = cid.cellID;
                    Debug.AddLog("RIL.GetAllInformations: cellid = " + cid.cellID);
                    DataRow row = cellIdDS.FindRow(cid.cellID, cid.localAreaCode, cid.mobileNetworkCode,
                                                   cid.mobileCountryCode,
                                                   (miOnlyOneCell.Checked ? "" : cid.signalStrength));

                    if (row == null)
                    {
                        Debug.AddLog("Found new CellID");

                        // This cell already exists in database?
                        if (!cellIdDS.ExistsCellID(cid.cellID, cid.localAreaCode, cid.mobileNetworkCode,
                                                   cid.mobileCountryCode,
                                                   (miOnlyOneCell.Checked ? "" : cid.signalStrength)))
                        {
                            row = cellIdDS.AddCellID(cid.cellID,
                                                     cid.localAreaCode,
                                                     cid.mobileNetworkCode,
                                                     cid.mobileCountryCode,
                                                     (longLatitude.Equals("") ? "" : longLatitude),
                                                     (longLongitude.Equals("") ? "" : longLongitude), "",
                                                     cid.highSignalStrength,
                                                     cid.lowSignalStrength,
                                                     cid.maxSignalStrength,
                                                     cid.minSignalStrength,
                                                     cid.signalStrength);

                            dgTowers.CurrentRowIndex = cellIdDS.DataTable.Rows.Count - 1;

                            if (miAutoSendData.Checked && row != null)
                            {
                                SendRowToCell2XY(row);
                            }
                        }
                    }
                    else if (row["lat"].Equals("") && !longLatitude.Equals(""))
                    {
                        Debug.AddLog("Updating coordinates of CellID: " + cid.cellID + ", " + longLatitude + ", " + longLongitude);
                        row = cellIdDS.UpdateCellID(cid.cellID,
                                                    cid.localAreaCode,
                                                    cid.mobileNetworkCode,
                                                    cid.mobileCountryCode,
                                                    (longLatitude.Equals("") ? "" : longLatitude),
                                                    (longLongitude.Equals("") ? "" : longLongitude), "Y", "",
                                                    cid.highSignalStrength,
                                                    cid.lowSignalStrength,
                                                    cid.maxSignalStrength,
                                                    cid.minSignalStrength,
                                                    cid.signalStrength, "Y");

                        if (miAutoSendData.Checked && row != null)
                        {
                            SendRowToCell2XY(row);
                        }
                    }
                    else
                    {
                        Debug.AddLog("Cellid " + cid.cellID + " already exists in database.");
                    }
                }
            }

            RefreshTotals();
        }

        private void mi10Sec_Click(object sender, EventArgs e)
        {
            ChangeTimersInterval(10000, 3);
        }

        private void mi30Sec_Click(object sender, EventArgs e)
        {
            ChangeTimersInterval(30000, 4);
        }

        private void mi1Min_Click(object sender, EventArgs e)
        {
            ChangeTimersInterval(60000, 5);
        }

        private void ChangeTimersInterval(int interval, int option)
        {
            CellIdTimer.Interval = interval;

            mi10Sec.Checked = false;
            mi30Sec.Checked = false;
            mi1Min.Checked = false;

            if (option == 3)
            {
                mi10Sec.Checked = true;
                Debug.AddLog("Interval changed to 10 sec.");
            }
            else if (option == 4)
            {
                mi30Sec.Checked = true;
                Debug.AddLog("Interval changed to 30 sec.");
            }
            else if (option == 5)
            {
                mi1Min.Checked = true;
                Debug.AddLog("Interval changed to 1 min.");
            }
        }

        private void LoadWidths()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey(registryKey);

            if (r != null)
            {
                try
                {
                    if (dgTowers.TableStyles.Count > 0)
                    {
                        int nCol = System.Convert.ToInt16(r.GetValue("dgtwcl", "0"));
                        if (nCol > 0)
                        {
                            if (nCol > dgTowers.TableStyles[0].GridColumnStyles.Count)
                                nCol = dgTowers.TableStyles[0].GridColumnStyles.Count;

                            int width = 0;

                            for (int i = 0; i < nCol; i++)
                            {
                                try
                                {
                                    width = System.Convert.ToInt16(r.GetValue("dgtwc" + System.Convert.ToString(i),
                                        System.Convert.ToString(dgTowers.TableStyles[0].GridColumnStyles[i].Width)));
                                }
                                catch
                                {
                                    width = dgTowers.TableStyles[0].GridColumnStyles[i].Width;
                                }

                                dgTowers.TableStyles[0].GridColumnStyles[i].Width = width;
                            }
                        }
                    }
                }
                finally
                {
                    r.Close();
                }
            }
        }

        private void SaveWidths()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(registryKey);
            try
            {
                r.SetValue("dgtwcl", System.Convert.ToString(dgTowers.TableStyles[0].GridColumnStyles.Count));

                for (int i = 0; i < dgTowers.TableStyles[0].GridColumnStyles.Count; i++)
                {
                    r.SetValue("dgtwc" + System.Convert.ToString(i),
                        System.Convert.ToString(dgTowers.TableStyles[0].GridColumnStyles[i].Width));
                }
            }
            finally
            {
                r.Close();
            }
        }

        private void LoadConfiguration()
        {
            int interval = 3;

            miDebugMode.Checked = false;
            miRestartGSM.Checked = false;
            miAutoSendData.Checked = false;
            miPreventSleep.Checked = true;
            miResetDevice.Checked = false;
            gpsType = GPSType.Windows;
            comPort = "Com4:";
            baudRate = "57600";
            miOnlyOneCell.Checked = true;

            RegistryKey r = Registry.LocalMachine.OpenSubKey(registryKey);

            if (r != null)
            {
                try
                {
                    interval = System.Convert.ToInt32(r.GetValue("interval", "3"));
                    miDebugMode.Checked = ((string)r.GetValue("debug", "N")).Equals("Y");
                    miRestartGSM.Checked = ((string)r.GetValue("restartGSM", "N")).Equals("Y");
                    miAutoSendData.Checked = ((string)r.GetValue("autoSendOpenID", "N")).Equals("Y");
                    miPreventSleep.Checked = ((string)r.GetValue("preventSleep", "Y")).Equals("Y");
                    miResetDevice.Checked = ((string)r.GetValue("resetdevice", "N")).Equals("Y");
                    gpsType = ((string)r.GetValue("gpstype", "W")).Equals("W") ? GPSType.Windows : GPSType.Manual;
                    comPort = (string)r.GetValue("gpscom", "");
                    baudRate = (string)r.GetValue("gpsbaud", "");
                    miOnlyOneCell.Checked = ((string)r.GetValue("onyonecell", "N")).Equals("Y");
                }
                catch {}
                finally
                {
                    r.Close();
                }
            }
            else
            {
                miDebugMode.Checked = false;
            }

            Debug.Logging = miDebugMode.Checked;

            if (interval == 1)
            {
                ChangeTimersInterval(1000, 1);
            } else if (interval == 2) {
                ChangeTimersInterval(5000, 2);
            } else if (interval == 3) {
                ChangeTimersInterval(10000, 3);
            } else if (interval == 4) {
                ChangeTimersInterval(30000, 4);
            } else {
                ChangeTimersInterval(60000, 5);
            }

            gps.ChangeGPSType(gpsType == GPSType.Manual ? GPSType.Manual : GPSType.Windows);
            Debug.AddLog("GPS Type: " + (gpsType == GPSType.Manual ? "Manual" : "Windows"));
            LoadWidths();
        }

        private void SaveConfiguration()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(registryKey);

            r.SetValue("interval", (/*mi1Sec.Checked ? "1" : (mi5Sec.Checked ? "2" :/**/ (mi10Sec.Checked ? "3" : (mi30Sec.Checked ? "4" : "5")))); //);
            r.SetValue("debug", (miDebugMode.Checked ? "Y" : "N"));
            r.SetValue("restartGSM", (miRestartGSM.Checked ? "Y" : "N"));
            r.SetValue("autoSendOpenID", (miAutoSendData.Checked ? "Y" : "N"));
            r.SetValue("preventSleep", (miPreventSleep.Checked ? "Y" : "N"));
            r.SetValue("resetdevice", (miResetDevice.Checked ? "Y" : "N"));
            r.SetValue("gpstype", (gpsType == GPSType.Windows ? "W" : "M"));
            r.SetValue("gpscom", comPort);
            r.SetValue("gpsbaud", baudRate);
            r.SetValue("onyonecell", (miOnlyOneCell.Checked ? "Y" : "N"));
            r.Close();

            SaveWidths();
        }

        private void miDebugMode_Click(object sender, EventArgs e)
        {
            miDebugMode.Checked = !miDebugMode.Checked;

            Debug.Logging = miDebugMode.Checked;
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Text + "\n" +
                            "Version " + version + "\n\n" +
                            "http://www.cell2xy.nl", "About");
        }

        private void miRestartGSM_Click(object sender, EventArgs e)
        {
            miRestartGSM.Checked = !miRestartGSM.Checked;
        }

        private void miPreventSleep_Click(object sender, EventArgs e)
        {
            miPreventSleep.Checked = !miPreventSleep.Checked;
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

        private void UpdateDataGridCellID()
        {
            DataGridTableStyle DGStyle = new DataGridTableStyle();
            DataGridTextBoxColumn textColumn;

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "cellid";
            textColumn.HeaderText = "CellID";
            textColumn.Width = 74;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "signal";
            textColumn.HeaderText = "Signal";
            textColumn.Width = 77;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "slat";
            textColumn.HeaderText = "Latitude";
            textColumn.Width = 113;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "slon";
            textColumn.HeaderText = "Longitude";
            textColumn.Width = 113;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "lac";
            textColumn.HeaderText = "LAC";
            textColumn.Width = 68;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "mnc";
            textColumn.HeaderText = "MNC";
            textColumn.Width = 66;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "mcc";
            textColumn.HeaderText = "MCC";
            textColumn.Width = 66;
            DGStyle.GridColumnStyles.Add(textColumn);

            DGStyle.MappingName = cellIdDS.DataTable.TableName;

            // The Clear() method is called to ensure that
            // the previous style is removed.
            dgTowers.TableStyles.Clear();

            // Add the new DataGridTableStyle collection to
            // the TableStyles collection
            dgTowers.TableStyles.Add(DGStyle);

            dgTowers.DataSource = cellIdDS.DataTable;
            LoadWidths();
        }

        private void miViewNew_Click(object sender, EventArgs e)
        {
            onlyNewCells = true;
            SaveWidths(); 
            LoadData(true);
            RefreshTotals();
            ChangePanel(Panels.Grid);
        }

        private void miViewAll_Click(object sender, EventArgs e)
        {
            onlyNewCells = false;
            SaveWidths();
            LoadData(false);
            RefreshTotals();
            ChangePanel(Panels.Grid);
        }

        private void miMainScreen_Click(object sender, EventArgs e)
        {
            ChangePanel(Panels.Main);
        }

        private void miClearHistory_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = 
                MessageBox.Show("Do you really want to clear all history?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dlgResult == DialogResult.Yes)
            {
                cellIdDS.DeleteAll();
                onlyNewCells = true;
                LoadData(true);
                RefreshTotals();
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (cellIdDS.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("The table is empty.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                DataRow dr = cellIdDS.DataTable.Rows[dgTowers.CurrentRowIndex];

                DialogResult dlgResult =
                    MessageBox.Show("Do you really want to delete the cellID " +
                    (string)dr["cellid"] + "?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dlgResult == DialogResult.Yes)
                {
                    cellIdDS.DelCellID((string)dr["cellid"], (string)dr["lac"],
                                       (string)dr["mnc"], (string)dr["mcc"], (string)dr["signal"]);
                    RefreshTotals();
                }
            }
        }

        private void miResetDevice_Click(object sender, EventArgs e)
        {
            miResetDevice.Checked = !miResetDevice.Checked;
        }

        private void miBackup_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();
            saveDialog.Filter = "(*.c2xy)|*.c2xy";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (db != null)
                    db.CloseDataBase();

                string[] dataFile = new string[1];
                dataFile[0] = "\\Application Data\\OpenCellClient\\cellid.sdf";
                JVUtils.Compress.Zip.SimpleZip.Compress(dataFile, saveDialog.FileName);

                if (db.OpenDataBase(version))
                {
                    if (onlyNewCells)
                    {
                        cellIdDS.SelectAll(false);
                    }
                    else
                    {
                        cellIdDS.SelectAll(true);
                    }
                }
            }
        }

        private void miRestore_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openDialog = new System.Windows.Forms.OpenFileDialog();
            openDialog.Filter = "(*.c2xy)|*.c2xy";
            
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (db != null)
                    db.CloseDataBase();

                JVUtils.Compress.Zip.SimpleZip.Decompress(openDialog.FileName, "\\Application Data\\OpenCellClient");

                if (db.OpenDataBase(version))
                {
                    if (onlyNewCells)
                    {
                        cellIdDS.SelectAll(false);
                    }
                    else
                    {
                        cellIdDS.SelectAll(true);
                    }
                }
            }
        }

        void GetGPSDataEventHandler(object sender, GetGPSDataEventArgs args)
        {
            if (compass != null)
            {
                compass.gpsData = args.GPSData;
                compass.RedrawInfo();
            }

            if (args.GPSData.IsValid)
            {
                longLatitude = System.Convert.ToString(args.GPSData.Latitude);
                longLongitude = System.Convert.ToString(args.GPSData.Longitude);
                shortLatitude = System.Convert.ToString(args.GPSData.ShortLatitude);
                shortLongitude = System.Convert.ToString(args.GPSData.ShortLongitude);
                degree = args.GPSData.Heading;
                satellites = args.GPSData.Satellites;
                latitudeDMS = args.GPSData.LatitudeDMS;
                longitudeDMS = args.GPSData.LongitudeDMS;
                fixType = args.GPSData.FixType;
                altitude = args.GPSData.Altitude;
                speed = args.GPSData.Speed;

                lblGPS.Text = "GPS: " + shortLatitude + ", " + shortLongitude;
                Debug.AddLog("Coordinates: Lat (" + longLatitude + ") Lon (" + longLongitude + ")");
                Debug.AddLog("Short: Lat (" + shortLatitude + ") Lon (" + shortLongitude + ")");
                Debug.AddLog("Fix: " + (fixType == FixType.Unknown ? "Not fixed" : (fixType == FixType.XyD ? "2D" : "3D")));
                Debug.AddLog("Altitude: " + System.Convert.ToString(altitude));
                Debug.AddLog("Speed: " + System.Convert.ToString(speed));
            }
            else
            {
                Debug.AddLog("Invalid GPS Data");
                longLatitude = "";
                longLongitude = "";
                shortLatitude = "";
                shortLongitude = "";
                lblGPS.Text = "GPS: NOT FIXED";
                degree = -1;
                satellites = null;
                latitudeDMS = "";
                longitudeDMS = "";
                fixType = FixType.Unknown;
                altitude = 0;
                speed = 0;
            }
        }

        private void miGPS_Click(object sender, EventArgs e)
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
                    Debug.AddLog("GPS configuration changed to: " +
                        (gpsType == GPSType.Manual ? ("Manual " + " port " + comPort + " baudrate " + baudRate) : "Windows"));
                }
            }
        }

        private void miCompass_Click(object sender, EventArgs e)
        {
            compass = new JVGPS.Forms.Compass();
            compass.ShowDialog();
            compass.Dispose();
            compass = null;
        }

        bool ShowGoogleMap(string lat, string lon)
        {
            if (!lat.Equals("") && !lon.Equals(""))
            {
                GoogleMaps gm = new GoogleMaps();
                gm.ViewMap(lat, lon);
                gm.ShowDialog();
                Refresh();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void cnViewMap_Click(object sender, EventArgs e)
        {
            DataRow row = cellIdDS.DataTable.Rows[dgTowers.CurrentRowIndex];

            if (!ShowGoogleMap((string)row["lat"], (string)row["lon"]))
                MessageBox.Show("This CELLID does not have coordinates.", "Error");
        }

        private bool SendRowToCell2XY(DataRow dr)
        {
            bool result = false;

            if (dr != null)
            {
                Debug.AddLog("Sending to Cell2XY: " + dr["cellid"]);

                string mnc = (string)dr["mnc"];
                string mcc = (string)dr["mcc"];
                string cellID = (string)dr["cellid"];
                string lac = (string)dr["lac"];
                string lat = (string)dr["lat"];
                string lon = (string)dr["lon"];
                string signal = (string)dr["signal"];

                if (!mnc.Trim().Equals("") && !mcc.Trim().Equals("") && !cellID.Trim().Equals("") &&
                    !lac.Trim().Equals("") && !lat.Trim().Equals("") && !lon.Trim().Equals(""))
                {
                    lat = (lat.StartsWith("-") ? lat.Remove(0, 1) + "S" : lat + "N");
                    lon = (lon.StartsWith("-") ? lon.Remove(0, 1) + "W" : lon + "E");

                    string res = Web.Request(cell2xy +
                                             "?imei=" + imei.Trim() +
                                             "&datetime=" + Utils.ChangeChar(System.DateTime.Now.ToString(), ' ', ',') +
                                             "&mnc=" + mnc.Trim() +
                                             "&mcc=" + mcc.Trim() +
                                             "&cell=" + cellID.Trim() +
                                             "&latitude=" + Utils.ChangeChar(lat.Trim(), ',', '.') +
                                             "&longitude=" + Utils.ChangeChar(lon.Trim(), ',', '.') +
                                             "&signal=" + signal.Trim() + 
                                             "&lac=" + lac.Trim());

                    result = res.ToLower().Contains("ok");
                }

                Debug.AddLog("Sending to Cell2XY: result = " + (result ? "Ok" : "Failed"));
                if (result)
                {
                    dr["new"] = "N";
                    cellIdDS.UpdateCellID((string)dr["cellid"],
                                          (string)dr["lac"],
                                          (string)dr["mnc"],
                                          (string)dr["mcc"],
                                          (string)dr["lat"],
                                          (string)dr["lon"], "N",
                                          "",
                                          (string)dr["highsignal"],
                                          (string)dr["lowsignal"],
                                          (string)dr["maxsignal"],
                                          (string)dr["minsignal"],
                                          (string)dr["signal"], "N");
                }
            }

            return result;
        }

        private void SendCell2XYData()
        {
            int total = 0;

            lblStatus.Text = "Sendig data...";
            Application.DoEvents();

            Debug.AddLog("Processing CellId List");

            DataRow dr;
            pb.Minimum = 0;
            pb.Maximum = cellIdDS.DataTable.Rows.Count;
            pb.Value = 0;

            for (int i = 0; i < cellIdDS.DataTable.Rows.Count; i++)
            {
                dr = cellIdDS.DataTable.Rows[i];
                Debug.AddLog("CellID: " + (string)dr["cellid"]);

                if (dr["new"].ToString().Equals("Y"))
                {
                    lblStatus.Text = "Sending CellID: " + (string)dr["cellid"] + " (" +
                        System.Convert.ToString(i) + "/" +
                        System.Convert.ToString(cellIdDS.DataTable.Rows.Count) + ")";
                    Application.DoEvents();

                    if (SendRowToCell2XY(dr))
                       total++;
                }

                pb.Value += 1;
                Application.DoEvents();
            }

            lblStatus.Text = System.Convert.ToString(total) + " CellIDs info successfully sent.";

            RefreshTotals();
        }

        private void miSendToCell2XY(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            try
            {
                miDataBase.Enabled = false;
                miStartStop.Enabled = false;
                ChangePanel(Panels.Main);

                SendCell2XYData();
            }
            finally
            {
                miDataBase.Enabled = true;
                miStartStop.Enabled = true;
                Cursor.Current = Cursors.Default;
                Kernel.SetCursor(IntPtr.Zero);
            }
        }

        private void miOnlyOneCell_Click(object sender, EventArgs e)
        {
            miOnlyOneCell.Checked = !miOnlyOneCell.Checked;
        }

        private void miAutoSendData_Click(object sender, EventArgs e)
        {
            miAutoSendData.Checked = !miAutoSendData.Checked;
        }

        private void OpenCellClientMain_Activated(object sender, EventArgs e)
        {
            if (!activated)
            {
                activated = true;

                string appName = Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase);
                appPath = appName.Substring(0, appName.LastIndexOf(@"\") + 1);
                inUse = false;
                lblStatus.Text = "";
                sLastCellID = "";
                bool bCanContinue = true;
                miCompass.Enabled = false;

                // Verify if the ToS was accepted
                if (File.Exists(appPath + "opencellclient.html"))
                {
                    bCanContinue = JVUtils.JVUtils.Get_ContractWasAccepted("OpenCellClient",
                        termOfServiceRevisionNumber);

                    if (!bCanContinue)
                    {
                        JVUtils.Forms.Contract c = new JVUtils.Forms.Contract();
                        c.ContractHTMLFileName = appPath + "opencellclient.html";

                        if (c.ShowDialog() == DialogResult.OK)
                        {
                            JVUtils.JVUtils.Set_ContractAccepted("OpenCellClient", termOfServiceRevisionNumber);
                            bCanContinue = true;
                        }
                    }
                }
                //

                if (bCanContinue)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Application.DoEvents();
                    try
                    {
                        PhoneInfo pi = new PhoneInfo();
                        imei = pi.GetIMEI();

                        // GPS
                        gps = new GPS();
                        gps.GetGPSDataEvent += new GPS.GetGPSDataEventHandler(GetGPSDataEventHandler);

                        // Configure AppToDate
                        if (AppToDate.IsInstalled())
                        {
                            AppToDate.CopyConfigFile(appPath, "cell2xy.xml", "cell2xy.ICO");
                        }

                        Debug.StartLog(appPath + "cell2xydebug.txt");
                        Debug.SaveAfterEachAdd = true;
                        Debug.AddLog("OpenCellClient Version: " + version + "\n" +
                                     "Instaled in: " + appPath + "\n" +
                                     "JVUtils version: " + JVUtils.JVUtils.Version + "\n" +
                                     "JVSQL version: " + JVSQL.JVSQL.Version + "\n" +
                                     "JVGPS version: " + gps.Version);

                        // Create database and load data
                        db = new OCCDataBase();
                        db.OpenDataBase(version);
                        cellIdDS = new CellIdDS(); // CellIdDS(db);
                        cellIdDS.DB = db;
                        cellIdDS.TableName = "cellid";
                        onlyNewCells = true;

                        LoadData(true);

                        Application.DoEvents();
                        LoadConfiguration();

                        // Battery Metter
                        bm = new BatteryMetter();
                        bm.BatteryEvent += new BatteryMetter.BatteryEventHandler(BatteryEventHandler);

                        // For VGA devices, resize the battery image. not beautiful... Just temporary.
                        if (Screen.PrimaryScreen.WorkingArea.Width > 240)
                        {
                            pbBattery.Size = new Size(32, 32);
                            pbBattery.Location = new Point(pbBattery.Location.X - 16, pbBattery.Location.Y);
                        }

                        // Autostart?
                        if (autoStart)
                        {
                            Debug.AddLog("Autostart activated. Deleting startup link file");

                            if (File.Exists("\\windows\\startup\\opencellclient.lnk"))
                                File.Delete("\\windows\\startup\\opencellclient.lnk");

                            Debug.AddLog("Autostarting");
                            Start();
                            ChangePanel(Panels.Grid);
                        }
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                        Kernel.SetCursor(IntPtr.Zero);
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }

    public enum Panels
    {
        Main = 0,
        Grid = 1
    }
}