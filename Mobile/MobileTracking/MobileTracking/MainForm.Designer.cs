namespace MobileTracking
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmMenu;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mmMenu = new System.Windows.Forms.MainMenu();
            this.miStartStop = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.tmSend = new System.Windows.Forms.Timer();
            this.lbNote = new System.Windows.Forms.Label();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.pbVGA = new System.Windows.Forms.PictureBox();
            this.pbQVGA = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // mmMenu
            // 
            this.mmMenu.MenuItems.Add(this.miStartStop);
            this.mmMenu.MenuItems.Add(this.miExit);
            // 
            // miStartStop
            // 
            this.miStartStop.Text = "Start";
            this.miStartStop.Click += new System.EventHandler(this.miStart_Click);
            // 
            // miExit
            // 
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // tmSend
            // 
            this.tmSend.Interval = 300000;
            this.tmSend.Tick += new System.EventHandler(this.tmSend_Tick);
            // 
            // lbNote
            // 
            this.lbNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNote.Location = new System.Drawing.Point(3, 219);
            this.lbNote.Name = "lbNote";
            this.lbNote.Size = new System.Drawing.Size(234, 22);
            // 
            // tbHost
            // 
            this.tbHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHost.Location = new System.Drawing.Point(3, 244);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(234, 21);
            this.tbHost.TabIndex = 6;
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(3, 163);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(234, 20);
            this.lblAuthor.Text = "Author: Joubert Vasconcelos";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(3, 135);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(234, 28);
            this.lblProjectName.Text = "Mobile Tracking";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbVGA
            // 
            this.pbVGA.Image = ((System.Drawing.Image)(resources.GetObject("pbVGA.Image")));
            this.pbVGA.Location = new System.Drawing.Point(57, 7);
            this.pbVGA.Name = "pbVGA";
            this.pbVGA.Size = new System.Drawing.Size(111, 111);
            this.pbVGA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // pbQVGA
            // 
            this.pbQVGA.Image = ((System.Drawing.Image)(resources.GetObject("pbQVGA.Image")));
            this.pbQVGA.Location = new System.Drawing.Point(3, 7);
            this.pbQVGA.Name = "pbQVGA";
            this.pbQVGA.Size = new System.Drawing.Size(48, 48);
            this.pbQVGA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.pbVGA);
            this.Controls.Add(this.pbQVGA);
            this.Controls.Add(this.tbHost);
            this.Controls.Add(this.lbNote);
            this.Menu = this.mmMenu;
            this.Name = "MainForm";
            this.Text = "Mobile Tracking";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miStartStop;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Timer tmSend;
        private System.Windows.Forms.Label lbNote;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.PictureBox pbVGA;
        private System.Windows.Forms.PictureBox pbQVGA;
    }
}

