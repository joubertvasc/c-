namespace Orcamento2005
{
    partial class Contas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmContas;

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
            this.mmContas = new System.Windows.Forms.MainMenu();
            this.miOpcoes = new System.Windows.Forms.MenuItem();
            this.miEditar = new System.Windows.Forms.MenuItem();
            this.miInserir = new System.Windows.Forms.MenuItem();
            this.miExcluir = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miMenuPrincipal = new System.Windows.Forms.MenuItem();
            this.tcContas = new System.Windows.Forms.TabControl();
            this.tpLista = new System.Windows.Forms.TabPage();
            this.grContas = new System.Windows.Forms.DataGrid();
            this.cmContas = new System.Windows.Forms.ContextMenu();
            this.cmEditar = new System.Windows.Forms.MenuItem();
            this.cmInserir = new System.Windows.Forms.MenuItem();
            this.cmExcluir = new System.Windows.Forms.MenuItem();
            this.tpEdicao = new System.Windows.Forms.TabPage();
            this.nuLimite = new NumericTextBox.NumericTextBox();
            this.nuSaldoInicial = new NumericTextBox.NumericTextBox();
            this.lblSaldoInicial = new System.Windows.Forms.Label();
            this.cbTipoConta = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLimite = new System.Windows.Forms.Label();
            this.cbForaUso = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btSalvar = new System.Windows.Forms.Button();
            this.tbDescricao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ipContas = new Microsoft.WindowsCE.Forms.InputPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nuVencimento = new System.Windows.Forms.NumericUpDown();
            this.nuDiaBom = new System.Windows.Forms.NumericUpDown();
            this.tcContas.SuspendLayout();
            this.tpLista.SuspendLayout();
            this.tpEdicao.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmContas
            // 
            this.mmContas.MenuItems.Add(this.miOpcoes);
            // 
            // miOpcoes
            // 
            this.miOpcoes.MenuItems.Add(this.miEditar);
            this.miOpcoes.MenuItems.Add(this.miInserir);
            this.miOpcoes.MenuItems.Add(this.miExcluir);
            this.miOpcoes.MenuItems.Add(this.menuItem5);
            this.miOpcoes.MenuItems.Add(this.miMenuPrincipal);
            this.miOpcoes.Text = "Opções";
            // 
            // miEditar
            // 
            this.miEditar.Text = "Editar";
            this.miEditar.Click += new System.EventHandler(this.miEditar_Click);
            // 
            // miInserir
            // 
            this.miInserir.Text = "Inserir";
            this.miInserir.Click += new System.EventHandler(this.miInserir_Click);
            // 
            // miExcluir
            // 
            this.miExcluir.Text = "Excluir";
            this.miExcluir.Click += new System.EventHandler(this.miExcluir_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // miMenuPrincipal
            // 
            this.miMenuPrincipal.Text = "Menu principal";
            this.miMenuPrincipal.Click += new System.EventHandler(this.miMenuPrincipal_Click);
            // 
            // tcContas
            // 
            this.tcContas.Controls.Add(this.tpLista);
            this.tcContas.Controls.Add(this.tpEdicao);
            this.tcContas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcContas.Location = new System.Drawing.Point(0, 0);
            this.tcContas.Name = "tcContas";
            this.tcContas.SelectedIndex = 0;
            this.tcContas.Size = new System.Drawing.Size(240, 268);
            this.tcContas.TabIndex = 0;
            this.tcContas.SelectedIndexChanged += new System.EventHandler(this.tcContas_SelectedIndexChanged);
            // 
            // tpLista
            // 
            this.tpLista.Controls.Add(this.grContas);
            this.tpLista.Location = new System.Drawing.Point(0, 0);
            this.tpLista.Name = "tpLista";
            this.tpLista.Size = new System.Drawing.Size(240, 245);
            this.tpLista.Text = "Lista";
            // 
            // grContas
            // 
            this.grContas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grContas.ContextMenu = this.cmContas;
            this.grContas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grContas.Location = new System.Drawing.Point(0, 0);
            this.grContas.Name = "grContas";
            this.grContas.Size = new System.Drawing.Size(240, 245);
            this.grContas.TabIndex = 0;
            this.grContas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grContas_MouseDown);
            // 
            // cmContas
            // 
            this.cmContas.MenuItems.Add(this.cmEditar);
            this.cmContas.MenuItems.Add(this.cmInserir);
            this.cmContas.MenuItems.Add(this.cmExcluir);
            // 
            // cmEditar
            // 
            this.cmEditar.Text = "Editar";
            this.cmEditar.Click += new System.EventHandler(this.miEditar_Click);
            // 
            // cmInserir
            // 
            this.cmInserir.Text = "Inserir";
            this.cmInserir.Click += new System.EventHandler(this.miInserir_Click);
            // 
            // cmExcluir
            // 
            this.cmExcluir.Text = "Excluir";
            this.cmExcluir.Click += new System.EventHandler(this.miExcluir_Click);
            // 
            // tpEdicao
            // 
            this.tpEdicao.Controls.Add(this.nuDiaBom);
            this.tpEdicao.Controls.Add(this.nuVencimento);
            this.tpEdicao.Controls.Add(this.label7);
            this.tpEdicao.Controls.Add(this.label6);
            this.tpEdicao.Controls.Add(this.nuLimite);
            this.tpEdicao.Controls.Add(this.nuSaldoInicial);
            this.tpEdicao.Controls.Add(this.lblSaldoInicial);
            this.tpEdicao.Controls.Add(this.cbTipoConta);
            this.tpEdicao.Controls.Add(this.label4);
            this.tpEdicao.Controls.Add(this.lblLimite);
            this.tpEdicao.Controls.Add(this.cbForaUso);
            this.tpEdicao.Controls.Add(this.label3);
            this.tpEdicao.Controls.Add(this.btCancelar);
            this.tpEdicao.Controls.Add(this.btSalvar);
            this.tpEdicao.Controls.Add(this.tbDescricao);
            this.tpEdicao.Controls.Add(this.label2);
            this.tpEdicao.Location = new System.Drawing.Point(0, 0);
            this.tpEdicao.Name = "tpEdicao";
            this.tpEdicao.Size = new System.Drawing.Size(240, 245);
            this.tpEdicao.Text = "Edição";
            this.tpEdicao.Click += new System.EventHandler(this.miEditar_Click);
            // 
            // nuLimite
            // 
            this.nuLimite.Location = new System.Drawing.Point(14, 125);
            this.nuLimite.Name = "nuLimite";
            this.nuLimite.Size = new System.Drawing.Size(100, 21);
            this.nuLimite.TabIndex = 2;
            this.nuLimite.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
            // 
            // nuSaldoInicial
            // 
            this.nuSaldoInicial.Location = new System.Drawing.Point(128, 125);
            this.nuSaldoInicial.Name = "nuSaldoInicial";
            this.nuSaldoInicial.Size = new System.Drawing.Size(100, 21);
            this.nuSaldoInicial.TabIndex = 3;
            this.nuSaldoInicial.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
            this.nuSaldoInicial.LostFocus += new System.EventHandler(this.nuSaldoInicial_LostFocus);
            this.nuSaldoInicial.GotFocus += new System.EventHandler(this.nuSaldoInicial_GotFocus);
            // 
            // lblSaldoInicial
            // 
            this.lblSaldoInicial.Location = new System.Drawing.Point(128, 109);
            this.lblSaldoInicial.Name = "lblSaldoInicial";
            this.lblSaldoInicial.Size = new System.Drawing.Size(100, 20);
            this.lblSaldoInicial.Text = "Saldo inicial:";
            // 
            // cbTipoConta
            // 
            this.cbTipoConta.Location = new System.Drawing.Point(12, 84);
            this.cbTipoConta.Name = "cbTipoConta";
            this.cbTipoConta.Size = new System.Drawing.Size(216, 22);
            this.cbTipoConta.TabIndex = 1;
            this.cbTipoConta.SelectedIndexChanged += new System.EventHandler(this.cbTipoConta_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Tipo de conta:";
            // 
            // lblLimite
            // 
            this.lblLimite.Location = new System.Drawing.Point(12, 109);
            this.lblLimite.Name = "lblLimite";
            this.lblLimite.Size = new System.Drawing.Size(100, 20);
            this.lblLimite.Text = "Limite:";
            // 
            // cbForaUso
            // 
            this.cbForaUso.Location = new System.Drawing.Point(14, 187);
            this.cbForaUso.Name = "cbForaUso";
            this.cbForaUso.Size = new System.Drawing.Size(100, 20);
            this.cbForaUso.TabIndex = 6;
            this.cbForaUso.Text = "Fora de uso";
            this.cbForaUso.CheckStateChanged += new System.EventHandler(this.cbForaUso_CheckStateChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 20);
            this.label3.Text = "Contas";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(89, 213);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(72, 20);
            this.btCancelar.TabIndex = 8;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click_1);
            // 
            // btSalvar
            // 
            this.btSalvar.Location = new System.Drawing.Point(12, 213);
            this.btSalvar.Name = "btSalvar";
            this.btSalvar.Size = new System.Drawing.Size(72, 20);
            this.btSalvar.TabIndex = 7;
            this.btSalvar.Text = "Salvar";
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click_1);
            // 
            // tbDescricao
            // 
            this.tbDescricao.Location = new System.Drawing.Point(12, 47);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(216, 21);
            this.tbDescricao.TabIndex = 0;
            this.tbDescricao.LostFocus += new System.EventHandler(this.tbDescricao_LostFocus_1);
            this.tbDescricao.GotFocus += new System.EventHandler(this.tbDescricao_GotFocus_1);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Descrição:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Vencimento:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(128, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.Text = "Dia bom:";
            // 
            // nuVencimento
            // 
            this.nuVencimento.Location = new System.Drawing.Point(14, 159);
            this.nuVencimento.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nuVencimento.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuVencimento.Name = "nuVencimento";
            this.nuVencimento.Size = new System.Drawing.Size(100, 22);
            this.nuVencimento.TabIndex = 4;
            this.nuVencimento.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nuDiaBom
            // 
            this.nuDiaBom.Location = new System.Drawing.Point(128, 159);
            this.nuDiaBom.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nuDiaBom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuDiaBom.Name = "nuDiaBom";
            this.nuDiaBom.Size = new System.Drawing.Size(100, 22);
            this.nuDiaBom.TabIndex = 5;
            this.nuDiaBom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Contas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tcContas);
            this.Menu = this.mmContas;
            this.Name = "Contas";
            this.Text = "Contas";
            this.Load += new System.EventHandler(this.Contas_Load);
            this.tcContas.ResumeLayout(false);
            this.tpLista.ResumeLayout(false);
            this.tpEdicao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcContas;
        private System.Windows.Forms.TabPage tpLista;
        private System.Windows.Forms.TabPage tpEdicao;
        private Microsoft.WindowsCE.Forms.InputPanel ipContas;
        private System.Windows.Forms.ContextMenu cmContas;
        private System.Windows.Forms.MenuItem miOpcoes;
        private System.Windows.Forms.MenuItem miEditar;
        private System.Windows.Forms.MenuItem miInserir;
        private System.Windows.Forms.MenuItem miExcluir;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miMenuPrincipal;
        private System.Windows.Forms.MenuItem cmEditar;
        private System.Windows.Forms.MenuItem cmInserir;
        private System.Windows.Forms.MenuItem cmExcluir;
        private System.Windows.Forms.DataGrid grContas;
        private System.Windows.Forms.Label lblSaldoInicial;
        private System.Windows.Forms.ComboBox cbTipoConta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLimite;
        private System.Windows.Forms.CheckBox cbForaUso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btSalvar;
        private System.Windows.Forms.TextBox tbDescricao;
        private System.Windows.Forms.Label label2;
        private NumericTextBox.NumericTextBox nuSaldoInicial;
        private NumericTextBox.NumericTextBox nuLimite;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nuDiaBom;
        private System.Windows.Forms.NumericUpDown nuVencimento;
    }
}