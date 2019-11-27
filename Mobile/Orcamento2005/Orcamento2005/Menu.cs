using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    public partial class FormMenu : Form
    {
        private ControleMensalDataSet controleMensalDataSet;
        private OrcamentoDataBase db;
        public int versao = 1000; // 1.0.0-0

        public FormMenu()
        {
            InitializeComponent();
        }

        private Boolean AbreBancoDados()
        {
            db = new OrcamentoDataBase(versao);
            if (!db.VerificaBancoDeDados())
            {
                MessageBox.Show("Não foi possível abrir ou criar o banco de dados. Chame o suporte.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void FechaBancoDados()
        {
            db.FechaBancoDados();
        }

        private void FechaSistema()
        {
            FechaBancoDados();
            Application.Exit();
        }

        private string PreencheEspacos(string original, int tamanho, Boolean esquerda)
        {
            string x = original;

            for (int i = original.Length; i < tamanho; i++)
            {
                if (esquerda)
                {
                    x = " " + x;
                }
                else
                {
                    x += " ";
                }
            }

            return x;
        }

        private string ValorFormatado(decimal valor)
        {
            if (valor == 0)
            {
                return "0,00";
            }
            else
            {
                return valor.ToString("###,000.00");
            }
        }

        private void MontaResumo()
        {
            lbResumo.Items.Clear();
            lbResumo.Items.Add("       Resumo do Orçamento");
            lbResumo.Items.Add("");
            lbResumo.Items.Add("Conta                |      Saldo");

            DataSet dsConta;
            DataSet dsMovim;
            decimal saldo = 0;
            string conta = "";
            string saldox = "";
            int i;
            int j;

            // Exibe os saldos das contas e aplicações
            db.SQLServerCEDataBase.OpenSQL(
                "select deconta, vlsaldoinicial, cdTipoConta " +
                "  from conta " +
                " where floperacao <> 'D'" +
                " order by deconta"
                , out dsConta);

            for (i = 0; i < dsConta.Tables[0].Rows.Count; i++)
            {   // Qualquer coisa diferente de cartões!
                if ((String)dsConta.Tables[0].Rows[i].ItemArray[2] != "B")
                {
                    conta = (String)dsConta.Tables[0].Rows[i].ItemArray[0];
                    saldo = System.Convert.ToDecimal(dsConta.Tables[0].Rows[i].ItemArray[1]);

                    db.SQLServerCEDataBase.OpenSQL(
                        "select l.vllancamento, tm.flnatureza " +
                        "  from tipomovim tm, lancamento l" +
                        " where l.cdtipomovim = tm.cdtipomovim" +
                        "   and l.dtquitacao is not null" +
                        "   and l.dtquitacao <= getdate()" +
                        "   and l.cdconta = '" + conta + "'" +
                        " order by l.cdconta", out dsMovim);

                    for (j = 0; j < dsMovim.Tables[0].Rows.Count; j++)
                    {
                        if ((String)dsMovim.Tables[0].Rows[j].ItemArray[2] == "C")
                        {
                            saldo = saldo + System.Convert.ToDecimal(dsMovim.Tables[0].Rows[j].ItemArray[1]);
                        }
                        else
                        {
                            saldo = saldo - System.Convert.ToDecimal(dsMovim.Tables[0].Rows[j].ItemArray[1]);
                        }
                    }

                    conta = PreencheEspacos(conta, 20, false);

                    if (saldo == 0)
                    {
                        saldox = "      0,00";
                    }
                    else
                    {
                        saldox = PreencheEspacos(ValorFormatado(saldo), 10, true);
                    }

                    lbResumo.Items.Add(conta + " | " + saldox);
                }
            }

            // Exibe os saldos dos cartões de crédito
            lbResumo.Items.Add("");
            lbResumo.Items.Add("Cartões              |  A Faturar");

            for (i = 0; i < dsConta.Tables[0].Rows.Count; i++)
            {   // Somente cartões de crédito!
                if ((String)dsConta.Tables[0].Rows[i].ItemArray[2] == "B")
                {
                    conta = (String)dsConta.Tables[0].Rows[i].ItemArray[0];
                    saldo = 0;

                    db.SQLServerCEDataBase.OpenSQL(
                        "select l.vllancamento, tm.flnatureza " +
                        "  from tipomovim tm, lancamento l" +
                        " where l.cdtipomovim = tm.cdtipomovim" +
                        "   and l.dtquitacao is null" +
                        "   and l.cdconta = '" + conta + "'" +
                        " order by l.cdconta", out dsMovim);

                    for (j = 0; j < dsMovim.Tables[0].Rows.Count; j++)
                    {
                        if ((String)dsMovim.Tables[0].Rows[j].ItemArray[2] == "C")
                        {
                            saldo = saldo + System.Convert.ToDecimal(dsMovim.Tables[0].Rows[j].ItemArray[1]);
                        }
                        else
                        {
                            saldo = saldo - System.Convert.ToDecimal(dsMovim.Tables[0].Rows[j].ItemArray[1]);
                        }
                    }

                    conta = PreencheEspacos(conta, 20, false);

                    if (saldo == 0)
                    {
                        saldox = "      0,00";
                    }
                    else
                    {
                        saldox = PreencheEspacos(ValorFormatado(saldo), 10, true);
                    }

                    lbResumo.Items.Add(conta + " | " + saldox);
                }
            }

            // Exibe o resumo dos próximos lançamentos
            db.SQLServerCEDataBase.OpenSQL(
                "select sum (l.vllancamento) as vltotal, tm.flnatureza " +
                "  from tipomovim tm, lancamento l" +
                " where l.cdtipomovim = tm.cdtipomovim" +
                "   and l.dtquitacao is null" +
                "   and l.dtlancamento is not null" +
                "   and l.dtlancamento >= getdate() " +
                "   and l.dtlancamento <= getdate()+5 " +
                " group by tm.flnatureza", out dsMovim);

            lbResumo.Items.Add("");
            lbResumo.Items.Add("Próximos Lançamentos (5 dias)");
            //			lbDetalhes.Items.Clear();
            //			lbDetalhes.Items.Add ("   Detalhamento dos Próximos");
            //			lbDetalhes.Items.Add ("          Lançamentos");
            //			lbDetalhes.Items.Add ("");

            decimal dCredito = 0;
            decimal dDebito = 0;

            if (dsMovim.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < dsMovim.Tables[0].Rows.Count; i++)
                {
                    if ((String)dsMovim.Tables[0].Rows[i].ItemArray[1] == "C")
                    {
                        dCredito = dCredito + System.Convert.ToDecimal(dsMovim.Tables[0].Rows[i].ItemArray[0]);
                    }
                    else
                    {
                        dDebito = dDebito + System.Convert.ToDecimal(dsMovim.Tables[0].Rows[i].ItemArray[0]);
                    }
                }
            }

            lbResumo.Items.Add("Crédito | " + PreencheEspacos(ValorFormatado(dCredito), 10, true));
            lbResumo.Items.Add("Débito  | " + PreencheEspacos(ValorFormatado(dDebito), 10, true));

            // Exibe o total de cheques a cair nos próximos dias
            db.SQLServerCEDataBase.OpenSQL(
                "select l.vllancamento " +
                "  from tipomovim tm, lancamento l" +
                " where l.cdtipomovim = tm.cdtipomovim" +
                "   and l.dtquitacao is null" +
                "   and l.dtlancamento is not null" +
                "   and l.dtlancamento >= getdate() " +
                "   and l.dtlancamento <= getdate()+5 " +
                "   and l.nucheque is not null "
                , out dsMovim);

            int nCheques = dsMovim.Tables[0].Rows.Count;
            saldo = 0;
            for (i = 0; i < nCheques; i++)
            {
                saldo = saldo + System.Convert.ToDecimal(dsMovim.Tables[0].Rows[i].ItemArray[0]);
            }

            lbResumo.Items.Add("Cheques | " + PreencheEspacos(ValorFormatado(saldo), 10, true) +
                                " (" + nCheques.ToString() + " cheques)");

            dsConta = null;
            dsMovim = null;
        }

        private void miSair_Click(object sender, EventArgs e)
        {
            FechaSistema();
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            FechaBancoDados();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            if (AbreBancoDados()) {
                MontaResumo();

                verificaSePrecisaLancarDadosMensais();
            } else {
                FechaSistema(); 
            }
        }

        private void miContas_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Form Frm_Cad = new Contas(db);
            Frm_Cad.Show();
        }

        private void miCentroCusto_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Form Frm_Cad = new CentroCusto(db);
            Frm_Cad.Show();
        }

        private void miTipoMovim_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Form Frm_Cad = new TipoMovim(db);
            Frm_Cad.Show();
        }

        private void miContasFixas_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Form Frm_Cad = new ContaFixa(db);
            Frm_Cad.Show();
        }

        private void miPrestacoes_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Form Frm_Cad = new Prestacao(db);
            Frm_Cad.Show();
        }

        private void miCreditosFixos_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Form Frm_Cad = new CreditoFixo(db);
            Frm_Cad.Show();
        }

        private void verificaSePrecisaLancarDadosMensais() {
            controleMensalDataSet = new ControleMensalDataSet(db);

            if (!controleMensalDataSet.JaFoiRealizadoLancamento (
                  System.DateTime.Today.Month,
                  System.DateTime.Today.Year)) {
                DialogResult dlgResult = MessageBox.Show(
                    "Deseja gerar os lançamentos de débitos e crédito fixos para " +
                    System.DateTime.Today.Month.ToString() + " de " + 
                    System.DateTime.Today.Year.ToString() + "?",
                    "Confirmação", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlgResult == DialogResult.Yes) {
                    controleMensalDataSet.RealizaLancamentos (
                        System.DateTime.Today.Month,
                        System.DateTime.Today.Year);
                }
            }
        }

        private void miCalculoJuros_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Form Frm_Cad = new CalculoJuros();
            Frm_Cad.Show();
        }

      private void miLancamentos_Click(object sender, EventArgs e)
      {
        this.Refresh();
        Form Frm_Cad = new Lancamento(db);
        Frm_Cad.Show();
      }

      private void miConsulta_Click(object sender, EventArgs e)
      {
        this.Refresh();
        Form Frm_Cad = new Consulta(db);
        Frm_Cad.Show();
      }
    }
}