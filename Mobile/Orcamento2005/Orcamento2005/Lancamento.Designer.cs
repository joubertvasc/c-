namespace Orcamento2005
{
  partial class Lancamento
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.MainMenu mmExtrato;

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
      this.mmExtrato = new System.Windows.Forms.MainMenu();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.miNovo = new System.Windows.Forms.MenuItem();
      this.miConsulta = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.miMenuPrincipal = new System.Windows.Forms.MenuItem();
      this.cmExtrato = new System.Windows.Forms.ContextMenu();
      this.cmNovo = new System.Windows.Forms.MenuItem();
      this.cmConsulta = new System.Windows.Forms.MenuItem();
      this.tcLancamento = new System.Windows.Forms.TabControl();
      this.tpSaldo = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.grExtrato = new System.Windows.Forms.DataGrid();
      this.tpNovo = new System.Windows.Forms.TabPage();
      this.btCancelar = new System.Windows.Forms.Button();
      this.btSalvar = new System.Windows.Forms.Button();
      this.nuCheque = new System.Windows.Forms.TextBox();
      this.dtQuitacao = new System.Windows.Forms.DateTimePicker();
      this.dtLancamento = new System.Windows.Forms.DateTimePicker();
      this.label12 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.vlDesconto = new NumericTextBox.NumericTextBox();
      this.vlJuros = new NumericTextBox.NumericTextBox();
      this.vlLancamento = new NumericTextBox.NumericTextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.deDescricao = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.cbTipoMovim = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.cbCentroCusto = new System.Windows.Forms.ComboBox();
      this.label9 = new System.Windows.Forms.Label();
      this.cbConta = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.tcLancamento.SuspendLayout();
      this.tpSaldo.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tpNovo.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // mmExtrato
      // 
      this.mmExtrato.MenuItems.Add(this.menuItem1);
      // 
      // menuItem1
      // 
      this.menuItem1.MenuItems.Add(this.miNovo);
      this.menuItem1.MenuItems.Add(this.miConsulta);
      this.menuItem1.MenuItems.Add(this.menuItem2);
      this.menuItem1.MenuItems.Add(this.miMenuPrincipal);
      this.menuItem1.Text = "Opções";
      // 
      // miNovo
      // 
      this.miNovo.Text = "Novo";
      this.miNovo.Click += new System.EventHandler(this.miNovo_Click);
      // 
      // miConsulta
      // 
      this.miConsulta.Text = "Consulta";
      this.miConsulta.Click += new System.EventHandler(this.miConsulta_Click);
      // 
      // menuItem2
      // 
      this.menuItem2.Text = "-";
      // 
      // miMenuPrincipal
      // 
      this.miMenuPrincipal.Text = "Menu principal";
      this.miMenuPrincipal.Click += new System.EventHandler(this.miMenuPrincipal_Click);
      // 
      // cmExtrato
      // 
      this.cmExtrato.MenuItems.Add(this.cmNovo);
      this.cmExtrato.MenuItems.Add(this.cmConsulta);
      // 
      // cmNovo
      // 
      this.cmNovo.Text = "Novo";
      this.cmNovo.Click += new System.EventHandler(this.miNovo_Click);
      // 
      // cmConsulta
      // 
      this.cmConsulta.Text = "Consulta";
      this.cmConsulta.Click += new System.EventHandler(this.miConsulta_Click);
      // 
      // tcLancamento
      // 
      this.tcLancamento.Controls.Add(this.tpSaldo);
      this.tcLancamento.Controls.Add(this.tpNovo);
      this.tcLancamento.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcLancamento.Location = new System.Drawing.Point(0, 0);
      this.tcLancamento.Name = "tcLancamento";
      this.tcLancamento.SelectedIndex = 0;
      this.tcLancamento.Size = new System.Drawing.Size(240, 268);
      this.tcLancamento.TabIndex = 0;
      // 
      // tpSaldo
      // 
      this.tpSaldo.Controls.Add(this.panel1);
      this.tpSaldo.Controls.Add(this.grExtrato);
      this.tpSaldo.Location = new System.Drawing.Point(0, 0);
      this.tpSaldo.Name = "tpSaldo";
      this.tpSaldo.Size = new System.Drawing.Size(240, 245);
      this.tpSaldo.Text = "Saldo";
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(240, 19);
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(240, 19);
      this.label1.Text = "Saldo das Contas";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // grExtrato
      // 
      this.grExtrato.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      this.grExtrato.ContextMenu = this.cmExtrato;
      this.grExtrato.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.grExtrato.Font = new System.Drawing.Font("Courier New", 7F, System.Drawing.FontStyle.Regular);
      this.grExtrato.Location = new System.Drawing.Point(0, 22);
      this.grExtrato.Name = "grExtrato";
      this.grExtrato.Size = new System.Drawing.Size(240, 223);
      this.grExtrato.TabIndex = 1;
      // 
      // tpNovo
      // 
      this.tpNovo.Controls.Add(this.btCancelar);
      this.tpNovo.Controls.Add(this.btSalvar);
      this.tpNovo.Controls.Add(this.nuCheque);
      this.tpNovo.Controls.Add(this.dtQuitacao);
      this.tpNovo.Controls.Add(this.dtLancamento);
      this.tpNovo.Controls.Add(this.label12);
      this.tpNovo.Controls.Add(this.label11);
      this.tpNovo.Controls.Add(this.label10);
      this.tpNovo.Controls.Add(this.vlDesconto);
      this.tpNovo.Controls.Add(this.vlJuros);
      this.tpNovo.Controls.Add(this.vlLancamento);
      this.tpNovo.Controls.Add(this.label7);
      this.tpNovo.Controls.Add(this.label6);
      this.tpNovo.Controls.Add(this.label5);
      this.tpNovo.Controls.Add(this.deDescricao);
      this.tpNovo.Controls.Add(this.label4);
      this.tpNovo.Controls.Add(this.cbTipoMovim);
      this.tpNovo.Controls.Add(this.label8);
      this.tpNovo.Controls.Add(this.cbCentroCusto);
      this.tpNovo.Controls.Add(this.label9);
      this.tpNovo.Controls.Add(this.cbConta);
      this.tpNovo.Controls.Add(this.label3);
      this.tpNovo.Controls.Add(this.panel2);
      this.tpNovo.Location = new System.Drawing.Point(0, 0);
      this.tpNovo.Name = "tpNovo";
      this.tpNovo.Size = new System.Drawing.Size(240, 245);
      this.tpNovo.Text = "Novo";
      // 
      // btCancelar
      // 
      this.btCancelar.Location = new System.Drawing.Point(85, 218);
      this.btCancelar.Name = "btCancelar";
      this.btCancelar.Size = new System.Drawing.Size(72, 20);
      this.btCancelar.TabIndex = 11;
      this.btCancelar.Text = "Cancelar";
      this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
      // 
      // btSalvar
      // 
      this.btSalvar.Location = new System.Drawing.Point(7, 218);
      this.btSalvar.Name = "btSalvar";
      this.btSalvar.Size = new System.Drawing.Size(72, 20);
      this.btSalvar.TabIndex = 10;
      this.btSalvar.Text = "Salvar";
      this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
      // 
      // nuCheque
      // 
      this.nuCheque.Location = new System.Drawing.Point(85, 190);
      this.nuCheque.Name = "nuCheque";
      this.nuCheque.Size = new System.Drawing.Size(70, 21);
      this.nuCheque.TabIndex = 8;
      // 
      // dtQuitacao
      // 
      this.dtQuitacao.CustomFormat = "dd/MM/yy";
      this.dtQuitacao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtQuitacao.Location = new System.Drawing.Point(161, 190);
      this.dtQuitacao.Name = "dtQuitacao";
      this.dtQuitacao.Size = new System.Drawing.Size(70, 22);
      this.dtQuitacao.TabIndex = 9;
      // 
      // dtLancamento
      // 
      this.dtLancamento.CustomFormat = "dd/MM/yy";
      this.dtLancamento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtLancamento.Location = new System.Drawing.Point(7, 190);
      this.dtLancamento.Name = "dtLancamento";
      this.dtLancamento.Size = new System.Drawing.Size(70, 22);
      this.dtLancamento.TabIndex = 7;
      // 
      // label12
      // 
      this.label12.Location = new System.Drawing.Point(85, 175);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(70, 20);
      this.label12.Text = "Cheque:";
      // 
      // label11
      // 
      this.label11.Location = new System.Drawing.Point(160, 175);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(70, 20);
      this.label11.Text = "Quitação:";
      // 
      // label10
      // 
      this.label10.Location = new System.Drawing.Point(7, 175);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(70, 20);
      this.label10.Text = "Lançam.:";
      // 
      // vlDesconto
      // 
      this.vlDesconto.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.vlDesconto.IntValue = 0;
      this.vlDesconto.Location = new System.Drawing.Point(163, 151);
      this.vlDesconto.Name = "vlDesconto";
      this.vlDesconto.Size = new System.Drawing.Size(70, 21);
      this.vlDesconto.TabIndex = 6;
      this.vlDesconto.Text = "0";
      this.vlDesconto.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
      // 
      // vlJuros
      // 
      this.vlJuros.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.vlJuros.IntValue = 0;
      this.vlJuros.Location = new System.Drawing.Point(85, 151);
      this.vlJuros.Name = "vlJuros";
      this.vlJuros.Size = new System.Drawing.Size(70, 21);
      this.vlJuros.TabIndex = 5;
      this.vlJuros.Text = "0";
      this.vlJuros.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
      // 
      // vlLancamento
      // 
      this.vlLancamento.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.vlLancamento.IntValue = 0;
      this.vlLancamento.Location = new System.Drawing.Point(7, 151);
      this.vlLancamento.Name = "vlLancamento";
      this.vlLancamento.Size = new System.Drawing.Size(70, 21);
      this.vlLancamento.TabIndex = 4;
      this.vlLancamento.Text = "0";
      this.vlLancamento.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(163, 136);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(67, 20);
      this.label7.Text = "Descontos:";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(85, 136);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(52, 20);
      this.label6.Text = "Juros:";
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(7, 136);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(41, 20);
      this.label5.Text = "Valor:";
      // 
      // deDescricao
      // 
      this.deDescricao.Location = new System.Drawing.Point(7, 114);
      this.deDescricao.Name = "deDescricao";
      this.deDescricao.Size = new System.Drawing.Size(226, 21);
      this.deDescricao.TabIndex = 3;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(7, 98);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(100, 20);
      this.label4.Text = "Descrição:";
      // 
      // cbTipoMovim
      // 
      this.cbTipoMovim.Location = new System.Drawing.Point(123, 73);
      this.cbTipoMovim.Name = "cbTipoMovim";
      this.cbTipoMovim.Size = new System.Drawing.Size(110, 22);
      this.cbTipoMovim.TabIndex = 2;
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(123, 57);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(110, 20);
      this.label8.Text = "Movimentação:";
      // 
      // cbCentroCusto
      // 
      this.cbCentroCusto.Location = new System.Drawing.Point(7, 73);
      this.cbCentroCusto.Name = "cbCentroCusto";
      this.cbCentroCusto.Size = new System.Drawing.Size(110, 22);
      this.cbCentroCusto.TabIndex = 1;
      // 
      // label9
      // 
      this.label9.Location = new System.Drawing.Point(7, 57);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(226, 17);
      this.label9.Text = "Centro de custo:";
      // 
      // cbConta
      // 
      this.cbConta.Location = new System.Drawing.Point(7, 34);
      this.cbConta.Name = "cbConta";
      this.cbConta.Size = new System.Drawing.Size(226, 22);
      this.cbConta.TabIndex = 0;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(7, 19);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 20);
      this.label3.Text = "Conta:";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.label2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(240, 19);
      // 
      // label2
      // 
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
      this.label2.Location = new System.Drawing.Point(0, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(240, 19);
      this.label2.Text = "Novo Lançamento";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // Lancamento
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(240, 268);
      this.Controls.Add(this.tcLancamento);
      this.Menu = this.mmExtrato;
      this.Name = "Lancamento";
      this.Text = "Lançamentos";
      this.Load += new System.EventHandler(this.Lancamento_Load);
      this.tcLancamento.ResumeLayout(false);
      this.tpSaldo.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.tpNovo.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem menuItem2;
    private System.Windows.Forms.MenuItem miMenuPrincipal;
    private System.Windows.Forms.MenuItem miNovo;
    private System.Windows.Forms.MenuItem miConsulta;
    private System.Windows.Forms.ContextMenu cmExtrato;
    private System.Windows.Forms.MenuItem cmNovo;
    private System.Windows.Forms.MenuItem cmConsulta;
    private System.Windows.Forms.TabControl tcLancamento;
    private System.Windows.Forms.TabPage tpSaldo;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGrid grExtrato;
    private System.Windows.Forms.TabPage tpNovo;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbConta;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbTipoMovim;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ComboBox cbCentroCusto;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox deDescricao;
    private System.Windows.Forms.Label label4;
    private NumericTextBox.NumericTextBox vlDesconto;
    private NumericTextBox.NumericTextBox vlJuros;
    private NumericTextBox.NumericTextBox vlLancamento;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.DateTimePicker dtLancamento;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox nuCheque;
    private System.Windows.Forms.DateTimePicker dtQuitacao;
    private System.Windows.Forms.Button btCancelar;
    private System.Windows.Forms.Button btSalvar;
  }
}