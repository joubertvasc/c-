namespace Config
{
    partial class ConfigEmergency
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmEmergency;

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
            this.mmEmergency = new System.Windows.Forms.MainMenu();
            this.miMenu = new System.Windows.Forms.MenuItem();
            this.miDelete = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.lblEmergency = new System.Windows.Forms.Label();
            this.tbEmergency = new System.Windows.Forms.TextBox();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miOutlook = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mmEmergency
            // 
            this.mmEmergency.MenuItems.Add(this.miMenu);
            this.mmEmergency.MenuItems.Add(this.miCancel);
            // 
            // miMenu
            // 
            this.miMenu.MenuItems.Add(this.miOutlook);
            this.miMenu.MenuItems.Add(this.menuItem2);
            this.miMenu.MenuItems.Add(this.miDelete);
            this.miMenu.MenuItems.Add(this.menuItem1);
            this.miMenu.MenuItems.Add(this.miConfirm);
            this.miMenu.Text = "Menu";
            // 
            // miDelete
            // 
            this.miDelete.Text = "Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
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
            // lblEmergency
            // 
            this.lblEmergency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmergency.Location = new System.Drawing.Point(3, 89);
            this.lblEmergency.Name = "lblEmergency";
            this.lblEmergency.Size = new System.Drawing.Size(234, 20);
            this.lblEmergency.Text = "Emergency Number:";
            // 
            // tbEmergency
            // 
            this.tbEmergency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEmergency.Location = new System.Drawing.Point(3, 112);
            this.tbEmergency.Name = "tbEmergency";
            this.tbEmergency.Size = new System.Drawing.Size(234, 21);
            this.tbEmergency.TabIndex = 1;
            // 
            // lblExplanation
            // 
            this.lblExplanation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExplanation.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExplanation.Location = new System.Drawing.Point(0, 0);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(240, 79);
            this.lblExplanation.Text = "Type a cellular phone number to be used if your SIM card was changed. This phone " +
                "will receive a SMS with useful informations to find your device.";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // miOutlook
            // 
            this.miOutlook.Text = "Outlook";
            this.miOutlook.Click += new System.EventHandler(this.miOutlook_Click);
            // 
            // ConfigEmergency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblExplanation);
            this.Controls.Add(this.tbEmergency);
            this.Controls.Add(this.lblEmergency);
            this.Menu = this.mmEmergency;
            this.Name = "ConfigEmergency";
            this.Text = "Emergency Number";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miMenu;
        private System.Windows.Forms.MenuItem miCancel;
        private System.Windows.Forms.MenuItem miDelete;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.Label lblEmergency;
        private System.Windows.Forms.TextBox tbEmergency;
        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.MenuItem miOutlook;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}