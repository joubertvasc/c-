using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsMobile.PocketOutlook;
using CommonDLL;
using JVUtils;

namespace ATMDLL
{
    public static class ATM
    {
        // Alterado na versão 1.0.0-10. A versão agora é definida pelo arquivo atmsistemas.xml
        // Se o arquivo não existir então é usado o valor de versaoInterna.
        static string versao = "";
        static string versaoInterna = "1.0.0-19";

        #region Variáveis internas
        static OwnerRecord _ownerRecord = null;
        static RTCommon rt = null;
//        static string comandoBuscarNumeroTelefone = "rs#cel";
        static string _iccid = "";
        static string _imei = "";
        static string _imsi = "";
        static string _arquivoTXT = "";
        static string palm = "";
        #endregion

        #region Variáveis públicas
        public static string aliasPrincipal = "Principal";
        public static string aliasContingente = "Contingente";
        public static string emailEmergencia = "controle@atmsistema.com.br,chamados@atmsistema.com.br";
        public static string emailChamado = "chamados@atmsistema.com.br";
        public static string emailCopia = " controle@atmsistema.com.br";
        public static string numeroChamado = "01697336056";
        public static string numeroBateria1 = "01797449572";
        public static string numeroBateria2 = "01796297203";
        public static string senha = "";
        public static int tempoEntreEnviosBateria = 30;
        // Alterado na versão 1.0.0-10 a pedido do Manuel
//        public static string contaSMTP = "taaxi";
        public static string contaSMTP = "pocket";
        public static bool debug = true; //false;
        public static string ftpServer = "ftp.3gloja.com.br";
        public static string ftpUser = "w3gloja";
        public static string ftpPass = "AtmSis10";
        public static string ftpRemoteDir = "/pocketlogs";
        public static string ftpLocalDir = "\\temp";
        public static string bftpLocalDir1 = "\\difarma\\dados";
        public static string bftpLocalDir2 = "\\dicon\\dados";
        public static string bftpLocalDir3 = "\\SuperWaba\\CheetaH";
        public static int ftpPort = 21;
        
        // Alterado na versão 1.0.0-10 a pedido do Manuel
        public static string smtpUserName = "pocket@atmsistemas.com.br";
        public static string smtpName = "pocket@atmsistemas.com.br";
        public static string smtpEMail = "pocket@atmsistemas.com.br";
        public static string smtpInHost = "pop.atmsistemas.com.br";
        public static string smtpOutHost = "smtp.atmsistemas.com.br";
        public static string smtpPassword = "pocket";
        public static string smtpGUID = "{BF1CAB9A-357B-46a9-A858-479CD71644E5}";
//        public static string smtpUserName = "palm@taaxi.com.br";
//        public static string smtpName = "palm@taaxi.com.br";
//        public static string smtpEMail = "palm@taaxi.com.br";
//        public static string smtpInHost = "pop.taaxi.com.br";
//        public static string smtpOutHost = "smtp.taaxi.com.br";
//        public static string smtpPassword = "palmn33";
//        public static string smtpGUID = "{6BDBC562-6F70-4cf7-A439-E48169BEB604}";
        #endregion

