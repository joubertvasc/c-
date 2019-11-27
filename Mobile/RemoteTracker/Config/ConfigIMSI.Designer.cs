namespace Config
{
    partial class ConfigIMSI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmSimCard;

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
            this.mmSimCard = new System.Windows.Forms.MainMenu();
            this.miMenu = new System.Windows.Forms.MenuItem();
            this.miAutoDetect = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miDelete = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.lblIMSI = new System.Windows.Forms.Label();
            this.tbIMSI = new System.Windows.Forms.TextBox();
            this.lblAlias = new System.Windows.Forms.Label();
            this.tbAlias = new System.Windows.Forms.TextBox();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mmSimCard
            // 
            this.mmSimCard.MenuItems.Add(this.miMenu);
            this.mmSimCard.MenuItems.Add(this.miCancel);
            // 
            // miMenu
            // 
            this.miMenu.MenuItems.Add(this.miAutoDetect);
            this.miMenu.MenuItems.Add(this.menuItem4);
            this.miMenu.MenuItems.Add(this.miDelete);
            this.miMenu.MenuItems.Add(this.menuItem1);
            this.miMenu.MenuItems.Add(this.miConfirm);
            this.miMenu.Text = "Menu";
            // 
            // miAutoDetect
            // 
            this.miAutoDetect.Text = "AutoDetect";
            this.miAutoDetect.Click += new System.EventHandler(this.miAutoDetect_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
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
            this.miConfirm.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // miCancel
            // 
            this.miCancel.Text = "Cancel";
            this.miCancel.Click += new System.EventHandler(this.miCancel_Click);
            // 
            // lblIMSI
            // 
            this.lblIMSI.Location = new System.Drawing.Point(3, 88);
            this.lblIMSI.Name = "lblIMSI";
            this.lblIMSI.Size = new System.Drawing.Size(100, 17);
            this.lblIMSI.Text = "Sim Card IMSI: ";
            // 
            // tbIMSI
            // 
            this.tbIMSI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIMSI.BackColor = System.Drawing.SystemColors.GrayText;
            this.tbIMSI.Location = new System.Drawing.Point(3, 108);
            this.tbIMSI.Name = "tbIMSI";
            this.tbIMSI.ReadOnly = true;
            this.tbIMSI.Size = new System.Drawing.Size(234, 21);
            this.tbIMSI.TabIndex = 1;
            // 
            // lblAlias
            // 
            this.lblAlias.Location = new System.Drawing.Point(3, 132);
            this.lblAlias.Name = "lblAlias";
            this.lblAlias.Size = new System.Drawing.Size(100, 15);
            this.lblAlias.Text = "Sim Card Alias:";
            // 
            // tbAlias
            // 
            this.tbAlias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAlias.Location = new System.Drawing.Point(3, 150);
            this.tbAlias.Name = "tbAlias";
            this.tbAlias.Size = new System.Drawing.Size(234, 21);
            this.tbAlias.TabIndex = 3;
            // 
            // lblExplanation
            // 
            this.lblExplanation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExplanation.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExplanation.Location = new System.Drawing.Point(0, 0);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(240, 79);
            this.lblExplanation.Text = "Configuring known Sim cards you will be able to change them without receive SMS a" +
                "lerting you about this exchange.";
            // 
            // ConfigIMSI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblExplanation);
            this.Controls.Add(this.tbAlias);
            this.Controls.Add(this.lblAlias);
            this.Controls.Add(this.tbIMSI);
            this.Controls.Add(this.lblIMSI);
            this.Menu = this.mmSimCard;
            this.Name = "ConfigIMSI";
            this.Text = "SIM Card";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miMenu;
        private System.Windows.Forms.MenuItem miCancel;
        private System.Windows.Forms.MenuItem miDelete;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.MenuItem miAutoDetect;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Label lblIMSI;
        private System.Windows.Forms.TextBox tbIMSI;
        private System.Windows.Forms.Label lblAlias;
        private System.Windows.Forms.TextBox tbAlias;
        private System.Windows.Forms.Label lblExplanation;
    }
}