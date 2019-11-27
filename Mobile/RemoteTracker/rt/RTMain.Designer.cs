namespace rt
{
    partial class RTMain
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
            this.tmELT = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // tmELT
            // 
            this.tmELT.Interval = 1000;
            this.tmELT.Tick += new System.EventHandler(this.tmELT_Tick);
            // 
            // RTMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Menu = this.mainMenu1;
            this.Name = "RTMain";
            this.Load += new System.EventHandler(this.RTMain_Load);
            this.Activated += new System.EventHandler(this.RTMain_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer tmELT;
    }
}

