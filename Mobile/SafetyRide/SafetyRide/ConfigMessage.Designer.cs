namespace SafetyRide
{
    partial class ConfigMessage
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
            this.pnlForm = new System.Windows.Forms.Panel();
            this.miOk = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.lblSMSTo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.tbInterval = new System.Windows.Forms.TextBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.lblHelp = new System.Windows.Forms.Label();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.miOk);
            this.mainMenu1.MenuItems.Add(this.miCancel);
            // 
            // pnlForm
            // 
            this.pnlForm.Controls.Add(this.lblHelp);
            this.pnlForm.Controls.Add(this.tbMessage);
            this.pnlForm.Controls.Add(this.tbInterval);
            this.pnlForm.Controls.Add(this.tbPhone);
            this.pnlForm.Controls.Add(this.label2);
            this.pnlForm.Controls.Add(this.label1);
            this.pnlForm.Controls.Add(this.lblSMSTo);
            this.pnlForm.Location = new System.Drawing.Point(3, 3);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(234, 262);
            // 
            // miOk
            // 
            this.miOk.Text = "Ok";
            this.miOk.Click += new System.EventHandler(this.miOk_Click);
            // 
            // miCancel
            // 
            this.miCancel.Text = "Cancel";
            this.miCancel.Click += new System.EventHandler(this.miCancel_Click);
            // 
            // lblSMSTo
            // 
            this.lblSMSTo.Location = new System.Drawing.Point(3, 10);
            this.lblSMSTo.Name = "lblSMSTo";
            this.lblSMSTo.Size = new System.Drawing.Size(100, 20);
            this.lblSMSTo.Text = "Send SMS to:";
            this.lblSMSTo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Interval (min.):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Message:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbPhone
            // 
            this.tbPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPhone.Location = new System.Drawing.Point(109, 9);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(122, 21);
            this.tbPhone.TabIndex = 3;
            // 
            // tbInterval
            // 
            this.tbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInterval.Location = new System.Drawing.Point(109, 32);
            this.tbInterval.MaxLength = 3;
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(122, 21);
            this.tbInterval.TabIndex = 4;
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.Location = new System.Drawing.Point(3, 75);
            this.tbMessage.MaxLength = 100;
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessage.Size = new System.Drawing.Size(228, 64);
            this.tbMessage.TabIndex = 5;
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHelp.Location = new System.Drawing.Point(3, 146);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(228, 36);
            this.lblHelp.Text = "The current position will be added at the end of your message.";
            // 
            // ConfigMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlForm);
            this.Menu = this.mainMenu1;
            this.Name = "ConfigMessage";
            this.Text = "Message Config.";
            this.pnlForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.MenuItem miCancel;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSMSTo;
        private System.Windows.Forms.TextBox tbInterval;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.TextBox tbMessage;
    }
}