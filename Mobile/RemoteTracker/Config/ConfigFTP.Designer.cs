namespace Config
{
    partial class ConfigFTP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmFTP;

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
            this.mmFTP = new System.Windows.Forms.MainMenu();
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.tbRemoteDir = new System.Windows.Forms.TextBox();
            this.lblRemoteDir = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mmFTP
            // 
            this.mmFTP.MenuItems.Add(this.miConfirm);
            this.mmFTP.MenuItems.Add(this.miCancel);
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
            // lblExplanation
            // 
            this.lblExplanation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExplanation.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExplanation.Location = new System.Drawing.Point(0, 0);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(240, 59);
            this.lblExplanation.Text = "Configuring a personal FTP server you will be able to use commands to send files " +
                "to you if you lost your device.";
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(80, 110);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(157, 21);
            this.tbPassword.TabIndex = 27;
            // 
            // lblPassword
            // 
            this.lblPassword.BackColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(3, 110);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(71, 21);
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbUser
            // 
            this.tbUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUser.Location = new System.Drawing.Point(80, 86);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(157, 21);
            this.tbUser.TabIndex = 26;
            // 
            // lblUser
            // 
            this.lblUser.BackColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(3, 86);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(71, 21);
            this.lblUser.Text = "User:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbServer
            // 
            this.tbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServer.Location = new System.Drawing.Point(80, 62);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(157, 21);
            this.tbServer.TabIndex = 25;
            // 
            // lblServer
            // 
            this.lblServer.BackColor = System.Drawing.Color.White;
            this.lblServer.Location = new System.Drawing.Point(3, 62);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(71, 21);
            this.lblServer.Text = "Server:";
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbRemoteDir
            // 
            this.tbRemoteDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRemoteDir.Location = new System.Drawing.Point(80, 134);
            this.tbRemoteDir.Name = "tbRemoteDir";
            this.tbRemoteDir.Size = new System.Drawing.Size(157, 21);
            this.tbRemoteDir.TabIndex = 32;
            // 
            // lblRemoteDir
            // 
            this.lblRemoteDir.BackColor = System.Drawing.Color.White;
            this.lblRemoteDir.Location = new System.Drawing.Point(3, 134);
            this.lblRemoteDir.Name = "lblRemoteDir";
            this.lblRemoteDir.Size = new System.Drawing.Size(71, 21);
            this.lblRemoteDir.Text = "Remote dir:";
            this.lblRemoteDir.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPort
            // 
            this.lblPort.BackColor = System.Drawing.Color.White;
            this.lblPort.Location = new System.Drawing.Point(3, 158);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(71, 21);
            this.lblPort.Text = "Port:";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbPort
            // 
            this.tbPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPort.Location = new System.Drawing.Point(80, 158);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(157, 21);
            this.tbPort.TabIndex = 36;
            // 
            // ConfigureFTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.tbRemoteDir);
            this.Controls.Add(this.lblRemoteDir);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblExplanation);
            this.Menu = this.mmFTP;
            this.Name = "ConfigureFTP";
            this.Text = "FTP";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.MenuItem miCancel;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox tbRemoteDir;
        private System.Windows.Forms.Label lblRemoteDir;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox tbPort;
    }
}