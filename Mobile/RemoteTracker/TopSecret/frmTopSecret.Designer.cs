namespace TopSecret
{
    partial class frmTopSecret
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
            this.miExit = new System.Windows.Forms.MenuItem();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.tmEmergency = new System.Windows.Forms.Timer();
            this.tmAutoStart = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miOk);
            this.mainMenu1.MenuItems.Add(this.miExit);
            // 
            // miOk
            // 
            this.miOk.Text = "Go";
            this.miOk.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // miExit
            // 
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.Location = new System.Drawing.Point(3, 10);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(234, 20);
            this.lblPassword.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(3, 33);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(234, 21);
            this.tbPassword.TabIndex = 1;
            // 
            // lblWait
            // 
            this.lblWait.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWait.Location = new System.Drawing.Point(3, 95);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(234, 20);
            this.lblWait.Text = "label1";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tmEmergency
            // 
            this.tmEmergency.Interval = 300000;
            this.tmEmergency.Tick += new System.EventHandler(this.tmEmergency_Tick);
            // 
            // tmAutoStart
            // 
            this.tmAutoStart.Enabled = true;
            this.tmAutoStart.Interval = 30000;
            this.tmAutoStart.Tick += new System.EventHandler(this.tmAutoStart_Tick);
            // 
            // frmTopSecret
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lblPassword);
            this.Menu = this.mainMenu1;
            this.Name = "frmTopSecret";
            this.Text = "Top Secret!";
            this.Load += new System.EventHandler(this.frmTopSecret_Load);
            this.Activated += new System.EventHandler(this.frmTopSecret_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Timer tmEmergency;
        private System.Windows.Forms.Timer tmAutoStart;
    }
}

