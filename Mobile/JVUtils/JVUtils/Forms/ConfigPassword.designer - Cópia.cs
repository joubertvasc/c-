namespace JVUtils.Forms
{
    partial class ConfigPassword
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
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.tbCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.tbNewPassword = new System.Windows.Forms.TextBox();
            this.tbConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.lblSecretQuestion = new System.Windows.Forms.Label();
            this.tbSecretQuestion = new System.Windows.Forms.TextBox();
            this.lblSecretAnswer = new System.Windows.Forms.Label();
            this.tbSecretAnswer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mmPassword
            // 
            this.mmPassword.MenuItems.Add(this.miConfirm);
            this.mmPassword.MenuItems.Add(this.miCancel);
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
            // lblCurrentPassword
            // 
            this.lblCurrentPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentPassword.Location = new System.Drawing.Point(3, 49);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(234, 12);
            this.lblCurrentPassword.Text = "Current Password:";
            // 
            // tbCurrentPassword
            // 
            this.tbCurrentPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCurrentPassword.Location = new System.Drawing.Point(3, 64);
            this.tbCurrentPassword.Name = "tbCurrentPassword";
            this.tbCurrentPassword.PasswordChar = '*';
            this.tbCurrentPassword.Size = new System.Drawing.Size(234, 21);
            this.tbCurrentPassword.TabIndex = 1;
            this.tbCurrentPassword.GotFocus += new System.EventHandler(this.tbCurrentPassword_GotFocus);
            this.tbCurrentPassword.LostFocus += new System.EventHandler(this.tbCurrentPassword_LostFocus);
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewPassword.Location = new System.Drawing.Point(3, 88);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(234, 12);
            this.lblNewPassword.Text = "New Password:";
            // 
            // tbNewPassword
            // 
            this.tbNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNewPassword.Location = new System.Drawing.Point(3, 103);
            this.tbNewPassword.Name = "tbNewPassword";
            this.tbNewPassword.PasswordChar = '*';
            this.tbNewPassword.Size = new System.Drawing.Size(234, 21);
            this.tbNewPassword.TabIndex = 3;
            this.tbNewPassword.GotFocus += new System.EventHandler(this.tbCurrentPassword_GotFocus);
            this.tbNewPassword.LostFocus += new System.EventHandler(this.tbCurrentPassword_LostFocus);
            // 
            // tbConfirmPassword
            // 
            this.tbConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConfirmPassword.Location = new System.Drawing.Point(3, 142);
            this.tbConfirmPassword.Name = "tbConfirmPassword";
            this.tbConfirmPassword.PasswordChar = '*';
            this.tbConfirmPassword.Size = new System.Drawing.Size(234, 21);
            this.tbConfirmPassword.TabIndex = 5;
            this.tbConfirmPassword.GotFocus += new System.EventHandler(this.tbCurrentPassword_GotFocus);
            this.tbConfirmPassword.LostFocus += new System.EventHandler(this.tbCurrentPassword_LostFocus);
            // 
            // lblConfirm
            // 
            this.lblConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirm.Location = new System.Drawing.Point(3, 127);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(234, 12);
            this.lblConfirm.Text = "Confirm Password:";
            // 
            // lblExplanation
            // 
            this.lblExplanation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExplanation.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExplanation.Location = new System.Drawing.Point(0, 0);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(240, 49);
            this.lblExplanation.Text = "Define a password is a good idea to keep your configuration safer.";
            // 
            // lblSecretQuestion
            // 
            this.lblSecretQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSecretQuestion.Location = new System.Drawing.Point(3, 166);
            this.lblSecretQuestion.Name = "lblSecretQuestion";
            this.lblSecretQuestion.Size = new System.Drawing.Size(234, 12);
            this.lblSecretQuestion.Text = "Secret Question:";
            // 
            // tbSecretQuestion
            // 
            this.tbSecretQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSecretQuestion.Location = new System.Drawing.Point(3, 181);
            this.tbSecretQuestion.Name = "tbSecretQuestion";
            this.tbSecretQuestion.Size = new System.Drawing.Size(234, 21);
            this.tbSecretQuestion.TabIndex = 11;
            this.tbSecretQuestion.GotFocus += new System.EventHandler(this.tbCurrentPassword_GotFocus);
            this.tbSecretQuestion.LostFocus += new System.EventHandler(this.tbCurrentPassword_LostFocus);
            // 
            // lblSecretAnswer
            // 
            this.lblSecretAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSecretAnswer.Location = new System.Drawing.Point(3, 205);
            this.lblSecretAnswer.Name = "lblSecretAnswer";
            this.lblSecretAnswer.Size = new System.Drawing.Size(234, 12);
            this.lblSecretAnswer.Text = "Secret Answer:";
            // 
            // tbSecretAnswer
            // 
            this.tbSecretAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSecretAnswer.Location = new System.Drawing.Point(3, 220);
            this.tbSecretAnswer.Name = "tbSecretAnswer";
            this.tbSecretAnswer.Size = new System.Drawing.Size(234, 21);
            this.tbSecretAnswer.TabIndex = 14;
            this.tbSecretAnswer.GotFocus += new System.EventHandler(this.tbCurrentPassword_GotFocus);
            this.tbSecretAnswer.LostFocus += new System.EventHandler(this.tbCurrentPassword_LostFocus);
            // 
            // ConfigPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.tbSecretAnswer);
            this.Controls.Add(this.lblSecretAnswer);
            this.Controls.Add(this.tbSecretQuestion);
            this.Controls.Add(this.lblSecretQuestion);
            this.Controls.Add(this.lblExplanation);
            this.Controls.Add(this.tbConfirmPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.tbNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.tbCurrentPassword);
            this.Controls.Add(this.lblCurrentPassword);
            this.Enabled = false;
            this.Menu = this.mmPassword;
            this.Name = "ConfigPassword";
            this.Text = "Password";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.TextBox tbCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox tbNewPassword;
        private System.Windows.Forms.TextBox tbConfirmPassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.MenuItem miCancel;
        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.Label lblSecretQuestion;
        private System.Windows.Forms.TextBox tbSecretQuestion;
        private System.Windows.Forms.Label lblSecretAnswer;
        private System.Windows.Forms.TextBox tbSecretAnswer;
    }
}