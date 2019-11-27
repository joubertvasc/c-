using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    public partial class CreditoFixo : Form
    {
        private CreditoFixoDataSet creditoFixoDataSet;
        private Boolean novoRegistro;
        private int colIndex;
        private Boolean ordemCrescente;
        private OrcamentoDataBase db;

        public CreditoFixo(OrcamentoDataBase orcamentoDataBase)
        {
            InitializeComponent();
            db = orcamentoDataBase;
        }

        private void miMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miEditar_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (creditoFixoDataSet.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não existe registro para ser editado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
                tcContaFixa.SelectedIndex = 0;
            }
            else
            {
                deDescricao.Text = (String)creditoFixoDataSet.DataTable.Rows[grContaFixa.CurrentRowIndex].ItemArray[1];
                nuDiaVencimento.Value = System.Convert.ToInt16(creditoFixoDataSet.DataTable.Rows[grContaFixa.CurrentRowIndex].ItemArray[3]);
                vlConta.Text = System.Convert.ToString(creditoFixoDataSet.DataTable.Rows[grContaFixa.CurrentRowIndex].ItemArray[4]);
                cbTipoMovim.SelectedIndex = cbTipoMovim.Items.IndexOf(creditoFixoDataSet.EncontraDeTipoMovim((String)creditoFixoDataSet.DataTable.Rows[grContaFixa.CurrentRowIndex].ItemArray[2]));
                flForaDeUso.Checked = ((String)creditoFixoDataSet.DataTable.Rows[grContaFixa.CurrentRowIndex].ItemArray[5] == "S");

                if (tcContaFixa.SelectedIndex != 1)
                {
                    tcContaFixa.SelectedIndex = 1;
                }

                deDescricao.Focus();
            }
        }

        private void miInserir_Click(object sender, EventArgs e)
        {
            novoRegistro = true;
            tcContaFixa.SelectedIndex = 1;
            deDescricao.Focus();
        }

        private void miExcluir_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (creditoFixoDataSet.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não existe registro para ser excluído.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                DialogResult dlgResult = MessageBox.Show("Deseja realmente excluir a conta: " +
                    (String)creditoFixoDataSet.DataTable.Rows[grContaFixa.CurrentRowIndex].ItemArray[1] + "?",
                    "Confirmação", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dlgResult == DialogResult.Yes)
                {
                    creditoFixoDataSet.DelCreditoFixo(grContaFixa.CurrentRowIndex);
                }
            }
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if (deDescricao.Text == "")
            {
                MessageBox.Show("O campo Descrição é obrigatório.",
                    "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                deDescricao.Focus();
            }
            else
            {
                if (vlConta.Text == "")
                {
                    MessageBox.Show("O campo Valor é obrigatório.",
                        "Erro", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    vlConta.Focus();
                }
                else
                {
                    if (cbTipoMovim.SelectedIndex == -1)
                    {
                        MessageBox.Show("O campo Tipo de Conta é obrigatório.",
                            "Erro", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
                        cbTipoMovim.Focus();
                    }
                    else
                    {
                            String tipoMovim = creditoFixoDataSet.EncontraCdTipoMovim(cbTipoMovim.Text);

                            String y;
                            if (flForaDeUso.Checked)
                            {
                                y = "S";
                            }
                            else
                            {
                                y = "N";
                            }

                            if (novoRegistro)
                            {
                                creditoFixoDataSet.AddCreditoFixo(
                                   creditoFixoDataSet.GeraCodigo(),
                                   deDescricao.Text,
                                   tipoMovim,
                                   nuDiaVencimento.Value,
                                   vlConta.DecimalValue,
                                   y);
                            }
                            else
                            {
                                creditoFixoDataSet.AltCreditoFixo(
                                    grContaFixa.CurrentRowIndex,
                                    (String)creditoFixoDataSet.DataTable.Rows[grContaFixa.CurrentRowIndex].ItemArray[0],
                                    deDescricao.Text,
                                    tipoMovim,
                                    nuDiaVencimento.Value,
                                    vlConta.DecimalValue,
                                    y);
                            }

                            novoRegistro = false;
                            deDescricao.Text = "";
                            nuDiaVencimento.Value = 1;
                            vlConta.Text = "";
                            cbTipoMovim.SelectedIndex = -1;
                            flForaDeUso.Checked = false;

                            tcContaFixa.SelectedIndex = 0;
                    }
                }
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Deseja realmente cancelar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dlgResult == DialogResult.Yes)
            {
                novoRegistro = false;
                tcContaFixa.SelectedIndex = 0;
            }
        }

        private void CreditoFixo_Load(object sender, EventArgs e)
        {
            creditoFixoDataSet = new CreditoFixoDataSet(db);
            creditoFixoDataSet.SelectAll(false, true, "cf.decreditofixo", true);
            UpdateDataGrid();
            colIndex = 0;
            ordemCrescente = true;

            for (int i = 0; i < creditoFixoDataSet.TipoMovim.Tables[0].Rows.Count; i++)
            {
                cbTipoMovim.Items.Add(creditoFixoDataSet.TipoMovim.Tables[0].Rows[i].ItemArray[1]);
            }
        }

        private void grContaFixa_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 21)
            {
                if (e.X > 20 && e.X < 141)
                {
                    if (colIndex == 0) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 0; ordemCrescente = true; }
                }
                if (e.X > 141 && e.X < 184)
                {
                    if (colIndex == 1) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 1; ordemCrescente = true; }
                }
                if (e.X > 184 && e.X < 223)
                {
                    if (colIndex == 2) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 2; ordemCrescente = true; }
                }

                ordenaGrid(colIndex, ordemCrescente);
            }
        }

        private void UpdateDataGrid()
        {
            //  ler no datagrid
            DataGridTableStyle DGStyle = new DataGridTableStyle();

            // In this example the .NET DataGridTextBoxColumn class is used.
            DataGridTextBoxColumn textColumn;

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "decreditofixo";
            textColumn.HeaderText = "Descrição";
            textColumn.Width = (grContaFixa.Width - 36) * 60 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "nudiarecebimento";
            textColumn.HeaderText = "Dia venc.";
            textColumn.Width = (grContaFixa.Width - 36) * 20 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "flforauso";
            textColumn.HeaderText = "Fora de uso";
            textColumn.Width = (grContaFixa.Width - 36) * 20 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            DGStyle.MappingName = creditoFixoDataSet.DataTable.TableName;

            // The Clear() method is called to ensure that
            // the previous style is removed.
            grContaFixa.TableStyles.Clear();

            // Add the new DataGridTableStyle collection to
            // the TableStyles collection
            grContaFixa.TableStyles.Add(DGStyle);

            grContaFixa.DataSource = creditoFixoDataSet.DataTable;
        }

        private void tcContaFixa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!novoRegistro && tcContaFixa.SelectedIndex == 1)
            {
                miEditar_Click(sender, e);
            }
        }

        private void ordenaGrid(int coluna, Boolean ordem)
        {
            String sColuna = "";

            switch (coluna)
            {
                case 0:
                    sColuna = "cf.decreditofixo";
                    break;

                case 1:
                    sColuna = "cf.nudiarecebimento";
                    break;

                case 2:
                    sColuna = "cf.flforauso";
                    break;
                default:
                    sColuna = "cf.decreditofixo";
                    break;
            }

            creditoFixoDataSet.SelectAll(false, true, sColuna, ordem);
            UpdateDataGrid();
        }
    }
}