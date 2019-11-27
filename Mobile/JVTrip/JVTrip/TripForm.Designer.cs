namespace JVTrip
{
    partial class TripForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmMain;

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
            this.components = new System.ComponentModel.Container();
            this.mmMain = new System.Windows.Forms.MainMenu();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miInsert = new System.Windows.Forms.MenuItem();
            this.miEdit = new System.Windows.Forms.MenuItem();
            this.miDelete = new System.Windows.Forms.MenuItem();
            this.miOk = new System.Windows.Forms.MenuItem();
            this.dgRows = new System.Windows.Forms.DataGrid();
            this.cmGrid = new System.Windows.Forms.ContextMenu();
            this.cmInsert = new System.Windows.Forms.MenuItem();
            this.cmEdit = new System.Windows.Forms.MenuItem();
            this.cmDelete = new System.Windows.Forms.MenuItem();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.tbNotes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mmEdit = new System.Windows.Forms.MainMenu();
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.ipForm = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmMain
            // 
            this.mmMain.MenuItems.Add(this.miOptions);
            this.mmMain.MenuItems.Add(this.miOk);
            // 
            // miOptions
            // 
            this.miOptions.MenuItems.Add(this.miInsert);
            this.miOptions.MenuItems.Add(this.miEdit);
            this.miOptions.MenuItems.Add(this.miDelete);
            this.miOptions.Text = "Options";
            // 
            // miInsert
            // 
            this.miInsert.Text = "Insert";
            this.miInsert.Click += new System.EventHandler(this.miInsert_Click);
            // 
            // miEdit
            // 
            this.miEdit.Text = "Edit";
            this.miEdit.Click += new System.EventHandler(this.miEdit_Click);
            // 
            // miDelete
            // 
            this.miDelete.Text = "Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miOk
            // 
            this.miOk.Text = "Ok";
            this.miOk.Click += new System.EventHandler(this.miOk_Click);
            // 
            // dgRows
            // 
            this.dgRows.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgRows.ContextMenu = this.cmGrid;
            this.dgRows.Location = new System.Drawing.Point(3, 3);
            this.dgRows.Name = "dgRows";
            this.dgRows.Size = new System.Drawing.Size(111, 155);
            this.dgRows.TabIndex = 0;
            // 
            // cmGrid
            // 
            this.cmGrid.MenuItems.Add(this.cmInsert);
            this.cmGrid.MenuItems.Add(this.cmEdit);
            this.cmGrid.MenuItems.Add(this.cmDelete);
            // 
            // cmInsert
            // 
            this.cmInsert.Text = "Insert";
            this.cmInsert.Click += new System.EventHandler(this.miInsert_Click);
            // 
            // cmEdit
            // 
            this.cmEdit.Text = "Edit";
            this.cmEdit.Click += new System.EventHandler(this.miEdit_Click);
            // 
            // cmDelete
            // 
            this.cmDelete.Text = "Delete";
            this.cmDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // pnlEdit
            // 
            this.pnlEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlEdit.AutoScroll = true;
            this.pnlEdit.Controls.Add(this.tbNotes);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.tbTo);
            this.pnlEdit.Controls.Add(this.label3);
            this.pnlEdit.Controls.Add(this.tbFrom);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Controls.Add(this.tbName);
            this.pnlEdit.Controls.Add(this.label1);
            this.pnlEdit.Location = new System.Drawing.Point(120, 3);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(210, 212);
            // 
            // tbNotes
            // 
            this.tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNotes.Location = new System.Drawing.Point(3, 149);
            this.tbNotes.Multiline = true;
            this.tbNotes.Name = "tbNotes";
            this.tbNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbNotes.Size = new System.Drawing.Size(184, 41);
            this.tbNotes.TabIndex = 7;
            this.tbNotes.GotFocus += new System.EventHandler(this.tbName_GotFocus);
            this.tbNotes.LostFocus += new System.EventHandler(this.tbName_LostFocus);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(3, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Notes:";
            // 
            // tbTo
            // 
            this.tbTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTo.Location = new System.Drawing.Point(3, 110);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(204, 21);
            this.tbTo.TabIndex = 5;
            this.tbTo.GotFocus += new System.EventHandler(this.tbName_GotFocus);
            this.tbTo.LostFocus += new System.EventHandler(this.tbName_LostFocus);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(3, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 20);
            this.label3.Text = "To:";
            // 
            // tbFrom
            // 
            this.tbFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFrom.Location = new System.Drawing.Point(3, 70);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(204, 21);
            this.tbFrom.TabIndex = 3;
            this.tbFrom.GotFocus += new System.EventHandler(this.tbName_GotFocus);
            this.tbFrom.LostFocus += new System.EventHandler(this.tbName_LostFocus);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 20);
            this.label2.Text = "From:";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(3, 28);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(204, 21);
            this.tbName.TabIndex = 1;
            this.tbName.GotFocus += new System.EventHandler(this.tbName_GotFocus);
            this.tbName.LostFocus += new System.EventHandler(this.tbName_LostFocus);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 20);
            this.label1.Text = "Trip name:";
            // 
            // mmEdit
            // 
            this.mmEdit.MenuItems.Add(this.miConfirm);
            this.mmEdit.MenuItems.Add(this.miCancel);
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
            // ipForm
            // 
            this.ipForm.EnabledChanged += new System.EventHandler(this.ipForm_EnabledChanged);
            // 
            // TripForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlEdit);
            this.Controls.Add(this.dgRows);
            this.Menu = this.mmMain;
            this.Name = "TripForm";
            this.Text = "Trips";
            this.Load += new System.EventHandler(this.TripForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.TripForm_Closing);
            this.pnlEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miInsert;
        private System.Windows.Forms.MenuItem miEdit;
        private System.Windows.Forms.MenuItem miDelete;
        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.DataGrid dgRows;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.MainMenu mmEdit;
        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.MenuItem miCancel;
        private System.Windows.Forms.TextBox tbTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNotes;
        private Microsoft.WindowsCE.Forms.InputPanel ipForm;
        private System.Windows.Forms.ContextMenu cmGrid;
        private System.Windows.Forms.MenuItem cmInsert;
        private System.Windows.Forms.MenuItem cmEdit;
        private System.Windows.Forms.MenuItem cmDelete;
    }
}