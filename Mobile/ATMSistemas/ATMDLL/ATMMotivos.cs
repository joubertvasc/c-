using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ATMDLL
{
    public class ATMMotivos
    {
        public ATMMotivosRecord[] Motivos;

        public ATMMotivos()
        {
            Motivos = new ATMMotivosRecord[6];
            Motivos[0] = new ATMMotivosRecord(ATMMotivosType.VivoNaoConecta, "VIVO da sinal e não coneta");
            Motivos[1] = new ATMMotivosRecord(ATMMotivosType.VivoSemSinal, "VIVO sem sinal");
            Motivos[2] = new ATMMotivosRecord(ATMMotivosType.TimNaoConecta, "TIM da sinal e não conecta");
            Motivos[3] = new ATMMotivosRecord(ATMMotivosType.TimSemSinal, "TIM sem sinal");
            Motivos[4] = new ATMMotivosRecord(ATMMotivosType.ClaroNaoConecta, "CLARO da sinal e não conecta");
            Motivos[5] = new ATMMotivosRecord(ATMMotivosType.ClaroSemSinal, "CLARO sem sinal");
        }
    }

    public enum ATMMotivosType
    {
        Nenhum = -1,
        VivoNaoConecta = 0,
        VivoSemSinal = 1,
        TimNaoConecta = 2,
        TimSemSinal = 3,
        ClaroNaoConecta = 4,
        ClaroSemSinal = 5,
        AlimentacaoConectada = 10,
        AlimentacaoDesconectada = 11
    }

    public class ATMMotivosRecord
    {
        private ATMMotivosType codigo;
        private string descricao;

        public ATMMotivosRecord(ATMMotivosType cod, string desc)
        {
            codigo = cod;
            descricao = desc;
        }

        public ATMMotivosType Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public override string ToString()
        {
            return Descricao;
        }
    }

}

// VIVO da sinal e não coneta
// VIVO sem sinal
// TIM da sinal e não conecta
// TIM sem sinal
// CLARO da sinal e não conecta
// CLARO sem sinal
