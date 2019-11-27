namespace Tester
{
    partial class Form1
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.miStart = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.rbUseInternal = new System.Windows.Forms.RadioButton();
            this.rbUseManualGPS = new System.Windows.Forms.RadioButton();
            this.log = new System.Windows.Forms.ListBox();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btCompass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miStart);
            this.mainMenu1.MenuItems.Add(this.miExit);
            // 
            // miStart
            // 
            this.miStart.Text = "Start";
            this.miStart.Click += new System.EventHandler(this.miStart_Click);
            // 
            // miExit
            // 
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // rbUseInternal
            // 
            this.rbUseInternal.Checked = true;
            this.rbUseInternal.Location = new System.Drawing.Point(3, 3);
            this.rbUseInternal.Name = "rbUseInternal";
            this.rbUseInternal.Size = new System.Drawing.Size(234, 20);
            this.rbUseInternal.TabIndex = 0;
            this.rbUseInternal.Text = "Use Windows Intermediate Driver";
            this.rbUseInternal.Click += new System.EventHandler(this.rbUseInternal_Click);
            // 
            // rbUseManualGPS
            // 
            this.rbUseManualGPS.Location = new System.Drawing.Point(3, 29);
            this.rbUseManualGPS.Name = "rbUseManualGPS";
            this.rbUseManualGPS.Size = new System.Drawing.Size(234, 20);
            this.rbUseManualGPS.TabIndex = 1;
            this.rbUseManualGPS.TabStop = false;
            this.rbUseManualGPS.Text = "Use Custom GPS";
            this.rbUseManualGPS.Click += new System.EventHandler(this.rbUseInternal_Click);
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.log.Location = new System.Drawing.Point(3, 109);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(231, 128);
            this.log.TabIndex = 23;
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.Location = new System.Drawing.Point(97, 82);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(108, 22);
            this.comboBoxBaudRate.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(28, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.Text = "Baud Rate:";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.Location = new System.Drawing.Point(97, 54);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(65, 22);
            this.comboBoxPort.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(28, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 20);
            this.label1.Text = "Port:";
            // 
            // btCompass
            // 
            this.btCompass.Location = new System.Drawing.Point(3, 245);
            this.btCompass.Name = "btCompass";
            this.btCompass.Size = new System.Drawing.Size(72, 20);
            this.btCompass.TabIndex = 26;
            this.btCompass.Text = "Compass";
            this.btCompass.Click += new System.EventHandler(this.btCompass_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btCompass);
            this.Controls.Add(this.log);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbUseManualGPS);
            this.Controls.Add(this.rbUseInternal);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "GPS Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbUseInternal;
        private System.Windows.Forms.RadioButton rbUseManualGPS;
        private System.Windows.Forms.ListBox log;
        public System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem miStart;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Button btCompass;
    }
}

