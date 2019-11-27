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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miView = new System.Windows.Forms.MenuItem();
            this.miViewCellTower = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.miViewNew = new System.Windows.Forms.MenuItem();
            this.miViewAll = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.miMainScreen = new System.Windows.Forms.MenuItem();
            this.miDataBase = new System.Windows.Forms.MenuItem();
            this.miDeleteCellTower = new System.Windows.Forms.MenuItem();
            this.miSeparator2 = new System.Windows.Forms.MenuItem();
            this.miBackup = new System.Windows.Forms.MenuItem();
            this.miRestore = new System.Windows.Forms.MenuItem();
            this.miSeparatorDataBase = new System.Windows.Forms.MenuItem();
            this.miClearHistory = new System.Windows.Forms.MenuItem();
            this.miCompass = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miDebugMode = new System.Windows.Forms.MenuItem();
            this.miAutoSendData = new System.Windows.Forms.MenuItem();
            this.miPreventSleep = new System.Windows.Forms.MenuItem();
            this.miOnlyOneCell = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.miRestartGSM = new System.Windows.Forms.MenuItem();
            this.miResetDevice = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miInterval = new System.Windows.Forms.MenuItem();
            this.mi10Sec = new System.Windows.Forms.MenuItem();
            this.mi30Sec = new System.Windows.Forms.MenuItem();
            this.mi1Min = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miGPS = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.lblURL = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.pnlData = new System.Windows.Forms.Panel();
            this.pbBattery = new System.Windows.Forms.PictureBox();
            this.dgTowers = new System.Windows.Forms.DataGrid();
            this.lblCellTowers = new System.Windows.Forms.Label();
            this.lblGPS = new System.Windows.Forms.Label();
            this.CellIdTimer = new System.Windows.Forms.Timer();
            this.ilBattery = new System.Windows.Forms.ImageList();
            this.pnlOptions.SuspendLayout();
            this.pnlData.SuspendLayout();
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
            this.miSendTo.Text = "Send Data to Cell2xy";
            this.miSendTo.Click += new System.EventHandler(this.miSendToCell2XY);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miView
            // 
            this.miView.MenuItems.Add(this.miViewCellTower);
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
            this.miOptions.MenuItems.Add(this.miAutoSendData);
            this.miOptions.MenuItems.Add(this.miPreventSleep);
            this.miOptions.MenuItems.Add(this.miOnlyOneCell);
            this.miOptions.MenuItems.Add(this.menuItem7);
            this.miOptions.MenuItems.Add(this.miRestartGSM);
            this.miOptions.MenuItems.Add(this.miResetDevice);
            this.miOptions.MenuItems.Add(this.menuItem2);
            this.miOptions.MenuItems.Add(this.miInterval);
            this.miOptions.MenuItems.Add(this.menuItem5);
            this.miOptions.MenuItems.Add(this.miGPS);
            this.miOptions.Text = "Options";
            // 
            // miDebugMode
            // 
            this.miDebugMode.Text = "Debug Mode";
            this.miDebugMode.Click += new System.EventHandler(this.miDebugMode_Click);
            // 
            // miAutoSendData
            // 
            this.miAutoSendData.Text = "Auto send data";
            this.miAutoSendData.Click += new System.EventHandler(this.miAutoSendData_Click);
            // 
            // miPreventSleep
            // 
            this.miPreventSleep.Text = "Prevent sleep mode";
            this.miPreventSleep.Click += new System.EventHandler(this.miPreventSleep_Click);
            // 
            // miOnlyOneCell
            // 
            this.miOnlyOneCell.Text = "Only different Cells";
            this.miOnlyOneCell.Click += new System.EventHandler(this.miOnlyOneCell_Click);
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
            this.miInterval.MenuItems.Add(this.mi10Sec);
            this.miInterval.MenuItems.Add(this.mi30Sec);
            this.miInterval.MenuItems.Add(this.mi1Min);
            this.miInterval.Text = "Set interval";
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
            this.pnlOptions.Controls.Add(this.lblVersion);
            this.pnlOptions.Controls.Add(this.lblProjectName);
            this.pnlOptions.Controls.Add(this.lblStatus);
            this.pnlOptions.Controls.Add(this.pb);
            this.pnlOptions.Location = new System.Drawing.Point(3, 3);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(221, 262);
            // 
            // lblURL
            // 
            this.lblURL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblURL.Location = new System.Drawing.Point(0, 202);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(221, 20);
            this.lblURL.TabIndex = 8;
            this.lblURL.Text = "www.cell2xy.nl";
            this.lblURL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.lblProjectName.Text = "Cell2XY";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // dgTowers
            // 
            this.dgTowers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgTowers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTowers.Location = new System.Drawing.Point(0, 40);
            this.dgTowers.Name = "dgTowers";
            this.dgTowers.Size = new System.Drawing.Size(224, 222);
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
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.pnlOptions);
            this.Menu = this.mmMain;
            this.Name = "OpenCellClientMain";
            this.Text = "Cell2XY";
            this.Load += new System.EventHandler(this.RTScannerMain_Load);
            this.Activated += new System.EventHandler(this.OpenCellClientMain_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.RTScannerMain_Closing);
            this.pnlOptions.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.MenuItem miActions;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.Label lblCellTowers;
        private System.Windows.Forms.Timer CellIdTimer;
        private System.Windows.Forms.DataGrid dgTowers;
        private System.Windows.Forms.MenuItem miStartStop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miDataBase;
        private System.Windows.Forms.Label lblGPS;
        private System.Windows.Forms.MenuItem miInterval;
        private System.Windows.Forms.MenuItem mi10Sec;
        private System.Windows.Forms.MenuItem mi30Sec;
        private System.Windows.Forms.MenuItem mi1Min;
        private System.Windows.Forms.MenuItem miDebugMode;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.LinkLabel lblURL;
        private System.Windows.Forms.MenuItem miRestartGSM;
        private System.Windows.Forms.MenuItem miAutoSendData;
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
        private System.Windows.Forms.MenuItem miDeleteCellTower;
        private System.Windows.Forms.MenuItem miViewCellTower;
        private System.Windows.Forms.MenuItem miSeparator2;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem miOnlyOneCell;

    }
}