namespace ATMDLL.Forms
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
            this.pnlTopo = new System.Windows.Forms.Panel();
            this.lblVlIMEI = new System.Windows.Forms.Label();
            this.lblVlICCID = new System.Windows.Forms.Label();
            this.lblVlLinha = new System.Windows.Forms.Label();
            this.lblIMEI = new System.Windows.Forms.Label();
            this.lblICCID = new System.Windows.Forms.Label();
            this.lblLinha = new System.Windows.Forms.Label();
            this.pnlCorpo = new System.Windows.Forms.Panel();
            this.cbMotivo = new System.Windows.Forms.ComboBox();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.tbBairro = new System.Windows.Forms.TextBox();
            this.lblBairro = new System.Windows.Forms.Label();
            this.tbDDD = new System.Windows.Forms.TextBox();
            this.tbEMail = new System.Windows.Forms.TextBox();
            this.lblEMail = new System.Windows.Forms.Label();
            this.tbCidade = new System.Windows.Forms.TextBox();
            this.lblCidade = new System.Windows.Forms.Label();
            this.tbCelContato = new System.Windows.Forms.TextBox();
            this.lblCelContato = new System.Windows.Forms.Label();
            this.tbPalm = new System.Windows.Forms.TextBox();
            this.lblPalm = new System.Windows.Forms.Label();
            this.tbNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.pnlDiv = new System.Windows.Forms.Panel();
            this.tmBateria = new System.Windows.Forms.Timer();
            this.btConfigurarGPS = new System.Windows.Forms.Button();
            this.pnlTopo.SuspendLayout();
            this.pnlCorpo.SuspendLayout();
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
            // pnlTopo
            // 
            this.pnlTopo.Controls.Add(this.lblVlIMEI);
            this.pnlTopo.Controls.Add(this.lblVlICCID);
            this.pnlTopo.Controls.Add(this.lblVlLinha);
            this.pnlTopo.Controls.Add(this.lblIMEI);
            this.pnlTopo.Controls.Add(this.lblICCID);
            this.pnlTopo.Controls.Add(this.lblLinha);
            this.pnlTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopo.Location = new System.Drawing.Point(0, 0);
            this.pnlTopo.Name = "pnlTopo";
            this.pnlTopo.Size = new System.Drawing.Size(240, 60);
            // 
            // lblVlIMEI
            // 
            this.lblVlIMEI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVlIMEI.Location = new System.Drawing.Point(88, 43);
            this.lblVlIMEI.Name = "lblVlIMEI";
            this.lblVlIMEI.Size = new System.Drawing.Size(144, 15);
            this.lblVlIMEI.Text = "imei";
            // 
            // lblVlICCID
            // 
            this.lblVlICCID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVlICCID.Location = new System.Drawing.Point(88, 28);
            this.lblVlICCID.Name = "lblVlICCID";
            this.lblVlICCID.Size = new System.Drawing.Size(144, 15);
            this.lblVlICCID.Text = "iccid";
            // 
            // lblVlLinha
            // 
            this.lblVlLinha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVlLinha.Location = new System.Drawing.Point(88, 13);
            this.lblVlLinha.Name = "lblVlLinha";
            this.lblVlLinha.Size = new System.Drawing.Size(144, 15);
            this.lblVlLinha.Text = "nnn";
            // 
            // lblIMEI
            // 
            this.lblIMEI.Location = new System.Drawing.Point(3, 43);
            this.lblIMEI.Name = "lblIMEI";
            this.lblIMEI.Size = new System.Drawing.Size(79, 15);
            this.lblIMEI.Text = "IMEI:";
            this.lblIMEI.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblICCID
            // 
            this.lblICCID.Location = new System.Drawing.Point(3, 28);
            this.lblICCID.Name = "lblICCID";
            this.lblICCID.Size = new System.Drawing.Size(79, 15);
            this.lblICCID.Text = "CHIP:";
            this.lblICCID.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblLinha
            // 
            this.lblLinha.Location = new System.Drawing.Point(3, 13);
            this.lblLinha.Name = "lblLinha";
            this.lblLinha.Size = new System.Drawing.Size(79, 15);
            this.lblLinha.Text = "Operadora:";
            this.lblLinha.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnlCorpo
            // 
            this.pnlCorpo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCorpo.AutoScroll = true;
            this.pnlCorpo.Controls.Add(this.btConfigurarGPS);
            this.pnlCorpo.Controls.Add(this.cbMotivo);
            this.pnlCorpo.Controls.Add(this.lblMotivo);
            this.pnlCorpo.Controls.Add(this.tbBairro);
            this.pnlCorpo.Controls.Add(this.lblBairro);
            this.pnlCorpo.Controls.Add(this.tbDDD);
            this.pnlCorpo.Controls.Add(this.tbEMail);
            this.pnlCorpo.Controls.Add(this.lblEMail);
            this.pnlCorpo.Controls.Add(this.tbCidade);
            this.pnlCorpo.Controls.Add(this.lblCidade);
            this.pnlCorpo.Controls.Add(this.tbCelContato);
            this.pnlCorpo.Controls.Add(this.lblCelContato);
            this.pnlCorpo.Controls.Add(this.tbPalm);
            this.pnlCorpo.Controls.Add(this.lblPalm);
            this.pnlCorpo.Controls.Add(this.tbNome);
            this.pnlCorpo.Controls.Add(this.lblNome);
            this.pnlCorpo.Location = new System.Drawing.Point(0, 63);
            this.pnlCorpo.Name = "pnlCorpo";
            this.pnlCorpo.Size = new System.Drawing.Size(235, 202);
            // 
            // cbMotivo
            // 
            this.cbMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMotivo.Items.Add("VIVO da sinal e não coneta");
            this.cbMotivo.Items.Add("VIVO sem sinal");
            this.cbMotivo.Items.Add("TIM da sinal e não conecta");
            this.cbMotivo.Items.Add("TIM sem sinal");
            this.cbMotivo.Items.Add("CLARO da sinal e não conecta");
            this.cbMotivo.Items.Add("CLARO sem sinal");
            this.cbMotivo.Location = new System.Drawing.Point(88, 3);
            this.cbMotivo.Name = "cbMotivo";
            this.cbMotivo.Size = new System.Drawing.Size(144, 22);
            this.cbMotivo.TabIndex = 1;
            // 
            // lblMotivo
            // 
            this.lblMotivo.Location = new System.Drawing.Point(3, 5);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(79, 20);
            this.lblMotivo.Text = "Motivo:";
            this.lblMotivo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbBairro
            // 
            this.tbBairro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBairro.Location = new System.Drawing.Point(88, 118);
            this.tbBairro.MaxLength = 36;
            this.tbBairro.Name = "tbBairro";
            this.tbBairro.Size = new System.Drawing.Size(144, 21);
            this.tbBairro.TabIndex = 7;
            this.tbBairro.GotFocus += new System.EventHandler(this.tbNome_GotFocus);
            this.tbBairro.LostFocus += new System.EventHandler(this.tbNome_LostFocus);
            // 
            // lblBairro
            // 
            this.lblBairro.Location = new System.Drawing.Point(3, 119);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(79, 20);
            this.lblBairro.Text = "Bairro:";
            this.lblBairro.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbDDD
            // 
            this.tbDDD.Location = new System.Drawing.Point(88, 72);
            this.tbDDD.MaxLength = 2;
            this.tbDDD.Name = "tbDDD";
            this.tbDDD.Size = new System.Drawing.Size(36, 21);
            this.tbDDD.TabIndex = 4;
            this.tbDDD.GotFocus += new System.EventHandler(this.tbNome_GotFocus);
            // 
            // tbEMail
            // 
            this.tbEMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEMail.Location = new System.Drawing.Point(88, 141);
            this.tbEMail.MaxLength = 36;
            this.tbEMail.Name = "tbEMail";
            this.tbEMail.Size = new System.Drawing.Size(144, 21);
            this.tbEMail.TabIndex = 8;
            this.tbEMail.GotFocus += new System.EventHandler(this.tbNome_GotFocus);
            // 
            // lblEMail
            // 
            this.lblEMail.Location = new System.Drawing.Point(3, 142);
            this.lblEMail.Name = "lblEMail";
            this.lblEMail.Size = new System.Drawing.Size(79, 20);
            this.lblEMail.Text = "E-Mail:";
            this.lblEMail.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbCidade
            // 
            this.tbCidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCidade.Location = new System.Drawing.Point(88, 95);
            this.tbCidade.MaxLength = 36;
            this.tbCidade.Name = "tbCidade";
            this.tbCidade.Size = new System.Drawing.Size(144, 21);
            this.tbCidade.TabIndex = 6;
            this.tbCidade.GotFocus += new System.EventHandler(this.tbNome_GotFocus);
            this.tbCidade.LostFocus += new System.EventHandler(this.tbNome_LostFocus);
            // 
            // lblCidade
            // 
            this.lblCidade.Location = new System.Drawing.Point(3, 96);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(79, 20);
            this.lblCidade.Text = "Cidade:";
            this.lblCidade.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbCelContato
            // 
            this.tbCelContato.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCelContato.Location = new System.Drawing.Point(130, 72);
            this.tbCelContato.MaxLength = 8;
            this.tbCelContato.Name = "tbCelContato";
            this.tbCelContato.Size = new System.Drawing.Size(102, 21);
            this.tbCelContato.TabIndex = 5;
            this.tbCelContato.GotFocus += new System.EventHandler(this.tbNome_GotFocus);
            // 
            // lblCelContato
            // 
            this.lblCelContato.Location = new System.Drawing.Point(3, 73);
            this.lblCelContato.Name = "lblCelContato";
            this.lblCelContato.Size = new System.Drawing.Size(79, 20);
            this.lblCelContato.Text = "Cel. Contato:";
            this.lblCelContato.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbPalm
            // 
            this.tbPalm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPalm.Location = new System.Drawing.Point(88, 49);
            this.tbPalm.MaxLength = 5;
            this.tbPalm.Name = "tbPalm";
            this.tbPalm.Size = new System.Drawing.Size(144, 21);
            this.tbPalm.TabIndex = 3;
            this.tbPalm.GotFocus += new System.EventHandler(this.tbNome_GotFocus);
            this.tbPalm.LostFocus += new System.EventHandler(this.tbNome_LostFocus);
            // 
            // lblPalm
            // 
            this.lblPalm.Location = new System.Drawing.Point(3, 50);
            this.lblPalm.Name = "lblPalm";
            this.lblPalm.Size = new System.Drawing.Size(79, 20);
            this.lblPalm.Text = "Palm:";
            this.lblPalm.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbNome
            // 
            this.tbNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNome.Location = new System.Drawing.Point(88, 26);
            this.tbNome.MaxLength = 36;
            this.tbNome.Name = "tbNome";
            this.tbNome.Size = new System.Drawing.Size(144, 21);
            this.tbNome.TabIndex = 2;
            this.tbNome.GotFocus += new System.EventHandler(this.tbNome_GotFocus);
            this.tbNome.LostFocus += new System.EventHandler(this.tbNome_LostFocus);
            // 
            // lblNome
            // 
            this.lblNome.Location = new System.Drawing.Point(3, 27);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(79, 20);
            this.lblNome.Text = "Nome:";
            this.lblNome.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnlDiv
            // 
            this.pnlDiv.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnlDiv.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDiv.Location = new System.Drawing.Point(0, 60);
            this.pnlDiv.Name = "pnlDiv";
            this.pnlDiv.Size = new System.Drawing.Size(240, 2);
            // 
            // tmBateria
            // 
            this.tmBateria.Interval = 10000;
            this.tmBateria.Tick += new System.EventHandler(this.tmBateria_Tick);
            // 
            // btConfigurarGPS
            // 
            this.btConfigurarGPS.Location = new System.Drawing.Point(88, 168);
            this.btConfigurarGPS.Name = "btConfigurarGPS";
            this.btConfigurarGPS.Size = new System.Drawing.Size(144, 20);
            this.btConfigurarGPS.TabIndex = 14;
            this.btConfigurarGPS.Text = "Configurar GPS";
            this.btConfigurarGPS.Click += new System.EventHandler(this.btConfigurarGPS_Click);
            // 
            // CadChamado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlCorpo);
            this.Controls.Add(this.pnlDiv);
            this.Controls.Add(this.pnlTopo);
            this.Menu = this.mmChamado;
            this.Name = "CadChamado";
            this.Text = "Abertura de Chamado";
            this.Load += new System.EventHandler(this.CadChamado_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CadChamado_Closing);
            this.pnlTopo.ResumeLayout(false);
            this.pnlCorpo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem miConfirmar;
        private System.Windows.Forms.MenuItem miSair;
        private System.Windows.Forms.Panel pnlTopo;
        private System.Windows.Forms.Label lblVlIMEI;
        private System.Windows.Forms.Label lblVlICCID;
        private System.Windows.Forms.Label lblVlLinha;
        private System.Windows.Forms.Label lblIMEI;
        private System.Windows.Forms.Label lblICCID;
        private System.Windows.Forms.Label lblLinha;
        private System.Windows.Forms.Panel pnlCorpo;
        private System.Windows.Forms.TextBox tbBairro;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.TextBox tbDDD;
        private System.Windows.Forms.TextBox tbEMail;
        private System.Windows.Forms.Label lblEMail;
        private System.Windows.Forms.TextBox tbCidade;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.TextBox tbCelContato;
        private System.Windows.Forms.Label lblCelContato;
        private System.Windows.Forms.TextBox tbPalm;
        private System.Windows.Forms.Label lblPalm;
        private System.Windows.Forms.TextBox tbNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Panel pnlDiv;
        private System.Windows.Forms.ComboBox cbMotivo;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.Timer tmBateria;
        private System.Windows.Forms.Button btConfigurarGPS;
    }
}

