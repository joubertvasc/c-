namespace Orcamento2005
{
    partial class CentroCusto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmCentroCusto;

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
            this.mmCentroCusto = new System.Windows.Forms.MainMenu();
            this.cmCentroCusto = new System.Windows.Forms.ContextMenu();
            this.ipCentroCusto = new Microsoft.WindowsCE.Forms.InputPanel();
            this.miOpcoes = new System.Windows.Forms.MenuItem();
            this.miEditar = new System.Windows.Forms.MenuItem();
            this.miInserir = new System.Windows.Forms.MenuItem();
            this.miExcluir = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miMenuPrincipal = new System.Windows.Forms.MenuItem();
            this.cmEditar = new System.Windows.Forms.MenuItem();
            this.cmInserir = new System.Windows.Forms.MenuItem();
            this.cmExcluir = new System.Windows.Forms.MenuItem();
            this.tbCentroCusto = new System.Windows.Forms.TabControl();
            this.tbLista = new System.Windows.Forms.TabPage();
            this.tpEdicao = new System.Windows.Forms.TabPage();
            this.grCentroCusto = new System.Windows.Forms.DataGrid();
            this.cbForaUso = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btSalvar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCentroCusto.SuspendLayout();
            this.tbLista.SuspendLayout();
            this.tpEdicao.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmCentroCusto
            // 
            this.mmCentroCusto.MenuItems.Add(this.miOpcoes);
            // 
            // cmCentroCusto
            // 
            this.cmCentroCusto.MenuItems.Add(this.cmEditar);
            this.cmCentroCusto.MenuItems.Add(this.cmInserir);
            this.cmCentroCusto.MenuItems.Add(this.cmExcluir);
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
            // tbCentroCusto
            // 
            this.tbCentroCusto.Controls.Add(this.tbLista);
            this.tbCentroCusto.Controls.Add(this.tpEdicao);
            this.tbCentroCusto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCentroCusto.Location = new System.Drawing.Point(0, 0);
            this.tbCentroCusto.Name = "tbCentroCusto";
            this.tbCentroCusto.SelectedIndex = 0;
            this.tbCentroCusto.Size = new System.Drawing.Size(240, 268);
            this.tbCentroCusto.TabIndex = 0;
            this.tbCentroCusto.SelectedIndexChanged += new System.EventHandler(this.tbCentroCusto_SelectedIndexChanged);
            // 
            // tbLista
            // 
            this.tbLista.Controls.Add(this.grCentroCusto);
            this.tbLista.Location = new System.Drawing.Point(0, 0);
            this.tbLista.Name = "tbLista";
            this.tbLista.Size = new System.Drawing.Size(240, 245);
            this.tbLista.Text = "Lista";
            // 
            // tpEdicao
            // 
            this.tpEdicao.Controls.Add(this.cbForaUso);
            this.tpEdicao.Controls.Add(this.label3);
            this.tpEdicao.Controls.Add(this.btCancelar);
            this.tpEdicao.Controls.Add(this.btSalvar);
            this.tpEdicao.Controls.Add(this.textBox1);
            this.tpEdicao.Controls.Add(this.label2);
            this.tpEdicao.Location = new System.Drawing.Point(0, 0);
            this.tpEdicao.Name = "tpEdicao";
            this.tpEdicao.Size = new System.Drawing.Size(240, 245);
            this.tpEdicao.Text = "Edição";
            // 
            // grCentroCusto
            // 
            this.grCentroCusto.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grCentroCusto.ContextMenu = this.cmCentroCusto;
            this.grCentroCusto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grCentroCusto.Location = new System.Drawing.Point(0, 0);
            this.grCentroCusto.Name = "grCentroCusto";
            this.grCentroCusto.Size = new System.Drawing.Size(240, 245);
            this.grCentroCusto.TabIndex = 0;
            this.grCentroCusto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grCentroCusto_MouseDown);
            // 
            // cbForaUso
            // 
            this.cbForaUso.Location = new System.Drawing.Point(13, 80);
            this.cbForaUso.Name = "cbForaUso";
            this.cbForaUso.Size = new System.Drawing.Size(100, 20);
            this.cbForaUso.TabIndex = 6;
            this.cbForaUso.Text = "Fora de uso";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 20);
            this.label3.Text = "Centro de Custo";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(88, 109);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(72, 20);
            this.btCancelar.TabIndex = 8;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click_1);
            // 
            // btSalvar
            // 
            this.btSalvar.Location = new System.Drawing.Point(11, 109);
            this.btSalvar.Name = "btSalvar";
            this.btSalvar.Size = new System.Drawing.Size(72, 20);
            this.btSalvar.TabIndex = 9;
            this.btSalvar.Text = "Salvar";
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(216, 21);
            this.textBox1.TabIndex = 10;
            this.textBox1.LostFocus += new System.EventHandler(this.textBox1_LostFocus_1);
            this.textBox1.GotFocus += new System.EventHandler(this.textBox1_GotFocus_1);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Descrição:";
            // 
            // CentroCusto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tbCentroCusto);
            this.Menu = this.mmCentroCusto;
            this.Name = "CentroCusto";
            this.Text = "CentroCusto";
            this.Load += new System.EventHandler(this.CentroCusto_Load);
            this.tbCentroCusto.ResumeLayout(false);
            this.tbLista.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenu cmCentroCusto;
        private System.Windows.Forms.MenuItem cmEditar;
        private System.Windows.Forms.MenuItem cmInserir;
        private System.Windows.Forms.MenuItem cmExcluir;
        private Microsoft.WindowsCE.Forms.InputPanel ipCentroCusto;
        private System.Windows.Forms.TabControl tbCentroCusto;
        private System.Windows.Forms.TabPage tbLista;
        private System.Windows.Forms.TabPage tpEdicao;
        private System.Windows.Forms.DataGrid grCentroCusto;
        private System.Windows.Forms.CheckBox cbForaUso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btSalvar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}