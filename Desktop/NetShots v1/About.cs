/*
 * NETMeter - by Alessandro Fragnani
 * About.cs
 * Created: 02 april 2005
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace NetShots
{
	/// <summary>
	/// Summary description for About.
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label labelAppName;
		private System.Windows.Forms.Label labelAppVersion;
		private System.Windows.Forms.Label labelAppCopyright;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabelEmail;
		private System.Windows.Forms.Button buttonClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public About()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			labelAppName.Text = Assembly.GetExecutingAssembly().GetName().Name.ToString();
			labelAppVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(About));
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelAppName = new System.Windows.Forms.Label();
			this.labelAppVersion = new System.Windows.Forms.Label();
			this.labelAppCopyright = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabelEmail = new System.Windows.Forms.LinkLabel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.labelAppVersion,
																				 this.labelAppName,
																				 this.pictureBox1});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(392, 64);
			this.panel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 56);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// labelAppName
			// 
			this.labelAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelAppName.Location = new System.Drawing.Point(72, 16);
			this.labelAppName.Name = "labelAppName";
			this.labelAppName.Size = new System.Drawing.Size(100, 16);
			this.labelAppName.TabIndex = 1;
			this.labelAppName.Text = "NETShots";
			// 
			// labelAppVersion
			// 
			this.labelAppVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelAppVersion.Location = new System.Drawing.Point(72, 32);
			this.labelAppVersion.Name = "labelAppVersion";
			this.labelAppVersion.Size = new System.Drawing.Size(100, 16);
			this.labelAppVersion.TabIndex = 2;
			this.labelAppVersion.Text = "Version 1.0";
			// 
			// labelAppCopyright
			// 
			this.labelAppCopyright.Location = new System.Drawing.Point(72, 80);
			this.labelAppCopyright.Name = "labelAppCopyright";
			this.labelAppCopyright.Size = new System.Drawing.Size(184, 23);
			this.labelAppCopyright.TabIndex = 1;
			this.labelAppCopyright.Text = "Copyright (c) Alessandro Fragnani";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Navy;
			this.label1.Location = new System.Drawing.Point(72, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "This is a free application. Tell everyone about it.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(72, 144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(304, 40);
			this.label2.TabIndex = 3;
			this.label2.Text = "This application is provided \"as is\". Use it at your own risk. It´s freely availa" +
				"ble to anyone that wants an easy to use wallpaper and screensaver picture viewer" +
				"";
			// 
			// linkLabelEmail
			// 
			this.linkLabelEmail.Location = new System.Drawing.Point(8, 208);
			this.linkLabelEmail.Name = "linkLabelEmail";
			this.linkLabelEmail.Size = new System.Drawing.Size(136, 23);
			this.linkLabelEmail.TabIndex = 4;
			this.linkLabelEmail.TabStop = true;
			this.linkLabelEmail.Text = "alefragnani@gmail.com";
			this.linkLabelEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelEmail_LinkClicked);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonClose.Location = new System.Drawing.Point(312, 200);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "&Close";
			// 
			// About
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(392, 230);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonClose,
																		  this.linkLabelEmail,
																		  this.label2,
																		  this.label1,
																		  this.labelAppCopyright,
																		  this.panel1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabelEmail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:" + linkLabelEmail.Text + "?subject=NETShots");
		}
	}
}
