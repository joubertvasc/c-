namespace CommonDLL.Forms
{
    partial class RTCommandLogViwer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmViewer;

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
            this.mmViewer = new System.Windows.Forms.MainMenu();
            this.miOk = new System.Windows.Forms.MenuItem();
            this.tbCommands = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mmViewer
            // 
            this.mmViewer.MenuItems.Add(this.miOk);
            // 
            // miOk
            // 
            this.miOk.Text = "Ok";
            this.miOk.Click += new System.EventHandler(this.miOk_Click);
            // 
            // tbCommands
            // 
            this.tbCommands.AcceptsReturn = true;
            this.tbCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCommands.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular);
            this.tbCommands.Location = new System.Drawing.Point(0, 0);
            this.tbCommands.Multiline = true;
            this.tbCommands.Name = "tbCommands";
            this.tbCommands.ReadOnly = true;
            this.tbCommands.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCommands.Size = new System.Drawing.Size(240, 268);
            this.tbCommands.TabIndex = 0;
            // 
            // RTCommandLogViwer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tbCommands);
            this.Menu = this.mmViewer;
            this.Name = "RTCommandLogViwer";
            this.Text = "Command Log";
            this.Load += new System.EventHandler(this.RTCommandLogViwer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.TextBox tbCommands;
    }
}