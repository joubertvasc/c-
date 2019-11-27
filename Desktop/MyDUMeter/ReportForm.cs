/*
 * MyDUMeter - by leepie 
 * ReportForm.cs
 * Original Project.
 * Intact
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyDUMeter
{
	/// <summary>
	/// Summary description for ReportForm.
	/// </summary>
	public class ReportForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.ComponentModel.IContainer components;
		float hours;
		MyDUMeter.Log.RateLogDataTable table;
		Bitmap graph;
		Pen h;
		Pen r;
		Pen s;
		Pen b;
		int linespeed;
		private System.Windows.Forms.ToolTip toolTip1;
		DateTime to = DateTime.Now;
		DateTime from;
		ControlOptions options;

		public ReportForm(float hours, Log.RateLogDataTable table, ControlOptions options)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			from = to.AddHours(-hours);
			this.options = options;
			this.table = table;
			this.hours = hours;
			linespeed = (int)options.LineSpeed/8;;

			Text = "Report for the last " + hours + " hours";
			int width = 720;

			graph = new Bitmap(width, pictureBox1.Height);
			h = new Pen(options.Lines, 1);
			r = new Pen(options.Down);
			s = new Pen(options.Up);
			b = new Pen(options.Both);

			pictureBox1.Image = graph;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				h.Dispose();
				r.Dispose();
				b.Dispose();
				s.Dispose();

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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(716, 224);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_Click);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 0;
			this.toolTip1.ShowAlways = true;
			// 
			// ReportForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(716, 224);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.Name = "ReportForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ReportForm";
			this.Resize += new System.EventHandler(this.ReportForm_Resize);
			this.Load += new System.EventHandler(this.ReportForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ReportForm_Load(object sender, System.EventArgs e)
		{
			DrawGraph();
		}

		private void DrawGraph()
		{
			Graphics g = Graphics.FromImage(graph);
			g.Clear(Color.WhiteSmoke);
         
			foreach (Log.RateLogRow row in table)
			{
				if (row.ID >= from & row.ID < to)
				{
					float maxr = row.Recv;
					float minr = row.Send;
					Pen p = r;
					if (row.Recv < row.Send)
					{
						p = s;
						maxr = row.Send;
						minr = row.Recv;
					}

					float w = graph.Width/(hours*60F);
					float x = (float)((row.ID - to.AddHours(-hours)).TotalMinutes*w);
					p.Width = w;
					b.Width = w;
					g.DrawLine(p, x - w/2, graph.Height, 
						x - w/2, graph.Height - graph.Height * ((maxr/60) / linespeed));
					g.DrawLine(b, x- w/2, graph.Height, 
						x - w/2, graph.Height - graph.Height * ((minr/60) / linespeed));
				}
			}

			pictureBox1.Image = graph;
			g.Dispose();
		}

		int inc = 60;

		private void GetSelection(out int left, out int right)
		{
			left = selpt1.X;
			right = selpt2.X;
			if (left > right)
			{
				left = selpt2.X;
				right = selpt1.X;
			}
			if (left < 0) left = 0;
			if (right > graph.Width) right = graph.Width;
		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{         
			h.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

			for (int i = inc; i < graph.Width; i+=inc)
			{
				e.Graphics.DrawLine(h,
					i, 0 , i, graph.Height);
			}

			h.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			int vlines = 4;

			for (int j = 1; j < vlines; j++)
			{
				int y = j*graph.Height/vlines;
				e.Graphics.DrawLine(h, 0, y, graph.Width, y);
			}

			if (selected)
			{
				int l,r;
				GetSelection(out l, out r);
				Rectangle dr = new Rectangle(l, 0, r - l, graph.Height - 1);
				Color c = Color.FromArgb(50, options.Selection);
				Pen selp = new Pen(options.Selection, 1);
            
				Brush selb = new SolidBrush(c);
				e.Graphics.FillRectangle(selb, dr);
				e.Graphics.DrawRectangle(selp, dr);
				selb.Dispose();
				selp.Dispose();
			}
			else if (mouseover)
			{
				Point m = mousept2;//pictureBox1.PointToClient(MousePosition);
				int section = m.X/inc;

				float minutewidth = graph.Width/(hours*60F);
            
				int l = (inc) * section,
					r = (inc) * (section + 1);
				//GetSelection(out l, out r);
				Rectangle dr = new Rectangle(l, 0, r - l, graph.Height - 1);
				Color c = Color.FromArgb(50, options.Selection);
				Pen selp = new Pen(options.Selection, 1);
            
				Brush selb = new SolidBrush(c);
				e.Graphics.FillRectangle(selb, dr);
				e.Graphics.DrawRectangle(selp, dr);
				selb.Dispose();
				selp.Dispose();
			}
		}

		private void ReportForm_Resize(object sender, System.EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
			{
				graph = new Bitmap(pictureBox1.Width - 2, pictureBox1.Height - 2);
				DrawGraph();
			}
			mouseover = false;
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
			if (FormBorderStyle != FormBorderStyle.SizableToolWindow)
				FormBorderStyle = FormBorderStyle.SizableToolWindow;
			else 
				FormBorderStyle = FormBorderStyle.None;
		}

		bool mousedown = false;
		bool selecting = false;
		bool selected = false;

		Point selpt1;
		Point selpt2;

		Point mousept;

		private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
         
			if (selected)
			{
				int l,r;
				GetSelection(out l, out r);
				if (e.X > l & e.X < r)
				{
					float minutewidth = graph.Width/(hours*60F);

					from = to.AddHours(-hours).AddMinutes((l/minutewidth));
					to = to.AddHours(-hours).AddMinutes((r/minutewidth));
					hours = (float)(to - from).TotalHours;

					DrawGraph();
					Text = String.Format("Report from {0} to {1}. Period: {2}",
						from, to,
						(to - from).TotalMinutes >= 60 ? 
						(to - from).Hours.ToString("f0") + " hours " + (to - from).Minutes.ToString("f0") + " minutes" : 
						(to - from).Minutes.ToString("f0") + " minutes " + (to - from).Seconds.ToString("f0") + " seconds"
						);

					selected = false;
					Cursor = Cursors.Default;
					return;
				}
			}
			pictureBox1.Invalidate();
			selected = false;
         
			switch (e.Button)
			{
				case MouseButtons.Left:
					mousept = new Point(e.X, e.Y);
					mousedown = true;
					mouseover = true;
					break;
				case MouseButtons.Right:
					selpt1 = new Point(e.X, e.Y);
					selecting = true;
					break;
			}   
		}

		private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (mousedown)
			{
				Location = new Point(Location.X - (mousept.X - e.X), Location.Y - (mousept.Y - e.Y)); 
			}
			else if (selecting)
			{
				selpt2 = new Point(e.X, e.Y);
				selected = true;
				pictureBox1.Invalidate();
			}
			else if (selected)
			{
				int l,r;
				GetSelection(out l, out r);
				if (e.X > l & r > e.X)
					Cursor = Cursors.NoMove2D;
				else Cursor = Cursors.Default;
			}
			mousept2 = new Point(e.X, e.Y);
		}

		Point mousept2;

		private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Right:
					selpt2 = new Point(e.X, e.Y);
					selecting = false;
					selected = true;
					//DrawSelecting();
					pictureBox1.Invalidate();
					ShowTT();
					break;
			}   
			mousedown = false;  
		}

		private void pictureBox1_MouseHover(object sender, System.EventArgs e)
		{
			mouseover = true;
		}

		private void ShowTT()
		{
			//dont confuse these with the 
			DateTime from;
			DateTime to;

			float minutewidth = graph.Width/(hours*60F);

			float rtot = 0;
			float stot = 0;
         
			int counter = 0; //dammit :) not 0 , NOT 1 but 0 infact

			if (selected)
			{
				int l,r;
				GetSelection(out l, out r);

				from = this.to.AddHours(-hours).AddMinutes((l/minutewidth));
				to = this.to.AddHours(-hours).AddMinutes((r/minutewidth));
			}
			else 
			{
				Point m = pictureBox1.PointToClient(MousePosition);

				int section = m.X/inc;

				from = this.to.AddHours(-hours).AddMinutes((inc/minutewidth) * section);
				to = this.to.AddHours(-hours).AddMinutes((inc/minutewidth) * (section + 1));
			}
			foreach (Log.RateLogRow row in table)
			{
				if (row.ID >= from &
					row.ID < to )
				{
					rtot += row.Recv;
					stot += row.Send;
					counter++;
				}
			}
			toolTip1.RemoveAll();
			toolTip1.SetToolTip(pictureBox1, String.Format(@"Period: {6}
Samples: {7} 
From:{4}
To:  {5}
Total Recv:{0} KB
Total Sent:{1} KB
Avg Recv:{2} KB/sec
Avg Sent:{3} KB/sec",
				(rtot/1024F).ToString("f0"),
				(stot/1024F).ToString("f0"),
				counter == 0 ? 0F.ToString("f1") : (rtot/(counter*options.LogInterval*1024)).ToString("f1"),
				counter == 0 ? 0F.ToString("f1") : (stot/(counter*options.LogInterval*1024)).ToString("f1"),
				from, 
				to,
				(to - from).TotalMinutes >= 60 ? 
				(to - from).Hours.ToString("f0") + " hours " + (to - from).Minutes.ToString("f0") + " minutes" : 
				(to - from).Minutes.ToString("f0") + " minutes " + (to - from).Seconds.ToString("f0") + " seconds",
				counter
				));
         
		}

		private void pictureBox1_Click_1(object sender, System.EventArgs e)
		{
			ShowTT();
		}

		bool mouseover = false;
	}
}
