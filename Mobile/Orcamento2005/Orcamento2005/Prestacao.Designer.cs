namespace Orcamento2005
{
    partial class Prestacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mnPrestacao;

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
            this.mnPrestacao = new System.Windows.Forms.MainMenu();
            this.miOpcoes = new System.Windows.Forms.MenuItem();
            this.miEditar = new System.Windows.Forms.MenuItem();
            this.miInserir = new System.Windows.Forms.MenuItem();
            this.miExcluir = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miMenuPrincipal = new System.Windows.Forms.MenuItem();
            this.tcPrestacao = new System.Windows.Forms.TabControl();
            this.tpLista = new System.Windows.Forms.TabPage();
            this.grPrestacao = new System.Windows.Forms.DataGrid();
            this.cmPrestacao = new System.Windows.Forms.ContextMenu();
            this.cmEditar = new System.Windows.Forms.MenuItem();
            this.cmInserir = new System.Windows.Forms.MenuItem();
            this.cmExcluir = new System.Windows.Forms.MenuItem();
            this.tpEdicao = new System.Windows.Forms.TabPage();
            this.vlParcela = new NumericTextBox.NumericTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dtPrimeira = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.nuParcelas = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.vlPrestacaoTotal = new NumericTextBox.NumericTextBox();
            this.nuDiaVenc = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btParcelas = new System.Windows.Forms.Button();
            this.dtAquisicao = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.cbTipoMovim = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbCentroCusto = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.edDescricao = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tpParcelas = new System.Windows.Forms.TabPage();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btProximo = new System.Windows.Forms.Button();
            this.btConfirmar = new System.Windows.Forms.Button();
            this.btAnterior = new System.Windows.Forms.Button();
            this.pnlClient = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ipPrestacao = new Microsoft.WindowsCE.Forms.InputPanel();
            this.tcPrestacao.SuspendLayout();
            this.tpLista.SuspendLayout();
            this.tpEdicao.SuspendLayout();
            this.tpParcelas.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnPrestacao
            // 
            this.mnPrestacao.MenuItems.Add(this.miOpcoes);
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
            this.miMenuPrincipal.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // tcPrestacao
            // 
            this.tcPrestacao.Controls.Add(this.tpLista);
            this.tcPrestacao.Controls.Add(this.tpEdicao);
            this.tcPrestacao.Controls.Add(this.tpParcelas);
            this.tcPrestacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPrestacao.Location = new System.Drawing.Point(0, 0);
            this.tcPrestacao.Name = "tcPrestacao";
            this.tcPrestacao.SelectedIndex = 0;
            this.tcPrestacao.Size = new System.Drawing.Size(240, 268);
            this.tcPrestacao.TabIndex = 0;
            this.tcPrestacao.SelectedIndexChanged += new System.EventHandler(this.tcPrestacao_SelectedIndexChanged);
            // 
            // tpLista
            // 
            this.tpLista.Controls.Add(this.grPrestacao);
            this.tpLista.Location = new System.Drawing.Point(0, 0);
            this.tpLista.Name = "tpLista";
            this.tpLista.Size = new System.Drawing.Size(240, 245);
            this.tpLista.Text = "Lista";
            // 
            // grPrestacao
            // 
            this.grPrestacao.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grPrestacao.ContextMenu = this.cmPrestacao;
            this.grPrestacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grPrestacao.Location = new System.Drawing.Point(0, 0);
            this.grPrestacao.Name = "grPrestacao";
            this.grPrestacao.Size = new System.Drawing.Size(240, 245);
            this.grPrestacao.TabIndex = 0;
            this.grPrestacao.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grPrestacao_MouseDown);
            // 
            // cmPrestacao
            // 
            this.cmPrestacao.MenuItems.Add(this.cmEditar);
            this.cmPrestacao.MenuItems.Add(this.cmInserir);
            this.cmPrestacao.MenuItems.Add(this.cmExcluir);
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
            this.tpEdicao.Controls.Add(this.vlParcela);
            this.tpEdicao.Controls.Add(this.label15);
            this.tpEdicao.Controls.Add(this.dtPrimeira);
            this.tpEdicao.Controls.Add(this.label14);
            this.tpEdicao.Controls.Add(this.nuParcelas);
            this.tpEdicao.Controls.Add(this.label13);
            this.tpEdicao.Controls.Add(this.vlPrestacaoTotal);
            this.tpEdicao.Controls.Add(this.nuDiaVenc);
            this.tpEdicao.Controls.Add(this.label10);
            this.tpEdicao.Controls.Add(this.label11);
            this.tpEdicao.Controls.Add(this.btCancelar);
            this.tpEdicao.Controls.Add(this.btParcelas);
            this.tpEdicao.Controls.Add(this.dtAquisicao);
            this.tpEdicao.Controls.Add(this.label12);
            this.tpEdicao.Controls.Add(this.cbTipoMovim);
            this.tpEdicao.Controls.Add(this.label8);
            this.tpEdicao.Controls.Add(this.cbCentroCusto);
            this.tpEdicao.Controls.Add(this.label9);
            this.tpEdicao.Controls.Add(this.edDescricao);
            this.tpEdicao.Controls.Add(this.label7);
            this.tpEdicao.Controls.Add(this.label6);
            this.tpEdicao.Location = new System.Drawing.Point(0, 0);
            this.tpEdicao.Name = "tpEdicao";
            this.tpEdicao.Size = new System.Drawing.Size(240, 245);
            this.tpEdicao.Text = "Edição";
            // 
            // vlParcela
            // 
            this.vlParcela.Location = new System.Drawing.Point(161, 74);
            this.vlParcela.Name = "vlParcela";
            this.vlParcela.Size = new System.Drawing.Size(71, 21);
            this.vlParcela.TabIndex = 3;
            this.vlParcela.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
            this.vlParcela.LostFocus += new System.EventHandler(this.vlParcela_LostFocus);
            this.vlParcela.GotFocus += new System.EventHandler(this.vlParcela_GotFocus);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(161, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 20);
            this.label15.Text = "Vl. parcela:";
            // 
            // dtPrimeira
            // 
            this.dtPrimeira.CustomFormat = "dd/MM/yy";
            this.dtPrimeira.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPrimeira.Location = new System.Drawing.Point(163, 119);
            this.dtPrimeira.Name = "dtPrimeira";
            this.dtPrimeira.Size = new System.Drawing.Size(69, 22);
            this.dtPrimeira.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(163, 103);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 17);
            this.label14.Text = "1ª Parcela:";
            // 
            // nuParcelas
            // 
            this.nuParcelas.Location = new System.Drawing.Point(85, 74);
            this.nuParcelas.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nuParcelas.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nuParcelas.Name = "nuParcelas";
            this.nuParcelas.Size = new System.Drawing.Size(70, 22);
            this.nuParcelas.TabIndex = 2;
            this.nuParcelas.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nuParcelas.ValueChanged += new System.EventHandler(this.nuParcelas_ValueChanged);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(85, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 14);
            this.label13.Text = "Parcelas:";
            // 
            // vlPrestacaoTotal
            // 
            this.vlPrestacaoTotal.Location = new System.Drawing.Point(3, 119);
            this.vlPrestacaoTotal.Name = "vlPrestacaoTotal";
            this.vlPrestacaoTotal.Size = new System.Drawing.Size(76, 21);
            this.vlPrestacaoTotal.TabIndex = 4;
            this.vlPrestacaoTotal.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
            this.vlPrestacaoTotal.LostFocus += new System.EventHandler(this.vlPrestacaoTotal_LostFocus);
            this.vlPrestacaoTotal.GotFocus += new System.EventHandler(this.vlPrestacaoTotal_GotFocus);
            // 
            // nuDiaVenc
            // 
            this.nuDiaVenc.Location = new System.Drawing.Point(3, 74);
            this.nuDiaVenc.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nuDiaVenc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuDiaVenc.Name = "nuDiaVenc";
            this.nuDiaVenc.Size = new System.Drawing.Size(76, 22);
            this.nuDiaVenc.TabIndex = 1;
            this.nuDiaVenc.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuDiaVenc.ValueChanged += new System.EventHandler(this.nuDiaVenc_ValueChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 14);
            this.label10.Text = "Valor total:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 14);
            this.label11.Text = "Dia venc.:";
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(85, 223);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(72, 20);
            this.btCancelar.TabIndex = 10;
            this.btCancelar.Text = "Cancelar";
            // 
            // btParcelas
            // 
            this.btParcelas.Location = new System.Drawing.Point(7, 223);
            this.btParcelas.Name = "btParcelas";
            this.btParcelas.Size = new System.Drawing.Size(72, 20);
            this.btParcelas.TabIndex = 9;
            this.btParcelas.Text = "Parcelas";
            this.btParcelas.Click += new System.EventHandler(this.btParcelas_Click);
            // 
            // dtAquisicao
            // 
            this.dtAquisicao.CustomFormat = "dd/MM/yy";
            this.dtAquisicao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAquisicao.Location = new System.Drawing.Point(85, 119);
            this.dtAquisicao.Name = "dtAquisicao";
            this.dtAquisicao.Size = new System.Drawing.Size(70, 22);
            this.dtAquisicao.TabIndex = 5;
            this.dtAquisicao.ValueChanged += new System.EventHandler(this.dtAquisicao_ValueChanged);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(85, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 17);
            this.label12.Text = "Aquisição:";
            // 
            // cbTipoMovim
            // 
            this.cbTipoMovim.Location = new System.Drawing.Point(3, 198);
            this.cbTipoMovim.Name = "cbTipoMovim";
            this.cbTipoMovim.Size = new System.Drawing.Size(230, 22);
            this.cbTipoMovim.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(234, 20);
            this.label8.Text = "Tipo de movimentação:";
            // 
            // cbCentroCusto
            // 
            this.cbCentroCusto.Location = new System.Drawing.Point(3, 160);
            this.cbCentroCusto.Name = "cbCentroCusto";
            this.cbCentroCusto.Size = new System.Drawing.Size(230, 22);
            this.cbCentroCusto.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(240, 17);
            this.label9.Text = "Centro de custo:";
            // 
            // edDescricao
            // 
            this.edDescricao.Location = new System.Drawing.Point(3, 36);
            this.edDescricao.Name = "edDescricao";
            this.edDescricao.Size = new System.Drawing.Size(230, 21);
            this.edDescricao.TabIndex = 0;
            this.edDescricao.LostFocus += new System.EventHandler(this.edDescricao_LostFocus);
            this.edDescricao.GotFocus += new System.EventHandler(this.edDescricao_GotFocus);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.Text = "Descrição:";
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 20);
            this.label6.Text = "Prestação/Empréstimo";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tpParcelas
            // 
            this.tpParcelas.Controls.Add(this.pnlFooter);
            this.tpParcelas.Controls.Add(this.pnlClient);
            this.tpParcelas.Controls.Add(this.pnlHeader);
            this.tpParcelas.Controls.Add(this.label1);
            this.tpParcelas.Location = new System.Drawing.Point(0, 0);
            this.tpParcelas.Name = "tpParcelas";
            this.tpParcelas.Size = new System.Drawing.Size(232, 242);
            this.tpParcelas.Text = "Parcelas";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btProximo);
            this.pnlFooter.Controls.Add(this.btConfirmar);
            this.pnlFooter.Controls.Add(this.btAnterior);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 220);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(232, 22);
            // 
            // btProximo
            // 
            this.btProximo.Location = new System.Drawing.Point(168, 0);
            this.btProximo.Name = "btProximo";
            this.btProximo.Size = new System.Drawing.Size(72, 20);
            this.btProximo.TabIndex = 2;
            this.btProximo.Text = "Próximo";
            this.btProximo.Click += new System.EventHandler(this.btProximo_Click);
            // 
            // btConfirmar
            // 
            this.btConfirmar.Location = new System.Drawing.Point(83, 0);
            this.btConfirmar.Name = "btConfirmar";
            this.btConfirmar.Size = new System.Drawing.Size(72, 20);
            this.btConfirmar.TabIndex = 1;
            this.btConfirmar.Text = "Confirmar";
            this.btConfirmar.Click += new System.EventHandler(this.btConfirmar_Click);
            // 
            // btAnterior
            // 
            this.btAnterior.Location = new System.Drawing.Point(0, 0);
            this.btAnterior.Name = "btAnterior";
            this.btAnterior.Size = new System.Drawing.Size(72, 20);
            this.btAnterior.TabIndex = 0;
            this.btAnterior.Text = "Anterior";
            this.btAnterior.Click += new System.EventHandler(this.btAnterior_Click);
            // 
            // pnlClient
            // 
            this.pnlClient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlClient.Location = new System.Drawing.Point(0, 44);
            this.pnlClient.Name = "pnlClient";
            this.pnlClient.Size = new System.Drawing.Size(232, 178);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.label5);
            this.pnlHeader.Controls.Add(this.label4);
            this.pnlHeader.Controls.Add(this.label3);
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 20);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(232, 24);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(184, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 24);
            this.label5.Text = "Cheque";
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(106, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 24);
            this.label4.Text = "Valor";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(30, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 24);
            this.label3.Text = "Data";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 24);
            this.label2.Text = "Nº";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 20);
            this.label1.Text = "Parcelas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Prestacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tcPrestacao);
            this.Menu = this.mnPrestacao;
            this.Name = "Prestacao";
            this.Text = "Prestações";
            this.Load += new System.EventHandler(this.Prestacao_Load);
            this.tcPrestacao.ResumeLayout(false);
            this.tpLista.ResumeLayout(false);
            this.tpEdicao.ResumeLayout(false);
            this.tpParcelas.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcPrestacao;
        private System.Windows.Forms.TabPage tpLista;
        private System.Windows.Forms.TabPage tpParcelas;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlClient;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btProximo;
        private System.Windows.Forms.Button btConfirmar;
        private System.Windows.Forms.Button btAnterior;
        private System.Windows.Forms.TabPage tpEdicao;
        private System.Windows.Forms.DataGrid grPrestacao;
        private System.Windows.Forms.TextBox edDescricao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbTipoMovim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbCentroCusto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtAquisicao;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btParcelas;
        private NumericTextBox.NumericTextBox vlPrestacaoTotal;
        private System.Windows.Forms.NumericUpDown nuDiaVenc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MenuItem miOpcoes;
        private System.Windows.Forms.MenuItem miEditar;
        private System.Windows.Forms.MenuItem miInserir;
        private System.Windows.Forms.MenuItem miExcluir;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miMenuPrincipal;
        private System.Windows.Forms.ContextMenu cmPrestacao;
        private System.Windows.Forms.MenuItem cmEditar;
        private System.Windows.Forms.MenuItem cmInserir;
        private System.Windows.Forms.MenuItem cmExcluir;
        private System.Windows.Forms.NumericUpDown nuParcelas;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtPrimeira;
        private System.Windows.Forms.Label label14;
        private Microsoft.WindowsCE.Forms.InputPanel ipPrestacao;
        private NumericTextBox.NumericTextBox vlParcela;
        private System.Windows.Forms.Label label15;
    }
}