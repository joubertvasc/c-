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
    public partial class CostForm : Form
    {
        private DataBase db;
        private CostsDS dataset;
        private bool inserting;
        private bool closing = false;
        private double latitude;
        private double longitude;
        private Int64 selectedTrip;
        private int panelHeight;

        public CostForm()
        {
            InitializeComponent();
        }

        private void CostForm_Closing(object sender, CancelEventArgs e)
        {
            closing = true;
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            closing = true;
            Close();
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

        public void Data(DataBase DB, CostsDS Dataset, Int64 SelectedTrip, double Latitude, double Longitude)
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

        private void CostForm_Load(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            tbCost.Text = "";
            tbValue.Text = "";

            ShowGrid();
        }

        private void miInsert_Click(object sender, EventArgs e)
        {
            inserting = true;

            tbCost.Text = "";
            tbValue.Text = "";

            ShowForm();
        }

        private void miEdit_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no costs to be edited.", "Warning");
            }
            else
            {
                inserting = false;

                DataRow row = dataset.DataTable.Rows[dgRows.CurrentRowIndex];

                tbCost.Text = (string)row["decost"];
                tbValue.Text = System.Convert.ToString(row["vlcost"]);

                ShowForm();
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no costs to be deleted.", "Warning");
            }
            else
            {
                DialogResult dlgResult =
                    MessageBox.Show("Do you really want to delete the cost '" +
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
            textColumn.MappingName = "decost";
            textColumn.HeaderText = "Description";
            textColumn.Width = 200;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "vlcost";
            textColumn.HeaderText = "Value";
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

            tbCost.Focus();
        }

        bool AreFieldsValid()
        {
            bool result = false;

            if (tbCost.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'Description' is mandatory.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbCost.Focus();
            }
            else if (tbCost.Text.Trim().Length > 250)
            {
                MessageBox.Show("The field 'Description' is larger than 250 characters.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbCost.Focus();
            }
            else if (tbValue.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'Value' is mandatory.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbValue.Focus();
            }
            else if (!Utils.IsNumberValid (tbValue.Text))
            {
                MessageBox.Show("The field 'Value' is not valid.",
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbValue.Focus();
            }
            else
            {
                result = true;
            }

            return result;
        }

        void AddNewRecord()
        {
            dataset.Add(selectedTrip, tbCost.Text, System.Convert.ToDouble (tbValue.Text), 
                latitude, longitude);
        }

        void EditRecord()
        {
            dataset.Update(selectedTrip, dgRows.CurrentRowIndex, tbCost.Text, 
                System.Convert.ToDouble(tbValue.Text), latitude, longitude);
        }

        private void miViewMap_Click(object sender, EventArgs e)
        {
            if (dataset.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("There are no costs to be viewd.", "Warning");
            }
            else
            {
                DataRow row = dataset.DataTable.Rows[dgRows.CurrentRowIndex];
                if (System.Convert.ToDouble(row["latitude"]) == 0 &&
                    System.Convert.ToDouble(row["longitude"]) == 0)
                {
                    MessageBox.Show(
                       "This Cost was not fixed with coordinates.",
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