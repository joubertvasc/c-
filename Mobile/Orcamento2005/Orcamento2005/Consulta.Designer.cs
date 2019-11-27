namespace Orcamento2005
{
  partial class Consulta
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.MainMenu mmConsultar;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.mmConsultar = new System.Windows.Forms.MainMenu();
      this.miOpcoes = new System.Windows.Forms.MenuItem();
      this.miConsultar = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.miMenuPrincipal = new System.Windows.Forms.MenuItem();
      this.deDescricao = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.cbTipoMovim = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.cbCentroCusto = new System.Windows.Forms.ComboBox();
      this.label9 = new System.Windows.Forms.Label();
      this.cbConta = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.tcConsulta = new System.Windows.Forms.TabControl();
      this.tpPeriodos = new System.Windows.Forms.TabPage();
      this.label6 = new System.Windows.Forms.Label();
      this.dtFinalQuit = new System.Windows.Forms.DateTimePicker();
      this.dtInicioQuit = new System.Windows.Forms.DateTimePicker();
      this.dtFinal = new System.Windows.Forms.DateTimePicker();
      this.dtInicial = new System.Windows.Forms.DateTimePicker();
      this.label5 = new System.Windows.Forms.Label();
      this.tpValores = new System.Windows.Forms.TabPage();
      this.cbSomenteOsComDesconto = new System.Windows.Forms.CheckBox();
      this.cbSomenteOsComJuros = new System.Windows.Forms.CheckBox();
      this.vlFinal = new NumericTextBox.NumericTextBox();
      this.vlInicial = new NumericTextBox.NumericTextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.tpVisualizacao = new System.Windows.Forms.TabPage();
      this.cbOrdemDecrescente = new System.Windows.Forms.CheckBox();
      this.rbSemAgrupamento = new System.Windows.Forms.RadioButton();
      this.rbMovim = new System.Windows.Forms.RadioButton();
      this.rbCentroCusto = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.nuCheque = new System.Windows.Forms.TextBox();
      this.btConsultar = new System.Windows.Forms.Button();
      this.tcConsulta.SuspendLayout();
      this.tpPeriodos.SuspendLayout();
      this.tpValores.SuspendLayout();
      this.tpVisualizacao.SuspendLayout();
      this.SuspendLayout();
      // 
      // mmConsultar
      // 
      this.mmConsultar.MenuItems.Add(this.miOpcoes);
      // 
      // miOpcoes
      // 
      this.miOpcoes.MenuItems.Add(this.miConsultar);
      this.miOpcoes.MenuItems.Add(this.menuItem3);
      this.miOpcoes.MenuItems.Add(this.miMenuPrincipal);
      this.miOpcoes.Text = "Opções";
      // 
      // miConsultar
      // 
      this.miConsultar.Text = "Consultar";
      this.miConsultar.Click += new System.EventHandler(this.btConsultar_Click);
      // 
      // menuItem3
      // 
      this.menuItem3.Text = "-";
      // 
      // miMenuPrincipal
      // 
      this.miMenuPrincipal.Text = "Menu principal";
      this.miMenuPrincipal.Click += new System.EventHandler(this.miMenuPrincipal_Click);
      // 
      // deDescricao
      // 
      this.deDescricao.Location = new System.Drawing.Point(3, 109);
      this.deDescricao.Name = "deDescricao";
      this.deDescricao.Size = new System.Drawing.Size(110, 21);
      this.deDescricao.TabIndex = 11;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(3, 93);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(100, 20);
      this.label4.Text = "Descrição:";
      // 
      // cbTipoMovim
      // 
      this.cbTipoMovim.Location = new System.Drawing.Point(119, 71);
      this.cbTipoMovim.Name = "cbTipoMovim";
      this.cbTipoMovim.Size = new System.Drawing.Size(110, 22);
      this.cbTipoMovim.TabIndex = 10;
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(119, 57);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(110, 20);
      this.label8.Text = "Movimentação:";
      // 
      // cbCentroCusto
      // 
      this.cbCentroCusto.Location = new System.Drawing.Point(3, 71);
      this.cbCentroCusto.Name = "cbCentroCusto";
      this.cbCentroCusto.Size = new System.Drawing.Size(110, 22);
      this.cbCentroCusto.TabIndex = 9;
      // 
      // label9
      // 
      this.label9.Location = new System.Drawing.Point(3, 57);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(110, 17);
      this.label9.Text = "Centro de custo:";
      // 
      // cbConta
      // 
      this.cbConta.Location = new System.Drawing.Point(3, 35);
      this.cbConta.Name = "cbConta";
      this.cbConta.Size = new System.Drawing.Size(226, 22);
      this.cbConta.TabIndex = 8;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(3, 21);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 20);
      this.label3.Text = "Conta:";
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(240, 20);
      this.label1.Text = "Consulta";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // tcConsulta
      // 
      this.tcConsulta.Controls.Add(this.tpPeriodos);
      this.tcConsulta.Controls.Add(this.tpValores);
      this.tcConsulta.Controls.Add(this.tpVisualizacao);
      this.tcConsulta.Dock = System.Windows.Forms.DockStyle.None;
      this.tcConsulta.Location = new System.Drawing.Point(3, 136);
      this.tcConsulta.Name = "tcConsulta";
      this.tcConsulta.SelectedIndex = 0;
      this.tcConsulta.Size = new System.Drawing.Size(226, 103);
      this.tcConsulta.TabIndex = 17;
      // 
      // tpPeriodos
      // 
      this.tpPeriodos.Controls.Add(this.label6);
      this.tpPeriodos.Controls.Add(this.dtFinalQuit);
      this.tpPeriodos.Controls.Add(this.dtInicioQuit);
      this.tpPeriodos.Controls.Add(this.dtFinal);
      this.tpPeriodos.Controls.Add(this.dtInicial);
      this.tpPeriodos.Controls.Add(this.label5);
      this.tpPeriodos.Location = new System.Drawing.Point(0, 0);
      this.tpPeriodos.Name = "tpPeriodos";
      this.tpPeriodos.Size = new System.Drawing.Size(226, 80);
      this.tpPeriodos.Text = "Períodos";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(0, 39);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(223, 16);
      this.label6.Text = "Quitação entre:";
      // 
      // dtFinalQuit
      // 
      this.dtFinalQuit.CustomFormat = "dd/MM/yyyy";
      this.dtFinalQuit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtFinalQuit.Location = new System.Drawing.Point(116, 58);
      this.dtFinalQuit.Name = "dtFinalQuit";
      this.dtFinalQuit.Size = new System.Drawing.Size(110, 22);
      this.dtFinalQuit.TabIndex = 5;
      // 
      // dtInicioQuit
      // 
      this.dtInicioQuit.CustomFormat = "dd/MM/yyyy";
      this.dtInicioQuit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtInicioQuit.Location = new System.Drawing.Point(3, 58);
      this.dtInicioQuit.Name = "dtInicioQuit";
      this.dtInicioQuit.Size = new System.Drawing.Size(107, 22);
      this.dtInicioQuit.TabIndex = 4;
      // 
      // dtFinal
      // 
      this.dtFinal.CustomFormat = "dd/MM/yyyy";
      this.dtFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtFinal.Location = new System.Drawing.Point(116, 16);
      this.dtFinal.Name = "dtFinal";
      this.dtFinal.Size = new System.Drawing.Size(110, 22);
      this.dtFinal.TabIndex = 2;
      // 
      // dtInicial
      // 
      this.dtInicial.CustomFormat = "dd/MM/yyyy";
      this.dtInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtInicial.Location = new System.Drawing.Point(3, 16);
      this.dtInicial.Name = "dtInicial";
      this.dtInicial.Size = new System.Drawing.Size(107, 22);
      this.dtInicial.TabIndex = 1;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(3, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(223, 20);
      this.label5.Text = "Lançamento entre:";
      // 
      // tpValores
      // 
      this.tpValores.Controls.Add(this.cbSomenteOsComDesconto);
      this.tpValores.Controls.Add(this.cbSomenteOsComJuros);
      this.tpValores.Controls.Add(this.vlFinal);
      this.tpValores.Controls.Add(this.vlInicial);
      this.tpValores.Controls.Add(this.label7);
      this.tpValores.Location = new System.Drawing.Point(0, 0);
      this.tpValores.Name = "tpValores";
      this.tpValores.Size = new System.Drawing.Size(218, 77);
      this.tpValores.Text = "Valores";
      // 
      // cbSomenteOsComDesconto
      // 
      this.cbSomenteOsComDesconto.Location = new System.Drawing.Point(3, 60);
      this.cbSomenteOsComDesconto.Name = "cbSomenteOsComDesconto";
      this.cbSomenteOsComDesconto.Size = new System.Drawing.Size(223, 20);
      this.cbSomenteOsComDesconto.TabIndex = 4;
      this.cbSomenteOsComDesconto.Text = "Somente os com desconto";
      // 
      // cbSomenteOsComJuros
      // 
      this.cbSomenteOsComJuros.Location = new System.Drawing.Point(3, 40);
      this.cbSomenteOsComJuros.Name = "cbSomenteOsComJuros";
      this.cbSomenteOsComJuros.Size = new System.Drawing.Size(223, 20);
      this.cbSomenteOsComJuros.TabIndex = 3;
      this.cbSomenteOsComJuros.Text = "Somente os com juros";
      // 
      // vlFinal
      // 
      this.vlFinal.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.vlFinal.IntValue = 0;
      this.vlFinal.Location = new System.Drawing.Point(116, 17);
      this.vlFinal.Name = "vlFinal";
      this.vlFinal.Size = new System.Drawing.Size(110, 21);
      this.vlFinal.TabIndex = 2;
      this.vlFinal.Text = "0";
      this.vlFinal.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
      // 
      // vlInicial
      // 
      this.vlInicial.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.vlInicial.IntValue = 0;
      this.vlInicial.Location = new System.Drawing.Point(3, 17);
      this.vlInicial.Name = "vlInicial";
      this.vlInicial.Size = new System.Drawing.Size(107, 21);
      this.vlInicial.TabIndex = 1;
      this.vlInicial.Text = "0";
      this.vlInicial.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(3, 0);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(223, 16);
      this.label7.Text = "Valor do lançamento entre:";
      // 
      // tpVisualizacao
      // 
      this.tpVisualizacao.Controls.Add(this.cbOrdemDecrescente);
      this.tpVisualizacao.Controls.Add(this.rbSemAgrupamento);
      this.tpVisualizacao.Controls.Add(this.rbMovim);
      this.tpVisualizacao.Controls.Add(this.rbCentroCusto);
      this.tpVisualizacao.Location = new System.Drawing.Point(0, 0);
      this.tpVisualizacao.Name = "tpVisualizacao";
      this.tpVisualizacao.Size = new System.Drawing.Size(218, 77);
      this.tpVisualizacao.Text = "Visualização";
      // 
      // cbOrdemDecrescente
      // 
      this.cbOrdemDecrescente.Location = new System.Drawing.Point(3, 57);
      this.cbOrdemDecrescente.Name = "cbOrdemDecrescente";
      this.cbOrdemDecrescente.Size = new System.Drawing.Size(223, 20);
      this.cbOrdemDecrescente.TabIndex = 3;
      this.cbOrdemDecrescente.Text = "Ordem decrescente?";
      // 
      // rbSemAgrupamento
      // 
      this.rbSemAgrupamento.Checked = true;
      this.rbSemAgrupamento.Location = new System.Drawing.Point(3, 34);
      this.rbSemAgrupamento.Name = "rbSemAgrupamento";
      this.rbSemAgrupamento.Size = new System.Drawing.Size(223, 20);
      this.rbSemAgrupamento.TabIndex = 2;
      this.rbSemAgrupamento.Text = "Sem agrupamentos";
      // 
      // rbMovim
      // 
      this.rbMovim.Location = new System.Drawing.Point(3, 17);
      this.rbMovim.Name = "rbMovim";
      this.rbMovim.Size = new System.Drawing.Size(223, 20);
      this.rbMovim.TabIndex = 1;
      this.rbMovim.TabStop = false;
      this.rbMovim.Text = "Agrupar por movimentação";
      // 
      // rbCentroCusto
      // 
      this.rbCentroCusto.Location = new System.Drawing.Point(3, 0);
      this.rbCentroCusto.Name = "rbCentroCusto";
      this.rbCentroCusto.Size = new System.Drawing.Size(223, 20);
      this.rbCentroCusto.TabIndex = 0;
      this.rbCentroCusto.TabStop = false;
      this.rbCentroCusto.Text = "Agrupar por centro de custo";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(119, 93);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 20);
      this.label2.Text = "Cheque:";
      // 
      // nuCheque
      // 
      this.nuCheque.Location = new System.Drawing.Point(119, 109);
      this.nuCheque.Name = "nuCheque";
      this.nuCheque.Size = new System.Drawing.Size(110, 21);
      this.nuCheque.TabIndex = 20;
      // 
      // btConsultar
      // 
      this.btConsultar.Location = new System.Drawing.Point(3, 245);
      this.btConsultar.Name = "btConsultar";
      this.btConsultar.Size = new System.Drawing.Size(72, 20);
      this.btConsultar.TabIndex = 21;
      this.btConsultar.Text = "Consultar";
      this.btConsultar.Click += new System.EventHandler(this.btConsultar_Click);
      // 
      // Consulta
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(240, 268);
      this.Controls.Add(this.btConsultar);
      this.Controls.Add(this.nuCheque);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.tcConsulta);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.deDescricao);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cbTipoMovim);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.cbCentroCusto);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.cbConta);
      this.Controls.Add(this.label3);
      this.Menu = this.mmConsultar;
      this.Name = "Consulta";
      this.Text = "Consulta";
      this.Load += new System.EventHandler(this.Consulta_Load);
      this.tcConsulta.ResumeLayout(false);
      this.tpPeriodos.ResumeLayout(false);
      this.tpValores.ResumeLayout(false);
      this.tpVisualizacao.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox deDescricao;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cbTipoMovim;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ComboBox cbCentroCusto;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.ComboBox cbConta;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabControl tcConsulta;
    private System.Windows.Forms.TabPage tpPeriodos;
    private System.Windows.Forms.TabPage tpValores;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox nuCheque;
    private System.Windows.Forms.Button btConsultar;
    private System.Windows.Forms.MenuItem miOpcoes;
    private System.Windows.Forms.MenuItem miConsultar;
    private System.Windows.Forms.MenuItem menuItem3;
    private System.Windows.Forms.MenuItem miMenuPrincipal;
    private System.Windows.Forms.DateTimePicker dtFinalQuit;
    private System.Windows.Forms.DateTimePicker dtInicioQuit;
    private System.Windows.Forms.DateTimePicker dtFinal;
    private System.Windows.Forms.DateTimePicker dtInicial;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private NumericTextBox.NumericTextBox vlFinal;
    private NumericTextBox.NumericTextBox vlInicial;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TabPage tpVisualizacao;
    private System.Windows.Forms.CheckBox cbSomenteOsComDesconto;
    private System.Windows.Forms.CheckBox cbSomenteOsComJuros;
    private System.Windows.Forms.RadioButton rbSemAgrupamento;
    private System.Windows.Forms.RadioButton rbMovim;
    private System.Windows.Forms.RadioButton rbCentroCusto;
    private System.Windows.Forms.CheckBox cbOrdemDecrescente;
  }
}