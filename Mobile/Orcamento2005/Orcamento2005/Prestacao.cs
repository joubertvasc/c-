using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    public partial class Prestacao : Form
    {
        private PrestacaoDataSet prestacaoDataSet;
        private LancamentoDataSet lancamentoDataSet;
        private Boolean novoRegistro;
        private int colIndex;
        private Boolean ordemCrescente;
        private OrcamentoDataBase db;
        private int painelAtivo = 0;
        private int paineisCriados = 0;
        private Decimal parcelasPagas = 0;
        private PrestacoesCollection prestacoesCollection;
        private Boolean tabAutomatico = false;
        private int lastTab = 0;
        private Decimal vlOldParcela = 0;
        private Decimal vlOldPrestacao = 0;

        public Prestacao(OrcamentoDataBase orcamentoDataBase)
        {
            InitializeComponent();
            db = orcamentoDataBase;

            prestacoesCollection = new PrestacoesCollection();
        }

        private void Prestacao_Load(object sender, EventArgs e)
        {
            prestacaoDataSet = new PrestacaoDataSet(db);
            prestacaoDataSet.SelectAll(false, true, "p.deprestacao", true, true);
            lancamentoDataSet = new LancamentoDataSet(db);

            UpdateDataGrid();
            colIndex = 0;
            ordemCrescente = true;

            for (int i = 0; i < prestacaoDataSet.TipoMovim.Tables[0].Rows.Count; i++) {
                cbTipoMovim.Items.Add(prestacaoDataSet.TipoMovim.Tables[0].Rows[i].ItemArray[1]);
            }

            for (int i = 0; i < prestacaoDataSet.CentroCusto.Tables[0].Rows.Count; i++) {
                cbCentroCusto.Items.Add(prestacaoDataSet.CentroCusto.Tables[0].Rows[i].ItemArray[1]);
            }
        }

        private void UpdateDataGrid()
        {
            //  ler no datagrid
            DataGridTableStyle DGStyle = new DataGridTableStyle();

            // In this example the .NET DataGridTextBoxColumn class is used.
            DataGridTextBoxColumn textColumn;

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "deprestacao";
            textColumn.HeaderText = "Descrição";
            textColumn.Width = (grPrestacao.Width - 36) * 70 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "nudiavenc";
            textColumn.HeaderText = "Dia";
            textColumn.Width = (grPrestacao.Width - 36) * 15 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "faltantes";
            textColumn.HeaderText = "Parc.";
            textColumn.Width = (grPrestacao.Width - 36) * 15 / 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            DGStyle.MappingName = prestacaoDataSet.DataTable.TableName;

            // The Clear() method is called to ensure that
            // the previous style is removed.
            grPrestacao.TableStyles.Clear();

            // Add the new DataGridTableStyle collection to
            // the TableStyles collection
            grPrestacao.TableStyles.Add(DGStyle);

            grPrestacao.DataSource = prestacaoDataSet.DataTable;
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miExcluir_Click(object sender, EventArgs e)
        {
            if (prestacaoDataSet.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não existe registro para ser editado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
                tabAutomatico = true;
                lastTab = 0;
                tcPrestacao.SelectedIndex = lastTab;
                tabAutomatico = false;
            }
            else
            {
                DialogResult dlgResult;
                Decimal parcelasPagas =
                    lancamentoDataSet.QuantidadePrestacoesPagas(
                      (String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[0]);

                if (parcelasPagas == 1)
                {
                    dlgResult = MessageBox.Show("Existem uma parcela paga. " +
                        "Deseja realmente excluir as parcelas restantes da prestação: " +
                        (String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[1] + "?",
                        "Confirmação", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
                }
                else if (parcelasPagas > 0)
                {
                    dlgResult = MessageBox.Show("Existem " + parcelasPagas.ToString() + " parcelas pagas. " +
                        "Deseja realmente excluir as parcelas restantes da prestação: " +
                        (String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[1] + "?",
                        "Confirmação", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
                }
                else
                {
                    dlgResult = MessageBox.Show("Deseja realmente excluir a prestação: " +
                        (String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[1] + "?",
                        "Confirmação", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);
                }

                if (dlgResult == DialogResult.Yes)
                {
                    lancamentoDataSet.ExcluirPrestacoesNaoPagas((String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[0]);
                    prestacaoDataSet.DelPrestacao(grPrestacao.CurrentRowIndex);
                }
            }
        }
        
        private void miEditar_Click(object sender, EventArgs e)
        {
            novoRegistro = false;
            if (prestacaoDataSet.DataTable.Rows.Count == 0)
            {
                MessageBox.Show("Não existe registro para ser editado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
                tabAutomatico = true;
                lastTab = 0;
                tcPrestacao.SelectedIndex = lastTab;
                tabAutomatico = false;
            } else {
                lancamentoDataSet.SelectPrestacao((String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[0]);

                edDescricao.Text = (String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[1];
                nuDiaVenc.Value = System.Convert.ToDecimal (prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[8]);
                nuParcelas.Value = System.Convert.ToDecimal (prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[4]);
                vlPrestacaoTotal.Text = System.Convert.ToString(prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[5]);
                vlParcela.Text = System.Convert.ToString(prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[9]);
                dtAquisicao.Value = System.Convert.ToDateTime(prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[6]);
                dtPrimeira.Value = System.Convert.ToDateTime (prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[7]);
                cbCentroCusto.SelectedIndex = cbCentroCusto.Items.IndexOf(prestacaoDataSet.EncontraDeCentroCusto((String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[3]));
                cbTipoMovim.SelectedIndex = cbTipoMovim.Items.IndexOf(prestacaoDataSet.EncontraDeTipoMovim((String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[2]));

                if (tcPrestacao.SelectedIndex != 1) {
                    tabAutomatico = true;
                    lastTab = 1;
                    tcPrestacao.SelectedIndex = lastTab;
                    tabAutomatico = false;
                }

                parcelasPagas = lancamentoDataSet.QuantidadePrestacoesPagas((String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[0]);
                nuParcelas.Minimum = parcelasPagas + 1;

                if (nuParcelas.Minimum < 2) {
                    nuParcelas.Minimum = 2;
                }

                edDescricao.Focus();
            }
        }

        private void miInserir_Click(object sender, EventArgs e)
        {
            novoRegistro = true;
            dtAquisicao.Value = System.DateTime.Today;
            tabAutomatico = true;
            lastTab = 1;
            tcPrestacao.SelectedIndex = lastTab;
            tabAutomatico = false;
            nuDiaVenc.Value = System.Convert.ToDecimal(dtAquisicao.Value.ToString("dd"));

            lancamentoDataSet.SelectPrestacao("-1"); // Abrir vazio

            calculaPrimeira();

            parcelasPagas = 0;
            edDescricao.Text = "";
            vlPrestacaoTotal.Text = "";
            vlParcela.Text = "";
            nuParcelas.Value = 2;
            nuParcelas.Minimum = 2;
            cbCentroCusto.SelectedIndex = -1;
            cbTipoMovim.SelectedIndex   = -1;

            edDescricao.Focus();
        }

        private Boolean validaCampos()
        {
            if (edDescricao.Text == "")
            {
                MessageBox.Show("O campo Descrição é obrigatório.",
                    "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                edDescricao.Focus();
                return false;
            } else if (vlPrestacaoTotal.Text == "")
            {
                MessageBox.Show("O campo Prestação é obrigatório.",
                    "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                vlPrestacaoTotal.Focus();
                return false;
            } else if (cbTipoMovim.SelectedIndex == -1)
            {
                MessageBox.Show("O campo Tipo de Movimentação é obrigatório.",
                     "Erro", MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation,
                     MessageBoxDefaultButton.Button1);
                cbTipoMovim.Focus();
                return false;
            } else if (cbCentroCusto.SelectedIndex == -1)
            {
                MessageBox.Show("O campo Centro de Custo é obrigatório.",
                   "Erro", MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
                cbCentroCusto.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void criaPaineis()
        {
            painelAtivo = 0;
            
            decimal parcela = 1;
            DataRow[] foundRows;

            String dtAq = "";
            DateTime data = dtPrimeira.Value;
            
            int paineis = (int)nuParcelas.Value / 8;
 
            decimal vlParcela = 0;
            decimal vlTotal = 0;

            if (this.vlParcela.DecimalValue != 0) {
                vlParcela = this.vlParcela.DecimalValue;
            } else {
                vlParcela = vlPrestacaoTotal.DecimalValue / nuParcelas.Value;
            }

            if (nuParcelas.Value % 8 != 0) { 
                paineis++; 
            };

            System.Windows.Forms.Panel pnl = null;
            System.Windows.Forms.Panel pnlParcela = null;
            System.Windows.Forms.TextBox tbNo = null;
            System.Windows.Forms.DateTimePicker dtData = null;
            NumericTextBox.NumericTextBox nuValor = null;
            System.Windows.Forms.TextBox nuCheque = null;

            pnlClient.SuspendLayout();
            pnlClient.Controls.Clear();
            prestacoesCollection.Clear();
            PrestacaoCollection prestacaoCollection;

            for (int i = 0; i < paineis; i++)
            {
                pnl = new System.Windows.Forms.Panel();
                pnl.Visible = false;
                pnl.Dock = System.Windows.Forms.DockStyle.Fill;
                pnl.SuspendLayout();
                pnlClient.Controls.Add(pnl);
                pnlClient.Controls.SetChildIndex(pnl, 0);

                for (int j = 0; j < 8; j++)
                {
                    pnlParcela = new System.Windows.Forms.Panel();
                    pnlParcela.SuspendLayout();
                    pnl.Controls.Add(pnlParcela);
                    pnl.Controls.SetChildIndex(pnlParcela, 0);

                    tbNo = new System.Windows.Forms.TextBox();
                    tbNo.SuspendLayout();

                    dtData = new System.Windows.Forms.DateTimePicker();
                    dtData.SuspendLayout();

                    nuValor = new NumericTextBox.NumericTextBox();
                    nuValor.SuspendLayout();

                    nuCheque = new System.Windows.Forms.TextBox();
                    nuCheque.SuspendLayout();

                    pnlParcela.Controls.Add(nuCheque);
                    pnlParcela.Controls.Add(nuValor);
                    pnlParcela.Controls.Add(dtData);
                    pnlParcela.Controls.Add(tbNo);
                    pnlParcela.Dock = System.Windows.Forms.DockStyle.Top;
                    pnlParcela.Size = new System.Drawing.Size(240, 22);
                    pnlParcela.Tag = parcela;

                    foundRows = 
                      lancamentoDataSet.DataSet.Tables[0].Select("nuparcela=" + parcela.ToString());

                    tbNo.Text = System.Convert.ToString(parcela);
                    tbNo.Dock = System.Windows.Forms.DockStyle.Left;
                    tbNo.Size = new System.Drawing.Size(30, 22);
                    tbNo.Enabled = false;
                    tbNo.Tag = parcela;

                    dtData.CustomFormat = "dd/MM/yy";
                    dtData.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                    dtData.Dock = System.Windows.Forms.DockStyle.Left;
                    dtData.Size = new System.Drawing.Size(76, 22);
                    dtData.Value = data;

                    if (!novoRegistro && foundRows.Length > 0) {
                        dtData.Value = (DateTime)foundRows[0]["dtlancamento"];

                        if (System.Convert.ToString(foundRows[0]["dtquitacao"]) != "") {
                            dtData.Enabled = false;
                        }
                    }

                    dtData.Tag = parcela;
                    dtData.ValueChanged += new System.EventHandler(dtData_ValueChanged);

                    nuValor.Dock = System.Windows.Forms.DockStyle.Left;
                    nuValor.Size = new System.Drawing.Size(78, 22);
                    nuValor.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
                    nuValor.Text = vlParcela.ToString("#0.00");

                    if (!novoRegistro && foundRows.Length > 0) {
                        nuValor.Text = System.Convert.ToString (foundRows[0]["vllancamento"]);

                        if (System.Convert.ToString(foundRows[0]["dtquitacao"]) != "") {
                            nuValor.Enabled = false;
                        }
                    }

                    nuValor.Tag = parcela;
                    nuValor.Validated += new System.EventHandler(nuValor_Validated);
                    nuValor.GotFocus  += new System.EventHandler(this.edDescricao_GotFocus);
                    nuValor.LostFocus += new System.EventHandler(this.edDescricao_LostFocus);

                    vlTotal = vlTotal + nuValor.DecimalValue;

                    nuCheque.Text = "";
                    if (!novoRegistro && foundRows.Length > 0) {
                        nuCheque.Text = (String)foundRows[0]["nucheque"];

                        if (System.Convert.ToString(foundRows[0]["dtquitacao"]) != "") {
                            nuCheque.Enabled = false;
                        }
                    }

                    nuCheque.Dock = System.Windows.Forms.DockStyle.Fill;
                    nuCheque.Size = new System.Drawing.Size(58, 22);
                    nuCheque.Enabled = true;
                    nuCheque.Tag = parcela;
                    nuCheque.GotFocus += new System.EventHandler(this.edDescricao_GotFocus);
                    nuCheque.LostFocus += new System.EventHandler(this.edDescricao_LostFocus);

                    tbNo.ResumeLayout(false);
                    dtData.ResumeLayout(false);
                    nuValor.ResumeLayout(false);
                    nuCheque.ResumeLayout(false);
                    pnlParcela.ResumeLayout(false);

                    data = data.AddMonths(1);
                    dtAq = nuDiaVenc.Value.ToString("00") + data.ToString("/MM/yyyy");
                    data = dataAnteriorValida (dtAq);

                    prestacaoCollection = new PrestacaoCollection();
                    prestacaoCollection.Prestacao = parcela;
                    prestacaoCollection.PanelVisao = pnl;
                    prestacaoCollection.PanelParcela = pnlParcela;
                    prestacaoCollection.NoParcela = tbNo;
                    prestacaoCollection.DataParcela = dtData;
                    prestacaoCollection.ValorParcela = nuValor;
                    prestacaoCollection.Cheque = nuCheque;

                    if (!novoRegistro && foundRows.Length > 0) {
                        prestacaoCollection.CDLancamento =
                            (String)foundRows[0]["cdlancamento"];
                    }

                    prestacoesCollection.Add(prestacaoCollection);
                    
                    parcela++;

                    if (parcela > nuParcelas.Value)
                    {
                        if (vlTotal != vlPrestacaoTotal.DecimalValue)
                        {
                            nuValor.Text = System.Convert.ToString (nuValor.DecimalValue + (vlPrestacaoTotal.DecimalValue - vlTotal));
                        }

                        break;
                    }
                }

                pnl.ResumeLayout(false);
            }

            pnlClient.ResumeLayout(false);

            paineisCriados = paineis;

            controlaPainel (1);
        }

        private DateTime dataAnteriorValida(String dtAq)
        {
            Decimal dia = System.Convert.ToDecimal(dtAq.Substring(0, 2));
            Decimal mes = System.Convert.ToDecimal(dtAq.Substring(3, 2));
            Decimal ano = System.Convert.ToDecimal(dtAq.Substring(6, 4));

            Boolean ok = false;

            while (!ok) 
            {
                if (dia < 29) {
                    // Dia 28 pra baixo, ok
                    ok = true;
                } else if (dia == 30 && mes != 2) {
                    // Dia 30 e não é fevereiro, ok
                    ok = true;
                } else if (dia == 29) {
                    // Se o dia for 29, verificar se estamos em Fevereiro
                    if (mes != 2) {
                        // Se o mês não for fevereiro, ok
                        ok = true;
                        // Dia 29, mês de fevereiro verificar se o ano é bissexto
                    } else if (System.DateTime.IsLeapYear ((int)ano)) {
                        // Se for bissexto, ok
                        ok = true;
                    } else { dia--; }
                } else if (dia == 31) {
                    // Se o dia é 31, verificar se estamos nos meses 
                    // 1, 3, 5, 7, 8, 10 ou 12
                    if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || 
                        mes == 8 || mes == 10 || mes == 12) {
                        // Se for, então ok
                        ok = true;
                    } else { dia--; }
                }
            }

            return System.DateTime.Parse (dia.ToString("00")+"/"+
                                          mes.ToString("00")+"/"+
                                          ano.ToString("0000"));
        }

        private void dtData_ValueChanged(object sender, EventArgs e)
        {
            Decimal parcela = (Decimal)((DateTimePicker)sender).Tag;
            PrestacaoCollection prestacaoCollection =
                prestacoesCollection.FindByParcela(parcela);

            if (prestacaoCollection != null)
            {
                DateTime data = prestacaoCollection.DataParcela.Value;

                for (Decimal i = parcela; i < prestacoesCollection.Count; i++)
                {
                    data = data.AddMonths(1);
                    String dtAq = nuDiaVenc.Value.ToString("00") +
                                  data.ToString("/MM/yyyy");
                    data = dataAnteriorValida(dtAq);

                    prestacoesCollection.GetItem(i).DataParcela.Value = data;
                }
            }
        }

        private void nuValor_Validated(object sender, EventArgs e)
        {
            Decimal parcela = (Decimal)((NumericTextBox.NumericTextBox)sender).Tag;
            PrestacaoCollection prestacaoCollection =
                prestacoesCollection.FindByParcela(parcela);

            if (prestacaoCollection != null)
            {
                Decimal totalPago = 0;

                // Primeiro descubro o valor que já foi pago
                for (Decimal i = 0; i < parcela; i++)
                {
                    totalPago = totalPago +
                        prestacoesCollection.GetItem(i).ValorParcela.DecimalValue;
                }

                // Agora descubro o que ainda falta pagar e qual o valor
                // das novas prestações
                Decimal totalRestante = vlPrestacaoTotal.DecimalValue - totalPago;

                // Verifico se o valor informado é maior que o restante das parcelas. Se for, então avisa
                // e aborta.
                if (((NumericTextBox.NumericTextBox)sender).DecimalValue > totalRestante) {
                    MessageBox.Show("O valor informado não é válido pois é maior que o valor restante.", 
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                        MessageBoxDefaultButton.Button1);
                    ((NumericTextBox.NumericTextBox)sender).Focus();
                } else {
                  // Se for a última parcela não faz nada.
                  if (nuParcelas.Value - parcela > 0) {
                      // Se não for a última parcela então pega o valor restante e divide entre as
                      // parcelas restantes.
                      Decimal novoValor = totalRestante / (nuParcelas.Value - parcela);
                      Decimal novoTotal = 0;

                      // Distribuo as novas prestações
                      for (Decimal i = parcela; i < prestacoesCollection.Count; i++)
                      {
                          if (novoValor > 0) {
                              prestacoesCollection.GetItem(i).ValorParcela.Text =
                                 novoValor.ToString("#0.00");
                          } else {
                              prestacoesCollection.GetItem(i).ValorParcela.Text = "0.00";
                          }

                          novoTotal = novoTotal +
                              prestacoesCollection.GetItem(i).ValorParcela.DecimalValue;
                      }

                      // Verifico se sobrou "resíduos". Se sobrou, calcula a diferença na última parcela
                      if (novoTotal != totalRestante)
                      {
                          if (prestacoesCollection.GetItem(prestacoesCollection.Count - 1).ValorParcela.DecimalValue + (totalRestante - novoTotal) > 0) {
                              prestacoesCollection.GetItem(prestacoesCollection.Count - 1).ValorParcela.Text =
                                System.Convert.ToString(
                                  prestacoesCollection.GetItem(prestacoesCollection.Count - 1).ValorParcela.DecimalValue + (totalRestante - novoTotal));
                          } else {
                              prestacoesCollection.GetItem(prestacoesCollection.Count - 1).ValorParcela.Text = "0.00";
                          }
                      }
                  }
                }
            }
        }

        private void btParcelas_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                criaPaineis();

                if (!novoRegistro) {
                    recalculaParcelas();
                }
            }
        }

        private void controlaPainel(int moveBy)
        {
            tabAutomatico = true;
            lastTab = 2;
            tcPrestacao.SelectedIndex = lastTab;
            tabAutomatico = false;
            painelAtivo = painelAtivo + moveBy;

            for (int i = 0; i < pnlClient.Controls.Count; i++)
            {
                if (i == (paineisCriados - painelAtivo))
                {
                    ((Panel)pnlClient.Controls[i]).Visible = true;
                }
                else
                {
                    ((Panel)pnlClient.Controls[i]).Visible = false;
                }
            }

            btAnterior.Enabled = (painelAtivo > 1);
            btProximo.Enabled = (painelAtivo < paineisCriados);
        }

        private void btAnterior_Click(object sender, EventArgs e)
        {
            controlaPainel(-1);
        }

        private void btProximo_Click(object sender, EventArgs e)
        {
            controlaPainel(1);
        }

        private void nuDiaVenc_ValueChanged(object sender, EventArgs e)
        {
            calculaPrimeira();
        }

        private void dtAquisicao_ValueChanged(object sender, EventArgs e)
        {
            calculaPrimeira();
        }

        private void calculaPrimeira()
        {
            String dtAq = dtAquisicao.Value.ToString("dd/MM/yyyy");
            dtPrimeira.Value = dataAnteriorValida (
               nuDiaVenc.Value.ToString("00") + dtAq.Substring(2, 8));

            if (nuDiaVenc.Value < 
                System.Convert.ToDecimal (dtAquisicao.Value.ToString ("dd"))) {
                    dtPrimeira.Value = dtPrimeira.Value.AddMonths(1);
            }
        }

        private void edDescricao_GotFocus(object sender, EventArgs e)
        {
            ipPrestacao.Enabled = true;
        }

        private void edDescricao_LostFocus(object sender, EventArgs e)
        {
            ipPrestacao.Enabled = false;
        }

        private void btConfirmar_Click(object sender, EventArgs e)
        {
            string codigo = "";

            if (novoRegistro)
            {
                codigo = prestacaoDataSet.GeraCodigo();
                prestacaoDataSet.AddPrestacao(
                    codigo,
                    edDescricao.Text,
                    prestacaoDataSet.EncontraCdTipoMovim(cbTipoMovim.Text),
                    prestacaoDataSet.EncontraCdCentroCusto(cbCentroCusto.Text),
                    nuParcelas.Value,
                    vlPrestacaoTotal.DecimalValue,
                    vlParcela.DecimalValue, 
                    nuDiaVenc.Value,
                    dtAquisicao.Value,
                    dtPrimeira.Value);
            } else {
                codigo = (String)prestacaoDataSet.DataTable.Rows[grPrestacao.CurrentRowIndex].ItemArray[0];

                prestacaoDataSet.AltPrestacao (
                    grPrestacao.CurrentRowIndex,
                    codigo,
                    edDescricao.Text,
                    prestacaoDataSet.EncontraCdTipoMovim(cbTipoMovim.Text),
                    prestacaoDataSet.EncontraCdCentroCusto(cbCentroCusto.Text),
                    nuParcelas.Value,
                    vlPrestacaoTotal.DecimalValue,
                    vlParcela.DecimalValue,
                    nuDiaVenc.Value,
                    dtAquisicao.Value,
                    dtPrimeira.Value,
                    nuParcelas.Value - parcelasPagas);

                lancamentoDataSet.ExcluirPrestacoesExcedentes(codigo, nuParcelas.Value);
            }
            
            PrestacaoCollection prestacaoCollection = null;
            string descricao;

            for (Decimal i = 0; i <= nuParcelas.Value; i++)
            {
                prestacaoCollection = prestacoesCollection.FindByParcela(i);

                if (prestacaoCollection != null)
                {
                   descricao = edDescricao.Text + "-" + System.Convert.ToString(i) + "/" + nuParcelas.Value.ToString();
                   if (novoRegistro || prestacaoCollection.CDLancamento == null)
                   {
                        lancamentoDataSet.AddLancamento
                          (lancamentoDataSet.GeraCodigo(),
                           descricao,
                           prestacaoCollection.DataParcela.Value,
                           prestacaoDataSet.EncontraCdTipoMovim(cbTipoMovim.Text),
                           prestacaoDataSet.EncontraCdCentroCusto(cbCentroCusto.Text),
                           prestacaoCollection.ValorParcela.DecimalValue,
                           "", System.Convert.ToDateTime("31/12/3000"),
                           "", "", codigo, (int)i, 0, 0,
                           prestacaoCollection.Cheque.Text);
                   } else {
                       lancamentoDataSet.AltLancamento (-1,
                          prestacaoCollection.CDLancamento, // TODO
                          descricao,
                          prestacaoCollection.DataParcela.Value,
                          prestacaoDataSet.EncontraCdTipoMovim(cbTipoMovim.Text),
                          prestacaoDataSet.EncontraCdCentroCusto(cbCentroCusto.Text),
                          prestacaoCollection.ValorParcela.DecimalValue,
                          "", System.Convert.ToDateTime("31/12/3000"),
                          "", "", codigo, (int)i, 0, 0,
                          prestacaoCollection.Cheque.Text);
                   }
                }
            }

            novoRegistro = false;
            dtAquisicao.Value = System.DateTime.Today;
            tabAutomatico = true;
            lastTab = 1;
            tcPrestacao.SelectedIndex = lastTab;
            tabAutomatico = false;
            nuDiaVenc.Value = System.Convert.ToDecimal(dtAquisicao.Value.ToString("dd"));

            calculaPrimeira();
            edDescricao.Text = "";
            vlPrestacaoTotal.Text = "";
            nuParcelas.Value = 2;
            cbCentroCusto.SelectedIndex = -1;
            cbTipoMovim.SelectedIndex = -1;

            tabAutomatico = true;
            lastTab = 0;
            tcPrestacao.SelectedIndex = lastTab;
            tabAutomatico = false;
        }

        private void recalculaParcelas()
        {
            Decimal totalJaPago = 0;
            Decimal primeiraNaoPaga = 0;

            for (Decimal i = 0; i < prestacoesCollection.Count; i++)
            {
                // Se está habilitado é porque ainda não foi quitado
                if (!prestacoesCollection.GetItem(i).ValorParcela.Enabled) {
                    totalJaPago = totalJaPago + prestacoesCollection.GetItem(i).ValorParcela.DecimalValue;
                } else {
                    primeiraNaoPaga = i -1;

                    if (primeiraNaoPaga < 0) { primeiraNaoPaga = 0; }
                    break;
                }
            }

            if (primeiraNaoPaga < prestacoesCollection.Count) {
                EventArgs e = new EventArgs();

                nuValor_Validated(prestacoesCollection.GetItem(primeiraNaoPaga).ValorParcela, e);
            }
        }

        private void grPrestacao_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 21)
            {
                if (e.X > 20 && e.X < 161)
                {
                    if (colIndex == 0) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 0; ordemCrescente = true; }
                }
                if (e.X > 162 && e.X < 194)
                {
                    if (colIndex == 1) { ordemCrescente = !ordemCrescente; }
                    else { colIndex = 1; ordemCrescente = true; }
                }
                if (e.X > 195 && e.X < 223)
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
                    sColuna = "p.deprestacao";
                    break;

                case 1:
                    sColuna = "p.nudiavenc";
                    break;

                case 2:
                    sColuna = "faltantes";
                    break;
                default:
                    sColuna = "p.deprestacao";
                    break;
            }

            prestacaoDataSet.SelectAll(false, true, sColuna, ordem, true);
            UpdateDataGrid();
        }

        private void tcPrestacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!tabAutomatico) {
                if (lastTab == 2 && tcPrestacao.SelectedIndex == 1) {
                    // OK!
                    lastTab = 1;
                } else {
                    tabAutomatico = true;
                    tcPrestacao.SelectedIndex = lastTab;
                    tabAutomatico = false;
                }
            }
        }

        private void vlParcela_GotFocus(object sender, EventArgs e)
        {
            vlOldParcela = vlParcela.DecimalValue;
            ipPrestacao.Enabled = true;
        }

        private void vlPrestacaoTotal_GotFocus(object sender, EventArgs e)
        {
            vlOldPrestacao = vlPrestacaoTotal.DecimalValue;
            ipPrestacao.Enabled = true;
        }

        private void vlPrestacaoTotal_LostFocus(object sender, EventArgs e)
        {
            ipPrestacao.Enabled = false;

            if (vlOldPrestacao != vlPrestacaoTotal.DecimalValue) {
                vlOldParcela = System.Convert.ToDecimal (
                                 System.Convert.ToDecimal (vlPrestacaoTotal.DecimalValue / nuParcelas.Value).ToString ("#0.00"));
                vlParcela.Text = System.Convert.ToString(vlOldParcela);
            }
        }

        private void vlParcela_LostFocus(object sender, EventArgs e)
        {
            ipPrestacao.Enabled = false;

            if (vlOldParcela != vlParcela.DecimalValue)
            {
                vlOldPrestacao = nuParcelas.Value * vlParcela.DecimalValue;
                vlPrestacaoTotal.Text = System.Convert.ToString(vlOldPrestacao);
            }
        }

        private void nuParcelas_ValueChanged(object sender, EventArgs e)
        {
            if (vlParcela.DecimalValue != 0 && vlOldParcela != vlParcela.DecimalValue)
            {
                vlOldPrestacao = nuParcelas.Value * vlParcela.DecimalValue;
                vlPrestacaoTotal.Text = System.Convert.ToString(vlOldPrestacao);
            } else if (vlPrestacaoTotal.Text != "") {
                vlOldParcela = System.Convert.ToDecimal(
                                 System.Convert.ToDecimal(vlPrestacaoTotal.DecimalValue / nuParcelas.Value).ToString("#0.00"));
                vlParcela.Text = System.Convert.ToString(vlOldParcela);
            }
        }
    }
}