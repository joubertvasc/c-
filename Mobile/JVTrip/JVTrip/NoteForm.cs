using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVSQL;
using JVGPS.Forms;

namespace JVTrip
{
    public partial class NoteForm : Form
    {
        private DataBase db;
        private NotesDS dataset;
        private bool inserting;
        private bool closing = false;
        private double latitude;
        private double longitude;
        private Int64 selectedTrip;
        private int heightOffSet = 21;
        private int panelHeight;

        public NoteForm()
        {
            InitializeComponent();
        }

        private void NoteForm_Closing(object sender, CancelEventArgs e)
        {
            closing = true;
        }

        private void miOk_Click_1(object sender, EventArgs e)
        {
            closing = true;
            Close();
        }

        private void NoteForm_Load(object sender, EventArgs e)
        {
            ShowGrid();
        }

        public void Data(DataBase DB, NotesDS Dataset, Int64 SelectedTrip, double Latitude, double Longitude)
        {
            db = DB;
            dataset = Dataset;
            latitude = Latitude;
            longitude = Longitude;
            selectedTrip = SelectedTrip;

            if (latitude != 0 && longitude != 0)
            {
                lblPosition.Text = System.Convert.ToString(latitude) + ", " +
                                   System.Convert.ToString(longitude);
            }
            else
            {
                lblPosition.Text = "";
            }

            UpdateDataGrid();
        }

        private void miInsert_Click(object sender, EventArgs e)
        {
            inserting = true;

            tbNotes.Text = "";

            ShowForm();
        }

        private void miEdit_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no notes to be edited.", "Warning");
            }
            else
            {
                inserting = false;

                DataRow row = dataset.DataTable.Rows[dgRows.CurrentRowIndex];

                tbNotes.Text = (string)row["denote"];

                ShowForm();
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no notes to be deleted.", "Warning");
            }
            else
            {
                DialogResult dlgResult =
                    MessageBox.Show("Do you really want to delete the note '" +
                    (string)dataset.DataTable.Rows[dgRows.CurrentRowIndex].ItemArray[2] + "?",
                    "Confirmation", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dlgResult == DialogResult.Yes)
                {
                    dataset.Del(selectedTrip, dgRows.CurrentRowIndex);
                }
                ipForm.Enabled = false;
            }
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            tbNotes.Text = "";

            ShowGrid();
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

        private void tbNotes_GotFocus(object sender, EventArgs e)
        {
            if (!closing)
                ipForm.Enabled = true;
        }

        private void tbNotes_LostFocus(object sender, EventArgs e)
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
            textColumn.MappingName = "dtcreated";
            textColumn.HeaderText = "Date";
            textColumn.Width = 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "denote";
            textColumn.HeaderText = "Note";
            textColumn.Width = 200;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "latitude";
            textColumn.HeaderText = "Latitude";
            textColumn.Width = 200;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "longitude";
            textColumn.HeaderText = "Longitude";
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

            tbNotes.Focus();
        }

        bool AreFieldsValid()
        {
            bool result = false;

            if (tbNotes.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'Note' is mandatory.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbNotes.Focus();
            }
            else if (tbNotes.Text.Trim().Length > 250)
            {
                MessageBox.Show("The field 'Notes' is larger than 250 characters.",
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
            dataset.Add(selectedTrip, tbNotes.Text, latitude, longitude);
        }

        void EditRecord()
        {
            dataset.Update(selectedTrip, dgRows.CurrentRowIndex, tbNotes.Text, latitude, longitude);
        }

        private void miViewMap_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no notes to be viewd.", "Warning");
            }
            else
            {
                DataRow row = dataset.DataTable.Rows[dgRows.CurrentRowIndex];
                if (System.Convert.ToDouble(row["latitude"]) == 0 &&
                    System.Convert.ToDouble(row["longitude"]) == 0)
                {
                    MessageBox.Show(
                       "This Note was not fixed with coordinates.",
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Asterisk,
                       MessageBoxDefaultButton.Button1);
                }
                else
                {
                    GoogleMaps gm = new GoogleMaps();
                    gm.ViewMap(System.Convert.ToDouble(row["latitude"]), 
                               System.Convert.ToDouble(row["longitude"]));
                    gm.Show();
                }
            }
        }
    }
}