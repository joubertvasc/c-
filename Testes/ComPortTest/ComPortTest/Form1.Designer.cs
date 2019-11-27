namespace ComPortTest
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miStart = new System.Windows.Forms.MenuItem();
            this.miSaveLog = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.ListBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tmCheckConnection = new System.Windows.Forms.Timer();
            this.tmGetData = new System.Windows.Forms.Timer();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCulture = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.miStart);
            this.menuItem1.MenuItems.Add(this.miSaveLog);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.Text = "Actions";
            // 
            // miStart
            // 
            this.miStart.Text = "Start";
            this.miStart.Click += new System.EventHandler(this.miStart_Click);
            // 
            // miSaveLog
            // 
            this.miSaveLog.Text = "Save log";
            this.miSaveLog.Click += new System.EventHandler(this.miSaveLog_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Clear log";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Exit";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.Location = new System.Drawing.Point(77, 59);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(108, 22);
            this.comboBoxBaudRate.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(8, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.Text = "Baud Rate:";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.Location = new System.Drawing.Point(77, 31);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(65, 22);
            this.comboBoxPort.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 20);
            this.label1.Text = "Port:";
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.log.Location = new System.Drawing.Point(6, 94);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(231, 170);
            this.log.TabIndex = 18;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "(*.txt)|*.txt";
            // 
            // tmCheckConnection
            // 
            this.tmCheckConnection.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // tmGetData
            // 
            this.tmGetData.Tick += new System.EventHandler(this.tmGetData_Tick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.Text = "Culture:";
            // 
            // cbCulture
            // 
            this.cbCulture.Location = new System.Drawing.Point(77, 3);
            this.cbCulture.Name = "cbCulture";
            this.cbCulture.Size = new System.Drawing.Size(100, 22);
            this.cbCulture.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cbCulture);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.log);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxPort);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Com Port Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ListBox log;
        private System.Windows.Forms.MenuItem miStart;
        private System.Windows.Forms.MenuItem miSaveLog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.Timer tmCheckConnection;
        private System.Windows.Forms.Timer tmGetData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCulture;
    }
}

