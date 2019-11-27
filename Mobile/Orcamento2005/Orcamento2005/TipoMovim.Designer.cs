namespace Orcamento2005
{
    partial class TipoMovim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmTipoMovim;

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
            this.mmTipoMovim = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miEditar = new System.Windows.Forms.MenuItem();
            this.miInserir = new System.Windows.Forms.MenuItem();
            this.miExcluir = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miMenuPrincipal = new System.Windows.Forms.MenuItem();
            this.cmTipoMovim = new System.Windows.Forms.ContextMenu();
            this.cmEditar = new System.Windows.Forms.MenuItem();
            this.cmInserir = new System.Windows.Forms.MenuItem();
            this.cmExcluir = new System.Windows.Forms.MenuItem();
            this.ipTipoMovim = new Microsoft.WindowsCE.Forms.InputPanel();
            this.tcTipoMovim = new System.Windows.Forms.TabControl();
            this.tpLista = new System.Windows.Forms.TabPage();
            this.grTipoMovim = new System.Windows.Forms.DataGrid();
            this.tpEdicao = new System.Windows.Forms.TabPage();
            this.Natureza = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbDebito = new System.Windows.Forms.RadioButton();
            this.rbCredito = new System.Windows.Forms.RadioButton();
            this.cbFlForaUso = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btSalvar = new System.Windows.Forms.Button();
            this.edDescricao = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbForaUso = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tcTipoMovim.SuspendLayout();
            this.tpLista.SuspendLayout();
            this.tpEdicao.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmTipoMovim
            // 
            this.mmTipoMovim.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.miEditar);
            this.menuItem1.MenuItems.Add(this.miInserir);
            this.menuItem1.MenuItems.Add(this.miExcluir);
            this.menuItem1.MenuItems.Add(this.menuItem5);
            this.menuItem1.MenuItems.Add(this.miMenuPrincipal);
            this.menuItem1.Text = "Opções";
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
            // cmTipoMovim
            // 
            this.cmTipoMovim.MenuItems.Add(this.cmEditar);
            this.cmTipoMovim.MenuItems.Add(this.cmInserir);
            this.cmTipoMovim.MenuItems.Add(this.cmExcluir);
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
            // tcTipoMovim
            // 
            this.tcTipoMovim.Controls.Add(this.tpLista);
            this.tcTipoMovim.Controls.Add(this.tpEdicao);
            this.tcTipoMovim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTipoMovim.Location = new System.Drawing.Point(0, 0);
            this.tcTipoMovim.Name = "tcTipoMovim";
            this.tcTipoMovim.SelectedIndex = 0;
            this.tcTipoMovim.Size = new System.Drawing.Size(240, 268);
            this.tcTipoMovim.TabIndex = 0;
            this.tcTipoMovim.SelectedIndexChanged += new System.EventHandler(this.tcTipoMovim_SelectedIndexChanged);
            // 
            // tpLista
            // 
            this.tpLista.Controls.Add(this.grTipoMovim);
            this.tpLista.Location = new System.Drawing.Point(0, 0);
            this.tpLista.Name = "tpLista";
            this.tpLista.Size = new System.Drawing.Size(240, 245);
            this.tpLista.Text = "Lista";
            // 
            // grTipoMovim
            // 
            this.grTipoMovim.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grTipoMovim.ContextMenu = this.cmTipoMovim;
            this.grTipoMovim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grTipoMovim.Location = new System.Drawing.Point(0, 0);
            this.grTipoMovim.Name = "grTipoMovim";
            this.grTipoMovim.Size = new System.Drawing.Size(240, 245);
            this.grTipoMovim.TabIndex = 0;
            this.grTipoMovim.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grTipoMovim_MouseDown);
            // 
            // tpEdicao
            // 
            this.tpEdicao.Controls.Add(this.Natureza);
            this.tpEdicao.Controls.Add(this.panel2);
            this.tpEdicao.Controls.Add(this.cbFlForaUso);
            this.tpEdicao.Controls.Add(this.label5);
            this.tpEdicao.Controls.Add(this.btCancelar);
            this.tpEdicao.Controls.Add(this.btSalvar);
            this.tpEdicao.Controls.Add(this.edDescricao);
            this.tpEdicao.Controls.Add(this.label6);
            this.tpEdicao.Location = new System.Drawing.Point(0, 0);
            this.tpEdicao.Name = "tpEdicao";
            this.tpEdicao.Size = new System.Drawing.Size(240, 245);
            this.tpEdicao.Text = "Edição";
            // 
            // Natureza
            // 
            this.Natureza.Location = new System.Drawing.Point(19, 78);
            this.Natureza.Name = "Natureza";
            this.Natureza.Size = new System.Drawing.Size(100, 20);
            this.Natureza.Text = "Natureza";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbDebito);
            this.panel2.Controls.Add(this.rbCredito);
            this.panel2.Location = new System.Drawing.Point(13, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 48);
            // 
            // rbDebito
            // 
            this.rbDebito.Location = new System.Drawing.Point(112, 13);
            this.rbDebito.Name = "rbDebito";
            this.rbDebito.Size = new System.Drawing.Size(100, 20);
            this.rbDebito.TabIndex = 1;
            this.rbDebito.Text = "Débito";
            // 
            // rbCredito
            // 
            this.rbCredito.Location = new System.Drawing.Point(6, 13);
            this.rbCredito.Name = "rbCredito";
            this.rbCredito.Size = new System.Drawing.Size(100, 20);
            this.rbCredito.TabIndex = 0;
            this.rbCredito.Text = "Crédito";
            // 
            // cbFlForaUso
            // 
            this.cbFlForaUso.Location = new System.Drawing.Point(13, 136);
            this.cbFlForaUso.Name = "cbFlForaUso";
            this.cbFlForaUso.Size = new System.Drawing.Size(100, 20);
            this.cbFlForaUso.TabIndex = 6;
            this.cbFlForaUso.Text = "Fora de uso";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(232, 20);
            this.label5.Text = "Tipo de Movimentação";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(91, 162);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(72, 20);
            this.btCancelar.TabIndex = 9;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.Click += new System.EventHandler(this.button3_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.Location = new System.Drawing.Point(13, 162);
            this.btSalvar.Name = "btSalvar";
            this.btSalvar.Size = new System.Drawing.Size(72, 20);
            this.btSalvar.TabIndex = 10;
            this.btSalvar.Text = "Salvar";
            this.btSalvar.Click += new System.EventHandler(this.button4_Click);
            // 
            // edDescricao
            // 
            this.edDescricao.Location = new System.Drawing.Point(13, 54);
            this.edDescricao.Name = "edDescricao";
            this.edDescricao.Size = new System.Drawing.Size(216, 21);
            this.edDescricao.TabIndex = 11;
            this.edDescricao.LostFocus += new System.EventHandler(this.edDescricao_LostFocus);
            this.edDescricao.GotFocus += new System.EventHandler(this.edDescricao_GotFocus);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Descrição:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 39);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, -6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Natureza";
            // 
            // cbForaUso
            // 
            this.cbForaUso.Location = new System.Drawing.Point(8, 140);
            this.cbForaUso.Name = "cbForaUso";
            this.cbForaUso.Size = new System.Drawing.Size(100, 20);
            this.cbForaUso.TabIndex = 0;
            this.cbForaUso.Text = "Fora de uso";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 20);
            this.label3.Text = "Tipo de Movimentação";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(86, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 20);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancelar";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 3;
            this.button1.Text = "Salvar";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 58);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(216, 21);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Descrição:";
            // 
            // TipoMovim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tcTipoMovim);
            this.Menu = this.mmTipoMovim;
            this.Name = "TipoMovim";
            this.Text = "TipoMovim";
            this.Load += new System.EventHandler(this.TipoMovim_Load);
            this.tcTipoMovim.ResumeLayout(false);
            this.tpLista.ResumeLayout(false);
            this.tpEdicao.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miEditar;
        private System.Windows.Forms.MenuItem miInserir;
        private System.Windows.Forms.MenuItem miExcluir;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miMenuPrincipal;
        private System.Windows.Forms.ContextMenu cmTipoMovim;
        private Microsoft.WindowsCE.Forms.InputPanel ipTipoMovim;
        private System.Windows.Forms.MenuItem cmEditar;
        private System.Windows.Forms.MenuItem cmInserir;
        private System.Windows.Forms.MenuItem cmExcluir;
        private System.Windows.Forms.TabControl tcTipoMovim;
        private System.Windows.Forms.TabPage tpLista;
        private System.Windows.Forms.TabPage tpEdicao;
        private System.Windows.Forms.DataGrid grTipoMovim;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbDebito;
        private System.Windows.Forms.RadioButton rbCredito;
        private System.Windows.Forms.CheckBox cbFlForaUso;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btSalvar;
        private System.Windows.Forms.TextBox edDescricao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbForaUso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Natureza;
    }
}