        #region Propriedades
        public static OwnerRecord Proprietario
        {
            get 
            {
                if (_ownerRecord == null)
                {
                    _ownerRecord = OwnerInfo.GetOwnerRecord();

                    if (_ownerRecord == null)
                        _ownerRecord = new OwnerRecord();
                }
                
                return _ownerRecord; 
            }

            set 
            {
                if (_ownerRecord == null)
                    _ownerRecord = new OwnerRecord();                
                
                _ownerRecord = value; 
            }
        }
        public static RTCommon RT
        {
            get { return rt; }
            set { rt = value; Configuracao.RT = value; }
        }
//        public static string ComandoBuscarNumeroTelefone
//        {
//            get { return comandoBuscarNumeroTelefone; }
//            set { comandoBuscarNumeroTelefone = value; }
//        }
        public static string Versao
        {
            get 
            {
                if (versao.Equals(""))
                {
                    string xml = RT.appPath + "atmsistemas.xml";

                    Debug.AddLog("Versao: verificando se existe XML de versao do AppToDate: " + xml, true);
                    if (!xml.Trim().Equals("") && File.Exists(xml))
                    {
                        try
                        {
                            Debug.AddLog("Versao: lendo arquivo de configuração", true);
                            DataSet ds = new DataSet();
                            ds.ReadXml(xml);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    //Rows[ind];
                                    if (!ds.Tables[0].Rows[0]["version"].ToString().Equals(""))
                                        versao = ds.Tables[0].Rows[0]["version"].ToString();

                                    Debug.AddLog("Versao: arquivo de configuração lido com sucesso", true);
                                }
                                else
                                {
                                    Debug.AddLog("Versao: arquivo de configuração inválido", true);
                                }
                            }
                            else
                            {
                                Debug.AddLog("Versao: arquivo de configuração inválido", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.AddLog("Versao erro: " + ex.Message.ToString(), true);
                        }
                    }
                    else
                    {
                        Debug.AddLog("Versao: XML de configuração não encontrado", true);
                    }

                    if (versao.Equals(""))
                        versao = versaoInterna;
                }

                return versao; 
            }
        }
        public static ATMModelo Modelo
        {
            get
            {
                if (Versao.Substring(0, 1).Equals("1"))
                    return ATMModelo.Padrao;
                else if (Versao.Substring(0, 1).Equals("2"))
                    return ATMModelo.Motorola;
                else if (Versao.Substring(0, 1).Equals("3"))
                    return ATMModelo.HTC;
                else if (Versao.Substring(0, 1).Equals("4"))
                    return ATMModelo.Servidor;
                else
                    return ATMModelo.Desconhecido;
            }
        }

        public static string ICCID
        {
            get
            {
                if (_iccid.Equals(""))
                    _iccid = Utils.RemoveChar(JVUtils.ICCID.GETICCID(), ' ');

                return _iccid;
            }
        }
        public static string IMEI
        {
            get
            {
                if (_imei.Equals(""))
                    _imei = Utils.RemoveChar(RT.GetIMEI(), ' ');

                return _imei;
            }
        }
        public static string IMSI
        {
            get
            {
                if (_imsi.Equals(""))
                    _imsi = Utils.RemoveChar(RT.GetIMSI(), ' ');

                return _imsi;
            }
        }

        public static string Palm
        {
            get { return palm; }
            set { palm = value; }
        }
        #endregion

        /// <summary>
        /// Configura o RemoteTracker com informações padrão
        /// </summary>
        public static void AutoConfig()
        {
            Debug.AddLog("AutoConfig iniciado", true);
            LoadXML(ShellFolders.TempFolder + "\\atmconfig.xml");

            Debug.AddLog("AutoConfig IMSI: " + IMSI, true);
     
            VerificaSIMCard(IMSI);

            rt.configuration.defaultEMailAccount = "";
            rt.configuration.AliasIMSI1 = aliasPrincipal;
            rt.configuration.AliasIMSI2 = aliasContingente;
            rt.configuration.defaultNumber1 = numeroChamado;  
            rt.configuration.defaultrecipientEMail = emailEmergencia;
            rt.configuration.defaultrecipientName = emailEmergencia;
            rt.configuration.emergencyEMail1 = emailEmergencia;
            rt.configuration.defaultPassword = senha;
            rt.configuration.FtpServer = ftpServer;
            rt.configuration.FtpUser = ftpUser;
            rt.configuration.FtpPassword = ftpPass;
            rt.configuration.FtpRemoteDir = ftpRemoteDir;
            rt.configuration.FtpPort = ftpPort;
            rt.configuration.DebugMode = debug;
            rt.configuration.defaultEMailAccount = ProcureContaDeEMail();
            rt.configuration.ScreenOff = false;

            JVUtils.JVUtils.Set_ContractAccepted("RT", rt.termOfServiceRevisionNumber);
            JVUtils.JVUtils.Set_ContractAccepted("RT-WEB", rt.termOfServiceRevisionNumber);

            Configuracao.DefineRTcomoATM();
            Configuracao.DefineFTPFolder(ftpLocalDir);
            Configuracao.GuardaFoldersBFTP(bftpLocalDir1, 
                                           (ATM.Modelo == ATMModelo.HTC ? bftpLocalDir3 : bftpLocalDir2));

            Debug.AddLog("AutoConfig terminado", true);
        }

