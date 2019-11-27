namespace OpenCellClient
{
    partial class OpenCellClientMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmMain;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenCellClientMain));
            this.mmMain = new System.Windows.Forms.MainMenu();
            this.miActions = new System.Windows.Forms.MenuItem();
            this.miStartStop = new System.Windows.Forms.MenuItem();
            this.miSendTo = new System.Windows.Forms.MenuItem();
            this.miSendOpenCellID = new System.Windows.Forms.MenuItem();
            this.miSendCellDB = new System.Windows.Forms.MenuItem();
            this.miSendRemoteTracker = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miView = new System.Windows.Forms.MenuItem();
            this.miViewCellTower = new System.Windows.Forms.MenuItem();
            this.miViewWiFi = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.miViewNew = new System.Windows.Forms.MenuItem();
            this.miViewAll = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.miMainScreen = new System.Windows.Forms.MenuItem();
            this.miDataBase = new System.Windows.Forms.MenuItem();
            this.miDeleteCellTower = new System.Windows.Forms.MenuItem();
            this.miDeleteSelectedWiFi = new System.Windows.Forms.MenuItem();
            this.miSeparator2 = new System.Windows.Forms.MenuItem();
            this.miBackup = new System.Windows.Forms.MenuItem();
            this.miRestore = new System.Windows.Forms.MenuItem();
            this.miSeparatorDataBase = new System.Windows.Forms.MenuItem();
            this.miClearHistory = new System.Windows.Forms.MenuItem();
            this.miCompass = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miDebugMode = new System.Windows.Forms.MenuItem();
            this.miAutoSendOpenCellID = new System.Windows.Forms.MenuItem();
            this.miPreventSleep = new System.Windows.Forms.MenuItem();
            this.miSendKey = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.miRestartGSM = new System.Windows.Forms.MenuItem();
            this.miResetDevice = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miInterval = new System.Windows.Forms.MenuItem();
            this.mi1Sec = new System.Windows.Forms.MenuItem();
            this.mi5Sec = new System.Windows.Forms.MenuItem();
            this.mi10Sec = new System.Windows.Forms.MenuItem();
            this.mi30Sec = new System.Windows.Forms.MenuItem();
            this.mi1Min = new System.Windows.Forms.MenuItem();
            this.miOpenCellIdKey = new System.Windows.Forms.MenuItem();
            this.miUseInternalKey = new System.Windows.Forms.MenuItem();
            this.miUseYourOwnKey = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miGPS = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.lblURL = new System.Windows.Forms.LinkLabel();
            this.lblContact = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.cbOnlyAP = new System.Windows.Forms.CheckBox();
            this.cbCellID = new System.Windows.Forms.CheckBox();
            this.cbWifi = new System.Windows.Forms.CheckBox();
            this.lblDonate = new System.Windows.Forms.LinkLabel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.pnlData = new System.Windows.Forms.Panel();
            this.pbBattery = new System.Windows.Forms.PictureBox();
            this.dgWiFi = new System.Windows.Forms.DataGrid();
            this.lblWiFi = new System.Windows.Forms.Label();
            this.dgTowers = new System.Windows.Forms.DataGrid();
            this.lblCellTowers = new System.Windows.Forms.Label();
            this.lblGPS = new System.Windows.Forms.Label();
            this.CellIdTimer = new System.Windows.Forms.Timer();
            this.pnlKey = new System.Windows.Forms.Panel();
            this.tbCellDBHash = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCellDBUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.lblKey = new System.Windows.Forms.Label();
            this.mmKey = new System.Windows.Forms.MainMenu();
            this.miKeyOk = new System.Windows.Forms.MenuItem();
            this.miKeyCancel = new System.Windows.Forms.MenuItem();
            this.ilBattery = new System.Windows.Forms.ImageList();
            this.pnlOptions.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.pnlKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmMain
            // 
            this.mmMain.MenuItems.Add(this.miActions);
            this.mmMain.MenuItems.Add(this.miExit);
            // 
            // miActions
            // 
            this.miActions.MenuItems.Add(this.miStartStop);
            this.miActions.MenuItems.Add(this.miSendTo);
            this.miActions.MenuItems.Add(this.menuItem1);
            this.miActions.MenuItems.Add(this.miView);
            this.miActions.MenuItems.Add(this.miDataBase);
            this.miActions.MenuItems.Add(this.miCompass);
            this.miActions.MenuItems.Add(this.menuItem4);
            this.miActions.MenuItems.Add(this.miOptions);
            this.miActions.MenuItems.Add(this.menuItem3);
            this.miActions.MenuItems.Add(this.miAbout);
            this.miActions.Text = "Menu";
            // 
            // miStartStop
            // 
            this.miStartStop.Text = "Start";
            this.miStartStop.Click += new System.EventHandler(this.miStartStop_Click);
            // 
            // miSendTo
            // 
            this.miSendTo.MenuItems.Add(this.miSendOpenCellID);
            this.miSendTo.MenuItems.Add(this.miSendCellDB);
            this.miSendTo.MenuItems.Add(this.miSendRemoteTracker);
            this.miSendTo.Text = "Send Data to...";
            // 
            // miSendOpenCellID
            // 
            this.miSendOpenCellID.Text = "Send to OpenCellID";
            this.miSendOpenCellID.Click += new System.EventHandler(this.miSendOpenCellID_Click);
            // 
            // miSendCellDB
            // 
            this.miSendCellDB.Text = "Send to CellDB";
            this.miSendCellDB.Click += new System.EventHandler(this.miSendCellDB_Click);
            // 
            // miSendRemoteTracker
            // 
            this.miSendRemoteTracker.Text = "Send to RemoteTracker";
            this.miSendRemoteTracker.Click += new System.EventHandler(this.miSendRemoteTracker_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miView
            // 
            this.miView.MenuItems.Add(this.miViewCellTower);
            this.miView.MenuItems.Add(this.miViewWiFi);
            this.miView.MenuItems.Add(this.menuItem6);
            this.miView.MenuItems.Add(this.miViewNew);
            this.miView.MenuItems.Add(this.miViewAll);
            this.miView.MenuItems.Add(this.menuItem9);
            this.miView.MenuItems.Add(this.miMainScreen);
            this.miView.Text = "View";
            // 
            // miViewCellTower
            // 
            this.miViewCellTower.Text = "View Map of Selected Cell Tower";
            this.miViewCellTower.Click += new System.EventHandler(this.cnViewMap_Click);
            // 
            // miViewWiFi
            // 
            this.miViewWiFi.Text = "View Map of Selected WiFi";
            this.miViewWiFi.Click += new System.EventHandler(this.cmViewMapWiFi_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "-";
            // 
            // miViewNew
            // 
            this.miViewNew.Text = "Coordinates not sent";
            this.miViewNew.Click += new System.EventHandler(this.miViewNew_Click);
            // 
            // miViewAll
            // 
            this.miViewAll.Text = "All Coordinates";
            this.miViewAll.Click += new System.EventHandler(this.miViewAll_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Text = "-";
            // 
            // miMainScreen
            // 
            this.miMainScreen.Text = "Main screen";
            this.miMainScreen.Click += new System.EventHandler(this.miMainScreen_Click);
            // 
            // miDataBase
            // 
            this.miDataBase.MenuItems.Add(this.miDeleteCellTower);
            this.miDataBase.MenuItems.Add(this.miDeleteSelectedWiFi);
            this.miDataBase.MenuItems.Add(this.miSeparator2);
            this.miDataBase.MenuItems.Add(this.miBackup);
            this.miDataBase.MenuItems.Add(this.miRestore);
            this.miDataBase.MenuItems.Add(this.miSeparatorDataBase);
            this.miDataBase.MenuItems.Add(this.miClearHistory);
            this.miDataBase.Text = "Database";
            // 
            // miDeleteCellTower
            // 
            this.miDeleteCellTower.Text = "Delete Selected Cell Tower";
            this.miDeleteCellTower.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miDeleteSelectedWiFi
            // 
            this.miDeleteSelectedWiFi.Text = "Delete Selected WiFi";
            this.miDeleteSelectedWiFi.Click += new System.EventHandler(this.miDeleteWiFi_Click);
            // 
            // miSeparator2
            // 
            this.miSeparator2.Text = "-";
            // 
            // miBackup
            // 
            this.miBackup.Text = "Backup";
            this.miBackup.Click += new System.EventHandler(this.miBackup_Click);
            // 
            // miRestore
            // 
            this.miRestore.Text = "Restore";
            this.miRestore.Click += new System.EventHandler(this.miRestore_Click);
            // 
            // miSeparatorDataBase
            // 
            this.miSeparatorDataBase.Text = "-";
            // 
            // miClearHistory
            // 
            this.miClearHistory.Text = "Clear all history";
            this.miClearHistory.Click += new System.EventHandler(this.miClearHistory_Click);
            // 
            // miCompass
            // 
            this.miCompass.Text = "Compass";
            this.miCompass.Click += new System.EventHandler(this.miCompass_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // miOptions
            // 
            this.miOptions.MenuItems.Add(this.miDebugMode);
            this.miOptions.MenuItems.Add(this.miAutoSendOpenCellID);
            this.miOptions.MenuItems.Add(this.miPreventSleep);
            this.miOptions.MenuItems.Add(this.miSendKey);
            this.miOptions.MenuItems.Add(this.menuItem7);
            this.miOptions.MenuItems.Add(this.miRestartGSM);
            this.miOptions.MenuItems.Add(this.miResetDevice);
            this.miOptions.MenuItems.Add(this.menuItem2);
            this.miOptions.MenuItems.Add(this.miInterval);
            this.miOptions.MenuItems.Add(this.miOpenCellIdKey);
            this.miOptions.MenuItems.Add(this.menuItem5);
            this.miOptions.MenuItems.Add(this.miGPS);
            this.miOptions.Text = "Options";
            // 
            // miDebugMode
            // 
            this.miDebugMode.Text = "Debug Mode";
            this.miDebugMode.Click += new System.EventHandler(this.miDebugMode_Click);
            // 
            // miAutoSendOpenCellID
            // 
            this.miAutoSendOpenCellID.Text = "Auto send data";
            this.miAutoSendOpenCellID.Click += new System.EventHandler(this.miAutoSendOpenCellID_Click);
            // 
            // miPreventSleep
            // 
            this.miPreventSleep.Text = "Prevent sleep mode";
            this.miPreventSleep.Click += new System.EventHandler(this.miPreventSleep_Click);
            // 
            // miSendKey
            // 
            this.miSendKey.Text = "Send key to keep alive";
            this.miSendKey.Click += new System.EventHandler(this.miSendKey_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "-";
            // 
            // miRestartGSM
            // 
            this.miRestartGSM.Text = "Reset GSM before start";
            this.miRestartGSM.Click += new System.EventHandler(this.miRestartGSM_Click);
            // 
            // miResetDevice
            // 
            this.miResetDevice.Text = "Reset device before start";
            this.miResetDevice.Click += new System.EventHandler(this.miResetDevice_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // miInterval
            // 
            this.miInterval.MenuItems.Add(this.mi1Sec);
            this.miInterval.MenuItems.Add(this.mi5Sec);
            this.miInterval.MenuItems.Add(this.mi10Sec);
            this.miInterval.MenuItems.Add(this.mi30Sec);
            this.miInterval.MenuItems.Add(this.mi1Min);
            this.miInterval.Text = "Set interval";
            // 
            // mi1Sec
            // 
            this.mi1Sec.Text = "1 sec";
            this.mi1Sec.Click += new System.EventHandler(this.mi1Sec_Click);
            // 
            // mi5Sec
            // 
            this.mi5Sec.Text = "5 sec";
            this.mi5Sec.Click += new System.EventHandler(this.mi5Sec_Click);
            // 
            // mi10Sec
            // 
            this.mi10Sec.Text = "10 sec";
            this.mi10Sec.Click += new System.EventHandler(this.mi10Sec_Click);
            // 
            // mi30Sec
            // 
            this.mi30Sec.Text = "30 sec";
            this.mi30Sec.Click += new System.EventHandler(this.mi30Sec_Click);
            // 
            // mi1Min
            // 
            this.mi1Min.Text = "1 min";
            this.mi1Min.Click += new System.EventHandler(this.mi1Min_Click);
            // 
            // miOpenCellIdKey
            // 
            this.miOpenCellIdKey.MenuItems.Add(this.miUseInternalKey);
            this.miOpenCellIdKey.MenuItems.Add(this.miUseYourOwnKey);
            this.miOpenCellIdKey.Text = "Set Key/Hash";
            // 
            // miUseInternalKey
            // 
            this.miUseInternalKey.Text = "Use Internal Key/Hash";
            this.miUseInternalKey.Click += new System.EventHandler(this.miUseRemoteTrackerKey_Click);
            // 
            // miUseYourOwnKey
            // 
            this.miUseYourOwnKey.Text = "Use Your Own Key/Hash";
            this.miUseYourOwnKey.Click += new System.EventHandler(this.miUseYourOwnKey_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // miGPS
            // 
            this.miGPS.Text = "GPS Options";
            this.miGPS.Click += new System.EventHandler(this.miGPS_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // miAbout
            // 
            this.miAbout.Text = "About";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miExit
            // 
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.lblURL);
            this.pnlOptions.Controls.Add(this.lblContact);
            this.pnlOptions.Controls.Add(this.lblVersion);
            this.pnlOptions.Controls.Add(this.lblProjectName);
            this.pnlOptions.Controls.Add(this.cbOnlyAP);
            this.pnlOptions.Controls.Add(this.cbCellID);
            this.pnlOptions.Controls.Add(this.cbWifi);
            this.pnlOptions.Controls.Add(this.lblDonate);
            this.pnlOptions.Controls.Add(this.lblStatus);
            this.pnlOptions.Controls.Add(this.pb);
            this.pnlOptions.Location = new System.Drawing.Point(3, 3);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(221, 262);
            // 
            // lblURL
            // 
            this.lblURL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblURL.Location = new System.Drawing.Point(0, 166);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(221, 20);
            this.lblURL.TabIndex = 8;
            this.lblURL.Text = "http://opencellclient.sourceforge.net";
            this.lblURL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblURL.Click += new System.EventHandler(this.lblURL_Click_1);
            // 
            // lblContact
            // 
            this.lblContact.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblContact.Location = new System.Drawing.Point(0, 186);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(221, 16);
            this.lblContact.TabIndex = 7;
            this.lblContact.Text = "Contacts: joubertvasc@gmail.com";
            this.lblContact.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblContact.Click += new System.EventHandler(this.lblContact_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.Location = new System.Drawing.Point(3, 25);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(213, 20);
            this.lblVersion.Text = "Version 0.0.1-0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(0, 0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(221, 25);
            this.lblProjectName.Text = "OpenCellClient";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbOnlyAP
            // 
            this.cbOnlyAP.Location = new System.Drawing.Point(22, 80);
            this.cbOnlyAP.Name = "cbOnlyAP";
            this.cbOnlyAP.Size = new System.Drawing.Size(196, 20);
            this.cbOnlyAP.TabIndex = 4;
            this.cbOnlyAP.Text = "Only Access Points";
            // 
            // cbCellID
            // 
            this.cbCellID.Location = new System.Drawing.Point(3, 106);
            this.cbCellID.Name = "cbCellID";
            this.cbCellID.Size = new System.Drawing.Size(215, 20);
            this.cbCellID.TabIndex = 1;
            this.cbCellID.Text = "Scan Cell Towers (cellId)";
            // 
            // cbWifi
            // 
            this.cbWifi.Location = new System.Drawing.Point(3, 57);
            this.cbWifi.Name = "cbWifi";
            this.cbWifi.Size = new System.Drawing.Size(215, 20);
            this.cbWifi.TabIndex = 0;
            this.cbWifi.Text = "Scan WiFi Spots";
            this.cbWifi.Click += new System.EventHandler(this.cbWifi_Click);
            // 
            // lblDonate
            // 
            this.lblDonate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDonate.Location = new System.Drawing.Point(0, 202);
            this.lblDonate.Name = "lblDonate";
            this.lblDonate.Size = new System.Drawing.Size(221, 20);
            this.lblDonate.TabIndex = 15;
            this.lblDonate.Text = "Please donate.";
            this.lblDonate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblDonate.Click += new System.EventHandler(this.lblDonate_Click_1);
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 222);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(221, 20);
            this.lblStatus.Text = "label2";
            // 
            // pb
            // 
            this.pb.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb.Location = new System.Drawing.Point(0, 242);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(221, 20);
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.pbBattery);
            this.pnlData.Controls.Add(this.dgWiFi);
            this.pnlData.Controls.Add(this.lblWiFi);
            this.pnlData.Controls.Add(this.dgTowers);
            this.pnlData.Controls.Add(this.lblCellTowers);
            this.pnlData.Controls.Add(this.lblGPS);
            this.pnlData.Location = new System.Drawing.Point(230, 3);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(224, 262);
            // 
            // pbBattery
            // 
            this.pbBattery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBattery.Location = new System.Drawing.Point(205, 0);
            this.pbBattery.Name = "pbBattery";
            this.pbBattery.Size = new System.Drawing.Size(16, 16);
            this.pbBattery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // dgWiFi
            // 
            this.dgWiFi.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgWiFi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgWiFi.Location = new System.Drawing.Point(0, 145);
            this.dgWiFi.Name = "dgWiFi";
            this.dgWiFi.Size = new System.Drawing.Size(224, 117);
            this.dgWiFi.TabIndex = 6;
            // 
            // lblWiFi
            // 
            this.lblWiFi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWiFi.Location = new System.Drawing.Point(0, 125);
            this.lblWiFi.Name = "lblWiFi";
            this.lblWiFi.Size = new System.Drawing.Size(224, 20);
            this.lblWiFi.Text = "WiFi Spots";
            this.lblWiFi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgTowers
            // 
            this.dgTowers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgTowers.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgTowers.Location = new System.Drawing.Point(0, 40);
            this.dgTowers.Name = "dgTowers";
            this.dgTowers.Size = new System.Drawing.Size(224, 85);
            this.dgTowers.TabIndex = 13;
            // 
            // lblCellTowers
            // 
            this.lblCellTowers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCellTowers.Location = new System.Drawing.Point(0, 20);
            this.lblCellTowers.Name = "lblCellTowers";
            this.lblCellTowers.Size = new System.Drawing.Size(224, 20);
            this.lblCellTowers.Text = "Cell Towers";
            this.lblCellTowers.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblGPS
            // 
            this.lblGPS.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGPS.Location = new System.Drawing.Point(0, 0);
            this.lblGPS.Name = "lblGPS";
            this.lblGPS.Size = new System.Drawing.Size(224, 20);
            this.lblGPS.Text = "GPS:";
            // 
            // CellIdTimer
            // 
            this.CellIdTimer.Interval = 5000;
            this.CellIdTimer.Tick += new System.EventHandler(this.CellIdTimer_Tick);
            // 
            // pnlKey
            // 
            this.pnlKey.Controls.Add(this.tbCellDBHash);
            this.pnlKey.Controls.Add(this.label2);
            this.pnlKey.Controls.Add(this.tbCellDBUser);
            this.pnlKey.Controls.Add(this.label1);
            this.pnlKey.Controls.Add(this.tbKey);
            this.pnlKey.Controls.Add(this.lblKey);
            this.pnlKey.Location = new System.Drawing.Point(457, 3);
            this.pnlKey.Name = "pnlKey";
            this.pnlKey.Size = new System.Drawing.Size(209, 242);
            // 
            // tbCellDBHash
            // 
            this.tbCellDBHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCellDBHash.Location = new System.Drawing.Point(12, 137);
            this.tbCellDBHash.Name = "tbCellDBHash";
            this.tbCellDBHash.Size = new System.Drawing.Size(194, 21);
            this.tbCellDBHash.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(12, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 20);
            this.label2.Text = "CellDB Hash:";
            // 
            // tbCellDBUser
            // 
            this.tbCellDBUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCellDBUser.Location = new System.Drawing.Point(12, 96);
            this.tbCellDBUser.Name = "tbCellDBUser";
            this.tbCellDBUser.Size = new System.Drawing.Size(194, 21);
            this.tbCellDBUser.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 20);
            this.label1.Text = "CellDB Username:";
            // 
            // tbKey
            // 
            this.tbKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKey.Location = new System.Drawing.Point(12, 38);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(194, 21);
            this.tbKey.TabIndex = 1;
            // 
            // lblKey
            // 
            this.lblKey.Location = new System.Drawing.Point(12, 20);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(194, 20);
            this.lblKey.Text = "Type your OpenCellId key:";
            // 
            // mmKey
            // 
            this.mmKey.MenuItems.Add(this.miKeyOk);
            this.mmKey.MenuItems.Add(this.miKeyCancel);
            // 
            // miKeyOk
            // 
            this.miKeyOk.Text = "Ok";
            this.miKeyOk.Click += new System.EventHandler(this.miKeyOk_Click);
            // 
            // miKeyCancel
            // 
            this.miKeyCancel.Text = "Cancel";
            this.miKeyCancel.Click += new System.EventHandler(this.miKeyCancel_Click);
            this.ilBattery.Images.Clear();
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            // 
            // OpenCellClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlKey);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.pnlOptions);
            this.Menu = this.mmMain;
            this.Name = "OpenCellClientMain";
            this.Text = "OpenCellClient";
            this.Load += new System.EventHandler(this.RTScannerMain_Load);
            this.Activated += new System.EventHandler(this.OpenCellClientMain_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.RTScannerMain_Closing);
            this.pnlOptions.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.pnlKey.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.CheckBox cbOnlyAP;
        private System.Windows.Forms.CheckBox cbCellID;
        private System.Windows.Forms.CheckBox cbWifi;
        private System.Windows.Forms.MenuItem miActions;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.Label lblCellTowers;
        private System.Windows.Forms.Label lblWiFi;
        private System.Windows.Forms.DataGrid dgWiFi;
        private System.Windows.Forms.Timer CellIdTimer;
        private System.Windows.Forms.DataGrid dgTowers;
        private System.Windows.Forms.MenuItem miStartStop;
        private System.Windows.Forms.MenuItem miSendOpenCellID;
        private System.Windows.Forms.MenuItem miSendRemoteTracker;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miDataBase;
        private System.Windows.Forms.Label lblGPS;
        private System.Windows.Forms.MenuItem miInterval;
        private System.Windows.Forms.MenuItem mi1Sec;
        private System.Windows.Forms.MenuItem mi5Sec;
        private System.Windows.Forms.MenuItem mi10Sec;
        private System.Windows.Forms.MenuItem mi30Sec;
        private System.Windows.Forms.MenuItem mi1Min;
        private System.Windows.Forms.MenuItem miOpenCellIdKey;
        private System.Windows.Forms.MenuItem miUseInternalKey;
        private System.Windows.Forms.MenuItem miUseYourOwnKey;
        private System.Windows.Forms.MenuItem miDebugMode;
        private System.Windows.Forms.Panel pnlKey;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.MainMenu mmKey;
        private System.Windows.Forms.MenuItem miKeyOk;
        private System.Windows.Forms.MenuItem miKeyCancel;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.LinkLabel lblURL;
        private System.Windows.Forms.LinkLabel lblContact;
        private System.Windows.Forms.MenuItem miRestartGSM;
        private System.Windows.Forms.MenuItem miAutoSendOpenCellID;
        private System.Windows.Forms.MenuItem miPreventSleep;
        private System.Windows.Forms.PictureBox pbBattery;
        private System.Windows.Forms.ImageList ilBattery;
        private System.Windows.Forms.MenuItem miViewNew;
        private System.Windows.Forms.MenuItem miViewAll;
        private System.Windows.Forms.MenuItem miMainScreen;
        private System.Windows.Forms.MenuItem miClearHistory;
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem miResetDevice;
        private System.Windows.Forms.MenuItem miBackup;
        private System.Windows.Forms.MenuItem miRestore;
        private System.Windows.Forms.MenuItem miSeparatorDataBase;
        private System.Windows.Forms.MenuItem miView;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miGPS;
        private System.Windows.Forms.MenuItem miCompass;
        private System.Windows.Forms.MenuItem miSendTo;
        private System.Windows.Forms.MenuItem miSendCellDB;
        private System.Windows.Forms.LinkLabel lblDonate;
        private System.Windows.Forms.TextBox tbCellDBHash;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCellDBUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem miSendKey;
        private System.Windows.Forms.MenuItem miDeleteCellTower;
        private System.Windows.Forms.MenuItem miViewCellTower;
        private System.Windows.Forms.MenuItem miDeleteSelectedWiFi;
        private System.Windows.Forms.MenuItem miViewWiFi;
        private System.Windows.Forms.MenuItem miSeparator2;
        private System.Windows.Forms.MenuItem menuItem6;

    }
}