using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    public partial class CentroCusto : Form
    {
        private CentroCustoDataSet centroCustoDataSet;
        private Boolean novoRegistro;
        private int colIndex;
        private Boolean ordemCrescente;
        private OrcamentoDataBase db;

        public CentroCusto(OrcamentoDataBase orcamentoDataBase)
        {
            InitializeComponent();
            db = orcamentoDataBase;
        }

        private void miMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CentroCusto_Load(object sender, EventArgs e)
        {
            centroCustoDataSet = new CentroCustoDataSet(db);
            centroCustoDataSet.SelectAll(false, true, "decentro", true);
            UpdateDataGrid();
            colIndex = 0;
            ordemCrescente = true;
        }
    
        private void UpdateDataGrid()
        {
            //  ler no datagrid
            DataGridTableStyle DGStyle = new DataGridTableStyle();

            // In this example the .NET DataGridTextBoxColumn class is used.
            DataGridTextBoxColumn textColumn;

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "decentro";
            textColumn.HeaderText = "Descrição";
            textColumn.Width = (grCentroCusto.Width - 36) * 60 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "flforauso";
            textColumn.HeaderText = "Fora de uso";
            textColumn.Width = (grCentroCusto.Width - 36) * 40 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            DGStyle.MappingName = centroCustoDataSet.DataTable.TableName;

            // The Clear() method is called to ensure that
            // the previous style is removed.
            grCentroCusto.TableStyles.Clear();

            // Add the new DataGridTableStyle collection to
            // the TableStyles collection
            grCentroCusto.TableStyles.Add(DGStyle);

            grCentroCusto.DataSource = centroCustoDataSet.DataTable;
        }

        private void btCancelar_Click_1(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Deseja realmente cancelar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dlgResult == DialogResult.Yes)
            {
                novoRegistro = false;
                tbCentroCusto.SelectedIndex = 0;
            }
        }

        private void miInserir_Click(object sender, EventArgs e)
        {
            novoRegistro = true;
            tbCentroCusto.SelectedIndex = 1;
            textBox1.Focus();
        }

        private void miEditar_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (centroCustoDataSet.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não existe registro para ser editado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
                tbCentroCusto.SelectedIndex = 0;
            }
            else
            {
                textBox1.Text = (String)centroCustoDataSet.DataTable.Rows[grCentroCusto.CurrentRowIndex].ItemArray[1];
                cbForaUso.Checked = ((String)centroCustoDataSet.DataTable.Rows[grCentroCusto.CurrentRowIndex].ItemArray[2] == "S");

                if (tbCentroCusto.SelectedIndex != 1)
                {
                    tbCentroCusto.SelectedIndex = 1;
                }

                textBox1.Focus();
            }
        }

        private void miExcluir_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (centroCustoDataSet.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não existe registro para ser excluído.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
                tbCentroCusto.SelectedIndex = 0;
            }
            else
            {
                DialogResult dlgResult = MessageBox.Show("Deseja realmente excluir o centro de custo: " +
                    (String)centroCustoDataSet.DataTable.Rows[grCentroCusto.CurrentRowIndex].ItemArray[1] + "?",
                    "Confirmação", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dlgResult == DialogResult.Yes)
                {
                    centroCustoDataSet.DelCentroCusto(grCentroCusto.CurrentRowIndex);
                }
            }
        }

        private void btSalvar_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("O campo Descrição é obrigatório.",
                    "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                String y;

                if (cbForaUso.Checked)
                {
                    y = "S";
                }
                else
                {
                    y = "N";
                }

                if (novoRegistro)
                {
                    centroCustoDataSet.AddCentroCusto(centroCustoDataSet.GeraCodigo(), textBox1.Text, y);
                }
                else
                {
                    centroCustoDataSet.AltCentroCusto(grCentroCusto.CurrentRowIndex,
                        (String)centroCustoDataSet.DataTable.Rows[grCentroCusto.CurrentRowIndex].ItemArray[0],
                        textBox1.Text, y);
                }

                novoRegistro = false;
                textBox1.Text = "";
                tbCentroCusto.SelectedIndex = 0;
            }
        }

        private void textBox1_GotFocus_1(object sender, EventArgs e)
        {
            ipCentroCusto.Enabled = true;
        }

        private void textBox1_LostFocus_1(object sender, EventArgs e)
        {
            ipCentroCusto.Enabled = false;
        }

        private void grCentroCusto_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 21)
            {
                if (e.X > 20 && e.X < 143)
                {
                    if (colIndex == 0) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 0; ordemCrescente = true; }
                }

                if (e.X > 143 && e.X < 229)
                {
                    if (colIndex == 1) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 1; ordemCrescente = true; }
                }

                ordenaGrid(colIndex, ordemCrescente);
            }
        }

        private void ordenaGrid(int coluna, Boolean ordem)
        {
            String sColuna = "";

            switch (coluna)
            {
                case 0:
                    sColuna = "decentro";
                    break;

                case 1:
                    sColuna = "flforauso";
                    break;

                default:
                    sColuna = "decentro";
                    break;
            }

            centroCustoDataSet.SelectAll(false, true, sColuna, ordem);
            UpdateDataGrid();
        }

        private void tbCentroCusto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!novoRegistro && tbCentroCusto.SelectedIndex == 1)
            {
                miEditar_Click(sender, e);
            }

        }
    }
}