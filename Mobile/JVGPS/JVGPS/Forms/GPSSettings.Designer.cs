namespace JVGPS.Forms
{
    partial class GPSSettings
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
            this.miOk = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.lblSerialPort = new System.Windows.Forms.Label();
            this.rbUseManualGPS = new System.Windows.Forms.CheckBox();
            this.rbUseInternal = new System.Windows.Forms.CheckBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.tmConfig = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miOk);
            this.mainMenu1.MenuItems.Add(this.miCancel);
            // 
            // miOk
            // 
            this.miOk.Text = "Ok";
            this.miOk.Click += new System.EventHandler(this.miOk_Click);
            // 
            // miCancel
            // 
            this.miCancel.Text = "Cancel";
            this.miCancel.Click += new System.EventHandler(this.miCancel_Click);
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBaudRate.Location = new System.Drawing.Point(74, 120);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(93, 22);
            this.comboBoxBaudRate.TabIndex = 28;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.Location = new System.Drawing.Point(1, 122);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(67, 20);
            this.lblBaudRate.Text = "Baud rate:";
            this.lblBaudRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPort.Location = new System.Drawing.Point(74, 92);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(93, 22);
            this.comboBoxPort.TabIndex = 27;
            // 
            // lblSerialPort
            // 
            this.lblSerialPort.Location = new System.Drawing.Point(3, 94);
            this.lblSerialPort.Name = "lblSerialPort";
            this.lblSerialPort.Size = new System.Drawing.Size(65, 20);
            this.lblSerialPort.Text = "Serial port:";
            this.lblSerialPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // rbUseManualGPS
            // 
            this.rbUseManualGPS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbUseManualGPS.Location = new System.Drawing.Point(3, 67);
            this.rbUseManualGPS.Name = "rbUseManualGPS";
            this.rbUseManualGPS.Size = new System.Drawing.Size(164, 20);
            this.rbUseManualGPS.TabIndex = 26;
            this.rbUseManualGPS.Text = "Use Custom GPS";
            this.rbUseManualGPS.Click += new System.EventHandler(this.rbUseInternal_Click_1);
            // 
            // rbUseInternal
            // 
            this.rbUseInternal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbUseInternal.Checked = true;
            this.rbUseInternal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbUseInternal.Location = new System.Drawing.Point(3, 41);
            this.rbUseInternal.Name = "rbUseInternal";
            this.rbUseInternal.Size = new System.Drawing.Size(164, 20);
            this.rbUseInternal.TabIndex = 25;
            this.rbUseInternal.Text = "Use Windows Managed GPS";
            this.rbUseInternal.Click += new System.EventHandler(this.rbUseInternal_Click_1);
            // 
            // lblCaption
            // 
            this.lblCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCaption.Location = new System.Drawing.Point(3, 9);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(164, 29);
            this.lblCaption.Text = "Please select the GPS type you want to use:";
            // 
            // tmConfig
            // 
            this.tmConfig.Tick += new System.EventHandler(this.tmConfig_Tick);
            // 
            // GPSSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(176, 180);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.comboBoxPort);
            this.Controls.Add(this.lblSerialPort);
            this.Controls.Add(this.rbUseManualGPS);
            this.Controls.Add(this.rbUseInternal);
            this.Menu = this.mainMenu1;
            this.Name = "GPSSettings";
            this.Text = "GPSSettings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.MenuItem miCancel;
        public System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Label lblBaudRate;
        public System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Label lblSerialPort;
        private System.Windows.Forms.CheckBox rbUseManualGPS;
        private System.Windows.Forms.CheckBox rbUseInternal;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Timer tmConfig;


    }
}