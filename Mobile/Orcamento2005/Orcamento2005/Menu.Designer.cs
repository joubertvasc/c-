namespace Orcamento2005
{
    partial class FormMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmMenu;

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
            this.mmMenu = new System.Windows.Forms.MainMenu();
            this.miLancamento = new System.Windows.Forms.MenuItem();
            this.miLancamentos = new System.Windows.Forms.MenuItem();
            this.miCalculoJuros = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miCreditosFixos = new System.Windows.Forms.MenuItem();
            this.miContasFixas = new System.Windows.Forms.MenuItem();
            this.miPrestacoes = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miSair = new System.Windows.Forms.MenuItem();
            this.miRelatorio = new System.Windows.Forms.MenuItem();
            this.miExtrato = new System.Windows.Forms.MenuItem();
            this.miConsulta = new System.Windows.Forms.MenuItem();
            this.miApoio = new System.Windows.Forms.MenuItem();
            this.miTipoMovim = new System.Windows.Forms.MenuItem();
            this.miCentroCusto = new System.Windows.Forms.MenuItem();
            this.miContas = new System.Windows.Forms.MenuItem();
            this.tcMenu = new System.Windows.Forms.TabControl();
            this.tpResumo = new System.Windows.Forms.TabPage();
            this.lbResumo = new System.Windows.Forms.ListBox();
            this.tpDetalhes = new System.Windows.Forms.TabPage();
            this.lbDetalhes = new System.Windows.Forms.ListBox();
            this.tcMenu.SuspendLayout();
            this.tpResumo.SuspendLayout();
            this.tpDetalhes.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmMenu
            // 
            this.mmMenu.MenuItems.Add(this.miLancamento);
            this.mmMenu.MenuItems.Add(this.miRelatorio);
            this.mmMenu.MenuItems.Add(this.miApoio);
            // 
            // miLancamento
            // 
            this.miLancamento.MenuItems.Add(this.miLancamentos);
            this.miLancamento.MenuItems.Add(this.miCalculoJuros);
            this.miLancamento.MenuItems.Add(this.menuItem1);
            this.miLancamento.MenuItems.Add(this.miCreditosFixos);
            this.miLancamento.MenuItems.Add(this.miContasFixas);
            this.miLancamento.MenuItems.Add(this.miPrestacoes);
            this.miLancamento.MenuItems.Add(this.menuItem2);
            this.miLancamento.MenuItems.Add(this.miSair);
            this.miLancamento.Text = "Lançamento";
            // 
            // miLancamentos
            // 
            this.miLancamentos.Text = "Lançamento";
            this.miLancamentos.Click += new System.EventHandler(this.miLancamentos_Click);
            // 
            // miCalculoJuros
            // 
            this.miCalculoJuros.Text = "Cálculo de Juros";
            this.miCalculoJuros.Click += new System.EventHandler(this.miCalculoJuros_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // miCreditosFixos
            // 
            this.miCreditosFixos.Text = "Créditos Fixos";
            this.miCreditosFixos.Click += new System.EventHandler(this.miCreditosFixos_Click);
            // 
            // miContasFixas
            // 
            this.miContasFixas.Text = "Débitos Fixos";
            this.miContasFixas.Click += new System.EventHandler(this.miContasFixas_Click);
            // 
            // miPrestacoes
            // 
            this.miPrestacoes.Text = "Prestações";
            this.miPrestacoes.Click += new System.EventHandler(this.miPrestacoes_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // miSair
            // 
            this.miSair.Text = "Sair";
            this.miSair.Click += new System.EventHandler(this.miSair_Click);
            // 
            // miRelatorio
            // 
            this.miRelatorio.MenuItems.Add(this.miExtrato);
            this.miRelatorio.MenuItems.Add(this.miConsulta);
            this.miRelatorio.Text = "Relatório";
            // 
            // miExtrato
            // 
            this.miExtrato.Text = "Extrato";
            // 
            // miConsulta
            // 
            this.miConsulta.Text = "Consulta";
            this.miConsulta.Click += new System.EventHandler(this.miConsulta_Click);
            // 
            // miApoio
            // 
            this.miApoio.MenuItems.Add(this.miTipoMovim);
            this.miApoio.MenuItems.Add(this.miCentroCusto);
            this.miApoio.MenuItems.Add(this.miContas);
            this.miApoio.Text = "Apoio";
            // 
            // miTipoMovim
            // 
            this.miTipoMovim.Text = "Tipo de Movimentação";
            this.miTipoMovim.Click += new System.EventHandler(this.miTipoMovim_Click);
            // 
            // miCentroCusto
            // 
            this.miCentroCusto.Text = "Centro de Custo";
            this.miCentroCusto.Click += new System.EventHandler(this.miCentroCusto_Click);
            // 
            // miContas
            // 
            this.miContas.Text = "Contas ";
            this.miContas.Click += new System.EventHandler(this.miContas_Click);
            // 
            // tcMenu
            // 
            this.tcMenu.Controls.Add(this.tpResumo);
            this.tcMenu.Controls.Add(this.tpDetalhes);
            this.tcMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMenu.Location = new System.Drawing.Point(0, 0);
            this.tcMenu.Name = "tcMenu";
            this.tcMenu.SelectedIndex = 0;
            this.tcMenu.Size = new System.Drawing.Size(240, 268);
            this.tcMenu.TabIndex = 0;
            // 
            // tpResumo
            // 
            this.tpResumo.Controls.Add(this.lbResumo);
            this.tpResumo.Location = new System.Drawing.Point(0, 0);
            this.tpResumo.Name = "tpResumo";
            this.tpResumo.Size = new System.Drawing.Size(240, 245);
            this.tpResumo.Text = "Resumo";
            // 
            // lbResumo
            // 
            this.lbResumo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResumo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.lbResumo.Location = new System.Drawing.Point(0, 0);
            this.lbResumo.Name = "lbResumo";
            this.lbResumo.Size = new System.Drawing.Size(240, 240);
            this.lbResumo.TabIndex = 0;
            // 
            // tpDetalhes
            // 
            this.tpDetalhes.Controls.Add(this.lbDetalhes);
            this.tpDetalhes.Location = new System.Drawing.Point(0, 0);
            this.tpDetalhes.Name = "tpDetalhes";
            this.tpDetalhes.Size = new System.Drawing.Size(240, 245);
            this.tpDetalhes.Text = "Detalhes";
            // 
            // lbDetalhes
            // 
            this.lbDetalhes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDetalhes.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.lbDetalhes.Items.Add("   Detalhamento dos Próximos ");
            this.lbDetalhes.Items.Add("          Lançamentos");
            this.lbDetalhes.Items.Add("");
            this.lbDetalhes.Items.Add("Créditos");
            this.lbDetalhes.Items.Add("Data      Valor/ Movimentação");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("");
            this.lbDetalhes.Items.Add("Débitos");
            this.lbDetalhes.Items.Add("Data      Valor/ Movimentação");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("           XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("");
            this.lbDetalhes.Items.Add("Cheques");
            this.lbDetalhes.Items.Add("Data/Nº   Valor/ Movimentação");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12 ");
            this.lbDetalhes.Items.Add("XXXXXXXXXX XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("XXXXXXXXXX XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("XXXXXXXXXX XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("XXXXXXXXXX XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Items.Add("12/12/1234 123.123,12");
            this.lbDetalhes.Items.Add("XXXXXXXXXX XXXXXXXXXXXXXXXXXXXX");
            this.lbDetalhes.Location = new System.Drawing.Point(0, 0);
            this.lbDetalhes.Name = "lbDetalhes";
            this.lbDetalhes.Size = new System.Drawing.Size(240, 240);
            this.lbDetalhes.TabIndex = 0;
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tcMenu);
            this.Menu = this.mmMenu;
            this.Name = "FormMenu";
            this.Text = "Orçamento";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.tcMenu.ResumeLayout(false);
            this.tpResumo.ResumeLayout(false);
            this.tpDetalhes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miLancamento;
        private System.Windows.Forms.MenuItem miContasFixas;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miCalculoJuros;
        private System.Windows.Forms.MenuItem miLancamentos;
        private System.Windows.Forms.MenuItem miSair;
        private System.Windows.Forms.MenuItem miRelatorio;
        private System.Windows.Forms.MenuItem miApoio;
        private System.Windows.Forms.MenuItem miExtrato;
        private System.Windows.Forms.MenuItem miConsulta;
        private System.Windows.Forms.MenuItem miTipoMovim;
        private System.Windows.Forms.MenuItem miCentroCusto;
        private System.Windows.Forms.MenuItem miContas;
        private System.Windows.Forms.TabControl tcMenu;
        private System.Windows.Forms.TabPage tpResumo;
        private System.Windows.Forms.TabPage tpDetalhes;
        private System.Windows.Forms.ListBox lbResumo;
        private System.Windows.Forms.ListBox lbDetalhes;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miPrestacoes;
        private System.Windows.Forms.MenuItem miCreditosFixos;
    }
}

