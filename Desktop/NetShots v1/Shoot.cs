/*
 * NETShots - by Alessandro Fragnani
 * Shoot.cs
 * Created: 28 march 2005
 * 
 * Displays an image, enlarged to fit the widest dimension (H x W). Its the "Screensaver window"
 * 
 * Originaly has:
 *  - A Timer to decides the time interval to shoot another picture
 *  - The image was shown without any kind of ratio. Just the original size
 * 
 * Changes:
 *  - Changed: Now uses PictureViewer.Viewer (also changed by me) to resizes the image mantaining 
 *    the Aspect Ratio
 * 
 * Thanks to:
 *  - Rakesh Rajan for his article "How to develop a screen saver in C#"
 *    (http://www.codeproject.com/csharp/scrframework.asp)
 *  - Gil Klod for his article "Scrollable and RatioStretch PictureBox"
 *    (http://www.codeproject.com/cs/miscctrl/ratioStretchpictureBox1.asp)
 * 
 */ 

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PictureViewer;

namespace NetShots
{
	/// <summary>
	/// Summary description for Shoot.
	/// </summary>
	public class Shoot : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		private Point MouseXY;

		private System.Windows.Forms.Timer timer1;

		private SettingsManager settingsManager = null;

		private FileManager fileManager = null;

		private Image image = null;

		private Random random = null;


		PictureViewer.Viewer viewer1;

		/// <summary>
		/// Created receiving the SettingsManager and the FileManager
		/// </summary>
		/// <param name="settingsManager"></param>
		/// <param name="fileManager"></param>
		public Shoot(SettingsManager settingsManager, FileManager fileManager) 
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			this.SuspendLayout();
			
			this.viewer1 = new PictureViewer.Viewer();
			// 
			// viewer1
			// 
			this.viewer1.AutoScroll = true;
			this.viewer1.BackColor = System.Drawing.Color.Black;
			this.viewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewer1.Image = null;
			this.viewer1.ImageSizeMode = PictureViewer.SizeMode.Scrollable;
			this.viewer1.Name = "viewer1";
			this.viewer1.Size = new System.Drawing.Size(744, 453);
			this.viewer1.TabIndex = 0;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.viewer1});
			this.viewer1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
			this.viewer1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);

			this.ResumeLayout();

			this.viewer1.ImageSizeMode = SizeMode.RatioStretch;


			this.settingsManager = settingsManager;
			this.fileManager = fileManager;

			timer1.Enabled = true;
			timer1.Interval = settingsManager.settings.ScreenshotCycleEach * 1000;

			// 06/04/2005 - creates here
			random = new Random();
		}


		/// <summary>
		/// Shoot another random picture
		/// </summary>
		private void ShootIt() 
		{
			// if there is not image, go away
			if (fileManager.images.Count > 0) 
			{
				// random value
				//Random r = new Random();
				int rr = random.Next(fileManager.images.Count);

				// get the equivalent image and shows on the PictureBox
				//Image img = Image.FromFile(fileManager.images[rr].ToString());
				
				// avoid creating an image for each shoot
				if (image != null) 
				{
					image.Dispose();
				}

				// 10/04/2005 - OutOfMemoryException
				try 
				{
					image = Image.FromFile(fileManager.images[rr].ToString());
					viewer1.Image = image;
				} 
				catch (OutOfMemoryException) 
				{
					// do nothing
				}				
			} 
			else 
			{
				// no message, just close
				this.Close();
			}
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
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Shoot
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(424, 286);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Shoot";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Shoot";
			this.TopMost = true;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Shoot_KeyDown);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);
			this.Load += new System.EventHandler(this.Shoot_Load);
			this.Closed += new System.EventHandler(this.Shoot_Closed);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseEvent);

		}
		#endregion

		/// <summary>
		/// While loading the form, independently how, must hide the button and shoot 
		/// another picture
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Shoot_Load(object sender, System.EventArgs e)
		{
			Cursor.Hide();
			ShootIt();
		}


		/// <summary>
		/// Each time interval, shoot another picture
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			ShootIt();
		}


		/// <summary>
		/// Anything that you do with the mouse, should close the Screensaver
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMouseEvent(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!MouseXY.IsEmpty)
			{
				Cursor.Show();
				if (MouseXY != new Point(e.X, e.Y))
					Close();
				if (e.Clicks > 0)
					Close();
			}
			MouseXY = new Point(e.X, e.Y);
		}


		/// <summary>
		/// Anything that you type, also close the Screensaver
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Shoot_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Cursor.Show();
			Close();
		}

		/// <summary>
		/// When closing, free image
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Shoot_Closed(object sender, System.EventArgs e)
		{
			if (image != null) 
			{
				image.Dispose();
			}
		}

	}
}
