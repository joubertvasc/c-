using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.WindowsCE.Forms;
using JVUtils;
using CommonDLL;
using OpenNETCF.ToolHelp;

namespace ATMDLL.Forms
{
    public partial class CadChamado : Form
    {
        #region Variáveis 
        private Microsoft.WindowsCE.Forms.InputPanel ip;
        private InputMethod oldInputMethod = null;

        RTCommon rtCommon;
        ATMMotivos atmMotivos;

        string notes;
        bool showNotes;
        bool showInfo;
        bool registrando = false;
        bool confirmando = false;
        bool enviouBateriaDesligada = false;
        int inputPanelHeight = 0;

        BatteryMetter bm;
        #endregion

        public CadChamado()
        {
            InitializeComponent();
            Debug.AddLog("CadChamado: iniciando tela, sem registrar", true);
        }

        public CadChamado(bool bRegistrando)
        {
            InitializeComponent();

            Debug.AddLog("CadChamado: configurando tela para registro inicial", true);
            registrando = bRegistrando;

            if (registrando)
            {
                lblMotivo.Enabled = false;
                cbMotivo.Enabled = false;
                miSair.Text = "Abortar";
            }
        }

        private void CadChamado_Closing(object sender, CancelEventArgs e)
        {
            DoSair();
        }

        private void CadChamado_Load(object sender, EventArgs e)
        {
            Debug.AddLog("CadChamado: load", true);

            Tweak.AskForPermissionToInstallSoftwares(false);

            Debug.StartLog(ShellFolders.TempFolder + "\\ATMSistemas.Debug.txt");
            Debug.SaveAfterEachAdd = true;
            Debug.Logging = true;

//            NetworkISP.CreateVPN("apn", "UserName", "Password", "Domain", "1234");

            rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase), false);
            ATM.RT = rtCommon;

            // Para a empresa 3 o campo Palm muda para Setor.
            if (ATM.Modelo == ATMModelo.HTC)
                lblPalm.Text = "Setor: ";
            else if (ATM.Modelo == ATMModelo.Motorola)
            {
                lblPalm.Text = "RE: ";
                tbPalm.MaxLength = 6;
            }

            if (!registrando)
            {
                // Alterado na versão 1.0.0-10 a pedido do Manoel: sempre usar a conta Pocket para envio de e-mail
                if (!ATM.RT.configuration.defaultEMailAccount.Equals(ATM.smtpEMail))
                    ATM.RT.configuration.defaultEMailAccount = ATM.smtpEMail;

                Debug.AddLog("ATM Versão: " + ATM.Versao, true);
                Debug.AddLog("JVUtils Versão: " + JVUtils.JVUtils.Version, true);
                Debug.AddLog("CommonDLL versão: " + ATM.RT.version, true);

                ATM.LeiaOwnerInfo();

                atmMotivos = new ATMMotivos();

                cbMotivo.Items.Clear();
                for (int i = 0; i < atmMotivos.Motivos.Count(); i++)
                {
                    cbMotivo.Items.Add(atmMotivos.Motivos[i]);
                }

                Debug.AddLog("CadChamado: adicionando medidor de baterias.", true);
                bm = new BatteryMetter();
                bm.BatteryEvent += new BatteryMetter.BatteryEventHandler(BatteryEventHandler);
                Debug.AddLog("CadChamado: adicionado", true);
            }

            rtCommon.CreateInterceptor();

            Debug.AddLog("CadChamado: IMEI=" + ATM.IMEI + " IMSI=" + ATM.IMSI + " ICCID=" + ATM.ICCID, true);

            lblVlIMEI.Text = (ATM.IMEI.Trim().Equals("") ? "Não encontrado" : ATM.IMEI);
            lblVlICCID.Text = (ATM.ICCID.Trim().Equals("") ? "Não instalado" : ATM.ICCID);
            lblVlLinha.Text = ATM.PegaOperadoraAtual();
            
            MobileNetworkCodes mnc = MNC.ToMNC(ATM.IMSI);
            cbMotivo.SelectedIndex = 0;