        /// <summary>
        /// Pesquisa nas contas de e-mail criadas no PocketOutlook por alguma que tenha o valor da variável pública
        /// 'contaSMTP' em seu nome. Se não encontrar, devolve a primeira conta de e-mail diferente de 'ActiveSync'
        /// </summary>
        public static string ProcureContaDeEMail()
        {
            Debug.AddLog("ProcureContaEMail iniciando procura.", true);
            string result = "";

            // Verifica se existe uma conta de e-mail que contenha o valor da variável 'contaSMTP' em seu nome
            foreach (string account in rt.EMailAccounts)
            {
                if (account.ToLower().Trim().Contains(contaSMTP.ToLower().Trim()))
                {
                    Debug.AddLog("ProcureContaEMail Conta de e-mail encontrada: " + account, true);
                    result = account;
                    break;
                }
            }

            // Se não achou nenhuma conta de e-mail com o valor da variável 'contaSMTP' em seu nome, retorna a primeira 
            // conta de e-mail que seja diferente de ActiveSync
            //
            // Alterado em 22/06/2010 na versão 1.0.0-10 a pedido do Manuel para sempre buscar a conta padrão.
//            if (result.Equals(""))
//            {
//                foreach (string account in rt.EMailAccounts)
//                {
//                    if (!account.ToLower().Trim().Contains("activesync"))
//                    {
//                        Debug.AddLog("ProcureContaEMail Conta de e-mail diferente de ActiveSync encontrada: " + account, true);
//                        result = account;
//                        break;
//                    }
//                }
//            }

            if (result.Equals(""))
            {
                Debug.AddLog("ProcureContaEMail nenhuma conta encontrada. Criando conta padrão.", true);
                CriaContaEMailPadrao();

                // Recursivo!
                Debug.AddLog("ProcureContaEMail. Entrando em recursividade.", true);
                result = ProcureContaDeEMail();
                Debug.AddLog("ProcureContaEMail. Saindo da recursividade.", true);
            }

            Debug.AddLog("ProcureContaEMail. Resultado = " + result, true);
            return result;
        }

        /// <summary>
        /// Verifica se a conta de e-mail existe no Pocket Outlook
        /// </summary>
        /// <param name="conta">Nome da conta de e-mail</param>
        /// <returns>TRUE se existir, FALSE se não.</returns>
        public static bool ContaEMailExiste(string conta)
        {
            Debug.AddLog("ContaEMailExiste iniciando procura da conta '" + conta + "'.", true);

            foreach (string account in rt.EMailAccounts)
            {
                if (account.ToLower().Trim().Equals(conta.ToLower().Trim()))
                {
                    Debug.AddLog("ContaEMailExiste Conta de e-mail encontrada: " + account, true);
                    return true;
                }
            }

            Debug.AddLog("ContaEMailExiste finalizando procura, sem encontrar a conta.", true);
            return false;
        }

        /// <summary>
        /// Cria a conta de e-mail indicada nas variáveis públicas iniciadas por smtp...
        /// </summary>
        public static void CriaContaEMailPadrao()
        {
            OutlookAccountRecord oar = new OutlookAccountRecord();
            oar.AccountType = "POP3";
            oar.AuthRequired = true;
            oar.DisplayName = smtpName;
            oar.EmailAddress = smtpEMail;
            oar.Guid = smtpGUID;
            oar.IncommingServer = smtpInHost;
            oar.OutgoingServer = smtpOutHost;
            oar.Linger = 0;
            oar.Password = smtpPassword;
            oar.ReplyToAddress = smtpEMail;
            oar.Name = smtpEMail;
            oar.Retrieve = 0;
            oar.DwnDay = 1;
            oar.UserName = smtpUserName;

            try
            {
                Outlook.CreateOutlookAccount(oar);

                rt.EMailAccounts = rt.LoadAccountList();
                Debug.AddLog("CriaContaEMailPadrao: sucesso", true);
            }
            catch (Exception ex)
            {
                Debug.AddLog("CriaContaEMailPadrao: erro: " + ex.Message.ToString(), true);
            }
        }

        /// <summary>
        /// Verifica se a conta de e-mail ainda existe. Se não existir ou se não estiver configurada, chama o método 
        /// ProcureContaDeEMail e realiza a nova configuração.
        /// </summary>
        public static void ReconfigureContaEMail()
        {
            if (rt.configuration.defaultEMailAccount.Trim().Equals("") || 
                !ContaEMailExiste(rt.configuration.defaultEMailAccount))
            {
                rt.configuration.defaultEMailAccount = ProcureContaDeEMail();
                SalvarConfiguracaoRT();
            }
        }

        public static string PegaOperadoraAtual()
        {
            return MNC.ToString(IMSI);
        }

