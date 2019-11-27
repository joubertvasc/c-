namespace AlphaMail
{
    partial class FormMain
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
            this.menuItemLeft = new System.Windows.Forms.MenuItem();
            this.menuItemRight = new System.Windows.Forms.MenuItem();
            this.menuItemSeperatorTop = new System.Windows.Forms.MenuItem();
            this.menuItemConversationView = new System.Windows.Forms.MenuItem();
            this.menuItemSeperatorBottom = new System.Windows.Forms.MenuItem();
            this.menuItemAccounts = new System.Windows.Forms.MenuItem();
            this.menuItemFolderOptions = new System.Windows.Forms.MenuItem();
            this.menuItemDeleteSelected = new System.Windows.Forms.MenuItem();
            this.menuItemReply = new System.Windows.Forms.MenuItem();
            this.menuItemMoveMessage = new System.Windows.Forms.MenuItem();
            this.menuItemMarkAs = new System.Windows.Forms.MenuItem();
            this.menuItemTools = new System.Windows.Forms.MenuItem();
            this.menuItemSettings = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemBackup = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.txtView = new TxtView.TxtViewer();
            this.treeViewFolders = new System.Windows.Forms.TreeView();
            this.buttonShowFolders = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemLeft);
            this.mainMenu1.MenuItems.Add(this.menuItemRight);
            // 
            // menuItemLeft
            // 
            this.menuItemLeft.Text = "New";
            this.menuItemLeft.Click += new System.EventHandler(this.menuItemLeft_Click);
            // 
            // menuItemRight
            // 
            this.menuItemRight.MenuItems.Add(this.menuItemSeperatorTop);
            this.menuItemRight.MenuItems.Add(this.menuItemConversationView);
            this.menuItemRight.MenuItems.Add(this.menuItemSeperatorBottom);
            this.menuItemRight.MenuItems.Add(this.menuItemAccounts);
            this.menuItemRight.MenuItems.Add(this.menuItemFolderOptions);
            this.menuItemRight.MenuItems.Add(this.menuItemTools);
            this.menuItemRight.Text = "Menu";
            // 
            // menuItemSeperatorTop
            // 
            this.menuItemSeperatorTop.Text = "-";
            // 
            // menuItemConversationView
            // 
            this.menuItemConversationView.Text = "Conversation View";
            this.menuItemConversationView.Click += new System.EventHandler(this.menuItemConversationView_Click);
            // 
            // menuItemSeperatorBottom
            // 
            this.menuItemSeperatorBottom.Text = "-";
            // 
            // menuItemAccounts
            // 
            this.menuItemAccounts.Text = "Switch Accounts";
            // 
            // menuItemFolderOptions
            // 
            this.menuItemFolderOptions.MenuItems.Add(this.menuItemDeleteSelected);
            this.menuItemFolderOptions.MenuItems.Add(this.menuItemReply);
            this.menuItemFolderOptions.MenuItems.Add(this.menuItemMoveMessage);
            this.menuItemFolderOptions.MenuItems.Add(this.menuItemMarkAs);
            this.menuItemFolderOptions.Text = "Message Options";
            // 
            // menuItemDeleteSelected
            // 
            this.menuItemDeleteSelected.Text = "Delete";
            // 
            // menuItemReply
            // 
            this.menuItemReply.Text = "Reply";
            // 
            // menuItemMoveMessage
            // 
            this.menuItemMoveMessage.Text = "Move";
            // 
            // menuItemMarkAs
            // 
            this.menuItemMarkAs.Text = "Mark as Unread";
            // 
            // menuItemTools
            // 
            this.menuItemTools.MenuItems.Add(this.menuItemSettings);
            this.menuItemTools.MenuItems.Add(this.menuItem2);
            this.menuItemTools.MenuItems.Add(this.menuItemBackup);
            this.menuItemTools.Text = "Tools";
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Text = "Settings";
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // menuItemBackup
            // 
            this.menuItemBackup.Text = "Backup";
            this.menuItemBackup.Click += new System.EventHandler(this.menuItemBackup_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 246);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(240, 22);
            this.statusBar.Text = "AlphaMail";
            // 
            // txtView
            // 
            this.txtView.Location = new System.Drawing.Point(0, 20);
            this.txtView.Name = "txtView";
            this.txtView.Size = new System.Drawing.Size(240, 226);
            this.txtView.TabIndex = 0;
            // 
            // treeViewFolders
            // 
            this.treeViewFolders.Location = new System.Drawing.Point(0, 20);
            this.treeViewFolders.Name = "treeViewFolders";
            this.treeViewFolders.ShowPlusMinus = false;
            this.treeViewFolders.Size = new System.Drawing.Size(190, 220);
            this.treeViewFolders.TabIndex = 2;
            this.treeViewFolders.Visible = false;
            this.treeViewFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFolders_AfterSelect);
            // 
            // buttonShowFolders
            // 
            this.buttonShowFolders.Location = new System.Drawing.Point(0, 0);
            this.buttonShowFolders.Name = "buttonShowFolders";
            this.buttonShowFolders.Size = new System.Drawing.Size(72, 20);
            this.buttonShowFolders.TabIndex = 5;
            this.buttonShowFolders.Text = "Folders";
            this.buttonShowFolders.Click += new System.EventHandler(this.buttonShowFolders_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.buttonShowFolders);
            this.Controls.Add(this.treeViewFolders);
            this.Controls.Add(this.txtView);
            this.Controls.Add(this.statusBar);
            this.Menu = this.mainMenu1;
            this.Name = "FormMain";
            this.Text = "AlphaMail";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItemLeft;
        private System.Windows.Forms.MenuItem menuItemRight;
        private System.Windows.Forms.MenuItem menuItemSeperatorBottom;
        private System.Windows.Forms.MenuItem menuItemAccounts;
        private System.Windows.Forms.MenuItem menuItemTools;
        private TxtView.TxtViewer txtView;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.MenuItem menuItemSeperatorTop;
        private System.Windows.Forms.MenuItem menuItemConversationView;
        private System.Windows.Forms.MenuItem menuItemBackup;
        private System.Windows.Forms.MenuItem menuItemSettings;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemFolderOptions;
        private System.Windows.Forms.MenuItem menuItemDeleteSelected;
        private System.Windows.Forms.MenuItem menuItemReply;
        private System.Windows.Forms.MenuItem menuItemMoveMessage;
        private System.Windows.Forms.MenuItem menuItemMarkAs;
        private System.Windows.Forms.TreeView treeViewFolders;
        private System.Windows.Forms.Button buttonShowFolders;
    }
}

