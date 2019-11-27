namespace RTRemote
{
    partial class Remote
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tbParameters = new System.Windows.Forms.TextBox();
            this.lblParameters = new System.Windows.Forms.Label();
            this.tbHelp = new System.Windows.Forms.TextBox();
            this.tbPhoneToCall = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbSender = new System.Windows.Forms.TextBox();
            this.lblSender = new System.Windows.Forms.Label();
            this.cbCommand = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.mmRemote = new System.Windows.Forms.MainMenu();
            this.miActions = new System.Windows.Forms.MenuItem();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miRT = new System.Windows.Forms.MenuItem();
            this.miRT2 = new System.Windows.Forms.MenuItem();
            this.miSetPassword = new System.Windows.Forms.MenuItem();
            this.miSaveFields = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miExecute = new System.Windows.Forms.MenuItem();
            this.miTest = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.tbParameters);
            this.pnlMain.Controls.Add(this.lblParameters);
            this.pnlMain.Controls.Add(this.tbHelp);
            this.pnlMain.Controls.Add(this.tbPhoneToCall);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.tbPassword);
            this.pnlMain.Controls.Add(this.lblPassword);
            this.pnlMain.Controls.Add(this.tbSender);
            this.pnlMain.Controls.Add(this.lblSender);
            this.pnlMain.Controls.Add(this.cbCommand);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblProjectName);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(240, 268);
            // 
            // tbParameters
            // 
            this.tbParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbParameters.Location = new System.Drawing.Point(90, 75);
            this.tbParameters.Name = "tbParameters";
            this.tbParameters.Size = new System.Drawing.Size(147, 21);
            this.tbParameters.TabIndex = 3;
            // 
            // lblParameters
            // 
            this.lblParameters.Location = new System.Drawing.Point(3, 76);
            this.lblParameters.Name = "lblParameters";
            this.lblParameters.Size = new System.Drawing.Size(85, 20);
            this.lblParameters.Text = "Parameters:";
            this.lblParameters.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbHelp
            // 
            this.tbHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHelp.Location = new System.Drawing.Point(3, 146);
            this.tbHelp.Multiline = true;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.ReadOnly = true;
            this.tbHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbHelp.Size = new System.Drawing.Size(234, 119);
            this.tbHelp.TabIndex = 6;
            // 
            // tbPhoneToCall
            // 
            this.tbPhoneToCall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPhoneToCall.Location = new System.Drawing.Point(90, 28);
            this.tbPhoneToCall.Name = "tbPhoneToCall";
            this.tbPhoneToCall.Size = new System.Drawing.Size(147, 21);
            this.tbPhoneToCall.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.Text = "Phone to call:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(90, 124);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(147, 21);
            this.tbPassword.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(3, 124);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(85, 19);
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbSender
            // 
            this.tbSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSender.Location = new System.Drawing.Point(90, 99);
            this.tbSender.Name = "tbSender";
            this.tbSender.Size = new System.Drawing.Size(147, 21);
            this.tbSender.TabIndex = 4;
            // 
            // lblSender
            // 
            this.lblSender.Location = new System.Drawing.Point(3, 99);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(85, 19);
            this.lblSender.Text = "Sender phone:";
            this.lblSender.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbCommand
            // 
            this.cbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCommand.Items.Add("help");
            this.cbCommand.Items.Add("ehelp");
            this.cbCommand.Items.Add("alarm");
            this.cbCommand.Items.Add("cb");
            this.cbCommand.Items.Add("dkz");
            this.cbCommand.Items.Add("elt");
            this.cbCommand.Items.Add("findme");
            this.cbCommand.Items.Add("ganfl");
            this.cbCommand.Items.Add("eganfl");
            this.cbCommand.Items.Add("eganf");
            this.cbCommand.Items.Add("rst");
            this.cbCommand.Items.Add("go");
            this.cbCommand.Items.Add("ego");
            this.cbCommand.Items.Add("pb");
            this.cbCommand.Items.Add("wip");
            this.cbCommand.Items.Add("ewip");
            this.cbCommand.Items.Add("epb");
            this.cbCommand.Items.Add("gp");
            this.cbCommand.Items.Add("egp");
            this.cbCommand.Items.Add("gi");
            this.cbCommand.Items.Add("egi");
            this.cbCommand.Items.Add("msg");
            this.cbCommand.Location = new System.Drawing.Point(90, 51);
            this.cbCommand.Name = "cbCommand";
            this.cbCommand.Size = new System.Drawing.Size(147, 22);
            this.cbCommand.TabIndex = 2;
            this.cbCommand.SelectedIndexChanged += new System.EventHandler(this.cbCommand_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.Text = "Command:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProjectName.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.lblProjectName.Location = new System.Drawing.Point(0, 0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(240, 25);
            this.lblProjectName.Text = "RTRemote";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mmRemote
            // 
            this.mmRemote.MenuItems.Add(this.miActions);
            this.mmRemote.MenuItems.Add(this.miExit);
            // 
            // miActions
            // 
            this.miActions.MenuItems.Add(this.miOptions);
            this.miActions.MenuItems.Add(this.menuItem2);
            this.miActions.MenuItems.Add(this.miExecute);
            this.miActions.MenuItems.Add(this.miTest);
            this.miActions.MenuItems.Add(this.menuItem1);
            this.miActions.MenuItems.Add(this.miAbout);
            this.miActions.Text = "Menu";
            // 
            // miOptions
            // 
            this.miOptions.MenuItems.Add(this.miRT);
            this.miOptions.MenuItems.Add(this.miRT2);
            this.miOptions.MenuItems.Add(this.menuItem3);
            this.miOptions.MenuItems.Add(this.miSetPassword);
            this.miOptions.MenuItems.Add(this.menuItem4);
            this.miOptions.MenuItems.Add(this.miSaveFields);
            this.miOptions.Text = "Options";
            // 
            // miRT
            // 
            this.miRT.Checked = true;
            this.miRT.Text = "Use RT# header (all users)";
            this.miRT.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // miRT2
            // 
            this.miRT2.Text = "Use RT2# header (HD2 users)";
            this.miRT2.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // miSetPassword
            // 
            this.miSetPassword.Text = "Set Password";
            this.miSetPassword.Click += new System.EventHandler(this.miSetPassword_Click);
            // 
            // miSaveFields
            // 
            this.miSaveFields.Text = "Save Fields Before Exit";
            this.miSaveFields.Click += new System.EventHandler(this.miSaveFields_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // miExecute
            // 
            this.miExecute.Text = "Execute";
            this.miExecute.Click += new System.EventHandler(this.miExecute_Click);
            // 
            // miTest
            // 
            this.miTest.Text = "Make a test";
            this.miTest.Click += new System.EventHandler(this.miExecute_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miAbout
            // 
            this.miAbout.Text = "About";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miExit
            // 
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // Remote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlMain);
            this.Menu = this.mmRemote;
            this.Name = "Remote";
            this.Text = "Remote";
            this.Load += new System.EventHandler(this.Remote_Load);
            this.Activated += new System.EventHandler(this.Remote_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Remote_Closing);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.MainMenu mmRemote;
        private System.Windows.Forms.MenuItem miActions;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.TextBox tbSender;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.ComboBox cbCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.MenuItem miExecute;
        private System.Windows.Forms.MenuItem miTest;
        private System.Windows.Forms.TextBox tbPhoneToCall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHelp;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.TextBox tbParameters;
        private System.Windows.Forms.Label lblParameters;
        private System.Windows.Forms.MenuItem miSetPassword;
        private System.Windows.Forms.MenuItem miSaveFields;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miRT;
        private System.Windows.Forms.MenuItem miRT2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;

    }
}