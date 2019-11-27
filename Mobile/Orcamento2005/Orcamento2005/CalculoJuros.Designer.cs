namespace Orcamento2005
{
    partial class CalculoJuros
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mnCalculo;

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
            this.mnCalculo = new System.Windows.Forms.MainMenu();
            this.miMenuPrincipal = new System.Windows.Forms.MenuItem();
            this.miCalcular = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miPrincipal = new System.Windows.Forms.MenuItem();
            this.tbDados = new System.Windows.Forms.TabControl();
            this.pc12 = new System.Windows.Forms.TabPage();
            this.lb12 = new System.Windows.Forms.ListBox();
            this.pc24 = new System.Windows.Forms.TabPage();
            this.lb24 = new System.Windows.Forms.ListBox();
            this.pc36 = new System.Windows.Forms.TabPage();
            this.lb36 = new System.Windows.Forms.ListBox();
            this.pc48 = new System.Windows.Forms.TabPage();
            this.lb48 = new System.Windows.Forms.ListBox();
            this.pc60 = new System.Windows.Forms.TabPage();
            this.lb60 = new System.Windows.Forms.ListBox();
            this.pnSuperior = new System.Windows.Forms.Panel();
            this.nuTaxa = new NumericTextBox.NumericTextBox();
            this.nuValor = new NumericTextBox.NumericTextBox();
            this.btCalcular = new System.Windows.Forms.Button();
            this.lbTaxa = new System.Windows.Forms.Label();
            this.lbValor = new System.Windows.Forms.Label();
            this.ipCalculoJuros = new Microsoft.WindowsCE.Forms.InputPanel();
            this.tbDados.SuspendLayout();
            this.pc12.SuspendLayout();
            this.pc24.SuspendLayout();
            this.pc36.SuspendLayout();
            this.pc48.SuspendLayout();
            this.pc60.SuspendLayout();
            this.pnSuperior.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnCalculo
            // 
            this.mnCalculo.MenuItems.Add(this.miMenuPrincipal);
            // 
            // miMenuPrincipal
            // 
            this.miMenuPrincipal.MenuItems.Add(this.miCalcular);
            this.miMenuPrincipal.MenuItems.Add(this.menuItem2);
            this.miMenuPrincipal.MenuItems.Add(this.miPrincipal);
            this.miMenuPrincipal.Text = "Opções";
            // 
            // miCalcular
            // 
            this.miCalcular.Text = "Calcular";
            this.miCalcular.Click += new System.EventHandler(this.miCalcular_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // miPrincipal
            // 
            this.miPrincipal.Text = "Menu principal";
            this.miPrincipal.Click += new System.EventHandler(this.miPrincipal_Click);
            // 
            // tbDados
            // 
            this.tbDados.Controls.Add(this.pc12);
            this.tbDados.Controls.Add(this.pc24);
            this.tbDados.Controls.Add(this.pc36);
            this.tbDados.Controls.Add(this.pc48);
            this.tbDados.Controls.Add(this.pc60);
            this.tbDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
            this.tbDados.Location = new System.Drawing.Point(0, 0);
            this.tbDados.Name = "tbDados";
            this.tbDados.SelectedIndex = 0;
            this.tbDados.Size = new System.Drawing.Size(240, 268);
            this.tbDados.TabIndex = 1;
            // 
            // pc12
            // 
            this.pc12.Controls.Add(this.lb12);
            this.pc12.Location = new System.Drawing.Point(0, 0);
            this.pc12.Name = "pc12";
            this.pc12.Size = new System.Drawing.Size(240, 245);
            this.pc12.Text = "12 X";
            // 
            // lb12
            // 
            this.lb12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb12.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Items.Add("T: 1,00  |P: 500,00  |Tt: 12.000,00");
            this.lb12.Location = new System.Drawing.Point(0, 47);
            this.lb12.Name = "lb12";
            this.lb12.Size = new System.Drawing.Size(240, 198);
            this.lb12.TabIndex = 0;
            // 
            // pc24
            // 
            this.pc24.Controls.Add(this.lb24);
            this.pc24.Location = new System.Drawing.Point(0, 0);
            this.pc24.Name = "pc24";
            this.pc24.Size = new System.Drawing.Size(240, 245);
            this.pc24.Text = "24 X";
            // 
            // lb24
            // 
            this.lb24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb24.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb24.Location = new System.Drawing.Point(0, 47);
            this.lb24.Name = "lb24";
            this.lb24.Size = new System.Drawing.Size(240, 198);
            this.lb24.TabIndex = 0;
            // 
            // pc36
            // 
            this.pc36.Controls.Add(this.lb36);
            this.pc36.Location = new System.Drawing.Point(0, 0);
            this.pc36.Name = "pc36";
            this.pc36.Size = new System.Drawing.Size(240, 245);
            this.pc36.Text = "36 X";
            // 
            // lb36
            // 
            this.lb36.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb36.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb36.Location = new System.Drawing.Point(0, 47);
            this.lb36.Name = "lb36";
            this.lb36.Size = new System.Drawing.Size(240, 198);
            this.lb36.TabIndex = 0;
            // 
            // pc48
            // 
            this.pc48.Controls.Add(this.lb48);
            this.pc48.Location = new System.Drawing.Point(0, 0);
            this.pc48.Name = "pc48";
            this.pc48.Size = new System.Drawing.Size(240, 245);
            this.pc48.Text = "48 X";
            // 
            // lb48
            // 
            this.lb48.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb48.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb48.Location = new System.Drawing.Point(0, 47);
            this.lb48.Name = "lb48";
            this.lb48.Size = new System.Drawing.Size(240, 198);
            this.lb48.TabIndex = 0;
            // 
            // pc60
            // 
            this.pc60.Controls.Add(this.lb60);
            this.pc60.Location = new System.Drawing.Point(0, 0);
            this.pc60.Name = "pc60";
            this.pc60.Size = new System.Drawing.Size(240, 245);
            this.pc60.Text = "60 X";
            // 
            // lb60
            // 
            this.lb60.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb60.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Items.Add("Tx: 1,00%   | 12x  R$   500,00  |  Tt: 12.000,00");
            this.lb60.Location = new System.Drawing.Point(0, 47);
            this.lb60.Name = "lb60";
            this.lb60.Size = new System.Drawing.Size(240, 198);
            this.lb60.TabIndex = 0;
            // 
            // pnSuperior
            // 
            this.pnSuperior.BackColor = System.Drawing.Color.Gainsboro;
            this.pnSuperior.Controls.Add(this.nuTaxa);
            this.pnSuperior.Controls.Add(this.nuValor);
            this.pnSuperior.Controls.Add(this.btCalcular);
            this.pnSuperior.Controls.Add(this.lbTaxa);
            this.pnSuperior.Controls.Add(this.lbValor);
            this.pnSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnSuperior.Name = "pnSuperior";
            this.pnSuperior.Size = new System.Drawing.Size(240, 47);
            // 
            // nuTaxa
            // 
            this.nuTaxa.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nuTaxa.IntValue = 0;
            this.nuTaxa.Location = new System.Drawing.Point(85, 21);
            this.nuTaxa.Name = "nuTaxa";
            this.nuTaxa.Size = new System.Drawing.Size(67, 21);
            this.nuTaxa.TabIndex = 6;
            this.nuTaxa.Text = "0";
            this.nuTaxa.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
            this.nuTaxa.LostFocus += new System.EventHandler(this.nuValor_LostFocus);
            this.nuTaxa.GotFocus += new System.EventHandler(this.nuValor_GotFocus);
            // 
            // nuValor
            // 
            this.nuValor.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nuValor.IntValue = 0;
            this.nuValor.Location = new System.Drawing.Point(7, 21);
            this.nuValor.Name = "nuValor";
            this.nuValor.Size = new System.Drawing.Size(72, 21);
            this.nuValor.TabIndex = 5;
            this.nuValor.Text = "0";
            this.nuValor.Type = NumericTextBox.NumericTextBox.NTBType.Numbers;
            this.nuValor.LostFocus += new System.EventHandler(this.nuValor_LostFocus);
            this.nuValor.GotFocus += new System.EventHandler(this.nuValor_GotFocus);
            // 
            // btCalcular
            // 
            this.btCalcular.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btCalcular.Location = new System.Drawing.Point(158, 21);
            this.btCalcular.Name = "btCalcular";
            this.btCalcular.Size = new System.Drawing.Size(72, 20);
            this.btCalcular.TabIndex = 0;
            this.btCalcular.Text = "Calcular";
            this.btCalcular.Click += new System.EventHandler(this.btCalcular_Click);
            // 
            // lbTaxa
            // 
            this.lbTaxa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbTaxa.Location = new System.Drawing.Point(85, 6);
            this.lbTaxa.Name = "lbTaxa";
            this.lbTaxa.Size = new System.Drawing.Size(75, 20);
            this.lbTaxa.Text = "Taxa (inicial)";
            // 
            // lbValor
            // 
            this.lbValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbValor.Location = new System.Drawing.Point(6, 6);
            this.lbValor.Name = "lbValor";
            this.lbValor.Size = new System.Drawing.Size(100, 20);
            this.lbValor.Text = "Valor";
            // 
            // CalculoJuros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnSuperior);
            this.Controls.Add(this.tbDados);
            this.Menu = this.mnCalculo;
            this.Name = "CalculoJuros";
            this.Text = "Cálculo de Juros";
            this.Load += new System.EventHandler(this.CalculoJuros_Load);
            this.tbDados.ResumeLayout(false);
            this.pc12.ResumeLayout(false);
            this.pc24.ResumeLayout(false);
            this.pc36.ResumeLayout(false);
            this.pc48.ResumeLayout(false);
            this.pc60.ResumeLayout(false);
            this.pnSuperior.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbDados;
        private System.Windows.Forms.TabPage pc12;
        private System.Windows.Forms.ListBox lb12;
        private System.Windows.Forms.TabPage pc24;
        private System.Windows.Forms.ListBox lb24;
        private System.Windows.Forms.TabPage pc36;
        private System.Windows.Forms.ListBox lb36;
        private System.Windows.Forms.TabPage pc48;
        private System.Windows.Forms.ListBox lb48;
        private System.Windows.Forms.TabPage pc60;
        private System.Windows.Forms.ListBox lb60;
        private System.Windows.Forms.MenuItem miMenuPrincipal;
        private System.Windows.Forms.MenuItem miCalcular;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miPrincipal;
        private System.Windows.Forms.Panel pnSuperior;
        private NumericTextBox.NumericTextBox nuTaxa;
        private NumericTextBox.NumericTextBox nuValor;
        private System.Windows.Forms.Button btCalcular;
        private System.Windows.Forms.Label lbTaxa;
        private System.Windows.Forms.Label lbValor;
        private Microsoft.WindowsCE.Forms.InputPanel ipCalculoJuros;
    }
}