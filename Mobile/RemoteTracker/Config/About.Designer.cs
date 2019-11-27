namespace Config
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmAbout;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.mmAbout = new System.Windows.Forms.MainMenu();
            this.miOk = new System.Windows.Forms.MenuItem();
            this.lblDonate = new System.Windows.Forms.LinkLabel();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.pbVGA = new System.Windows.Forms.PictureBox();
            this.pbQVGA = new System.Windows.Forms.PictureBox();
            this.lblContact = new System.Windows.Forms.LinkLabel();
            this.lblURL = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // mmAbout
            // 
            this.mmAbout.MenuItems.Add(this.miOk);
            // 
            // miOk
            // 
            this.miOk.Text = "Ok";
            this.miOk.Click += new System.EventHandler(this.miOk_Click);
            // 
            // lblDonate
            // 
            this.lblDonate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDonate.Location = new System.Drawing.Point(0, 248);
            this.lblDonate.Name = "lblDonate";
            this.lblDonate.Size = new System.Drawing.Size(240, 20);
            this.lblDonate.TabIndex = 21;
            this.lblDonate.Text = "  Please donate  ";
            this.lblDonate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblDonate.Click += new System.EventHandler(this.lblDonate_Click);
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(3, 158);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(234, 20);
            this.lblAuthor.Text = "Author: Joubert Vasconcelos";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(3, 130);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(234, 28);
            this.lblProjectName.Text = "RTConfig";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbVGA
            // 
            this.pbVGA.Image = ((System.Drawing.Image)(resources.GetObject("pbVGA.Image")));
            this.pbVGA.Location = new System.Drawing.Point(57, 2);
            this.pbVGA.Name = "pbVGA";
            this.pbVGA.Size = new System.Drawing.Size(128, 125);
            this.pbVGA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // pbQVGA
            // 
            this.pbQVGA.Image = ((System.Drawing.Image)(resources.GetObject("pbQVGA.Image")));
            this.pbQVGA.Location = new System.Drawing.Point(3, 2);
            this.pbQVGA.Name = "pbQVGA";
            this.pbQVGA.Size = new System.Drawing.Size(48, 75);
            this.pbQVGA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // lblContact
            // 
            this.lblContact.Location = new System.Drawing.Point(0, 208);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(240, 16);
            this.lblContact.TabIndex = 26;
            this.lblContact.Text = "Contacts: joubertvasc@gmail.com";
            this.lblContact.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblURL
            // 
            this.lblURL.Location = new System.Drawing.Point(0, 188);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(240, 20);
            this.lblURL.TabIndex = 27;
            this.lblURL.Text = "http://remotetracker.sourceforge.net";
            this.lblURL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.lblDonate);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.pbVGA);
            this.Controls.Add(this.pbQVGA);
            this.Menu = this.mmAbout;
            this.Name = "About";
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbVGA;
        private System.Windows.Forms.PictureBox pbQVGA;
        public System.Windows.Forms.LinkLabel lblDonate;
        public System.Windows.Forms.Label lblAuthor;
        public System.Windows.Forms.Label lblProjectName;
        public System.Windows.Forms.MenuItem miOk;
        public System.Windows.Forms.LinkLabel lblContact;
        public System.Windows.Forms.LinkLabel lblURL;
    }
}