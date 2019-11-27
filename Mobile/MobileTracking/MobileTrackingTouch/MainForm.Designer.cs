namespace MobileTrackingTouch
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.titleBar1 = new mirabyte.Mobile.TouchControls.TitleBar();
            this.buttonBar2 = new mirabyte.Mobile.TouchControls.ButtonBar();
            this.touchListBox1 = new mirabyte.Mobile.TouchControls.TouchListBox();
            this.SuspendLayout();
            // 
            // titleBar1
            // 
            this.titleBar1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.titleBar1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("titleBar1.BackgroundImage")));
            this.titleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.titleBar1.ForeColor = System.Drawing.Color.White;
            this.titleBar1.HAlignment = System.Drawing.StringAlignment.Center;
            this.titleBar1.LButtonImage = "";
            this.titleBar1.LButtonImageDown = "";
            this.titleBar1.Location = new System.Drawing.Point(0, 0);
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.RButtonImage = "";
            this.titleBar1.RButtonImageDown = "";
            this.titleBar1.ShowLButton = false;
            this.titleBar1.ShowRButton = false;
            this.titleBar1.Size = new System.Drawing.Size(240, 52);
            this.titleBar1.SpaceBetweenLButtonAndText = 0;
            this.titleBar1.SpaceBetweenRButtonAndText = 0;
            this.titleBar1.SpaceLeft = 0;
            this.titleBar1.SpaceRight = 0;
            this.titleBar1.TabIndex = 0;
            this.titleBar1.Text = "Mobile Tracking";
            this.titleBar1.TitleText = "Mobile Tracking";
            this.titleBar1.VAlignment = System.Drawing.StringAlignment.Center;
            // 
            // buttonBar2
            // 
            this.buttonBar2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBar2.BackgroundImage")));
            this.buttonBar2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonBar2.HAlignment = mirabyte.Mobile.TouchControls.ButtonBar.ButtonAlignment.Right;
            this.buttonBar2.LButtonImage = "";
            this.buttonBar2.LButtonImageDown = "";
            this.buttonBar2.Location = new System.Drawing.Point(0, 228);
            this.buttonBar2.Name = "buttonBar2";
            this.buttonBar2.RButtonImage = "";
            this.buttonBar2.RButtonImageDown = "";
            this.buttonBar2.ShowLButton = true;
            this.buttonBar2.ShowRButton = true;
            this.buttonBar2.Size = new System.Drawing.Size(240, 40);
            this.buttonBar2.SpaceLeft = 0;
            this.buttonBar2.SpaceRight = 0;
            this.buttonBar2.TabIndex = 4;
            this.buttonBar2.Text = "buttonBar2";
            this.buttonBar2.Visible = false;
            // 
            // touchListBox1
            // 
            this.touchListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.touchListBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.touchListBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.touchListBox1.HAlignment = System.Drawing.StringAlignment.Near;
            this.touchListBox1.ItemBackgroundColor = System.Drawing.Color.White;
            this.touchListBox1.ItemBackgroundColor2 = System.Drawing.Color.Gainsboro;
            this.touchListBox1.ItemBackgroundColorSelected = System.Drawing.Color.Firebrick;
            this.touchListBox1.ItemBackgroundImage = null;
            this.touchListBox1.ItemBackgroundImage2 = null;
            this.touchListBox1.ItemBackgroundImageSelected = null;
            this.touchListBox1.ItemHeight = 50;
            this.touchListBox1.Location = new System.Drawing.Point(0, 52);
            this.touchListBox1.MoveThreshold = 15;
            this.touchListBox1.Name = "touchListBox1";
            this.touchListBox1.SelectedIndex = -1;
            this.touchListBox1.ShowCategoryIcons = true;
            this.touchListBox1.ShowIcons = true;
            this.touchListBox1.ShowSubText = true;
            this.touchListBox1.Size = new System.Drawing.Size(240, 176);
            this.touchListBox1.SpaceBetweenCategoryIconAndText = 0;
            this.touchListBox1.SpaceBetweenIconAndText = 0;
            this.touchListBox1.SpaceLeft = 0;
            this.touchListBox1.SpaceRight = 0;
            this.touchListBox1.SubTextColor = System.Drawing.Color.Gray;
            this.touchListBox1.SubTextColorSelected = System.Drawing.Color.White;
            this.touchListBox1.SubTextFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.touchListBox1.TabIndex = 5;
            this.touchListBox1.Text = "touchListBox1";
            this.touchListBox1.TextColor = System.Drawing.Color.Black;
            this.touchListBox1.TextColorSelected = System.Drawing.Color.White;
            this.touchListBox1.VAlignment = System.Drawing.StringAlignment.Center;
            this.touchListBox1.Click += new System.EventHandler(this.touchListBox1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.touchListBox1);
            this.Controls.Add(this.buttonBar2);
            this.Controls.Add(this.titleBar1);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "Mobile Tracking";
            this.ResumeLayout(false);

        }

        #endregion

        private mirabyte.Mobile.TouchControls.TitleBar titleBar1;
        private mirabyte.Mobile.TouchControls.ButtonBar buttonBar2;
        private mirabyte.Mobile.TouchControls.TouchListBox touchListBox1;
    }
}

