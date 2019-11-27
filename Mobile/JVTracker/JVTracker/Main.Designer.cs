namespace JVTracker
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmTracker;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mmTracker = new System.Windows.Forms.MainMenu();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.miApply = new System.Windows.Forms.MenuItem();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblExecuted = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbStop = new System.Windows.Forms.PictureBox();
            this.pbStart = new System.Windows.Forms.PictureBox();
            this.lblNextExec = new System.Windows.Forms.Label();
            this.lblLastExec = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.pnlConfig = new System.Windows.Forms.Panel();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.nuInterval = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbWebServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbGPRS = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbPhoneNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSMS = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.lblTrial = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblContact = new System.Windows.Forms.LinkLabel();
            this.lblURL = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tmMain = new System.Windows.Forms.Timer();
            this.ilStartStop = new System.Windows.Forms.ImageList();
            this.ipTracker = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.tmReg = new System.Windows.Forms.Timer();
            this.tcMain.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.pnlConfig.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tpAbout.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmTracker
            // 
            this.mmTracker.MenuItems.Add(this.miExit);
            this.mmTracker.MenuItems.Add(this.miApply);
            // 
            // miExit
            // 
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // miApply
            // 
            this.miApply.Text = "Apply";
            this.miApply.Click += new System.EventHandler(this.miApply_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpMain);
            this.tcMain.Controls.Add(this.tpConfig);
            this.tcMain.Controls.Add(this.tpAbout);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(240, 268);
            this.tcMain.TabIndex = 0;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpMain
            // 
            this.tpMain.Controls.Add(this.pnlMain);
            this.tpMain.Location = new System.Drawing.Point(0, 0);
            this.tpMain.Name = "tpMain";
            this.tpMain.Size = new System.Drawing.Size(240, 245);
            this.tpMain.Text = "Main";
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.lblExecuted);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.lblNextExec);
            this.pnlMain.Controls.Add(this.lblLastExec);
            this.pnlMain.Controls.Add(this.lblProjectName);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(240, 245);
            // 
            // lblExecuted
            // 
            this.lblExecuted.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExecuted.Location = new System.Drawing.Point(0, 69);
            this.lblExecuted.Name = "lblExecuted";
            this.lblExecuted.Size = new System.Drawing.Size(240, 20);
            this.lblExecuted.Text = "Never executed";
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 179);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(240, 20);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbStop);
            this.panel1.Controls.Add(this.pbStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 199);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 46);
            // 
            // pbStop
            // 
            this.pbStop.Location = new System.Drawing.Point(123, 3);
            this.pbStop.Name = "pbStop";
            this.pbStop.Size = new System.Drawing.Size(113, 38);
            this.pbStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbStop_MouseDown);
            this.pbStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbStop_MouseUp);
            // 
            // pbStart
            // 
            this.pbStart.Location = new System.Drawing.Point(4, 3);
            this.pbStart.Name = "pbStart";
            this.pbStart.Size = new System.Drawing.Size(113, 38);
            this.pbStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbStart_MouseDown);
            this.pbStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbStart_MouseUp);
            // 
            // lblNextExec
            // 
            this.lblNextExec.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNextExec.Location = new System.Drawing.Point(0, 49);
            this.lblNextExec.Name = "lblNextExec";
            this.lblNextExec.Size = new System.Drawing.Size(240, 20);
            this.lblNextExec.Text = "Next execution:";
            // 
            // lblLastExec
            // 
            this.lblLastExec.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLastExec.Location = new System.Drawing.Point(0, 29);
            this.lblLastExec.Name = "lblLastExec";
            this.lblLastExec.Size = new System.Drawing.Size(240, 20);
            this.lblLastExec.Text = "Last execution: ";
            // 
            // lblProjectName
            // 
            this.lblProjectName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(0, 0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(240, 29);
            this.lblProjectName.Text = "JVTracker";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.pnlConfig);
            this.tpConfig.Controls.Add(this.label1);
            this.tpConfig.Location = new System.Drawing.Point(0, 0);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Size = new System.Drawing.Size(232, 242);
            this.tpConfig.Text = "Config";
            // 
            // pnlConfig
            // 
            this.pnlConfig.AutoScroll = true;
            this.pnlConfig.Controls.Add(this.cbAutoStart);
            this.pnlConfig.Controls.Add(this.panel4);
            this.pnlConfig.Controls.Add(this.panel3);
            this.pnlConfig.Controls.Add(this.cbGPRS);
            this.pnlConfig.Controls.Add(this.panel2);
            this.pnlConfig.Controls.Add(this.cbSMS);
            this.pnlConfig.Controls.Add(this.label2);
            this.pnlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConfig.Location = new System.Drawing.Point(0, 29);
            this.pnlConfig.Name = "pnlConfig";
            this.pnlConfig.Size = new System.Drawing.Size(232, 213);
            // 
            // cbAutoStart
            // 
            this.cbAutoStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbAutoStart.Location = new System.Drawing.Point(0, 182);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(232, 20);
            this.cbAutoStart.TabIndex = 24;
            this.cbAutoStart.Text = "Auto start sending coordinates?";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.nuInterval);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 154);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(232, 28);
            // 
            // nuInterval
            // 
            this.nuInterval.Location = new System.Drawing.Point(129, 3);
            this.nuInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nuInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuInterval.Name = "nuInterval";
            this.nuInterval.Size = new System.Drawing.Size(56, 22);
            this.nuInterval.TabIndex = 2;
            this.nuInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuInterval.ValueChanged += new System.EventHandler(this.nuInterval_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.Text = "Interval in minutes:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tbWebServer);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 107);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(232, 47);
            // 
            // tbWebServer
            // 
            this.tbWebServer.Location = new System.Drawing.Point(23, 20);
            this.tbWebServer.Name = "tbWebServer";
            this.tbWebServer.Size = new System.Drawing.Size(206, 21);
            this.tbWebServer.TabIndex = 18;
            this.tbWebServer.GotFocus += new System.EventHandler(this.tbPhoneNumber_GotFocus);
            this.tbWebServer.LostFocus += new System.EventHandler(this.tbPhoneNumber_LostFocus);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(23, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Webserver:";
            // 
            // cbGPRS
            // 
            this.cbGPRS.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbGPRS.Location = new System.Drawing.Point(0, 87);
            this.cbGPRS.Name = "cbGPRS";
            this.cbGPRS.Size = new System.Drawing.Size(232, 20);
            this.cbGPRS.TabIndex = 15;
            this.cbGPRS.Text = "Internet";
            this.cbGPRS.CheckStateChanged += new System.EventHandler(this.cbGPRS_CheckStateChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbPhoneNumber);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(232, 47);
            // 
            // tbPhoneNumber
            // 
            this.tbPhoneNumber.Location = new System.Drawing.Point(23, 20);
            this.tbPhoneNumber.Name = "tbPhoneNumber";
            this.tbPhoneNumber.Size = new System.Drawing.Size(206, 21);
            this.tbPhoneNumber.TabIndex = 16;
            this.tbPhoneNumber.GotFocus += new System.EventHandler(this.tbPhoneNumber_GotFocus);
            this.tbPhoneNumber.LostFocus += new System.EventHandler(this.tbPhoneNumber_LostFocus);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Phone number:";
            // 
            // cbSMS
            // 
            this.cbSMS.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSMS.Location = new System.Drawing.Point(0, 20);
            this.cbSMS.Name = "cbSMS";
            this.cbSMS.Size = new System.Drawing.Size(232, 20);
            this.cbSMS.TabIndex = 13;
            this.cbSMS.Text = "SMS";
            this.cbSMS.CheckStateChanged += new System.EventHandler(this.cbSMS_CheckStateChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 20);
            this.label2.Text = "Send coordinates using:";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 29);
            this.label1.Text = "Options";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tpAbout
            // 
            this.tpAbout.Controls.Add(this.lblTrial);
            this.tpAbout.Controls.Add(this.panel5);
            this.tpAbout.Controls.Add(this.label7);
            this.tpAbout.Controls.Add(this.label6);
            this.tpAbout.Location = new System.Drawing.Point(0, 0);
            this.tpAbout.Name = "tpAbout";
            this.tpAbout.Size = new System.Drawing.Size(232, 242);
            this.tpAbout.Text = "About";
            // 
            // lblTrial
            // 
            this.lblTrial.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTrial.Location = new System.Drawing.Point(0, 72);
            this.lblTrial.Name = "lblTrial";
            this.lblTrial.Size = new System.Drawing.Size(232, 67);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblContact);
            this.panel5.Controls.Add(this.lblURL);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 199);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(232, 43);
            // 
            // lblContact
            // 
            this.lblContact.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblContact.Location = new System.Drawing.Point(0, 20);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(232, 16);
            this.lblContact.TabIndex = 1;
            this.lblContact.Text = "Contacts: joubertvasc@gmail.com";
            // 
            // lblURL
            // 
            this.lblURL.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblURL.Location = new System.Drawing.Point(0, 0);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(232, 20);
            this.lblURL.TabIndex = 4;
            this.lblURL.Text = "http://remotetracker.sourceforge.net";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(0, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(232, 33);
            this.label7.Text = "Version 1.0";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(232, 39);
            this.label6.Text = "JVTracker";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tmMain
            // 
            this.tmMain.Tick += new System.EventHandler(this.tmMain_Tick);
            // 
            // ilStartStop
            // 
            this.ilStartStop.ImageSize = new System.Drawing.Size(113, 38);
            this.ilStartStop.Images.Clear();
            this.ilStartStop.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.ilStartStop.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.ilStartStop.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.ilStartStop.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.ilStartStop.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.ilStartStop.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            // 
            // ipTracker
            // 
            this.ipTracker.EnabledChanged += new System.EventHandler(this.ipTracker_EnabledChanged);
            // 
            // tmReg
            // 
            this.tmReg.Enabled = true;
            this.tmReg.Interval = 30000;
            this.tmReg.Tick += new System.EventHandler(this.tmReg_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tcMain);
            this.Menu = this.mmTracker;
            this.Name = "Main";
            this.Text = "JVTracker";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Main_Closing);
            this.tcMain.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.pnlConfig.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tpAbout.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNextExec;
        private System.Windows.Forms.Label lblLastExec;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Timer tmMain;
        private System.Windows.Forms.PictureBox pbStop;
        private System.Windows.Forms.PictureBox pbStart;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ImageList ilStartStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlConfig;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tbWebServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbGPRS;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbPhoneNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbSMS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAutoStart;
        private System.Windows.Forms.NumericUpDown nuInterval;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuItem miApply;
        private System.Windows.Forms.Label lblExecuted;
        private Microsoft.WindowsCE.Forms.InputPanel ipTracker;
        private System.Windows.Forms.TabPage tpAbout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.LinkLabel lblContact;
        private System.Windows.Forms.LinkLabel lblURL;
        private System.Windows.Forms.Label lblTrial;
        private System.Windows.Forms.Timer tmReg;
    }
}

