using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ATMDLL
{
    public enum ATMResultado
    {
        NenhumaMensagem = 0,
        UsuarioTirouChipPrincipal = 1,
        UsuarioTirouChipContingente = 2,
        ChipNaoEhPrincipalNemContingente = 3,
        ChipNaoConectaNaInternetOuNaoTemIP = 4,
        ErroEnvioSMS = 5,
        EnvioSMSComSucesso = 6,
        ErroEnvioSMSComChipAnterior = 7,
        VerificacaoInicialComIP = 8,
        VerificacaoInicialSemIP = 9,
        AlimentacaoConectada = 10,
        AlimentacaoDesconectada = 11,
        RespostaComandoOperadora = 12
    }
}

// x=0 abertura normal de chamado 
// X=1 o usuário tirou o chip principal e colocou o contingente
// X=2 o usuário tirou o chip contingente e colocou o principal
// X=3 o usuário colocou um chip diferente do principal e diferente do contingente
// X=4 o chip inserido não conecta na internet ou não tem IP
// X=5 erro no envio do SMS, mensagem enviada por e-mail com sucesso
// X=6 envio de SMS com sucesso, e-mail não cadastrado.
// X=7 erro no envio do SMS e erro no envio da mensagem com o chip anterior ao atual.
// X=8 Verificação inicial com IP válido
// X=9 Verificação inicial sem IP válido