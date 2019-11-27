using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Orcamento2005
{
  public partial class Consulta : Form
  {
    private LancamentoDataSet lancamentoDataSet;
    private OrcamentoDataBase db;

    public Consulta(OrcamentoDataBase orcamentoDataBase)
    {
      InitializeComponent();
      db = orcamentoDataBase;
    }

    private void miMenuPrincipal_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private bool validaCampos()
    {
      bool result = true;

      if (cbConta.SelectedIndex == -1) {
        MessageBox.Show("O campo 'Conta' é obrigatório.",
                 "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        cbConta.Focus();
        result = false;
      }

      return result;
    }

    private void btConsultar_Click(object sender, EventArgs e)
    {
      if (validaCampos()) {
        String agrupamento = "";
        
        if (rbSemAgrupamento.Checked) { agrupamento = "N"; }
        else if (rbCentroCusto.Checked) { agrupamento = "C"; }
        else { agrupamento = "M"; }

        DataSet ds = lancamentoDataSet.Consulta(
          lancamentoDataSet.EncontraCdConta(cbConta.Text),
          lancamentoDataSet.EncontraCdCentroCusto (cbCentroCusto.Text),
          lancamentoDataSet.EncontraCdTipoMovim (cbTipoMovim.Text),
          deDescricao.Text,
          nuCheque.Text,
          dtInicial.Value,
          dtFinal.Value,
          dtInicioQuit.Value,
          dtFinalQuit.Value,
          vlInicial.DecimalValue,
          vlFinal.DecimalValue,
          cbSomenteOsComJuros.Checked,
          cbSomenteOsComDesconto.Checked,
          agrupamento,
          cbOrdemDecrescente.Checked);

        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
          MessageBox.Show("Nenhum lançamento foi encontrado com os parâmetros informados.",
                   "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        } else {
          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
          {

            // TODO apresentação dos resultados.
          }
        }

        ds.Dispose();
      }
    }

    private void Consulta_Load(object sender, EventArgs e)
    {
      lancamentoDataSet = new LancamentoDataSet(db);

      for (int i = 0; i < lancamentoDataSet.TipoMovim.Tables[0].Rows.Count; i++)
      {
        cbTipoMovim.Items.Add(lancamentoDataSet.TipoMovim.Tables[0].Rows[i].ItemArray[1]);
      }

      for (int i = 0; i < lancamentoDataSet.CentroCusto.Tables[0].Rows.Count; i++)
      {
        cbCentroCusto.Items.Add(lancamentoDataSet.CentroCusto.Tables[0].Rows[i].ItemArray[1]);
      }

      for (int i = 0; i < lancamentoDataSet.Conta.Tables[0].Rows.Count; i++)
      {
        cbConta.Items.Add(lancamentoDataSet.Conta.Tables[0].Rows[i].ItemArray[1]);
      }
    }
  }
}