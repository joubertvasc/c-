namespace Config
{
    partial class ConfigEmergencyMessage
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
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.lblLabel = new System.Windows.Forms.Label();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.tbEmergencyMessage = new System.Windows.Forms.TextBox();
            this.lblAlertInterval = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miConfirm);
            this.mainMenu1.MenuItems.Add(this.miCancel);
            // 
            // miConfirm
            // 
            this.miConfirm.Text = "Confirm";
            this.miConfirm.Click += new System.EventHandler(this.miConfirm_Click);
            // 
            // miCancel
            // 
            this.miCancel.Text = "Cancel";
            this.miCancel.Click += new System.EventHandler(this.miCancel_Click);
            // 
            // lblLabel
            // 
            this.lblLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLabel.Location = new System.Drawing.Point(0, 49);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(240, 21);
            this.lblLabel.Text = "Emergency message:";
            // 
            // lblExplanation
            // 
            this.lblExplanation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExplanation.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExplanation.Location = new System.Drawing.Point(0, 0);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(240, 49);
            // 
            // tbEmergencyMessage
            // 
            this.tbEmergencyMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEmergencyMessage.Location = new System.Drawing.Point(0, 70);
            this.tbEmergencyMessage.Multiline = true;
            this.tbEmergencyMessage.Name = "tbEmergencyMessage";
            this.tbEmergencyMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbEmergencyMessage.Size = new System.Drawing.Size(240, 135);
            this.tbEmergencyMessage.TabIndex = 4;
            // 
            // lblAlertInterval
            // 
            this.lblAlertInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlertInterval.Location = new System.Drawing.Point(0, 208);
            this.lblAlertInterval.Name = "lblAlertInterval";
            this.lblAlertInterval.Size = new System.Drawing.Size(234, 22);
            this.lblAlertInterval.Text = "Interval between alerts:";
            // 
            // ConfigEmergencyMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblAlertInterval);
            this.Controls.Add(this.tbEmergencyMessage);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.lblExplanation);
            this.Menu = this.mainMenu1;
            this.Name = "ConfigEmergencyMessage";
            this.Text = "Emergency Message";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.MenuItem miCancel;
        public System.Windows.Forms.Label lblLabel;
        public System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.TextBox tbEmergencyMessage;
        public System.Windows.Forms.Label lblAlertInterval;
    }
}