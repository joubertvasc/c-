using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
  public partial class Lancamento : Form
  {
    private LancamentoDataSet lancamentoDataSet;
    private ContaDataSet contaDataSet;
    private OrcamentoDataBase db;
    private Boolean novoRegistro;
    private int colIndex;
    private Boolean ordemCrescente;

    public Lancamento(OrcamentoDataBase orcamentoDataBase)
    {
      InitializeComponent();
      db = orcamentoDataBase;
    }

    private void miMenuPrincipal_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void Lancamento_Load(object sender, EventArgs e)
    {
      lancamentoDataSet = new LancamentoDataSet(db);
      contaDataSet = new ContaDataSet(db);

      for (int i = 0; i < lancamentoDataSet.TipoMovim.Tables[0].Rows.Count; i++) {
        cbTipoMovim.Items.Add(lancamentoDataSet.TipoMovim.Tables[0].Rows[i].ItemArray[1]);
      }

      for (int i = 0; i < lancamentoDataSet.CentroCusto.Tables[0].Rows.Count; i++) {
        cbCentroCusto.Items.Add(lancamentoDataSet.CentroCusto.Tables[0].Rows[i].ItemArray[1]);
      }

      for (int i = 0; i < lancamentoDataSet.Conta.Tables[0].Rows.Count; i++) {
        cbConta.Items.Add(lancamentoDataSet.Conta.Tables[0].Rows[i].ItemArray[1]);
      }

      colIndex = 0;
      ordemCrescente = true;

      ordenaGrid (colIndex, ordemCrescente);
    }

    private void UpdateDataGrid()
    {
      //  ler no datagrid
      DataGridTableStyle DGStyle = new DataGridTableStyle();

      // In this example the .NET DataGridTextBoxColumn class is used.
      DataGridTextBoxColumn textColumn;

      textColumn = new DataGridTextBoxColumn();
      textColumn.MappingName = "deconta";
      textColumn.HeaderText = "Conta";
      textColumn.Width = (grExtrato.Width - 36) * 40 / 100;
      DGStyle.GridColumnStyles.Add(textColumn);

      textColumn = new DataGridTextBoxColumn();
      textColumn.MappingName = "detipoconta";
      textColumn.HeaderText = "Tipo";
      textColumn.Width = (grExtrato.Width - 36) * 30 / 100;
      DGStyle.GridColumnStyles.Add(textColumn);

      textColumn = new DataGridTextBoxColumn();
      textColumn.MappingName = "vlsaldoformatado";
      textColumn.HeaderText = "Saldo";
      textColumn.Width = (grExtrato.Width - 36) * 30 / 100;
      DGStyle.GridColumnStyles.Add(textColumn);

      DGStyle.MappingName = contaDataSet.DataTable.TableName;

      // The Clear() method is called to ensure that
      // the previous style is removed.
      grExtrato.TableStyles.Clear();

      // Add the new DataGridTableStyle collection to
      // the TableStyles collection
      grExtrato.TableStyles.Add(DGStyle);

      grExtrato.DataSource = contaDataSet.DataTable;
    }

    private void grExtrato_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Y < 21)
      {
        if (e.X > 20 && e.X < 100)
        {
          if (colIndex == 0) { ordemCrescente = !ordemCrescente; }
          else { colIndex = 0; ordemCrescente = true; }
        }
        if (e.X > 101 && e.X < 165)
        {
          if (colIndex == 1) { ordemCrescente = !ordemCrescente; }
          else { colIndex = 1; ordemCrescente = true; }
        }
        if (e.X > 166 && e.X < 229)
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
          sColuna = "c.vlsaldo";
          break;

        default:
          sColuna = "c.deconta";
          break;
      }

      contaDataSet.SelectAll(false, true, sColuna, ordem);
      UpdateDataGrid();
    }

    private void btCancelar_Click(object sender, EventArgs e)
    {
      DialogResult dlgResult = MessageBox.Show("Deseja realmente cancelar?",
         "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

      if (dlgResult == DialogResult.Yes)
      {
        novoRegistro = false;
        tcLancamento.SelectedIndex = 0;
      }
    }

    private void miNovo_Click(object sender, EventArgs e)
    {
      novoRegistro = true;
      cbConta.SelectedIndex = -1;
      cbCentroCusto.SelectedIndex = -1;
      cbTipoMovim.SelectedIndex = -1;
      deDescricao.Text = "";
      vlLancamento.DecimalValue = 0;
      vlDesconto.DecimalValue = 0;
      vlJuros.DecimalValue = 0;
      dtLancamento.Value = System.DateTime.Today;
      dtQuitacao.Value = System.DateTime.Today;
      nuCheque.Text = "";
      tcLancamento.SelectedIndex = 1;

      cbConta.Focus();
    }

    private Boolean validaCampos() {
      Boolean result = true;
      if (cbConta.SelectedIndex == -1)
      {
        MessageBox.Show("O campo 'Conta' é obrigatório.",
                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        tcLancamento.SelectedIndex = 1;

        result = false;
        cbConta.Focus();
      }
      else if (cbCentroCusto.SelectedIndex == -1)
      {
        MessageBox.Show("O campo 'Centro de Custo' é obrigatório.",
                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        tcLancamento.SelectedIndex = 1;

        result = false;
        cbCentroCusto.Focus();
      }
      else if (cbTipoMovim.SelectedIndex == -1)
      {
        MessageBox.Show("O campo 'Movimentação' é obrigatório.",
                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        tcLancamento.SelectedIndex = 1;

        result = false;
        cbTipoMovim.Focus();
      }
      else if (deDescricao.Text == "")
      {
        MessageBox.Show("O campo 'Descrição' é obrigatório.",
                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        tcLancamento.SelectedIndex = 1;

        result = false;
        deDescricao.Focus();
      }
      else if (vlLancamento.DecimalValue == 0)
      {
        MessageBox.Show("O campo 'Valor' tem que ser maior que zero.",
                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        tcLancamento.SelectedIndex = 1;

        result = false;
        vlLancamento.Focus();
      }

      return result;
    }

    private void btSalvar_Click(object sender, EventArgs e)
    {
      if (validaCampos()) {
        if (novoRegistro) {
          lancamentoDataSet.AddLancamento (lancamentoDataSet.GeraCodigo(), deDescricao.Text, 
            dtLancamento.Value, lancamentoDataSet.EncontraCdTipoMovim(cbTipoMovim.Text),
            lancamentoDataSet.EncontraCdCentroCusto (cbCentroCusto.Text), vlLancamento.DecimalValue,
            lancamentoDataSet.EncontraCdConta(cbConta.Text), dtQuitacao.Value, "", "", "", 0, 
            vlJuros.DecimalValue, vlDesconto.DecimalValue, nuCheque.Text);
        } else {
          // TODO
        }
    
        novoRegistro = false;
        tcLancamento.SelectedIndex = 0;
        ordenaGrid (colIndex, ordemCrescente);
      }
    }

    private void miConsulta_Click(object sender, EventArgs e)
    {
      this.Refresh();
      Form Frm_Cad = new Consulta(db);
      Frm_Cad.Show();
    }
  }
}