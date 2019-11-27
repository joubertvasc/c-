using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Orcamento2005
{
  class LancamentoItens: Collection<LancamentoItem>
  {
    public LancamentoItem GetItem(Decimal i)
    {
      if (i <= this.Count) {
        return this.Items[(int)i];
      } else {
        return null;
      }
    }
  }
}
