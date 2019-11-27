namespace JVGPS.Forms
{
    partial class Compass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Compass));
            this.mmCompass = new System.Windows.Forms.MainMenu();
            this.miOk = new System.Windows.Forms.MenuItem();
            this.pbCompass = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFixType = new System.Windows.Forms.Label();
            this.lblAltitude = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblCoordinates = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDMS = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.picGSVSkyview = new System.Windows.Forms.PictureBox();
            this.picGSVSignals = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // mmCompass
            // 
            this.mmCompass.MenuItems.Add(this.miOk);
            // 
            // miOk
            // 
            this.miOk.Text = "OK";
            this.miOk.Click += new System.EventHandler(this.miOk_Click);
            // 
            // pbCompass
            // 
            this.pbCompass.Image = ((System.Drawing.Image)(resources.GetObject("pbCompass.Image")));
            this.pbCompass.Location = new System.Drawing.Point(3, 27);
            this.pbCompass.Name = "pbCompass";
            this.pbCompass.Size = new System.Drawing.Size(99, 90);
            this.pbCompass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCompass.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCompass_Paint);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.Text = "Lat, Lon:";
            // 
            // lblFixType
            // 
            this.lblFixType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFixType.Location = new System.Drawing.Point(4, 4);
            this.lblFixType.Name = "lblFixType";
            this.lblFixType.Size = new System.Drawing.Size(72, 20);
            this.lblFixType.Text = "Not fixed";
            // 
            // lblAltitude
            // 
            this.lblAltitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAltitude.Location = new System.Drawing.Point(124, 165);
            this.lblAltitude.Name = "lblAltitude";
            this.lblAltitude.Size = new System.Drawing.Size(112, 15);
            this.lblAltitude.Text = "Altitude";
            this.lblAltitude.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpeed.Location = new System.Drawing.Point(4, 165);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(114, 15);
            this.lblSpeed.Text = "Speed";
            // 
            // lblCoordinates
            // 
            this.lblCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCoordinates.Location = new System.Drawing.Point(58, 120);
            this.lblCoordinates.Name = "lblCoordinates";
            this.lblCoordinates.Size = new System.Drawing.Size(175, 20);
            this.lblCoordinates.Text = "label8";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.Text = "Degrees:";
            // 
            // lblDMS
            // 
            this.lblDMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDMS.Location = new System.Drawing.Point(58, 140);
            this.lblDMS.Name = "lblDMS";
            this.lblDMS.Size = new System.Drawing.Size(176, 20);
            this.lblDMS.Text = "label9";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.Location = new System.Drawing.Point(165, 4);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(71, 20);
            this.lblTime.Text = "lblTime";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // picGSVSkyview
            // 
            this.picGSVSkyview.Location = new System.Drawing.Point(138, 27);
            this.picGSVSkyview.Name = "picGSVSkyview";
            this.picGSVSkyview.Size = new System.Drawing.Size(99, 90);
            // 
            // picGSVSignals
            // 
            this.picGSVSignals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picGSVSignals.Location = new System.Drawing.Point(4, 183);
            this.picGSVSignals.Name = "picGSVSignals";
            this.picGSVSignals.Size = new System.Drawing.Size(233, 82);
            // 
            // Compass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.picGSVSignals);
            this.Controls.Add(this.picGSVSkyview);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDMS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCoordinates);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblAltitude);
            this.Controls.Add(this.lblFixType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbCompass);
            this.Menu = this.mmCompass;
            this.Name = "Compass";
            this.Text = "Compass";
            this.Load += new System.EventHandler(this.Compass_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCompass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAltitude;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblCoordinates;
        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDMS;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox picGSVSkyview;
        private System.Windows.Forms.PictureBox picGSVSignals;
        public System.Windows.Forms.MainMenu mmCompass;
        public System.Windows.Forms.Label lblFixType;
    }
}