using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MyDUMeter
{
	/// <summary>
	/// Summary description for AutoResizeDataGridTableStyle.
	/// </summary>
	public class AutoResizeDataGridTableStyle: DataGridTableStyle
	{
		private int OFFSET_GRID = 39;

		public AutoResizeDataGridTableStyle(): base()
		{
			AlternatingBackColor = System.Drawing.Color.Lavender;
			BackColor = System.Drawing.Color.WhiteSmoke;
			ForeColor = System.Drawing.Color.MidnightBlue;
			GridLineColor = System.Drawing.Color.Gainsboro;
			GridLineStyle = System.Windows.Forms.DataGridLineStyle.Solid;
			HeaderBackColor = System.Drawing.Color.MidnightBlue;
			HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			HeaderForeColor = System.Drawing.Color.WhiteSmoke;
			LinkColor = System.Drawing.Color.Teal;
			ReadOnly = true;
			SelectionBackColor = System.Drawing.Color.CadetBlue;
			SelectionForeColor = System.Drawing.Color.WhiteSmoke;
		}

		/// <summary>
		/// Called when the DataSource property of the parent DataGrid 
		/// changes. When the new source is a DataTable, rebuild the 
		/// DataGridColumnStyles and resize.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		public void OnDataSourceChanged(object sender, EventArgs e)
//		{
//			GridColumnStyles.Clear();
//			if(DataGrid != null && DataGrid.DataSource != null && DataGrid.DataSource is DataTable)
//			{
//				DataTable currentTable = (DataTable)DataGrid.DataSource;
//				foreach(DataColumn column in currentTable.Columns)
//				{
//					DataGridColumnStyle style = new DataGridTextBoxColumn();
//					style.HeaderText = column.ColumnName;
//					style.MappingName = column.ColumnName;
//
//					if (column.ColumnName.ToLower().Equals("day") || column.ColumnName.ToLower().Equals("month / year"))
//					{
//						style.Alignment = HorizontalAlignment.Left;
//					} 
//					else 
//					{
//						style.Alignment = HorizontalAlignment.Right;
//					}
//
//
//					GridColumnStyles.Add(style);
//				}
//			}
//			// Call the eventhandler for resize events
//			OnDataGridResize(this,new EventArgs());
//		}

		public void OnDataGridResize(object sender, EventArgs e)
		{
			// Parent?
			if(DataGrid != null)
			{
				// Get column width
				int columnWidth;
				if( (columnWidth = GetGridColumnWidth()) != -1)
				{
					// Get the client width
					int clientWidth = DataGrid.ClientSize.Width;
					// Are there columns? redundant check
					if(GridColumnStyles.Count > 0)
					{
						// whats the newWidth
						int newWidth = GridColumnStyles[GridColumnStyles.Count - 1].Width + clientWidth - columnWidth;
						
						// is the new width valid?
						if(newWidth > PreferredColumnWidth)
							GridColumnStyles[GridColumnStyles.Count - 1].Width = newWidth;
						else
							GridColumnStyles[GridColumnStyles.Count - 1].Width = PreferredColumnWidth;
					}
				}
				// Redraw
				DataGrid.Invalidate(true);
			}
		}

		private int GetGridColumnWidth()
		{
			// No columns, return error
			if(GridColumnStyles.Count == 0)
				return -1;
			// Easy 1
			int width = 0;
			foreach(DataGridColumnStyle columnStyle in GridColumnStyles)
			{
				width += columnStyle.Width;
			}
		
			return width + OFFSET_GRID;
		}
	}
}
