using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    public partial class TipoMovim : Form
    {
        private TipoMovimDataSet tipoMovimDataSet;
        private Boolean novoRegistro;
        private int colIndex;
        private Boolean ordemCrescente;
        private OrcamentoDataBase db;

        public TipoMovim(OrcamentoDataBase orcamentoDataBase)
        {
            InitializeComponent();
            db = orcamentoDataBase;
        }

        private void miMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TipoMovim_Load(object sender, EventArgs e)
        {
            tipoMovimDataSet = new TipoMovimDataSet(db);
            tipoMovimDataSet.SelectAll(false, true, "detipomovim", true);
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
            textColumn.MappingName = "detipomovim";
            textColumn.HeaderText = "Descrição";
            textColumn.Width = (grTipoMovim.Width - 36) * 60 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "flnatureza";
            textColumn.HeaderText = "Natureza";
            textColumn.Width = (grTipoMovim.Width - 36) * 20 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "flforauso";
            textColumn.HeaderText = "Fora de uso";
            textColumn.Width = (grTipoMovim.Width - 36) * 20 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            DGStyle.MappingName = tipoMovimDataSet.DataTable.TableName;

            // The Clear() method is called to ensure that
            // the previous style is removed.
            grTipoMovim.TableStyles.Clear();

            // Add the new DataGridTableStyle collection to
            // the TableStyles collection
            grTipoMovim.TableStyles.Add(DGStyle);

            grTipoMovim.DataSource = tipoMovimDataSet.DataTable;
        }

        private void grTipoMovim_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 21)
            {
                if (e.X > 20 && e.X < 129)
                {
                    if (colIndex == 0) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 0; ordemCrescente = true; }
                }
                if (e.X > 148 && e.X < 187)
                {
                    if (colIndex == 1) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 1; ordemCrescente = true; }
                }
                if (e.X > 191 && e.X < 229)
                {
                    if (colIndex == 2) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 2; ordemCrescente = true; }
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
                    sColuna = "detipomovim";
                    break;

                case 1:
                    sColuna = "flnatureza";
                    break;

                case 2:
                    sColuna = "flforauso";
                    break;

                default:
                    sColuna = "detipomovim";
                    break;
            }

            tipoMovimDataSet.SelectAll(false, true, sColuna, ordem);
            UpdateDataGrid();
        }

        private void miInserir_Click(object sender, EventArgs e)
        {
            novoRegistro = true;
            tcTipoMovim.SelectedIndex = 1;
            edDescricao.Focus();
        }

        private void miEditar_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (tipoMovimDataSet.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não existe registro para ser editado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
                tcTipoMovim.SelectedIndex = 0;
            }
            else
            {
                edDescricao.Text = (String)tipoMovimDataSet.DataTable.Rows[grTipoMovim.CurrentRowIndex].ItemArray[1];

                String x = (String)tipoMovimDataSet.DataTable.Rows[grTipoMovim.CurrentRowIndex].ItemArray[2];
                if (x.Equals("D"))
                {
                    rbDebito.Checked = true;
                }
                else
                {
                    rbCredito.Checked = true;
                }

                cbFlForaUso.Checked = ((String)tipoMovimDataSet.DataTable.Rows[grTipoMovim.CurrentRowIndex].ItemArray[3] == "S");

                if (tcTipoMovim.SelectedIndex != 1)
                {
                    tcTipoMovim.SelectedIndex = 1;
                }
                edDescricao.Focus();
            }
        }

        private void miExcluir_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (tipoMovimDataSet.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não existe registro para ser excluído.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                DialogResult dlgResult = MessageBox.Show("Deseja realmente excluir o tipo de movimentação: " +
                                                          (String)tipoMovimDataSet.DataTable.Rows[grTipoMovim.CurrentRowIndex].ItemArray[1] + "?",
                                                          "Confirmação", MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question,
                                                          MessageBoxDefaultButton.Button2);

                if (dlgResult == DialogResult.Yes)
                {
                    tipoMovimDataSet.DelTipoMovim(grTipoMovim.CurrentRowIndex);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (edDescricao.Text == "")
            {
                MessageBox.Show("O campo Descrição é obrigatório.",
                    "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                String x;
                String y;
                if (rbDebito.Checked)
                {
                    x = "D";
                }
                else
                {
                    x = "C";
                }

                if (cbFlForaUso.Checked)
                {
                    y = "S";
                }
                else
                {
                    y = "N";
                }

                if (novoRegistro)
                {
                    tipoMovimDataSet.AddTipoMovim(tipoMovimDataSet.GeraCodigo(), edDescricao.Text, x, y);
                }
                else
                {
                    tipoMovimDataSet.AltTipoMovim(grTipoMovim.CurrentRowIndex,
                        (String)tipoMovimDataSet.DataTable.Rows[grTipoMovim.CurrentRowIndex].ItemArray[0],
                        edDescricao.Text, x, y);
                }

                novoRegistro = false;
                edDescricao.Text = "";
                rbDebito.Checked = true;

                tcTipoMovim.SelectedIndex = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Deseja realmente cancelar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dlgResult == DialogResult.Yes)
            {
                novoRegistro = false;
                tcTipoMovim.SelectedIndex = 0;
            }
        }

        private void tcTipoMovim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!novoRegistro && tcTipoMovim.SelectedIndex == 1)
            {
                miEditar_Click(sender, e);
            }
        }

        private void edDescricao_GotFocus(object sender, EventArgs e)
        {
            ipTipoMovim.Enabled = true;
        }

        private void edDescricao_LostFocus(object sender, EventArgs e)
        {
            ipTipoMovim.Enabled = false;
        }
    }
}