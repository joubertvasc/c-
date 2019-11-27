namespace Config
{
    partial class Config
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmConfig;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.mmConfig = new System.Windows.Forms.MainMenu();
            this.miMenu = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miLanguages = new System.Windows.Forms.MenuItem();
            this.miSepLang = new System.Windows.Forms.MenuItem();
            this.miDebugMode = new System.Windows.Forms.MenuItem();
            this.miScreenOff = new System.Windows.Forms.MenuItem();
            this.miActiveTopSecret = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miPassword = new System.Windows.Forms.MenuItem();
            this.miEMail = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miGPSAttempts = new System.Windows.Forms.MenuItem();
            this.miTimeoutELT = new System.Windows.Forms.MenuItem();
            this.miAlarmSound = new System.Windows.Forms.MenuItem();
            this.miFTP = new System.Windows.Forms.MenuItem();
            this.miGPS = new System.Windows.Forms.MenuItem();
            this.menuItem49 = new System.Windows.Forms.MenuItem();
            this.miHide = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miCellIDConverter = new System.Windows.Forms.MenuItem();
            this.miOpenCellID = new System.Windows.Forms.MenuItem();
            this.miGoogle = new System.Windows.Forms.MenuItem();
            this.miCellDB = new System.Windows.Forms.MenuItem();
            this.miWEB = new System.Windows.Forms.MenuItem();
            this.miCreateWebAccount = new System.Windows.Forms.MenuItem();
            this.miSendConfigurations = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miSIMCards = new System.Windows.Forms.MenuItem();
            this.miSIM1 = new System.Windows.Forms.MenuItem();
            this.miSIM2 = new System.Windows.Forms.MenuItem();
            this.miSIM3 = new System.Windows.Forms.MenuItem();
            this.miSIM4 = new System.Windows.Forms.MenuItem();
            this.miEmergencyNumbers = new System.Windows.Forms.MenuItem();
            this.miEN1 = new System.Windows.Forms.MenuItem();
            this.miEN2 = new System.Windows.Forms.MenuItem();
            this.miEN3 = new System.Windows.Forms.MenuItem();
            this.miEN4 = new System.Windows.Forms.MenuItem();
            this.miEmergencyEMails = new System.Windows.Forms.MenuItem();
            this.miEEM1 = new System.Windows.Forms.MenuItem();
            this.miEEM2 = new System.Windows.Forms.MenuItem();
            this.miEEM3 = new System.Windows.Forms.MenuItem();
            this.miEEM4 = new System.Windows.Forms.MenuItem();
            this.miEmergencyMessage = new System.Windows.Forms.MenuItem();
            this.menuItem47 = new System.Windows.Forms.MenuItem();
            this.miCommandLog = new System.Windows.Forms.MenuItem();
            this.miApply = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miHelp = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.pbQVGA = new System.Windows.Forms.PictureBox();
            this.pbVGA = new System.Windows.Forms.PictureBox();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblDonate = new System.Windows.Forms.LinkLabel();
            this.lblBlog = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // mmConfig
            // 
            this.mmConfig.MenuItems.Add(this.miMenu);
            this.mmConfig.MenuItems.Add(this.miExit);
            // 
            // miMenu
            // 
            this.miMenu.MenuItems.Add(this.miOptions);
            this.miMenu.MenuItems.Add(this.menuItem4);
            this.miMenu.MenuItems.Add(this.miCellIDConverter);
            this.miMenu.MenuItems.Add(this.miWEB);
            this.miMenu.MenuItems.Add(this.menuItem5);
            this.miMenu.MenuItems.Add(this.miSIMCards);
            this.miMenu.MenuItems.Add(this.miEmergencyNumbers);
            this.miMenu.MenuItems.Add(this.miEmergencyEMails);
            this.miMenu.MenuItems.Add(this.miEmergencyMessage);
            this.miMenu.MenuItems.Add(this.menuItem47);
            this.miMenu.MenuItems.Add(this.miCommandLog);
            this.miMenu.MenuItems.Add(this.miApply);
            this.miMenu.MenuItems.Add(this.menuItem3);
            this.miMenu.MenuItems.Add(this.miHelp);
            this.miMenu.MenuItems.Add(this.miAbout);
            this.miMenu.Text = "Menu";
            // 
            // miOptions
            // 
            this.miOptions.MenuItems.Add(this.miLanguages);
            this.miOptions.MenuItems.Add(this.miSepLang);
            this.miOptions.MenuItems.Add(this.miDebugMode);
            this.miOptions.MenuItems.Add(this.miScreenOff);
            this.miOptions.MenuItems.Add(this.miActiveTopSecret);
            this.miOptions.MenuItems.Add(this.menuItem1);
            this.miOptions.MenuItems.Add(this.miPassword);
            this.miOptions.MenuItems.Add(this.miEMail);
            this.miOptions.MenuItems.Add(this.menuItem2);
            this.miOptions.MenuItems.Add(this.miGPSAttempts);
            this.miOptions.MenuItems.Add(this.miTimeoutELT);
            this.miOptions.MenuItems.Add(this.miAlarmSound);
            this.miOptions.MenuItems.Add(this.miFTP);
            this.miOptions.MenuItems.Add(this.miGPS);
            this.miOptions.MenuItems.Add(this.menuItem49);
            this.miOptions.MenuItems.Add(this.miHide);
            this.miOptions.Text = "Options";
            // 
            // miLanguages
            // 
            this.miLanguages.Text = "Languages";
            this.miLanguages.Click += new System.EventHandler(this.miLanguages_Click);
            // 
            // miSepLang
            // 
            this.miSepLang.Text = "-";
            // 
            // miDebugMode
            // 
            this.miDebugMode.Text = "Debug Mode";
            this.miDebugMode.Click += new System.EventHandler(this.miDebugMode_Click);
            // 
            // miScreenOff
            // 
            this.miScreenOff.Text = "Turn Screen Off When Running";
            this.miScreenOff.Click += new System.EventHandler(this.miScreenOff_Click);
            // 
            // miActiveTopSecret
            // 
            this.miActiveTopSecret.Text = "Active TopSecret";
            this.miActiveTopSecret.Click += new System.EventHandler(this.miActiveTopSecret_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miPassword
            // 
            this.miPassword.Text = "Password";
            this.miPassword.Click += new System.EventHandler(this.miPassword_Click);
            // 
            // miEMail
            // 
            this.miEMail.Text = "E-Mail Configuration";
            this.miEMail.Click += new System.EventHandler(this.miEMail_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // miGPSAttempts
            // 
            this.miGPSAttempts.Text = "GPS Attempts for GPS commands";
            this.miGPSAttempts.Click += new System.EventHandler(this.miGPSAttempts_Click);
            // 
            // miTimeoutELT
            // 
            this.miTimeoutELT.Text = "Timeout for ELT command";
            this.miTimeoutELT.Click += new System.EventHandler(this.miTimeoutELT_Click);
            // 
            // miAlarmSound
            // 
            this.miAlarmSound.Text = "Define Alarm Sound";
            this.miAlarmSound.Click += new System.EventHandler(this.miAlarmSound_Click);
            // 
            // miFTP
            // 
            this.miFTP.Text = "FTP Configuration";
            this.miFTP.Click += new System.EventHandler(this.miFTP_Click);
            // 
            // miGPS
            // 
            this.miGPS.Text = "GPS Configuration";
            this.miGPS.Click += new System.EventHandler(this.miGPS_Click);
            // 
            // menuItem49
            // 
            this.menuItem49.Text = "-";
            // 
            // miHide
            // 
            this.miHide.Text = "Hide RemoteTracker";
            this.miHide.Click += new System.EventHandler(this.miHide_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // miCellIDConverter
            // 
            this.miCellIDConverter.MenuItems.Add(this.miOpenCellID);
            this.miCellIDConverter.MenuItems.Add(this.miGoogle);
            this.miCellIDConverter.MenuItems.Add(this.miCellDB);
            this.miCellIDConverter.Text = "CellID to Coordinates Provider";
            // 
            // miOpenCellID
            // 
            this.miOpenCellID.Text = "OpenCellID";
            this.miOpenCellID.Click += new System.EventHandler(this.miOpenCellID_Click);
            // 
            // miGoogle
            // 
            this.miGoogle.Text = "Google Maps";
            this.miGoogle.Click += new System.EventHandler(this.miGoogle_Click);
            // 
            // miCellDB
            // 
            this.miCellDB.Text = "CellDB";
            this.miCellDB.Click += new System.EventHandler(this.miCellDB_Click);
            // 
            // miWEB
            // 
            this.miWEB.MenuItems.Add(this.miCreateWebAccount);
            this.miWEB.MenuItems.Add(this.miSendConfigurations);
            this.miWEB.Text = "WEB";
            // 
            // miCreateWebAccount
            // 
            this.miCreateWebAccount.Text = "Create Account";
            this.miCreateWebAccount.Click += new System.EventHandler(this.miCreateWebAccount_Click);
            // 
            // miSendConfigurations
            // 
            this.miSendConfigurations.Text = "Send Configurations";
            this.miSendConfigurations.Click += new System.EventHandler(this.miSendConfigurations_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // miSIMCards
            // 
            this.miSIMCards.MenuItems.Add(this.miSIM1);
            this.miSIMCards.MenuItems.Add(this.miSIM2);
            this.miSIMCards.MenuItems.Add(this.miSIM3);
            this.miSIMCards.MenuItems.Add(this.miSIM4);
            this.miSIMCards.Text = "SIM Cards";
            // 
            // miSIM1
            // 
            this.miSIM1.Text = "1 - ";
            this.miSIM1.Click += new System.EventHandler(this.miSIM1_Click);
            // 
            // miSIM2
            // 
            this.miSIM2.Text = "2 - ";
            this.miSIM2.Click += new System.EventHandler(this.miSIM1_Click);
            // 
            // miSIM3
            // 
            this.miSIM3.Text = "3 - ";
            this.miSIM3.Click += new System.EventHandler(this.miSIM1_Click);
            // 
            // miSIM4
            // 
            this.miSIM4.Text = "4 - ";
            this.miSIM4.Click += new System.EventHandler(this.miSIM1_Click);
            // 
            // miEmergencyNumbers
            // 
            this.miEmergencyNumbers.MenuItems.Add(this.miEN1);
            this.miEmergencyNumbers.MenuItems.Add(this.miEN2);
            this.miEmergencyNumbers.MenuItems.Add(this.miEN3);
            this.miEmergencyNumbers.MenuItems.Add(this.miEN4);
            this.miEmergencyNumbers.Text = "Emergency Numbers";
            // 
            // miEN1
            // 
            this.miEN1.Text = "1 - ";
            this.miEN1.Click += new System.EventHandler(this.miEN1_Click);
            // 
            // miEN2
            // 
            this.miEN2.Text = "2 - ";
            this.miEN2.Click += new System.EventHandler(this.miEN1_Click);
            // 
            // miEN3
            // 
            this.miEN3.Text = "3 - ";
            this.miEN3.Click += new System.EventHandler(this.miEN1_Click);
            // 
            // miEN4
            // 
            this.miEN4.Text = "4 - ";
            this.miEN4.Click += new System.EventHandler(this.miEN1_Click);
            // 
            // miEmergencyEMails
            // 
            this.miEmergencyEMails.MenuItems.Add(this.miEEM1);
            this.miEmergencyEMails.MenuItems.Add(this.miEEM2);
            this.miEmergencyEMails.MenuItems.Add(this.miEEM3);
            this.miEmergencyEMails.MenuItems.Add(this.miEEM4);
            this.miEmergencyEMails.Text = "Emergency E-Mails";
            // 
            // miEEM1
            // 
            this.miEEM1.Text = "1 - ";
            this.miEEM1.Click += new System.EventHandler(this.miEEM1_Click);
            // 
            // miEEM2
            // 
            this.miEEM2.Text = "2 - ";
            this.miEEM2.Click += new System.EventHandler(this.miEEM1_Click);
            // 
            // miEEM3
            // 
            this.miEEM3.Text = "3 - ";
            this.miEEM3.Click += new System.EventHandler(this.miEEM1_Click);
            // 
            // miEEM4
            // 
            this.miEEM4.Text = "4 - ";
            this.miEEM4.Click += new System.EventHandler(this.miEEM1_Click);
            // 
            // miEmergencyMessage
            // 
            this.miEmergencyMessage.Text = "Emergency Message";
            this.miEmergencyMessage.Click += new System.EventHandler(this.miEmergencyMessage_Click);
            // 
            // menuItem47
            // 
            this.menuItem47.Text = "-";
            // 
            // miCommandLog
            // 
            this.miCommandLog.Text = "View Command Log";
            this.miCommandLog.Click += new System.EventHandler(this.miCommandLog_Click);
            // 
            // miApply
            // 
            this.miApply.Text = "Apply Configurations";
            this.miApply.Click += new System.EventHandler(this.miApply_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // miHelp
            // 
            this.miHelp.Text = "Help";
            this.miHelp.Click += new System.EventHandler(this.miHelp_Click);
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
            // pbQVGA
            // 
            this.pbQVGA.Image = ((System.Drawing.Image)(resources.GetObject("pbQVGA.Image")));
            this.pbQVGA.Location = new System.Drawing.Point(3, 3);
            this.pbQVGA.Name = "pbQVGA";
            this.pbQVGA.Size = new System.Drawing.Size(48, 75);
            this.pbQVGA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // pbVGA
            // 
            this.pbVGA.Image = ((System.Drawing.Image)(resources.GetObject("pbVGA.Image")));
            this.pbVGA.Location = new System.Drawing.Point(57, 3);
            this.pbVGA.Name = "pbVGA";
            this.pbVGA.Size = new System.Drawing.Size(128, 125);
            this.pbVGA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(3, 131);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(234, 28);
            this.lblProjectName.Text = "RTConfig";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(3, 159);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(234, 20);
            this.lblAuthor.Text = "Author: Joubert Vasconcelos";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDonate
            // 
            this.lblDonate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDonate.Location = new System.Drawing.Point(0, 248);
            this.lblDonate.Name = "lblDonate";
            this.lblDonate.Size = new System.Drawing.Size(240, 20);
            this.lblDonate.TabIndex = 16;
            this.lblDonate.Text = "  Please donate  ";
            this.lblDonate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblDonate.Click += new System.EventHandler(this.lblDonate_Click);
            // 
            // lblBlog
            // 
            this.lblBlog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBlog.Location = new System.Drawing.Point(3, 179);
            this.lblBlog.Name = "lblBlog";
            this.lblBlog.Size = new System.Drawing.Size(234, 20);
            this.lblBlog.TabIndex = 21;
            this.lblBlog.Text = "http://remotetracker.blogspot.com";
            this.lblBlog.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblBlog.Click += new System.EventHandler(this.lblBlog_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblBlog);
            this.Controls.Add(this.lblDonate);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.pbVGA);
            this.Controls.Add(this.pbQVGA);
            this.Menu = this.mmConfig;
            this.Name = "Config";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Config_Load);
            this.Activated += new System.EventHandler(this.Config_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Config_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbQVGA;
        private System.Windows.Forms.PictureBox pbVGA;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.LinkLabel lblDonate;
        private System.Windows.Forms.MenuItem miMenu;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem miLanguages;
        private System.Windows.Forms.MenuItem miSIMCards;
        private System.Windows.Forms.MenuItem miSIM1;
        private System.Windows.Forms.MenuItem miSIM2;
        private System.Windows.Forms.MenuItem miSIM3;
        private System.Windows.Forms.MenuItem miSIM4;
        private System.Windows.Forms.MenuItem miEmergencyNumbers;
        private System.Windows.Forms.MenuItem miEN1;
        private System.Windows.Forms.MenuItem miEN2;
        private System.Windows.Forms.MenuItem miEN3;
        private System.Windows.Forms.MenuItem miEN4;
        private System.Windows.Forms.MenuItem miEmergencyEMails;
        private System.Windows.Forms.MenuItem miEEM1;
        private System.Windows.Forms.MenuItem miEEM2;
        private System.Windows.Forms.MenuItem miEEM3;
        private System.Windows.Forms.MenuItem miEEM4;
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miDebugMode;
        private System.Windows.Forms.MenuItem miScreenOff;
        private System.Windows.Forms.MenuItem menuItem47;
        private System.Windows.Forms.MenuItem miPassword;
        private System.Windows.Forms.MenuItem miGPS;
        private System.Windows.Forms.MenuItem miEMail;
        private System.Windows.Forms.MenuItem miAlarmSound;
        private System.Windows.Forms.MenuItem miCommandLog;
        private System.Windows.Forms.MenuItem miHelp;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.MenuItem menuItem49;
        private System.Windows.Forms.MenuItem miHide;
        private System.Windows.Forms.MenuItem miGPSAttempts;
        private System.Windows.Forms.MenuItem miTimeoutELT;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miFTP;
        private System.Windows.Forms.MenuItem miApply;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miEmergencyMessage;
        private System.Windows.Forms.MenuItem miSepLang;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miActiveTopSecret;
        private System.Windows.Forms.MenuItem miCellIDConverter;
        private System.Windows.Forms.MenuItem miOpenCellID;
        private System.Windows.Forms.MenuItem miGoogle;
        private System.Windows.Forms.MenuItem miCellDB;
        private System.Windows.Forms.MenuItem miWEB;
        private System.Windows.Forms.MenuItem miCreateWebAccount;
        private System.Windows.Forms.MenuItem miSendConfigurations;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.LinkLabel lblBlog;

    }
}

