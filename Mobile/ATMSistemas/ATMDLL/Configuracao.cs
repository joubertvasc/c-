using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using JVUtils;
using CommonDLL;

namespace ATMDLL
{
    public static class Configuracao
    {
        #region Variables
        static string _key = JVUtils.JVUtils.JVSoftwareKey + "\\ATM";
        static RTCommon rt;
        #endregion

        #region Properties
        public static string ATMKey
        {
            get { return _key; }
        }
        public static RTCommon RT
        {
            get { return rt; }
            set { rt = value; }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Grava no registro do Windows qual foi o último IMSI utilizado
        /// </summary>
        public static void SetUltimoCartaoUsado()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                PhoneInfo pi = new PhoneInfo();

                r.SetValue("UltimoIMSI", pi.GetIMSI());

                r.Close();
            }
        }

        /// <summary>
        /// Busca no registro do Windows qual foi o último IMSI utilizado
        /// </summary>
        /// <returns></returns>
        public static string GetUltimoCartaoUsado()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);
            string result = "";

            if (r != null)
            {
                PhoneInfo pi = new PhoneInfo();

                result = (string)r.GetValue("UltimoIMSI", pi.GetIMSI());

                r.Close();
            }

            return result;
        }

        /// <summary>
        /// Usado na remoção do ATMSistemas. Remove alguns itens do registro do Windows
        /// </summary>
        public static void RemoveRegistros()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(JVUtils.JVUtils.JVSoftwareKey);
            if (r != null)
            {
                try
                {
                    r.DeleteSubKey("ATM");
                }
                catch { }
                r.Close();
            }

            RegistryKey r2 = Registry.LocalMachine.CreateSubKey(RT.configuration.defaultRegistryKey);
            if (r2 != null)
            {
                try
                {
                    r2.DeleteValue("atm");
                }
                catch { }
                r2.Close();
            }
        }

        /// <summary>
        /// Configura o RemoteTracker para mudar o comportamento de acordo com as especificações do ATM
        /// </summary>
        public static void DefineRTcomoATM()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(RT.configuration.defaultRegistryKey);
            try
            {
                if (r != null)
                {
                    r.SetValue("atm", 1);
                }
            }
            finally
            {
                r.Close();
            }
        }

        /// <summary>
        /// Grava no registro do Windows onde encontrar o executável do ATM
        /// </summary>
        public static void DefineCaminhoATM()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                string path = Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase);
                path = path.Substring(0, path.LastIndexOf(@"\") + 1);

                r.SetValue("path", path);

                r.Close();
            }
        }

        /// <summary>
        /// Pega do registro do Windows o caminho para o executável do ATM
        /// </summary>
        /// <returns>Caminho para o executável do ATM ou String.Empty</returns>
        public static string PegaCaminhoATM()
        {
            string result = "";
            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);

            if (r != null)
            {
                result = r.GetValue("path", "").ToString();

                r.Close();
            }

            return result;
        }

        /// <summary>
        /// Grava no registro do Windows a pasta padrão para ser usada pelo comando AFTP do RemoteTracker
        /// </summary>
        /// <param name="path">Caminho</param>
        public static void DefineFTPFolder(string path)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                r.SetValue("ftpfolder", path);

                r.Close();
            }
        }

        /// <summary>
        /// Retorna a pasta utilizada pelo comando AFTP do RemoteTracker
        /// </summary>
        /// <returns>Caminho</returns>
        public static string PegaFTPFolder()
        {
            string result = "";
            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);

            if (r != null)
            {
                result = r.GetValue("ftpfolder", "").ToString();

                r.Close();
            }

            return result;
        }

        /// <summary>
        /// Salva no registro do Windows a última data de utilização de determinado motivo. Para uso do controle de solicitações diárias
        /// </summary>
        /// <param name="motivo">Código do motivo</param>
        /// <param name="data">Data da solicitação</param>
        public static void GuardaDataMotivo(ATMMotivosType motivo, DateTime data)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                r.SetValue(System.Convert.ToString((int)motivo), data.ToString());

                r.Close();
            }
        }

        /// <summary>
        /// Retorna a última data de utilização do motivo para controlar as solicitações diárias
        /// </summary>
        /// <param name="motivo">Código do Motivo</param>
        /// <returns>Data da última solicitação ou DateTime.MinValue caso nunca tenha sido executado</returns>
        public static DateTime PegaDataMotivo(ATMMotivosType motivo)
        {
            DateTime result = DateTime.MinValue;
            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);

            if (r != null)
            {
                result = System.Convert.ToDateTime(r.GetValue(System.Convert.ToString((int)motivo), 
                                                              DateTime.MinValue.ToString()).ToString());

                r.Close();
            }

            return result;
        }

        /// <summary>
        /// Para atender a regra do X=7, se der erro ao enviar um SMS, salva a mensagem no registro para tentar
        /// remeter a mensagem com outro SIM card.
        /// </summary>
        /// <param name="imsi">imsi do sim card com problemas</param>
        /// <param name="mensagem">mensagem a enviar</param>
        public static void GuardaErroEnvioSMS(string imsi, string mensagem)
        {
            Debug.AddLog("GuardaErroEnvioSMS: imsi=" + imsi + ", mensagem=" + mensagem, true);
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                r.SetValue("ErroSMS", imsi + "\\" + mensagem);
                r.Close();
                Debug.AddLog("GuardaErroEnvioSMS: mensagem armazenada", true);
            }
            else
            {
                Debug.AddLog("GuardaErroEnvioSMS: não foi possível armazenar a mensagem", true);
            }
        }

        /// <summary>
        /// Verifica se existe mensagem pendende de envio (regra X=7). Se existir retorna a mensagem para envio.
        /// </summary>
        /// <param name="imsi">IMSI do cartão instalado</param>
        /// <returns>Mensagem para envio caso exista</returns>
        public static string PegaErroEnvioSMS(string imsi)
        {
            Debug.AddLog("PegaErroEnvioSMS: imsi=" + imsi, true);
            string result = "";

            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);

            if (r != null)
            {
                string valor = r.GetValue("ErroSMS", "").ToString();

                if (!valor.Equals("") && valor.Contains("\\"))
                {
                    string i = valor.Substring(0, valor.IndexOf("\\"));
                    string m = valor.Substring(valor.IndexOf("\\") + 1);

                    Debug.AddLog("PegaErroEnvioSMS: existe mensagem de erro pendente: imsi=" + i + ", mensagem=" + m, true);

                    if (!i.Equals(imsi))
                    {
                        Debug.AddLog("PegaErroEnvioSMS: a mensagem deverá ser enviada pois o SIM card foi trocado.", true);
                        result = m;
                    }
                    else
                    {
                        Debug.AddLog("PegaErroEnvioSMS: a mensagem não será enviada pois o SIM card não foi trocado.", true);
                    }
                }
                else
                {
                    Debug.AddLog("PegaErroEnvioSMS: não existe mensagem de erro pendente de envio.", true);
                }
            }
            else
            {
                Debug.AddLog("PegaErroEnvioSMS: não foi possível ler o registro.", true);
            }

            return result;
        }

        /// <summary>
        /// Remove do registro do Windows a última mensagem de erro mal sucedida.
        /// </summary>
        public static void ApagaErroEnvioSMS()
        {
            Debug.AddLog("ApagaErroEnvioSMS: início", true);
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                try
                {
                    r.DeleteValue("ErroSMS");
                }
                catch { }
                r.Close();

                Debug.AddLog("ApagaErroEnvioSMS: mensagem apagada", true);
            }
            else
            {
                Debug.AddLog("ApagaErroEnvioSMS: não foi possível apagar a mensagem", true);
            }
        }

        /// <summary>
        /// Armazena no registro do windows quais são as pastas para o RemoteTracker usar o comando BFTP
        /// </summary>
        /// <param name="pasta1">caminho 1</param>
        /// <param name="pasta2">caminho 2</param>
        public static void GuardaFoldersBFTP(string pasta1, string pasta2)
        {
            Debug.AddLog("GuardaFoldersBFTP: pasta1=" + pasta1 + ", pasta2=" + pasta2, true);
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                r.SetValue("Pasta1", pasta1);
                r.SetValue("Pasta2", pasta2);
                r.Close();
                Debug.AddLog("GuardaFoldersBFTP: pastas armazenadas", true);
            }
            else
            {
                Debug.AddLog("GuardaFoldersBFTP: não foi possível armazenar as pastas", true);
            }
        }

        /// <summary>
        /// Retorna as pastas para o RemoteTracker usar o comando BFTP
        /// </summary>
        /// <param name="pasta1"></param>
        /// <param name="pasta2"></param>
        public static void PegaFoldersBFTP(ref string pasta1, ref string pasta2)
        {
            Debug.AddLog("PegaFoldersBFTP: iniciando", true);
            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);

            if (r != null)
            {
                pasta1 = r.GetValue("Pasta1", "").ToString();
                pasta2 = r.GetValue("Pasta2", "").ToString();
                r.Close();
                Debug.AddLog("PegaFoldersBFTP: pasta1=" + pasta1 + ", pasta2=" + pasta2, true);
            }
            else
            {
                Debug.AddLog("PegaFoldersBFTP: não foi possível encontrar as definições das pastas", true);
            }
        }

        /// <summary>
        /// Guarda no registro do Windows a última data que o SMSLauncher enviou informação de verificação inicial
        /// </summary>
        /// <param name="data">Data da última verificação</param>
        public static void GuardaUltimaVerificacaoInicial(DateTime data)
        {
            Debug.AddLog("GuardaUltimaVerificacaoInicial: data=" + data.ToString(), true);
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                r.SetValue("UltimaVerificacao", data.ToString());
                r.Close();
                Debug.AddLog("GuardaUltimaVerificacaoInicial: data armazenada", true);
            }
            else
            {
                Debug.AddLog("GuardaUltimaVerificacaoInicial: não foi possível armazenar a data", true);
            }
        }

        /// <summary>
        /// Busca do registro do Windows a data que o SMSLauncher enviou informações de verificação inicial pela última vez
        /// </summary>
        /// <returns>Data do envio da última verificação inicial</returns>
        public static DateTime PegaUltimaVerificacaoInicial()
        {
            Debug.AddLog("PegaUltimaVerificacaoInicial: iniciando", true);
            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);
            DateTime result = DateTime.MinValue;

            if (r != null)
            {
                result = System.Convert.ToDateTime(r.GetValue("UltimaVerificacao", DateTime.MinValue.ToString()).ToString());
                r.Close();
                Debug.AddLog("PegaUltimaVerificacaoInicial: data=" + result.ToString(), true);
            }
            else
            {
                Debug.AddLog("PegaUltimaVerificacaoInicial: não foi possível encontrar a data", true);
            }

            return result;
        }

        public static void SetVersao()
        {
            Debug.AddLog("SetVersao: versão=" + ATM.Versao, true);
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                r.SetValue("Versao", ATM.Versao);
                r.Close();
                Debug.AddLog("SetVersao: versão armazenada", true);
            }
            else
            {
                Debug.AddLog("SetVersao: não foi possível armazenar a versão", true);
            }
        }

        /// <summary>
        /// Grava no registro do Windows qual foi o último IMSI utilizado
        /// </summary>
        public static void SetPalm (string palm)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey);

            if (r != null)
            {
                PhoneInfo pi = new PhoneInfo();

                r.SetValue("PALM", palm);

                r.Close();
            }
        }

        /// <summary>
        /// Busca no registro do Windows qual foi o último IMSI utilizado
        /// </summary>
        /// <returns></returns>
        public static string GetPalm()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);
            string result = "";

            if (r != null)
            {
                result = (string)r.GetValue("PALM", "");

                r.Close();
            }

            return result;
        }

        /// <summary>
        /// Verifica se o envio de emails com coordenadas para rastreamento de servidores
        /// está ligado ou desligado.
        /// </summary>
        /// <returns>boolean</returns>
        public static bool GetRSState()
        {
            bool result = true;
            RegistryKey r = Registry.LocalMachine.OpenSubKey(ATMKey);

            if (r != null)
            {
                result = r.GetValue("rs", "1").ToString().Equals("1");

                r.Close();
            }

            return result;
        }

        public static void SaveConfig()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(ATMKey + "\\Config");

            if (r != null)
            {
                r.SetValue("AliasPrincipal", SimpleCryptography.Cryptography(ATM.aliasPrincipal));
                r.SetValue("AliasContingente", SimpleCryptography.Cryptography(ATM.aliasContingente));
                r.SetValue("EmailEmergencia", SimpleCryptography.Cryptography(ATM.emailEmergencia));
                r.SetValue("EmailChamado", SimpleCryptography.Cryptography(ATM.emailChamado));
                r.SetValue("EmailCopia", SimpleCryptography.Cryptography(ATM.emailCopia));
                r.SetValue("NumeroChamado", SimpleCryptography.Cryptography(ATM.numeroChamado));
                r.SetValue("NumeroBateria1", SimpleCryptography.Cryptography(ATM.numeroBateria1));
                r.SetValue("NumeroBateria2", SimpleCryptography.Cryptography(ATM.numeroBateria2));
                r.SetValue("Senha", SimpleCryptography.Cryptography(ATM.senha));
                r.SetValue("TempoEntreEnviosBateria", SimpleCryptography.Cryptography(ATM.tempoEntreEnviosBateria.ToString()));
                r.SetValue("ContaSMTP", SimpleCryptography.Cryptography(ATM.contaSMTP));
                r.SetValue("FTPServer", SimpleCryptography.Cryptography(ATM.ftpServer));
                r.SetValue("FTPUser", SimpleCryptography.Cryptography(ATM.ftpUser));
                r.SetValue("FTPPass", SimpleCryptography.Cryptography(ATM.ftpPass));
                r.SetValue("FTPRemoteDir", SimpleCryptography.Cryptography(ATM.ftpRemoteDir));
                r.SetValue("FTPLocalDir", SimpleCryptography.Cryptography(ATM.ftpLocalDir));
                r.SetValue("bFTPLocalDir1", SimpleCryptography.Cryptography(ATM.bftpLocalDir1));
                r.SetValue("bFTPLocalDir2", SimpleCryptography.Cryptography(ATM.bftpLocalDir2));
                r.SetValue("bFTPLocalDir3", SimpleCryptography.Cryptography(ATM.bftpLocalDir3));
                r.SetValue("FTPPort", SimpleCryptography.Cryptography(ATM.ftpPort.ToString()));
                r.SetValue("SMTPUserName", SimpleCryptography.Cryptography(ATM.smtpUserName));
                r.SetValue("SMTPName", SimpleCryptography.Cryptography(ATM.smtpName));
                r.SetValue("SMTPEMail", SimpleCryptography.Cryptography(ATM.smtpEMail));
                r.SetValue("SMTPInHost", SimpleCryptography.Cryptography(ATM.smtpInHost));
                r.SetValue("SMTPOutHost", SimpleCryptography.Cryptography(ATM.smtpOutHost));
                r.SetValue("SMTPPassword", SimpleCryptography.Cryptography(ATM.smtpPassword));
                r.SetValue("SMTPGUID", SimpleCryptography.Cryptography(ATM.smtpGUID));

                r.Close();
            }
        }

        #endregion
    }
}
