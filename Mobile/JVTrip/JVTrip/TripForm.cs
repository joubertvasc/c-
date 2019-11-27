using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVSQL;

namespace JVTrip
{
    public partial class TripForm : Form
    {
        private DataBase db;
        private TripDS dataset;
        private bool inserting;
        private bool closing = false;
        private int heightOffSet = 21;
        private int panelHeight;

        public TripForm()
        {
            InitializeComponent();
        }

        private void TripForm_Closing(object sender, CancelEventArgs e)
        {
            closing = true;
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            closing = true; 
            Close();
        }

        public void Data(DataBase DB, TripDS Dataset)
        {
            db = DB;
            dataset = Dataset;

            UpdateDataGrid();
        }

        private void TripForm_Load(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void miInsert_Click(object sender, EventArgs e)
        {
            inserting = true;
            
            tbName.Text = "";
            tbFrom.Text = "";
            tbTo.Text = "";
            tbNotes.Text = "";

            ShowForm();
        }

        private void miEdit_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no trips to be edited.", "Warning");
            }
            else
            {
                inserting = false;

                DataRow row = dataset.DataTable.Rows[dgRows.CurrentRowIndex];

                tbName.Text = (string)row["nmtrip"];
                tbFrom.Text = (string)row["nmstart"];
                tbTo.Text = (string)row["nmdestiny"];
                tbNotes.Text = (string)row["blnotes"];

                ShowForm();
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no trip to be deleted.", "Warning");
            }
            else
            {
                DialogResult dlgResult = 
                    MessageBox.Show("Do you really want to delete the trip '" + 
                    (string)dataset.DataTable.Rows[dgRows.CurrentRowIndex].ItemArray[1] + "?",
                    "Confirmation", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dlgResult == DialogResult.Yes)
                {
                    dataset.Del(dgRows.CurrentRowIndex);
                }
                ipForm.Enabled = false;
            }
        }

        private void miConfirm_Click(object sender, EventArgs e)
        {
            if (AreFieldsValid())
            {
                ShowGrid();

                if (inserting)
                {
                    AddNewRecord();
                }
                else
                {
                    EditRecord();
                }
            }
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            tbName.Text = "";
            tbFrom.Text = "";
            tbTo.Text = "";
            tbNotes.Text = "";

            ShowGrid();
        }

        private void tbName_GotFocus(object sender, EventArgs e)
        {
            if (!closing)
                ipForm.Enabled = true;
        }

        private void tbName_LostFocus(object sender, EventArgs e)
        {
            if (!closing)
                ipForm.Enabled = false;
        }

        private void ipForm_EnabledChanged(object sender, EventArgs e)
        {
            if (!closing)
            {
                if (!closing)
                {
                    if (ipForm.Enabled)
                    {
                        pnlEdit.Height -= (ipForm.Bounds.Height + (Screen.PrimaryScreen.WorkingArea.Height / 10));
                    }
                    else
                    {
                        pnlEdit.Height = panelHeight;
                    }
                }
            }
        }

        void UpdateDataGrid()
        {
            DataGridTableStyle DGStyle = new DataGridTableStyle();
            DataGridTextBoxColumn textColumn;

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "nmtrip";
            textColumn.HeaderText = "Name";
            textColumn.Width = 200;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "nmstart";
            textColumn.HeaderText = "From";
            textColumn.Width = 200;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "nmdestiny";
            textColumn.HeaderText = "To";
            textColumn.Width = 200;
            DGStyle.GridColumnStyles.Add(textColumn);

            DGStyle.MappingName = dataset.DataTable.TableName;

            // The Clear() method is called to ensure that
            // the previous style is removed.
            dgRows.TableStyles.Clear();

            // Add the new DataGridTableStyle collection to
            // the TableStyles collection
            dgRows.TableStyles.Add(DGStyle);

            dgRows.DataSource = dataset.DataTable;
        }

        void ShowGrid()
        {
            dgRows.Visible = true;
            dgRows.Dock = DockStyle.Fill;
            pnlEdit.Visible = false;
            ipForm.Enabled = false;
            Menu = mmMain;
        }

        void ShowForm()
        {
            dgRows.Visible = false;
            pnlEdit.Visible = true;
            pnlEdit.Width = Screen.PrimaryScreen.WorkingArea.Width;
            pnlEdit.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pnlEdit.Top = 0;
            pnlEdit.Left = 0;
            panelHeight = pnlEdit.Height;
            Menu = mmEdit;

            tbName.Focus();
        }

        bool AreFieldsValid()
        {
            bool result = false;

            if (tbName.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'Name' is mandatory.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbName.Focus();
            }
            else if (tbName.Text.Trim().Length > 20)
            {
                MessageBox.Show("The field 'Name' is larger than 20 characters.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbName.Focus();
            }
            else if (tbFrom.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'From' is mandatory.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbFrom.Focus();
            }
            else if (tbFrom.Text.Trim().Length > 250)
            {
                MessageBox.Show("The field 'Name' is larger than 250 characters.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbFrom.Focus();
            }
            else if (tbTo.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'To' is mandatory.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbTo.Focus();
            }
            else if (tbTo.Text.Trim().Length > 250)
            {
                MessageBox.Show("The field 'To' is larger than 250 characters.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbTo.Focus();
            }
            else if (tbNotes.Text.Trim().Length > 2000)
            {
                MessageBox.Show("The field 'Notes' is larger than 2000 characters.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbNotes.Focus();
            }
            else
            {
                result = true;
            }

            return result;
        }

        void AddNewRecord()
        {
            dataset.Add(tbName.Text, tbFrom.Text, tbTo.Text, tbNotes.Text);
        }

        void EditRecord()
        {
            dataset.Update(dgRows.CurrentRowIndex, tbName.Text, tbFrom.Text, tbTo.Text, tbNotes.Text);
        }
    }
}