namespace Config
{
    partial class ConfigNumber
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mnNumber;

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
            this.mnNumber = new System.Windows.Forms.MainMenu();
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.lblLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mnNumber
            // 
            this.mnNumber.MenuItems.Add(this.miConfirm);
            this.mnNumber.MenuItems.Add(this.miCancel);
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
            this.lblExplanation.Size = new System.Drawing.Size(240, 49);
            // 
            // lblLabel
            // 
            this.lblLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLabel.Location = new System.Drawing.Point(3, 61);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(137, 21);
            this.lblLabel.Text = "label:";
            // 
            // ConfigNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.lblExplanation);
            this.Menu = this.mnNumber;
            this.Name = "ConfigNumber";
            this.Text = "ConfigNumber";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.MenuItem miCancel;
        public System.Windows.Forms.Label lblExplanation;
        public System.Windows.Forms.Label lblLabel;
    }
}