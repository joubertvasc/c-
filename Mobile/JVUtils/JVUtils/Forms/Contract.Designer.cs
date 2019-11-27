namespace JVUtils.Forms
{
    partial class Contract
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmContract;

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
            this.mmContract = new System.Windows.Forms.MainMenu();
            this.miAccept = new System.Windows.Forms.MenuItem();
            this.miDontAccept = new System.Windows.Forms.MenuItem();
            this.wbContract = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // mmContract
            // 
            this.mmContract.MenuItems.Add(this.miAccept);
            this.mmContract.MenuItems.Add(this.miDontAccept);
            // 
            // miAccept
            // 
            this.miAccept.Text = "Accept";
            this.miAccept.Click += new System.EventHandler(this.miAccept_Click);
            // 
            // miDontAccept
            // 
            this.miDontAccept.Text = "Don\'t Accept";
            this.miDontAccept.Click += new System.EventHandler(this.miDontAccept_Click);
            // 
            // wbContract
            // 
            this.wbContract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbContract.Location = new System.Drawing.Point(0, 0);
            this.wbContract.Name = "wbContract";
            this.wbContract.Size = new System.Drawing.Size(240, 268);
            // 
            // Contract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.wbContract);
            this.Menu = this.mmContract;
            this.Name = "Contract";
            this.Text = "Term of Service";
            this.Activated += new System.EventHandler(this.Contract_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Contract_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miAccept;
        private System.Windows.Forms.MenuItem miDontAccept;
        private System.Windows.Forms.WebBrowser wbContract;
    }
}