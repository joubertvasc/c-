namespace Config
{
    partial class ConfigEMail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmEMail;

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
            this.mmEMail = new System.Windows.Forms.MainMenu();
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.tbSubject = new System.Windows.Forms.TextBox();
            this.lblDefaultSubject = new System.Windows.Forms.Label();
            this.tbrecipientEMail = new System.Windows.Forms.TextBox();
            this.lblDefaultrecipientEMail = new System.Windows.Forms.Label();
            this.tbrecipientName = new System.Windows.Forms.TextBox();
            this.lblDefaultrecipientName = new System.Windows.Forms.Label();
            this.cbEMailAccount = new System.Windows.Forms.ComboBox();
            this.lblEMailAccount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mmEMail
            // 
            this.mmEMail.MenuItems.Add(this.miConfirm);
            this.mmEMail.MenuItems.Add(this.miCancel);
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
            this.lblExplanation.Size = new System.Drawing.Size(176, 59);
            this.lblExplanation.Text = "Setting an e-mail account you will be able to use several commands that reply by " +
                "e-mail to the address specified here.";
            // 
            // tbSubject
            // 
            this.tbSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSubject.Location = new System.Drawing.Point(66, 147);
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new System.Drawing.Size(107, 21);
            this.tbSubject.TabIndex = 21;
            // 
            // lblDefaultSubject
            // 
            this.lblDefaultSubject.BackColor = System.Drawing.Color.White;
            this.lblDefaultSubject.Location = new System.Drawing.Point(3, 147);
            this.lblDefaultSubject.Name = "lblDefaultSubject";
            this.lblDefaultSubject.Size = new System.Drawing.Size(57, 21);
            this.lblDefaultSubject.Text = "Subject:";
            this.lblDefaultSubject.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbrecipientEMail
            // 
            this.tbrecipientEMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbrecipientEMail.Location = new System.Drawing.Point(66, 119);
            this.tbrecipientEMail.Name = "tbrecipientEMail";
            this.tbrecipientEMail.Size = new System.Drawing.Size(107, 21);
            this.tbrecipientEMail.TabIndex = 20;
            // 
            // lblDefaultrecipientEMail
            // 
            this.lblDefaultrecipientEMail.BackColor = System.Drawing.Color.White;
            this.lblDefaultrecipientEMail.Location = new System.Drawing.Point(3, 120);
            this.lblDefaultrecipientEMail.Name = "lblDefaultrecipientEMail";
            this.lblDefaultrecipientEMail.Size = new System.Drawing.Size(57, 21);
            this.lblDefaultrecipientEMail.Text = "E-mail:";
            this.lblDefaultrecipientEMail.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbrecipientName
            // 
            this.tbrecipientName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbrecipientName.Location = new System.Drawing.Point(66, 95);
            this.tbrecipientName.Name = "tbrecipientName";
            this.tbrecipientName.Size = new System.Drawing.Size(107, 21);
            this.tbrecipientName.TabIndex = 19;
            // 
            // lblDefaultrecipientName
            // 
            this.lblDefaultrecipientName.BackColor = System.Drawing.Color.White;
            this.lblDefaultrecipientName.Location = new System.Drawing.Point(3, 95);
            this.lblDefaultrecipientName.Name = "lblDefaultrecipientName";
            this.lblDefaultrecipientName.Size = new System.Drawing.Size(57, 21);
            this.lblDefaultrecipientName.Text = "Recipient:";
            this.lblDefaultrecipientName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbEMailAccount
            // 
            this.cbEMailAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEMailAccount.Location = new System.Drawing.Point(66, 67);
            this.cbEMailAccount.Name = "cbEMailAccount";
            this.cbEMailAccount.Size = new System.Drawing.Size(107, 22);
            this.cbEMailAccount.TabIndex = 18;
            // 
            // lblEMailAccount
            // 
            this.lblEMailAccount.BackColor = System.Drawing.Color.White;
            this.lblEMailAccount.Location = new System.Drawing.Point(3, 67);
            this.lblEMailAccount.Name = "lblEMailAccount";
            this.lblEMailAccount.Size = new System.Drawing.Size(57, 22);
            this.lblEMailAccount.Text = "Account:";
            this.lblEMailAccount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ConfigEMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(176, 180);
            this.Controls.Add(this.tbSubject);
            this.Controls.Add(this.lblDefaultSubject);
            this.Controls.Add(this.tbrecipientEMail);
            this.Controls.Add(this.lblDefaultrecipientEMail);
            this.Controls.Add(this.tbrecipientName);
            this.Controls.Add(this.lblDefaultrecipientName);
            this.Controls.Add(this.cbEMailAccount);
            this.Controls.Add(this.lblEMailAccount);
            this.Controls.Add(this.lblExplanation);
            this.Menu = this.mmEMail;
            this.Name = "ConfigEMail";
            this.Text = "E-Mail";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.TextBox tbSubject;
        private System.Windows.Forms.Label lblDefaultSubject;
        private System.Windows.Forms.TextBox tbrecipientEMail;
        private System.Windows.Forms.Label lblDefaultrecipientEMail;
        private System.Windows.Forms.TextBox tbrecipientName;
        private System.Windows.Forms.Label lblDefaultrecipientName;
        private System.Windows.Forms.ComboBox cbEMailAccount;
        private System.Windows.Forms.Label lblEMailAccount;
        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.MenuItem miCancel;
    }
}