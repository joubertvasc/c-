using System;
using System.Windows.Forms;

namespace MyDUMeter
{
	/// <summary>
	/// Summary description for OptionForm.
	/// </summary>
	public class OptionForm : Form
	{
      private System.Windows.Forms.PropertyGrid propertyGrid1;

      private void InitializeComponent()
      {
		  this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
		  this.SuspendLayout();
		  // 
		  // propertyGrid1
		  // 
		  this.propertyGrid1.CommandsVisibleIfAvailable = true;
		  this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		  this.propertyGrid1.LargeButtons = false;
		  this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
		  this.propertyGrid1.Name = "propertyGrid1";
		  this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
		  this.propertyGrid1.Size = new System.Drawing.Size(292, 556);
		  this.propertyGrid1.TabIndex = 0;
		  this.propertyGrid1.Text = "propertyGrid1";
		  this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
		  this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
		  // 
		  // OptionForm
		  // 
		  this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		  this.ClientSize = new System.Drawing.Size(292, 556);
		  this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		this.propertyGrid1});
		  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		  this.MaximizeBox = false;
		  this.MinimizeBox = false;
		  this.Name = "OptionForm";
		  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		  this.Text = "Options";
		  this.ResumeLayout(false);

	  }
   
		public OptionForm(object propclass)
		{
         InitializeComponent();
         propertyGrid1.SelectedObject = propclass;
		}
	}
}
