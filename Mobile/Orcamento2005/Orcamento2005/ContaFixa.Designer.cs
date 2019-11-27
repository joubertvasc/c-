namespace Orcamento2005
{
    partial class ContaFixa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmContaFixa;

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
            this.mmContaFixa = new System.Windows.Forms.MainMenu();
            this.miOpcoes = new System.Windows.Forms.MenuItem();
            this.miEditar = new System.Windows.Forms.MenuItem();
            this.miInserir = new System.Windows.Forms.MenuItem();
            this.miExcluir = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miMenuPrincipal = new System.Windows.Forms.MenuItem();
            this.cmContaFixa = new System.Windows.Forms.ContextMenu();
            this.cmEditar = new System.Windows.Forms.MenuItem();
            this.cmInserir = new System.Windows.Forms.MenuItem();
            this.cmExcluir = new System.Windows.Forms.MenuItem();
            this.ipContaFixa = new Microsoft.WindowsCE.Forms.InputPanel();
            this.tcContaFixa = new System.Windows.Forms.TabControl();
            this.tpLista = new System.Windows.Forms.TabPage();
            this.grContaFixa = new System.Windows.Forms.DataGrid();
            this.tpEdicao = new System.Windows.Forms.TabPage();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btSalvar = new System.Windows.Forms.Button();
            this.flForaDeUso = new System.Windows.Forms.CheckBox();
            this.cbTipoMovim = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCentroCusto = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.vlConta = new NumericTextBox.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nuDiaVencimento = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.deDescricao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tcContaFixa.SuspendLayout();
            this.tpLista.SuspendLayout();
            this.tpEdicao.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmContaFixa
            // 
            this.mmContaFixa.MenuItems.Add(this.miOpcoes);
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
            // cmContaFixa
            // 
            this.cmContaFixa.MenuItems.Add(this.cmEditar);
            this.cmContaFixa.MenuItems.Add(this.cmInserir);
            this.cmContaFixa.MenuItems.Add(this.cmExcluir);
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
            // tcContaFixa
            // 
            this.tcContaFixa.Controls.Add(this.tpLista);
            this.tcContaFixa.Controls.Add(this.tpEdicao);
            this.tcContaFixa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcContaFixa.Location = new System.Drawing.Point(0, 0);
            this.tcContaFixa.Name = "tcContaFixa";
            this.tcContaFixa.SelectedIndex = 0;
            this.tcContaFixa.Size = new System.Drawing.Size(240, 268);
            this.tcContaFixa.TabIndex = 0;
            this.tcContaFixa.SelectedIndexChanged += new System.EventHandler(this.tcContaFixa_SelectedIndexChanged);
            // 
            // tpLista
            // 
            this.tpLista.Controls.Add(this.grContaFixa);
            this.tpLista.Location = new System.Drawing.Point(0, 0);
            this.tpLista.Name = "tpLista";
            this.tpLista.Size = new System.Drawing.Size(240, 245);
            this.tpLista.Text = "Lista";
            // 
            // grContaFixa
            // 
            this.grContaFixa.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grContaFixa.ContextMenu = this.cmContaFixa;
            this.grContaFixa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grContaFixa.Location = new System.Drawing.Point(0, 0);
            this.grContaFixa.Name = "grContaFixa";
            this.grContaFixa.Size = new System.Drawing.Size(240, 245);
            this.grContaFixa.TabIndex = 0;
            this.grContaFixa.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grContaFixa_MouseDown);
            // 
            // tpEdicao
            // 
            this.tpEdicao.Controls.Add(this.btCancelar);
            this.tpEdicao.Controls.Add(this.btSalvar);
            this.tpEdicao.Controls.Add(this.flForaDeUso);
            this.tpEdicao.Controls.Add(this.cbTipoMovim);
            this.tpEdicao.Controls.Add(this.label6);
            this.tpEdicao.Controls.Add(this.cbCentroCusto);
            this.tpEdicao.Controls.Add(this.label5);
            this.tpEdicao.Controls.Add(this.vlConta);
            this.tpEdicao.Controls.Add(this.label4);
            this.tpEdicao.Controls.Add(this.nuDiaVencimento);
            this.tpEdicao.Controls.Add(this.label3);
            this.tpEdicao.Controls.Add(this.deDescricao);
            this.tpEdicao.Controls.Add(this.label2);
            this.tpEdicao.Controls.Add(this.label1);
            this.tpEdicao.Location = new System.Drawing.Point(0, 0);
            this.tpEdicao.Name = "tpEdicao";
            this.tpEdicao.Size = new System.Drawing.Size(240, 245);
            this.tpEdicao.Text = "Edição";
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(88, 211);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(72, 20);
            this.btCancelar.TabIndex = 13;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.Location = new System.Drawing.Point(10, 211);
            this.btSalvar.Name = "btSalvar";
            this.btSalvar.Size = new System.Drawing.Size(72, 20);
            this.btSalvar.TabIndex = 12;
            this.btSalvar.Text = "Salvar";
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // flForaDeUso
            // 
            this.flForaDeUso.Location = new System.Drawing.Point(10, 185);
            this.flForaDeUso.Name = "flForaDeUso";
            this.flForaDeUso.Size = new System.Drawing.Size(223, 20);
            this.flForaDeUso.TabIndex = 11;
            this.flForaDeUso.Text = "Fora de Uso";
            // 
            // cbTipoMovim
            // 
            this.cbTipoMovim.Location = new System.Drawing.Point(10, 157);
            this.cbTipoMovim.Name = "cbTipoMovim";
            this.cbTipoMovim.Size = new System.Drawing.Size(223, 22);
            this.cbTipoMovim.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(223, 20);
            this.label6.Text = "Tipo de movimentação:";
            // 
            // cbCentroCusto
            // 
            this.cbCentroCusto.Location = new System.Drawing.Point(10, 118);
            this.cbCentroCusto.Name = "cbCentroCusto";
            this.cbCentroCusto.Size = new System.Drawing.Size(223, 22);
            this.cbCentroCusto.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "Centro de custo:";
            // 
            // vlConta
            // 
            this.vlConta.Location = new System.Drawing.Point(116, 79);
            this.vlConta.Name = "vlConta";
            this.vlConta.Size = new System.Drawing.Size(100, 21);
            this.vlConta.TabIndex = 6;
            this.vlConta.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(116, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Valor:";
            // 
            // nuDiaVencimento
            // 
            this.nuDiaVencimento.Location = new System.Drawing.Point(10, 79);
            this.nuDiaVencimento.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nuDiaVencimento.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuDiaVencimento.Name = "nuDiaVencimento";
            this.nuDiaVencimento.Size = new System.Drawing.Size(100, 22);
            this.nuDiaVencimento.TabIndex = 4;
            this.nuDiaVencimento.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Dia vencimento:";
            // 
            // deDescricao
            // 
            this.deDescricao.Location = new System.Drawing.Point(10, 41);
            this.deDescricao.Name = "deDescricao";
            this.deDescricao.Size = new System.Drawing.Size(223, 21);
            this.deDescricao.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Descrição:";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 20);
            this.label1.Text = "Débito Fixo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ContaFixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tcContaFixa);
            this.Menu = this.mmContaFixa;
            this.Name = "ContaFixa";
            this.Text = "Débitos Fixos";
            this.Load += new System.EventHandler(this.ContaFixa_Load);
            this.tcContaFixa.ResumeLayout(false);
            this.tpLista.ResumeLayout(false);
            this.tpEdicao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miOpcoes;
        private System.Windows.Forms.MenuItem miEditar;
        private System.Windows.Forms.MenuItem miInserir;
        private System.Windows.Forms.MenuItem miExcluir;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miMenuPrincipal;
        private System.Windows.Forms.ContextMenu cmContaFixa;
        private System.Windows.Forms.MenuItem cmEditar;
        private System.Windows.Forms.MenuItem cmInserir;
        private System.Windows.Forms.MenuItem cmExcluir;
        private Microsoft.WindowsCE.Forms.InputPanel ipContaFixa;
        private System.Windows.Forms.TabControl tcContaFixa;
        private System.Windows.Forms.TabPage tpLista;
        private System.Windows.Forms.TabPage tpEdicao;
        private System.Windows.Forms.DataGrid grContaFixa;
        private System.Windows.Forms.CheckBox flForaDeUso;
        private System.Windows.Forms.ComboBox cbTipoMovim;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCentroCusto;
        private System.Windows.Forms.Label label5;
        private NumericTextBox.NumericTextBox vlConta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nuDiaVencimento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox deDescricao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btSalvar;
    }
}