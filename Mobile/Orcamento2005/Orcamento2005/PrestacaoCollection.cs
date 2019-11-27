using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    class PrestacaoCollection
    {
        private Decimal prestacao;
        private Panel panelVisao;
        private Panel panelParcela;
        private TextBox tbNoParcela;
        private DateTimePicker dtParcela;
        private NumericTextBox.NumericTextBox vlParcela;
        private TextBox nuCheque;
        private String cdLancamento;

        public Decimal Prestacao
        {
            get { return prestacao; }
            set { prestacao = value; }
        }

        public Panel PanelVisao
        {
            get { return panelVisao; }
            set { panelVisao = value; }
        }

        public Panel PanelParcela
        {
            get { return panelParcela; }
            set { panelParcela = value; }
        }

        public TextBox NoParcela
        {
            get { return tbNoParcela; }
            set { tbNoParcela = value; }
        }

        public DateTimePicker DataParcela
        {
            get { return dtParcela; }
            set { dtParcela = value; }
        }

        public NumericTextBox.NumericTextBox ValorParcela
        {
            get { return vlParcela; }
            set { vlParcela = value; }
        }

        public TextBox Cheque
        {
            get { return nuCheque; }
            set { nuCheque = value; }
        }

        public String CDLancamento
        {
            get { return cdLancamento; }
            set { cdLancamento = value; }
        }
    }
}
