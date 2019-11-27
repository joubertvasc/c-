using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using ATMDLL;
using JVUtils;
using CommonDLL;

namespace ATMConnVerify
{
    class Program
    {
        static void Main(string[] args)
        {
            // Esconde o cursor
            Utils.HideWaitCursor();

            Debug.StartLog(ShellFolders.TempFolder + "\\ATMConnVerify.txt");
            Debug.SaveAfterEachAdd = true;
            Debug.Logging = true;

            Debug.AddLog("Iniciando...", true);

            Debug.AddLog("Criando rtCommon...", true);
            RTCommon rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase), false);
            ATM.RT = rtCommon;

            if (DateTime.Today != Configuracao.PegaUltimaVerificacaoInicial())
            {
                Debug.AddLog("ATM Versão: " + ATM.Versao, true);
                Debug.AddLog("JVUtils Versão: " + JVUtils.JVUtils.Version, true);
                Debug.AddLog("CommonDLL versão: " + ATM.RT.version, true);
                
                Debug.AddLog("Verificando conectividade do dispositivo.", true);
                bool notificacao = ATM.DispositivoEmEstadoDeNotificacao();
                string ip = Utils.PegaIP();
                bool ok = false;

                ATM.LeiaOwnerInfo();

                Debug.AddLog("IP encontrado: " + ip + ". Enviando mensagem...", true);
                if (ip.Equals("0"))
                    ok = ATM.EnviaMensagem(ATMResultado.VerificacaoInicialSemIP,
                                           ATMMotivosType.Nenhum,
                                           TipoMensagem.SomenteSMS);
                else
                    ok = ATM.EnviaMensagem(ATMResultado.VerificacaoInicialComIP,
                                           ATMMotivosType.Nenhum,
                                           (notificacao ? TipoMensagem.SomenteSMS : TipoMensagem.Ambos));

                Debug.AddLog("Mensagem enviada. Resultado: " + (ok ? "ok" : "erro"), true);

                if (ok)
                    Configuracao.GuardaUltimaVerificacaoInicial(DateTime.Today);
            }
            else
            {
                Debug.AddLog("Nada para fazer. O log de hoje já foi gerado.", true);
            }

            Debug.AddLog("Terminando...", true);
            Debug.SaveLog();
        }
    }
}