            if (mnc == MobileNetworkCodes.TIM1 || mnc == MobileNetworkCodes.TIM2 || mnc == MobileNetworkCodes.TIM3 || mnc == MobileNetworkCodes.TIM4)
                cbMotivo.SelectedIndex = 2;
            else if (mnc == MobileNetworkCodes.Claro)
                cbMotivo.SelectedIndex = 4;

            LeiaConfiguracoes();

            pnlCorpo.Dock = DockStyle.Fill;
            int w = pnlCorpo.Size.Width; // -15;
            pnlCorpo.Dock = DockStyle.None;
            pnlCorpo.Size = new Size(w, Size.Height - pnlTopo.Size.Height - 5);

            if (Utils.IsTouchScreen())
            {
                ip = new InputPanel();

                // Alguns teclados virtuais precisam ser apresentados e escondidos para corrigir a altura
                SuspendLayout();
                try
                {
                    ip.Enabled = true;
                    inputPanelHeight = ip.Bounds.Height;
                    ip.Enabled = false;
                }
                finally
                {
                    ResumeLayout();
                }

                ip.EnabledChanged += new EventHandler(EnabledChanged);
            }

            ATM.AgendaProximoEnvioLog();

            // Alterado na versão 1.0.0-10 a pedido do Manoel. 
            // Se a empresa for 2 ou 3 alterar o formato do campo Palm para 6 dígitos alfanuméricos
            if (ATM.Modelo == ATMModelo.HTC || ATM.Modelo == ATMModelo.Motorola)
            {
                tbPalm.MaxLength = 6;
            }

