namespace ATMSistemas
{
    partial class CadChamado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mmChamado;

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
            this.mmChamado = new System.Windows.Forms.MainMenu();
            this.miConfirmar = new System.Windows.Forms.MenuItem();
            this.miSair = new System.Windows.Forms.MenuItem();
            this.lblNome = new System.Windows.Forms.Label();
            this.tbNome = new System.Windows.Forms.TextBox();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.tbEmpresa = new System.Windows.Forms.TextBox();
            this.lblCelContato = new System.Windows.Forms.Label();
            this.tbCelContato = new System.Windows.Forms.TextBox();
            this.lblCidade = new System.Windows.Forms.Label();
            this.tbCidade = new System.Windows.Forms.TextBox();
            this.lblEMail = new System.Windows.Forms.Label();
            this.tbEMail = new System.Windows.Forms.TextBox();
            this.lblLinha = new System.Windows.Forms.Label();
            this.lblIMSI = new System.Windows.Forms.Label();
            this.lblIMEI = new System.Windows.Forms.Label();
            this.lblVlLinha = new System.Windows.Forms.Label();
            this.lblVlIMSI = new System.Windows.Forms.Label();
            this.lblVlIMEI = new System.Windows.Forms.Label();
            this.tbDDD = new System.Windows.Forms.TextBox();
            this.lblBairro = new System.Windows.Forms.Label();
            this.tbBairro = new System.Windows.Forms.TextBox();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.cbMotivo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mmChamado
            // 
            this.mmChamado.MenuItems.Add(this.miConfirmar);
            this.mmChamado.MenuItems.Add(this.miSair);
            // 
            // miConfirmar
            // 
            this.miConfirmar.Text = "Confirmar";
            this.miConfirmar.Click += new System.EventHandler(this.miChamado_Click);
            // 
            // miSair
            // 
            this.miSair.Text = "Sair";
            this.miSair.Click += new System.EventHandler(this.miCancel_Click);
            // 
            // lblNome
            // 
            this.lblNome.Location = new System.Drawing.Point(3, 73);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(79, 20);
            this.lblNome.Text = "Nome:";
            this.lblNome.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbNome
            // 
            this.tbNome.Location = new System.Drawing.Point(88, 72);
            this.tbNome.MaxLength = 36;
            this.tbNome.Name = "tbNome";
            this.tbNome.Size = new System.Drawing.Size(149, 21);
            this.tbNome.TabIndex = 1;
            this.tbNome.LostFocus += new System.EventHandler(this.tbNome_LostFocus);
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Location = new System.Drawing.Point(3, 96);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(79, 20);
            this.lblEmpresa.Text = "Empresa:";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbEmpresa
            // 
            this.tbEmpresa.Location = new System.Drawing.Point(88, 95);
            this.tbEmpresa.MaxLength = 36;
            this.tbEmpresa.Name = "tbEmpresa";
            this.tbEmpresa.Size = new System.Drawing.Size(149, 21);
            this.tbEmpresa.TabIndex = 3;
            this.tbEmpresa.LostFocus += new System.EventHandler(this.tbNome_LostFocus);
            // 
            // lblCelContato
            // 
            this.lblCelContato.Location = new System.Drawing.Point(3, 119);
            this.lblCelContato.Name = "lblCelContato";
            this.lblCelContato.Size = new System.Drawing.Size(79, 20);
            this.lblCelContato.Text = "Cel. Contato:";
            this.lblCelContato.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbCelContato
            // 
            this.tbCelContato.Location = new System.Drawing.Point(130, 118);
            this.tbCelContato.MaxLength = 8;
            this.tbCelContato.Name = "tbCelContato";
            this.tbCelContato.Size = new System.Drawing.Size(107, 21);
            this.tbCelContato.TabIndex = 7;
            // 
            // lblCidade
            // 
            this.lblCidade.Location = new System.Drawing.Point(3, 142);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(79, 20);
            this.lblCidade.Text = "Cidade:";
            this.lblCidade.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbCidade
            // 
            this.tbCidade.Location = new System.Drawing.Point(88, 141);
            this.tbCidade.MaxLength = 36;
            this.tbCidade.Name = "tbCidade";
            this.tbCidade.Size = new System.Drawing.Size(149, 21);
            this.tbCidade.TabIndex = 9;
            this.tbCidade.LostFocus += new System.EventHandler(this.tbNome_LostFocus);
            // 
            // lblEMail
            // 
            this.lblEMail.Location = new System.Drawing.Point(3, 188);
            this.lblEMail.Name = "lblEMail";
            this.lblEMail.Size = new System.Drawing.Size(79, 20);
            this.lblEMail.Text = "E-Mail:";
            this.lblEMail.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbEMail
            // 
            this.tbEMail.Location = new System.Drawing.Point(88, 187);
            this.tbEMail.MaxLength = 36;
            this.tbEMail.Name = "tbEMail";
            this.tbEMail.Size = new System.Drawing.Size(149, 21);
            this.tbEMail.TabIndex = 13;
            // 
            // lblLinha
            // 
            this.lblLinha.Location = new System.Drawing.Point(3, 24);
            this.lblLinha.Name = "lblLinha";
            this.lblLinha.Size = new System.Drawing.Size(79, 15);
            this.lblLinha.Text = "Linha: ";
            this.lblLinha.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIMSI
            // 
            this.lblIMSI.Location = new System.Drawing.Point(3, 39);
            this.lblIMSI.Name = "lblIMSI";
            this.lblIMSI.Size = new System.Drawing.Size(79, 15);
            this.lblIMSI.Text = "IMSI:";
            this.lblIMSI.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIMEI
            // 
            this.lblIMEI.Location = new System.Drawing.Point(3, 54);
            this.lblIMEI.Name = "lblIMEI";
            this.lblIMEI.Size = new System.Drawing.Size(79, 15);
            this.lblIMEI.Text = "IMEI:";
            this.lblIMEI.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVlLinha
            // 
            this.lblVlLinha.Location = new System.Drawing.Point(88, 24);
            this.lblVlLinha.Name = "lblVlLinha";
            this.lblVlLinha.Size = new System.Drawing.Size(149, 15);
            this.lblVlLinha.Text = "nnn";
            // 
            // lblVlIMSI
            // 
            this.lblVlIMSI.Location = new System.Drawing.Point(88, 39);
            this.lblVlIMSI.Name = "lblVlIMSI";
            this.lblVlIMSI.Size = new System.Drawing.Size(149, 15);
            this.lblVlIMSI.Text = "imsi";
            // 
            // lblVlIMEI
            // 
            this.lblVlIMEI.Location = new System.Drawing.Point(88, 54);
            this.lblVlIMEI.Name = "lblVlIMEI";
            this.lblVlIMEI.Size = new System.Drawing.Size(149, 15);
            this.lblVlIMEI.Text = "imei";
            // 
            // tbDDD
            // 
            this.tbDDD.Location = new System.Drawing.Point(88, 118);
            this.tbDDD.MaxLength = 2;
            this.tbDDD.Name = "tbDDD";
            this.tbDDD.Size = new System.Drawing.Size(36, 21);
            this.tbDDD.TabIndex = 5;
            // 
            // lblBairro
            // 
            this.lblBairro.Location = new System.Drawing.Point(3, 165);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(79, 20);
            this.lblBairro.Text = "Bairro:";
            this.lblBairro.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbBairro
            // 
            this.tbBairro.Location = new System.Drawing.Point(88, 164);
            this.tbBairro.MaxLength = 36;
            this.tbBairro.Name = "tbBairro";
            this.tbBairro.Size = new System.Drawing.Size(149, 21);
            this.tbBairro.TabIndex = 11;
            this.tbBairro.LostFocus += new System.EventHandler(this.tbNome_LostFocus);
            // 
            // lblMotivo
            // 
            this.lblMotivo.Location = new System.Drawing.Point(3, 212);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(79, 20);
            this.lblMotivo.Text = "Motivo:";
            this.lblMotivo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbMotivo
            // 
            this.cbMotivo.Items.Add("VIVO da sinal e não coneta (defaut)");
            this.cbMotivo.Items.Add("VIVO sem sinal");
            this.cbMotivo.Items.Add("TIM da sinal e não conecta");
            this.cbMotivo.Items.Add("TIM sem sinal");
            this.cbMotivo.Items.Add("CLARO da sinal e não conecta");
            this.cbMotivo.Items.Add("CLARO sem sinal");
            this.cbMotivo.Location = new System.Drawing.Point(88, 210);
            this.cbMotivo.Name = "cbMotivo";
            this.cbMotivo.Size = new System.Drawing.Size(149, 22);
            this.cbMotivo.TabIndex = 15;
            // 
            // CadChamado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cbMotivo);
            this.Controls.Add(this.lblMotivo);
            this.Controls.Add(this.tbBairro);
            this.Controls.Add(this.lblBairro);
            this.Controls.Add(this.tbDDD);
            this.Controls.Add(this.lblVlIMEI);
            this.Controls.Add(this.lblVlIMSI);
            this.Controls.Add(this.lblVlLinha);
            this.Controls.Add(this.lblIMEI);
            this.Controls.Add(this.lblIMSI);
            this.Controls.Add(this.lblLinha);
            this.Controls.Add(this.tbEMail);
            this.Controls.Add(this.lblEMail);
            this.Controls.Add(this.tbCidade);
            this.Controls.Add(this.lblCidade);
            this.Controls.Add(this.tbCelContato);
            this.Controls.Add(this.lblCelContato);
            this.Controls.Add(this.tbEmpresa);
            this.Controls.Add(this.lblEmpresa);
            this.Controls.Add(this.tbNome);
            this.Controls.Add(this.lblNome);
            this.Menu = this.mmChamado;
            this.Name = "CadChamado";
            this.Text = "Abertura de Chamado";
            this.Load += new System.EventHandler(this.CadChamado_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CadChamado_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox tbNome;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.TextBox tbEmpresa;
        private System.Windows.Forms.Label lblCelContato;
        private System.Windows.Forms.TextBox tbCelContato;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.TextBox tbCidade;
        private System.Windows.Forms.MenuItem miConfirmar;
        private System.Windows.Forms.MenuItem miSair;
        private System.Windows.Forms.Label lblEMail;
        private System.Windows.Forms.TextBox tbEMail;
        private System.Windows.Forms.Label lblLinha;
        private System.Windows.Forms.Label lblIMSI;
        private System.Windows.Forms.Label lblIMEI;
        private System.Windows.Forms.Label lblVlLinha;
        private System.Windows.Forms.Label lblVlIMSI;
        private System.Windows.Forms.Label lblVlIMEI;
        private System.Windows.Forms.TextBox tbDDD;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.TextBox tbBairro;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.ComboBox cbMotivo;
    }
}

