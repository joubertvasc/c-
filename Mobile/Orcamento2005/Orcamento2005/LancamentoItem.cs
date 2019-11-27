using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    class LancamentoItem
    {
      private int nTag;
      private String cdLancamento;
		 	private String deLancamento;
      private DateTime dtLancamento;
			private String cdTipoMovim;
			private String cdCentroCusto;
			private Decimal vlLancamento;
      private String cdConta;
      private DateTime dtQuitacao;
      private String cdContaFixa;
      private String cdCreditoFixo;
      private String cdPrestacao;
      private int nuParcela;
      private Decimal vlJuros;
      private Decimal vlDesconto;
      private String nuCheque;

      public int Tag
      {
        get { return nTag; }
        set { nTag = value; }
      }

      public String Lancamento
      {
        get { return cdLancamento; }
        set { cdLancamento = value; }
      }

      public String DescrLancamento
      {
        get { return deLancamento; }
        set { deLancamento = value; }
      }

      public DateTime Data
      {
        get { return dtLancamento; }
        set { dtLancamento = value; }
      }

      public String TipoMovim
      {
        get { return cdTipoMovim; }
        set { cdTipoMovim = value; }
      }

      public String CentroCusto
      {
        get { return cdCentroCusto; }
        set { cdCentroCusto = value; }
      }

      public Decimal Valor
      {
        get { return vlLancamento; }
        set { vlLancamento = value; }
      }

      public String Conta
      {
        get { return cdConta; }
        set { cdConta = value; }
      }

      public DateTime Quitacao
      {
        get { return dtQuitacao; }
        set { dtQuitacao = value; }
      }

      public String ContaFixa
      {
        get { return cdContaFixa; }
        set { cdContaFixa = value; }
      }

      public String Creditofixo
      {
        get { return cdCreditoFixo; }
        set { cdCreditoFixo = value; }
      }

      public String Prestacao
      {
        get { return cdPrestacao; }
        set { cdPrestacao = value; }
      }

      public int Parcela
      {
        get { return nuParcela; }
        set { nuParcela = value; }
      }

      public Decimal Juros
      {
        get { return vlJuros; }
        set { vlJuros = value; }
      }

      public Decimal Desconto
      {
        get { return vlDesconto; }
        set { vlDesconto = value; }
      }

      public String Cheque
      {
        get { return nuCheque; }
        set { nuCheque = value; }
      }      
    }
}