        /// <summary>
        /// Lê as informações do proprietário do dispositivo e preenche a propriedade Proprietario
        /// </summary>
        public static void LeiaOwnerInfo()
        {
            palm = Configuracao.GetPalm();

            Debug.AddLog("LeiaOwnerInfo: iniciando", true);
            Proprietario = OwnerInfo.GetOwnerRecord();

            if (Proprietario.EMail.Equals(""))
                BuscaEMailDoArquivoTXT();
                        
            if (palm.Equals(""))
                palm = BuscaPALMDoArquivoTXT();

            if (palm.Equals(""))
                palm = Proprietario.Company;

            Debug.AddLog("LeiaOwnerInfo: finalizando", true);
        }

        private static void BuscaArquivoTXT()
        {
            string[] arqs = Utils.ListFilesInFolder("listafone.txt", "\\");

            if (arqs.Length > 0)
                _arquivoTXT = arqs[0];
        }

        public static string BuscaPALMDoArquivoTXT()
        {
            if (_arquivoTXT.Equals(""))
                BuscaArquivoTXT();

            if (!_arquivoTXT.Equals(""))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(_arquivoTXT))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            char[] delimiterChars = { '|' };
                            string[] partes = line.Split(delimiterChars);

                            if (partes.Length >= 12)
                            {
                                return partes[7].ToUpper();
                            }

                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.AddLog("BuscaPalm: erro: " + ex.Message.ToString(), true);
                    return "";
                }
            }

            return "";
        }

        public static void BuscaEMailDoArquivoTXT()
        {
            if (_arquivoTXT.Equals(""))
                BuscaArquivoTXT();

            if (!_arquivoTXT.Equals(""))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(_arquivoTXT))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            char[] delimiterChars = { '|' };
                            string[] partes = line.Split(delimiterChars);

                            if (partes.Length >= 10)
                            {
                                Proprietario.EMail = partes[9];
                            }

                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.AddLog("BuscaEMailDoArquivoTXT: erro: " + ex.Message.ToString(), true);
                }
            }
        }

        /// <summary>
        /// Grava as informações da propriedade Proprietario nas informações do proprietário do dispositivo
        /// </summary>
        public static void SalveConfiguracao()
        {
            Debug.AddLog("SalveConfiguracao: iniciando", true);
            OwnerInfo.SetOwnerRecord(Proprietario);
            Configuracao.SetUltimoCartaoUsado();
            Configuracao.DefineCaminhoATM();
            Configuracao.SetPalm(Palm);
            Configuracao.SaveConfig();

//            AgendaProximoEnvioLog();
            Debug.AddLog("SalveConfiguracao: finalizando", true);
        }

        /// <summary>
        /// Salva as configurações realizadas automaticamente nos registros do RemoteTracker
        /// </summary>
        public static void SalvarConfiguracaoRT()
        {
            Debug.AddLog("SalvarConfiguracao: salvando configuração do RemoteTracker", true);
            rt.configuration.SaveConfiguration();
        }

        /// <summary>
        /// Usado para configurar os IMSI automaticamente. Se for um chip da Vivo, então usará o IMSI1, senão o IMSI2. Só altera o valor dos campos se estes estiverem vazios.
        /// Retorna TRUE se configurou um dos campos e FALSE se nenhuma configuração foi realizada.
        /// </summary>
        /// <param name="rt">Objeto RTCommon</param>
        /// <param name="imsi">string contendo o número IMSI do SIM instalado</param>
        public static bool VerificaSIMCard(string imsi)
        {
            Debug.AddLog("VerificaSIMCard: Avaliando IMSI: " + imsi, true);

            if (!imsi.Trim().Equals(""))
            {
                if (IMSIdaVivo(imsi))
                {
                    // Se a o número da linha do IMSI encontrado não estiver definido, então envia a mensagem para o servidor
                    // devolver o número para o RemoteTracker
//                    if (PegaLinhaAtual().Equals("0"))
//                        EnviaSMS(ComandoBuscarNumeroTelefone);

                    Debug.AddLog("VerificaSIMCard: IMSI é da Vivo.", true);

                    if (rt.configuration.IMSI1.Trim().Equals(""))
                    {
                        Debug.AddLog("VerificaSIMCard: IMSI da VIVO", true);
                        rt.configuration.IMSI1 = imsi;
                        rt.configuration.AliasIMSI1 = aliasPrincipal;

                        return true;
                    }
                    else
                    {
                        Debug.AddLog("VerificaSIMCard: IMSI principal já estava configurado.", true);
                    }
                }
                else
                {
                    Debug.AddLog("VerificaSIMCard: IMSI não é da Vivo.", true);

                    if (rt.configuration.IMSI2.Trim().Equals(""))
                    {
                        Debug.AddLog("AutoConfig IMSI de operadora diferente da VIVO", true);
                        rt.configuration.IMSI2 = imsi;
                        rt.configuration.AliasIMSI2 = aliasContingente;

                        return true;
                    }
                    else
                    {
                        Debug.AddLog("VerificaSIMCard: IMSI contingente já estava configurado.", true);
                    }
                }
            }

            Debug.AddLog("VerificaSIMCard: Nenhuma alteração necessária. SIM card não instalado.", true);
            return false;
        }

        /// <summary>
        /// Verifica se o IMSI repassado é da operadora VIVO
        /// </summary>
        /// <param name="imsi">Número a ser verificado</param>
        /// <returns>TRUE se o IMSI pertencer a VIVO, FALSO caso contrário.</returns>
        public static bool IMSIdaVivo(string imsi)
        {
            if (imsi.Trim().Equals("") || imsi.Length != 15)
            {
                Debug.AddLog("IMSIdaVivo: IMSI VAZIO OU INVÁLIDO", true);
                return false;
            }
            else
            {
                Debug.AddLog("IMSIdaVivo: MCC=" + imsi.Substring(0, 3) + " MNC=" + imsi.Substring(3, 2), true);

                if (System.Convert.ToInt32(imsi.Substring(0, 3)) == (int)MobileCountryCodes.Brazil &&
                     (System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Vivo1 ||
                      System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Vivo2 ||
                      System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Vivo3))
                {
                    Debug.AddLog("IMSIdaVivo: É da Vivo.", true);
                    return true;
                }
                else
                {
                    Debug.AddLog("IMSIdaVivo: não é da Vivo.", true);
                    return false;
                }
            }
        }

        /// <summary>
        /// Carrega um arquivo XML de configuração. Usado para testes
        /// </summary>
        /// <param name="xml"></param>
        public static void LoadXML(string xml)
        {
            Debug.AddLog("LoadXML: verificando se existe XML de configuração (" + xml + ")", true);
            if (!xml.Trim().Equals("") && File.Exists(xml))
            {
                try
                {
                    Debug.AddLog("LoadXML: lendo arquivo de configuração", true);
                    DataSet ds = new DataSet();
                    ds.ReadXml(xml);

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //Rows[index];
                            emailEmergencia = ds.Tables[0].Rows[0]["EMailEmergencia"].ToString();
                            emailChamado = ds.Tables[0].Rows[0]["EMailChamado"].ToString();
                            emailCopia = ds.Tables[0].Rows[0]["EMailCopia"].ToString();
                            numeroChamado = ds.Tables[0].Rows[0]["NumeroChamado"].ToString();
                            senha = ds.Tables[0].Rows[0]["Senha"].ToString();
                            contaSMTP = ds.Tables[0].Rows[0]["ContaSMTP"].ToString();
                            ftpServer = ds.Tables[0].Rows[0]["FTPServer"].ToString();
                            ftpUser = ds.Tables[0].Rows[0]["FTPUser"].ToString();
                            ftpPass = ds.Tables[0].Rows[0]["FTPPassword"].ToString();
                            ftpRemoteDir = ds.Tables[0].Rows[0]["FTPRemoteDir"].ToString();
                            ftpPort = System.Convert.ToInt32(ds.Tables[0].Rows[0]["FTPPort"]);
                            debug = ds.Tables[0].Rows[0]["DebugMode"].ToString().Equals("1");
                            ftpLocalDir = ds.Tables[0].Rows[0]["FTPFolder"].ToString();
                            bftpLocalDir1 = ds.Tables[0].Rows[0]["BFTPFolder1"].ToString();
                            bftpLocalDir2 = ds.Tables[0].Rows[0]["BFTPFolder2"].ToString();
                            tempoEntreEnviosBateria = System.Convert.ToInt32(ds.Tables[0].Rows[0]["TempoEntreEnviosBateria"]);
                            numeroBateria1 = ds.Tables[0].Rows[0]["NumeroBateria1"].ToString();
                            numeroBateria2 = ds.Tables[0].Rows[0]["NumeroBateria2"].ToString();

                            Debug.AddLog("LoadXML: arquivo de configuração lido com sucesso", true);
                        }
                        else
                        {
                            Debug.AddLog("LoadXML: arquivo de configuração inválido", true);
                        }
                    }
                    else
                    {
                        Debug.AddLog("LoadXML: arquivo de configuração inválido", true);
                    }
                }
                catch (Exception ex)
                {
                    Debug.AddLog("LoadXML erro: " + ex.Message.ToString(), true);
                }
            }
            else
            {
                Debug.AddLog("LoadXML: XML de configuração não encontrado", true);
            }
       }

        /// <summary>
        /// Grava um arquivo de log contendo as mensagens enviadas. Os arquivos de log são mensais (201001.txt para janeiro de 2010 por exemplo)
        /// </summary>
        /// <param name="mensagem"></param>
        public static void GravaLog(string mensagem)
        {
            StreamWriter sw;

            string file = RT.appPath + 
                          Utils.FillZeros(DateTime.Today.Year.ToString(), 4) + 
                          Utils.FillZeros(DateTime.Today.Month.ToString(), 2) + 
                          Utils.PegaLinhaAtual(IMSI) + ".txt";

            if (File.Exists(file))
                sw = File.AppendText(file);
            else
                sw = File.CreateText(file);

            sw.WriteLine(Utils.FillZeros (DateTime.Now.Day.ToString(), 2) + "/" +
                         Utils.FillZeros(DateTime.Now.Month.ToString(), 2) + "/" +
                         Utils.FillZeros(DateTime.Now.Year.ToString(), 4) + ";" +
                         Utils.FillZeros(DateTime.Now.Hour.ToString(), 2) + ":" +
                         Utils.FillZeros(DateTime.Now.Minute.ToString(), 2) + ";" +                
                         mensagem);
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Envia um e-mail para o endereço de chamado
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static bool EnviaEMail(string mensagem)
        {
            try
            {
                EmailAccount account = (new OutlookSession()).EmailAccounts[RT.configuration.defaultEMailAccount];
                if (account != null)
                {
                    EmailMessage msg = new EmailMessage();

                    if (msg != null)
                    {
                        msg.Subject = mensagem;
                        msg.To.Add(new Recipient(emailChamado));

                        if (!emailCopia.Equals(""))
                            msg.To.Add(new Recipient(emailCopia));

                        msg.BodyText = mensagem;
                        msg.Importance = Importance.High;

                        try
                        {
                            if (Modelo == ATMModelo.Motorola)
                                NetworkISP.CreateGPRS("ATMSISTEMAS", "zap.vivo.com.br", "vivo", "vivo", true);

                            Tweak.GPRSConnectionStatus(false);
                            try
                            {
                                Debug.AddLog("SendEMail: antes de enviar a mensagem", true);
                                msg.Send(account);
                                Application.DoEvents();

                                Debug.AddLog("Sincronize: antes de sincronizar", true);
                                try
                                {
                                    MessagingApplication.Synchronize(account);
                                }
                                catch (Exception ex)
                                {
                                    Debug.AddLog("Sincronize: erro: " + ex.Message.ToString(), true);
                                }
                            }
                            finally
                            {
                                Tweak.GPRSConnectionStatus(true);
                                Debug.AddLog("Sincronize: depois de sincronizar", true);

                                if (Modelo == ATMModelo.Motorola)
                                    NetworkISP.RemoveGPRS("ATMSISTEMAS");
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.AddLog("SendEMail: error while sending email. Original message: " + e.Message, true);
                            return false;
                        }
                    }
                    else
                    {
                        Debug.AddLog("SendEMail: msg could not be created.", true);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.AddLog("SendEMail: Error: " + Utils.GetOnlyErrorMessage(e.Message));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Envia um SMS para o celular de chamado
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static bool EnviaSMS(string mensagem)
        {
            return SMSWrapper.SendSMS(numeroChamado, mensagem);
        }

        private static string MontaMensagem(ATMResultado motivo, ATMMotivosType nMotivo, string ip)
        {
            string currentIP;

            if (ip.Equals(""))
                currentIP = Utils.PegaIP();
            else
                currentIP = ip;

            // Regra: se estiver encaminhando uma mensagem e não tiver IP, envia o parâmetro X com o valor 4
            if (motivo == ATMResultado.NenhumaMensagem && (currentIP.Equals("") || currentIP.Equals("0")))
            {
                motivo = ATMResultado.ChipNaoConectaNaInternetOuNaoTemIP;
            }

            if (Proprietario == null)
                LeiaOwnerInfo();

            string mensagem =
                   ATM.Versao + ";" +
                   System.Convert.ToString((int)motivo) + ";" +
                   Utils.PegaLinhaAtual(IMSI) + ";" +
                   currentIP + ";" +
                   Proprietario.EMail + ";" +
                   ATM.ICCID + ";" +
                   ATM.IMEI + ";" +
                   System.Convert.ToString(Utils.BatteryMetter()) + ";" +
                   Proprietario.UserName + ";" +
                   ATM.Palm + ";" +
                   Proprietario.Phone + ";" +
                   Proprietario.Address.Replace("\\", " ") + ";" +
                   (nMotivo == ATMMotivosType.Nenhum ? ";" : System.Convert.ToString((int)nMotivo)) + ";";

            Debug.AddLog("Mensagem: " + mensagem, true);
            return mensagem;
        }

        /// <summary>
        /// Envia mensagem (E-Mail e SMS) além de informar um motivo textual
        /// </summary>
        /// <param name="motivo"></param>
        /// <param name="sMotivo"></param>
        /// <returns></returns>
        public static bool EnviaMensagem(ATMResultado motivo, ATMMotivosType nMotivo, TipoMensagem tipoMensagem)
        {
            // A pedido do Manoel, a partir da versão 1.0.0-10 o envio de mensagens de códigos 5,6,7,8,10 e 11 
            // são remetidos APENAS por E-Mail por motivo de economia
            if (motivo == ATMResultado.ErroEnvioSMS ||
                motivo == ATMResultado.EnvioSMSComSucesso ||
                motivo == ATMResultado.ErroEnvioSMSComChipAnterior ||
                motivo == ATMResultado.VerificacaoInicialComIP ||
                motivo == ATMResultado.AlimentacaoConectada ||
                motivo == ATMResultado.AlimentacaoDesconectada)
            {
                tipoMensagem = TipoMensagem.SomenteEMail;
            } 

            string ip = Utils.PegaIP();

            bool result = true;
            Debug.AddLog("EnviaMensagem. Tipo de mensagem: " + 
                (tipoMensagem == TipoMensagem.Nenhum ? "Nenhum" : 
                  (tipoMensagem == TipoMensagem.Ambos ? "Ambos" : 
                    (tipoMensagem == TipoMensagem.SomenteSMS ? "Somente SMS" : "Somente E-Mail"))) + " IP: " + ip, true);

            if (tipoMensagem != TipoMensagem.Nenhum)
            {
                string mensagem = MontaMensagem(motivo, nMotivo, ip);

                // Grava a mensagem no log para ser acessado no futuro
                GravaLog(mensagem);

                if (tipoMensagem == TipoMensagem.Ambos || tipoMensagem == TipoMensagem.SomenteSMS)
                {
                    // Envia por SMS
                    result = EnviaSMS(mensagem);
                    Debug.AddLog("EnviaMensagem: SMS ok? " + (result ? "sim" : "não"), true);

                    // Envia por EMail
                    if (ip != "0" && tipoMensagem == TipoMensagem.Ambos)
                        EnviaEMail(mensagem);
                }
                else
                    Debug.AddLog("EnviaMensagem: enviar somente e-mail", true);

                // REGRA: Se não conseguir encaminhar o SMS, então envia e-mail com o código 5. 
                // REGRA: Se não conseguir encaminhar o EMAIL, então guarda a mensagem com o código 7, para tentar enviar com outro chip.
                if (ip != "0" && tipoMensagem == TipoMensagem.Ambos)
                {
                    if (!result)
                    {
                        // Regra X=7
                        mensagem = MontaMensagem(ATMResultado.ErroEnvioSMSComChipAnterior, nMotivo, ip);
                        Configuracao.GuardaErroEnvioSMS(IMSI, mensagem);

                        // Regra X=5
                        mensagem = MontaMensagem(ATMResultado.ErroEnvioSMS, nMotivo, ip);
                    }
                    else
                    {
                        // REGRA: Se conseguir enviar o SMS, então envia e-mail com código 6
                        mensagem = MontaMensagem(ATMResultado.EnvioSMSComSucesso, nMotivo, ip);
                    }
                }

                // Envia por e-mail
                if (ip != "0" && (tipoMensagem == TipoMensagem.Ambos || tipoMensagem == TipoMensagem.SomenteEMail))
                    result = result || EnviaEMail(mensagem);
            }
            else
            {
                Debug.AddLog("Nada a fazer, abortando...", true);
            }

            return result;
        }

        /// <summary>
        /// Tenta enviar mensagem pendente
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public static bool EnviaMensagemPendente(string mensagem)
        {
            Debug.AddLog("EnviaMensagemPendente", true);
            // Grava a mensagem no log para ser acessado no futuro
            GravaLog(mensagem);

            Debug.AddLog("EnviaMensagemPendente. Enviando SMS.", true);
            bool smsok = EnviaSMS(mensagem);
            Debug.AddLog("EnviaMensagemPendente. Resultado: " + (smsok ? "sim" : "não"), true);

            if (!smsok)
                return false;

            Debug.AddLog("EnviaMensagemPendente. Enviando EMail.", true);
            bool emailok = EnviaEMail(mensagem);
            Debug.AddLog("EnviaMensagemPendente. Resultado: " + (emailok ? "sim" : "não"), true);
            
            return emailok;
        }

        /// <summary>
        /// Configura automaticamente o próximo envio de LOGs para o servidor de FTP da ATMSistemas
        /// </summary>
        public static void AgendaProximoEnvioLog()
        {
            int ano = DateTime.Today.Year;
            int mes = DateTime.Today.Month;
            int hora = 12;

            if (mes == 12)
            {
                mes = 1;
                ano++;
            }
            else
                mes++;

            DateTime proxExecucao = 
                DateTime.Parse("01/" + System.Convert.ToString(mes) + "/" + System.Convert.ToString(ano));

            if (!IMEI.Trim().Equals(""))
            {
                if (System.Convert.ToInt32(IMEI.Substring(IMEI.Length - 1)) % 2 != 0)
                    hora++;

                proxExecucao = proxExecucao + new TimeSpan(hora, 0, 0);

                Utils.CreateScheduledTask(proxExecucao, RT.appPath + "\\ATMWakeUp.exe");
                Debug.AddLog("AgendaProximoEnvioLog: Agendado para: " + proxExecucao.ToString(), true);
            }
            else
            {
                Debug.AddLog("AgendaProximoEnvioLog: IMEI inexistente!", true);
            }
        }

        public static void AgendaProximoEnvioDiario()
        {
            DateTime proxExecucao = DateTime.Today.AddDays(1);
            proxExecucao = proxExecucao + new TimeSpan(8, 0, 0);
//            DateTime proxExecucao = DateTime.Today;                      // Para testes
//            proxExecucao = proxExecucao + new TimeSpan(16, 28, 0);       // Para testes

            Debug.AddLog("AgendaProximoEnvioDiario: " + proxExecucao.ToString(), true);

            Utils.CreateScheduledTask(proxExecucao, RT.appPath + "\\atmdiario.exe");
        }

        public static void AgendaWakeUpApplication()
        {
            bool rv = Kernel.CeRunAppAtEvent(RT.appPath + "\\ATMConnVerify.exe", Kernel.NOTIFICATION_EVENT_WAKEUP);
            Debug.AddLog("AgendaWakeUpApplication: configurado", true);
        }

        /// <summary>
        /// Cria registro no AppToDate para configurar a atualização automática
        /// </summary>
        public static void CreateAppToDateLink()
        {
            RegistryKey r = Registry.CurrentUser.CreateSubKey("\\Software\\MoDaCo\\AppToDate\\XML");

            if (r != null)
            {
                r.SetValue("ATMSistemas", RT.appPath + "atmsistemas.xml");

                r.Close();
            }
        }

        /// <summary>
        /// Remove registro do AppToDate de atualização automática
        /// </summary>
        public static void RemoveAppToDateLink()
        {
            RegistryKey r = Registry.CurrentUser.OpenSubKey("\\Software\\MoDaCo\\AppToDate\\XML");

            if (r != null)
            {
                try
                {
                    r.DeleteSubKey("ATMSistemas");
                    r.Close();
                }
                catch { }
            }
        }

        public static bool DispositivoEmEstadoDeNotificacao()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey(@"\System\State\Shell\Notifications\{A6077028-C0C3-4C83-98EE-46F0B9DE7DD2}\1");
            bool result = false;

            if (r != null)
            {
                string value = (string)r.GetValue("TodaySK", "");

                if (value.ToLower().Equals("notificação") || value.ToLower().Equals("sim") ||
                    value.ToLower().Equals("notification") || value.ToLower().Equals("yes"))
                    result = true;

                r.Close();
            }

            Debug.AddLog("DispositivoEmEstadoDeNotificacao: " + (result ? "Sim" : "Não"), true);
            return result;
        }
    }
}