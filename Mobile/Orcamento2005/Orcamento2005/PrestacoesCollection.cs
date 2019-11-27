using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Orcamento2005
{
    class PrestacoesCollection : Collection<PrestacaoCollection>
    {
        public PrestacaoCollection FindByParcela(Decimal parcela)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.Items[i].Prestacao == parcela)
                {
                    return this.Items[i];
                }
            }

            return null;
        }

        public PrestacaoCollection GetItem(Decimal i)
        {
            if (i <= this.Count)
            {
                return this.Items[(int)i];
            }
            else
            {
                return null;
            }
        }
    }
}
