﻿namespace JVTrip
{
    partial class PictureForm
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
            this.components = new System.ComponentModel.Container();
            this.dgRows = new System.Windows.Forms.DataGrid();
            this.cmGrid = new System.Windows.Forms.ContextMenu();
            this.cmInsert = new System.Windows.Forms.MenuItem();
            this.cmEdit = new System.Windows.Forms.MenuItem();
            this.cmDelete = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.cmViewMap = new System.Windows.Forms.MenuItem();
            this.ipForm = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.mmEdit = new System.Windows.Forms.MainMenu();
            this.miConfirm = new System.Windows.Forms.MenuItem();
            this.miCancel = new System.Windows.Forms.MenuItem();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.btPicture = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPicture = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mmMain = new System.Windows.Forms.MainMenu();
            this.miOptions = new System.Windows.Forms.MenuItem();
            this.miInsert = new System.Windows.Forms.MenuItem();
            this.miEdit = new System.Windows.Forms.MenuItem();
            this.miDelete = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miViewMap = new System.Windows.Forms.MenuItem();
            this.miOk = new System.Windows.Forms.MenuItem();
            this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgRows
            // 
            this.dgRows.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgRows.ContextMenu = this.cmGrid;
            this.dgRows.Location = new System.Drawing.Point(3, 3);
            this.dgRows.Name = "dgRows";
            this.dgRows.Size = new System.Drawing.Size(111, 155);
            this.dgRows.TabIndex = 6;
            // 
            // cmGrid
            // 
            this.cmGrid.MenuItems.Add(this.cmInsert);
            this.cmGrid.MenuItems.Add(this.cmEdit);
            this.cmGrid.MenuItems.Add(this.cmDelete);
            this.cmGrid.MenuItems.Add(this.menuItem2);
            this.cmGrid.MenuItems.Add(this.cmViewMap);
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
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // cmViewMap
            // 
            this.cmViewMap.Text = "View in Map";
            this.cmViewMap.Click += new System.EventHandler(this.miViewMap_Click);
            // 
            // ipForm
            // 
            this.ipForm.EnabledChanged += new System.EventHandler(this.ipForm_EnabledChanged);
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
            // pnlEdit
            // 
            this.pnlEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlEdit.Controls.Add(this.btPicture);
            this.pnlEdit.Controls.Add(this.tbPath);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Controls.Add(this.tbPicture);
            this.pnlEdit.Controls.Add(this.label4);
            this.pnlEdit.Controls.Add(this.lblPosition);
            this.pnlEdit.Controls.Add(this.label1);
            this.pnlEdit.Location = new System.Drawing.Point(120, 3);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(178, 95);
            // 
            // btPicture
            // 
            this.btPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btPicture.Location = new System.Drawing.Point(149, 144);
            this.btPicture.Name = "btPicture";
            this.btPicture.Size = new System.Drawing.Size(26, 20);
            this.btPicture.TabIndex = 17;
            this.btPicture.Text = "...";
            this.btPicture.Click += new System.EventHandler(this.btPicture_Click);
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.Location = new System.Drawing.Point(3, 144);
            this.tbPath.MaxLength = 12;
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(140, 21);
            this.tbPath.TabIndex = 12;
            this.tbPath.GotFocus += new System.EventHandler(this.tbCost_GotFocus);
            this.tbPath.LostFocus += new System.EventHandler(this.tbCost_LostFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Picture:";
            // 
            // tbPicture
            // 
            this.tbPicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPicture.Location = new System.Drawing.Point(3, 79);
            this.tbPicture.MaxLength = 250;
            this.tbPicture.Multiline = true;
            this.tbPicture.Name = "tbPicture";
            this.tbPicture.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPicture.Size = new System.Drawing.Size(157, 44);
            this.tbPicture.TabIndex = 7;
            this.tbPicture.GotFocus += new System.EventHandler(this.tbCost_GotFocus);
            this.tbPicture.LostFocus += new System.EventHandler(this.tbCost_LostFocus);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(3, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.Text = "Description:";
            // 
            // lblPosition
            // 
            this.lblPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPosition.Location = new System.Drawing.Point(3, 30);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(172, 20);
            this.lblPosition.Text = "From:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.Text = "Current position:";
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
            this.miOptions.MenuItems.Add(this.menuItem1);
            this.miOptions.MenuItems.Add(this.miViewMap);
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
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miViewMap
            // 
            this.miViewMap.Text = "View in Map";
            this.miViewMap.Click += new System.EventHandler(this.miViewMap_Click);
            // 
            // miOk
            // 
            this.miOk.Text = "Ok";
            this.miOk.Click += new System.EventHandler(this.miOk_Click);
            // 
            // ofdPicture
            // 
            this.ofdPicture.Filter = "(*.jpg)|*.jpg";
            // 
            // PictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.dgRows);
            this.Controls.Add(this.pnlEdit);
            this.Menu = this.mmMain;
            this.Name = "PictureForm";
            this.Text = "PictureForm";
            this.Load += new System.EventHandler(this.PictureForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PictureForm_Closing);
            this.pnlEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgRows;
        private System.Windows.Forms.ContextMenu cmGrid;
        private System.Windows.Forms.MenuItem cmInsert;
        private System.Windows.Forms.MenuItem cmEdit;
        private System.Windows.Forms.MenuItem cmDelete;
        private Microsoft.WindowsCE.Forms.InputPanel ipForm;
        private System.Windows.Forms.MainMenu mmEdit;
        private System.Windows.Forms.MenuItem miConfirm;
        private System.Windows.Forms.MenuItem miCancel;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPicture;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MainMenu mmMain;
        private System.Windows.Forms.MenuItem miOptions;
        private System.Windows.Forms.MenuItem miInsert;
        private System.Windows.Forms.MenuItem miEdit;
        private System.Windows.Forms.MenuItem miDelete;
        private System.Windows.Forms.MenuItem miOk;
        private System.Windows.Forms.Button btPicture;
        private System.Windows.Forms.OpenFileDialog ofdPicture;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miViewMap;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem cmViewMap;
    }
}