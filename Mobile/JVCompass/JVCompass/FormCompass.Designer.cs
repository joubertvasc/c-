namespace JVCompass
{
    partial class FormCompass
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
            this.mmMain = new System.Windows.Forms.MainMenu();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miStart = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miConfigGPS = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miMetric = new System.Windows.Forms.MenuItem();
            this.miImperial = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mmMain
            // 
            this.mmMain.MenuItems.Add(this.miOptions);
            this.mmMain.MenuItems.Add(this.miExit);
            // 
            // miOptions
            // 
            this.miOptions.MenuItems.Add(this.miStart);
            this.miOptions.MenuItems.Add(this.menuItem1);
            this.miOptions.MenuItems.Add(this.miConfigGPS);
            this.miOptions.MenuItems.Add(this.menuItem3);
            this.miOptions.MenuItems.Add(this.miMetric);
            this.miOptions.MenuItems.Add(this.miImperial);
            this.miOptions.MenuItems.Add(this.menuItem2);
            this.miOptions.MenuItems.Add(this.miAbout);
            this.miOptions.Text = "Menu";
            // 
            // miStart
            // 
            this.miStart.Text = "Start";
            this.miStart.Click += new System.EventHandler(this.miStart_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miConfigGPS
            // 
            this.miConfigGPS.Text = "Config GPS";
            this.miConfigGPS.Click += new System.EventHandler(this.miConfigGPS_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // miMetric
            // 
            this.miMetric.Text = "Metric";
            this.miMetric.Click += new System.EventHandler(this.miMetric_Click);
            // 
            // miImperial
            // 
            this.miImperial.Text = "Imperial";
            this.miImperial.Click += new System.EventHandler(this.miImperial_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
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
            // FormCompass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Menu = this.mmMain;
            this.Name = "FormCompass";
            this.Text = "JVCompass";
            this.Load += new System.EventHandler(this.FormCompass_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormCompass_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miStart;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miConfigGPS;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem miMetric;
        private System.Windows.Forms.MenuItem miImperial;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miAbout;
    }
}

