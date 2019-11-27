namespace JVUtils.Forms
{
    partial class NewOpenDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmOpenDialog;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewOpenDialog));
            this.mmOpenDialog = new System.Windows.Forms.MainMenu();
            this.miOk = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.ilFolders = new System.Windows.Forms.ImageList();
            this.dsFiles = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dsFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // mmOpenDialog
            // 
            this.mmOpenDialog.MenuItems.Add(this.miOk);
            this.mmOpenDialog.MenuItems.Add(this.miCancel);
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
            // tvFolders
            // 
            this.tvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFolders.ImageIndex = 0;
            this.tvFolders.ImageList = this.ilFolders;
            this.tvFolders.Location = new System.Drawing.Point(0, 0);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageIndex = 1;
            this.tvFolders.Size = new System.Drawing.Size(240, 268);
            this.tvFolders.TabIndex = 0;
            this.tvFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFolders_AfterSelect);
            this.ilFolders.Images.Clear();
            this.ilFolders.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.ilFolders.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.ilFolders.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            // 
            // dsFiles
            // 
            this.dsFiles.DataSetName = "NewDataSet";
            this.dsFiles.Namespace = "";
            this.dsFiles.Prefix = "";
            this.dsFiles.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // NewOpenDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tvFolders);
            this.Menu = this.mmOpenDialog;
            this.MinimizeBox = false;
            this.Name = "NewOpenDialog";
            this.Text = "NewOpenDialog";
            this.Activated += new System.EventHandler(this.NewOpenDialog_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.NewOpenDialog_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.dsFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvFolders;
        private System.Windows.Forms.ImageList ilFolders;
        private System.Data.DataSet dsFiles;
        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.MenuItem miCancel;
    }
}