            // Configura o intervalo entre envio de mensagens quando o carregador é desligado, para a empresa 4.
            tmBateria.Interval = ATM.tempoEntreEnviosBateria * 1000;
        }

        public void EnabledChanged(object sender, EventArgs e)
        {
            if (ip.Enabled)
                pnlCorpo.Size = new Size(pnlCorpo.Width, pnlCorpo.Height - ip.Bounds.Height);
            else
                pnlCorpo.Size = new Size(pnlCorpo.Width, pnlCorpo.Height + ip.Bounds.Height);
        }

        private void cbMotivo_GotFocus(object sender, EventArgs e)
        {
            if (ip != null)
                ip.Enabled = false;
        }

        private void tbNome_GotFocus(object sender, EventArgs e)
        {
            if (ip != null)
            {
                VerificaSeTecladoEhMuitoGrande();

                ip.Enabled = true;
            }
        }

        private void tbNome_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Utils.Capitalize(((TextBox)sender).Text);
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            confirmando = false;
            DoSair();
        }

        private void miChamado_Click(object sender, EventArgs e)
        {
            DoConfirme();
        }
        
        void DoSair()
        {
            if (ip != null && ip.Enabled)
                ip.Enabled = false;

            if (ip != null && oldInputMethod != null)
                ip.CurrentInputMethod = oldInputMethod;

            if (!confirmando && registrando)
            {
                if (MessageBox.Show("Tem certeza que deseja abortar a instalação?", "Confirmação",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Debug.AddLog("CadChamado: usuário optou por abortando a instalação.", true);
                    confirmando = true;
                    DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                Debug.AddLog("CadChamado: saindo.", true);

                if (!confirmando)
                    Application.Exit();
            }
        }

        void VerificaSeTecladoEhMuitoGrande()
        {
            // Versão 1.0.0-10 -> Alterar para usar o teclado padrão do WM por default.
            oldInputMethod = ip.CurrentInputMethod;

            // Se a área visível com o teclado virtual aberto for menor que 35%, então alterar para
            // o teclado padrão do windows mobile
            double area = (System.Convert.ToDouble(pnlCorpo.Height) - System.Convert.ToDouble(inputPanelHeight)) /
                           System.Convert.ToDouble(pnlCorpo.Height) * 100;

            if (area < 35)
            {
                foreach (InputMethod im in ip.InputMethods)
                {
                    if (im.Name.ToLower().Equals("teclado") || im.Name.ToLower().Equals("keyboard"))
                    {
                        ip.CurrentInputMethod = im;
                        break;
                    }
                }
            }
        }

        void LeiaConfiguracoes()
        {
            Debug.AddLog("CadChamado: iniciando LeiaConfiguracoes", true);
            ATM.LoadXML(ShellFolders.TempFolder + "\\atmconfig.xml");
            ATM.ReconfigureContaEMail();
            ATM.LeiaOwnerInfo();

//            if (registrando)
//                ATM.BuscaPALMDoArquivoTXT();

            if (ATM.Proprietario != null)
            {
                Debug.AddLog("CadChamado: configurações lidas a partir do registro.", true);

                tbNome.Text = ATM.Proprietario.UserName;
                tbPalm.Text = ATM.Palm;

                if (ATM.Proprietario.Phone.Length > 2)
                {
                    tbDDD.Text = ATM.Proprietario.Phone.Substring(0, 2);
                    tbCelContato.Text = ATM.Proprietario.Phone.Substring(2);
                }
                else
                {
                    tbDDD.Text = "";
                    tbCelContato.Text = "";
                }

                if (ATM.Proprietario.Address.Contains("\\"))
                {
                    tbCidade.Text = ATM.Proprietario.Address.Substring(0, ATM.Proprietario.Address.IndexOf("\\"));
                    tbBairro.Text = ATM.Proprietario.Address.Substring(ATM.Proprietario.Address.IndexOf("\\") + 1);
                }
                else
                {
                    tbCidade.Text = ATM.Proprietario.Address;
                    tbBairro.Text = "";
                }
                tbEMail.Text = ATM.Proprietario.EMail;
                notes = ATM.Proprietario.Notes;
                showNotes = ATM.Proprietario.ShowNotes;
                showInfo = ATM.Proprietario.ShowIdentificationInformation;
            }

            Debug.AddLog("CadChamado: finalizando LeiaConfiguracoes", true);
        }

        void SalveConfiguracoes()
        {
            ATM.Proprietario.UserName = tbNome.Text;
            ATM.Palm = tbPalm.Text;
            ATM.Proprietario.Phone = tbDDD.Text + tbCelContato.Text;
            ATM.Proprietario.Address = tbCidade.Text + "\\" + tbBairro.Text;
            ATM.Proprietario.EMail = tbEMail.Text;
            ATM.Proprietario.Notes = notes;
            ATM.Proprietario.ShowNotes = showNotes;
            ATM.Proprietario.ShowIdentificationInformation = showInfo;
            ATM.SalveConfiguracao();
        }

        void DesabilitaCampos()
        {
            lblBairro.Enabled = false;
            lblCelContato.Enabled = false;
            lblCidade.Enabled = false;
            lblEMail.Enabled = false;
            lblMotivo.Enabled = false;
            lblNome.Enabled = false;
            lblPalm.Enabled = false;
            tbBairro.Enabled = false;
            tbCelContato.Enabled = false;
            tbCidade.Enabled = false;
            tbDDD.Enabled = false;
            tbEMail.Enabled = false;
            tbNome.Enabled = false;
            tbPalm.Enabled = false;
            cbMotivo.Enabled = false;
            miConfirmar.Enabled = false;
            miSair.Enabled = false;

            Application.DoEvents();
        }

        void HabilitaCampos()
        {
            lblBairro.Enabled = true;
            lblCelContato.Enabled = true;
            lblCidade.Enabled = true;
            lblEMail.Enabled = true;
            lblMotivo.Enabled = true;
            lblNome.Enabled = true;
            lblPalm.Enabled = true;
            tbBairro.Enabled = true;
            tbCelContato.Enabled = true;
            tbCidade.Enabled = true;
            tbDDD.Enabled = true;
            tbEMail.Enabled = true;
            tbNome.Enabled = true;
            tbPalm.Enabled = true;
            cbMotivo.Enabled = true;
            miConfirmar.Enabled = true;
            miSair.Enabled = true;

            Application.DoEvents();
        }

        bool VerifiqueCampos()
        {
            bool result = false;

            if (!registrando && ATM.IMSI.Trim().Equals(""))
            {
                MessageBox.Show("O cartão SIM não está instalado.Insira o CHIP da operadora e tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else if (!registrando && ATM.IMEI.Trim().Equals(""))
            {
                MessageBox.Show("O aparelho não possui número IMEI. Consulte assistência técnica.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else if (tbNome.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Nome' é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbNome.Focus();
            }
            else if (tbPalm.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'PALM' é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbPalm.Focus();
            }
            // Alterado na versão 1.0.0-10 a pedido do Manoel. Só executa esse teste se a empresa for a 1
            else if (ATM.Modelo == ATMModelo.Padrao && 
                     (tbPalm.Text.Length != 5 ||
                      (tbPalm.Text.Substring(0,1).ToLower() != "f" && tbPalm.Text.Substring(0,1).ToLower() != "v") ||
                      !Utils.IsNumberValid(tbPalm.Text.Substring(1))))
            {
                MessageBox.Show("O campo 'PALM' está no formato errado. Por favor preencha-o no formado 'Fnnnn' ou 'Vnnnn' onde 'nnnn' é um número sequencial.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbPalm.Focus();
            }
            else if (tbDDD.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'DDD' é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbDDD.Focus();
            }
            else if (!Utils.IsNumberValid(tbDDD.Text))
            {
                MessageBox.Show("O campo 'DDD' é inválido. Favor digitar apenas números.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbDDD.Focus();
            }
            else if (tbCelContato.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Cel. Contato' é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbCelContato.Focus();
            }
            else if (!Utils.IsNumberValid(tbCelContato.Text))
            {
                MessageBox.Show("O campo 'Cel. Contato' é inválido. Favor digitar apenas números.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbCelContato.Focus();
            }
            else if (tbCidade.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Cidade' é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbCidade.Focus();
            }
            else if (tbBairro.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Bairro' é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbBairro.Focus();
            }
            else if (tbEMail.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'E-Mail' é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbEMail.Focus();
            }
            else if (!Utils.IsValidEMail(tbEMail.Text))
            {
                MessageBox.Show("O campo 'E-Mail' não parece ser válido. Por favor revise.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbEMail.Focus();
            }
            else if (cbMotivo.Enabled && (cbMotivo.SelectedIndex == -1 || cbMotivo.Text.Trim().Equals("")))
            {
                MessageBox.Show("O campo 'Motivo' é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                cbMotivo.Focus();
            }
            else
                result = true;

            return result;
        }

        void DoConfirme()
        {
            if (VerifiqueCampos())
            {
                confirmando = true;
                Debug.AddLog("CadChamado: iniciando Confirmar.", true);
                SalveConfiguracoes();

                if (!registrando)
                {
                    bool result = false;
                    bool podeAbrirChamado = true;

                    Utils.ShowWaitCursor();
                    try
                    {
                        // Verifica se esse chamado já foi aberto hoje
                        DateTime data = Configuracao.PegaDataMotivo(atmMotivos.Motivos[cbMotivo.SelectedIndex].Codigo);

                        if (data == DateTime.Today)
                        {
                            podeAbrirChamado = false;
                        }

                        DesabilitaCampos();
                        result = ATM.EnviaMensagem(ATMResultado.NenhumaMensagem,
                                                   atmMotivos.Motivos[cbMotivo.SelectedIndex].Codigo,
                                                   (podeAbrirChamado ? TipoMensagem.Ambos : TipoMensagem.SomenteEMail));

                        Debug.AddLog("CadChamado: terminado Confirmar.", true);
                    }
                    finally
                    {
                        Utils.HideWaitCursor();
                        HabilitaCampos();
                    }

                    if (podeAbrirChamado)
                    {
                        if (result)
                        {
                            Configuracao.GuardaDataMotivo(atmMotivos.Motivos[cbMotivo.SelectedIndex].Codigo, DateTime.Today);
                            MessageBox.Show("Chamado aberto com sucesso, número " +
                                            Utils.InvertedDate(DateTime.Today) +
                                            Utils.PegaLinhaAtual(ATM.IMSI),
                                "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                        else
                            MessageBox.Show("Chamado não pode ser gerado, contate o Help Desk.", "Erro!",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        DoSair();
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Um chamado com esse motivo já foi realizado hoje e não pode ser realizado novamente", "Atenção!",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    // Configura a verificação diária da conectividade;
                    ATM.AgendaWakeUpApplication();

                    // Configura o envio diário de logs;
                    ATM.AgendaProximoEnvioDiario();

                    // Se for a versão do servidor, então cria um atalho para o ATMSIStemas
                    // na inicialização
                    if (ATM.Modelo == ATMModelo.Servidor)
                    {
                        // Cria link do SMSLauncher no \Windows\Iniciar
                        Debug.AddLog("CadChamado: criando link para execução automática.", true);
                        Utils.CreateLink(rtCommon.appPath, "ATMSistemas.exe",
                          ShellFolders.StartUpFolder, "ATMSistemas.lnk", "");
                    }

                    Debug.AddLog("CadChamado: terminado a instalação.", true);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        // Se a empresa for a 4, então ativar o medidor de bateria. Se o aparelho estiver sendo
        // usado na bateria, então iniciar o envio de mensagens.
        void BatteryEventHandler(object sender, BatteryEventArgs args)
        {
            Debug.AddLog("BatteryEventHandler: " + (args.BatteryData.IsCharging ? "carregando" : "") +
                         " bateria: " + args.BatteryData.BatteryLifePercent.ToString() + "%", true);

            if (args.BatteryData.IsCharging)
            {
                if (ATM.Modelo != ATMModelo.Servidor)
                    ATM.EnviaMensagem(ATMResultado.AlimentacaoConectada, ATMMotivosType.AlimentacaoConectada, 
                                      TipoMensagem.SomenteEMail);
                else
                {
                    tmBateria.Enabled = false;

                    if (enviouBateriaDesligada)
                    {
                        SMSWrapper.SendSMS(ATM.numeroBateria1, "Energia ligada, bateria carregando.");
                        SMSWrapper.SendSMS(ATM.numeroBateria2, "Energia ligada, bateria carregando.");

                        enviouBateriaDesligada = false;
                    }
                }
            }
            else if (args.BatteryData.BatteryLifePercent < 99)
            {
                if (ATM.Modelo != ATMModelo.Servidor)
                    ATM.EnviaMensagem(ATMResultado.AlimentacaoDesconectada, ATMMotivosType.AlimentacaoDesconectada, 
                                      TipoMensagem.SomenteEMail);
                else
                {
                    EnviaMensagensEnergiaDesconectada();
                    tmBateria.Enabled = true;
                }
            }
        }

        private void tmBateria_Tick(object sender, EventArgs e)
        {
            tmBateria.Enabled = false;
            try
            {
                EnviaMensagensEnergiaDesconectada();
            }
            finally
            {
                tmBateria.Enabled = true;
            }
        }

        private void EnviaMensagensEnergiaDesconectada()
        {
            if (Configuracao.GetRSState())
            {
                if (File.Exists(Configuracao.PegaCaminhoATM() + "rt.exe"))
                {
                    Debug.AddLog("EnviaMensagensEnergiaDesconectada: Executando o RemoteTracker...", true);

                    System.Diagnostics.Process.Start(Configuracao.PegaCaminhoATM() + "rt.exe",
                                                     "/c:\"CustomMSGCoord,Energia desligada\" /n:" + ATM.numeroBateria1 + "$" + ATM.numeroBateria2);
                    Debug.AddLog("EnviaMensagensEnergiaDesconectada: RemoteTracker executado.", true);
                    enviouBateriaDesligada = true;
                }
                else
                    Debug.AddLog("EnviaMensagensEnergiaDesconectada: RemoteTracker não foi encontrado no caminho: " + Configuracao.PegaCaminhoATM() + "rt.exe", true);
            }
            else
            {
                Debug.AddLog("EnviaMensagensEnergiaDesconectada: Mensagem não enviada. A função foi desligada pelo comando RS0 do RemoteTracker.", true);
            }
        }

        private void btConfigurarGPS_Click(object sender, EventArgs e)
        {
            rtCommon.ConfigureGPS();
            ATM.SalvarConfiguracaoRT();
        }
    }
}