namespace SafetyRide
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.miMenu = new System.Windows.Forms.MenuItem();
            this.miStartStop = new System.Windows.Forms.MenuItem();
            this.miCompass = new System.Windows.Forms.MenuItem();
            this.miExportGoogle = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miConfigMessage = new System.Windows.Forms.MenuItem();
            this.miGPS = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miMetric = new System.Windows.Forms.MenuItem();
            this.miImperial = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miPreventSleepMode = new System.Windows.Forms.MenuItem();
            this.miDebugMode = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.lblAltitude = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCoordinates = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbBattery = new System.Windows.Forms.PictureBox();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblDonate = new System.Windows.Forms.LinkLabel();
            this.lblGPS = new System.Windows.Forms.Label();
            this.lblGPSStatus = new System.Windows.Forms.Label();
            this.ilBattery = new System.Windows.Forms.ImageList();
            this.tmMessage = new System.Windows.Forms.Timer();
            this.sfKMZ = new System.Windows.Forms.SaveFileDialog();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.miPlaySound = new System.Windows.Forms.MenuItem();
            this.miSelectSound = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miMenu);
            this.mainMenu1.MenuItems.Add(this.miExit);
            // 
            // miMenu
            // 
            this.miMenu.MenuItems.Add(this.miStartStop);
            this.miMenu.MenuItems.Add(this.miCompass);
            this.miMenu.MenuItems.Add(this.miExportGoogle);
            this.miMenu.MenuItems.Add(this.menuItem2);
            this.miMenu.MenuItems.Add(this.miOptions);
            this.miMenu.MenuItems.Add(this.miAbout);
            this.miMenu.Text = "Menu";
            // 
            // miStartStop
            // 
            this.miStartStop.Text = "Start Ride";
            this.miStartStop.Click += new System.EventHandler(this.miStartStop_Click);
            // 
            // miCompass
            // 
            this.miCompass.Text = "Compass";
            this.miCompass.Click += new System.EventHandler(this.miCompass_Click);
            // 
            // miExportGoogle
            // 
            this.miExportGoogle.Text = "Export to Google Earth";
            this.miExportGoogle.Click += new System.EventHandler(this.miExportGoogle_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // miOptions
            // 
            this.miOptions.MenuItems.Add(this.miConfigMessage);
            this.miOptions.MenuItems.Add(this.miGPS);
            this.miOptions.MenuItems.Add(this.menuItem3);
            this.miOptions.MenuItems.Add(this.miMetric);
            this.miOptions.MenuItems.Add(this.miImperial);
            this.miOptions.MenuItems.Add(this.menuItem5);
            this.miOptions.MenuItems.Add(this.miPlaySound);
            this.miOptions.MenuItems.Add(this.miSelectSound);
            this.miOptions.MenuItems.Add(this.menuItem6);
            this.miOptions.MenuItems.Add(this.miPreventSleepMode);
            this.miOptions.MenuItems.Add(this.miDebugMode);
            this.miOptions.Text = "Options";
            // 
            // miConfigMessage
            // 
            this.miConfigMessage.Text = "Configure Message";
            this.miConfigMessage.Click += new System.EventHandler(this.miConfigMessage_Click);
            // 
            // miGPS
            // 
            this.miGPS.Text = "Configure GPS";
            this.miGPS.Click += new System.EventHandler(this.miGPS_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // miMetric
            // 
            this.miMetric.Text = "Use Metric System";
            this.miMetric.Click += new System.EventHandler(this.miMetric_Click);
            // 
            // miImperial
            // 
            this.miImperial.Text = "Use Imperial System";
            this.miImperial.Click += new System.EventHandler(this.miImperial_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // miPreventSleepMode
            // 
            this.miPreventSleepMode.Text = "Prevent Sleep Mode";
            this.miPreventSleepMode.Click += new System.EventHandler(this.miPreventSleepMode_Click);
            // 
            // miDebugMode
            // 
            this.miDebugMode.Text = "Debug Mode";
            this.miDebugMode.Click += new System.EventHandler(this.miDebugMode_Click);
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
            // lblAltitude
            // 
            this.lblAltitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAltitude.Location = new System.Drawing.Point(74, 125);
            this.lblAltitude.Name = "lblAltitude";
            this.lblAltitude.Size = new System.Drawing.Size(161, 20);
            this.lblAltitude.Text = "label5";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 20);
            this.label7.Text = "Altitude:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpeed.Location = new System.Drawing.Point(74, 105);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(161, 20);
            this.lblSpeed.Text = "label5";
            // 
            // lblDistance
            // 
            this.lblDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDistance.Location = new System.Drawing.Point(74, 85);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(161, 20);
            this.lblDistance.Text = "label5";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.Text = "Speed:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.Text = "Distance:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCoordinates
            // 
            this.lblCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCoordinates.Location = new System.Drawing.Point(74, 65);
            this.lblCoordinates.Name = "lblCoordinates";
            this.lblCoordinates.Size = new System.Drawing.Size(161, 20);
            this.lblCoordinates.Text = "label3";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.Text = "Position:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pbBattery
            // 
            this.pbBattery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBattery.Location = new System.Drawing.Point(219, 0);
            this.pbBattery.Name = "pbBattery";
            this.pbBattery.Size = new System.Drawing.Size(16, 16);
            this.pbBattery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(0, 0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(240, 25);
            this.lblProjectName.Text = "SafetyRide";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDonate
            // 
            this.lblDonate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDonate.Location = new System.Drawing.Point(0, 248);
            this.lblDonate.Name = "lblDonate";
            this.lblDonate.Size = new System.Drawing.Size(240, 20);
            this.lblDonate.TabIndex = 26;
            this.lblDonate.Text = "Please donate.";
            this.lblDonate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblGPS
            // 
            this.lblGPS.Location = new System.Drawing.Point(12, 45);
            this.lblGPS.Name = "lblGPS";
            this.lblGPS.Size = new System.Drawing.Size(56, 20);
            this.lblGPS.Text = "GPS:";
            this.lblGPS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblGPSStatus
            // 
            this.lblGPSStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGPSStatus.Location = new System.Drawing.Point(74, 45);
            this.lblGPSStatus.Name = "lblGPSStatus";
            this.lblGPSStatus.Size = new System.Drawing.Size(161, 20);
            this.lblGPSStatus.Text = "label1";
            this.ilBattery.Images.Clear();
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            // 
            // tmMessage
            // 
            this.tmMessage.Tick += new System.EventHandler(this.tmMessage_Tick);
            // 
            // sfKMZ
            // 
            this.sfKMZ.Filter = "(*.kmz)|*.kmz";
            // 
            // lblAuthor
            // 
            this.lblAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAuthor.Location = new System.Drawing.Point(3, 162);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(237, 20);
            this.lblAuthor.Text = "Author: Joubert Vasconcelos";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // miPlaySound
            // 
            this.miPlaySound.Text = "Play Sound After Send SMS";
            this.miPlaySound.Click += new System.EventHandler(this.miPlaySound_Click);
            // 
            // miSelectSound
            // 
            this.miSelectSound.Text = "Select Sound File";
            this.miSelectSound.Click += new System.EventHandler(this.miSelectSound_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblGPSStatus);
            this.Controls.Add(this.lblGPS);
            this.Controls.Add(this.lblAltitude);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCoordinates);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbBattery);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.lblDonate);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "SafetyRide";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miMenu;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem miStartStop;
        private System.Windows.Forms.MenuItem miCompass;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miPreventSleepMode;
        private System.Windows.Forms.MenuItem miDebugMode;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miGPS;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem miMetric;
        private System.Windows.Forms.MenuItem miImperial;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.Label lblAltitude;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCoordinates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbBattery;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.LinkLabel lblDonate;
        private System.Windows.Forms.Label lblGPS;
        private System.Windows.Forms.Label lblGPSStatus;
        private System.Windows.Forms.MenuItem miExportGoogle;
        private System.Windows.Forms.ImageList ilBattery;
        private System.Windows.Forms.MenuItem miConfigMessage;
        private System.Windows.Forms.Timer tmMessage;
        private System.Windows.Forms.SaveFileDialog sfKMZ;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.MenuItem miPlaySound;
        private System.Windows.Forms.MenuItem miSelectSound;
        private System.Windows.Forms.MenuItem menuItem6;
    }
}

