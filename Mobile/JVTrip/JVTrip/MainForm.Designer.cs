namespace JVTrip
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mmMain = new System.Windows.Forms.MainMenu();
            this.miActions = new System.Windows.Forms.MenuItem();
            this.miStartStop = new System.Windows.Forms.MenuItem();
            this.miCompass = new System.Windows.Forms.MenuItem();
            this.miWhereAmI = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.miAddTrip = new System.Windows.Forms.MenuItem();
            this.miExportGoogle = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.miAddNote = new System.Windows.Forms.MenuItem();
            this.miAddCost = new System.Windows.Forms.MenuItem();
            this.miAddPicture = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.miDatabase = new System.Windows.Forms.MenuItem();
            this.miBackup = new System.Windows.Forms.MenuItem();
            this.miRestore = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miClear = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miPreventSleepMode = new System.Windows.Forms.MenuItem();
            this.miDebugMode = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miGPS = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miMetric = new System.Windows.Forms.MenuItem();
            this.miImperial = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.lblAltitude = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCoordinates = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTrips = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbBattery = new System.Windows.Forms.PictureBox();
            this.lblURL = new System.Windows.Forms.LinkLabel();
            this.lblContact = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblDonate = new System.Windows.Forms.LinkLabel();
            this.ilBattery = new System.Windows.Forms.ImageList();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.ipMain = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.sfgKMZ = new System.Windows.Forms.SaveFileDialog();
            this.pnlOptions.SuspendLayout();
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
            this.miActions.MenuItems.Add(this.miCompass);
            this.miActions.MenuItems.Add(this.miWhereAmI);
            this.miActions.MenuItems.Add(this.menuItem9);
            this.miActions.MenuItems.Add(this.miAddTrip);
            this.miActions.MenuItems.Add(this.miExportGoogle);
            this.miActions.MenuItems.Add(this.menuItem10);
            this.miActions.MenuItems.Add(this.miAddNote);
            this.miActions.MenuItems.Add(this.miAddCost);
            this.miActions.MenuItems.Add(this.miAddPicture);
            this.miActions.MenuItems.Add(this.menuItem7);
            this.miActions.MenuItems.Add(this.miDatabase);
            this.miActions.MenuItems.Add(this.menuItem1);
            this.miActions.MenuItems.Add(this.miOptions);
            this.miActions.MenuItems.Add(this.menuItem4);
            this.miActions.MenuItems.Add(this.miAbout);
            this.miActions.Text = "Menu";
            // 
            // miStartStop
            // 
            this.miStartStop.Text = "Start Trip";
            this.miStartStop.Click += new System.EventHandler(this.miStartStop_Click);
            // 
            // miCompass
            // 
            this.miCompass.Enabled = false;
            this.miCompass.Text = "Compass";
            this.miCompass.Click += new System.EventHandler(this.miCompass_Click);
            // 
            // miWhereAmI
            // 
            this.miWhereAmI.Enabled = false;
            this.miWhereAmI.Text = "Where Am I?";
            this.miWhereAmI.Click += new System.EventHandler(this.miWhereAmI_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Text = "-";
            // 
            // miAddTrip
            // 
            this.miAddTrip.Text = "Stored Trips";
            this.miAddTrip.Click += new System.EventHandler(this.miAddTrip_Click);
            // 
            // miExportGoogle
            // 
            this.miExportGoogle.Text = "Export Trip to Google";
            this.miExportGoogle.Click += new System.EventHandler(this.miExportGoogle_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Text = "-";
            // 
            // miAddNote
            // 
            this.miAddNote.Text = "Notes";
            this.miAddNote.Click += new System.EventHandler(this.miAddNote_Click);
            // 
            // miAddCost
            // 
            this.miAddCost.Text = "Costs";
            this.miAddCost.Click += new System.EventHandler(this.miAddCost_Click);
            // 
            // miAddPicture
            // 
            this.miAddPicture.Text = "Pictures";
            this.miAddPicture.Click += new System.EventHandler(this.miAddPicture_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "-";
            // 
            // miDatabase
            // 
            this.miDatabase.MenuItems.Add(this.miBackup);
            this.miDatabase.MenuItems.Add(this.miRestore);
            this.miDatabase.MenuItems.Add(this.menuItem2);
            this.miDatabase.MenuItems.Add(this.miClear);
            this.miDatabase.Text = "Database";
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
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // miClear
            // 
            this.miClear.Text = "Clear Database";
            this.miClear.Click += new System.EventHandler(this.miClear_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miOptions
            // 
            this.miOptions.MenuItems.Add(this.miPreventSleepMode);
            this.miOptions.MenuItems.Add(this.miDebugMode);
            this.miOptions.MenuItems.Add(this.menuItem5);
            this.miOptions.MenuItems.Add(this.miGPS);
            this.miOptions.MenuItems.Add(this.menuItem3);
            this.miOptions.MenuItems.Add(this.miMetric);
            this.miOptions.MenuItems.Add(this.miImperial);
            this.miOptions.Text = "Options";
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
            // menuItem5
            // 
            this.menuItem5.Text = "-";
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
            // menuItem4
            // 
            this.menuItem4.Text = "-";
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
            this.pnlOptions.AutoScroll = true;
            this.pnlOptions.Controls.Add(this.lblAltitude);
            this.pnlOptions.Controls.Add(this.label7);
            this.pnlOptions.Controls.Add(this.lblCost);
            this.pnlOptions.Controls.Add(this.label6);
            this.pnlOptions.Controls.Add(this.lblSpeed);
            this.pnlOptions.Controls.Add(this.lblDistance);
            this.pnlOptions.Controls.Add(this.label4);
            this.pnlOptions.Controls.Add(this.label3);
            this.pnlOptions.Controls.Add(this.lblCoordinates);
            this.pnlOptions.Controls.Add(this.label2);
            this.pnlOptions.Controls.Add(this.cbTrips);
            this.pnlOptions.Controls.Add(this.label1);
            this.pnlOptions.Controls.Add(this.pbBattery);
            this.pnlOptions.Controls.Add(this.lblURL);
            this.pnlOptions.Controls.Add(this.lblContact);
            this.pnlOptions.Controls.Add(this.lblVersion);
            this.pnlOptions.Controls.Add(this.lblProjectName);
            this.pnlOptions.Controls.Add(this.lblDonate);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOptions.Location = new System.Drawing.Point(0, 0);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(240, 268);
            // 
            // lblAltitude
            // 
            this.lblAltitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAltitude.Location = new System.Drawing.Point(74, 168);
            this.lblAltitude.Name = "lblAltitude";
            this.lblAltitude.Size = new System.Drawing.Size(161, 20);
            this.lblAltitude.Text = "label5";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 20);
            this.label7.Text = "Altitude:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCost
            // 
            this.lblCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCost.Location = new System.Drawing.Point(74, 108);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(161, 20);
            this.lblCost.Text = "label5";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.Text = "Cost:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpeed.Location = new System.Drawing.Point(74, 148);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(161, 20);
            this.lblSpeed.Text = "label5";
            // 
            // lblDistance
            // 
            this.lblDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDistance.Location = new System.Drawing.Point(74, 128);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(161, 20);
            this.lblDistance.Text = "label5";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.Text = "Speed:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.Text = "Distance:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCoordinates
            // 
            this.lblCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCoordinates.Location = new System.Drawing.Point(74, 88);
            this.lblCoordinates.Name = "lblCoordinates";
            this.lblCoordinates.Size = new System.Drawing.Size(161, 20);
            this.lblCoordinates.Text = "label3";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.Text = "Position:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbTrips
            // 
            this.cbTrips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTrips.Location = new System.Drawing.Point(12, 63);
            this.cbTrips.Name = "cbTrips";
            this.cbTrips.Size = new System.Drawing.Size(223, 22);
            this.cbTrips.TabIndex = 18;
            this.cbTrips.SelectedIndexChanged += new System.EventHandler(this.cbTrips_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 20);
            this.label1.Text = "Current trip:";
            // 
            // pbBattery
            // 
            this.pbBattery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBattery.Location = new System.Drawing.Point(219, 0);
            this.pbBattery.Name = "pbBattery";
            this.pbBattery.Size = new System.Drawing.Size(16, 16);
            this.pbBattery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblURL
            // 
            this.lblURL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblURL.Location = new System.Drawing.Point(0, 212);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(240, 20);
            this.lblURL.TabIndex = 8;
            this.lblURL.Text = "http://jvtrip.sourceforge.net";
            this.lblURL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblURL.Click += new System.EventHandler(this.lblURL_Click);
            // 
            // lblContact
            // 
            this.lblContact.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblContact.Location = new System.Drawing.Point(0, 232);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(240, 16);
            this.lblContact.TabIndex = 7;
            this.lblContact.Text = "Contacts: joubertvasc@gmail.com";
            this.lblContact.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblContact.Click += new System.EventHandler(this.lblContact_Click_1);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.Location = new System.Drawing.Point(3, 25);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(232, 20);
            this.lblVersion.Text = "Version 0.0.1-0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(0, 0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(240, 25);
            this.lblProjectName.Text = "JVTrip";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDonate
            // 
            this.lblDonate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDonate.Location = new System.Drawing.Point(0, 248);
            this.lblDonate.Name = "lblDonate";
            this.lblDonate.Size = new System.Drawing.Size(240, 20);
            this.lblDonate.TabIndex = 14;
            this.lblDonate.Text = "Please donate.";
            this.lblDonate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblDonate.Click += new System.EventHandler(this.lblDonate_Click);
            this.ilBattery.Images.Clear();
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.ilBattery.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            // 
            // saveDialog
            // 
            this.saveDialog.Filter = "(*.occ)|*.occ";
            // 
            // openDialog
            // 
            this.openDialog.Filter = "(*.occ)|*.occ";
            // 
            // sfgKMZ
            // 
            this.sfgKMZ.Filter = "(*.kmz)|*.kmz";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlOptions);
            this.Menu = this.mmMain;
            this.Name = "MainForm";
            this.Text = "JVTrip";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.pnlOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miActions;
        private System.Windows.Forms.MenuItem miGPS;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.LinkLabel lblURL;
        private System.Windows.Forms.LinkLabel lblContact;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.MenuItem miCompass;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ImageList ilBattery;
        private System.Windows.Forms.LinkLabel lblDonate;
        private System.Windows.Forms.MenuItem miDatabase;
        private System.Windows.Forms.MenuItem miBackup;
        private System.Windows.Forms.MenuItem miRestore;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miClear;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miPreventSleepMode;
        private System.Windows.Forms.MenuItem miDebugMode;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.PictureBox pbBattery;
        private System.Windows.Forms.ComboBox cbTrips;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCoordinates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.MenuItem miStartStop;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem miAddNote;
        private System.Windows.Forms.MenuItem miAddCost;
        private System.Windows.Forms.MenuItem miAddPicture;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuItem miExportGoogle;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem miAddTrip;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.Label lblAltitude;
        private System.Windows.Forms.Label label7;
        private Microsoft.WindowsCE.Forms.InputPanel ipMain;
        private System.Windows.Forms.SaveFileDialog sfgKMZ;
        private System.Windows.Forms.MenuItem miWhereAmI;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem miMetric;
        private System.Windows.Forms.MenuItem miImperial;
    }
}

