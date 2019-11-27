using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    public partial class Contas : Form
    {
        private ContaDataSet contaDataSet;
        private Boolean novoRegistro;
        private int colIndex;
        private Boolean ordemCrescente;
    		private OrcamentoDataBase db;

        public Contas(OrcamentoDataBase orcamentoDataBase)
        {
            InitializeComponent();
            db = orcamentoDataBase;
        }

        private void miMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Contas_Load(object sender, EventArgs e)
        {
            contaDataSet = new ContaDataSet(db);
            contaDataSet.SelectAll(false, true, "c.deconta", true);
            UpdateDataGrid();
            colIndex = 0;
            ordemCrescente = true;

            for (int i = 0; i < contaDataSet.TiposConta.Tables[0].Rows.Count; i++) {
                cbTipoConta.Items.Add(contaDataSet.TiposConta.Tables[0].Rows[i].ItemArray[1]);
            }
        }

        private void UpdateDataGrid()
        {
            //  ler no datagrid
            DataGridTableStyle DGStyle = new DataGridTableStyle();

            // In this example the .NET DataGridTextBoxColumn class is used.
            DataGridTextBoxColumn textColumn;

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "deconta";
            textColumn.HeaderText = "Descrição";
            textColumn.Width = (grContas.Width - 36) * 60 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "detipoconta";
            textColumn.HeaderText = "Tipo";
            textColumn.Width = (grContas.Width - 36) * 20 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "flforauso";
            textColumn.HeaderText = "Fora de uso";
            textColumn.Width = (grContas.Width - 36) * 20 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            DGStyle.MappingName = contaDataSet.DataTable.TableName;

            // The Clear() method is called to ensure that
            // the previous style is removed.
            grContas.TableStyles.Clear();

            // Add the new DataGridTableStyle collection to
            // the TableStyles collection
            grContas.TableStyles.Add(DGStyle);

            grContas.DataSource = contaDataSet.DataTable;
        }

        private void miInserir_Click(object sender, EventArgs e)
        {
            novoRegistro = true;
            tcContas.SelectedIndex = 1;
            nuSaldoInicial.Enabled = true;
            nuDiaBom.Enabled = false;
            nuVencimento.Enabled = false;
            lblLimite.Text = "Limite:";
            lblSaldoInicial.Text = "Saldo inicial:";
            tbDescricao.Focus();
        }

        private void miEditar_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (contaDataSet.DataTable.Rows.Count == 0) {
                MessageBox.Show("Não existe registro para ser editado.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 
                    MessageBoxDefaultButton.Button1);
                tcContas.SelectedIndex = 0;
            } else {
                lblLimite.Text = "Limite:";
                lblSaldoInicial.Text = "Saldo inicial:";
                tbDescricao.Text = (String)contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[1];
              nuLimite.Text = System.Convert.ToString(contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[2]);
              nuSaldoInicial.Text = System.Convert.ToString(contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[3]);
              nuSaldoInicial.Enabled = false;
              String tipoConta = (String)contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[4];
              cbTipoConta.SelectedIndex = cbTipoConta.Items.IndexOf(contaDataSet.EncontraDeTipoConta(tipoConta));
              nuDiaBom.Value = System.Convert.ToDecimal(contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[9]);
              nuVencimento.Value = System.Convert.ToDecimal(contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[10]);

              cbForaUso.Checked = ((String)contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[5] == "S");

              if (tcContas.SelectedIndex != 1) {
                  tcContas.SelectedIndex = 1;
              }

              tbDescricao.Focus();
            }
        }

        private void miExcluir_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (contaDataSet.DataTable.Rows.Count == 0) {
                MessageBox.Show("Não existe registro para ser excluído.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
            } else {
                DialogResult dlgResult = MessageBox.Show("Deseja realmente excluir a conta: " +
                    (String)contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[1] + "?",
                    "Confirmação", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dlgResult == DialogResult.Yes) {
                    contaDataSet.DelConta(grContas.CurrentRowIndex);
                }
            }
        }

        private void btSalvar_Click_1(object sender, EventArgs e)
        {
            if (tbDescricao.Text == "") {
                MessageBox.Show("O campo Descrição é obrigatório.",
                    "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tbDescricao.Focus();
            } else {
                if (cbTipoConta.SelectedIndex == -1) {
                    MessageBox.Show("O campo Tipo de Conta é obrigatório.",
                        "Erro", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    cbTipoConta.Focus();
                } else {
                    String tipoConta = contaDataSet.EncontraCdTipoConta(cbTipoConta.Text);

                    String y;
                    if (cbForaUso.Checked) {
                        y = "S";
                    } else {
                        y = "N";
                    }

                    if (novoRegistro) {
                        contaDataSet.AddConta(contaDataSet.GeraCodigo(), tbDescricao.Text,
                            nuLimite.DecimalValue, nuSaldoInicial.DecimalValue, tipoConta, y, 
                            (int)nuDiaBom.Value, (int) nuVencimento.Value);
                    } else {
                        contaDataSet.AltConta(grContas.CurrentRowIndex,
                            (String)contaDataSet.DataTable.Rows[grContas.CurrentRowIndex].ItemArray[0],
                            tbDescricao.Text, nuLimite.DecimalValue, nuSaldoInicial.DecimalValue, tipoConta, y, 
                            (int)nuDiaBom.Value, (int)nuVencimento.Value);
                    }

                    novoRegistro = false;
                    tbDescricao.Text = "";
                    nuLimite.Text = "0";
                    nuSaldoInicial.Text = "0";
                    cbTipoConta.SelectedIndex = -1;
                    cbForaUso.Checked = false;

                    tcContas.SelectedIndex = 0;
                }
            }
        }

        private void btCancelar_Click_1(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Deseja realmente cancelar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dlgResult == DialogResult.Yes)
            {
                novoRegistro = false;
                tcContas.SelectedIndex = 0;
            }
        }

        private void grContas_MouseDown(object sender, MouseEventArgs e)
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
                    sColuna = "c.deconta";
                    break;

                case 1:
                    sColuna = "tc.detipoconta";
                    break;

                case 2:
                    sColuna = "c.flforauso";
                    break;

                default:
                    sColuna = "c.deconta";
                    break;
            }

            contaDataSet.SelectAll(false, true, sColuna, ordem);
            UpdateDataGrid();
        }

        private void tbDescricao_GotFocus_1(object sender, EventArgs e)
        {
            ipContas.Enabled = true;
        }

        private void tbDescricao_LostFocus_1(object sender, EventArgs e)
        {
            ipContas.Enabled = false;
        }

        private void nuLimite_GotFocus(object sender, EventArgs e)
        {
            ipContas.Enabled = true;
        }

        private void nuLimite_LostFocus(object sender, EventArgs e)
        {
            ipContas.Enabled = false;
        }

        private void nuSaldoInicial_GotFocus(object sender, EventArgs e)
        {
            ipContas.Enabled = true;
        }

        private void nuSaldoInicial_LostFocus(object sender, EventArgs e)
        {
            ipContas.Enabled = false;
        }

        private void tcContas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!novoRegistro && tcContas.SelectedIndex == 1)
            {
                miEditar_Click(sender, e);
            }
        }

        private void cbForaUso_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void cbTipoConta_SelectedIndexChanged(object sender, EventArgs e) {
            nuDiaBom.Enabled = contaDataSet.EncontraCdTipoConta(cbTipoConta.Text) == "B";
            nuVencimento.Enabled = nuDiaBom.Enabled;

            if (nuDiaBom.Enabled) {
                lblLimite.Text = "Crédito";
                lblSaldoInicial.Text = "Crédito usado:";
            } else {
                lblLimite.Text = "Limite:";
                lblSaldoInicial.Text = "Saldo inicial:";
            }
        }
    }
}