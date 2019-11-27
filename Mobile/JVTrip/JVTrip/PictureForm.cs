using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVSQL;
using JVUtils;
using JVGPS.Forms;

namespace JVTrip
{
    public partial class PictureForm : Form
    {
        private DataBase db;
        private PicturesDS dataset;
        private bool inserting;
        private bool closing = false;
        private double latitude;
        private double longitude;
        private Int64 selectedTrip;
        private int panelHeight;

        public PictureForm()
        {
            InitializeComponent();
        }

        private void PictureForm_Closing(object sender, CancelEventArgs e)
        {
            closing = true;
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            closing = true;
            Close();
        }

        private void PictureForm_Load(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void tbCost_GotFocus(object sender, EventArgs e)
        {
            if (!closing)
                ipForm.Enabled = true;
        }

        private void tbCost_LostFocus(object sender, EventArgs e)
        {
            if (!closing)
                ipForm.Enabled = false;
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            tbPicture.Text = "";
            tbPath.Text = "";

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

        private void miInsert_Click(object sender, EventArgs e)
        {
            inserting = true;

            tbPicture.Text = "";
            tbPath.Text = "";

            ShowForm();
        }

        private void miEdit_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no picture to be edited.", "Warning");
            }
            else
            {
                inserting = false;

                DataRow row = dataset.DataTable.Rows[dgRows.CurrentRowIndex];

                tbPicture.Text = (string)row["depicture"];
                tbPath.Text = System.Convert.ToString(row["depathpicture"]);

                ShowForm();
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no picture to be deleted.", "Warning");
            }
            else
            {
                DialogResult dlgResult =
                    MessageBox.Show("Do you really want to delete the picture '" +
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

        private void ipForm_EnabledChanged(object sender, EventArgs e)
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

        public void Data(DataBase DB, PicturesDS Dataset, Int64 SelectedTrip, double Latitude, double Longitude)
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
            textColumn.MappingName = "depicture";
            textColumn.HeaderText = "Description";
            textColumn.Width = 200;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "depathpicture";
            textColumn.HeaderText = "Path";
            textColumn.Width = 100;
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

            tbPicture.Focus();
        }

        bool AreFieldsValid()
        {
            bool result = false;

            if (tbPicture.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'Description' is mandatory.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbPicture.Focus();
            }
            else if (tbPicture.Text.Trim().Length > 250)
            {
                MessageBox.Show("The field 'Description' is larger than 250 characters.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbPicture.Focus();
            }
            else if (tbPath.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'Value' is mandatory.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbPath.Focus();
            }
            else if (tbPath.Text.Trim().Length > 250)
            {
                MessageBox.Show("The field 'Path' is larger than 250 characters.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbPath.Focus();
            }
            else
            {
                result = true;
            }

            return result;
        }

        void AddNewRecord()
        {
            dataset.Add(selectedTrip, tbPicture.Text, tbPath.Text, latitude, longitude);
        }

        void EditRecord()
        {
            dataset.Update(selectedTrip, dgRows.CurrentRowIndex, tbPicture.Text, tbPath.Text, 
                latitude, longitude);
        }

        private void btPicture_Click(object sender, EventArgs e)
        {
            if (ofdPicture.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = ofdPicture.FileName;
            }
        }

        private void miViewMap_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no pictures to be viewd.", "Warning");
            }
            else
            {
                DataRow row = dataset.DataTable.Rows[dgRows.CurrentRowIndex];
                if (System.Convert.ToDouble(row["latitude"]) == 0 &&
                    System.Convert.ToDouble(row["longitude"]) == 0)
                {
                    MessageBox.Show(
                       "This picture was not fixed with coordinates.",
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