namespace JVUtils.Forms
{
    partial class Password
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmPassword;

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
            this.mmPassword = new System.Windows.Forms.MainMenu();
            this.btOK = new System.Windows.Forms.MenuItem();
            this.btCancel = new System.Windows.Forms.MenuItem();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mmPassword
            // 
            this.mmPassword.MenuItems.Add(this.btOK);
            this.mmPassword.MenuItems.Add(this.btCancel);
            // 
            // btOK
            // 
            this.btOK.Text = "Ok";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Text = "Cancel";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lblProjectName
            // 
            this.lblProjectName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(0, 0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(240, 35);
            this.lblProjectName.Text = "Project Name";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPassword
            // 
            this.lblPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPassword.Location = new System.Drawing.Point(0, 35);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(240, 20);
            this.lblPassword.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbPassword.Location = new System.Drawing.Point(0, 55);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(240, 21);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.GotFocus += new System.EventHandler(this.tbPassword_GotFocus);
            this.tbPassword.LostFocus += new System.EventHandler(this.tbPassword_LostFocus);
            // 
            // Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblProjectName);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mmPassword;
            this.Name = "Password";
            this.Text = "Password";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Password_Load);
            this.Activated += new System.EventHandler(this.Password_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbPassword;
        public System.Windows.Forms.Label lblPassword;
        public System.Windows.Forms.MenuItem btOK;
        public System.Windows.Forms.MenuItem btCancel;
        public System.Windows.Forms.Label lblProjectName;
    }
}