using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
namespace DrJSoftware
{
	/// <summary>
	/// Summary description for Thumbnail.
	/// </summary>
	public class Thumbnail : System.Windows.Forms.UserControl
	{
		#region Fields
		private string _filePath = "";
		private System.Drawing.Image _image = null;
		#endregion

		#region Properties
		public string FilePath
		{
			get { return _filePath; }
		}
		public System.Drawing.Image Image
		{
			get { return _image; }
		}
		public bool Handled
		{
			get { return !this.pictureBox.BackColor.Equals(SystemColors.Window); }
			set
			{
				if (value)
				{
					this.pictureBox.BackColor = Color.FromArgb(255, 192, 128);
				}
				else
				{
					this.pictureBox.BackColor = SystemColors.Window;
				}
			}
		}
		#endregion

		#region Generated Fields
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Label labelFileName;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label labelImageSize;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Generated Methods
		public Thumbnail(string filePath)
		{
			InitializeComponent();

			_filePath = filePath;
			try
			{
				_image = System.Drawing.Image.FromFile(_filePath);
			}
			catch (System.OutOfMemoryException e)
			{
				// file does not represent a valid image
				throw e;
			}
			int max = Math.Min(this.pictureBox.Width, this.pictureBox.Height);
			int width = _image.Width;
			int height = _image.Height;
			// determine the size for the thumbnail image
			if (_image.Width > max || _image.Height > max)
			{
				if (_image.Width > _image.Height)
				{
					width = max;
					height = (int) (_image.Height * max / _image.Width);
				}
				else
				{
					width = (int) (_image.Width * max / _image.Height);
					height = max;
				}
			}
			// set feedback information
			this.pictureBox.Image = new Bitmap(_image, width, height);
			this.labelFileName.Text = Path.GetFileName(_filePath);
			this.labelImageSize.Text = string.Format("{0} x {1}", _image.Size.Width, _image.Size.Height);
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
				if (_image != null)
				{
					_image.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel = new System.Windows.Forms.Panel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.labelFileName = new System.Windows.Forms.Label();
			this.labelImageSize = new System.Windows.Forms.Label();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.BackColor = System.Drawing.SystemColors.Control;
			this.panel.Controls.Add(this.pictureBox);
			this.panel.Controls.Add(this.labelFileName);
			this.panel.Controls.Add(this.labelImageSize);
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(2, 2);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(126, 166);
			this.panel.TabIndex = 0;
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(126, 126);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox.TabIndex = 1;
			this.pictureBox.TabStop = false;
			// 
			// labelFileName
			// 
			this.labelFileName.BackColor = System.Drawing.SystemColors.Window;
			this.labelFileName.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.labelFileName.Location = new System.Drawing.Point(0, 127);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Size = new System.Drawing.Size(126, 23);
			this.labelFileName.TabIndex = 0;
			this.labelFileName.Text = "Filename";
			this.labelFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelImageSize
			// 
			this.labelImageSize.BackColor = System.Drawing.SystemColors.Window;
			this.labelImageSize.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.labelImageSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelImageSize.Location = new System.Drawing.Point(0, 150);
			this.labelImageSize.Name = "labelImageSize";
			this.labelImageSize.Size = new System.Drawing.Size(126, 16);
			this.labelImageSize.TabIndex = 2;
			this.labelImageSize.Text = "image size";
			this.labelImageSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Thumbnail
			// 
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Controls.Add(this.panel);
			this.DockPadding.All = 2;
			this.Name = "Thumbnail";
			this.Size = new System.Drawing.Size(130, 170);
			this.panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
