namespace JVGPS.Forms
{
    partial class GoogleMaps
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
            this.wbGoogle = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miOk);
            // 
            // miOk
            // 
            this.miOk.Text = "Ok";
            this.miOk.Click += new System.EventHandler(this.miOk_Click);
            // 
            // wbGoogle
            // 
            this.wbGoogle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbGoogle.Location = new System.Drawing.Point(0, 0);
            this.wbGoogle.Name = "wbGoogle";
            this.wbGoogle.Size = new System.Drawing.Size(240, 268);
            // 
            // GoogleMaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.wbGoogle);
            this.Menu = this.mainMenu1;
            this.Name = "GoogleMaps";
            this.Text = "GoogleMaps";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.WebBrowser wbGoogle;
    }
}